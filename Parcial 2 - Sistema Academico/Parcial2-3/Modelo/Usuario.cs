namespace Parcial2_3
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Cuenta { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public bool EsNuevo
        {
            get { return IdUsuario <= 0; }
        }

        public override string ToString()
        {
            return Cuenta + "   ·   " + Nombre;
        }
    }
}
