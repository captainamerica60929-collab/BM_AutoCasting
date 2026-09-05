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
    public partial class RM_MATERIAL_RECONCILATION : Form
    {
        public RM_MATERIAL_RECONCILATION()
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
            //DataTable dt = dbFunctions.getTable("pr_getCurrent_Assy_Stock");//  '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            //dataGridView1.DataSource = dt;
            //dbFunctions.DGVStyle(dataGridView1);
            //dataGridView1.Columns[0].Visible = true;
            //dataGridView1.Columns[0].Width = 150;
            //txt_Rows.Text = dbFunctions.getRows(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();

            //calc();
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
            FINAL();
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }

      //  public void FINAL()
   
            public DataTable FINAL()
        {
            string fromDate = dtpFrom.Value.ToString("yyyyMMdd");
            string toDate = todate.Value.ToString("yyyyMMdd");


            // Load Sales Invoice Details (today)
            DataTable dt1 = dbFunctions.getTable1(@"
                SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
                FROM dbo.Sales_Invoice_Details
                WHERE SD_Status = 'A' 
                  AND CONVERT(DATE, SD_Created_Date, 112) 
                  BETWEEN '" + fromDate + @"' AND '" + toDate + @"' 
                  AND CONVERT(date, SD_Created_Date, 112) >= '20250526'
                GROUP BY SD_Part_No");



            // Load Sales Invoice Details (before today)
            DataTable dt2 = dbFunctions.getTable1(@"
            SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
            FROM dbo.Sales_Invoice_Details
            WHERE SD_Status = 'A' 
              AND CONVERT(DATE, SD_Created_Date, 112) 
              < '" + fromDate + @"' AND CONVERT(date, SD_Created_Date, 112) >= '20250526'
            GROUP BY SD_Part_No");




            DataTable dt = dbFunctions.getTable(@"SELECT
            0 AS ID,
             IM_ID as  Id,          
        IM_PartName AS [Part Name],          
        IM_PartNo AS [Part No], 
           -- 0 AS DESCRIPTION,
                SUM(CASE WHEN convert(date,CS_CreatedDate,113) < '" + fromDate + @"'  AND CS_Type = 3 THEN CS_Qty ELSE 0 END) AS [RM IN KGS],

      CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName AND Pq_Status='A'    
                WHERE PD_Route_Card_ID = Pq_iid AND PD_Status='A'    
                  AND CONVERT(DATE, PD_Date, 113) < '" + fromDate + @"'), 0) AS INT)
     +
			  CAST(ISNULL((SELECT SUM(CS_Qty)
             FROM CURRENT_STOCK
            WHERE CS_Item_ID = IM_ID  and CS_Status = 'A' AND CS_Type=1
            AND CONVERT(DATE, CS_CreatedDate, 112) < '" + fromDate + @"'), 0) AS INT)
                as  [yesterday.wip],

              CAST(ISNULL((SELECT SUM(fe_rej)
            FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2)) as [yesterday.fgrej],


            CAST(ISNULL((SELECT SUM(fe_ok)
             FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2))        


            as [yesterday.FGok],

                
            CAST(ISNULL((SELECT SUM(wor_Qty)
             FROM Work_Order_Receive_Details
            WHERE WOR_PID = IM_ID  and wor_status = 'A' and wor_MaterialType='FG'
            AND CONVERT(DATE, wor_Date, 112)< '" + fromDate + @"' ), 0) AS DECIMAL(18,2)) AS [YES dc],



                BM_Net_Part_Wt,
              0 as [wip opening],
              0 as [fg opening],
              0 AS [Opening Stock],

            0 AS [KGS],
           -- 0 AS [FG IN RM IN KGS],
CAST(0 AS DECIMAL(18,2)) AS [FG IN RM IN KGS],

BM_ItemId as [RM ID],
           -- 0 AS [TOTAL],
    CAST(0 AS DECIMAL(18,2)) AS [TOTAL],
            IM_Purchase_Price AS [RM PRICE],
                 (
        SELECT ISNULL(SUM(GRND_dTotal_Qty), 0)
        FROM GRN
        LEFT JOIN GRN_Details ON GRND_iGRN_No = GRN_iID AND GRND_cStatus = 'A'
        LEFT JOIN Purchase_Order ON PO_iID = GRN_iPO_NO
        WHERE GRND_iItem = im_id AND  convert(varchar,GRN_dGRN_Date,112)>='" + fromDate + @"'            
            and convert(varchar,GRN_dGRN_Date,112)<='" + toDate + @"'   and GRN_dStatus='A' 
    ) AS  [RM PURCHASE IN KGS],
            0 AS [KGS],
             CAST(ISNULL((SELECT SUM(wos_Qty)
             FROM Work_Order_Send_Details
            WHERE wos_MaterialId = IM_ID  and wos_status = 'A' 
            AND CONVERT(DATE, wos_date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'  ), 0) AS DECIMAL(18,2))  AS [JO SALES IN KGS],
                CAST(0 AS DECIMAL(18,2)) AS [RM USAGE (IN-HOUSE) KGS],
                CAST(0 AS DECIMAL(18,2)) AS [FG QTY],
                CAST(0 AS DECIMAL(18,2)) AS [FG SALES IN KGS],
                CAST(0 AS DECIMAL(18,2)) AS [CLOSING STOCK AS PER BOOK],
            --(((SUM(CASE WHEN convert(date,CS_CreatedDate,113) < '" + fromDate + @"'   AND CS_Type = 3 THEN CS_Qty ELSE 0 END))+(SUM(CASE WHEN convert(date,CS_CreatedDate,113)  = '" + fromDate + @"'   AND CS_Trans_type = 'GRN Inward' AND CS_Type = 3 THEN CS_Qty ELSE 0 END)))      
               --  -(SUM(CASE WHEN CONVERT(DATE, CS_CreatedDate, 113) = '" + fromDate + @"'   AND CS_Trans_type = 'Material Issue' AND CS_Type = 3  THEN ABS(CS_Qty) ELSE 0 END) )) AS [CL RM IN KGS],

             (((SUM(CASE WHEN convert(date,CS_CreatedDate,113) < '" + fromDate + @"'  AND CS_Type = 3 THEN CS_Qty ELSE 0 END))+(SUM(CASE WHEN convert(date,CS_CreatedDate,113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"' AND CS_Trans_type = 'GRN Inward' AND CS_Type = 3 THEN CS_Qty ELSE 0 END)))        
  -(SUM(CASE WHEN CONVERT(DATE, CS_CreatedDate, 113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"' AND CS_Trans_type = 'Material Issue' AND CS_Type = 3  THEN ABS(CS_Qty) ELSE 0 END) ))  AS [CL RM IN KGS],



                  CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName AND Pq_Status='A'    
                WHERE PD_Route_Card_ID = Pq_iid AND PD_Status='A'    
                  AND CONVERT(DATE, PD_Date, 113)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT) 
            +
			  CAST(ISNULL((SELECT SUM(CS_Qty)
             FROM CURRENT_STOCK
            WHERE CS_Item_ID = IM_ID  and CS_Status = 'A' AND CS_Type=1
            AND CONVERT(DATE, CS_CreatedDate, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
            as  [prod qty],


            0 AS [total wip stock],

            CAST(ISNULL((SELECT SUM(fe_ok)
             FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2))
              as [Final inspection ok qty],

              CAST(ISNULL((SELECT SUM(fe_rej)
            FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) as [Final inspection rej qty],
                CAST(0 AS DECIMAL(18,2)) AS [CL KGS],

             CAST(ISNULL((SELECT SUM(wor_Qty)
             FROM Work_Order_Receive_Details
            WHERE WOR_PID = IM_ID  and wor_status = 'A' and wor_MaterialType='FG'
            AND CONVERT(DATE, wor_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) AS [dc],
                CAST(0 AS DECIMAL(18,2)) AS [fg closing],
                CAST(0 AS DECIMAL(18,2)) AS [Invoice],
                CAST(0 AS DECIMAL(18,2)) AS [closing],
                CAST(0 AS DECIMAL(18,2)) AS [wip closing stock],
                CAST(0 AS DECIMAL(18,2)) AS [CL FG IN RM IN KGS],
                CAST(0 AS DECIMAL(18,2)) AS [TOTAL1],
                CAST(0 AS DECIMAL(18,2)) AS [SHORTAGE OR EXCESS],
                CAST(0 AS DECIMAL(18,2)) AS [DIFF VALUE],
                CAST(0 AS DECIMAL(18,2)) as [yes_invoice],
                CAST(0 AS DECIMAL(18,2)) AS [REMARKS]

          FROM Item_Master           
    LEFT OUTER JOIN CURRENT_STOCK ON IM_ID = CS_Item_ID  
     left outer join bom_master on bm_bomid=IM_ID AND BM_Status='A' 
   where CS_Type != 3    
    GROUP BY IM_PartName, IM_PartNo,IM_ID,BM_Net_Part_Wt,IM_Purchase_Price , BM_ItemId  order by IM_PartName asc  

");

            foreach (DataRow dr in dt.Rows)
            {
                decimal PRO = Convert.ToDecimal(dr["yesterday.wip"]);
                decimal tfg = Convert.ToDecimal(dr["yesterday.FGok"]);
                decimal tfgR = Convert.ToDecimal(dr["yesterday.fgrej"]);
                decimal tocl = PRO - tfg - tfgR;
                dr["wip opening"] = tocl;
            }
            foreach (DataRow dr in dt.Rows)
            {

                string partNo = dr["Part No"].ToString();

                DataRow[] invoiceMatch = dt2.Select("SD_Part_No = '" + partNo + "'");
                decimal pin = invoiceMatch.Length > 0 ? Convert.ToDecimal(invoiceMatch[0]["Qty"]) : 0;

                decimal yfg = Convert.ToDecimal(dr["yesterday.FGok"]);

                decimal dc = Convert.ToDecimal(dr["YES dc"]);

                decimal fgClosing = yfg - pin + dc;
                dr["fg Opening"] = fgClosing;
            }
            foreach (DataRow dr in dt.Rows)
            {

                decimal tocl = Convert.ToDecimal(dr["wip opening"]);
                decimal fgClosing = Convert.ToDecimal(dr["fg opening"]);
                dr["Opening Stock"] = tocl + fgClosing;
            }
            //foreach (DataRow dr in dt.Rows)
            //{
            //    decimal fgo = dr["BM_Net_Part_Wt"] != DBNull.Value ? Convert.ToDecimal(dr["BM_Net_Part_Wt"]) : 0m;
            //    decimal tfg = dr["Opening Stock"] != DBNull.Value ? Convert.ToDecimal(dr["Opening Stock"]) : 0m;

            //   // decimal tocl = Math.Round((fgo * tfg) / 1000, 2);  // round to 2 decimals
            //    decimal tocl = (fgo * tfg) / 1000;
            //    //dr["FG IN RM IN KGS"] = tocl;
            //    dr["FG IN RM IN KGS"] = tocl.ToString("0.000");

            //}
            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = dr["BM_Net_Part_Wt"] != DBNull.Value ? Convert.ToDecimal(dr["BM_Net_Part_Wt"]) : 0m;
                decimal tfg = dr["Opening Stock"] != DBNull.Value ? Convert.ToDecimal(dr["Opening Stock"]) : 0m;

                decimal tocl = (fgo * tfg) / 1000;
                dr["FG IN RM IN KGS"] = Convert.ToDecimal(tocl);

                // Option A: For decimal column
                // dr["FG IN RM IN KGS"] = tocl;

                // Option B: For string column with precision formatting
               // dr["FG IN RM IN KGS"] = tocl.ToString("0.000000"); // 6 decimal places
            }

            foreach (DataRow dr in dt.Rows)
            {
               
                decimal tocl = Convert.ToDecimal(dr["RM IN KGS"]);
                decimal FGRM = Convert.ToDecimal(dr["FG IN RM IN KGS"]);
                dr["TOTAL"] = tocl + FGRM;
            }
           

            foreach (DataRow dr in dt.Rows)
            {

                decimal tocl = Convert.ToDecimal(dr["RM PURCHASE IN KGS"]);
                decimal FGRM = Convert.ToDecimal(dr["JO SALES IN KGS"]);
                dr["RM USAGE (IN-HOUSE) KGS"] = tocl - FGRM;
            }

            // FG ALES IN KGS
            foreach (DataRow dr in dt.Rows)
            {
                //string partNo = dr["Part No"].ToString();
                //DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                //dr["FG QTY"] = match.Length > 0 ? match[0]["Qty"] : 0;

                //decimal tocl = Math.Round((fgo * tfg) / 1000, 2);  // round to 2 decimals
                //dr["FG IN RM IN KGS"] = tocl;


                string partNo = dr["Part No"].ToString();

                // Match the part number in dt1
                DataRow[] match = dt1.Select($"[SD_Part_No] = '{partNo}'");

                // Safely extract FG QTY
                decimal fgQty = 0;
                if (match.Length > 0 && match[0]["Qty"] != DBNull.Value)
                {
                    fgQty = Convert.ToDecimal(match[0]["Qty"]);
                }
                dr["FG QTY"] = fgQty;

                // Calculate FG IN RM IN KGS
                //decimal tocl = Math.Round(fgQty / 1000, 2); // Divide by 1000 and round to 2 decimals
                //dr["FG IN RM IN KGS"] = tocl;


                decimal fgo = dr["BM_Net_Part_Wt"] != DBNull.Value ? Convert.ToDecimal(dr["BM_Net_Part_Wt"]) : 0m;
                decimal tfg = dr["FG QTY"] != DBNull.Value ? Convert.ToDecimal(dr["FG QTY"]) : 0m;

                decimal tocl1 = Math.Round((fgo * tfg) / 1000, 2);  // round to 2 decimals
              //dr["FG IN RM IN KGS"] = tocl1;

            }

            foreach (DataRow dr in dt.Rows)
            {
                decimal tfgR = Convert.ToDecimal(dr["wip opening"]);
                decimal tfD = Convert.ToDecimal(dr["prod qty"]);

                decimal tocl = tfD + tfgR;
                dr["total wip stock"] = tocl;
            }
            foreach (DataRow dr in dt.Rows)
            {
                decimal PRO = Convert.ToDecimal(dr["total wip stock"]);
                decimal tfg = Convert.ToDecimal(dr["Final inspection ok qty"]);
                decimal tfgr = Convert.ToDecimal(dr["Final inspection rej qty"]);
                decimal tocl = PRO - tfg - tfgr;

                dr["wip closing stock"] = tocl;

            }
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                dr["invoice"] = match.Length > 0 ? match[0]["Qty"] : 0;
            }

            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt2.Select("[SD_Part_No] = '" + partNo + "'");
                dr["yes_invoice"] = match.Length > 0 ? match[0]["Qty"] : 0;
            }


            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = dr["BM_Net_Part_Wt"] != DBNull.Value ? Convert.ToDecimal(dr["BM_Net_Part_Wt"]) : 0m;
                decimal tfg = dr["invoice"] != DBNull.Value ? Convert.ToDecimal(dr["invoice"]) : 0m;

                decimal tocl = Math.Round((tfg * fgo) / 1000, 2);  // round to 2 decimals
                dr["FG SALES IN KGS"] = tocl;

            }

            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = Convert.ToDecimal(dr["fg Opening"]);
                decimal tfg = Convert.ToDecimal(dr["Final inspection ok qty"]);
                decimal dc = Convert.ToDecimal(dr["dc"]);
                decimal inv = Convert.ToDecimal(dr["Invoice"]);

                decimal tocl = (fgo + tfg + dc) - inv;

                dr["fg closing"] = tocl;
            }
            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = Convert.ToDecimal(dr["wip closing stock"]);
                decimal tfg = Convert.ToDecimal(dr["fg closing"]);

                decimal tocl = (fgo + tfg);
                dr["closing"] = tocl;

            }
            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = dr["BM_Net_Part_Wt"] != DBNull.Value ? Convert.ToDecimal(dr["BM_Net_Part_Wt"]) : 0m;
                decimal tfg = dr["closing"] != DBNull.Value ? Convert.ToDecimal(dr["closing"]) : 0m;

                decimal tocl = Math.Round((fgo * tfg) / 1000, 2);  // round to 2 decimals
                dr["CL FG IN RM IN KGS"] = tocl;

            }

            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = Convert.ToDecimal(dr["CL RM IN KGS"]);
                decimal tfg = Convert.ToDecimal(dr["FG IN RM IN KGS"]);

                decimal tocl = (fgo + tfg);
                dr["TOTAL1"] = tocl;

            }


            //foreach (DataRow dr in dt.Rows)
            //{
            //    decimal fgo = Convert.ToDecimal(dr["TOTAL"]);
            //    decimal tfg = Convert.ToDecimal(dr["CLOSING STOCK AS PER BOOK"]);

            //    decimal tocl = (fgo - tfg);
            //    dr["CLOSING STOCK AS PER BOOK"] = tocl;

            //}

            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = Convert.ToDecimal(dr["TOTAL"]);
                decimal tfg = Convert.ToDecimal(dr["RM USAGE (IN-HOUSE) KGS"]);

                decimal fgo1 = Convert.ToDecimal(dr["FG SALES IN KGS"]);
                decimal tfg1 = Convert.ToDecimal(dr["JO SALES IN KGS"]);


                decimal tocl = (fgo + tfg)- (fgo1 + tfg1);
                dr["CLOSING STOCK AS PER BOOK"] = tocl;

            }
            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = Convert.ToDecimal(dr["TOTAL"]);
                decimal tfg = Convert.ToDecimal(dr["CLOSING STOCK AS PER BOOK"]);

                decimal tocl = (fgo + tfg);
                dr["SHORTAGE OR EXCESS"] = tocl;

            }

            foreach (DataRow dr in dt.Rows)
            {
                //decimal fgo = Convert.ToDecimal(dr["SHORTAGE OR EXCESS"]);
                //decimal tfg = Convert.ToDecimal(dr["RM PRICE"]);
                decimal fgo = dr["SHORTAGE OR EXCESS"] != DBNull.Value && !string.IsNullOrWhiteSpace(dr["SHORTAGE OR EXCESS"].ToString())
              ? Convert.ToDecimal(dr["SHORTAGE OR EXCESS"])
              : 0m;

                decimal tfg = dr["RM PRICE"] != DBNull.Value && !string.IsNullOrWhiteSpace(dr["RM PRICE"].ToString())
                              ? Convert.ToDecimal(dr["RM PRICE"])
                              : 0m; 

                decimal tocl = (fgo * tfg);
                dr["DIFF VALUE"] = tocl;

            }

            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            dataGridView1.Columns["yesterday.FGok"].Visible = false;
            dataGridView1.Columns["yesterday.fgrej"].Visible = false;
            dataGridView1.Columns["yesterday.wip"].Visible = false;
            //dataGridView1.Columns["yes wip"].Visible = false;
            //dataGridView1.Columns["yes fg"].Visible = false;
            dataGridView1.Columns["YES dc"].Visible = false;

            dataGridView1.Columns["Opening Stock"].Visible = false;
            dataGridView1.Columns["BM_Net_Part_Wt"].Visible = false;
            dataGridView1.Columns["fg opening"].Visible = false;
            dataGridView1.Columns["wip opening"].Visible = false;
            dataGridView1.Columns["FG QTY"].Visible = false;

            dataGridView1.Columns["dc"].Visible = false;
            dataGridView1.Columns["prod qty"].Visible = false;
            dataGridView1.Columns["total wip stock"].Visible = false;
            dataGridView1.Columns["Final inspection ok qty"].Visible = false;
            dataGridView1.Columns["Final inspection rej qty"].Visible = false;
            dataGridView1.Columns["fg closing"].Visible = false;
            dataGridView1.Columns["Invoice"].Visible = false;
            dataGridView1.Columns["closing"].Visible = false;

            dataGridView1.Columns["wip closing stock"].Visible = false;
            dataGridView1.Columns["closing"].Visible = false;
            dataGridView1.Columns["closing"].Visible = false;
            return dt;


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
            //calc();
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
            ASD();
        }

        // public void ASD()
        public DataTable ASD()
        {
           
            string fromDate = dtpFrom.Value.ToString("yyyyMMdd");
            string toDate = todate.Value.ToString("yyyyMMdd");


            // Load Sales Invoice Details (today)
            DataTable dt1 = dbFunctions.getTable1(@"
                SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
                FROM dbo.Sales_Invoice_Details
                WHERE SD_Status = 'A' 
                  AND CONVERT(DATE, SD_Created_Date, 112) 
                  BETWEEN '" + fromDate + @"' AND '" + toDate + @"' 
                  AND CONVERT(date, SD_Created_Date, 112) >= '20250526'
                GROUP BY SD_Part_No");



            // Load Sales Invoice Details (before today)
            DataTable dt2 = dbFunctions.getTable1(@"
            SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
            FROM dbo.Sales_Invoice_Details
            WHERE SD_Status = 'A' 
              AND CONVERT(DATE, SD_Created_Date, 112) 
              < '" + fromDate + @"' AND CONVERT(date, SD_Created_Date, 112) >= '20250526'
            GROUP BY SD_Part_No");




            DataTable dt = dbFunctions.getTable(@"SELECT
            0 AS ID,
             IM_ID as  Id,          
        IM_PartName AS [Part Name],          
        IM_PartNo AS [Part No], 
           -- 0 AS DESCRIPTION,
                SUM(CASE WHEN convert(date,CS_CreatedDate,113) < '" + fromDate + @"'  AND CS_Type = 3 THEN CS_Qty ELSE 0 END) AS [RM IN KGS],

      CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName AND Pq_Status='A'    
                WHERE PD_Route_Card_ID = Pq_iid AND PD_Status='A'    
                  AND CONVERT(DATE, PD_Date, 113) < '" + fromDate + @"'), 0) AS INT)
     +
			  CAST(ISNULL((SELECT SUM(CS_Qty)
             FROM CURRENT_STOCK
            WHERE CS_Item_ID = IM_ID  and CS_Status = 'A' AND CS_Type=1
            AND CONVERT(DATE, CS_CreatedDate, 112) < '" + fromDate + @"'), 0) AS INT)
                as  [yesterday.wip],

              CAST(ISNULL((SELECT SUM(fe_rej)
            FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2)) as [yesterday.fgrej],


            CAST(ISNULL((SELECT SUM(fe_ok)
             FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2))        


            as [yesterday.FGok],

                
            CAST(ISNULL((SELECT SUM(wor_Qty)
             FROM Work_Order_Receive_Details
            WHERE WOR_PID = IM_ID  and wor_status = 'A' and wor_MaterialType='FG'
            AND CONVERT(DATE, wor_Date, 112)< '" + fromDate + @"' ), 0) AS DECIMAL(18,2)) AS [YES dc],



                BM_Net_Part_Wt,
                  CAST(0 AS DECIMAL(18,2)) as [wip opening],
                  CAST(0 AS DECIMAL(18,2)) as [fg opening],
                  CAST(0 AS DECIMAL(18,2)) AS [Opening Stock],

                CAST(0 AS DECIMAL(18,2)) AS [KGS],
           -- 0 AS [FG IN RM IN KGS],
CAST(0 AS DECIMAL(18,2)) AS [FG IN RM IN KGS],

BM_ItemId as [RM ID],
           -- 0 AS [TOTAL],
CAST(0 AS DECIMAL(18,2)) AS [TOTAL],
            IM_Purchase_Price AS [RM PRICE],
                 (
        SELECT ISNULL(SUM(GRND_dTotal_Qty), 0)
        FROM GRN
        LEFT JOIN GRN_Details ON GRND_iGRN_No = GRN_iID AND GRND_cStatus = 'A'
        LEFT JOIN Purchase_Order ON PO_iID = GRN_iPO_NO
        WHERE GRND_iItem = im_id AND  convert(varchar,GRN_dGRN_Date,112)>='" + fromDate + @"'            
            and convert(varchar,GRN_dGRN_Date,112)<='" + toDate + @"'   and GRN_dStatus='A' 
    ) AS  [RM PURCHASE IN KGS],
                CAST(0 AS DECIMAL(18,2)) AS [KGS],
                    CAST(ISNULL((SELECT SUM(wos_Qty)
             FROM Work_Order_Send_Details
            WHERE wos_MaterialId = IM_ID  and wos_status = 'A' 
            AND CONVERT(DATE, wos_date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'  ), 0) AS DECIMAL(18,2))  AS [JO SALES IN KGS],
                CAST(0 AS DECIMAL(18,2)) AS [RM USAGE (IN-HOUSE) KGS],
                CAST(0 AS DECIMAL(18,2)) AS [FG QTY],
                CAST(0 AS DECIMAL(18,2)) AS [FG SALES IN KGS],
                CAST(0 AS DECIMAL(18,2)) AS [CLOSING STOCK AS PER BOOK],
            --(((SUM(CASE WHEN convert(date,CS_CreatedDate,113) < '" + fromDate + @"'   AND CS_Type = 3 THEN CS_Qty ELSE 0 END))+(SUM(CASE WHEN convert(date,CS_CreatedDate,113)  = '" + fromDate + @"'   AND CS_Trans_type = 'GRN Inward' AND CS_Type = 3 THEN CS_Qty ELSE 0 END)))      
               --  -(SUM(CASE WHEN CONVERT(DATE, CS_CreatedDate, 113) = '" + fromDate + @"'   AND CS_Trans_type = 'Material Issue' AND CS_Type = 3  THEN ABS(CS_Qty) ELSE 0 END) )) AS [CL RM IN KGS],

             (((SUM(CASE WHEN convert(date,CS_CreatedDate,113) < '" + fromDate + @"'  AND CS_Type = 3 THEN CS_Qty ELSE 0 END))+(SUM(CASE WHEN convert(date,CS_CreatedDate,113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"' AND CS_Trans_type = 'GRN Inward' AND CS_Type = 3 THEN CS_Qty ELSE 0 END)))        
  -(SUM(CASE WHEN CONVERT(DATE, CS_CreatedDate, 113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"' AND CS_Trans_type = 'Material Issue' AND CS_Type = 3  THEN ABS(CS_Qty) ELSE 0 END) ))  AS [CL RM IN KGS],



                  CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName AND Pq_Status='A'    
                WHERE PD_Route_Card_ID = Pq_iid AND PD_Status='A'    
                  AND CONVERT(DATE, PD_Date, 113)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT) 
            +
			  CAST(ISNULL((SELECT SUM(CS_Qty)
             FROM CURRENT_STOCK
            WHERE CS_Item_ID = IM_ID  and CS_Status = 'A' AND CS_Type=1
            AND CONVERT(DATE, CS_CreatedDate, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
            as  [prod qty],


                CAST(0 AS DECIMAL(18,2)) AS [total wip stock],

            CAST(ISNULL((SELECT SUM(fe_ok)
             FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2))
              as [Final inspection ok qty],

              CAST(ISNULL((SELECT SUM(fe_rej)
            FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) as [Final inspection rej qty],
                CAST(0 AS DECIMAL(18,2)) AS [CL KGS],

             CAST(ISNULL((SELECT SUM(wor_Qty)
             FROM Work_Order_Receive_Details
            WHERE WOR_PID = IM_ID  and wor_status = 'A' and wor_MaterialType='FG'
            AND CONVERT(DATE, wor_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) AS [dc],
                CAST(0 AS DECIMAL(18,2)) AS [fg closing],
                CAST(0 AS DECIMAL(18,2)) AS [Invoice],
                CAST(0 AS DECIMAL(18,2)) AS [closing],
                CAST(0 AS DECIMAL(18,2)) AS [wip closing stock],
                CAST(0 AS DECIMAL(18,2)) AS [CL FG IN RM IN KGS],
                CAST(0 AS DECIMAL(18,2)) AS [TOTAL1],
                CAST(0 AS DECIMAL(18,2)) AS [SHORTAGE OR EXCESS],
                CAST(0 AS DECIMAL(18,2)) AS [DIFF VALUE],
                CAST(0 AS DECIMAL(18,2)) as [yes_invoice],
                CAST(0 AS DECIMAL(18,2)) AS [REMARKS]

          FROM CURRENT_STOCK          
    LEFT OUTER JOIN Item_Master ON IM_ID = CS_Item_ID  
     left outer join bom_master on bm_bomid=IM_ID AND BM_Status='A' 
   where CS_Type = 3    
    GROUP BY IM_PartName, IM_PartNo,IM_ID,BM_Net_Part_Wt,IM_Purchase_Price , BM_ItemId  order by IM_PartName asc  

");

            foreach (DataRow dr in dt.Rows)
            {
                decimal PRO = Convert.ToDecimal(dr["yesterday.wip"]);
                decimal tfg = Convert.ToDecimal(dr["yesterday.FGok"]);
                decimal tfgR = Convert.ToDecimal(dr["yesterday.fgrej"]);
                decimal tocl = PRO - tfg - tfgR;
                dr["wip opening"] = tocl;
            }
            foreach (DataRow dr in dt.Rows)
            {

                string partNo = dr["Part No"].ToString();

                DataRow[] invoiceMatch = dt2.Select("SD_Part_No = '" + partNo + "'");
                decimal pin = invoiceMatch.Length > 0 ? Convert.ToDecimal(invoiceMatch[0]["Qty"]) : 0;

                decimal yfg = Convert.ToDecimal(dr["yesterday.FGok"]);

                decimal dc = Convert.ToDecimal(dr["YES dc"]);

                decimal fgClosing = yfg - pin + dc;
                dr["fg Opening"] = fgClosing;
            }
            foreach (DataRow dr in dt.Rows)
            {       

                decimal tocl = Convert.ToDecimal(dr["wip opening"]);
                decimal fgClosing = Convert.ToDecimal(dr["fg opening"]);
                dr["Opening Stock"] = tocl + fgClosing;
            }
            //foreach (DataRow dr in dt.Rows)
            //{
            //    decimal fgo = dr["BM_Net_Part_Wt"] != DBNull.Value ? Convert.ToDecimal(dr["BM_Net_Part_Wt"]) : 0m;
            //    decimal tfg = dr["Opening Stock"] != DBNull.Value ? Convert.ToDecimal(dr["Opening Stock"]) : 0m;

            //   // decimal tocl = Math.Round((fgo * tfg) / 1000, 2);  // round to 2 decimals
            //    decimal tocl = (fgo * tfg) / 1000;
            //   // dr["FG IN RM IN KGS"] = tocl;
            //    dr["FG IN RM IN KGS"] = tocl.ToString("0.000");

            //}
            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = dr["BM_Net_Part_Wt"] != DBNull.Value ? Convert.ToDecimal(dr["BM_Net_Part_Wt"]) : 0m;
                decimal tfg = dr["Opening Stock"] != DBNull.Value ? Convert.ToDecimal(dr["Opening Stock"]) : 0m;

                decimal tocl = (fgo * tfg) / 1000;
                dr["FG IN RM IN KGS"] = Convert.ToDecimal(tocl);

                // Option A: For decimal column
                // dr["FG IN RM IN KGS"] = tocl;

                // Option B: For string column with precision formatting
               // dr["FG IN RM IN KGS"] = tocl.ToString("0.000000"); // 6 decimal places
            }

            foreach (DataRow dr in dt.Rows)
            {

                decimal tocl = Convert.ToDecimal(dr["RM IN KGS"]);
                decimal FGRM = Convert.ToDecimal(dr["FG IN RM IN KGS"]);
                dr["TOTAL"] = tocl + FGRM;
            }


            foreach (DataRow dr in dt.Rows)
            {

                decimal tocl = Convert.ToDecimal(dr["RM PURCHASE IN KGS"]);
                decimal FGRM = Convert.ToDecimal(dr["JO SALES IN KGS"]);
                dr["RM USAGE (IN-HOUSE) KGS"] = tocl - FGRM;
            }

            // FG ALES IN KGS
            foreach (DataRow dr in dt.Rows)
            {
                //string partNo = dr["Part No"].ToString();
                //DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                //dr["FG QTY"] = match.Length > 0 ? match[0]["Qty"] : 0;

                //decimal tocl = Math.Round((fgo * tfg) / 1000, 2);  // round to 2 decimals
                //dr["FG IN RM IN KGS"] = tocl;


                string partNo = dr["Part No"].ToString();

                // Match the part number in dt1
                DataRow[] match = dt1.Select($"[SD_Part_No] = '{partNo}'");

                // Safely extract FG QTY
                decimal fgQty = 0;
                if (match.Length > 0 && match[0]["Qty"] != DBNull.Value)
                {
                    fgQty = Convert.ToDecimal(match[0]["Qty"]);
                }
                dr["FG QTY"] = fgQty;

                // Calculate FG IN RM IN KGS
                //decimal tocl = Math.Round(fgQty / 1000, 2); // Divide by 1000 and round to 2 decimals
                //dr["FG IN RM IN KGS"] = tocl;


                decimal fgo = dr["BM_Net_Part_Wt"] != DBNull.Value ? Convert.ToDecimal(dr["BM_Net_Part_Wt"]) : 0m;
                decimal tfg = dr["FG QTY"] != DBNull.Value ? Convert.ToDecimal(dr["FG QTY"]) : 0m;

                decimal tocl1 = Math.Round((fgo * tfg) / 1000, 2);  // round to 2 decimals
                                                                    //dr["FG IN RM IN KGS"] = tocl1;

            }

            foreach (DataRow dr in dt.Rows)
            {
                decimal tfgR = Convert.ToDecimal(dr["wip opening"]);
                decimal tfD = Convert.ToDecimal(dr["prod qty"]);

                decimal tocl = tfD + tfgR;
                dr["total wip stock"] = tocl;
            }
            foreach (DataRow dr in dt.Rows)
            {
                decimal PRO = Convert.ToDecimal(dr["total wip stock"]);
                decimal tfg = Convert.ToDecimal(dr["Final inspection ok qty"]);
                decimal tfgr = Convert.ToDecimal(dr["Final inspection rej qty"]);
                decimal tocl = PRO - tfg - tfgr;

                dr["wip closing stock"] = tocl;

            }
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                dr["invoice"] = match.Length > 0 ? match[0]["Qty"] : 0;
            }

            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt2.Select("[SD_Part_No] = '" + partNo + "'");
                dr["yes_invoice"] = match.Length > 0 ? match[0]["Qty"] : 0;
            }


            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = dr["BM_Net_Part_Wt"] != DBNull.Value ? Convert.ToDecimal(dr["BM_Net_Part_Wt"]) : 0m;
                decimal tfg = dr["invoice"] != DBNull.Value ? Convert.ToDecimal(dr["invoice"]) : 0m;

                decimal tocl = Math.Round((tfg * fgo) / 1000, 2);  // round to 2 decimals
                dr["FG SALES IN KGS"] = tocl;

            }

            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = Convert.ToDecimal(dr["fg Opening"]);
                decimal tfg = Convert.ToDecimal(dr["Final inspection ok qty"]);
                decimal dc = Convert.ToDecimal(dr["dc"]);
                decimal inv = Convert.ToDecimal(dr["Invoice"]);

                decimal tocl = (fgo + tfg + dc) - inv;

                dr["fg closing"] = tocl;
            }
            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = Convert.ToDecimal(dr["wip closing stock"]);
                decimal tfg = Convert.ToDecimal(dr["fg closing"]);

                decimal tocl = (fgo + tfg);
                dr["closing"] = tocl;

            }
            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = dr["BM_Net_Part_Wt"] != DBNull.Value ? Convert.ToDecimal(dr["BM_Net_Part_Wt"]) : 0m;
                decimal tfg = dr["closing"] != DBNull.Value ? Convert.ToDecimal(dr["closing"]) : 0m;

                decimal tocl = Math.Round((fgo * tfg) / 1000, 2);  // round to 2 decimals
                dr["CL FG IN RM IN KGS"] = tocl;

            }

            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = Convert.ToDecimal(dr["CL RM IN KGS"]);
                decimal tfg = Convert.ToDecimal(dr["FG IN RM IN KGS"]);

                decimal tocl = (fgo + tfg);
                dr["TOTAL1"] = tocl;

            }


            //foreach (DataRow dr in dt.Rows)
            //{
            //    decimal fgo = Convert.ToDecimal(dr["TOTAL"]);
            //    decimal tfg = Convert.ToDecimal(dr["CLOSING STOCK AS PER BOOK"]);

            //    decimal tocl = (fgo - tfg);
            //    dr["CLOSING STOCK AS PER BOOK"] = tocl;

            //}

            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = Convert.ToDecimal(dr["TOTAL"]);
                decimal tfg = Convert.ToDecimal(dr["RM USAGE (IN-HOUSE) KGS"]);

                decimal fgo1 = Convert.ToDecimal(dr["FG SALES IN KGS"]);
                decimal tfg1 = Convert.ToDecimal(dr["JO SALES IN KGS"]);


                decimal tocl = (fgo + tfg) - (fgo1 + tfg1);
                dr["CLOSING STOCK AS PER BOOK"] = tocl;

            }
            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = Convert.ToDecimal(dr["TOTAL"]);
                decimal tfg = Convert.ToDecimal(dr["CLOSING STOCK AS PER BOOK"]);

                decimal tocl = (fgo + tfg);
                dr["SHORTAGE OR EXCESS"] = tocl;

            }

            foreach (DataRow dr in dt.Rows)
            {
                //decimal fgo = Convert.ToDecimal(dr["SHORTAGE OR EXCESS"]);
                //decimal tfg = Convert.ToDecimal(dr["RM PRICE"]);
                decimal fgo = dr["SHORTAGE OR EXCESS"] != DBNull.Value && !string.IsNullOrWhiteSpace(dr["SHORTAGE OR EXCESS"].ToString())
              ? Convert.ToDecimal(dr["SHORTAGE OR EXCESS"])
              : 0m;

                decimal tfg = dr["RM PRICE"] != DBNull.Value && !string.IsNullOrWhiteSpace(dr["RM PRICE"].ToString())
                              ? Convert.ToDecimal(dr["RM PRICE"])
                              : 0m;

                decimal tocl = (fgo * tfg);
                dr["DIFF VALUE"] = tocl;

            }
            

            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            dataGridView1.Columns["yesterday.FGok"].Visible = false;
            dataGridView1.Columns["yesterday.fgrej"].Visible = false;
            dataGridView1.Columns["yesterday.wip"].Visible = false;
            //dataGridView1.Columns["yes wip"].Visible = false;
            //dataGridView1.Columns["yes fg"].Visible = false;
            dataGridView1.Columns["YES dc"].Visible = false;

            dataGridView1.Columns["Opening Stock"].Visible = false;
            dataGridView1.Columns["BM_Net_Part_Wt"].Visible = false;
            dataGridView1.Columns["fg opening"].Visible = false;
            dataGridView1.Columns["wip opening"].Visible = false;
            dataGridView1.Columns["FG QTY"].Visible = false;

            dataGridView1.Columns["dc"].Visible = false;
            dataGridView1.Columns["prod qty"].Visible = false;
            dataGridView1.Columns["total wip stock"].Visible = false;
            dataGridView1.Columns["Final inspection ok qty"].Visible = false;
            dataGridView1.Columns["Final inspection rej qty"].Visible = false;
            dataGridView1.Columns["fg closing"].Visible = false;
            dataGridView1.Columns["Invoice"].Visible = false;
            dataGridView1.Columns["closing"].Visible = false;

            dataGridView1.Columns["wip closing stock"].Visible = false;
            dataGridView1.Columns["closing"].Visible = false;
            dataGridView1.Columns["closing"].Visible = false;
            return dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DONE1();
    //        // Call your methods to get the DataTables
    //        DataTable table1 = ASD(); // returns Table1 data
    //        DataTable table2 = FINAL();  // returns Table2 data

    //        // List of columns to sum/merge
    //        List<string> numericColumns = new List<string>
    //{
    //    "RM IN KGS", "yesterday.wip", "yesterday.fgrej", "yesterday.FGok", "YES dc",
    //    "wip opening", "fg opening", "Opening Stock", "KGS", "FG IN RM IN KGS", "TOTAL",
    //    "RM PURCHASE IN KGS", "KGS1", "JO SALES IN KGS", "RM USAGE (IN-HOUSE) KGS",
    //    "FG QTY", "FG SALES IN KGS", "CLOSING STOCK AS PER BOOK", "CL RM IN KGS",
    //    "prod qty", "total wip stock", "Final inspection ok qty", "Final inspection rej qty",
    //    "CL KGS", "dc", "fg closing", "Invoice", "closing", "wip closing stock",
    //    "CL FG IN RM IN KGS", "TOTAL1", "SHORTAGE OR EXCESS", "DIFF VALUE", "yes_invoice"
    //};

    //        // Loop through table1 rows
    //        foreach (DataRow row1 in table1.Rows)
    //        {
    //            string id1 = row1["Id1"].ToString();

    //            // Find matching rows in table2 where RM ID = Id1
    //            DataRow[] matches = table2.Select($"[RM ID] = '{id1}'");

    //            foreach (string column in numericColumns)
    //            {
    //                if (!table1.Columns.Contains(column) || !table2.Columns.Contains(column))
    //                    continue;

    //                decimal val1 = row1[column] != DBNull.Value ? Convert.ToDecimal(row1[column]) : 0;
    //                decimal totalVal2 = 0;

    //                foreach (DataRow row2 in matches)
    //                {
    //                    if (row2[column] != DBNull.Value)
    //                    {
    //                        totalVal2 += Convert.ToDecimal(row2[column]);
    //                    }
    //                }

    //                row1[column] = val1 + totalVal2;
    //            }
    //        }

    //        // Optional: Show result
    //        dataGridView1.DataSource = table1;
        }

        private void DONE1()
        {
            // Call your methods to get the DataTables
    DataTable table1 = ASD(); // returns Table1 data
            DataTable table2 = FINAL();  // returns Table2 data

            // List of columns to sum/merge
            List<string> numericColumns = new List<string>
    {
        "RM IN KGS", "yesterday.wip", "yesterday.fgrej", "yesterday.FGok", "YES dc",
        "wip opening", "fg opening", "Opening Stock", "KGS", "FG IN RM IN KGS", "TOTAL",
        "RM PURCHASE IN KGS", "KGS1", "JO SALES IN KGS", "RM USAGE (IN-HOUSE) KGS",
        "FG QTY", "FG SALES IN KGS", "CLOSING STOCK AS PER BOOK", "CL RM IN KGS",
        "prod qty", "total wip stock", "Final inspection ok qty", "Final inspection rej qty",
        "CL KGS", "dc", "fg closing", "Invoice", "closing", "wip closing stock",
        "CL FG IN RM IN KGS", "TOTAL1", "SHORTAGE OR EXCESS", "DIFF VALUE", "yes_invoice"
    };

            // Loop through table1 rows
            foreach (DataRow row1 in table1.Rows)
            {
                string id1 = row1["Id1"].ToString(); // Or adjust to "Id" if needed

                // Find matching rows in table2 where RM ID = Id1
                DataRow[] matches = table2.Select($"[RM ID] = '{id1}'");

                foreach (string column in numericColumns)
                {
                    if (!table1.Columns.Contains(column) || !table2.Columns.Contains(column))
                        continue;

                    decimal val1 = row1[column] != DBNull.Value ? Convert.ToDecimal(row1[column]) : 0;
                    decimal totalVal2 = 0;

                    foreach (DataRow row2 in matches)
                    {
                        if (row2[column] != DBNull.Value)
                        {
                            totalVal2 += Convert.ToDecimal(row2[column]);
                        }
                    }

                    row1[column] = val1 + totalVal2;
                }

                // ➕ Apply your calculations now:

                // TOTAL = RM IN KGS + FG IN RM IN KGS
                decimal rmInKgs = row1["RM IN KGS"] != DBNull.Value ? Convert.ToDecimal(row1["RM IN KGS"]) : 0;
                decimal fgInRm = row1["FG IN RM IN KGS"] != DBNull.Value ? Convert.ToDecimal(row1["FG IN RM IN KGS"]) : 0;
                row1["TOTAL"] = rmInKgs + fgInRm;

                // TOTAL1 = CL RM IN KGS + CL FG IN RM IN KGS
                decimal clRm = row1["CL RM IN KGS"] != DBNull.Value ? Convert.ToDecimal(row1["CL RM IN KGS"]) : 0;
                decimal clFg = row1["CL FG IN RM IN KGS"] != DBNull.Value ? Convert.ToDecimal(row1["CL FG IN RM IN KGS"]) : 0;
                row1["TOTAL1"] = clRm + clFg;

                // CLOSING STOCK AS PER BOOK = (TOTAL + RM USAGE) - (FG SALES + JO SALES)
                decimal rmUsage = row1["RM USAGE (IN-HOUSE) KGS"] != DBNull.Value ? Convert.ToDecimal(row1["RM USAGE (IN-HOUSE) KGS"]) : 0;
                decimal fgSales = row1["FG SALES IN KGS"] != DBNull.Value ? Convert.ToDecimal(row1["FG SALES IN KGS"]) : 0;
               // decimal joSales = row1["JO SALES IN KGS"] != DBNull.Value ? Convert.ToDecimal(row1["JO SALES IN KGS"]) : 0;
                row1["CLOSING STOCK AS PER BOOK"] = (rmInKgs + fgInRm + rmUsage) - (fgSales);



                decimal TOTAL1 = row1["TOTAL1"] != DBNull.Value ? Convert.ToDecimal(row1["TOTAL1"]) : 0;
                decimal clapb = row1["CLOSING STOCK AS PER BOOK"] != DBNull.Value ? Convert.ToDecimal(row1["CLOSING STOCK AS PER BOOK"]) : 0;
                row1["SHORTAGE OR EXCESS"] = TOTAL1 - clapb;
            }

            // Optional: Show result
            dataGridView1.DataSource = table1;
            dbFunctions.DGVStyle(dataGridView1);
        }
    }
}
