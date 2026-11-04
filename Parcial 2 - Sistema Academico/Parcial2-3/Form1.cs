using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Parcial2_3
{
    public partial class Form1 : Form
    {
        private readonly Conexion_base_datos datos = new Conexion_base_datos();
        private List<Alumno> todos = new List<Alumno>();
        private List<Alumno> visibles = new List<Alumno>();
        private Dictionary<string, Control> controles;
        private bool editando, esNuevo;

        public Form1()
        {
            InitializeComponent();
            controles = new Dictionary<string, Control>
            {
                { ValidadorAcademico.CODIGO, txtCodigo },
                { ValidadorAcademico.NOMBRE, txtNombre },
                { ValidadorAcademico.TELEFONO, txtTelefono }
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tslAyuda.Text = "Escriba en el buscador para filtrar por código o nombre  ";
            establecerEdicion(false);
            recargar(null);
        }

        private void recargar(string codigoSeleccionar)
        {
            try
            {
                todos = datos.listarAlumnos();
            }
            catch (Exception ex)
            {
                todos = new List<Alumno>();
                MessageBox.Show("No se pudo leer la tabla de alumnos.\n\n" + ex.Message, "Base de datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            filtrar(codigoSeleccionar);
        }

        private void filtrar(string codigoSeleccionar)
        {
            string buscado = txtBuscar.Text.Trim().ToLower();
            visibles = todos.Where(a => buscado == "" ||
                                        a.Codigo.ToLower().Contains(buscado) ||
                                        a.Nombre.ToLower().Contains(buscado)).ToList();

            lstRegistros.DataSource = null;
            lstRegistros.DataSource = visibles;

            int i = visibles.FindIndex(a => a.Codigo == codigoSeleccionar);
            lstRegistros.SelectedIndex = visibles.Count == 0 ? -1 : (i >= 0 ? i : 0);
            mostrarFicha();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            filtrar(null);
        }

        private void lstRegistros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!editando) mostrarFicha();
        }

        private void mostrarFicha()
        {
            epErrores.Clear();
            Alumno a = lstRegistros.SelectedItem as Alumno;
            if (a == null)
            {
                limpiar();
                tslRegistro.Text = todos.Count == 0 ? "Sin registros" : "Ningún alumno coincide con la búsqueda";
                lblNotasAlumno.Text = "";
                return;
            }
            txtId.Text = a.IdAlumno.ToString("000");
            txtCodigo.Text = a.Codigo;
            txtNombre.Text = a.Nombre;
            txtDireccion.Text = a.Direccion;
            txtTelefono.Text = a.Telefono;
            tslRegistro.Text = "Registro " + (lstRegistros.SelectedIndex + 1) + " de " + visibles.Count;
            try
            {
                int notas = datos.contarNotasAlumno(a.IdAlumno);
                lblNotasAlumno.Text = notas == 0 ? "Este alumno no tiene notas registradas."
                                       : "Este alumno tiene " + notas + " notas registradas.";
            }
            catch (Exception)
            {
                lblNotasAlumno.Text = "";
            }
        }

        private void limpiar()
        {
            foreach (TextBox t in new[] { txtId, txtCodigo, txtNombre, txtDireccion, txtTelefono }) t.Clear();
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
            esNuevo = true;
            establecerEdicion(true);
            limpiar();
            txtId.Text = "nuevo";
            txtCodigo.Focus();
        }

        private void tsbModificar_Click(object sender, EventArgs e)
        {
            if (lstRegistros.SelectedItem == null) return;
            esNuevo = false;
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
            Alumno a = lstRegistros.SelectedItem as Alumno;
            if (a == null) return;

            try
            {
                int notas = datos.contarNotasAlumno(a.IdAlumno);
                string aviso = notas == 0
                    ? "Se eliminará a " + a.Nombre + ".\n¿Desea continuar?"
                    : a.Nombre + " tiene " + notas + " notas registradas.\nSi continúa, también se borrarán sus notas.\n¿Desea continuar?";

                if (MessageBox.Show(aviso, "Eliminar alumno", MessageBoxButtons.YesNo,
                        notas == 0 ? MessageBoxIcon.Question : MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                datos.mantenimientoAlumnos(a, Operacion.Eliminar);
                recargar(null);
                tslRegistro.Text += "  ·  alumno eliminado";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            Alumno actual = lstRegistros.SelectedItem as Alumno;
            Alumno a = new Alumno
            {
                IdAlumno = esNuevo || actual == null ? 0 : actual.IdAlumno,
                Codigo = txtCodigo.Text.Trim().ToUpper(),
                Nombre = txtNombre.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Telefono = txtTelefono.Text.Trim()
            };

            Dictionary<string, string> errores = ValidadorAcademico.ValidarAlumno(a);
            try
            {
                if (!errores.ContainsKey(ValidadorAcademico.CODIGO) && datos.existeCodigoAlumno(a.Codigo, a.IdAlumno))
                    errores[ValidadorAcademico.CODIGO] = "Ya existe otro alumno con ese código.";

                if (!marcarErrores(errores)) return;

                datos.mantenimientoAlumnos(a, esNuevo ? Operacion.Agregar : Operacion.Modificar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            establecerEdicion(false);
            txtBuscar.Clear();
            recargar(a.Codigo);
            tslRegistro.Text += esNuevo ? "  ·  agregado" : "  ·  actualizado";
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
            foreach (TextBox t in new[] { txtCodigo, txtNombre, txtDireccion, txtTelefono }) t.ReadOnly = !activar;

            lstRegistros.Enabled = !activar;
            txtBuscar.Enabled = !activar;
            foreach (ToolStripItem b in new ToolStripItem[] { tsbPrimero, tsbAnterior, tsbSiguiente, tsbUltimo,
                                                              tsbAgregar, tsbModificar, tsbEliminar })
                b.Enabled = !activar;
            tsbGuardar.Enabled = activar;
            tsbCancelar.Enabled = activar;

            lblModo.Text = !activar ? "Modo consulta" : esNuevo ? "Agregando un alumno nuevo" : "Modificando el alumno seleccionado";
        }
    }
}
