using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace miPrimeaAplicacion
{
    public partial class frmPeriodos : Form
    {
        public frmPeriodos()
        {
            InitializeComponent();
        }

        private void periodosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.periodosBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.db_academicaDataSet);

                // Opcional: Un mensaje de éxito
                MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Data.NoNullAllowedException)
            {
                // Atrapamos el error de los campos vacíos
                MessageBox.Show("Por favor, llena todos los campos (Período y Fecha) antes de guardar.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Atrapamos cualquier otro error por si acaso
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmPeriodos_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'db_academicaDataSet.periodos' Puede moverla o quitarla según sea necesario.
            this.periodosTableAdapter.Fill(this.db_academicaDataSet.periodos);

        }

        private void idPeriodoTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
