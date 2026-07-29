
namespace calculadora_basica
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
            this.txtNum1 = new System.Windows.Forms.TextBox();
            this.btnNum1 = new System.Windows.Forms.Button();
            this.lblNum1 = new System.Windows.Forms.Label();
            this.lblnum2 = new System.Windows.Forms.Label();
            this.txtNum2 = new System.Windows.Forms.TextBox();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.btn2 = new System.Windows.Forms.Button();
            this.rdbsuma = new System.Windows.Forms.RadioButton();
            this.grbopciones = new System.Windows.Forms.GroupBox();
            this.rdbresta = new System.Windows.Forms.RadioButton();
            this.rdbdivi = new System.Windows.Forms.RadioButton();
            this.rdbmulti = new System.Windows.Forms.RadioButton();
            this.rdbexpo = new System.Windows.Forms.RadioButton();
            this.rdbraiz = new System.Windows.Forms.RadioButton();
            this.rdbfactrial = new System.Windows.Forms.RadioButton();
            this.cbonum1 = new System.Windows.Forms.ComboBox();
            this.lbl5 = new System.Windows.Forms.Label();
            this.grbopciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNum1
            // 
            this.txtNum1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNum1.Location = new System.Drawing.Point(273, 90);
            this.txtNum1.Name = "txtNum1";
            this.txtNum1.Size = new System.Drawing.Size(212, 34);
            this.txtNum1.TabIndex = 0;
            // 
            // btnNum1
            // 
            this.btnNum1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum1.Location = new System.Drawing.Point(231, 251);
            this.btnNum1.Name = "btnNum1";
            this.btnNum1.Size = new System.Drawing.Size(213, 80);
            this.btnNum1.TabIndex = 1;
            this.btnNum1.Text = "calcular";
            this.btnNum1.UseVisualStyleBackColor = true;
            this.btnNum1.Click += new System.EventHandler(this.btnNum1_Click);
            // 
            // lblNum1
            // 
            this.lblNum1.AutoSize = true;
            this.lblNum1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNum1.Location = new System.Drawing.Point(182, 97);
            this.lblNum1.Name = "lblNum1";
            this.lblNum1.Size = new System.Drawing.Size(77, 27);
            this.lblNum1.TabIndex = 2;
            this.lblNum1.Text = "Num1:";
            // 
            // lblnum2
            // 
            this.lblnum2.AutoSize = true;
            this.lblnum2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnum2.Location = new System.Drawing.Point(182, 145);
            this.lblnum2.Name = "lblnum2";
            this.lblnum2.Size = new System.Drawing.Size(77, 27);
            this.lblnum2.TabIndex = 3;
            this.lblnum2.Text = "Num2:";
            // 
            // txtNum2
            // 
            this.txtNum2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNum2.Location = new System.Drawing.Point(273, 142);
            this.txtNum2.Name = "txtNum2";
            this.txtNum2.Size = new System.Drawing.Size(212, 34);
            this.txtNum2.TabIndex = 4;
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl3.Location = new System.Drawing.Point(182, 203);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(122, 27);
            this.lbl3.TabIndex = 5;
            this.lbl3.Text = "Respuesta: ";
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl4.Location = new System.Drawing.Point(297, 203);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(22, 27);
            this.lbl4.TabIndex = 6;
            this.lbl4.Text = "?";
            // 
            // btn2
            // 
            this.btn2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2.Location = new System.Drawing.Point(231, 358);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(213, 80);
            this.btn2.TabIndex = 7;
            this.btn2.Text = "Salir";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // rdbsuma
            // 
            this.rdbsuma.AutoSize = true;
            this.rdbsuma.Checked = true;
            this.rdbsuma.Location = new System.Drawing.Point(6, 31);
            this.rdbsuma.Name = "rdbsuma";
            this.rdbsuma.Size = new System.Drawing.Size(63, 21);
            this.rdbsuma.TabIndex = 8;
            this.rdbsuma.TabStop = true;
            this.rdbsuma.Text = "suma";
            this.rdbsuma.UseVisualStyleBackColor = true;
            // 
            // grbopciones
            // 
            this.grbopciones.Controls.Add(this.rdbfactrial);
            this.grbopciones.Controls.Add(this.rdbexpo);
            this.grbopciones.Controls.Add(this.rdbraiz);
            this.grbopciones.Controls.Add(this.rdbdivi);
            this.grbopciones.Controls.Add(this.rdbmulti);
            this.grbopciones.Controls.Add(this.rdbresta);
            this.grbopciones.Controls.Add(this.rdbsuma);
            this.grbopciones.Location = new System.Drawing.Point(559, 107);
            this.grbopciones.Name = "grbopciones";
            this.grbopciones.Size = new System.Drawing.Size(200, 331);
            this.grbopciones.TabIndex = 9;
            this.grbopciones.TabStop = false;
            this.grbopciones.Text = "Opciones ";
            this.grbopciones.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // rdbresta
            // 
            this.rdbresta.AutoSize = true;
            this.rdbresta.Location = new System.Drawing.Point(6, 67);
            this.rdbresta.Name = "rdbresta";
            this.rdbresta.Size = new System.Drawing.Size(61, 21);
            this.rdbresta.TabIndex = 9;
            this.rdbresta.Text = "resta";
            this.rdbresta.UseVisualStyleBackColor = true;
            // 
            // rdbdivi
            // 
            this.rdbdivi.AutoSize = true;
            this.rdbdivi.Location = new System.Drawing.Point(6, 132);
            this.rdbdivi.Name = "rdbdivi";
            this.rdbdivi.Size = new System.Drawing.Size(76, 21);
            this.rdbdivi.TabIndex = 11;
            this.rdbdivi.Text = "division";
            this.rdbdivi.UseVisualStyleBackColor = true;
            // 
            // rdbmulti
            // 
            this.rdbmulti.AutoSize = true;
            this.rdbmulti.Location = new System.Drawing.Point(6, 96);
            this.rdbmulti.Name = "rdbmulti";
            this.rdbmulti.Size = new System.Drawing.Size(113, 21);
            this.rdbmulti.TabIndex = 10;
            this.rdbmulti.Text = "multiplicacion";
            this.rdbmulti.UseVisualStyleBackColor = true;
            // 
            // rdbexpo
            // 
            this.rdbexpo.AutoSize = true;
            this.rdbexpo.Location = new System.Drawing.Point(6, 159);
            this.rdbexpo.Name = "rdbexpo";
            this.rdbexpo.Size = new System.Drawing.Size(99, 21);
            this.rdbexpo.TabIndex = 13;
            this.rdbexpo.Text = "exponente ";
            this.rdbexpo.UseVisualStyleBackColor = true;
            // 
            // rdbraiz
            // 
            this.rdbraiz.AutoSize = true;
            this.rdbraiz.Location = new System.Drawing.Point(6, 186);
            this.rdbraiz.Name = "rdbraiz";
            this.rdbraiz.Size = new System.Drawing.Size(52, 21);
            this.rdbraiz.TabIndex = 12;
            this.rdbraiz.Text = "raiz";
            this.rdbraiz.UseVisualStyleBackColor = true;
            // 
            // rdbfactrial
            // 
            this.rdbfactrial.AutoSize = true;
            this.rdbfactrial.Location = new System.Drawing.Point(6, 213);
            this.rdbfactrial.Name = "rdbfactrial";
            this.rdbfactrial.Size = new System.Drawing.Size(79, 21);
            this.rdbfactrial.TabIndex = 14;
            this.rdbfactrial.Text = "factorial";
            this.rdbfactrial.UseVisualStyleBackColor = true;
            // 
            // cbonum1
            // 
            this.cbonum1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbonum1.FormattingEnabled = true;
            this.cbonum1.Items.AddRange(new object[] {
            "Suma",
            "Resta",
            "Multiplicacion",
            "Division",
            "Exponete",
            "Raiz",
            "Factorial"});
            this.cbonum1.Location = new System.Drawing.Point(426, 21);
            this.cbonum1.Name = "cbonum1";
            this.cbonum1.Size = new System.Drawing.Size(267, 24);
            this.cbonum1.TabIndex = 10;
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl5.Location = new System.Drawing.Point(312, 21);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(108, 27);
            this.lbl5.TabIndex = 11;
            this.lbl5.Text = "Opciones:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 450);
            this.Controls.Add(this.lbl5);
            this.Controls.Add(this.cbonum1);
            this.Controls.Add(this.grbopciones);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.txtNum2);
            this.Controls.Add(this.lblnum2);
            this.Controls.Add(this.lblNum1);
            this.Controls.Add(this.btnNum1);
            this.Controls.Add(this.txtNum1);
            this.Name = "Form1";
            this.Text = "Calculadora Basica";
            this.grbopciones.ResumeLayout(false);
            this.grbopciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNum1;
        private System.Windows.Forms.Button btnNum1;
        private System.Windows.Forms.Label lblNum1;
        private System.Windows.Forms.Label lblnum2;
        private System.Windows.Forms.TextBox txtNum2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.RadioButton rdbsuma;
        private System.Windows.Forms.GroupBox grbopciones;
        private System.Windows.Forms.RadioButton rdbexpo;
        private System.Windows.Forms.RadioButton rdbraiz;
        private System.Windows.Forms.RadioButton rdbdivi;
        private System.Windows.Forms.RadioButton rdbmulti;
        private System.Windows.Forms.RadioButton rdbresta;
        private System.Windows.Forms.RadioButton rdbfactrial;
        private System.Windows.Forms.ComboBox cbonum1;
        private System.Windows.Forms.Label lbl5;
    }
}

