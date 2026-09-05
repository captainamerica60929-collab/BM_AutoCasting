using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using CRM_App.Crystal;
using WebApplication;


namespace CRM_App.Transaction
{
    public partial class Delivery_Challan_Print : Form
    {

        public int Distance = 56;
        public Delivery_Challan_Print()
        {
            InitializeComponent();
        }

        private void Purchase_Approval_print_Load(object sender, EventArgs e)
        {
            Display();
        }

       
        public void Display()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_DC_Details  '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1); dataGridView1.Columns[0].Visible = false;
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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

        public void print_Bill(string DC_No, string Type_of_Copy)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DC_Pdf oRpt = new DC_Pdf();
               
                string SQlQuery = "pr_Print_DC '" + DC_No+ "'";
                dbFunctions.printpdf("DC", SQlQuery, oRpt);
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


                string BillNo = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                print_Bill(BillNo, "Orginal for Buyer");

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

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("delete from DC_Details where INV_Invoice_No='" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString()+"'");
                    dt = dbFunctions.getTable("delete from DC where IN_Invoice_No='" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + "'");
                     Display();
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
