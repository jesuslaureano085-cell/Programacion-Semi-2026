using System;
using System.Windows.Forms;

namespace Parcial2_3
{
    public class SesionApp : ApplicationContext
    {
        public SesionApp()
        {
            mostrarLogin();
        }

        private void mostrarLogin()
        {
            login frmLogin = new login();
            frmLogin.FormClosed += login_FormClosed;
            frmLogin.Show();
        }

        private void login_FormClosed(object sender, FormClosedEventArgs e)
        {
            login frmLogin = (login)sender;

            if (frmLogin.UsuarioAutenticado == null)
            {
                ExitThread();
                return;
            }

            frmPrincipal principal = new frmPrincipal(frmLogin.UsuarioAutenticado);
            principal.FormClosed += principal_FormClosed;
            principal.Show();
        }

        private void principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmPrincipal principal = (frmPrincipal)sender;

            if (principal.CerrarSesion) mostrarLogin();
            else ExitThread();
        }
    }
}
