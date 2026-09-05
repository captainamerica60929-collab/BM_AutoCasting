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
    public partial class GRN_Details_Print : Form
    {

        public int Distance = 56;
        public GRN_Details_Print()
        {
            InitializeComponent();
        }

        private void Purchase_Approval_print_Load(object sender, EventArgs e)
        {
            Display();
        }



        public void Display1()
        {
            dataGridView1.DataSource = null;
            DataTable dt = dbFunctions.getTable("pr_get_GRN_Details_byDate_Details  '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            //dataGridView1.Columns["PO No"].Visible = true;


            dbFunctions.DGVStyle(dataGridView1); dataGridView1.Columns[0].Visible = false;
            //dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            calculation();
        }
       
        public void Display()
        {
            dataGridView1.DataSource = null;
            DataTable dt = dbFunctions.getTable("pr_get_GRN_Details_byDate  '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
           //dataGridView1.Columns["PO No"].Visible = true;
           

            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1); dataGridView1.Columns[0].Visible = false;
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            calculation();
        }
        public void calculation()
        {
            decimal Sub_Total = 0.0m;
            decimal Grand_Total = 0.0m;
            decimal Tax_Total = 0.0m;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Sub_Total += decimal.Parse(dataGridView1.Rows[i].Cells["SUB Total"].Value.ToString());
                Grand_Total += decimal.Parse(dataGridView1.Rows[i].Cells["Gross Total"].Value.ToString());
                Tax_Total += decimal.Parse(dataGridView1.Rows[i].Cells["Tax Amount"].Value.ToString());

            }
            txtsubtotal.Text = Sub_Total.ToString();
            txtgrandtotal.Text = Grand_Total.ToString();
            txtTaxTotal.Text = Tax_Total.ToString();
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[GRN No] LIKE '%{0}%' or [Supplier Name] LIKE '%{0}%'", textBoxX1.Text);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Display1();
        }
    }
}
