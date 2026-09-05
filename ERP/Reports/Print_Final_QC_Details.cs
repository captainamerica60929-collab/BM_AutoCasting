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
    public partial class Print_Final_QC_Details : Form
    {

        public int Distance = 56;
        public Print_Final_QC_Details()
        {
            InitializeComponent();
        }

        private void Purchase_Approval_print_Load(object sender, EventArgs e)
        {
            Display();
        }

       
        public void Display()
        {
            DataTable dt = dbFunctions.getTable("pr_get_Final_QC_Detail  '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "'");
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

        public void print_Bill()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Final_Inspection_QCDetails oRpt = new Final_Inspection_QCDetails();

                string SQlQuery = "pr_Print_Final_QC_Detail '" + dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString() + "'," + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + ",'" + dataGridView1.SelectedRows[0].Cells["Shift"].Value.ToString() + "'";
                dbFunctions.printpdf("QC", SQlQuery, oRpt);
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
                print_Bill();
              
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Route Card No] like '%{0}%'  OR [Part Name] like '%{0}%'  OR [Part No] like '%{0}%'", textBoxX1.Text);
                }


            }
            catch (Exception ex)
            {

            }

        }
    }
}
