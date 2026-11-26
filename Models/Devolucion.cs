using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorVentas.Models;

/// <summary>
/// Devolución de productos de una venta.
/// Formulario AVANZADO: Devoluciones + DetallesDevolucion + DetallesVenta + Ventas + Productos + Empleados.
/// Cada renglón apunta al renglón de la venta original, así el dinero devuelto
/// se calcula con el precio HISTÓRICO que pagó el cliente.
/// </summary>
public class Devolucion
{
    public static readonly string[] Motivos =
    {
        "Producto defectuoso",
        "Producto vencido",
        "Error en la venta",
        "Cliente cambió de opinión",
        "Otro"
    };

    public int DevolucionId { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Display(Name = "Venta")]
    public int VentaId { get; set; }
    public Venta? Venta { get; set; }

    [Display(Name = "Atendido por")]
    public int EmpleadoId { get; set; }
    public Empleado? Empleado { get; set; }

    [Required]
    [StringLength(40)]
    public string Motivo { get; set; } = string.Empty;

    [StringLength(200)]
    [Display(Name = "Observación")]
    public string? Observacion { get; set; }

    /// <summary>
    /// true = los productos vuelven al stock para venderse otra vez.
    /// false = se descartan (por ejemplo, si están dañados o vencidos).
    /// </summary>
    [Display(Name = "Regresó al inventario")]
    public bool Reintegrado { get; set; } = true;

    public ICollection<DetalleDevolucion> Detalles { get; set; } = new List<DetalleDevolucion>();

    // Dinero devuelto al cliente (calculado con el precio histórico)
    [NotMapped]
    public decimal Total => Detalles.Sum(d => d.Cantidad * (d.DetalleVenta?.PrecioUnitario ?? 0));
}

/// <summary>Un producto devuelto: qué renglón de la venta y cuántas unidades.</summary>
public class DetalleDevolucion
{
    public int DetalleDevolucionId { get; set; }

    public int DevolucionId { get; set; }
    public Devolucion? Devolucion { get; set; }

    public int DetalleVentaId { get; set; }
    public DetalleVenta? DetalleVenta { get; set; }

    public int Cantidad { get; set; }

    [NotMapped]
    public decimal Subtotal => Cantidad * (DetalleVenta?.PrecioUnitario ?? 0);
}
