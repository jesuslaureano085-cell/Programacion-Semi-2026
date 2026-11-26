using System.Globalization;

namespace MonitorVentas.Models;

/// <summary>Cómo se muestra el valor de una columna.</summary>
public enum TipoColumna { Texto, Entero, Moneda, Fecha, FechaHora, Porcentaje }

/// <summary>Una columna del reporte: título, tipo de dato y si se suma al final.</summary>
public record ColumnaReporte(string Titulo, TipoColumna Tipo = TipoColumna.Texto, bool Total = false)
{
    public bool EsNumero => Tipo is TipoColumna.Entero or TipoColumna.Moneda or TipoColumna.Porcentaje;
}

/// <summary>Una fila del reporte. Clase = estilo opcional (por ejemplo "stock-bajo").</summary>
public record FilaReporte(object?[] Celdas, string? Clase = null);

/// <summary>Cada uno de los 10 reportes del sistema (para el menú de reportes).</summary>
public record InfoReporte(string Clave, string Titulo, string Descripcion, string Grupo);

/// <summary>
/// Reporte ya calculado, listo para mostrarse, imprimirse o exportarse.
/// Los 10 reportes usan esta misma estructura, así una sola vista los dibuja todos.
/// </summary>
public class Reporte
{
    public static readonly InfoReporte[] Catalogo =
    {
        new("ventas-dia",       "Ventas por día",            "Cuánto se vendió cada día, lo devuelto y el neto.",            "Ventas"),
        new("productos-top",    "Productos más vendidos",    "Ranking de productos por unidades vendidas.",                 "Ventas"),
        new("por-categoria",    "Ventas por categoría",      "Qué categorías dejan más ingresos.",                          "Ventas"),
        new("por-empleado",     "Ventas por cajero",         "Ventas, artículos y ticket promedio de cada cajero.",         "Ventas"),
        new("por-metodo",       "Ventas por método de pago", "Efectivo, tarjeta o transferencia: cuánto entró por cada uno.", "Ventas"),
        new("por-cliente",      "Mejores clientes",          "Clientes que más compran (incluye consumidor final).",        "Ventas"),
        new("inventario",       "Inventario valorizado",     "Existencias actuales, su valor y los productos por agotarse.", "Inventario"),
        new("kardex",           "Kardex de producto",        "Todas las entradas y salidas de un producto con su saldo.",   "Inventario"),
        new("compras-proveedor","Compras por proveedor",     "Cuánto se le compró a cada proveedor.",                       "Compras y devoluciones"),
        new("devoluciones",     "Devoluciones por motivo",   "Por qué devuelven los clientes y cuánto dinero representa.",  "Compras y devoluciones"),
    };

    public string Clave { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;

    // Filtros
    public bool UsaFechas { get; set; } = true;
    public bool UsaProducto { get; set; }
    public DateTime Desde { get; set; }
    public DateTime Hasta { get; set; }
    public int? ProductoId { get; set; }
    public string? Aviso { get; set; }          // mensaje informativo (ej. "seleccione un producto")

    // Contenido
    public List<(string Etiqueta, string Valor)> Resumen { get; } = new();
    public List<ColumnaReporte> Columnas { get; } = new();
    public List<FilaReporte> Filas { get; } = new();
    public string? Nota { get; set; }            // aclaración al pie

    // Gráfico de barras opcional: qué columna da el nombre y cuál el valor
    public string? GraficoTitulo { get; set; }
    public int GraficoEtiqueta { get; set; }
    public int GraficoValor { get; set; } = -1;

    public bool TieneTotales => Columnas.Any(c => c.Total);

    /// <summary>Suma de una columna numérica (para la fila de totales).</summary>
    public decimal Sumar(int columna) =>
        Filas.Sum(f => f.Celdas[columna] is null ? 0m : Convert.ToDecimal(f.Celdas[columna], CultureInfo.InvariantCulture));

    /// <summary>Barras del gráfico (máximo 12), con el porcentaje respecto a la mayor.</summary>
    public List<(string Etiqueta, decimal Valor, double Ancho)> Barras()
    {
        if (GraficoValor < 0 || Filas.Count == 0) return new();
        var datos = Filas
            .Select(f => (Etiqueta: Convert.ToString(f.Celdas[GraficoEtiqueta] is DateTime d ? d.ToString("dd/MM") : f.Celdas[GraficoEtiqueta]) ?? "",
                          Valor: f.Celdas[GraficoValor] is null ? 0m : Convert.ToDecimal(f.Celdas[GraficoValor])))
            .Take(12).ToList();
        var mayor = datos.Max(d => d.Valor);
        return datos.Select(d => (d.Etiqueta, d.Valor, mayor <= 0 ? 0 : (double)(d.Valor / mayor) * 100)).ToList();
    }

    /// <summary>Texto que se muestra en pantalla para un valor.</summary>
    public static string Formatear(object? valor, TipoColumna tipo) => valor switch
    {
        null => "—",
        DateTime f when tipo == TipoColumna.FechaHora => f.ToString("dd/MM/yyyy HH:mm"),
        DateTime f => f.ToString("dd/MM/yyyy"),
        _ when tipo == TipoColumna.Moneda => Convert.ToDecimal(valor).ToString("C"),
        _ when tipo == TipoColumna.Porcentaje => Convert.ToDecimal(valor).ToString("0.0") + " %",
        _ when tipo == TipoColumna.Entero => Convert.ToDecimal(valor).ToString("#,0"),
        _ => valor.ToString() ?? ""
    };
}
