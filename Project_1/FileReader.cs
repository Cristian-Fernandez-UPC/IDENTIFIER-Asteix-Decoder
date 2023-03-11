using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;

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


        // TABLE
        public DataTable CAT10_table = new DataTable();       // We create a table that will be fulfilled with all the data needed
        public DataTable getTablaCAT10()
        {
            return CAT10_table;
        }


        // File
        string file_path;
        public FileReader(string name)
        {
            this.file_path = name;
        }

        
        // READING PROCESS
        public void ReadFile()
        {
            byte[] file_in_bytes = File.ReadAllBytes(file_path);
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
                string[] arraystring = hexadecimallist[q];
                int CAT = int.Parse(arraystring[0], System.Globalization.NumberStyles.HexNumber);

                // We are interested only in the category 10 at thi moment...
                if (CAT == 10)
                {
                    CAT10 newcat10 = new CAT10(arraystring);
                    CAT10_list.Add(newcat10);
                }
                // Elif for the following categories
            }
        }

        // TABLE GENERATION
        public void Generate_Table_CAT10()
        {
            CAT10_table.Columns.Add("CAT number");
            CAT10_table.Columns.Add("Category");
            CAT10_table.Columns.Add("SAC");
            CAT10_table.Columns.Add("SIC");
            CAT10_table.Columns.Add("Track Number");
        }

        // How we will add the information to our table
        public void ADD_Row_Table_CAT10(CAT10 message)
        {
            var row = CAT10_table.NewRow();

            // MODIFICAR!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            row["Number"] = Message.num;
            row["CAT number"] = Message.cat10num;
            if (Message.CAT != null) { row["Category"] = Message.CAT; }
            else { row["Category"] = "No Data"; }
            if (Message.SAC != null) { row["SAC"] = Message.SAC; }
            else { row["SAC"] = "No Data"; }
            if (Message.SIC != null) { row["SIC"] = Message.SIC; }
            else { row["SIC"] = "No Data"; }
            if (Message.Track_Number != null) { row["Track\nNumber"] = Message.Track_Number; }
            else { row["Track\nNumber"] = "No Data"; }
        }
    }
}
