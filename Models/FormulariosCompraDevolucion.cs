using System.ComponentModel.DataAnnotations;

namespace MonitorVentas.Models;

// =====================================================================
// Lo que envían las pantallas de Compras y Devoluciones.
// No son tablas: los controladores los convierten en encabezado + detalle.
// =====================================================================

public class CompraFormulario
{
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione el proveedor.")]
    [Display(Name = "Proveedor")]
    public int ProveedorId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Seleccione quién recibe la mercadería.")]
    [Display(Name = "Recibido por")]
    public int EmpleadoId { get; set; }

    [Required(ErrorMessage = "El número de factura es obligatorio.")]
    [StringLength(30, ErrorMessage = "El número de factura no puede pasar de 30 caracteres.")]
    [RegularExpression(@"^[A-Za-z0-9\-/]+$", ErrorMessage = "La factura solo puede tener letras, números, guiones y /.")]
    [Display(Name = "N.º de factura")]
    public string NumeroFactura { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "La observación no puede pasar de 200 caracteres.")]
    [Display(Name = "Observación")]
    public string? Observacion { get; set; }

    public List<LineaCompra> Lineas { get; set; } = new();
}

public class LineaCompra
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal CostoUnitario { get; set; }
}

public class CompraResumen
{
    public int CompraId { get; set; }
    public DateTime Fecha { get; set; }
    public string Proveedor { get; set; } = string.Empty;
    public string NumeroFactura { get; set; } = string.Empty;
    public string RecibidoPor { get; set; } = string.Empty;
    public int Unidades { get; set; }
    public decimal Total { get; set; }
    public bool Anulada { get; set; }
}

public class DevolucionFormulario
{
    [Range(1, int.MaxValue, ErrorMessage = "Indique el número de venta.")]
    [Display(Name = "Venta N.º")]
    public int VentaId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Seleccione quién atiende la devolución.")]
    [Display(Name = "Atendido por")]
    public int EmpleadoId { get; set; }

    [Required(ErrorMessage = "Seleccione el motivo.")]
    [StringLength(40)]
    public string Motivo { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "La observación no puede pasar de 200 caracteres.")]
    [Display(Name = "Observación")]
    public string? Observacion { get; set; }

    [Display(Name = "Regresar los productos al inventario")]
    public bool Reintegrar { get; set; } = true;

    public List<LineaDevolucion> Lineas { get; set; } = new();
}

public class LineaDevolucion
{
    public int DetalleVentaId { get; set; }
    public int Cantidad { get; set; }
}

public class DevolucionResumen
{
    public int DevolucionId { get; set; }
    public DateTime Fecha { get; set; }
    public int VentaId { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Motivo { get; set; } = string.Empty;
    public string AtendidoPor { get; set; } = string.Empty;
    public int Unidades { get; set; }
    public decimal Total { get; set; }
    public bool Reintegrado { get; set; }
}
