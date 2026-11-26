using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

public class MetodosPagoController : Controller
{
    private readonly AppDbContext _db;

    public MetodosPagoController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /MetodosPago  (lista + búsqueda del lado del servidor)
    public async Task<IActionResult> Index(string? buscar)
    {
        var consulta = _db.MetodosPago.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            consulta = consulta.Where(m => EF.Functions.Collate(m.Nombre, Colacion.SinAcentos).Contains(t) || (m.Descripcion != null && EF.Functions.Collate(m.Descripcion, Colacion.SinAcentos).Contains(t)));
        }
        ViewData["Buscar"] = buscar;
        return View(await consulta.OrderByDescending(m => m.Activo).ThenBy(m => m.Nombre).ToListAsync());
    }

    // GET: /MetodosPago/Create
    public IActionResult Create() => View(new MetodoPago());

    // POST: /MetodosPago/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Descripcion,Activo")] MetodoPago metodo)
    {
        await Validar(metodo);
        if (!ModelState.IsValid) return View(metodo);

        _db.MetodosPago.Add(metodo);
        if (!await Guardar()) return View(metodo);

        TempData["Exito"] = $"Método de pago \"{metodo.Nombre}\" guardado.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /MetodosPago/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var metodo = await _db.MetodosPago.FindAsync(id);
        return metodo == null ? NotFound() : View(metodo);
    }

    // POST: /MetodosPago/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("MetodoPagoId,Nombre,Descripcion,Activo")] MetodoPago metodo)
    {
        if (id != metodo.MetodoPagoId) return NotFound();
        if (!await _db.MetodosPago.AnyAsync(m => m.MetodoPagoId == id)) return NotFound();

        await Validar(metodo);
        if (!ModelState.IsValid) return View(metodo);

        _db.Update(metodo);
        if (!await Guardar()) return View(metodo);

        TempData["Exito"] = $"Método de pago \"{metodo.Nombre}\" actualizado.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /MetodosPago/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var metodo = await _db.MetodosPago.AsNoTracking().FirstOrDefaultAsync(m => m.MetodoPagoId == id);
        if (metodo == null) return NotFound();

        // Si no se puede borrar, se avisa desde esta pantalla (y no se muestra el botón)
        ViewData["Bloqueo"] = await _db.Ventas.AnyAsync(v => v.MetodoPagoId == id)
            ? "Este método de pago se usó en ventas registradas (incluso anuladas) y no se puede eliminar. Edítelo y desmarque \"Disponible\"."
            : null;
        return View(metodo);
    }

    // POST: /MetodosPago/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var metodo = await _db.MetodosPago.FindAsync(id);
        if (metodo == null)
        {
            TempData["Error"] = "El método de pago ya no existe.";
            return RedirectToAction(nameof(Index));
        }
        // Si ya se usó en ventas no se borra: se marca como "no disponible"
        if (await _db.Ventas.AnyAsync(v => v.MetodoPagoId == id))
        {
            TempData["Error"] = $"No se puede eliminar \"{metodo.Nombre}\": se usó en ventas registradas. Edítelo y desmarque \"Disponible\".";
            return RedirectToAction(nameof(Index));
        }
        _db.MetodosPago.Remove(metodo);
        await _db.SaveChangesAsync();
        TempData["Exito"] = $"Método de pago \"{metodo.Nombre}\" eliminado.";
        return RedirectToAction(nameof(Index));
    }

    // ---------- Validación del lado del servidor ----------
    private async Task Validar(MetodoPago m)
    {
        m.Nombre = (m.Nombre ?? string.Empty).Trim();
        m.Descripcion = string.IsNullOrWhiteSpace(m.Descripcion) ? null : m.Descripcion.Trim();

        ModelState.Clear();
        TryValidateModel(m);

        if (m.Nombre.Length > 0 &&
            await _db.MetodosPago.AnyAsync(x => EF.Functions.Collate(x.Nombre, Colacion.SinAcentos) == m.Nombre && x.MetodoPagoId != m.MetodoPagoId))
        {
            ModelState.AddModelError(nameof(MetodoPago.Nombre), "Ya existe un método de pago con ese nombre.");
        }
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
            ModelState.AddModelError(nameof(MetodoPago.Nombre), "Ya existe un método de pago con ese nombre.");
            _db.ChangeTracker.Clear();
            return false;
        }
    }
}
