using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trabajo_Fin_De_Grado.Clases;

namespace Trabajo_Fin_De_Grado
{
    public partial class RegistroCU : UserControl
    {
        private Inicio formularioPadre;
        public RegistroCU(Inicio form)
        {
            InitializeComponent();
            formularioPadre = form;
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellidos.Text) ||
            string.IsNullOrWhiteSpace(cmbProvincia.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("Debes rellenar todos los datos");
                return;
            }

            if (txtContraseña.Text != txtContraseña2.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden");
                return;
            }

            try
            {
                if (comprobarUsuario())
                {
                    MessageBox.Show("Ya existe un usuario con los mismos datos");
                    return;
                }
                Conexion objetoConexion = new Conexion();
                using (MySqlConnection conexion = objetoConexion.establecerConexion())
                {
                    string query = @"INSERT INTO usuarios (nombre, apellidos, provincia, contraseña)
                             VALUES (@nombre, @apellidos, @provincia, @contraseña)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@apellidos", txtApellidos.Text.Trim());
                        cmd.Parameters.AddWithValue("@provincia", cmbProvincia.Text.Trim());
                        cmd.Parameters.AddWithValue("@contraseña", txtContraseña.Text);

                        int filas = cmd.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            MessageBox.Show("Nuevo usuario creado correctamente");
                            limpiarDatos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo crear el usuario");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al introducir el usuario: " + ex.Message);
            }
        }

        private Boolean comprobarUsuario()
        {
            Conexion objetoConexion = new Conexion();
            using (MySqlConnection conexion = objetoConexion.establecerConexion())
            {
                string query = "SELECT Nombre, Apellidos, Provincia FROM usuarios";
                using (MySqlCommand myCommand = new MySqlCommand(query, conexion))
                using (MySqlDataReader reader = myCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nombre = reader.GetString("Nombre");
                        string apellidos = reader.GetString("Apellidos");
                        string provincia = reader.GetString("Provincia");

                        if (nombre == txtNombre.Text && apellidos == txtApellidos.Text && provincia == cmbProvincia.Text)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void limpiarDatos()
        {
            txtNombre.Text = "";
            txtApellidos.Text = "";
            txtContraseña.Text = "";
            txtContraseña2.Text = "";
            cmbProvincia.Text = "";
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            formularioPadre.CargarControlEnPanel(new InicioCU(formularioPadre));
        }
    }
}
