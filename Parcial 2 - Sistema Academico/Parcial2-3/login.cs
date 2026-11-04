using System;
using System.Windows.Forms;

namespace Parcial2_3
{
    public partial class login : Form
    {
        private const int MAX_INTENTOS = 3;
        private const int SEGUNDOS_BLOQUEO = 30;

        private readonly Conexion_base_datos datos = new Conexion_base_datos();
        private int fallos;
        private int segundosRestantes;

        public Usuario UsuarioAutenticado { get; private set; }

        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            txtCuenta.Focus();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string cuenta = txtCuenta.Text.Trim();
            lblEstado.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);

            if (cuenta == "" || txtClave.Text == "")
            {
                lblEstado.Text = "Escriba su usuario y su clave.";
                return;
            }

            Usuario encontrado;
            try
            {
                encontrado = datos.autenticar(cuenta, txtClave.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible conectarse a la base de datos.\n" +
                    "Ejecute primero script_parcial2_3.sql.\n\n" + ex.Message,
                    "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (encontrado != null)
            {
                UsuarioAutenticado = encontrado;
                Close();
                return;
            }

            fallos++;
            txtClave.Clear();

            if (fallos >= MAX_INTENTOS)
            {
                bloquear();
            }
            else
            {
                lblEstado.Text = "Acceso denegado (intento " + fallos + " de " + MAX_INTENTOS + ").";
                txtClave.Focus();
            }
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            using (frm_usuario registro = frm_usuario.ParaRegistro())
            {
                if (registro.ShowDialog(this) != DialogResult.OK) return;

                txtCuenta.Text = registro.CuentaRegistrada;
                txtClave.Clear();
                lblEstado.ForeColor = System.Drawing.Color.FromArgb(15, 118, 110);
                lblEstado.Text = "Cuenta creada. Escriba su clave para entrar.";
                txtClave.Focus();
            }
        }

        private void bloquear()
        {
            segundosRestantes = SEGUNDOS_BLOQUEO;
            txtCuenta.Enabled = false;
            txtClave.Enabled = false;
            btnIngresar.Enabled = false;
            btnRegistrarse.Enabled = false;
            lblEstado.Text = "Demasiados intentos. Espere " + segundosRestantes + " s.";
            tmrBloqueo.Start();
        }

        private void tmrBloqueo_Tick(object sender, EventArgs e)
        {
            segundosRestantes--;

            if (segundosRestantes > 0)
            {
                lblEstado.Text = "Demasiados intentos. Espere " + segundosRestantes + " s.";
                return;
            }

            tmrBloqueo.Stop();
            fallos = 0;
            txtCuenta.Enabled = true;
            txtClave.Enabled = true;
            btnIngresar.Enabled = true;
            btnRegistrarse.Enabled = true;
            lblEstado.Text = "";
            txtClave.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            UsuarioAutenticado = null;
            Close();
        }
    }
}
