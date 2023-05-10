using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
using System.Xml;
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
using Panel = System.Windows.Forms.Panel;




namespace Project_1
{
    public partial class MapInterface : Form
    {
        FilterException exception = new FilterException();
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
        bool fileloaded;
        int filaselecionada = 0;
        double LatInicial = 41.2985227506962;
        double LngInicial = 2.083493712899025;
        CoordinatesWGS84 AirportGeodesic = new CoordinatesWGS84();
        List<string> timeIntervalList = new List<string>();
        int x = 0;
        int playbuttonselected = 0;
        int pausebuttonselected = 0;
        static string directory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BlackMarker.png");
        Bitmap bmpMarker = (Bitmap)System.Drawing.Image.FromFile(System.IO.Path.Combine(directory, "Images", "BlackMarker.png"));
        Bitmap bmpMarker2 = (Bitmap)System.Drawing.Image.FromFile(System.IO.Path.Combine(directory, "Images", "RedMarker.png"));
        Bitmap bmpMarker3 = (Bitmap)System.Drawing.Image.FromFile(System.IO.Path.Combine(directory, "Images", "BlueMarker.png"));
        Bitmap markerCAT10;
        bool SMRmarkers;
        bool MLATmarkers;
        bool ADSBmarkers;
        string first_time;
        string last_time;
        public DataView dataView = new DataView();
        public DataView dataView2 = new DataView();
        int a = 0;
        public DataView Test = new DataView();
        public DataView Test2 = new DataView();
        public int rows;
        public int counter10;
        public int counter21;
        public DataTable Table1 = new DataTable();
        public DataTable Table2 = new DataTable();
        public DateTime startTime;
        public DateTime endTime;

        public MapInterface()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            RoundPanelCorners(panel6, 20);
            textBox1.Text = "hh:mm:ss";
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

            timer1.Interval = 1000;
            SMRmarkers = true;
            MLATmarkers = true;
            ADSBmarkers = true;

            toggleButton1.CheckedChanged -= toggleButton1_CheckedChanged;
            toggleButton1.Checked = true;
            toggleButton1.CheckedChanged += toggleButton1_CheckedChanged;

            toggleButton2.CheckedChanged -= toggleButton2_CheckedChanged;
            toggleButton2.Checked = true;
            toggleButton2.CheckedChanged += toggleButton2_CheckedChanged;

            toggleButton3.CheckedChanged -= toggleButton3_CheckedChanged;
            toggleButton3.Checked = true;
            toggleButton3.CheckedChanged += toggleButton3_CheckedChanged;


            if (this.fileloaded == true)
            {
                this.rows = MapCAT10.Rows.Count;

                HashSet<string> timeIntervals = new HashSet<string>();

                // Iterate through each row of the DataTable and add the time intervals to the HashSet
                foreach (DataRow row in MapCAT10.Rows)
                {
                    string timeValue = row["Time_of_Day"].ToString();
                    DateTime dateTime = DateTime.ParseExact(timeValue, "HH:mm:ss:fff", CultureInfo.InvariantCulture);
                    string timeInterval = string.Format("{0:HH:mm:ss}", dateTime);
                    timeIntervals.Add(timeInterval);
                }

                this.timeIntervalList = timeIntervals.ToList();

                this.Test = new DataView(this.MapCAT10);
                this.Test2 = new DataView(this.MapCAT21);

                this.startTime = DateTime.ParseExact("08:00:00:000", "HH:mm:ss:fff", CultureInfo.InvariantCulture);
                this.endTime = startTime.AddMinutes(5);

                this.Test2.RowFilter = string.Format("Time_of_Report_Transmission >= '{0}' AND Time_of_Report_Transmission < '{1}'", startTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"));


                if (this.SMRmarkers == true && this.MLATmarkers == false)
                {
                    this.Test.RowFilter = string.Format("Time_of_Day >= '{0}' AND Time_of_Day < '{1}' AND SIC = '{2}'", startTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"), 7);
                }
                if (this.SMRmarkers == false && this.MLATmarkers == true)
                {
                    this.Test.RowFilter = string.Format("Time_of_Day >= '{0}' AND Time_of_Day < '{1}' AND SIC = '{2}'", startTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"), 107);
                }
                if (this.SMRmarkers == true && this.MLATmarkers == true)
                {
                    this.Test.RowFilter = string.Format("Time_of_Day >= '{0}' AND Time_of_Day < '{1}'", startTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"));
                }

                this.Table1 = this.Test.ToTable();
                this.Table2 = this.Test2.ToTable();
                this.dataView = new DataView(this.Table1);
                this.dataView2 = new DataView(this.Table2);
                this.Test = null;
                this.Test2 = null;
            }
            else
            {
                exception.ShowDialog();
            }



            GMapOverlay polygonOverlay = new GMapOverlay("polygonOverlay");

            // TAXI
            List<PointLatLng> polygonPoints4 = new List<PointLatLng>();
            polygonPoints4.Add(new PointLatLng(41.282317, 2.073395));
            polygonPoints4.Add(new PointLatLng(41.283405, 2.072890));
            polygonPoints4.Add(new PointLatLng(41.283857, 2.073019));
            polygonPoints4.Add(new PointLatLng(41.284228, 2.073373));
            polygonPoints4.Add(new PointLatLng(41.284808, 2.071678));
            polygonPoints4.Add(new PointLatLng(41.287275, 2.078684));
            polygonPoints4.Add(new PointLatLng(41.288299, 2.081924));
            polygonPoints4.Add(new PointLatLng(41.291572, 2.083405));
            polygonPoints4.Add(new PointLatLng(41.292354, 2.083791));
            polygonPoints4.Add(new PointLatLng(41.293998, 2.083029));
            polygonPoints4.Add(new PointLatLng(41.295256, 2.082246));
            polygonPoints4.Add(new PointLatLng(41.293200, 2.076313));
            polygonPoints4.Add(new PointLatLng(41.291983, 2.077053));
            polygonPoints4.Add(new PointLatLng(41.291693, 2.076098));
            polygonPoints4.Add(new PointLatLng(41.292555, 2.075540));
            polygonPoints4.Add(new PointLatLng(41.289379, 2.066292));
            polygonPoints4.Add(new PointLatLng(41.290484, 2.062591));
            polygonPoints4.Add(new PointLatLng(41.292257, 2.061518));
            polygonPoints4.Add(new PointLatLng(41.293724, 2.060069));
            polygonPoints4.Add(new PointLatLng(41.293982, 2.060112));
            polygonPoints4.Add(new PointLatLng(41.294353, 2.059844));
            polygonPoints4.Add(new PointLatLng(41.304306, 2.088862));
            polygonPoints4.Add(new PointLatLng(41.304580, 2.089656));
            polygonPoints4.Add(new PointLatLng(41.309528, 2.091833));
            polygonPoints4.Add(new PointLatLng(41.310076, 2.093400));
            polygonPoints4.Add(new PointLatLng(41.311938, 2.094258));
            polygonPoints4.Add(new PointLatLng(41.312115, 2.094591));
            polygonPoints4.Add(new PointLatLng(41.312131, 2.094988));
            polygonPoints4.Add(new PointLatLng(41.311672, 2.096962));
            polygonPoints4.Add(new PointLatLng(41.311365, 2.097359));
            polygonPoints4.Add(new PointLatLng(41.311051, 2.097734));
            polygonPoints4.Add(new PointLatLng(41.310898, 2.098292));
            polygonPoints4.Add(new PointLatLng(41.310866, 2.098968));
            polygonPoints4.Add(new PointLatLng(41.310970, 2.099902));
            polygonPoints4.Add(new PointLatLng(41.311406, 2.101210));
            polygonPoints4.Add(new PointLatLng(41.308924, 2.102423));
            polygonPoints4.Add(new PointLatLng(41.308738, 2.102519));
            polygonPoints4.Add(new PointLatLng(41.308351, 2.103893));
            polygonPoints4.Add(new PointLatLng(41.306602, 2.104912));
            polygonPoints4.Add(new PointLatLng(41.304581, 2.106157));
            polygonPoints4.Add(new PointLatLng(41.303227, 2.105599));
            polygonPoints4.Add(new PointLatLng(41.303001, 2.105320));
            polygonPoints4.Add(new PointLatLng(41.297730, 2.089849));
            polygonPoints4.Add(new PointLatLng(41.296109, 2.085826));
            polygonPoints4.Add(new PointLatLng(41.289975, 2.089613));
            polygonPoints4.Add(new PointLatLng(41.294006, 2.101736));
            polygonPoints4.Add(new PointLatLng(41.293925, 2.102444));
            polygonPoints4.Add(new PointLatLng(41.293780, 2.102852));
            polygonPoints4.Add(new PointLatLng(41.293441, 2.103045));
            polygonPoints4.Add(new PointLatLng(41.292684, 2.103517));
            polygonPoints4.Add(new PointLatLng(41.282354, 2.073526));


            // RUNWAYS
            List<PointLatLng> polygonPoints1 = new List<PointLatLng>();
            polygonPoints1.Add(new PointLatLng(41.309552, 2.094485));
            polygonPoints1.Add(new PointLatLng(41.287886, 2.084615));
            polygonPoints1.Add(new PointLatLng(41.287757, 2.085087));
            polygonPoints1.Add(new PointLatLng(41.307505, 2.094078));
            polygonPoints1.Add(new PointLatLng(41.309310, 2.095022));

            List<PointLatLng> polygonPoints2 = new List<PointLatLng>();
            polygonPoints2.Add(new PointLatLng(41.293061, 2.065539));
            polygonPoints2.Add(new PointLatLng(41.292513, 2.065882));
            polygonPoints2.Add(new PointLatLng(41.305957, 2.105236));
            polygonPoints2.Add(new PointLatLng(41.306505, 2.104892));
            polygonPoints2.Add(new PointLatLng(41.293442, 2.066485));

            List<PointLatLng> polygonPoints3 = new List<PointLatLng>();
            polygonPoints3.Add(new PointLatLng(41.282349, 2.073480));
            polygonPoints3.Add(new PointLatLng(41.281833, 2.073824));
            polygonPoints3.Add(new PointLatLng(41.292249, 2.104251));
            polygonPoints3.Add(new PointLatLng(41.292829, 2.103736));
            polygonPoints3.Add(new PointLatLng(41.282704, 2.074553));


            // APRON
            List<PointLatLng> polygonPoints5 = new List<PointLatLng>();
            polygonPoints5.Add(new PointLatLng(41.289286, 2.065778));
            polygonPoints5.Add(new PointLatLng(41.292526, 2.075606));
            polygonPoints5.Add(new PointLatLng(41.291188, 2.076421));
            polygonPoints5.Add(new PointLatLng(41.293316, 2.082987));
            polygonPoints5.Add(new PointLatLng(41.292268, 2.083717));
            polygonPoints5.Add(new PointLatLng(41.290688, 2.083009));
            polygonPoints5.Add(new PointLatLng(41.288866, 2.077709));
            polygonPoints5.Add(new PointLatLng(41.287270, 2.078674));
            polygonPoints5.Add(new PointLatLng(41.285126, 2.072366));
            polygonPoints5.Add(new PointLatLng(41.289157, 2.065864));

            List<PointLatLng> polygonPoints6 = new List<PointLatLng>();
            polygonPoints6.Add(new PointLatLng(41.300442, 2.069748));
            polygonPoints6.Add(new PointLatLng(41.300812, 2.070864));
            polygonPoints6.Add(new PointLatLng(41.299491, 2.072859));
            polygonPoints6.Add(new PointLatLng(41.304778, 2.088524));
            polygonPoints6.Add(new PointLatLng(41.304439, 2.088760));
            polygonPoints6.Add(new PointLatLng(41.298459, 2.071057));
            polygonPoints6.Add(new PointLatLng(41.300297, 2.069791));

            List<PointLatLng> polygonPoints7 = new List<PointLatLng>();
            polygonPoints7.Add(new PointLatLng(41.307905, 2.087343));
            polygonPoints7.Add(new PointLatLng(41.306100, 2.088352));
            polygonPoints7.Add(new PointLatLng(41.306696, 2.090240));
            polygonPoints7.Add(new PointLatLng(41.309388, 2.091463));
            polygonPoints7.Add(new PointLatLng(41.308045, 2.087517));

            // AIRBONE
            List<PointLatLng> polygonPoints8 = new List<PointLatLng>();
            polygonPoints8.Add(new PointLatLng(41.309602, 2.094489));
            polygonPoints8.Add(new PointLatLng(41.309446, 2.095110));
            polygonPoints8.Add(new PointLatLng(41.316242, 2.101017));
            polygonPoints8.Add(new PointLatLng(41.317274, 2.094880));
            polygonPoints8.Add(new PointLatLng(41.309761, 2.094492));

            List<PointLatLng> polygonPoints9 = new List<PointLatLng>();
            polygonPoints9.Add(new PointLatLng(41.287863, 2.084631));
            polygonPoints9.Add(new PointLatLng(41.287746, 2.085082));
            polygonPoints9.Add(new PointLatLng(41.280129, 2.084029));
            polygonPoints9.Add(new PointLatLng(41.281419, 2.078836));
            polygonPoints9.Add(new PointLatLng(41.287788, 2.084548));

            List<PointLatLng> polygonPoints10 = new List<PointLatLng>();
            polygonPoints10.Add(new PointLatLng(41.282573, 2.074252));
            polygonPoints10.Add(new PointLatLng(41.282127, 2.074490));
            polygonPoints10.Add(new PointLatLng(41.276118, 2.064106));
            polygonPoints10.Add(new PointLatLng(41.280956, 2.061617));
            polygonPoints10.Add(new PointLatLng(41.282550, 2.074135));

            List<PointLatLng> polygonPoints11 = new List<PointLatLng>();
            polygonPoints11.Add(new PointLatLng(41.292440, 2.103071));
            polygonPoints11.Add(new PointLatLng(41.291990, 2.103361));
            polygonPoints11.Add(new PointLatLng(41.294941, 2.119083));
            polygonPoints11.Add(new PointLatLng(41.299616, 2.115736));
            polygonPoints11.Add(new PointLatLng(41.292502, 2.103189));

            List<PointLatLng> polygonPoints12 = new List<PointLatLng>();
            polygonPoints12.Add(new PointLatLng(41.305961, 2.103531));
            polygonPoints12.Add(new PointLatLng(41.305501, 2.103820));
            polygonPoints12.Add(new PointLatLng(41.307404, 2.116436));
            polygonPoints12.Add(new PointLatLng(41.311982, 2.112874));
            polygonPoints12.Add(new PointLatLng(41.306047, 2.103664));

            List<PointLatLng> polygonPoints13 = new List<PointLatLng>();
            polygonPoints13.Add(new PointLatLng(41.295085, 2.071799));
            polygonPoints13.Add(new PointLatLng(41.294634, 2.072056));
            polygonPoints13.Add(new PointLatLng(41.286815, 2.055941));
            polygonPoints13.Add(new PointLatLng(41.292200, 2.052851));
            polygonPoints13.Add(new PointLatLng(41.295088, 2.071558));


            // Create a new polygon and add it to the overlay
            GMap.NET.WindowsForms.GMapPolygon polygon4 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints4, "My Polygon");
            polygonOverlay.Polygons.Add(polygon4);
            GMap.NET.WindowsForms.GMapPolygon polygon1 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints1, "My Polygon");
            polygonOverlay.Polygons.Add(polygon1);
            GMap.NET.WindowsForms.GMapPolygon polygon2 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints2, "My Polygon");
            polygonOverlay.Polygons.Add(polygon2);
            GMap.NET.WindowsForms.GMapPolygon polygon3 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints3, "My Polygon");
            polygonOverlay.Polygons.Add(polygon3);
            GMap.NET.WindowsForms.GMapPolygon polygon5 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints5, "My Polygon");
            polygonOverlay.Polygons.Add(polygon5);
            GMap.NET.WindowsForms.GMapPolygon polygon6 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints6, "My Polygon");
            polygonOverlay.Polygons.Add(polygon6);
            GMap.NET.WindowsForms.GMapPolygon polygon7 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints7, "My Polygon");
            polygonOverlay.Polygons.Add(polygon7);
            GMap.NET.WindowsForms.GMapPolygon polygon8 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints8, "My Polygon");
            polygonOverlay.Polygons.Add(polygon8);
            GMap.NET.WindowsForms.GMapPolygon polygon9 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints9, "My Polygon");
            polygonOverlay.Polygons.Add(polygon9);
            GMap.NET.WindowsForms.GMapPolygon polygon10 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints10, "My Polygon");
            polygonOverlay.Polygons.Add(polygon10);
            GMap.NET.WindowsForms.GMapPolygon polygon11 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints11, "My Polygon");
            polygonOverlay.Polygons.Add(polygon11);
            GMap.NET.WindowsForms.GMapPolygon polygon12 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints12, "My Polygon");
            polygonOverlay.Polygons.Add(polygon12);
            GMap.NET.WindowsForms.GMapPolygon polygon13 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints13, "My Polygon");
            polygonOverlay.Polygons.Add(polygon13);

            // Add the overlay to the map control
            gMapControl1.Overlays.Add(polygonOverlay);
            gMapControl1.Refresh();
        }
        
        public void getMapPointsCAT10(DataTable MAP)
        {
            this.MapCAT10 = MAP;
        }

        public void getMapPointsCAT21(DataTable MAP)
        {
            this.MapCAT21 = MAP;
        }

        public void getfileloaded(bool file)
        {
            this.fileloaded = file;
        }

        private void iconPictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (this.playbuttonselected == 0) { iconPictureBox1.IconColor = Color.Red; }
        }

        private void iconPictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (this.playbuttonselected == 0) { iconPictureBox1.IconColor = Color.White; }
        }

        private void iconPictureBox2_MouseEnter(object sender, EventArgs e)
        {
            if (this.pausebuttonselected == 0) { iconPictureBox2.IconColor = Color.MediumSeaGreen; }
        }

        private void iconPictureBox2_MouseLeave(object sender, EventArgs e)
        {
            if (this.pausebuttonselected == 0) { iconPictureBox2.IconColor = Color.White; }
        }

        private void iconPictureBox6_MouseEnter(object sender, EventArgs e)
        {
            if (this.restartsimulation == false) { iconPictureBox6.IconColor = Color.DarkOrange; }
        }

        private void iconPictureBox6_MouseLeave(object sender, EventArgs e)
        {
            if ( this.restartsimulation == false) { iconPictureBox6.IconColor = Color.White; }
        }

        
        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            if (this.fileloaded == true)
            {
                iconPictureBox6.IconColor = Color.White;
                this.restartsimulation = false;
                if (this.playbuttonselected == 0)
                {
                    iconPictureBox1.IconColor = Color.Red;
                    iconPictureBox2.IconColor = Color.White;
                    this.playbuttonselected = 1;
                    this.pausebuttonselected = 0;
                }
                this.first_time = timeIntervalList.First().ToString();
                this.last_time = timeIntervalList.Last().ToString();
                this.Table1 = null;
                this.Table2 = null;

                timer1.Start();
            }
            else
            {
                exception.ShowDialog();
            }
        }



        
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label25.Text != this.timeIntervalList[this.timeIntervalList.Count-2].ToString())
            {
                string timeValue = timeIntervalList[x];
                label25.Text = timeValue;


                if (label25.Text == this.endTime.TimeOfDay.ToString())
                {
                    this.Test = new DataView(this.MapCAT10);
                    this.Test2 = new DataView(this.MapCAT21);

                    this.startTime = this.endTime;
                    this.endTime = startTime.AddMinutes(5);

                    this.Test2.RowFilter = string.Format("Time_of_Report_Transmission >= '{0}' AND Time_of_Report_Transmission < '{1}'", startTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"));

                    if (this.SMRmarkers == true && this.MLATmarkers == false)
                    {
                        this.Test.RowFilter = string.Format("Time_of_Day >= '{0}' AND Time_of_Day < '{1}' AND SIC = '{2}'", startTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"), 7);
                    }
                    if (this.SMRmarkers == false && this.MLATmarkers == true)
                    {
                        this.Test.RowFilter = string.Format("Time_of_Day >= '{0}' AND Time_of_Day < '{1}' AND SIC = '{2}'", startTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"), 107);
                    }
                    if (this.SMRmarkers == true && this.MLATmarkers == true)
                    {
                        this.Test.RowFilter = string.Format("Time_of_Day >= '{0}' AND Time_of_Day < '{1}'", startTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"));
                    }

                    this.Table1 = this.Test.ToTable();
                    this.Table2 = this.Test2.ToTable();
                    this.dataView = new DataView(this.Table1);
                    this.dataView2 = new DataView(this.Table2);
                    this.Test = null;
                    this.Test2 = null;
                }


                x++;
                if (x >= this.rows)
                {
                    x = 0;
                    timer1.Stop();
                }

                SIMULATION();
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("The simulation has finished!");
                iconPictureBox1.IconColor = Color.White;
                this.x = 0;
                this.restartsimulation = true;
                this.playbuttonselected = 0;
                label25.Text = this.first_time;
            }
        }

        
        private void SIMULATION()
        {
            if (this.SMRmarkers == true || this.MLATmarkers== true || this.ADSBmarkers == true)
            {
                gMapControl1.Overlays.Clear();
                //label25.Text = "08:00:47";

                
                dataView2.RowFilter = $"Time_of_Report_Transmission LIKE '%{label25.Text.Substring(0, 8)}%'";

                int i = 0;
                markerOverlay = new GMapOverlay("Marker");
                while (i < dataView2.Count)
                {
                    if (this.ADSBmarkers == true)
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
                            marker.ToolTipText = $"Target ID: {actualflights.Rows[i]["Target_ID"]} \nTarget Address: {actualflights.Rows[i]["Target_Address"]} \nTrack Number: {actualflights.Rows[i]["Track Number"]} \nMode 3/A Code: {actualflights.Rows[i]["Mode_3A_Code"]} \nFlight Level: {actualflights.Rows[i]["Flight Level"]} \nLatitude: {this.latitude21} \nLongitude: {this.longitude21} \nSIC: {actualflights.Rows[i]["SIC"]}";
                            marker.ToolTipMode = MarkerTooltipMode.Never;
                            gMapControl1.UpdateMarkerLocalPosition(marker);
                        }
                        catch { }
                        markerOverlay.Markers.Add(marker);
                    }

                    i = i + 1;
                }


                if (this.SMRmarkers == true && this.MLATmarkers == false)
                {
                    dataView.RowFilter = $"Time_of_Day LIKE '%{label25.Text.Substring(0, 8)}%' AND SIC = '{7}'";
                }
                if (this.SMRmarkers == false && this.MLATmarkers == true)
                {
                    dataView.RowFilter = $"Time_of_Day LIKE '%{label25.Text.Substring(0, 8)}%' AND SIC = '{107}'";
                }
                if (this.SMRmarkers == true && this.MLATmarkers == true)
                {
                    dataView.RowFilter = $"Time_of_Day LIKE '%{label25.Text.Substring(0, 8)}%'";
                }

                if (this.SMRmarkers == true || this.MLATmarkers == true)
                {
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
                            marker2.ToolTipText = $"Target ID: {actualflights2.Rows[x]["Target_ID"]} \nTarget Address: {actualflights2.Rows[x]["Target_Address"]} \nTrack Number: {actualflights2.Rows[x]["Track Number"]} \nMode 3/A Code: {actualflights2.Rows[x]["Mode_3A_Code"]} \nFlight Level: {actualflights2.Rows[x]["Flight Level"]} \nLatitude: {ObjectWGS84.Lat} \nLongitude: {ObjectWGS84.Lon} \nSIC: {actualflights2.Rows[x]["SIC"]}";
                            marker2.ToolTipMode = MarkerTooltipMode.Never;
                            markerOverlay.Markers.Add(marker2);
                            gMapControl1.UpdateMarkerLocalPosition(marker2);
                        }
                        catch { }

                        x = x + 1;
                    }
                }
                
                this.a = 1;
                gMapControl1.Overlays.Add(markerOverlay);
                gMapControl1.OnMarkerEnter += gMapControl1_OnMarkerEnter;
                gMapControl1.OnMarkerLeave += gMapControl1_OnMarkerLeave;
                gMapControl1.Refresh();
            }
            else
            {
                if (this.a == 1) { gMapControl1.Overlays.Clear(); gMapControl1.Refresh(); this.a = 0; }
            }
        }

        private void gMapControl1_OnMarkerEnter(GMap.NET.WindowsForms.GMapMarker item)
        {
            // Assuming marker2.ToolTipText contains a string with comma-separated values
            string[] tooltipParts = item.ToolTipText.Split('\n');

            if (tooltipParts.Length > 0)
            {
                // Get the first element of the tooltip string
                string firstElement = tooltipParts[0];
                string secondElement = tooltipParts[2];
                string thirdElement = tooltipParts[3];
                string fouthElement = tooltipParts[4];
                string fifthElement = tooltipParts[5];
                string sixthElement = tooltipParts[6];
                string seventhElement = tooltipParts[1];
                string lastElement = tooltipParts[7];

                // Do something with the first element, such as set it as the text of a label
                label2.Text = firstElement.Replace("Target ID: ", "");
                label15.Text = seventhElement.Replace("Target Address: ", "");
                label4.Text = secondElement.Replace("Track Number: ", "");
                label9.Text = thirdElement.Replace("Mode 3/A Code: ", "");
                label17.Text = fouthElement.Replace("Flight Level: ", "");
                //label13.Text = fifthElement.Replace("Latitude: ", "");
                //label11.Text = sixthElement.Replace("Longitude: ", "");
                if (lastElement.Replace("SIC: ", "") == "7")
                {
                    label6.Text = "SMR";
                }
                if (lastElement.Replace("SIC: ", "") == "107")
                {
                    label6.Text = "MLAT";
                }
                if (lastElement.Replace("SIC: ", "") != "107" && lastElement.Replace("SIC: ", "") != "7")
                {
                    label6.Text = "ADS-B";
                }
            } 
        }

        private void gMapControl1_OnMarkerLeave(GMap.NET.WindowsForms.GMapMarker item)
        {
            label2.Text = "No Data";
            label15.Text = "No Data";
            label4.Text = "No Data";
            label9.Text = "No Data";
            label17.Text = "No Data";
            //label13.Text = "No Data";
            //label11.Text = "No Data";
            label6.Text = "No Data";
        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            if (this.fileloaded == true)
            {
                if (this.pausebuttonselected == 0)
                {
                    iconPictureBox2.IconColor = Color.MediumSeaGreen;
                    iconPictureBox1.IconColor = Color.White;
                    this.playbuttonselected = 0;
                    this.pausebuttonselected = 1;
                }

                if (timer1.Enabled == true)
                {
                    timer1.Stop();

                }
            }
            else
            {
                exception.ShowDialog();
            }
        }

        private void iconPictureBox3_Click(object sender, EventArgs e)
        {
            if (this.fileloaded == true)
            {
                if (iconPictureBox3.IconFont == IconFont.Regular)
                {
                    iconPictureBox3.IconFont = IconFont.Solid;

                    iconPictureBox4.IconFont = IconFont.Regular;
                    iconPictureBox5.IconFont = IconFont.Regular;
                    timer1.Interval = 1000;
                }
            }
            else
            {
                exception.ShowDialog();
            }
        }

        private void iconPictureBox4_Click(object sender, EventArgs e)
        {
            if (this.fileloaded == true)
            {
                if (iconPictureBox4.IconFont == IconFont.Regular)
                {
                    iconPictureBox4.IconFont = IconFont.Solid;

                    iconPictureBox3.IconFont = IconFont.Regular;
                    iconPictureBox5.IconFont = IconFont.Regular;
                    timer1.Interval = 100;
                }
            }
            else
            {
                exception.ShowDialog();
            }
        }

        private void iconPictureBox5_Click(object sender, EventArgs e)
        {
            if (this.fileloaded == true)
            {
                if (iconPictureBox5.IconFont == IconFont.Regular)
                {
                    iconPictureBox5.IconFont = IconFont.Solid;

                    iconPictureBox3.IconFont = IconFont.Regular;
                    iconPictureBox4.IconFont = IconFont.Regular;
                    timer1.Interval = 10;
                }
            }
            else
            {
                exception.ShowDialog();
            }
        }


        private void toggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.fileloaded == true)
            {
                if (toggleButton1.Checked == true)
                {
                    this.SMRmarkers = true;
                }
                if (toggleButton1.Checked == false)
                {
                    this.SMRmarkers = false;
                }
            }
            else
            {
                exception.ShowDialog();
                toggleButton1.CheckedChanged -= toggleButton1_CheckedChanged;
                toggleButton1.Checked = true;
                toggleButton1.CheckedChanged += toggleButton1_CheckedChanged;
            }
        }

        private void toggleButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.fileloaded == true)
            {
                if (toggleButton2.Checked == true)
                {
                    this.MLATmarkers = true;
                }
                if (toggleButton2.Checked == false)
                {
                    this.MLATmarkers = false;
                }
            }
            else
            {
                exception.ShowDialog();
                toggleButton2.CheckedChanged -= toggleButton2_CheckedChanged;
                toggleButton2.Checked = true;
                toggleButton2.CheckedChanged += toggleButton2_CheckedChanged;
            }
        }

        private void toggleButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.fileloaded == true)
            {
                if (toggleButton3.Checked == true)
                {
                    this.ADSBmarkers = true;
                }
                if (toggleButton3.Checked == false)
                {
                    this.ADSBmarkers = false;
                }
            }
            else
            {
                exception.ShowDialog();
                toggleButton3.CheckedChanged -= toggleButton3_CheckedChanged;
                toggleButton3.Checked = true;
                toggleButton3.CheckedChanged += toggleButton3_CheckedChanged;
            } 
        }
        public bool restartsimulation = false;
        private void iconPictureBox6_Click(object sender, EventArgs e)
        {
            if (this.fileloaded == true)
            {
                iconPictureBox1.IconColor = Color.White;
                timer1.Stop();
                this.x = 0;
                this.restartsimulation = true;
                this.playbuttonselected = 0;
                label25.Text = this.first_time;
            }
            else
            {
                exception.ShowDialog();
            }
        }
        public string targetId;
        private void iconPictureBox7_Click(object sender, EventArgs e)
        {
            if (this.fileloaded == true)
            {
                // Create an XmlDocument object to store the KML file
                XmlDocument kmlDoc = new XmlDocument();

                // Create the KML file
                XmlElement kml = kmlDoc.CreateElement("kml", "http://www.opengis.net/kml/2.2");
                kml.SetAttribute("xmlns", "http://www.opengis.net/kml/2.2");
                kmlDoc.AppendChild(kml);

                XmlElement document = kmlDoc.CreateElement("Document");
                kml.AppendChild(document);

                XmlElement name = kmlDoc.CreateElement("name");
                name.InnerText = "My Map";
                document.AppendChild(name);

                // Get all the markers on the map and add them to the KML file
                foreach (GMapOverlay overlay in gMapControl1.Overlays)
                {
                    foreach (GMap.NET.WindowsForms.GMapMarker marker in overlay.Markers)
                    {
                        // Add a marker to the KML file
                        XmlElement placemark = kmlDoc.CreateElement("Placemark");
                        document.AppendChild(placemark);

                        XmlElement placemarkName = kmlDoc.CreateElement("name");
                        string input = marker.ToolTipText.ToString();

                        // Find the position of the "Target ID" field
                        int targetIdIndex = input.IndexOf("Target ID: ");
                        if (targetIdIndex == -1)
                        {
                            // The "Target ID" field was not found in the string
                            // Handle the error here
                        }
                        else
                        {
                            // Extract the value of the "Target ID" field
                            int valueStartIndex = targetIdIndex + "Target ID: ".Length;
                            int valueEndIndex = input.IndexOf(" ", valueStartIndex);
                            if (valueEndIndex == -1)
                            {
                                // The value extends to the end of the string
                                valueEndIndex = input.Length;
                            }
                            this.targetId = input.Substring(valueStartIndex, valueEndIndex - valueStartIndex);
                        }

                        
                        placemarkName.InnerText = this.targetId;
                        placemark.AppendChild(placemarkName);

                        XmlElement description = kmlDoc.CreateElement("description");
                        description.InnerText = marker.ToolTipText;
                        placemark.AppendChild(description);

                        XmlElement point = kmlDoc.CreateElement("Point");
                        placemark.AppendChild(point);

                        XmlElement coordinates = kmlDoc.CreateElement("coordinates");
                        coordinates.InnerText = string.Format("{0},{1}", marker.Position.Lng.ToString().Replace(",", "."), marker.Position.Lat.ToString().Replace(",", "."));
                        point.AppendChild(coordinates);
                    }
                }

                // Save the KML file to disk
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "KML files (*.kml)|*.kml|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    kmlDoc.Save(saveFileDialog.FileName);
                    MessageBox.Show("Current Map Successfully Exported to KML");
                }
            }
            else
            {
                exception.ShowDialog();
            }
        }

        private void iconPictureBox7_MouseEnter(object sender, EventArgs e)
        {
            iconPictureBox7.IconColor = color1;
        }

        private void iconPictureBox7_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox7.IconColor = Color.White;
        }

        private void RoundPanelCorners(Panel panel, int radius)
        {
            GraphicsPath panelPath = new GraphicsPath();
            panelPath.StartFigure();
            panelPath.AddArc(new System.Drawing.Rectangle(0, 0, radius, radius), 180, 90);
            panelPath.AddLine(radius, 0, panel.Width - radius, 0);
            panelPath.AddArc(new System.Drawing.Rectangle(panel.Width - radius, 0, radius, radius), -90, 90);
            panelPath.AddLine(panel.Width, radius, panel.Width, panel.Height - radius);
            panelPath.AddArc(new System.Drawing.Rectangle(panel.Width - radius, panel.Height - radius, radius, radius), 0, 90);
            panelPath.AddLine(panel.Width - radius, panel.Height, radius, panel.Height);
            panelPath.AddArc(new System.Drawing.Rectangle(0, panel.Height - radius, radius, radius), 90, 90);
            panelPath.CloseFigure();

            panel.Region = new Region(panelPath);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fileloaded == true)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (this.fileloaded == true)
                        {
                            string textBoxValue = textBox1.Text;
                            DateTime textBoxTime = DateTime.ParseExact(textBoxValue, "HH:mm:ss", CultureInfo.InvariantCulture);
                            DateTime startTime = DateTime.ParseExact(this.first_time, "HH:mm:ss", CultureInfo.InvariantCulture);
                            DateTime endTime = DateTime.ParseExact(this.last_time, "HH:mm:ss", CultureInfo.InvariantCulture);

                            // Compare the TextBox value with the start and end times
                            if (textBoxTime >= startTime && textBoxTime <= endTime)
                            {
                                textBox1.Text = "hh:mm:ss";
                                iconPictureBox1.IconColor = Color.White;
                                timer1.Stop();
                                this.x = this.timeIntervalList.IndexOf(textBoxValue); ;
                                this.restartsimulation = true;
                                this.playbuttonselected = 0;
                                label25.Text = textBoxValue;

                                this.Test = new DataView(this.MapCAT10);
                                this.Test2 = new DataView(this.MapCAT21);

                                this.startTime = textBoxTime;
                                this.endTime = startTime.AddMinutes(5);

                                this.Test2.RowFilter = string.Format("Time_of_Report_Transmission >= '{0}' AND Time_of_Report_Transmission < '{1}'", startTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"));

                                if (this.SMRmarkers == true && this.MLATmarkers == false)
                                {
                                    this.Test.RowFilter = string.Format("Time_of_Day >= '{0}' AND Time_of_Day < '{1}' AND SIC = '{2}'", startTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"), 7);
                                }
                                if (this.SMRmarkers == false && this.MLATmarkers == true)
                                {
                                    this.Test.RowFilter = string.Format("Time_of_Day >= '{0}' AND Time_of_Day < '{1}' AND SIC = '{2}'", startTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"), 107);
                                }
                                if (this.SMRmarkers == true && this.MLATmarkers == true)
                                {
                                    this.Test.RowFilter = string.Format("Time_of_Day >= '{0}' AND Time_of_Day < '{1}'", startTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"));
                                }

                                this.Table1 = this.Test.ToTable();
                                this.Table2 = this.Test2.ToTable();
                                this.dataView = new DataView(this.Table1);
                                this.dataView2 = new DataView(this.Table2);
                                this.Test = null;
                                this.Test2 = null;
                            }
                            else
                            {
                                MessageBox.Show(string.Format("Please enter a valid time between {0} and {1}", this.first_time, this.last_time));
                            }

                        }
                        else
                        {
                            textBox1.Text = "hh:mm:ss";
                            exception.ShowDialog();
                        }
                    }
                }
                else
                {
                    textBox1.Text = "hh:mm:ss";
                    exception.ShowDialog();
                }
            }
        }
    }
}
