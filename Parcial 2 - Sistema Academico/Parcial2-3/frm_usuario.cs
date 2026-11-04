using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Parcial2_3
{
    public partial class frm_usuario : Form
    {
        private readonly Conexion_base_datos datos = new Conexion_base_datos();
        private readonly Usuario usuarioSesion;
        private List<Usuario> usuarios = new List<Usuario>();
        private Dictionary<string, Control> controlesPorCampo;

        private bool editando;
        private bool esNuevo;

        private bool soloRegistro;
        public string CuentaRegistrada { get; private set; }

        public static frm_usuario ParaRegistro()
        {
            frm_usuario f = new frm_usuario(null);
            f.soloRegistro = true;
            return f;
        }

        public frm_usuario() : this(null)
        {
        }

        public frm_usuario(Usuario sesion)
        {
            InitializeComponent();
            usuarioSesion = sesion;

            controlesPorCampo = new Dictionary<string, Control>
            {
                { ValidadorUsuario.CAMPO_CUENTA, txtCuenta },
                { ValidadorUsuario.CAMPO_CLAVE, txtClave },
                { ValidadorUsuario.CAMPO_CONFIRMACION, txtConfirmar },
                { ValidadorUsuario.CAMPO_NOMBRE, txtNombre },
                { ValidadorUsuario.CAMPO_TELEFONO, txtTelefono }
            };
        }

        private void frm_usuario_Load(object sender, EventArgs e)
        {
            if (soloRegistro)
            {
                Text = "Registrarse";
                lblTitulo.Text = "Crear una cuenta";
                StartPosition = FormStartPosition.CenterParent;
            }
            tslSesion.Text = usuarioSesion == null ? "" : "Conectado como " + usuarioSesion.Nombre + "  ";
            establecerEdicion(false);
            recargar(null);
        }

        private void recargar(string cuentaSeleccionar)
        {
            try
            {
                usuarios = datos.listarUsuarios();
            }
            catch (Exception ex)
            {
                usuarios = new List<Usuario>();
                MessageBox.Show("No se pudo leer la tabla de usuarios.\n\n" + ex.Message,
                    "Base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = usuarios;

            int indice = usuarios.FindIndex(u => u.Cuenta == cuentaSeleccionar);
            seleccionar(indice >= 0 ? indice : 0);

            mostrarFicha();
        }

        private int indiceActual
        {
            get { return dgvUsuarios.CurrentRow == null ? -1 : dgvUsuarios.CurrentRow.Index; }
        }

        private Usuario usuarioActual
        {
            get { return indiceActual >= 0 && indiceActual < usuarios.Count ? usuarios[indiceActual] : null; }
        }

        private void seleccionar(int indice)
        {
            if (indice < 0 || indice >= dgvUsuarios.Rows.Count) return;
            dgvUsuarios.CurrentCell = dgvUsuarios.Rows[indice].Cells[0];
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (!editando) mostrarFicha();
        }

        private void mostrarFicha()
        {
            epErrores.Clear();
            Usuario u = usuarioActual;

            if (u == null)
            {
                limpiarFicha();
                tslRegistro.Text = "Sin registros";
                return;
            }

            txtIdUsuario.Text = u.IdUsuario.ToString("000");
            txtCuenta.Text = u.Cuenta;
            txtClave.Text = u.Clave;
            txtConfirmar.Text = u.Clave;
            txtNombre.Text = u.Nombre;
            txtDireccion.Text = u.Direccion;
            txtTelefono.Text = u.Telefono;

            tslRegistro.Text = "Registro " + (indiceActual + 1) + " de " + usuarios.Count;
        }

        private void limpiarFicha()
        {
            txtIdUsuario.Clear();
            txtCuenta.Clear();
            txtClave.Clear();
            txtConfirmar.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
        }

        private Usuario leerFicha()
        {
            Usuario actual = usuarioActual;
            return new Usuario
            {
                IdUsuario = esNuevo || actual == null ? 0 : actual.IdUsuario,
                Cuenta = txtCuenta.Text.Trim(),
                Clave = txtClave.Text,
                Nombre = txtNombre.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Telefono = txtTelefono.Text.Trim()
            };
        }

        private void irA(int indice)
        {
            if (usuarios.Count == 0) return;
            if (indice < 0) indice = 0;
            if (indice > usuarios.Count - 1) indice = usuarios.Count - 1;
            seleccionar(indice);
        }

        private void tsbPrimero_Click(object sender, EventArgs e) { irA(0); }
        private void tsbAnterior_Click(object sender, EventArgs e) { irA(indiceActual - 1); }
        private void tsbSiguiente_Click(object sender, EventArgs e) { irA(indiceActual + 1); }
        private void tsbUltimo_Click(object sender, EventArgs e) { irA(usuarios.Count - 1); }

        private void tsbAgregar_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            establecerEdicion(true);
            limpiarFicha();
            txtIdUsuario.Text = "nuevo";
            txtCuenta.Focus();
        }

        private void tsbModificar_Click(object sender, EventArgs e)
        {
            if (usuarioActual == null) return;
            esNuevo = false;
            establecerEdicion(true);
            txtConfirmar.Clear();
            txtCuenta.Focus();
        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            establecerEdicion(false);
            mostrarFicha();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            Usuario u = usuarioActual;
            if (u == null) return;

            if (usuarioSesion != null && u.IdUsuario == usuarioSesion.IdUsuario)
            {
                MessageBox.Show("No puede eliminar el usuario con el que inició sesión.", "Eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Se eliminará a " + u.Nombre + " (" + u.Cuenta + ").\n¿Desea continuar?",
                    "Eliminar usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                datos.mantenimiento_usuarios_USSS081126(u, Operacion.Eliminar);
                tslRegistro.Text = "Usuario eliminado";
                recargar(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            Usuario u = leerFicha();
            Dictionary<string, string> errores = ValidadorUsuario.Validar(u, txtConfirmar.Text);

            try
            {
                if (!errores.ContainsKey(ValidadorUsuario.CAMPO_CUENTA) && datos.existeCuenta(u.Cuenta, u.IdUsuario))
                {
                    errores[ValidadorUsuario.CAMPO_CUENTA] = "Ya existe otro usuario con ese nombre.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            epErrores.Clear();
            if (errores.Count > 0)
            {
                Control primero = null;
                foreach (KeyValuePair<string, string> error in errores)
                {
                    Control c = controlesPorCampo[error.Key];
                    epErrores.SetError(c, error.Value);
                    if (primero == null) primero = c;
                }
                tslRegistro.Text = "Revise los campos marcados — no se guardó";
                primero.Focus();
                return;
            }

            try
            {
                datos.mantenimiento_usuarios_USSS081126(u, esNuevo ? Operacion.Agregar : Operacion.Modificar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (esNuevo) CuentaRegistrada = u.Cuenta;

            establecerEdicion(false);
            recargar(u.Cuenta);
            tslRegistro.Text += esNuevo ? "  ·  agregado" : "  ·  actualizado";
        }

        private void establecerEdicion(bool activar)
        {
            editando = activar;

            foreach (TextBox t in new[] { txtCuenta, txtClave, txtConfirmar, txtNombre, txtDireccion, txtTelefono })
            {
                t.ReadOnly = !activar;
            }

            dgvUsuarios.Enabled = !activar;
            tsbPrimero.Enabled = !activar;
            tsbAnterior.Enabled = !activar;
            tsbSiguiente.Enabled = !activar;
            tsbUltimo.Enabled = !activar;
            tsbAgregar.Enabled = !activar;
            tsbModificar.Enabled = !activar;
            tsbEliminar.Enabled = !activar;
            tsbGuardar.Enabled = activar;
            tsbCancelar.Enabled = activar;

            if (!activar) lblModo.Text = "Modo consulta";
            else lblModo.Text = esNuevo ? "Agregando un usuario nuevo" : "Modificando el usuario seleccionado";
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            if (soloRegistro)
                DialogResult = CuentaRegistrada != null ? DialogResult.OK : DialogResult.Cancel;
            else
                Close();
        }
    }
}
