namespace Parcial2_3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tsAcciones = new System.Windows.Forms.ToolStrip();
            this.tsbPrimero = new System.Windows.Forms.ToolStripButton();
            this.tsbAnterior = new System.Windows.Forms.ToolStripButton();
            this.tsbSiguiente = new System.Windows.Forms.ToolStripButton();
            this.tsbUltimo = new System.Windows.Forms.ToolStripButton();
            this.tssSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAgregar = new System.Windows.Forms.ToolStripButton();
            this.tsbModificar = new System.Windows.Forms.ToolStripButton();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            this.tssSeparador2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGuardar = new System.Windows.Forms.ToolStripButton();
            this.tsbCancelar = new System.Windows.Forms.ToolStripButton();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblModo = new System.Windows.Forms.Label();
            this.lblLista = new System.Windows.Forms.Label();
            this.lstRegistros = new System.Windows.Forms.ListBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.pnlFicha = new System.Windows.Forms.Panel();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblNotasAlumno = new System.Windows.Forms.Label();
            this.ssEstado = new System.Windows.Forms.StatusStrip();
            this.tslRegistro = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslAyuda = new System.Windows.Forms.ToolStripStatusLabel();
            this.epErrores = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.epErrores)).BeginInit();
            this.tsAcciones.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.pnlFicha.SuspendLayout();
            this.ssEstado.SuspendLayout();
            this.SuspendLayout();

            this.tsAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPrimero,
            this.tsbAnterior,
            this.tsbSiguiente,
            this.tsbUltimo,
            this.tssSeparador1,
            this.tsbAgregar,
            this.tsbModificar,
            this.tsbEliminar,
            this.tssSeparador2,
            this.tsbGuardar,
            this.tsbCancelar});
            this.tsAcciones.BackColor = System.Drawing.Color.White;
            this.tsAcciones.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsAcciones.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsAcciones.Location = new System.Drawing.Point(0, 0);
            this.tsAcciones.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.tsAcciones.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsAcciones.Size = new System.Drawing.Size(910, 30);
            this.tsAcciones.TabIndex = 0;
            this.tsAcciones.Name = "tsAcciones";

            this.tsbPrimero.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbPrimero.Text = "« Primero";
            this.tsbPrimero.Name = "tsbPrimero";
            this.tsbPrimero.Click += new System.EventHandler(this.tsbPrimero_Click);

            this.tsbAnterior.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAnterior.Text = "‹ Anterior";
            this.tsbAnterior.Name = "tsbAnterior";
            this.tsbAnterior.Click += new System.EventHandler(this.tsbAnterior_Click);

            this.tsbSiguiente.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSiguiente.Text = "Siguiente ›";
            this.tsbSiguiente.Name = "tsbSiguiente";
            this.tsbSiguiente.Click += new System.EventHandler(this.tsbSiguiente_Click);

            this.tsbUltimo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbUltimo.Text = "Último »";
            this.tsbUltimo.Name = "tsbUltimo";
            this.tsbUltimo.Click += new System.EventHandler(this.tsbUltimo_Click);

            this.tssSeparador1.Name = "tssSeparador1";

            this.tsbAgregar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAgregar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.tsbAgregar.Text = "+ Agregar";
            this.tsbAgregar.Name = "tsbAgregar";
            this.tsbAgregar.Click += new System.EventHandler(this.tsbAgregar_Click);

            this.tsbModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbModificar.Text = "Modificar";
            this.tsbModificar.Name = "tsbModificar";
            this.tsbModificar.Click += new System.EventHandler(this.tsbModificar_Click);

            this.tsbEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbEliminar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.tsbEliminar.Text = "Eliminar";
            this.tsbEliminar.Name = "tsbEliminar";
            this.tsbEliminar.Click += new System.EventHandler(this.tsbEliminar_Click);

            this.tssSeparador2.Name = "tssSeparador2";

            this.tsbGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbGuardar.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbGuardar.Text = "Guardar";
            this.tsbGuardar.Name = "tsbGuardar";
            this.tsbGuardar.Click += new System.EventHandler(this.tsbGuardar_Click);

            this.tsbCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCancelar.Text = "Cancelar";
            this.tsbCancelar.Name = "tsbCancelar";
            this.tsbCancelar.Click += new System.EventHandler(this.tsbCancelar_Click);

            this.pnlEncabezado.Controls.Add(this.lblModo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 30);
            this.pnlEncabezado.Size = new System.Drawing.Size(910, 72);
            this.pnlEncabezado.TabIndex = 1;
            this.pnlEncabezado.Name = "pnlEncabezado";

            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Georgia", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 9);
            this.lblTitulo.Text = "Alumnos";
            this.lblTitulo.Name = "lblTitulo";

            this.lblModo.AutoSize = true;
            this.lblModo.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(251)))), ((int)(((byte)(241)))));
            this.lblModo.Location = new System.Drawing.Point(24, 44);
            this.lblModo.Text = "Modo consulta";
            this.lblModo.Name = "lblModo";

            this.lblLista.AutoSize = true;
            this.lblLista.Font = new System.Drawing.Font("Georgia", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLista.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.lblLista.Location = new System.Drawing.Point(18, 116);
            this.lblLista.Text = "Alumnos registrados";
            this.lblLista.Name = "lblLista";

            this.lstRegistros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstRegistros.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lstRegistros.IntegralHeight = false;
            this.lstRegistros.ItemHeight = 18;
            this.lstRegistros.Location = new System.Drawing.Point(20, 176);
            this.lstRegistros.Size = new System.Drawing.Size(280, 294);
            this.lstRegistros.TabIndex = 3;
            this.lstRegistros.Name = "lstRegistros";
            this.lstRegistros.SelectedIndexChanged += new System.EventHandler(this.lstRegistros_SelectedIndexChanged);

            this.txtBuscar.Location = new System.Drawing.Point(20, 142);
            this.txtBuscar.Size = new System.Drawing.Size(280, 25);
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.MaxLength = 65;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);

            this.pnlFicha.Controls.Add(this.lblNotasAlumno);
            this.pnlFicha.Controls.Add(this.txtTelefono);
            this.pnlFicha.Controls.Add(this.lblTelefono);
            this.pnlFicha.Controls.Add(this.txtDireccion);
            this.pnlFicha.Controls.Add(this.lblDireccion);
            this.pnlFicha.Controls.Add(this.txtNombre);
            this.pnlFicha.Controls.Add(this.lblNombre);
            this.pnlFicha.Controls.Add(this.txtCodigo);
            this.pnlFicha.Controls.Add(this.lblCodigo);
            this.pnlFicha.Controls.Add(this.txtId);
            this.pnlFicha.Controls.Add(this.lblId);
            this.pnlFicha.BackColor = System.Drawing.Color.White;
            this.pnlFicha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFicha.Location = new System.Drawing.Point(320, 116);
            this.pnlFicha.Size = new System.Drawing.Size(570, 354);
            this.pnlFicha.TabIndex = 4;
            this.pnlFicha.Name = "pnlFicha";

            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblId.Location = new System.Drawing.Point(18, 18);
            this.lblId.Text = "ID";
            this.lblId.Name = "lblId";

            this.txtId.Location = new System.Drawing.Point(20, 40);
            this.txtId.Size = new System.Drawing.Size(120, 25);
            this.txtId.TabIndex = 0;
            this.txtId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.txtId.ReadOnly = true;
            this.txtId.TabStop = false;
            this.txtId.Name = "txtId";

            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblCodigo.Location = new System.Drawing.Point(158, 18);
            this.lblCodigo.Text = "Código del alumno";
            this.lblCodigo.Name = "lblCodigo";

            this.txtCodigo.Location = new System.Drawing.Point(160, 40);
            this.txtCodigo.Size = new System.Drawing.Size(200, 25);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.MaxLength = 10;
            this.txtCodigo.Name = "txtCodigo";

            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblNombre.Location = new System.Drawing.Point(18, 84);
            this.lblNombre.Text = "Nombre completo";
            this.lblNombre.Name = "lblNombre";

            this.txtNombre.Location = new System.Drawing.Point(20, 106);
            this.txtNombre.Size = new System.Drawing.Size(510, 25);
            this.txtNombre.TabIndex = 2;
            this.txtNombre.MaxLength = 65;
            this.txtNombre.Name = "txtNombre";

            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblDireccion.Location = new System.Drawing.Point(18, 150);
            this.lblDireccion.Text = "Dirección";
            this.lblDireccion.Name = "lblDireccion";

            this.txtDireccion.Location = new System.Drawing.Point(20, 172);
            this.txtDireccion.Size = new System.Drawing.Size(510, 25);
            this.txtDireccion.TabIndex = 3;
            this.txtDireccion.MaxLength = 85;
            this.txtDireccion.Name = "txtDireccion";

            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblTelefono.Location = new System.Drawing.Point(18, 216);
            this.lblTelefono.Text = "Teléfono";
            this.lblTelefono.Name = "lblTelefono";

            this.txtTelefono.Location = new System.Drawing.Point(20, 238);
            this.txtTelefono.Size = new System.Drawing.Size(150, 25);
            this.txtTelefono.TabIndex = 4;
            this.txtTelefono.MaxLength = 9;
            this.txtTelefono.Name = "txtTelefono";

            this.lblNotasAlumno.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotasAlumno.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblNotasAlumno.Location = new System.Drawing.Point(20, 290);
            this.lblNotasAlumno.Size = new System.Drawing.Size(510, 40);
            this.lblNotasAlumno.Text = "";
            this.lblNotasAlumno.Name = "lblNotasAlumno";

            this.ssEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslRegistro,
            this.tslAyuda});
            this.ssEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(237)))), ((int)(((byte)(230)))));
            this.ssEstado.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssEstado.Location = new System.Drawing.Point(0, 488);
            this.ssEstado.Size = new System.Drawing.Size(910, 24);
            this.ssEstado.SizingGrip = false;
            this.ssEstado.TabIndex = 9;
            this.ssEstado.Name = "ssEstado";

            this.tslRegistro.Text = "Sin registros";
            this.tslRegistro.Name = "tslRegistro";

            this.tslAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.tslAyuda.Spring = true;
            this.tslAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tslAyuda.Name = "tslAyuda";

            this.epErrores.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.epErrores.ContainerControl = this;

            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(910, 512);
            this.Controls.Add(this.ssEstado);
            this.Controls.Add(this.pnlFicha);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lstRegistros);
            this.Controls.Add(this.lblLista);
            this.Controls.Add(this.pnlEncabezado);
            this.Controls.Add(this.tsAcciones);
            this.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alumnos";
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.epErrores)).EndInit();
            this.tsAcciones.ResumeLayout(false);
            this.tsAcciones.PerformLayout();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlFicha.ResumeLayout(false);
            this.pnlFicha.PerformLayout();
            this.ssEstado.ResumeLayout(false);
            this.ssEstado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStrip tsAcciones;
        private System.Windows.Forms.ToolStripButton tsbPrimero;
        private System.Windows.Forms.ToolStripButton tsbAnterior;
        private System.Windows.Forms.ToolStripButton tsbSiguiente;
        private System.Windows.Forms.ToolStripButton tsbUltimo;
        private System.Windows.Forms.ToolStripSeparator tssSeparador1;
        private System.Windows.Forms.ToolStripButton tsbAgregar;
        private System.Windows.Forms.ToolStripButton tsbModificar;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.ToolStripSeparator tssSeparador2;
        private System.Windows.Forms.ToolStripButton tsbGuardar;
        private System.Windows.Forms.ToolStripButton tsbCancelar;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblModo;
        private System.Windows.Forms.Label lblLista;
        private System.Windows.Forms.ListBox lstRegistros;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Panel pnlFicha;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblNotasAlumno;
        private System.Windows.Forms.StatusStrip ssEstado;
        private System.Windows.Forms.ToolStripStatusLabel tslRegistro;
        private System.Windows.Forms.ToolStripStatusLabel tslAyuda;
        private System.Windows.Forms.ErrorProvider epErrores;
    }
}
