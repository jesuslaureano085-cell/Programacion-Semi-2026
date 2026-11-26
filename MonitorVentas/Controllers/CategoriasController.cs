using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

public class CategoriasController : Controller
{
    private readonly AppDbContext _db;

    public CategoriasController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Categorias  (lista + búsqueda del lado del servidor)
    public async Task<IActionResult> Index(string? buscar)
    {
        var consulta = _db.Categorias.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            consulta = consulta.Where(c => EF.Functions.Collate(c.Nombre, Colacion.SinAcentos).Contains(t) || (c.Descripcion != null && EF.Functions.Collate(c.Descripcion, Colacion.SinAcentos).Contains(t)));
        }
        ViewData["Buscar"] = buscar;
        return View(await consulta.OrderBy(c => c.Nombre).ToListAsync());
    }

    // GET: /Categorias/Create
    public IActionResult Create() => View(new Categoria());

    // POST: /Categorias/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Descripcion")] Categoria categoria)
    {
        await Validar(categoria);
        if (!ModelState.IsValid) return View(categoria);

        _db.Categorias.Add(categoria);
        if (!await Guardar(categoria)) return View(categoria);

        TempData["Exito"] = $"Categoría \"{categoria.Nombre}\" guardada.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Categorias/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var categoria = await _db.Categorias.FindAsync(id);
        return categoria == null ? NotFound() : View(categoria);
    }

    // POST: /Categorias/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CategoriaId,Nombre,Descripcion")] Categoria categoria)
    {
        if (id != categoria.CategoriaId) return NotFound();
        if (!await _db.Categorias.AnyAsync(c => c.CategoriaId == id)) return NotFound();

        await Validar(categoria);
        if (!ModelState.IsValid) return View(categoria);

        _db.Update(categoria);
        if (!await Guardar(categoria)) return View(categoria);

        TempData["Exito"] = $"Categoría \"{categoria.Nombre}\" actualizada.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Categorias/Delete/5  (pantalla de confirmación)
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var categoria = await _db.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.CategoriaId == id);
        if (categoria == null) return NotFound();

        // Si no se puede borrar, se avisa desde esta pantalla (y no se muestra el botón)
        ViewData["Bloqueo"] = await _db.Productos.AnyAsync(p => p.CategoriaId == id)
            ? "Esta categoría tiene productos asignados y no se puede eliminar. Primero cambie esos productos a otra categoría."
            : null;
        return View(categoria);
    }

    // POST: /Categorias/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var categoria = await _db.Categorias.FindAsync(id);
        if (categoria == null)
        {
            TempData["Error"] = "La categoría ya no existe.";
            return RedirectToAction(nameof(Index));
        }
        if (await _db.Productos.AnyAsync(p => p.CategoriaId == id))
        {
            TempData["Error"] = $"No se puede eliminar \"{categoria.Nombre}\": tiene productos asignados.";
            return RedirectToAction(nameof(Index));
        }
        _db.Categorias.Remove(categoria);
        await _db.SaveChangesAsync();
        TempData["Exito"] = $"Categoría \"{categoria.Nombre}\" eliminada.";
        return RedirectToAction(nameof(Index));
    }

    // ---------- Validación del lado del servidor ----------
    // Se limpian los espacios y se valida de nuevo, para que nada
    // del navegador se pueda saltar las reglas.
    private async Task Validar(Categoria c)
    {
        c.Nombre = (c.Nombre ?? string.Empty).Trim();
        c.Descripcion = string.IsNullOrWhiteSpace(c.Descripcion) ? null : c.Descripcion.Trim();

        ModelState.Clear();
        TryValidateModel(c);

        if (c.Nombre.Length > 0 &&
            await _db.Categorias.AnyAsync(x => EF.Functions.Collate(x.Nombre, Colacion.SinAcentos) == c.Nombre && x.CategoriaId != c.CategoriaId))
        {
            ModelState.AddModelError(nameof(Categoria.Nombre), "Ya existe una categoría con ese nombre.");
        }
    }

    // Guarda y convierte un posible duplicado (por ejemplo, dos personas guardando
    // a la vez) en un mensaje entendible en lugar de un error de SQL.
    private async Task<bool> Guardar(Categoria c)
    {
        try
        {
            await _db.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(Categoria.Nombre), "Ya existe una categoría con ese nombre.");
            _db.ChangeTracker.Clear();
            return false;
        }
    }
}
