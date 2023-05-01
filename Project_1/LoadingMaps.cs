using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Project_1
{
    public partial class LoadingScreen : Form
    {
        


        public LoadingScreen()
        {
            InitializeComponent();

        }

        private void LoadingScreen_Load(object sender, EventArgs e)
        {
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        public void changelabel(string text)
        {
            label1.Text = text;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
