using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

/// <summary>
/// Asistente IA: predicción de inventario (ML.NET), alertas para el escritorio,
/// compra sugerida y generador de datos de demostración.
/// </summary>
public class AsistenteController : Controller
{
    private readonly AppDbContext _db;
    public AsistenteController(AppDbContext db) => _db = db;

    public const string PrefijoDemo = "DEMO-";

    // GET: Asistente
    public async Task<IActionResult> Index(string? buscar)
    {
        var predicciones = await PrediccionInventario.CalcularAsync(_db);
        ViewData["Buscar"] = buscar;
        ViewData["HayDemo"] = await _db.Compras.AnyAsync(c => c.NumeroFactura.StartsWith(PrefijoDemo));
        return View(predicciones);
    }

    // GET: Asistente/Alertas  (lo consulta el escritorio para mostrar un globo de aviso)
    [HttpGet]
    public async Task<IActionResult> Alertas()
    {
        var urgentes = (await PrediccionInventario.CalcularAsync(_db)).Where(p => p.Urgente).ToList();
        return Json(new
        {
            total = urgentes.Count,
            productos = urgentes.Take(3).Select(p => new { nombre = p.Producto, dias = p.DiasParaAgotarse, stock = p.Stock, sugerido = p.CantidadSugerida })
        });
    }

    // GET: Asistente/DatosDemo  (pantalla de confirmación)
    public async Task<IActionResult> DatosDemo()
    {
        ViewData["Bloqueo"] = await MotivoBloqueoDemo();
        ViewData["Productos"] = await _db.Productos.CountAsync();
        return View();
    }

    // POST: Asistente/DatosDemo
    // Crea 90 días de historial (ventas diarias y compras semanales) para que la IA tenga de dónde aprender.
    // Las compras reponen exactamente lo vendido, así el stock ACTUAL de cada producto no cambia.
    [HttpPost, ActionName("DatosDemo")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DatosDemoConfirmado()
    {
        var bloqueo = await MotivoBloqueoDemo();
        if (bloqueo != null)
        {
            TempData["Error"] = bloqueo;
            return RedirectToAction(nameof(DatosDemo));
        }

        var productos = await _db.Productos.AsNoTracking().OrderBy(p => p.ProductoId).ToListAsync();
        var empleados = await _db.Empleados.Where(e => e.Activo).Select(e => e.EmpleadoId).ToListAsync();
        var metodos = await _db.MetodosPago.Where(m => m.Activo).Select(m => m.MetodoPagoId).ToListAsync();
        var clientes = await _db.Clientes.Select(c => (int?)c.ClienteId).ToListAsync();

        var azar = new Random(2026);
        const int dias = 90;
        var hoy = DateTime.Today;
        var primerDia = hoy.AddDays(-dias);

        // 1) Demanda de cada producto, día por día (con patrón semanal y una leve tendencia)
        var basePorProducto = productos.ToDictionary(p => p.ProductoId, _ => 1.5 + azar.NextDouble() * 6.5);
        var demanda = new int[dias, productos.Count];
        for (var d = 0; d < dias; d++)
        {
            var fecha = primerDia.AddDays(d);
            var semana = fecha.DayOfWeek switch
            {
                DayOfWeek.Saturday => 1.45, DayOfWeek.Sunday => 1.3, DayOfWeek.Friday => 1.2,
                DayOfWeek.Monday => 0.85, _ => 1.0
            };
            var tendencia = 0.85 + 0.3 * d / dias; // las ventas crecen poco a poco
            for (var j = 0; j < productos.Count; j++)
            {
                var esperado = basePorProducto[productos[j].ProductoId] * semana * tendencia;
                demanda[d, j] = Math.Max(0, (int)Math.Round(esperado * (0.6 + azar.NextDouble() * 0.8)));
            }
        }

        // 2) Ventas: cada día se reparten las unidades en varios tickets
        var ventas = new List<Venta>();
        for (var d = 0; d < dias; d++)
        {
            var fecha = primerDia.AddDays(d);
            var tickets = Enumerable.Range(0, azar.Next(6, 12)).Select(_ => new Venta
            {
                Fecha = fecha.AddHours(azar.Next(8, 20)).AddMinutes(azar.Next(0, 60)),
                EmpleadoId = empleados[azar.Next(empleados.Count)],
                MetodoPagoId = metodos[azar.Next(metodos.Count)],
                ClienteId = clientes.Count > 0 && azar.NextDouble() < 0.6 ? clientes[azar.Next(clientes.Count)] : null
            }).ToList();

            for (var j = 0; j < productos.Count; j++)
            {
                var pendiente = demanda[d, j];
                while (pendiente > 0)
                {
                    var cantidad = Math.Min(pendiente, azar.Next(1, 4));
                    var ticket = tickets[azar.Next(tickets.Count)];
                    var renglon = ticket.Detalles.FirstOrDefault(x => x.ProductoId == productos[j].ProductoId);
                    if (renglon != null) renglon.Cantidad += cantidad;
                    else ticket.Detalles.Add(new DetalleVenta
                    {
                        ProductoId = productos[j].ProductoId,
                        Cantidad = cantidad,
                        PrecioUnitario = productos[j].Precio,
                        DescuentoPorcentaje = 0
                    });
                    pendiente -= cantidad;
                }
            }
            ventas.AddRange(tickets.Where(t => t.Detalles.Count > 0));
        }

        // 3) Compras semanales: cada proveedor entrega lo que se venderá esa semana
        var compras = new List<Compra>();
        for (var d = 0; d < dias; d += 7)
        {
            var fecha = primerDia.AddDays(d).AddHours(7);
            foreach (var grupo in productos.Select((p, j) => (p, j)).GroupBy(x => x.p.ProveedorId))
            {
                var compra = new Compra
                {
                    Fecha = fecha,
                    ProveedorId = grupo.Key,
                    EmpleadoId = empleados[azar.Next(empleados.Count)],
                    NumeroFactura = $"{PrefijoDemo}{fecha:yyMMdd}",
                    Observacion = "Dato de demostración"
                };
                foreach (var (p, j) in grupo)
                {
                    var semana = 0;
                    for (var k = d; k < Math.Min(d + 7, dias); k++) semana += demanda[k, j];
                    if (semana > 0)
                        compra.Detalles.Add(new DetalleCompra
                        {
                            ProductoId = p.ProductoId,
                            Cantidad = semana,
                            CostoUnitario = Math.Max(0.01m, Math.Round(p.Precio * 0.7m, 2))
                        });
                }
                if (compra.Detalles.Count > 0) compras.Add(compra);
            }
        }

        _db.Ventas.AddRange(ventas);
        _db.Compras.AddRange(compras);
        await _db.SaveChangesAsync(); // todo o nada

        TempData["Exito"] = $"Se generaron {ventas.Count} ventas y {compras.Count} compras de los últimos {dias} días. " +
                            "El stock actual de los productos no cambió.";
        return RedirectToAction(nameof(Index));
    }

    private async Task<string?> MotivoBloqueoDemo()
    {
        if (await _db.Compras.AnyAsync(c => c.NumeroFactura.StartsWith(PrefijoDemo)))
            return "Los datos de demostración ya se generaron. Solo se pueden crear una vez.";
        if (!await _db.Productos.AnyAsync())
            return "Primero registre al menos un producto.";
        if (!await _db.Empleados.AnyAsync(e => e.Activo))
            return "Primero registre al menos un empleado activo.";
        if (!await _db.MetodosPago.AnyAsync(m => m.Activo))
            return "Primero active al menos un método de pago.";
        return null;
    }
}
