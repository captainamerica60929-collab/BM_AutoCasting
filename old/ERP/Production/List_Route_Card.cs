using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using CRM_App.Crystal;
using CRM_App.Production;
using Shasun_Printing_Toll.Masters;
using System.Drawing.Printing;
using System.Data.SqlClient;

namespace CRM_App.Transaction
{
    public partial class List_Route_Card : Form

    {
        private DataTable dt;

        public int Distance = 56;
        public List_Route_Card()
        {
            InitializeComponent();
        }
        int Column = 0;

        private void Purchase_Approval_print_Load(object sender, EventArgs e)
        {
            Inspector.Text = dbFunctions.username;
            PrintDocument prtdoc = new PrintDocument();
            string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;

            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                cmbPrinter.Items.Add(strPrinter);
                if (strPrinter == strDefaultPrinter)
                {
                    cmbPrinter.SelectedIndex = cmbPrinter.Items.IndexOf(strPrinter);
                }
            }
            dataClass.printerName = cmbPrinter.Text;

            //string targetColumnName = "PDI Ins Rej Qty"; // Replace "SK" with the actual name or index of your column

            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    DataGridViewCell cell = row.Cells[targetColumnName];
            //    if (string.IsNullOrWhiteSpace(Convert.ToString(cell.Value)))
            //    {
            //        cell.Value = "0";
            //    }
            //}
            loadpalt();
            textBoxX1.TextChanged += FilterData;
            textBoxX4.TextChanged += FilterData;

        }
        private void FilterData(object sender, EventArgs e)
        {
            string searchText1 = textBoxX1.Text.Trim().ToLower();
            string searchText2 = textBoxX4.Text.Trim().ToLower();

            if (dt == null) return;

            // Define column indexes (change if needed)
            int column1Index = 0; // First column to search
            int column2Index = 1; // Second column to refine

            DataTable filteredTable = dt.Clone(); // Copy structure

            foreach (DataRow row in dt.Rows)
            {
                bool match1 = string.IsNullOrEmpty(searchText1) || row[column1Index].ToString().ToLower().Contains(searchText1);
                bool match2 = string.IsNullOrEmpty(searchText2) || row[column2Index].ToString().ToLower().Contains(searchText2);

                if (match1 && match2)
                {
                    filteredTable.Rows.Add(row.ItemArray);
                }
            }

            dataGridView1.DataSource = filteredTable;
        }

        private void loadpalt()
        {
            try
            {
                DataTable a = dbFunctions.getTable("select * from plant_master where pl_status='A'");
                plant.DataSource = a;
                plant.DisplayMember = "PL_Name";
                plant.ValueMember = "PL_ID";
                plant.SelectedIndex = -1;
            }
            catch { }
        }

        public void Display()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_RouteCard_Details  '" + dtpFrom.Value.ToString("yyyyMMdd") + "'");
                dataGridView1.DataSource = dt;
                dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["Part No"].Width = 150;
                dataGridView1.ColumnHeadersHeight = 50;
                dataGridView1.Columns["Part Name"].Width = 350;
                dataGridView1.Columns["Part No"].Visible = false;
                dataGridView1.Columns["Pack Qty"].Visible = false;
                dataGridView1.Columns["Mc Name"].Width = 150;
                dataGridView1.Rows[0].Selected = false;
                //dataGridView1.Columns[8].DefaultCellStyle.ForeColor = Color.Green;
                //dataGridView1.Columns[9].DefaultCellStyle.ForeColor = Color.Red;

                dataGridView1.Columns["FG STOCK"].DefaultCellStyle.ForeColor = Color.Green;
                dataGridView1.Columns["Inprocess OK Qty"].DefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.Columns["PDI Ins OK Qty"].DefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.Columns["PDI Ins Rej Qty"].DefaultCellStyle.ForeColor = Color.Red;
                dataGridView1.Columns["Inprocess Rej Qty"].DefaultCellStyle.ForeColor = Color.Red;

                // After loading data into the DataGridView
                // Ensure that the data is loaded into the DataGridView first
                // Ensure that the data is loaded into the DataGridView first
                // Ensure that the data is loaded into the DataGridView first
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        DataGridViewCell statusCell = row.Cells["RM Status"];
                        string status = statusCell.Value?.ToString();

                        if (status != null)
                        {
                            // Modify the text (forecolor) based on the status value
                            if (status == "Waitting")
                            {
                                statusCell.Style.ForeColor = Color.Red;
                                statusCell.Style.SelectionForeColor = Color.Red;
                                statusCell.Style.BackColor = Color.White; // To make sure text is visible
                            }
                            else if (status == "Issued")
                            {
                                //statusCell.Style.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
                                statusCell.Style.ForeColor = Color.Green;
                                statusCell.Style.SelectionForeColor = Color.Green;
                               statusCell.Style.BackColor = Color.White; // Ensure visibility
                            }
                            else
                            {
                                statusCell.Style.ForeColor = Color.Black;
                                statusCell.Style.SelectionForeColor = Color.Black;
                                statusCell.Style.BackColor = Color.White; // Ensure visibility
                            }
                        }
                    }
                }

                // Force the DataGridView to refresh
                dataGridView1.Refresh();





                //private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
                //{
                //    // Check if the formatting is for the "Inprocess Rej Qty" column
                //    if (dataGridView1.Columns[e.ColumnIndex].Name == "RM Status")
                //    {
                //        // Get the value of the cell
                //        string cellValue = e.Value?.ToString();

                //        // Change the color based on the value
                //        if (cellValue == "Issue")
                //        {
                //            e.CellStyle.ForeColor = Color.Red;
                //        }
                //        else if (cellValue == "Waiting")
                //        {
                //            e.CellStyle.ForeColor = Color.Green;
                //        }
                //        else
                //        {
                //            // Optionally reset to default color for other values
                //            e.CellStyle.ForeColor = Color.Black;
                //        }
                //    }
                //}


                int x = 4;
                dataGridView1.Columns[x + 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dataGridView1.Columns[x + 1].DefaultCellStyle.Format = "##,##,##,###.00";

                dataGridView1.Columns[x + 2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dataGridView1.Columns[x + 2].DefaultCellStyle.Format = "##,##,##,###";

                dataGridView1.Columns[x + 3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dataGridView1.Columns[x + 3].DefaultCellStyle.Format = "##,##,##,###";

                dataGridView1.Columns[x + 4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dataGridView1.Columns[x + 4].DefaultCellStyle.Format = "##,##,##,###";

                dataGridView1.Columns[x + 5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dataGridView1.Columns[x + 5].DefaultCellStyle.Format = "##,##,##,###";

                dataGridView1.Columns[x + 6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dataGridView1.Columns[x + 6].DefaultCellStyle.Format = "##,##,##,###";

                dataGridView1.Columns[x + 7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dataGridView1.Columns[x + 7].DefaultCellStyle.Format = "##,##,##,###";

                dataGridView1.Columns[x + 8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dataGridView1.Columns[x + 8].DefaultCellStyle.Format = "##,##,##,###";

                dataGridView1.Columns[x + 9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dataGridView1.Columns[x + 9].DefaultCellStyle.Format = "##,##,##,###";

            }
            catch { }

        }



        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                View_Route_Card ObjProduction_Plan = new View_Route_Card();
                dbFunctions.Route_Card_ID = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
                panel2.Controls.Clear();
                panel2.Visible = true;

                panel2.Dock = System.Windows.Forms.DockStyle.Fill;
                if (ObjProduction_Plan.IsDisposed)
                {
                    ObjProduction_Plan = new View_Route_Card();
                }
                ObjProduction_Plan.TopLevel = false;
                ObjProduction_Plan.FormBorderStyle = FormBorderStyle.None;
                ObjProduction_Plan.Dock = DockStyle.Fill;
                panel2.Controls.Add(ObjProduction_Plan);
                ObjProduction_Plan.Show();
            }
            catch { }
        }

        private void panel2_ControlRemoved(object sender, ControlEventArgs e)
        {
            panel2.Visible = false;
        }

        private void List_Route_Card_Shown(object sender, EventArgs e)
        {
            Display();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int n = 0;
            try
            {
                n = int.Parse(textBoxX2.Text);
            }
            catch
            {

                MessageBox.Show("Enter valid No of Prints", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string PrinterName = "";
            try
            {
                PrinterName = dbFunctions.Printer_Name;
            }
            catch { }
            for (int i = 0; i < n; i++)
            {
                string text = System.IO.File.ReadAllText(@"D:\Small_Barcode.prn");

                string ReplaceText = text.Replace("@Barcode@", dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                RawPrinterHelper.SendStringToPrinter(PrinterName, ReplaceText);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
           // save();
            int n = 0;
            try
            {
                n = int.Parse(textBoxX3.Text);
            }
            catch
            {

                MessageBox.Show("Enter valid No of Prints", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string PrinterName = "";
            try
            {
                PrinterName = dbFunctions.Printer_Name;// dataClass.printerName.ToString();
            }
            catch { }
            for (int i = 0; i < n; i++)
            {
                string text = System.IO.File.ReadAllText(@"D:\FG_Tag.prn");

                string ReplaceText = text.Replace("@PartName@", dataGridView1.SelectedRows[0].Cells["Part Name"].Value.ToString().Replace("/ " + dataGridView1.SelectedRows[0].Cells["Part No"].Value.ToString(), ""));
                ReplaceText = ReplaceText.Replace("@PartNo@", dataGridView1.SelectedRows[0].Cells["Part No"].Value.ToString());
                ReplaceText = ReplaceText.Replace("@Qty@", dataGridView1.SelectedRows[0].Cells["Pack Qty"].Value.ToString());
                ReplaceText = ReplaceText.Replace("@Barcode@", dataGridView1.SelectedRows[0].Cells["Route Card"].Value.ToString());
                ReplaceText = ReplaceText.Replace("@Inspector@", Inspector.Text);//dataGridView1.SelectedRows[0].Cells["Inspector"].Value.ToString());
                ReplaceText = ReplaceText.Replace("@Date@", System.DateTime.Now.ToString("dd-MMM-yyyy"));

                RawPrinterHelper.SendStringToPrinter(PrinterName, ReplaceText);
            }
        }
        String ID2 = "0";
        private void save()
        {
            //string a1 = dataGridView1.Rows[0].Cells["Pack Qty"].Value?.ToString();
            int a1 = Convert.ToInt32(dataGridView1.Rows[0].Cells["Pack Qty"].Value ?? 0);

            string a3 = dataGridView1.Rows[0].Cells["Route Card"].Value?.ToString();

            //string a2 = dataGridView1.Rows[0].Cells["PDI Ins OK Qty"].Value?.ToString();
            //int a2 = Convert.ToInt32(dataGridView1.Rows[0].Cells["PDI Ins OK Qty"].Value ?? 0);
            int a2 = Convert.ToInt32(textBoxX3.Text);


            int bag = (a2 / a1);

            int c = bag * a1;

            int t = c - a2;


            int count = (int)Math.Ceiling((double)a2 / a1);

            for (int i = 1; i <= count; i++)
            {
                string modifiedRouteCard = $"{a3}-{i}";
                int bagQty = (i == count) ? (a2 - (a1 * (count - 1))) : a1;

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.Text;

                    if (ID2 == "0")
                    {
                        // Generate a new idl_id
                        DataTable dd = dbFunctions.getTable("SELECT ISNULL(MAX(fg_bar_id), 0) + 1 FROM fg_barcode_details");
                        if (dd.Rows.Count > 0)
                        {
                            ID2 = dd.Rows[0][0].ToString();
                        }

                        com.CommandText = "INSERT INTO fg_barcode_details (fg_bar_id,fg_bar_routecard, fg_bar_Barcode, fg_bar_qty, fg_bar_username, fg_bar_date, fg_bar_status) " +
                       "VALUES (@fg_bar_id,@fg_bar_routecard, @fg_bar_Barcode, @fg_bar_qty, @fg_bar_username, @fg_bar_date, @fg_bar_status)";

                    }
                    else
                    {

                    }

                    // Add parameters
                    com.Parameters.Add("@fg_bar_id", SqlDbType.Int).Value = ID2;
                    com.Parameters.Add("@fg_bar_routecard", SqlDbType.VarChar).Value = a3;
                    com.Parameters.Add("@fg_bar_Barcode", SqlDbType.VarChar).Value = modifiedRouteCard;
                    com.Parameters.Add("@fg_bar_qty", SqlDbType.Int).Value = bagQty;
                    com.Parameters.Add("@fg_bar_username", SqlDbType.VarChar).Value = dbFunctions.username;
                    com.Parameters.Add("@fg_bar_date", SqlDbType.DateTime).Value = dbFunctions.getdate();
                    com.Parameters.Add("@fg_bar_status", SqlDbType.VarChar).Value = "A";



                    // Execute the query
                    com.ExecuteNonQuery();
                   

                    string PrinterName = "";
                    try
                    {
                        PrinterName = dbFunctions.Printer_Name;// dataClass.printerName.ToString();
                    }
                    catch { }
                   
                        string text = System.IO.File.ReadAllText(@"D:\FG_Tag.prn");

                        string ReplaceText = text.Replace("@PartName@", dataGridView1.SelectedRows[0].Cells["Part Name"].Value.ToString().Replace("/ " + dataGridView1.SelectedRows[0].Cells["Part No"].Value.ToString(), ""));
                        ReplaceText = ReplaceText.Replace("@PartNo@", dataGridView1.SelectedRows[0].Cells["Part No"].Value.ToString());
                        //ReplaceText = ReplaceText.Replace("@Qty@", dataGridView1.SelectedRows[0].Cells["Pack Qty"].Value.ToString());
                        //ReplaceText = ReplaceText.Replace("@Qty@", $"{bagQty} / {dataGridView1.SelectedRows[0].Cells["Pack Qty"].Value}");
                        ReplaceText = ReplaceText.Replace("@Qty@", $"{bagQty}");
                        ReplaceText = ReplaceText.Replace("@Barcode@", modifiedRouteCard);
                        ReplaceText = ReplaceText.Replace("@Inspector@", Inspector.Text);//dataGridView1.SelectedRows[0].Cells["Inspector"].Value.ToString());
                        ReplaceText = ReplaceText.Replace("@Date@", System.DateTime.Now.ToString("dd-MMM-yyyy"));

                        RawPrinterHelper.SendStringToPrinter(PrinterName, ReplaceText);
                    


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ID2 = "0";

            }
            MessageBox.Show("Details Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);



            //string a3 = dataGridView1.Rows[0].Cells["Route Card"].Value?.ToString();


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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Route Card] LIKE '%{0}%' OR [Part Name] LIKE '%{0}%'  OR [Mc Name] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int Row_ID = 0;
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Row_ID = e.RowIndex;
                Menus.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (dataGridView1.SelectedRows[0].Cells["RM Status"].Value.ToString() == "Issued")
            {

                Production_Data_Entry ObjProduction_Plan = new Production_Data_Entry();
                dbFunctions.Route_Card_ID = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
                panel2.Controls.Clear();
                panel2.Visible = true;

                panel2.Dock = System.Windows.Forms.DockStyle.Fill;
                if (ObjProduction_Plan.IsDisposed)
                {
                    ObjProduction_Plan = new Production_Data_Entry();
                }
                ObjProduction_Plan.TopLevel = false;
                ObjProduction_Plan.FormBorderStyle = FormBorderStyle.None;
                ObjProduction_Plan.Dock = DockStyle.Fill;
                panel2.Controls.Add(ObjProduction_Plan);
                ObjProduction_Plan.Show();
            }
            else
            {
                MessageBox.Show("RM Not Issued", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void printBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inprocess_Inspection_Entry ObjInprocess_Inspection_Entry = new Inprocess_Inspection_Entry();
            dbFunctions.Route_Card_ID = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
            panel2.Controls.Clear();
            panel2.Visible = true;

            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            if (ObjInprocess_Inspection_Entry.IsDisposed)
            {
                ObjInprocess_Inspection_Entry = new Inprocess_Inspection_Entry();
            }
            ObjInprocess_Inspection_Entry.TopLevel = false;
            ObjInprocess_Inspection_Entry.FormBorderStyle = FormBorderStyle.None;
            ObjInprocess_Inspection_Entry.Dock = DockStyle.Fill;
            panel2.Controls.Add(ObjInprocess_Inspection_Entry);
            ObjInprocess_Inspection_Entry.Show();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inspected_Production_Data_Entry ObjInprocess_Inspection_Entry = new Inspected_Production_Data_Entry();
            dbFunctions.Route_Card_ID = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
            panel2.Controls.Clear();
            panel2.Visible = true;

            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            if (ObjInprocess_Inspection_Entry.IsDisposed)
            {
                ObjInprocess_Inspection_Entry = new Inspected_Production_Data_Entry();
            }
            ObjInprocess_Inspection_Entry.TopLevel = false;
            ObjInprocess_Inspection_Entry.FormBorderStyle = FormBorderStyle.None;
            ObjInprocess_Inspection_Entry.Dock = DockStyle.Fill;
            panel2.Controls.Add(ObjInprocess_Inspection_Entry);
            ObjInprocess_Inspection_Entry.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
               
              //  Display();
                // Check if the formatting is for the "Inprocess Rej Qty" column
                if (dataGridView1.Columns[e.ColumnIndex].Name == "RM Status")
                {
                    // Get the value of the cell
                    string cellValue = e.Value?.ToString();

                    // Change the color based on the value
                    if (cellValue == "Issue")
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                    else if (cellValue == "Waiting")
                    {
                        e.CellStyle.ForeColor = Color.Green;
                    }
                    else
                    {
                        // Optionally reset to default color for other values
                        e.CellStyle.ForeColor = Color.Black;
                    }

                }
            }
            catch (Exception A) { }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void plant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(plant.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Plant Name] LIKE '%{0}%'", plant.Text);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
