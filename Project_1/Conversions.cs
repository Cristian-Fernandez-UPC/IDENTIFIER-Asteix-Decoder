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
                return string.Concat('0', hexamessage);
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
            int decimalNumber = Convert.ToInt32(binarynumber, 2);

            // Invert all the bits in the binary number
            int invertedNumber = ~decimalNumber;

            // Add 1 to the inverted number
            int twosComplement = invertedNumber + 1;

            // Convert the result back to binary
            string binaryTwosComplement = Convert.ToString(twosComplement, 2);

            return binaryTwosComplement;
        }
    }
}
