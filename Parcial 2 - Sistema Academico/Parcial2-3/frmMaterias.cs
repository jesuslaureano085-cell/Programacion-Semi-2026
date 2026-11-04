using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Parcial2_3
{
    public partial class frmMaterias : Form
    {
        private readonly Conexion_base_datos datos = new Conexion_base_datos();
        private List<Materia> todas = new List<Materia>();
        private List<Materia> visibles = new List<Materia>();
        private Dictionary<string, Control> controles;
        private bool editando, esNueva;

        public frmMaterias()
        {
            InitializeComponent();
            controles = new Dictionary<string, Control>
            {
                { ValidadorAcademico.CODIGO, txtCodigo },
                { ValidadorAcademico.NOMBRE, txtNombre },
                { ValidadorAcademico.UV, txtUv }
            };
        }

        private void frmMaterias_Load(object sender, EventArgs e)
        {
            cboCriterio.SelectedIndex = 1;
            tslAyuda.Text = "Elija el criterio y escriba para filtrar  ";
            establecerEdicion(false);
            recargar(null);
        }

        private void recargar(int? codigoSeleccionar)
        {
            try
            {
                todas = datos.listarMaterias();
            }
            catch (Exception ex)
            {
                todas = new List<Materia>();
                MessageBox.Show("No se pudo leer la tabla de materias.\n\n" + ex.Message, "Base de datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            filtrar(codigoSeleccionar);
        }

        private void filtrar(int? codigoSeleccionar)
        {
            string buscado = txtBuscar.Text.Trim().ToLower();
            bool porCodigo = cboCriterio.SelectedIndex == 0;

            visibles = todas.Where(m => buscado == "" ||
                                        (porCodigo ? m.Codigo.ToString().StartsWith(buscado)
                                                   : m.Nombre.ToLower().Contains(buscado))).ToList();

            lstRegistros.DataSource = null;
            lstRegistros.DataSource = visibles;

            int i = codigoSeleccionar.HasValue ? visibles.FindIndex(m => m.Codigo == codigoSeleccionar.Value) : -1;
            lstRegistros.SelectedIndex = visibles.Count == 0 ? -1 : (i >= 0 ? i : 0);
            mostrarFicha();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (todas.Count > 0 || txtBuscar.Text != "") filtrar(null);
        }

        private void lstRegistros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!editando) mostrarFicha();
        }

        private void mostrarFicha()
        {
            epErrores.Clear();
            lblNotasMateria.Text = "";
            Materia m = lstRegistros.SelectedItem as Materia;
            if (m == null)
            {
                txtId.Clear(); txtCodigo.Clear(); txtNombre.Clear(); txtUv.Clear();
                tslRegistro.Text = todas.Count == 0 ? "Sin registros" : "Ninguna materia coincide";
                return;
            }
            txtId.Text = m.IdMateria.ToString("000");
            txtCodigo.Text = m.Codigo.ToString();
            txtNombre.Text = m.Nombre;
            txtUv.Text = m.Uv.ToString();
            tslRegistro.Text = "Registro " + (lstRegistros.SelectedIndex + 1) + " de " + visibles.Count;
            try
            {
                int notas = datos.contarNotasMateria(m.IdMateria);
                lblNotasMateria.Text = notas == 0 ? "Esta materia no tiene notas registradas."
                                       : "Esta materia tiene " + notas + " notas registradas.";
            }
            catch (Exception)
            {
                lblNotasMateria.Text = "";
            }
        }

        private void irA(int i)
        {
            if (visibles.Count == 0) return;
            lstRegistros.SelectedIndex = Math.Max(0, Math.Min(i, visibles.Count - 1));
        }

        private void tsbPrimero_Click(object sender, EventArgs e) { irA(0); }
        private void tsbAnterior_Click(object sender, EventArgs e) { irA(lstRegistros.SelectedIndex - 1); }
        private void tsbSiguiente_Click(object sender, EventArgs e) { irA(lstRegistros.SelectedIndex + 1); }
        private void tsbUltimo_Click(object sender, EventArgs e) { irA(visibles.Count - 1); }

        private void tsbAgregar_Click(object sender, EventArgs e)
        {
            esNueva = true;
            establecerEdicion(true);
            txtId.Text = "nuevo";
            txtCodigo.Clear(); txtNombre.Clear(); txtUv.Text = "4";
            txtCodigo.Focus();
        }

        private void tsbModificar_Click(object sender, EventArgs e)
        {
            if (lstRegistros.SelectedItem == null) return;
            esNueva = false;
            establecerEdicion(true);
            txtCodigo.Focus();
        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            establecerEdicion(false);
            mostrarFicha();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            Materia m = lstRegistros.SelectedItem as Materia;
            if (m == null) return;

            try
            {
                int notas = datos.contarNotasMateria(m.IdMateria);
                string aviso = notas == 0
                    ? "Se eliminará la materia " + m.Nombre + ".\n¿Desea continuar?"
                    : m.Nombre + " tiene " + notas + " notas registradas.\nSi continúa, también se borrarán esas notas.\n¿Desea continuar?";

                if (MessageBox.Show(aviso, "Eliminar materia", MessageBoxButtons.YesNo,
                        notas == 0 ? MessageBoxIcon.Question : MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                datos.mantenimientoMaterias(m, Operacion.Eliminar);
                recargar(null);
                tslRegistro.Text += "  ·  materia eliminada";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            Materia m;
            Dictionary<string, string> errores = ValidadorAcademico.ValidarMateria(txtCodigo.Text, txtNombre.Text, txtUv.Text, out m);

            Materia actual = lstRegistros.SelectedItem as Materia;
            int id = esNueva || actual == null ? 0 : actual.IdMateria;

            try
            {
                if (m != null && datos.existeCodigoMateria(m.Codigo, id))
                    errores[ValidadorAcademico.CODIGO] = "Ya existe otra materia con ese código.";

                if (!marcarErrores(errores)) return;

                m.IdMateria = id;
                datos.mantenimientoMaterias(m, esNueva ? Operacion.Agregar : Operacion.Modificar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            establecerEdicion(false);
            txtBuscar.Clear();
            recargar(m.Codigo);
            tslRegistro.Text += esNueva ? "  ·  agregada" : "  ·  actualizada";
        }

        private bool marcarErrores(Dictionary<string, string> errores)
        {
            epErrores.Clear();
            if (errores.Count == 0) return true;

            foreach (KeyValuePair<string, string> error in errores)
                epErrores.SetError(controles[error.Key], error.Value);

            controles[errores.Keys.First()].Focus();
            tslRegistro.Text = "Revise los campos marcados — no se guardó";
            return false;
        }

        private void establecerEdicion(bool activar)
        {
            editando = activar;
            foreach (TextBox t in new[] { txtCodigo, txtNombre, txtUv }) t.ReadOnly = !activar;

            lstRegistros.Enabled = !activar;
            txtBuscar.Enabled = !activar;
            cboCriterio.Enabled = !activar;
            foreach (ToolStripItem b in new ToolStripItem[] { tsbPrimero, tsbAnterior, tsbSiguiente, tsbUltimo,
                                                              tsbAgregar, tsbModificar, tsbEliminar })
                b.Enabled = !activar;
            tsbGuardar.Enabled = activar;
            tsbCancelar.Enabled = activar;

            lblModo.Text = !activar ? "Modo consulta" : esNueva ? "Agregando una materia nueva" : "Modificando la materia seleccionada";
        }
    }
}
