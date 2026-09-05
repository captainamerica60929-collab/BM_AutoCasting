using CRM_App.Crystal;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using iTextSharp.text.pdf;
using Maintanence_Printing_Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace CRM_App.Transaction
{
    public partial class Purchase_Print : Form
    {

        public int Distance = 56;
        public Purchase_Print()
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

            decimal Rej_Price = 0.0m;
            

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
            catch (Exception ex)
            {
               
            }
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
                string id = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();

                // Step 1: Print Confirmation
                DialogResult confirmPrint = MessageBox.Show("Do you want to Print?", "Custom Print", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (confirmPrint == DialogResult.Yes)
                {
                    // Step 2: Format Selection
                    DialogResult formatChoice = MessageBox.Show("Do you want PDF or Excel?\n\nYES -> PDF\nNO -> EXCEL", "Select Format", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (formatChoice == DialogResult.Yes)
                    {
                        print_Bill_WithFormat(id, "PDF");
                    }
                    else
                    {
                        print_Bill_WithFormat(id, "Excel");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select one Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        public void print_Bill_WithFormat(string s, string format)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                CrystalReport1 oRpt = new CrystalReport1();
                string SQlQuery = "Pr_Print_Purchase_Order_Approval_Report '" + s + "'";

                if (format == "PDF")
                {
                    dbFunctions.printpdf(s, SQlQuery, oRpt);
                }
                else if (format == "Excel")
                {
                    DataTable ReportData = dbFunctions.getTable(SQlQuery);
                    oRpt.Database.Tables[0].SetDataSource(ReportData);

                    string folderPath = dbFunctions.path;
                    if (string.IsNullOrEmpty(folderPath)) folderPath = @"C:\Reports";
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                    string exportPath = Path.Combine(folderPath, s + ".xls");

                    // Using Excel format (not data-only) to preserve report layout (Logo, Headers, etc.)
                    oRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, exportPath);

                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Excel file generated successfully:\n" + exportPath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(exportPath);
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel21(dataGridView1);
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

        public void print_Bill1(string s, string exportType)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Other_Purchase oRpt = new Other_Purchase();
                string SQlQuery = "Pr_Print_Purchase_Order_Approval_Report '" + s + "'";
                dbFunctions.printpdf(s, SQlQuery, oRpt); // your method to set data source

                string folderPath = @"C:\Reports";
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string exportPath = Path.Combine(folderPath, $"PurchaseOrder_{s}");

                if (exportType == "Excel")
                {
                    exportPath += ".xlsx";

                    // Use Excel Record (Data-only format)
                    oRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, exportPath);
                }
                else if (exportType == "PDF")
                {
                    exportPath += ".pdf";
                    oRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, exportPath);
                }

                Cursor.Current = Cursors.Default;

                MessageBox.Show($"Report exported successfully to {exportType}:\n{exportPath}",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                System.Diagnostics.Process.Start(exportPath);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Error while exporting: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable dtData = new DataTable();
            DataTable ReportData = new DataTable();
            ReportDocument oRpt = new ReportDocument();
            //DataTable db
            ReportData = dbFunctions.getTable("Pr_Print_Purchase_Order_Approval_Report '"+ dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString()+"'");
            oRpt = new Other_Purchase();

            try
            {
                ExportOptions exportOpts = new ExportOptions();
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();

                oRpt.Database.Tables[0].SetDataSource(ReportData);

                ExportOptions CrExportOptions = default(ExportOptions);
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                // PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                ExcelFormatOptions CrFormatTypeOptions = new ExcelFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = System.IO.Path.Combine(System.Configuration.ConfigurationSettings.AppSettings["Filepath"].ToString(), "Tally_Format.xls");
                CrExportOptions = oRpt.ExportOptions;
                var _with1 = CrExportOptions;
                _with1.ExportDestinationType = ExportDestinationType.DiskFile;
                _with1.ExportFormatType = ExportFormatType.ExcelRecord;
                _with1.DestinationOptions = CrDiskFileDestinationOptions;
                _with1.FormatOptions = CrFormatTypeOptions;
                oRpt.Export();
                System.Diagnostics.Process.Start(CrDiskFileDestinationOptions.DiskFileName);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
