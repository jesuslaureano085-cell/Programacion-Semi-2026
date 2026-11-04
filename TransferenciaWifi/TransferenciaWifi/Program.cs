using System;
using System.Windows.Forms;

namespace TransferenciaWifi
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormServidor());
        }
    }
}
