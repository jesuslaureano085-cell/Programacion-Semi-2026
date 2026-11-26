using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

public class EmpleadosController : Controller
{
    private static readonly DateTime FechaMinima = new(1950, 1, 1);
    private readonly AppDbContext _db;

    public EmpleadosController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Empleados  (lista + búsqueda del lado del servidor)
    public async Task<IActionResult> Index(string? buscar)
    {
        var consulta = _db.Empleados.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            consulta = consulta.Where(e =>
                EF.Functions.Collate(e.Nombre, Colacion.SinAcentos).Contains(t) ||
                e.Dui.Contains(t) ||
                EF.Functions.Collate(e.Cargo, Colacion.SinAcentos).Contains(t) ||
                (e.Telefono != null && e.Telefono.Contains(t)));
        }
        ViewData["Buscar"] = buscar;
        return View(await consulta.OrderByDescending(e => e.Activo).ThenBy(e => e.Nombre).ToListAsync());
    }

    // GET: /Empleados/Create
    public IActionResult Create() => View(new Empleado());

    // POST: /Empleados/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Dui,Cargo,Telefono,FechaIngreso,Activo")] Empleado empleado)
    {
        await Validar(empleado);
        if (!ModelState.IsValid) return View(empleado);

        _db.Empleados.Add(empleado);
        if (!await Guardar()) return View(empleado);

        TempData["Exito"] = $"Empleado \"{empleado.Nombre}\" guardado.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Empleados/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var empleado = await _db.Empleados.FindAsync(id);
        return empleado == null ? NotFound() : View(empleado);
    }

    // POST: /Empleados/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("EmpleadoId,Nombre,Dui,Cargo,Telefono,FechaIngreso,Activo")] Empleado empleado)
    {
        if (id != empleado.EmpleadoId) return NotFound();
        if (!await _db.Empleados.AnyAsync(e => e.EmpleadoId == id)) return NotFound();

        await Validar(empleado);
        if (!ModelState.IsValid) return View(empleado);

        _db.Update(empleado);
        if (!await Guardar()) return View(empleado);

        TempData["Exito"] = $"Empleado \"{empleado.Nombre}\" actualizado.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Empleados/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var empleado = await _db.Empleados.AsNoTracking().FirstOrDefaultAsync(e => e.EmpleadoId == id);
        if (empleado == null) return NotFound();

        // Si no se puede borrar, se avisa desde esta pantalla (y no se muestra el botón)
        ViewData["Bloqueo"] = await TieneMovimientos(id)
            ? "Este empleado tiene ventas, compras, devoluciones o ajustes registrados y no se puede eliminar, porque se perdería el historial. Edítelo y desmarque \"Activo\"."
            : null;
        return View(empleado);
    }

    // POST: /Empleados/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var empleado = await _db.Empleados.FindAsync(id);
        if (empleado == null)
        {
            TempData["Error"] = "El empleado ya no existe.";
            return RedirectToAction(nameof(Index));
        }
        // Si ya registró ventas no se borra (se perdería el historial): se marca como inactivo
        if (await TieneMovimientos(id))
        {
            TempData["Error"] = $"No se puede eliminar a \"{empleado.Nombre}\": tiene movimientos registrados (ventas, compras, devoluciones o ajustes). Edítelo y desmarque \"Activo\".";
            return RedirectToAction(nameof(Index));
        }
        _db.Empleados.Remove(empleado);
        await _db.SaveChangesAsync();
        TempData["Exito"] = $"Empleado \"{empleado.Nombre}\" eliminado.";
        return RedirectToAction(nameof(Index));
    }

    // ¿El empleado aparece en algún movimiento? (entonces no se borra, se inactiva)
    private async Task<bool> TieneMovimientos(int? id) =>
        await _db.Ventas.AnyAsync(v => v.EmpleadoId == id) ||
        await _db.AjustesInventario.AnyAsync(a => a.EmpleadoId == id) ||
        await _db.Compras.AnyAsync(c => c.EmpleadoId == id) ||
        await _db.Devoluciones.AnyAsync(d => d.EmpleadoId == id);

    // ---------- Validación del lado del servidor ----------
    private async Task Validar(Empleado e)
    {
        e.Nombre = (e.Nombre ?? string.Empty).Trim();
        e.Dui = (e.Dui ?? string.Empty).Trim();
        e.Cargo = (e.Cargo ?? string.Empty).Trim();
        e.Telefono = string.IsNullOrWhiteSpace(e.Telefono) ? null : e.Telefono.Trim();

        // Si escribieron los 9 dígitos sin guion, se lo ponemos
        if (e.Dui.Length == 9 && e.Dui.All(char.IsDigit))
        {
            e.Dui = e.Dui[..8] + "-" + e.Dui[8];
        }

        ModelState.Clear();
        TryValidateModel(e);

        if (e.Cargo.Length > 0 && !Empleado.Cargos.Contains(e.Cargo))
        {
            ModelState.AddModelError(nameof(Empleado.Cargo), "Seleccione un cargo de la lista.");
        }

        if (e.FechaIngreso.Date > DateTime.Today)
        {
            ModelState.AddModelError(nameof(Empleado.FechaIngreso), "La fecha de ingreso no puede ser futura.");
        }
        else if (e.FechaIngreso.Date < FechaMinima)
        {
            ModelState.AddModelError(nameof(Empleado.FechaIngreso), "Ingrese una fecha de ingreso válida.");
        }

        if (e.Dui.Length > 0 &&
            await _db.Empleados.AnyAsync(x => x.Dui == e.Dui && x.EmpleadoId != e.EmpleadoId))
        {
            ModelState.AddModelError(nameof(Empleado.Dui), "Ya existe un empleado con ese DUI.");
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
            ModelState.AddModelError(nameof(Empleado.Dui), "No se pudo guardar: revise que el DUI no esté repetido y tenga el formato 00000000-0.");
            _db.ChangeTracker.Clear();
            return false;
        }
    }
}
