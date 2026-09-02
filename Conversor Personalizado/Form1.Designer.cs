
namespace Conversor_Personalizado
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
            this.Load += new System.EventHandler(this.Form1_Load);
            this.label1 = new System.Windows.Forms.Label();
            this.btnConvertirPersonalizado = new System.Windows.Forms.Button();
            this.btnLimpiarPersonalizado = new System.Windows.Forms.Button();
            this.lblValorConversion = new System.Windows.Forms.Label();
            this.txtValorConversion = new System.Windows.Forms.TextBox();
            this.txtCantidadPersonalizado = new System.Windows.Forms.TextBox();
            this.lblUnidadDestino = new System.Windows.Forms.Label();
            this.lblUnidadOrigen = new System.Windows.Forms.Label();
            this.lblCantidadPersonalizado = new System.Windows.Forms.Label();
            this.lblResultadoPersonalizado = new System.Windows.Forms.Label();
            this.cdbUnidadOrigen = new System.Windows.Forms.ComboBox();
            this.cdbUnidadDestino = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(301, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 22);
            this.label1.TabIndex = 23;
            this.label1.Text = "Conversor Personalizado";
            // 
            // btnConvertirPersonalizado
            // 
            this.btnConvertirPersonalizado.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvertirPersonalizado.Location = new System.Drawing.Point(515, 151);
            this.btnConvertirPersonalizado.Name = "btnConvertirPersonalizado";
            this.btnConvertirPersonalizado.Size = new System.Drawing.Size(88, 41);
            this.btnConvertirPersonalizado.TabIndex = 22;
            this.btnConvertirPersonalizado.Text = "Generar";
            this.btnConvertirPersonalizado.UseVisualStyleBackColor = true;
            this.btnConvertirPersonalizado.Click += new System.EventHandler(this.btnConvertirPersonalizado_Click);
            // 
            // btnLimpiarPersonalizado
            // 
            this.btnLimpiarPersonalizado.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarPersonalizado.Location = new System.Drawing.Point(609, 151);
            this.btnLimpiarPersonalizado.Name = "btnLimpiarPersonalizado";
            this.btnLimpiarPersonalizado.Size = new System.Drawing.Size(85, 41);
            this.btnLimpiarPersonalizado.TabIndex = 21;
            this.btnLimpiarPersonalizado.Text = "Limpiar";
            this.btnLimpiarPersonalizado.UseVisualStyleBackColor = true;
            // 
            // lblValorConversion
            // 
            this.lblValorConversion.AutoSize = true;
            this.lblValorConversion.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorConversion.Location = new System.Drawing.Point(107, 336);
            this.lblValorConversion.Name = "lblValorConversion";
            this.lblValorConversion.Size = new System.Drawing.Size(186, 22);
            this.lblValorConversion.TabIndex = 20;
            this.lblValorConversion.Text = "Valor de Conversion :";
            // 
            // txtValorConversion
            // 
            this.txtValorConversion.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorConversion.Location = new System.Drawing.Point(305, 336);
            this.txtValorConversion.Name = "txtValorConversion";
            this.txtValorConversion.Size = new System.Drawing.Size(100, 28);
            this.txtValorConversion.TabIndex = 19;
            // 
            // txtCantidadPersonalizado
            // 
            this.txtCantidadPersonalizado.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadPersonalizado.Location = new System.Drawing.Point(302, 137);
            this.txtCantidadPersonalizado.Name = "txtCantidadPersonalizado";
            this.txtCantidadPersonalizado.Size = new System.Drawing.Size(100, 28);
            this.txtCantidadPersonalizado.TabIndex = 16;
            // 
            // lblUnidadDestino
            // 
            this.lblUnidadDestino.AutoSize = true;
            this.lblUnidadDestino.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidadDestino.Location = new System.Drawing.Point(107, 282);
            this.lblUnidadDestino.Name = "lblUnidadDestino";
            this.lblUnidadDestino.Size = new System.Drawing.Size(169, 22);
            this.lblUnidadDestino.TabIndex = 15;
            this.lblUnidadDestino.Text = "Unidad de Destino :";
            // 
            // lblUnidadOrigen
            // 
            this.lblUnidadOrigen.AutoSize = true;
            this.lblUnidadOrigen.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidadOrigen.Location = new System.Drawing.Point(107, 206);
            this.lblUnidadOrigen.Name = "lblUnidadOrigen";
            this.lblUnidadOrigen.Size = new System.Drawing.Size(164, 22);
            this.lblUnidadOrigen.TabIndex = 14;
            this.lblUnidadOrigen.Text = "Unidad de Origen :";
            // 
            // lblCantidadPersonalizado
            // 
            this.lblCantidadPersonalizado.AutoSize = true;
            this.lblCantidadPersonalizado.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadPersonalizado.Location = new System.Drawing.Point(107, 137);
            this.lblCantidadPersonalizado.Name = "lblCantidadPersonalizado";
            this.lblCantidadPersonalizado.Size = new System.Drawing.Size(189, 22);
            this.lblCantidadPersonalizado.TabIndex = 13;
            this.lblCantidadPersonalizado.Text = "Cantidad a convertir :";
            // 
            // lblResultadoPersonalizado
            // 
            this.lblResultadoPersonalizado.AutoSize = true;
            this.lblResultadoPersonalizado.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultadoPersonalizado.Location = new System.Drawing.Point(533, 277);
            this.lblResultadoPersonalizado.Name = "lblResultadoPersonalizado";
            this.lblResultadoPersonalizado.Size = new System.Drawing.Size(113, 22);
            this.lblResultadoPersonalizado.TabIndex = 24;
            this.lblResultadoPersonalizado.Text = "Personalizar";
            // 
            // cdbUnidadOrigen
            // 
            this.cdbUnidadOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cdbUnidadOrigen.FormattingEnabled = true;
            this.cdbUnidadOrigen.Location = new System.Drawing.Point(289, 202);
            this.cdbUnidadOrigen.Name = "cdbUnidadOrigen";
            this.cdbUnidadOrigen.Size = new System.Drawing.Size(121, 24);
            this.cdbUnidadOrigen.TabIndex = 25;
            this.cdbUnidadOrigen.SelectedIndexChanged += new System.EventHandler(this.cdbUnidadOrigen_SelectedIndexChanged);
            // 
            // cdbUnidadDestino
            // 
            this.cdbUnidadDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cdbUnidadDestino.FormattingEnabled = true;
            this.cdbUnidadDestino.Location = new System.Drawing.Point(289, 282);
            this.cdbUnidadDestino.Name = "cdbUnidadDestino";
            this.cdbUnidadDestino.Size = new System.Drawing.Size(121, 24);
            this.cdbUnidadDestino.TabIndex = 26;
            this.cdbUnidadDestino.SelectedIndexChanged += new System.EventHandler(this.cdbUnidadDestino_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cdbUnidadDestino);
            this.Controls.Add(this.cdbUnidadOrigen);
            this.Controls.Add(this.lblResultadoPersonalizado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConvertirPersonalizado);
            this.Controls.Add(this.btnLimpiarPersonalizado);
            this.Controls.Add(this.lblValorConversion);
            this.Controls.Add(this.txtValorConversion);
            this.Controls.Add(this.txtCantidadPersonalizado);
            this.Controls.Add(this.lblUnidadDestino);
            this.Controls.Add(this.lblUnidadOrigen);
            this.Controls.Add(this.lblCantidadPersonalizado);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConvertirPersonalizado;
        private System.Windows.Forms.Button btnLimpiarPersonalizado;
        private System.Windows.Forms.Label lblValorConversion;
        private System.Windows.Forms.TextBox txtValorConversion;
        private System.Windows.Forms.TextBox txtCantidadPersonalizado;
        private System.Windows.Forms.Label lblUnidadDestino;
        private System.Windows.Forms.Label lblUnidadOrigen;
        private System.Windows.Forms.Label lblCantidadPersonalizado;
        private System.Windows.Forms.Label lblResultadoPersonalizado;
        private System.Windows.Forms.ComboBox cdbUnidadOrigen;
        private System.Windows.Forms.ComboBox cdbUnidadDestino;
    }
}

