using miPrimeaAplicacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace miPrimerProyectoCsharp
{
    public partial class frmNotas : Form
    {
        public frmNotas()
        {
            InitializeComponent();
        }

        Conexion objConexion = new Conexion();
        DataSet objNotas = new DataSet();

        private void actualizarDs()
        {
            int idMateria = 1;
            int idPeriodo = 1;

            if (cboMateria.SelectedValue != null) int.TryParse(cboMateria.SelectedValue.ToString(), out idMateria);
            if (cboPeriodo.SelectedValue != null) int.TryParse(cboPeriodo.SelectedValue.ToString(), out idPeriodo);

            objNotas.Clear(); // Limpiar el DataSet

            // CONSULTA CORREGIDA: Usando 'matricula' e 'idMatricula' que son los nombres reales de tu base de datos
            objConexion.objAdaptador = new SqlDataAdapter("SELECT alumnos.nombre, dnotas.idDetalle, dnotas.idNota, dnotas.idMateria, " +
                 "dnotas.lab1, dnotas.lab2, dnotas.parcial, (dnotas.lab1*0.3 + dnotas.lab2*0.3 + dnotas.parcial*0.4) AS nf " +
                 "FROM dnotas INNER JOIN matricula ON (matricula.idMatricula = dnotas.idNota) " +
                 "INNER JOIN alumnos ON (alumnos.idAlumno = matricula.idAlumno) " +
                 "WHERE matricula.idPeriodo = " + idPeriodo + " AND dnotas.idMateria = " + idMateria, objConexion.objConexion);

            objConexion.objAdaptador.Fill(objNotas, "notasAlumnos");
        }

        private void actualizarGrid()
        {
            actualizarDs();
            dnotasDataGridView.DataSource = objNotas;
            dnotasDataGridView.DataMember = "notasAlumnos";
        }

        private void frmNotas_Load(object sender, EventArgs e)
        {
            try
            {
                // Llenamos los ComboBox usando la clase Conexion general del proyecto
                DataSet dsCombos = objConexion.obtenerDatos();

                cboMateria.DataSource = dsCombos.Tables["materias"];
                cboMateria.DisplayMember = "nombre";
                cboMateria.ValueMember = "idMateria";

                // Cargamos los períodos directamente con un adaptador seguro
                SqlDataAdapter daPeriodos = new SqlDataAdapter("SELECT * FROM periodos", objConexion.objConexion);
                DataTable dtPeriodos = new DataTable();
                daPeriodos.Fill(dtPeriodos);

                cboPeriodo.DataSource = dtPeriodos;
                cboPeriodo.DisplayMember = "periodo";
                cboPeriodo.ValueMember = "idPeriodo";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar catálogos: " + ex.Message);
            }

            actualizarGrid();
        }

        private void cboMateria_SelectedValueChanged(object sender, EventArgs e)
        {
            actualizarGrid();
        }

        private void cboPeriodo_SelectedValueChanged(object sender, EventArgs e)
        {
            actualizarGrid();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int nFilas = dnotasDataGridView.Rows.Count;
            for (int i = 0; i < nFilas; i++)
            {
                double lab1 = 0, lab2 = 0, parcial = 0;
                int idDetalle = int.Parse(dnotasDataGridView.Rows[i].Cells["idDetalle"]?.Value?.ToString() ?? "0");

                if (idDetalle == 0) continue; // Salta filas vacías

                lab1 = double.Parse(dnotasDataGridView.Rows[i].Cells["lab1"]?.Value?.ToString() ?? "0");
                lab2 = double.Parse(dnotasDataGridView.Rows[i].Cells["lab2"]?.Value?.ToString() ?? "0");
                parcial = double.Parse(dnotasDataGridView.Rows[i].Cells["parcial"]?.Value?.ToString() ?? "0");

                string sql = "UPDATE dnotas SET lab1='" + lab1 + "', lab2='" + lab2 + "', parcial='" + parcial +
                    "' WHERE idDetalle='" + idDetalle + "'";

                String resp = objConexion.ejecutarSQL(sql);
                if (resp != "1")
                {
                    MessageBox.Show(resp, "Error al actualizar notas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            MessageBox.Show("Notas actualizadas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            actualizarGrid();
        }
    }
}