using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Project_1
{
    public partial class DataInspectorInterface : Form
    {
        
        public DataInspectorInterface()
        {
            InitializeComponent();
        }

        public void Load_Data_To_Table(DataTable newtable)
        {
            dataGridView1.DataSource = newtable;

        }




    }
}
