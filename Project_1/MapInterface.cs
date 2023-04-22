using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using FontAwesome.Sharp;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;


namespace Project_1
{
    public partial class MapInterface : Form
    {
        public static Color color1 = Color.FromArgb(52, 192, 215);
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        DataTable dt;

        int filaselecionada = 0;
        double LatInicial = 41.2985227506962;
        double LngInicial = 2.083493712899025;

        public void Load_Data_Grid_View(DataTable newtable)
        {
            //dataGridView1.DataSource = newtable;
            //dataGridView1.Update();
            //dataGridView1.Refresh();
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        public MapInterface()
        {
            InitializeComponent();
            


        }


        private void button1_Click(object sender, EventArgs e)
        {


            //dataGridView1 = this.dt;
            //dataGridView1.Update();

            //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }

        private void MapInterface_Load(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInicial, LngInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 15;
            gMapControl1.AutoScroll = true;
            markerOverlay = new GMapOverlay("Marker");
            marker = new GMarkerGoogle(new PointLatLng(41.29824003968431, 2.0679504387537717), GMarkerGoogleType.red);
            markerOverlay.Markers.Add(marker);
            gMapControl1.Overlays.Add(markerOverlay);
        }

        private void iconPictureBox1_MouseEnter(object sender, EventArgs e)
        {
            iconPictureBox1.IconColor = color1;
        }

        private void iconPictureBox1_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox1.IconColor = Color.White;
        }

        private void iconPictureBox2_MouseEnter(object sender, EventArgs e)
        {
            iconPictureBox2.IconColor = color1;
        }

        private void iconPictureBox2_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox2.IconColor = Color.White;
        }
    }
}
