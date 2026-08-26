
namespace Tarifa_de_agua_potable
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCalcularAgua = new System.Windows.Forms.Button();
            this.lblMetros = new System.Windows.Forms.Label();
            this.txtMetros = new System.Windows.Forms.TextBox();
            this.txtTotalAgua = new System.Windows.Forms.TextBox();
            this.lblTotalAgua = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(161, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(525, 23);
            this.label1.TabIndex = 26;
            this.label1.Text = "Tarifa de agua potable (por tramos según metros consumidos)";
            // 
            // btnCalcularAgua
            // 
            this.btnCalcularAgua.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalcularAgua.Location = new System.Drawing.Point(549, 193);
            this.btnCalcularAgua.Name = "btnCalcularAgua";
            this.btnCalcularAgua.Size = new System.Drawing.Size(147, 52);
            this.btnCalcularAgua.TabIndex = 25;
            this.btnCalcularAgua.Text = "Calcular";
            this.btnCalcularAgua.UseVisualStyleBackColor = true;
            this.btnCalcularAgua.Click += new System.EventHandler(this.btnCalcularAgua_Click);
            // 
            // lblMetros
            // 
            this.lblMetros.AutoSize = true;
            this.lblMetros.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetros.Location = new System.Drawing.Point(56, 165);
            this.lblMetros.Name = "lblMetros";
            this.lblMetros.Size = new System.Drawing.Size(245, 23);
            this.lblMetros.TabIndex = 24;
            this.lblMetros.Text = "Metros cúbicos consumidos:";
            this.lblMetros.Click += new System.EventHandler(this.lblMetros_Click);
            // 
            // txtMetros
            // 
            this.txtMetros.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMetros.Location = new System.Drawing.Point(317, 162);
            this.txtMetros.Name = "txtMetros";
            this.txtMetros.Size = new System.Drawing.Size(180, 30);
            this.txtMetros.TabIndex = 23;
            this.txtMetros.TextChanged += new System.EventHandler(this.txtMetros_TextChanged);
            // 
            // txtTotalAgua
            // 
            this.txtTotalAgua.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAgua.Location = new System.Drawing.Point(290, 242);
            this.txtTotalAgua.Name = "txtTotalAgua";
            this.txtTotalAgua.ReadOnly = true;
            this.txtTotalAgua.Size = new System.Drawing.Size(176, 30);
            this.txtTotalAgua.TabIndex = 21;
            this.txtTotalAgua.TextChanged += new System.EventHandler(this.txtTotalAgua_TextChanged);
            // 
            // lblTotalAgua
            // 
            this.lblTotalAgua.AutoSize = true;
            this.lblTotalAgua.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAgua.Location = new System.Drawing.Point(90, 242);
            this.lblTotalAgua.Name = "lblTotalAgua";
            this.lblTotalAgua.Size = new System.Drawing.Size(169, 23);
            this.lblTotalAgua.TabIndex = 19;
            this.lblTotalAgua.Text = "Resultado a pagar:";
            this.lblTotalAgua.Click += new System.EventHandler(this.lblTotalAgua_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCalcularAgua);
            this.Controls.Add(this.lblMetros);
            this.Controls.Add(this.txtMetros);
            this.Controls.Add(this.txtTotalAgua);
            this.Controls.Add(this.lblTotalAgua);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCalcularAgua;
        private System.Windows.Forms.Label lblMetros;
        private System.Windows.Forms.TextBox txtMetros;
        private System.Windows.Forms.TextBox txtTotalAgua;
        private System.Windows.Forms.Label lblTotalAgua;
    }
}

