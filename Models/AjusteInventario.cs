using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorVentas.Models;

/// <summary>
/// Entrada o salida de mercadería que NO es una venta
/// (producto vencido, dañado, conteo físico, mercadería recibida...).
/// Formulario INTERMEDIO: AjustesInventario + Productos + Empleados.
/// Cada ajuste mueve el stock del producto automáticamente.
/// </summary>
public class AjusteInventario
{
    public static readonly string[] Tipos = { "Entrada", "Salida" };

    public static readonly string[] Motivos =
    {
        "Mercadería recibida",
        "Conteo físico",
        "Producto vencido",
        "Producto dañado",
        "Robo o pérdida",
        "Devolución a proveedor",
        "Otro"
    };

    public int AjusteInventarioId { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Range(1, int.MaxValue, ErrorMessage = "Seleccione un producto.")]
    [Display(Name = "Producto")]
    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Seleccione quién hace el ajuste.")]
    [Display(Name = "Responsable")]
    public int EmpleadoId { get; set; }
    public Empleado? Empleado { get; set; }

    [Required(ErrorMessage = "Seleccione si es entrada o salida.")]
    [StringLength(10)]
    public string Tipo { get; set; } = "Entrada";

    [Required(ErrorMessage = "Seleccione el motivo.")]
    [StringLength(40)]
    public string Motivo { get; set; } = string.Empty;

    [Range(1, 100000, ErrorMessage = "La cantidad debe estar entre 1 y 100,000.")]
    public int Cantidad { get; set; } = 1;

    [StringLength(200, ErrorMessage = "La observación no puede pasar de 200 caracteres.")]
    [Display(Name = "Observación")]
    public string? Observacion { get; set; }

    /// <summary>Cuánto cambia el stock: +Cantidad si es entrada, -Cantidad si es salida.</summary>
    [NotMapped]
    public int Efecto => Tipo == "Entrada" ? Cantidad : -Cantidad;
}
