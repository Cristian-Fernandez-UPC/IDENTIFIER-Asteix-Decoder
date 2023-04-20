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
using System.Runtime.ExceptionServices;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Controls;
using Panel = System.Windows.Forms.Panel;
using System.ComponentModel;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using PrintDialog = System.Windows.Forms.PrintDialog;

namespace Project_1
{
    public partial class FileLoader : Form
    {
        public static Color color1 = Color.FromArgb(52, 192, 215);
        public const string AstFileFilter = "Archivos AST (*.ast)|*.ast";
        FileReader read = new FileReader();
        FilterException exception = new FilterException();
        public string FILE_PATH;
        int previous_case = 0;
        int previous_case2 = 0;
        int file_loaded = 0;


        private void RoundPanelCorners(Panel panel, int radius)
        {
            GraphicsPath panelPath = new GraphicsPath();
            panelPath.StartFigure();
            panelPath.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            panelPath.AddLine(radius, 0, panel.Width - radius, 0);
            panelPath.AddArc(new Rectangle(panel.Width - radius, 0, radius, radius), -90, 90);
            panelPath.AddLine(panel.Width, radius, panel.Width, panel.Height - radius);
            panelPath.AddArc(new Rectangle(panel.Width - radius, panel.Height - radius, radius, radius), 0, 90);
            panelPath.AddLine(panel.Width - radius, panel.Height, radius, panel.Height);
            panelPath.AddArc(new Rectangle(0, panel.Height - radius, radius, radius), 90, 90);
            panelPath.CloseFigure();

            panel.Region = new Region(panelPath);
        }


        public FileLoader()
        {
            InitializeComponent();
            openFileDialog1.Filter = AstFileFilter;
            RoundPanelCorners(panel3, 20);
            textBox1.Text = "Enter an ID";
        }



        private void FileLoader_Load(object sender, EventArgs e)
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
            LoadingScreen loadingscreen = new LoadingScreen();
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = AstFileFilter;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                label2.Text = Path.GetFileName(filePath);
                this.FILE_PATH = filePath;
                //loadingscreen.TopMost = true;
                
                loadingscreen.Show();
                loadingscreen.BringToFront();

                int i = 0;
                while (i == 0)
                {
                    loadingscreen.Refresh();
                    read.ReadFile(filePath); // Leemos el fichero
                    label4.Text = "Loaded Successfully";
                    this.file_loaded = 1;
                    i = 1;
                }


                this.dataGridView1.DataSource = read.getTableCAT10();
                this.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                
                toggleButton1.Checked = true;
                toggleButton2.Checked = true;

                loadingscreen.Close();


                //this.Data_Inspector.Load_Data_Grid_View(this.dataGridView1);


            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dataView = new DataView(read.getTableCAT10());
            if (e.KeyCode == Keys.Enter)
            {
                if(this.file_loaded == 1)
                {
                    try
                    {
                        dataView.RowFilter = $"Target_ID LIKE '%{textBox1.Text}%'";
                        dataGridView1.DataSource = dataView;
                        togglesrestart();

                    }
                    catch
                    {
                        textBox1.Text = "No Valid ID";
                    }
                }
                else
                {
                    textBox1.Text = "Enter an ID";
                    exception.ShowDialog();
                }
            }
        }
        

        private void toggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.file_loaded == 1)
            {
                textBox1.Text = "Enter an ID";
                DataView dataView = new DataView(read.getTableCAT10());
                if (this.previous_case == 0)
                {
                    if (toggleButton2.Checked==false)
                    {
                        dataView.RowFilter = $"SIC = '{7}'";
                        dataGridView1.DataSource = dataView;
                    }
                    if (toggleButton2.Checked == true)
                    {
                        dataView.RowFilter = $"SIC = '{7}' OR SIC = '{107}'";
                        dataGridView1.DataSource = dataView;
                    }
                    this.previous_case = 1;
                }
                else
                {
                    if(toggleButton2.Checked == false)
                    {
                        dataGridView1.DataSource = read.getTableCAT10();
                    }
                    if (toggleButton2.Checked == true)
                    {
                        dataView.RowFilter = $"SIC = '{107}'";
                        dataGridView1.DataSource = dataView;
                    }
                    this.previous_case = 0;
                }
            }
            else
            {
                toggleButton1.CheckedChanged -= toggleButton1_CheckedChanged;
                toggleButton1.Checked = false;
                exception.ShowDialog();
                toggleButton1.CheckedChanged += toggleButton1_CheckedChanged;
            }
        }

        private void toggleButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.file_loaded == 1)
            {
                textBox1.Text = "Enter an ID";
                DataView dataView = new DataView(read.getTableCAT10());
                if (this.previous_case2 == 0)
                {
                    if (toggleButton1.Checked == false)
                    {
                        dataView.RowFilter = $"SIC = '{107}'";
                        dataGridView1.DataSource = dataView;
                    }
                    if (toggleButton1.Checked == true)
                    {
                        dataView.RowFilter = $"SIC = '{7}' OR SIC = '{107}'";
                        dataGridView1.DataSource = dataView;
                    }
                    this.previous_case2 = 1;
                }
                else
                {
                    if (toggleButton1.Checked == false)
                    {
                        dataGridView1.DataSource = read.getTableCAT10();
                    }
                    if (toggleButton1.Checked == true)
                    {
                        dataView.RowFilter = $"SIC = '{7}'";
                        dataGridView1.DataSource = dataView;
                    }
                    this.previous_case2 = 0;
                }
            }
            else
            {
                toggleButton2.CheckedChanged -= toggleButton2_CheckedChanged;
                toggleButton2.Checked = false;
                exception.ShowDialog();
                toggleButton2.CheckedChanged += toggleButton2_CheckedChanged;
            }
        }


        private void toggleButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            toggleButton1.Checked = false;
            toggleButton2.Checked = false;
            this.dataGridView1.DataSource = read.getTableCAT21();
            this.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }





        private void iconPictureBox4_MouseEnter(object sender, EventArgs e)
        {
            iconPictureBox4.IconColor = color1;
        }

        private void iconPictureBox4_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox4.IconColor = Color.White;
        }

        private void iconPictureBox4_Click(object sender, EventArgs e)
        {
            if (this.file_loaded == 1)
            {
                //togglesrestart();

                if (toggleButton4.Checked == true && toggleButton5.Checked == false)
                {
                    SaveFileDialog sfd = new SaveFileDialog() { Filter = "Archivo CSV|*.csv" };
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        List<string> filas = new List<string>();
                        List<string> cabeceras = new List<string>();
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            cabeceras.Add(col.HeaderText);
                        }
                        string SEP = "\t";
                        filas.Add(string.Join(SEP, cabeceras));

                        foreach (DataGridViewRow fila in dataGridView1.Rows)
                        {
                            try
                            {
                                List<string> celdas = new List<string>();
                                foreach (DataGridViewCell c in fila.Cells)
                                    celdas.Add(c.Value.ToString().Replace("\r\n", ","));

                                filas.Add(string.Join(SEP, celdas));
                            }
                            catch (Exception ex) { }
                        }

                        File.WriteAllLines(sfd.FileName, filas);
                        MessageBox.Show("CSV file saved successfully!");
                    }
                }
                if (toggleButton4.Checked == false && toggleButton5.Checked == true)
                {

                    // Create a PrintDialog and show it to the user
                    PrintDialog printDialog = new PrintDialog();
                    if (printDialog.ShowDialog() != DialogResult.OK)
                        return;

                    // Create a PrintDocument and set its properties
                    System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                    printDocument.DocumentName = "DataGridView Print";
                    printDocument.DefaultPageSettings.Landscape = true;
                    printDocument.PrinterSettings = printDialog.PrinterSettings;

                    // Handle the PrintPage event of the PrintDocument
                    printDocument.PrintPage += (s, pe) =>
                    {
                        // Define the margin and spacing for the header and footer
                        int headerMargin = 50;
                        int footerMargin = 30;
                        int footerSpacing = 10;

                        // Define the height of the header and footer
                        int headerHeight = (int)headerFont.GetHeight();
                        int footerHeight = (int)footerFont.GetHeight() + footerSpacing;

                        // Define the rectangle for the header
                        Rectangle headerRect = new Rectangle(pe.MarginBounds.Left, pe.MarginBounds.Top, pe.MarginBounds.Width, headerHeight);

                        // Define the rectangle for the footer
                        Rectangle footerRect = new Rectangle(pe.MarginBounds.Left, pe.MarginBounds.Bottom - footerHeight, pe.MarginBounds.Width, footerHeight);

                        // Draw the header text
                        pe.Graphics.DrawString("Header Text", headerFont, Brushes.Black, headerRect);

                        // Draw the footer text
                        pe.Graphics.DrawString("Footer Text", footerFont, Brushes.Black, footerRect);

                        // Define the rectangle for the DataGridView
                        Rectangle dgvRect = new Rectangle(pe.MarginBounds.Left, headerRect.Bottom + headerMargin, pe.MarginBounds.Width, footerRect.Top - headerRect.Bottom - headerMargin);

                        // Draw the DataGridView
                        dataGridView1.DrawToBitmap(new Bitmap(dataGridView1.Width, dataGridView1.Height), dgvRect);
                    };

                    // Show a SaveFileDialog to the user and save the document
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        printDocument.PrinterSettings.PrintToFile = true;
                        printDocument.PrinterSettings.PrintFileName = saveFileDialog.FileName;
                        printDocument.Print();
                    }
                }
                if(toggleButton4.Checked == false && toggleButton5.Checked == false)
                {
                    MessageBox.Show("Please select the output format");
                }
                if(toggleButton4.Checked == true && toggleButton5.Checked == true)
                {
                    MessageBox.Show("Please select only one format (.csv or .xml)");
                }

            }
            else
            {
                toggleButton4.Checked = false;
                toggleButton5.Checked = false;
                exception.ShowDialog();
            }
        }

        private void iconPictureBox3_MouseEnter(object sender, EventArgs e)
        {
            iconPictureBox3.IconColor = color1;
        }

        private void iconPictureBox5_MouseEnter(object sender, EventArgs e)
        {
            iconPictureBox5.IconColor = color1;
        }

        private void iconPictureBox6_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox6.IconColor = Color.White;
        }

        private void iconPictureBox6_MouseEnter(object sender, EventArgs e)
        {
            iconPictureBox6.IconColor = color1;
        }

        private void iconPictureBox5_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox5.IconColor = Color.White;
        }

        private void iconPictureBox3_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox3.IconColor = Color.White;
        }

        private void iconPictureBox3_Click(object sender, EventArgs e)
        {
            if (this.file_loaded == 1)
            {
                dataGridView1.DataSource = read.getTableCAT10();
                togglesrestart();
                toggleButton1.Checked = true;
                toggleButton2.Checked = true;

            }
            else
            {
                exception.ShowDialog();
            }
        }

        public void togglesrestart()
        {
            toggleButton1.CheckedChanged -= toggleButton1_CheckedChanged;
            toggleButton1.Checked = false;
            toggleButton1.CheckedChanged += toggleButton1_CheckedChanged;
            toggleButton2.CheckedChanged -= toggleButton2_CheckedChanged;
            toggleButton2.Checked = false;
            toggleButton2.CheckedChanged += toggleButton2_CheckedChanged;
            //toggleButton3.CheckedChanged -= toggleButton3_CheckedChanged;
            //toggleButton3.Checked = false;
            //toggleButton3.CheckedChanged += toggleButton3_CheckedChanged;
            this.previous_case = 0;
            this.previous_case2 = 0;
        }

        private void iconPictureBox5_Click(object sender, EventArgs e)
        {
            DataView dataView = new DataView();
            if (this.file_loaded == 1)
            {
                dataGridView1.DataSource = dataView;

            }
            else
            {
                exception.ShowDialog();
            }
        }

        private void iconPictureBox6_Click(object sender, EventArgs e)
        {
            if (this.file_loaded == 1)
            {
                
            }
            else
            {
                exception.ShowDialog();
            }
        }

        private Font headerFont = new Font("Arial", 12, FontStyle.Bold);
        private Font footerFont = new Font("Arial", 10, FontStyle.Regular);

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
