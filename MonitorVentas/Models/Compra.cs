using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorVentas.Models;

/// <summary>
/// Encabezado de una compra a un proveedor (la factura que llega con la mercadería).
/// Formulario AVANZADO: Compras + DetallesCompra + Proveedores + Productos + Empleados.
/// Al guardarla, sube el stock de cada producto. Se puede anular (baja el stock).
/// </summary>
public class Compra
{
    public int CompraId { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Display(Name = "Proveedor")]
    public int ProveedorId { get; set; }
    public Proveedor? Proveedor { get; set; }

    [Display(Name = "Recibido por")]
    public int EmpleadoId { get; set; }
    public Empleado? Empleado { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "N.º de factura")]
    public string NumeroFactura { get; set; } = string.Empty;

    [StringLength(200)]
    [Display(Name = "Observación")]
    public string? Observacion { get; set; }

    public bool Anulada { get; set; }

    public ICollection<DetalleCompra> Detalles { get; set; } = new List<DetalleCompra>();

    // Calculado (no se guarda): suma de los renglones
    [NotMapped]
    public decimal Total => Detalles.Sum(d => d.Cantidad * d.CostoUnitario);
}

/// <summary>Un renglón de la compra: producto, cantidad y costo unitario pagado al proveedor.</summary>
public class DetalleCompra
{
    public int DetalleCompraId { get; set; }

    public int CompraId { get; set; }
    public Compra? Compra { get; set; }

    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Costo unitario")]
    public decimal CostoUnitario { get; set; }

    [NotMapped]
    public decimal Subtotal => Cantidad * CostoUnitario;
}
