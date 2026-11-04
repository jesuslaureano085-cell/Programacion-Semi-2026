using System.ComponentModel.DataAnnotations;

namespace webapp.models
{
    public class Alumnocs
    {
        [Key]
        public int idAlumno { get; set; }

        public string codigo { get; set; } = string.Empty;

        public string nombre { get; set; } = string.Empty;

        public string direccion { get; set; } = string.Empty;

        public string telefono { get; set; } = string.Empty;

        public string casil { get; set; } = string.Empty;
    }
}
