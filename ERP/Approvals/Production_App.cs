using Maintanence_Printing_Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM_App.Approvals
{
    public partial class Production_App : Form
    {
        public Production_App()
        {
            InitializeComponent();
        }

        private void Production_App_Load(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
           // doWork.HeaderText = "Select";
           // doWork.FalseValue = "0";
           // doWork.TrueValue = "1";
           // dataGridView1.Columns.Insert(0, doWork);
            display();
        }
        void display()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Fetch_peoduction_Approval");
                dataGridView1.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Cambria", 10, FontStyle.Bold);
                //dataGridView1.Columns["ID"].Visible = true;
                // txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
            }
            catch { }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string id = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
                dbFunctions.getTable("UPDATE Production_Details " +"SET PD_Approval='Approved' " +"WHERE PD_iid='" + id + "'");
                MessageBox.Show("Approved Successfully");
                display();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }
    }
}
