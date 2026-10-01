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
            // AQUÍ ESTÁ EL CAMBIO IMPORTANTE:
            Application.Run(new frmPrincipal());
        }
    }
}