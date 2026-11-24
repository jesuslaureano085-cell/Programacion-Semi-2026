using System.ComponentModel.DataAnnotations;

namespace webapp.Models
{
    public class Docente
    {
        [Key]
        public int idDocente { get; set; }

        public string codigo { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string escalafon { get; set; } = string.Empty;
    }
}
