namespace Parcial2_3
{
    partial class frmPrincipal
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
            this.mnuPrincipal = new System.Windows.Forms.MenuStrip();
            this.ssPrincipal = new System.Windows.Forms.StatusStrip();
            this.menuArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAplicaciones = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAlumnos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMaterias = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPeriodos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNotas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDocentes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.tslConectado = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslFecha = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuPrincipal.SuspendLayout();
            this.ssPrincipal.SuspendLayout();
            this.SuspendLayout();

            this.mnuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuArchivo,
            this.menuAplicaciones,
            this.menuSesion});
            this.mnuPrincipal.BackColor = System.Drawing.Color.White;
            this.mnuPrincipal.Font = new System.Drawing.Font("Calibri", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mnuPrincipal.Padding = new System.Windows.Forms.Padding(10, 4, 0, 4);
            this.mnuPrincipal.Size = new System.Drawing.Size(1000, 31);
            this.mnuPrincipal.TabIndex = 0;
            this.mnuPrincipal.Name = "mnuPrincipal";

            this.ssPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslConectado,
            this.tslFecha});
            this.ssPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.ssPrincipal.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssPrincipal.Location = new System.Drawing.Point(0, 596);
            this.ssPrincipal.Size = new System.Drawing.Size(1000, 24);
            this.ssPrincipal.SizingGrip = false;
            this.ssPrincipal.TabIndex = 1;
            this.ssPrincipal.Name = "ssPrincipal";

            this.menuArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCerrarSesion,
            this.menuSep1,
            this.menuSalir});
            this.menuArchivo.Text = "&Archivo";
            this.menuArchivo.Name = "menuArchivo";

            this.menuCerrarSesion.Text = "Cerrar sesión";
            this.menuCerrarSesion.Name = "menuCerrarSesion";
            this.menuCerrarSesion.Click += new System.EventHandler(this.menuCerrarSesion_Click);

            this.menuSep1.Name = "menuSep1";

            this.menuSalir.Text = "Salir";
            this.menuSalir.Name = "menuSalir";
            this.menuSalir.Click += new System.EventHandler(this.menuSalir_Click);

            this.menuAplicaciones.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAlumnos,
            this.menuDocentes,
            this.menuMaterias,
            this.menuPeriodos,
            this.menuNotas,
            this.menuSep2,
            this.menuUsuarios});
            this.menuAplicaciones.Text = "A&plicaciones";
            this.menuAplicaciones.Name = "menuAplicaciones";

            this.menuAlumnos.Text = "Alumnos";
            this.menuAlumnos.Name = "menuAlumnos";
            this.menuAlumnos.Click += new System.EventHandler(this.menuFormulario_Click);

            this.menuMaterias.Text = "Materias";
            this.menuMaterias.Name = "menuMaterias";
            this.menuMaterias.Click += new System.EventHandler(this.menuFormulario_Click);

            this.menuPeriodos.Text = "Periodos";
            this.menuPeriodos.Name = "menuPeriodos";
            this.menuPeriodos.Click += new System.EventHandler(this.menuFormulario_Click);

            this.menuNotas.Text = "Notas";
            this.menuNotas.Name = "menuNotas";
            this.menuNotas.Click += new System.EventHandler(this.menuFormulario_Click);

            this.menuUsuarios.Text = "Usuarios del sistema";
            this.menuUsuarios.Name = "menuUsuarios";
            this.menuUsuarios.Click += new System.EventHandler(this.menuFormulario_Click);

            this.menuDocentes.Enabled = false;
            this.menuDocentes.Text = "Docentes";
            this.menuDocentes.ToolTipText = "No incluido: la guía no trae formulario de docentes";
            this.menuDocentes.Name = "menuDocentes";

            this.menuSep2.Name = "menuSep2";

            this.menuSesion.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.menuSesion.Enabled = false;
            this.menuSesion.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuSesion.Text = "";
            this.menuSesion.Name = "menuSesion";

            this.tslConectado.ForeColor = System.Drawing.Color.White;
            this.tslConectado.Text = "Conectado";
            this.tslConectado.Name = "tslConectado";

            this.tslFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(251)))), ((int)(((byte)(241)))));
            this.tslFecha.Spring = true;
            this.tslFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tslFecha.Name = "tslFecha";

            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Controls.Add(this.ssPrincipal);
            this.Controls.Add(this.mnuPrincipal);
            this.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuPrincipal;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Académico";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Name = "frmPrincipal";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.mnuPrincipal.ResumeLayout(false);
            this.mnuPrincipal.PerformLayout();
            this.ssPrincipal.ResumeLayout(false);
            this.ssPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip mnuPrincipal;
        private System.Windows.Forms.StatusStrip ssPrincipal;
        private System.Windows.Forms.ToolStripMenuItem menuArchivo;
        private System.Windows.Forms.ToolStripMenuItem menuCerrarSesion;
        private System.Windows.Forms.ToolStripSeparator menuSep1;
        private System.Windows.Forms.ToolStripMenuItem menuSalir;
        private System.Windows.Forms.ToolStripMenuItem menuAplicaciones;
        private System.Windows.Forms.ToolStripMenuItem menuAlumnos;
        private System.Windows.Forms.ToolStripMenuItem menuMaterias;
        private System.Windows.Forms.ToolStripMenuItem menuPeriodos;
        private System.Windows.Forms.ToolStripMenuItem menuNotas;
        private System.Windows.Forms.ToolStripMenuItem menuUsuarios;
        private System.Windows.Forms.ToolStripMenuItem menuDocentes;
        private System.Windows.Forms.ToolStripSeparator menuSep2;
        private System.Windows.Forms.ToolStripMenuItem menuSesion;
        private System.Windows.Forms.ToolStripStatusLabel tslConectado;
        private System.Windows.Forms.ToolStripStatusLabel tslFecha;
    }
}
