using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public string MessageTYPE;             // DATA ITEM I021/000
        public string SAC;                     // DATA ITEM I021/010
        public string SIC;                     // DATA ITEM I021/010
        public string RA;                     // DATA ITEM I021/008
        public string TC;                     // DATA ITEM I021/008
        public string TS;                     // DATA ITEM I021/008
        public string ARV;                     // DATA ITEM I021/008
        public string CDTI;                     // DATA ITEM I021/008
        public string NotTCAS;                     // DATA ITEM I021/008
        public string SA;                     // DATA ITEM I021/008
        public string ATP;                     // DATA ITEM I021/040
        public string ARC;                     // DATA ITEM I021/040
        public string RC;                     // DATA ITEM I021/040
        public string RAB;                     // DATA ITEM I021/040
        public string FX;                     // DATA ITEM I021/040
        public string DCR;                   // DATA ITEM I021/040
        public string GBS;          // DATA ITEM I021/040
        public string SIM;         // DATA ITEM I021/040
        public string TST;             // DATA ITEM I021/040
        public string SAA;             // DATA ITEM I021/040
        public string CL;                // DATA ITEM I021/040
        public string CA;                // DATA ITEM I021/040
        public string Track_number;                // DATA ITEM I010/060
        public string Service_identification;            // DATA ITEM I010/060
        public string IM;                    // DATA ITEM I010/090
        public string RE;                    // DATA ITEM I010/090
        public string FL;                      // DATA ITEM I010/090
        public string VNS;                 // DATA ITEM I010/091
        public string VN;                     // DATA ITEM I010/131
        public string LTT;   // DATA ITEM I010/140
        public int Time_of_day_in_seconds;     // DATA ITEM I010/140
        public string OctalA;            // DATA ITEM I010/161
        public string OctalB;                     // DATA ITEM I010/170
        public string OctalC;                     // DATA ITEM I010/170
        public string OctalD;                     // DATA ITEM I010/170
        public string M3AC;                     // DATA ITEM I010/170
        public string FlightLevel;                     // DATA ITEM I010/170
        public string ICF;                     // DATA ITEM I010/170
        public string LNAV;                     // DATA ITEM I010/170
        public string PS;                     // DATA ITEM I010/170
        public string SS;                     // DATA ITEM I010/170
        public string TAR;                     // DATA ITEM I010/170
        public string Ground_Speed;            // DATA ITEM I010/200
        public string Track_Angle;             // DATA ITEM I010/200
        public string Vx;                      // DATA ITEM I010/202
        public string Vy;                      // DATA ITEM I010/202
        public string Ax;                      // DATA ITEM I010/210
        public string Ay;                      // DATA ITEM I010/210
        public string ToAfP;           // DATA ITEM I010/220
        public string Tort;                     // DATA ITEM I010/245
        public string ECAT;               // DATA ITEM I010/245
        public string WS;               // DATA ITEM I010/250
        public string WD;         // DATA ITEM I010/250
        public string TMP;            // DATA ITEM I010/250
        public string TRB;            // DATA ITEM I010/250
        public string SAS;                  // DATA ITEM I010/270
        public string Source;             // DATA ITEM I010/270
        public string Altitude;                   // DATA ITEM I010/270
        public int REP;                        // DATA ITEM I010/270
        public string MV;                  // DATA ITEM I010/270
        public string AH;                // DATA ITEM I010/270
        public string AM;        // DATA ITEM I010/300
        public string AltitudeF;                     // DATA ITEM I010/310
        public string MSG;                     // DATA ITEM I010/310
        public string TIS;                 // DATA ITEM I010/500
        public string TID;                 // DATA ITEM I010/500
        public string NAV;                // DATA ITEM I010/500
        public string NVD;                    // DATA ITEM I010/550
        public string OVL;                     // DATA ITEM I010/550
        public string TSV;                     // DATA ITEM I010/550
        public string DIV;                     // DATA ITEM I010/550
        public string TTF;                     // DATA ITEM I010/550
        public int X = 1;
        public string REP1;                  // DATA ITEM I010/270
        public string TCA;                // DATA ITEM I010/270
        public string NC;        // DATA ITEM I010/300
        public string TCPNumber;                     // DATA ITEM I010/310
        public string AltitudeTID;                     // DATA ITEM I010/310
        public string LATITUDE;                 // DATA ITEM I010/500
        public string LONGITUDE;                 // DATA ITEM I010/500
        public string PT;                // DATA ITEM I010/500
        public string TD;                      // DATA ITEM I010/550
        public string TRA;                     // DATA ITEM I010/550
        public string TOA;                     // DATA ITEM I010/550
        public string TOV;                     // DATA ITEM I010/550
        public string TTR;                     // DATA ITEM I010/550
        public string RP;
        public string POA;
        public string CDTIS;
        public string B2low;
        public string RAS;
        public string IDENT;
        public string LW;
        public string MAM;
        public string RID;


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
                if (FSPEC[47] == '1')
                    position = this.Reserved_expantion_Field(message, position);
                if (FSPEC[48] == '1')
                    position = this.Spetial_Purpose_Field(message, position);
            }
        }


        // Data Item I021/008
        public int Aircraft_Operational_Status(string[] message, int position)
        {
            this.RA = message[position].Substring(0, 1);
            if (this.RA == "0") { this.RA = "TCAS II or ACAS RA not active"; }
            if (this.RA == "1") { this.RA = "TCAS RA active"; }

            this.TC = message[position].Substring(1,2);
            if (this.TC == "00") { this.TC = "no capability for Trajectory Change Reports"; }
            if (this.TC == "01") { this.TC = "support for TC+0 reports only"; }
            if (this.TC == "10") { this.TC = "support for multiple TC reports"; }
            if (this.TC == "11") { this.TC = "reserved"; }

            this.TS = message[position].Substring(3,1);
            if (this.TS == "0") { this.TS = "no capability to support Target State Reports"; }
            if (this.TS == "1") { this.TS = "capable of supporting target State Reports"; }

            this.ARV = message[position].Substring(4,1);
            if (this.ARV == "0") { this.ARV = "no capability to generate ARV-reports"; }
            if (this.ARV == "1") { this.ARV = "capable of generate ARV-reports"; }

            this.CDTI = message[position].Substring(5,1);
            if (this.CDTI == "0") { this.CDTI = "CDTI not operational"; }
            if (this.CDTI == "1") { this.CDTI = "CDTI operational"; }

            this.NotTCAS = message[position].Substring(6,1);
            if (this.NotTCAS == "0") { this.NotTCAS = "TCAS operationa"; }
            if (this.NotTCAS == "1") { this.NotTCAS = "TCAS not operational"; }

            this.SA = message[position].Substring(7,1);
            if (this.SA == "0") { this.SA = "Antenna Diversity"; }
            if (this.SA == "1") { this.SA = "Single Antenna only"; }
            else
            {
                // End of Data Item
                position = position + 1;
            }
            return position;
        }

        // Data Item I021/010
        public int Data_Source_Identifier(string[] message, int position)
        {
            this.SAC = Convert.ToString(Convert.ToInt32(message[position], 2));
            this.SIC = Convert.ToString(Convert.ToInt32(message[position + 1], 2));
            position = position + 2;

            return position;
        }

        // Data Item I021/040
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

            this.FX = message[position].Substring(7, 1);
            if (this.FX == "0") { this.FX = "Report from target transponder"; }
            if (this.FX == "1") 
            { 
                this.FX = "Extension into first extension"; 
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

                this.CA = message[position].Substring(5, 2);
                if (this.CA == "00") { this.CA = "Report valid"; }
                if (this.CA == "01") { this.CA = "Report suspect"; }
                if (this.CA == "10") { this.CA = "No information"; }
                if (this.CA == "11") { this.CA = "Reserved for future use"; }

                this.FX = message[position].Substring(7, 1);
                if (this.FX == "0") { this.FX = "End of item"; }
                if (this.FX == "1") { this.FX = "Extension into second extension"; }

                position++;

            }
            return position;
        }

        // Data Item I021/161
        public int Track_Number(string[] message, int position)
        {
            
            string fullmessage = String.Concat(message[position], message[position + 1]);
            this.Track_number = Convert.ToString(Convert.ToInt32(fullmessage));
            position = position + 2;

            return position;
        }

        // DATA ITEM I021/015
        public int Service_Identification(string[] message, int position)
        {
            this.Service_identification = "Service Identification: " + Convert.ToString(Convert.ToInt32(convertor.Twos_Complement(message[position])));
            position= position + 1;

            return position;
        }

        // DATA ITEM I021/071
        public int Time_of_Aplicability_for_Position(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1], message[position + 2]);
            this.ToAfP = Convert.ToString(Convert.ToInt32((fullmessage)) * 1/128) + "s";
            position = position + 3;

            return position;
        }

        // DATA ITEM I021/130
        public int Position_in_WGS84_Coordinates(string[] message, int position)
        {

            position = position + 6;

            return position;
        }

        // DATA ITEM I021/131
        public int Position_in_WGS84_Coordinates_Highres(string[] message, int position)
        {

            position = position + 8;

            return position;
        }

        // DATA ITEM I021/072
        public int Time_of_Aplicability_for_Velocity(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1], message[position + 2]);
            this.ToAfP = Convert.ToString(Convert.ToInt32((fullmessage)) * 1 / 128) + "s";
            position = position + 3;

            return position;
        }

        // DATA ITEM I021/150
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
                this.IM = "Mach Number: " + Convert.ToString(Convert.ToInt32(fullmessage.Substring(1, 15)) * 0.001);
            }
            position = position + 2;

            return position;
        }

        // DATA ITEM I021/151
        public int True_Air_Speed(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position] + 1);
            this.RE = fullmessage.Substring(0, 1);
            if (this.RE == "0")
            {
                this.RE = "Value in defined range: " + Convert.ToString(Convert.ToInt32((fullmessage.Substring(1, 15))) + "Knots");
            }
            if (this.RE == "1")
            {
                this.RE = "Value exceeds defined range";
            }
            position = position + 2;

            return position;
        }

        // DATA ITEM I021/080
        public int Target_Address(string[] message, int position)
        {

            position = position + 3;

            return position;
        }

        // DATA ITEM I021/073
        public int Time_of_Message_Reception_of_Position(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1], message[position + 2]);
            this.ToAfP = Convert.ToString(Convert.ToInt32((fullmessage)) * 1 / 128) + "s";
            position = position + 3;

            return position;
        }

        // DATA ITEM I021/074
        public int Time_of_Message_Reception_of_Position_HighPrecision(string[] message, int position)
        {
            position = position +4;
            return position;
        }

        // DATA ITEM I021/075
        public int Time_of_Message_Reception_of_Velocity(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1], message[position + 2]);
            this.ToAfP = Convert.ToString(Convert.ToInt32((fullmessage)) * 1 / 128) + "s";
            position = position + 3;
            return position;
        }

        // DATA ITEM I021/076
        public int Time_of_Message_Reception_of_Velocity_HighPrecision(string[] message, int position)
        {
            position = position +4;

            return position;
        }

        // DATA ITEM I021/140
        public int Geometric_Height(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1]);
            this.Track_number = Convert.ToString(Convert.ToInt32(convertor.Twos_Complement(fullmessage) )* 6.25 + "ft");
            position = position + 2;

            return position;
        }

        // DATA ITEM I021/140
        public int Quality_Indicators(string[] message, int position)
        {
            position = position + 1;
            
            return position;
        }

        // DATA ITEM I021/090
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

        // DATA ITEM I021/070
        public int Mode_3A_Code(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1]);
            this.OctalA = Convert.ToString(Convert.ToInt32((fullmessage.Substring(4, 3))));
            this.OctalB = Convert.ToString(Convert.ToInt32((fullmessage.Substring(7, 3))));
            this.OctalC = Convert.ToString(Convert.ToInt32((fullmessage.Substring(10, 3))));
            this.OctalD = Convert.ToString(Convert.ToInt32((fullmessage.Substring(13, 3))));
            this.M3AC = "Mode 3/A Code" + this.OctalA + this.OctalB + this.OctalC + this.OctalD;
            
            position = position + 2;

            return position;
        }

        // DATA ITEM I021/230
        public int Roll_Angle(string[] message, int position)
        {
            position = position + 2;

            return position;
        }

        // DATA ITEM I021/145
        public int Flight_Level(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1]);
            this.FlightLevel = "Flight Level: " + Convert.ToString(Convert.ToInt32(convertor.Twos_Complement(fullmessage)));

            position = position + 2;

            return position;
        }

        // DATA ITEM I021/152
        public int Magnetic_Heading(string[] message, int position)
        {
            string fullmessage = String.Concat(message[position], message[position + 1]);
            this.Track_number = Convert.ToString(Convert.ToInt32((fullmessage) )* 360*2E-16 + "deg");
            position = position + 2;

            return position;
        }

        // DATA ITEM I021/200
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

        // DATA ITEM I021/155
        public int Barometric_Vertical_Rate(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position] + 1);
            this.RE = fullmessage.Substring(0, 1);
            if (this.RE == "0")
            {
                this.RE = "Value in defined range: " + Convert.ToString(Convert.ToInt32((fullmessage.Substring(1, 15)))*6.25 + "feet/minute");
            }
            if (this.RE == "1")
            {
                this.RE = "Value exceeds defined range";
            }
            position = position + 2;

            return position;
        }

        // DATA ITEM I021/155
        public int Geometric_Vertical_Rate(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position] + 1);
            this.RE = fullmessage.Substring(0, 1);
            if (this.RE == "0")
            {
                this.RE = "Value in defined range: " + Convert.ToString(Convert.ToInt32(convertor.TWO_Complement(fullmessage.Substring(1, 15)) * 6.25) + "feet/minute");
            }
            if (this.RE == "1")
            {
                this.RE = "Value exceeds defined range";
            }
            position = position + 2;

            return position;
        }

        // DATA ITEM I021/160
        public int Airborne_Ground_Vector(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position] + 1);
            string fullmessage2 = string.Concat(message[position] + 2, message[position] + 3);
            this.RE = fullmessage.Substring(0, 1);
            if (this.RE == "0")
            {
                this.Ground_Speed = "Value in defined range: " + Convert.ToString(Convert.ToInt32((fullmessage.Substring(1, 15)) )* 2E-14 + "NM/s");
                this.Track_Angle = "Value in defined range: " + Convert.ToString(Convert.ToInt32((fullmessage2.Substring(1, 15)) )* 360 * 2E-16 + "deg");

            }
            if (this.RE == "1")
            {
                this.Ground_Speed = "Value exceeds defined range";
                this.Track_Angle = "Value exceeds defined range";
            }
            position = position + 4;

            return position;
        }

        // DATA ITEM I021/165
        public int Track_Angle_Rate(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position] + 1);
            this.TAR = Convert.ToString(Convert.ToInt32(fullmessage.Substring(6, 10)));

            position = position + 1;
            return position;
        }

        // DATA ITEM I021/077
        public int Time_of_Report_Transmission(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position] + 1, message[position] + 2);
            this.Tort = Convert.ToString(Convert.ToInt32(fullmessage) * 1 / 128);
            position = position + 3;
            return position;
        }

        // DATA ITEM I021/170
        public int Target_Identification(string[] message, int position)
        {
            

            position = position + 8;
            return position;
        }

        // DATA ITEM I021/020
        public int Emitter_Category(string[] message, int position)
        {
            this.ECAT = message[position];
            if (this.ECAT == "00000001") { this.ECAT = "No ADS-B Emitter Category InformationNo ADS-B Emitter Category Information"; }
            if (this.ECAT == "00000010") { this.ECAT = "light aircraft <= 15500 lbs"; }
            if (this.ECAT == "00000011") { this.ECAT = "15500 lbs < small aircraft <75000 lbs"; }
            if (this.ECAT == "00000100") { this.ECAT = "75000 lbs < medium a/c < 300000 lbs"; }
            if (this.ECAT == "00000101") { this.ECAT = "High Vortex Large"; }
            if (this.ECAT == "00000110") { this.ECAT = "300000 lbs <= heavy aircraft"; }
            if (this.ECAT == "00000111") { this.ECAT = "highly manoeuvrable (5g acceleration capability) and high speed (>400 knots cruise)"; }
            if (this.ECAT == "00001000") { this.ECAT = "reserved"; }
            if (this.ECAT == "00001001") { this.ECAT = "reserved"; }
            if (this.ECAT == "00001010") { this.ECAT = "reserved"; }
            if (this.ECAT == "00001011") { this.ECAT = "rotocraft"; }
            if (this.ECAT == "00001100") { this.ECAT = "glider / sailplane"; }
            if (this.ECAT == "00001101") { this.ECAT = "lighter-than-air"; }
            if (this.ECAT == "00001110") { this.ECAT = "unmanned aerial vehicle"; }
            if (this.ECAT == "00001111") { this.ECAT = "space / transatmospheric vehicle"; }
            if (this.ECAT == "00010000") { this.ECAT = "ultralight / handglider / paraglider"; }
            if (this.ECAT == "00010001") { this.ECAT = "parachutist / skydiver"; }
            if (this.ECAT == "00010010") { this.ECAT = "reserved"; }
            if (this.ECAT == "00010011") { this.ECAT = "reserved"; }
            if (this.ECAT == "00010100") { this.ECAT = "reserved"; }
            if (this.ECAT == "00010101") { this.ECAT = "surface emergency vehicle"; }
            if (this.ECAT == "00010110") { this.ECAT = "surface service vehicle"; }
            if (this.ECAT == "00010111") { this.ECAT = "fixed ground or tethered obstruction"; }
            if (this.ECAT == "00011000") { this.ECAT = "cluster obstacle"; }
            if (this.ECAT == "00011001") { this.ECAT = "line obstacle"; }

            position = position + 1;
            return position;
        }

        // DATA ITEM I021/220
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

            this.FX = message[position].Substring(7, 1);
            if (this.FX == "0") { this.FX = "no extension"; }
            if (this.FX == "1") 
            { 
                position = position + 1;
                this.FX = "extension";
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
            return position;
        }

        // DATA ITEM I021/146
        public int Selected_Altitude(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position] + 1);
            this.SAS = fullmessage.Substring(0,1);
            if (this.SAS == "0") { this.SAS = "No source information provided"; }
            if (this.SAS == "1") { this.SAS = "Source Information provided"; }

            this.Source = fullmessage.Substring(1,2);
            if (this.Source == "00") { this.Source = "Unknown"; }
            if (this.Source == "01") { this.Source = "Aircraft Altitude (Holding Altitude)"; }
            if (this.Source == "10") { this.Source = "MCP/FCU Selected Altitude"; }
            if (this.Source == "11") { this.Source = "FMS Selected Altitude"; }

            this.Altitude = Convert.ToString(Convert.ToInt32(convertor.TWO_Complement(fullmessage.Substring(3,13))));

            position = position + 2;
            return position;
        }

        // DATA ITEM I021/148
        public int Final_State_Selected_Altitude(string[] message, int position)
        {
            string fullmessage = string.Concat(message[position], message[position] + 1);
            this.MV = fullmessage.Substring(0, 1);
            if (this.MV == "0") { this.MV = "Not active or unknown"; }
            if (this.MV == "1") { this.MV = "Active"; }

            this.AH = fullmessage.Substring(1, 1);
            if (this.AH == "0") { this.AH = "Not active or unknown"; }
            if (this.AH == "1") { this.AH = "Active"; }

            this.AM = fullmessage.Substring(2, 1);
            if (this.AM == "0") { this.AM = "Not active or unknown"; }
            if (this.AM == "1") { this.AM = "Active"; }

            this.AltitudeF = Convert.ToString(Convert.ToInt32(convertor.TWO_Complement(fullmessage.Substring(3, 13))*25)) + " ft";

            position = position + 2; 
            return position;
        }

        // DATA ITEM I021/110
        public int Trajectory_Intent(string[] message, int position)
        {
            this.TIS = message[position].Substring(0, 1);
            if (this.TIS == "0") { this.WS = "Absence of Subfield #1"; }

            this.TID = message[position].Substring(1, 1);
            if (this.TID == "0") { this.WD = "Absence of Subfield #2"; }

            this.FX = message[position].Substring(7, 1);
            if (this.FX == "0") { this.FX = "no extension"; }
            if (this.FX == "1")
            {
                position = position + 1;
                this.FX = "extension";
                if (this.TIS == "1")
                {
                    this.NAV = message[position].Substring(1, 1);
                    if (this.NAV == "0") { this.NAV = "Trajectory Intent Data is available for this aircraft"; }
                    if (this.NAV == "1") { this.NAV = "Trajectory Intent Data is not available for this aircraft"; }

                    this.NVD = message[position].Substring(2, 1);
                    if (this.NVD == "0") { this.NVD = "Trajectory Intent Data is valid"; }
                    if (this.NVD == "1") { this.NVD = "Trajectory Intent Data is not valid"; }

                    this.FX = message[position].Substring(7, 1);
                    if (this.FX == "0") { this.FX = "no extension"; }
                    if (this.FX == "1") { this.FX = "extension"; }
                    
                }
                if (this.TID == "1")
                {
                    position = position + 1;
                    this.REP1 = Convert.ToString(Convert.ToInt32(message[position]));

                    this.TCA = message[position].Substring(0, 1);
                    if (this.TCA == "0") { this.TCA = "TCP number available"; }
                    if (this.TCA == "1") { this.TCA = "TCP number not available"; }

                    this.NC = message[position].Substring(1, 1);
                    if (this.NC == "0") { this.NC = "TCP compliance"; }
                    if (this.NC == "1") { this.NC = "TCP non-compliance"; }

                    this.TCPNumber = Convert.ToString(Convert.ToInt32(message[position].Substring(2, 5)));

                    position = position + 1;

                    string fullmessage = string.Concat(message[position], message[position] + 1);
                    this.AltitudeTID = Convert.ToString(Convert.ToInt32(convertor.TWO_Complement(fullmessage)) * 10);
                    position = position + 2;

                    string fullmessage1 = string.Concat(message[position], message[position] + 1, message[position] + 2);
                    this.LATITUDE = Convert.ToString(Convert.ToInt32(convertor.TWO_Complement(fullmessage1)) * 180 * 2E-23);
                    position = position + 3;

                    string fullmessage2 = string.Concat(message[position], message[position] + 1, message[position] + 2);
                    this.LONGITUDE = Convert.ToString(Convert.ToInt32(convertor.TWO_Complement(fullmessage1)) * 180 * 2E-23);
                    position = position + 3;

                    this.PT = message[position].Substring(0, 4);
                    if (this.PT == "0000") { this.PT = "Unknown"; }
                    if (this.PT == "0001") { this.PT = "Fly by waypoint (LT)"; }
                    if (this.PT == "0010") { this.PT = "Fly over waypoint (LT)"; }
                    if (this.PT == "0011") { this.PT = "Hold pattern (LT)"; }
                    if (this.PT == "0100") { this.PT = "Procedure hold (LT)"; }
                    if (this.PT == "0101") { this.PT = "Procedure turn (LT)"; }
                    if (this.PT == "0110") { this.PT = "RF leg (LT)"; }
                    if (this.PT == "0111") { this.PT = "Top of climb (VT)"; }
                    if (this.PT == "1000") { this.PT = "Top of descent (VT)"; }
                    if (this.PT == "1001") { this.PT = "Start of level (VT)"; }
                    if (this.PT == "1010") { this.PT = "Cross-over altitude (VT)"; }
                    if (this.PT == "1011") { this.PT = "Transition altitude (VT)"; }

                    this.TD = message[position].Substring(4, 2);
                    if (this.PT == "00") { this.PT = "N/A"; }
                    if (this.PT == "01") { this.PT = "Turn right"; }
                    if (this.PT == "10") { this.PT = "Turn left"; }
                    if (this.PT == "11") { this.PT = "No turn"; }

                    this.TRA = message[position].Substring(6, 1);
                    if (this.TRA == "0") { this.TRA = "TTR not available"; }
                    if (this.TRA == "1") { this.TRA = "TTR available"; }
                    position = position + 1;

                    this.TOV = Convert.ToString(Convert.ToInt32(string.Concat(message[position], message[position] + 1, message[position] + 2))) + "s";
                    position = position + 3;

                    this.TTR = Convert.ToString(Convert.ToInt32(string.Concat(message[position], message[position] + 1)) * 0.01) + "Nm";
                    position = position + 1;
                }
                
            }
            position = position + 1;
            return position;
        }

        // DATA ITEM I021/016
        public int Service_Managment(string[] message, int position)
        {
            this.RP = Convert.ToString(Convert.ToInt32(message[position]) * 0.5) + " s";

            position = position + 1;
            return position;
        }

        // DATA ITEM I021/008
        public int Aircraft_operational_Status(string[] message, int position)
        {
            position = position + 1;
            return position;
        }

        // DATA ITEM I021/271
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

            this.FX = message[position].Substring(7, 1);
            if (this.FX == "0") { this.FX = "no extension"; }
            if (this.FX == "1") 
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

        // DATA ITEM I021/132
        public int Message_Amplitude(string[] message, int position)
        {
            this.MAM = Convert.ToString(Convert.ToInt32(convertor.TWO_Complement(message[position])));

            position = position + 1;
            return position;
        }

        // DATA ITEM I021/250
        public int ModeS_MB_Data(string[] message, int position)
        {
            position = position + 9;
            return position;
        }

        // DATA ITEM I021/260
        public int ACAS_Resolution_Advisory_Report(string[] message, int position)
        {
            position = position + 7;
            return position;
        }

        // DATA ITEM I021/400
        public int Reciever_ID(string[] message, int position)
        {
            this.RID = Convert.ToString(Convert.ToInt32(message[position]));

            position = position + 1;
            return position;
        }

        public int Data_Ages(string[] message, int position)
        {
            
            return position;
        }
    }
}