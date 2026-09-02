using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabajo_con_LINQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btm1_Click(object sender, EventArgs e)
        {
            string[] serie = txt1.Text.Split(',');
            lts1.Items.Clear();

            lts1.DataSource = serie.Select(n => int.Parse(n)).Where(n => n % 2 == 0)
                .OrderBy(n => n)
                .ToList();
        }
    }
}
