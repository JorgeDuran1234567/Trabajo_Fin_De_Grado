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
    public partial class EditarRecetas : Form
    {
        public string usuarioEditar;
        public EditarRecetas(String nombreReceta, String nombreUsuario)
        {
            InitializeComponent();

            this.Size = new Size(this.Width + 50, this.Height + 50);
            usuarioEditar = nombreUsuario;
            lblReceta.Text = nombreReceta;
            lblReceta.Text.ToUpper();
            CargarRecetaParaEditar(lblReceta.Text);
        }

        public void CargarRecetaParaEditar(string nombreReceta)
        {
            Conexion objetoConexion = new Conexion();
            using (MySqlConnection conexion = objetoConexion.establecerConexion())
            {
                string query = @"SELECT Nombre, Ingredientes, Preparacion, Imagen, Comensales, Usuario, Alergenos 
                         FROM Recetas WHERE LOWER(Nombre) = @nombre LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreReceta.ToLower());

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //Nombre
                            lblReceta.Text = reader["Nombre"].ToString();

                            //Ingredientes → separados por línea
                            string ingredientesTexto = reader["Ingredientes"].ToString();
                            var ingredientes = ingredientesTexto
                                .Split(';')
                                .Select(i => i.Trim())
                                .Where(i => !string.IsNullOrWhiteSpace(i))
                                .Select(i => "- " + i);
                            txtIngredientes.Text = string.Join(Environment.NewLine, ingredientes);

                            //Preparación
                            string PreparacionTexto = reader["Preparacion"].ToString();
                            var pasos = PreparacionTexto
                                .Split(';')
                                .Select(i => i.Trim())
                                .Where(i => !string.IsNullOrWhiteSpace(i))
                                .Select(i => "- " + i);
                            txtPreparacion.Text = string.Join(Environment.NewLine, pasos);

                            //Comensales
                            txtComensales.Text = reader["Comensales"].ToString();

                            // Imagen
                            if (!reader.IsDBNull(reader.GetOrdinal("Imagen")))
                            {
                                byte[] imgBytes = (byte[])reader["Imagen"];
                                using (MemoryStream ms = new MemoryStream(imgBytes))
                                {
                                    pictureBox1.Image = Image.FromStream(ms);
                                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                                }
                            }

                            // Alergenos
                            string alergenosTexto = reader["Alergenos"].ToString().ToLower();
                            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                            {
                                string itemTexto = checkedListBox1.Items[i].ToString().ToLower();
                                if (alergenosTexto.Contains(itemTexto))
                                {
                                    checkedListBox1.SetItemChecked(i, true);
                                }
                                else
                                {
                                    checkedListBox1.SetItemChecked(i, false);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la receta.");
                        }
                    }
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Conexion objetoConexion = new Conexion();
            using (MySqlConnection conexion = objetoConexion.establecerConexion())
            {
                string query = @"UPDATE Recetas SET 
                            Ingredientes = @ingredientes,
                            Preparacion = @preparacion,
                            Imagen = @imagen,
                            Comensales = @comensales,
                            Alergenos = @alergenos
                         WHERE LOWER(Nombre) = @nombreOriginal";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    // Ingredientes y preparación separados por ';'
                    string ingredientesTexto = string.Join(";",
                        txtIngredientes.Lines
                            .Select(l => l.TrimStart('-', ' ').Trim())
                            .Where(l => !string.IsNullOrWhiteSpace(l))
                    );

                    string preparacionTexto = string.Join(";",
                        txtPreparacion.Lines
                            .Select(l => l.TrimStart('-', ' ').Trim())
                            .Where(l => !string.IsNullOrWhiteSpace(l))
                    );
                    // Alergenos seleccionados
                    List<string> alergenosSeleccionados = new List<string>();
                    foreach (var item in checkedListBox1.CheckedItems)
                    {
                        alergenosSeleccionados.Add(item.ToString().Trim().ToLower());
                    }

                    string alergenosTexto = string.Join(",", alergenosSeleccionados);

                    // Imagen
                    byte[] imagenBytes;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (Bitmap bmp = new Bitmap(pictureBox1.Image))
                        {
                            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        imagenBytes = ms.ToArray();
                    }

                    // Parámetros
                    cmd.Parameters.AddWithValue("@ingredientes", ingredientesTexto);
                    cmd.Parameters.AddWithValue("@preparacion", preparacionTexto);
                    cmd.Parameters.AddWithValue("@imagen", imagenBytes);
                    cmd.Parameters.AddWithValue("@comensales", txtComensales.Text.Trim());
                    cmd.Parameters.AddWithValue("@alergenos", alergenosTexto);
                    cmd.Parameters.AddWithValue("@nombreOriginal", lblReceta.Text.ToLower());

                    try
                    {
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Receta actualizada correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la receta para actualizar.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar la receta:\n" + ex.Message);
                    }
                }
            }
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar imagen";
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image imagen = Image.FromFile(openFileDialog.FileName);
                pictureBox1.Image = imagen;
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
            RecetasDeUsuario recetasDeUsuario = new RecetasDeUsuario(usuarioEditar);
            AbrirFormularioEnPanel(recetasDeUsuario);
        }
    }
}
