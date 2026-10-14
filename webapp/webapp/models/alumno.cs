using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Alumnocs
    {
        [Key]

        public int Id { get; set; }


        public int codigo { get; set; }

        public int nombre { get; set; }

        public int direccion { get; set; }

        public int telefono { get; set; }

        public int casil { get; set; }


    }
}