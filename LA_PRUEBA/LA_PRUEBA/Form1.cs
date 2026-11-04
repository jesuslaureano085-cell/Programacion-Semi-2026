using System;
using System.Windows.Forms;

namespace LA_PRUEBA
{
    /// <summary>
    /// Ventana de inicio de sesión.
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            if (usuario == "" || contrasena == "")
            {
                MessageBox.Show("Escribe tu usuario y contraseña.", "Faltan datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AlmacenUsuarios.Validar(usuario, contrasena))
            {
                MessageBox.Show("Prueba concluida", "Inicio de sesión",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.\n¿Ya te registraste?", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContrasena.Clear();
                txtContrasena.Focus();
            }
        }

        private void lnkRegistrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var registro = new frmRegistro())
            {
                if (registro.ShowDialog(this) == DialogResult.OK)
                {
                    // Rellenamos el usuario recién registrado para iniciar sesión más rápido
                    txtUsuario.Text = registro.UsuarioRegistrado;
                    txtContrasena.Clear();
                    txtContrasena.Focus();
                }
            }
        }
    }
}
