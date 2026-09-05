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


namespace CRM_App.Transaction
{
    public partial class Pending_Mess_Cutting_List : Form
    {

        public int Distance = 56;
        public Pending_Mess_Cutting_List()
        {
            InitializeComponent();
        }
        int Column = 0;

        private void Purchase_Approval_print_Load(object sender, EventArgs e)
        {
           
        }

       
        public void Display()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_Mess_Cutting_Details");
                dataGridView1.DataSource = dt;
                dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
               // dataGridView1.Columns["Part No"].Width = 150;
                dataGridView1.Columns["Part Name"].Width = 250;
                dataGridView1.Columns["B"].Width = 40;
               
                //dataGridView1.Columns["Prod. Qty"].DefaultCellStyle.ForeColor = Color.Green;





                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                   
                    dataGridView1.Rows[i].Cells["B"].Value = (System.Drawing.Image)Properties.Resources.Barcode;
                    dataGridView1.Rows[i].Cells["B"].ToolTipText = "Barcode";

                   

                }
         //       dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;

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

            if (e.ColumnIndex == 8)
            {
                panel3.Visible = true;
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

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 8)
            {
                panel3.Visible = true;
            }
            else
            {
                Mess_Cutting_Data_Entry ObjProduction_Plan = new Mess_Cutting_Data_Entry();
                dbFunctions.Route_Card_ID = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
                panel2.Controls.Clear();
                panel2.Visible = true;

                panel2.Dock = System.Windows.Forms.DockStyle.Fill;
                if (ObjProduction_Plan.IsDisposed)
                {
                    ObjProduction_Plan = new Mess_Cutting_Data_Entry();
                }
                ObjProduction_Plan.TopLevel = false;
                ObjProduction_Plan.FormBorderStyle = FormBorderStyle.None;
                ObjProduction_Plan.Dock = DockStyle.Fill;
                panel2.Controls.Add(ObjProduction_Plan);
                ObjProduction_Plan.Show();
            }
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 8)
            {
                panel3.Visible = true;
            }
        }
    }
}
