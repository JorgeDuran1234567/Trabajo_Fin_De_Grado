using Microsoft.Win32;
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
using Trabajo_Fin_De_Grado.Clases;
using MySql.Data.MySqlClient;
using Trabajo_Fin_De_Grado.Usuarios;
using Trabajo_Fin_De_Grado.Administrador;

namespace Trabajo_Fin_De_Grado
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
            //CargarControlInicio();
            CargarControlEnPanel(new InicioCU(this));
        }

        public void CargarControlEnPanel(UserControl control)
        {
            panel1.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panel1.Controls.Add(control);
        }

        public void AbrirFormularioEnPanel(Form formulario)
        {
            panel1.Controls.Clear(); // Limpiar el panel
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            panel1.Controls.Add(formulario);
            formulario.Show();
        }
    }
}
