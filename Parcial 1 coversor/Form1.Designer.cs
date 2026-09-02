namespace Parcial_1_conversor
{
    partial class Parcial_1_Conversor
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblValor = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.lblUnidadOrigen = new System.Windows.Forms.Label();
            this.cmbUnidadOrigen = new System.Windows.Forms.ComboBox();
            this.btnIntercambiar = new System.Windows.Forms.Button();
            this.lblUnidadDestino = new System.Windows.Forms.Label();
            this.cmbUnidadDestino = new System.Windows.Forms.ComboBox();
            this.btnConvertir = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblResultado = new System.Windows.Forms.Label();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(340, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Conversor de Área (Superficie)";

            // lblValor
            this.lblValor.AutoSize = true;
            this.lblValor.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblValor.Location = new System.Drawing.Point(20, 60);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(100, 17);
            this.lblValor.TabIndex = 1;
            this.lblValor.Text = "Valor a convertir:";

            // txtValor
            this.txtValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtValor.Location = new System.Drawing.Point(23, 80);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(430, 25);
            this.txtValor.TabIndex = 2;

            // lblUnidadOrigen
            this.lblUnidadOrigen.AutoSize = true;
            this.lblUnidadOrigen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblUnidadOrigen.Location = new System.Drawing.Point(20, 120);
            this.lblUnidadOrigen.Name = "lblUnidadOrigen";
            this.lblUnidadOrigen.Size = new System.Drawing.Size(110, 17);
            this.lblUnidadOrigen.TabIndex = 3;
            this.lblUnidadOrigen.Text = "Unidad de origen:";

            // cmbUnidadOrigen
            this.cmbUnidadOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnidadOrigen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbUnidadOrigen.FormattingEnabled = true;
            this.cmbUnidadOrigen.Location = new System.Drawing.Point(23, 140);
            this.cmbUnidadOrigen.Name = "cmbUnidadOrigen";
            this.cmbUnidadOrigen.Size = new System.Drawing.Size(190, 25);
            this.cmbUnidadOrigen.TabIndex = 4;

            // btnIntercambiar
            this.btnIntercambiar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnIntercambiar.Location = new System.Drawing.Point(223, 138);
            this.btnIntercambiar.Name = "btnIntercambiar";
            this.btnIntercambiar.Size = new System.Drawing.Size(40, 30);
            this.btnIntercambiar.TabIndex = 5;
            this.btnIntercambiar.Text = "⇄";
            this.btnIntercambiar.UseVisualStyleBackColor = true;
            this.btnIntercambiar.Click += new System.EventHandler(this.btnIntercambiar_Click);

            // lblUnidadDestino
            this.lblUnidadDestino.AutoSize = true;
            this.lblUnidadDestino.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblUnidadDestino.Location = new System.Drawing.Point(273, 120);
            this.lblUnidadDestino.Name = "lblUnidadDestino";
            this.lblUnidadDestino.Size = new System.Drawing.Size(115, 17);
            this.lblUnidadDestino.TabIndex = 6;
            this.lblUnidadDestino.Text = "Unidad de destino:";

            // cmbUnidadDestino
            this.cmbUnidadDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnidadDestino.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbUnidadDestino.FormattingEnabled = true;
            this.cmbUnidadDestino.Location = new System.Drawing.Point(276, 140);
            this.cmbUnidadDestino.Name = "cmbUnidadDestino";
            this.cmbUnidadDestino.Size = new System.Drawing.Size(177, 25);
            this.cmbUnidadDestino.TabIndex = 7;

            // btnConvertir
            this.btnConvertir.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnConvertir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnConvertir.ForeColor = System.Drawing.Color.White;
            this.btnConvertir.Location = new System.Drawing.Point(23, 185);
            this.btnConvertir.Name = "btnConvertir";
            this.btnConvertir.Size = new System.Drawing.Size(200, 35);
            this.btnConvertir.TabIndex = 8;
            this.btnConvertir.Text = "Convertir";
            this.btnConvertir.UseVisualStyleBackColor = false;
            this.btnConvertir.Click += new System.EventHandler(this.btnConvertir_Click);

            // btnLimpiar
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(198, 40, 40);
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(253, 185);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(200, 35);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            // lblResultado
            this.lblResultado.AutoSize = true;
            this.lblResultado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblResultado.Location = new System.Drawing.Point(20, 235);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(75, 19);
            this.lblResultado.TabIndex = 10;
            this.lblResultado.Text = "Resultado:";

            // txtResultado
            this.txtResultado.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.txtResultado.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtResultado.Location = new System.Drawing.Point(23, 257);
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ReadOnly = true;
            this.txtResultado.Size = new System.Drawing.Size(430, 29);
            this.txtResultado.TabIndex = 11;

            // dgvHistorial
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Location = new System.Drawing.Point(23, 305);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersWidth = 25;
            this.dgvHistorial.Size = new System.Drawing.Size(430, 180);
            this.dgvHistorial.TabIndex = 12;

            // Parcial_1_Conversor
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 505);
            this.Controls.Add(this.dgvHistorial);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnConvertir);
            this.Controls.Add(this.cmbUnidadDestino);
            this.Controls.Add(this.lblUnidadDestino);
            this.Controls.Add(this.btnIntercambiar);
            this.Controls.Add(this.cmbUnidadOrigen);
            this.Controls.Add(this.lblUnidadOrigen);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Parcial_1_Conversor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conversor de Área - Parcial 1";
            this.Load += new System.EventHandler(this.Parcial_1_Conversor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label lblUnidadOrigen;
        private System.Windows.Forms.ComboBox cmbUnidadOrigen;
        private System.Windows.Forms.Button btnIntercambiar;
        private System.Windows.Forms.Label lblUnidadDestino;
        private System.Windows.Forms.ComboBox cmbUnidadDestino;
        private System.Windows.Forms.Button btnConvertir;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.DataGridView dgvHistorial;
    }
}
