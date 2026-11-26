using System.ComponentModel.DataAnnotations;

namespace MonitorVentas.Models;

/// <summary>
/// Lo que envía la pantalla de "Nueva venta" (la caja).
/// No es una tabla: el controlador lo convierte en Venta + DetallesVenta.
/// </summary>
public class VentaFormulario
{
    [Display(Name = "Cliente")]
    public int? ClienteId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Seleccione el cajero.")]
    [Display(Name = "Cajero")]
    public int EmpleadoId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Seleccione el método de pago.")]
    [Display(Name = "Método de pago")]
    public int MetodoPagoId { get; set; }

    public List<LineaVenta> Lineas { get; set; } = new();
}

/// <summary>Un producto agregado a la venta (solo producto y cantidad: el precio lo pone el servidor).</summary>
public class LineaVenta
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}

/// <summary>Fila del historial de ventas (con el total ya calculado en SQL).</summary>
public class VentaResumen
{
    public int VentaId { get; set; }
    public DateTime Fecha { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Cajero { get; set; } = string.Empty;
    public string MetodoPago { get; set; } = string.Empty;
    public int Articulos { get; set; }
    public decimal Total { get; set; }
    public bool Anulada { get; set; }
}
