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
        public string RHO;                     // DATA ITEM I010/040
        public string Theta;                   // DATA ITEM I010/040
        public string Latitude_WGS84;          // DATA ITEM I010/041
        public string Longitude_WGS84;         // DATA ITEM I010/041
        public string X_Component;             // DATA ITEM I010/042
        public string Y_Component;             // DATA ITEM I010/042
        public string V_Mode3A;                // DATA ITEM I010/060
        public string G_Mode3A;                // DATA ITEM I010/060
        public string L_Mode3A;                // DATA ITEM I010/060
        public string Mode3A_reply;            // DATA ITEM I010/060
        public string V_FL;                    // DATA ITEM I010/090
        public string G_FL;                    // DATA ITEM I010/090
        public string FL;                      // DATA ITEM I010/090
        public string MHeight;                 // DATA ITEM I010/091
        public string PAM;                     // DATA ITEM I010/131
        public string Time_of_day_in_format;   // DATA ITEM I010/140
        public int Time_of_day_in_seconds;     // DATA ITEM I010/140
        public string Track_number;            // DATA ITEM I010/161
        public string CNF;                     // DATA ITEM I010/170
        public string TRE;                     // DATA ITEM I010/170
        public string CST;                     // DATA ITEM I010/170
        public string MAH;                     // DATA ITEM I010/170
        public string TCC;                     // DATA ITEM I010/170
        public string STH;                     // DATA ITEM I010/170
        public string TOM;                     // DATA ITEM I010/170
        public string DOU;                     // DATA ITEM I010/170
        public string MRS;                     // DATA ITEM I010/170
        public string GHO;                     // DATA ITEM I010/170
        public string Ground_Speed;            // DATA ITEM I010/200
        public string Track_Angle;             // DATA ITEM I010/200
        public string Vx;                      // DATA ITEM I010/202
        public string Vy;                      // DATA ITEM I010/202
        public string Ax;                      // DATA ITEM I010/210
        public string Ay;                      // DATA ITEM I010/210
        public string TargetAddress;           // DATA ITEM I010/220
        public string STI;                     // DATA ITEM I010/245
        public string Target_ID;               // DATA ITEM I010/245
        public string REP_ModeS;               // DATA ITEM I010/250
        public string[] MB_Data_ModeS;         // DATA ITEM I010/250
        public string[] BDS1_ModeS;            // DATA ITEM I010/250
        public string[] BDS2_ModeS;            // DATA ITEM I010/250
        public string Lenght;                  // DATA ITEM I010/270
        public string Orientation;             // DATA ITEM I010/270
        public string Width;                   // DATA ITEM I010/270
        public int REP;                        // DATA ITEM I010/270
        public string[] DRHO;                  // DATA ITEM I010/270
        public string[] DTHETA;                // DATA ITEM I010/270
        public string Vehicle_Fleet_ID;        // DATA ITEM I010/300
        public string TRB;                     // DATA ITEM I010/310
        public string MSG;                     // DATA ITEM I010/310
        public string omega_x;                 // DATA ITEM I010/500
        public string omega_y;                 // DATA ITEM I010/500
        public string omega_xy;                // DATA ITEM I010/500
        public string NOGO;                    // DATA ITEM I010/550
        public string OVL;                     // DATA ITEM I010/550
        public string TSV;                     // DATA ITEM I010/550
        public string DIV;                     // DATA ITEM I010/550
        public string TTF;                     // DATA ITEM I010/550




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

            if (FSPEC.Count() > 8)
            {
                if (FSPEC[7] == '1')
                    position = this.Calculated_Track_Velocity_in_Polar_Coordinates(message, position);
                if (FSPEC[8] == '1')
                    position = this.Calculated_Track_Velocity_in_Cartesian_Coordiantes(message, position);
                if (FSPEC[9] == '1')
                    position = this.Track_Number(message, position);
                if (FSPEC[10] == '1')
                    position = this.Track_Status(message, position);
                if (FSPEC[11] == '1')
                    position = this.Mode3A_Code_in_Octal_Representation(message, position);
                if (FSPEC[12] == '1')
                    position = this.Target_Address(message, position);
                if (FSPEC[13] == '1')
                    position = this.Target_Identification(message, position);
            }

            if (FSPEC.Count() > 16)
            {
                if (FSPEC[14] == '1')
                    position = this.Mode_S_MB_Data(message, position);
                if (FSPEC[15] == '1')
                    position = this.Vehicle_Fleet_Identfication(message, position);
                if (FSPEC[16] == '1')
                    position = this.Flight_Level_in_Binary_Representation(message, position);
                if (FSPEC[17] == '1')
                    position = this.Measured_Height(message, position);
                if (FSPEC[18] == '1')
                    position = this.Target_Size__and_Orientation(message, position);
                if (FSPEC[19] == '1')
                    position = this.System_Status(message, position);
                if (FSPEC[20] == '1')
                    position = this.Preporgramed_Message(message, position);
            }

            if (FSPEC.Count() > 22)
            {
                if (FSPEC[21] == '1')
                    position = this.Standard_Deviation_of_Position(message, position);
                if (FSPEC[22] == '1')
                    position = this.Presence(message, position);
                if (FSPEC[23] == '1')
                    position = this.Amplitude_of_Primary_Plot(message, position);
                if (FSPEC[24] == '1')
                    position = this.Calculated_Acceleration(message, position);
            }
        }


        // DATA ITEM I010/000 [Message Type]----------DONE
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


        // DATA ITEM I010/010 [Data Source Identifier]------------DONE
        public int Data_Source_Identifier(string[] message,int position)
        {
            this.SAC = Convert.ToString(Convert.ToInt32(message[position], 2));
            this.SIC = Convert.ToString(Convert.ToInt32(message[position + 1], 2));
            position = position+2;

            return position;
        }


        // DATA ITEM I010/020 [Target Report Description]----------DONE
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


        // DATA ITEM I010/040 [Measured Position in Polar Coordinates]-------DONE
        public int Measured_Position_in_Polar_Coordinates(string[] message, int position)
        {
            this.RHO = Convert.ToString(Convert.ToInt32(string.Concat(message[position], message[position + 1]), 2)) + " m";
            this.Theta = String.Format("{0:0.00}", Convert.ToDouble(Convert.ToInt32(string.Concat(message[position + 2], message[position + 3]), 2)) * (360 / (Math.Pow(2, 16)))) + "°";
            position = position + 4;

            return position;
        }


        // DATA ITEM I010/041 [Position in WGS-84 Coordinates]------------DONE
        public int Position_in_WGS84_Coordinates(string[] message, int position)
        {
            double Latitude = convertor.TWO_Complement(string.Concat(message[position], message[position + 1], message[position + 2], message[position + 3])) * (180.0 / Math.Pow(2, 31));
            position += 4;
            double Longitude = convertor.TWO_Complement(string.Concat(message[position], message[position + 1], message[position + 2], message[position + 3])) * (180.0 / Math.Pow(2, 31));
            position += 4;
            int Latdegres = (int)Math.Truncate(Latitude);
            double Latmin = (Latitude - Latdegres) * 60.0;
            double Latsec = Math.Round((Latmin - Math.Truncate(Latmin)) * 60.0, 5);
            Latmin = Math.Truncate(Latmin);
            int Londegres = (int)Math.Truncate(Longitude);
            double Lonmin = (Longitude - Londegres) * 60.0;
            double Lonsec = Math.Round((Lonmin - Math.Truncate(Lonmin)) * 60.0, 5);
            Lonmin = Math.Truncate(Lonmin);
            this.Latitude_WGS84 = $"{Latdegres}º {Latmin}' {Latsec}''";
            this.Longitude_WGS84 = $"{Londegres}º {Lonmin}' {Lonsec}''";

            return position;
        }


        // DATA ITEM I010/042 [Position in Cartesioan Coordinates]-----------DONE
        public int Position_in_Cartesian_Coordinates(string[] message, int position)
        {
            this.X_Component = Convert.ToString(convertor.TWO_Complement(String.Concat(message[position], message[position + 1]))) + "m";
            this.Y_Component = Convert.ToString(convertor.TWO_Complement(String.Concat(message[position + 2], message[position + 3]))) + "m";
            position = position + 4;

            return position;
        }


        // DATA ITEM I010/060 [Mode-3/A Code in Octal Representation]----------DONE
        public int Mode3A_Code_in_Octal_Representation(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position],message[position +1]);
            this.V_Mode3A = fullmessage.Substring(0, 1);
            this.G_Mode3A = fullmessage.Substring(1, 1);
            this.L_Mode3A = fullmessage.Substring(2, 1);

            if (this.V_Mode3A == "0")
                this.V_Mode3A = "V: Code validated";
            else
                this.V_Mode3A = "V: Code not validated";

            if (this.G_Mode3A == "0")
                this.G_Mode3A = "G: Default";
            else
                this.G_Mode3A = "G: Garbled code";

            if (this.L_Mode3A == "0")
                this.L_Mode3A = "L: Mode-3/A code derived from the reply of the transponder";
            else
                this.L_Mode3A = "L: Mode-3/A code not extracted during the last scan";

            this.Mode3A_reply = fullmessage.Substring(4, 12);



            position = position + 2;

            return position;
        }


        // DATA ITEM I010/090 [Flight Level in Binary Representation]------------DONE
        public int Flight_Level_in_Binary_Representation(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1]);
            this.V_FL = fullmessage.Substring(0, 1);
            if (this.V_FL == "0") { this.V_FL = "Code validated"; }
            if (this.V_FL == "1") { this.V_FL = "Code not validated"; }

            this.G_FL = fullmessage.Substring(1, 1);
            if (this.G_FL == "0") { this.G_FL = "Default"; }
            if (this.G_FL == "1") { this.G_FL = "Garbled code"; }

            this.FL = V_FL = Convert.ToString(Convert.ToInt32(convertor.TWO_Complement(fullmessage.Substring(2, 14))) * (1 / 4));

            position = position + 2;

            return position;
        }


        // DATA ITEM I010/091 [Measured Height]-------------DONE
        public int Measured_Height(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1]);
            this.MHeight = Convert.ToString(Convert.ToInt32(convertor.Twos_Complement(fullmessage)) * 6.25) + "ft";
            position = position + 2;

            return position;
        }


        // DATA ITEM I010/131 [Amplitude of Primariy Plot]------------DONE
        public int Amplitude_of_Primary_Plot(string[] message, int position)
        {
            this.PAM = convertor.Binary_Octet_To_Hexadecimal(message[position]);
            position = position + 1;

            return position;
        }


        // DATA ITEM I010/140 [Time of Day]---------------DONE
        public int Time_of_Day(string[] message, int position)
        {
            int binaryValue = Convert.ToInt32(string.Concat(message[position], message[position + 1], message[position + 2]), 2);
            double seconds = binaryValue / 128.0;
            Time_of_day_in_seconds = Convert.ToInt32(Math.Truncate(seconds));
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            Time_of_day_in_format = time.ToString(@"hh\:mm\:ss\:fff");
            position = position + 3;

            return position;
        }


        // DATA ITEM I010/161 [Track Number]---------------DONE
        public int Track_Number(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1]).Substring(4, 12);
            this.Track_number = Convert.ToString(Convert.ToInt32(fullmessage, 2));
            position = position + 2;

            return position;
        }


        // DATA ITEM I010/170 [Track Status]----------DONE
        public int Track_Status(string[] message, int position)
        {
            this.CNF = message[position].Substring(0, 1);
            if (this.CNF == "0") { this.CNF = "Confirmed track"; }
            if (this.CNF == "1") { this.CNF = "Track in initialisation phase"; }

            this.TRE = message[position].Substring(1, 1);
            if (this.TRE == "0") { this.TRE = "Default"; }
            if (this.TRE == "1") { this.TRE = "Last report for a track"; }

            this.CST = message[position].Substring(2, 2);
            if (this.CST == "00") { this.CST = "No extrapolation"; }
            if (this.CST == "01") { this.CST = "Predictable extrapolation due to sensor refresh period"; }
            if (this.CST == "10") { this.CST = "Predictable extrapolation in masked area"; }
            if (this.CST == "11") { this.CST = "Extrapolation due to unpredictable absence of detection"; }

            this.MAH = message[position].Substring(4, 1);
            if (this.MAH == "0") { this.MAH = "Default"; }
            if (this.MAH == "1") { this.MAH = "Horizontal manoeuvre"; }

            this.TCC = message[position].Substring(5, 1);
            if (this.TCC == "0") { this.TCC = "Tracking performed in 'Sensor Plane', i.e.\r\nneither slant range correction nor projection\r\nwas applied. \r\n"; }
            if (this.TCC == "1") { this.TCC = "Slant range correction and a suitable\r\nprojection technique are used to track in a\r\n2D.reference plane, tangential to the earth\r\nmodel at the Sensor Site co-ordinates."; }

            this.STH = message[position].Substring(6, 1);
            if (this.STH == "0") { this.STH = "Measured position"; }
            if (this.STH == "1") { this.STH = "Smoothed position"; }

            if (message[position].Substring(7, 1) == "1")
            {
                // Extension into first extent
                position = position + 1;
                this.TOM = message[position].Substring(0, 2);
                if (this.TOM == "00") { this.TOM = "Unknown type of movement"; }
                if (this.TOM == "01") { this.TOM = "Taking-off"; }
                if (this.TOM == "10") { this.TOM = "Landing"; }
                if (this.TOM == "11") { this.TOM = "Other types of movement"; }

                this.DOU = message[position].Substring(2, 3);
                if (this.DOU == "000") { this.DOU = "No doubt"; }
                if (this.DOU == "001") { this.DOU = "Doubtful correlation (undeterminated resaon)"; }
                if (this.DOU == "010") { this.DOU = "Doubtful correlation in clutter"; }
                if (this.DOU == "011") { this.DOU = "Loss of accuracy"; }
                if (this.DOU == "100") { this.DOU = "Loss of accuracy in clutter"; }
                if (this.DOU == "101") { this.DOU = "Unstable track"; }
                if (this.DOU == "110") { this.DOU = "Previously coasted"; }

                this.MRS = message[position].Substring(5, 2);
                if (this.MRS == "00") { this.MRS = "Merge or split indication undetermined"; }
                if (this.MRS == "01") { this.MRS = "Track merged by association to plot"; }
                if (this.MRS == "10") { this.MRS = "Track merged by non-association plot"; }
                if (this.MRS == "11") { this.MRS = "Split track"; }


                if (message[position].Substring(7, 1) == "1")
                {
                    // Extension into next extent
                    position = position + 1;
                    this.GHO = message[position].Substring(0, 1);
                    if (this.GHO == "0") { this.GHO = "Default"; }
                    if (this.GHO == "1") { this.GHO = "Ghost track"; }
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


        // DATA ITEM I010/200 [Calculated Track Velocity in Polar Coordiantes]----------DONE
        public int Calculated_Track_Velocity_in_Polar_Coordinates(string[] message, int position)
        {
            this.Ground_Speed = String.Format("{0:0.00}", (Convert.ToInt32(String.Concat(message[position], message[position + 1]), 2)* (Math.Pow(2, -14)) *1852)) + "m/s"; // In m/s
            this.Track_Angle = String.Format("{0:0.00}", Convert.ToInt32(String.Concat(message[position + 2], message[position + 3]), 2) * (360 / Math.Pow(2, 16)))+ "°";
            if (Convert.ToInt32(String.Concat(message[position], message[position + 1]), 2) * (Math.Pow(2, -14)) >= 2)
                this.Ground_Speed = "Max value exceded (> 2NM/s)";
            position = position + 4;

            return position;
        }


        // DATA ITEM I010/202 [Calculated Track Velocity in Cartesian Cooridinates]-----------DONE
        public int Calculated_Track_Velocity_in_Cartesian_Coordiantes(string[] message, int position)
        {
            this.Vx = Convert.ToString(convertor.TWO_Complement(String.Concat(message[position], message[position + 1]))*0.25) + " m/s";
            this.Vy = Convert.ToString(convertor.TWO_Complement(String.Concat(message[position + 2], message[position + 3])) * 0.25) + " m/s";
            position = position + 4;

            return position;
        }


        // DATA ITEM I010/210 [Calculated Acceleration]--------------DONE
        public int Calculated_Acceleration(string[] message, int position)
        {
            this.Ax = Convert.ToString(convertor.TWO_Complement(message[position]) * 0.25) + "m/s^2";
            this.Ay = Convert.ToString(convertor.TWO_Complement(message[position+1]) * 0.25) + "m/s^2";

            if (convertor.TWO_Complement(message[position]) * 0.25 >= 31 || (convertor.TWO_Complement(message[position])) * 0.25 <= -31)
                this.Ax = "Max acceleration value exceeded (+-32 m/s^2)";
            if ((convertor.TWO_Complement(message[position+1])) * 0.25 >= 31 || (convertor.TWO_Complement(message[position+1]) * 0.25 <= -31))
                this.Ay = "Max acceleration value exceeded (+-32 m/s^2)";

            position = position + 2;

            return position;
        }

        
        // DATA ITEM I010/220 [Target Address]-----------DONE
        public int Target_Address(string[] message, int position)
        {
            this.TargetAddress = String.Concat(convertor.Binary_Octet_To_Hexadecimal(message[position]), convertor.Binary_Octet_To_Hexadecimal(message[position + 1]), convertor.Binary_Octet_To_Hexadecimal(message[position + 2]));
            position = position + 3;

            return position;
        }


        // DATA ITEM I010/245 [Target Identification]----------DONE
        public int Target_Identification(string[] message, int position)
        {
            string charecter;
            this.STI = message[position].Substring(0, 2);
            if (this.STI == "00") { this.STI = "Callsign or registration downlinked from transponder"; }
            if (this.STI == "01") { this.STI = "Callsign not downlinked from transponder"; }
            if (this.STI == "10") { this.STI = "Registration not downlinked from transponder"; }

            string fullmessage = String.Concat(message[position + 1], message[position + 2], message[position + 3], message[position + 4], message[position + 5], message[position + 6]);
            this.Target_ID = "";
            int i = 2;
            while (i<fullmessage.Length)
            {
                string reversedStr = fullmessage.Substring(i, 4);
                charecter = fullmessage.Substring(i-2, 4);
                //char[] stringArray = charecter.ToCharArray();
                //Array.Reverse(stringArray);
                //string reversedStr = new string(stringArray);
                if (reversedStr == "0000")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                }
                if (reversedStr == "0001")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "A";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "Q";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "1";
                }
                if (reversedStr == "0010")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "B";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "R";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "2";
                }
                if (reversedStr == "0011")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "C";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "S";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "3";
                }
                if (reversedStr == "0100")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "D";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "T";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "4";
                }
                if (reversedStr == "0101")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "E";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "U";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "5";
                }
                if (reversedStr == "0110")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "F";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "V";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "6";
                }
                if (reversedStr == "0111")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "G";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "W";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "7";
                }
                if (reversedStr == "1000")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "H";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "X";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "8";
                }
                if (reversedStr == "1001")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "I";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "Y";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "9";
                }
                if (reversedStr == "1010")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "J";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "Z";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                }
                if (reversedStr == "1011")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "K";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                }
                if (reversedStr == "1100")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "L";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                }
                if (reversedStr == "1101")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "M";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                }
                if (reversedStr == "1110")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "N";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                }
                if (reversedStr == "1111")
                {
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "O";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "0")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "0" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                    if (charecter.Substring(1, 1) == "1" && charecter.Substring(0, 1) == "1")
                        this.Target_ID = this.Target_ID + "";
                }



                i = i + 6;
            }
            position = position + 7;

            return position;
        }


        // DATA ITEM I010/250 [Mode S MB Data]-------------DONE
        public int Mode_S_MB_Data(string[] message, int position)
        {
            this.REP_ModeS = convertor.Binary_Octet_To_Hexadecimal(message[position]);
            if (Convert.ToInt32(this.REP_ModeS) < 0) 
            { 
                this.MB_Data_ModeS = new string[Convert.ToInt32(this.REP_ModeS)]; 
                this.BDS1_ModeS = new string[Convert.ToInt32(this.REP_ModeS)];
                this.BDS2_ModeS = new string[Convert.ToInt32(this.REP_ModeS)];
            }
            position = position +1;
            int i = 0;

            while (i < Convert.ToInt32(this.REP_ModeS))
            {
                this.MB_Data_ModeS[i] = String.Concat(message[position], message[position + 1], message[position + 2], message[position + 3], message[position + 4], message[position + 5], message[position + 6]);
                this.BDS1_ModeS[1] = message[position + 7].Substring(0, 4);
                this.BDS2_ModeS[1] = message[position + 7].Substring(4, 4);
                position = position + 8;
                i = i + 1;
            }
            
            return position;
        }


        // DATA ITEM I010/270 [Target Size & Orientation]--------------DONE
        public int Target_Size__and_Orientation(string[] message, int position)
        {
            this.Lenght = Convert.ToString(Convert.ToInt32(message[position].Substring(0, 7), 2)) + "m";

            if (message[position].Substring(7, 1) == "1")
            {
                // Extension into first extent
                position = position + 1;
                this.Orientation = Convert.ToString(Convert.ToInt32(message[position].Substring(0, 7),2)*2.81) + "°";
                
                if (message[position].Substring(7, 1) == "1")
                {
                    // Extension into next extent
                    position = position + 1;
                    this.Width = Convert.ToString(Convert.ToInt32(message[position].Substring(0, 7), 2)) + "m";

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


        // DATA ITEM I010/280 [Presence]--------------------DONE
        public int Presence(string[] message, int position)
        {
            this.REP = Convert.ToInt32(convertor.Binary_Octet_To_Hexadecimal(message[position]));
            position = position + 1;

            int i = 0;
            while (i < this.REP)
            {
                this.DRHO[i] = convertor.Binary_Octet_To_Hexadecimal(message[position]) + "m";
                this.DRHO[i] = Convert.ToString(Convert.ToInt32(convertor.Binary_Octet_To_Hexadecimal(message[position]))*0.15)+ "°";

                position = position + 2;
                i ++;
            }

            return position;
        }

        
        // DATA ITEM I010/300 [Vehicle Fleet Identification]------------DONE
        public int Vehicle_Fleet_Identfication(string[] message, int position)
        {
            this.Vehicle_Fleet_ID = convertor.Binary_Octet_To_Hexadecimal(message[position]);
            if (this.Vehicle_Fleet_ID == "0")
                this.Vehicle_Fleet_ID = "Unknown";
            if (this.Vehicle_Fleet_ID == "1")
                this.Vehicle_Fleet_ID = "ATC equipament maintenance";
            if (this.Vehicle_Fleet_ID == "2")
                this.Vehicle_Fleet_ID = "Airport maintenance";
            if (this.Vehicle_Fleet_ID == "3")
                this.Vehicle_Fleet_ID = "Fire";
            if (this.Vehicle_Fleet_ID == "4")
                this.Vehicle_Fleet_ID = "Bird scarer";
            if (this.Vehicle_Fleet_ID == "5")
                this.Vehicle_Fleet_ID = "Snow plough";
            if (this.Vehicle_Fleet_ID == "6")
                this.Vehicle_Fleet_ID = "runway sweeper";
            if (this.Vehicle_Fleet_ID == "7")
                this.Vehicle_Fleet_ID = "Emergency";
            if (this.Vehicle_Fleet_ID == "8")
                this.Vehicle_Fleet_ID = "Police";
            if (this.Vehicle_Fleet_ID == "9")
                this.Vehicle_Fleet_ID = "Bus";
            if (this.Vehicle_Fleet_ID == "10")
                this.Vehicle_Fleet_ID = "Tug (push/tow)";
            if (this.Vehicle_Fleet_ID == "11")
                this.Vehicle_Fleet_ID = "Grass cutter";
            if (this.Vehicle_Fleet_ID == "12")
                this.Vehicle_Fleet_ID = "Fuel";
            if (this.Vehicle_Fleet_ID == "13")
                this.Vehicle_Fleet_ID = "Baggage";
            if (this.Vehicle_Fleet_ID == "14")
                this.Vehicle_Fleet_ID = "Catering";
            if (this.Vehicle_Fleet_ID == "15")
                this.Vehicle_Fleet_ID = "Aircraft maintenance";
            if (this.Vehicle_Fleet_ID == "16")
                this.Vehicle_Fleet_ID = "Flyco (follow me)";

            position = position + 1;


            return position;
        }


        // DATA ITEM I010/310 [Pre-programed Message]------------DONE
        public int Preporgramed_Message(string[] message, int position)
        {
            this.TRB = message[position].Substring(0, 1);
            if (this.TYP == "0") { this.TYP = "Default"; }
            if (this.TYP == "1") { this.TYP = "In Trouble"; }


            this.MSG = convertor.Binary_Octet_To_Hexadecimal(message[position].Substring(1, 7));
            if (this.Vehicle_Fleet_ID == "1")
                this.Vehicle_Fleet_ID = "Towing aircraft";
            if (this.Vehicle_Fleet_ID == "2")
                this.Vehicle_Fleet_ID = "''Follow me'' operation";
            if (this.Vehicle_Fleet_ID == "3")
                this.Vehicle_Fleet_ID = "Runway check";
            if (this.Vehicle_Fleet_ID == "4")
                this.Vehicle_Fleet_ID = "Emergency operation (fire, medical…) ";
            if (this.Vehicle_Fleet_ID == "5")
                this.Vehicle_Fleet_ID = "Work in progress (maintenance, birds scarer, sweepers…)";

            position = position + 1;

            return position;
        }


        // DATA ITEM I010/500 [Standard Deviation of Position]---------------DONE
        public int Standard_Deviation_of_Position(string[] message, int position)
        {
            this.omega_x = Convert.ToString(Convert.ToInt32(convertor.Binary_Octet_To_Hexadecimal(message[position])) * 0.25) +"m";
            this.omega_y = Convert.ToString(Convert.ToInt32(convertor.Binary_Octet_To_Hexadecimal(message[position + 1])) * 0.25) +"m";
            this.omega_xy = Convert.ToString(Convert.ToInt32(convertor.Twos_Complement(String.Concat(message[position + 2], message[position + 3]))) * 0.25) +"m^2";
            position = position + 4;

            return position;
        }


        // DATA ITEM I010/550 [System Status]---------------DONE
        public int System_Status(string[] message, int position)
        {
            this.NOGO = message[position].Substring(0, 2);
            if (this.NOGO == "00") { this.NOGO = "Operational"; }
            if (this.NOGO == "01") { this.NOGO = "Degraded"; }
            if (this.NOGO == "10") { this.NOGO = "NOGO"; }

            this.OVL = message[position].Substring(2, 1);
            if (this.OVL == "0") { this.OVL = "No overload"; }
            if (this.OVL == "1") { this.OVL = "Overload"; }

            this.TSV = message[position].Substring(3, 1);
            if (this.TSV == "0") { this.TSV = "Valid"; }
            if (this.TSV == "1") { this.TSV = "Invalid"; }

            this.DIV = message[position].Substring(4, 1);
            if (this.DIV == "0") { this.DIV = "Normal Operation"; }
            if (this.DIV == "1") { this.DIV = "Diversity degraded"; }

            this.TTF = message[position].Substring(5, 1);
            if (this.TTF == "0") { this.TTF = "Test Target Operative"; }
            if (this.TTF == "1") { this.TTF = "Test Target Failure"; }

            position = position + 1;

            return position;
        }


    }
}
