using System;
using System.Windows.Forms;

namespace Parcial2_3
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SesionApp());
        }
    }
}
