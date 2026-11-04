using System.Collections.Generic;
using System.Linq;

namespace Parcial2_3
{
    public static class ValidadorUsuario
    {
        public const int MIN_USUARIO = 6;
        public const int MAX_USUARIO = 16;

        public const string CAMPO_CUENTA = "Cuenta";
        public const string CAMPO_CLAVE = "Clave";
        public const string CAMPO_CONFIRMACION = "Confirmacion";
        public const string CAMPO_NOMBRE = "Nombre";
        public const string CAMPO_TELEFONO = "Telefono";

        public static Dictionary<string, string> Validar(Usuario u, string confirmacion)
        {
            Dictionary<string, string> errores = new Dictionary<string, string>();

            string cuenta = (u.Cuenta ?? "").Trim();
            if (cuenta.Length < MIN_USUARIO || cuenta.Length > MAX_USUARIO)
            {
                errores[CAMPO_CUENTA] = "El usuario debe tener entre " + MIN_USUARIO + " y " + MAX_USUARIO +
                                        " caracteres (ahora tiene " + cuenta.Length + ").";
            }

            string clave = u.Clave ?? "";
            if (clave.Length == 0)
            {
                errores[CAMPO_CLAVE] = "Escriba una clave.";
            }
            else if (!(clave.Any(char.IsLetter) && clave.Any(char.IsDigit)))
            {
                errores[CAMPO_CLAVE] = "La clave debe combinar letras y números.";
            }

            if (clave != (confirmacion ?? ""))
            {
                errores[CAMPO_CONFIRMACION] = "La confirmación no coincide con la clave.";
            }

            if (string.IsNullOrWhiteSpace(u.Nombre))
            {
                errores[CAMPO_NOMBRE] = "El nombre es obligatorio.";
            }

            string tel = u.Telefono ?? "";
            if (tel.Length > 0 && !tel.All(c => char.IsDigit(c) || c == '-'))
            {
                errores[CAMPO_TELEFONO] = "El teléfono solo admite números y guion.";
            }

            return errores;
        }
    }
}
