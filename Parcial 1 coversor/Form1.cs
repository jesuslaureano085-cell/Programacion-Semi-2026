using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace Parcial_1_conversor
{
    public partial class Parcial_1_Conversor : Form
    {
        // Factor de conversión de cada unidad hacia Metro Cuadrado (unidad base)
        private Dictionary<string, double> factoresAM2;

        public Parcial_1_Conversor()
        {
            InitializeComponent();
        }

        private void Parcial_1_Conversor_Load(object sender, EventArgs e)
        {
            CargarFactores();
            CargarUnidadesEnCombos();
            ConfigurarHistorial();
        }

        private void CargarFactores()
        {
            // Valores de referencia usados comúnmente en El Salvador / Centroamérica
            factoresAM2 = new Dictionary<string, double>
            {
                { "Pie Cuadrado",    0.09290304 },
                { "Vara Cuadrada",   0.698896   },
                { "Yarda Cuadrada",  0.83612736 },
                { "Metro Cuadrado",  1.0        },
                { "Tareas",          436.81    },
                { "Manzana",         6988.96    },
                { "Hectárea",        10000.0    },
            };
        }

        private void CargarUnidadesEnCombos()
        {
            foreach (string unidad in factoresAM2.Keys)
            {
                cmbUnidadOrigen.Items.Add(unidad);
                cmbUnidadDestino.Items.Add(unidad);
            }

            cmbUnidadOrigen.SelectedItem = "Manzana";
            cmbUnidadDestino.SelectedItem = "Metro Cuadrado";
        }

        private void ConfigurarHistorial()
        {
            dgvHistorial.Columns.Clear();
            dgvHistorial.Columns.Add("colValorOrigen", "Valor");
            dgvHistorial.Columns.Add("colUnidadOrigen", "Unidad Origen");
            dgvHistorial.Columns.Add("colValorDestino", "Resultado");
            dgvHistorial.Columns.Add("colUnidadDestino", "Unidad Destino");
        }

        // Convierte pasando primero a Metro Cuadrado y luego a la unidad destino
        private double ConvertirArea(double valor, string unidadOrigen, string unidadDestino)
        {
            double valorEnM2 = valor * factoresAM2[unidadOrigen];
            double resultado = valorEnM2 / factoresAM2[unidadDestino];
            return resultado;
        }

        private void btnConvertir_Click(object sender, EventArgs e)
        {
            if (cmbUnidadOrigen.SelectedItem == null || cmbUnidadDestino.SelectedItem == null)
            {
                MessageBox.Show("Seleccione la unidad de origen y destino.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtValor.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double valor) || valor < 0)
            {
                MessageBox.Show("Ingrese un valor numérico válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string unidadOrigen = cmbUnidadOrigen.SelectedItem.ToString();
            string unidadDestino = cmbUnidadDestino.SelectedItem.ToString();

            double resultado = ConvertirArea(valor, unidadOrigen, unidadDestino);

            txtResultado.Text = resultado.ToString("N4") + "  " + unidadDestino;

            dgvHistorial.Rows.Insert(0,
                valor.ToString("N2"),
                unidadOrigen,
                resultado.ToString("N4"),
                unidadDestino
            );
        }

        private void btnIntercambiar_Click(object sender, EventArgs e)
        {
            object temp = cmbUnidadOrigen.SelectedItem;
            cmbUnidadOrigen.SelectedItem = cmbUnidadDestino.SelectedItem;
            cmbUnidadDestino.SelectedItem = temp;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtValor.Clear();
            txtResultado.Clear();
            dgvHistorial.Rows.Clear();
            txtValor.Focus();
        }
    }
}
