using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // Libreria para usar comandos de BD
using System.Data.SqlClient; // Libreria para trabajar con SQL Server

namespace miPrimeaAplicacion
{
    internal class Conexion
    {
        // Definir los miembros de la clase, atributos y metodos.
        public SqlConnection objConexion = new SqlConnection();
        public SqlCommand objComando = new SqlCommand();
        public SqlDataAdapter objAdaptador = new SqlDataAdapter();
        DataSet objDs = new DataSet();

        public Conexion()
        { // Constructor
            String cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_academica.mdf;Integrated Security=True";
            objConexion.ConnectionString = cadenaConexion;
            objConexion.Open(); // Abrir la conexion a la BD
        }

        public DataSet obtenerDatos()
        {
            objDs.Clear(); // Limpiar el DataSet
            objComando.Connection = objConexion;
            objAdaptador.SelectCommand = objComando;

            // 1. Carga de Alumnos (Intacto)
            objComando.CommandText = "SELECT * FROM alumnos";
            objAdaptador.Fill(objDs, "alumnos");

            // 2. Carga de Materias (Intacto)
            objComando.CommandText = "SELECT * FROM materias";
            objAdaptador.Fill(objDs, "materias");

            // 3. NUEVO SEGURO: Carga de Periodos para los menús desplegables
            objComando.CommandText = "SELECT * FROM periodos";
            objAdaptador.Fill(objDs, "periodos");

            return objDs;
        }

        // ---> FUNCIÓN DE ALUMNOS (100% Intacta para evitar fallos) <---
        public string administrarDatosAlumnos(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {
                sql = "INSERT INTO alumnos(codigo,nombre,direccion,telefono) VALUES ('" + datos[1] + "', '" + datos[2] + "', '" + datos[3] + "', '" + datos[4] + "')";
            }
            else if (accion == "modificar")
            {
                sql = "UPDATE alumnos SET codigo='" + datos[1] + "', nombre='" + datos[2] + "', direccion='" + datos[3] + "', telefono='" + datos[4] + "' WHERE idAlumno='" + datos[0] + "'";
            }
            else if (accion == "eliminar")
            {
                sql = "DELETE FROM alumnos WHERE idAlumno='" + datos[0] + "'";
            }
            return ejecutarSQL(sql);
        }

        // ---> FUNCIÓN DE MATERIAS (100% Intacta para evitar fallos) <---
        public string administrarDatosMaterias(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {
                sql = "INSERT INTO materias(codigo,nombre,uv) VALUES ('" + datos[1] + "', '" + datos[2] + "', '" + datos[3] + "')";
            }
            else if (accion == "modificar")
            {
                sql = "UPDATE materias SET codigo='" + datos[1] + "', nombre='" + datos[2] + "', uv='" + datos[3] + "' WHERE idMateria='" + datos[0] + "'";
            }
            else if (accion == "eliminar")
            {
                sql = "DELETE FROM materias WHERE idMateria='" + datos[0] + "'";
            }
            return ejecutarSQL(sql);
        }

        // ---> NUEVO SEGURO: Función opcional para administrar Periodos <---
        public string administrarDatosPeriodos(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {
                sql = "INSERT INTO periodos(periodo,fecha) VALUES ('" + datos[1] + "', '" + datos[2] + "')";
            }
            else if (accion == "modificar")
            {
                sql = "UPDATE periodos SET periodo='" + datos[1] + "', fecha='" + datos[2] + "' WHERE idPeriodo='" + datos[0] + "'";
            }
            else if (accion == "eliminar")
            {
                sql = "DELETE FROM periodos WHERE idPeriodo='" + datos[0] + "'";
            }
            return ejecutarSQL(sql);
        }

        // ---> FUNCIÓN QUE EJECUTA LAS ACCIONES EN LA BASE DE DATOS <---
        public String ejecutarSQL(String sql)
        {
            try
            {
                objComando.Connection = objConexion;
                objComando.CommandText = sql;
                return objComando.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}