namespace Parcial2_3
{
    partial class frmPeriodos
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
            this.bnPeriodos = new System.Windows.Forms.BindingNavigator(this.components);
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblModo = new System.Windows.Forms.Label();
            this.bnPrimero = new System.Windows.Forms.ToolStripButton();
            this.bnAnterior = new System.Windows.Forms.ToolStripButton();
            this.bnPosicion = new System.Windows.Forms.ToolStripTextBox();
            this.bnContador = new System.Windows.Forms.ToolStripLabel();
            this.bnSiguiente = new System.Windows.Forms.ToolStripButton();
            this.bnUltimo = new System.Windows.Forms.ToolStripButton();
            this.bnSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.bnAgregar = new System.Windows.Forms.ToolStripButton();
            this.bnEliminar = new System.Windows.Forms.ToolStripButton();
            this.bnSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.bnGuardar = new System.Windows.Forms.ToolStripButton();
            this.pnlFicha = new System.Windows.Forms.Panel();
            this.lblId = new System.Windows.Forms.Label();
            this.lblIdValor = new System.Windows.Forms.Label();
            this.lblPeriodo = new System.Windows.Forms.Label();
            this.txtPeriodo = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.ssEstado = new System.Windows.Forms.StatusStrip();
            this.tslRegistro = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslAyuda = new System.Windows.Forms.ToolStripStatusLabel();
            this.epErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.bsPeriodos = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bnPeriodos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPeriodos)).BeginInit();
            this.bnPeriodos.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.pnlFicha.SuspendLayout();
            this.ssEstado.SuspendLayout();
            this.SuspendLayout();

            this.bnPeriodos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bnPrimero,
            this.bnAnterior,
            this.bnPosicion,
            this.bnContador,
            this.bnSiguiente,
            this.bnUltimo,
            this.bnSep1,
            this.bnAgregar,
            this.bnEliminar,
            this.bnSep2,
            this.bnGuardar});
            this.bnPeriodos.AddNewItem = this.bnAgregar;
            this.bnPeriodos.BackColor = System.Drawing.Color.White;
            this.bnPeriodos.BindingSource = this.bsPeriodos;
            this.bnPeriodos.CountItem = this.bnContador;
            this.bnPeriodos.CountItemFormat = "de {0}";
            this.bnPeriodos.DeleteItem = null;
            this.bnPeriodos.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnPeriodos.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bnPeriodos.Location = new System.Drawing.Point(0, 0);
            this.bnPeriodos.MoveFirstItem = this.bnPrimero;
            this.bnPeriodos.MoveLastItem = this.bnUltimo;
            this.bnPeriodos.MoveNextItem = this.bnSiguiente;
            this.bnPeriodos.MovePreviousItem = this.bnAnterior;
            this.bnPeriodos.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.bnPeriodos.PositionItem = this.bnPosicion;
            this.bnPeriodos.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.bnPeriodos.Size = new System.Drawing.Size(560, 30);
            this.bnPeriodos.TabIndex = 0;
            this.bnPeriodos.Name = "bnPeriodos";

            this.pnlEncabezado.Controls.Add(this.lblModo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 30);
            this.pnlEncabezado.Size = new System.Drawing.Size(560, 72);
            this.pnlEncabezado.TabIndex = 1;
            this.pnlEncabezado.Name = "pnlEncabezado";

            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Georgia", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 9);
            this.lblTitulo.Text = "Periodos";
            this.lblTitulo.Name = "lblTitulo";

            this.lblModo.AutoSize = true;
            this.lblModo.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(251)))), ((int)(((byte)(241)))));
            this.lblModo.Location = new System.Drawing.Point(24, 44);
            this.lblModo.Text = "Modo consulta";
            this.lblModo.Name = "lblModo";

            this.bnPrimero.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bnPrimero.Text = "«";
            this.bnPrimero.Name = "bnPrimero";

            this.bnAnterior.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bnAnterior.Text = "‹";
            this.bnAnterior.Name = "bnAnterior";

            this.bnPosicion.AutoSize = false;
            this.bnPosicion.Size = new System.Drawing.Size(36, 23);
            this.bnPosicion.Text = "0";
            this.bnPosicion.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bnPosicion.Name = "bnPosicion";

            this.bnContador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.bnContador.Text = "de {0}";
            this.bnContador.Name = "bnContador";

            this.bnSiguiente.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bnSiguiente.Text = "›";
            this.bnSiguiente.Name = "bnSiguiente";

            this.bnUltimo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bnUltimo.Text = "»";
            this.bnUltimo.Name = "bnUltimo";

            this.bnSep1.Name = "bnSep1";

            this.bnAgregar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bnAgregar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.bnAgregar.Text = "+ Agregar";
            this.bnAgregar.Name = "bnAgregar";

            this.bnEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bnEliminar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.bnEliminar.Text = "Eliminar";
            this.bnEliminar.Name = "bnEliminar";
            this.bnEliminar.Click += new System.EventHandler(this.bnEliminar_Click);

            this.bnSep2.Name = "bnSep2";

            this.bnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bnGuardar.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnGuardar.Text = "Guardar";
            this.bnGuardar.Name = "bnGuardar";
            this.bnGuardar.Click += new System.EventHandler(this.bnGuardar_Click);

            this.pnlFicha.Controls.Add(this.dtpFecha);
            this.pnlFicha.Controls.Add(this.lblFecha);
            this.pnlFicha.Controls.Add(this.txtPeriodo);
            this.pnlFicha.Controls.Add(this.lblPeriodo);
            this.pnlFicha.Controls.Add(this.lblIdValor);
            this.pnlFicha.Controls.Add(this.lblId);
            this.pnlFicha.BackColor = System.Drawing.Color.White;
            this.pnlFicha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFicha.Location = new System.Drawing.Point(20, 118);
            this.pnlFicha.Size = new System.Drawing.Size(520, 150);
            this.pnlFicha.TabIndex = 2;
            this.pnlFicha.Name = "pnlFicha";

            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblId.Location = new System.Drawing.Point(18, 16);
            this.lblId.Text = "Código";
            this.lblId.Name = "lblId";

            this.lblIdValor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.lblIdValor.Location = new System.Drawing.Point(20, 38);
            this.lblIdValor.Size = new System.Drawing.Size(100, 25);
            this.lblIdValor.Text = "";
            this.lblIdValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIdValor.Name = "lblIdValor";

            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriodo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblPeriodo.Location = new System.Drawing.Point(138, 16);
            this.lblPeriodo.Text = "Nombre del período";
            this.lblPeriodo.Name = "lblPeriodo";

            this.txtPeriodo.Location = new System.Drawing.Point(140, 38);
            this.txtPeriodo.Size = new System.Drawing.Size(340, 25);
            this.txtPeriodo.TabIndex = 0;
            this.txtPeriodo.MaxLength = 30;
            this.txtPeriodo.Name = "txtPeriodo";

            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblFecha.Location = new System.Drawing.Point(18, 80);
            this.lblFecha.Text = "Fecha de inicio";
            this.lblFecha.Name = "lblFecha";

            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFecha.Location = new System.Drawing.Point(20, 102);
            this.dtpFecha.Size = new System.Drawing.Size(260, 25);
            this.dtpFecha.TabIndex = 1;
            this.dtpFecha.Name = "dtpFecha";

            this.ssEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslRegistro,
            this.tslAyuda});
            this.ssEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(237)))), ((int)(((byte)(230)))));
            this.ssEstado.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssEstado.Location = new System.Drawing.Point(0, 292);
            this.ssEstado.Size = new System.Drawing.Size(560, 24);
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
            this.ClientSize = new System.Drawing.Size(560, 316);
            this.Controls.Add(this.ssEstado);
            this.Controls.Add(this.pnlFicha);
            this.Controls.Add(this.pnlEncabezado);
            this.Controls.Add(this.bnPeriodos);
            this.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Periodos";
            this.Name = "frmPeriodos";
            this.Load += new System.EventHandler(this.frmPeriodos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bnPeriodos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPeriodos)).EndInit();
            this.bnPeriodos.ResumeLayout(false);
            this.bnPeriodos.PerformLayout();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlFicha.ResumeLayout(false);
            this.pnlFicha.PerformLayout();
            this.ssEstado.ResumeLayout(false);
            this.ssEstado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.BindingNavigator bnPeriodos;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblModo;
        private System.Windows.Forms.ToolStripButton bnPrimero;
        private System.Windows.Forms.ToolStripButton bnAnterior;
        private System.Windows.Forms.ToolStripTextBox bnPosicion;
        private System.Windows.Forms.ToolStripLabel bnContador;
        private System.Windows.Forms.ToolStripButton bnSiguiente;
        private System.Windows.Forms.ToolStripButton bnUltimo;
        private System.Windows.Forms.ToolStripSeparator bnSep1;
        private System.Windows.Forms.ToolStripButton bnAgregar;
        private System.Windows.Forms.ToolStripButton bnEliminar;
        private System.Windows.Forms.ToolStripSeparator bnSep2;
        private System.Windows.Forms.ToolStripButton bnGuardar;
        private System.Windows.Forms.Panel pnlFicha;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblIdValor;
        private System.Windows.Forms.Label lblPeriodo;
        private System.Windows.Forms.TextBox txtPeriodo;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.StatusStrip ssEstado;
        private System.Windows.Forms.ToolStripStatusLabel tslRegistro;
        private System.Windows.Forms.ToolStripStatusLabel tslAyuda;
        private System.Windows.Forms.ErrorProvider epErrores;
        private System.Windows.Forms.BindingSource bsPeriodos;
    }
}
