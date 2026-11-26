using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

/// <summary>
/// Restablecer sistema: borra TODOS los datos registrados y deja Monitor como recién instalado
/// (solo con los métodos de pago y categorías iniciales). Los números (ID) vuelven a empezar en 1.
/// Pide escribir la palabra RESTABLECER para evitar accidentes.
/// </summary>
public class SistemaController : Controller
{
    private const string Confirmacion = "RESTABLECER";
    private readonly AppDbContext _db;
    public SistemaController(AppDbContext db) => _db = db;

    // GET: /Sistema/Restablecer
    public async Task<IActionResult> Restablecer()
    {
        await CargarConteos();
        return View();
    }

    // POST: /Sistema/Restablecer
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Restablecer(string? confirmacion)
    {
        if (!string.Equals(confirmacion?.Trim(), Confirmacion, StringComparison.OrdinalIgnoreCase))
        {
            ModelState.AddModelError(string.Empty, $"Para continuar escriba la palabra {Confirmacion}.");
            await CargarConteos();
            return View();
        }

        await using var transaccion = await _db.Database.BeginTransactionAsync();

        // 1) Se borra del detalle hacia el encabezado (por las llaves foráneas)
        await _db.DetallesDevolucion.ExecuteDeleteAsync();
        await _db.Devoluciones.ExecuteDeleteAsync();
        await _db.DetallesVenta.ExecuteDeleteAsync();
        await _db.Ventas.ExecuteDeleteAsync();
        await _db.DetallesCompra.ExecuteDeleteAsync();
        await _db.Compras.ExecuteDeleteAsync();
        await _db.AjustesInventario.ExecuteDeleteAsync();
        await _db.Promociones.ExecuteDeleteAsync();
        await _db.Productos.ExecuteDeleteAsync();
        await _db.Clientes.ExecuteDeleteAsync();
        await _db.Proveedores.ExecuteDeleteAsync();
        await _db.Empleados.ExecuteDeleteAsync();
        await _db.MetodosPago.ExecuteDeleteAsync();
        await _db.Categorias.ExecuteDeleteAsync();

        // 2) Los contadores de ID vuelven a empezar en 1
        foreach (var tipo in _db.Model.GetEntityTypes())
        {
            var tabla = tipo.GetTableName();
            if (tabla == null) continue;
            var esquema = tipo.GetSchema() ?? "dbo";
            await _db.Database.ExecuteSqlRawAsync(
                $"IF EXISTS (SELECT 1 FROM sys.identity_columns WHERE object_id = OBJECT_ID(N'[{esquema}].[{tabla}]') AND last_value IS NOT NULL) " +
                $"DBCC CHECKIDENT (N'[{esquema}].[{tabla}]', RESEED, 0);");
        }

        // 3) Se vuelven a crear los datos iniciales (los mismos que tiene la base al crearse).
        //    Se insertan sin ID para que la base los numere 1, 2, 3... igual que al principio.
        foreach (var m in AppDbContext.MetodosPagoIniciales()) { m.MetodoPagoId = 0; _db.MetodosPago.Add(m); await _db.SaveChangesAsync(); }
        foreach (var c in AppDbContext.CategoriasIniciales()) { c.CategoriaId = 0; _db.Categorias.Add(c); await _db.SaveChangesAsync(); }

        await transaccion.CommitAsync();

        ViewData["Listo"] = true;
        await CargarConteos();
        return View();
    }

    private async Task CargarConteos()
    {
        ViewData["Conteos"] = new (string, int)[]
        {
            ("Ventas", await _db.Ventas.CountAsync()),
            ("Compras", await _db.Compras.CountAsync()),
            ("Devoluciones", await _db.Devoluciones.CountAsync()),
            ("Ajustes de inventario", await _db.AjustesInventario.CountAsync()),
            ("Promociones", await _db.Promociones.CountAsync()),
            ("Productos", await _db.Productos.CountAsync()),
            ("Clientes", await _db.Clientes.CountAsync()),
            ("Proveedores", await _db.Proveedores.CountAsync()),
            ("Empleados", await _db.Empleados.CountAsync()),
            ("Categorías", await _db.Categorias.CountAsync()),
            ("Métodos de pago", await _db.MetodosPago.CountAsync())
        };
    }
}
