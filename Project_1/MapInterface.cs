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
        // We find the directory where the application is running to know the path to the icons that we will use
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


        public MapInterface()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
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
            iconPictureBox6.IconColor = Color.DarkOrange;
        }

        private void iconPictureBox6_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox6.IconColor = Color.White;
        }


        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            if (this.fileloaded == true)
            {
                if (this.playbuttonselected == 0)
                {
                    iconPictureBox1.IconColor = Color.Red;
                    iconPictureBox2.IconColor = Color.White;
                    this.playbuttonselected = 1;
                    this.pausebuttonselected = 0;
                }


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
            else
            {
                exception.ShowDialog();
            }
        }



        
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Retrieve the time value from the current row
            string timeValue = timeIntervalList[x];
            this.first_time = timeIntervalList.First().ToString();
            this.last_time = timeIntervalList.Last().ToString();
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

        int a = 0;
        private void SIMULATION()
        {
            if (this.SMRmarkers == true || this.MLATmarkers== true || this.ADSBmarkers == true)
            {
                gMapControl1.Overlays.Clear();
                //label25.Text = "08:00:47";

                DataView dataView2 = new DataView(this.MapCAT21);
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

                DataView dataView = new DataView(this.MapCAT10);

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
                label13.Text = fifthElement.Replace("Latitude: ", "");
                label11.Text = sixthElement.Replace("Longitude: ", "");
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
            label13.Text = "No Data";
            label11.Text = "No Data";
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
                    timer1.Interval = 500;
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
                    timer1.Interval = 100;
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

        private void iconPictureBox6_Click(object sender, EventArgs e)
        {
            if (this.fileloaded == true)
            {
                //FALTA ACABAR
            }
            else
            {
                exception.ShowDialog();
            }
        }

        
    }
}
