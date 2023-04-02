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
                //read.ReadFile(filePath); // Leemos el fichero
                
                this.dataGridView1.DataSource = read.getTableCAT10();
                this.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                
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
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Output.csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dataGridView1.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[dataGridView1.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += dataGridView1.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCsv[i] += dataGridView1.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                }
                            }

                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
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
    }
}
