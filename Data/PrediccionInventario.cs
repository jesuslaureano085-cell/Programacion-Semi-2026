using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using MonitorVentas.Models;

namespace MonitorVentas.Data;

/// <summary>
/// ASISTENTE IA — Predicción de inventario con ML.NET.
///
/// Para cada producto se arma su "serie de tiempo": cuántas unidades se vendieron cada día.
/// Con esa serie se entrena un modelo SSA (Singular Spectrum Analysis), que aprende la
/// tendencia y los patrones que se repiten (por ejemplo, que el sábado se vende más),
/// y pronostica las ventas de los próximos 7 días con un rango de confianza.
///
/// Con el pronóstico se calcula en cuántos días se agota el producto y cuánto comprar
/// para tener existencias durante 14 días.
/// Si un producto tiene pocos datos (menos de 4 semanas), se usa un promedio simple.
/// </summary>
public static class PrediccionInventario
{
    public const int Horizonte = 7;       // días que se pronostican
    public const int DiasCobertura = 14;  // la compra sugerida alcanza para 14 días
    public const int DiasHistoria = 90;   // historial que se analiza
    public const int MinimoParaIA = 28;   // días mínimos de historial para usar el modelo

    // Formato de datos que usa ML.NET
    private class Punto { public float Ventas { get; set; } }
    private class Salida
    {
        public float[] Pronostico { get; set; } = Array.Empty<float>();
        public float[] Minimo { get; set; } = Array.Empty<float>();
        public float[] Maximo { get; set; } = Array.Empty<float>();
    }

    public static async Task<List<Prediccion>> CalcularAsync(AppDbContext db)
    {
        var hoy = DateTime.Today;
        var inicio = hoy.AddDays(-DiasHistoria);
        var manana = hoy.AddDays(1);

        var productos = await db.Productos.AsNoTracking()
            .Select(p => new { p.ProductoId, p.Nombre, Categoria = p.Categoria!.Nombre, p.ProveedorId, Proveedor = p.Proveedor!.Nombre, p.Stock, p.Precio })
            .ToListAsync();

        // Unidades vendidas por producto y por día (sin ventas anuladas)
        var vendidas = (await db.DetallesVenta.AsNoTracking()
                .Where(d => !d.Venta!.Anulada && d.Venta.Fecha >= inicio && d.Venta.Fecha < manana)
                .Select(d => new { d.ProductoId, d.Venta!.Fecha, d.Cantidad })
                .ToListAsync())
            .GroupBy(x => x.ProductoId)
            .ToDictionary(g => g.Key, g => g.GroupBy(x => x.Fecha.Date).ToDictionary(d => d.Key, d => (float)d.Sum(x => x.Cantidad)));

        var ml = new MLContext(seed: 2026);
        var resultado = new List<Prediccion>();

        foreach (var p in productos)
        {
            var pr = new Prediccion
            {
                ProductoId = p.ProductoId, Producto = p.Nombre, Categoria = p.Categoria,
                ProveedorId = p.ProveedorId, Proveedor = p.Proveedor, Stock = p.Stock, Precio = p.Precio
            };
            var porDia = vendidas.GetValueOrDefault(p.ProductoId) ?? new Dictionary<DateTime, float>();

            // Últimos 21 días (incluye hoy) para la mini gráfica
            pr.Historia = Enumerable.Range(0, 21).Select(i => porDia.GetValueOrDefault(hoy.AddDays(i - 20))).ToArray();

            if (porDia.Count == 0)
            {
                pr.Metodo = "Sin datos";
                resultado.Add(pr);
                continue;
            }

            // Serie diaria desde la primera venta hasta AYER (el día de hoy aún no termina)
            var primerDia = porDia.Keys.Min();
            var serie = new List<float>();
            for (var d = primerDia; d < hoy; d = d.AddDays(1)) serie.Add(porDia.GetValueOrDefault(d));
            pr.DiasConDatos = (hoy - primerDia).Days + 1;

            float[]? pronostico = null, minimo = null, maximo = null;
            if (serie.Count >= MinimoParaIA && serie.Sum() > 0)
            {
                try
                {
                    var datos = ml.Data.LoadFromEnumerable(serie.Select(v => new Punto { Ventas = v }));
                    var tuberia = ml.Forecasting.ForecastBySsa(
                        outputColumnName: nameof(Salida.Pronostico),
                        inputColumnName: nameof(Punto.Ventas),
                        windowSize: 7,                              // ciclo semanal
                        seriesLength: Math.Min(serie.Count, 30),
                        trainSize: serie.Count,
                        horizon: Horizonte,
                        confidenceLevel: 0.90f,
                        confidenceLowerBoundColumn: nameof(Salida.Minimo),
                        confidenceUpperBoundColumn: nameof(Salida.Maximo));
                    var modelo = tuberia.Fit(datos);
                    var motor = modelo.CreateTimeSeriesEngine<Punto, Salida>(ml);
                    var salida = motor.Predict();

                    pronostico = salida.Pronostico.Select(v => Math.Max(0f, v)).ToArray();
                    minimo = salida.Minimo.Select(v => Math.Max(0f, v)).ToArray();
                    maximo = salida.Maximo.Select(v => Math.Max(0f, v)).ToArray();
                    pr.Metodo = "IA (ML.NET · SSA)";

                    // Tendencia: pronóstico contra la última semana real
                    var ultimaSemana = serie.TakeLast(7).Average();
                    var proxima = pronostico.Average();
                    pr.Tendencia = ultimaSemana == 0 ? "—"
                        : proxima > ultimaSemana * 1.1 ? "Subiendo"
                        : proxima < ultimaSemana * 0.9 ? "Bajando" : "Estable";
                }
                catch
                {
                    pronostico = null; // si el modelo falla, se usa el promedio
                }
            }

            if (pronostico == null)
            {
                // Pocos datos: promedio de lo vendido por día desde la primera venta (incluye hoy)
                var diaria = porDia.Values.Sum() / Math.Max(1, pr.DiasConDatos);
                pronostico = Enumerable.Repeat(diaria, Horizonte).ToArray();
                minimo = pronostico.Select(v => v * 0.7f).ToArray();
                maximo = pronostico.Select(v => v * 1.3f).ToArray();
                pr.Metodo = "Promedio (pocos datos)";
            }

            pr.Pronostico = pronostico;
            pr.Proximos7 = (int)Math.Round(pronostico.Sum());
            pr.Minimo7 = (int)Math.Floor(minimo!.Sum());
            pr.Maximo7 = (int)Math.Ceiling(maximo!.Sum());
            pr.VentaDiaria = Math.Round(pronostico.Average(), 1);

            if (pr.VentaDiaria >= 0.05)
            {
                pr.DiasParaAgotarse = (int)Math.Floor(p.Stock / pr.VentaDiaria);
                pr.FechaAgotado = hoy.AddDays(pr.DiasParaAgotarse.Value);

                // Compra sugerida: lo que se vende en 14 días + margen de seguridad
                // (la diferencia entre el escenario alto y el esperado), menos lo que ya hay.
                var objetivo = pr.VentaDiaria * DiasCobertura + Math.Max(0, pr.Maximo7 - pr.Proximos7);
                pr.CantidadSugerida = Math.Max(0, (int)Math.Ceiling(objetivo) - p.Stock);
            }
            resultado.Add(pr);
        }

        return resultado
            .OrderBy(r => r.DiasParaAgotarse ?? int.MaxValue)
            .ThenBy(r => r.Producto)
            .ToList();
    }
}
