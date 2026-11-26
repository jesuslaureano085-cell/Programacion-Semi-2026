using System.ComponentModel.DataAnnotations;

namespace MonitorVentas.Models;

public class Cliente
{
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede pasar de 100 caracteres.")]
    [Display(Name = "Nombre completo")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [StringLength(20, ErrorMessage = "El teléfono no puede pasar de 20 caracteres.")]
    [RegularExpression(@"^[0-9+\-\s()]{8,20}$", ErrorMessage = "Teléfono no válido (mínimo 8 dígitos; solo números, espacios, + - ( ) ).")]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "La dirección no puede pasar de 200 caracteres.")]
    [Display(Name = "Dirección")]
    public string? Direccion { get; set; }

    // Un cliente tiene muchas ventas
    public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
}
