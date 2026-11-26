using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

/// <summary>
/// Ventas (formulario AVANZADO).
/// - Create: la "caja": se arma la venta con varios productos.
/// - Index: historial con búsqueda y filtro por fecha.
/// - Details: el ticket de la venta.
/// - Anular: en vez de borrar, se anula y se devuelve el stock.
/// Las ventas no se editan: igual que en un supermercado real, si algo
/// salió mal se anula la venta y se registra una nueva.
/// </summary>
public class VentasController : Controller
{
    private const int MaxLineas = 100;
    private const int MaxCantidad = 10000;
    private readonly AppDbContext _db;

    public VentasController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Ventas  (historial)
    public async Task<IActionResult> Index(string? buscar, string? periodo)
    {
        periodo = periodo is "hoy" or "semana" or "mes" ? periodo : "todas";
        var hoy = DateTime.Today;

        var consulta = _db.Ventas.AsNoTracking().AsQueryable();
        consulta = periodo switch
        {
            "hoy" => consulta.Where(v => v.Fecha >= hoy),
            "semana" => consulta.Where(v => v.Fecha >= hoy.AddDays(-6)),
            "mes" => consulta.Where(v => v.Fecha >= new DateTime(hoy.Year, hoy.Month, 1)),
            _ => consulta
        };

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            int.TryParse(t.TrimStart('#'), out var numero);
            consulta = consulta.Where(v =>
                v.VentaId == numero ||
                (v.Cliente != null && EF.Functions.Collate(v.Cliente.Nombre, Colacion.SinAcentos).Contains(t)) ||
                EF.Functions.Collate(v.Empleado!.Nombre, Colacion.SinAcentos).Contains(t) ||
                EF.Functions.Collate(v.MetodoPago!.Nombre, Colacion.SinAcentos).Contains(t));
        }

        var ventas = await consulta
            .OrderByDescending(v => v.Fecha)
            .Select(v => new VentaResumen
            {
                VentaId = v.VentaId,
                Fecha = v.Fecha,
                Cliente = v.Cliente != null ? v.Cliente.Nombre : "Consumidor final",
                Cajero = v.Empleado!.Nombre,
                MetodoPago = v.MetodoPago!.Nombre,
                Articulos = v.Detalles.Sum(d => (int?)d.Cantidad) ?? 0,
                Total = v.Detalles.Sum(d => (decimal?)(d.Cantidad * d.PrecioUnitario)) ?? 0m,
                Anulada = v.Anulada
            })
            .ToListAsync();

        ViewData["Buscar"] = buscar;
        ViewData["Periodo"] = periodo;
        return View(ventas);
    }

    // GET: /Ventas/Create  (la caja)
    public async Task<IActionResult> Create()
    {
        await CargarListas(null);
        return View(new VentaFormulario());
    }

    // POST: /Ventas/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VentaFormulario formulario)
    {
        formulario.Lineas ??= new List<LineaVenta>();

        // 1) Validar encabezado
        ModelState.Clear();
        TryValidateModel(formulario);

        if (formulario.EmpleadoId > 0 &&
            !await _db.Empleados.AnyAsync(e => e.EmpleadoId == formulario.EmpleadoId && e.Activo))
            ModelState.AddModelError(nameof(formulario.EmpleadoId), "El cajero no existe o está inactivo.");

        if (formulario.MetodoPagoId > 0 &&
            !await _db.MetodosPago.AnyAsync(m => m.MetodoPagoId == formulario.MetodoPagoId && m.Activo))
            ModelState.AddModelError(nameof(formulario.MetodoPagoId), "El método de pago no existe o no está disponible.");

        if (formulario.ClienteId is > 0 &&
            !await _db.Clientes.AnyAsync(c => c.ClienteId == formulario.ClienteId))
            ModelState.AddModelError(nameof(formulario.ClienteId), "El cliente seleccionado no existe.");
        if (formulario.ClienteId is <= 0) formulario.ClienteId = null;

        // 2) Validar renglones (se juntan productos repetidos)
        var lineas = formulario.Lineas
            .Where(l => l.ProductoId > 0)
            .GroupBy(l => l.ProductoId)
            .Select(g => new LineaVenta { ProductoId = g.Key, Cantidad = g.Sum(l => l.Cantidad) })
            .ToList();

        if (lineas.Count == 0)
            ModelState.AddModelError(string.Empty, "Agregue al menos un producto a la venta.");
        if (lineas.Count > MaxLineas)
            ModelState.AddModelError(string.Empty, $"Una venta no puede tener más de {MaxLineas} productos distintos.");
        if (lineas.Any(l => l.Cantidad < 1 || l.Cantidad > MaxCantidad))
            ModelState.AddModelError(string.Empty, $"Cada cantidad debe estar entre 1 y {MaxCantidad}.");

        if (!ModelState.IsValid)
        {
            await CargarListas(formulario);
            return View(formulario);
        }

        // 3) Guardar todo en una transacción: si algo falla, no se guarda nada
        await using var transaccion = await _db.Database.BeginTransactionAsync();

        var ids = lineas.Select(l => l.ProductoId).ToList();
        var productos = await _db.Productos.Where(p => ids.Contains(p.ProductoId)).ToDictionaryAsync(p => p.ProductoId);
        var descuentos = await Precios.DescuentosVigentesAsync(_db, ids);   // promociones vigentes hoy

        foreach (var l in lineas)
        {
            if (!productos.TryGetValue(l.ProductoId, out var p))
                ModelState.AddModelError(string.Empty, "Uno de los productos ya no existe. Quítelo de la venta.");
            else if (l.Cantidad > p.Stock)
                ModelState.AddModelError(string.Empty, $"Stock insuficiente de \"{p.Nombre}\": hay {p.Stock} y se piden {l.Cantidad}.");
        }

        if (!ModelState.IsValid)
        {
            await transaccion.RollbackAsync();
            await CargarListas(formulario);
            return View(formulario);
        }

        var venta = new Venta
        {
            Fecha = DateTime.Now,
            ClienteId = formulario.ClienteId,
            EmpleadoId = formulario.EmpleadoId,
            MetodoPagoId = formulario.MetodoPagoId
        };

        foreach (var l in lineas)
        {
            var p = productos[l.ProductoId];
            var descuento = descuentos.GetValueOrDefault(p.ProductoId);
            venta.Detalles.Add(new DetalleVenta
            {
                ProductoId = p.ProductoId,
                Cantidad = l.Cantidad,
                DescuentoPorcentaje = descuento,
                PrecioUnitario = Precios.ConDescuento(p.Precio, descuento)   // precio histórico (ya con descuento)
            });
            p.Stock -= l.Cantidad;            // se descuenta del inventario
        }

        _db.Ventas.Add(venta);
        try
        {
            await _db.SaveChangesAsync();
            await transaccion.CommitAsync();
        }
        catch (DbUpdateException)
        {
            await transaccion.RollbackAsync();
            _db.ChangeTracker.Clear();
            ModelState.AddModelError(string.Empty, "No se pudo registrar la venta. Revise el stock e intente de nuevo.");
            await CargarListas(formulario);
            return View(formulario);
        }

        TempData["Exito"] = $"Venta #{venta.VentaId} registrada por {venta.Total:C}.";
        return RedirectToAction(nameof(Details), new { id = venta.VentaId });
    }

    // GET: /Ventas/Details/5  (ticket)
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var venta = await CargarVenta(id.Value);
        if (venta == null) return NotFound();

        // Devoluciones hechas sobre esta venta (se muestran debajo del ticket)
        ViewData["Devoluciones"] = await _db.Devoluciones
            .AsNoTracking()
            .Where(d => d.VentaId == venta.VentaId)
            .OrderBy(d => d.Fecha)
            .Select(d => new DevolucionResumen
            {
                DevolucionId = d.DevolucionId,
                Fecha = d.Fecha,
                Motivo = d.Motivo,
                Unidades = d.Detalles.Sum(x => x.Cantidad),
                Total = d.Detalles.Sum(x => x.Cantidad * x.DetalleVenta!.PrecioUnitario)
            })
            .ToListAsync();
        return View(venta);
    }

    // GET: /Ventas/Anular/5  (confirmación)
    public async Task<IActionResult> Anular(int? id)
    {
        if (id == null) return NotFound();
        var venta = await CargarVenta(id.Value);
        if (venta == null) return NotFound();
        if (venta.Anulada)
        {
            TempData["Error"] = $"La venta #{venta.VentaId} ya estaba anulada.";
            return RedirectToAction(nameof(Details), new { id });
        }
        if (await _db.Devoluciones.AnyAsync(d => d.VentaId == venta.VentaId))
        {
            TempData["Error"] = $"La venta #{venta.VentaId} tiene devoluciones registradas y ya no se puede anular.";
            return RedirectToAction(nameof(Details), new { id });
        }
        return View(venta);
    }

    // POST: /Ventas/Anular/5
    [HttpPost, ActionName("Anular")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AnularConfirmado(int id)
    {
        await using var transaccion = await _db.Database.BeginTransactionAsync();

        var venta = await _db.Ventas
            .Include(v => v.Detalles).ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(v => v.VentaId == id);

        if (venta == null)
        {
            TempData["Error"] = "La venta ya no existe.";
            return RedirectToAction(nameof(Index));
        }
        if (venta.Anulada)
        {
            TempData["Error"] = $"La venta #{id} ya estaba anulada.";
            return RedirectToAction(nameof(Details), new { id });
        }
        if (await _db.Devoluciones.AnyAsync(d => d.VentaId == id))
        {
            TempData["Error"] = $"La venta #{id} tiene devoluciones registradas y ya no se puede anular.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // Se devuelve al inventario lo que se había vendido
        foreach (var d in venta.Detalles)
        {
            if (d.Producto != null) d.Producto.Stock += d.Cantidad;
        }
        venta.Anulada = true;

        await _db.SaveChangesAsync();
        await transaccion.CommitAsync();

        TempData["Exito"] = $"Venta #{id} anulada. El stock de sus productos fue devuelto.";
        return RedirectToAction(nameof(Details), new { id });
    }

    // ---------- Apoyo ----------
    private Task<Venta?> CargarVenta(int id) =>
        _db.Ventas
            .AsNoTracking()
            .Include(v => v.Cliente)
            .Include(v => v.Empleado)
            .Include(v => v.MetodoPago)
            .Include(v => v.Detalles).ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(v => v.VentaId == id);

    private async Task CargarListas(VentaFormulario? f)
    {
        ViewData["Clientes"] = new SelectList(
            await _db.Clientes.AsNoTracking().OrderBy(c => c.Nombre).ToListAsync(),
            nameof(Cliente.ClienteId), nameof(Cliente.Nombre), f?.ClienteId);

        ViewData["Cajeros"] = new SelectList(
            await _db.Empleados.AsNoTracking().Where(e => e.Activo).OrderBy(e => e.Nombre).ToListAsync(),
            nameof(Empleado.EmpleadoId), nameof(Empleado.Nombre), f?.EmpleadoId);

        ViewData["Metodos"] = new SelectList(
            await _db.MetodosPago.AsNoTracking().Where(m => m.Activo).OrderBy(m => m.Nombre).ToListAsync(),
            nameof(MetodoPago.MetodoPagoId), nameof(MetodoPago.Nombre), f?.MetodoPagoId);

        // Catálogo de productos para la caja (precio, descuento vigente y stock actuales)
        var descuentos = await Precios.DescuentosVigentesAsync(_db);
        var catalogo = await _db.Productos
            .AsNoTracking()
            .OrderBy(p => p.Nombre)
            .Select(p => new { p.ProductoId, p.Nombre, p.Codigo, p.Precio, p.Stock, Categoria = p.Categoria!.Nombre })
            .ToListAsync();
        ViewData["Productos"] = catalogo.Select(p => new
        {
            p.ProductoId, p.Nombre, p.Codigo, p.Precio, p.Stock, p.Categoria,
            Descuento = descuentos.GetValueOrDefault(p.ProductoId),
            PrecioFinal = Precios.ConDescuento(p.Precio, descuentos.GetValueOrDefault(p.ProductoId))
        }).ToList();
    }
}
