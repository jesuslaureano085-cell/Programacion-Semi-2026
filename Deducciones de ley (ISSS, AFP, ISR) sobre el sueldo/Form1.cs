using System;
using System.Windows.Forms;

namespace Deducciones_de_ley__ISSS__AFP__ISR__sobre_el_sueldo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ConfigurarTabla();
        }

        // ============ Configura las columnas de la tabla una sola vez, al iniciar ============
        private void ConfigurarTabla()
        {
            dgvResultados.Columns.Clear();
            dgvResultados.Columns.Add("colNombre", "Nombre");
            dgvResultados.Columns.Add("colSueldo", "Sueldo");
            dgvResultados.Columns.Add("colISSS", "ISSS");
            dgvResultados.Columns.Add("colAFP", "AFP");
            dgvResultados.Columns.Add("colISR", "ISR");
            dgvResultados.Columns.Add("colLiquido", "Líquido a pagar");
            dgvResultados.ReadOnly = true;
            dgvResultados.AllowUserToAddRows = false;
        }

        private double CalcularISR(double rentaImponible)
        {
            double isr;
            if (rentaImponible <= 472.00)
                isr = 0.00;
            else if (rentaImponible <= 895.24)
                isr = (rentaImponible - 472.00) * 0.10 + 17.67;
            else if (rentaImponible <= 2038.10)
                isr = (rentaImponible - 895.24) * 0.20 + 60.00;
            else
                isr = (rentaImponible - 2038.10) * 0.30 + 288.57;
            return isr;
        }

        private void btnCalcularPlanilla_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre de la persona.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtSueldo.Text, out double sueldo) || sueldo <= 0)
            {
                MessageBox.Show("Ingrese un sueldo válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ISSS: 3% del sueldo, con tope de cotización de $1000.00
            double topeISSS = 1000.00;
            double baseISSS = sueldo > topeISSS ? topeISSS : sueldo;
            double isss = baseISSS * 0.03;

            // AFP: 7.25% del sueldo (sin tope)
            double afp = sueldo * 0.0725;

            // Renta imponible para ISR = sueldo - ISSS - AFP
            double rentaImponible = sueldo - isss - afp;
            double isr = CalcularISR(rentaImponible);

            double liquido = sueldo - isss - afp - isr;

            // Llena las cajas de texto de arriba (igual que antes)
            txtISSS.Text = isss.ToString("N2");
            txtAFP.Text = afp.ToString("N2");
            txtISR.Text = isr.ToString("N2");
            txtLiquido.Text = liquido.ToString("N2");

            // Agrega una fila nueva a la tabla de abajo
            dgvResultados.Rows.Add(
                txtNombre.Text,
                sueldo.ToString("N2"),
                isss.ToString("N2"),
                afp.ToString("N2"),
                isr.ToString("N2"),
                liquido.ToString("N2")
            );
        }

        // ============ Botón Limpiar: borra cajas de texto y la tabla ============
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtSueldo.Clear();
            txtISSS.Clear();
            txtAFP.Clear();
            txtISR.Clear();
            txtLiquido.Clear();

            dgvResultados.Rows.Clear();

            txtNombre.Focus(); // coloca el cursor listo para el siguiente ingreso
        }

        private void txtSueldo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtISSS_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblAFP_Click(object sender, EventArgs e)
        {

        }

        private void lblISR_Click(object sender, EventArgs e)
        {

        }
    }
}