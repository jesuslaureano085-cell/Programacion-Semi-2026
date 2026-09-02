
namespace Tabla_de_meses_acumulados
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblMontoInicial = new System.Windows.Forms.Label();
            this.lblAporteMensual = new System.Windows.Forms.Label();
            this.lblTasaAnual = new System.Windows.Forms.Label();
            this.txtMontoInicial = new System.Windows.Forms.TextBox();
            this.txtAporteMensual = new System.Windows.Forms.TextBox();
            this.txtTasaAnual = new System.Windows.Forms.TextBox();
            this.txtNumMeses = new System.Windows.Forms.TextBox();
            this.lblNumMeses = new System.Windows.Forms.Label();
            this.btnLimpiarMeses = new System.Windows.Forms.Button();
            this.btnGenerarTabla = new System.Windows.Forms.Button();
            this.dgvAcumulado = new System.Windows.Forms.DataGridView();
            this.chrtAcumulado = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcumulado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtAcumulado)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMontoInicial
            // 
            this.lblMontoInicial.AutoSize = true;
            this.lblMontoInicial.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoInicial.Location = new System.Drawing.Point(129, 77);
            this.lblMontoInicial.Name = "lblMontoInicial";
            this.lblMontoInicial.Size = new System.Drawing.Size(125, 22);
            this.lblMontoInicial.TabIndex = 0;
            this.lblMontoInicial.Text = "MontoInicial :";
            // 
            // lblAporteMensual
            // 
            this.lblAporteMensual.AutoSize = true;
            this.lblAporteMensual.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAporteMensual.Location = new System.Drawing.Point(129, 146);
            this.lblAporteMensual.Name = "lblAporteMensual";
            this.lblAporteMensual.Size = new System.Drawing.Size(146, 22);
            this.lblAporteMensual.TabIndex = 1;
            this.lblAporteMensual.Text = "AporteMensual :";
            // 
            // lblTasaAnual
            // 
            this.lblTasaAnual.AutoSize = true;
            this.lblTasaAnual.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTasaAnual.Location = new System.Drawing.Point(129, 222);
            this.lblTasaAnual.Name = "lblTasaAnual";
            this.lblTasaAnual.Size = new System.Drawing.Size(107, 22);
            this.lblTasaAnual.TabIndex = 2;
            this.lblTasaAnual.Text = "TasaAnual :";
            // 
            // txtMontoInicial
            // 
            this.txtMontoInicial.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoInicial.Location = new System.Drawing.Point(266, 77);
            this.txtMontoInicial.Name = "txtMontoInicial";
            this.txtMontoInicial.Size = new System.Drawing.Size(100, 28);
            this.txtMontoInicial.TabIndex = 3;
            // 
            // txtAporteMensual
            // 
            this.txtAporteMensual.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAporteMensual.Location = new System.Drawing.Point(281, 140);
            this.txtAporteMensual.Name = "txtAporteMensual";
            this.txtAporteMensual.Size = new System.Drawing.Size(100, 28);
            this.txtAporteMensual.TabIndex = 4;
            // 
            // txtTasaAnual
            // 
            this.txtTasaAnual.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTasaAnual.Location = new System.Drawing.Point(266, 217);
            this.txtTasaAnual.Name = "txtTasaAnual";
            this.txtTasaAnual.Size = new System.Drawing.Size(100, 28);
            this.txtTasaAnual.TabIndex = 5;
            // 
            // txtNumMeses
            // 
            this.txtNumMeses.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumMeses.Location = new System.Drawing.Point(266, 276);
            this.txtNumMeses.Name = "txtNumMeses";
            this.txtNumMeses.Size = new System.Drawing.Size(100, 28);
            this.txtNumMeses.TabIndex = 6;
            // 
            // lblNumMeses
            // 
            this.lblNumMeses.AutoSize = true;
            this.lblNumMeses.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumMeses.Location = new System.Drawing.Point(129, 276);
            this.lblNumMeses.Name = "lblNumMeses";
            this.lblNumMeses.Size = new System.Drawing.Size(111, 22);
            this.lblNumMeses.TabIndex = 7;
            this.lblNumMeses.Text = "NumMeses :";
            // 
            // btnLimpiarMeses
            // 
            this.btnLimpiarMeses.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarMeses.Location = new System.Drawing.Point(631, 91);
            this.btnLimpiarMeses.Name = "btnLimpiarMeses";
            this.btnLimpiarMeses.Size = new System.Drawing.Size(85, 41);
            this.btnLimpiarMeses.TabIndex = 8;
            this.btnLimpiarMeses.Text = "Limpiar";
            this.btnLimpiarMeses.UseVisualStyleBackColor = true;
            this.btnLimpiarMeses.Click += new System.EventHandler(this.btnLimpiarMeses_Click);
            // 
            // btnGenerarTabla
            // 
            this.btnGenerarTabla.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarTabla.Location = new System.Drawing.Point(537, 91);
            this.btnGenerarTabla.Name = "btnGenerarTabla";
            this.btnGenerarTabla.Size = new System.Drawing.Size(88, 41);
            this.btnGenerarTabla.TabIndex = 9;
            this.btnGenerarTabla.Text = "Generar";
            this.btnGenerarTabla.UseVisualStyleBackColor = true;
            this.btnGenerarTabla.Click += new System.EventHandler(this.btnGenerarTabla_Click);
            // 
            // dgvAcumulado
            // 
            this.dgvAcumulado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAcumulado.Location = new System.Drawing.Point(1, 330);
            this.dgvAcumulado.Name = "dgvAcumulado";
            this.dgvAcumulado.RowHeadersWidth = 51;
            this.dgvAcumulado.RowTemplate.Height = 24;
            this.dgvAcumulado.Size = new System.Drawing.Size(479, 108);
            this.dgvAcumulado.TabIndex = 10;
            // 
            // chrtAcumulado
            // 
            chartArea2.Name = "ChartArea1";
            this.chrtAcumulado.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chrtAcumulado.Legends.Add(legend2);
            this.chrtAcumulado.Location = new System.Drawing.Point(488, 138);
            this.chrtAcumulado.Name = "chrtAcumulado";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chrtAcumulado.Series.Add(series2);
            this.chrtAcumulado.Size = new System.Drawing.Size(300, 300);
            this.chrtAcumulado.TabIndex = 11;
            this.chrtAcumulado.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(262, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 22);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tabla de meses acumulados";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chrtAcumulado);
            this.Controls.Add(this.dgvAcumulado);
            this.Controls.Add(this.btnGenerarTabla);
            this.Controls.Add(this.btnLimpiarMeses);
            this.Controls.Add(this.lblNumMeses);
            this.Controls.Add(this.txtNumMeses);
            this.Controls.Add(this.txtTasaAnual);
            this.Controls.Add(this.txtAporteMensual);
            this.Controls.Add(this.txtMontoInicial);
            this.Controls.Add(this.lblTasaAnual);
            this.Controls.Add(this.lblAporteMensual);
            this.Controls.Add(this.lblMontoInicial);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcumulado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtAcumulado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMontoInicial;
        private System.Windows.Forms.Label lblAporteMensual;
        private System.Windows.Forms.Label lblTasaAnual;
        private System.Windows.Forms.TextBox txtMontoInicial;
        private System.Windows.Forms.TextBox txtAporteMensual;
        private System.Windows.Forms.TextBox txtTasaAnual;
        private System.Windows.Forms.TextBox txtNumMeses;
        private System.Windows.Forms.Label lblNumMeses;
        private System.Windows.Forms.Button btnLimpiarMeses;
        private System.Windows.Forms.Button btnGenerarTabla;
        private System.Windows.Forms.DataGridView dgvAcumulado;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtAcumulado;
        private System.Windows.Forms.Label label1;
    }
}

