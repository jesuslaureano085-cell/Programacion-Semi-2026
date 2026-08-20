
namespace Proyecto_de_programacion
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
            this.btmMenu = new System.Windows.Forms.Button();
            this.btmVerificar = new System.Windows.Forms.Button();
            this.btmREGISTRO = new System.Windows.Forms.Button();
            this.cbx1 = new System.Windows.Forms.ComboBox();
            this.cbx2 = new System.Windows.Forms.ComboBox();
            this.lblTriage = new System.Windows.Forms.Label();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.lblGenero = new System.Windows.Forms.Label();
            this.lblEdad = new System.Windows.Forms.Label();
            this.lblExpediente = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.dtpNacimiento = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btmMenu
            // 
            this.btmMenu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btmMenu.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btmMenu.Location = new System.Drawing.Point(533, 352);
            this.btmMenu.Name = "btmMenu";
            this.btmMenu.Size = new System.Drawing.Size(208, 77);
            this.btmMenu.TabIndex = 43;
            this.btmMenu.Text = "Volver A menu Principal";
            this.btmMenu.UseVisualStyleBackColor = false;
            this.btmMenu.Click += new System.EventHandler(this.btmMenu_Click);
            // 
            // btmVerificar
            // 
            this.btmVerificar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btmVerificar.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btmVerificar.Location = new System.Drawing.Point(296, 352);
            this.btmVerificar.Name = "btmVerificar";
            this.btmVerificar.Size = new System.Drawing.Size(207, 77);
            this.btmVerificar.TabIndex = 42;
            this.btmVerificar.Text = "Verificar Expediente ";
            this.btmVerificar.UseVisualStyleBackColor = false;
            this.btmVerificar.Click += new System.EventHandler(this.btmVerificar_Click);
            // 
            // btmREGISTRO
            // 
            this.btmREGISTRO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btmREGISTRO.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btmREGISTRO.Location = new System.Drawing.Point(60, 352);
            this.btmREGISTRO.Name = "btmREGISTRO";
            this.btmREGISTRO.Size = new System.Drawing.Size(207, 77);
            this.btmREGISTRO.TabIndex = 41;
            this.btmREGISTRO.Text = "Procesar Registro ";
            this.btmREGISTRO.UseVisualStyleBackColor = false;
            this.btmREGISTRO.Click += new System.EventHandler(this.btmREGISTRO_Click);
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
            this.cbx1.Location = new System.Drawing.Point(527, 139);
            this.cbx1.Name = "cbx1";
            this.cbx1.Size = new System.Drawing.Size(209, 24);
            this.cbx1.TabIndex = 40;
            this.cbx1.SelectedIndexChanged += new System.EventHandler(this.cbx1_SelectedIndexChanged);
            // 
            // cbx2
            // 
            this.cbx2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx2.FormattingEnabled = true;
            this.cbx2.Items.AddRange(new object[] {
            "Masculino",
            "Femenino",
            "Otro"});
            this.cbx2.Location = new System.Drawing.Point(527, 182);
            this.cbx2.Name = "cbx2";
            this.cbx2.Size = new System.Drawing.Size(205, 24);
            this.cbx2.TabIndex = 39;
            this.cbx2.SelectedIndexChanged += new System.EventHandler(this.cbx2_SelectedIndexChanged);
            // 
            // lblTriage
            // 
            this.lblTriage.AutoSize = true;
            this.lblTriage.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTriage.Location = new System.Drawing.Point(431, 137);
            this.lblTriage.Name = "lblTriage";
            this.lblTriage.Size = new System.Drawing.Size(90, 23);
            this.lblTriage.TabIndex = 37;
            this.lblTriage.Text = "El triage:";
            // 
            // txt2
            // 
            this.txt2.Location = new System.Drawing.Point(211, 182);
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(151, 22);
            this.txt2.TabIndex = 36;
            this.txt2.TextChanged += new System.EventHandler(this.txt2_TextChanged);
            // 
            // txt1
            // 
            this.txt1.Location = new System.Drawing.Point(246, 141);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(151, 22);
            this.txt1.TabIndex = 35;
            this.txt1.TextChanged += new System.EventHandler(this.txt1_TextChanged);
            // 
            // lblGenero
            // 
            this.lblGenero.AutoSize = true;
            this.lblGenero.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenero.Location = new System.Drawing.Point(439, 185);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(81, 23);
            this.lblGenero.TabIndex = 34;
            this.lblGenero.Text = "Genero:";
            // 
            // lblEdad
            // 
            this.lblEdad.AutoSize = true;
            this.lblEdad.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEdad.Location = new System.Drawing.Point(69, 229);
            this.lblEdad.Name = "lblEdad";
            this.lblEdad.Size = new System.Drawing.Size(60, 23);
            this.lblEdad.TabIndex = 33;
            this.lblEdad.Text = "Edad:";
            // 
            // lblExpediente
            // 
            this.lblExpediente.AutoSize = true;
            this.lblExpediente.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpediente.Location = new System.Drawing.Point(69, 182);
            this.lblExpediente.Name = "lblExpediente";
            this.lblExpediente.Size = new System.Drawing.Size(136, 23);
            this.lblExpediente.TabIndex = 32;
            this.lblExpediente.Text = "N. Expediente:";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(69, 141);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(170, 23);
            this.lblNombre.TabIndex = 31;
            this.lblNombre.Text = "Nombre Completo:";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.Location = new System.Drawing.Point(190, 69);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(207, 26);
            this.lbl2.TabIndex = 30;
            this.lbl2.Text = "Datos del paciente:";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(137, 21);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(604, 26);
            this.lbl1.TabIndex = 29;
            this.lbl1.Text = "Sistema de Gestion del Tiempo y la Demanda primer nivel";
            this.lbl1.Click += new System.EventHandler(this.lbl1_Click);
            // 
            // dtpNacimiento
            // 
            this.dtpNacimiento.Location = new System.Drawing.Point(135, 230);
            this.dtpNacimiento.Name = "dtpNacimiento";
            this.dtpNacimiento.Size = new System.Drawing.Size(262, 22);
            this.dtpNacimiento.TabIndex = 45;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dtpNacimiento);
            this.Controls.Add(this.btmMenu);
            this.Controls.Add(this.btmVerificar);
            this.Controls.Add(this.btmREGISTRO);
            this.Controls.Add(this.cbx1);
            this.Controls.Add(this.cbx2);
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

        private System.Windows.Forms.Button btmMenu;
        private System.Windows.Forms.Button btmVerificar;
        private System.Windows.Forms.Button btmREGISTRO;
        private System.Windows.Forms.ComboBox cbx1;
        private System.Windows.Forms.ComboBox cbx2;
        private System.Windows.Forms.Label lblTriage;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.Label lblExpediente;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.DateTimePicker dtpNacimiento;
    }
}

