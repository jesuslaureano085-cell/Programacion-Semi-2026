
namespace ppp
{
    partial class frm
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
            this.cbo1 = new System.Windows.Forms.ComboBox();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.btm1 = new System.Windows.Forms.Button();
            this.cbo2 = new System.Windows.Forms.ComboBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(148, 44);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(48, 26);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "De:";
            // 
            // cbo1
            // 
            this.cbo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo1.FormattingEnabled = true;
            this.cbo1.Items.AddRange(new object[] {
            "Metros",
            "Cm",
            "Pulgadas",
            "Pies",
            "Varas",
            "Yardas",
            "Km ",
            "Millas"});
            this.cbo1.Location = new System.Drawing.Point(202, 44);
            this.cbo1.Name = "cbo1";
            this.cbo1.Size = new System.Drawing.Size(153, 33);
            this.cbo1.TabIndex = 1;
            this.cbo1.SelectedIndexChanged += new System.EventHandler(this.cbo1_SelectedIndexChanged);
            // 
            // txt1
            // 
            this.txt1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt1.Location = new System.Drawing.Point(269, 127);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(176, 34);
            this.txt1.TabIndex = 2;
            // 
            // btm1
            // 
            this.btm1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btm1.Location = new System.Drawing.Point(323, 265);
            this.btm1.Name = "btm1";
            this.btm1.Size = new System.Drawing.Size(150, 58);
            this.btm1.TabIndex = 3;
            this.btm1.Text = "Cálcular";
            this.btm1.UseVisualStyleBackColor = true;
            // 
            // cbo2
            // 
            this.cbo2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo2.FormattingEnabled = true;
            this.cbo2.Location = new System.Drawing.Point(442, 47);
            this.cbo2.Name = "cbo2";
            this.cbo2.Size = new System.Drawing.Size(150, 33);
            this.cbo2.TabIndex = 5;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.Location = new System.Drawing.Point(399, 50);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(37, 26);
            this.lbl2.TabIndex = 4;
            this.lbl2.Text = "A:";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl3.Location = new System.Drawing.Point(489, 122);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(135, 26);
            this.lbl3.TabIndex = 6;
            this.lbl3.Text = "Respuesta:?";
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl4.Location = new System.Drawing.Point(148, 130);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(115, 26);
            this.lbl4.TabIndex = 7;
            this.lbl4.Text = "Cantidad:";
            // 
            // frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.cbo2);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.btm1);
            this.Controls.Add(this.txt1);
            this.Controls.Add(this.cbo1);
            this.Controls.Add(this.lbl1);
            this.Name = "frm";
            this.Text = "fmr";
            this.Load += new System.EventHandler(this.frm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.ComboBox cbo1;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.Button btm1;
        private System.Windows.Forms.ComboBox cbo2;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl4;
    }
}

