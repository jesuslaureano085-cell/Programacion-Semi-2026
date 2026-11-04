using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Parcial2_3
{
    public partial class frmNotas : Form
    {
        private readonly Conexion_base_datos datos = new Conexion_base_datos();
        private BindingList<DetalleNota> notas = new BindingList<DetalleNota>();
        private bool cargando = true;

        public frmNotas()
        {
            InitializeComponent();
        }

        private void frmNotas_Load(object sender, EventArgs e)
        {
            dgvNotas.AutoGenerateColumns = false;
            try
            {
                tscMateria.ComboBox.DisplayMember = "Nombre";
                tscMateria.ComboBox.DataSource = datos.listarMaterias();
                tscPeriodo.ComboBox.DisplayMember = "Nombre";
                tscPeriodo.ComboBox.DataSource = datos.listarPeriodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron leer materias y períodos.\n\n" + ex.Message, "Base de datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cargando = false;
            cargarNotas();
        }

        private void filtro_Changed(object sender, EventArgs e)
        {
            if (cargando) return;

            if (notas.Any(n => n.Modificado))
            {
                DialogResult r = MessageBox.Show("Hay notas sin guardar. ¿Guardarlas antes de cambiar?", "Notas",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes) guardar();
            }
            cargarNotas();
        }

        private void cargarNotas()
        {
            Materia materia = tscMateria.SelectedItem as Materia;
            Periodo periodo = tscPeriodo.SelectedItem as Periodo;
            if (materia == null || periodo == null) return;

            try
            {
                notas = new BindingList<DetalleNota>(datos.listarNotas(materia.IdMateria, periodo.IdPeriodo));
                dgvNotas.DataSource = notas;
                lblModo.Text = materia.Nombre + "  ·  " + periodo.Nombre + " (inicio " + periodo.Fecha.ToShortDateString() + ")";
                actualizarResumen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudieron leer las notas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void actualizarResumen()
        {
            if (notas.Count == 0)
            {
                tslRegistro.Text = "No hay notas para esta materia en este período";
                tslAyuda.Text = "";
                return;
            }
            decimal promedio = notas.Average(n => n.NF);
            int aprobados = notas.Count(n => n.NF >= DetalleNota.NOTA_MINIMA);
            int pendientes = notas.Count(n => n.Modificado);

            tslRegistro.Text = notas.Count + " alumnos  ·  promedio " + promedio.ToString("0.00") +
                               "  ·  aprobados " + aprobados;
            tslAyuda.Text = pendientes == 0 ? "Todo guardado  " : pendientes + " fila(s) sin guardar  ";
        }

        private void dgvNotas_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string col = dgvNotas.Columns[e.ColumnIndex].Name;
            if (col != "colLab1" && col != "colLab2" && col != "colParcial") return;

            decimal nota;
            string texto = e.FormattedValue.ToString().Replace(',', '.');
            bool ok = decimal.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out nota)
                      && ValidadorAcademico.NotaValida(nota);

            dgvNotas.Rows[e.RowIndex].ErrorText = ok ? "" : "La nota debe estar entre 0 y 10";
            e.Cancel = !ok;
        }

        private void dgvNotas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvNotas.Rows[e.RowIndex].ErrorText = "";
            dgvNotas.InvalidateRow(e.RowIndex);
            actualizarResumen();
        }

        private void dgvNotas_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            dgvNotas.Rows[e.RowIndex].ErrorText = "Valor no válido";
            e.Cancel = true;
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void guardar()
        {
            dgvNotas.EndEdit();
            try
            {
                int n = datos.guardarNotas(notas);
                actualizarResumen();
                tslAyuda.Text = n == 0 ? "No había cambios  " : n + " nota(s) guardada(s)  ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbDeshacer_Click(object sender, EventArgs e)
        {
            dgvNotas.CancelEdit();
            cargarNotas();
        }
    }
}
