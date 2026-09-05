using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Shasun_Printing_Toll.Masters;

namespace GenuineHR.Reports
{
    
    public partial class Lable_Print : Form
    {
        public Lable_Print()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("pr_gettodayStatus_all '" + dtpFrom.Value.ToString("yyyyMMdd") + "',1");//+ shift.SelectedValue);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns["Code"].Width = 800;
            dataGridView1.Columns["Name"].Width = 180;
            dataGridView1.Columns["Designation"].Width = 180;
            dataGridView1.Columns["Category"].Width = 120;

            int ab = 0, late = 0, present = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Status"].Equals("Absent"))
                {
                    ab++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                }

                if (dt.Rows[i]["Status"].Equals("Late"))
                {
                    late++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                }

                if (dt.Rows[i]["Status"].Equals("Present"))
                {
                    present++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightSeaGreen;
                }

            }
            //txt_Absent.Text = ab.ToString();
            //txtPresent.Text = (present + late).ToString();
            //txtLate.Text = late.ToString();
            //txtTotal.Text = dt.Rows.Count.ToString();
            //txt_early.Text = present.ToString();

        }
        decimal a1 = 0;
        int b = 0;
        void getdata()
        {
            DataTable dt = dbFunctions.getTable("pr_getCurrent_Stock_table '" + dtpFrom.Value.ToString("yyyyMMdd") + "'");//  '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            //UpdateCellColors();
            //dataGridView1.Columns[0].Visible = true;
            //dataGridView1.Columns[0].Width = 150;
            //txt_Rows.Text = dbFunctions.getRows(dataGridView1);




            //txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();

            ////calc();
            ////dataGridView1.Refresh();
            //UpdateCellColors();

        }

        public void UpdateCellColors()
        {
            try
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    try
                    {
                        b = int.Parse(dataGridView1.Rows[i].Cells["Min"].Value.ToString());
                        a1 = Decimal.Parse(dataGridView1.Rows[i].Cells["Stock"].Value.ToString());


                    }
                    catch (Exception a) { }

                    if (b > a1)
                    {

                        dataGridView1.Rows[i].Cells["Stock"].Style.ForeColor = Color.Red;

                    }
                    else if (a1 > b)
                    {
                        dataGridView1.Rows[i].Cells["Stock"].Style.ForeColor = Color.Green;

                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells["Stock"].Style.ForeColor = Color.Yellow;

                    }
                    // dataGridView1.Refresh();

                }
            }
            catch { }
            

        }

        void calc()
        {
            int total = 0;
            int present = 0;
            int absent = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                total += int.Parse(dataGridView1.Rows[i].Cells["Total Employee"].Value.ToString());
                present += int.Parse(dataGridView1.Rows[i].Cells["Present"].Value.ToString());
                absent += int.Parse(dataGridView1.Rows[i].Cells["Absent"].Value.ToString());

            }
            label5.Text = total.ToString();
            label7.Text = present.ToString();
            label9.Text = absent.ToString();
        }

        private void shift_SelectedIndexChanged(object sender, EventArgs e)
        {
            getdata();
        }

        private void ToadayStatus_Load(object sender, EventArgs e)
        {
            getdata();
           



        }

        private void ToadayStatus_Activated(object sender, EventArgs e)
        {
            getdata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getdata();
        }

        private void button8_Click(object sender, EventArgs e)
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[CS_Part_No] like '%{0}%' or [CS_Part_Name] like '%{0}%'or [CS_Lot_No] like '%{0}%' or [CS_Lot_Date] like '%{0}%' ", textBoxX1.Text);
                }

               
            }
            catch (Exception ex)
            {
               
            }
       //     calc();
            txt_Rows.Text = dbFunctions.getRows(dataGridView1);
        }

        private void textBoxX1_Enter(object sender, EventArgs e)
        {
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
             //dbFunctions.isclose = true; this.Close(); 
            DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {

                this.Close();

            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            getdata();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Want to Lable Print, Press YES", "Alcove", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                if (e.ColumnIndex == 1)
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        string PrinterName = dbFunctions.Printer_Name;
                        string text = System.IO.File.ReadAllText(@"D:\Lable.prn");

                        string ReplaceText1 = text.Replace("@Format@", dataGridView1.SelectedRows[0].Cells["CS_Lable_Format"].Value.ToString());
                        string ReplaceText2 = ReplaceText1.Replace("@PartNo@", dataGridView1.SelectedRows[0].Cells["CS_Part_No"].Value.ToString());
                        string ReplaceText3 = ReplaceText2.Replace("@PartName@", dataGridView1.SelectedRows[0].Cells["CS_Part_Name"].Value.ToString());
                        string ReplaceText4 = ReplaceText3.Replace("@PartGrade@", dataGridView1.SelectedRows[0].Cells["CS_Part_No"].Value.ToString());
                        string ReplaceText5 = ReplaceText4.Replace("@GRNNo@", dataGridView1.SelectedRows[0].Cells["CS_Lable_Format"].Value.ToString());
                        string ReplaceText6 = ReplaceText5.Replace("@GRNDate@", dataGridView1.SelectedRows[0].Cells["CS_Lot_Date"].Value.ToString());
                        string ReplaceText7 = ReplaceText6.Replace("@Lot@", dataGridView1.SelectedRows[0].Cells["CS_Lot_No"].Value.ToString());
                        string ReplaceText8 = ReplaceText7.Replace("@PackQty@", dataGridView1.SelectedRows[0].Cells["CS_Qty"].Value.ToString());
                        string ReplaceText9 = ReplaceText8.Replace("@Rec_Date@", dataGridView1.SelectedRows[0].Cells["CS_Lot_Date"].Value.ToString());
                        string ReplaceText10 = ReplaceText9.Replace("@Mfg_Date@", dataGridView1.SelectedRows[0].Cells["CS_Lot_Date"].Value.ToString());
                        string ReplaceText11 = ReplaceText10.Replace("@Exp_Date@", dataGridView1.SelectedRows[0].Cells["CS_Lot_Date"].Value.ToString());
                        string ReplaceText12 = ReplaceText11.Replace("@Approve_By@", dataGridView1.SelectedRows[0].Cells["CS_Part_Name"].Value.ToString());
                        string ReplaceText13 = ReplaceText12.Replace("@Approve_Date@", dataGridView1.SelectedRows[0].Cells["CS_Lot_Date"].Value.ToString());
                        string ReplaceText14 = ReplaceText13.Replace("@Year@", System.DateTime.Now.ToString("MMM yyyy"));
                        string ReplaceText15 = ReplaceText14.Replace("@Barcode@", dataGridView1.SelectedRows[0].Cells["CS_Barcode"].Value.ToString());
                        RawPrinterHelper.SendStringToPrinter(PrinterName, ReplaceText15);


                    }
                    else
                    {
                        MessageBox.Show("Select atleast 1 row for Re-Print", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }





        }

        private void print()
        {
          
        }


        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panel1.Visible = true;
            DataTable dt = dbFunctions.getTable("pr_getCurrent_Stock_bypard '" + dataGridView1.SelectedRows[0].Cells["Part NO"].Value + "'");
            dataGridView2.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView2);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            getdata();
        }

        private void BtnDisplay_Click_1(object sender, EventArgs e)
        {
            UpdateCellColors();
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            getdata();

        }
    }
}
