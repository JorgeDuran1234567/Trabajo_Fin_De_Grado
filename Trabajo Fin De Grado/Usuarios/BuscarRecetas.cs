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
using Newtonsoft.Json;
using System.Windows.Forms;
using Trabajo_Fin_De_Grado.Clases;
using Trabajo_Fin_De_Grado_DAM.Clases;

namespace Trabajo_Fin_De_Grado.Usuarios
{
    public partial class BuscarRecetas : Form
    {
        List<Recetas> recetasCargadas = new List<Recetas>();
        Recetas recetaAcual;
        public string usuarioBuscar, nombreReceta;
        public BuscarRecetas(string usuario)
        {
            InitializeComponent();
            this.Size = new Size(this.Width + 50, this.Height + 50);

            usuarioBuscar = usuario;
            

            CargarRecetasEnDataGridView();
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is System.Windows.Forms.CheckBox chk)
                {
                    chk.CheckedChanged += (s, e) => FiltrarRecetas();
                }
            }
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkCyan;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.ClearSelection();
        }

        private void CargarRecetasEnDataGridView()
        {
            Conexion objetoConexion = new Conexion();
            MySqlConnection conexion = objetoConexion.establecerConexion();

            try
            {
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
                    Image imagenDefault = Image.FromFile("../../Imagenes/trsiteza.jpg");
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
        private void FiltrarRecetas()
        {
            string filtroTexto = txtBuscar.Text.Trim().ToLower();

            // Palabras clave desde el TextBox
            string[] palabrasClave = filtroTexto
                .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim().ToLower())
                .ToArray();

            // Alergenos seleccionados desde los CheckBox
            List<string> alergenosSeleccionados = new List<string>();
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is System.Windows.Forms.CheckBox chk && chk.Checked)
                {
                    alergenosSeleccionados.Add(chk.Text.Trim().ToLower());
                }
            }

            Conexion objetoConexion = new Conexion();
            var conexion = objetoConexion.establecerConexion();

            List<string> condiciones = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;

            int paramIndex = 0;

            // Filtro por palabras clave (ingredientes)
            foreach (string palabra in palabrasClave)
            {
                condiciones.Add($"LOWER(ingredientes) LIKE @param{paramIndex}");
                cmd.Parameters.AddWithValue($"@param{paramIndex}", $"%{palabra}%");
                paramIndex++;
            }

            // Filtro por alérgenos seleccionados
            foreach (string alergeno in alergenosSeleccionados)
            {
                condiciones.Add($"LOWER(alergenos) NOT LIKE @param{paramIndex}");
                cmd.Parameters.AddWithValue($"@param{paramIndex}", $"%{alergeno}%");
                paramIndex++;
            }

            string query = "SELECT nombre, imagen, alergenos FROM Recetas";
            if (condiciones.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", condiciones);
            }

            cmd.CommandText = query;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conexion.Close();

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
            recetasCargadas.Clear();

            Image imagenDefault = Image.FromFile("../../Imagenes/trsiteza.jpg");
            imagenDefault = RedimensionarImagen(imagenDefault, 64, 64);

            foreach (DataRow row in dt.Rows)
            {
                Image imagen;
                byte[] fotoBytes = row["imagen"] as byte[];

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

                string nombre = row["nombre"].ToString();
                string alergenos = row["alergenos"].ToString();

                Recetas receta = new Recetas
                {
                    nombre = nombre,
                    alergenos = alergenos,
                    imagen = fotoBytes
                };
                recetasCargadas.Add(receta);

                dataGridView1.Rows.Add(imagen, nombre, alergenos);
            }

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron recetas con los filtros aplicados.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dataGridView1.Sort(dataGridView1.Columns["Nombre"], ListSortDirection.Ascending);
                dataGridView1.ClearSelection();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                nombreReceta = row.Cells["Nombre"].Value?.ToString();

                //Mostrar ingredientes (separado por comas)
                try
                {
                    List<string> ingredientes = ObtenerIngredientes(nombreReceta);

                    if (ingredientes.Count > 0)
                    {
                        label1.Text = "INGREDIENTES:\n" + string.Join("\n• ", ingredientes.Prepend(""));
                    }
                    else
                    {
                        label1.Text = "Ingredientes no encontrados para la receta seleccionada.";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener los ingredientes:\n" + ex.Message);
                    label1.Text = "Error al obtener los ingredientes.";
                }

                btnVerReceta.Visible = true;

                //Obtener la imagen desde la base de datos
                try
                {
                    Conexion objetoConexion = new Conexion();
                    using (MySqlConnection conexion = objetoConexion.establecerConexion())
                    {
                        string query = "SELECT imagen FROM Recetas WHERE LOWER(nombre) = @nombre LIMIT 1";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@nombre", nombreReceta.ToLower());

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read() && !reader.IsDBNull(0))
                                {
                                    byte[] imgBytes = (byte[])reader["imagen"];
                                    using (MemoryStream ms = new MemoryStream(imgBytes))
                                    {
                                        Image imagenCompleta = Image.FromStream(ms);
                                        pictureBox1.Image = imagenCompleta;
                                        pictureBox1.Visible = true;
                                        btnVerReceta.Visible=true;
                                    }
                                }
                                else
                                {
                                    pictureBox1.Image = Image.FromFile("../../Imagenes/trsiteza.jpg");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen: " + ex.Message);
                    pictureBox1.Image = Image.FromFile("../../Imagenes/trsiteza.jpg");
                }
            }
        }

        public List<string> ObtenerIngredientes(string nombreReceta)
        {
            Conexion objetoConexion = new Conexion();
            using (MySqlConnection conexion = objetoConexion.establecerConexion())
            {
                try
                {
                    string query = "SELECT ingredientes FROM Recetas WHERE LOWER(nombre) = @nombre LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombreReceta.ToLower());

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string ingredientesTexto = reader.IsDBNull(0) ? "" : reader.GetString(0);

                                // Separar por ';' y luego dividir cada uno por ',' si aplica
                                return ingredientesTexto
                                    .Split(';')
                                    .Select(i =>
                                    {
                                        var partes = i.Split(',');
                                        string nombre = partes.ElementAtOrDefault(0)?.Trim();
                                        string cantidad = partes.ElementAtOrDefault(1)?.Trim();

                                        return string.IsNullOrEmpty(cantidad)
                                            ? nombre
                                            : $"{nombre}, {cantidad}";
                                    })
                                    .Where(i => !string.IsNullOrWhiteSpace(i))
                                    .ToList();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener los ingredientes: " + ex.Message, ex);
                }
            }

            return new List<string>();
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

        private void AbrirFormularioEnPanel(Form formulario)
        {
            panel1.Controls.Clear();
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            panel1.Controls.Add(formulario);
            formulario.Show();
        }

        private void btnVerReceta_Click(object sender, EventArgs e)
        {
            VerRecetas verRecetas = new VerRecetas(nombreReceta);
            verRecetas.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FiltrarRecetas();
        }

        private void atrásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InicioUsuarios inicioUsuarios = new InicioUsuarios(usuarioBuscar);
            AbrirFormularioEnPanel(inicioUsuarios);
        }
    }
}
