using System;
using System.Windows.Forms;

namespace miPrimeaAplicacion
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            login frmLogin = new login();
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new frmPrincipal());
            }
        }
    }
}