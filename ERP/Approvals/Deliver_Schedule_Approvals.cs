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

    public partial class Deliver_Schedule_Approval : Form
    {
        public string arrow = "Up";
        public int Distance = 36;
        public string ID = "";
        string ErrorMessage = "";
        public string SupType = "";
        public string Department = "";
        public Deliver_Schedule_Approval()
        {
            InitializeComponent();
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
        }
        private void ItemMaster_Shown(object sender, EventArgs e)
        {

            splitContainer1.SplitterDistance = Distance;
        }

        private void FG_Approval_Load(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
            doWork.HeaderText = "Select";
            doWork.FalseValue = "0";
            doWork.TrueValue = "1";
            dataGridView1.Columns.Insert(0, doWork);
            display();
           
        }

        void display()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_Pending_Schedule");
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["ID"].Visible = false;
                txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
            }
            catch { }
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) == "1")
                {


                    DataTable dtx = dbFunctions.getTable("Pr_Approve_Schedule  '" + dataGridView1.Rows[i].Cells["ID"].Value + "','Approved'");
                    
                }
            }


            display();
            MessageBox.Show("Selected Items Approved Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            panel1.Visible = true;
            DataTable dt = dbFunctions.getTable("pr_get_Schdule_Details_  '" + dataGridView1.SelectedRows[0].Cells["PO Number"].Value.ToString()+"'");
            dataGridView2.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView2);

            //for(int i=0;i<dt.Rows.Count;i++)
            //{
            //    if (dt.Rows[i][3].ToString().Equals("1"))
            //    {
            //       dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red;
            //    }
            //}

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) == "1")
                {


                    DataTable dtx = dbFunctions.getTable("Pr_Approve_Schedule  '" + dataGridView1.Rows[i].Cells["ID"].Value + "','Rejected'");

                }
            }


            display();
            MessageBox.Show("Selected Items Rejected Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }



    }
}