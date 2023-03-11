using FontAwesome.Sharp;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_1
{
    public partial class FileLoader : Form
    {
        new DataInspectorInterface TABLE = new DataInspectorInterface();
        public const string AstFileFilter = "Archivos AST (*.ast)|*.ast";
        FileReader read = new FileReader();

        public FileLoader()
        {
            InitializeComponent();
            openFileDialog1.Filter = AstFileFilter;
            
        }

        private void FileLoader_Load(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = AstFileFilter;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                textBox1.Text = filePath;

                read.ReadFile(filePath); // Leemos el fichero
                TABLE.Load_Data_To_Table(read.getTableCAT10());
                
            }
        }


        private static void AsterixDecoder(string filePath)
        {
            // decodificar el archivo .ast
        }
    }
}
