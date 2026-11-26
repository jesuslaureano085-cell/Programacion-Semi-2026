using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

public class ClientesController : Controller
{
    private readonly AppDbContext _db;

    public ClientesController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Clientes  (lista + búsqueda)
    public async Task<IActionResult> Index(string? buscar)
    {
        var consulta = _db.Clientes.AsNoTracking();

        // Búsqueda del lado del servidor (funciona aunque JavaScript esté apagado).
        // La búsqueda en tiempo real la hace el script de la vista.
        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var texto = buscar.Trim();
            consulta = consulta.Where(c =>
                EF.Functions.Collate(c.Nombre, Colacion.SinAcentos).Contains(texto) ||
                c.Telefono.Contains(texto) ||
                (c.Direccion != null && EF.Functions.Collate(c.Direccion, Colacion.SinAcentos).Contains(texto)));
        }

        ViewData["Buscar"] = buscar;
        var clientes = await consulta.OrderBy(c => c.Nombre).ToListAsync();
        return View(clientes);
    }

    // GET: /Clientes/Create
    public IActionResult Create()
    {
        return View(new Cliente());
    }

    // POST: /Clientes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Telefono,Direccion")] Cliente cliente)
    {
        Normalizar(cliente);
        await ValidarDuplicado(cliente);

        if (!ModelState.IsValid)
        {
            return View(cliente);
        }

        _db.Clientes.Add(cliente);
        if (!await Guardar()) return View(cliente);
        TempData["Exito"] = $"Cliente \"{cliente.Nombre}\" guardado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Clientes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var cliente = await _db.Clientes.FindAsync(id);
        if (cliente == null) return NotFound();

        return View(cliente);
    }

    // POST: /Clientes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Nombre,Telefono,Direccion")] Cliente cliente)
    {
        if (id != cliente.ClienteId) return NotFound();

        Normalizar(cliente);
        await ValidarDuplicado(cliente);

        if (!ModelState.IsValid)
        {
            return View(cliente);
        }

        if (!await _db.Clientes.AnyAsync(c => c.ClienteId == id)) return NotFound();

        _db.Update(cliente);
        if (!await Guardar()) return View(cliente);

        TempData["Exito"] = $"Cliente \"{cliente.Nombre}\" actualizado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Clientes/Delete/5  (pantalla de confirmación)
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var cliente = await _db.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ClienteId == id);
        if (cliente == null) return NotFound();

        ViewData["TieneVentas"] = await _db.Ventas.AnyAsync(v => v.ClienteId == id);
        return View(cliente);
    }

    // POST: /Clientes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cliente = await _db.Clientes.FindAsync(id);
        if (cliente == null)
        {
            TempData["Error"] = "El cliente ya no existe.";
            return RedirectToAction(nameof(Index));
        }

        // Regla de negocio: un cliente con ventas registradas no se puede borrar
        if (await _db.Ventas.AnyAsync(v => v.ClienteId == id))
        {
            TempData["Error"] = $"No se puede eliminar a \"{cliente.Nombre}\": tiene ventas registradas.";
            return RedirectToAction(nameof(Index));
        }

        _db.Clientes.Remove(cliente);
        await _db.SaveChangesAsync();
        TempData["Exito"] = $"Cliente \"{cliente.Nombre}\" eliminado.";
        return RedirectToAction(nameof(Index));
    }

    // Quita espacios sobrantes antes de validar y guardar
    // Se limpian los espacios y se valida de nuevo, para que nada
    // del navegador se pueda saltar las reglas.
    private void Normalizar(Cliente c)
    {
        c.Nombre = (c.Nombre ?? string.Empty).Trim();
        c.Telefono = (c.Telefono ?? string.Empty).Trim();
        c.Direccion = string.IsNullOrWhiteSpace(c.Direccion) ? null : c.Direccion.Trim();
        ModelState.Clear();
        TryValidateModel(c);
    }

    // Guarda y convierte un teléfono duplicado (por ejemplo, dos personas
    // guardando a la vez) en un mensaje entendible en lugar de un error de SQL.
    private async Task<bool> Guardar()
    {
        try
        {
            await _db.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(Cliente.Telefono), "Ya existe un cliente con ese teléfono.");
            _db.ChangeTracker.Clear();
            return false;
        }
    }

    // Evita registrar dos clientes con el mismo teléfono
    private async Task ValidarDuplicado(Cliente c)
    {
        if (string.IsNullOrEmpty(c.Telefono)) return;

        bool existe = await _db.Clientes.AnyAsync(x =>
            x.Telefono == c.Telefono && x.ClienteId != c.ClienteId);

        if (existe)
        {
            ModelState.AddModelError(nameof(Cliente.Telefono), "Ya existe un cliente con ese teléfono.");
        }
    }
}
