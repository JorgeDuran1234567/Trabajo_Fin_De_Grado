using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabajo_Fin_De_Grado.Clases
{
    internal class Conexion
    {
        MySqlConnection conexion = new MySqlConnection();
        static string servidor = "127.0.0.1";
        static string db = "finaldam";
        static string usuario = "admin";
        static string password = "admin";
        static string puerto = "3306";
        string cadenaConexion = "server=" + servidor + "; port=" + puerto + "; user id=" + usuario
        + "; password=" + password + "; database=" + db + ";";
        public MySqlConnection establecerConexion()
        {
            try
            {
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar a la base de datos, error: " + ex.ToString());
            }
            return conexion;
        }
        public void cerrarConexion()
        {
            conexion.Close();
        }
    }
}
