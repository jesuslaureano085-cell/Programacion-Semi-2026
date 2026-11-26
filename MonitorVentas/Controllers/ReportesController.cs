using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

/// <summary>
/// Los 10 reportes del sistema. Todos se arman con la misma estructura (Reporte),
/// así una sola vista los muestra, los imprime y los exporta a Excel (.xlsx).
/// Las ventas y compras ANULADAS nunca cuentan en los reportes.
/// </summary>
public class ReportesController : Controller
{
    private readonly AppDbContext _db;
    public ReportesController(AppDbContext db) => _db = db;

    // Renglones que se leen de la base de datos (solo los datos necesarios)
    private record LineaVendida(int VentaId, DateTime Fecha, int ProductoId, string Producto, string Categoria,
                                string Cajero, string Metodo, string? Cliente, int Cantidad, decimal Importe);
    private record LineaDevuelta(int DevolucionId, DateTime Fecha, string Motivo, bool Reintegrado,
                                 int ProductoId, int Cantidad, decimal Monto);
    private record Movimiento(DateTime Fecha, string Tipo, string Documento, int Entrada, int Salida);

    // GET: Reportes  (menú con los 10 reportes)
    public IActionResult Index() => View(Reporte.Catalogo);

    // GET: Reportes/Ver/ventas-dia?desde=2026-11-01&hasta=2026-11-30
    public async Task<IActionResult> Ver(string? id, DateTime? desde, DateTime? hasta, int? productoId)
    {
        var reporte = await Construir(id, desde, hasta, productoId);
        if (reporte == null) return NotFound();
        if (reporte.UsaProducto)
            ViewData["Productos"] = new SelectList(
                await _db.Productos.OrderBy(p => p.Nombre).ToListAsync(), "ProductoId", "Nombre", reporte.ProductoId);
        return View(reporte);
    }

    // GET: Reportes/Exportar/ventas-dia?...  (descarga un archivo de Excel .xlsx)
    public async Task<IActionResult> Exportar(string? id, DateTime? desde, DateTime? hasta, int? productoId)
    {
        var r = await Construir(id, desde, hasta, productoId);
        if (r == null) return NotFound();
        return File(ExcelSimple.Crear(r),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"reporte-{r.Clave}-{DateTime.Now:yyyyMMdd-HHmm}.xlsx");
    }

    // ==================================================================
    // Armado de cada reporte
    // ==================================================================
    private async Task<Reporte?> Construir(string? clave, DateTime? desde, DateTime? hasta, int? productoId)
    {
        var info = Reporte.Catalogo.FirstOrDefault(c => c.Clave == clave);
        if (info == null) return null;

        // Rango por defecto: del 1 del mes actual a hoy. Si vienen al revés, se corrigen.
        var d = (desde ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)).Date;
        var h = (hasta ?? DateTime.Today).Date;
        string? aviso = null;
        if (d > h) { (d, h) = (h, d); aviso = "Las fechas estaban invertidas; se ordenaron automáticamente."; }

        var r = new Reporte { Clave = info.Clave, Titulo = info.Titulo, Descripcion = info.Descripcion, Desde = d, Hasta = h, Aviso = aviso };
        var fin = h.AddDays(1); // hasta el final del día "hasta"

        switch (info.Clave)
        {
            case "ventas-dia": await VentasPorDia(r, d, fin); break;
            case "productos-top": await ProductosTop(r, d, fin); break;
            case "por-categoria": await PorCategoria(r, d, fin); break;
            case "por-empleado": await PorEmpleado(r, d, fin); break;
            case "por-metodo": await PorMetodo(r, d, fin); break;
            case "por-cliente": await PorCliente(r, d, fin); break;
            case "inventario": await Inventario(r); break;
            case "kardex": await Kardex(r, d, fin, productoId); break;
            case "compras-proveedor": await ComprasProveedor(r, d, fin); break;
            case "devoluciones": await Devoluciones(r, d, fin); break;
        }
        return r;
    }

    // Lee los renglones vendidos en el rango (sin ventas anuladas)
    private Task<List<LineaVendida>> Vendido(DateTime desde, DateTime fin) =>
        _db.DetallesVenta
            .Where(x => !x.Venta!.Anulada && x.Venta.Fecha >= desde && x.Venta.Fecha < fin)
            .Select(x => new LineaVendida(
                x.VentaId, x.Venta!.Fecha, x.ProductoId, x.Producto!.Nombre, x.Producto.Categoria!.Nombre,
                x.Venta.Empleado!.Nombre, x.Venta.MetodoPago!.Nombre,
                x.Venta.Cliente != null ? x.Venta.Cliente.Nombre : null,
                x.Cantidad, x.Cantidad * x.PrecioUnitario))
            .ToListAsync();

    // Lee los productos devueltos en el rango
    private Task<List<LineaDevuelta>> Devuelto(DateTime desde, DateTime fin) =>
        _db.DetallesDevolucion
            .Where(x => x.Devolucion!.Fecha >= desde && x.Devolucion.Fecha < fin)
            .Select(x => new LineaDevuelta(
                x.DevolucionId, x.Devolucion!.Fecha, x.Devolucion.Motivo, x.Devolucion.Reintegrado,
                x.DetalleVenta!.ProductoId, x.Cantidad, x.Cantidad * x.DetalleVenta.PrecioUnitario))
            .ToListAsync();

    private static decimal Porcentaje(decimal parte, decimal total) => total == 0 ? 0 : Math.Round(parte * 100 / total, 1);

    // 1) Ventas por día ------------------------------------------------
    private async Task VentasPorDia(Reporte r, DateTime desde, DateTime fin)
    {
        var vendido = await Vendido(desde, fin);
        var devuelto = await Devuelto(desde, fin);

        r.Columnas.AddRange(new ColumnaReporte[]
        {
            new("Fecha", TipoColumna.Fecha), new("Ventas", TipoColumna.Entero, true),
            new("Artículos", TipoColumna.Entero, true), new("Vendido", TipoColumna.Moneda, true),
            new("Devuelto", TipoColumna.Moneda, true), new("Neto", TipoColumna.Moneda, true)
        });

        var dias = vendido.Select(v => v.Fecha.Date).Union(devuelto.Select(x => x.Fecha.Date)).OrderBy(x => x);
        foreach (var dia in dias)
        {
            var v = vendido.Where(x => x.Fecha.Date == dia).ToList();
            var monto = v.Sum(x => x.Importe);
            var dev = devuelto.Where(x => x.Fecha.Date == dia).Sum(x => x.Monto);
            r.Filas.Add(new(new object?[] { dia, v.Select(x => x.VentaId).Distinct().Count(), v.Sum(x => x.Cantidad), monto, dev, monto - dev }));
        }

        var ventas = vendido.Select(x => x.VentaId).Distinct().Count();
        var total = vendido.Sum(x => x.Importe);
        r.Resumen.Add(("Ventas", ventas.ToString()));
        r.Resumen.Add(("Total vendido", total.ToString("C")));
        r.Resumen.Add(("Ticket promedio", (ventas == 0 ? 0 : total / ventas).ToString("C")));
        r.Resumen.Add(("Neto (− devoluciones)", (total - devuelto.Sum(x => x.Monto)).ToString("C")));
        r.GraficoTitulo = "Neto por día"; r.GraficoEtiqueta = 0; r.GraficoValor = 5;
    }

    // 2) Productos más vendidos ----------------------------------------
    private async Task ProductosTop(Reporte r, DateTime desde, DateTime fin)
    {
        var vendido = await Vendido(desde, fin);
        var devuelto = (await Devuelto(desde, fin)).GroupBy(x => x.ProductoId).ToDictionary(g => g.Key, g => g.Sum(x => x.Cantidad));

        r.Columnas.AddRange(new ColumnaReporte[]
        {
            new("N.º", TipoColumna.Entero), new("Producto"), new("Categoría"),
            new("Unidades", TipoColumna.Entero, true), new("Devueltas", TipoColumna.Entero, true),
            new("Ingresos", TipoColumna.Moneda, true)
        });

        var pos = 0;
        foreach (var g in vendido.GroupBy(x => new { x.ProductoId, x.Producto, x.Categoria })
                                 .OrderByDescending(g => g.Sum(x => x.Cantidad)).ThenBy(g => g.Key.Producto))
            r.Filas.Add(new(new object?[] { ++pos, g.Key.Producto, g.Key.Categoria, g.Sum(x => x.Cantidad),
                                            devuelto.GetValueOrDefault(g.Key.ProductoId), g.Sum(x => x.Importe) }));

        r.Resumen.Add(("Productos distintos", r.Filas.Count.ToString()));
        r.Resumen.Add(("Unidades vendidas", vendido.Sum(x => x.Cantidad).ToString("#,0")));
        r.Resumen.Add(("Más vendido", r.Filas.FirstOrDefault()?.Celdas[1]?.ToString() ?? "—"));
        r.GraficoTitulo = "Unidades vendidas (top 12)"; r.GraficoEtiqueta = 1; r.GraficoValor = 3;
    }

    // 3) Ventas por categoría ------------------------------------------
    private async Task PorCategoria(Reporte r, DateTime desde, DateTime fin)
    {
        var vendido = await Vendido(desde, fin);
        var total = vendido.Sum(x => x.Importe);

        r.Columnas.AddRange(new ColumnaReporte[]
        {
            new("Categoría"), new("Productos distintos", TipoColumna.Entero),
            new("Unidades", TipoColumna.Entero, true), new("Ingresos", TipoColumna.Moneda, true),
            new("% del total", TipoColumna.Porcentaje)
        });

        foreach (var g in vendido.GroupBy(x => x.Categoria).OrderByDescending(g => g.Sum(x => x.Importe)))
            r.Filas.Add(new(new object?[] { g.Key, g.Select(x => x.ProductoId).Distinct().Count(),
                                            g.Sum(x => x.Cantidad), g.Sum(x => x.Importe), Porcentaje(g.Sum(x => x.Importe), total) }));

        r.Resumen.Add(("Categorías con ventas", r.Filas.Count.ToString()));
        r.Resumen.Add(("Total vendido", total.ToString("C")));
        r.Resumen.Add(("Categoría líder", r.Filas.FirstOrDefault()?.Celdas[0]?.ToString() ?? "—"));
        r.GraficoTitulo = "Ingresos por categoría"; r.GraficoEtiqueta = 0; r.GraficoValor = 3;
    }

    // 4) Ventas por cajero ---------------------------------------------
    private async Task PorEmpleado(Reporte r, DateTime desde, DateTime fin)
    {
        var vendido = await Vendido(desde, fin);

        r.Columnas.AddRange(new ColumnaReporte[]
        {
            new("Cajero"), new("Ventas", TipoColumna.Entero, true), new("Artículos", TipoColumna.Entero, true),
            new("Total", TipoColumna.Moneda, true), new("Ticket promedio", TipoColumna.Moneda)
        });

        foreach (var g in vendido.GroupBy(x => x.Cajero).OrderByDescending(g => g.Sum(x => x.Importe)))
        {
            var ventas = g.Select(x => x.VentaId).Distinct().Count();
            var total = g.Sum(x => x.Importe);
            r.Filas.Add(new(new object?[] { g.Key, ventas, g.Sum(x => x.Cantidad), total, total / ventas }));
        }

        r.Resumen.Add(("Cajeros con ventas", r.Filas.Count.ToString()));
        r.Resumen.Add(("Total vendido", vendido.Sum(x => x.Importe).ToString("C")));
        r.Resumen.Add(("Mejor cajero", r.Filas.FirstOrDefault()?.Celdas[0]?.ToString() ?? "—"));
        r.GraficoTitulo = "Total vendido por cajero"; r.GraficoEtiqueta = 0; r.GraficoValor = 3;
    }

    // 5) Ventas por método de pago -------------------------------------
    private async Task PorMetodo(Reporte r, DateTime desde, DateTime fin)
    {
        var vendido = await Vendido(desde, fin);
        var total = vendido.Sum(x => x.Importe);

        r.Columnas.AddRange(new ColumnaReporte[]
        {
            new("Método de pago"), new("Ventas", TipoColumna.Entero, true),
            new("Total", TipoColumna.Moneda, true), new("% del total", TipoColumna.Porcentaje)
        });

        foreach (var g in vendido.GroupBy(x => x.Metodo).OrderByDescending(g => g.Sum(x => x.Importe)))
            r.Filas.Add(new(new object?[] { g.Key, g.Select(x => x.VentaId).Distinct().Count(),
                                            g.Sum(x => x.Importe), Porcentaje(g.Sum(x => x.Importe), total) }));

        r.Resumen.Add(("Total cobrado", total.ToString("C")));
        r.Resumen.Add(("Método más usado", r.Filas.OrderByDescending(f => (int)f.Celdas[1]!).FirstOrDefault()?.Celdas[0]?.ToString() ?? "—"));
        r.GraficoTitulo = "Total por método de pago"; r.GraficoEtiqueta = 0; r.GraficoValor = 2;
    }

    // 6) Mejores clientes ----------------------------------------------
    private async Task PorCliente(Reporte r, DateTime desde, DateTime fin)
    {
        var vendido = await Vendido(desde, fin);

        r.Columnas.AddRange(new ColumnaReporte[]
        {
            new("N.º", TipoColumna.Entero), new("Cliente"), new("Compras", TipoColumna.Entero, true),
            new("Artículos", TipoColumna.Entero, true), new("Total", TipoColumna.Moneda, true),
            new("Última compra", TipoColumna.FechaHora)
        });

        var pos = 0;
        foreach (var g in vendido.GroupBy(x => x.Cliente ?? "Consumidor final").OrderByDescending(g => g.Sum(x => x.Importe)))
            r.Filas.Add(new(new object?[] { ++pos, g.Key, g.Select(x => x.VentaId).Distinct().Count(),
                                            g.Sum(x => x.Cantidad), g.Sum(x => x.Importe), g.Max(x => x.Fecha) }));

        r.Resumen.Add(("Clientes que compraron", r.Filas.Count(f => (string)f.Celdas[1]! != "Consumidor final").ToString()));
        r.Resumen.Add(("Mejor cliente", r.Filas.FirstOrDefault(f => (string)f.Celdas[1]! != "Consumidor final")?.Celdas[1]?.ToString() ?? "—"));
        r.GraficoTitulo = "Total comprado por cliente"; r.GraficoEtiqueta = 1; r.GraficoValor = 4;
    }

    // 7) Inventario valorizado (no depende de fechas: es la foto de HOY) ---
    private async Task Inventario(Reporte r)
    {
        r.UsaFechas = false;
        var productos = await _db.Productos
            .OrderBy(p => p.Stock).ThenBy(p => p.Nombre)
            .Select(p => new { p.Codigo, p.Nombre, Categoria = p.Categoria!.Nombre, Proveedor = p.Proveedor!.Nombre, p.Stock, p.Precio })
            .ToListAsync();

        r.Columnas.AddRange(new ColumnaReporte[]
        {
            new("Código"), new("Producto"), new("Categoría"), new("Proveedor"),
            new("Stock", TipoColumna.Entero, true), new("Precio", TipoColumna.Moneda),
            new("Valor en inventario", TipoColumna.Moneda, true), new("Estado")
        });

        const int umbral = DashboardViewModel.UmbralStockBajo;
        foreach (var p in productos)
        {
            var estado = p.Stock == 0 ? "Agotado" : p.Stock <= umbral ? "Stock bajo" : "Normal";
            r.Filas.Add(new(new object?[] { p.Codigo, p.Nombre, p.Categoria, p.Proveedor, p.Stock, p.Precio, p.Stock * p.Precio, estado },
                            p.Stock <= umbral ? "fila-alerta" : null));
        }

        r.Resumen.Add(("Productos", productos.Count.ToString()));
        r.Resumen.Add(("Unidades en bodega", productos.Sum(p => p.Stock).ToString("#,0")));
        r.Resumen.Add(("Valor total (a precio de venta)", productos.Sum(p => p.Stock * p.Precio).ToString("C")));
        r.Resumen.Add(("Agotados o con stock bajo", productos.Count(p => p.Stock <= umbral).ToString()));
        r.Nota = $"Las filas en rojo tienen {umbral} unidades o menos. El valor se calcula con el precio de venta actual.";
    }

    // 8) Kardex de un producto -----------------------------------------
    private async Task Kardex(Reporte r, DateTime desde, DateTime fin, int? productoId)
    {
        r.UsaProducto = true;
        r.Columnas.AddRange(new ColumnaReporte[]
        {
            new("Fecha", TipoColumna.FechaHora), new("Movimiento"), new("Documento"),
            new("Entrada", TipoColumna.Entero, true), new("Salida", TipoColumna.Entero, true), new("Saldo", TipoColumna.Entero)
        });

        var producto = productoId is > 0 ? await _db.Productos.FindAsync(productoId) : null;
        if (producto == null)
        {
            r.Aviso = "Seleccione un producto y pulse Generar para ver su kardex.";
            return;
        }
        r.ProductoId = producto.ProductoId;
        r.Titulo = $"Kardex · {producto.Nombre}";
        var id = producto.ProductoId;

        // Se leen TODOS los movimientos (de cualquier fecha) para poder calcular el saldo.
        // (los textos de "Documento" se arman en memoria, después de leer los datos)
        var movs = new List<Movimiento>();
        movs.AddRange((await _db.DetallesVenta.Where(x => x.ProductoId == id && !x.Venta!.Anulada)
                .Select(x => new { x.Venta!.Fecha, x.VentaId, x.Cantidad }).ToListAsync())
            .Select(x => new Movimiento(x.Fecha, "Venta", $"Venta #{x.VentaId}", 0, x.Cantidad)));
        movs.AddRange((await _db.DetallesCompra.Where(x => x.ProductoId == id && !x.Compra!.Anulada)
                .Select(x => new { x.Compra!.Fecha, x.CompraId, x.Compra.NumeroFactura, x.Cantidad }).ToListAsync())
            .Select(x => new Movimiento(x.Fecha, "Compra", $"Compra #{x.CompraId} · Fact. {x.NumeroFactura}", x.Cantidad, 0)));
        movs.AddRange((await _db.DetallesDevolucion.Where(x => x.DetalleVenta!.ProductoId == id && x.Devolucion!.Reintegrado)
                .Select(x => new { x.Devolucion!.Fecha, x.DevolucionId, x.Cantidad }).ToListAsync())
            .Select(x => new Movimiento(x.Fecha, "Devolución", $"Devolución #{x.DevolucionId}", x.Cantidad, 0)));
        movs.AddRange((await _db.AjustesInventario.Where(x => x.ProductoId == id)
                .Select(x => new { x.Fecha, x.AjusteInventarioId, x.Motivo, x.Tipo, x.Cantidad }).ToListAsync())
            .Select(x => new Movimiento(x.Fecha, $"Ajuste · {x.Motivo}", $"Ajuste #{x.AjusteInventarioId}",
                                        x.Tipo == "Entrada" ? x.Cantidad : 0, x.Tipo == "Salida" ? x.Cantidad : 0)));
        movs = movs.OrderBy(m => m.Fecha).ToList();

        // Saldo inicial = stock actual − todo lo que entró + todo lo que salió
        var saldo = producto.Stock - movs.Sum(m => m.Entrada) + movs.Sum(m => m.Salida);
        foreach (var m in movs.Where(m => m.Fecha < desde)) saldo += m.Entrada - m.Salida;
        var saldoAnterior = saldo;

        r.Filas.Add(new(new object?[] { desde, "Saldo anterior", "—", null, null, saldoAnterior }, "fila-saldo"));
        foreach (var m in movs.Where(m => m.Fecha >= desde && m.Fecha < fin))
        {
            saldo += m.Entrada - m.Salida;
            r.Filas.Add(new(new object?[] { m.Fecha, m.Tipo, m.Documento,
                                            m.Entrada == 0 ? null : m.Entrada, m.Salida == 0 ? null : m.Salida, saldo }));
        }

        var enRango = movs.Where(m => m.Fecha >= desde && m.Fecha < fin).ToList();
        r.Resumen.Add(("Saldo anterior", saldoAnterior.ToString("#,0")));
        r.Resumen.Add(("Entradas", "+" + enRango.Sum(m => m.Entrada).ToString("#,0")));
        r.Resumen.Add(("Salidas", "−" + enRango.Sum(m => m.Salida).ToString("#,0")));
        r.Resumen.Add(("Saldo al final", saldo.ToString("#,0")));
        r.Nota = "Las ventas y compras anuladas no aparecen (su efecto ya se revirtió). " +
                 "Las devoluciones descartadas tampoco, porque no regresaron al inventario.";
    }

    // 9) Compras por proveedor -----------------------------------------
    private async Task ComprasProveedor(Reporte r, DateTime desde, DateTime fin)
    {
        var lineas = await _db.DetallesCompra
            .Where(x => !x.Compra!.Anulada && x.Compra.Fecha >= desde && x.Compra.Fecha < fin)
            .Select(x => new { x.CompraId, x.Compra!.Fecha, Proveedor = x.Compra.Proveedor!.Nombre, x.Cantidad, Importe = x.Cantidad * x.CostoUnitario })
            .ToListAsync();

        r.Columnas.AddRange(new ColumnaReporte[]
        {
            new("Proveedor"), new("Compras", TipoColumna.Entero, true), new("Unidades", TipoColumna.Entero, true),
            new("Total comprado", TipoColumna.Moneda, true), new("Última compra", TipoColumna.FechaHora)
        });

        foreach (var g in lineas.GroupBy(x => x.Proveedor).OrderByDescending(g => g.Sum(x => x.Importe)))
            r.Filas.Add(new(new object?[] { g.Key, g.Select(x => x.CompraId).Distinct().Count(),
                                            g.Sum(x => x.Cantidad), g.Sum(x => x.Importe), g.Max(x => x.Fecha) }));

        r.Resumen.Add(("Compras", lineas.Select(x => x.CompraId).Distinct().Count().ToString()));
        r.Resumen.Add(("Unidades recibidas", lineas.Sum(x => x.Cantidad).ToString("#,0")));
        r.Resumen.Add(("Total comprado", lineas.Sum(x => x.Importe).ToString("C")));
        r.GraficoTitulo = "Total comprado por proveedor"; r.GraficoEtiqueta = 0; r.GraficoValor = 3;
    }

    // 10) Devoluciones por motivo --------------------------------------
    private async Task Devoluciones(Reporte r, DateTime desde, DateTime fin)
    {
        var devuelto = await Devuelto(desde, fin);

        r.Columnas.AddRange(new ColumnaReporte[]
        {
            new("Motivo"), new("Devoluciones", TipoColumna.Entero, true), new("Unidades", TipoColumna.Entero, true),
            new("Reintegradas", TipoColumna.Entero, true), new("Descartadas", TipoColumna.Entero, true),
            new("Dinero devuelto", TipoColumna.Moneda, true)
        });

        foreach (var g in devuelto.GroupBy(x => x.Motivo).OrderByDescending(g => g.Sum(x => x.Monto)))
            r.Filas.Add(new(new object?[] { g.Key, g.Select(x => x.DevolucionId).Distinct().Count(), g.Sum(x => x.Cantidad),
                                            g.Where(x => x.Reintegrado).Sum(x => x.Cantidad),
                                            g.Where(x => !x.Reintegrado).Sum(x => x.Cantidad), g.Sum(x => x.Monto) }));

        r.Resumen.Add(("Devoluciones", devuelto.Select(x => x.DevolucionId).Distinct().Count().ToString()));
        r.Resumen.Add(("Dinero devuelto", devuelto.Sum(x => x.Monto).ToString("C")));
        r.Resumen.Add(("Motivo más frecuente", r.Filas.OrderByDescending(f => (int)f.Celdas[1]!).FirstOrDefault()?.Celdas[0]?.ToString() ?? "—"));
        r.GraficoTitulo = "Dinero devuelto por motivo"; r.GraficoEtiqueta = 0; r.GraficoValor = 5;
    }
}
