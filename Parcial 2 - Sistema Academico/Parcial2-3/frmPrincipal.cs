using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Parcial2_3
{
    public partial class frmPrincipal : Form
    {
        private readonly Usuario usuarioSesion;
        private Dictionary<ToolStripItem, Func<Form>> fabricas;

        public bool CerrarSesion { get; private set; }

        public frmPrincipal() : this(null)
        {
        }

        public frmPrincipal(Usuario sesion)
        {
            InitializeComponent();
            usuarioSesion = sesion;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            fabricas = new Dictionary<ToolStripItem, Func<Form>>
            {
                { menuAlumnos,  () => new Form1() },
                { menuMaterias, () => new frmMaterias() },
                { menuPeriodos, () => new frmPeriodos() },
                { menuNotas,    () => new frmNotas() },
                { menuUsuarios, () => new frm_usuario(usuarioSesion) }
            };

            string nombre = usuarioSesion == null ? "" : usuarioSesion.Nombre;
            menuSesion.Text = nombre;
            tslConectado.Text = "Conectado como " + nombre;
            tslFecha.Text = DateTime.Today.ToLongDateString() + "  ";
        }

        private void menuFormulario_Click(object sender, EventArgs e)
        {
            foreach (Form hijo in MdiChildren) hijo.Close();

            Form nuevo = fabricas[(ToolStripItem)sender]();
            nuevo.MdiParent = this;
            nuevo.Show();
        }

        private void menuCerrarSesion_Click(object sender, EventArgs e)
        {
            CerrarSesion = true;
            Close();
        }

        private void menuSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
