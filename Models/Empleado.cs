using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorVentas.Models;

/// <summary>
/// Persona que trabaja en el supermercado (cajeros, supervisores...).
/// Formulario BÁSICO: una sola tabla.
/// </summary>
public class Empleado
{
    /// <summary>Cargos permitidos (se muestran en una lista desplegable).</summary>
    public static readonly string[] Cargos = { "Cajero", "Supervisor", "Bodeguero", "Gerente" };

    public int EmpleadoId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    [Display(Name = "Nombre completo")]
    public string Nombre { get; set; } = string.Empty;

    // El DUI siempre mide 10 caracteres (00000000-0), por eso es char(10) y no nvarchar.
    [Required(ErrorMessage = "El DUI es obligatorio.")]
    [StringLength(10)]
    [RegularExpression(@"^\d{8}-\d$", ErrorMessage = "El DUI debe tener el formato 00000000-0.")]
    [Column(TypeName = "char(10)")]
    [Display(Name = "DUI")]
    public string Dui { get; set; } = string.Empty;

    [Required(ErrorMessage = "Seleccione un cargo.")]
    [StringLength(30)]
    public string Cargo { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "El teléfono no puede pasar de 20 caracteres.")]
    [RegularExpression(@"^[0-9+\-\s()]{8,20}$", ErrorMessage = "Teléfono no válido (mínimo 8 dígitos; solo números, espacios, + - ( ) ).")]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [DataType(DataType.Date)]
    [Column(TypeName = "date")]
    [Display(Name = "Fecha de ingreso")]
    public DateTime FechaIngreso { get; set; } = DateTime.Today;

    [Display(Name = "Activo")]
    public bool Activo { get; set; } = true;
}
