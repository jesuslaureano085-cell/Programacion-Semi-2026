using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace LA_PRUEBA
{
    /// <summary>
    /// Guarda y valida usuarios en un archivo de texto (usuarios.txt) junto al .exe.
    /// Cada línea tiene el formato: usuario|sal|hash
    /// La contraseña nunca se guarda en texto plano: se guarda su hash SHA-256 con una "sal" aleatoria.
    /// </summary>
    internal static class AlmacenUsuarios
    {
        private static readonly string RutaArchivo =
            Path.Combine(Application.StartupPath, "usuarios.txt");

        public static bool Existe(string usuario)
        {
            if (!File.Exists(RutaArchivo)) return false;

            foreach (string linea in File.ReadAllLines(RutaArchivo))
            {
                string[] partes = linea.Split('|');
                if (partes.Length == 3 &&
                    partes[0].Equals(usuario, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public static void Registrar(string usuario, string contrasena)
        {
            byte[] salBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salBytes);

            string sal = Convert.ToBase64String(salBytes);
            string hash = CalcularHash(contrasena, sal);

            File.AppendAllText(RutaArchivo, $"{usuario}|{sal}|{hash}{Environment.NewLine}");
        }

        public static bool Validar(string usuario, string contrasena)
        {
            if (!File.Exists(RutaArchivo)) return false;

            foreach (string linea in File.ReadAllLines(RutaArchivo))
            {
                string[] partes = linea.Split('|');
                if (partes.Length != 3) continue;

                if (partes[0].Equals(usuario, StringComparison.OrdinalIgnoreCase))
                    return CalcularHash(contrasena, partes[1]) == partes[2];
            }
            return false;
        }

        private static string CalcularHash(string contrasena, string sal)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(sal + contrasena));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
