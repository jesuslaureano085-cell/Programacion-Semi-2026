using System.ComponentModel.DataAnnotations;

namespace MonitorVentas.Models;

/// <summary>
/// Forma en que el cliente paga (efectivo, tarjeta, transferencia...).
/// Formulario BÁSICO: una sola tabla.
/// "Disponible" permite dejar de usar un método sin borrarlo.
/// </summary>
public class MetodoPago
{
    public int MetodoPagoId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 40 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "La descripción no puede pasar de 200 caracteres.")]
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    [Display(Name = "Disponible")]
    public bool Activo { get; set; } = true;
}
