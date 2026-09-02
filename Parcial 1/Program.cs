using System;
using System.Windows.Forms;

namespace Impuesto_Actividades_Economicas
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Parcial_1());
        }
    }
}