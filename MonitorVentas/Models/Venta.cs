using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorVentas.Models;

/// <summary>
/// Encabezado de la venta: quién compró, quién cobró, cómo pagó y cuándo.
/// Los productos van en DetalleVenta (una venta puede tener varios).
/// Formulario AVANZADO: Ventas + DetallesVenta + Productos + Clientes + Empleados + MetodosPago.
/// </summary>
public class Venta
{
    public int VentaId { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime Fecha { get; set; } = DateTime.Now;

    // Opcional: si queda vacío, la venta es a "Consumidor final"
    [Display(Name = "Cliente")]
    public int? ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    [Display(Name = "Cajero")]
    public int EmpleadoId { get; set; }
    public Empleado? Empleado { get; set; }

    [Display(Name = "Método de pago")]
    public int MetodoPagoId { get; set; }
    public MetodoPago? MetodoPago { get; set; }

    // Las ventas no se borran ni se editan: se ANULAN (y se devuelve el stock).
    // Así queda el historial completo.
    public bool Anulada { get; set; }

    public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();

    // Total NO se guarda en la base de datos (se calcula a partir del detalle).
    // Así se respeta la 3FN: un dato derivado no se almacena dos veces.
    [NotMapped]
    [DataType(DataType.Currency)]
    public decimal Total => Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
}
