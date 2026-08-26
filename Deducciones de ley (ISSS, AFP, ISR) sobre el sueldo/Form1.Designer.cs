namespace Deducciones_de_ley__ISSS__AFP__ISR__sobre_el_sueldo
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
            this.txtSueldo = new System.Windows.Forms.TextBox();
            this.lblSueldo = new System.Windows.Forms.Label();
            this.btnCalcularPlanilla = new System.Windows.Forms.Button();
            this.lblISSS = new System.Windows.Forms.Label();
            this.lblAFP = new System.Windows.Forms.Label();
            this.lblISR = new System.Windows.Forms.Label();
            this.lblLiquido = new System.Windows.Forms.Label();
            this.txtISSS = new System.Windows.Forms.TextBox();
            this.txtISR = new System.Windows.Forms.TextBox();
            this.txtLiquido = new System.Windows.Forms.TextBox();
            this.txtAFP = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSueldo
            // 
            this.txtSueldo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSueldo.Location = new System.Drawing.Point(174, 122);
            this.txtSueldo.Name = "txtSueldo";
            this.txtSueldo.Size = new System.Drawing.Size(176, 30);
            this.txtSueldo.TabIndex = 0;
            this.txtSueldo.TextChanged += new System.EventHandler(this.txtSueldo_TextChanged);
            // 
            // lblSueldo
            // 
            this.lblSueldo.AutoSize = true;
            this.lblSueldo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSueldo.Location = new System.Drawing.Point(95, 125);
            this.lblSueldo.Name = "lblSueldo";
            this.lblSueldo.Size = new System.Drawing.Size(73, 23);
            this.lblSueldo.TabIndex = 1;
            this.lblSueldo.Text = "Sueldo:";
            // 
            // btnCalcularPlanilla
            // 
            this.btnCalcularPlanilla.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalcularPlanilla.Location = new System.Drawing.Point(479, 218);
            this.btnCalcularPlanilla.Name = "btnCalcularPlanilla";
            this.btnCalcularPlanilla.Size = new System.Drawing.Size(164, 52);
            this.btnCalcularPlanilla.TabIndex = 2;
            this.btnCalcularPlanilla.Text = "CalcularPlanilla";
            this.btnCalcularPlanilla.UseVisualStyleBackColor = true;
            this.btnCalcularPlanilla.Click += new System.EventHandler(this.btnCalcularPlanilla_Click);
            // 
            // lblISSS
            // 
            this.lblISSS.AutoSize = true;
            this.lblISSS.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblISSS.Location = new System.Drawing.Point(111, 161);
            this.lblISSS.Name = "lblISSS";
            this.lblISSS.Size = new System.Drawing.Size(57, 23);
            this.lblISSS.TabIndex = 3;
            this.lblISSS.Text = "ISSS:";
            // 
            // lblAFP
            // 
            this.lblAFP.AutoSize = true;
            this.lblAFP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAFP.Location = new System.Drawing.Point(114, 197);
            this.lblAFP.Name = "lblAFP";
            this.lblAFP.Size = new System.Drawing.Size(54, 23);
            this.lblAFP.TabIndex = 4;
            this.lblAFP.Text = "AFP:";
            this.lblAFP.Click += new System.EventHandler(this.lblAFP_Click);
            // 
            // lblISR
            // 
            this.lblISR.AutoSize = true;
            this.lblISR.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblISR.Location = new System.Drawing.Point(119, 233);
            this.lblISR.Name = "lblISR";
            this.lblISR.Size = new System.Drawing.Size(49, 23);
            this.lblISR.TabIndex = 5;
            this.lblISR.Text = "ISR:";
            this.lblISR.Click += new System.EventHandler(this.lblISR_Click);
            // 
            // lblLiquido
            // 
            this.lblLiquido.AutoSize = true;
            this.lblLiquido.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLiquido.Location = new System.Drawing.Point(88, 269);
            this.lblLiquido.Name = "lblLiquido";
            this.lblLiquido.Size = new System.Drawing.Size(80, 23);
            this.lblLiquido.TabIndex = 6;
            this.lblLiquido.Text = "Liquido:";
            // 
            // txtISSS
            // 
            this.txtISSS.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtISSS.Location = new System.Drawing.Point(174, 158);
            this.txtISSS.Name = "txtISSS";
            this.txtISSS.ReadOnly = true;
            this.txtISSS.Size = new System.Drawing.Size(176, 30);
            this.txtISSS.TabIndex = 7;
            this.txtISSS.TextChanged += new System.EventHandler(this.txtISSS_TextChanged);
            // 
            // txtISR
            // 
            this.txtISR.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtISR.Location = new System.Drawing.Point(174, 230);
            this.txtISR.Name = "txtISR";
            this.txtISR.ReadOnly = true;
            this.txtISR.Size = new System.Drawing.Size(176, 30);
            this.txtISR.TabIndex = 8;
            // 
            // txtLiquido
            // 
            this.txtLiquido.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLiquido.Location = new System.Drawing.Point(174, 266);
            this.txtLiquido.Name = "txtLiquido";
            this.txtLiquido.ReadOnly = true;
            this.txtLiquido.Size = new System.Drawing.Size(176, 30);
            this.txtLiquido.TabIndex = 9;
            // 
            // txtAFP
            // 
            this.txtAFP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAFP.Location = new System.Drawing.Point(174, 194);
            this.txtAFP.Name = "txtAFP";
            this.txtAFP.ReadOnly = true;
            this.txtAFP.Size = new System.Drawing.Size(176, 30);
            this.txtAFP.TabIndex = 11;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(77, 84);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(84, 23);
            this.lblNombre.TabIndex = 13;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(174, 81);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(180, 30);
            this.txtNombre.TabIndex = 12;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(479, 144);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(147, 52);
            this.btnLimpiar.TabIndex = 14;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // dgvResultados
            // 
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Location = new System.Drawing.Point(12, 309);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.RowHeadersWidth = 51;
            this.dgvResultados.RowTemplate.Height = 24;
            this.dgvResultados.Size = new System.Drawing.Size(764, 129);
            this.dgvResultados.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(205, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 23);
            this.label1.TabIndex = 16;
            this.label1.Text = "Deducciones de ley ISSS AFP ISR sobre el sueldo";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvResultados);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtAFP);
            this.Controls.Add(this.txtLiquido);
            this.Controls.Add(this.txtISR);
            this.Controls.Add(this.txtISSS);
            this.Controls.Add(this.lblLiquido);
            this.Controls.Add(this.lblISR);
            this.Controls.Add(this.lblAFP);
            this.Controls.Add(this.lblISSS);
            this.Controls.Add(this.btnCalcularPlanilla);
            this.Controls.Add(this.lblSueldo);
            this.Controls.Add(this.txtSueldo);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSueldo;
        private System.Windows.Forms.Label lblSueldo;
        private System.Windows.Forms.Button btnCalcularPlanilla;
        private System.Windows.Forms.Label lblISSS;
        private System.Windows.Forms.Label lblAFP;
        private System.Windows.Forms.Label lblISR;
        private System.Windows.Forms.Label lblLiquido;
        private System.Windows.Forms.TextBox txtISSS;
        private System.Windows.Forms.TextBox txtISR;
        private System.Windows.Forms.TextBox txtLiquido;
        private System.Windows.Forms.TextBox txtAFP;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.Label label1;
    }
}