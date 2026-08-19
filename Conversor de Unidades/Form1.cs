using System;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ConversorUnidades
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Vinculación automática de eventos para evitar fallos de conexión
            this.Load += Form1_Load;
            this.cdbOpciones.SelectedIndexChanged += cdbOpciones_SelectedIndexChanged;
            this.btnCalcular.Click += btnCalcular_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cdbOpciones.Items.Clear();
            cdbOpciones.Items.AddRange(new string[] {
                "Monedas", "Masa", "Volumen", "Longitud", "Almacenamiento", "Tiempo"
            });
            cdbOpciones.SelectedIndex = 0;
        }

        private void cdbOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            cdbDe.Items.Clear();
            cdbA.Items.Clear();

            if (cdbOpciones.SelectedItem == null) return;

            string seleccion = cdbOpciones.SelectedItem.ToString();
            string[] unidades = new string[0];

            switch (seleccion)
            {
                case "Monedas":
                    unidades = new string[] {
                        "Dólar (USD)", "Euro (EUR)", "Yen (JPY)", "Libra (GBP)",
                        "Dólar Canadiense (CAD)", "Franco Suizo (CHF)", "Dólar Australiano (AUD)",
                        "Peso Mexicano (MXN)", "Quetzal (GTQ)", "Lempira (HNL)"
                    };
                    break;

                case "Masa":
                    unidades = new string[] {
                        "Kilogramos", "Gramos", "Miligramos", "Libras", "Onzas",
                        "Toneladas", "Arrobas", "Quintales", "Stones", "Carates"
                    };
                    break;

                case "Volumen":
                    unidades = new string[] {
                        "Litros", "Mililitros", "Metros cúbicos", "Galones", "Cuartos",
                        "Pintas", "Tazas", "Onzas líquidas", "Cucharadas", "Cucharaditas"
                    };
                    break;

                case "Longitud":
                    unidades = new string[] {
                        "Metros", "Kilómetros", "Centímetros", "Milímetros", "Millas",
                        "Yardas", "Pies", "Pulgadas", "Millas Náuticas", "Micrómetros"
                    };
                    break;

                case "Almacenamiento":
                    unidades = new string[] {
                        "Bytes", "Kilobytes (KB)", "Megabytes (MB)", "Gigabytes (GB)", "Terabytes (TB)",
                        "Petabytes (PB)", "Bits", "Kilobits (Kb)", "Megabits (Mb)", "Gigabits (Gb)"
                    };
                    break;

                case "Tiempo":
                    unidades = new string[] {
                        "Segundos", "Minutos", "Horas", "Días", "Semanas",
                        "Meses", "Años", "Milisegundos", "Décadas", "Siglos"
                    };
                    break;
            }

            cdbDe.Items.AddRange(unidades);
            cdbA.Items.AddRange(unidades);

            if (cdbDe.Items.Count > 0) cdbDe.SelectedIndex = 0;
            if (cdbA.Items.Count > 1) cdbA.SelectedIndex = 1;
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txt.Text, out double cantidad))
            {
                MessageBox.Show("Por favor, ingresa una cantidad numérica válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cdbDe.SelectedItem == null || cdbA.SelectedItem == null)
            {
                MessageBox.Show("Selecciona las unidades de conversión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string categoria = cdbOpciones.SelectedItem.ToString();
            string unidadDe = cdbDe.SelectedItem.ToString();
            string unidadA = cdbA.SelectedItem.ToString();
            double resultado = 0;

            switch (categoria)
            {
                case "Monedas":
                    resultado = ConvertirMonedas(cantidad, unidadDe, unidadA);
                    break;
                case "Masa":
                    resultado = ConvertirMasa(cantidad, unidadDe, unidadA);
                    break;
                case "Volumen":
                    resultado = ConvertirVolumen(cantidad, unidadDe, unidadA);
                    break;
                case "Longitud":
                    resultado = ConvertirLongitud(cantidad, unidadDe, unidadA);
                    break;
                case "Almacenamiento":
                    resultado = ConvertirAlmacenamiento(cantidad, unidadDe, unidadA);
                    break;
                case "Tiempo":
                    resultado = ConvertirTiempo(cantidad, unidadDe, unidadA);
                    break;
            }

            lblRespuestas.Text = $"{cantidad} {unidadDe} = {resultado:N4} {unidadA}";
        }

        #region Métodos de Conversión

        private double ObtenerFactorMoneda(string unidad)
        {
            switch (unidad)
            {
                case "Dólar (USD)": return 1.0;
                case "Euro (EUR)": return 0.92;
                case "Yen (JPY)": return 155.0;
                case "Libra (GBP)": return 0.79;
                case "Dólar Canadiense (CAD)": return 1.36;
                case "Franco Suizo (CHF)": return 0.91;
                case "Dólar Australiano (AUD)": return 1.51;
                case "Peso Mexicano (MXN)": return 17.0;
                case "Quetzal (GTQ)": return 7.75;
                case "Lempira (HNL)": return 24.7;
                default: return 1.0;
            }
        }

        private double ConvertirMonedas(double val, string de, string a)
        {
            double enUSD = val / ObtenerFactorMoneda(de);
            return enUSD * ObtenerFactorMoneda(a);
        }

        private double ObtenerFactorMasa(string unidad)
        {
            switch (unidad)
            {
                case "Kilogramos": return 1.0;
                case "Gramos": return 1000.0;
                case "Miligramos": return 1000000.0;
                case "Libras": return 2.20462;
                case "Onzas": return 35.274;
                case "Toneladas": return 0.001;
                case "Arrobas": return 0.087;
                case "Quintales": return 0.0220462;
                case "Stones": return 0.157473;
                case "Carates": return 5000.0;
                default: return 1.0;
            }
        }

        private double ConvertirMasa(double val, string de, string a)
        {
            double enKg = val / ObtenerFactorMasa(de);
            return enKg * ObtenerFactorMasa(a);
        }

        private double ObtenerFactorVolumen(string unidad)
        {
            switch (unidad)
            {
                case "Litros": return 1.0;
                case "Mililitros": return 1000.0;
                case "Metros cúbicos": return 0.001;
                case "Galones": return 0.264172;
                case "Cuartos": return 1.05669;
                case "Pintas": return 2.11338;
                case "Tazas": return 4.22675;
                case "Onzas líquidas": return 33.814;
                case "Cucharadas": return 67.628;
                case "Cucharaditas": return 202.884;
                default: return 1.0;
            }
        }

        private double ConvertirVolumen(double val, string de, string a)
        {
            double enLitros = val / ObtenerFactorVolumen(de);
            return enLitros * ObtenerFactorVolumen(a);
        }

        private double ObtenerFactorLongitud(string unidad)
        {
            switch (unidad)
            {
                case "Metros": return 1.0;
                case "Kilómetros": return 0.001;
                case "Centímetros": return 100.0;
                case "Milímetros": return 1000.0;
                case "Millas": return 0.000621371;
                case "Yardas": return 1.09361;
                case "Pies": return 3.28084;
                case "Pulgadas": return 39.3701;
                case "Millas Náuticas": return 0.000539957;
                case "Micrómetros": return 1000000.0;
                default: return 1.0;
            }
        }

        private double ConvertirLongitud(double val, string de, string a)
        {
            double enMetros = val / ObtenerFactorLongitud(de);
            return enMetros * ObtenerFactorLongitud(a);
        }

        private double ObtenerFactorAlmacenamiento(string unidad)
        {
            switch (unidad)
            {
                case "Bytes": return 1.0;
                case "Kilobytes (KB)": return 1024.0;
                case "Megabytes (MB)": return Math.Pow(1024, 2);
                case "Gigabytes (GB)": return Math.Pow(1024, 3);
                case "Terabytes (TB)": return Math.Pow(1024, 4);
                case "Petabytes (PB)": return Math.Pow(1024, 5);
                case "Bits": return 0.125;
                case "Kilobits (Kb)": return 128.0;
                case "Megabits (Mb)": return 128.0 * 1024;
                case "Gigabits (Gb)": return 128.0 * Math.Pow(1024, 2);
                default: return 1.0;
            }
        }

        private double ConvertirAlmacenamiento(double val, string de, string a)
        {
            double enBytes = val * ObtenerFactorAlmacenamiento(de);
            return enBytes / ObtenerFactorAlmacenamiento(a);
        }

        private double ObtenerFactorTiempo(string unidad)
        {
            switch (unidad)
            {
                case "Segundos": return 1.0;
                case "Minutos": return 60.0;
                case "Horas": return 3600.0;
                case "Días": return 86400.0;
                case "Semanas": return 604800.0;
                case "Meses": return 2.628e+6;
                case "Años": return 3.154e+7;
                case "Milisegundos": return 0.001;
                case "Décadas": return 3.154e+8;
                case "Siglos": return 3.154e+9;
                default: return 1.0;
            }
        }

        private double ConvertirTiempo(double val, string de, string a)
        {
            double enSegundos = val * ObtenerFactorTiempo(de);
            return enSegundos / ObtenerFactorTiempo(a);
        }

        #endregion

        private void label1_Click(object sender, EventArgs e) { }
        private void lblDe_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}










