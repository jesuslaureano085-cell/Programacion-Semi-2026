namespace miPrimerProyectoCsharp
{
    partial class frmMaterias
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtBuscarMaterias = new System.Windows.Forms.TextBox();
            this.grbEdicion = new System.Windows.Forms.GroupBox();
            this.btnModificarMateria = new System.Windows.Forms.Button();
            this.btnEliminarMateria = new System.Windows.Forms.Button();
            this.btnAgregarMateria = new System.Windows.Forms.Button();
            this.grbNavegacionMateria = new System.Windows.Forms.GroupBox();
            this.lblnRegistrosMateria = new System.Windows.Forms.Label();
            this.btnUltimoMateria = new System.Windows.Forms.Button();
            this.btnSiguienteMateria = new System.Windows.Forms.Button();
            this.btnAnteriorMateria = new System.Windows.Forms.Button();
            this.btnPrimeroMateria = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grbDatosMateria = new System.Windows.Forms.GroupBox();
            this.idMateria = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBuscarMaterias = new System.Windows.Forms.ComboBox();
            this.grdMaterias = new System.Windows.Forms.DataGridView();
            this.txtUvMateria = new System.Windows.Forms.TextBox();
            this.lblDireccionAlumno = new System.Windows.Forms.Label();
            this.txtNombreMateria = new System.Windows.Forms.TextBox();
            this.lblNombreAlumno = new System.Windows.Forms.Label();
            this.txtCodigoMateria = new System.Windows.Forms.TextBox();
            this.lblCodigoAlumno = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grbEdicion.SuspendLayout();
            this.grbNavegacionMateria.SuspendLayout();
            this.grbDatosMateria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaterias)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBuscarMaterias
            // 
            this.txtBuscarMaterias.Location = new System.Drawing.Point(232, 9);
            this.txtBuscarMaterias.Name = "txtBuscarMaterias";
            this.txtBuscarMaterias.Size = new System.Drawing.Size(157, 20);
            this.txtBuscarMaterias.TabIndex = 14;
            this.txtBuscarMaterias.TextChanged += new System.EventHandler(this.txtBuscarMaterias_TextChanged);
            this.txtBuscarMaterias.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscarMaterias_KeyUp);
            // 
            // grbEdicion
            // 
            this.grbEdicion.Controls.Add(this.btnModificarMateria);
            this.grbEdicion.Controls.Add(this.btnEliminarMateria);
            this.grbEdicion.Controls.Add(this.btnAgregarMateria);
            this.grbEdicion.Location = new System.Drawing.Point(240, 336);
            this.grbEdicion.Name = "grbEdicion";
            this.grbEdicion.Size = new System.Drawing.Size(303, 61);
            this.grbEdicion.TabIndex = 13;
            this.grbEdicion.TabStop = false;
            this.grbEdicion.Text = "Edicion";
            // 
            // btnModificarMateria
            // 
            this.btnModificarMateria.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarMateria.Location = new System.Drawing.Point(93, 19);
            this.btnModificarMateria.Name = "btnModificarMateria";
            this.btnModificarMateria.Size = new System.Drawing.Size(96, 36);
            this.btnModificarMateria.TabIndex = 8;
            this.btnModificarMateria.Text = "Modificar";
            this.btnModificarMateria.UseVisualStyleBackColor = true;
            this.btnModificarMateria.Click += new System.EventHandler(this.btnModificarMateria_Click);
            // 
            // btnEliminarMateria
            // 
            this.btnEliminarMateria.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarMateria.Location = new System.Drawing.Point(195, 20);
            this.btnEliminarMateria.Name = "btnEliminarMateria";
            this.btnEliminarMateria.Size = new System.Drawing.Size(96, 36);
            this.btnEliminarMateria.TabIndex = 9;
            this.btnEliminarMateria.Text = "Eliminar";
            this.btnEliminarMateria.UseVisualStyleBackColor = true;
            this.btnEliminarMateria.Click += new System.EventHandler(this.btnEliminarMateria_Click);
            // 
            // btnAgregarMateria
            // 
            this.btnAgregarMateria.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarMateria.Location = new System.Drawing.Point(3, 19);
            this.btnAgregarMateria.Name = "btnAgregarMateria";
            this.btnAgregarMateria.Size = new System.Drawing.Size(84, 36);
            this.btnAgregarMateria.TabIndex = 7;
            this.btnAgregarMateria.Text = "Nuevo";
            this.btnAgregarMateria.UseVisualStyleBackColor = true;
            this.btnAgregarMateria.Click += new System.EventHandler(this.btnAgregarMateria_Click);
            // 
            // grbNavegacionMateria
            // 
            this.grbNavegacionMateria.Controls.Add(this.lblnRegistrosMateria);
            this.grbNavegacionMateria.Controls.Add(this.btnUltimoMateria);
            this.grbNavegacionMateria.Controls.Add(this.btnSiguienteMateria);
            this.grbNavegacionMateria.Controls.Add(this.btnAnteriorMateria);
            this.grbNavegacionMateria.Controls.Add(this.btnPrimeroMateria);
            this.grbNavegacionMateria.Controls.Add(this.groupBox3);
            this.grbNavegacionMateria.Location = new System.Drawing.Point(20, 336);
            this.grbNavegacionMateria.Name = "grbNavegacionMateria";
            this.grbNavegacionMateria.Size = new System.Drawing.Size(208, 61);
            this.grbNavegacionMateria.TabIndex = 12;
            this.grbNavegacionMateria.TabStop = false;
            this.grbNavegacionMateria.Text = "Navegacion";
            // 
            // lblnRegistrosMateria
            // 
            this.lblnRegistrosMateria.AutoSize = true;
            this.lblnRegistrosMateria.Location = new System.Drawing.Point(83, 37);
            this.lblnRegistrosMateria.Name = "lblnRegistrosMateria";
            this.lblnRegistrosMateria.Size = new System.Drawing.Size(36, 13);
            this.lblnRegistrosMateria.TabIndex = 10;
            this.lblnRegistrosMateria.Text = "x de n";
            // 
            // btnUltimoMateria
            // 
            this.btnUltimoMateria.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUltimoMateria.Location = new System.Drawing.Point(169, 20);
            this.btnUltimoMateria.Name = "btnUltimoMateria";
            this.btnUltimoMateria.Size = new System.Drawing.Size(39, 41);
            this.btnUltimoMateria.TabIndex = 6;
            this.btnUltimoMateria.Text = ">|";
            this.btnUltimoMateria.UseVisualStyleBackColor = true;
            this.btnUltimoMateria.Click += new System.EventHandler(this.btnUltimoMateria_Click);
            // 
            // btnSiguienteMateria
            // 
            this.btnSiguienteMateria.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSiguienteMateria.Location = new System.Drawing.Point(133, 20);
            this.btnSiguienteMateria.Name = "btnSiguienteMateria";
            this.btnSiguienteMateria.Size = new System.Drawing.Size(39, 41);
            this.btnSiguienteMateria.TabIndex = 5;
            this.btnSiguienteMateria.Text = ">";
            this.btnSiguienteMateria.UseVisualStyleBackColor = true;
            this.btnSiguienteMateria.Click += new System.EventHandler(this.btnSiguienteMateria_Click);
            // 
            // btnAnteriorMateria
            // 
            this.btnAnteriorMateria.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnteriorMateria.Location = new System.Drawing.Point(38, 20);
            this.btnAnteriorMateria.Name = "btnAnteriorMateria";
            this.btnAnteriorMateria.Size = new System.Drawing.Size(39, 41);
            this.btnAnteriorMateria.TabIndex = 4;
            this.btnAnteriorMateria.Text = "<";
            this.btnAnteriorMateria.UseVisualStyleBackColor = true;
            this.btnAnteriorMateria.Click += new System.EventHandler(this.btnAnteriorMateria_Click);
            // 
            // btnPrimeroMateria
            // 
            this.btnPrimeroMateria.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrimeroMateria.Location = new System.Drawing.Point(2, 20);
            this.btnPrimeroMateria.Name = "btnPrimeroMateria";
            this.btnPrimeroMateria.Size = new System.Drawing.Size(39, 41);
            this.btnPrimeroMateria.TabIndex = 3;
            this.btnPrimeroMateria.Text = "|<";
            this.btnPrimeroMateria.UseVisualStyleBackColor = true;
            this.btnPrimeroMateria.Click += new System.EventHandler(this.btnPrimeroMateria_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(214, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(237, 61);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // grbDatosMateria
            // 
            this.grbDatosMateria.Controls.Add(this.idMateria);
            this.grbDatosMateria.Controls.Add(this.label1);
            this.grbDatosMateria.Controls.Add(this.txtUvMateria);
            this.grbDatosMateria.Controls.Add(this.lblDireccionAlumno);
            this.grbDatosMateria.Controls.Add(this.txtNombreMateria);
            this.grbDatosMateria.Controls.Add(this.lblNombreAlumno);
            this.grbDatosMateria.Controls.Add(this.txtCodigoMateria);
            this.grbDatosMateria.Controls.Add(this.lblCodigoAlumno);
            this.grbDatosMateria.Enabled = false;
            this.grbDatosMateria.Location = new System.Drawing.Point(20, 28);
            this.grbDatosMateria.Name = "grbDatosMateria";
            this.grbDatosMateria.Size = new System.Drawing.Size(377, 290);
            this.grbDatosMateria.TabIndex = 11;
            this.grbDatosMateria.TabStop = false;
            this.grbDatosMateria.Text = "DATOS";
            // 
            // idMateria
            // 
            this.idMateria.AutoSize = true;
            this.idMateria.Location = new System.Drawing.Point(40, 51);
            this.idMateria.Name = "idMateria";
            this.idMateria.Size = new System.Drawing.Size(0, 13);
            this.idMateria.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "ID:";
            // 
            // cboBuscarMaterias
            // 
            this.cboBuscarMaterias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBuscarMaterias.FormattingEnabled = true;
            this.cboBuscarMaterias.Items.AddRange(new object[] {
            "Codigo",
            "Nombre"});
            this.cboBuscarMaterias.Location = new System.Drawing.Point(123, 8);
            this.cboBuscarMaterias.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboBuscarMaterias.Name = "cboBuscarMaterias";
            this.cboBuscarMaterias.Size = new System.Drawing.Size(92, 21);
            this.cboBuscarMaterias.TabIndex = 15;
            this.cboBuscarMaterias.SelectedIndexChanged += new System.EventHandler(this.cboBuscarMaterias_SelectedIndexChanged);
            // 
            // grdMaterias
            // 
            this.grdMaterias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMaterias.Enabled = false;
            this.grdMaterias.Location = new System.Drawing.Point(10, 34);
            this.grdMaterias.Name = "grdMaterias";
            this.grdMaterias.RowHeadersWidth = 51;
            this.grdMaterias.Size = new System.Drawing.Size(438, 245);
            this.grdMaterias.TabIndex = 8;
            this.grdMaterias.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdMaterias_CellClick);
            this.grdMaterias.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdMaterias_CellContentClick);
            // 
            // txtUvMateria
            // 
            this.txtUvMateria.Location = new System.Drawing.Point(86, 165);
            this.txtUvMateria.Multiline = true;
            this.txtUvMateria.Name = "txtUvMateria";
            this.txtUvMateria.Size = new System.Drawing.Size(223, 31);
            this.txtUvMateria.TabIndex = 5;
            // 
            // lblDireccionAlumno
            // 
            this.lblDireccionAlumno.AutoSize = true;
            this.lblDireccionAlumno.Location = new System.Drawing.Point(11, 168);
            this.lblDireccionAlumno.Name = "lblDireccionAlumno";
            this.lblDireccionAlumno.Size = new System.Drawing.Size(69, 13);
            this.lblDireccionAlumno.TabIndex = 4;
            this.lblDireccionAlumno.Text = "DIRECCION:";
            // 
            // txtNombreMateria
            // 
            this.txtNombreMateria.Location = new System.Drawing.Point(86, 121);
            this.txtNombreMateria.Name = "txtNombreMateria";
            this.txtNombreMateria.Size = new System.Drawing.Size(272, 20);
            this.txtNombreMateria.TabIndex = 3;
            // 
            // lblNombreAlumno
            // 
            this.lblNombreAlumno.AutoSize = true;
            this.lblNombreAlumno.Location = new System.Drawing.Point(11, 124);
            this.lblNombreAlumno.Name = "lblNombreAlumno";
            this.lblNombreAlumno.Size = new System.Drawing.Size(57, 13);
            this.lblNombreAlumno.TabIndex = 2;
            this.lblNombreAlumno.Text = "NOMBRE:";
            // 
            // txtCodigoMateria
            // 
            this.txtCodigoMateria.Location = new System.Drawing.Point(86, 85);
            this.txtCodigoMateria.Name = "txtCodigoMateria";
            this.txtCodigoMateria.Size = new System.Drawing.Size(149, 20);
            this.txtCodigoMateria.TabIndex = 1;
            // 
            // lblCodigoAlumno
            // 
            this.lblCodigoAlumno.AutoSize = true;
            this.lblCodigoAlumno.Location = new System.Drawing.Point(11, 89);
            this.lblCodigoAlumno.Name = "lblCodigoAlumno";
            this.lblCodigoAlumno.Size = new System.Drawing.Size(52, 13);
            this.lblCodigoAlumno.TabIndex = 0;
            this.lblCodigoAlumno.Text = "CODIGO:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdMaterias);
            this.groupBox1.Controls.Add(this.cboBuscarMaterias);
            this.groupBox1.Controls.Add(this.txtBuscarMaterias);
            this.groupBox1.Location = new System.Drawing.Point(435, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 285);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Busqueda de Materias";
            // 
            // frmMaterias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 424);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbEdicion);
            this.Controls.Add(this.grbNavegacionMateria);
            this.Controls.Add(this.grbDatosMateria);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmMaterias";
            this.Text = "frmMaterias";
            this.Load += new System.EventHandler(this.frmMaterias_Load);
            this.grbEdicion.ResumeLayout(false);
            this.grbNavegacionMateria.ResumeLayout(false);
            this.grbNavegacionMateria.PerformLayout();
            this.grbDatosMateria.ResumeLayout(false);
            this.grbDatosMateria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaterias)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtBuscarMaterias;
        private System.Windows.Forms.GroupBox grbEdicion;
        private System.Windows.Forms.Button btnModificarMateria;
        private System.Windows.Forms.Button btnEliminarMateria;
        private System.Windows.Forms.Button btnAgregarMateria;
        private System.Windows.Forms.GroupBox grbNavegacionMateria;
        private System.Windows.Forms.Label lblnRegistrosMateria;
        private System.Windows.Forms.Button btnUltimoMateria;
        private System.Windows.Forms.Button btnSiguienteMateria;
        private System.Windows.Forms.Button btnAnteriorMateria;
        private System.Windows.Forms.Button btnPrimeroMateria;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox grbDatosMateria;
        private System.Windows.Forms.DataGridView grdMaterias;
        private System.Windows.Forms.TextBox txtUvMateria;
        private System.Windows.Forms.Label lblDireccionAlumno;
        private System.Windows.Forms.TextBox txtNombreMateria;
        private System.Windows.Forms.Label lblNombreAlumno;
        private System.Windows.Forms.TextBox txtCodigoMateria;
        private System.Windows.Forms.Label lblCodigoAlumno;
        private System.Windows.Forms.ComboBox cboBuscarMaterias;
        private System.Windows.Forms.Label idMateria;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}