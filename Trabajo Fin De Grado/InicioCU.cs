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
using Trabajo_Fin_De_Grado.Administrador;
using Trabajo_Fin_De_Grado.Clases;
using Trabajo_Fin_De_Grado.Usuarios;

namespace Trabajo_Fin_De_Grado
{
    public partial class InicioCU : UserControl
    {
        private Inicio formularioPadre;
        public InicioCU(Inicio form)
        {
            InitializeComponent();
            formularioPadre = form;
        }
        public void CargarControlEnPanel(UserControl control)
        {
            panel1.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panel1.Controls.Add(control);
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            Form formularioPadre = this.FindForm();
            try
            {

                if (txtUsuario.Text == "admin" && txtContraseña.Text == "admin")
                {
                    string nombreAdmin = txtUsuario.Text;
                    InicioAdministrador inicioAdministrador = new InicioAdministrador();
                    inicioAdministrador.FormClosed += (s, args) => formularioPadre.Close();
                    inicioAdministrador.Show();
                    formularioPadre.Hide();
                }

                else
                {
                    string query = "SELECT * FROM usuarios WHERE nombre = '" + txtUsuario.Text + "' AND contraseña = " + txtContraseña.Text + ";";

                    Conexion objetoConexion = new Conexion();
                    MySqlCommand myCommand = new MySqlCommand(query, objetoConexion.establecerConexion());
                    MySqlDataReader reader = myCommand.ExecuteReader();


                    if (reader.HasRows)
                    {
                        string nombreUsuarios = txtUsuario.Text;
                        InicioUsuarios inicioUsuarios = new InicioUsuarios(nombreUsuarios);

                        inicioUsuarios.FormClosed += (s, args) => formularioPadre.Close();
                        inicioUsuarios.Show();

                        formularioPadre.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o Contraseña incorrectos");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("El usuario introducido no existe");
            }
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            formularioPadre.CargarControlEnPanel(new RegistroCU(formularioPadre));
        }
    }
}
