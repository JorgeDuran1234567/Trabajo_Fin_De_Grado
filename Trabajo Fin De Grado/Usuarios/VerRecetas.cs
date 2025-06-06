using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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
using Trabajo_Fin_De_Grado_DAM.Clases;

namespace Trabajo_Fin_De_Grado.Usuarios
{
    public partial class VerRecetas : Form
    {
        public VerRecetas(string receta)
        {
            InitializeComponent();

            lblNombre.Text = receta;
            mostrarDatos(lblNombre.Text);
        }

        public void mostrarDatos(string nombreReceta)
        {
            Conexion objetoConexion = new Conexion();

            using (MySqlConnection conexion = objetoConexion.establecerConexion())
            {
                string query = @"SELECT Preparacion 
                 FROM Recetas 
                 WHERE LOWER(Nombre) = @nombre 
                 LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreReceta.ToLower());

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string preparacionTexto = reader["Preparacion"]?.ToString() ?? "";
                            var pasos = preparacionTexto
                                .Split(';')
                                .Select(p => p.Trim())
                                .Where(p => !string.IsNullOrWhiteSpace(p))
                                .Select(p => "- " + p);
                            lblPreparacion.Text = string.Join(Environment.NewLine, pasos);
                        }
                        else
                        {
                            lblPreparacion.Text = "Receta no encontrada.";
                        }
                    }
                }
            }
        }
    }
}
