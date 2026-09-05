using CRM_App.Crystal;
using Maintanence_Printing_Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM_App.Reports
{
    public partial class oa_report : Form
    {
        public oa_report()
        {
            InitializeComponent();
        }

        private void Oa_report_Load(object sender, EventArgs e)
        {
            Getstock1();
            calculation();
        }

        private void Getstock1()    
        {
            DataTable dt = dbFunctions.getTable("poverall_report '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "' ");
          

            //string fromDate = DateTime.Parse(dtFromDt.Text).ToString("yyyyMMdd");
            //string toDate = DateTime.Parse(dtToDt.Text).ToString("yyyyMMdd");
            //DataTable dt = dbFunctions.getTable(@"select '' as no, MM_MachineCode as [Machine Code],
            //    Pq_Route_Card_No as [RouteCardNo],PD_Shift as [Shift],
            //    Pq_RM_Grade as [MATERIAL NAME ],IM_Model as [Model], 
            //    Pq_vPart_No as [PART NAME], MLD_MoldCavity as [NO.OF CAVITIES],
            //    IM_Sales_Price as [Cost Part],

            //     CAST( NULLIF(Pq_RM_Plan_Qty, 0) / (3600 / NULLIF(MLD_Cycle_Time, 0) * 1.0) AS DECIMAL(10, 2) ) AS [TIMING],  
            //     PD_time AS [Production TIMING], 
            //     RIGHT('0' + CAST(DATEDIFF(MINUTE, idl_fromtime, idl_totime) / 60 AS VARCHAR), 2) + ':' + RIGHT('0' + CAST(DATEDIFF(MINUTE, idl_fromtime, idl_totime) % 60 AS VARCHAR), 2) AS [IDL_time_range],

            //    RIGHT('0' + CAST(CAST(LEFT(PD_time, 2) AS decimal) - DATEDIFF(MINUTE, idl_fromtime, idl_totime) / 60 AS VARCHAR), 2) + ':' + 
            //        RIGHT('0' + CAST((CAST(RIGHT(PD_time, 2) AS decimal) - DATEDIFF(MINUTE, idl_fromtime, idl_totime) % 60) % 60 AS VARCHAR), 2) AS [Remai_Time],



            //    MLD_Cycle_Time  as [Cycle Time],
            //    Pq_RM_Plan_Qty as [TARGET qty],(PD_OK_Qty + PD_Reject_Qty) as [production Qty],
            //    PD_OK_Qty as [OK qty],PD_Reject_Qty as [TOTAL REJ], 
            //    CAST(((PD_OK_Qty + PD_Reject_Qty) * 1.0 / Pq_RM_Plan_Qty) * 100 AS DECIMAL(10, 2)) AS [Productivity %],
            //    CAST((PD_OK_Qty * 1.0) / NULLIF((PD_OK_Qty + PD_Reject_Qty), 0) * 100 AS DECIMAL(10, 2)) AS [Quality %],
            //    CAST(    (PD_Reject_Qty * 1.0) / NULLIF((PD_OK_Qty + PD_Reject_Qty), 0) * 100 AS DECIMAL(10, 2)) AS [rej %],
            //    (PD_Reject_Qty * IM_Sales_Price) as [Rej Price], FORMAT(PD_Date, 'dd-MM-yyyy') AS [Date], '' as none1 

            //    from  Production_Details 

            //    left outer join  production_request on PD_Route_Card_ID = Pq_iid   
            //    left outer  join machine_master on Pq_Machine_ID = MM_ID
            //    left outer join item_master on im_id = Pq_iPart_ID  
            //    left outer join Mold_Master on MLD_ID = Pq_Mould_Id 
            //    left outer join Production_idl_resons on PD_Route_Card_ID=idl_rcard
            //    WHERE            
            //      CONVERT(DATE, PD_Date, 114) BETWEEN '" + fromDate + "' AND '" + toDate + "' group by idl_fromtime,idl_totime,MM_MachineCode,MLD_Cycle_Time, PD_Shift, Pq_vPart_No, IM_Model, Pq_RM_Grade, MLD_MoldCavity, IM_Sales_Price, txt_Hourly_Pro, Pq_Route_Card_No, Pq_RM_Plan_Qty, PD_OK_Qty, PD_Date, PD_Reject_Qty,PD_time");

            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            calculation();
            //CAST((((Pq_RM_Plan_Qty - (txt_Hourly_Pro * 12)) / txt_Hourly_Pro) + 12) AS DECIMAL(10, 2)) AS TIMING,
        }

        private void stock1()
        {
            DataTable dt = dbFunctions.getTable("select '' as no, MM_MachineName as [Machine name],Pq_Route_Card_No as [RouteCardNo],PD_Shift as [Shift], Pq_RM_Grade as [MATERIAL NAME ],IM_Model as [Model], Pq_vPart_No as [PART NAME], MLD_MoldCavity as [NO.OF CAVITIES],IM_Sales_Price as [Cost Part],CAST((((Pq_RM_Plan_Qty - (txt_Hourly_Pro * 12)) / txt_Hourly_Pro) + 12) AS DECIMAL(10, 2)) AS TIMING,((Pq_RM_Plan_Qty * 1)/txt_Hourly_Pro)  as [PLAN],Pq_RM_Plan_Qty as [TARGET qty],(PD_OK_Qty + PD_Reject_Qty) as [production Qty],PD_OK_Qty as [OK qty],PD_Reject_Qty as [TOTAL REJ], CAST(((PD_OK_Qty + PD_Reject_Qty) * 1.0 / Pq_RM_Plan_Qty) * 100 AS DECIMAL(10, 2)) AS [Productivity %],CAST((PD_OK_Qty * 1.0) / NULLIF((PD_OK_Qty + PD_Reject_Qty), 0) * 100 AS DECIMAL(10, 2)) AS [Quality %],CAST(    (PD_Reject_Qty * 1.0) / NULLIF((PD_OK_Qty + PD_Reject_Qty), 0) * 100 AS DECIMAL(10, 2)) AS [rej %],(PD_Reject_Qty * IM_Sales_Price) as [sales Price], FORMAT(PD_Date, 'dd-MM-yyyy') AS [Date], '' as none1 from production_request left outer  join machine_master on Pq_Machine_ID = MM_ID left outer join  Production_Details on PD_Route_Card_ID = Pq_iid  left outer join item_master on im_id = Pq_iPart_ID  left outer join Mold_Master on MLD_ID = Pq_Mould_Id group by MM_MachineName, PD_Shift, Pq_vPart_No, IM_Model, Pq_RM_Grade, MLD_MoldCavity, IM_Sales_Price, txt_Hourly_Pro,Pq_Route_Card_No,Pq_RM_Plan_Qty, PD_OK_Qty,PD_Date,PD_Reject_Qty");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            calculation();

        }

        private void calculation()
        {
            //decimal totay_qty1 = 0.0m;

            //decimal totay_rec = 0.0m;

            //decimal Sub_Total = 0.0m;
            //decimal Grand_Total = 0.0m;
            //decimal REJ_PRICE = 0.0m;

            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    totay_qty1 += decimal.Parse(dataGridView1.Rows[i].Cells["TARGET qty"].Value.ToString());
            //    totay_rec += decimal.Parse(dataGridView1.Rows[i].Cells["production Qty"].Value.ToString());
            //    Sub_Total += decimal.Parse(dataGridView1.Rows[i].Cells["OK qty"].Value.ToString());
            //    Grand_Total += decimal.Parse(dataGridView1.Rows[i].Cells["Remaining_Time"].Value.ToString());
            //    REJ_PRICE += decimal.Parse(dataGridView1.Rows[i].Cells["Production TIMING"].Value.ToString());

            //}
            //txtgrandtotal.Text = totay_qty1.ToString();
            //label4.Text = totay_rec.ToString();
            //label5.Text = Sub_Total.ToString();
            //label6.Text = Grand_Total.ToString();
            //label12.Text = REJ_PRICE.ToString();
            decimal totay_qty1 = 0.0m;
            decimal totay_rec = 0.0m;
            decimal Sub_Total = 0.0m;
            decimal Grand_Total = 0.0m; // Stores total minutes
            decimal REJ_PRICE = 0.0m;   // Stores total minutes
            decimal REJ_qty = 0.0m; 

            decimal LUMPS = 0.0m;
            decimal REJ_VALUE = 0.0m;
            decimal IDLTIME = 0.0m;


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["TARGET qty"].Value != null && decimal.TryParse(row.Cells["TARGET qty"].Value.ToString(), out decimal targetQty))
                    totay_qty1 += targetQty;

                if (row.Cells["production Qty"].Value != null && decimal.TryParse(row.Cells["production Qty"].Value.ToString(), out decimal prodQty))
                    totay_rec += prodQty;

                if (row.Cells["OK qty"].Value != null && decimal.TryParse(row.Cells["OK qty"].Value.ToString(), out decimal ok_Qty))
                    Sub_Total += ok_Qty;


                if (row.Cells["TOTAL REJ"].Value != null && decimal.TryParse(row.Cells["TOTAL REJ"].Value.ToString(), out decimal rej_Qty1))
                    REJ_qty += rej_Qty1;
           



                if (row.Cells["Remaining_Time"].Value != null && TimeSpan.TryParse(row.Cells["Remaining_Time"].Value.ToString(), out TimeSpan remainingTime))
                    Grand_Total += (decimal)remainingTime.TotalMinutes; // Convert to minutes

                if (row.Cells["Production TIMING"].Value != null && TimeSpan.TryParse(row.Cells["Production TIMING"].Value.ToString(), out TimeSpan prodTime))
                    REJ_PRICE += (decimal)prodTime.TotalMinutes; // Convert to minutes

                //lumps

                if (row.Cells["Lumps"].Value != null && decimal.TryParse(row.Cells["Lumps"].Value.ToString(), out decimal Lumps1))
                    LUMPS += Lumps1;

                // rej value

                if (row.Cells["Rej Value"].Value != null && decimal.TryParse(row.Cells["Rej Value"].Value.ToString(), out decimal RejValue1))
                    REJ_VALUE += RejValue1;

                // idl time

                if (row.Cells["IDL_time_range"].Value != null && TimeSpan.TryParse(row.Cells["IDL_time_range"].Value.ToString(), out TimeSpan idltime1))
                    IDLTIME += (decimal)idltime1.TotalMinutes; // Convert to minutes


            }

            // Convert total minutes back to HH:mm format
            TimeSpan totalRemaining = TimeSpan.FromMinutes((double)Grand_Total);
            TimeSpan totalProduction = TimeSpan.FromMinutes((double)REJ_PRICE);

            TimeSpan idltime123 = TimeSpan.FromMinutes((double)IDLTIME);

            // Assign values to labels
            txtgrandtotal.Text = totay_qty1.ToString();

            label24.Text = LUMPS.ToString();
            label22.Text = REJ_VALUE.ToString();
           // label20.Text = IDLTIME.ToString();
            label20.Text = $"{(int)idltime123.TotalHours:D2}:{idltime123.Minutes:D2}";

            label4.Text = totay_rec.ToString();
            label5.Text = Sub_Total.ToString();
            label16.Text = REJ_qty.ToString();
            label6.Text = $"{(int)totalRemaining.TotalHours:D2}:{totalRemaining.Minutes:D2}"; // HH:mm format
            label12.Text = $"{(int)totalProduction.TotalHours:D2}:{totalProduction.Minutes:D2}"; // HH:mm format

            // ✅ Convert label6.Text (time format) to total minutes
            decimal totalTime = 0;
            if (!string.IsNullOrWhiteSpace(label6.Text) && label6.Text.Contains(":"))
            {
                string[] timeParts = label6.Text.Split(':');
                if (timeParts.Length == 2 && int.TryParse(timeParts[0], out int hours) && int.TryParse(timeParts[1], out int minutes))
                {
                    totalTime = (hours * 60) + minutes; // Convert to total minutes
                }
            }
            // rej qty %
            decimal rej1= decimal.TryParse(label16.Text, out decimal tQty1) ? tQty1 : 0;

            // ✅ Convert label12.Text (time format) to total minutes
            decimal prod_Time = 0;
            if (!string.IsNullOrWhiteSpace(label12.Text) && label12.Text.Contains(":"))
            {
                string[] timeParts = label12.Text.Split(':');
                if (timeParts.Length == 2 && int.TryParse(timeParts[0], out int hours) && int.TryParse(timeParts[1], out int minutes))
                {
                    prod_Time = (hours * 60) + minutes; // Convert to total minutes
                }
            }
            
            // ✅ Parsing numbers from textboxes
            decimal totalQty = decimal.TryParse(txtgrandtotal.Text, out decimal tQty) ? tQty : 0;
            decimal prod_Quantity = decimal.TryParse(label4.Text, out decimal pQty) ? pQty : 0;
            decimal ok_Quantity = decimal.TryParse(label5.Text, out decimal oQty) ? oQty : 0;

            // ✅ Ensure no division by zero
            decimal PR = (totalQty > 0) ? (prod_Quantity / totalQty) * 100 : 0;
            decimal QU = (prod_Quantity > 0) ? (ok_Quantity / prod_Quantity) * 100 : 0;
            decimal TK = (prod_Time > 0) ? (totalTime / prod_Time) * 100 : 0;

            // ✅ Final Calculation
            decimal result = (PR * QU * TK) / 10000;


            //decimal(18,2) a2 = (rej1 / prod_Quantity) * 100;
            //decimal a2 = Math.Round((rej1 / prod_Quantity) * 100, 2);
            decimal a2 = (prod_Quantity != 0) ? Math.Round((rej1 / prod_Quantity) * 100, 2) : 0;

            label18.Text = a2.ToString();

            // ✅ Assign to label
            label13.Text = result.ToString("0.00");

            // ✅ Debugging
            //MessageBox.Show($"Total Time (Minutes): {totalTime}\nProduction Time (Minutes): {prod_Time}");
            //MessageBox.Show($"PR: {PR}, QU: {QU}, TK: {TK}, Final: {result}");


        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
        bool search1 = true;
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Getstock1();
             search1 = true;

            calculation();
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;

            //Cursor.Current = Cursors.WaitCursor;
            //dbFunctions.ExportDataGridViewToExcel(dataGridView1);
            //Cursor.Current = Cursors.Default;


        }

        private void Button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBoxX1_TextChanged(object sender, EventArgs e)
        {

            //try
            //{

            //    if (string.IsNullOrEmpty(textBoxX1.Text))
            //    {
            //        (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            //    }
            //    else
            //    {
            //        (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Machine Code] LIKE '%{0}%' or [RouteCardNo] LIKE '%{0}%' or  [Model] LIKE '%{0}%'  or  [PART NAME] LIKE '%{0}%'  or  [Plant NAME] LIKE '%{0}%' ", textBoxX1.Text);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

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

                        if (dt.Columns.Contains("Machine Code"))
                            filters.Add($"CONVERT([Machine Code], System.String) LIKE '%{filterText}%'");

                        if (dt.Columns.Contains("RouteCardNo"))
                            filters.Add($"CONVERT([RouteCardNo], System.String) LIKE '%{filterText}%'");

                        if (dt.Columns.Contains("Model"))
                            filters.Add($"CONVERT([Model], System.String) LIKE '%{filterText}%'");

                        if (dt.Columns.Contains("PART NAME"))
                            filters.Add($"CONVERT([PART NAME], System.String) LIKE '%{filterText}%'");

                        if (dt.Columns.Contains("Plant NAME"))
                            filters.Add($"CONVERT([Plant NAME], System.String) LIKE '%{filterText}%'");

                        if (filters.Count > 0)
                            dt.DefaultView.RowFilter = string.Join(" OR ", filters);
                        else
                            dt.DefaultView.RowFilter = string.Empty; // No matching columns found
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering: " + ex.Message);
            }
            calculation();
            //if (search1==true)
            //{
            //    calculation();
            //}

        }

        private void button9_Click(object sender, EventArgs e)
        {
            print_Bill();
            //if (dataGridView1.SelectedRows.Count > 0)
            //{
            //    //print_Bill(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());




            //}
            //else
            //{
            //    MessageBox.Show("Please Select one Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
        }
        //public void print_Bill(date s)
        public void print_Bill()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Production_wise_dashbord_desigh oRpt = new Production_wise_dashbord_desigh();
                string SQlQuery = "poverall_report1  '" + To_Date.Value.ToString("yyyyMMdd") + "' ";
                //dbFunctions.printpdf(s, SQlQuery, oRpt);
                dbFunctions.printpdf("s",SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {

            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  DataTable dt = dbFunctions.getTable("poverall_report_GM '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "' ");
            DataTable dt = dbFunctions.getTable("poverall_report '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "' ");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            dataGridView1.Columns["Production TIMING"].Visible = false;
            dataGridView1.Columns["Remaining_Time"].Visible = false;
            dataGridView1.Columns["TARGET qty"].Visible = false;
            dataGridView1.Columns["Lumps"].Visible = false;
            dataGridView1.Columns["IDL_time_range"].Visible = false;
            dataGridView1.Columns["Shift"].Visible = false;
            dataGridView1.Columns["MATERIAL NAME"].Visible = false;
            dataGridView1.Columns["Mold No"].Visible = false;
            dataGridView1.Columns["Employee Name"].Visible = false;
            dataGridView1.Columns["Part No"].Visible = false; 
            
            dataGridView1.Columns["NO.OF CAVITIES"].Visible = false;
            dataGridView1.Columns["Cost Part"].Visible = false;
            dataGridView1.Columns["TIMING"].Visible = false; 
            
            dataGridView1.Columns["Productivity %"].Visible = false;
            dataGridView1.Columns["Quality %"].Visible = false; 
            dataGridView1.Columns["Machine_Utility"].Visible = false; 
            dataGridView1.Columns["Date"].Visible = false;
            
            dataGridView1.Columns["RouteCardNo"].Visible = false;




            // dataGridView1.Columns["Rej Value"].Visible = false;    
            calculation();
             search1 = false;

        }
    }
}
