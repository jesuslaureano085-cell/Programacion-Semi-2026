
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
            this.lblDui = new System.Windows.Forms.Label();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblSangre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtExpediente = new System.Windows.Forms.TextBox();
            this.txtDui = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtTriage = new System.Windows.Forms.TextBox();
            this.lblTriage = new System.Windows.Forms.Label();
            this.btmSalir = new System.Windows.Forms.Button();
            this.btmVerificar = new System.Windows.Forms.Button();
            this.txtEdad = new System.Windows.Forms.TextBox();
            this.cbxGenero = new System.Windows.Forms.ComboBox();
            this.cbxSangre = new System.Windows.Forms.ComboBox();
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
            this.lblGenero.Location = new System.Drawing.Point(51, 268);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(81, 23);
            this.lblGenero.TabIndex = 5;
            this.lblGenero.Text = "Genero:";
            // 
            // lblDui
            // 
            this.lblDui.AutoSize = true;
            this.lblDui.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDui.Location = new System.Drawing.Point(395, 139);
            this.lblDui.Name = "lblDui";
            this.lblDui.Size = new System.Drawing.Size(111, 23);
            this.lblDui.TabIndex = 6;
            this.lblDui.Text = "Cedula/Dui:";
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.Location = new System.Drawing.Point(395, 180);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(97, 23);
            this.lblDireccion.TabIndex = 7;
            this.lblDireccion.Text = "Dirección:";
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.Location = new System.Drawing.Point(395, 227);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(90, 23);
            this.lblTelefono.TabIndex = 8;
            this.lblTelefono.Text = "Telefono:";
            this.lblTelefono.Click += new System.EventHandler(this.lblTelefono_Click);
            // 
            // lblSangre
            // 
            this.lblSangre.AutoSize = true;
            this.lblSangre.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSangre.Location = new System.Drawing.Point(395, 268);
            this.lblSangre.Name = "lblSangre";
            this.lblSangre.Size = new System.Drawing.Size(144, 23);
            this.lblSangre.TabIndex = 9;
            this.lblSangre.Text = "Tipo de Sangre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(228, 139);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(151, 22);
            this.txtNombre.TabIndex = 10;
            this.txtNombre.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtExpediente
            // 
            this.txtExpediente.Location = new System.Drawing.Point(193, 180);
            this.txtExpediente.Name = "txtExpediente";
            this.txtExpediente.Size = new System.Drawing.Size(151, 22);
            this.txtExpediente.TabIndex = 11;
            this.txtExpediente.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txtDui
            // 
            this.txtDui.Location = new System.Drawing.Point(512, 141);
            this.txtDui.Name = "txtDui";
            this.txtDui.Size = new System.Drawing.Size(151, 22);
            this.txtDui.TabIndex = 14;
            this.txtDui.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(512, 182);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(151, 22);
            this.txtDireccion.TabIndex = 15;
            this.txtDireccion.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(512, 228);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(151, 22);
            this.txtTelefono.TabIndex = 16;
            this.txtTelefono.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            // 
            // txtTriage
            // 
            this.txtTriage.Location = new System.Drawing.Point(334, 312);
            this.txtTriage.Name = "txtTriage";
            this.txtTriage.Size = new System.Drawing.Size(151, 22);
            this.txtTriage.TabIndex = 19;
            this.txtTriage.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblTriage
            // 
            this.lblTriage.AutoSize = true;
            this.lblTriage.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTriage.Location = new System.Drawing.Point(238, 310);
            this.lblTriage.Name = "lblTriage";
            this.lblTriage.Size = new System.Drawing.Size(90, 23);
            this.lblTriage.TabIndex = 18;
            this.lblTriage.Text = "El triage:";
            this.lblTriage.Click += new System.EventHandler(this.label1_Click);
            // 
            // btmSalir
            // 
            this.btmSalir.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btmSalir.Location = new System.Drawing.Point(230, 371);
            this.btmSalir.Name = "btmSalir";
            this.btmSalir.Size = new System.Drawing.Size(144, 67);
            this.btmSalir.TabIndex = 20;
            this.btmSalir.Text = "Salir";
            this.btmSalir.UseVisualStyleBackColor = true;
            this.btmSalir.Click += new System.EventHandler(this.btmSalir_Click);
            // 
            // btmVerificar
            // 
            this.btmVerificar.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btmVerificar.Location = new System.Drawing.Point(399, 371);
            this.btmVerificar.Name = "btmVerificar";
            this.btmVerificar.Size = new System.Drawing.Size(144, 67);
            this.btmVerificar.TabIndex = 22;
            this.btmVerificar.Text = "Verificar Datos";
            this.btmVerificar.UseVisualStyleBackColor = true;
            this.btmVerificar.Click += new System.EventHandler(this.btmVerificar_Click);
            // 
            // txtEdad
            // 
            this.txtEdad.Location = new System.Drawing.Point(138, 227);
            this.txtEdad.Name = "txtEdad";
            this.txtEdad.Size = new System.Drawing.Size(206, 22);
            this.txtEdad.TabIndex = 23;
            this.txtEdad.TextChanged += new System.EventHandler(this.txtEdad_TextChanged);
            // 
            // cbxGenero
            // 
            this.cbxGenero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGenero.FormattingEnabled = true;
            this.cbxGenero.Items.AddRange(new object[] {
            "Masculino",
            "Femenino",
            "Otro"});
            this.cbxGenero.Location = new System.Drawing.Point(139, 265);
            this.cbxGenero.Name = "cbxGenero";
            this.cbxGenero.Size = new System.Drawing.Size(205, 24);
            this.cbxGenero.TabIndex = 24;
            this.cbxGenero.SelectedIndexChanged += new System.EventHandler(this.cbxGenero_SelectedIndexChanged);
            // 
            // cbxSangre
            // 
            this.cbxSangre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSangre.FormattingEnabled = true;
            this.cbxSangre.Items.AddRange(new object[] {
            "Masculino",
            "Femenino",
            "Otro"});
            this.cbxSangre.Location = new System.Drawing.Point(545, 268);
            this.cbxSangre.Name = "cbxSangre";
            this.cbxSangre.Size = new System.Drawing.Size(205, 24);
            this.cbxSangre.TabIndex = 25;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbxSangre);
            this.Controls.Add(this.cbxGenero);
            this.Controls.Add(this.txtEdad);
            this.Controls.Add(this.btmVerificar);
            this.Controls.Add(this.btmSalir);
            this.Controls.Add(this.txtTriage);
            this.Controls.Add(this.lblTriage);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.txtDui);
            this.Controls.Add(this.txtExpediente);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblSangre);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.lblDireccion);
            this.Controls.Add(this.lblDui);
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
        private System.Windows.Forms.Label lblDui;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblSangre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtExpediente;
        private System.Windows.Forms.TextBox txtDui;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtTriage;
        private System.Windows.Forms.Label lblTriage;
        private System.Windows.Forms.Button btmSalir;
        private System.Windows.Forms.Button btmVerificar;
        private System.Windows.Forms.TextBox txtEdad;
        private System.Windows.Forms.ComboBox cbxGenero;
        private System.Windows.Forms.ComboBox cbxSangre;
    }
}

