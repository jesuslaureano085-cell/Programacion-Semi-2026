using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

public class ProveedoresController : Controller
{
    private readonly AppDbContext _db;

    public ProveedoresController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Proveedores  (lista + búsqueda del lado del servidor)
    public async Task<IActionResult> Index(string? buscar)
    {
        var consulta = _db.Proveedores.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            consulta = consulta.Where(p =>
                EF.Functions.Collate(p.Nombre, Colacion.SinAcentos).Contains(t) ||
                p.Telefono.Contains(t) ||
                (p.Contacto != null && EF.Functions.Collate(p.Contacto, Colacion.SinAcentos).Contains(t)) ||
                (p.Correo != null && EF.Functions.Collate(p.Correo, Colacion.SinAcentos).Contains(t)) ||
                (p.Direccion != null && EF.Functions.Collate(p.Direccion, Colacion.SinAcentos).Contains(t)));
        }
        ViewData["Buscar"] = buscar;
        return View(await consulta.OrderBy(p => p.Nombre).ToListAsync());
    }

    // GET: /Proveedores/Create
    public IActionResult Create() => View(new Proveedor());

    // POST: /Proveedores/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Contacto,Telefono,Correo,Direccion")] Proveedor proveedor)
    {
        await Validar(proveedor);
        if (!ModelState.IsValid) return View(proveedor);

        _db.Proveedores.Add(proveedor);
        if (!await Guardar()) return View(proveedor);

        TempData["Exito"] = $"Proveedor \"{proveedor.Nombre}\" guardado.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Proveedores/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var proveedor = await _db.Proveedores.FindAsync(id);
        return proveedor == null ? NotFound() : View(proveedor);
    }

    // POST: /Proveedores/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProveedorId,Nombre,Contacto,Telefono,Correo,Direccion")] Proveedor proveedor)
    {
        if (id != proveedor.ProveedorId) return NotFound();
        if (!await _db.Proveedores.AnyAsync(p => p.ProveedorId == id)) return NotFound();

        await Validar(proveedor);
        if (!ModelState.IsValid) return View(proveedor);

        _db.Update(proveedor);
        if (!await Guardar()) return View(proveedor);

        TempData["Exito"] = $"Proveedor \"{proveedor.Nombre}\" actualizado.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Proveedores/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var proveedor = await _db.Proveedores.AsNoTracking().FirstOrDefaultAsync(p => p.ProveedorId == id);
        if (proveedor == null) return NotFound();

        // Si no se puede borrar, se avisa desde esta pantalla (y no se muestra el botón)
        ViewData["Bloqueo"] = await _db.Productos.AnyAsync(p => p.ProveedorId == id) || await _db.Compras.AnyAsync(c => c.ProveedorId == id)
            ? "Este proveedor tiene productos o compras registradas y no se puede eliminar."
            : null;
        return View(proveedor);
    }

    // POST: /Proveedores/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var proveedor = await _db.Proveedores.FindAsync(id);
        if (proveedor == null)
        {
            TempData["Error"] = "El proveedor ya no existe.";
            return RedirectToAction(nameof(Index));
        }
        if (await _db.Productos.AnyAsync(p => p.ProveedorId == id) || await _db.Compras.AnyAsync(c => c.ProveedorId == id))
        {
            TempData["Error"] = $"No se puede eliminar \"{proveedor.Nombre}\": tiene productos o compras registradas.";
            return RedirectToAction(nameof(Index));
        }
        _db.Proveedores.Remove(proveedor);
        await _db.SaveChangesAsync();
        TempData["Exito"] = $"Proveedor \"{proveedor.Nombre}\" eliminado.";
        return RedirectToAction(nameof(Index));
    }

    // ---------- Validación del lado del servidor ----------
    private async Task Validar(Proveedor p)
    {
        p.Nombre = (p.Nombre ?? string.Empty).Trim();
        p.Telefono = (p.Telefono ?? string.Empty).Trim();
        p.Contacto = string.IsNullOrWhiteSpace(p.Contacto) ? null : p.Contacto.Trim();
        p.Correo = string.IsNullOrWhiteSpace(p.Correo) ? null : p.Correo.Trim().ToLowerInvariant();
        p.Direccion = string.IsNullOrWhiteSpace(p.Direccion) ? null : p.Direccion.Trim();

        ModelState.Clear();
        TryValidateModel(p);

        if (p.Nombre.Length > 0 &&
            await _db.Proveedores.AnyAsync(x => EF.Functions.Collate(x.Nombre, Colacion.SinAcentos) == p.Nombre && x.ProveedorId != p.ProveedorId))
        {
            ModelState.AddModelError(nameof(Proveedor.Nombre), "Ya existe un proveedor con ese nombre.");
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
            ModelState.AddModelError(nameof(Proveedor.Nombre), "Ya existe un proveedor con ese nombre.");
            _db.ChangeTracker.Clear();
            return false;
        }
    }
}
