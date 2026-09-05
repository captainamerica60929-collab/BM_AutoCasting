using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;

namespace LarchERP.Master
{

    public partial class BO_Alreat : Form
    {
        public string arrow = "Up";
        public int Distance = 220;
        public string ID = "";
        string ErrorMessage = "";
        public bool isMachSupLoad = false;
        public BO_Alreat()
        {
            InitializeComponent();
            dbFunctions.DGVStyle(dataGridView1);
        }
        private void ItemMaster_Shown(object sender, EventArgs e)
        {
            display();
            
        }
        private void ItemMaster_Load(object sender, EventArgs e)
        {
           
            display();
           
        }
        public void display()
        {
            DataTable dt = dbFunctions.getTable("pr_BO_DASHBORD_Stock  ");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}