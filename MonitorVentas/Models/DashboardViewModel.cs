namespace MonitorVentas.Models;

/// <summary>Datos que muestra la pantalla principal (dashboard).</summary>
public class DashboardViewModel
{
    public int TotalClientes { get; set; }
    public int TotalProductos { get; set; }
    public int ProductosStockBajo { get; set; }
    public int VentasHoy { get; set; }
    public decimal MontoHoy { get; set; }

    /// <summary>Un producto se considera con stock bajo con esta cantidad o menos.</summary>
    public const int UmbralStockBajo = 5;
}
