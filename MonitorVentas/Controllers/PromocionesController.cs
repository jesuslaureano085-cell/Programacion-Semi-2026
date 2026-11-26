using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

/// <summary>
/// Promociones (formulario INTERMEDIO).
/// Un descuento por fechas para un producto o para toda una categoría.
/// La caja (Ventas) aplica automáticamente el mejor descuento vigente.
/// </summary>
public class PromocionesController : Controller
{
    private readonly AppDbContext _db;

    public PromocionesController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Promociones  (lista + búsqueda + filtro por estado)
    public async Task<IActionResult> Index(string? buscar, string? estado)
    {
        var consulta = _db.Promociones
            .AsNoTracking()
            .Include(p => p.Producto)
            .Include(p => p.Categoria)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            consulta = consulta.Where(p =>
                EF.Functions.Collate(p.Nombre, Colacion.SinAcentos).Contains(t) ||
                (p.Producto != null && EF.Functions.Collate(p.Producto.Nombre, Colacion.SinAcentos).Contains(t)) ||
                (p.Categoria != null && EF.Functions.Collate(p.Categoria.Nombre, Colacion.SinAcentos).Contains(t)));
        }

        var hoy = DateTime.Today;
        consulta = estado switch
        {
            "Vigente" => consulta.Where(p => p.Activa && p.FechaInicio <= hoy && p.FechaFin >= hoy),
            "Programada" => consulta.Where(p => p.Activa && p.FechaInicio > hoy),
            "Vencida" => consulta.Where(p => p.Activa && p.FechaFin < hoy),
            "Inactiva" => consulta.Where(p => !p.Activa),
            _ => consulta
        };

        ViewData["Buscar"] = buscar;
        ViewData["Estado"] = estado;
        return View(await consulta.OrderByDescending(p => p.FechaInicio).ThenBy(p => p.Nombre).ToListAsync());
    }

    // GET: /Promociones/Create
    public async Task<IActionResult> Create()
    {
        await CargarListas(null);
        return View(new Promocion());
    }

    // POST: /Promociones/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Alcance,ProductoId,CategoriaId,Porcentaje,FechaInicio,FechaFin,Activa")] Promocion promocion)
    {
        await Validar(promocion);
        if (!ModelState.IsValid)
        {
            await CargarListas(promocion);
            return View(promocion);
        }

        _db.Promociones.Add(promocion);
        await _db.SaveChangesAsync();
        TempData["Exito"] = $"Promoción \"{promocion.Nombre}\" guardada ({promocion.Estado.ToLower()}).";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Promociones/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var promocion = await _db.Promociones.FindAsync(id);
        if (promocion == null) return NotFound();
        promocion.Alcance = promocion.CategoriaId != null ? "categoria" : "producto";
        await CargarListas(promocion);
        return View(promocion);
    }

    // POST: /Promociones/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("PromocionId,Nombre,Alcance,ProductoId,CategoriaId,Porcentaje,FechaInicio,FechaFin,Activa")] Promocion promocion)
    {
        if (id != promocion.PromocionId) return NotFound();
        if (!await _db.Promociones.AnyAsync(p => p.PromocionId == id)) return NotFound();

        await Validar(promocion);
        if (!ModelState.IsValid)
        {
            await CargarListas(promocion);
            return View(promocion);
        }

        _db.Update(promocion);
        await _db.SaveChangesAsync();
        TempData["Exito"] = $"Promoción \"{promocion.Nombre}\" actualizada ({promocion.Estado.ToLower()}).";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Promociones/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var promocion = await _db.Promociones.AsNoTracking()
            .Include(p => p.Producto)
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.PromocionId == id);
        return promocion == null ? NotFound() : View(promocion);
    }

    // POST: /Promociones/Delete/5
    // Borrar una promoción no cambia las ventas ya hechas: cada ticket guardó su precio con descuento.
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var promocion = await _db.Promociones.FindAsync(id);
        if (promocion == null)
        {
            TempData["Error"] = "La promoción ya no existe.";
            return RedirectToAction(nameof(Index));
        }
        _db.Promociones.Remove(promocion);
        await _db.SaveChangesAsync();
        TempData["Exito"] = $"Promoción \"{promocion.Nombre}\" eliminada.";
        return RedirectToAction(nameof(Index));
    }

    // ---------- Apoyo ----------
    private async Task CargarListas(Promocion? p)
    {
        ViewData["Productos"] = new SelectList(
            await _db.Productos.AsNoTracking().OrderBy(x => x.Nombre).ToListAsync(),
            nameof(Producto.ProductoId), nameof(Producto.Nombre), p?.ProductoId);
        ViewData["Categorias"] = new SelectList(
            await _db.Categorias.AsNoTracking().OrderBy(x => x.Nombre).ToListAsync(),
            nameof(Categoria.CategoriaId), nameof(Categoria.Nombre), p?.CategoriaId);
    }

    private async Task Validar(Promocion p)
    {
        p.Nombre = (p.Nombre ?? string.Empty).Trim();
        p.Alcance = p.Alcance == "categoria" ? "categoria" : "producto";

        // Solo se guarda el destino elegido; el otro queda vacío
        if (p.Alcance == "producto") p.CategoriaId = null; else p.ProductoId = null;
        if (p.ProductoId is <= 0) p.ProductoId = null;
        if (p.CategoriaId is <= 0) p.CategoriaId = null;

        ModelState.Clear();
        TryValidateModel(p);

        if (p.Alcance == "producto")
        {
            if (p.ProductoId == null)
                ModelState.AddModelError(nameof(Promocion.ProductoId), "Seleccione el producto.");
            else if (!await _db.Productos.AnyAsync(x => x.ProductoId == p.ProductoId))
                ModelState.AddModelError(nameof(Promocion.ProductoId), "El producto no existe.");
        }
        else
        {
            if (p.CategoriaId == null)
                ModelState.AddModelError(nameof(Promocion.CategoriaId), "Seleccione la categoría.");
            else if (!await _db.Categorias.AnyAsync(x => x.CategoriaId == p.CategoriaId))
                ModelState.AddModelError(nameof(Promocion.CategoriaId), "La categoría no existe.");
        }

        if (p.FechaInicio.Year < 2000)
            ModelState.AddModelError(nameof(Promocion.FechaInicio), "Ingrese una fecha de inicio válida.");
        if (p.FechaFin.Date < p.FechaInicio.Date)
            ModelState.AddModelError(nameof(Promocion.FechaFin), "La fecha final no puede ser anterior a la de inicio.");
        if ((p.FechaFin.Date - p.FechaInicio.Date).TotalDays > 366)
            ModelState.AddModelError(nameof(Promocion.FechaFin), "Una promoción no puede durar más de un año.");
    }
}
