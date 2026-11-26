using Microsoft.AspNetCore.Mvc;
using MonitorVentas.Data;

namespace MonitorVentas.Controllers;

/// <summary>
/// Moni, el ayudante del escritorio: recibe una pregunta y devuelve la respuesta en JSON.
/// La lógica (base de conocimiento y datos en vivo) está en Data/AyudanteMoni.cs.
/// </summary>
public class MoniController : Controller
{
    private readonly AppDbContext _db;
    public MoniController(AppDbContext db) => _db = db;

    // GET: /Moni/Preguntar?texto=¿cuánto se vendió hoy?
    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Preguntar(string? texto)
    {
        if (texto?.Length > 200) texto = texto[..200];
        var r = await AyudanteMoni.ResponderAsync(_db, texto);
        return Json(new
        {
            texto = r.Texto,
            detalle = r.Detalle,
            guia = r.Guia,
            pideGuia = r.PideGuia,
            modulo = r.Modulo,
            sugerencias = r.Sugerencias ?? Array.Empty<string>()
        });
    }
}
