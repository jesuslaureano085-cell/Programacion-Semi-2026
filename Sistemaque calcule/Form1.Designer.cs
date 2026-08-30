namespace Sistema_estadistico__calcule
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbl1 = new Label();
            txt1 = new TextBox();
            btn1 = new Button();
            lbl2 = new Label();
            txt2 = new TextBox();
            lbl3 = new Label();
            txt3 = new TextBox();
            SuspendLayout();
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl1.Location = new Point(29, -1);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(1092, 31);
            lbl1.TabIndex = 0;
            lbl1.Text = "1.Ingrese el Dataset (Edad ,Tiempo de traslado ala UGB, Hora usando el celular,separados por comas):";
            // 
            // txt1
            // 
            txt1.Location = new Point(89, 45);
            txt1.Multiline = true;
            txt1.Name = "txt1";
            txt1.Size = new Size(722, 33);
            txt1.TabIndex = 1;
            // 
            // btn1
            // 
            btn1.BackColor = Color.FromArgb(255, 192, 128);
            btn1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn1.ForeColor = Color.Red;
            btn1.Location = new Point(131, 95);
            btn1.Name = "btn1";
            btn1.Size = new Size(620, 59);
            btn1.TabIndex = 2;
            btn1.Text = "Procesar dataset";
            btn1.UseVisualStyleBackColor = false;
            btn1.Click += btn1_Click;
            // 
            // lbl2
            // 
            lbl2.AutoSize = true;
            lbl2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl2.Location = new Point(29, 157);
            lbl2.Name = "lbl2";
            lbl2.Size = new Size(282, 31);
            lbl2.TabIndex = 3;
            lbl2.Text = "2. Resultado Estadisticos:";
            // 
            // txt2
            // 
            txt2.Location = new Point(29, 191);
            txt2.Multiline = true;
            txt2.Name = "txt2";
            txt2.Size = new Size(782, 261);
            txt2.TabIndex = 4;
            // 
            // lbl3
            // 
            lbl3.AutoSize = true;
            lbl3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl3.Location = new Point(12, 469);
            lbl3.Name = "lbl3";
            lbl3.Size = new Size(380, 31);
            lbl3.TabIndex = 5;
            lbl3.Text = "3.Explicacion teorica (diferencias):";
            // 
            // txt3
            // 
            txt3.Location = new Point(12, 503);
            txt3.Multiline = true;
            txt3.Name = "txt3";
            txt3.Size = new Size(593, 191);
            txt3.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(1121, 719);
            Controls.Add(txt3);
            Controls.Add(lbl3);
            Controls.Add(txt2);
            Controls.Add(lbl2);
            Controls.Add(btn1);
            Controls.Add(txt1);
            Controls.Add(lbl1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl1;
        private TextBox txt1;
        private Button btn1;
        private Label lbl2;
        private TextBox txt2;
        private Label lbl3;
        private TextBox txt3;
    }
}
