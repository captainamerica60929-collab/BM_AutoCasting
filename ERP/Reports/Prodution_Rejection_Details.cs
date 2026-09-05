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
using CRM_App.Crystal;

namespace GenuineHR.Reports
{
    public partial class Prodution_Rejection_Details : Form
    {
        public Prodution_Rejection_Details()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            getdata();
                 }

        void getdata()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_monthwise_prodution_rejection_details '"+ todate.Value.ToString("yyyyMM01")+ "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();

            //DataTable dt = dbFunctions.getTable("SELECT MM_MachineName as MachineName,Pq_RM_Spec as [RM PART NO],Pq_RM_Grade AS [RM PART NAME] FROM Production_Request LEFT OUTER JOIN Machine_Master  ON Pq_Machine_ID = MM_ID  WHERE  CONVERT(DATE, Pq_CreatedDate)  = '{todate.Value.ToString("dd-MM-yyyy")}");
//            DataTable dt = dbFunctions.getTable($@"
//    SELECT mm_id, Pq_Machine_Name as [Machine Code],              
//Pq_vPart_No  AS [PART NAME] ,Pq_RM_Grade as [RM GRADE],Pq_RM_Plan_Qty AS [PLAN QTY],txt_Hourly_Pro AS [HOURLY PROD]
//    FROM Production_Request 
//    LEFT OUTER JOIN Machine_Master ON Pq_Machine_ID = MM_ID 
//    WHERE Pq_Status='A' and CONVERT(DATE, Pq_CreatedDate) = '{todate.Value.ToString("yyyy-MM-dd")}'");
//            dataGridView1.DataSource = dt;
//            dbFunctions.DGVStyle(dataGridView1);


        }

        void calc()
        {

           
                int totalHours = 0;
                int totalMinutes = 0;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    var cellValue = dataGridView1.Rows[i].Cells["TotalHours"].Value;
                    if (cellValue == null) continue;

                    string timeStr = cellValue.ToString().Trim();
                    if (!timeStr.Contains(":")) continue;

                    string[] parts = timeStr.Split(':'); // ✅ correct

                    int hours = int.Parse(parts[0]);
                    int minutes = int.Parse(parts[1]);

                    totalHours += hours;
                    totalMinutes += minutes;
                }

                // Convert minutes to hours
                totalHours += totalMinutes / 60;
                totalMinutes = totalMinutes % 60;

                label5.Text = $"{totalHours:D2}:{totalMinutes:D2}";
            

            //int totalHours = 0;
            //int totalMinutes = 0;

            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    string timeStr = dataGridView1.Rows[i].Cells["TotalHours"].Value.ToString();
            //    string[] parts = timeStr.Split('.'); // Split hours and minutes
            //    int hours = int.Parse(parts[0]);
            //    int minutes = int.Parse(parts[1]);

            //    totalHours += hours;
            //    totalMinutes += minutes;
            //}

            //// Convert total minutes to hours
            //totalHours += totalMinutes / 60;
            //totalMinutes = totalMinutes % 60;

            //label5.Text = $"{totalHours:D2}.{totalMinutes:D2}";

        }

        private void shift_SelectedIndexChanged(object sender, EventArgs e)
        {
            getdata();
        }

        private void ToadayStatus_Load(object sender, EventArgs e)
        {
            search = false;
            getdata();
           

           
        }

        private void ToadayStatus_Activated(object sender, EventArgs e)
        {
            getdata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getdata();
            search = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;

        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBoxX1_Enter(object sender, EventArgs e)
        {
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Machine_plan oRpt = new Machine_plan();

                string SQlQuery = "Machinewise_Plan '" + todate.Value.ToString("yyyyMMdd") + "'";// '" + dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString() + "'," + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + ",'" + dataGridView1.SelectedRows[0].Cells["Shift"].Value.ToString() + "'";
                dbFunctions.printpdf("QC", SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
            //try
            //{
            //    Cursor.Current = Cursors.WaitCursor;
            //    Production_Plan_ActualPrint oRpt = new Production_Plan_ActualPrint();
            //    string SQlQuery = "pr_PRINT_Machinewise_Plan  '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "','" + todate.Value.ToString("yyyyMMdd") + "'";

            //    oRpt.DataDefinition.FormulaFields["Date1"].Text = "'" + todate.Value.ToString("dd-MMM-yyyy")+ "'";
            //    oRpt.DataDefinition.FormulaFields["Date2"].Text = "'" + todate.Value.AddDays(1).ToString("dd-MMM-yyyy") + "'";
            //    oRpt.DataDefinition.FormulaFields["Date3"].Text = "'" + todate.Value.AddDays(2).ToString("dd-MMM-yyyy") + "'";
            //    oRpt.DataDefinition.FormulaFields["Date4"].Text = "'" + todate.Value.AddDays(3).ToString("dd-MMM-yyyy") + "'";
            //    oRpt.DataDefinition.FormulaFields["Date5"].Text = "'" + todate.Value.AddDays(4).ToString("dd-MMM-yyyy") + "'";
            //    oRpt.DataDefinition.FormulaFields["Date6"].Text = "'" + todate.Value.AddDays(5).ToString("dd-MMM-yyyy") + "'";
            //    oRpt.DataDefinition.FormulaFields["Date7"].Text = "'" + todate.Value.AddDays(6).ToString("dd-MMM-yyyy") + "'";
            //    oRpt.DataDefinition.FormulaFields["Machine"].Text = "'" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + "'";
            //    dbFunctions.printpdf("Plan_Sheet", SQlQuery, oRpt);
            //    Cursor.Current = Cursors.Default;
            //}
            //catch { }

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

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Month_Wise_Prodution_Rejection_Report oRpt = new Month_Wise_Prodution_Rejection_Report();

                string SQlQuery = "pr_fetch_monthwise_prodution_rejection_list '"+ todate.Value.ToString("yyyyMMdd") + "'";// '" + dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString() + "'," + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + ",'" + dataGridView1.SelectedRows[0].Cells["Shift"].Value.ToString() + "'";
                dbFunctions.printpdf("P&D_List", SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Production_Idle_Hours_Report oRpt = new Production_Idle_Hours_Report();

                string SQlQuery = "pr_fetch_Production_Idle_list '" + todate.Value.ToString("yyyyMMdd") + "'";// '" + dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString() + "'," + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + ",'" + dataGridView1.SelectedRows[0].Cells["Shift"].Value.ToString() + "'";
                dbFunctions.printpdf("Idle_List", SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Final_Inspection_Rejection_Report oRpt = new Final_Inspection_Rejection_Report();

                string SQlQuery = "pr_fetch_daily_inhouse_rej '" + todate.Value.ToString("yyyyMMdd") + "'";// '" + dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString() + "'," + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + ",'" + dataGridView1.SelectedRows[0].Cells["Shift"].Value.ToString() + "'";
                dbFunctions.printpdf("InHouse_List", SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            search = true;
            DataTable dt = dbFunctions.getTable("pr_fetch_reson_Idle_list '" + todate.Value.ToString("yyyyMM01") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
            calc();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_daily_inhouse_rej '" + todate.Value.ToString("yyyyMM01") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
            search = false;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Production_Inspection_Rejection_Report oRpt = new Production_Inspection_Rejection_Report();

                string SQlQuery = "pr_fetch_daily_production_rej '" + todate.Value.ToString("yyyyMMdd") + "'";// '" + dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString() + "'," + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + ",'" + dataGridView1.SelectedRows[0].Cells["Shift"].Value.ToString() + "'";
                dbFunctions.printpdf("InHouse_List", SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }

        bool search =false;
        private void button11_Click(object sender, EventArgs e)
        {
            search = false;
            DataTable dt = dbFunctions.getTable("pr_fetch_daywise_prodution_rejection_details '" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            search = true;
            DataTable dt = dbFunctions.getTable("pr_fetch_day_reson_Idle_list '" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
            calc();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            search = false;
            DataTable dt = dbFunctions.getTable("pr_fetch_Production_Idle_list '" + todate.Value.ToString("yyyyMMdd") + "'");// '" + dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString() + "'," + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + ",'" + dataGridView1.SelectedRows[0].Cells["Shift"].Value.ToString() + "'";
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            search = false;
            DataTable dt = dbFunctions.getTable("pr_fetch_daily_inhouse_rej '" + todate.Value.ToString("yyyyMMdd") + "'");
                  dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            search = false;
            DataTable dt = dbFunctions.getTable("pr_fetch_daily_production_rej '" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            search = false;
            DataTable dt = dbFunctions.getTable("pr_fetch_monthwise_prodution_rejection_list '" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void textBoxX1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                var dt = dataGridView1.DataSource as DataTable;
                if (dt != null)
                {
                    string filterText = textBoxX1.Text.Trim().Replace("'", "''"); // escape quotes

                    if (string.IsNullOrEmpty(filterText))
                    {
                        dt.DefaultView.RowFilter = string.Empty;
                    }
                    else
                    {
                        List<string> filters = new List<string>();

                        if (dt.Columns.Contains("Part_Name"))
                            filters.Add($"CONVERT([Part_Name], System.String) LIKE '%{filterText}%'");

                        if (dt.Columns.Contains("Plant"))
                            filters.Add($"CONVERT([Plant], System.String) LIKE '%{filterText}%'");

                        if (dt.Columns.Contains("IM_PartName"))
                            filters.Add($"CONVERT([IM_PartName], System.String) LIKE '%{filterText}%'");

                        if (filters.Count > 0)
                            dt.DefaultView.RowFilter = string.Join(" OR ", filters);
                        else
                            dt.DefaultView.RowFilter = string.Empty; // No matching columns, no filter
                    }
                }
            }
            catch (Exception ex)
            {
                // No message shown to user if columns are missing — only real errors
                MessageBox.Show("Error filtering data: " + ex.Message);
            }
            if(search)
            {
                calc();
               search = true;
            }

            //try
            //{

            //    if (string.IsNullOrEmpty(textBoxX1.Text))
            //    {
            //        (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            //    }
            //    else
            //    {
            //        (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Part_Name] LIKE '%{0}%' or [Plant] LIKE '%{0}%'", textBoxX1.Text);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
