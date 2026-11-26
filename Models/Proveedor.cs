using System.ComponentModel.DataAnnotations;

namespace MonitorVentas.Models;

/// <summary>
/// Empresa que le vende mercadería al supermercado.
/// Formulario BÁSICO: una sola tabla.
/// </summary>
public class Proveedor
{
    public int ProveedorId { get; set; }

    [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    [Display(Name = "Empresa")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "El contacto no puede pasar de 100 caracteres.")]
    [Display(Name = "Persona de contacto")]
    public string? Contacto { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [StringLength(20, ErrorMessage = "El teléfono no puede pasar de 20 caracteres.")]
    [RegularExpression(@"^[0-9+\-\s()]{8,20}$", ErrorMessage = "Teléfono no válido (mínimo 8 dígitos; solo números, espacios, + - ( ) ).")]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "El correo no puede pasar de 100 caracteres.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    [Display(Name = "Correo electrónico")]
    public string? Correo { get; set; }

    [StringLength(200, ErrorMessage = "La dirección no puede pasar de 200 caracteres.")]
    [Display(Name = "Dirección")]
    public string? Direccion { get; set; }
}
