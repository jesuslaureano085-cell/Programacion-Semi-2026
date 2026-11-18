using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AlumnosController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            return await _context.Alumnos.ToListAsync();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Alumno>>> BuscarAlumno([FromQuery] AlumnoBusquedaParametros parametros)
        {
            var consulta = _context.Alumnos.AsQueryable();

            if (!string.IsNullOrEmpty(parametros.buscar))
            {
                consulta = consulta.Where(alumno => alumno.nombre.Contains(parametros.buscar)
                                                 || alumno.codigo.Contains(parametros.buscar));
            }

            return await consulta.ToListAsync();
        }

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

        [HttpPost]
        public async Task<ActionResult<Alumno>> PostAlumno(Alumno alumno)
        {
            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlumno), new { idalumno = alumno.idAlumno }, alumno);
        }

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
