using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    [Table("materia")]
    public class Materias
    {
        [Key]
        public int IdMateria { get; set; }

        public int codigo { get; set; }

        [Column(TypeName = "char(10)")]
        public string nombre { get; set; } = string.Empty;

        public byte uv { get; set; }
    }
}
