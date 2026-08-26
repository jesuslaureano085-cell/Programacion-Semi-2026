
namespace Sistema_de_gestion_del_tiempo_y_demanda
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
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblExpediente = new System.Windows.Forms.Label();
            this.lblEdad = new System.Windows.Forms.Label();
            this.lblGenero = new System.Windows.Forms.Label();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.lblTriage = new System.Windows.Forms.Label();
            this.txt3 = new System.Windows.Forms.TextBox();
            this.cbx2 = new System.Windows.Forms.ComboBox();
            this.cbx1 = new System.Windows.Forms.ComboBox();
            this.btn3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(119, 19);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(604, 26);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Sistema de Gestion del Tiempo y la Demanda primer nivel";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.Location = new System.Drawing.Point(172, 67);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(207, 26);
            this.lbl2.TabIndex = 1;
            this.lbl2.Text = "Datos del paciente:";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(51, 139);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(170, 23);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "Nombre Completo:";
            // 
            // lblExpediente
            // 
            this.lblExpediente.AutoSize = true;
            this.lblExpediente.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpediente.Location = new System.Drawing.Point(51, 180);
            this.lblExpediente.Name = "lblExpediente";
            this.lblExpediente.Size = new System.Drawing.Size(136, 23);
            this.lblExpediente.TabIndex = 3;
            this.lblExpediente.Text = "N. Expediente:";
            // 
            // lblEdad
            // 
            this.lblEdad.AutoSize = true;
            this.lblEdad.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEdad.Location = new System.Drawing.Point(51, 227);
            this.lblEdad.Name = "lblEdad";
            this.lblEdad.Size = new System.Drawing.Size(60, 23);
            this.lblEdad.TabIndex = 4;
            this.lblEdad.Text = "Edad:";
            // 
            // lblGenero
            // 
            this.lblGenero.AutoSize = true;
            this.lblGenero.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenero.Location = new System.Drawing.Point(421, 183);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(81, 23);
            this.lblGenero.TabIndex = 5;
            this.lblGenero.Text = "Genero:";
            // 
            // txt1
            // 
            this.txt1.Location = new System.Drawing.Point(228, 139);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(151, 22);
            this.txt1.TabIndex = 10;
            this.txt1.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txt2
            // 
            this.txt2.Location = new System.Drawing.Point(193, 180);
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(151, 22);
            this.txt2.TabIndex = 11;
            this.txt2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // lblTriage
            // 
            this.lblTriage.AutoSize = true;
            this.lblTriage.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTriage.Location = new System.Drawing.Point(413, 135);
            this.lblTriage.Name = "lblTriage";
            this.lblTriage.Size = new System.Drawing.Size(90, 23);
            this.lblTriage.TabIndex = 18;
            this.lblTriage.Text = "El triage:";
            this.lblTriage.Click += new System.EventHandler(this.label1_Click);
            // 
            // txt3
            // 
            this.txt3.Location = new System.Drawing.Point(138, 227);
            this.txt3.Name = "txt3";
            this.txt3.Size = new System.Drawing.Size(206, 22);
            this.txt3.TabIndex = 23;
            this.txt3.TextChanged += new System.EventHandler(this.txtEdad_TextChanged);
            // 
            // cbx2
            // 
            this.cbx2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx2.FormattingEnabled = true;
            this.cbx2.Items.AddRange(new object[] {
            "Masculino",
            "Femenino",
            "Otro"});
            this.cbx2.Location = new System.Drawing.Point(509, 180);
            this.cbx2.Name = "cbx2";
            this.cbx2.Size = new System.Drawing.Size(205, 24);
            this.cbx2.TabIndex = 24;
            this.cbx2.SelectedIndexChanged += new System.EventHandler(this.cbxGenero_SelectedIndexChanged);
            // 
            // cbx1
            // 
            this.cbx1.FormattingEnabled = true;
            this.cbx1.Items.AddRange(new object[] {
            "Emergencia ",
            "Urgencia",
            "Prioridad",
            "Estable",
            "Menor"});
            this.cbx1.Location = new System.Drawing.Point(509, 137);
            this.cbx1.Name = "cbx1";
            this.cbx1.Size = new System.Drawing.Size(209, 24);
            this.cbx1.TabIndex = 25;
            // 
            // btn3
            // 
            this.btn3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn3.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn3.Location = new System.Drawing.Point(515, 350);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(208, 77);
            this.btn3.TabIndex = 28;
            this.btn3.Text = "Volver A menu Principal";
            this.btn3.UseVisualStyleBackColor = false;
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button2.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(278, 350);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(207, 77);
            this.button2.TabIndex = 27;
            this.button2.Text = "Verificar Expediente ";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(42, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(207, 77);
            this.button1.TabIndex = 26;
            this.button1.Text = "Procesar Registro ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbx1);
            this.Controls.Add(this.cbx2);
            this.Controls.Add(this.txt3);
            this.Controls.Add(this.lblTriage);
            this.Controls.Add(this.txt2);
            this.Controls.Add(this.txt1);
            this.Controls.Add(this.lblGenero);
            this.Controls.Add(this.lblEdad);
            this.Controls.Add(this.lblExpediente);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblExpediente;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.Label lblTriage;
        private System.Windows.Forms.TextBox txt3;
        private System.Windows.Forms.ComboBox cbx2;
        private System.Windows.Forms.ComboBox cbx1;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

