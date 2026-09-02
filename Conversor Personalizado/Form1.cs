using System;
using System.Windows.Forms;

namespace Conversor_Personalizado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Lista de 10 unidades de origen (puedes poner las que quieras, son solo nombres libres)
            cdbUnidadOrigen.Items.Clear();
            cdbUnidadOrigen.Items.AddRange(new string[] {
                "Cajas", "Docenas", "Paquetes", "Bolsas", "Sacos",
                "Fardos", "Rollos", "Botellas", "Latas", "Unidades"
            });

            // Lista de 10 unidades de destino
            cdbUnidadDestino.Items.Clear();
            cdbUnidadDestino.Items.AddRange(new string[] {
                "Piezas", "Kilos", "Litros", "Metros", "Toneladas",
                "Galones", "Gramos", "Yardas", "Docenas", "Unidades"
            });

            cdbUnidadOrigen.SelectedIndex = 0;
            cdbUnidadDestino.SelectedIndex = 0;
        }

        private void btnConvertirPersonalizado_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtCantidadPersonalizado.Text, out double cantidad))
            {
                MessageBox.Show("Ingrese una cantidad numérica válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtValorConversion.Text, out double factor) || factor == 0)
            {
                MessageBox.Show("Ingrese un valor de conversión válido (diferente de cero).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cdbUnidadOrigen.SelectedItem == null || cdbUnidadDestino.SelectedItem == null)
            {
                MessageBox.Show("Seleccione ambas unidades.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string unidadOrigen = cdbUnidadOrigen.SelectedItem.ToString();
            string unidadDestino = cdbUnidadDestino.SelectedItem.ToString();

            double resultado = cantidad * factor;

            lblResultadoPersonalizado.Text =
                $"Resultado: {cantidad} {unidadOrigen} = {resultado:N4} {unidadDestino}";
        }

        private void btnLimpiarPersonalizado_Click(object sender, EventArgs e)
        {
            txtCantidadPersonalizado.Clear();
            txtValorConversion.Clear();
            lblResultadoPersonalizado.Text = "";

            cdbUnidadOrigen.SelectedIndex = 0;
            cdbUnidadDestino.SelectedIndex = 0;

            txtCantidadPersonalizado.Focus();
        }

        private void cdbUnidadOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cdbUnidadDestino_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}