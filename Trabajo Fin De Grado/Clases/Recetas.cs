using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_Fin_De_Grado_DAM.Clases
{
    internal class Recetas
    {
        public string nombre { get; set; }
        public byte[] imagen { get; set; }
        //public Foto foto { get; set; }
        public List<string> ingredientes { get; set; }
        public List<string> preparacion { get; set; }
        public int Comensales { get; set; }
        public string alergenos { get; set; }
        public string usuario { get; set; }
    }
}
