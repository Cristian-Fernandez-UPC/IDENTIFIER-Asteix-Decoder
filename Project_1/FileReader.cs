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
        public List<CAT10> CAT10_list = new List<CAT10>();    // We create a list where all CAT10 messages will be
        public List<CAT10> getListCAT10()
        {
            return CAT10_list;
        }
        Conversions convertor = new Conversions();

        // TABLE
        public DataTable CAT10_table = new DataTable();       // We create a table that will be fulfilled with all the data needed
        public DataGridView CAT10_data = new DataGridView(); 
        public DataGridView getDataGridView()
        {
            this.CAT10_data.DataSource = this.CAT10_table;
            
            return this.CAT10_data;
        }

        public DataTable getTableCAT10()
        {
            return CAT10_table;
        }

        public string REPmessage;
        public string MB_Data_ModeSmessage;

        // READING PROCESS
        public void ReadFile(string filepath)
        {
            this.Generate_Table_CAT10(); // We generate the columns of the table

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

                // We are interested only in the category 10 at thi moment...
                if (CAT == 10)
                {
                    CAT10 newcat10 = new CAT10(arraystring, convertor);
                    
                    CAT10_list.Add(newcat10);
                    ADD_Row_Table_CAT10(newcat10);
                }
                // Elif for the following categories
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
            row[9] = "CNF: " + newrow.CNF + "\n" + "TRE: " + newrow.TRE + "\n" + "CST: " + newrow.CST + "\n" + "MAH: " + newrow.MAH + "\n" + "TCC: " + newrow.TCC + "\n" + "STH: " + newrow.STH + "\n" + "TOM: " + newrow.TOM + "\n" + "DOU: " + newrow.DOU + "\n" + "MRS: " + newrow.MRS + "\n" + "GHO: " + newrow.GHO;
            row[10] = "Latitude= " + newrow.Latitude_WGS84 + "\n" + "Longitude= " + newrow.Longitude_WGS84;
            row[11] = "X= " + newrow.X_Component + "\n" + "Y= " + newrow.Y_Component;
            row[12] = "RHO= " + newrow.RHO+ "\n" + "THETA= " + newrow.Theta;
            row[13] = "Ground speed= " + newrow.Ground_Speed + "\n" + "Track Angle= " + newrow.Track_Angle;
            row[14] = "V_x= " + newrow.Vx+ "\n" + "V_y= " + newrow.Vy;
            row[15] = "Lenght: " + newrow.Lenght + "\n" + "Orientation: " + newrow.Orientation + "\n" + "Width: " + newrow.Width;
            row[16] = newrow.TargetAddress;
            row[17] = "NOGO:" + newrow.NOGO + "\n" + "OVL:" + newrow.OVL + "\n" + "TSV:" + newrow.TSV + "\n" + "DIV:" + newrow.DIV + "\n" + "TTF:" + newrow.TTF;
            row[18] = newrow.Vehicle_Fleet_ID;
            row[19] = "TRB:" + newrow.TRB + "\n" + "MSG:" + newrow.MSG;
            row[20] = newrow.MHeight;
            row[21] = newrow.V_Mode3A + "\n" + newrow.G_Mode3A + "\n" + newrow.L_Mode3A + "\n" + newrow.Mode3A_reply;
            if (newrow.MB_Data_ModeS != null)
                row[22] = newrow.MB_Data_ModeS;
            else
                row[22] = this.MB_Data_ModeSmessage;
            row[23] = "Omega_X:" + newrow.omega_x + "\n" + "Omega_Y:" + newrow.omega_y + "\n" + "Omega_XY:" + newrow.omega_y;
            if (newrow.REP != 0)
                row[24] = newrow.REP;
            else
                row[24] = this.REPmessage;
            row[25] = newrow.PAM;
            row[26] = "Ax: " + newrow.Ax + "\n" + "Ay: " + newrow.Ay;


            

            // Adding all this new information
            CAT10_table.Rows.Add(row);
            //CAT10_table.AcceptChanges();



        }
    }
}
