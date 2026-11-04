using System;
using System.Windows.Forms;

namespace LA_PRUEBA
{
    /// <summary>
    /// Ventana para registrar un usuario nuevo.
    /// </summary>
    public partial class frmRegistro : Form
    {
        /// <summary>Usuario que se acaba de registrar (para pasarlo al login).</summary>
        public string UsuarioRegistrado { get; private set; }

        public frmRegistro()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;
            string confirmar = txtConfirmar.Text;

            if (usuario == "" || contrasena == "")
            {
                MessageBox.Show("El usuario y la contraseña son obligatorios.", "Faltan datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (usuario.Contains("|"))
            {
                MessageBox.Show("El usuario no puede contener el carácter '|'.", "Dato inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (contrasena.Length < 4)
            {
                MessageBox.Show("La contraseña debe tener al menos 4 caracteres.", "Contraseña corta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (contrasena != confirmar)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmar.Clear();
                txtConfirmar.Focus();
                return;
            }

            if (AlmacenUsuarios.Existe(usuario))
            {
                MessageBox.Show("Ese usuario ya está registrado.", "Usuario existente",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AlmacenUsuarios.Registrar(usuario, contrasena);
            MessageBox.Show("¡Registro exitoso! Ahora inicia sesión.", "Registro",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            UsuarioRegistrado = usuario;
            DialogResult = DialogResult.OK; // cierra la ventana y vuelve al login
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
