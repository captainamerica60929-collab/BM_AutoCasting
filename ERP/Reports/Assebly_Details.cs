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
    public partial class Assebly_Details : Form
    {
        public Assebly_Details()
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

        void getdata()
        {
            DataTable dt = dbFunctions.getTable("pr_Assembly_details_display  '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
           // DataTable dt = dbFunctions.getTable("pr_Assembly_Production_Details  '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            //dataGridView1.Columns[0].Visible = true;
            //dataGridView1.Columns[0].Width = 150;
            //txt_Rows.Text = dbFunctions.getRows(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();

            //calc();
        }

        //void calc()
        //{
        //    int total = 0;
        //    int present = 0;
        //    int absent = 0;
        //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //    {
        //        total += int.Parse(dataGridView1.Rows[i].Cells["Total Employee"].Value.ToString());
        //        present += int.Parse(dataGridView1.Rows[i].Cells["Present"].Value.ToString());
        //        absent += int.Parse(dataGridView1.Rows[i].Cells["Absent"].Value.ToString());

        //    }
        //    label5.Text = total.ToString();
        //    label7.Text = present.ToString();
        //    label9.Text = absent.ToString();
        //}

        private void shift_SelectedIndexChanged(object sender, EventArgs e)
        {
            getdata();
        }

        private void ToadayStatus_Load(object sender, EventArgs e)
        {
            Inspector.Text = dbFunctions.username;
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Part No] like '%{0}%' or [Part Name] like '%{0}%'", textBoxX1.Text);
                }

               
            }
            catch (Exception ex)
            {
               
            }
           // calc();
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
        int a1 = 0;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                panel1.Visible = true;
                 a1 = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["EDIT ID"].Value);

                DataTable a = dbFunctions.getTable(@"select  im_partno as [Part No],  
                            im_partname as [Part Name], ASD_Qty as [Qty], ASD_Rejqty as [Rej Qty] from Assembly_details
                            left outer join Assembly_data_Details on AS_iid = as_id
                            left outer join item_master on IM_ID = ASD_ITEMID   where AS_iid ='"+ dataGridView1.Rows[e.RowIndex].Cells["EDIT ID"].Value.ToString() +"'");
                             dataGridView2.DataSource = a;
                dbFunctions.DGVStyle(dataGridView2);

               


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panel1.Visible = true;
            DataTable dt = dbFunctions.getTable("pr_get_Assembly_Production_Detaila '" + dataGridView1.SelectedRows[0].Cells["Barcode"].Value + "'");
            dataGridView2.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView2);
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

                string ReplaceText = text.Replace("@Barcode@", dataGridView1.SelectedRows[0].Cells["Barcode"].Value.ToString());
                RawPrinterHelper.SendStringToPrinter(PrinterName, ReplaceText);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            panel5.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
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
                PrinterName = dbFunctions.Printer_Name;// dataClass.printerName.ToString();
            }
            catch { }
            for (int i = 0; i < n; i++)
            {
                string text = System.IO.File.ReadAllText(@"D:\FG_Tag.prn");

                string ReplaceText = text.Replace("@PartName@", dataGridView1.SelectedRows[0].Cells["Part Name"].Value.ToString().Replace("/ " + dataGridView1.SelectedRows[0].Cells["Part No"].Value.ToString(), ""));
                ReplaceText = ReplaceText.Replace("@PartNo@", dataGridView1.SelectedRows[0].Cells["Part No"].Value.ToString());
                ReplaceText = ReplaceText.Replace("@Qty@", dataGridView1.SelectedRows[0].Cells["Pack Qty"].Value.ToString());
                ReplaceText = ReplaceText.Replace("@Barcode@", dataGridView1.SelectedRows[0].Cells["Barcode"].Value.ToString());
                ReplaceText = ReplaceText.Replace("@Inspector@", Inspector.Text);//dataGridView1.SelectedRows[0].Cells["Inspector"].Value.ToString());
                ReplaceText = ReplaceText.Replace("@Date@", System.DateTime.Now.ToString("dd-MMM-yyyy"));
  
                RawPrinterHelper.SendStringToPrinter(PrinterName, ReplaceText);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UPDATE_Click(object sender, EventArgs e)
        {
            DataTable A = dbFunctions.getTable("UPDATE Assembly_data_Details SET ASD_Qty='" + OK_QTY.Text + "',ASD_Rejqty='" + REJ_QTY.Text + "' WHERE as_id='" + a1 + "'");
            int ok = string.IsNullOrWhiteSpace(OK_QTY.Text) ? 0 : Convert.ToInt32(OK_QTY.Text);
            int rej = string.IsNullOrWhiteSpace(REJ_QTY.Text) ? 0 : Convert.ToInt32(REJ_QTY.Text);
            int total = ok + rej;
            DataTable a1s = dbFunctions.getTable("UPDATE Assembly_details SET AS_Qty='" + total + "' WHERE AS_iid='" + a1 + "'");
            MessageBox.Show("Update successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OK_QTY.Text = "";
            REJ_QTY.Text = "";
            panel1.Visible = false;
            getdata();

        }
    }
}
