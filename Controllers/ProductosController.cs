using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

public class ProductosController : Controller
{
    private readonly AppDbContext _db;

    public ProductosController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Productos  (lista + búsqueda + filtro por categoría)
    public async Task<IActionResult> Index(string? buscar, int? categoriaId)
    {
        var consulta = _db.Productos
            .AsNoTracking()
            .Include(p => p.Categoria)
            .Include(p => p.Proveedor)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            consulta = consulta.Where(p =>
                EF.Functions.Collate(p.Nombre, Colacion.SinAcentos).Contains(t) ||
                (p.Codigo != null && p.Codigo.Contains(t)) ||
                EF.Functions.Collate(p.Categoria!.Nombre, Colacion.SinAcentos).Contains(t) ||
                EF.Functions.Collate(p.Proveedor!.Nombre, Colacion.SinAcentos).Contains(t));
        }
        if (categoriaId is > 0)
        {
            consulta = consulta.Where(p => p.CategoriaId == categoriaId);
        }

        ViewData["Buscar"] = buscar;
        ViewData["CategoriaId"] = categoriaId;
        ViewData["Categorias"] = new SelectList(
            await _db.Categorias.AsNoTracking().OrderBy(c => c.Nombre).ToListAsync(),
            nameof(Categoria.CategoriaId), nameof(Categoria.Nombre), categoriaId);

        return View(await consulta.OrderBy(p => p.Nombre).ToListAsync());
    }

    // GET: /Productos/Create
    public async Task<IActionResult> Create()
    {
        await CargarListas(null);
        return View(new Producto());
    }

    // POST: /Productos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Codigo,Nombre,CategoriaId,ProveedorId,Precio,Stock")] Producto producto)
    {
        await Validar(producto);
        if (!ModelState.IsValid)
        {
            await CargarListas(producto);
            return View(producto);
        }

        _db.Productos.Add(producto);
        if (!await Guardar())
        {
            await CargarListas(producto);
            return View(producto);
        }

        TempData["Exito"] = $"Producto \"{producto.Nombre}\" guardado.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Productos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var producto = await _db.Productos.FindAsync(id);
        if (producto == null) return NotFound();
        await CargarListas(producto);
        return View(producto);
    }

    // POST: /Productos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProductoId,Codigo,Nombre,CategoriaId,ProveedorId,Precio,Stock")] Producto producto)
    {
        if (id != producto.ProductoId) return NotFound();
        if (!await _db.Productos.AnyAsync(p => p.ProductoId == id)) return NotFound();

        await Validar(producto);
        if (!ModelState.IsValid)
        {
            await CargarListas(producto);
            return View(producto);
        }

        // Cambiar el precio NO afecta las ventas anteriores:
        // cada renglón de venta guardó su propio precio histórico.
        _db.Update(producto);
        if (!await Guardar())
        {
            await CargarListas(producto);
            return View(producto);
        }

        TempData["Exito"] = $"Producto \"{producto.Nombre}\" actualizado.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Productos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var producto = await _db.Productos
            .AsNoTracking()
            .Include(p => p.Categoria)
            .Include(p => p.Proveedor)
            .FirstOrDefaultAsync(p => p.ProductoId == id);
        if (producto == null) return NotFound();

        ViewData["TieneVentas"] = await _db.DetallesVenta.AnyAsync(d => d.ProductoId == id)
                                  || await _db.AjustesInventario.AnyAsync(a => a.ProductoId == id)
                                  || await _db.DetallesCompra.AnyAsync(d => d.ProductoId == id);
        return View(producto);
    }

    // POST: /Productos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var producto = await _db.Productos.FindAsync(id);
        if (producto == null)
        {
            TempData["Error"] = "El producto ya no existe.";
            return RedirectToAction(nameof(Index));
        }
        if (await _db.DetallesVenta.AnyAsync(d => d.ProductoId == id) ||
            await _db.AjustesInventario.AnyAsync(a => a.ProductoId == id) ||
            await _db.DetallesCompra.AnyAsync(d => d.ProductoId == id))
        {
            TempData["Error"] = $"No se puede eliminar \"{producto.Nombre}\": ya aparece en ventas, compras o ajustes de inventario.";
            return RedirectToAction(nameof(Index));
        }

        _db.Productos.Remove(producto);
        await _db.SaveChangesAsync();
        TempData["Exito"] = $"Producto \"{producto.Nombre}\" eliminado.";
        return RedirectToAction(nameof(Index));
    }

    // ---------- Listas desplegables ----------
    private async Task CargarListas(Producto? p)
    {
        ViewData["Categorias"] = new SelectList(
            await _db.Categorias.AsNoTracking().OrderBy(c => c.Nombre).ToListAsync(),
            nameof(Categoria.CategoriaId), nameof(Categoria.Nombre), p?.CategoriaId);
        ViewData["Proveedores"] = new SelectList(
            await _db.Proveedores.AsNoTracking().OrderBy(x => x.Nombre).ToListAsync(),
            nameof(Proveedor.ProveedorId), nameof(Proveedor.Nombre), p?.ProveedorId);
    }

    // ---------- Validación del lado del servidor ----------
    private async Task Validar(Producto p)
    {
        p.Nombre = (p.Nombre ?? string.Empty).Trim();
        p.Codigo = string.IsNullOrWhiteSpace(p.Codigo) ? null : p.Codigo.Trim();

        ModelState.Clear();
        TryValidateModel(p);

        if (p.CategoriaId > 0 && !await _db.Categorias.AnyAsync(c => c.CategoriaId == p.CategoriaId))
            ModelState.AddModelError(nameof(Producto.CategoriaId), "La categoría seleccionada no existe.");

        if (p.ProveedorId > 0 && !await _db.Proveedores.AnyAsync(x => x.ProveedorId == p.ProveedorId))
            ModelState.AddModelError(nameof(Producto.ProveedorId), "El proveedor seleccionado no existe.");

        if (p.Codigo != null &&
            await _db.Productos.AnyAsync(x => x.Codigo == p.Codigo && x.ProductoId != p.ProductoId))
            ModelState.AddModelError(nameof(Producto.Codigo), "Ya existe un producto con ese código de barras.");

        if (p.Nombre.Length > 0 &&
            await _db.Productos.AnyAsync(x => EF.Functions.Collate(x.Nombre, Colacion.SinAcentos) == p.Nombre
                                              && x.ProveedorId == p.ProveedorId
                                              && x.ProductoId != p.ProductoId))
            ModelState.AddModelError(nameof(Producto.Nombre), "Ese proveedor ya tiene un producto con ese nombre.");
    }

    private async Task<bool> Guardar()
    {
        try
        {
            await _db.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "No se pudo guardar: revise que el código de barras no esté repetido.");
            _db.ChangeTracker.Clear();
            return false;
        }
    }
}
