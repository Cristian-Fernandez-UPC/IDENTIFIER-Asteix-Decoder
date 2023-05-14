using GMap.NET.WindowsForms;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsPresentation;
using MultiCAT6.Utils;
using System.Reflection;
using GMap.NET.MapProviders;
using System.Globalization;
using System.Windows.Documents;

namespace Project_1
{
    public partial class Performance : Form
    {
        Conversions convertor = new Conversions();
        public DataTable MapCAT10 = new DataTable();
        public DataTable MapCAT21 = new DataTable();
        bool fileloaded;
        GMapOverlay markerOverlay;
        CoordinatesWGS84 AirportGeodesic = new CoordinatesWGS84();
        Bitmap markerCAT10;
        GMarkerGoogle marker2;
        List<GMarkerGoogle> markerslist = new List<GMarkerGoogle>();
        List<PointLatLng> polygonlist_STAND1 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_STAND2 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_STAND3 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_STAND4 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_TAXI = new List<PointLatLng>();
        List<PointLatLng> polygonlist_RUNWAY1 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_RUNWAY2 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_RUNWAY3 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_APRON1 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_APRON2 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_APRON3 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_AIRBONE1 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_AIRBONE2 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_AIRBONE3 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_AIRBONE4 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_AIRBONE5 = new List<PointLatLng>();
        List<PointLatLng> polygonlist_AIRBONE6 = new List<PointLatLng>();


        static string directory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Bitmap bmpMarker3 = (Bitmap)System.Drawing.Image.FromFile(System.IO.Path.Combine(directory, "Images", "PerformanceMarker.png"));
        double LatInicial = 41.296857;
        double LngInicial = 2.084174;
        DataTable inside_taxi_zone = new DataTable();
        DataTable inside_runway_zone = new DataTable();
        DataTable inside_apron_zone = new DataTable();
        DataTable inside_airbone_zone = new DataTable();
        DataTable inside_stand_zone = new DataTable();
        double updateratelist_taxi;
        double updateratelist_runway;
        double updateratelist_apron;
        double updateratelist_airbone;
        double updateratelist_stand;
        double updates_taxi = 0;
        double updates_runway = 0;
        double updates_apron = 0;
        double updates_airbone = 0;
        double updates_stand = 0;
        double expected_taxi = 0;
        double expected_runway = 0;
        double expected_apron = 0;
        double expected_airbone = 0;
        double expected_stand = 0;






        public Performance()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInicial, LngInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 15;
            circularProgressBar1.Value = 0;
            circularProgressBar2.Value = 0;
            circularProgressBar3.Value = 0;
            circularProgressBar4.Value = 0;


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
            this.polygonlist_TAXI = polygonPoints4;


            // STAND
            List<PointLatLng> polygonPoints14 = new List<PointLatLng>();
            polygonPoints14.Add(new PointLatLng(41.293244, 2.076281));
            polygonPoints14.Add(new PointLatLng(41.292003, 2.077032));
            polygonPoints14.Add(new PointLatLng(41.294002, 2.082976));
            polygonPoints14.Add(new PointLatLng(41.295292, 2.082225));
            this.polygonlist_STAND1 = polygonPoints14;

            List<PointLatLng> polygonPoints15 = new List<PointLatLng>();
            polygonPoints15.Add(new PointLatLng(41.288507, 2.079002));
            polygonPoints15.Add(new PointLatLng(41.287556, 2.079624));
            polygonPoints15.Add(new PointLatLng(41.288314, 2.081920));
            polygonPoints15.Add(new PointLatLng(41.289717, 2.082585));
            this.polygonlist_STAND2 = polygonPoints15;

            List<PointLatLng> polygonPoints16 = new List<PointLatLng>();
            polygonPoints16.Add(new PointLatLng(41.289101, 2.065510));
            polygonPoints16.Add(new PointLatLng(41.284828, 2.071561));
            polygonPoints16.Add(new PointLatLng(41.287231, 2.078685));
            polygonPoints16.Add(new PointLatLng(41.288891, 2.077741));
            polygonPoints16.Add(new PointLatLng(41.290681, 2.082976));
            polygonPoints16.Add(new PointLatLng(41.292309, 2.083706));
            polygonPoints16.Add(new PointLatLng(41.293309, 2.083041));
            polygonPoints16.Add(new PointLatLng(41.291100, 2.076432));
            polygonPoints16.Add(new PointLatLng(41.292551, 2.075595));
            this.polygonlist_STAND3 = polygonPoints16;

            List<PointLatLng> polygonPoints17 = new List<PointLatLng>();
            polygonPoints17.Add(new PointLatLng(41.294398, 2.059768));
            polygonPoints17.Add(new PointLatLng(41.298396, 2.071463));
            polygonPoints17.Add(new PointLatLng(41.298847, 2.071205));
            polygonPoints17.Add(new PointLatLng(41.303383, 2.084502));
            polygonPoints17.Add(new PointLatLng(41.302956, 2.084748));
            polygonPoints17.Add(new PointLatLng(41.304310, 2.088815));
            polygonPoints17.Add(new PointLatLng(41.305898, 2.087871));
            polygonPoints17.Add(new PointLatLng(41.306841, 2.090606));
            polygonPoints17.Add(new PointLatLng(41.309484, 2.091733));
            polygonPoints17.Add(new PointLatLng(41.307905, 2.087120));
            polygonPoints17.Add(new PointLatLng(41.307236, 2.087474));
            polygonPoints17.Add(new PointLatLng(41.300884, 2.069173));
            polygonPoints17.Add(new PointLatLng(41.300432, 2.069452));
            polygonPoints17.Add(new PointLatLng(41.296650, 2.058448));
            this.polygonlist_STAND4 = polygonPoints17;


            // RUNWAYS
            List<PointLatLng> polygonPoints1 = new List<PointLatLng>();
            polygonPoints1.Add(new PointLatLng(41.309552, 2.094485));
            polygonPoints1.Add(new PointLatLng(41.287886, 2.084615));
            polygonPoints1.Add(new PointLatLng(41.287757, 2.085087));
            polygonPoints1.Add(new PointLatLng(41.307505, 2.094078));
            polygonPoints1.Add(new PointLatLng(41.309310, 2.095022));
            this.polygonlist_RUNWAY1 = polygonPoints1;

            List<PointLatLng> polygonPoints2 = new List<PointLatLng>();
            polygonPoints2.Add(new PointLatLng(41.293061, 2.065539));
            polygonPoints2.Add(new PointLatLng(41.292513, 2.065882));
            polygonPoints2.Add(new PointLatLng(41.305957, 2.105236));
            polygonPoints2.Add(new PointLatLng(41.306505, 2.104892));
            polygonPoints2.Add(new PointLatLng(41.293442, 2.066485));
            this.polygonlist_RUNWAY2 = polygonPoints2;

            List<PointLatLng> polygonPoints3 = new List<PointLatLng>();
            polygonPoints3.Add(new PointLatLng(41.282349, 2.073480));
            polygonPoints3.Add(new PointLatLng(41.281833, 2.073824));
            polygonPoints3.Add(new PointLatLng(41.292249, 2.104251));
            polygonPoints3.Add(new PointLatLng(41.292829, 2.103736));
            polygonPoints3.Add(new PointLatLng(41.282704, 2.074553));
            this.polygonlist_RUNWAY3 = polygonPoints3;


            // APRON
            List<PointLatLng> polygonPoints5 = new List<PointLatLng>();
            polygonPoints5.Add(new PointLatLng(41.289088, 2.065591));
            polygonPoints5.Add(new PointLatLng(41.292506, 2.075547));
            polygonPoints5.Add(new PointLatLng(41.291684, 2.076062));
            polygonPoints5.Add(new PointLatLng(41.293957, 2.082993));
            polygonPoints5.Add(new PointLatLng(41.292296, 2.083830));
            polygonPoints5.Add(new PointLatLng(41.288249, 2.081727));
            polygonPoints5.Add(new PointLatLng(41.286645, 2.077178));
            polygonPoints5.Add(new PointLatLng(41.284896, 2.071878));
            this.polygonlist_APRON1 = polygonPoints5;

            List<PointLatLng> polygonPoints6 = new List<PointLatLng>();
            polygonPoints6.Add(new PointLatLng(41.298397, 2.071427));
            polygonPoints6.Add(new PointLatLng(41.304346, 2.088787));
            polygonPoints6.Add(new PointLatLng(41.304732, 2.088551));
            polygonPoints6.Add(new PointLatLng(41.299542, 2.073337));
            polygonPoints6.Add(new PointLatLng(41.301154, 2.071170));
            polygonPoints6.Add(new PointLatLng(41.300735, 2.069990));
            this.polygonlist_APRON2 = polygonPoints6;

            List<PointLatLng> polygonPoints7 = new List<PointLatLng>();
            polygonPoints7.Add(new PointLatLng(41.304293, 2.088809));
            polygonPoints7.Add(new PointLatLng(41.305889, 2.087865));
            polygonPoints7.Add(new PointLatLng(41.306808, 2.090590));
            polygonPoints7.Add(new PointLatLng(41.304615, 2.089732));
            this.polygonlist_APRON3 = polygonPoints7;


            // AIRBONE
            List<PointLatLng> polygonPoints8 = new List<PointLatLng>();
            polygonPoints8.Add(new PointLatLng(41.309602, 2.094489));
            polygonPoints8.Add(new PointLatLng(41.309446, 2.095110));
            polygonPoints8.Add(new PointLatLng(41.316242, 2.101017));
            polygonPoints8.Add(new PointLatLng(41.317274, 2.094880));
            polygonPoints8.Add(new PointLatLng(41.309761, 2.094492));
            this.polygonlist_AIRBONE1 = polygonPoints8;

            List<PointLatLng> polygonPoints9 = new List<PointLatLng>();
            polygonPoints9.Add(new PointLatLng(41.287863, 2.084631));
            polygonPoints9.Add(new PointLatLng(41.287746, 2.085082));
            polygonPoints9.Add(new PointLatLng(41.280129, 2.084029));
            polygonPoints9.Add(new PointLatLng(41.281419, 2.078836));
            polygonPoints9.Add(new PointLatLng(41.287788, 2.084548));
            this.polygonlist_AIRBONE2 = polygonPoints9;

            List<PointLatLng> polygonPoints10 = new List<PointLatLng>();
            polygonPoints10.Add(new PointLatLng(41.282573, 2.074252));
            polygonPoints10.Add(new PointLatLng(41.282127, 2.074490));
            polygonPoints10.Add(new PointLatLng(41.276118, 2.064106));
            polygonPoints10.Add(new PointLatLng(41.280956, 2.061617));
            polygonPoints10.Add(new PointLatLng(41.282550, 2.074135));
            this.polygonlist_AIRBONE3 = polygonPoints10;

            List<PointLatLng> polygonPoints11 = new List<PointLatLng>();
            polygonPoints11.Add(new PointLatLng(41.292440, 2.103071));
            polygonPoints11.Add(new PointLatLng(41.291990, 2.103361));
            polygonPoints11.Add(new PointLatLng(41.294941, 2.119083));
            polygonPoints11.Add(new PointLatLng(41.299616, 2.115736));
            polygonPoints11.Add(new PointLatLng(41.292502, 2.103189));
            this.polygonlist_AIRBONE4 = polygonPoints11;

            List<PointLatLng> polygonPoints12 = new List<PointLatLng>();
            polygonPoints12.Add(new PointLatLng(41.305961, 2.103531));
            polygonPoints12.Add(new PointLatLng(41.305501, 2.103820));
            polygonPoints12.Add(new PointLatLng(41.307404, 2.116436));
            polygonPoints12.Add(new PointLatLng(41.311982, 2.112874));
            polygonPoints12.Add(new PointLatLng(41.306047, 2.103664));
            this.polygonlist_AIRBONE5 = polygonPoints12;

            List<PointLatLng> polygonPoints13 = new List<PointLatLng>();
            polygonPoints13.Add(new PointLatLng(41.295085, 2.071799));
            polygonPoints13.Add(new PointLatLng(41.294634, 2.072056));
            polygonPoints13.Add(new PointLatLng(41.286815, 2.055941));
            polygonPoints13.Add(new PointLatLng(41.292200, 2.052851));
            polygonPoints13.Add(new PointLatLng(41.295088, 2.071558));
            this.polygonlist_AIRBONE6 = polygonPoints13;


            // Create a new polygon and add it to the overlay
            GMap.NET.WindowsForms.GMapPolygon polygon4 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints4, "My Polygon");
            Pen pen4 = new Pen(Color.Red, 2);
            polygon4.Stroke = pen4;
            SolidBrush brush4 = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon4.Fill = brush4;
            polygonOverlay.Polygons.Add(polygon4);
            GMap.NET.WindowsForms.GMapPolygon polygon14 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints14, "My Polygon");
            Pen pen14 = new Pen(Color.Gray, 2);
            polygon14.Stroke = pen14;
            SolidBrush brush14 = new SolidBrush(Color.FromArgb(50, Color.Gray));
            polygon14.Fill = brush14;
            polygonOverlay.Polygons.Add(polygon14);
            GMap.NET.WindowsForms.GMapPolygon polygon15 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints15, "My Polygon");
            Pen pen15 = new Pen(Color.Gray, 2);
            polygon15.Stroke = pen15;
            SolidBrush brush15 = new SolidBrush(Color.FromArgb(50, Color.Gray));
            polygon15.Fill = brush15;
            polygonOverlay.Polygons.Add(polygon15);
            GMap.NET.WindowsForms.GMapPolygon polygon16 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints16, "My Polygon");
            Pen pen16 = new Pen(Color.Gray, 2);
            polygon16.Stroke = pen16;
            SolidBrush brush16 = new SolidBrush(Color.FromArgb(50, Color.Gray));
            polygon16.Fill = brush16;
            polygonOverlay.Polygons.Add(polygon16);
            GMap.NET.WindowsForms.GMapPolygon polygon17 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints17, "My Polygon");
            Pen pen17 = new Pen(Color.Gray, 2);
            polygon17.Stroke = pen17;
            SolidBrush brush17 = new SolidBrush(Color.FromArgb(50, Color.Gray));
            polygon17.Fill = brush17;
            polygonOverlay.Polygons.Add(polygon17);
            GMap.NET.WindowsForms.GMapPolygon polygon1 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints1, "My Polygon");
            Pen pen1 = new Pen(Color.Blue, 2);
            polygon1.Stroke = pen1;
            SolidBrush brush1 = new SolidBrush(Color.FromArgb(50, Color.Blue));
            polygon1.Fill = brush1;
            polygonOverlay.Polygons.Add(polygon1);
            GMap.NET.WindowsForms.GMapPolygon polygon2 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints2, "My Polygon");
            Pen pen2 = new Pen(Color.Blue, 2);
            polygon2.Stroke = pen2;
            SolidBrush brush2 = new SolidBrush(Color.FromArgb(50, Color.Blue));
            polygon2.Fill = brush2;
            polygonOverlay.Polygons.Add(polygon2);
            GMap.NET.WindowsForms.GMapPolygon polygon3 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints3, "My Polygon");
            Pen pen3 = new Pen(Color.Blue, 2);
            polygon3.Stroke = pen3;
            SolidBrush brush3 = new SolidBrush(Color.FromArgb(50, Color.Blue));
            polygon3.Fill = brush3;
            polygonOverlay.Polygons.Add(polygon3);
            GMap.NET.WindowsForms.GMapPolygon polygon5 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints5, "My Polygon");
            Pen pen5 = new Pen(Color.Green, 2);
            polygon5.Stroke = pen5;
            SolidBrush brush5 = new SolidBrush(Color.FromArgb(50, Color.Green));
            polygon5.Fill = brush5;
            polygonOverlay.Polygons.Add(polygon5);
            GMap.NET.WindowsForms.GMapPolygon polygon6 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints6, "My Polygon");
            Pen pen6 = new Pen(Color.Green, 2);
            polygon6.Stroke = pen6;
            SolidBrush brush6 = new SolidBrush(Color.FromArgb(50, Color.Green));
            polygon6.Fill = brush6;
            polygonOverlay.Polygons.Add(polygon6);
            GMap.NET.WindowsForms.GMapPolygon polygon7 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints7, "My Polygon");
            Pen pen7 = new Pen(Color.Green, 2);
            polygon7.Stroke = pen7;
            SolidBrush brush7 = new SolidBrush(Color.FromArgb(50, Color.Green));
            polygon7.Fill = brush7;
            polygonOverlay.Polygons.Add(polygon7);
            GMap.NET.WindowsForms.GMapPolygon polygon8 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints8, "My Polygon");
            Pen pen8 = new Pen(Color.Yellow, 2);
            polygon8.Stroke = pen8;
            SolidBrush brush8 = new SolidBrush(Color.FromArgb(50, Color.Yellow));
            polygon8.Fill = brush8;
            polygonOverlay.Polygons.Add(polygon8);
            GMap.NET.WindowsForms.GMapPolygon polygon9 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints9, "My Polygon");
            Pen pen9 = new Pen(Color.Yellow, 2);
            polygon9.Stroke = pen9;
            SolidBrush brush9 = new SolidBrush(Color.FromArgb(50, Color.Yellow));
            polygon9.Fill = brush9;
            polygonOverlay.Polygons.Add(polygon9);
            GMap.NET.WindowsForms.GMapPolygon polygon10 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints10, "My Polygon");
            Pen pen10 = new Pen(Color.Yellow, 2);
            polygon10.Stroke = pen10;
            SolidBrush brush10 = new SolidBrush(Color.FromArgb(50, Color.Yellow));
            polygon10.Fill = brush10;
            polygonOverlay.Polygons.Add(polygon10);
            GMap.NET.WindowsForms.GMapPolygon polygon11 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints11, "My Polygon");
            Pen pen11 = new Pen(Color.Yellow, 2);
            polygon11.Stroke = pen11;
            SolidBrush brush11 = new SolidBrush(Color.FromArgb(50, Color.Yellow));
            polygon11.Fill = brush11;
            polygonOverlay.Polygons.Add(polygon11);
            GMap.NET.WindowsForms.GMapPolygon polygon12 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints12, "My Polygon");
            Pen pen12 = new Pen(Color.Yellow, 2);
            polygon12.Stroke = pen12;
            SolidBrush brush12 = new SolidBrush(Color.FromArgb(50, Color.Yellow));
            polygon12.Fill = brush12;
            polygonOverlay.Polygons.Add(polygon12);
            GMap.NET.WindowsForms.GMapPolygon polygon13 = new GMap.NET.WindowsForms.GMapPolygon(polygonPoints13, "My Polygon");
            Pen pen13 = new Pen(Color.Yellow, 2);
            polygon13.Stroke = pen13;
            SolidBrush brush13 = new SolidBrush(Color.FromArgb(50, Color.Yellow));
            polygon13.Fill = brush13;
            polygonOverlay.Polygons.Add(polygon13);

            // Add the overlay to the map control
            gMapControl1.Overlays.Add(polygonOverlay);
            gMapControl1.Refresh();
        }

        private void Performance_Load(object sender, EventArgs e)
        {
            


        }
        public void getMapPointsCAT10(DataTable MAP)
        {
           this.MapCAT10= MAP;
        }

        
        
        public void getMapPointsCAT21(DataTable MAP)
        {
            this.MapCAT21 = MAP;
        }

        public void getfileloaded(bool file)
        {
            this.fileloaded = file;
        }

        bool IsPointInPolygon(PointLatLng point, List<PointLatLng> polygon)
        {
            int windingNumber = 0;
            for (int i = 0; i < polygon.Count; i++)
            {
                int j = (i + 1) % polygon.Count;
                if (polygon[i].Lat <= point.Lat)
                {
                    if (polygon[j].Lat > point.Lat && IsLeft(polygon[i], polygon[j], point) > 0)
                    {
                        windingNumber++;
                    }
                }
                else
                {
                    if (polygon[j].Lat <= point.Lat && IsLeft(polygon[i], polygon[j], point) < 0)
                    {
                        windingNumber--;
                    }
                }
            }
            return windingNumber != 0;
        }

        double IsLeft(PointLatLng a, PointLatLng b, PointLatLng c)
        {
            return (b.Lng - a.Lng) * (c.Lat - a.Lat) - (b.Lat - a.Lat) * (c.Lng - a.Lng);
        }

        bool IsMarkerInsidePolygon(GMarkerGoogle marker, List<PointLatLng> polygon)
        {
            return IsPointInPolygon(marker.Position, polygon);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DataTable MAP = this.MapCAT10;
            markerOverlay = new GMapOverlay("Marker");
            this.MapCAT10 = MAP.AsEnumerable().Where(row => row.Field<string>("Target Report Descriptor").Contains("TOT: Aircraft")).CopyToDataTable();
            List<string> distinctIds = this.MapCAT10.AsEnumerable().Select(row => row.Field<string>("Target_ID")).Distinct().ToList();
            int a = 0;
            while (a < distinctIds.Count)
            {
                this.MapCAT10 = MAP.AsEnumerable().Where(row => row.Field<string>("Target_ID").Contains(distinctIds[a]) && row.Field<string>("Target Report Descriptor").Contains("TOT: Aircraft")).CopyToDataTable();
                DataView dataView = new DataView(this.MapCAT10);
                this.inside_taxi_zone = this.MapCAT10.Clone();
                this.inside_taxi_zone.Clear();
                this.inside_runway_zone = this.MapCAT10.Clone();
                this.inside_runway_zone.Clear();
                this.inside_apron_zone = this.MapCAT10.Clone();
                this.inside_apron_zone.Clear();
                this.inside_airbone_zone = this.MapCAT10.Clone();
                this.inside_airbone_zone.Clear();
                this.inside_stand_zone = this.MapCAT10.Clone();
                this.inside_stand_zone.Clear();

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

                    this.AirportGeodesic = new CoordinatesWGS84(41.2970767 * (Math.PI / 180), 2.07846278 * (Math.PI / 180));
                    this.markerCAT10 = bmpMarker3;

                    try
                    {
                        CoordinatesXYZ ObjectCartesian = new CoordinatesXYZ(Convert.ToDouble(latitude10.Substring(0, latitude10.Length - 1)), Convert.ToDouble(longitude10.Substring(0, longitude10.Length - 1)), 0);
                        PointLatLng pos = convertor.Cartesian_2_WGS84(ObjectCartesian, AirportGeodesic);
                        CoordinatesWGS84 ObjectWGS84 = new CoordinatesWGS84(pos.Lat, pos.Lng, 0);
                        marker2 = new GMarkerGoogle(new PointLatLng(ObjectWGS84.Lat, ObjectWGS84.Lon), markerCAT10);
                        this.markerslist.Add(marker2);
                        bool is_inside_taxi_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_TAXI);
                        bool is_inside_runway1_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_RUNWAY1);
                        bool is_inside_runway2_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_RUNWAY2);
                        bool is_inside_runway3_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_RUNWAY3);
                        bool is_inside_apron1_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_APRON1);
                        bool is_inside_apron2_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_APRON2);
                        bool is_inside_apron3_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_APRON3);
                        bool is_inside_airbone1_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_AIRBONE1);
                        bool is_inside_airbone2_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_AIRBONE2);
                        bool is_inside_airbone3_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_AIRBONE3);
                        bool is_inside_airbone4_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_AIRBONE4);
                        bool is_inside_airbone5_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_AIRBONE5);
                        bool is_inside_airbone6_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_AIRBONE6);
                        bool is_inside_stand1_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_STAND1);
                        bool is_inside_stand2_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_STAND2);
                        bool is_inside_stand3_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_STAND3);
                        bool is_inside_stand4_zone = IsMarkerInsidePolygon(marker2, this.polygonlist_STAND4);

                        if (is_inside_taxi_zone == true)
                        {
                            DataRow sourceRow = actualflights2.Rows[x];
                            this.inside_taxi_zone.Rows.Add(sourceRow.ItemArray);
                            markerOverlay.Markers.Add(marker2);
                            gMapControl1.UpdateMarkerLocalPosition(marker2);
                        }

                        if (is_inside_runway1_zone == true || is_inside_runway2_zone == true || is_inside_runway3_zone == true)
                        {
                            DataRow sourceRow = actualflights2.Rows[x];
                            this.inside_runway_zone.Rows.Add(sourceRow.ItemArray);
                            markerOverlay.Markers.Add(marker2);
                            gMapControl1.UpdateMarkerLocalPosition(marker2);
                        }

                        if (is_inside_apron1_zone == true || is_inside_apron2_zone == true || is_inside_apron3_zone == true)
                        {
                            DataRow sourceRow = actualflights2.Rows[x];
                            this.inside_apron_zone.Rows.Add(sourceRow.ItemArray);
                            markerOverlay.Markers.Add(marker2);
                            gMapControl1.UpdateMarkerLocalPosition(marker2);
                        }

                        if (is_inside_airbone1_zone == true || is_inside_airbone2_zone == true || is_inside_airbone3_zone == true || is_inside_airbone4_zone == true || is_inside_airbone5_zone == true || is_inside_airbone6_zone == true)
                        {
                            DataRow sourceRow = actualflights2.Rows[x];
                            this.inside_airbone_zone.Rows.Add(sourceRow.ItemArray);
                            markerOverlay.Markers.Add(marker2);
                            gMapControl1.UpdateMarkerLocalPosition(marker2);
                        }

                        if (is_inside_stand1_zone == true || is_inside_stand2_zone == true || is_inside_stand3_zone == true || is_inside_stand4_zone == true)
                        {
                            DataRow sourceRow = actualflights2.Rows[x];
                            this.inside_stand_zone.Rows.Add(sourceRow.ItemArray);
                            markerOverlay.Markers.Add(marker2);
                            gMapControl1.UpdateMarkerLocalPosition(marker2);
                        }

                    }
                    catch { }



                    x = x + 1;
                }
                gMapControl1.Overlays.Add(markerOverlay);
                gMapControl1.Refresh();

                int positions_taxi = this.inside_taxi_zone.Rows.Count;
                foreach (DataRow row in this.inside_taxi_zone.Rows)
                {
                    string timeValue = row["Time_of_Day"].ToString();
                    row["Time_of_Day"] = DateTime.ParseExact(timeValue, "HH:mm:ss:fff", CultureInfo.InvariantCulture);
                }
                try
                {
                    string timeStr = Convert.ToString(Convert.ToDateTime(this.inside_taxi_zone.Rows[this.inside_taxi_zone.Rows.Count - 1]["Time_of_Day"]) - Convert.ToDateTime(this.inside_taxi_zone.Rows[0]["Time_of_Day"]));
                    TimeSpan timeSpan = TimeSpan.Parse(timeStr);
                    double totalSeconds = timeSpan.TotalSeconds;
                    totalSeconds += 1;
                    this.updates_taxi = this.updates_taxi + positions_taxi;
                    this.expected_taxi = this.expected_taxi + totalSeconds;
                }
                catch { }

                int positions_runway = this.inside_runway_zone.Rows.Count;
                foreach (DataRow row in this.inside_runway_zone.Rows)
                {
                    string timeValue = row["Time_of_Day"].ToString();
                    row["Time_of_Day"] = DateTime.ParseExact(timeValue, "HH:mm:ss:fff", CultureInfo.InvariantCulture);
                }
                try
                {
                    string timeStr = Convert.ToString(Convert.ToDateTime(this.inside_runway_zone.Rows[this.inside_runway_zone.Rows.Count - 1]["Time_of_Day"]) - Convert.ToDateTime(this.inside_runway_zone.Rows[0]["Time_of_Day"]));
                    TimeSpan timeSpan = TimeSpan.Parse(timeStr);
                    double totalSeconds = timeSpan.TotalSeconds;
                    totalSeconds += 1;
                    this.updates_runway = this.updates_runway + positions_runway;
                    this.expected_runway = this.expected_runway + totalSeconds;
                    //this.updates_taxi = this.updates_taxi + positions_taxi;
                    //this.expected_taxi = this.expected_taxi + totalSeconds;
                }
                catch { }

                int positions_apron = this.inside_apron_zone.Rows.Count;
                foreach (DataRow row in this.inside_apron_zone.Rows)
                {
                    string timeValue = row["Time_of_Day"].ToString();
                    row["Time_of_Day"] = DateTime.ParseExact(timeValue, "HH:mm:ss:fff", CultureInfo.InvariantCulture);
                }
                try
                {
                    string timeStr = Convert.ToString(Convert.ToDateTime(this.inside_apron_zone.Rows[this.inside_apron_zone.Rows.Count - 1]["Time_of_Day"]) - Convert.ToDateTime(this.inside_apron_zone.Rows[0]["Time_of_Day"]));
                    TimeSpan timeSpan = TimeSpan.Parse(timeStr);
                    double totalSeconds = timeSpan.TotalSeconds;
                    totalSeconds += 1;
                    this.updates_apron = this.updates_apron + positions_apron;
                    this.expected_apron = this.expected_apron + totalSeconds;
                }
                catch { }

                int positions_airbone = this.inside_airbone_zone.Rows.Count;
                foreach (DataRow row in this.inside_airbone_zone.Rows)
                {
                    string timeValue = row["Time_of_Day"].ToString();
                    row["Time_of_Day"] = DateTime.ParseExact(timeValue, "HH:mm:ss:fff", CultureInfo.InvariantCulture);
                }
                try
                {
                    string timeStr = Convert.ToString(Convert.ToDateTime(this.inside_airbone_zone.Rows[this.inside_airbone_zone.Rows.Count - 1]["Time_of_Day"]) - Convert.ToDateTime(this.inside_airbone_zone.Rows[0]["Time_of_Day"]));
                    TimeSpan timeSpan = TimeSpan.Parse(timeStr);
                    double totalSeconds = timeSpan.TotalSeconds;
                    totalSeconds += 1;
                    this.updates_airbone = this.updates_airbone + positions_airbone;
                    this.expected_airbone = this.expected_airbone + totalSeconds;
                }
                catch { }

                int positions_stand = this.inside_stand_zone.Rows.Count;
                foreach (DataRow row in this.inside_stand_zone.Rows)
                {
                    string timeValue = row["Time_of_Day"].ToString();
                    row["Time_of_Day"] = DateTime.ParseExact(timeValue, "HH:mm:ss:fff", CultureInfo.InvariantCulture);
                }
                try
                {
                    string timeStr = Convert.ToString(Convert.ToDateTime(this.inside_stand_zone.Rows[this.inside_stand_zone.Rows.Count - 1]["Time_of_Day"]) - Convert.ToDateTime(this.inside_stand_zone.Rows[0]["Time_of_Day"]));
                    TimeSpan timeSpan = TimeSpan.Parse(timeStr);
                    double totalSeconds = timeSpan.TotalSeconds;
                    totalSeconds += 1;
                    this.updates_stand = this.updates_stand + positions_stand;
                    this.expected_stand = this.expected_stand + totalSeconds;
                }
                catch { }

                a++;
            }

            this.updateratelist_stand = this.updates_stand / this.expected_stand;
            this.updateratelist_taxi = this.updates_taxi / this.expected_taxi;
            this.updateratelist_runway = this.updates_runway / this.expected_runway;
            this.updateratelist_apron = this.updates_apron / this.expected_apron;
            this.updateratelist_airbone = this.updates_airbone / this.expected_airbone;
            label25.Text = Convert.ToString(Convert.ToInt32(this.updateratelist_stand*100)) + " %";
            circularProgressBar1.Value = Convert.ToInt32(this.updateratelist_stand*100);
            label2.Text = Convert.ToString(Convert.ToInt32(this.updateratelist_apron * 100)) + " %";
            circularProgressBar2.Value = Convert.ToInt32(this.updateratelist_apron * 100);
            label4.Text = Convert.ToString(Convert.ToInt32(this.updateratelist_airbone * 100)) + " %";
            circularProgressBar3.Value = Convert.ToInt32(this.updateratelist_airbone * 100);
            label6.Text = Convert.ToString(Convert.ToInt32(this.updateratelist_taxi * 100)) + " %";
            circularProgressBar4.Value = Convert.ToInt32(this.updateratelist_taxi * 100);

        }
    }
}
