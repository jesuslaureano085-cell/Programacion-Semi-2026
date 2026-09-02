using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tarifa_de_agua_potable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private double CalcularTarifaAgua(double metros)
        {
            double total;

            if (metros <= 15)
            {
                total = 3.55; // tarifa mínima / cargo fijo
            }
            else if (metros <= 30)
            {
                total = 3.55 + (metros - 15) * 0.24;
            }
            else if (metros <= 45)
            {
                total = 3.55 + (15 * 0.24) + (metros - 30) * 0.36;
            }
            else
            {
                total = 3.55 + (15 * 0.24) + (15 * 0.36) + (metros - 45) * 0.50;
            }

            return total;
        }

        private void btnCalcularAgua_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtMetros.Text, out double metros) || metros < 0)
            {
                MessageBox.Show("Ingrese una cantidad de metros válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double total = CalcularTarifaAgua(metros);
            txtTotalAgua.Text = total.ToString("N2");
        }

        private void lblMetros_Click(object sender, EventArgs e)
        {

        }

        private void txtMetros_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTotalAgua_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTotalAgua_Click(object sender, EventArgs e)
        {

        }

       
    }
}
