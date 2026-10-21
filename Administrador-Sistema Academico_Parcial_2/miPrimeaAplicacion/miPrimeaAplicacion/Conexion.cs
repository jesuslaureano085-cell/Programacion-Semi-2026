using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; 
using System.Data.SqlClient; 

namespace miPrimeaAplicacion
{
    internal class Conexion
    {
        
        public SqlConnection objConexion = new SqlConnection();
        public SqlCommand objComando = new SqlCommand();
        public SqlDataAdapter objAdaptador = new SqlDataAdapter();
        DataSet objDs = new DataSet();

        public Conexion()
        { 
            String cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_academica.mdf;Integrated Security=True";
            objConexion.ConnectionString = cadenaConexion;
            objConexion.Open(); 
            crearTablaUsuarios(); 
        }

      
        private void crearTablaUsuarios()
        {
            String sql =
                "IF OBJECT_ID('dbo.usuarios_USSS077426', 'U') IS NULL " +
                "BEGIN " +
                "CREATE TABLE usuarios_USSS077426 (" +
                "idUsuario INT IDENTITY(1,1) NOT NULL PRIMARY KEY, " +
                "usuario VARCHAR(35) NOT NULL UNIQUE, " +
                "clave VARCHAR(35) NOT NULL, " +
                "nombre VARCHAR(65) NOT NULL, " +
                "direccion VARCHAR(85) NULL, " +
                "telefono VARCHAR(9) NULL); " +
                "INSERT INTO usuarios_USSS077426 (usuario, clave, nombre, direccion, telefono) VALUES " +
                "('admin01','admin123','Administrador del sistema','Usulutan, El Salvador','2662-0000'), " +
                "('jlaureano','clave2026','Jesus Laureano','Usulutan, Barrio El Calvario','7777-1234'), " +
                "('usuario03','abc12345','Maria Lopez','San Miguel, El Salvador','7000-5678'); " +
                "END";
            using (SqlCommand cmd = new SqlCommand(sql, objConexion))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public DataSet obtenerDatos()
        {
            objDs.Clear(); 
            objComando.Connection = objConexion;
            objAdaptador.SelectCommand = objComando;


            objComando.CommandText = "SELECT * FROM alumnos";
            objAdaptador.Fill(objDs, "alumnos");


            objComando.CommandText = "SELECT * FROM materias";
            objAdaptador.Fill(objDs, "materias");


            objComando.CommandText = "SELECT * FROM periodos";
            objAdaptador.Fill(objDs, "periodos");


            crearDs();

            return objDs;
        }

  
        public void crearDs()
        {
            objComando.Connection = objConexion;
            objAdaptador.SelectCommand = objComando;

            objComando.CommandText = "SELECT * FROM usuarios_USSS077426 ORDER BY idUsuario";
            objAdaptador.Fill(objDs, "usuarios_USSS077426");
        }


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

        
        public string mantenimiento_usuarios_USSS077426(String[] datos, String accion)
        {
            for (int i = 0; i < datos.Length; i++)
            {
                datos[i] = (datos[i] ?? "").Replace("'", "''"); 
            }

            String sql = "";
            if (accion == "nuevo")
            {
                sql = "INSERT INTO usuarios_USSS077426(usuario,clave,nombre,direccion,telefono) VALUES ('" + datos[1] + "', '" + datos[2] + "', '" + datos[3] + "', '" + datos[4] + "', '" + datos[5] + "')";
            }
            else if (accion == "modificar")
            {
                sql = "UPDATE usuarios_USSS077426 SET usuario='" + datos[1] + "', clave='" + datos[2] + "', nombre='" + datos[3] + "', direccion='" + datos[4] + "', telefono='" + datos[5] + "' WHERE idUsuario='" + datos[0] + "'";
            }
            else if (accion == "eliminar")
            {
                sql = "DELETE FROM usuarios_USSS077426 WHERE idUsuario='" + datos[0] + "'";
            }
            return procesar(sql);
        }

        public bool validarLogin(String usuario, String clave)
        {
            using (SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(*) FROM usuarios_USSS077426 WHERE usuario=@usuario AND clave=@clave", objConexion))
            {
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@clave", clave);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

  
        public String procesar(String sql)
        {
            return ejecutarSQL(sql);
        }


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