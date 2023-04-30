using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using FontAwesome.Sharp;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using iTextSharp.text;
using MultiCAT6.Utils;
using Org.BouncyCastle.Crypto.Macs;


namespace Project_1
{
    public partial class MapInterface : Form
    {
        Conversions convertor = new Conversions();
        public DataTable MapCAT10 = new DataTable();
        public DataTable MapCAT21 = new DataTable();
        public static Color color1 = Color.FromArgb(52, 192, 215);
        GMarkerGoogle marker;
        GMarkerGoogle marker2;
        GMapOverlay markerOverlay;
        DataTable dt;

        int filaselecionada = 0;
        double LatInicial = 41.2985227506962;
        double LngInicial = 2.083493712899025;
        CoordinatesWGS84 AirportGeodesic = new CoordinatesWGS84();


        // We find the directory where the application is running to know the path to the icons that we will use
        static string directory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BlackMarker.png");
        Bitmap bmpMarker = (Bitmap)System.Drawing.Image.FromFile(System.IO.Path.Combine(directory, "Images", "BlackMarker.png"));
        Bitmap bmpMarker2 = (Bitmap)System.Drawing.Image.FromFile(System.IO.Path.Combine(directory, "Images", "RedMarker.png"));
        Bitmap bmpMarker3 = (Bitmap)System.Drawing.Image.FromFile(System.IO.Path.Combine(directory, "Images", "BlueMarker.png"));
        Bitmap markerCAT10;
        //Bitmap bmpMarker = new Bitmap(@"C:\Users\HP\Desktop\UPC-EETAC\4. QUART CURS\4B\Projecte en Gestió del Trànsit Aeri\Project_1\Project_1\Images\BlackMarker.png");
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
            this.AutoScaleMode = AutoScaleMode.Dpi;



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
            //markerOverlay = new GMapOverlay("Marker");
            //marker = new GMarkerGoogle(new PointLatLng(41.286941, 2.007218), GMarkerGoogleType.red);
            //markerOverlay.Markers.Add(marker);
            //gMapControl1.Overlays.Add(markerOverlay);
            gMapControl1.OnMarkerClick += new MarkerClick(gMapControl1_OnMarkerClick);





        }
        
        public void getMapPointsCAT10(DataTable MAP)
        {
            this.MapCAT10 = MAP;
        }

        public void getMapPointsCAT21(DataTable MAP)
        {
            this.MapCAT21 = MAP;

            
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
        public double latitude21;
        public double longitude21;

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            int i = 0;
            markerOverlay = new GMapOverlay("Marker");
            while (i < this.MapCAT21.Rows.Count)
            {
                if (i == 398)
                {
                    int a = i;
                }
                try
                {
                    string description = this.MapCAT21.Rows[i]["Position in WGS-84 Co-ordinates"].ToString();
                    string[] lines = description.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    string firstLine = lines[0];
                    string secondLine = lines[1];
                    this.latitude21 = convertor.DMSToDD_Latitude(firstLine.Substring("Latitude= ".Length).Trim(), i);
                    this.longitude21 = convertor.DMSToDD_Longitude(secondLine.Substring("Longitude= ".Length).Trim());
                    marker = new GMarkerGoogle(new PointLatLng(this.latitude21, this.longitude21), bmpMarker);
                    marker.ToolTipText = $"Target ID: {this.MapCAT21.Rows[i]["Target_ID"]}, \nTarget Address: {this.MapCAT21.Rows[i]["Target_Address"]}, \nTrack Number: {this.MapCAT21.Rows[i]["Track Number"]}, \nMode 3/A Code: {this.MapCAT21.Rows[i]["Mode_3A_Code"]}, \nFlight Level: {this.MapCAT21.Rows[i]["Flight Level"]}, \nLatitude: {this.latitude21}, \nLongitude: {this.longitude21}";

                }
                catch { }

                markerOverlay.Markers.Add(marker);

                i = i + 1;
            }

            int x = 0;
            while (x < this.MapCAT10.Rows.Count)
            {
                string description2 = this.MapCAT10.Rows[x]["Position in Cartesian Co-ordinates"].ToString();
                string[] lines2 = description2.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                string firstLine2 = lines2[0];
                string secondLine2 = lines2[1];
                string latitude10 = firstLine2.Substring("X= ".Length).Trim();
                string longitude10 = secondLine2.Substring("Y= ".Length).Trim();

                if (this.MapCAT10.Rows[x]["SIC"].ToString() == "7")
                {
                    // SMR
                    this.AirportGeodesic = new CoordinatesWGS84(41.29561833 * (Math.PI / 180), 2.095114167 * (Math.PI / 180));
                    this.markerCAT10 = bmpMarker2;
                }
                else
                {
                    // ARP
                    this.AirportGeodesic = new CoordinatesWGS84(41.2970767 * (Math.PI / 180), 2.07846278 * (Math.PI / 180));
                    this.markerCAT10 = bmpMarker3;
                }


                try
                {
                    CoordinatesXYZ ObjectCartesian = new CoordinatesXYZ(Convert.ToDouble(latitude10.Substring(0, latitude10.Length - 1)), Convert.ToDouble(longitude10.Substring(0, longitude10.Length - 1)), 0);
                    PointLatLng pos = convertor.Cartesian_2_WGS84(ObjectCartesian, AirportGeodesic);
                    CoordinatesWGS84 ObjectWGS84 = new CoordinatesWGS84(pos.Lat, pos.Lng, 0);
                    marker2 = new GMarkerGoogle(new PointLatLng(ObjectWGS84.Lat, ObjectWGS84.Lon), markerCAT10);
                    marker2.ToolTipText = $"Target ID: {this.MapCAT10.Rows[x]["Target_ID"]}, \nTarget Address: {this.MapCAT10.Rows[x]["Target_Address"]}, \nTrack Number: {this.MapCAT10.Rows[x]["Track Number"]}, \nMode 3/A Code: {this.MapCAT10.Rows[x]["Mode_3A_Code"]}, \nFlight Level: {this.MapCAT10.Rows[x]["Flight Level"]}, \nLatitude: {ObjectWGS84.Lat}, \nLongitude: {ObjectWGS84.Lon}";
                    markerOverlay.Markers.Add(marker2);
                }
                catch { }
                x = x + 1;
            }



            gMapControl1.Overlays.Add(markerOverlay);
        }

        private void gMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (item == marker)
            {
                // Change the text of your label
                label2.Text = "Marker clicked!";
            }
        }
    }
}
