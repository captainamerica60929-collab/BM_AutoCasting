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
    public partial class Print_Invoice : Form
    {

        public int Distance = 56;
        public Print_Invoice()
        {
            InitializeComponent();
        }

        private void Purchase_Approval_print_Load(object sender, EventArgs e)
        {
            Display();
        }

       
        public void Display()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Invoice_Details  '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "'");
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

        public void print_Bill(string s, string Type_of_Copy)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Invoice_Pdf oRpt = new Invoice_Pdf();
                oRpt.DataDefinition.FormulaFields["Copy_Type"].Text = "'" + Type_of_Copy + "'";

                string SQlQuery = "pr_Print_Invoice '" + s.Replace("_", "/") + "'";
                dbFunctions.printpdf1(Type_of_Copy+s, SQlQuery, oRpt);
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
                string BillNo=dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Replace("/","_");
                print_Bill(BillNo, "Orginal for Buyer");
                print_Bill(BillNo, "Triplicate for Supplier");
                print_Bill(BillNo, "Duplicate for Transporter");
                print_Bill(BillNo, "Office Copy");
                print_Bill(BillNo, "Extra Copy");


              
                PDFMerger merge = new PDFMerger();
                merge.AddFile(dbFunctions.path + "\\Orginal for Buyer" + BillNo.ToString() + ".pdf");
                merge.AddFile(dbFunctions.path + "\\Triplicate for Supplier" + BillNo.ToString() + ".pdf");
                merge.AddFile(dbFunctions.path + "\\Duplicate for Transporter" + BillNo.ToString() + ".pdf");
                merge.AddFile(dbFunctions.path + "\\Office Copy" + BillNo.ToString() + ".pdf");
                merge.AddFile(dbFunctions.path + "\\Extra Copy" + BillNo.ToString() + ".pdf");

                string file = dbFunctions.path + BillNo.ToString() + ".pdf";

                merge.DestinationFile = file;
                merge.Execute();
                System.Diagnostics.Process.Start(file);
   
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
    }
}
