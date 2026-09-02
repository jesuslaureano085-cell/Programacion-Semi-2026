using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sistema_estadistico__calcule
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Leer y convertir el texto ingresado en txt1
                double[] datos = txt1.Text
                    .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(double.Parse)
                    .OrderBy(n => n)
                    .ToArray();

                if (datos.Length == 0)
                {
                    MessageBox.Show("Por favor, ingresa un dataset válido en el primer cuadro.");
                    return;
                }

                // Limpiar resultados anteriores
                txt2.Clear();
                txt3.Clear();

                // 2. Realizar los cálculos estadísticos
                double media = datos.Average();
                txt2.AppendText("• Media Aritmética: " + media.ToString("F2") + "\r\n");

                double mediana;
                int mitad = datos.Length / 2;
                if (datos.Length % 2 == 0)
                    mediana = (datos[mitad - 1] + datos[mitad]) / 2.0;
                else
                    mediana = datos[mitad];
                txt2.AppendText("• Mediana: " + mediana.ToString("F2") + "\r\n");

                var frecuenciasGlobales = datos.GroupBy(v => v).ToList();
                int maxFrecuencia = frecuenciasGlobales.Max(g => g.Count());
                var modas = frecuenciasGlobales.Where(g => g.Count() == maxFrecuencia).Select(g => g.Key).ToList();
                string modaStr = string.Join(", ", modas);
                txt2.AppendText("• Moda: " + modaStr + "\r\n");

                double varianza = 0;
                if (datos.Length > 1)
                {
                    varianza = datos.Sum(val => Math.Pow(val - media, 2)) / (datos.Length - 1);
                }
                txt2.AppendText("• Varianza: " + varianza.ToString("F4") + "\r\n");

                double desvEst = Math.Sqrt(varianza);
                txt2.AppendText("• Desviación Estándar: " + desvEst.ToString("F4") + "\r\n");

                double rango = datos.Max() - datos.Min();
                txt2.AppendText("• Rango: " + rango.ToString("F2") + " (Mín: " + datos.Min() + ", Máx: " + datos.Max() + ")\r\n");

                // 3. Rellenar la explicación teórica en txt3
                txt3.AppendText("Explicación de diferencias:\r\n");
                txt3.AppendText("- Media: Promedio general de los datos.\r\n");
                txt3.AppendText("- Mediana: Valor central que no se ve afectado por valores extremos.\r\n");
                txt3.AppendText("- Varianza y Desv. Estándar: Miden qué tan dispersos están los datos respecto a la media.\r\n");
                txt3.AppendText("- Rango: Distancia matemática entre el valor máximo y el mínimo.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en los datos. Asegúrate de separarlos correctamente por comas. Detalle: " + ex.Message);
            }
        }

        private void txt2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}