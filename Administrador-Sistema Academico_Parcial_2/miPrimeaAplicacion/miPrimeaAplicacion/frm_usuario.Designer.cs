namespace miPrimeaAplicacion
{
    partial class frm_usuario
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
            this.lblIdUsuario = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblClave = new System.Windows.Forms.Label();
            this.lblConfirmar = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtIdUsuario = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.txtConfirmar = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.grdUsuarios = new System.Windows.Forms.DataGridView();
            this.grbNavegacion = new System.Windows.Forms.GroupBox();
            this.btnPrimero = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.lblRegistro = new System.Windows.Forms.Label();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnUltimo = new System.Windows.Forms.Button();
            this.grbEdicion = new System.Windows.Forms.GroupBox();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsuarios)).BeginInit();
            this.grbNavegacion.SuspendLayout();
            this.grbEdicion.SuspendLayout();
            this.SuspendLayout();
            //
            // Etiquetas
            //
            this.lblIdUsuario.AutoSize = true;
            this.lblIdUsuario.Location = new System.Drawing.Point(20, 23);
            this.lblIdUsuario.Name = "lblIdUsuario";
            this.lblIdUsuario.Text = "ID Usuario:";

            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(20, 58);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Text = "Usuario:";

            this.lblClave.AutoSize = true;
            this.lblClave.Location = new System.Drawing.Point(20, 93);
            this.lblClave.Name = "lblClave";
            this.lblClave.Text = "Clave:";

            this.lblConfirmar.AutoSize = true;
            this.lblConfirmar.Location = new System.Drawing.Point(20, 128);
            this.lblConfirmar.Name = "lblConfirmar";
            this.lblConfirmar.Text = "Confirmar clave:";

            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(20, 163);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Text = "Nombre:";

            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Location = new System.Drawing.Point(20, 198);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Text = "Direccion:";

            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(20, 233);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Text = "Telefono:";
            //
            // Cajas de texto
            //
            this.txtIdUsuario.Location = new System.Drawing.Point(140, 20);
            this.txtIdUsuario.Name = "txtIdUsuario";
            this.txtIdUsuario.ReadOnly = true;
            this.txtIdUsuario.Size = new System.Drawing.Size(80, 23);
            this.txtIdUsuario.TabStop = false;

            this.txtUsuario.Location = new System.Drawing.Point(140, 55);
            this.txtUsuario.MaxLength = 16;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(220, 23);
            this.txtUsuario.TabIndex = 0;

            this.txtClave.Location = new System.Drawing.Point(140, 90);
            this.txtClave.MaxLength = 35;
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(220, 23);
            this.txtClave.TabIndex = 1;

            this.txtConfirmar.Location = new System.Drawing.Point(140, 125);
            this.txtConfirmar.MaxLength = 35;
            this.txtConfirmar.Name = "txtConfirmar";
            this.txtConfirmar.PasswordChar = '*';
            this.txtConfirmar.Size = new System.Drawing.Size(220, 23);
            this.txtConfirmar.TabIndex = 2;

            this.txtNombre.Location = new System.Drawing.Point(140, 160);
            this.txtNombre.MaxLength = 65;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(220, 23);
            this.txtNombre.TabIndex = 3;

            this.txtDireccion.Location = new System.Drawing.Point(140, 195);
            this.txtDireccion.MaxLength = 85;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(220, 23);
            this.txtDireccion.TabIndex = 4;

            this.txtTelefono.Location = new System.Drawing.Point(140, 230);
            this.txtTelefono.MaxLength = 9;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(120, 23);
            this.txtTelefono.TabIndex = 5;
            //
            // grdUsuarios
            //
            this.grdUsuarios.AllowUserToAddRows = false;
            this.grdUsuarios.AllowUserToDeleteRows = false;
            this.grdUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdUsuarios.Location = new System.Drawing.Point(385, 20);
            this.grdUsuarios.MultiSelect = false;
            this.grdUsuarios.Name = "grdUsuarios";
            this.grdUsuarios.ReadOnly = true;
            this.grdUsuarios.RowHeadersVisible = false;
            this.grdUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdUsuarios.Size = new System.Drawing.Size(470, 233);
            this.grdUsuarios.TabStop = false;
            this.grdUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdUsuarios_CellClick);
            //
            // grbNavegacion
            //
            this.grbNavegacion.Controls.Add(this.btnPrimero);
            this.grbNavegacion.Controls.Add(this.btnAnterior);
            this.grbNavegacion.Controls.Add(this.lblRegistro);
            this.grbNavegacion.Controls.Add(this.btnSiguiente);
            this.grbNavegacion.Controls.Add(this.btnUltimo);
            this.grbNavegacion.Location = new System.Drawing.Point(20, 270);
            this.grbNavegacion.Name = "grbNavegacion";
            this.grbNavegacion.Size = new System.Drawing.Size(400, 70);
            this.grbNavegacion.Text = "Navegacion";

            this.btnPrimero.Location = new System.Drawing.Point(15, 25);
            this.btnPrimero.Name = "btnPrimero";
            this.btnPrimero.Size = new System.Drawing.Size(50, 30);
            this.btnPrimero.Text = "|<";
            this.btnPrimero.Click += new System.EventHandler(this.btnPrimero_Click);

            this.btnAnterior.Location = new System.Drawing.Point(70, 25);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(50, 30);
            this.btnAnterior.Text = "<";
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);

            this.lblRegistro.Location = new System.Drawing.Point(125, 25);
            this.lblRegistro.Name = "lblRegistro";
            this.lblRegistro.Size = new System.Drawing.Size(150, 30);
            this.lblRegistro.Text = "Registro 0 de 0";
            this.lblRegistro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.btnSiguiente.Location = new System.Drawing.Point(280, 25);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(50, 30);
            this.btnSiguiente.Text = ">";
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);

            this.btnUltimo.Location = new System.Drawing.Point(335, 25);
            this.btnUltimo.Name = "btnUltimo";
            this.btnUltimo.Size = new System.Drawing.Size(50, 30);
            this.btnUltimo.Text = ">|";
            this.btnUltimo.Click += new System.EventHandler(this.btnUltimo_Click);
            //
            // grbEdicion
            //
            this.grbEdicion.Controls.Add(this.btnNuevo);
            this.grbEdicion.Controls.Add(this.btnModificar);
            this.grbEdicion.Controls.Add(this.btnEliminar);
            this.grbEdicion.Controls.Add(this.btnCerrar);
            this.grbEdicion.Location = new System.Drawing.Point(435, 270);
            this.grbEdicion.Name = "grbEdicion";
            this.grbEdicion.Size = new System.Drawing.Size(420, 70);
            this.grbEdicion.Text = "Edicion";

            this.btnNuevo.Location = new System.Drawing.Point(15, 25);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(90, 30);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);

            this.btnModificar.Location = new System.Drawing.Point(115, 25);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(90, 30);
            this.btnModificar.Text = "Modificar";
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);

            this.btnEliminar.Location = new System.Drawing.Point(215, 25);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(90, 30);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            this.btnCerrar.Location = new System.Drawing.Point(315, 25);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(90, 30);
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            //
            // frm_usuario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 360);
            this.Controls.Add(this.lblIdUsuario);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lblClave);
            this.Controls.Add(this.lblConfirmar);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblDireccion);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtIdUsuario);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.txtClave);
            this.Controls.Add(this.txtConfirmar);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.grdUsuarios);
            this.Controls.Add(this.grbNavegacion);
            this.Controls.Add(this.grbEdicion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frm_usuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de usuarios";
            this.Load += new System.EventHandler(this.frm_usuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdUsuarios)).EndInit();
            this.grbNavegacion.ResumeLayout(false);
            this.grbEdicion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblIdUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.Label lblConfirmar;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtIdUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.TextBox txtConfirmar;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.DataGridView grdUsuarios;
        private System.Windows.Forms.GroupBox grbNavegacion;
        private System.Windows.Forms.Button btnPrimero;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Label lblRegistro;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnUltimo;
        private System.Windows.Forms.GroupBox grbEdicion;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCerrar;
    }
}
