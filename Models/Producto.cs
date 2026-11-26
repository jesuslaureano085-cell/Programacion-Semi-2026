using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorVentas.Models;

/// <summary>
/// Artículo que vende el supermercado.
/// Formulario INTERMEDIO: usa 3 tablas (Productos + Categorías + Proveedores).
/// </summary>
public class Producto
{
    public int ProductoId { get; set; }

    // El código de barras EAN-13 siempre mide 13 dígitos, por eso es char(13).
    // Es opcional: hay productos (frutas, pan) que no lo tienen.
    [StringLength(13)]
    [RegularExpression(@"^\d{13}$", ErrorMessage = "El código de barras debe tener exactamente 13 dígitos.")]
    [Column(TypeName = "char(13)")]
    [Display(Name = "Código de barras")]
    public string? Codigo { get; set; }

    [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Seleccione una categoría.")]
    [Display(Name = "Categoría")]
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Seleccione un proveedor.")]
    [Display(Name = "Proveedor")]
    public int ProveedorId { get; set; }
    public Proveedor? Proveedor { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor que cero.")]
    [Column(TypeName = "decimal(18,2)")]
    [DataType(DataType.Currency)]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio.")]
    [Range(0, 1000000, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }

    // Un producto aparece en muchos renglones de venta
    public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
}
