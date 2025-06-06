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
using Trabajo_Fin_De_Grado.Usuarios;

namespace Trabajo_Fin_De_Grado.Usuarios
{
    public partial class InicioUsuarios : Form
    {
        public string usuarioInicio;
        public InicioUsuarios(String nombreUsuario)
        {
            InitializeComponent();

            this.Size = new Size(this.Width + 50, this.Height + 50);
            usuarioInicio = nombreUsuario;
            CargarRecetasEnDataGridView();
        }

        private void CargarRecetasEnDataGridView()
        {
            Conexion objetoConexion = new Conexion();
            MySqlConnection conexion = objetoConexion.establecerConexion();

            try
            {
                //conexion.Open();
                string query = "SELECT * FROM Recetas";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    // Crear columnas si aún no están
                    if (dataGridView1.Columns.Count == 0)
                    {
                        DataGridViewImageColumn colImagen = new DataGridViewImageColumn();
                        colImagen.HeaderText = "Foto";
                        colImagen.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        dataGridView1.Columns.Add(colImagen);

                        dataGridView1.Columns.Add("Nombre", "Nombre");
                        dataGridView1.Columns.Add("Alergenos", "Alérgenos");
                        dataGridView1.RowTemplate.Height = 64;
                    }

                    dataGridView1.Rows.Clear();
                    Image imagenDefault = Image.FromFile("../../Imagenes/trsiteza.jpg"); // Ajusta la ruta
                    imagenDefault = RedimensionarImagen(imagenDefault, 64, 64);

                    while (reader.Read())
                    {
                        string nombre = reader.GetString("Nombre");
                        string alergenos = reader.GetString("Alergenos");
                        byte[] fotoBytes = reader["imagen"] as byte[];

                        Image imagen;

                        if (fotoBytes == null || fotoBytes.Length == 0)
                        {
                            imagen = imagenDefault;
                        }
                        else
                        {
                            try
                            {
                                using (MemoryStream ms = new MemoryStream(fotoBytes))
                                {
                                    imagen = Image.FromStream(ms);
                                    imagen = RedimensionarImagen(imagen, 64, 44);
                                }
                            }
                            catch
                            {
                                imagen = imagenDefault;
                            }
                        }

                        dataGridView1.Rows.Add(imagen, nombre, alergenos);
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

        private Image RedimensionarImagen(Image original, int width, int height)
        {
            Bitmap resized = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.DrawImage(original, 0, 0, width, height);
            }
            return resized;
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
                // Agrega más alérgenos si quieres
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

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuscarRecetas buscarRecetas = new BuscarRecetas(usuarioInicio);
            AbrirFormularioEnPanel(buscarRecetas);
        }

        private void añadirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AñadirRecetas añadirRecetas = new AñadirRecetas(usuarioInicio);
            AbrirFormularioEnPanel(añadirRecetas);
        }

        private void misRecetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecetasDeUsuario recetasDeUsuario = new RecetasDeUsuario(usuarioInicio);
            AbrirFormularioEnPanel(recetasDeUsuario);
        }

        private void mapaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecetasEspaña recetasEspaña=new RecetasEspaña(usuarioInicio);
            AbrirFormularioEnPanel(recetasEspaña);
        }
    }
}
