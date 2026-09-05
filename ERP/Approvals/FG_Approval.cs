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

    public partial class FG_Approval : Form
    {
        public string arrow = "Up";
        public int Distance = 36;
        public string ID = "";
        string ErrorMessage = "";
        public string SupType = "";
        public string Department = "";
        public FG_Approval()
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
                DataTable dt = dbFunctions.getTable("pr_Display_FG_Waiting");
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

                    if (dataGridView1.Rows[i].Cells["Operation Type"].Value.ToString() == "Newly Added")
                    {
                        DataTable dtx = dbFunctions.getTable("pr_Approve_FG_Item_Master  '" + dataGridView1.Rows[i].Cells["ID"].Value + "'");
                    }
                }
            }


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) == "1")
                {

                    if (dataGridView1.Rows[i].Cells["Operation Type"].Value.ToString() == "Details Updated")
                    {
                        DataTable dtx = dbFunctions.getTable("pr_Update_FG_Item_Master  '" + dataGridView1.Rows[i].Cells["ID"].Value + "'");
                    }
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
            DataTable dt = dbFunctions.getTable("pr_get_changed_Details  "+dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
            dataGridView2.DataSource = dt;

            for(int i=0;i<dt.Rows.Count;i++)
            {
                if (dt.Rows[i][3].ToString().Equals("1"))
                {
                   dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }

        }



    }
}