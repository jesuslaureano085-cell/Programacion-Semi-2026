using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_gestion_del_tiempo_y_demanda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btmVerificar_Click(object sender, EventArgs e)
        {
            string nombreCompleto = txtNombre.Text;
            string numeroExpediente = txtExpediente.Text;
            string edad = txtEdad.Text;
            string genero = cbxGenero.Text;
            string dui = txtDui.Text;
            string direccion = txtDireccion.Text;
            string telefono = txtTelefono.Text;
            string tipoSangre = cbxSangre.Text;
            string triaje = txtTriage.Text;

            if (string.IsNullOrWhiteSpace(nombreCompleto) && string.IsNullOrWhiteSpace(numeroExpediente) &&
        string.IsNullOrWhiteSpace(edad) && string.IsNullOrWhiteSpace(genero) &&
        string.IsNullOrWhiteSpace(dui) && string.IsNullOrWhiteSpace(direccion) &&
        string.IsNullOrWhiteSpace(telefono) && string.IsNullOrWhiteSpace(tipoSangre) &&
        string.IsNullOrWhiteSpace(triaje))
            {
                MessageBox.Show("Por favor, introduzca datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

     
            if (string.IsNullOrWhiteSpace(nombreCompleto) || string.IsNullOrWhiteSpace(numeroExpediente) ||
                string.IsNullOrWhiteSpace(edad) || string.IsNullOrWhiteSpace(genero) ||
                string.IsNullOrWhiteSpace(dui) && string.IsNullOrWhiteSpace(direccion) ||
                string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(tipoSangre) ||
                string.IsNullOrWhiteSpace(triaje))
            {
                MessageBox.Show("Por favor, complete el formulario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string mensaje = "DATOS ANALIZADOS" + Environment.NewLine + Environment.NewLine +
                    "Nombre completo: " + nombreCompleto + Environment.NewLine +
                    "Número de Expediente" + numeroExpediente + Environment.NewLine +
                    "Edad: " + edad + Environment.NewLine +
                    "Genero: " + genero + Environment.NewLine +
                    "Dui/Cedula: " + dui + Environment.NewLine +
                    "Dirección: " + direccion + Environment.NewLine +
                    "Teléfono: " + telefono + Environment.NewLine +
                    "Tipo de Sangre: " + tipoSangre + Environment.NewLine +
                    "Triaje: " + triaje;

            MessageBox.Show(mensaje, "Datos Verificados", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btmSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtEdad_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbxGenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblTelefono_Click(object sender, EventArgs e)
        {

        }
        // 1. La firma del método debe ser EXACTA (mismo tipo de parámetros)
        private void TextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
