using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
using GMap.NET.WindowsPresentation;
using iTextSharp.text;
using MultiCAT6.Utils;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Crypto.Macs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Project_1
{
    public partial class MapInterface : Form
    {
        LoadingMap loadingscreen = new LoadingMap();
        Conversions convertor = new Conversions();
        public DataTable MapCAT10 = new DataTable();
        public DataTable MapCAT21 = new DataTable();
        public static Color color1 = Color.FromArgb(52, 192, 215);
        GMarkerGoogle marker;
        GMarkerGoogle marker2;
        GMapOverlay markerOverlay;
        DataTable dt;
        public double latitude21;
        public double longitude21;

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
            //loadingscreen.Show();
            //loadingscreen.BringToFront();
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInicial, LngInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 15;
            //gMapControl1.AutoScroll = true;
            //markerOverlay = new GMapOverlay("Marker");
            //marker = new GMarkerGoogle(new PointLatLng(41.286941, 2.007218), GMarkerGoogleType.red);
            //markerOverlay.Markers.Add(marker);
            //gMapControl1.Overlays.Add(markerOverlay);
            //gMapControl1.OnMarkerClick += new MarkerClick(gMapControl1_OnMarkerClick);

            timer1.Interval = 100;

            


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




        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            // Declare a HashSet to store the unique time intervals
            HashSet<string> timeIntervals = new HashSet<string>();

            // Iterate through each row of the DataTable and add the time intervals to the HashSet
            foreach (DataRow row in MapCAT10.Rows)
            {
                string timeValue = row["Time_of_Day"].ToString();
                DateTime dateTime = DateTime.ParseExact(timeValue, "HH:mm:ss:fff", CultureInfo.InvariantCulture);
                string timeInterval = string.Format("{0:HH:mm:ss}", dateTime);
                timeIntervals.Add(timeInterval);
            }

            // Convert the HashSet to a List
            this.timeIntervalList = timeIntervals.ToList();



            timer1.Start();
            
            
        }




        List<string> timeIntervalList = new List<string>();
        int x = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            // string prueba = this.MapCAT10.Rows[1]["Time_of_Day"].ToString();
            //label25.Text = Convert.ToString(DateTime.ParseExact(this.MapCAT10.Rows[1]["Time_of_Day"].ToString(), "HH:mm:ss:fff", null));



            // Retrieve the time value from the current row
            string timeValue = timeIntervalList[x];

            // Update the label with the time value
            label25.Text = timeValue;

            // Move to the next row
            x++;
            if (x >= MapCAT10.Rows.Count)
            {
                x = 0; // Restart from the first row
                timer1.Stop();
            }
            SIMULATION();
        }




        private void SIMULATION()
        {
            gMapControl1.Overlays.Clear();
            //label25.Text = "08:00:47";
            DataView dataView = new DataView(this.MapCAT10);
            dataView.RowFilter = $"Time_of_Day LIKE '%{label25.Text.Substring(0,8)}%'";
            DataView dataView2 = new DataView(this.MapCAT21);
            dataView2.RowFilter = $"Time_of_Report_Transmission LIKE '%{label25.Text.Substring(0, 8)}%'";

            int i = 0;
            markerOverlay = new GMapOverlay("Marker");
            while (i < dataView2.Count)
            {
                try
                {
                    DataTable actualflights = dataView2.ToTable();
                    string description = actualflights.Rows[i]["Position in WGS-84 Co-ordinates"].ToString();
                    string[] lines = description.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    string firstLine = lines[0];
                    string secondLine = lines[1];
                    this.latitude21 = convertor.DMSToDD_Latitude(firstLine.Substring("Latitude= ".Length).Trim(), i);
                    this.longitude21 = convertor.DMSToDD_Longitude(secondLine.Substring("Longitude= ".Length).Trim());
                    marker = new GMarkerGoogle(new PointLatLng(this.latitude21, this.longitude21), bmpMarker);
                    marker.ToolTipText = $"Target ID: {actualflights.Rows[i]["Target_ID"]}, \nTarget Address: {actualflights.Rows[i]["Target_Address"]}, \nTrack Number: {actualflights.Rows[i]["Track Number"]}, \nMode 3/A Code: {actualflights.Rows[i]["Mode_3A_Code"]}, \nFlight Level: {actualflights.Rows[i]["Flight Level"]}, \nLatitude: {this.latitude21}, \nLongitude: {this.longitude21}";
                    gMapControl1.UpdateMarkerLocalPosition(marker);
                }
                catch { }
                markerOverlay.Markers.Add(marker);

                i = i + 1;
            }

            int x = 0;
            while (x < dataView.Count)
            {
                DataTable actualflights2 = dataView.ToTable();
                string description2 = actualflights2.Rows[x]["Position in Cartesian Co-ordinates"].ToString();
                string[] lines2 = description2.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                string firstLine2 = lines2[0];
                string secondLine2 = lines2[1];
                string latitude10 = firstLine2.Substring("X= ".Length).Trim();
                string longitude10 = secondLine2.Substring("Y= ".Length).Trim();

                if (actualflights2.Rows[x]["SIC"].ToString() == "7")
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
                    marker2.ToolTipText = $"Target ID: {actualflights2.Rows[x]["Target_ID"]}, \nTarget Address: {actualflights2.Rows[x]["Target_Address"]}, \nTrack Number: {actualflights2.Rows[x]["Track Number"]}, \nMode 3/A Code: {actualflights2.Rows[x]["Mode_3A_Code"]}, \nFlight Level: {actualflights2.Rows[x]["Flight Level"]}, \nLatitude: {ObjectWGS84.Lat}, \nLongitude: {ObjectWGS84.Lon}";
                    markerOverlay.Markers.Add(marker2);
                    gMapControl1.UpdateMarkerLocalPosition(marker2);
                }
                catch { }
                x = x + 1;
            }

            gMapControl1.Overlays.Add(markerOverlay);
            gMapControl1.OnMarkerEnter += gMapControl1_OnMarkerEnter;
            gMapControl1.OnMarkerLeave += gMapControl1_OnMarkerLeave;
            gMapControl1.Refresh();
        }

        private void gMapControl1_OnMarkerEnter(GMap.NET.WindowsForms.GMapMarker item)
        {
            // Assuming marker2.ToolTipText contains a string with comma-separated values

            string[] tooltipParts = marker2.ToolTipText.Split('\n');

            if (tooltipParts.Length > 0)
            {
                // Get the first element of the tooltip string
                string firstElement = tooltipParts[0];
                string secondElement = tooltipParts[2];
                string thirdElement = tooltipParts[3];
                string fouthElement = tooltipParts[4];

                // Do something with the first element, such as set it as the text of a label
                label2.Text = firstElement.Replace("Target ID: ", "");
                label4.Text = secondElement.Replace("Track Number: ", "");
                label9.Text = thirdElement.Replace("Mode 3/A Code: ", "");
                label17.Text = fouthElement.Replace("Flight Level: ", "");
            }



            
        }

        private void gMapControl1_OnMarkerLeave(GMap.NET.WindowsForms.GMapMarker item)
        {
            label2.Text = "No Data";
            label4.Text = "No Data";
            label9.Text = "No Data";
            label17.Text = "No Data";
        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                timer1.Stop();
            }
            
        }
    }
}
