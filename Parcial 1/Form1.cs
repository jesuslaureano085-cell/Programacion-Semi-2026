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
            public double ImpuestoFijo;
            public double FactorPorMillar;
        }

        private List<TramoIAE> tablaComercial;

        public Parcial_1()
        {
            InitializeComponent();
        }

        private void Parcial_1_Load(object sender, EventArgs e)
        {
            ConfigurarTabla();
            CargarTablaIAE();
        }

        private void CargarTablaIAE()
        {
            tablaComercial = new List<TramoIAE>
            {
                new TramoIAE { Desde = 0,             Hasta = 1000.00,        ImpuestoFijo = 6.86,     FactorPorMillar = 0.000000 },
                new TramoIAE { Desde = 1000.01,        Hasta = 2857.14,        ImpuestoFijo = 3.00,     FactorPorMillar = 3.000000 },
                new TramoIAE { Desde = 2857.15,        Hasta = 5714.29,        ImpuestoFijo = 27.43,    FactorPorMillar = 1.234286 },
                new TramoIAE { Desde = 5714.30,        Hasta = 22857.14,       ImpuestoFijo = 58.29,    FactorPorMillar = 1.097143 },
                new TramoIAE { Desde = 22857.15,       Hasta = 57142.86,       ImpuestoFijo = 222.86,   FactorPorMillar = 0.960000 },
                new TramoIAE { Desde = 57142.87,       Hasta = 91428.57,       ImpuestoFijo = 510.86,   FactorPorMillar = 0.822857 },
                new TramoIAE { Desde = 91428.58,       Hasta = 137142.86,      ImpuestoFijo = 757.71,   FactorPorMillar = 0.685714 },
                new TramoIAE { Desde = 137142.87,      Hasta = 182857.14,      ImpuestoFijo = 1032.00,  FactorPorMillar = 0.548571 },
                new TramoIAE { Desde = 182857.15,      Hasta = 228571.43,      ImpuestoFijo = 1251.43,  FactorPorMillar = 0.411429 },
                new TramoIAE { Desde = 228571.44,      Hasta = 342857.14,      ImpuestoFijo = 1416.00,  FactorPorMillar = 0.274286 },
                new TramoIAE { Desde = 342857.15,      Hasta = 571428.57,      ImpuestoFijo = 1690.29,  FactorPorMillar = 0.205714 },
                new TramoIAE { Desde = 571428.57,      Hasta = 857142.86,      ImpuestoFijo = 2101.71,  FactorPorMillar = 0.137143 },
                new TramoIAE { Desde = 857142.86,      Hasta = 1142857.14,     ImpuestoFijo = 2444.57,  FactorPorMillar = 0.068571 },
                new TramoIAE { Desde = 1142857.14,     Hasta = 1714285.72,     ImpuestoFijo = 2616.00,  FactorPorMillar = 0.041143 },
                new TramoIAE { Desde = 1714285.72,     Hasta = double.MaxValue,ImpuestoFijo = 2821.71,  FactorPorMillar = 0.027429 },
            };
        }

        private void ConfigurarTabla()
        {
            dgvResultados.Columns.Clear();
            dgvResultados.Columns.Add("colNombre", "Nombre / Negocio");
            dgvResultados.Columns.Add("colActivo", "Activo Imponible ($)");
            dgvResultados.Columns.Add("colTramo", "Tramo Aplicado");
            dgvResultados.Columns.Add("colImpuesto", "Impuesto a Pagar ($)");
            dgvResultados.ReadOnly = true;
            dgvResultados.AllowUserToAddRows = false;
        }


        private double CalcularImpuesto(double activoImponible, out string tramoTexto)
        {
            foreach (TramoIAE tramo in tablaComercial)
            {
                if (activoImponible >= tramo.Desde && activoImponible <= tramo.Hasta)
                {
                    double excedente = activoImponible - tramo.Desde;
                    double fraccionMillar = excedente / 1000.0;
                    double impuestoVariable = fraccionMillar * tramo.FactorPorMillar;
                    double impuestoTotal = tramo.ImpuestoFijo + impuestoVariable;

                    string hastaTexto = tramo.Hasta == double.MaxValue ? "en adelante" : tramo.Hasta.ToString("N2");
                    tramoTexto = $"${tramo.Desde:N2} - ${hastaTexto}";

                    return impuestoTotal;
                }
            }

            tramoTexto = "No aplica";
            return 0;
        }


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

        private void btnLimpiarImpuesto_Click(object sender, EventArgs e)
        {
            txtNombreNegocio.Clear();
            txtCapitalDeclarado.Clear();
            txtTramoAplicado.Clear();
            txtImpuestoAPagar.Clear();

            dgvResultados.Rows.Clear();

            txtNombreNegocio.Focus();
        }

        private void txtNombreNegocio_TextChanged(object sender, EventArgs e)
        {

        }
    }
}