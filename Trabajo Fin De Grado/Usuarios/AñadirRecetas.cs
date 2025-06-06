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
using Trabajo_Fin_De_Grado_DAM.Clases;

namespace Trabajo_Fin_De_Grado.Usuarios
{
    public partial class AñadirRecetas : Form
    {
        public string usuarioAñadir;
        public AñadirRecetas(string usuario)
        {
            InitializeComponent();

            this.Size = new Size(this.Width + 50, this.Height + 50);
            usuarioAñadir = usuario;
            txtNombre.Text.ToUpper();
            txtIngredientes.Text = "- ";
            txtPreparacion.Text = "- ";
        }

        private void txtPreparacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el "ding" del Enter

                int caretPosition = txtPreparacion.SelectionStart;
                string prefix = "\r\n- ";

                txtPreparacion.Text = txtPreparacion.Text.Insert(caretPosition, prefix);
                txtPreparacion.SelectionStart = caretPosition + prefix.Length;
            }
        }

        private void txtIngredientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el "ding" del Enter

                int caretPosition = txtIngredientes.SelectionStart;
                string prefix = "\r\n- ";

                txtIngredientes.Text = txtIngredientes.Text.Insert(caretPosition, prefix);
                txtIngredientes.SelectionStart = caretPosition + prefix.Length;
            }
        }

        private void guardarReceta(Recetas receta)
        {
            Conexion objetoConexion = new Conexion();
            MySqlConnection conexion = objetoConexion.establecerConexion();

            try
            {
                string query = @"INSERT INTO Recetas 
        (Nombre, Imagen, Ingredientes, Preparacion, Alergenos, Comensales, Usuario) 
        VALUES (@nombre, @Imagen, @ingredientes, @preparacion, @alergenos, @comensales, @usuario)";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", receta.nombre);
                    cmd.Parameters.AddWithValue("@Imagen", receta.imagen);

                    // Unir listas como texto plano
                    string ingredientesTextoPlano = string.Join(";", receta.ingredientes);
                    string preparacionTextoPlano = string.Join(";", receta.preparacion);

                    cmd.Parameters.AddWithValue("@ingredientes", ingredientesTextoPlano);
                    cmd.Parameters.AddWithValue("@preparacion", preparacionTextoPlano);
                    cmd.Parameters.AddWithValue("@alergenos", receta.alergenos);
                    cmd.Parameters.AddWithValue("@comensales", receta.Comensales);
                    cmd.Parameters.AddWithValue("@usuario", receta.usuario);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Receta guardada correctamente.");
                LimpiarFormulario();
            }
            catch
            {
                MessageBox.Show("Error al meter la receta");
            }
            finally
            {
                conexion.Close();
            }

        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            Recetas receta = new Recetas();
            //Nombre
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre de la receta es obligatorio");
            }
            else
            {
                receta.nombre = txtNombre.Text;
            }
            //Ingredientes
            if (string.IsNullOrWhiteSpace(txtIngredientes.Text))
            {
                MessageBox.Show("Debes introducir los ingredientes");
            }
            else
            {
                receta.ingredientes = txtIngredientes.Lines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.TrimStart('-', ' ').Trim())
                .ToList();
            }
            //Preparacion
            if (txtPreparacion.Text == null)
            {
                MessageBox.Show("Debes introducir los pasos para seguir la receta");
            }
            else
            {
                receta.preparacion = txtPreparacion.Lines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.TrimStart('-', ' ').Trim())
                .ToList();
            }
            //Comensales
            if (txtComensales.Text == null)
            {
                MessageBox.Show("Debes introducir para cuantos comensales será la receta");
            }
            else
            {
                receta.Comensales = int.Parse(txtComensales.Text);
            }

            receta.alergenos = string.Join(", ", checkedListBox1.CheckedItems.Cast<string>());
            receta.usuario = usuarioAñadir;

            if (pictureBox1.Image == null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Image imagenDefault = Image.FromFile("../../Imagenes/trsiteza.jpg");
                    imagenDefault.Save(ms, imagenDefault.RawFormat);
                    receta.imagen = ms.ToArray();
                }
            }
            else
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    receta.imagen = ms.ToArray();
                }
            }

            if (ComprobarReceta(receta.nombre))
            {
                MessageBox.Show("Ya existe una receta con ese nombre.");
            }
            else
            {
                guardarReceta(receta);
            }
            
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            txtComensales.Clear();

            txtIngredientes.Clear();
            txtPreparacion.Clear();

            pictureBox1.Visible = false;
            pictureBox1.Image = null;
        }
        private bool ComprobarReceta(string nombreReceta)
        {
            Conexion objetoConexion = new Conexion();
            using (MySqlConnection conexion = objetoConexion.establecerConexion())
            {
                string query = "SELECT COUNT(*) FROM Recetas WHERE LOWER(Nombre) = @nombre";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreReceta.ToLower());

                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                    return cantidad > 0;
                }
            }
        }


        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar imagen";
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                Image imagen = Image.FromFile(openFileDialog.FileName);
                //RedimensionarImagen(imagen, pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = imagen;
                pictureBox1.Visible = true;
            }
        }

        private void AbrirFormularioEnPanel(Form formulario)
        {
            panel1.Controls.Clear(); // Limpiar el panel
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            panel1.Controls.Add(formulario);
            formulario.Show();
        }

        private void atrásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InicioUsuarios inicioUsuarios = new InicioUsuarios(usuarioAñadir);
            AbrirFormularioEnPanel(inicioUsuarios);
        }
    }
}
