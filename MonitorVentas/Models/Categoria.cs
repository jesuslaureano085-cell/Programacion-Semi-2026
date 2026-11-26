using System.ComponentModel.DataAnnotations;

namespace MonitorVentas.Models;

/// <summary>
/// Agrupa los productos del supermercado (Lácteos, Bebidas, Limpieza...).
/// Formulario BÁSICO: una sola tabla.
/// </summary>
public class Categoria
{
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "La descripción no puede pasar de 200 caracteres.")]
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }
}
