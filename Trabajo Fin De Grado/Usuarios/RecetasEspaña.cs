using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trabajo_Fin_De_Grado.Clases;

namespace Trabajo_Fin_De_Grado.Usuarios
{
    public partial class RecetasEspaña : Form
    {
        public string usuarioEspaña;
        public RecetasEspaña(string usuario)
        {
            InitializeComponent();

            this.Size = new Size(this.Width + 50, this.Height + 30);
            menuStrip1.BringToFront();
            usuarioEspaña = usuario;
        }

        private void mapa_MouseClick(object sender, MouseEventArgs e)
        {
            Point clickPoint = MapearCoordenadasClick(e, mapa);
            if (clickPoint == Point.Empty)
                return;

            // GALICIA
            Point[] Galicia = new Point[]
            {
        new Point(123,55),new Point(136,152),new Point(176,133),new Point(176,133),new Point(176,156),
        new Point(238,150),new Point(253,121),new Point(238,109),new Point(238,96),new Point(253,85),
        new Point(235,37),new Point(199,19)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(Galicia);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "GALICIA";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // ASTURIAS
            Point[] Asturias = new Point[]
            {
        new Point(235,37),new Point(253,85),new Point(342,73), new Point(372,55),new Point(307,33)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(Asturias);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "ASTURIAS";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // CANTABRIA
            Point[] Cantabria = new Point[]
            {
        new Point(327,55),new Point(356,66),new Point(364,81),new Point(382,82),new Point(394,97),
        new Point(412,97),new Point(406,81),new Point(446,59),new Point(427,46)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(Cantabria);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "CANTABRIA";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // PAIS VASCO
            Point[] PaisVasco = new Point[]
            {
        new Point(446,59),new Point(429,72),new Point(449,72),new Point(449,95),new Point(482,119),
        new Point(521,56)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(PaisVasco);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "PAIS VASCO";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // NAVARRA
            Point[] Navarra = new Point[]
            {
        new Point(521,56),new Point(482,119),new Point(525,138),new Point(515,150),new Point(542,158),
        new Point(555,142),new Point(541,132),new Point(576,85)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(Navarra);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "NAVARRA";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // NAVARRA
            Point[] Aragon = new Point[]
            {
        new Point(576,85),new Point(541,132),new Point(555,142),new Point(542,158),new Point(515,150),
        new Point(521,174),new Point(503,192),new Point(503,212),new Point(530,225),new Point(533,251),
        new Point(522,265),new Point(541,281),new Point(558,294),new Point(576,293),new Point(604,260),
        new Point(601,246),new Point(610,237),new Point(630,239),new Point(653,101)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(Aragon);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "ARAGON";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // CATALUÑA
            Point[] Cataluña = new Point[]
            {
        new Point(653,101),new Point(630,239),new Point(649,254),new Point(669,239),new Point(659,234),
        new Point(671,218),new Point(753,195),new Point(793,151),new Point(793,112),new Point(695,115),
        new Point(689,96),new Point(650,88)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(Cataluña);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "CATALUÑA";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // LA RIOJA
            Point[] laRioja = new Point[]
            {
        new Point(482,119),new Point(525,138),new Point(515,150),new Point(510,158),new Point(490,144),
        new Point(477,152),new Point(467,150),new Point(447,140),new Point(451,107)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(laRioja);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "LA RIOJA";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // VALENCIA
            Point[] Valencia = new Point[]
            {
        new Point(649,254),new Point(630,239),new Point(610,237),new Point(601,246),new Point(604,260),
        new Point(576,293),new Point(558,294),new Point(552,311),new Point(538,328),new Point(539,335),
        new Point(557,340),new Point(552,357),new Point(559,367),new Point(571,366),new Point(572,385),
        new Point(565,389),new Point(562,408),new Point(568,419),new Point(566,430),new Point(581,441),
        new Point(601,406),new Point(637,379),new Point(613,359),new Point(605,327)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(Valencia);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "COMUNIDAD VALENCIANA";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // MURCIA
            Point[] Murcia = new Point[]
            {
        new Point(581,441),new Point(566,430),new Point(568,419),new Point(562,407),new Point(566,388),
        new Point(557,381),new Point(541,385),new Point(540,405),new Point(532,413),new Point(526,406),
        new Point(489,430),new Point(497,439),new Point(511,444),new Point(509,456),new Point(521,472),
        new Point(530,479),new Point(551,464),new Point(585,458)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(Murcia);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "MURCIA";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // ANDALUCIA
            Point[] Andalucia = new Point[]
            {
        new Point(530,479),new Point(521,472),new Point(509,456),new Point(511,444),new Point(489,430),
        new Point(479,426),new Point(485,415),new Point(475,396),new Point(382,405),new Point(338,382),
        new Point(330,382),new Point(306,401),new Point(309,415),new Point(299,421),new Point(298,415),
        new Point(285,420),new Point(282,428),new Point(263,430),new Point(258,424),new Point(226,412),
        new Point(222,423),new Point(207,427),new Point(191,454),new Point(194,482),new Point(217,482),
        new Point(251,514),new Point(271,556),new Point(301,570),new Point(325,543),new Point(355,537),
        new Point(371,523),new Point(473,526),new Point(484,515),new Point(502,522)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(Andalucia);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "ANDALUCIA";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // EXTREMADURA
            Point[] Extremadura = new Point[]
            {
        new Point(338,382),new Point(330,382),new Point(306,401),new Point(309,415),new Point(299,421),
        new Point(298,415),new Point(285,420),new Point(282,428),new Point(263,430),new Point(258,424),
        new Point(226,412),new Point(219,415),new Point(206,395),new Point(209,375),new Point(228,356),
        new Point(219,348),new Point(199,309),new Point(227,309),new Point(231,270),new Point(255,270),
        new Point(277,254),new Point(291,269),new Point(304,271),new Point(312,276),new Point(323,272),
        new Point(320,297),new Point(325,300),new Point(323,308),new Point(333,306),new Point(330,320),
        new Point(346,336),new Point(358,331)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(Extremadura);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "EXTREMADURA";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // CASTILLA Y LEON
            Point[] CastillaYLeon = new Point[]
            {
        new Point(323,272),new Point(312,276),new Point(304,271),new Point(291,269),new Point(277,254),
        new Point(255,270),new Point(240,266),new Point(238,215),new Point(279,176),new Point(271,170),
        new Point(262,171),new Point(262,150),new Point(238,150),new Point(253,121),new Point(238,109),
        new Point(238,96),new Point(253,85),new Point(342,73),new Point(356,66),new Point(364,81),
        new Point(382,82),new Point(394,97),new Point(412,97),new Point(406,81),new Point(429,72),
        new Point(449,72),new Point(449,95),new Point(461,105),new Point(452,107),new Point(447,140),
        new Point(467,150),new Point(477,152),new Point(483,144),new Point(490,144),new Point(510,158),
        new Point(518,158),new Point(521,174),new Point(503,192),new Point(503,212),new Point(486,217),
        new Point(464,199),new Point(440,201),new Point(404,201),new Point(362,275),new Point(361,269),
        new Point(343,280),new Point(323,281)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(CastillaYLeon);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "CASTILLA Y LEON";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // MADRID
            Point[] Madrid = new Point[]
            {
        new Point(362,371),new Point(371,275),new Point(390,271),new Point(420,286),new Point(405,298),
        new Point(428,289),new Point(451,288),new Point(446,261),new Point(427,240),new Point(431,222),
        new Point(424,212),new Point(404,223)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(Madrid);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "MADRID";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // CASTILLA LA MANCHA
            Point[] CastillaLaMancha = new Point[]
            {
        new Point(503,212),new Point(530,225),new Point(533,251),new Point(522,265),new Point(541,281),
        new Point(558,294),new Point(552,311),new Point(538,328),new Point(539,335),new Point(557,340),
        new Point(552,357),new Point(559,367),new Point(571,366),new Point(572,385),new Point(565,389),
        new Point(557,381),new Point(541,385),new Point(540,405),new Point(532,413),new Point(526,406),
        new Point(489,430),new Point(479,426),new Point(485,415),new Point(475,396),new Point(382,405),
        new Point(338,892),new Point(358,331),new Point(346,336),new Point(330,320),new Point(333,308),
        new Point(325,300),new Point(320,297),new Point(323,281),new Point(343,280),new Point(361,269),
        new Point(362,275),new Point(390,275),new Point(390,271),new Point(420,286),new Point(405,298),
        new Point(428,298),new Point(451,288),new Point(446,261),new Point(427,240),new Point(431,222),
        new Point(424,223),new Point(440,201),new Point(464,201),new Point(486,217)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(CastillaLaMancha);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "CASTILLA LA MANCHA";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // CANARIAS
            Point[] ICanarias = new Point[]
            {
        new Point(251,545),new Point(251,652),new Point(1,650),new Point(5,545)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(ICanarias);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "ISLAS CANARIAS";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            // BALEARES
            Point[] IBaleares = new Point[]
            {
        new Point(859,239),new Point(873,298),new Point(709,393),new Point(672,256)
            };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(IBaleares);

                if (path.IsVisible(clickPoint))
                {
                    lblComunidad.Text = "ISLAS BALEARES";
                    cargarDatosYRecetasPorComunidad();
                    return;
                }
            }

            lblComunidad.Text = "ESPAÑA";
            lblHistoria.Visible = false;
            dataGridView1.Visible = false;
        }

        private Point MapearCoordenadasClick(MouseEventArgs e, PictureBox pb)
        {
            if (pb.Image == null)
                return e.Location;

            int pbWidth = pb.Width;
            int pbHeight = pb.Height;

            int imgWidth = pb.Image.Width;
            int imgHeight = pb.Image.Height;

            Point click = e.Location;

            if (pb.SizeMode == PictureBoxSizeMode.StretchImage)
            {
                // Mapea el click del PictureBox (escalado) a la imagen original
                int x = click.X * imgWidth / pbWidth;
                int y = click.Y * imgHeight / pbHeight;
                return new Point(x, y);
            }
            else if (pb.SizeMode == PictureBoxSizeMode.Zoom)
            {
                float ratioWidth = (float)pbWidth / imgWidth;
                float ratioHeight = (float)pbHeight / imgHeight;
                float ratio = Math.Min(ratioWidth, ratioHeight);

                int displayedWidth = (int)(imgWidth * ratio);
                int displayedHeight = (int)(imgHeight * ratio);

                int offsetX = (pbWidth - displayedWidth) / 2;
                int offsetY = (pbHeight - displayedHeight) / 2;

                int x = (int)((click.X - offsetX) / ratio);
                int y = (int)((click.Y - offsetY) / ratio);

                // Opcional: controlar si click está fuera del área de la imagen visible
                if (x < 0 || x >= imgWidth || y < 0 || y >= imgHeight)
                    return Point.Empty;

                return new Point(x, y);
            }
            else
            {
                // Otros modos: Normal, AutoSize, CenterImage
                return click;
            }
        }

        private void cargarDatosYRecetasPorComunidad()
        {
            // Leer archivo JSON
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../Archivos/ComunidadesAutonomas.json");
            var comunidades = JsonConvert.DeserializeObject<List<ComunidadesA>>(File.ReadAllText(ruta));

            var comunidadSeleccionada = comunidades
                .FirstOrDefault(c => c.nombre.Equals(lblComunidad.Text, StringComparison.OrdinalIgnoreCase));

            if (comunidadSeleccionada == null)
            {
                MessageBox.Show("No se encontró la comunidad en el archivo JSON.");
                return;
            }

            lblHistoria.Text = comunidadSeleccionada.historia;

            // Crear DataTable para combinar resultados
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Recetas", typeof(string));

            // Añadir recetas del archivo JSON
            foreach (var receta in comunidadSeleccionada.recetas)
            {
                string recetaFormateada = char.ToUpper(receta[0]) + receta.Substring(1).ToLower();
                tabla.Rows.Add(recetaFormateada);
                //tabla.Rows.Add(receta);
            }

            // Mapa provincias → comunidades
            var mapa = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["Almería"] = "ANDALUCIA",
                ["Cádiz"] = "ANDALUCIA",
                ["Huelva"] = "ANDALUCIA",
                ["Granada"] = "ANDALUCIA",
                ["Córdoba"] = "ANDALUCIA",
                ["Jaén"] = "ANDALUCIA",
                ["Málaga"] = "ANDALUCIA",
                ["Sevilla"] = "ANDALUCIA",
                ["Huesca"] = "ARAGON",
                ["Teruel"] = "ARAGON",
                ["Zaragoza"] = "ARAGON",
                ["Las Palmas"] = "ISLAS CANARIAS",
                ["Tenerife"] = "ISLAS CANARIAS",
                ["Cantabria"] = "CANTABRIA",
                ["Ávila"] = "CASTILLA Y LEON",
                ["Burgos"] = "CASTILLA Y LEON",
                ["León"] = "CASTILLA Y LEON",
                ["Soria"] = "CASTILLA Y LEON",
                ["Palencia"] = "CASTILLA Y LEON",
                ["Salamanca"] = "CASTILLA Y LEON",
                ["Segovia"] = "CASTILLA Y LEON",
                ["Valladolid"] = "CASTILLA Y LEON",
                ["Zamora"] = "CASTILLA Y LEON",
                ["Albacete"] = "CASTILLA LA MANCHA",
                ["Ciudad Real"] = "CASTILLA LA MANCHA",
                ["Toledo"] = "CASTILLA LA MANCHA",
                ["Guadalajara"] = "CASTILLA LA MANCHA",
                ["Cuenca"] = "CASTILLA LA MANCHA",
                ["Barcelona"] = "CATALUÑA",
                ["Girona"] = "CATALUÑA",
                ["Lleida"] = "CATALUÑA",
                ["Tarragona"] = "CATALUÑA",
                ["Madrid"] = "MADRID",
                ["Valencia"] = "COMUNIDAD VALENCIANA",
                ["Alicante"] = "COMUNIDAD VALENCIANA",
                ["Castellón"] = "COMUNIDAD VALENCIANA",
                ["Badajoz"] = "EXTREMADURA",
                ["Cáceres"] = "EXTREMADURA",
                ["A Coruña"] = "GALICIA",
                ["Lugo"] = "GALICIA",
                ["Ourense"] = "GALICIA",
                ["Pontevedra"] = "GALICIA",
                ["Islas Baleares"] = "ISLAS BALEARES",
                ["La Rioja"] = "LA RIOJA",
                ["Navarra"] = "NAVARRA",
                ["Álava"] = "PAIS VASCO",
                ["Vizcaya"] = "PAIS VASCO",
                ["Guipúzcoa"] = "PAIS VASCO",
                ["Asturias"] = "ASTURIAS",
                ["Murcia"] = "MURCIA"
            };

            // Obtener provincias asociadas
            var provincias = mapa
                .Where(kv => kv.Value.Equals(lblComunidad.Text, StringComparison.OrdinalIgnoreCase))
                .Select(kv => kv.Key)
                .ToList();

            if (provincias.Count == 0)
            {
                MessageBox.Show("No se encontraron provincias para esa comunidad.");
                return;
            }

            // Leer recetas de la base de datos
            Conexion objetoConexion = new Conexion();
            using (MySqlConnection conexion = objetoConexion.establecerConexion())
            {
                try
                {
                    string inClause = string.Join(", ", provincias.Select((p, i) => $"@prov{i}"));
                    string query = $@"
                SELECT r.nombre
                FROM Recetas r
                INNER JOIN usuarios u ON r.usuario = u.nombre
                WHERE u.provincia IN ({inClause})";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        for (int i = 0; i < provincias.Count; i++)
                        {
                            cmd.Parameters.AddWithValue($"@prov{i}", provincias[i]);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dbRecetas = new DataTable();
                            adapter.Fill(dbRecetas);

                            // Agregar recetas de la base de datos al DataTable
                            foreach (DataRow row in dbRecetas.Rows)
                            {
                                string recetaBD = row["nombre"].ToString();
                                string recetaFormateada = char.ToUpper(recetaBD[0]) + recetaBD.Substring(1).ToLower();
                                tabla.Rows.Add(recetaFormateada);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar recetas desde la base de datos: " + ex.Message);
                }
            }
            lblHistoria.Visible = true;
            dataGridView1.Visible = true;
            // Asignar DataTable final al DataGridView
            dataGridView1.DataSource = tabla;
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
            InicioUsuarios inicioUsuarios = new InicioUsuarios(usuarioEspaña);
            AbrirFormularioEnPanel(inicioUsuarios);
        }
    }
}
