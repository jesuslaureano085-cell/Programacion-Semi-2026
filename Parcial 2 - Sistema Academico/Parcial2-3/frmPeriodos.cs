using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Parcial2_3
{
    public partial class frmPeriodos : Form
    {
        private readonly Conexion_base_datos datos = new Conexion_base_datos();
        private BindingList<Periodo> periodos = new BindingList<Periodo>();
        private readonly List<int> eliminados = new List<int>();

        public frmPeriodos()
        {
            InitializeComponent();
        }

        private void frmPeriodos_Load(object sender, EventArgs e)
        {
            bsPeriodos.AddingNew += (s, ev) =>
            {
                ev.NewObject = new Periodo { Nombre = "Nuevo período", Fecha = DateTime.Today };
            };
            bsPeriodos.CurrentChanged += (s, ev) => actualizarEstado();

            cargar();

            Binding id = lblIdValor.DataBindings.Add("Text", bsPeriodos, "IdPeriodo", true);
            id.Format += (s, ev) => { if (ev.Value is int && (int)ev.Value == 0) ev.Value = "nuevo"; };
            txtPeriodo.DataBindings.Add("Text", bsPeriodos, "Nombre", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpFecha.DataBindings.Add("Value", bsPeriodos, "Fecha", true, DataSourceUpdateMode.OnPropertyChanged);
            actualizarEstado();
        }

        private void cargar()
        {
            eliminados.Clear();
            try
            {
                periodos = new BindingList<Periodo>(datos.listarPeriodos());
            }
            catch (Exception ex)
            {
                periodos = new BindingList<Periodo>();
                MessageBox.Show("No se pudieron leer los períodos.\n\n" + ex.Message, "Base de datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            bsPeriodos.DataSource = periodos;
        }

        private int pendientes()
        {
            return eliminados.Count + periodos.Count(p => p.IdPeriodo == 0 || p.Modificado);
        }

        private void actualizarEstado()
        {
            int n = pendientes();
            tslRegistro.Text = periodos.Count == 0 ? "Sin períodos" : "Período " + (bsPeriodos.Position + 1) + " de " + periodos.Count;
            tslAyuda.Text = n == 0 ? "Todo guardado  " : n + " cambio(s) sin guardar  ";
        }

        private void bnEliminar_Click(object sender, EventArgs e)
        {
            Periodo p = bsPeriodos.Current as Periodo;
            if (p == null) return;

            try
            {
                if (p.IdPeriodo > 0 && datos.periodoTieneNotas(p.IdPeriodo))
                {
                    MessageBox.Show("«" + p.Nombre + "» ya tiene notas registradas y no se puede eliminar.",
                        "Eliminar período", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("¿Quitar el período «" + p.Nombre + "»?", "Eliminar período",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (p.IdPeriodo > 0) eliminados.Add(p.IdPeriodo);
            bsPeriodos.RemoveCurrent();
            actualizarEstado();
        }

        private void bnGuardar_Click(object sender, EventArgs e)
        {
            Validate();
            bsPeriodos.EndEdit();
            epErrores.Clear();

            Periodo sinNombre = periodos.FirstOrDefault(p => string.IsNullOrWhiteSpace(p.Nombre));
            if (sinNombre != null)
            {
                bsPeriodos.Position = periodos.IndexOf(sinNombre);
                epErrores.SetError(txtPeriodo, "Escriba el nombre del período.");
                return;
            }

            try
            {
                int cambios = datos.guardarPeriodos(periodos, eliminados);
                cargar();
                actualizarEstado();
                tslAyuda.Text = cambios + " cambio(s) guardado(s)  ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
