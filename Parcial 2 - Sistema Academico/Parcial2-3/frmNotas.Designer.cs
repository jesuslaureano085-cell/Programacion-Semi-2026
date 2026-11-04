namespace Parcial2_3
{
    partial class frmNotas
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
            System.Windows.Forms.DataGridViewCellStyle estiloEncabezado = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle estiloCelda = new System.Windows.Forms.DataGridViewCellStyle();
            this.tsAcciones = new System.Windows.Forms.ToolStrip();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblModo = new System.Windows.Forms.Label();
            this.tslMateria = new System.Windows.Forms.ToolStripLabel();
            this.tscMateria = new System.Windows.Forms.ToolStripComboBox();
            this.tslPeriodo = new System.Windows.Forms.ToolStripLabel();
            this.tscPeriodo = new System.Windows.Forms.ToolStripComboBox();
            this.tssSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGuardar = new System.Windows.Forms.ToolStripButton();
            this.tsbDeshacer = new System.Windows.Forms.ToolStripButton();
            this.dgvNotas = new System.Windows.Forms.DataGridView();
            this.colAlumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLab1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLab2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParcial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFormula = new System.Windows.Forms.Label();
            this.ssEstado = new System.Windows.Forms.StatusStrip();
            this.tslRegistro = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslAyuda = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotas)).BeginInit();
            this.tsAcciones.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.ssEstado.SuspendLayout();
            this.SuspendLayout();

            this.tsAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslMateria,
            this.tscMateria,
            this.tslPeriodo,
            this.tscPeriodo,
            this.tssSep1,
            this.tsbGuardar,
            this.tsbDeshacer});
            this.tsAcciones.BackColor = System.Drawing.Color.White;
            this.tsAcciones.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsAcciones.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsAcciones.Location = new System.Drawing.Point(0, 0);
            this.tsAcciones.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.tsAcciones.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsAcciones.Size = new System.Drawing.Size(910, 30);
            this.tsAcciones.TabIndex = 0;
            this.tsAcciones.Name = "tsAcciones";

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
            this.lblTitulo.Text = "Notas";
            this.lblTitulo.Name = "lblTitulo";

            this.lblModo.AutoSize = true;
            this.lblModo.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(251)))), ((int)(((byte)(241)))));
            this.lblModo.Location = new System.Drawing.Point(24, 44);
            this.lblModo.Text = "Modo consulta";
            this.lblModo.Name = "lblModo";

            this.tslMateria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.tslMateria.Text = "Materia:";
            this.tslMateria.Name = "tslMateria";

            this.tscMateria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscMateria.Size = new System.Drawing.Size(260, 26);
            this.tscMateria.Name = "tscMateria";
            this.tscMateria.SelectedIndexChanged += new System.EventHandler(this.filtro_Changed);

            this.tslPeriodo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.tslPeriodo.Text = "  Período:";
            this.tslPeriodo.Name = "tslPeriodo";

            this.tscPeriodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscPeriodo.Size = new System.Drawing.Size(170, 26);
            this.tscPeriodo.Name = "tscPeriodo";
            this.tscPeriodo.SelectedIndexChanged += new System.EventHandler(this.filtro_Changed);

            this.tssSep1.Name = "tssSep1";

            this.tsbGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbGuardar.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbGuardar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.tsbGuardar.Text = "Guardar notas";
            this.tsbGuardar.Name = "tsbGuardar";
            this.tsbGuardar.Click += new System.EventHandler(this.tsbGuardar_Click);

            this.tsbDeshacer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDeshacer.Text = "Deshacer cambios";
            this.tsbDeshacer.Name = "tsbDeshacer";
            this.tsbDeshacer.Click += new System.EventHandler(this.tsbDeshacer_Click);

            estiloEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(237)))), ((int)(((byte)(230)))));
            estiloEncabezado.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            estiloEncabezado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            estiloEncabezado.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(237)))), ((int)(((byte)(230)))));
            estiloEncabezado.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            estiloCelda.BackColor = System.Drawing.Color.White;
            estiloCelda.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            estiloCelda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            estiloCelda.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(251)))), ((int)(((byte)(241)))));
            estiloCelda.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.dgvNotas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAlumno,
            this.colLab1,
            this.colLab2,
            this.colParcial,
            this.colNF,
            this.colEstado});
            this.dgvNotas.AllowUserToAddRows = false;
            this.dgvNotas.AllowUserToDeleteRows = false;
            this.dgvNotas.AllowUserToResizeRows = false;
            this.dgvNotas.BackgroundColor = System.Drawing.Color.White;
            this.dgvNotas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvNotas.ColumnHeadersDefaultCellStyle = estiloEncabezado;
            this.dgvNotas.ColumnHeadersHeight = 34;
            this.dgvNotas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvNotas.DefaultCellStyle = estiloCelda;
            this.dgvNotas.EnableHeadersVisualStyles = false;
            this.dgvNotas.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.dgvNotas.Location = new System.Drawing.Point(20, 118);
            this.dgvNotas.MultiSelect = false;
            this.dgvNotas.RowHeadersVisible = false;
            this.dgvNotas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvNotas.Size = new System.Drawing.Size(870, 330);
            this.dgvNotas.TabIndex = 2;
            this.dgvNotas.RowTemplate.Height = 30;
            this.dgvNotas.Name = "dgvNotas";
            this.dgvNotas.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvNotas_CellValidating);
            this.dgvNotas.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNotas_CellEndEdit);
            this.dgvNotas.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvNotas_DataError);

            this.colAlumno.DataPropertyName = "Alumno";
            this.colAlumno.HeaderText = "Alumno";
            this.colAlumno.Name = "colAlumno";
            this.colAlumno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAlumno.ReadOnly = true;

            this.colLab1.DataPropertyName = "Lab1";
            this.colLab1.HeaderText = "Laboratorio 1";
            this.colLab1.Name = "colLab1";
            this.colLab1.Width = 120;
            this.colLab1.DefaultCellStyle.Format = "0.00";

            this.colLab2.DataPropertyName = "Lab2";
            this.colLab2.HeaderText = "Laboratorio 2";
            this.colLab2.Name = "colLab2";
            this.colLab2.Width = 120;
            this.colLab2.DefaultCellStyle.Format = "0.00";

            this.colParcial.DataPropertyName = "Parcial";
            this.colParcial.HeaderText = "Parcial";
            this.colParcial.Name = "colParcial";
            this.colParcial.Width = 110;
            this.colParcial.DefaultCellStyle.Format = "0.00";

            this.colNF.DataPropertyName = "NF";
            this.colNF.HeaderText = "Nota final";
            this.colNF.Name = "colNF";
            this.colNF.Width = 110;
            this.colNF.ReadOnly = true;
            this.colNF.DefaultCellStyle.Format = "0.00";
            this.colNF.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNF.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(250)))));

            this.colEstado.DataPropertyName = "Estado";
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.Width = 110;
            this.colEstado.ReadOnly = true;

            this.lblFormula.AutoSize = true;
            this.lblFormula.Font = new System.Drawing.Font("Georgia", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormula.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.lblFormula.Location = new System.Drawing.Point(18, 458);
            this.lblFormula.Text = "Nota final = Lab. 1 × 0.30 + Lab. 2 × 0.30 + Parcial × 0.40";
            this.lblFormula.Name = "lblFormula";

            this.ssEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslRegistro,
            this.tslAyuda});
            this.ssEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(237)))), ((int)(((byte)(230)))));
            this.ssEstado.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssEstado.Location = new System.Drawing.Point(0, 516);
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

            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(910, 540);
            this.Controls.Add(this.ssEstado);
            this.Controls.Add(this.lblFormula);
            this.Controls.Add(this.dgvNotas);
            this.Controls.Add(this.pnlEncabezado);
            this.Controls.Add(this.tsAcciones);
            this.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notas";
            this.Name = "frmNotas";
            this.Load += new System.EventHandler(this.frmNotas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotas)).EndInit();
            this.tsAcciones.ResumeLayout(false);
            this.tsAcciones.PerformLayout();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.ssEstado.ResumeLayout(false);
            this.ssEstado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStrip tsAcciones;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblModo;
        private System.Windows.Forms.ToolStripLabel tslMateria;
        private System.Windows.Forms.ToolStripComboBox tscMateria;
        private System.Windows.Forms.ToolStripLabel tslPeriodo;
        private System.Windows.Forms.ToolStripComboBox tscPeriodo;
        private System.Windows.Forms.ToolStripSeparator tssSep1;
        private System.Windows.Forms.ToolStripButton tsbGuardar;
        private System.Windows.Forms.ToolStripButton tsbDeshacer;
        private System.Windows.Forms.DataGridView dgvNotas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLab1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLab2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParcial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNF;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.Label lblFormula;
        private System.Windows.Forms.StatusStrip ssEstado;
        private System.Windows.Forms.ToolStripStatusLabel tslRegistro;
        private System.Windows.Forms.ToolStripStatusLabel tslAyuda;
    }
}
