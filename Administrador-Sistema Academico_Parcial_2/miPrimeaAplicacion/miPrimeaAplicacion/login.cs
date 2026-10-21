using System;
using System.Windows.Forms;

namespace miPrimeaAplicacion
{
    public partial class login : Form
    {
        Conexion objConexion = new Conexion();
        int intentos = 0;

        public login()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Trim() == "" || txtClave.Text == "")
            {
                MessageBox.Show("Escriba su usuario y clave.", "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (objConexion.validarLogin(txtUsuario.Text.Trim(), txtClave.Text))
            {
                MessageBox.Show("Bienvenido " + txtUsuario.Text.Trim(), "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Program.cs abre el sistema
                this.Close();
            }
            else
            {
                intentos++;
                if (intentos >= 3)
                {
                    MessageBox.Show("Acceso denegado. Supero los 3 intentos.", "Login",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
                MessageBox.Show("Acceso denegado: usuario o clave incorrectos.\nIntentos restantes: " + (3 - intentos),
                    "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClave.Clear();
                txtClave.Focus();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            frm_usuario objUsuario = new frm_usuario();
            objUsuario.ShowDialog(); // llama al registro de usuarios
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
