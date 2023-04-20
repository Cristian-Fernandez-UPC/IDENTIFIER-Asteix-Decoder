using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Project_1
{
    public class FileReader
    {
        // LIST
        //public List<CAT10> CAT10_list = new List<CAT10>();    // We create a list where all CAT10 messages will be
        //public List<CAT21> CAT21_list = new List<CAT21>();    // We create a list where all CAT21 messages will be

        Conversions convertor = new Conversions();



        // TABLE
        public DataTable CAT10_table = new DataTable();       // We create a table that will be fulfilled with all the data needed
        public DataTable CAT21_table = new DataTable();       // We create a table that will be fulfilled with all the data needed

        public DataTable getTableCAT10()
        {
            return CAT10_table;
        }

        public DataTable getTableCAT21()
        {
            return CAT21_table;
        }


        public string REPmessage;
        public string MB_Data_ModeSmessage;

        public FileReader()
        {
            this.Generate_Table_CAT10(); // We generate the columns of the table
            this.Generate_Table_CAT21(); // We generate the columns of the table
        }


        // READING PROCESS
        public void ReadFile(string filepath)
        {

            byte[] file_in_bytes = File.ReadAllBytes(filepath);
            List<byte[]> bytelist = new List<byte[]>();
            int i = 0;
            int counter = file_in_bytes[2];

            // We generate a list composed by the reading of all the bytes
            while (i < file_in_bytes.Length)
            {
                byte[] array = new byte[counter];
                for (int j = 0; j < array.Length; j++)
                {
                    array[j] = file_in_bytes[i];
                    i++;
                }
                bytelist.Add(array);

                if (i + 2 < file_in_bytes.Length)
                {
                    counter = file_in_bytes[i + 2];
                }
            }

            // Converts this list generated above in an hexadecimal list
            List<string[]> hexadecimallist = new List<string[]>();
            for (int x = 0; x < bytelist.Count; x++)
            {
                byte[] buffer = bytelist[x];
                string[] arrayhex = new string[buffer.Length];
                for (int y = 0; y < buffer.Length; y++)
                {
                    arrayhex[y] = buffer[y].ToString("X");
                }
                hexadecimallist.Add(arrayhex);
            }

            // Here we will work with this hexadecimal list (which are octets in binary) to pass the messages to Categories Classes to be decoded
            for (int q = 0; q < hexadecimallist.Count; q++)
            {
                //  EN VEZ DE 3 hexadecimallist.Count
                string[] arraystring = hexadecimallist[q];
                int CAT = int.Parse(arraystring[0], System.Globalization.NumberStyles.HexNumber);

                if (CAT == 10)
                {
                    CAT10 newcat10 = new CAT10(arraystring, convertor);
                    //CAT10_list.Add(newcat10);
                    ADD_Row_Table_CAT10(newcat10);
                }
                if (CAT == 21)
                {
                    CAT21 newcat21 = new CAT21(arraystring, convertor);
                    //CAT21_list.Add(newcat21);
                    ADD_Row_Table_CAT21(newcat21);

                }
            }
        }

        // TABLE GENERATION
        public void Generate_Table_CAT10()
        {
            CAT10_table.Columns.Add("Category");
            CAT10_table.Columns.Add("SAC");
            CAT10_table.Columns.Add("SIC");
            CAT10_table.Columns.Add("Target_ID");
            CAT10_table.Columns.Add("Track Number");
            CAT10_table.Columns.Add("Target Report Descriptor");
            CAT10_table.Columns.Add("Message Type");
            CAT10_table.Columns.Add("Flight Level");
            CAT10_table.Columns.Add("Time of Day");
            CAT10_table.Columns.Add("Track Status");
            CAT10_table.Columns.Add("Position in WGS-84 Co-ordinates");
            CAT10_table.Columns.Add("Position in Cartesian Co-ordinates");
            CAT10_table.Columns.Add("Position in Polar Co-ordinates");
            CAT10_table.Columns.Add("Track Velocity in Polar Coordinates");
            CAT10_table.Columns.Add("Track Velocity in Cartesian Coordinates");
            CAT10_table.Columns.Add("Target Size & Orientation");
            CAT10_table.Columns.Add("Target Address");
            CAT10_table.Columns.Add("System Status");
            CAT10_table.Columns.Add("Vehicle Fleet Identification");
            CAT10_table.Columns.Add("Pre-programmed Message");
            CAT10_table.Columns.Add("Measured Height");
            CAT10_table.Columns.Add("Mode-3A Code");
            CAT10_table.Columns.Add("Mode S MB Data");
            CAT10_table.Columns.Add("Standard Deviation of Position");
            CAT10_table.Columns.Add("Presence");
            CAT10_table.Columns.Add("Amplitude of Primary Plot");
            CAT10_table.Columns.Add("Calculated Acceleration");
        }

        public void Generate_Table_CAT21()
        {
            CAT21_table.Columns.Add("Category");
            CAT21_table.Columns.Add("SAC");
            CAT21_table.Columns.Add("SIC");
            CAT21_table.Columns.Add("Target Report Descriptor");
            CAT21_table.Columns.Add("Track Number");
            CAT21_table.Columns.Add("Service_ID");
            CAT21_table.Columns.Add("Time of Applicability for Position");
            CAT21_table.Columns.Add("Position in WGS-84 Co-ordinates");
            CAT21_table.Columns.Add("Position in WGS-84 Co-ordinates Hi-Res");
            CAT21_table.Columns.Add("Time of Applicability for Velocity ");
            CAT21_table.Columns.Add("Air Speed");
            CAT21_table.Columns.Add("True Air Speed");
            CAT21_table.Columns.Add("Target Address");
            CAT21_table.Columns.Add("Time of Message Reception of Position");
            CAT21_table.Columns.Add("Time of Message Reception of Position (High Precision)");
            CAT21_table.Columns.Add("Time of Message Reception of Velocity ");
            CAT21_table.Columns.Add("Time of Message Reception of Velocity (High Precision)");
            CAT21_table.Columns.Add("Geometric Height");
            CAT21_table.Columns.Add("Quality Indicators");
            CAT21_table.Columns.Add("MOPS Version");
            CAT21_table.Columns.Add("Mode 3/A Code");
            CAT21_table.Columns.Add("Roll Angle");
            CAT21_table.Columns.Add("Flight Level");
            CAT21_table.Columns.Add("Magnetic Heading");
            CAT21_table.Columns.Add("Target Status");
            CAT21_table.Columns.Add("Barometric Vertical Rate");
            CAT21_table.Columns.Add("Geometric Vertical Rate");
            CAT21_table.Columns.Add("Airborne Ground Vector");
            CAT21_table.Columns.Add("Track Angle Rate");
            CAT21_table.Columns.Add("Time of Report Transmission");
            CAT21_table.Columns.Add("Target_ID");
            CAT21_table.Columns.Add("Emitter Category");
            CAT21_table.Columns.Add("Met Information");
            CAT21_table.Columns.Add("Selected Altitude");
            CAT21_table.Columns.Add("Final State Selected Altitude");
            CAT21_table.Columns.Add("Trajectory Intent");
            CAT21_table.Columns.Add("Service Management");
            CAT21_table.Columns.Add("Aircraft Operational Status");
            CAT21_table.Columns.Add("Surface Capabilities and Characteristics");
            CAT21_table.Columns.Add("Message Amplitude");
            CAT21_table.Columns.Add("Mode S MB Data");
            CAT21_table.Columns.Add("ACAS Resolution Advisory Report");
            CAT21_table.Columns.Add("Receiver ID");
            CAT21_table.Columns.Add("Data Ages");
        }






        // How we will add the information to our table
        public void ADD_Row_Table_CAT10(CAT10 newrow)
        {
            // Excepcions with no data
            if (newrow.CAT == null) { newrow.CAT = "No Data"; }
            if (newrow.SAC == null) { newrow.SAC = "No Data"; }
            if (newrow.SIC == null) { newrow.SIC = "No Data"; }
            if (newrow.Target_ID == null) { newrow.Target_ID = "No Data"; }
            if (newrow.Track_number == null) { newrow.Track_number = "No Data"; }
            if (newrow.TYP == null) { newrow.TYP = "No Data"; }
            if (newrow.DCR == null) { newrow.DCR = "No Data"; }
            if (newrow.CHN == null) { newrow.CHN = "No Data"; }
            if (newrow.GBS == null) { newrow.GBS = "No Data"; }
            if (newrow.CRT == null) { newrow.CRT = "No Data"; }
            if (newrow.MessageTYPE == null) { newrow.MessageTYPE = "No Data"; }
            if (newrow.FL == null) { newrow.FL = "No Data"; }
            if (newrow.Time_of_day_in_format == null) { newrow.Time_of_day_in_format = "No Data"; }
            if (newrow.CNF == null) { newrow.CNF = "No Data"; }
            if (newrow.TRE == null) { newrow.TRE = "No Data"; }
            if (newrow.CST == null) { newrow.CST = "No Data"; }
            if (newrow.MAH == null) { newrow.MAH = "No Data"; }
            if (newrow.TCC == null) { newrow.TCC = "No Data"; }
            if (newrow.STH == null) { newrow.STH = "No Data"; }
            if (newrow.TOM == null) { newrow.TOM = "No Data"; }
            if (newrow.DOU == null) { newrow.DOU = "No Data"; }
            if (newrow.MRS == null) { newrow.MRS = "No Data"; }
            if (newrow.GHO == null) { newrow.GHO = "No Data"; }
            if (newrow.Latitude_WGS84 == null) { newrow.Latitude_WGS84 = "No Data"; }
            if (newrow.Longitude_WGS84 == null) { newrow.Longitude_WGS84 = "No Data"; }
            if (newrow.X_Component == null) { newrow.X_Component = "No Data"; }
            if (newrow.Y_Component == null) { newrow.Y_Component = "No Data"; }
            if (newrow.RHO == null) { newrow.RHO = "No Data"; }
            if (newrow.Theta == null) { newrow.Theta = "No Data"; }
            if (newrow.Ground_Speed == null) { newrow.Ground_Speed = "No Data"; }
            if (newrow.Track_Angle == null) { newrow.Track_Angle = "No Data"; }
            if (newrow.Vx == null) { newrow.Vx = "No Data"; }
            if (newrow.Vy == null) { newrow.Vy = "No Data"; }
            if (newrow.Lenght == null) { newrow.Lenght = "No Data"; }
            if (newrow.Orientation == null) { newrow.Orientation = "No Data"; }
            if (newrow.Width == null) { newrow.Width = "No Data"; }
            if (newrow.TargetAddress == null) { newrow.TargetAddress = "No Data"; }
            if (newrow.NOGO == null) { newrow.NOGO = "No Data"; }
            if (newrow.OVL == null) { newrow.OVL = "No Data"; }
            if (newrow.TSV == null) { newrow.TSV = "No Data"; }
            if (newrow.DIV == null) { newrow.DIV = "No Data"; }
            if (newrow.TTF == null) { newrow.TTF = "No Data"; }
            if (newrow.Vehicle_Fleet_ID == null) { newrow.Vehicle_Fleet_ID = "No Data"; }
            if (newrow.TRB == null) { newrow.TRB = "No Data"; }
            if (newrow.MSG == null) { newrow.MSG = "No Data"; }
            if (newrow.MHeight == null) { newrow.MHeight = "No Data"; }
            if (newrow.V_Mode3A == null) { newrow.V_Mode3A = "No Data"; }
            if (newrow.G_Mode3A == null) { newrow.G_Mode3A = "No Data"; }
            if (newrow.L_Mode3A == null) { newrow.L_Mode3A = "No Data"; }
            if (newrow.Mode3A_reply == null) { newrow.Mode3A_reply = "No Data"; }
            if (newrow.MB_Data_ModeS == null) { this.MB_Data_ModeSmessage = "No Data"; }
            if (newrow.omega_x == null) { newrow.omega_x = "No Data"; }
            if (newrow.omega_y == null) { newrow.omega_y = "No Data"; }
            if (newrow.omega_xy == null) { newrow.omega_xy = "No Data"; }
            if (newrow.REP == 0) { this.REPmessage = "No Data"; }
            if (newrow.PAM == null) { newrow.PAM = "No Data"; }
            if (newrow.Ax == null) { newrow.Ax = "No Data"; }
            if (newrow.Ay == null) { newrow.Ay = "No Data"; }


            // Filling the rows
            DataRow row = CAT10_table.NewRow();
            string nl = Environment.NewLine;
            row[0] = newrow.CAT;
            row[1] = newrow.SAC;
            row[2] = newrow.SIC;
            row[3] = newrow.Target_ID;
            row[4] = newrow.Track_number;
            row[5] = "TYP: " + newrow.TYP + nl + "DCR: " + newrow.DCR + nl + "CHN: " + newrow.CHN + nl + "GBS: " + newrow.GBS + nl + "CRT: " + newrow.CRT;
            row[6] = newrow.MessageTYPE;
            row[7] = newrow.FL;
            row[8] = newrow.Time_of_day_in_format;
            row[9] = "CNF: " + newrow.CNF + nl + "TRE: " + newrow.TRE + nl + "CST: " + newrow.CST + nl + "MAH: " + newrow.MAH + nl + "TCC: " + newrow.TCC + nl + "STH: " + newrow.STH + nl + "TOM: " + newrow.TOM + nl + "DOU: " + newrow.DOU + nl + "MRS: " + newrow.MRS + nl + "GHO: " + newrow.GHO;
            row[10] = "Latitude= " + newrow.Latitude_WGS84 + nl + "Longitude= " + newrow.Longitude_WGS84;
            row[11] = "X= " + newrow.X_Component + nl + "Y= " + newrow.Y_Component;
            row[12] = "RHO= " + newrow.RHO + nl + "THETA= " + newrow.Theta;
            row[13] = "Ground speed= " + newrow.Ground_Speed + nl + "Track Angle= " + newrow.Track_Angle;
            row[14] = "V_x= " + newrow.Vx + nl + "V_y= " + newrow.Vy;
            row[15] = "Lenght: " + newrow.Lenght + nl + "Orientation: " + newrow.Orientation + nl + "Width: " + newrow.Width;
            row[16] = newrow.TargetAddress;
            row[17] = "NOGO:" + newrow.NOGO + nl + "OVL:" + newrow.OVL + nl + "TSV:" + newrow.TSV + nl + "DIV:" + newrow.DIV + nl + "TTF:" + newrow.TTF;
            row[18] = newrow.Vehicle_Fleet_ID;
            row[19] = "TRB:" + newrow.TRB + nl + "MSG:" + newrow.MSG;
            row[20] = newrow.MHeight;
            row[21] = newrow.V_Mode3A + nl + newrow.G_Mode3A + nl + newrow.L_Mode3A + nl + newrow.Mode3A_reply;
            if (newrow.MB_Data_ModeS != null)
                row[22] = newrow.MB_Data_ModeS;
            else
                row[22] = this.MB_Data_ModeSmessage;
            row[23] = "Omega_X:" + newrow.omega_x + nl + "Omega_Y:" + newrow.omega_y + nl + "Omega_XY:" + newrow.omega_y;
            if (newrow.REP != 0)
                row[24] = newrow.REP;
            else
                row[24] = this.REPmessage;
            row[25] = newrow.PAM;
            row[26] = "Ax: " + newrow.Ax + nl + "Ay: " + newrow.Ay;


            // Adding all this new information
            CAT10_table.Rows.Add(row);
        }


        public void ADD_Row_Table_CAT21(CAT21 newrow)
        {
            // Excepcions with no data
            if (newrow.CAT == null) { newrow.CAT = "No Data"; }
            if (newrow.SAC == null) { newrow.SAC = "No Data"; }
            if (newrow.SIC == null) { newrow.SIC = "No Data"; }
            if (newrow.ATP == null) { newrow.ATP = "No Data"; }
            if (newrow.ARC == null) { newrow.ARC = "No Data"; }
            if (newrow.RC == null) { newrow.RC = "No Data"; }
            if (newrow.RAB == null) { newrow.RAB = "No Data"; }
            if (newrow.DCR == null) { newrow.DCR = "No Data"; }
            if (newrow.GBS == null) { newrow.GBS = "No Data"; }
            if (newrow.SIM == null) { newrow.SIM = "No Data"; }
            if (newrow.TST == null) { newrow.TST = "No Data"; }
            if (newrow.SAA == null) { newrow.SAA = "No Data"; }
            if (newrow.CA == null) { newrow.CA = "No Data"; }
            if (newrow.Track_number == null) { newrow.Track_number = "No Data"; }
            if (newrow.Service_identification == null) { newrow.Service_identification = "No Data"; }
            if (newrow.ToAfP == null) { newrow.ToAfP = "No Data"; }
            if (newrow.Latitude_WGS84 == null) { newrow.Latitude_WGS84 = "No Data"; }
            if (newrow.Longitude_WGS84 == null) { newrow.Longitude_WGS84 = "No Data"; }
            if (newrow.Latitude_WGS84_HI_RES == null) { newrow.Latitude_WGS84_HI_RES = "No Data"; }
            if (newrow.Longitude_WGS84_HI_RES == null) { newrow.Longitude_WGS84_HI_RES = "No Data"; }
            if (newrow.ToAfV == null) { newrow.ToAfV = "No Data"; }
            if (newrow.IM == null) { newrow.IM = "No Data"; }
            if (newrow.RE == null) { newrow.RE = "No Data"; }
            if (newrow.TargetAddress == null) { newrow.TargetAddress = "No Data"; }
            if (newrow.ToMRfP == null) { newrow.ToMRfP = "No Data"; }
            if (newrow.ToMRfP_HI_RES == null) { newrow.ToMRfP_HI_RES = "No Data"; }
            if (newrow.ToMRfV == null) { newrow.ToMRfV = "No Data"; }
            if (newrow.ToMRfV_HI_RES == null) { newrow.ToMRfV_HI_RES = "No Data"; }
            if (newrow.GeometricHeight == null) { newrow.GeometricHeight = "No Data"; }
            if (newrow.QualityIndicators == null) { newrow.QualityIndicators = "No Data"; }
            if (newrow.VNS == null) { newrow.VNS = "No Data"; }
            if (newrow.VN == null) { newrow.VN = "No Data"; }
            if (newrow.LTT == null) { newrow.LTT = "No Data"; }
            if (newrow.M3AC == null) { newrow.M3AC = "No Data"; }
            if (newrow.RollAngle == null) { newrow.RollAngle = "No Data"; }
            if (newrow.FlightLevel == null) { newrow.FlightLevel = "No Data"; }
            if (newrow.MagneticHeading == null) { newrow.MagneticHeading = "No Data"; }
            if (newrow.ICF == null) { newrow.ICF = "No Data"; }
            if (newrow.LNAV == null) { newrow.LNAV = "No Data"; }
            if (newrow.PS == null) { newrow.PS = "No Data"; }
            if (newrow.SS == null) { newrow.SS = "No Data"; }
            if (newrow.BarometricVerticalRate == null) { newrow.BarometricVerticalRate = "No Data"; }
            if (newrow.GeometricVerticalRate == null) { newrow.GeometricVerticalRate = "No Data"; }
            if (newrow.Ground_Speed == null) { newrow.Ground_Speed = "No Data"; }
            if (newrow.Track_Angle == null) { newrow.Track_Angle = "No Data"; }
            if (newrow.TAR == null) { newrow.TAR = "No Data"; }
            if (newrow.Tort == null) { newrow.Tort = "No Data"; }
            if (newrow.Target_ID == null) { newrow.Target_ID = "No Data"; }
            if (newrow.ECAT == null) { newrow.ECAT = "No Data"; }
            if (newrow.WS == null) { newrow.WS = "No Data"; }
            if (newrow.WD == null) { newrow.WD = "No Data"; }
            if (newrow.TMP == null) { newrow.TMP = "No Data"; }
            if (newrow.TRB == null) { newrow.TRB = "No Data"; }
            if (newrow.SAS == null) { newrow.SAS = "No Data"; }
            if (newrow.Source == null) { newrow.Source = "No Data"; }
            if (newrow.Altitude == null) { newrow.Altitude = "No Data"; }
            if (newrow.MV == null) { newrow.MV = "No Data"; }
            if (newrow.AH == null) { newrow.AH = "No Data"; }
            if (newrow.AM == null) { newrow.AM = "No Data"; }
            if (newrow.AltitudeF == null) { newrow.AltitudeF = "No Data"; }
            if (newrow.TIS == null) { newrow.TIS = "No Data"; }
            if (newrow.TID == null) { newrow.TID = "No Data"; }
            if (newrow.NAV == null) { newrow.NAV = "No Data"; }
            if (newrow.NVB == null) { newrow.NVB = "No Data"; }
            if (newrow.REP1 == null) { newrow.REP1 = "No Data"; }
            if (newrow.TCA == null) { newrow.TCA = "No Data"; }
            if (newrow.NC == null) { newrow.NC = "No Data"; }
            if (newrow.TCPNumber == null) { newrow.TCPNumber = "No Data"; }
            if (newrow.AltitudeTID == null) { newrow.AltitudeTID = "No Data"; }
            if (newrow.LATITUDE == null) { newrow.LATITUDE = "No Data"; }
            if (newrow.LONGITUDE == null) { newrow.LONGITUDE = "No Data"; }
            if (newrow.PT == null) { newrow.PT = "No Data"; }
            if (newrow.TD == null) { newrow.TD = "No Data"; }
            if (newrow.TRA == null) { newrow.TRA = "No Data"; }
            if (newrow.TOA == null) { newrow.TOA = "No Data"; }
            if (newrow.TOV == null) { newrow.TOV = "No Data"; }
            if (newrow.TTR == null) { newrow.TTR = "No Data"; }
            if (newrow.RP == null) { newrow.RP = "No Data"; }
            if (newrow.RA_Status == null) { newrow.RA_Status = "No Data"; }
            if (newrow.TC_Status == null) { newrow.TC_Status = "No Data"; }
            if (newrow.TS_Status == null) { newrow.TS_Status = "No Data"; }
            if (newrow.ARV_Status == null) { newrow.ARV_Status = "No Data"; }
            if (newrow.CDTI_A_Status == null) { newrow.CDTI_A_Status = "No Data"; }
            if (newrow.not_TCAS_Status == null) { newrow.not_TCAS_Status = "No Data"; }
            if (newrow.SA_Status == null) { newrow.SA_Status = "No Data"; }
            if (newrow.POA == null) { newrow.POA = "No Data"; }
            if (newrow.CDTIS == null) { newrow.CDTIS = "No Data"; }
            if (newrow.B2low == null) { newrow.B2low = "No Data"; }
            if (newrow.RAS == null) { newrow.RAS = "No Data"; }
            if (newrow.IDENT == null) { newrow.IDENT = "No Data"; }
            if (newrow.LW == null) { newrow.LW = "No Data"; }
            if (newrow.MAM == null) { newrow.MAM = "No Data"; }
            if (newrow.ModeSMBData == null) { newrow.ModeSMBData = "No Data"; }
            if (newrow.TYP == null) { newrow.TYP = "No Data"; }
            if (newrow.STYP == null) { newrow.STYP = "No Data"; }
            if (newrow.ARA == null) { newrow.ARA = "No Data"; }
            if (newrow.RAC == null) { newrow.RAC = "No Data"; }
            if (newrow.RAT == null) { newrow.RAT = "No Data"; }
            if (newrow.MTE == null) { newrow.MTE = "No Data"; }
            if (newrow.TTI == null) { newrow.TTI = "No Data"; }
            if (newrow.TID_acas == null) { newrow.TID_acas = "No Data"; }
            if (newrow.RID == null) { newrow.RID = "No Data"; }
            if (newrow.AOS == null) { newrow.AOS = "No Data"; }
            if (newrow.TRD == null) { newrow.TRD = "No Data"; }
            if (newrow.M3A == null) { newrow.M3A = "No Data"; }
            if (newrow.QI == null) { newrow.QI = "No Data"; }
            if (newrow.TI == null) { newrow.TI = "No Data"; }
            if (newrow.MAM_ages == null) { newrow.MAM_ages = "No Data"; }
            if (newrow.GH == null) { newrow.GH = "No Data"; }
            if (newrow.FL_ages == null) { newrow.FL_ages = "No Data"; }
            if (newrow.ISA == null) { newrow.ISA = "No Data"; }
            if (newrow.FSA == null) { newrow.FSA = "No Data"; }
            if (newrow.AS == null) { newrow.AS = "No Data"; }
            if (newrow.TAS == null) { newrow.TAS = "No Data"; }
            if (newrow.MH == null) { newrow.MH = "No Data"; }
            if (newrow.MVR == null) { newrow.MVR = "No Data"; }
            if (newrow.GVR == null) { newrow.GVR = "No Data"; }
            if (newrow.GV == null) { newrow.GV = "No Data"; }
            if (newrow.TAR_ages == null) { newrow.TAR_ages = "No Data"; }
            if (newrow.TI_ages == null) { newrow.TI_ages = "No Data"; }
            if (newrow.TS_ages == null) { newrow.TS_ages = "No Data"; }
            if (newrow.MET == null) { newrow.MET = "No Data"; }
            if (newrow.ROA == null) { newrow.ROA = "No Data"; }
            if (newrow.ARA_ages == null) { newrow.ARA_ages = "No Data"; }
            if (newrow.SCC == null) { newrow.SCC = "No Data"; }


            // Filling the rows
            DataRow row = CAT21_table.NewRow();
            string nl = Environment.NewLine;
            row[0] = newrow.CAT;
            row[1] = newrow.SAC;
            row[2] = newrow.SIC;
            row[3] = "ATP: " + newrow.ATP + nl + "ARC: " + newrow.ARC + nl + "RC: " + newrow.RC + nl + "RAB: " + newrow.RAB + nl + "DCR: " + newrow.DCR + nl + "GBS: " + newrow.GBS + nl + "SIM: " + newrow.SIM + nl + "TST: " + newrow.TST + nl + "SAA: " + newrow.SAA + nl + "CA: " + newrow.CA;
            row[4] = newrow.Track_number;
            row[5] = newrow.Service_identification;
            row[6] = newrow.ToAfP;
            row[7] = "Latitude= " + newrow.Latitude_WGS84 + nl + "Longitude= " + newrow.Longitude_WGS84;
            row[8] = "Latitude= " + newrow.Latitude_WGS84_HI_RES + nl + "Longitude= " + newrow.Longitude_WGS84_HI_RES;
            row[9] = newrow.ToAfV;
            row[10] = newrow.IM;
            row[11] = newrow.RE;
            row[12] = newrow.TargetAddress;
            row[13] = newrow.ToMRfP;
            row[14] = newrow.ToMRfP_HI_RES;
            row[15] = newrow.ToMRfV;
            row[16] = newrow.ToMRfV_HI_RES;
            row[17] = newrow.GeometricHeight;
            row[18] = newrow.QualityIndicators;
            row[19] = "VNS: " + newrow.VNS + nl + "VN: " + newrow.VN + nl + "LTT: " + newrow.LTT;
            row[20] = newrow.M3AC;
            row[21] = newrow.RollAngle;
            row[22] = newrow.FlightLevel;
            row[23] = newrow.MagneticHeading;
            row[24] = "ICF: " + newrow.ICF + nl + "LNAV: " + newrow.LNAV + nl + "PS: " + newrow.PS + nl + "SS: " + newrow.SS;
            row[25] = newrow.BarometricVerticalRate;
            row[26] = newrow.GeometricVerticalRate;
            row[27] = newrow.Ground_Speed + nl + newrow.Track_Angle;
            row[28] = newrow.TAR;
            row[29] = newrow.Tort;
            row[30] = newrow.Target_ID;
            row[31] = newrow.ECAT;
            row[32] = "WS: " + newrow.WS + nl + "WD: " + newrow.WD + nl + "TMP: " + newrow.TMP + nl + "TRB: " + newrow.TRB;
            row[33] = "SAS: " + newrow.SAS + nl + "Source: " + newrow.Source + nl + "Altitude: " + newrow.Altitude;
            row[34] = "MV: " + newrow.MV + nl + "AH: " + newrow.AH + nl + "AM: " + newrow.AM + nl + "Final Altitude: " + newrow.AltitudeF;
            row[35] = "TIS: " + newrow.TIS + nl + "TID: " + newrow.TID + nl + "NAV: " + newrow.NAV + nl + "NVB: " + newrow.NVB + nl + "REP: " + newrow.REP1 + nl + "TCA: " + newrow.TCA + nl + "NC: " + newrow.NC + nl + "TCP Number: " + newrow.TCPNumber + nl + "Altitude: " + newrow.AltitudeTID + nl + "Latitude: " + newrow.LATITUDE + nl + "Longitude: " + newrow.LONGITUDE + nl + "Point Type: " + newrow.PT + nl + "TD: " + newrow.TD + nl + "TRA: " + newrow.TRA + nl + "TOA: " + newrow.TOA + nl + "TOV: " + newrow.TOV + nl + "TTR: " + newrow.TTR;
            row[36] = "RA: " + newrow.RA_Status + nl + "TC: " + newrow.TC_Status + nl + "TS: " + newrow.TS_Status + nl + "ARV: " + newrow.ARV_Status + nl + "CDTI/A: " + newrow.CDTI_A_Status + nl + "not TCAS: " + newrow.not_TCAS_Status + nl + "SA: " + newrow.SA_Status;
            row[37] = "POA: " + newrow.POA + nl + "CDTIS: " + newrow.CDTIS + nl + "B2LOW: " + newrow.B2low + nl + "RAS: " + newrow.RAS + nl + "IDENT: " + newrow.IDENT + nl + "LW: " + newrow.LW;
            row[38] = newrow.MAM;
            row[39] = newrow.ModeSMBData;
            row[40] = "TYP: " + newrow.TYP + nl + "STYP: " + newrow.STYP + nl + "ARA: " + newrow.ARA + nl + "RAC: " + newrow.RAC + nl + "RAT: " + newrow.RAT + nl + "MTE: " + newrow.MTE + nl + "TTI: " + newrow.TTI + nl + "TID: " + newrow.TID_acas;
            row[41] = newrow.RID;
            row[42] = "AOS: " + newrow.AOS + nl + "TRD: " + newrow.TRD + nl + "M3A: " + newrow.M3A + nl + "QI: " + newrow.QI + nl + "TI: " + newrow.TI + nl + "MAM: " + newrow.MAM_ages + nl + "GH: " + newrow.GH + nl + "FL: " + newrow.FL_ages + nl + "ISA: " + newrow.ISA + nl + "FSA: " + newrow.FSA + nl + "AS: " + newrow.AS + nl + "TAS: " + newrow.TAS + nl + "MH: " + newrow.MH + nl + "MVR: " + newrow.MVR + nl + "GVR: " + newrow.GVR + nl + "GV: " + newrow.GV + nl + "TAR: " + newrow.TAR + nl + "TI: " + newrow.TI + nl + "TS: " + newrow.TS + nl + "MET: " + newrow.MET + nl + "ROA: " + newrow.ROA + nl + "ARA: " + newrow.ARA + nl + "SCC: " + newrow.SCC;


            CAT21_table.Rows.Add(row);
        }
    }
}
