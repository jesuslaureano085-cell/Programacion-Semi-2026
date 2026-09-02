
namespace Trabajo_con_LINQ
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
            this.lts1 = new System.Windows.Forms.ListBox();
            this.btm1 = new System.Windows.Forms.Button();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(204, 78);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(45, 17);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Serie:";
            // 
            // lts1
            // 
            this.lts1.FormattingEnabled = true;
            this.lts1.ItemHeight = 16;
            this.lts1.Location = new System.Drawing.Point(147, 163);
            this.lts1.Name = "lts1";
            this.lts1.Size = new System.Drawing.Size(228, 260);
            this.lts1.TabIndex = 1;
            this.lts1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // btm1
            // 
            this.btm1.Location = new System.Drawing.Point(525, 273);
            this.btm1.Name = "btm1";
            this.btm1.Size = new System.Drawing.Size(156, 82);
            this.btm1.TabIndex = 2;
            this.btm1.Text = "calcular";
            this.btm1.UseVisualStyleBackColor = true;
            this.btm1.Click += new System.EventHandler(this.btm1_Click);
            // 
            // txt1
            // 
            this.txt1.Location = new System.Drawing.Point(255, 78);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(241, 22);
            this.txt1.TabIndex = 3;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(144, 131);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(45, 17);
            this.lbl2.TabIndex = 4;
            this.lbl2.Text = "Serie:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.txt1);
            this.Controls.Add(this.btm1);
            this.Controls.Add(this.lts1);
            this.Controls.Add(this.lbl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.ListBox lts1;
        private System.Windows.Forms.Button btm1;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.Label lbl2;
    }
}

