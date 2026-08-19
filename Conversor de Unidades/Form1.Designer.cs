namespace ConversorUnidades
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblOpciones = new System.Windows.Forms.Label();
            this.lblDe = new System.Windows.Forms.Label();
            this.lblA = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblRespuestas = new System.Windows.Forms.Label();
            this.cdbOpciones = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.cdbDe = new System.Windows.Forms.ComboBox();
            this.cdbA = new System.Windows.Forms.ComboBox();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.txt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblOpciones
            // 
            this.lblOpciones.AutoSize = true;
            this.lblOpciones.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpciones.Location = new System.Drawing.Point(136, 36);
            this.lblOpciones.Name = "lblOpciones";
            this.lblOpciones.Size = new System.Drawing.Size(127, 31);
            this.lblOpciones.TabIndex = 0;
            this.lblOpciones.Text = "Opciones:";
            this.lblOpciones.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblDe
            // 
            this.lblDe.AutoSize = true;
            this.lblDe.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDe.Location = new System.Drawing.Point(97, 108);
            this.lblDe.Name = "lblDe";
            this.lblDe.Size = new System.Drawing.Size(52, 31);
            this.lblDe.TabIndex = 1;
            this.lblDe.Text = "De:";
            this.lblDe.Click += new System.EventHandler(this.lblDe_Click);
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblA.Location = new System.Drawing.Point(383, 108);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(38, 31);
            this.lblA.TabIndex = 2;
            this.lblA.Text = "A:";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.Location = new System.Drawing.Point(97, 217);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(123, 31);
            this.lblCantidad.TabIndex = 3;
            this.lblCantidad.Text = "Cantidad:";
            // 
            // lblRespuestas
            // 
            this.lblRespuestas.AutoSize = true;
            this.lblRespuestas.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRespuestas.Location = new System.Drawing.Point(111, 307);
            this.lblRespuestas.Name = "lblRespuestas";
            this.lblRespuestas.Size = new System.Drawing.Size(155, 31);
            this.lblRespuestas.TabIndex = 4;
            this.lblRespuestas.Text = "Respuesta: ?";
            // 
            // cdbOpciones
            // 
            this.cdbOpciones.FormattingEnabled = true;
            this.cdbOpciones.Items.AddRange(new object[] {
            "Monedas",
            "Masa",
            "Volumen",
            "Longitud",
            "Almacenamiento",
            "Tiempo"});
            this.cdbOpciones.Location = new System.Drawing.Point(284, 41);
            this.cdbOpciones.Name = "cdbOpciones";
            this.cdbOpciones.Size = new System.Drawing.Size(208, 24);
            this.cdbOpciones.TabIndex = 5;
            // 
            // cdbDe
            // 
            this.cdbDe.FormattingEnabled = true;
            this.cdbDe.Location = new System.Drawing.Point(168, 108);
            this.cdbDe.Name = "cdbDe";
            this.cdbDe.Size = new System.Drawing.Size(121, 24);
            this.cdbDe.TabIndex = 6;
            // 
            // cdbA
            // 
            this.cdbA.FormattingEnabled = true;
            this.cdbA.Location = new System.Drawing.Point(446, 113);
            this.cdbA.Name = "cdbA";
            this.cdbA.Size = new System.Drawing.Size(121, 24);
            this.cdbA.TabIndex = 7;
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(466, 182);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(134, 74);
            this.btnCalcular.TabIndex = 8;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            // 
            // txt
            // 
            this.txt.Location = new System.Drawing.Point(218, 224);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(100, 22);
            this.txt.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(375, 365);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 58);
            this.button1.TabIndex = 10;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.cdbA);
            this.Controls.Add(this.cdbDe);
            this.Controls.Add(this.cdbOpciones);
            this.Controls.Add(this.lblRespuestas);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.lblA);
            this.Controls.Add(this.lblDe);
            this.Controls.Add(this.lblOpciones);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOpciones;
        private System.Windows.Forms.Label lblDe;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblRespuestas;
        private System.Windows.Forms.ComboBox cdbOpciones;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ComboBox cdbDe;
        private System.Windows.Forms.ComboBox cdbA;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.TextBox txt;
        private System.Windows.Forms.Button button1;
    }
}

