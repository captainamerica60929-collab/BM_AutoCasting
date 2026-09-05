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
    public partial class Grin_Against_PO_Print : Form
    {
        public Grin_Against_PO_Print()
        {
            InitializeComponent();
        }

       
        private void button7_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void Grin_Against_PO_Print_Load(object sender, EventArgs e)
        {
            Display();
            get_Item();
        }


        public void get_Item()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_RM_ItemName");
            BM_BOM_Item.DataSource = dt;
            BM_BOM_Item.DisplayMember = "IM_PartName";
            BM_BOM_Item.ValueMember = "IM_ID";
            BM_BOM_Item.SelectedIndex = -1;
        }


        public void Display()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Fetch_GRN_Print  '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "',0");
                dataGridView1.DataSource = dt;
                dbFunctions.DGVStyle(dataGridView1);
            }
            catch { }
            //dataGridView1.Columns[0].Visible = true;
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

        private void button9_Click(object sender, EventArgs e)
        {
          
                print_Bill("QC");
          
        }


        public void print_Bill(string s)
        {
            try
            {
                //Cursor.Current = Cursors.WaitCursor;
               //GRN_QC_Details oRpt = new GRN_QC_Details();
                inspecation_report oRpt = new inspecation_report();

                string SQlQuery = "Pr_Fetch_GRN_Print_Pdf  '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "'," + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
                //string SQlQuery = "Pr_Fetch_GRN_Print_Pdf  '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "','4584'" ;

                dbFunctions.printpdf("Qc", SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string  Location = System.Configuration.ConfigurationSettings.AppSettings["Location"];

            if (dataGridView1.SelectedRows.Count > 0)
            {

                if (!dataGridView1.SelectedRows[0].Cells["Document"].Value.ToString().Equals("Not Uploaded"))
                {
                    DialogResult result = MessageBox.Show("Are You Sure Want to Open File Press yes", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        string Path = Location + "" + dataGridView1.SelectedRows[0].Cells["Location"].Value.ToString();

                        label2.Text = Path;
                        System.Diagnostics.Process.Start(Path);
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload Document", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            else
            {
                MessageBox.Show("Please Select One Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BM_BOM_Item_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void BM_BOM_Item_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (string.IsNullOrEmpty(BM_BOM_Item.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Grade] like '%{0}%' ", BM_BOM_Item.Text);
                }


            }
            catch (Exception ex)
            {

            }
        }
    }
}
