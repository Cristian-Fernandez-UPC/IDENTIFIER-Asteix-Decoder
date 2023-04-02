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
    public partial class FilterException : Form
    {
        public FilterException()
        {
            InitializeComponent();
        }

        private void iconPictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void iconPictureBox3_MouseEnter(object sender, EventArgs e)
        {
            iconPictureBox3.IconColor = Color.Red;
        }

        private void iconPictureBox3_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox3.IconColor = Color.White;
        }

        private void FilterException_Load(object sender, EventArgs e)
        {

        }
    }
}
