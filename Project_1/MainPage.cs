using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace Project_1
{
    public partial class MainPage : Form
    {

        // Fields
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        public FileLoader File_Loader = new FileLoader();

        public MapInterface Data_Inspector = new MapInterface();


        // Constructor
        public MainPage()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 149);
            panel1.Controls.Add(leftBorderBtn);
            // Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null) 
                currentChildForm.Hide();
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconPictureBox1.IconChar = IconChar.HomeLg;
            iconPictureBox1.IconColor = Color.White;
            label1.Text = "Home";
            File_Loader.Hide();
            Data_Inspector.Hide();
        }

        //Structs
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(52,192,215);
            // Here we can add more differetn colors to the menu
        }




        // Methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                // Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(71,currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                // Principal Icon
                iconPictureBox1.IconChar = currentBtn.IconChar;
                iconPictureBox1.IconColor = color;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(21, 30, 45);
                currentBtn.ForeColor = Color.White;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.White;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        public bool fileloaded = false;

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(File_Loader);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            this.fileloaded = File_Loader.IsFileLoaded();
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(Data_Inspector);
            //filereaded.ReadFile(File_Loader.GetFilePath());
            //dataGridView1.DataSource= filereaded.getTableCAT10();
            Data_Inspector.getfileloaded(this.fileloaded);
            if (this.fileloaded == true)
            {
                Data_Inspector.getMapPointsCAT10(File_Loader.getMapPointsCAT10());
                Data_Inspector.getMapPointsCAT21(File_Loader.getMapPointsCAT21());
            }
            



        }


        // Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void MainPage_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //open only form
                currentChildForm.Hide();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.Show();
            label1.Text = childForm.Text;
        }

        private void iconPictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void iconPictureBox4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void iconPictureBox3_MouseEnter(object sender, EventArgs e)
        {
            iconPictureBox3.IconColor = Color.Red;
        }

        private void iconPictureBox3_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox3.IconColor = Color.White;
        }

        private void iconPictureBox2_MouseEnter(object sender, EventArgs e)
        {
            iconPictureBox2.IconColor = Color.MediumSeaGreen;
        }

        private void iconPictureBox2_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox2.IconColor = Color.White;
        }

        private void iconPictureBox4_MouseEnter(object sender, EventArgs e)
        {
            iconPictureBox4.IconColor = Color.DarkOrange;
        }

        private void iconPictureBox4_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox4.IconColor = Color.White;
        }

        private void MainPage_Load(object sender, EventArgs e)
        {

        }
    }
}
