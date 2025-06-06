using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trabajo_Fin_De_Grado.Clases;

namespace Trabajo_Fin_De_Grado.Administrador
{
    public partial class InicioAdministrador : Form
    {
        public string nombreUsuario;
        public InicioAdministrador()
        {
            InitializeComponent();
            this.Size = new Size(this.Width + 50, this.Height + 30);
            CargarUsuariosEnDataGridView();
        }

        private void CargarUsuariosEnDataGridView()
        {
            Conexion objetoConexion = new Conexion();
            MySqlConnection conexion = objetoConexion.establecerConexion();

            try
            {
                //conexion.Open();
                string query = "SELECT * FROM Usuarios;";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    // Crear columnas si aún no están
                    if (dataGridView1.Columns.Count == 0)
                    {
                        dataGridView1.Columns.Add("Nombre", "Nombre");
                        dataGridView1.Columns.Add("Apellidos", "Apellidos");
                        dataGridView1.Columns.Add("Provincia", "Provincia");

                        dataGridView1.RowTemplate.Height = 64;

                    }

                    dataGridView1.Rows.Clear();


                    while (reader.Read())
                    {
                        string nombre = reader.GetString("Nombre");
                        if (nombre.Equals("admin", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        string apellidos = reader.GetString("Apellidos");
                        string provincia = reader.GetString("provincia");

                        dataGridView1.Rows.Add(nombre, apellidos, provincia);
                    }

                    dataGridView1.Sort(dataGridView1.Columns["Nombre"], ListSortDirection.Ascending);
                    dataGridView1.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar recetas: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                nombreUsuario = row.Cells["Nombre"].Value?.ToString();

            }
        }

        private void eliminarUsuario(string usuario)
        {
            try
            {
                Conexion objetoConexion = new Conexion();
                using (MySqlConnection conexion = objetoConexion.establecerConexion())
                {
                    try
                    {
                        // Abrimos la conexión si no está abierta
                        if (conexion.State != ConnectionState.Open)
                        {
                            conexion.Open();
                        }

                        // Eliminamos primero las recetas asociadas al usuario
                        string queryRecetas = "DELETE FROM Recetas WHERE LOWER(usuario) = @nombre";
                        using (MySqlCommand cmdRecetas = new MySqlCommand(queryRecetas, conexion))
                        {
                            cmdRecetas.Parameters.AddWithValue("@nombre", usuario.ToLower());
                            cmdRecetas.ExecuteNonQuery(); // No importa cuántas filas se borren
                        }

                        // Luego eliminamos el usuario
                        string queryUsuario = "DELETE FROM Usuarios WHERE LOWER(Nombre) = @nombre";
                        using (MySqlCommand cmdUsuario = new MySqlCommand(queryUsuario, conexion))
                        {
                            cmdUsuario.Parameters.AddWithValue("@nombre", usuario.ToLower());

                            int filasAfectadas = cmdUsuario.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                MessageBox.Show("Usuario eliminado correctamente.");
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el usuario para eliminar.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el usuario: " + ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el suario:\n" + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(nombreUsuario))
            {
                //string nombreReceta = dataGridView1.SelectedRows[0].Cells["Nombre"].Value.ToString();

                DialogResult result = MessageBox.Show($"¿Seguro que deseas eliminar el usuario \"{nombreUsuario}\", se eliminarán todas sus recetas?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    eliminarUsuario(nombreUsuario);
                    // Actualiza el DataGridView si lo deseas aquí

                }
                CargarUsuariosEnDataGridView();
            }
            else
            {
                MessageBox.Show("Selecciona un usuario para eliminar.");
            }
        }

        private void porUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Cargar informe Crystal
                ReportDocument reporte = new ReportDocument();
                reporte.Load("../../Administrador/Informes/InformeRecetasPorUsuarios.rpt"); // Ajusta si está en otra carpeta

                // Conexión con tu clase Conexion
                Conexion objetoConexion = new Conexion();
                MySqlConnection conexion = objetoConexion.establecerConexion();


                // Exportar a PDF
                string destino = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "InformeRecetasPorUsuario.pdf"
                );

                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, destino);

                MessageBox.Show("Informe exportado correctamente al escritorio.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el informe: " + ex.Message);
            }
        }

        private void todasLasRecetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Cargar informe Crystal
                ReportDocument reporte = new ReportDocument();
                reporte.Load("../../Administrador/Informes/InformeRecetas.rpt"); // Ajusta si está en otra carpeta

                // Conexión con tu clase Conexion
                Conexion objetoConexion = new Conexion();
                MySqlConnection conexion = objetoConexion.establecerConexion();


                // Exportar a PDF
                string destino = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "InformeRecetas.pdf"
                );

                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, destino);

                MessageBox.Show("Informe exportado correctamente al escritorio.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el informe: " + ex.Message);
            }
        }

        private void informeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Cargar informe Crystal
                ReportDocument reporte = new ReportDocument();
                reporte.Load("../../Administrador/Informes/InformeUsuarios.rpt"); // Ajusta si está en otra carpeta

                // Conexión con tu clase Conexion
                Conexion objetoConexion = new Conexion();
                MySqlConnection conexion = objetoConexion.establecerConexion();


                // Exportar a PDF
                string destino = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "InformeUsuarios.pdf"
                );

                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, destino);

                MessageBox.Show("Informe exportado correctamente al escritorio.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el informe: " + ex.Message);
            }
        }
    }
}
