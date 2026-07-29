using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculadora_basica
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnNum1_Click(object sender, EventArgs e)
        { 
          

            double num1=0, num2=0, resultado=0;
            num1 = Double.Parse(txtNum1.Text); //
            num2 = Double.Parse(txtNum2.Text);

            if (rdbsuma.Checked) {
                    resultado = num1 + num2;
                }
            if (rdbresta.Checked)
            {
                resultado = num1 - num2;
            }
            if (rdbmulti.Checked)
            {
                resultado = num1 * num2;
            }
            if (rdbdivi.Checked)
            {
                resultado = num1 / num2;
            }
            if (rdbexpo.Checked)
            {
                resultado = Math.Pow(num1, num2);
            }
            if (rdbraiz.Checked)
            {
                resultado = Math.Pow(num1, 1 / num2);
            }
            if (rdbfactrial.Checked) 
            {
                resultado = Math.Pow(num1, 1 / num2);
            }
            lbl4.Text = resultado.ToString();
            }

        private void btn2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
