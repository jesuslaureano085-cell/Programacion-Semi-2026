using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_de_programacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // cbx1 = Triaje
            cbx1.Items.Clear();
            cbx1.Items.AddRange(new string[] {
                "Rojo - Emergencia", "Naranja - Muy Urgente", "Amarillo - Urgente",
                "Verde - Poco Urgente", "Azul - No Urgente"
            });

            // cbx2 = Género
            cbx2.Items.Clear();
            cbx2.Items.AddRange(new string[] { "Masculino", "Femenino" });

            txt2.ReadOnly = true; // Expediente: autogenerado, no editable

            // Configurar el DateTimePicker
            dtpNacimiento.Format = DateTimePickerFormat.Custom;
            dtpNacimiento.CustomFormat = "dd/MM/yyyy";
            dtpNacimiento.MaxDate = DateTime.Now;
            dtpNacimiento.MinDate = DateTime.Now.AddYears(-120);
            dtpNacimiento.Value = DateTime.Now.AddYears(-20);
        }

        private void txt1_TextChanged(object sender, EventArgs e)
        {
        }

        private void cbx1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txt2_TextChanged(object sender, EventArgs e)
        {
        }

        private void cbx2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // ============ Función auxiliar: calcula la edad a partir de una fecha ============
        private int CalcularEdad(DateTime fechaNac)
        {
            int edad = DateTime.Now.Year - fechaNac.Year;
            if (DateTime.Now.DayOfYear < fechaNac.DayOfYear)
            {
                edad--;
            }
            return edad;
        }

        // ============ Generar expediente aleatorio (nuevo cada vez que se llama) ============
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

        // ============ btmREGISTRO: procesa el registro ============
        private void btmREGISTRO_Click(object sender, EventArgs e)
        {
            string nombreCompleto = txt1.Text;
            string triaje = cbx1.Text;
            string genero = cbx2.Text;
            DateTime fechaNac = dtpNacimiento.Value;
            int edad = CalcularEdad(fechaNac);

            if (string.IsNullOrWhiteSpace(nombreCompleto) ||
                string.IsNullOrWhiteSpace(triaje) || string.IsNullOrWhiteSpace(genero))
            {
                MessageBox.Show("Por favor, complete el formulario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Genera un número de expediente nuevo cada vez
            txt2.Text = GenerarExpediente();

            string mensaje = "DATOS ANALIZADOS" + Environment.NewLine + Environment.NewLine +
                    "Nombre completo: " + nombreCompleto + Environment.NewLine +
                    "Número de Expediente: " + txt2.Text + Environment.NewLine +
                    "Fecha de Nacimiento: " + fechaNac.ToString("dd/MM/yyyy") + Environment.NewLine +
                    "Edad: " + edad + " años" + Environment.NewLine +
                    "Triaje: " + triaje + Environment.NewLine +
                    "Genero: " + genero;

            MessageBox.Show(mensaje, "Datos Registrados", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Text = "✔ Datos Registrados - " + nombreCompleto;
        }

        // ============ btmVerificar: revisa si el formulario está lleno o incompleto ============
        private void btmVerificar_Click(object sender, EventArgs e)
        {
            int camposVacios = 0;
            int totalCampos = 3; // Nombre, Triaje, Género

            if (string.IsNullOrWhiteSpace(txt1.Text)) camposVacios++;
            if (string.IsNullOrWhiteSpace(cbx1.Text)) camposVacios++;
            if (string.IsNullOrWhiteSpace(cbx2.Text)) camposVacios++;

            if (camposVacios == totalCampos)
            {
                MessageBox.Show("El paciente no ha rellenado ningún campo del formulario.", "Formulario Vacío", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (camposVacios > 0)
            {
                MessageBox.Show(
                    "El paciente no ha rellenado la mayoría de los campos.\nCampos pendientes: " + camposVacios + " de " + totalCampos,
                    "Formulario Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fechaNac = dtpNacimiento.Value;
            int edad = CalcularEdad(fechaNac);

            // También genera un expediente nuevo al verificar
            txt2.Text = GenerarExpediente();

            string mensaje = "El expediente está completo." + Environment.NewLine + Environment.NewLine +
                    "Nombre: " + txt1.Text + Environment.NewLine +
                    "Número de Expediente: " + txt2.Text + Environment.NewLine +
                    "Fecha de Nacimiento: " + fechaNac.ToString("dd/MM/yyyy") + Environment.NewLine +
                    "Edad: " + edad + " años" + Environment.NewLine +
                    "Triaje: " + cbx1.Text + Environment.NewLine +
                    "Genero: " + cbx2.Text;

            MessageBox.Show(mensaje, "Verificación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ============ btmMenu: volver al menú principal ============
        private void btmMenu_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lbl1_Click(object sender, EventArgs e)
        {

        }
    }
}