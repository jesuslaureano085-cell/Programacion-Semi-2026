using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

/// <summary>
/// Compras a proveedores (formulario AVANZADO, encabezado + detalle).
/// - Create: se registra la factura con varios productos; el stock SUBE.
/// - Index: historial con búsqueda y período.
/// - Details: comprobante de la compra.
/// - Anular: la compra queda en el historial y el stock BAJA (si todavía alcanza).
/// </summary>
public class ComprasController : Controller
{
    private const int MaxLineas = 100;
    private readonly AppDbContext _db;

    public ComprasController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Compras  (historial)
    public async Task<IActionResult> Index(string? buscar, string? periodo)
    {
        periodo = periodo is "hoy" or "semana" or "mes" ? periodo : "todas";
        var hoy = DateTime.Today;

        var consulta = _db.Compras.AsNoTracking().AsQueryable();
        consulta = periodo switch
        {
            "hoy" => consulta.Where(c => c.Fecha >= hoy),
            "semana" => consulta.Where(c => c.Fecha >= hoy.AddDays(-6)),
            "mes" => consulta.Where(c => c.Fecha >= new DateTime(hoy.Year, hoy.Month, 1)),
            _ => consulta
        };

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            int.TryParse(t.TrimStart('#'), out var numero);
            consulta = consulta.Where(c =>
                c.CompraId == numero ||
                c.NumeroFactura.Contains(t) ||
                EF.Functions.Collate(c.Proveedor!.Nombre, Colacion.SinAcentos).Contains(t) ||
                EF.Functions.Collate(c.Empleado!.Nombre, Colacion.SinAcentos).Contains(t));
        }

        var compras = await consulta
            .OrderByDescending(c => c.Fecha)
            .Select(c => new CompraResumen
            {
                CompraId = c.CompraId,
                Fecha = c.Fecha,
                Proveedor = c.Proveedor!.Nombre,
                NumeroFactura = c.NumeroFactura,
                RecibidoPor = c.Empleado!.Nombre,
                Unidades = c.Detalles.Sum(d => (int?)d.Cantidad) ?? 0,
                Total = c.Detalles.Sum(d => (decimal?)(d.Cantidad * d.CostoUnitario)) ?? 0m,
                Anulada = c.Anulada
            })
            .ToListAsync();

        ViewData["Buscar"] = buscar;
        ViewData["Periodo"] = periodo;
        return View(compras);
    }

    // GET: /Compras/Create
    // Con ?proveedorId=3&sugerida=true el Asistente IA llena la compra con lo que conviene pedir.
    public async Task<IActionResult> Create(int? proveedorId, bool sugerida = false)
    {
        var f = new CompraFormulario();
        if (sugerida && proveedorId is > 0)
        {
            var sugerencias = (await PrediccionInventario.CalcularAsync(_db))
                .Where(p => p.ProveedorId == proveedorId && p.CantidadSugerida > 0)
                .ToList();
            var ids = sugerencias.Select(s => s.ProductoId).ToList();

            // Costo: el último que se pagó; si nunca se compró, el 70 % del precio de venta
            var ultimosCostos = (await _db.DetallesCompra
                    .Where(d => ids.Contains(d.ProductoId) && !d.Compra!.Anulada)
                    .Select(d => new { d.ProductoId, d.Compra!.Fecha, d.CostoUnitario })
                    .ToListAsync())
                .GroupBy(x => x.ProductoId)
                .ToDictionary(g => g.Key, g => g.OrderByDescending(x => x.Fecha).First().CostoUnitario);

            f.ProveedorId = proveedorId.Value;
            f.Observacion = "Compra sugerida por el Asistente IA";
            f.Lineas = sugerencias.Select(s => new LineaCompra
            {
                ProductoId = s.ProductoId,
                Cantidad = s.CantidadSugerida,
                CostoUnitario = ultimosCostos.TryGetValue(s.ProductoId, out var c) ? c : Math.Max(0.01m, Math.Round(s.Precio * 0.7m, 2))
            }).ToList();

            ViewData["Sugerida"] = f.Lineas.Count > 0
                ? $"El Asistente IA preparó {f.Lineas.Count} producto(s) para cubrir {PrediccionInventario.DiasCobertura} días de ventas. Revise cantidades y costos, escriba el número de factura y registre."
                : "El Asistente IA no encontró productos de este proveedor que necesiten compra.";
        }
        await CargarListas(f);
        return View(f);
    }

    // POST: /Compras/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CompraFormulario f)
    {
        f.Lineas ??= new List<LineaCompra>();
        f.NumeroFactura = (f.NumeroFactura ?? string.Empty).Trim().ToUpperInvariant();
        f.Observacion = string.IsNullOrWhiteSpace(f.Observacion) ? null : f.Observacion.Trim();

        // 1) Encabezado
        ModelState.Clear();
        TryValidateModel(f);

        if (f.ProveedorId > 0 && !await _db.Proveedores.AnyAsync(p => p.ProveedorId == f.ProveedorId))
            ModelState.AddModelError(nameof(f.ProveedorId), "El proveedor no existe.");
        if (f.EmpleadoId > 0 && !await _db.Empleados.AnyAsync(e => e.EmpleadoId == f.EmpleadoId && e.Activo))
            ModelState.AddModelError(nameof(f.EmpleadoId), "El empleado no existe o está inactivo.");
        if (f.NumeroFactura.Length > 0 && f.ProveedorId > 0 &&
            await _db.Compras.AnyAsync(c => c.ProveedorId == f.ProveedorId && c.NumeroFactura == f.NumeroFactura && !c.Anulada))
            ModelState.AddModelError(nameof(f.NumeroFactura), "Esa factura de ese proveedor ya fue registrada.");

        // 2) Renglones
        var lineas = f.Lineas.Where(l => l.ProductoId > 0).ToList();
        if (lineas.Count == 0)
            ModelState.AddModelError(string.Empty, "Agregue al menos un producto a la compra.");
        if (lineas.Count > MaxLineas)
            ModelState.AddModelError(string.Empty, $"Una compra no puede tener más de {MaxLineas} productos distintos.");
        if (lineas.GroupBy(l => l.ProductoId).Any(g => g.Count() > 1))
            ModelState.AddModelError(string.Empty, "Hay un producto repetido. Junte las cantidades en una sola línea.");
        if (lineas.Any(l => l.Cantidad < 1 || l.Cantidad > 100000))
            ModelState.AddModelError(string.Empty, "Cada cantidad debe estar entre 1 y 100,000.");
        if (lineas.Any(l => l.CostoUnitario < 0.01m || l.CostoUnitario > 999999.99m))
            ModelState.AddModelError(string.Empty, "Cada costo unitario debe ser mayor que cero.");

        var ids = lineas.Select(l => l.ProductoId).Distinct().ToList();
        var productos = await _db.Productos.Where(p => ids.Contains(p.ProductoId)).ToDictionaryAsync(p => p.ProductoId);
        foreach (var l in lineas)
        {
            if (!productos.TryGetValue(l.ProductoId, out var p))
                ModelState.AddModelError(string.Empty, "Uno de los productos ya no existe. Quítelo de la compra.");
            else if (f.ProveedorId > 0 && p.ProveedorId != f.ProveedorId)
                ModelState.AddModelError(string.Empty, $"\"{p.Nombre}\" no pertenece al proveedor seleccionado.");
        }

        if (!ModelState.IsValid)
        {
            await CargarListas(f);
            return View(f);
        }

        // 3) Guardar y subir el stock, todo junto
        await using var transaccion = await _db.Database.BeginTransactionAsync();
        var compra = new Compra
        {
            Fecha = DateTime.Now,
            ProveedorId = f.ProveedorId,
            EmpleadoId = f.EmpleadoId,
            NumeroFactura = f.NumeroFactura,
            Observacion = f.Observacion
        };
        foreach (var l in lineas)
        {
            compra.Detalles.Add(new DetalleCompra
            {
                ProductoId = l.ProductoId,
                Cantidad = l.Cantidad,
                CostoUnitario = Math.Round(l.CostoUnitario, 2)
            });
            productos[l.ProductoId].Stock += l.Cantidad;
        }
        _db.Compras.Add(compra);

        try
        {
            await _db.SaveChangesAsync();
            await transaccion.CommitAsync();
        }
        catch (DbUpdateException)
        {
            await transaccion.RollbackAsync();
            _db.ChangeTracker.Clear();
            ModelState.AddModelError(string.Empty, "No se pudo registrar la compra. Revise que la factura no esté repetida.");
            await CargarListas(f);
            return View(f);
        }

        TempData["Exito"] = $"Compra #{compra.CompraId} registrada por {compra.Total:C}. El stock fue actualizado.";
        return RedirectToAction(nameof(Details), new { id = compra.CompraId });
    }

    // GET: /Compras/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var compra = await CargarCompra(id.Value);
        return compra == null ? NotFound() : View(compra);
    }

    // GET: /Compras/Anular/5
    public async Task<IActionResult> Anular(int? id)
    {
        if (id == null) return NotFound();
        var compra = await CargarCompra(id.Value);
        if (compra == null) return NotFound();
        if (compra.Anulada)
        {
            TempData["Error"] = $"La compra #{compra.CompraId} ya estaba anulada.";
            return RedirectToAction(nameof(Details), new { id });
        }
        ViewData["Faltantes"] = Faltantes(compra);
        return View(compra);
    }

    // POST: /Compras/Anular/5
    [HttpPost, ActionName("Anular")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AnularConfirmado(int id)
    {
        await using var transaccion = await _db.Database.BeginTransactionAsync();
        var compra = await _db.Compras
            .Include(c => c.Detalles).ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(c => c.CompraId == id);

        if (compra == null)
        {
            TempData["Error"] = "La compra ya no existe.";
            return RedirectToAction(nameof(Index));
        }
        if (compra.Anulada)
        {
            TempData["Error"] = $"La compra #{id} ya estaba anulada.";
            return RedirectToAction(nameof(Details), new { id });
        }

        var faltantes = Faltantes(compra);
        if (faltantes.Count > 0)
        {
            TempData["Error"] = "No se puede anular: ya se vendió parte de esta mercadería (" + string.Join(", ", faltantes) + ").";
            return RedirectToAction(nameof(Details), new { id });
        }

        foreach (var d in compra.Detalles) d.Producto!.Stock -= d.Cantidad;   // la mercadería sale del inventario
        compra.Anulada = true;
        await _db.SaveChangesAsync();
        await transaccion.CommitAsync();

        TempData["Exito"] = $"Compra #{id} anulada. Se retiraron sus unidades del inventario.";
        return RedirectToAction(nameof(Details), new { id });
    }

    // ---------- Apoyo ----------

    // Productos que ya no tienen suficiente stock para "deshacer" la compra
    private static List<string> Faltantes(Compra c) =>
        c.Detalles
            .Where(d => d.Producto != null && d.Producto.Stock < d.Cantidad)
            .Select(d => $"{d.Producto!.Nombre}: hay {d.Producto.Stock} y la compra trajo {d.Cantidad}")
            .ToList();

    private Task<Compra?> CargarCompra(int id) =>
        _db.Compras
            .AsNoTracking()
            .Include(c => c.Proveedor)
            .Include(c => c.Empleado)
            .Include(c => c.Detalles).ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(c => c.CompraId == id);

    private async Task CargarListas(CompraFormulario? f)
    {
        ViewData["Proveedores"] = new SelectList(
            await _db.Proveedores.AsNoTracking().OrderBy(p => p.Nombre).ToListAsync(),
            nameof(Proveedor.ProveedorId), nameof(Proveedor.Nombre), f?.ProveedorId);

        ViewData["Empleados"] = new SelectList(
            await _db.Empleados.AsNoTracking().Where(e => e.Activo).OrderBy(e => e.Nombre).ToListAsync(),
            nameof(Empleado.EmpleadoId), nameof(Empleado.Nombre), f?.EmpleadoId);

        // Catálogo para la pantalla: incluye el último costo pagado (para sugerirlo)
        ViewData["Productos"] = await _db.Productos
            .AsNoTracking()
            .OrderBy(p => p.Nombre)
            .Select(p => new
            {
                p.ProductoId,
                p.Nombre,
                p.Codigo,
                p.ProveedorId,
                p.Stock,
                p.Precio,
                UltimoCosto = _db.DetallesCompra
                    .Where(d => d.ProductoId == p.ProductoId && !d.Compra!.Anulada)
                    .OrderByDescending(d => d.Compra!.Fecha)
                    .Select(d => (decimal?)d.CostoUnitario)
                    .FirstOrDefault()
            })
            .ToListAsync();
    }
}
