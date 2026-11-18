using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MateriasController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materias>>> GetMaterias()
        {
            return await _context.Materias.ToListAsync();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Materias>>> BuscarMateria([FromQuery] MateriaBusquedaParametros parametros)
        {
            var consulta = _context.Materias.AsQueryable();

            if (!string.IsNullOrEmpty(parametros.buscar))
            {
                consulta = consulta.Where(materia => materia.nombre.Contains(parametros.buscar)
                                                  || materia.codigo.ToString().Contains(parametros.buscar));
            }

            return await consulta.ToListAsync();
        }

        [HttpGet("{idmateria}")]
        public async Task<ActionResult<Materias>> GetMateria(int idmateria)
        {
            var materia = await _context.Materias.FindAsync(idmateria);

            if (materia == null)
            {
                return NotFound();
            }

            return materia;
        }

        [HttpPut("{idmateria}")]
        public async Task<IActionResult> PutMateria(int idmateria, Materias materia)
        {
            if (idmateria != materia.IdMateria)
            {
                return BadRequest();
            }

            _context.Entry(materia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaExists(idmateria))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Materias>> PostMateria(Materias materia)
        {
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMateria), new { idmateria = materia.IdMateria }, materia);
        }

        [HttpDelete("{idmateria}")]
        public async Task<IActionResult> DeleteMateria(int idmateria)
        {
            var materia = await _context.Materias.FindAsync(idmateria);
            if (materia == null)
            {
                return NotFound();
            }

            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MateriaExists(int idmateria)
        {
            return _context.Materias.Any(e => e.IdMateria == idmateria);
        }
    }
}
