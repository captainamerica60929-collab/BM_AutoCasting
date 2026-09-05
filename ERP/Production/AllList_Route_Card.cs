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


namespace CRM_App.Transaction
{
    public partial class AllList_Route_Card : Form
    {

        public int Distance = 56;
        public AllList_Route_Card()
        {
            InitializeComponent();
        }
        int Column = 0;

        private void Purchase_Approval_print_Load(object sender, EventArgs e)
        {
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
        }



        public void Display()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_RouteCard_Details1");
                dataGridView1.DataSource = dt;
                dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                // dataGridView1.Columns["Part No"].Width = 150;
                dataGridView1.ColumnHeadersHeight = 50;
                dataGridView1.Columns["Part Name"].Width = 150;
                dataGridView1.Columns["Part No"].Visible = false;
                dataGridView1.Columns["Pack Qty"].Visible = false;
                dataGridView1.Columns["Mc Name"].Width = 150;
                dataGridView1.Rows[0].Selected = false;
                dataGridView1.Columns[8].DefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.Columns[9].DefaultCellStyle.ForeColor = Color.Red;

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
            if (e.ColumnIndex == (13+Column))
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

            if (e.ColumnIndex == (14 + Column))
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
            if (e.ColumnIndex == (15 + Column))
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

            if (e.ColumnIndex == (17 + Column))
            {
                panel3.Visible = true;
            }

            if (e.ColumnIndex == (18 + Column))
            {
                panel5.Visible = true;
            }

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex >= 13)
                dataGridView1.Cursor = Cursors.Hand;
            else
                dataGridView1.Cursor = Cursors.Default;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int n=0;
            try{
                n=int.Parse(textBoxX2.Text);
            }catch{
            
            MessageBox.Show("Enter valid No of Prints","Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            return;
            }
            string PrinterName ="";
            try
            {
                PrinterName = dataClass.printerName.ToString();
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
                PrinterName = dataClass.printerName.ToString();
            }
            catch { }
            for (int i = 0; i < n; i++)
            {
                string text = System.IO.File.ReadAllText(@"D:\Accept_TAG.prn");

                string ReplaceText = text.Replace("@PartName@", dataGridView1.SelectedRows[0].Cells["Part Name"].Value.ToString());
                ReplaceText = ReplaceText.Replace("@PartNo@", dataGridView1.SelectedRows[0].Cells["Part No"].Value.ToString());
                ReplaceText = ReplaceText.Replace("@Qty@", dataGridView1.SelectedRows[0].Cells["Pack Qty"].Value.ToString());
                ReplaceText = ReplaceText.Replace("@Barcode@", dataGridView1.SelectedRows[0].Cells["Route Card"].Value.ToString());
                
                RawPrinterHelper.SendStringToPrinter(PrinterName, ReplaceText);
            }
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Route Card] LIKE '%{0}%' OR [Part Name] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
