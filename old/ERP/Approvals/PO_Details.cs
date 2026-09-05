using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using CRM_App.Crystal;


namespace CRM_App.Transaction
{
    public partial class PO_Details : Form
    {

        public int Distance = 56;
        public PO_Details()
        {
            InitializeComponent();
        }

        private void Purchase_Approval_print_Load(object sender, EventArgs e)
        {
            Display();
        }

       
        public void Display()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Purchase_Order_Approval_Print  '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
           

            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1); dataGridView1.Columns[0].Visible = false;
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            calculation();
        }

        public void calculation()
        {
            decimal Sub_Total = 0.0m; 

            decimal Grand_Total = 0.0m; 

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Sub_Total += decimal.Parse(dataGridView1.Rows[i].Cells["Sub Total"].Value.ToString());
                Grand_Total += decimal.Parse(dataGridView1.Rows[i].Cells["Grand Total"].Value.ToString());

            }
            txtsubtotal.Text = Sub_Total.ToString();
            txtgrandtotal.Text = Grand_Total.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void button9_Click(object sender, EventArgs e)
        {
           
        }

        public void print_Bill(string s)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                CrystalReport1 oRpt = new CrystalReport1();
                string SQlQuery = "Pr_Print_Purchase_Order_Approval_Report '" + s + "'";
                dbFunctions.printpdf(s, SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }


        public void print_Bill1(string s)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Other_Purchase oRpt = new Other_Purchase();
                string SQlQuery = "Pr_Print_Purchase_Order_Approval_Report '" + s + "'";
                dbFunctions.printpdf(s, SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                print_Bill(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
            }
            else
            {
                MessageBox.Show("Please Select one Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(textBoxX1.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Supplier Name] LIKE '%{0}%' or [P.O No] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtsubtotal_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                print_Bill1(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
            }
            else
            {
                MessageBox.Show("Please Select one Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dd = dbFunctions.getTable("Update Purchase_Order set PO_vApproval_Status='Waiting' where PO_iID=" + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
                MessageBox.Show("Allowew Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Display();
            }
            else
            {
                MessageBox.Show("Please Select one Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
