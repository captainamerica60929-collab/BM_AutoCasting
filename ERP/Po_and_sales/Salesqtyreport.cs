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

namespace GenuineHR.Reports
{
    
    public partial class Salesqtyreport : Form
    {
        public Salesqtyreport()
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
            DataTable dt = dbFunctions.getTable1(@"select '' as ok,SD_Part_No as [Part No],	SD_Part_Name as [Part Name],sum(SD_Qty) as [Qty] from Sales_Invoice_Details
where  SD_Status = 'A' and Convert(date, SD_Created_Date, 112) Between  '" + dtpFrom.Value.ToString("yyyyMMdd") + "' and '" + todate.Value.ToString("yyyyMMdd") + "'  group by SD_Part_No, SD_Part_Name");
            //  '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");

            //DataTable dt = dbFunctions.getTable("pr_getCurrent_Stock");//  '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            UpdateCellColors();
            //dataGridView1.Columns[0].Visible = true;
            //dataGridView1.Columns[0].Width = 150;
            //txt_Rows.Text = dbFunctions.getRows(dataGridView1);
          
            
           

            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();

            //calc();
            //dataGridView1.Refresh();
            UpdateCellColors();

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
                        dataGridView1.Rows[i].Cells["Stock"].Style.ForeColor = Color.Blue;

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
            getdata1();
        }

        private void getdata1()
        {

            //DataTable dt = dbFunctions.getTable("Pr_Fetch_day_RM_Currentoutward_Stock  '" + todate.Value.ToString("yyyyMMdd") + "' ");//  '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            string fromDate = dtpFrom.Value.ToString("yyyyMMdd");
            string toDate = todate.Value.ToString("yyyyMMdd");
            string previousDate = dtpFrom.Value.AddDays(-1).ToString("yyyyMMdd");

            DataTable b = dbFunctions.getTable(@"
                    select
                        ''  as rk,
                        IM_PartNo as [Part No],
                        IM_PartName as [Part Name],
                        (
                            dbo.fget_Production_Qty('20140801', '" + previousDate + @"', IM_ID) +
                            cast(isnull((
                                select sum(fe_ok + fe_rej)
                                from [CHE_SRUTHY_PLASTIC].[final_entry]
                                where fe_partid = IM_ID and convert(date, fe_Date, 113) < '" + fromDate + @"'
                            ), 0) as int) -
                            dbo.fget_Invoice_Qty('20140801', '" + previousDate + @"', IM_ID)
                        )
                        +
                        dbo.fget_Production_Qty1('" + fromDate + @"', '" + toDate + @"', IM_ID) +
                        dbo.fget_Production_Assmbly('" + fromDate + @"', '" + toDate + @"', IM_ID) +
                        cast(isnull((
                            select sum(fe_ok + fe_rej)
                            from [CHE_SRUTHY_PLASTIC].[final_entry]
                            where fe_partid = IM_ID and convert(date, fe_Date, 113) between '" + fromDate + @"' and '" + toDate + @"'
                        ), 0) as int) -
                        dbo.fget_Invoice_Qty('" + fromDate + @"', '" + toDate + @"', IM_ID) +
                        (
                            select sum(wor_Qty)
                            from Work_Order_Receive_Details
                            where wor_status = 'A' AND wor_MaterialType = 'FG' AND WOR_PID = IM_ID
                        ) as [Qty]
                    from Item_Master
                    where IM_Status = 'A' and IM_Type = 1
                ");
            dataGridView1.DataSource = b;
            dbFunctions.DGVStyle(dataGridView1);
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

        private void button3_Click_1(object sender, EventArgs e)
        {
            getdata();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void todate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
