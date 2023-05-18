using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Markup;
using static iTextSharp.text.pdf.qrcode.Version;

namespace Project_1
{

    public class CAT21
    {
        //In this class we are going to decode all the category 21 messages

        Conversions convertor = new Conversions();
        public string[] message;
        public string Full_FSPEC;
        public string CAT = "21";
        public int num;
        public int cat21num;
        public int airportCode;

        public string RA_Status;               // DATA ITEM I021/008
        public string TC_Status;               // DATA ITEM I021/008
        public string TS_Status;               // DATA ITEM I021/008
        public string ARV_Status;              // DATA ITEM I021/008
        public string CDTI_A_Status;           // DATA ITEM I021/008
        public string not_TCAS_Status;         // DATA ITEM I021/008
        public string SA_Status;               // DATA ITEM I021/008
        public string SAC;                     // DATA ITEM I021/010
        public string SIC;                     // DATA ITEM I021/010
        public string Service_identification;  // DATA ITEM I010/010
        public string RP;                      // DATA ITEM I010/016
        public string ECAT;                    // DATA ITEM I010/020
        public string ATP;                     // DATA ITEM I021/040
        public string ARC;                     // DATA ITEM I021/040
        public string RC;                      // DATA ITEM I021/040
        public string RAB;                     // DATA ITEM I021/040
        public string DCR;                     // DATA ITEM I021/040
        public string GBS;                     // DATA ITEM I021/040
        public string SIM;                     // DATA ITEM I021/040
        public string TST;                     // DATA ITEM I021/040
        public string SAA;                     // DATA ITEM I021/040
        public string CL;                      // DATA ITEM I021/040
        public string IPC;                     // Data Item I021/040
        public string NOGO;                    // Data Item I021/040
        public string CRP;                     // Data Item I021/040
        public string LDPJ;                    // Data Item I021/040
        public string RCF;                     // Data Item I021/040
        public string M3AC;                    // DATA ITEM I010/070
        public string ToAfP;                   // DATA ITEM I010/071
        public string ToAfV;                   // DATA ITEM I010/072
        public string ToMRfP;                  // DATA ITEM I010/073
        public string ToMRfP_HI_RES;           // DATA ITEM I010/074
        public string ToMRfV;                  // DATA ITEM I010/075
        public string ToMRfV_HI_RES;           // DATA ITEM I010/076
        public string Tort;                    // DATA ITEM I010/077
        public string TargetAddress;           // DATA ITEM I021/080
        public string QualityIndicators;       // DATA ITEM I021/090
        public string ICB;                     // DATA ITEM I021/090
        public string NUCp;                    // DATA ITEM I021/090
        public string NIC;                     // DATA ITEM I021/090
        public string NUCr_NACv;               // DATA ITEM I021/090
        public string NUCp_NIC;                // DATA ITEM I021/090
        public string NICbaro;                 // DATA ITEM I021/090
        public string SIL;                     // DATA ITEM I021/090
        public string NACp;                    // DATA ITEM I021/090
        public string SIL2;                    // DATA ITEM I021/090
        public string SDA;                     // DATA ITEM I021/090
        public string GVA;                     // DATA ITEM I021/090
        public string PIC;                     // DATA ITEM I021/090
        public int REP;                        // DATA ITEM I010/110
        public string TIS;                     // DATA ITEM I010/110
        public string TID;                     // DATA ITEM I010/110
        public string NAV;                     // DATA ITEM I010/110
        public string NVB;                     // DATA ITEM I010/110
        public int REP1;                       // DATA ITEM I010/110
        public string TrajectoryIntent;        // DATA ITEM I021/110
        public string[] TCA;                   // DATA ITEM I021/110
        public string[] NC;                    // DATA ITEM I021/110
        public int[] TCPNumber;                // DATA ITEM I021/110
        public string[] AltitudeTID;           // DATA ITEM I021/110
        public string[] LATITUDE;              // DATA ITEM I021/110
        public string[] LONGITUDE;             // DATA ITEM I021/110
        public string[] Point_Type;            // DATA ITEM I021/110
        public string[] TD;                    // DATA ITEM I021/110
        public string[] TRA;                   // DATA ITEM I021/110
        public string[] TOA;                   // DATA ITEM I021/110
        public string[] TOV;                   // DATA ITEM I021/110
        public string[] TTR;                   // DATA ITEM I021/110
        public string[] PT;                    // DATA ITEM I021/110
        public string Latitude_WGS84;          // DATA ITEM I010/130
        public string Longitude_WGS84;         // DATA ITEM I010/130
        public string Latitude_WGS84_HI_RES;   // DATA ITEM I010/131
        public string Longitude_WGS84_HI_RES;  // DATA ITEM I010/131
        public string MAM;                     // DATA ITEM I010/132
        public string GeometricHeight;         // DATA ITEM I010/140
        public string FlightLevel;             // DATA ITEM I010/145
        public string SAS;                     // DATA ITEM I010/146
        public string Source;                  // DATA ITEM I010/146
        public string Altitude;                // DATA ITEM I010/146
        public string MV;                      // DATA ITEM I010/148
        public string AH;                      // DATA ITEM I010/148
        public string AM;                      // DATA ITEM I010/148
        public string AltitudeF;               // DATA ITEM I010/148
        public string IM;                      // DATA ITEM I010/150
        public string RE;                      // DATA ITEM I010/151
        public string MagneticHeading;         // DATA ITEM I010/152
        public string BarometricVerticalRate;  // DATA ITEM I010/155
        public string GeometricVerticalRate;   // DATA ITEM I010/157
        public string Ground_Speed;            // DATA ITEM I010/160
        public string Track_Angle;             // DATA ITEM I010/160
        public string Track_number;            // DATA ITEM I010/161
        public string TAR;                     // DATA ITEM I010/165
        public string Target_ID;               // DATA ITEM I010/170
        public string ICF;                     // DATA ITEM I010/200
        public string LNAV;                    // DATA ITEM I010/200
        public string PS;                      // DATA ITEM I010/200
        public string SS;                      // DATA ITEM I010/200
        public string VNS;                     // DATA ITEM I010/210
        public string VN;                      // DATA ITEM I010/210
        public string LTT;                     // DATA ITEM I010/210
        public string WS;                      // DATA ITEM I010/220
        public string WD;                      // DATA ITEM I010/220
        public string TMP;                     // DATA ITEM I010/220
        public string TRB;                     // DATA ITEM I010/220
        public string RollAngle;               // Dara Item I021/230
        public string ModeSMBData;             // Data Item I021/250 
        public string[] MB_Data_ModeS;         // Data Item I021/250 
        public string[] BDS1_ModeS;            // Data Item I021/250 
        public string[] BDS2_ModeS;            // Data Item I021/250 
        public string ACAS;                    // Data Item I021/260 
        public string TYP;                     // Data Item I021/260 
        public string STYP;                    // Data Item I021/260 
        public string ARA;                     // Data Item I021/260 
        public string RAC;                     // Data Item I021/260 
        public string RAT;                     // Data Item I021/260 
        public string MTE;                     // Data Item I021/260 
        public string TTI;                     // Data Item I021/260 
        public string TID_acas;                // Data Item I021/260 
        public string POA;                     // Data Item I021/271 
        public string CDTIS;                   // Data Item I021/271 
        public string B2low;                   // Data Item I021/271 
        public string RAS;                     // Data Item I021/271 
        public string IDENT;                   // Data Item I021/271 
        public string LW;                      // Data Item I021/271
        public string FL;                      // DATA ITEM I010/145
        public string AOS;                     // DATA ITEM I021/295
        public string TRD;                     // DATA ITEM I021/295
        public string M3A;                     // DATA ITEM I021/295
        public string QI;                      // DATA ITEM I021/295
        public string TI;                      // DATA ITEM I021/295
        public string MAM_ages;                // DATA ITEM I021/295
        public string GH;                      // DATA ITEM I021/295
        public string FL_ages;                 // DATA ITEM I021/295
        public string ISA;                     // DATA ITEM I021/295
        public string FSA;                     // DATA ITEM I021/295
        public string AS;                      // DATA ITEM I021/295
        public string TAS;                     // DATA ITEM I021/295
        public string MH;                      // DATA ITEM I021/295
        public string BVR;                     // DATA ITEM I021/295
        public string GVR;                     // DATA ITEM I021/295
        public string GV;                      // DATA ITEM I021/295
        public string TAR_ages;                // DATA ITEM I021/295
        public string TI_ages;                 // DATA ITEM I021/295
        public string TS_ages;                 // DATA ITEM I021/295
        public string MET;                     // DATA ITEM I021/295
        public string ROA;                     // DATA ITEM I021/295
        public string ARA_ages;                // DATA ITEM I021/295
        public string SCC;                     // DATA ITEM I021/295
        public string RID;                     // DATA ITEM I021/400




        public CAT21(string[] message_in_hexa, Conversions convertor)
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
                if (a >= 3 && i == true)
                {
                    if (message[a - 1].EndsWith("1"))
                    {
                        Full_FSPEC = Full_FSPEC + message[a].Remove(message[a].Length - 1);
                    }
                }

                if (a == 3 && i == true)
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
                position = this.Target_Report_Descriptor(message, position);
            if (FSPEC[2] == '1')
                position = this.Track_Number(message, position);
            if (FSPEC[3] == '1')
                position = this.Service_Identification(message, position);
            if (FSPEC[4] == '1')
                position = this.Time_of_Aplicability_for_Position(message, position);
            if (FSPEC[5] == '1')
                position = this.Position_in_WGS84_Coordinates(message, position);
            if (FSPEC[6] == '1')
                position = this.Position_in_WGS84_Coordinates_Highres(message, position);

            if (FSPEC.Count() > 8)
            {
                if (FSPEC[7] == '1')
                    position = this.Time_of_Aplicability_for_Velocity(message, position);
                if (FSPEC[8] == '1')
                    position = this.Air_Speed(message, position);
                if (FSPEC[9] == '1')
                    position = this.True_Air_Speed(message, position);
                if (FSPEC[10] == '1')
                    position = this.Target_Address(message, position);
                if (FSPEC[11] == '1')
                    position = this.Time_of_Message_Reception_of_Position(message, position);
                if (FSPEC[12] == '1')
                    position = this.Time_of_Message_Reception_of_Position_HighPrecision(message, position);
                if (FSPEC[13] == '1')
                    position = this.Time_of_Message_Reception_of_Velocity(message, position);
            }

            if (FSPEC.Count() > 16)
            {
                if (FSPEC[14] == '1')
                    position = this.Time_of_Message_Reception_of_Velocity_HighPrecision(message, position);
                if (FSPEC[15] == '1')
                    position = this.Geometric_Height(message, position);
                if (FSPEC[16] == '1')
                    position = this.Quality_Indicators(message, position);
                if (FSPEC[17] == '1')
                    position = this.MOPS_Version(message, position);
                if (FSPEC[18] == '1')
                    position = this.Mode_3A_Code(message, position);
                if (FSPEC[19] == '1')
                    position = this.Roll_Angle(message, position);
                if (FSPEC[20] == '1')
                    position = this.Flight_Level(message, position);
            }

            if (FSPEC.Count() > 22)
            {
                if (FSPEC[21] == '1')
                    position = this.Magnetic_Heading(message, position);
                if (FSPEC[22] == '1')
                    position = this.Target_Status(message, position);
                if (FSPEC[23] == '1')
                    position = this.Barometric_Vertical_Rate(message, position);
                if (FSPEC[24] == '1')
                    position = this.Geometric_Vertical_Rate(message, position);
                if (FSPEC[25] == '1')
                    position = this.Airborne_Ground_Vector(message, position);
                if (FSPEC[26] == '1')
                    position = this.Track_Angle_Rate(message, position);
                if (FSPEC[27] == '1')
                    position = this.Time_of_Report_Transmission(message, position);
            }
            if (FSPEC.Count() > 29)
            {
                if (FSPEC[28] == '1')
                    position = this.Target_Identification(message, position);
                if (FSPEC[29] == '1')
                    position = this.Emitter_Category(message, position);
                if (FSPEC[30] == '1')
                    position = this.Met_Information(message, position);
                if (FSPEC[31] == '1')
                    position = this.Selected_Altitude(message, position);
                if (FSPEC[32] == '1')
                    position = this.Final_State_Selected_Altitude(message, position);
                if (FSPEC[33] == '1')
                    position = this.Trajectory_Intent(message, position);
                if (FSPEC[34] == '1')
                    position = this.Service_Managment(message, position);
            }
            if (FSPEC.Count() > 36)
            {
                if (FSPEC[35] == '1')
                    position = this.Aircraft_operational_Status(message, position);
                if (FSPEC[36] == '1')
                    position = this.Surface_Capabilities_and_Characteristics(message, position);
                if (FSPEC[37] == '1')
                    position = this.Message_Amplitude(message, position);
                if (FSPEC[38] == '1')
                    position = this.ModeS_MB_Data(message, position);
                if (FSPEC[39] == '1')
                    position = this.ACAS_Resolution_Advisory_Report(message, position);
                if (FSPEC[40] == '1')
                    position = this.Reciever_ID(message, position);
                if (FSPEC[41] == '1')
                    position = this.Data_Ages(message, position);
            }
            if (FSPEC.Count() > 43)
            {
                //if (FSPEC[47] == '1')
                //    position = this.Reserved_expantion_Field(message, position);
                //if (FSPEC[48] == '1')
                //    position = this.Spetial_Purpose_Field(message, position);
            }
        }
        

        // Data Item I021/010 [Data Source Identifier]
        public int Data_Source_Identifier(string[] message, int position)
        {
            this.SAC = Convert.ToString(Convert.ToInt32(message[position], 2));
            this.SIC = Convert.ToString(Convert.ToInt32(message[position + 1], 2));
            position = position + 2;

            return position;
        }



        // Data Item I021/040 [Target Report Descriptor]
        public int Target_Report_Descriptor(string[] message, int position)
        {
            this.ATP = message[position].Substring(0, 3);
            if (this.ATP == "000") { this.ATP = "24-Bit ICAO address"; }
            if (this.ATP == "001") { this.ATP = "Duplicate address"; }
            if (this.ATP == "010") { this.ATP = "Surface vehicle address"; }
            if (this.ATP == "011") { this.ATP = "Anonymous address"; }
            if (this.ATP == "100") { this.ATP = "Reserved"; }
            if (this.ATP == "101") { this.ATP = "Reserved"; }
            if (this.ATP == "110") { this.ATP = "Reserved"; }
            if (this.ATP == "111") { this.ATP = "Reserved"; }

            this.ARC = message[position].Substring(3, 2);
            if (this.ARC == "00") { this.ARC = "25 ft"; }
            if (this.ARC == "01") { this.ARC = "100 ft"; }
            if (this.ARC == "10") { this.ARC = "Unknown"; }
            if (this.ARC == "11") { this.ARC = "Invalid"; }

            this.RC = message[position].Substring(5, 1);
            if (this.RC == "0") { this.RC = "Default"; }
            if (this.RC == "1") { this.RC = "Range Check passed, CPR Validation pending"; }

            this.RAB = message[position].Substring(6, 1);
            if (this.RAB == "0") { this.RAB = "Report from target transponder"; }
            if (this.RAB == "1") { this.RAB = "Report from field monitor (fixed transponder)"; }


            if (message[position].Substring(7, 1) == "1") 
            { 
                position++;

                this.DCR = message[position].Substring(0, 1);
                if (this.DCR == "0") { this.DCR = "No differential correction (ADS-B)"; }
                if (this.DCR == "1") { this.DCR = "Differential correction (ADS-B)"; }

                this.GBS = message[position].Substring(1, 1);
                if (this.GBS == "0") { this.GBS = "Ground Bit not set"; }
                if (this.GBS == "1") { this.GBS = "Ground Bit set"; }

                this.SIM = message[position].Substring(2, 1);
                if (this.SIM == "0") { this.SIM = "Actual target report"; }
                if (this.SIM == "1") { this.SIM = "Simulated target report"; }

                this.TST = message[position].Substring(3, 1);
                if (this.TST == "0") { this.TST = "Default"; }
                if (this.TST == "1") { this.TST = "Test Target"; }

                this.SAA = message[position].Substring(4, 1);
                if (this.SAA == "0") { this.SAA = "Equipment capable to provide Selected Altitude"; }
                if (this.SAA == "1") { this.SAA = "Equipment not capable to provide Selected Altitude"; }

                this.CL = message[position].Substring(5, 2);
                if (this.CL == "00") { this.CL = "Report valid"; }
                if (this.CL == "01") { this.CL = "Report suspect"; }
                if (this.CL == "10") { this.CL = "No information"; }
                if (this.CL == "11") { this.CL = "Reserved for future use"; }


                if (message[position].Substring(7, 1) == "1")
                {
                    // Extension into next extent
                    position = position + 1;
                    this.IPC = message[position].Substring(0, 1);
                    if (this.IPC == "0") { this.IPC = "Default"; }
                    if (this.IPC == "1") { this.IPC = "Failed"; }

                    this.NOGO = message[position].Substring(1, 1);
                    if (this.NOGO == "0") { this.NOGO = "Bit not set "; }
                    if (this.NOGO == "1") { this.NOGO = "Bit set"; }

                    this.CRP = message[position].Substring(0, 1);
                    if (this.CRP == "0") { this.CRP = "Validation correct"; }
                    if (this.CRP == "1") { this.CRP = "Validation failed"; }

                    this.LDPJ = message[position].Substring(1, 1);
                    if (this.LDPJ == "0") { this.LDPJ = "Not detected"; }
                    if (this.LDPJ == "1") { this.LDPJ = "Detected"; }

                    this.RCF = message[position].Substring(0, 1);
                    if (this.RCF == "0") { this.RCF = "Default"; }
                    if (this.RCF == "1") { this.RCF = "Range Check failed "; }
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
                position++;
            }
            return position;
        }



        // Data Item I021/161 [Track Number]
        public int Track_Number(string[] message, int position)
        {
            
            string fullmessage = String.Concat(message[position], message[position + 1]);
            this.Track_number = Convert.ToString(Convert.ToInt32(fullmessage,2));
            position = position + 2;

            return position;
        }



        // DATA ITEM I021/015 [Service Identification]
        public int Service_Identification(string[] message, int position)
        {
            this.Service_identification = Convert.ToString(Convert.ToInt32(message[position],2));
            position= position + 1;

            return position;
        }



        // DATA ITEM I021/071 [Time of Aplicability for Position]
        public int Time_of_Aplicability_for_Position(string[] message, int position)
        {
            int binaryValue = Convert.ToInt32(string.Concat(message[position], message[position + 1], message[position + 2]), 2);
            double seconds = binaryValue / 128.0;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            this.ToAfP = time.ToString(@"hh\:mm\:ss\:fff");
            position = position + 3;

            return position;
        }



        // DATA ITEM I021/130 [Position on WGS84 Coordinates]
        public int Position_in_WGS84_Coordinates(string[] message, int position)
        {
            double Latitude = convertor.TWO_Complement(string.Concat(message[position], message[position + 1], message[position + 2])) * (180.0 / Math.Pow(2, 23));
            position += 3;
            double Longitude = convertor.TWO_Complement(string.Concat(message[position], message[position + 1], message[position + 2])) * (180.0 / Math.Pow(2, 23));
            position += 3;
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



        // DATA ITEM I021/131 [Position in WGS84 Coordinates High Resoultion]
        public int Position_in_WGS84_Coordinates_Highres(string[] message, int position)
        {
            double Latitude = convertor.TWO_Complement(string.Concat(message[position], message[position + 1], message[position + 2], message[position + 3])) * (180.0 / Math.Pow(2, 30));
            position += 4;
            double Longitude = convertor.TWO_Complement(string.Concat(message[position], message[position + 1], message[position + 2], message[position + 3])) * (180.0 / Math.Pow(2, 30));
            position += 4;
            int Latdegres = (int)Math.Truncate(Latitude);
            double Latmin = (Latitude - Latdegres) * 60.0;
            double Latsec = Math.Round((Latmin - Math.Truncate(Latmin)) * 60.0, 5);
            Latmin = Math.Truncate(Latmin);
            int Londegres = (int)Math.Truncate(Longitude);
            double Lonmin = (Longitude - Londegres) * 60.0;
            double Lonsec = Math.Round((Lonmin - Math.Truncate(Lonmin)) * 60.0, 5);
            Lonmin = Math.Truncate(Lonmin);
            this.Latitude_WGS84_HI_RES = $"{Latdegres}º {Latmin}' {Latsec}''";
            this.Longitude_WGS84_HI_RES = $"{Londegres}º {Lonmin}' {Lonsec}''";

            return position;
        }



        // DATA ITEM I021/072 [Time of Aplicability fro Velocity]
        public int Time_of_Aplicability_for_Velocity(string[] message, int position)
        {
            int binaryValue = Convert.ToInt32(string.Concat(message[position], message[position + 1], message[position + 2]), 2);
            double seconds = binaryValue / 128.0;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            this.ToAfV = time.ToString(@"hh\:mm\:ss\:fff");
            position = position + 3;

            return position;
        }



        // DATA ITEM I021/150 [Air Speed]
        public int Air_Speed(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position] + 1);
            this.IM = fullmessage.Substring(0,1);
            if (this.IM == "0")
            {
                this.IM = "IAS: " + Convert.ToString(Convert.ToInt32((fullmessage.Substring(1,15)) ) * 2E-14 + "NM/s");
            }
            if (this.IM == "1")
            {
                this.IM = "Mach Number: " + Convert.ToString(Convert.ToInt32(fullmessage.Substring(1, 15),2) * 0.001);
            }
            position = position + 2;

            return position;
        }



        // DATA ITEM I021/151 [True Air Speed]
        public int True_Air_Speed(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position] + 1);
            this.RE = fullmessage.Substring(0, 1);
            if (this.RE == "0")
            {
                this.RE = "Value in defined range: " + Convert.ToString(Convert.ToInt32((fullmessage.Substring(1, 15)),2) + "Knots");
            }
            if (this.RE == "1")
            {
                this.RE = "Value exceeds defined range";
            }
            position = position + 2;

            return position;
        }



        // DATA ITEM I021/080 [Target Adress]
        public int Target_Address(string[] message, int position)
        {
            this.TargetAddress = String.Concat(convertor.Binary_Octet_To_Hexadecimal(message[position]), convertor.Binary_Octet_To_Hexadecimal(message[position + 1]), convertor.Binary_Octet_To_Hexadecimal(message[position + 2]));
            position = position + 3;

            return position;
        }


        // DATA ITEM I021/073 [Time of Message Reception of Position]
        public int Time_of_Message_Reception_of_Position(string[] message, int position)
        {
            int binaryValue = Convert.ToInt32(string.Concat(message[position], message[position + 1], message[position + 2]), 2);
            double seconds = binaryValue / 128.0;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            this.ToMRfP = time.ToString(@"hh\:mm\:ss\:fff");
            position = position + 3;

            return position;
        }



        // DATA ITEM I021/074 [Time of Message Reception of Position High Precision]
        public int Time_of_Message_Reception_of_Position_HighPrecision(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position + 1], message[position + 2], message[position + 3]);
            string FSI = fullmessage.Substring(0, 2);
            string time = fullmessage.Substring(2, 30);
            int str = Convert.ToInt32(time, 2);
            double seconds = (Convert.ToDouble(str)) * Math.Pow(2, -30);
            if (FSI == "10")
            {
                seconds = seconds - 1;
            }
            if (FSI == "01")
            {
                seconds = seconds + 1;
            }
            TimeSpan time2 = TimeSpan.FromSeconds(seconds);
            this.ToMRfP_HI_RES = time2.ToString(@"hh\:mm\:ss\:fff");
            position = position +4;

            return position;
        }



        // DATA ITEM I021/075 [Time of Message reception of Velocity]
        public int Time_of_Message_Reception_of_Velocity(string[] message, int position)
        {
            int binaryValue = Convert.ToInt32(string.Concat(message[position], message[position + 1], message[position + 2]), 2);
            double seconds = binaryValue / 128.0;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            this.ToMRfV = time.ToString(@"hh\:mm\:ss\:fff");
            position = position + 3;
            return position;
        }



        // DATA ITEM I021/076 [Time of Message reception of Velocity High Precission]
        public int Time_of_Message_Reception_of_Velocity_HighPrecision(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position + 1], message[position + 2], message[position + 3]);
            string FSI = fullmessage.Substring(0, 2);
            string time = fullmessage.Substring(2, 30);
            int str = Convert.ToInt32(time, 2);
            double seconds = (Convert.ToDouble(str)) * Math.Pow(2, -30);
            if (FSI == "10")
            {
                seconds = seconds - 1;
            }
            if (FSI == "01")
            {
                seconds = seconds + 1;
            }
            TimeSpan time2 = TimeSpan.FromSeconds(seconds);
            this.ToMRfV_HI_RES = time2.ToString(@"hh\:mm\:ss\:fff");
            position = position + 4;

            return position;
        }



        // DATA ITEM I021/140 [Geometric Height]
        public int Geometric_Height(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1]);
            this.GeometricHeight = Convert.ToString(Convert.ToInt32(fullmessage,2) * 6.25 + " ft");
            position = position + 2;

            return position;
        }



        // DATA ITEM I021/140 [Quality Indicators]
        public int Quality_Indicators(string[] message, int position)
        {
            NUCr_NACv = Convert.ToString(Convert.ToInt32(message[position].Substring(0, 3), 2));
            NUCp_NIC = Convert.ToString(Convert.ToInt32(message[position].Substring(3, 4), 2));
            if (message[position].Substring(7, 1) == "1")
            {
                position = position + 1;
                NICbaro = Convert.ToString(Convert.ToInt32(message[position].Substring(0, 1), 2));
                SIL = Convert.ToString(Convert.ToInt32(message[position].Substring(1, 2), 2));
                NACp = Convert.ToString(Convert.ToInt32(message[position].Substring(3, 4), 2));

                if (message[position].Substring(7, 1) == "1")
                {
                    position = position + 1;
                    if (message[position].Substring(2, 1) == "0") { SIL2 = "Measured per flight-Hour"; }
                    else { SIL2 = "Measured per sample"; }
                    SDA = Convert.ToString(Convert.ToInt32(message[position].Substring(3, 2), 2));
                    GVA = Convert.ToString(Convert.ToInt32(message[position].Substring(5, 2), 2));

                    if (message[position].Substring(7, 1) == "1")
                    {
                        position = position + 1;
                        PIC = Convert.ToString(Convert.ToInt32(message[position].Substring(0, 4), 2));
                        if (PIC == "0") { ICB = "No integrity(or > 20.0 NM)"; NUCp = "0"; NIC = "0"; }
                        if (PIC == "1") { ICB = "< 20.0 NM"; NUCp = "1"; NIC = "1"; }
                        if (PIC == "2") { ICB = "< 10.0 NM"; NUCp = "2"; NIC = "-"; }
                        if (PIC == "3") { ICB = "< 8.0 NM"; NUCp = "-"; NIC = "2"; }
                        if (PIC == "4") { ICB = "< 4.0 NM"; NUCp = "-"; NIC = "3"; }
                        if (PIC == "5") { ICB = "< 2.0 NM"; NUCp = "3"; NIC = "4"; }
                        if (PIC == "6") { ICB = "< 1.0 NM"; NUCp = "4"; NIC = "5"; }
                        if (PIC == "7") { ICB = "< 0.6 NM"; NUCp = "-"; NIC = "6 (+ 1/1)"; }
                        if (PIC == "8") { ICB = "< 0.5 NM"; NUCp = "5"; NIC = "6 (+ 0/0)"; }
                        if (PIC == "9") { ICB = "< 0.3 NM"; NUCp = "-"; NIC = "6 (+ 0/1)"; }
                        if (PIC == "10") { ICB = "< 0.2 NM"; NUCp = "6"; NIC = "7"; }
                        if (PIC == "11") { ICB = "< 0.1 NM"; NUCp = "7"; NIC = "8"; }
                        if (PIC == "12") { ICB = "< 0.04 NM"; NUCp = ""; NIC = "9"; }
                        if (PIC == "13") { ICB = "< 0.013 NM"; NUCp = "8"; NIC = "10"; }
                        if (PIC == "14") { ICB = "< 0.004 NM"; NUCp = "9"; NIC = "11"; }
                        position = position + 1;
                    }
                    else
                    {
                        position = position + 1;
                    }
                }
                else
                {
                    position = position + 1;
                }
            }
            else
            {
                position = position + 1;
            }
            return position;
        }



        // DATA ITEM I021/090 [MOPS Version]
        public int MOPS_Version(string[] message, int position)
        {
            this.VNS = message[position].Substring(1,1);
            if (this.VNS == "0") { this.VNS = "The MOPS Version is supported by the GS"; }
            if (this.VNS == "1") { this.VNS = "The MOPS Version is not supported by the GS"; }

            this.VN = message[position].Substring(2,3);
            if (this.VN == "000") { this.VN = "ED102/DO-260 [Ref. 8]"; }
            if (this.VN == "001") { this.VN = "DO-260A [Ref. 9]"; }
            if (this.VN == "010") { this.VN = "ED102A/DO-260B [Ref. 11]"; }

            this.LTT = message[position].Substring(3,3);
            if (this.LTT == "000") { this.LTT = "Other"; }
            if (this.LTT == "001") { this.LTT = "UAT"; }
            if (this.LTT == "010") { this.LTT = "1090 ES"; }
            if (this.LTT == "011") { this.LTT = "VDL 4"; }
            if (this.LTT == "100") { this.LTT = "Not assigned"; }
            if (this.LTT == "101") { this.LTT = "Not assigned"; }
            if (this.LTT == "110") { this.LTT = "Not assigned"; }
            if (this.LTT == "111") { this.LTT = "Not assigned"; }

            position++;

            return position;
        }



        // DATA ITEM I021/070 [Mode 3A Code in Octal Representation]
        public int Mode_3A_Code(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1]);
            this.M3AC = Convert.ToString(Convert.ToInt32(fullmessage.Substring(4, 12), 2), 8).PadLeft(4, '0');            
            position = position + 2;

            return position;
        }



        // DATA ITEM I021/230 [Roll Angle]
        public int Roll_Angle(string[] message, int position)
        {
            this.RollAngle = Convert.ToString(convertor.TWO_Complement(string.Concat(message[position], message[position + 1])) * 0.01) + "º";
            position = position + 2;

            return position;
        }



        // DATA ITEM I021/145 [Flight Level]
        public int Flight_Level(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1]);
            this.FlightLevel = Convert.ToString(convertor.TWO_Complement(fullmessage) * (0.25));
            position = position + 2;

            return position;
        }



        // DATA ITEM I021/152 [Magnetic Heading]
        public int Magnetic_Heading(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1]);
            this.MagneticHeading = Convert.ToString(Convert.ToInt32((fullmessage),2) * (360 / (Math.Pow(2, 16))) + "º");
            position = position + 2;

            return position;
        }



        // DATA ITEM I021/200 [Target Status]
        public int Target_Status(string[] message, int position)
        {
            this.ICF = message[position].Substring(0, 1);
            if (this.ICF == "0") {this.ICF = "No intent change active"; }
            if (this.ICF == "1") { this.ICF = "Intent change flag raised"; }

            this.LNAV = message[position].Substring(1, 1);
            if (this.LNAV == "0") { this.LNAV= "LNAV Mode engaged"; }
            if (this.LNAV == "1") { this.LNAV = "LNAV Mode not engaged"; }

            this.PS = message[position].Substring(3, 3);
            if (this.PS == "000") { this.PS = "No emergency / not reported"; }
            if (this.PS == "001") { this.PS = "General emergency"; }
            if (this.PS == "010") { this.PS = "Lifeguard / medical emergency"; }
            if (this.PS == "011") { this.PS = "Minimum fuel"; }
            if (this.PS == "100") { this.PS = "No communications"; }
            if (this.PS == "101") { this.PS = "Unlawful interference"; }
            if (this.PS == "110") { this.PS = "“Downed” Aircraft"; }

            this.SS = message[position].Substring(6, 2);
            if (this.SS == "00") { this.SS = "No condition reported"; }
            if (this.SS == "01") { this.SS = "Permanent Alert (Emergency condition)"; }
            if (this.SS == "10") { this.SS = "Temporary Alert (change in Mode 3/A Code other than emergency)"; }
            if (this.SS == "11") { this.SS = "SPI set"; }

            position++;

            return position;
        }



        // DATA ITEM I021/155 [Barometric Vertical Rate]
        public int Barometric_Vertical_Rate(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position+1]);
            this.BarometricVerticalRate = fullmessage.Substring(0, 1);
            if (this.BarometricVerticalRate == "0")
            {
                this.BarometricVerticalRate = Convert.ToString(convertor.TWO_Complement(fullmessage.Substring(1, 15)) * 6.25) + "ft/min";
            }
            if (this.BarometricVerticalRate == "1")
            {
                this.BarometricVerticalRate = "Value exceeds defined range";
            }
            position = position + 2;

            return position;
        }



        // DATA ITEM I021/155 [Geometric Vertical Rate]
        public int Geometric_Vertical_Rate(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position + 1]);
            this.GeometricVerticalRate = fullmessage.Substring(0, 1);
            if (this.GeometricVerticalRate == "0")
            {
                this.GeometricVerticalRate = Convert.ToString(convertor.TWO_Complement(fullmessage.Substring(1, 15)) * 6.25) + "ft/min";
            }
            if (this.GeometricVerticalRate == "1")
            {
                this.GeometricVerticalRate = "Value exceeds defined range";
            }
            position = position + 2;

            return position;
        }



        // DATA ITEM I021/160 [Airbone Ground Vector]
        public int Airborne_Ground_Vector(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position + 1]);
            string fullmessage2 = string.Concat(message[position + 2], message[position + 3]);
            this.RE = fullmessage.Substring(0, 1);
            if (this.RE == "0")
            {
                double Ground_Speed_with_no_format = Convert.ToInt32(fullmessage.Substring(1, 15),2) * (Math.Pow(2, -14) * 3600);
                double Track_Angle_with_no_format = Convert.ToInt32(fullmessage2.Substring(0, 16),2) * (360 / (Math.Pow(2, 16)));
                this.Ground_Speed = String.Format("{0:0.00}", Ground_Speed_with_no_format) + " kt";
                this.Track_Angle = String.Format("{0:0.00}", Track_Angle_with_no_format) + "º";
               

            }
            if (this.RE == "1")
            {
                this.Ground_Speed = "Value exceeds defined range";
                this.Track_Angle = "Value exceeds defined range";
            }
            position = position + 4;

            return position;
        }



        // DATA ITEM I021/165 [Track Angle Rate]
        public int Track_Angle_Rate(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position + 1]);
            this.TAR = Convert.ToString(Convert.ToInt32(fullmessage.Substring(6, 10),2));
            position = position + 2;

            return position;
        }



        // DATA ITEM I021/077 [Time Of Report Transmission]
        public int Time_of_Report_Transmission(string[] message, int position)
        {
            int binaryValue = Convert.ToInt32(string.Concat(message[position], message[position + 1], message[position + 2]), 2);
            double seconds = binaryValue / 128.0;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            this.Tort = time.ToString(@"hh\:mm\:ss\:fff");
            position = position + 3;

            return position;
        }



        // DATA ITEM I021/170 [Target Identification]
        public int Target_Identification(string[] message, int position)
        {
            string charecter;
            string fullmessage = String.Concat(message[position], message[position + 1], message[position + 2], message[position + 3], message[position + 4], message[position + 5]);
            this.Target_ID = "";
            int i = 2;
            while (i < fullmessage.Length)
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
            position = position + 6;

            return position;
        }



        // DATA ITEM I021/020 [Emitte Category]
        public int Emitter_Category(string[] message, int position)
        {
            this.ECAT = Convert.ToString(Convert.ToInt32(message[position],2));
            if (this.Target_ID == "7777XBEG") { this.ECAT = "No ADS-B Emitter Category Information"; }
            else
            {
                if (this.ECAT == "0") { this.ECAT = "No ADS-B Emitter Category InformationNo ADS-B Emitter Category Information"; }
                if (this.ECAT == "1") { this.ECAT = "light aircraft <= 15500 lbs"; }
                if (this.ECAT == "2") { this.ECAT = "15500 lbs < small aircraft <75000 lbs"; }
                if (this.ECAT == "3") { this.ECAT = "75000 lbs < medium a/c < 300000 lbs"; }
                if (this.ECAT == "4") { this.ECAT = "High Vortex Large"; }
                if (this.ECAT == "5") { this.ECAT = "300000 lbs <= heavy aircraft"; }
                if (this.ECAT == "6") { this.ECAT = "highly manoeuvrable (5g acceleration capability) and high speed (>400 knots cruise)"; }
                if (this.ECAT == "7") { this.ECAT = "reserved"; }
                if (this.ECAT == "8") { this.ECAT = "reserved"; }
                if (this.ECAT == "9") { this.ECAT = "reserved"; }
                if (this.ECAT == "10") { this.ECAT = "rotocraft"; }
                if (this.ECAT == "11") { this.ECAT = "glider / sailplane"; }
                if (this.ECAT == "12") { this.ECAT = "lighter-than-air"; }
                if (this.ECAT == "13") { this.ECAT = "unmanned aerial vehicle"; }
                if (this.ECAT == "14") { this.ECAT = "space / transatmospheric vehicle"; }
                if (this.ECAT == "15") { this.ECAT = "ultralight / handglider / paraglider"; }
                if (this.ECAT == "16") { this.ECAT = "parachutist / skydiver"; }
                if (this.ECAT == "17") { this.ECAT = "reserved"; }
                if (this.ECAT == "18") { this.ECAT = "reserved"; }
                if (this.ECAT == "19") { this.ECAT = "reserved"; }
                if (this.ECAT == "20") { this.ECAT = "surface emergency vehicle"; }
                if (this.ECAT == "21") { this.ECAT = "surface service vehicle"; }
                if (this.ECAT == "22") { this.ECAT = "fixed ground or tethered obstruction"; }
                if (this.ECAT == "23") { this.ECAT = "cluster obstacle"; }
                if (this.ECAT == "24") { this.ECAT = "line obstacle"; }
            }
            

            position = position + 1;
            return position;
        }



        // DATA ITEM I021/220 [Met Information]
        public int Met_Information(string[] message, int position)
        {
            this.WS = message[position].Substring(0, 1);
            if (this.WS == "0") { this.WS = "Absence of Subfield #1"; }
            
            this.WD = message[position].Substring(1, 1);
            if (this.WD == "0") { this.WD = "Absence of Subfield #2"; }
            
            this.TMP = message[position].Substring(2, 1);
            if (this.TMP == "0") { this.TMP = "Absence of Subfield #3"; }
            
            this.TRB = message[position].Substring(3, 1);
            if (this.TRB == "0") { this.TRB = "Absence of Subfield #3"; }

            if (message[position].Substring(7, 1) == "1") 
            { 
                position = position + 1;
                if (this.WS == "1") 
                { 
                    this.WS = "Presence of Subfield #1: " + Convert.ToString(Convert.ToInt32(string.Concat(message[position], message[position] + 1))); 
                    position = position + 2;
                }
                if (this.WD == "1") 
                { 
                    this.WD = "Presence of Subfield #2: " + Convert.ToString(Convert.ToInt32(string.Concat(message[position], message[position] + 1)));
                    position = position + 2;
                }
                if (this.TMP == "1") 
                { 
                    this.TMP = "Presence of Subfield #3: " + Convert.ToString(Convert.ToInt32(string.Concat(message[position], message[position] + 1)) * 0.25);
                    position = position + 2;
                }
                if (this.TRB == "1") 
                { 
                    this.TRB = "Presence of Subfield #3: " + Convert.ToString(Convert.ToInt32(message[position])); 
                    position = position + 1;
                }
            }
            else
            {
                position = position + 1;
            }
            return position;
        }



        // DATA ITEM I021/146 [Selected Altitude]
        public int Selected_Altitude(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position+1]);
            this.SAS = fullmessage.Substring(0,1);
            if (this.SAS == "0") { this.SAS = "No source information provided"; }
            if (this.SAS == "1") { this.SAS = "Source Information provided"; }

            this.Source = fullmessage.Substring(1,2);
            if (this.Source == "00") { this.Source = "Unknown"; }
            if (this.Source == "01") { this.Source = "Aircraft Altitude (Holding Altitude)"; }
            if (this.Source == "10") { this.Source = "MCP/FCU Selected Altitude"; }
            if (this.Source == "11") { this.Source = "FMS Selected Altitude"; }

            this.Altitude = Convert.ToString((convertor.TWO_Complement(fullmessage.Substring(3,13))*25))+ " ft";
            position = position + 2;

            return position;
        }



        // DATA ITEM I021/148 [Final State Selected Altitude]
        public int Final_State_Selected_Altitude(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position+1]);
            this.MV = fullmessage.Substring(0, 1);
            if (this.MV == "0") { this.MV = "Not active or unknown"; }
            if (this.MV == "1") { this.MV = "Active"; }

            this.AH = fullmessage.Substring(1, 1);
            if (this.AH == "0") { this.AH = "Not active or unknown"; }
            if (this.AH == "1") { this.AH = "Active"; }

            this.AM = fullmessage.Substring(2, 1);
            if (this.AM == "0") { this.AM = "Not active or unknown"; }
            if (this.AM == "1") { this.AM = "Active"; }

            this.AltitudeF = Convert.ToString(convertor.TWO_Complement(fullmessage.Substring(3, 13))*25) + " ft";
            position = position + 2; 

            return position;
        }



        // DATA ITEM I021/110 [Trajectory Intent]
        public int Trajectory_Intent(string[] message, int position)
        {
            this.TrajectoryIntent = "Data Avaiable";
            this.TIS = message[position].Substring(0, 1);
            if (this.TIS == "0") { this.WS = "Absence of Subfield #1"; }

            this.TID = message[position].Substring(1, 1);
            if (this.TID == "0") { this.WD = "Absence of Subfield #2"; }


            if (this.TIS == "1")
            {
                position = position + 1;
                this.NAV = message[position].Substring(1, 1);
                if (this.NAV == "0") { this.NAV = "Trajectory Intent Data is available for this aircraft"; }
                if (this.NAV == "1") { this.NAV = "Trajectory Intent Data is not available for this aircraft"; }

                this.NVB = message[position].Substring(2, 1);
                if (this.NVB == "0") { this.NVB = "Trajectory Intent Data is valid"; }
                if (this.NVB == "1") { this.NVB = "Trajectory Intent Data is not valid"; }


            }
            if (this.TID == "1")
            {
                position = position + 1;
                this.REP1 = Convert.ToInt32(message[position]);
                TCA = new string[REP];
                NC = new string[REP];
                TCPNumber = new int[REP];
                AltitudeTID = new string[REP];
                LATITUDE = new string[REP];
                LONGITUDE = new string[REP];
                Point_Type = new string[REP];
                TD = new string[REP];
                TRA = new string[REP];
                TOA = new string[REP];
                TOV = new string[REP];
                TTR = new string[REP];
                position = position + 1; ;

                for (int i = 0; i < REP; i++)
                {
                    this.TCA[i] = message[position].Substring(0, 1);
                    if (this.TCA[i] == "0") { this.TCA[i] = "TCP number available"; }
                    if (this.TCA[i] == "1") { this.TCA[i] = "TCP number not available"; }

                    this.NC[i] = message[position].Substring(1, 1);
                    if (this.NC[i] == "0") { this.NC[i] = "TCP compliance"; }
                    if (this.NC[i] == "1") { this.NC[i] = "TCP non-compliance"; }

                    this.TCPNumber[i] = Convert.ToInt32(message[position].Substring(2, 5));

                    position = position + 1;

                    string fullmessage = string.Concat(message[position], message[position] + 1);
                    this.AltitudeTID[i] = Convert.ToString(Convert.ToInt32(convertor.TWO_Complement(fullmessage)) * 10);
                    position = position + 2;

                    string fullmessage1 = string.Concat(message[position], message[position] + 1, message[position] + 2);
                    this.LATITUDE[i] = Convert.ToString(Convert.ToInt32(convertor.TWO_Complement(fullmessage1)) * 180 * 2E-23);
                    position = position + 3;

                    string fullmessage2 = string.Concat(message[position], message[position] + 1, message[position] + 2);
                    this.LONGITUDE[i] = Convert.ToString(Convert.ToInt32(convertor.TWO_Complement(fullmessage1)) * 180 * 2E-23);
                    position = position + 3;

                    this.PT[i] = message[position].Substring(0, 4);
                    if (this.PT[i] == "0000") { this.PT[i] = "Unknown"; }
                    if (this.PT[i] == "0001") { this.PT[i] = "Fly by waypoint (LT)"; }
                    if (this.PT[i] == "0010") { this.PT[i] = "Fly over waypoint (LT)"; }
                    if (this.PT[i] == "0011") { this.PT[i] = "Hold pattern (LT)"; }
                    if (this.PT[i] == "0100") { this.PT[i] = "Procedure hold (LT)"; }
                    if (this.PT[i] == "0101") { this.PT[i] = "Procedure turn (LT)"; }
                    if (this.PT[i] == "0110") { this.PT[i] = "RF leg (LT)"; }
                    if (this.PT[i] == "0111") { this.PT[i] = "Top of climb (VT)"; }
                    if (this.PT[i] == "1000") { this.PT[i] = "Top of descent (VT)"; }
                    if (this.PT[i] == "1001") { this.PT[i] = "Start of level (VT)"; }
                    if (this.PT[i] == "1010") { this.PT[i] = "Cross-over altitude (VT)"; }
                    if (this.PT[i] == "1011") { this.PT[i] = "Transition altitude (VT)"; }

                    this.TD[i] = message[position].Substring(4, 2);
                    if (this.TD[i] == "00") { this.TD[i] = "N/A"; }
                    if (this.TD[i] == "01") { this.TD[i] = "Turn right"; }
                    if (this.TD[i] == "10") { this.TD[i] = "Turn left"; }
                    if (this.TD[i] == "11") { this.TD[i] = "No turn"; }

                    this.TRA[i] = message[position].Substring(6, 1);
                    if (this.TRA[i] == "0") { this.TRA[i] = "TTR not available"; }
                    if (this.TRA[i] == "1") { this.TRA[i] = "TTR available"; }
                    position = position + 1;

                    this.TOV[i] = Convert.ToString(Convert.ToInt32(string.Concat(message[position], message[position] + 1, message[position] + 2))) + "s";
                    position = position + 3;

                    this.TTR[i] = Convert.ToString(Convert.ToInt32(string.Concat(message[position], message[position] + 1)) * 0.01) + "Nm";
                    position = position + 1;
                }
            }

            return position;
        }



        // DATA ITEM I021/016 [Service Managment]
        public int Service_Managment(string[] message, int position)
        {
            this.RP = Convert.ToString(Convert.ToInt32(message[position],2) * 0.5) + " s";
            position = position + 1;

            return position;
        }



        // DATA ITEM I021/008 [Aircraft Operational Status]
        public int Aircraft_operational_Status(string[] message, int position)
        {
            this.RA_Status = message[position].Substring(0, 1);
            if (this.RA_Status == "0") { this.RA_Status = "TCAS II or ACAS RA not active"; }
            if (this.RA_Status == "1") { this.RA_Status = "TCAS RA active "; }

            this.TC_Status = message[position].Substring(1, 2);
            if (this.TC_Status == "00") { this.TC_Status = "no capability for Trajectory Change Reports"; }
            if (this.TC_Status == "01") { this.TC_Status = "support for TC+0 reports only"; }
            if (this.TC_Status == "10") { this.TC_Status = "support for multiple TC reports"; }
            if (this.TC_Status == "11") { this.TC_Status = "reserved"; }

            this.TS_Status = message[position].Substring(3, 1);
            if (this.TS_Status == "0") { this.TS_Status = "no capability to support Target State Reports"; }
            if (this.TS_Status == "1") { this.TS_Status = "capable of supporting target State Reports"; }

            this.ARV_Status = message[position].Substring(4, 1);
            if (this.ARV_Status == "0") { this.ARV_Status = "no capability to generate ARV-reports"; }
            if (this.ARV_Status == "1") { this.ARV_Status = "capable of generate ARV-reports"; }

            this.CDTI_A_Status = message[position].Substring(5, 1);
            if (this.CDTI_A_Status == "0") { this.CDTI_A_Status = "CDTI not operational"; }
            if (this.CDTI_A_Status == "1") { this.CDTI_A_Status = "CDTI operational"; }

            this.not_TCAS_Status = message[position].Substring(6, 1);
            if (this.not_TCAS_Status == "0") { this.not_TCAS_Status = "TCAS operational"; }
            if (this.not_TCAS_Status == "1") { this.not_TCAS_Status = "TCAS not operational "; }

            this.SA_Status = message[position].Substring(6, 1);
            if (this.SA_Status == "0") { this.SA_Status = "Antenna Diversity"; }
            if (this.SA_Status == "1") { this.SA_Status = "Single Antenna only"; }

            position = position + 1;

            return position;
        }



        // DATA ITEM I021/271 [Surface Capabilities and Characteristics]
        public int Surface_Capabilities_and_Characteristics(string[] message, int position)
        {
            this.POA = message[position].Substring(2, 1);
            if (this.POA == "0") { this.POA = "Position transmitted is not ADS-B position reference point"; }
            if (this.POA == "1") { this.POA = "Position transmitted is the ADS-B position reference point"; }

            this.CDTIS = message[position].Substring(3, 1);
            if (this.CDTIS == "0") { this.CDTIS = "CDTI not operational"; }
            if (this.CDTIS == "1") { this.CDTIS = "CDTI operational"; }

            this.B2low = message[position].Substring(4, 1);
            if (this.B2low == "0") { this.B2low = "≥ 70 Watts"; }
            if (this.B2low == "1") { this.B2low = "< 70 Watts"; }

            this.RAS = message[position].Substring(5, 1);
            if (this.RAS == "0") { this.RAS = "Aircraft not receiving ATC-services"; }
            if (this.RAS == "1") { this.RAS = "Aircraft receiving ATC services"; }

            this.IDENT = message[position].Substring(6, 1);
            if (this.IDENT == "0") { this.IDENT = "IDENT switch not active"; }
            if (this.IDENT == "1") { this.IDENT = "IDENT switch active"; }

            if (message[position].Substring(7, 1) == "1") 
            { 
                position = position + 1;
                this.LW = message[position].Substring(4, 4);
                if (this.LW == "0000") { this.LW = "L < 15 and W < 11.5"; }
                if (this.LW == "0001") { this.LW = "L < 15 and W < 23"; }
                if (this.LW == "0010") { this.LW = "L < 25 and W < 28.5"; }
                if (this.LW == "0011") { this.LW = "L < 25 and W < 34"; }
                if (this.LW == "0100") { this.LW = "L < 35 and W < 33"; }
                if (this.LW == "0101") { this.LW = "L < 35 and W < 38"; }
                if (this.LW == "0110") { this.LW = "L < 45 and W < 39.5"; }
                if (this.LW == "0111") { this.LW = "L < 45 and W < 45"; }
                if (this.LW == "1000") { this.LW = "L < 55 and W < 45"; }
                if (this.LW == "1001") { this.LW = "L < 55 and W < 52"; }
                if (this.LW == "1010") { this.LW = "L < 65 and W < 59.5"; }
                if (this.LW == "1011") { this.LW = "L < 65 and W < 67"; }
                if (this.LW == "1100") { this.LW = "L < 75 and W < 72.5"; }
                if (this.LW == "1101") { this.LW = "L < 75 and W < 80"; }
                if (this.LW == "1110") { this.LW = "L < 85 and W < 80"; }
                if (this.LW == "1111") { this.LW = "L < 85 and W > 80"; }
                position = position + 1;
            }
            position = position + 1;
            return position;
        }



        // DATA ITEM I021/132 [Message Amplitude]
        public int Message_Amplitude(string[] message, int position)
        {
            this.MAM = Convert.ToString(convertor.TWO_Complement(message[position])) + " dBm"; ;
            position = position + 1;

            return position;
        }



        // DATA ITEM I021/250 [ModeS MB Data]
        public int ModeS_MB_Data(string[] message, int position)
        {
            this.ModeSMBData = convertor.Binary_Octet_To_Hexadecimal(message[position]);
            if (Convert.ToInt32(this.ModeSMBData) < 0)
            {
                this.MB_Data_ModeS = new string[Convert.ToInt32(this.ModeSMBData)];
                this.BDS1_ModeS = new string[Convert.ToInt32(this.ModeSMBData)];
                this.BDS2_ModeS = new string[Convert.ToInt32(this.ModeSMBData)];
            }
            position = position + 1;
            int i = 0;

            while (i < Convert.ToInt32(this.ModeSMBData))
            {
                this.MB_Data_ModeS[i] = String.Concat(message[position], message[position + 1], message[position + 2], message[position + 3], message[position + 4], message[position + 5], message[position + 6]);
                this.BDS1_ModeS[1] = message[position + 7].Substring(0, 4);
                this.BDS2_ModeS[1] = message[position + 7].Substring(4, 4);
                position = position + 8;
                i = i + 1;
            }

            return position;
        }



        // DATA ITEM I021/260 [ACAS Resolution Advisory Report]
        public int ACAS_Resolution_Advisory_Report(string[] message, int position)
        {
            string messg = string.Concat(message[position], message[position + 1], message[position + 2], message[position + 3], message[position + 4], message[position + 5], message[position + 6]);
            TYP = messg.Substring(0, 5);
            STYP = messg.Substring(5, 3);
            ARA = messg.Substring(8, 14);
            RAC = messg.Substring(22, 4);
            RAT = messg.Substring(26, 1);
            MTE = messg.Substring(27, 1);
            TTI = messg.Substring(28, 2);
            TID = messg.Substring(30, 26);
            position = position + 7;

            return position;
        }



        // DATA ITEM I021/400 [Reciver ID]
        public int Reciever_ID(string[] message, int position)
        {
            this.RID = Convert.ToString(Convert.ToInt32(message[position],2));
            position = position + 1;

            return position;
        }



        // DATA ITEM I021/295 [Data Ages]
        public int Data_Ages(string[] message, int position)
        {
            int x = position;
            if (message[position].Substring(7, 1) == "1")
            {
                position = position +1;
                if (message[position].Substring(7, 1) == "1")
                {
                    position = position + 1;
                    if (message[position].Substring(7, 1) == "1")
                    {
                        position = position + 1;
                    }
                }
            }
            position = position + 1;
            if (message[x].Substring(0, 1) == "1")
            { 
                this.AOS = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                position = position + 1;
            }
            if (message[x].Substring(1, 1) == "1")
            {
                this.TRD = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                position = position + 1;
            }
            if (message[x].Substring(2, 1) == "1")
            {
                this.M3A = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                position = position + 1;
            }
            if (message[x].Substring(3, 1) == "1")
            {
                this.QI = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                position = position + 1;
            }
            if (message[x].Substring(4, 1) == "1")
            {
                this.TI = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                position = position + 1;
            }
            if (message[x].Substring(5, 1) == "1")
            {
                this.MAM_ages = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                position = position + 1;
            }
            if (message[x].Substring(6, 1) == "1")
            {
                this.GH = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                position = position + 1;
            }
            if (message[x].Substring(7, 1) == "1")
            {
                if (message[x + 1].Substring(0, 1) == "1") 
                { 
                    this.FL_ages = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
                if (message[x + 1].Substring(1, 1) == "1")
                {
                    this.ISA = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
                if (message[x + 1].Substring(2, 1) == "1")
                {
                    this.FSA = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
                if (message[x + 1].Substring(3, 1) == "1")
                {
                    this.AS = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
                if (message[x + 1].Substring(4, 1) == "1")
                {
                    this.TAS = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
                if (message[x + 1].Substring(5, 1) == "1")
                {
                    this.MH = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
                if (message[x + 1].Substring(6, 1) == "1")
                {
                    this.BVR = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
            }
            if (message[x + 1].Substring(7, 1) == "1")
            {
                if (message[x + 2].Substring(0, 1) == "1") 
                { 
                    this.GVR = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
                if (message[x + 2].Substring(1, 1) == "1")
                {
                    this.GV = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
                if (message[x + 2].Substring(2, 1) == "1")
                {
                    this.TAR_ages = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
                if (message[x + 2].Substring(3, 1) == "1")
                {
                    this.TI_ages = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
                if (message[x + 2].Substring(4, 1) == "1")
                {
                    this.TS_ages = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
                if (message[x + 2].Substring(5, 1) == "1")
                {
                    this.MET = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
                if (message[x + 2].Substring(6, 1) == "1")
                {
                    this.ROA = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
            }
            if (message[x + 2].Substring(7, 1) == "1")
            {
                if (message[x + 3].Substring(0, 1) == "1") 
                { 
                    this.ARA_ages = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
                if (message[x + 3].Substring(1, 1) == "1") 
                { 
                    this.SCC = Convert.ToString(Convert.ToInt32(message[position], 2) * 0.1) + " s";
                    position = position + 1;
                }
            }

            return position;
        }

    }


    //.....................................................................................................................................................................................
    //.....................................................................................................................................................................................
    //.....................................................................................................................................................................................
    //.....................................................................................................................................................................................
    //.....................................................................................................................................................................................
}