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
        // ============ VARIABLES NUEVAS ============
        private DateTime fechaNacimientoSeleccionada;
        private int edadCalculada = -1;
        private string expedienteGenerado = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Preparar los ComboBox (según tu código: cbx1 = triaje, cbx2 = género)
            cbx2.Items.Clear();
            cbx2.Items.AddRange(new string[] { "Masculino", "Femenino" });

            cbx1.Items.Clear();
            cbx1.Items.AddRange(new string[] {
                "Rojo - Emergencia", "Naranja - Muy Urgente", "Amarillo - Urgente",
                "Verde - Poco Urgente", "Azul - No Urgente"
            });

            txt2.ReadOnly = true; // Expediente: no se escribe a mano
            txt3.ReadOnly = true; // Edad: viene del segundo formulario
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

        // ============ MÉTODO NUEVO: Generar expediente aleatorio ============
        private string GenerarExpediente()
        {
            Random rnd = new Random();
            string numero = "";
            for (int i = 1; i <= 9; i++)
            {
                numero += rnd.Next(0, 10).ToString();
            }
            return "USS" + numero;
        }

        private void btmVerificar_Click(object sender, EventArgs e)
        {
            string nombreCompleto = txt1.Text;
            string genero = cbx2.Text;
            string triaje = cbx1.Text;

            // Caso: nada lleno
            if (string.IsNullOrWhiteSpace(nombreCompleto) && edadCalculada < 0 &&
                string.IsNullOrWhiteSpace(genero) && string.IsNullOrWhiteSpace(triaje))
            {
                MessageBox.Show("Por favor, introduzca datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Caso: falta al menos uno (nombre, edad, género o triaje)
            if (string.IsNullOrWhiteSpace(nombreCompleto) || edadCalculada < 0 ||
                string.IsNullOrWhiteSpace(genero) || string.IsNullOrWhiteSpace(triaje))
            {
                MessageBox.Show("Por favor, complete el formulario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Generar expediente solo si aún no existe
            if (expedienteGenerado == "")
            {
                expedienteGenerado = GenerarExpediente();
            }
            txt2.Text = expedienteGenerado;

            string numeroExpediente = txt2.Text;
            string edad = txt3.Text;

            string mensaje = "DATOS ANALIZADOS" + Environment.NewLine + Environment.NewLine +
                    "Nombre completo: " + nombreCompleto + Environment.NewLine +
                    "Número de Expediente: " + numeroExpediente + Environment.NewLine +
                    "Edad: " + edad + Environment.NewLine +
                    "Genero: " + genero + Environment.NewLine +
                    "Triaje: " + triaje;

            MessageBox.Show(mensaje, "Datos Verificados", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Text = "✔ Datos Registrados - " + nombreCompleto;
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

        private void TextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ============ MÉTODO NUEVO: Abrir segundo formulario al hacer clic en txt3 ============
        private void txt3_Click(object sender, EventArgs e)
        {
            FormEdad formEdad = new FormEdad();
            formEdad.ShowDialog();

            if (formEdad.EdadFueCalculada)
            {
                edadCalculada = formEdad.EdadResultado;
                fechaNacimientoSeleccionada = formEdad.FechaNacimientoResultado;
                txt3.Text = edadCalculada + " años - Nació el " + fechaNacimientoSeleccionada.ToString("dd/MM/yyyy");
            }
        }
    }
}