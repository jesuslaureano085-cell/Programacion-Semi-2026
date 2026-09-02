using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Tabla_de_meses_acumulados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ConfigurarGrafico();
        }

        private void ConfigurarGrafico()
        {
            chrtAcumulado.Series.Clear();
            chrtAcumulado.Titles.Clear();

            chrtAcumulado.Titles.Add("Crecimiento del saldo acumulado");

            Series serie = new Series("Saldo");
            serie.ChartType = SeriesChartType.Line;
            serie.BorderWidth = 3;
            serie.Color = System.Drawing.Color.SeaGreen;
            serie.MarkerStyle = MarkerStyle.Circle;
            serie.MarkerSize = 6;

            chrtAcumulado.Series.Add(serie);
            chrtAcumulado.ChartAreas[0].AxisX.Title = "Mes";
            chrtAcumulado.ChartAreas[0].AxisY.Title = "Saldo ($)";
        }

        private void btnGenerarTabla_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtMontoInicial.Text, out double montoInicial) ||
                !double.TryParse(txtAporteMensual.Text, out double aporteMensual) ||
                !double.TryParse(txtTasaAnual.Text, out double tasaAnual) ||
                !int.TryParse(txtNumMeses.Text, out int nMeses) || nMeses <= 0)
            {
                MessageBox.Show("Verifique que todos los datos sean numéricos y válidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvAcumulado.Rows.Clear();
            dgvAcumulado.Columns.Clear();
            dgvAcumulado.Columns.Add("mes", "Mes");
            dgvAcumulado.Columns.Add("aporte", "Aporte");
            dgvAcumulado.Columns.Add("interes", "Interés");
            dgvAcumulado.Columns.Add("saldo", "Saldo Acumulado");

            chrtAcumulado.Series["Saldo"].Points.Clear();

            double tasaMensual = (tasaAnual / 100) / 12;
            double saldo = montoInicial;

            // Punto inicial en el gráfico (mes 0 = monto inicial)
            chrtAcumulado.Series["Saldo"].Points.AddXY(0, saldo);

            for (int mes = 1; mes <= nMeses; mes++)
            {
                double interes = saldo * tasaMensual;
                saldo = saldo + aporteMensual + interes;

                dgvAcumulado.Rows.Add(mes, aporteMensual.ToString("N2"), interes.ToString("N2"), saldo.ToString("N2"));
                chrtAcumulado.Series["Saldo"].Points.AddXY(mes, saldo);
            }
        }

        private void btnLimpiarMeses_Click(object sender, EventArgs e)
        {
            txtMontoInicial.Clear();
            txtAporteMensual.Clear();
            txtTasaAnual.Clear();
            txtNumMeses.Clear();

            dgvAcumulado.Rows.Clear();
            chrtAcumulado.Series["Saldo"].Points.Clear();

            txtMontoInicial.Focus();
        }

        
    }
}