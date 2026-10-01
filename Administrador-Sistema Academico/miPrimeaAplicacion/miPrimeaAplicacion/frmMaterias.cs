using miPrimeaAplicacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace miPrimerProyectoCsharp
{
    // Asegúrate de que el namespace coincida con el tuyo si es diferente
    public partial class frmMaterias : Form
    {
        public frmMaterias()
        {
            InitializeComponent();
        }

        Conexion objConexion = new Conexion();
        DataSet objDs = new DataSet();
        DataTable objDt = new DataTable();
        public int posicion = 0;
        public string accion = "nuevo";

        private void actualizarDs()
        {
            objDs.Clear();
            objDs = objConexion.obtenerDatos();
            objDt = objDs.Tables["materias"];
            objDt.PrimaryKey = new DataColumn[] { objDt.Columns["idMateria"] };
            grdMaterias.DataSource = objDt.DefaultView;
            mostrarDatos();
        }

        private void mostrarDatos()
        {
            if (objDt.Rows.Count > 0 && posicion >= 0 && posicion < objDt.Rows.Count)
            {
                idMateria.Text = objDt.Rows[posicion]["idMateria"].ToString();
                txtCodigoMateria.Text = objDt.Rows[posicion]["codigo"].ToString();
                txtNombreMateria.Text = objDt.Rows[posicion]["nombre"].ToString();
                txtUvMateria.Text = objDt.Rows[posicion]["uv"].ToString();

                // Escudo protector: Solo dibuja la barra azul si la tabla visual tiene suficientes filas
                if (posicion < grdMaterias.Rows.Count)
                {
                    grdMaterias.CurrentCell = grdMaterias.Rows[posicion].Cells[0];
                }
                lblnRegistrosMateria.Text = (posicion + 1) + " de " + objDt.Rows.Count;
            }
            else
            {
                // Si la posición no es válida, limpiamos las cajas para evitar errores visuales
                limpiarControles();
                lblnRegistrosMateria.Text = "0 de 0";
            }
        }

        private void frmMaterias_Load(object sender, EventArgs e)
        {
            actualizarDs();
            cboBuscarMaterias.SelectedIndex = 1; // Selecciona "Nombre" por defecto
        }

        private void btnSiguienteMateria_Click(object sender, EventArgs e)
        {
            if (posicion < objDt.Rows.Count - 1)
            {
                posicion++;
                mostrarDatos();
            }
            else
            {
                MessageBox.Show("Estas en el ultimo registro.", "Navegacion de Materias", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAnteriorMateria_Click(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion--;
                mostrarDatos();
            }
            else
            {
                MessageBox.Show("Estas en el primer registro.", "Navegacion de Materias", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUltimoMateria_Click(object sender, EventArgs e)
        {
            posicion = objDt.Rows.Count - 1;
            mostrarDatos();
        }

        private void btnPrimeroMateria_Click(object sender, EventArgs e)
        {
            posicion = 0;
            mostrarDatos();
        }

        private void estadoControles(Boolean estado)
        {
            grbDatosMateria.Enabled = estado;
            grbNavegacionMateria.Enabled = !estado;
            btnEliminarMateria.Enabled = !estado;

            // --- LÍNEAS NUEVAS: Forzar que el buscador siempre esté activo ---
            cboBuscarMaterias.Enabled = true;
            txtBuscarMaterias.Enabled = true;
        }

        private void limpiarControles()
        {
            idMateria.Text = "";
            txtCodigoMateria.Text = "";
            txtNombreMateria.Text = "";
            txtUvMateria.Text = "";
        }

        private void btnAgregarMateria_Click(object sender, EventArgs e)
        {
            if (btnAgregarMateria.Text == "Nuevo")
            {
                btnAgregarMateria.Text = "Guardar";
                btnModificarMateria.Text = "Cancelar";
                estadoControles(true);
                accion = "nuevo";
                limpiarControles();
            }
            else
            {
                //Guardar o Modificar
                string idActual = "";

                if (accion == "modificar")
                {
                    // Escudo 1: Si por alguna razón no hay filas, cancelamos para no crashear
                    if (objDt.Rows.Count == 0) return;

                    // Usamos el TRUCO SEGURO de la posición (igual que en Eliminar)
                    idActual = objDt.Rows[posicion][0].ToString();
                }

                String[] materias = {
            idActual, txtCodigoMateria.Text, txtNombreMateria.Text, txtUvMateria.Text
        };

                String respuesta = objConexion.administrarDatosMaterias(materias, accion);
                if (respuesta != "1")
                {
                    MessageBox.Show(respuesta, "Error al guardar materias.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    estadoControles(false);
                    btnAgregarMateria.Text = "Nuevo";
                    btnModificarMateria.Text = "Modificar";
                    actualizarDs();
                    txtBuscarMaterias.Text = "";
                }
            }
        }

        private void btnModificarMateria_Click(object sender, EventArgs e)
        {
            if (btnModificarMateria.Text == "Modificar")
            {
                // Escudo 2: No permitir entrar a modificar si la tabla está vacía
                if (objDt.Rows.Count == 0)
                {
                    MessageBox.Show("No hay registros para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnAgregarMateria.Text = "Guardar";
                btnModificarMateria.Text = "Cancelar";
                estadoControles(true);
                accion = "modificar";
            }
            else
            {   //Cancelar
                mostrarDatos();
                estadoControles(false);
                btnAgregarMateria.Text = "Nuevo";
                btnModificarMateria.Text = "Modificar";
            }
        }

        private void btnEliminarMateria_Click(object sender, EventArgs e)
        {
            if (objDt.Rows.Count == 0) return;

            if (MessageBox.Show("Esta seguro de eliminar a " + txtNombreMateria.Text,
                "Eliminando materias", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Lee de la posición actual de los datos (evita eliminar a otro)
                string idActual = objDt.Rows[posicion][0].ToString();

                // Llama al método de materias y no al de alumnos
                String respuesta = objConexion.administrarDatosMaterias(
                    new String[] { idActual, "", "", "" }, "eliminar"
                );

                if (respuesta != "1")
                {
                    MessageBox.Show(respuesta, "Error al eliminar materia.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    posicion = 0;
                    actualizarDs();
                    MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtBuscarMaterias_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                filtrarDatos(txtBuscarMaterias.Text);
            }
            catch (Exception ex)
            {
                // Silencioso para evitar popups molestos mientras se escribe
            }
        }

        private void filtrarDatos(String valor)
        {
            try
            {
                DataView objDv = objDt.DefaultView;

                // --- NUEVA REGLA: Si la caja está vacía, limpia el filtro ---
                if (valor == "")
                {
                    objDv.RowFilter = "";
                }
                else
                {
                    switch (cboBuscarMaterias.SelectedIndex)
                    {
                        case 0: //codigo
                            objDv.RowFilter = "codigo = " + valor;
                            break;
                        case 1: //nombre
                            objDv.RowFilter = "nombre like '%" + valor + "%'";
                            break;
                    }
                }

                grdMaterias.DataSource = objDv;
                seleccionarMateria();
            }
            catch (Exception e)
            {
                // Silencioso
            }
        }

        private void seleccionarMateria()
        {
            try
            {
                if (grdMaterias.CurrentRow == null) return;

                // Lee usando el índice de columna seguro
                string id = grdMaterias.CurrentRow.Cells[0].Value.ToString();
                posicion = objDt.Rows.IndexOf(objDt.Rows.Find(id));
                mostrarDatos();
            }
            catch (Exception e)
            {
                // Silencioso
            }
        }

        private void grdMaterias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionarMateria();
        }

        private void grdMaterias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cboBuscarMaterias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtBuscarMaterias_TextChanged(object sender, EventArgs e)
        {
        }
    }
}