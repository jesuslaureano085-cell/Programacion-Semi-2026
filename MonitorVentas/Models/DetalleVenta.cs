using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorVentas.Models;

/// <summary>
/// Un renglón de la venta: qué producto, cuántas unidades y a qué precio.
/// PrecioUnitario es el precio HISTÓRICO: se copia del producto al vender,
/// para que las ventas viejas no cambien si luego se modifica el precio.
/// </summary>
public class DetalleVenta
{
    public int DetalleVentaId { get; set; }

    public int VentaId { get; set; }
    public Venta? Venta { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un producto.")]
    [Display(Name = "Producto")]
    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [DataType(DataType.Currency)]
    [Display(Name = "Precio unitario")]
    public decimal PrecioUnitario { get; set; }

    // Descuento de promoción que se aplicó al vender (0 = sin descuento).
    // PrecioUnitario ya es el precio CON descuento.
    [Range(0, 90)]
    [Display(Name = "Descuento (%)")]
    public int DescuentoPorcentaje { get; set; }

    // Calculado (no se guarda): Cantidad x PrecioUnitario
    [NotMapped]
    [DataType(DataType.Currency)]
    public decimal Subtotal => Cantidad * PrecioUnitario;
}
