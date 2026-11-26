using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

/// <summary>
/// Devoluciones (formulario AVANZADO, encabezado + detalle).
/// 1) Se busca la venta por su número.
/// 2) Se marcan los productos y cuántas unidades devuelve el cliente
///    (nunca más de lo que compró, descontando devoluciones anteriores).
/// 3) Se devuelve el dinero con el precio histórico y, si se elige,
///    las unidades vuelven al inventario.
/// Las devoluciones no se editan ni se borran: son un comprobante.
/// </summary>
public class DevolucionesController : Controller
{
    private readonly AppDbContext _db;

    public DevolucionesController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Devoluciones
    public async Task<IActionResult> Index(string? buscar, string? periodo)
    {
        periodo = periodo is "hoy" or "semana" or "mes" ? periodo : "todas";
        var hoy = DateTime.Today;

        var consulta = _db.Devoluciones.AsNoTracking().AsQueryable();
        consulta = periodo switch
        {
            "hoy" => consulta.Where(d => d.Fecha >= hoy),
            "semana" => consulta.Where(d => d.Fecha >= hoy.AddDays(-6)),
            "mes" => consulta.Where(d => d.Fecha >= new DateTime(hoy.Year, hoy.Month, 1)),
            _ => consulta
        };

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            int.TryParse(t.TrimStart('#'), out var numero);
            consulta = consulta.Where(d =>
                d.DevolucionId == numero || d.VentaId == numero ||
                EF.Functions.Collate(d.Motivo, Colacion.SinAcentos).Contains(t) ||
                EF.Functions.Collate(d.Empleado!.Nombre, Colacion.SinAcentos).Contains(t) ||
                (d.Venta!.Cliente != null && EF.Functions.Collate(d.Venta.Cliente.Nombre, Colacion.SinAcentos).Contains(t)));
        }

        var lista = await consulta
            .OrderByDescending(d => d.Fecha)
            .Select(d => new DevolucionResumen
            {
                DevolucionId = d.DevolucionId,
                Fecha = d.Fecha,
                VentaId = d.VentaId,
                Cliente = d.Venta!.Cliente != null ? d.Venta.Cliente.Nombre : "Consumidor final",
                Motivo = d.Motivo,
                AtendidoPor = d.Empleado!.Nombre,
                Unidades = d.Detalles.Sum(x => (int?)x.Cantidad) ?? 0,
                Total = d.Detalles.Sum(x => (decimal?)(x.Cantidad * x.DetalleVenta!.PrecioUnitario)) ?? 0m,
                Reintegrado = d.Reintegrado
            })
            .ToListAsync();

        ViewData["Buscar"] = buscar;
        ViewData["Periodo"] = periodo;
        return View(lista);
    }

    // GET: /Devoluciones/Create?ventaId=5
    // Sin ventaId muestra el cuadro para buscar la venta.
    public async Task<IActionResult> Create(int? ventaId)
    {
        var f = new DevolucionFormulario();
        if (ventaId is > 0)
        {
            f.VentaId = ventaId.Value;
            var error = await CargarVenta(f.VentaId);
            if (error != null)
            {
                ModelState.AddModelError(nameof(f.VentaId), error);
                f.VentaId = 0;
            }
        }
        await CargarListas(f);
        return View(f);
    }

    // POST: /Devoluciones/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DevolucionFormulario f)
    {
        f.Lineas ??= new List<LineaDevolucion>();
        f.Motivo = (f.Motivo ?? string.Empty).Trim();
        f.Observacion = string.IsNullOrWhiteSpace(f.Observacion) ? null : f.Observacion.Trim();

        ModelState.Clear();
        TryValidateModel(f);

        if (f.Motivo.Length > 0 && !Devolucion.Motivos.Contains(f.Motivo))
            ModelState.AddModelError(nameof(f.Motivo), "Seleccione un motivo de la lista.");
        if (f.Motivo == "Otro" && f.Observacion == null)
            ModelState.AddModelError(nameof(f.Observacion), "Si el motivo es \"Otro\", explique en la observación.");
        if (f.EmpleadoId > 0 && !await _db.Empleados.AnyAsync(e => e.EmpleadoId == f.EmpleadoId && e.Activo))
            ModelState.AddModelError(nameof(f.EmpleadoId), "El empleado no existe o está inactivo.");

        var errorVenta = await CargarVenta(f.VentaId);
        if (errorVenta != null)
        {
            ModelState.AddModelError(nameof(f.VentaId), errorVenta);
            f.VentaId = 0;
            await CargarListas(f);
            return View(f);
        }

        var venta = (Venta)ViewData["Venta"]!;
        var yaDevuelto = (Dictionary<int, int>)ViewData["YaDevuelto"]!;
        var renglones = venta.Detalles.ToDictionary(d => d.DetalleVentaId);

        var lineas = f.Lineas.Where(l => l.Cantidad > 0).ToList();
        if (lineas.Count == 0)
            ModelState.AddModelError(string.Empty, "Indique al menos un producto y cuántas unidades se devuelven.");

        foreach (var l in lineas)
        {
            if (!renglones.TryGetValue(l.DetalleVentaId, out var r))
            {
                ModelState.AddModelError(string.Empty, "Uno de los productos no pertenece a esta venta.");
                continue;
            }
            var disponible = r.Cantidad - yaDevuelto.GetValueOrDefault(r.DetalleVentaId);
            if (l.Cantidad > disponible)
                ModelState.AddModelError(string.Empty,
                    $"\"{r.Producto?.Nombre}\": se pueden devolver como máximo {disponible} unidades.");
        }

        if (!ModelState.IsValid)
        {
            await CargarListas(f);
            return View(f);
        }

        await using var transaccion = await _db.Database.BeginTransactionAsync();
        var devolucion = new Devolucion
        {
            Fecha = DateTime.Now,
            VentaId = f.VentaId,
            EmpleadoId = f.EmpleadoId,
            Motivo = f.Motivo,
            Observacion = f.Observacion,
            Reintegrado = f.Reintegrar
        };
        foreach (var l in lineas)
        {
            devolucion.Detalles.Add(new DetalleDevolucion { DetalleVentaId = l.DetalleVentaId, Cantidad = l.Cantidad });
        }
        _db.Devoluciones.Add(devolucion);

        if (f.Reintegrar)
        {
            // Las unidades vuelven al stock para venderse otra vez
            var porProducto = lineas
                .GroupBy(l => renglones[l.DetalleVentaId].ProductoId)
                .ToDictionary(g => g.Key, g => g.Sum(l => l.Cantidad));
            var ids = porProducto.Keys.ToList();
            var productos = await _db.Productos.Where(p => ids.Contains(p.ProductoId)).ToListAsync();
            foreach (var p in productos) p.Stock += porProducto[p.ProductoId];
        }

        await _db.SaveChangesAsync();
        await transaccion.CommitAsync();

        var monto = lineas.Sum(l => l.Cantidad * renglones[l.DetalleVentaId].PrecioUnitario);
        TempData["Exito"] = $"Devolución #{devolucion.DevolucionId} registrada. Devolver al cliente: {monto:C}.";
        return RedirectToAction(nameof(Details), new { id = devolucion.DevolucionId });
    }

    // GET: /Devoluciones/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var devolucion = await _db.Devoluciones
            .AsNoTracking()
            .Include(d => d.Empleado)
            .Include(d => d.Venta).ThenInclude(v => v!.Cliente)
            .Include(d => d.Detalles).ThenInclude(x => x.DetalleVenta).ThenInclude(dv => dv!.Producto)
            .FirstOrDefaultAsync(d => d.DevolucionId == id);
        return devolucion == null ? NotFound() : View(devolucion);
    }

    // ---------- Apoyo ----------

    // Carga la venta y lo ya devuelto de cada renglón. Devuelve un mensaje si no se puede usar.
    private async Task<string?> CargarVenta(int ventaId)
    {
        if (ventaId <= 0) return "Indique el número de venta.";

        var venta = await _db.Ventas
            .AsNoTracking()
            .Include(v => v.Cliente)
            .Include(v => v.Detalles).ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(v => v.VentaId == ventaId);

        if (venta == null) return $"No existe la venta #{ventaId}.";
        if (venta.Anulada) return $"La venta #{ventaId} está anulada; no se pueden hacer devoluciones sobre ella.";

        var idsRenglones = venta.Detalles.Select(d => d.DetalleVentaId).ToList();
        var yaDevuelto = await _db.DetallesDevolucion
            .Where(x => idsRenglones.Contains(x.DetalleVentaId))
            .GroupBy(x => x.DetalleVentaId)
            .Select(g => new { g.Key, Total = g.Sum(x => x.Cantidad) })
            .ToDictionaryAsync(x => x.Key, x => x.Total);

        if (venta.Detalles.All(d => d.Cantidad - yaDevuelto.GetValueOrDefault(d.DetalleVentaId) <= 0))
            return $"Todos los productos de la venta #{ventaId} ya fueron devueltos.";

        ViewData["Venta"] = venta;
        ViewData["YaDevuelto"] = yaDevuelto;
        return null;
    }

    private async Task CargarListas(DevolucionFormulario f)
    {
        ViewData["Empleados"] = new SelectList(
            await _db.Empleados.AsNoTracking().Where(e => e.Activo).OrderBy(e => e.Nombre).ToListAsync(),
            nameof(Empleado.EmpleadoId), nameof(Empleado.Nombre), f.EmpleadoId);
        ViewData["Motivos"] = new SelectList(Devolucion.Motivos, f.Motivo);
    }
}
