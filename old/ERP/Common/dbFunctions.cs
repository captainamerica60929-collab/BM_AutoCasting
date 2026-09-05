using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;


namespace Maintanence_Printing_Tool
{
    public class fetchid
    {
        public static string loadid;
    }
    class dbFunctions
    {
        public static string username="";
        public static string rights="";
        public static string loanno = "";
        public static string Lodge = "";
        public static string RoomNo = "";
        public static string CIN_ID = "";
        public static string  Selecdtroom = "";
        public static string statusrest = "";
        public static string Mould_ID = "";
        public static string Mould_Name = "";
        public static string status = "";
        public static string EmpCode = "";
        //public static string getdate = "true";
        public static bool isclose = false;
        public static bool isPM_data = false;
        public static bool item_Data = false;
        public static bool isMainpanelEmpty= false;
        public static string Route_Card_ID = "";
        public static string connectionstring = System.Configuration.ConfigurationSettings.AppSettings["ConStr"];
        public static string connectionstring1 = System.Configuration.ConfigurationSettings.AppSettings["ConStr1"];
        public static string path = System.Configuration.ConfigurationSettings.AppSettings["Filepath"];

        public static string Printer_Name = System.Configuration.ConfigurationSettings.AppSettings["Printer_Name"];
        public static string FMB = System.Configuration.ConfigurationSettings.AppSettings["FMB"];
       // public static string PrinterName = System.Configuration.ConfigurationSettings.AppSettings["Printer"];

        public static DataTable getTable(string sqlQuery)
        {
            
            Cursor.Current = Cursors.WaitCursor; 
            DataTable dt = new DataTable();
            try
            {

                SqlConnection con = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                sqlDA.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
          //   MessageBox.Show(ex.Message);
                //Logs(ex.ToString(), dbFunctions.username);
            }

            Cursor.Current = Cursors.Default;
            return dt;
        }
        public static DataTable getTable1(string sqlQuery)
        {

            Cursor.Current = Cursors.WaitCursor;
            DataTable dt = new DataTable();
            try
            {

                SqlConnection con = new SqlConnection(connectionstring1);
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                sqlDA.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                //   MessageBox.Show(ex.Message);
                //Logs(ex.ToString(), dbFunctions.username);
            }

            Cursor.Current = Cursors.Default;
            return dt;
        }
        public static string getdate() => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd-MMM-yyyy hh:mm:ss tt");

        public static void Logs(string Error,string User)
        {
           //// string sqlQuery = "Insert into Logs values(getdate(),'" + Error.Replace('\'', '~') +"','" + User + "')";
           
           


           // SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
           // try
           // {
           //     con.Open();
           //     SqlCommand com = new SqlCommand();
           //     com.Connection = con;
           //     com.CommandType = CommandType.StoredProcedure;
           //     com.CommandText = "pr_insertLogs";
           //     com.Parameters.Add("@L_Error_Details", SqlDbType.VarChar).Value = Error;
           //     com.Parameters.Add("@L_User", SqlDbType.VarChar).Value = User;

           //     com.ExecuteNonQuery();
           // }
           // catch { }
        }


        public static bool Numeric_DecimalOnly(char KeyChar, TextBox textBox)
        {
            bool IsHandled = false;
            if (!char.IsControl(KeyChar) && !char.IsDigit(KeyChar) && KeyChar != '.')
            {
                IsHandled = true;
            }

            // only allow one decimal point
            if (KeyChar == '.'
                && textBox.Text.IndexOf('.') > -1)
            {
                IsHandled = true;
            }
            return IsHandled;
        }

        public static string getRows(DataGridView dgv)
        {
            return "Rows : " + dgv.Rows.Count;
        }
        public static void DGVStyle1(DataGridView dgv)
        {

            try
            {

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                //for (int i = 0; i < dgv.Rows.Count - 1; i++)
                //{
                //    dgv.Rows[i].HeaderCell.Value = (i + 1).ToString();
                //    dgv.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                //    if (i % 2 == 0)
                //    {
                //        dgv.Rows[i].DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
                //    }
                //}


                for (int i = 1; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }

                // dgv.RowsDefaultCellStyle.BackColor = Color.LightGray;
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 249, 249);

                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;


                //dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.GradientInactiveCaption; //System.Drawing.SystemColors.SlateGray;
                //dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

                // dgv.GridColor = Color.Teal; ;

                //  dgv.DefaultCellStyle.SelectionBackColor = Color.Lavender;//Color.FromArgb(239, 243, 250);
                // dgv.DefaultCellStyle.SelectionForeColor = Color.DarkRed;

                //dgv.DefaultCellStyle.ForeColor = Color.DimGray;


                dgv.EnableHeadersVisualStyles = false;
                // dgv.RowHeadersWidth = 55;
                dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dgv.ColumnHeadersHeight = 27;
                // dgv.ReadOnly = true;
                //        dgv.ColumnHeadersBorderStyle =
                //DataGridViewHeaderBorderStyle.Single;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;



                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(54, 54, 54);//System.Drawing.SystemColors.GrayText;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(223, 227, 232);
                dgv.RowHeadersWidth = 25;

                dgv.GridColor = Color.FromArgb(218, 220, 221);
                dgv.BackgroundColor = Color.White;
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(183, 219, 255);// Color.FromArgb(251, 228, 141);
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;


                // new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.75F);
                //dgv.Rows[0].Selected = false;
                if (dgv.Rows.Count > 0)
                {
                    dgv.Rows[0].Selected = true;
                }
                dgv.Columns[0].Visible = false;
                dgv.RowsDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.75F);

            }
            catch (Exception sc) { }
        }
        public static void DGVStyle(DataGridView dgv)
        {
            
            try
            {

               dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                //for (int i = 0; i < dgv.Rows.Count - 1; i++)
                //{
                //    dgv.Rows[i].HeaderCell.Value = (i + 1).ToString();
                //    dgv.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                //    if (i % 2 == 0)
                //    {
                //        dgv.Rows[i].DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
                //    }
                //}


                for (int i = 1; i < dgv.Columns.Count - 1; i++)
                {
                    dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; 
                }

                // dgv.RowsDefaultCellStyle.BackColor = Color.LightGray;
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 249, 249);

                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;


                //dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.GradientInactiveCaption; //System.Drawing.SystemColors.SlateGray;
                //dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

                // dgv.GridColor = Color.Teal; ;

                //  dgv.DefaultCellStyle.SelectionBackColor = Color.Lavender;//Color.FromArgb(239, 243, 250);
                // dgv.DefaultCellStyle.SelectionForeColor = Color.DarkRed;

                //dgv.DefaultCellStyle.ForeColor = Color.DimGray;


                dgv.EnableHeadersVisualStyles = false;
                // dgv.RowHeadersWidth = 55;
                dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dgv.ColumnHeadersHeight = 27;
                // dgv.ReadOnly = true;
        //        dgv.ColumnHeadersBorderStyle =
        //DataGridViewHeaderBorderStyle.Single;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;



                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(54, 54, 54);//System.Drawing.SystemColors.GrayText;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(223, 227, 232);
                dgv.RowHeadersWidth = 25;
              
                dgv.GridColor = Color.FromArgb(218, 220, 221);
                dgv.BackgroundColor = Color.White;
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(183, 219, 255);// Color.FromArgb(251, 228, 141);
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
                

                // new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.75F);
                //dgv.Rows[0].Selected = false;
                if (dgv.Rows.Count > 0)
                {
                    dgv.Rows[0].Selected = true;
                }
                dgv.Columns[0].Visible = false;
                dgv.RowsDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.75F);

            }
            catch (Exception sc){ }
        }

        public static void DGVStyleAutoSizeColumn(DataGridView dgv)
        {

            try
            {

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 249, 249);

                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;



                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dgv.ColumnHeadersHeight = 27;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(54, 54, 54);//System.Drawing.SystemColors.GrayText;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(223, 227, 232);
                dgv.RowHeadersWidth = 25;

                dgv.GridColor = Color.FromArgb(218, 220, 221);
                dgv.BackgroundColor = Color.White;
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(183, 219, 255);// Color.FromArgb(251, 228, 141);
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
                dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.75F);
                //dgv.Rows[0].Selected = false;
                if (dgv.Rows.Count > 0)
                {
                    dgv.Rows[0].Selected = true;
                }
                dgv.Columns[0].Visible = false;
                dgv.RowsDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.75F);

            }
            catch { }
        }


        public static bool sendsms(string msg, string PhoneNo)
        {
            DataTable dt = dbFunctions.getTable("select * from Sms_Settings");

            try
            {
               // HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://203.212.70.200/smpp/sendsms?username=srivijaya&password=srivijaya123&to=" + PhoneNo + "&from=SVVBMS&udh=&text=" + msg + "&dlr-mask=19");
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(dt.Rows[0]["SM_ConStr1"].ToString() + PhoneNo + dt.Rows[0]["SM_ConStr2"].ToString() + msg + dt.Rows[0]["SM_ConStr3"].ToString());
                
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                // MessageBox.Show("hai");
                myResp.Close();
                
                return true;

            }
            catch (Exception ex)
            {

                return false;
            }
        }



        public static void ExportExcel21(DataGridView dgvGrid)
        {
            Cursor.Current = Cursors.WaitCursor;

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;

            try
            {
                worksheet.Name = "Exported by Genuine";

                // ===== HEADER =====
                for (int i = 0; i < dgvGrid.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dgvGrid.Columns[i].HeaderText;
                }

                // ===== DATA =====
                for (int i = 0; i < dgvGrid.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvGrid.Columns.Count; j++)
                    {
                        var value = dgvGrid.Rows[i].Cells[j].Value;

                        if (value != null)
                        {
                            Excel.Range cell =
                                (Excel.Range)worksheet.Cells[i + 2, j + 1];

                            DateTime dt;

                            // Try convert to DateTime (handles string + datetime both)
                            if (DateTime.TryParse(value.ToString(), out dt))
                            {
                                cell.Value2 = dt;
                                cell.NumberFormat = "dd/MM/yyyy";
                            }
                            else
                            {
                                cell.Value2 = value.ToString();
                            }
                        }
                    }
                }

                worksheet.Columns.AutoFit();
                app.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;

                // Release COM Objects (Important)
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(app);
            }
        }

        public static void ExportExcel(DataGridView dgvGrid)
        {
            Cursor.Current = Cursors.WaitCursor;
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;

            try
            {
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
                worksheet.Name = "Exported by Genuine";
                for (int i = 1; i < dgvGrid.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dgvGrid.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dgvGrid.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvGrid.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgvGrid.Rows[i].Cells[j].Value.ToString();
                    }
                }

            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                app.Quit();
                workbook = null;
                app = null;
            }
            Cursor.Current = Cursors.Default;
        }


        public static void ExportDataGridViewToExcel(DataGridView dgv)
        {
            try
            {
                if (dgv.Rows.Count == 0)
                {
                    MessageBox.Show("No data to export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Excel.Application excelApp = new Excel.Application();
                excelApp.Workbooks.Add(Type.Missing);

                // Add column headers
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    excelApp.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
                }

                // Add rows
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        object value = dgv.Rows[i].Cells[j].Value;

                        if (value != null)
                        {
                            // Check if value is a DateTime
                            if (DateTime.TryParse(value.ToString(), out DateTime dateValue))
                            {
                                excelApp.Cells[i + 2, j + 1] = dateValue.ToString("dd-MM-yyyy");
                            }
                            else
                            {
                                excelApp.Cells[i + 2, j + 1] = value.ToString();
                            }
                        }
                    }
                }

                excelApp.Columns.AutoFit();
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void printexcel(string s, string query, ReportClass report)
        {
            DataTable dt = getTable(query);
            report.SetDataSource(dt);

            string folderPath = @"C:\Reports";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string exportPath = Path.Combine(folderPath, $"PurchaseOrder_{s}.xlsx");

            report.ExportToDisk(ExportFormatType.ExcelWorkbook, exportPath);

            // Open file automatically
            Process.Start(exportPath);
        }

        public static void printpdf(string PdfFileName, string SqlQuery, ReportDocument RptFile)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable ReportData = new DataTable();
            ReportDocument oRpt = new ReportDocument();

            ReportData = dbFunctions.getTable(SqlQuery);

            oRpt = RptFile;



            int cnt = ReportData.Rows.Count;

            try
            {
                ExportOptions exportOpts = new ExportOptions();
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();

                oRpt.Database.Tables[0].SetDataSource(ReportData);
                ExportOptions CrExportOptions = default(ExportOptions);
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = System.IO.Path.Combine(dbFunctions.path, PdfFileName + ".pdf");
                CrExportOptions = oRpt.ExportOptions;
                var _with1 = CrExportOptions;
                _with1.ExportDestinationType = ExportDestinationType.DiskFile;
                _with1.ExportFormatType = ExportFormatType.PortableDocFormat;
                _with1.DestinationOptions = CrDiskFileDestinationOptions;
                _with1.FormatOptions = CrFormatTypeOptions;
                oRpt.Export();
                DialogResult result = MessageBox.Show("If You Want Print Press YES", "Genuine", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(CrDiskFileDestinationOptions.DiskFileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File Name already in Open,"+ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // MessageBox.Show("Error in Bill Creation, Contact Admin");
            }
            Cursor.Current = Cursors.Default;
        }



        public static void printWord(string PdfFileName, string SqlQuery, ReportDocument RptFile)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable ReportData = new DataTable();
            ReportDocument oRpt = new ReportDocument();

            ReportData = dbFunctions.getTable(SqlQuery);

            oRpt = RptFile;



            int cnt = ReportData.Rows.Count;

            try
            {
                ExportOptions exportOpts = new ExportOptions();
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();

                oRpt.Database.Tables[0].SetDataSource(ReportData);
                ExportOptions CrExportOptions = default(ExportOptions);
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
               
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = System.IO.Path.Combine(dbFunctions.path, PdfFileName + ".doc");
                CrExportOptions = oRpt.ExportOptions;
                var _with1 = CrExportOptions;
                _with1.ExportDestinationType = ExportDestinationType.DiskFile;
                _with1.ExportFormatType = ExportFormatType.WordForWindows;
                _with1.DestinationOptions = CrDiskFileDestinationOptions;
                _with1.FormatOptions = CrFormatTypeOptions;
                oRpt.Export();
                DialogResult result = MessageBox.Show("If You Want Print Press YES", "Genuine", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(CrDiskFileDestinationOptions.DiskFileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File Name already in Open,", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // MessageBox.Show("Error in Bill Creation, Contact Admin");
            }
            Cursor.Current = Cursors.Default;
        }




        public static void printpdf1(string PdfFileName, string SqlQuery, ReportDocument RptFile)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable ReportData = new DataTable();
            ReportDocument oRpt = new ReportDocument();

            ReportData = dbFunctions.getTable(SqlQuery);

            oRpt = RptFile;



            int cnt = ReportData.Rows.Count;

            try
            {
                ExportOptions exportOpts = new ExportOptions();
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();

                oRpt.Database.Tables[0].SetDataSource(ReportData);
                ExportOptions CrExportOptions = default(ExportOptions);
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = System.IO.Path.Combine(dbFunctions.path, PdfFileName + ".pdf");
                CrExportOptions = oRpt.ExportOptions;
                var _with1 = CrExportOptions;
                _with1.ExportDestinationType = ExportDestinationType.DiskFile;
                _with1.ExportFormatType = ExportFormatType.PortableDocFormat;
                _with1.DestinationOptions = CrDiskFileDestinationOptions;
                _with1.FormatOptions = CrFormatTypeOptions;
                oRpt.Export();
                //    DialogResult result = MessageBox.Show("If You Want Print Press YES", "Genuine", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //    if (result == DialogResult.Yes)
                //    {
                //        System.Diagnostics.Process.Start(CrDiskFileDestinationOptions.DiskFileName);
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("File Name already in Open,"+ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // MessageBox.Show("Error in Bill Creation, Contact Admin");
            }
            Cursor.Current = Cursors.Default;
        }
       
    }
}
