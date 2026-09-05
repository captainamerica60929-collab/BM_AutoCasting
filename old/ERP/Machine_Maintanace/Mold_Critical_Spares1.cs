using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using LarchERP.Master;
using CRM_App.Production;

namespace CRM_App.Maintanace
{
    public partial class Mold_Critical_Spares1 : Form
    {
        public Mold_Critical_Spares1()
        {
            InitializeComponent();
        }

        private void Mould_Maintenance_List_Load(object sender, EventArgs e)
        {
            
            display();
        }
       public void display()
        {
            DataTable dt = dbFunctions.getTable("pr_Dispaly_Mold_Critical_Spares");
            dataGridView1.DataSource = dt;
            //dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
            dataGridView1.ColumnHeadersHeight = 40; 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }
    }
}
