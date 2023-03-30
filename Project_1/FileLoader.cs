using FontAwesome.Sharp;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Project_1
{
    public partial class FileLoader : Form
    {
        public static Color color1 = Color.FromArgb(52, 192, 215);
        public const string AstFileFilter = "Archivos AST (*.ast)|*.ast";
        FileReader read = new FileReader();
        LoadingScreen loadingscreen = new LoadingScreen();
        public string FILE_PATH;

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
            
        }

        public string GetFilePath()
        {
            return this.FILE_PATH;
        }



        public FileReader GetFileReaded()
        {
            return read;
        }


        public DataTable GetDataTable()
        {
            return read.getTableCAT10();
        }

        private static void AsterixDecoder(string filePath)
        {
            // decodificar el archivo .ast
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }

        private void iconPictureBox1_MouseEnter(object sender, EventArgs e)
        {
            iconPictureBox1.IconColor = color1;
        }

        private void iconPictureBox1_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox1.IconColor = Color.White;
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = AstFileFilter;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                textBox1.Text = Path.GetFileName(filePath);
                this.FILE_PATH = filePath;
                loadingscreen.TopMost = true;
                loadingscreen.Show();
                read.ReadFile(filePath); // Leemos el fichero
                loadingscreen.Close();
                this.dataGridView1.DataSource = read.getTableCAT10();
                this.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;



                //this.Data_Inspector.Load_Data_Grid_View(this.dataGridView1);


            }
        }
    }
}
