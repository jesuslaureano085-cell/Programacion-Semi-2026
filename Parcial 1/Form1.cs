using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Impuesto_Actividades_Economicas
{
    public partial class Parcial_1 : Form
    {
        // Estructura de cada tramo de la tabla
        private class TramoIAE
        {
            public double Desde;
            public double Hasta;
            public double CuotaFija;
            public double TasaPorMillar; // por cada $1,000 o fracción de excedente
        }

        private List<TramoIAE> tablaIAE;

        public Parcial_1()
        {
            InitializeComponent();
        }

        private void Parcial_1_Load(object sender, EventArgs e)
        {
            ConfigurarTabla();
            CargarTablaIAE();
        }

        // ============ Tabla real IAE - Tarifas Mensuales (San Salvador) ============
        private void CargarTablaIAE()
        {
            tablaIAE = new List<TramoIAE>
            {
                new TramoIAE { Desde = 0.01,           Hasta = 1142.86,          CuotaFija = 1.14,   TasaPorMillar = 0.00 },
                new TramoIAE { Desde = 1142.87,         Hasta = 5714.29,          CuotaFija = 2.29,   TasaPorMillar = 0.20 },
                new TramoIAE { Desde = 5714.30,         Hasta = 11428.57,         CuotaFija = 5.71,   TasaPorMillar = 0.30 },
                new TramoIAE { Desde = 11428.58,        Hasta = 57142.86,         CuotaFija = 11.43,  TasaPorMillar = 0.40 },
                new TramoIAE { Desde = 57142.87,        Hasta = 114285.71,        CuotaFija = 34.29,  TasaPorMillar = 0.50 },
                new TramoIAE { Desde = 114285.72,       Hasta = 571428.57,        CuotaFija = 80.00,  TasaPorMillar = 0.60 },
                new TramoIAE { Desde = 571428.58,       Hasta = 1142857.14,       CuotaFija = 428.57, TasaPorMillar = 0.70 },
                new TramoIAE { Desde = 1142857.15,      Hasta = double.MaxValue,  CuotaFija = 928.57, TasaPorMillar = 0.80 },
            };
        }

        // ============ Configura las columnas de la tabla de resultados ============
        private void ConfigurarTabla()
        {
            dgvResultados.Columns.Clear();
            dgvResultados.Columns.Add("colNombre", "Nombre / Negocio");
            dgvResultados.Columns.Add("colActivo", "Activo Imponible ($)");
            dgvResultados.Columns.Add("colTramo", "Tramo Aplicado");
            dgvResultados.Columns.Add("colImpuesto", "Impuesto Mensual a Pagar ($)");
            dgvResultados.ReadOnly = true;
            dgvResultados.AllowUserToAddRows = false;
        }

        // ============ Función que calcula el impuesto según la tabla real ============
        private double CalcularImpuesto(double activoImponible, out string tramoTexto)
        {
            foreach (TramoIAE tramo in tablaIAE)
            {
                if (activoImponible >= tramo.Desde && activoImponible <= tramo.Hasta)
                {
                    double excedente = activoImponible - tramo.Desde;

                    // "por cada $1,000 o fracción de excedente" = se redondea hacia arriba
                    double bloquesDeMil = Math.Ceiling(excedente / 1000.0);
                    if (bloquesDeMil < 0) bloquesDeMil = 0;

                    double impuestoVariable = bloquesDeMil * tramo.TasaPorMillar;
                    double impuestoTotal = tramo.CuotaFija + impuestoVariable;

                    string hastaTexto = tramo.Hasta == double.MaxValue ? "En adelante" : tramo.Hasta.ToString("N2");
                    tramoTexto = $"${tramo.Desde:N2} - ${hastaTexto}";

                    return impuestoTotal;
                }
            }

            tramoTexto = "No aplica";
            return 0;
        }

        // ============ Botón Calcular ============
        private void btnCalcularImpuesto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreNegocio.Text))
            {
                MessageBox.Show("Ingrese el nombre del negocio o contribuyente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtCapitalDeclarado.Text, out double activoImponible) || activoImponible < 0)
            {
                MessageBox.Show("Ingrese un valor de activo imponible válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double impuesto = CalcularImpuesto(activoImponible, out string tramo);

            txtTramoAplicado.Text = tramo;
            txtImpuestoAPagar.Text = impuesto.ToString("N2");

            dgvResultados.Rows.Add(
                txtNombreNegocio.Text,
                activoImponible.ToString("N2"),
                tramo,
                impuesto.ToString("N2")
            );
        }

        // ============ Botón Limpiar ============
        private void btnLimpiarImpuesto_Click(object sender, EventArgs e)
        {
            txtNombreNegocio.Clear();
            txtCapitalDeclarado.Clear();
            txtTramoAplicado.Clear();
            txtImpuestoAPagar.Clear();

            dgvResultados.Rows.Clear();

            txtNombreNegocio.Focus();
        }
    }
}