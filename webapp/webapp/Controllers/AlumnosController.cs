using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Controllers
{
    [Route("api/[controller]")]   // la dirección será: /api/Alumnos
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly MyDbContext _context;

        // El "puente" a la base de datos llega aquí automáticamente (lo registramos en Program.cs)
        public AlumnosController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Alumnos  → devuelve TODOS los alumnos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            return await _context.Alumnos.ToListAsync();
        }

        // GET: api/Alumnos/5  → devuelve UN alumno
        [HttpGet("{idalumno}")]
        public async Task<ActionResult<Alumno>> GetAlumno(int idalumno)
        {
            var alumno = await _context.Alumnos.FindAsync(idalumno);

            if (alumno == null)
            {
                return NotFound();
            }

            return alumno;
        }

        // PUT: api/Alumnos/5  → MODIFICA un alumno
        [HttpPut("{idalumno}")]
        public async Task<IActionResult> PutAlumno(int idalumno, Alumno alumno)
        {
            if (idalumno != alumno.idAlumno)
            {
                return BadRequest();
            }

            _context.Entry(alumno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoExists(idalumno))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Alumnos  → CREA un alumno nuevo
        [HttpPost]
        public async Task<ActionResult<Alumno>> PostAlumno(Alumno alumno)
        {
            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlumno), new { idalumno = alumno.idAlumno }, alumno);
        }

        // DELETE: api/Alumnos/5  → ELIMINA un alumno
        [HttpDelete("{idalumno}")]
        public async Task<IActionResult> DeleteAlumno(int idalumno)
        {
            var alumno = await _context.Alumnos.FindAsync(idalumno);
            if (alumno == null)
            {
                return NotFound();
            }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlumnoExists(int idalumno)
        {
            return _context.Alumnos.Any(e => e.idAlumno == idalumno);
        }
    }
}
