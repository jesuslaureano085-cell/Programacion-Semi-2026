using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorVentas.Models;

/// <summary>
/// Descuento por porcentaje, válido entre dos fechas, para UN producto
/// o para TODA una categoría. La caja lo aplica sola.
/// Formulario INTERMEDIO: Promociones + Productos + Categorías.
/// </summary>
public class Promocion
{
    public int PromocionId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 60 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    // Solo uno de los dos tiene valor: o un producto, o una categoría
    [Display(Name = "Producto")]
    public int? ProductoId { get; set; }
    public Producto? Producto { get; set; }

    [Display(Name = "Categoría")]
    public int? CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    [Range(1, 90, ErrorMessage = "El descuento debe estar entre 1% y 90%.")]
    [Display(Name = "Descuento (%)")]
    public int Porcentaje { get; set; } = 10;

    [DataType(DataType.Date)]
    [Column(TypeName = "date")]
    [Display(Name = "Desde")]
    public DateTime FechaInicio { get; set; } = DateTime.Today;

    [DataType(DataType.Date)]
    [Column(TypeName = "date")]
    [Display(Name = "Hasta")]
    public DateTime FechaFin { get; set; } = DateTime.Today.AddDays(7);

    [Display(Name = "Activa")]
    public bool Activa { get; set; } = true;

    /// <summary>Solo para el formulario: "producto" o "categoria".</summary>
    [NotMapped]
    [Display(Name = "Aplica a")]
    public string Alcance { get; set; } = "producto";

    /// <summary>Estado para mostrar: Vigente, Programada, Vencida o Inactiva.</summary>
    [NotMapped]
    public string Estado
    {
        get
        {
            var hoy = DateTime.Today;
            if (!Activa) return "Inactiva";
            if (hoy < FechaInicio.Date) return "Programada";
            if (hoy > FechaFin.Date) return "Vencida";
            return "Vigente";
        }
    }
}
