using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    [Table("docentes")]
    public class Docentes
    {
        [Key]
        public int IdDocentes { get; set; }

        [Column(TypeName = "char(10)")]
        public string? codigo { get; set; }

        [Column(TypeName = "char(10)")]
        public string? numero { get; set; }

        [Column(TypeName = "char(10)")]
        public string? nombre { get; set; }

        [Column(TypeName = "char(10)")]
        public string? direccion { get; set; }

        [Column(TypeName = "char(10)")]
        public string? telefono { get; set; }

        [Column(TypeName = "char(10)")]
        public string? email { get; set; }

        [Column(TypeName = "char(25)")]
        public string? escalafon { get; set; }
    }
}
