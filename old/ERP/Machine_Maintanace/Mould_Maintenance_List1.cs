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
using CRM_App.Crystal;

namespace CRM_App.Maintanace
{
    public partial class Mould_Maintenance_List1 : Form
    {
        public Mould_Maintenance_List1()
        {
            InitializeComponent();
        }

        private void Mould_Maintenance_List_Load(object sender, EventArgs e)
        {
            radio_Today_List.Checked = true;
            display();
        }

        private void radio_Today_List_CheckedChanged(object sender, EventArgs e)
        {
            display();


        }

        void display()
        {
            if (radio_Today_List.Checked == true)
            {

                
                     DataTable dt = dbFunctions.getTable("pr_get_MACHINE_TODAY_PM_Need");
                //DataTable dt = dbFunctions.getTable("pr_get_Mould_TODAY_PM_Need");
                dataGridView1.DataSource = dt;
                dbFunctions.DGVStyle(dataGridView1);
               // dataGridView1.ColumnHeadersHeight = 40;
            }
            else
            {
                DataTable dt = dbFunctions.getTable("pr_get_MACHINE_ALL_PM_Need");
                dataGridView1.DataSource = dt;
                dbFunctions.DGVStyle(dataGridView1);
               // dataGridView1.ColumnHeadersHeight = 40;
            }

            dataGridView1.ReadOnly = true;
        }

        private void radio_All_Schedule_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               dbFunctions.Mould_ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
               dbFunctions.Mould_Name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                dbFunctions.isPM_data = true;
              

                Preventive_Matainance ObjInvoice = new Preventive_Matainance();

                panel4.Controls.Clear();
                panel4.Visible = true;

                panel4.Dock = System.Windows.Forms.DockStyle.Fill;
                if (ObjInvoice.IsDisposed)
                {
                    ObjInvoice = new Preventive_Matainance();
                }
                ObjInvoice.TopLevel = false;
                ObjInvoice.FormBorderStyle = FormBorderStyle.None;
                ObjInvoice.Dock = DockStyle.Fill;
                panel4.Controls.Add(ObjInvoice);
                ObjInvoice.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel4_ControlRemoved(object sender, ControlEventArgs e)
        {
            panel4.Visible = false;
            display();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Mould_Plan_Vs_Actual oRpt = new Mould_Plan_Vs_Actual();
                
                    string SQlQuery = "pr_get_MACHINE_Plan_vs_Actual '" + dateTimePicker1.Text + "'";
               // string SQlQuery = "pr_get_Mould_Plan_vs_Actual '" + dateTimePicker1.Text + "'";
                dbFunctions.printpdf("Plan_Vs_Actual", SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }
    }
}
