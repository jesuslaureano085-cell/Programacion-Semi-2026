using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace miPrimeaAplicacion
{
    public partial class frm_usuario : Form
    {
        Conexion objConexion = new Conexion();
        DataSet ds = new DataSet();
        DataTable miTabla = new DataTable();
        int posicion = 0;
        String accion = "nuevo";

        public frm_usuario()
        {
            InitializeComponent();
        }

        private void frm_usuario_Load(object sender, EventArgs e)
        {
            actualizarDs();
            mostrarDatos();
            estadoEdicion(false);
        }

        private void actualizarDs()
        {
            ds = objConexion.obtenerDatos();
            miTabla = ds.Tables["usuarios_USSS077426"];
            miTabla.PrimaryKey = new DataColumn[] { miTabla.Columns["idUsuario"] };
            grdUsuarios.DataSource = miTabla;
            if (grdUsuarios.Columns.Contains("clave"))
            {
                grdUsuarios.Columns["clave"].Visible = false; // no mostrar contrasenas
            }
        }

        private void mostrarDatos()
        {
            if (miTabla.Rows.Count > 0)
            {
                if (posicion >= miTabla.Rows.Count) posicion = miTabla.Rows.Count - 1;
                if (posicion < 0) posicion = 0;

                DataRow fila = miTabla.Rows[posicion];
                txtIdUsuario.Text = fila["idUsuario"].ToString();
                txtUsuario.Text = fila["usuario"].ToString();
                txtClave.Text = fila["clave"].ToString();
                txtConfirmar.Text = fila["clave"].ToString();
                txtNombre.Text = fila["nombre"].ToString();
                txtDireccion.Text = fila["direccion"].ToString();
                txtTelefono.Text = fila["telefono"].ToString();
                lblRegistro.Text = "Registro " + (posicion + 1) + " de " + miTabla.Rows.Count;
            }
            else
            {
                limpiarCajas();
                lblRegistro.Text = "Registro 0 de 0";
            }
        }

        private void limpiarCajas()
        {
            txtIdUsuario.Text = "";
            txtUsuario.Text = "";
            txtClave.Text = "";
            txtConfirmar.Text = "";
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
        }

        private void estadoEdicion(bool editando)
        {
            txtUsuario.ReadOnly = !editando;
            txtClave.ReadOnly = !editando;
            txtConfirmar.ReadOnly = !editando;
            txtNombre.ReadOnly = !editando;
            txtDireccion.ReadOnly = !editando;
            txtTelefono.ReadOnly = !editando;

            btnPrimero.Enabled = !editando;
            btnAnterior.Enabled = !editando;
            btnSiguiente.Enabled = !editando;
            btnUltimo.Enabled = !editando;
            btnEliminar.Enabled = !editando;
            grdUsuarios.Enabled = !editando;
        }

        private void restaurarBotones()
        {
            btnNuevo.Text = "Nuevo";
            btnModificar.Text = "Modificar";
            estadoEdicion(false);
        }

        // ===== Validaciones (Pregunta 2 c y d) =====
        private bool validarDatos()
        {
            String usuario = txtUsuario.Text.Trim();
            if (usuario.Length < 6 || usuario.Length > 16)
            {
                MessageBox.Show("El usuario debe tener entre 6 y 16 caracteres.", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return false;
            }

            foreach (DataRow fila in miTabla.Rows)
            {
                if (fila["usuario"].ToString().Equals(usuario, StringComparison.OrdinalIgnoreCase) &&
                    fila["idUsuario"].ToString() != txtIdUsuario.Text)
                {
                    MessageBox.Show("Ese usuario ya existe, escriba otro.", "Validacion",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    return false;
                }
            }

            if (!Regex.IsMatch(txtClave.Text, @"^(?=.*[A-Za-z])(?=.*\d).+$"))
            {
                MessageBox.Show("La clave debe contener letras y numeros.", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClave.Focus();
                return false;
            }

            if (txtClave.Text != txtConfirmar.Text)
            {
                MessageBox.Show("La clave y la confirmacion no coinciden. No se guardara.", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmar.Focus();
                return false;
            }

            if (txtNombre.Text.Trim() == "")
            {
                MessageBox.Show("El nombre es obligatorio.", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (txtTelefono.Text.Trim() != "" && !Regex.IsMatch(txtTelefono.Text.Trim(), @"^\d{4}-\d{4}$"))
            {
                MessageBox.Show("El telefono debe tener el formato 0000-0000.", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return false;
            }
            return true;
        }

        // ===== Botones de navegacion =====
        private void btnPrimero_Click(object sender, EventArgs e)
        {
            posicion = 0;
            mostrarDatos();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion--;
                mostrarDatos();
            }
            else
            {
                MessageBox.Show("Ya esta en el primer registro.", "Navegacion");
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (posicion < miTabla.Rows.Count - 1)
            {
                posicion++;
                mostrarDatos();
            }
            else
            {
                MessageBox.Show("Ya esta en el ultimo registro.", "Navegacion");
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            posicion = miTabla.Rows.Count - 1;
            mostrarDatos();
        }

        // ===== Botones de edicion =====
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (btnNuevo.Text == "Nuevo")
            {
                accion = "nuevo";
                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                limpiarCajas();
                estadoEdicion(true);
                txtUsuario.Focus();
            }
            else // Guardar
            {
                if (!validarDatos()) return;

                String[] datos = {
                    txtIdUsuario.Text,
                    txtUsuario.Text.Trim(),
                    txtClave.Text,
                    txtNombre.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    txtTelefono.Text.Trim()
                };

                String respuesta = objConexion.mantenimiento_usuarios_USSS077426(datos, accion);
                if (respuesta != "1")
                {
                    MessageBox.Show("Error al guardar: " + respuesta, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                actualizarDs();
                if (accion == "nuevo") posicion = miTabla.Rows.Count - 1;
                mostrarDatos();
                restaurarBotones();
                MessageBox.Show("Usuario guardado correctamente.", "Usuarios",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (btnModificar.Text == "Modificar")
            {
                if (miTabla.Rows.Count == 0)
                {
                    MessageBox.Show("No hay registros para modificar.", "Usuarios");
                    return;
                }
                accion = "modificar";
                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                estadoEdicion(true);
                txtUsuario.Focus();
            }
            else // Cancelar
            {
                restaurarBotones();
                mostrarDatos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (miTabla.Rows.Count == 0)
            {
                MessageBox.Show("No hay registros para eliminar.", "Usuarios");
                return;
            }

            if (MessageBox.Show("Esta seguro de eliminar al usuario " + txtUsuario.Text + "?", "Eliminar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String[] datos = { txtIdUsuario.Text, "", "", "", "", "" };
                String respuesta = objConexion.mantenimiento_usuarios_USSS077426(datos, "eliminar");
                if (respuesta != "1")
                {
                    MessageBox.Show("Error al eliminar: " + respuesta, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                actualizarDs();
                mostrarDatos();
            }
        }

        private void grdUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            object id = grdUsuarios.Rows[e.RowIndex].Cells["idUsuario"].Value;
            DataRow fila = miTabla.Rows.Find(id);
            if (fila != null)
            {
                posicion = miTabla.Rows.IndexOf(fila);
                mostrarDatos();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
