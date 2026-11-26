using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;
using MonitorVentas.Models;

namespace MonitorVentas.Controllers;

/// <summary>
/// Ajustes de inventario (formulario INTERMEDIO).
/// Cada operación mantiene el stock correcto:
///  - Crear:    aplica el ajuste (+ entrada / - salida).
///  - Editar:   deshace el efecto anterior y aplica el nuevo.
///  - Eliminar: deshace el efecto del ajuste.
/// Todo dentro de una transacción y sin permitir stock negativo.
/// </summary>
public class AjustesInventarioController : Controller
{
    private readonly AppDbContext _db;

    public AjustesInventarioController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /AjustesInventario  (lista + búsqueda + filtro por tipo)
    public async Task<IActionResult> Index(string? buscar, string? tipo)
    {
        var consulta = _db.AjustesInventario
            .AsNoTracking()
            .Include(a => a.Producto)
            .Include(a => a.Empleado)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            consulta = consulta.Where(a =>
                EF.Functions.Collate(a.Producto!.Nombre, Colacion.SinAcentos).Contains(t) ||
                EF.Functions.Collate(a.Motivo, Colacion.SinAcentos).Contains(t) ||
                EF.Functions.Collate(a.Empleado!.Nombre, Colacion.SinAcentos).Contains(t) ||
                (a.Observacion != null && EF.Functions.Collate(a.Observacion, Colacion.SinAcentos).Contains(t)));
        }
        if (tipo is "Entrada" or "Salida")
        {
            consulta = consulta.Where(a => a.Tipo == tipo);
        }

        ViewData["Buscar"] = buscar;
        ViewData["Tipo"] = tipo;
        return View(await consulta.OrderByDescending(a => a.Fecha).ToListAsync());
    }

    // GET: /AjustesInventario/Create
    public async Task<IActionResult> Create()
    {
        await CargarListas(null);
        return View(new AjusteInventario());
    }

    // POST: /AjustesInventario/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProductoId,EmpleadoId,Tipo,Motivo,Cantidad,Observacion")] AjusteInventario ajuste)
    {
        await Validar(ajuste);
        if (!ModelState.IsValid)
        {
            await CargarListas(ajuste);
            return View(ajuste);
        }

        await using var transaccion = await _db.Database.BeginTransactionAsync();
        var producto = await _db.Productos.FirstAsync(p => p.ProductoId == ajuste.ProductoId);

        if (producto.Stock + ajuste.Efecto < 0)
        {
            ModelState.AddModelError(nameof(AjusteInventario.Cantidad),
                $"No hay suficiente stock: \"{producto.Nombre}\" tiene {producto.Stock} unidades.");
            await CargarListas(ajuste);
            return View(ajuste);
        }

        ajuste.Fecha = DateTime.Now;
        producto.Stock += ajuste.Efecto;
        _db.AjustesInventario.Add(ajuste);
        await _db.SaveChangesAsync();
        await transaccion.CommitAsync();

        TempData["Exito"] = $"Ajuste registrado: {producto.Nombre} queda con {producto.Stock} unidades.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /AjustesInventario/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var ajuste = await _db.AjustesInventario.AsNoTracking()
            .Include(a => a.Producto)
            .FirstOrDefaultAsync(a => a.AjusteInventarioId == id);
        if (ajuste == null) return NotFound();
        await CargarListas(ajuste);
        return View(ajuste);
    }

    // POST: /AjustesInventario/Edit/5
    // El producto no se cambia al editar (si se equivocó de producto, se elimina y se crea otro).
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("AjusteInventarioId,EmpleadoId,Tipo,Motivo,Cantidad,Observacion")] AjusteInventario datos)
    {
        if (id != datos.AjusteInventarioId) return NotFound();

        await using var transaccion = await _db.Database.BeginTransactionAsync();
        var ajuste = await _db.AjustesInventario.Include(a => a.Producto).FirstOrDefaultAsync(a => a.AjusteInventarioId == id);
        if (ajuste == null) return NotFound();

        datos.ProductoId = ajuste.ProductoId;
        datos.Producto = ajuste.Producto;
        await Validar(datos);
        if (!ModelState.IsValid)
        {
            await CargarListas(datos);
            return View(datos);
        }

        var producto = ajuste.Producto!;
        var stockResultante = producto.Stock - ajuste.Efecto + datos.Efecto;   // deshacer el anterior + aplicar el nuevo
        if (stockResultante < 0)
        {
            ModelState.AddModelError(nameof(AjusteInventario.Cantidad),
                $"Con este cambio \"{producto.Nombre}\" quedaría con stock negativo ({stockResultante}). Hoy tiene {producto.Stock}.");
            await CargarListas(datos);
            return View(datos);
        }

        producto.Stock = stockResultante;
        ajuste.EmpleadoId = datos.EmpleadoId;
        ajuste.Tipo = datos.Tipo;
        ajuste.Motivo = datos.Motivo;
        ajuste.Cantidad = datos.Cantidad;
        ajuste.Observacion = datos.Observacion;
        await _db.SaveChangesAsync();
        await transaccion.CommitAsync();

        TempData["Exito"] = $"Ajuste actualizado: {producto.Nombre} queda con {producto.Stock} unidades.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /AjustesInventario/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var ajuste = await _db.AjustesInventario.AsNoTracking()
            .Include(a => a.Producto)
            .Include(a => a.Empleado)
            .FirstOrDefaultAsync(a => a.AjusteInventarioId == id);
        if (ajuste == null) return NotFound();

        // Borrar una ENTRADA quita unidades: si ya se vendieron, no alcanza
        if (ajuste.Producto != null && ajuste.Producto.Stock - ajuste.Efecto < 0)
        {
            ViewData["Bloqueo"] = $"No se puede eliminar: deshacer esta entrada dejaría \"{ajuste.Producto.Nombre}\" con stock negativo " +
                                  $"(hoy tiene {ajuste.Producto.Stock}). Probablemente esas unidades ya se vendieron.";
        }
        return View(ajuste);
    }

    // POST: /AjustesInventario/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await using var transaccion = await _db.Database.BeginTransactionAsync();
        var ajuste = await _db.AjustesInventario.Include(a => a.Producto).FirstOrDefaultAsync(a => a.AjusteInventarioId == id);
        if (ajuste == null)
        {
            TempData["Error"] = "El ajuste ya no existe.";
            return RedirectToAction(nameof(Index));
        }

        var producto = ajuste.Producto!;
        if (producto.Stock - ajuste.Efecto < 0)
        {
            TempData["Error"] = $"No se puede eliminar: \"{producto.Nombre}\" quedaría con stock negativo.";
            return RedirectToAction(nameof(Index));
        }

        producto.Stock -= ajuste.Efecto;   // se deshace el ajuste
        _db.AjustesInventario.Remove(ajuste);
        await _db.SaveChangesAsync();
        await transaccion.CommitAsync();

        TempData["Exito"] = $"Ajuste eliminado: {producto.Nombre} queda con {producto.Stock} unidades.";
        return RedirectToAction(nameof(Index));
    }

    // ---------- Apoyo ----------
    private async Task CargarListas(AjusteInventario? a)
    {
        ViewData["Productos"] = new SelectList(
            await _db.Productos.AsNoTracking().OrderBy(p => p.Nombre)
                .Select(p => new { p.ProductoId, Texto = p.Nombre + " (stock " + p.Stock + ")" }).ToListAsync(),
            "ProductoId", "Texto", a?.ProductoId);

        // Solo empleados activos (pero si se edita un ajuste viejo, se incluye a su responsable)
        var responsableActual = a?.EmpleadoId ?? 0;
        ViewData["Empleados"] = new SelectList(
            await _db.Empleados.AsNoTracking().Where(e => e.Activo || e.EmpleadoId == responsableActual)
                .OrderBy(e => e.Nombre).ToListAsync(),
            nameof(Empleado.EmpleadoId), nameof(Empleado.Nombre), a?.EmpleadoId);
    }

    private async Task Validar(AjusteInventario a)
    {
        a.Observacion = string.IsNullOrWhiteSpace(a.Observacion) ? null : a.Observacion.Trim();
        a.Motivo = (a.Motivo ?? string.Empty).Trim();
        a.Tipo = (a.Tipo ?? string.Empty).Trim();

        ModelState.Clear();
        TryValidateModel(a);

        if (!AjusteInventario.Tipos.Contains(a.Tipo))
            ModelState.AddModelError(nameof(AjusteInventario.Tipo), "Seleccione entrada o salida.");
        if (a.Motivo.Length > 0 && !AjusteInventario.Motivos.Contains(a.Motivo))
            ModelState.AddModelError(nameof(AjusteInventario.Motivo), "Seleccione un motivo de la lista.");
        if (a.Motivo == "Otro" && a.Observacion == null)
            ModelState.AddModelError(nameof(AjusteInventario.Observacion), "Si el motivo es \"Otro\", explique en la observación.");
        if (a.ProductoId > 0 && !await _db.Productos.AnyAsync(p => p.ProductoId == a.ProductoId))
            ModelState.AddModelError(nameof(AjusteInventario.ProductoId), "El producto no existe.");
        if (a.EmpleadoId > 0 && !await _db.Empleados.AnyAsync(e => e.EmpleadoId == a.EmpleadoId))
            ModelState.AddModelError(nameof(AjusteInventario.EmpleadoId), "El responsable no existe.");
    }
}
