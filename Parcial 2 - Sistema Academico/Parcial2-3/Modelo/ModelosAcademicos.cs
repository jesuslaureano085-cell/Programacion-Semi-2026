using System;

namespace Parcial2_3
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public override string ToString()
        {
            return Codigo + "   ·   " + Nombre;
        }
    }

    public class Materia
    {
        public int IdMateria { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int Uv { get; set; }

        public override string ToString()
        {
            return Codigo + "   ·   " + Nombre;
        }
    }

    public class Periodo
    {
        private string nombre = "";
        private DateTime fecha = DateTime.Today;

        public int IdPeriodo { get; set; }
        public bool Modificado { get; set; }

        public string Nombre
        {
            get { return nombre; }
            set { if (value != nombre) { nombre = value; Modificado = true; } }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { if (value != fecha) { fecha = value; Modificado = true; } }
        }

        public override string ToString()
        {
            return Nombre;
        }
    }

    public class DetalleNota
    {
        public const decimal NOTA_MINIMA = 6.0m;

        private decimal lab1, lab2, parcial;

        public int IdDetalle { get; set; }
        public string Alumno { get; set; }
        public bool Modificado { get; set; }

        public decimal Lab1 { get { return lab1; } set { if (value != lab1) { lab1 = value; Modificado = true; } } }
        public decimal Lab2 { get { return lab2; } set { if (value != lab2) { lab2 = value; Modificado = true; } } }
        public decimal Parcial { get { return parcial; } set { if (value != parcial) { parcial = value; Modificado = true; } } }

        public decimal NF
        {
            get { return Math.Round(lab1 * 0.30m + lab2 * 0.30m + parcial * 0.40m, 2); }
        }

        public string Estado
        {
            get { return NF >= NOTA_MINIMA ? "Aprobado" : "Reprobado"; }
        }

        public static DetalleNota Crear(int id, string alumno, decimal l1, decimal l2, decimal p)
        {
            return new DetalleNota { IdDetalle = id, Alumno = alumno, lab1 = l1, lab2 = l2, parcial = p };
        }
    }
}
