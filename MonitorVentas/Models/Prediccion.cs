namespace MonitorVentas.Models;

/// <summary>
/// Resultado del Asistente IA para un producto: cuánto se va a vender,
/// en cuántos días se agota y cuánto conviene comprar.
/// </summary>
public class Prediccion
{
    public int ProductoId { get; set; }
    public string Producto { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public int ProveedorId { get; set; }
    public string Proveedor { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal Precio { get; set; }

    /// <summary>Unidades vendidas en los últimos 21 días (para la mini gráfica).</summary>
    public float[] Historia { get; set; } = Array.Empty<float>();
    /// <summary>Unidades que se espera vender en cada uno de los próximos 7 días.</summary>
    public float[] Pronostico { get; set; } = Array.Empty<float>();

    public int Proximos7 { get; set; }          // total pronosticado para 7 días
    public int Minimo7 { get; set; }            // rango probable (90 % de confianza)
    public int Maximo7 { get; set; }
    public double VentaDiaria { get; set; }     // promedio pronosticado por día
    public int? DiasParaAgotarse { get; set; }  // null = no se agota (no tiene ventas)
    public DateTime? FechaAgotado { get; set; }
    public int CantidadSugerida { get; set; }   // unidades a comprar para cubrir 14 días
    public string Tendencia { get; set; } = "—";
    public string Metodo { get; set; } = string.Empty;
    public int DiasConDatos { get; set; }

    public string Estado =>
        DiasParaAgotarse == null ? "Sin ventas" :
        Stock == 0 ? "Agotado" :
        DiasParaAgotarse <= 3 ? "Crítico" :
        DiasParaAgotarse <= 7 ? "Pronto" : "Normal";

    /// <summary>Clase CSS para pintar el estado.</summary>
    public string ClaseEstado => Estado switch
    {
        "Agotado" or "Crítico" => "ia-critico",
        "Pronto" => "ia-pronto",
        "Normal" => "si",
        _ => "promo-inactiva"
    };

    public bool Urgente => Estado is "Agotado" or "Crítico";
}
