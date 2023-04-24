using GMap.NET;
using MultiCAT6.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project_1
{
    public class Conversions
    {
        public Dictionary<char, string> Hexadecimal_To_Binary = new Dictionary<char, string>
        {
            { '0', "0000" },
            { '1', "0001" },
            { '2', "0010" },
            { '3', "0011" },
            { '4', "0100" },
            { '5', "0101" },
            { '6', "0110" },
            { '7', "0111" },
            { '8', "1000" },
            { '9', "1001" },
            { 'A', "1010" },
            { 'B', "1011" },
            { 'C', "1100" },
            { 'D', "1101" },
            { 'E', "1110" },
            { 'F', "1111" }
        };

        public Dictionary<string, char> Binary_To_Hexadecimal = new Dictionary<string, char>
        {
            {"0000", '0' },
            {"0001", '1'},
            {"0010",'2' },
            {"0011",'3' },
            {"0100",'4' },
            {"0101",'5' },
            {"0110",'6' },
            {"0111",'7' },
            {"1000",'8' },
            {"1001",'9' },
            {"1010",'A' },
            {"1011",'B' },
            {"1100",'C' },
            {"1101",'D' },
            {"1110",'E' },
            {"1111",'F' }
        };

        public string Binary_Octet_To_Hexadecimal(string binaryoctet)
        {
            string hexamessage = Convert.ToInt32(binaryoctet, 2).ToString("X");
            return hexamessage;
        }

        public string Hexadecimal_To_Binary_Octet(string hexamessage)
        {
            // If the hexadecimal only has one character we add a zero before to allow the conversion to binary work correcly
            if (hexamessage.Length == 1)
            {
                hexamessage = string.Concat('0', hexamessage);
            }

            StringBuilder Octet = new StringBuilder();
            foreach (char a in hexamessage)
            {
                Octet.Append(Hexadecimal_To_Binary[char.ToUpper(Convert.ToChar(a))]);
            }
            return Octet.ToString();
        }

        public string[] Full_Message_To_Binary(string[] message)
        {
            string[] Message_In_Binary = new string[message.Length];
            int i = 0;
            while (i < message.Length)
            {
                Message_In_Binary[i] = this.Hexadecimal_To_Binary_Octet(message[i]);
                i++;
            }

            return Message_In_Binary;
        }

        public string Twos_Complement(string binarynumber)
        {
            string complement = "";
            bool flip = false;

            for (int i = binarynumber.Length - 1; i >= 0; i--)
            {
                if (binarynumber[i] == '0' && !flip)
                {
                    complement = "0" + complement;
                }
                else if (binarynumber[i] == '1' && !flip)
                {
                    flip = true;
                    complement = "1" + complement;
                }
                else if (binarynumber[i] == '0' && flip)
                {
                    complement = "1" + complement;
                }
                else if (binarynumber[i] == '1' && flip)
                {
                    complement = "0" + complement;
                }
            }

            if (!flip)
            {
                complement = binarynumber.PadLeft(complement.Length + 1, '0');
            }

            return complement;
        }

        public double TWO_Complement(string bits)
        {
            if (bits[0] == '0')
            {
                int num = Convert.ToInt32(bits, 2);
                return Convert.ToDouble(num);
            }
            else
            {
                string invertedBits = "";
                for (int i = 1; i < bits.Length; i++)
                {
                    invertedBits += bits[i] == '1' ? '0' : '1';
                }
                int num = Convert.ToInt32(invertedBits, 2);
                return -(num + 1);
            }
        }

        public double DMSToDD_Latitude(string Latitude)
        {
            int degreeIndex = Latitude.IndexOf("º");
            int minuteIndex = Latitude.IndexOf("'");
            int secondIndex = Latitude.LastIndexOf("''");
            string degreeString = Latitude.Substring(0, degreeIndex);
            string minuteString = Latitude.Substring(degreeIndex + 2, minuteIndex - degreeIndex - 2);
            string secondString = Latitude.Substring(minuteIndex + 2, secondIndex - minuteIndex - 2);
            int degreeValue = int.Parse(degreeString);
            int minuteValue = int.Parse(minuteString);
            double secondValue = double.Parse(secondString);

            double dd = degreeValue + (Convert.ToDouble(minuteValue) / 60) + (secondValue/3600);

            return dd;
        }

        public double DMSToDD_Longitude(string Longitude)
        {
            int degreeIndex = Longitude.IndexOf("º");
            int minuteIndex = Longitude.IndexOf("'");
            int secondIndex = Longitude.LastIndexOf("''");
            string degreeString = Longitude.Substring(0, degreeIndex);
            string minuteString = Longitude.Substring(degreeIndex + 2, minuteIndex - degreeIndex - 2);
            string secondString = Longitude.Substring(minuteIndex + 2, secondIndex - minuteIndex - 2);
            int degreeValue = int.Parse(degreeString);
            int minuteValue = int.Parse(minuteString);
            double secondValue = double.Parse(secondString);

            double dd = degreeValue + (Convert.ToDouble(minuteValue) / 60) + (secondValue / 3600);

            return dd;
        }



        //public PointLatLng ComputeWGS_84_from_Cartesian(Point p, string SIC)
        //{
        //    PointLatLng pos = new PointLatLng();
        //    //PointLatLng ARPBarcelona = new PointLatLng(41.2970767, 2.07846278);
        //    double X = p.X;
        //    double Y = p.Y;
        //    CoordinatesXYZ ObjectCartesian = new CoordinatesXYZ(X, Y, 0); //We pass from Point to CoordinatesXYZ to be able to work with the GeoUtils library
        //    PointLatLng AirportPoint = GetCoordenatesSMRMALT(Convert.ToInt32(SIC)); //We get the Radar coordinates from its SIC
        //    CoordinatesWGS84 AirportGeodesic = new CoordinatesWGS84(AirportPoint.Lat * (Math.PI / 180), AirportPoint.Lng * (Math.PI / 180)); //We went from PointLatLng to Coordinates WGS84 to be able to work with GeoUtils. Coordinates must be passed from degrees to radians
        //    GeoUtils geoUtils = new GeoUtils();
        //    CoordinatesWGS84 MarkerGeodesic = geoUtils.change_system_cartesian2geodesic(ObjectCartesian, AirportGeodesic); //We apply the change from CoordiantesXYZ to Coordinate GS83
        //    geoUtils = null;
        //    double LatitudeWGS_84_map = MarkerGeodesic.Lat * (180 / Math.PI);
        //    double LongitudeWGS_84_map = MarkerGeodesic.Lon * (180 / Math.PI);
        //    pos.Lat = LatitudeWGS_84_map;
        //    pos.Lng = LongitudeWGS_84_map;
        //    return pos;
        //}
    }
}
