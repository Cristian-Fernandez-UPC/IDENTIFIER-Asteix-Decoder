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
        
        public DataTable getTableCAT10()
        {
            return CAT10_table;
        }


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
            for (int q = 0; q < 10; q++)
            {
                //  EN VEZ DE 3 hexadecimallist.Count
                string[] arraystring = hexadecimallist[q];
                int CAT = int.Parse(arraystring[0], System.Globalization.NumberStyles.HexNumber);

                // We are interested only in the category 10 at thi moment...
                if (CAT == 10)
                {
                    CAT10 newcat10 = new CAT10(arraystring, convertor);
                    
                    CAT10_list.Add(newcat10);
                    this.ADD_Row_Table_CAT10(newcat10);
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
            CAT10_table.Columns.Add("Track Number");
            CAT10_table.Columns.Add("Target Report Description");
        }

        // How we will add the information to our table
        public void ADD_Row_Table_CAT10(CAT10 newrow)
        {
            if (newrow.CAT == null) { newrow.CAT = "No Data"; }
            if (newrow.SAC == null) { newrow.SAC = "No Data"; }
            if (newrow.SIC == null) { newrow.SIC = "No Data"; }
            if (newrow.Track_number == null) { newrow.Track_number = "No Data"; }
            if (newrow.TYP == null) { newrow.TYP = "No Data"; }
            if (newrow.DCR == null) { newrow.DCR = "No Data"; }
            if (newrow.CHN == null) { newrow.CHN = "No Data"; }
            if (newrow.GBS == null) { newrow.GBS = "No Data"; }
            if (newrow.CRT == null) { newrow.CRT = "No Data"; }

            DataRow row = CAT10_table.NewRow();
            row[0] = newrow.CAT;
            row[1] = newrow.SAC;
            row[2] = newrow.SIC;
            row[3] = newrow.Track_number;
            row[4] = "TYP:" + newrow.TYP + "\n" + "DCR:" + newrow.DCR + "\n" + "CHN:" + newrow.CHN + "\n" + "GBS:" + newrow.GBS + "\n" + "CRT:" + newrow.CRT;

            CAT10_table.Rows.Add(row);
            CAT10_table.AcceptChanges();



        }
    }
}
