using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_1
{
    public class CAT10
    {
        // In this class we are going to decode all the category 10 messages

        Conversions convertor = new Conversions();
        public string[] message;
        public string Full_FSPEC;
        public string CAT = "10";
        public int num;
        public int cat10num;
        public int airportCode;

        public string MessageTYPE;             // DATA ITEM I010/000
        public string SAC;                     // DATA ITEM I010/010
        public string SIC;                     // DATA ITEM I010/010
        public string TYP;                     // DATA ITEM I010/020
        public string DCR;                     // DATA ITEM I010/020
        public string CHN;                     // DATA ITEM I010/020
        public string GBS;                     // DATA ITEM I010/020
        public string CRT;                     // DATA ITEM I010/020
        public string SIM;                     // DATA ITEM I010/020
        public string TST;                     // DATA ITEM I010/020
        public string RAB;                     // DATA ITEM I010/020
        public string LOP;                     // DATA ITEM I010/020
        public string TOT;                     // DATA ITEM I010/020
        public string SPI;                     // DATA ITEM I010/020

        public string Time_of_day_in_format;   // DATA ITEM I010/140
        public int Time_of_day_in_seconds;     // DATA ITEM I010/140

        public string Latitude_WGS84;
        public string Longitude_WGS84;


        public string Track_number;   // DATA ITEM I010/161






        public CAT10(string[] message_in_hexa, Conversions convertor)
        {
            this.message = convertor.Full_Message_To_Binary(message_in_hexa);

            bool i = false;
            int a = 3; // We start in the FSPEC octet
            Full_FSPEC = "";
            while (i == false)
            {
                if (message[a].EndsWith("1"))
                    Full_FSPEC = Full_FSPEC + message[a].Remove(message[a].Length - 1);
                else
                    i = true;

                // The last case of FSPEC
                if (a>=3 && i==true)
                {
                    if (message[a - 1].EndsWith("1"))
                    {
                        Full_FSPEC = Full_FSPEC + message[a].Remove(message[a].Length - 1);
                    }
                }

                if (a==3 && i==true)
                {
                    Full_FSPEC = message[a].Remove(message[a].Length - 1);
                }
                a++;
            }

            char[] FSPEC = Full_FSPEC.ToCharArray(0, Full_FSPEC.Length);
            int position = a; // Number of octets that fill the FSPEC 


            // DEFINITION OF FSPEC
            if (FSPEC[0] == '1')
                position = this.Data_Source_Identifier(message, position);
            if (FSPEC[1] == '1')
                position = this.Message_Type(message, position);
            if (FSPEC[2] == '1')
                position = this.Target_Report_Description(message, position);
            if (FSPEC[3] == '1')
                position = this.Time_of_Day(message, position);
            if (FSPEC[4] == '1')
                position = this.Position_in_WGS84_Coordinates(message, position);
            if (FSPEC[5] == '1')
                position = this.Measured_Position_in_Polar_Coordinates(message, position);
            if (FSPEC[6] == '1')
                position = this.Position_in_Cartesian_Coordinates(message, position);





        }


        // DATA ITEM I010/000 [Message Type]
        public int Message_Type(string[] message, int position)
        {
            int value = Int32.Parse(message[position].Substring(message[position].Length - 1));

            if (value == 1)
                this.MessageTYPE = "Target Report";
            if (value == 2)
                this.MessageTYPE = "Start of Update Cycle ";
            if (value == 3)
                this.MessageTYPE = "Periodic Status Message";
            if (value == 4)
                this.MessageTYPE = "Event-triggered Status Message";

            position = position + 1;

            return position;
        }



        // DATA ITEM I010/010 [Data Source Identifier]
        public int Data_Source_Identifier(string[] message,int position)
        {
            this.SAC = convertor.Binary_Octet_To_Hexadecimal(message[position]);
            this.SIC = convertor.Binary_Octet_To_Hexadecimal(message[position+1]);
            position = position+2;

            return position;
        }



        // DATA ITEM I010/020 [Target Report Description]
        public int Target_Report_Description(string[] message, int position)
        {
            this.TYP = message[position].Substring(0, 3);
            if (this.TYP == "000") { this.TYP = "SSR multilateration"; }
            if (this.TYP == "001") { this.TYP = "Mode S multilateration"; }
            if (this.TYP == "010") { this.TYP = "ADS-B"; }
            if (this.TYP == "011") { this.TYP = "PSR"; }
            if (this.TYP == "100") { this.TYP = "Magnetic Loop System"; }
            if (this.TYP == "101") { this.TYP = "HF multilateration"; }
            if (this.TYP == "110") { this.TYP = "Not defined"; }
            if (this.TYP == "111") { this.TYP = "Other types"; }

            this.DCR = message[position].Substring(3, 1);
            if (this.DCR == "0") { this.DCR = "No differential correction (ADS-B)"; }
            if (this.DCR == "1") { this.DCR = "Differential correction (ADS-B)"; }

            this.CHN = message[position].Substring(4, 1);
            if (this.CHN == "0") { this.CHN = "Chain 1"; }
            if (this.CHN == "1") { this.CHN = "Chain 2"; }

            this.GBS = message[position].Substring(5, 1);
            if (this.GBS == "0") { this.GBS = "Transponder Ground bit not set"; }
            if (this.GBS == "1") { this.GBS = "Transponder Ground bit set"; }

            this.CRT = message[position].Substring(6, 1);
            if (this.CRT == "0") { this.CRT = "No Corrupted reply in multilateration"; }
            if (this.CRT == "1") { this.CRT = "Corrupted replies in multilateration"; }

            if(message[position].Substring(7,1) == "1")
            {
                // Extension into first extent
                position = position + 1;
                this.SIM = message[position].Substring(0, 1);
                if (this.SIM == "0") { this.SIM = "Actual target report"; }
                if (this.SIM == "1") { this.SIM = "Simulated target report"; }

                this.TST = message[position].Substring(1, 1);
                if (this.TST == "0") { this.TST = "Default"; }
                if (this.TST == "1") { this.TST = "Test Target"; }

                this.RAB = message[position].Substring(2, 1);
                if (this.RAB == "0") { this.RAB = "Report from target transponder"; }
                if (this.RAB == "1") { this.RAB = "Report from field monitor (fixed transponder)"; }

                this.LOP = message[position].Substring(3, 2);
                if (this.LOP == "00") { this.LOP = "Undetermined"; }
                if (this.LOP == "01") { this.LOP = "Loop start"; }
                if (this.LOP == "10") { this.LOP = "Loop finish"; }

                this.TOT = message[position].Substring(5, 2);
                if (this.TOT == "00") { this.TOT = "Undetermined"; }
                if (this.TOT == "01") { this.TOT = "Aircraft"; }
                if (this.TOT == "10") { this.TOT = "Ground vehicle"; }
                if (this.TOT == "11") { this.TOT = "Helicopter"; }

                if (message[position].Substring(7, 1) == "1")
                {
                    // Extension into next extent
                    position = position + 1;
                    this.SPI = message[position].Substring(0, 1);
                    if (this.SPI == "0") { this.SPI = "Absense of SPI"; }
                    if (this.SPI == "1") { this.SPI = "Special Position Identification"; }
                    position = position + 1;
                }
                else
                {
                    // End of the Data Item
                    position = position + 1;
                }
            }
            else
            {
                // End of Data Item
                position = position + 1;
            }
            return position;
        }



        // DATA ITEM I010/040 [Measured Position in Polar Coordinates]
        public int Measured_Position_in_Polar_Coordinates(string[] message, int position)
        {
            // FALTA ACABAR
            return position;
        }
        // DATA ITEM I010/041 [Position in WGS-84 Coordinates]
        public int Position_in_WGS84_Coordinates(string[] message, int position)
        {
            string Latitude = convertor.Twos_Complement(string.Concat(message[position], message[position + 1], message[position + 2], message[position + 3]));
            string Longitude = convertor.Twos_Complement(string.Concat(message[position + 4], message[position + 5], message[position + 6], message[position + 7]));

            // FALTA TERMINARLA


            position = position + 8;
            return position;
        }


        // DATA ITEM I010/042 [Position in Cartesioan Coordinates]
        public int Position_in_Cartesian_Coordinates(string[] message, int position)
        {
            // FALTA ACABAR
            return position;
        }


        // DATA ITEM I010/060 [Mode-3/A Code in Octal Representation]

        // DATA ITEM I010/090 [Flight Level in Binary Representation]

        // DATA ITEM I010/091 [Measured Height]

        // DATA ITEM I010/131 [Amplitude of Primariy Plot]

        // DATA ITEM I010/140 [Time of Day]
        public int Time_of_Day(string[] message, int position)
        {
            string binaryTime = string.Concat(message[position], message[position + 1], message[position + 2]);
            int timeInSeconds = Convert.ToInt32(binaryTime, 2) / 128;
            Time_of_day_in_seconds = timeInSeconds;
            TimeSpan timeOfDay = TimeSpan.FromSeconds(timeInSeconds);
            Time_of_day_in_format = timeOfDay.ToString(@"hh\:mm\:ss\:fff");
            position= position + 3;

            return position;
        }


        // DATA ITEM I010/161 [Track Number]
        public int Track_Number(string[] message, int position)
        {
            this.Track_number = Convert.ToInt32(message[position], 2).ToString("X");
            position = position + 2;

            return position;
        }



        // DATA ITEM I010/170 [Track Status]

        // DATA ITEM I010/200 [Calculated Track Velocity in Polar Coordiantes]

        // DATA ITEM I010/202 [Calculated Track Velocity in Cartesian Cooridinates]

        // DATA ITEM I010/210 [Calculated Acceleration]

        // DATA ITEM I010/220 [Target Address]

        // DATA ITEM I010/245 [Target Identification]

        // DATA ITEM I010/250 [Mode S MB Data]

        // DATA ITEM I010/270 [Target Size & Orientation]

        // DATA ITEM I010/280 [Presence]

        // DATA ITEM I010/300 [Vehicle Fleet Identification]

        // DATA ITEM I010/310 [Pre-programed Message]

        // DATA ITEM I010/500 [Standard Deviation of Position]

        // DATA ITEM I010/550 [System Status]



    }
}
