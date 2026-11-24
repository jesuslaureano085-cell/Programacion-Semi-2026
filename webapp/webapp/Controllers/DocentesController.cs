using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocentesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DocentesController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Docente>>> GetDocentes()
        {
            return await _context.Docentes.ToListAsync();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Docente>>> BuscarDocente([FromQuery] DocenteBusquedaParametros parametros)
        {
            var consulta = _context.Docentes.AsQueryable();

            if (!string.IsNullOrEmpty(parametros.buscar))
            {
                consulta = consulta.Where(docente => docente.nombre.Contains(parametros.buscar)
                                                  || docente.codigo.Contains(parametros.buscar));
            }

            return await consulta.ToListAsync();
        }

        [HttpGet("{iddocente}")]
        public async Task<ActionResult<Docente>> GetDocente(int iddocente)
        {
            var docente = await _context.Docentes.FindAsync(iddocente);

            if (docente == null)
            {
                return NotFound();
            }

            return docente;
        }

        [HttpPut("{iddocente}")]
        public async Task<IActionResult> PutDocente(int iddocente, Docente docente)
        {
            if (iddocente != docente.idDocente)
            {
                return BadRequest();
            }

            _context.Entry(docente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocenteExists(iddocente))
                {
                    return NotFound();
                }
                throw;
            }

            return CreatedAtAction(nameof(GetDocente), new { iddocente = docente.idDocente }, docente);
        }

        [HttpPost]
        public async Task<ActionResult<Docente>> PostDocente(Docente docente)
        {
            _context.Docentes.Add(docente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocente), new { iddocente = docente.idDocente }, docente);
        }

        [HttpDelete("{iddocente}")]
        public async Task<IActionResult> DeleteDocente(int iddocente)
        {
            var docente = await _context.Docentes.FindAsync(iddocente);
            if (docente == null)
            {
                return NotFound();
            }

            _context.Docentes.Remove(docente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocenteExists(int iddocente)
        {
            return _context.Docentes.Any(e => e.idDocente == iddocente);
        }
    }
}
