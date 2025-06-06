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

namespace Trabajo_Fin_De_Grado.Usuarios
{
    public partial class RecetasDeUsuario : Form
    {
        public string usuarioRecetas;
        public string usuarioRecetas2;
        public string nombreReceta;
        public RecetasDeUsuario(string nombreUsuario)
        {
            InitializeComponent();

            this.Size = new Size(this.Width + 50, this.Height + 30);
            usuarioRecetas = nombreUsuario;


            label1.Text = "Las Recetas de " + nombreUsuario;
            CargarRecetasEnDataGridView();
        }


        private void CargarRecetasEnDataGridView()
        {
            Conexion objetoConexion = new Conexion();
            MySqlConnection conexion = objetoConexion.establecerConexion();

            try
            {
                string query = "SELECT * FROM Recetas WHERE usuario = @usuario";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuarioRecetas);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            //No hay recetas
                            dataGridView1.Visible = false;
                            lblNoRecetas.Text = "No ha introducido ninguna receta de momento";
                            lblNoRecetas.Visible = true;
                            btnEditar.Visible = false;
                            btnEliminar.Visible = false;
                        }
                        else
                        {
                            //Hay recetas
                            dataGridView1.Visible = true;
                            lblNoRecetas.Visible = false;

                            if (dataGridView1.Columns.Count == 0)
                            {
                                DataGridViewImageColumn colImagen = new DataGridViewImageColumn();
                                colImagen.HeaderText = "Foto";
                                colImagen.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                dataGridView1.Columns.Add(colImagen);

                                dataGridView1.Columns.Add("Nombre", "Nombre");
                                dataGridView1.Columns.Add("Comensales", "Comensales");
                                dataGridView1.Columns.Add("Alergenos", "Alérgenos");
                                dataGridView1.RowTemplate.Height = 64;
                            }

                            dataGridView1.Rows.Clear();
                            Image imagenDefault = Image.FromFile("../../Imagenes/trsiteza.jpg");

                            foreach (DataRow row in dt.Rows)
                            {
                                string nombre = row["Nombre"].ToString();
                                string alergenos = row["Alergenos"].ToString();
                                int comensales = Convert.ToInt32(row["Comensales"]);
                                byte[] fotoBytes = row["imagen"] as byte[];

                                Image imagen;

                                if (fotoBytes == null || fotoBytes.Length == 0)
                                {
                                    imagen = RedimensionarImagen(imagenDefault, 104, 84);
                                }
                                else
                                {
                                    try
                                    {
                                        using (MemoryStream ms = new MemoryStream(fotoBytes))
                                        {
                                            imagen = Image.FromStream(ms);
                                            imagen = RedimensionarImagen(imagen, 134, 114);
                                        }
                                    }
                                    catch
                                    {
                                        imagen = RedimensionarImagen(imagenDefault, 104, 84);
                                    }
                                }

                                dataGridView1.Rows.Add(imagen, nombre, comensales, alergenos);
                            }

                            dataGridView1.Sort(dataGridView1.Columns["Nombre"], ListSortDirection.Ascending);
                            dataGridView1.ClearSelection();
                        }
                    }
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

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dataGridView1.Columns["Alergenos"].Index)
                return;

            e.PaintBackground(e.CellBounds, true);
            string texto = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString()?.ToLower() ?? "";

            int x = e.CellBounds.Left + 4;
            int y = e.CellBounds.Top + 4;
            int tamaño = 34;

            Dictionary<string, Image> iconos = new Dictionary<string, Image>
            {
                { "gluten", Image.FromFile("../../Imagenes/Alergenos/AlergenoGluten.png") },
                { "crustáceos", Image.FromFile("../../Imagenes/Alergenos/AlergenoCrustaceos.png") },
                { "huevos", Image.FromFile("../../Imagenes/Alergenos/AlergenoHuevos.png") },
                { "pescado", Image.FromFile("../../Imagenes/Alergenos/AlergenoPescado.png") },
                { "cacahuetes", Image.FromFile("../../Imagenes/Alergenos/AlergenoCacahuetes.png") },
                { "soja", Image.FromFile("../../Imagenes/Alergenos/AlergenoSoja.png") },
                { "lácteos", Image.FromFile("../../Imagenes/Alergenos/AlergenoLacteos.png") },
                { "frutos secos", Image.FromFile("../../Imagenes/Alergenos/AlergenoFrutosSecos.png") },
                { "apio", Image.FromFile("../../Imagenes/Alergenos/AlergenoApio.png") },
                { "mostaza", Image.FromFile("../../Imagenes/Alergenos/AlergenoMostaza.png") },
                { "sulfitos", Image.FromFile("../../Imagenes/Alergenos/AlergenoSulfitos.png") },
                { "altramuces", Image.FromFile("../../Imagenes/Alergenos/AlergenoAltramuces.png") },
                { "moluscos", Image.FromFile("../../Imagenes/Alergenos/AlergenoMoluscos.png") },
            };

            foreach (var aler in iconos.Keys)
            {
                if (texto.Contains(aler))
                {
                    e.Graphics.DrawImage(iconos[aler], new Rectangle(x, y, tamaño, tamaño));
                    x += tamaño + 4;
                }
            }

            e.Handled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(nombreReceta))
            {
                EditarRecetas editarRecetas = new EditarRecetas(nombreReceta, usuarioRecetas);
                AbrirFormularioEnPanel(editarRecetas);
            }
            else
            {
                MessageBox.Show("Selecciona una receta para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                nombreReceta = row.Cells["Nombre"].Value?.ToString();
            }
        }

        private void eliminarReceta(string nombreReceta)
        {
            try
            {
                Conexion objetoConexion = new Conexion();
                using (MySqlConnection conexion = objetoConexion.establecerConexion())
                {
                    string query = "DELETE FROM Recetas WHERE LOWER(Nombre) = @nombre";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombreReceta.ToLower());

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Receta eliminada correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la receta para eliminar.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la receta:\n" + ex.Message);
            }
        }

        private Image RedimensionarImagen(Image original, int width, int height)
        {
            Bitmap resized = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.DrawImage(original, 0, 0, width, height);
            }
            return resized;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(nombreReceta))
            {
                DialogResult result = MessageBox.Show($"¿Seguro que deseas eliminar la receta \"{nombreReceta}\"?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    eliminarReceta(nombreReceta);
                }
                CargarRecetasEnDataGridView();
            }
            else
            {
                MessageBox.Show("Selecciona una receta para eliminar.");
            }
        }

        private void AbrirFormularioEnPanel(Form formulario)
        {
            panel1.Controls.Clear();
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            panel1.Controls.Add(formulario);
            formulario.Show();
        }

        private void atrásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InicioUsuarios inicioUsuarios = new InicioUsuarios(usuarioRecetas);
            AbrirFormularioEnPanel(inicioUsuarios);
        }
    }
}
