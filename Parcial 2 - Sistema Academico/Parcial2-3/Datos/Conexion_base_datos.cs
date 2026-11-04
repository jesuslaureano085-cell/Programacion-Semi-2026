using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Parcial2_3
{
    public enum Operacion
    {
        Agregar,
        Modificar,
        Eliminar
    }

    public class Conexion_base_datos
    {
        private readonly string cadenaConexion;

        public Conexion_base_datos()
    : this(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database3.mdf;Integrated Security=True")
        {
        }

        public Conexion_base_datos(string cadena)
        {
            cadenaConexion = cadena;
        }

        public DataSet crearDs()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                llenar(ds, "usuarios", "SELECT idUsuario, usuario, clave, nombre, direccion, telefono FROM usuarios_USSS081126 ORDER BY nombre", cn);
                llenar(ds, "alumnos", "SELECT idAlumno, codigo, nombre, direccion, telefono FROM alumnos ORDER BY nombre", cn);
                llenar(ds, "materias", "SELECT idMateria, codigo, nombre, uv FROM materias ORDER BY codigo", cn);
                llenar(ds, "periodos", "SELECT idPeriodo, periodo, fecha FROM periodos ORDER BY fecha", cn);
            }
            return ds;
        }

        private static void llenar(DataSet ds, string tabla, string sql, SqlConnection cn)
        {
            using (SqlDataAdapter adaptador = new SqlDataAdapter(sql, cn))
            {
                adaptador.Fill(ds, tabla);
            }
        }

        public int procesar(SqlCommand cmd)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            using (cmd)
            {
                cmd.Connection = cn;
                cn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        private int contar(string sql, params SqlParameter[] parametros)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddRange(parametros);
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private static SqlParameter entero(string nombre, int valor)
        {
            return new SqlParameter(nombre, SqlDbType.Int) { Value = valor };
        }

        private static SqlParameter texto(string nombre, int largo, string valor)
        {
            return new SqlParameter(nombre, SqlDbType.VarChar, largo)
            {
                Value = string.IsNullOrWhiteSpace(valor) ? (object)DBNull.Value : valor.Trim()
            };
        }

        public List<Usuario> listarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            foreach (DataRow fila in crearDs().Tables["usuarios"].Rows)
            {
                lista.Add(aUsuario(fila));
            }
            return lista;
        }

        public int mantenimiento_usuarios_USSS081126(Usuario u, Operacion operacion)
        {
            SqlCommand cmd = new SqlCommand();

            switch (operacion)
            {
                case Operacion.Agregar:
                    cmd.CommandText =
                        "INSERT INTO usuarios_USSS081126 (usuario, clave, nombre, direccion, telefono) " +
                        "VALUES (@usuario, @clave, @nombre, @direccion, @telefono)";
                    agregarCamposUsuario(cmd, u);
                    break;

                case Operacion.Modificar:
                    cmd.CommandText =
                        "UPDATE usuarios_USSS081126 SET usuario = @usuario, clave = @clave, nombre = @nombre, " +
                        "direccion = @direccion, telefono = @telefono WHERE idUsuario = @idUsuario";
                    agregarCamposUsuario(cmd, u);
                    cmd.Parameters.Add(entero("@idUsuario", u.IdUsuario));
                    break;

                case Operacion.Eliminar:
                    cmd.CommandText = "DELETE FROM usuarios_USSS081126 WHERE idUsuario = @idUsuario";
                    cmd.Parameters.Add(entero("@idUsuario", u.IdUsuario));
                    break;
            }

            return procesar(cmd);
        }

        public bool existeCuenta(string cuenta, int idExcluir)
        {
            return contar("SELECT COUNT(*) FROM usuarios_USSS081126 WHERE usuario = @usuario AND idUsuario <> @id",
                          texto("@usuario", 35, cuenta), entero("@id", idExcluir)) > 0;
        }

        public Usuario autenticar(string cuenta, string clave)
        {
            DataTable resultado = new DataTable();
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            using (SqlDataAdapter adaptador = new SqlDataAdapter(
                "SELECT idUsuario, usuario, clave, nombre, direccion, telefono FROM usuarios_USSS081126 " +
                "WHERE usuario = @usuario AND clave = @clave COLLATE Latin1_General_CS_AS", cn))
            {
                adaptador.SelectCommand.Parameters.Add("@usuario", SqlDbType.VarChar, 35).Value = cuenta;
                adaptador.SelectCommand.Parameters.Add("@clave", SqlDbType.VarChar, 35).Value = clave;
                adaptador.Fill(resultado);
            }

            return resultado.Rows.Count == 1 ? aUsuario(resultado.Rows[0]) : null;
        }

        private static void agregarCamposUsuario(SqlCommand cmd, Usuario u)
        {
            cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 35).Value = u.Cuenta;
            cmd.Parameters.Add("@clave", SqlDbType.VarChar, 35).Value = u.Clave;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 65).Value = u.Nombre;
            cmd.Parameters.Add(texto("@direccion", 85, u.Direccion));
            cmd.Parameters.Add(texto("@telefono", 9, u.Telefono));
        }

        private static Usuario aUsuario(DataRow fila)
        {
            return new Usuario
            {
                IdUsuario = Convert.ToInt32(fila["idUsuario"]),
                Cuenta = fila["usuario"].ToString(),
                Clave = fila["clave"].ToString(),
                Nombre = fila["nombre"].ToString(),
                Direccion = fila["direccion"] == DBNull.Value ? "" : fila["direccion"].ToString(),
                Telefono = fila["telefono"] == DBNull.Value ? "" : fila["telefono"].ToString()
            };
        }

        public List<Alumno> listarAlumnos()
        {
            List<Alumno> lista = new List<Alumno>();
            foreach (DataRow f in crearDs().Tables["alumnos"].Rows)
            {
                lista.Add(new Alumno
                {
                    IdAlumno = (int)f["idAlumno"],
                    Codigo = f["codigo"].ToString(),
                    Nombre = f["nombre"].ToString(),
                    Direccion = f["direccion"].ToString(),
                    Telefono = f["telefono"].ToString()
                });
            }
            return lista;
        }

        public int mantenimientoAlumnos(Alumno a, Operacion operacion)
        {
            SqlCommand cmd = new SqlCommand();
            switch (operacion)
            {
                case Operacion.Agregar:
                    cmd.CommandText = "INSERT INTO alumnos (codigo, nombre, direccion, telefono) VALUES (@codigo, @nombre, @direccion, @telefono)";
                    break;
                case Operacion.Modificar:
                    cmd.CommandText = "UPDATE alumnos SET codigo = @codigo, nombre = @nombre, direccion = @direccion, telefono = @telefono WHERE idAlumno = @id";
                    break;
                case Operacion.Eliminar:

                    cmd.CommandText =
                        "SET XACT_ABORT ON; BEGIN TRAN; " +
                        "DELETE d FROM dnotas d INNER JOIN notas n ON n.idNota = d.idNota WHERE n.idAlumno = @id; " +
                        "DELETE FROM notas WHERE idAlumno = @id; " +
                        "DELETE FROM alumnos WHERE idAlumno = @id; " +
                        "COMMIT;";
                    break;
            }
            cmd.Parameters.Add(entero("@id", a.IdAlumno));
            if (operacion != Operacion.Eliminar)
            {
                cmd.Parameters.Add(texto("@codigo", 10, a.Codigo));
                cmd.Parameters.Add(texto("@nombre", 65, a.Nombre));
                cmd.Parameters.Add(texto("@direccion", 85, a.Direccion));
                cmd.Parameters.Add(texto("@telefono", 9, a.Telefono));
            }
            return procesar(cmd);
        }

        public bool existeCodigoAlumno(string codigo, int idExcluir)
        {
            return contar("SELECT COUNT(*) FROM alumnos WHERE codigo = @codigo AND idAlumno <> @id",
                          texto("@codigo", 10, codigo), entero("@id", idExcluir)) > 0;
        }

        public int contarNotasAlumno(int idAlumno)
        {
            return contar("SELECT COUNT(*) FROM dnotas d INNER JOIN notas n ON n.idNota = d.idNota WHERE n.idAlumno = @id",
                          entero("@id", idAlumno));
        }

        public List<Materia> listarMaterias()
        {
            List<Materia> lista = new List<Materia>();
            foreach (DataRow f in crearDs().Tables["materias"].Rows)
            {
                lista.Add(new Materia
                {
                    IdMateria = (int)f["idMateria"],
                    Codigo = (int)f["codigo"],
                    Nombre = f["nombre"].ToString(),
                    Uv = (int)f["uv"]
                });
            }
            return lista;
        }

        public int mantenimientoMaterias(Materia m, Operacion operacion)
        {
            SqlCommand cmd = new SqlCommand();
            switch (operacion)
            {
                case Operacion.Agregar:
                    cmd.CommandText = "INSERT INTO materias (codigo, nombre, uv) VALUES (@codigo, @nombre, @uv)";
                    break;
                case Operacion.Modificar:
                    cmd.CommandText = "UPDATE materias SET codigo = @codigo, nombre = @nombre, uv = @uv WHERE idMateria = @id";
                    break;
                case Operacion.Eliminar:
                    cmd.CommandText =
                        "SET XACT_ABORT ON; BEGIN TRAN; " +
                        "DELETE FROM dnotas WHERE idMateria = @id; " +
                        "DELETE FROM materias WHERE idMateria = @id; " +
                        "COMMIT;";
                    break;
            }
            cmd.Parameters.Add(entero("@id", m.IdMateria));
            if (operacion != Operacion.Eliminar)
            {
                cmd.Parameters.Add(entero("@codigo", m.Codigo));
                cmd.Parameters.Add(texto("@nombre", 65, m.Nombre));
                cmd.Parameters.Add(entero("@uv", m.Uv));
            }
            return procesar(cmd);
        }

        public bool existeCodigoMateria(int codigo, int idExcluir)
        {
            return contar("SELECT COUNT(*) FROM materias WHERE codigo = @codigo AND idMateria <> @id",
                          entero("@codigo", codigo), entero("@id", idExcluir)) > 0;
        }

        public int contarNotasMateria(int idMateria)
        {
            return contar("SELECT COUNT(*) FROM dnotas WHERE idMateria = @id", entero("@id", idMateria));
        }

        public List<Periodo> listarPeriodos()
        {
            List<Periodo> lista = new List<Periodo>();
            foreach (DataRow f in crearDs().Tables["periodos"].Rows)
            {
                Periodo p = new Periodo { IdPeriodo = (int)f["idPeriodo"], Nombre = f["periodo"].ToString(), Fecha = (DateTime)f["fecha"] };
                p.Modificado = false;
                lista.Add(p);
            }
            return lista;
        }

        public bool periodoTieneNotas(int idPeriodo)
        {
            return contar("SELECT COUNT(*) FROM notas WHERE idPeriodo = @id", entero("@id", idPeriodo)) > 0;
        }

        public int guardarPeriodos(IEnumerable<Periodo> periodos, IEnumerable<int> eliminados)
        {
            int cambios = 0;
            foreach (int id in eliminados)
            {
                SqlCommand borrar = new SqlCommand("DELETE FROM periodos WHERE idPeriodo = @id");
                borrar.Parameters.Add(entero("@id", id));
                cambios += procesar(borrar);
            }
            foreach (Periodo p in periodos)
            {
                if (p.IdPeriodo > 0 && !p.Modificado) continue;

                SqlCommand cmd = new SqlCommand(p.IdPeriodo == 0
                    ? "INSERT INTO periodos (periodo, fecha) VALUES (@periodo, @fecha)"
                    : "UPDATE periodos SET periodo = @periodo, fecha = @fecha WHERE idPeriodo = @id");
                cmd.Parameters.Add(texto("@periodo", 30, p.Nombre));
                cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = p.Fecha.Date;
                cmd.Parameters.Add(entero("@id", p.IdPeriodo));
                cambios += procesar(cmd);
            }
            return cambios;
        }

        public List<DetalleNota> listarNotas(int idMateria, int idPeriodo)
        {
            List<DetalleNota> lista = new List<DetalleNota>();
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT d.idDetalle, a.nombre, d.lab1, d.lab2, d.parcial " +
                "FROM dnotas d INNER JOIN notas n ON n.idNota = d.idNota " +
                "INNER JOIN alumnos a ON a.idAlumno = n.idAlumno " +
                "WHERE d.idMateria = @materia AND n.idPeriodo = @periodo ORDER BY a.nombre", cn))
            {
                cmd.Parameters.Add(entero("@materia", idMateria));
                cmd.Parameters.Add(entero("@periodo", idPeriodo));
                cn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        lista.Add(DetalleNota.Crear(r.GetInt32(0), r.GetString(1), r.GetDecimal(2), r.GetDecimal(3), r.GetDecimal(4)));
                    }
                }
            }
            return lista;
        }

        public int guardarNotas(IEnumerable<DetalleNota> notas)
        {
            int guardadas = 0;
            foreach (DetalleNota d in notas)
            {
                if (!d.Modificado) continue;

                SqlCommand cmd = new SqlCommand(
                    "UPDATE dnotas SET lab1 = @lab1, lab2 = @lab2, parcial = @parcial WHERE idDetalle = @id");
                cmd.Parameters.Add("@lab1", SqlDbType.Decimal).Value = d.Lab1;
                cmd.Parameters.Add("@lab2", SqlDbType.Decimal).Value = d.Lab2;
                cmd.Parameters.Add("@parcial", SqlDbType.Decimal).Value = d.Parcial;
                cmd.Parameters.Add(entero("@id", d.IdDetalle));
                guardadas += procesar(cmd);
                d.Modificado = false;
            }
            return guardadas;
        }
    }
}
