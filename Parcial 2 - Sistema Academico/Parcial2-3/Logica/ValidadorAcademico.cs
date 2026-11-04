using System.Collections.Generic;
using System.Linq;

namespace Parcial2_3
{
    public static class ValidadorAcademico
    {
        public const string CODIGO = "Codigo";
        public const string NOMBRE = "Nombre";
        public const string TELEFONO = "Telefono";
        public const string UV = "Uv";

        public static Dictionary<string, string> ValidarAlumno(Alumno a)
        {
            var errores = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(a.Codigo))
                errores[CODIGO] = "Escriba el código del alumno.";

            if (string.IsNullOrWhiteSpace(a.Nombre) || a.Nombre.Trim().Length < 3)
                errores[NOMBRE] = "Escriba el nombre completo.";

            string tel = a.Telefono ?? "";
            if (tel.Length > 0 && !tel.All(c => char.IsDigit(c) || c == '-'))
                errores[TELEFONO] = "El teléfono solo admite números y guion.";

            return errores;
        }

        public static Dictionary<string, string> ValidarMateria(string codigo, string nombre, string uv, out Materia materia)
        {
            var errores = new Dictionary<string, string>();
            materia = null;

            int numCodigo, numUv;
            if (!int.TryParse((codigo ?? "").Trim(), out numCodigo) || numCodigo <= 0)
                errores[CODIGO] = "El código debe ser un número entero positivo.";

            if (string.IsNullOrWhiteSpace(nombre))
                errores[NOMBRE] = "Escriba el nombre de la materia.";

            if (!int.TryParse((uv ?? "").Trim(), out numUv) || numUv < 1 || numUv > 10)
                errores[UV] = "Las UV deben estar entre 1 y 10.";

            if (errores.Count == 0)
                materia = new Materia { Codigo = numCodigo, Nombre = nombre.Trim(), Uv = numUv };

            return errores;
        }

        public static bool NotaValida(decimal nota)
        {
            return nota >= 0 && nota <= 10;
        }
    }
}
