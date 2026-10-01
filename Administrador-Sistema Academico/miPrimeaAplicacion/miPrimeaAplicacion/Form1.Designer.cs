namespace miPrimeaAplicacion {
    partial class Form1 {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent() {
            this.grbDatos = new System.Windows.Forms.GroupBox();
            this.grdAlumnos = new System.Windows.Forms.DataGridView();
            this.txtTelefonoAlumno = new System.Windows.Forms.TextBox();
            this.lblTelefonoAlumno = new System.Windows.Forms.Label();
            this.txtDireccionAlumno = new System.Windows.Forms.TextBox();
            this.lblDireccionAlumno = new System.Windows.Forms.Label();
            this.txtNombreAlumno = new System.Windows.Forms.TextBox();
            this.lblNombreAlumno = new System.Windows.Forms.Label();
            this.txtCodigoAlumno = new System.Windows.Forms.TextBox();
            this.lblCodigoAlumno = new System.Windows.Forms.Label();
            this.grbNavegacion = new System.Windows.Forms.GroupBox();
            this.lblRegistrosAlumnos = new System.Windows.Forms.Label();
            this.btnUltimoAlumno = new System.Windows.Forms.Button();
            this.btnSiguienteAlumno = new System.Windows.Forms.Button();
            this.btnAnteriorAlumno = new System.Windows.Forms.Button();
            this.btnPrimeroAlumno = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grbEdicion = new System.Windows.Forms.GroupBox();
            this.btnModificarALumno = new System.Windows.Forms.Button();
            this.btnAgregarAlumno = new System.Windows.Forms.Button();
            this.btnEliminarAlumno = new System.Windows.Forms.Button();
            this.txtBuscarAlumnos = new System.Windows.Forms.TextBox();
            this.grbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAlumnos)).BeginInit();
            this.grbNavegacion.SuspendLayout();
            this.grbEdicion.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbDatos
            // 
            this.grbDatos.Controls.Add(this.grdAlumnos);
            this.grbDatos.Controls.Add(this.txtTelefonoAlumno);
            this.grbDatos.Controls.Add(this.lblTelefonoAlumno);
            this.grbDatos.Controls.Add(this.txtDireccionAlumno);
            this.grbDatos.Controls.Add(this.lblDireccionAlumno);
            this.grbDatos.Controls.Add(this.txtNombreAlumno);
            this.grbDatos.Controls.Add(this.lblNombreAlumno);
            this.grbDatos.Controls.Add(this.txtCodigoAlumno);
            this.grbDatos.Controls.Add(this.lblCodigoAlumno);
            this.grbDatos.Enabled = false;
            this.grbDatos.Location = new System.Drawing.Point(37, 32);
            this.grbDatos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grbDatos.Name = "grbDatos";
            this.grbDatos.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grbDatos.Size = new System.Drawing.Size(1176, 357);
            this.grbDatos.TabIndex = 0;
            this.grbDatos.TabStop = false;
            this.grbDatos.Text = "DATOS";
            this.grbDatos.Enter += new System.EventHandler(this.grbDatos_Enter);
            // 
            // grdAlumnos
            // 
            this.grdAlumnos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAlumnos.Location = new System.Drawing.Point(532, 23);
            this.grdAlumnos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grdAlumnos.Name = "grdAlumnos";
            this.grdAlumnos.RowHeadersWidth = 51;
            this.grdAlumnos.Size = new System.Drawing.Size(584, 302);
            this.grdAlumnos.TabIndex = 8;
            this.grdAlumnos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAlumnos_CellContentClick);
            // 
            // txtTelefonoAlumno
            // 
            this.txtTelefonoAlumno.Location = new System.Drawing.Point(124, 234);
            this.txtTelefonoAlumno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTelefonoAlumno.Name = "txtTelefonoAlumno";
            this.txtTelefonoAlumno.Size = new System.Drawing.Size(197, 22);
            this.txtTelefonoAlumno.TabIndex = 7;
            this.txtTelefonoAlumno.TextChanged += new System.EventHandler(this.txtTelefonoAlumno_TextChanged);
            // 
            // lblTelefonoAlumno
            // 
            this.lblTelefonoAlumno.AutoSize = true;
            this.lblTelefonoAlumno.Location = new System.Drawing.Point(25, 238);
            this.lblTelefonoAlumno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTelefonoAlumno.Name = "lblTelefonoAlumno";
            this.lblTelefonoAlumno.Size = new System.Drawing.Size(82, 16);
            this.lblTelefonoAlumno.TabIndex = 6;
            this.lblTelefonoAlumno.Text = "TELEFONO:";
            this.lblTelefonoAlumno.Click += new System.EventHandler(this.lblTelefonoAlumno_Click);
            // 
            // txtDireccionAlumno
            // 
            this.txtDireccionAlumno.Location = new System.Drawing.Point(124, 151);
            this.txtDireccionAlumno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDireccionAlumno.Multiline = true;
            this.txtDireccionAlumno.Name = "txtDireccionAlumno";
            this.txtDireccionAlumno.Size = new System.Drawing.Size(361, 58);
            this.txtDireccionAlumno.TabIndex = 5;
            this.txtDireccionAlumno.TextChanged += new System.EventHandler(this.txtDireccionAlumno_TextChanged);
            // 
            // lblDireccionAlumno
            // 
            this.lblDireccionAlumno.AutoSize = true;
            this.lblDireccionAlumno.Location = new System.Drawing.Point(25, 155);
            this.lblDireccionAlumno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDireccionAlumno.Name = "lblDireccionAlumno";
            this.lblDireccionAlumno.Size = new System.Drawing.Size(83, 16);
            this.lblDireccionAlumno.TabIndex = 4;
            this.lblDireccionAlumno.Text = "DIRECCION:";
            this.lblDireccionAlumno.Click += new System.EventHandler(this.lblDireccionAlumno_Click);
            // 
            // txtNombreAlumno
            // 
            this.txtNombreAlumno.Location = new System.Drawing.Point(124, 97);
            this.txtNombreAlumno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNombreAlumno.Name = "txtNombreAlumno";
            this.txtNombreAlumno.Size = new System.Drawing.Size(361, 22);
            this.txtNombreAlumno.TabIndex = 3;
            this.txtNombreAlumno.TextChanged += new System.EventHandler(this.txtNombreAlumno_TextChanged);
            // 
            // lblNombreAlumno
            // 
            this.lblNombreAlumno.AutoSize = true;
            this.lblNombreAlumno.Location = new System.Drawing.Point(25, 101);
            this.lblNombreAlumno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombreAlumno.Name = "lblNombreAlumno";
            this.lblNombreAlumno.Size = new System.Drawing.Size(69, 16);
            this.lblNombreAlumno.TabIndex = 2;
            this.lblNombreAlumno.Text = "NOMBRE:";
            this.lblNombreAlumno.Click += new System.EventHandler(this.lblNombreAlumno_Click);
            // 
            // txtCodigoAlumno
            // 
            this.txtCodigoAlumno.Location = new System.Drawing.Point(124, 53);
            this.txtCodigoAlumno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCodigoAlumno.Name = "txtCodigoAlumno";
            this.txtCodigoAlumno.Size = new System.Drawing.Size(197, 22);
            this.txtCodigoAlumno.TabIndex = 1;
            this.txtCodigoAlumno.TextChanged += new System.EventHandler(this.txtCodigoAlumno_TextChanged);
            // 
            // lblCodigoAlumno
            // 
            this.lblCodigoAlumno.AutoSize = true;
            this.lblCodigoAlumno.Location = new System.Drawing.Point(25, 57);
            this.lblCodigoAlumno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigoAlumno.Name = "lblCodigoAlumno";
            this.lblCodigoAlumno.Size = new System.Drawing.Size(62, 16);
            this.lblCodigoAlumno.TabIndex = 0;
            this.lblCodigoAlumno.Text = "CODIGO:";
            this.lblCodigoAlumno.Click += new System.EventHandler(this.lblCodigoAlumno_Click);
            // 
            // grbNavegacion
            // 
            this.grbNavegacion.Controls.Add(this.lblRegistrosAlumnos);
            this.grbNavegacion.Controls.Add(this.btnUltimoAlumno);
            this.grbNavegacion.Controls.Add(this.btnSiguienteAlumno);
            this.grbNavegacion.Controls.Add(this.btnAnteriorAlumno);
            this.grbNavegacion.Controls.Add(this.btnPrimeroAlumno);
            this.grbNavegacion.Controls.Add(this.groupBox3);
            this.grbNavegacion.Location = new System.Drawing.Point(37, 410);
            this.grbNavegacion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grbNavegacion.Name = "grbNavegacion";
            this.grbNavegacion.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grbNavegacion.Size = new System.Drawing.Size(277, 75);
            this.grbNavegacion.TabIndex = 1;
            this.grbNavegacion.TabStop = false;
            this.grbNavegacion.Text = "Navegacion";
            this.grbNavegacion.Enter += new System.EventHandler(this.grbNavegacion_Enter);
            // 
            // lblRegistrosAlumnos
            // 
            this.lblRegistrosAlumnos.AutoSize = true;
            this.lblRegistrosAlumnos.Location = new System.Drawing.Point(111, 46);
            this.lblRegistrosAlumnos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegistrosAlumnos.Name = "lblRegistrosAlumnos";
            this.lblRegistrosAlumnos.Size = new System.Drawing.Size(42, 16);
            this.lblRegistrosAlumnos.TabIndex = 10;
            this.lblRegistrosAlumnos.Text = "x de n";
            this.lblRegistrosAlumnos.Click += new System.EventHandler(this.lblRegistrosAlumnos_Click);
            // 
            // btnUltimoAlumno
            // 
            this.btnUltimoAlumno.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUltimoAlumno.Location = new System.Drawing.Point(225, 25);
            this.btnUltimoAlumno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUltimoAlumno.Name = "btnUltimoAlumno";
            this.btnUltimoAlumno.Size = new System.Drawing.Size(52, 50);
            this.btnUltimoAlumno.TabIndex = 6;
            this.btnUltimoAlumno.Text = ">|";
            this.btnUltimoAlumno.UseVisualStyleBackColor = true;
            this.btnUltimoAlumno.Click += new System.EventHandler(this.btnUltimoAlumno_Click);
            // 
            // btnSiguienteAlumno
            // 
            this.btnSiguienteAlumno.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSiguienteAlumno.Location = new System.Drawing.Point(177, 25);
            this.btnSiguienteAlumno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSiguienteAlumno.Name = "btnSiguienteAlumno";
            this.btnSiguienteAlumno.Size = new System.Drawing.Size(52, 50);
            this.btnSiguienteAlumno.TabIndex = 5;
            this.btnSiguienteAlumno.Text = ">";
            this.btnSiguienteAlumno.UseVisualStyleBackColor = true;
            this.btnSiguienteAlumno.Click += new System.EventHandler(this.btnSiguienteAlumno_Click);
            // 
            // btnAnteriorAlumno
            // 
            this.btnAnteriorAlumno.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnteriorAlumno.Location = new System.Drawing.Point(51, 25);
            this.btnAnteriorAlumno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAnteriorAlumno.Name = "btnAnteriorAlumno";
            this.btnAnteriorAlumno.Size = new System.Drawing.Size(52, 50);
            this.btnAnteriorAlumno.TabIndex = 4;
            this.btnAnteriorAlumno.Text = "<";
            this.btnAnteriorAlumno.UseVisualStyleBackColor = true;
            this.btnAnteriorAlumno.Click += new System.EventHandler(this.btnAnteriorAlumno_Click);
            // 
            // btnPrimeroAlumno
            // 
            this.btnPrimeroAlumno.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrimeroAlumno.Location = new System.Drawing.Point(3, 25);
            this.btnPrimeroAlumno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrimeroAlumno.Name = "btnPrimeroAlumno";
            this.btnPrimeroAlumno.Size = new System.Drawing.Size(52, 50);
            this.btnPrimeroAlumno.TabIndex = 3;
            this.btnPrimeroAlumno.Text = "|<";
            this.btnPrimeroAlumno.UseVisualStyleBackColor = true;
            this.btnPrimeroAlumno.Click += new System.EventHandler(this.btnPrimeroAlumno_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(285, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(316, 75);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // grbEdicion
            // 
            this.grbEdicion.Controls.Add(this.btnModificarALumno);
            this.grbEdicion.Controls.Add(this.btnEliminarAlumno);
            this.grbEdicion.Controls.Add(this.btnAgregarAlumno);
            this.grbEdicion.Location = new System.Drawing.Point(331, 410);
            this.grbEdicion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grbEdicion.Name = "grbEdicion";
            this.grbEdicion.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grbEdicion.Size = new System.Drawing.Size(404, 75);
            this.grbEdicion.TabIndex = 3;
            this.grbEdicion.TabStop = false;
            this.grbEdicion.Text = "Edicion";
            this.grbEdicion.Enter += new System.EventHandler(this.grbEdicion_Enter);
            // 
            // btnModificarALumno
            // 
            this.btnModificarALumno.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarALumno.Location = new System.Drawing.Point(124, 23);
            this.btnModificarALumno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnModificarALumno.Name = "btnModificarALumno";
            this.btnModificarALumno.Size = new System.Drawing.Size(128, 44);
            this.btnModificarALumno.TabIndex = 8;
            this.btnModificarALumno.Text = "Modificar";
            this.btnModificarALumno.UseVisualStyleBackColor = true;
            this.btnModificarALumno.Click += new System.EventHandler(this.btnModificarALumno_Click);
            // 
            // btnAgregarAlumno
            // 
            this.btnAgregarAlumno.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarAlumno.Location = new System.Drawing.Point(4, 23);
            this.btnAgregarAlumno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAgregarAlumno.Name = "btnAgregarAlumno";
            this.btnAgregarAlumno.Size = new System.Drawing.Size(112, 44);
            this.btnAgregarAlumno.TabIndex = 7;
            this.btnAgregarAlumno.Text = "Agregar";
            this.btnAgregarAlumno.UseVisualStyleBackColor = true;
            this.btnAgregarAlumno.Click += new System.EventHandler(this.btnAgregarAlumno_Click);
            // 
            // btnEliminarAlumno
            // 
            this.btnEliminarAlumno.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarAlumno.Location = new System.Drawing.Point(260, 25);
            this.btnEliminarAlumno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEliminarAlumno.Name = "btnEliminarAlumno";
            this.btnEliminarAlumno.Size = new System.Drawing.Size(128, 44);
            this.btnEliminarAlumno.TabIndex = 9;
            this.btnEliminarAlumno.Text = "Eliminar";
            this.btnEliminarAlumno.UseVisualStyleBackColor = true;
            this.btnEliminarAlumno.Click += new System.EventHandler(this.btnEliminarAlumno_Click);
            // 
            // txtBuscarAlumnos
            // 
            this.txtBuscarAlumnos.Location = new System.Drawing.Point(840, 379);
            this.txtBuscarAlumnos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscarAlumnos.Name = "txtBuscarAlumnos";
            this.txtBuscarAlumnos.Size = new System.Drawing.Size(208, 22);
            this.txtBuscarAlumnos.TabIndex = 10;
            this.txtBuscarAlumnos.TextChanged += new System.EventHandler(this.txtBuscarAlumnos_TextChanged);
            this.txtBuscarAlumnos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscarAlumnos_KeyUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 522);
            this.Controls.Add(this.txtBuscarAlumnos);
            this.Controls.Add(this.grbEdicion);
            this.Controls.Add(this.grbNavegacion);
            this.Controls.Add(this.grbDatos);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Administracion de Alumnos";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grbDatos.ResumeLayout(false);
            this.grbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAlumnos)).EndInit();
            this.grbNavegacion.ResumeLayout(false);
            this.grbNavegacion.PerformLayout();
            this.grbEdicion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbDatos;
        private System.Windows.Forms.GroupBox grbNavegacion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox grbEdicion;
        private System.Windows.Forms.TextBox txtCodigoAlumno;
        private System.Windows.Forms.Label lblCodigoAlumno;
        private System.Windows.Forms.Button btnPrimeroAlumno;
        private System.Windows.Forms.TextBox txtTelefonoAlumno;
        private System.Windows.Forms.Label lblTelefonoAlumno;
        private System.Windows.Forms.TextBox txtDireccionAlumno;
        private System.Windows.Forms.Label lblDireccionAlumno;
        private System.Windows.Forms.TextBox txtNombreAlumno;
        private System.Windows.Forms.Label lblNombreAlumno;
        private System.Windows.Forms.Button btnUltimoAlumno;
        private System.Windows.Forms.Button btnSiguienteAlumno;
        private System.Windows.Forms.Button btnAnteriorAlumno;
        private System.Windows.Forms.Button btnModificarALumno;
        private System.Windows.Forms.Button btnAgregarAlumno;
        private System.Windows.Forms.Label lblRegistrosAlumnos;
        private System.Windows.Forms.Button btnEliminarAlumno;
        private System.Windows.Forms.DataGridView grdAlumnos;
        private System.Windows.Forms.TextBox txtBuscarAlumnos;
    }
}

