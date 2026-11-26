using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db)
    {
        _db = db;
    }

    // Escritorio principal (dashboard). Los números iniciales vienen de aquí
    // y luego el escritorio los actualiza solo llamando a Estado().
    public async Task<IActionResult> Index()
    {
        return View(await ObtenerResumen());
    }

    // GET /Home/Estado -> JSON con los indicadores del "Monitor de ventas".
    // El escritorio lo consulta cada 30 segundos para mostrar avisos.
    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Estado()
    {
        var r = await ObtenerResumen();
        return Json(new
        {
            r.TotalClientes,
            r.TotalProductos,
            r.ProductosStockBajo,
            r.VentasHoy,
            r.MontoHoy,
            UmbralStockBajo = DashboardViewModel.UmbralStockBajo
        });
    }

    // Página que se muestra si ocurre un error inesperado (solo fuera de desarrollo)
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task<DashboardViewModel> ObtenerResumen()
    {
        var hoy = DateTime.Today;
        var manana = hoy.AddDays(1);

        return new DashboardViewModel
        {
            TotalClientes = await _db.Clientes.CountAsync(),
            TotalProductos = await _db.Productos.CountAsync(),
            ProductosStockBajo = await _db.Productos.CountAsync(p => p.Stock <= DashboardViewModel.UmbralStockBajo),
            VentasHoy = await _db.Ventas.CountAsync(v => !v.Anulada && v.Fecha >= hoy && v.Fecha < manana),
            MontoHoy = await _db.DetallesVenta
                .Where(d => !d.Venta!.Anulada && d.Venta.Fecha >= hoy && d.Venta.Fecha < manana)
                .SumAsync(d => d.Cantidad * d.PrecioUnitario)
        };
    }
}
