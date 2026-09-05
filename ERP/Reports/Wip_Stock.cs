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
    public partial class Wip_Stock : Form
    {       
        public Wip_Stock()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //getdata();
            //DATAVALUE2();
            // //DATAVALUE3();
            // DATAVALUE4();
            // DATAVALUE5();
            //DATAVALUE6();
            // DATAVALUE7();   fg goog
            // DATAVALUE8(); wip
            final();

        }

        private void final()
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
                  AND CONVERT(date, SD_Created_Date, 112) >= '20260331'
                GROUP BY SD_Part_No");


            DataTable dt11 = dbFunctions.getTable1(@"                
            select IM_Part_No,cm_vSupplierName from   [dbo].[Supplier_Master]
               left outer join [dbo].[Item_Master] on IM_CustomerCode=cm_iId
               group by cm_vSupplierName,IM_Part_No");


            // Load Sales Invoice Details (before today)
            DataTable dt2 = dbFunctions.getTable1(@"
            SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
            FROM dbo.Sales_Invoice_Details
            WHERE SD_Status = 'A' 
              AND CONVERT(DATE, SD_Created_Date, 112) 
              < '" + fromDate + @"' AND CONVERT(date, SD_Created_Date, 112) >= '20260331'
            GROUP BY SD_Part_No");


            // main table

            DataTable dt = dbFunctions.getTable(@"
            SELECT                               
            IM_ID AS ID,                              
            ROW_NUMBER() OVER(ORDER BY IM_ID) AS [S.No],                        
            IM_PartNo AS [Part No],                              
            IM_PartName AS [Part Name],
             0 as [CustomerName],
            0 AS [Opening Stock],
            
             0 as [wip opening],

            
                  CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName AND Pq_Status='A'    
                WHERE PD_Route_Card_ID = Pq_iid AND PD_Status='A'    
                  AND CONVERT(DATE, PD_Date, 113)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'  AND CONVERT(date, PD_Date, 112) >= '20260331' ), 0) AS INT) 
            +
			  CAST(ISNULL((SELECT SUM(CS_Qty)
             FROM CURRENT_STOCK
            WHERE CS_Item_ID = IM_ID  and CS_Status = 'A' AND CS_Type=1
            AND CONVERT(DATE, CS_CreatedDate, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"' AND CONVERT(date, CS_CreatedDate, 112) >= '20260331'), 0) AS INT)
            as  [prod qty],

             0 as [total wip stock],

            0 as [fg opening],  

            CAST(ISNULL((SELECT SUM(fe_ok)
             FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'  AND CONVERT(date, fe_Date, 112) >= '20260331' ), 0) AS DECIMAL(18,2))
              as [Final inspection ok qty],

              CAST(ISNULL((SELECT SUM(fe_rej)
            FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'  AND CONVERT(date, fe_Date, 112) >= '20260331' ), 0) AS DECIMAL(18,2)) as [Final inspection rej qty],

        
            CAST(ISNULL((SELECT SUM(wor_Qty)
             FROM Work_Order_Receive_Details
            WHERE WOR_PID = IM_ID  and wor_status = 'A' and wor_MaterialType='FG'
            AND CONVERT(DATE, wor_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'  AND CONVERT(date, wor_Date, 112) >= '20260331'), 0) AS DECIMAL(18,2)) AS [dc],

             
        0 as [total fg stock],
            
             0 as [invoice],

             0 as [wip closing stock],
              0 as [fg closing],

             0 AS [closing],

            CAST(ISNULL((SELECT SUM(fe_ok)
             FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"' AND CONVERT(date, fe_Date, 112) >= '20260331'), 0) AS DECIMAL(18,2))
           


            as [yesterday.FGok],


 
            CAST(ISNULL((SELECT SUM(wor_Qty)
             FROM Work_Order_Receive_Details
            WHERE WOR_PID = IM_ID  and wor_status = 'A' and wor_MaterialType='FG'
            AND CONVERT(DATE, wor_Date, 112)< '" + fromDate + @"' AND CONVERT(date, wor_Date, 112) >= '20260331'), 0) AS DECIMAL(18,2)) AS [YES dc],


            CAST(ISNULL((SELECT SUM(fe_rej)
            FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"' AND CONVERT(date, fe_Date, 112) >= '20260331'), 0) AS DECIMAL(18,2)) as [yesterday.fgrej],

                  CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName AND Pq_Status='A'    
                WHERE PD_Route_Card_ID = Pq_iid AND PD_Status='A'    
                  AND CONVERT(DATE, PD_Date, 113) < '" + fromDate + @"' AND CONVERT(date, PD_Date, 112) >= '20260331'), 0) AS INT)
     +
			  CAST(ISNULL((SELECT SUM(CS_Qty)
             FROM CURRENT_STOCK
            WHERE CS_Item_ID = IM_ID  and CS_Status = 'A' AND CS_Type=1
            AND CONVERT(DATE, CS_CreatedDate, 112) < '" + fromDate + @"'  AND CONVERT(date, CS_CreatedDate, 112) >= '20260331' ), 0) AS INT)
                as  [yesterday.wip],
                BM_Qty,
CAST(0.00 AS decimal(18,2)) as [fg closing rm],


               
                0 as [yes wip], 0 as [yes fg],  
                
                 0 AS [Remarks]
            FROM Item_Master  
            left outer join bom_master on bm_bomid=IM_ID AND BM_Status='A'     

            WHERE IM_Status = 'A' AND IM_Type = 1 and  IM_ID NOT IN (
    SELECT ABM_AssyPartName
    FROM Assy_BOM_Master
    WHERE ABM_Status = 'A')                          
            ORDER BY IM_ID");

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
                decimal tfgR = Convert.ToDecimal(dr["wip opening"]);
                decimal tfD = Convert.ToDecimal(dr["prod qty"]);

                decimal tocl = tfD + tfgR;
                dr["total wip stock"] = tocl;
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
                decimal tfgR = Convert.ToDecimal(dr["fg Opening"]);
                decimal tfD = Convert.ToDecimal(dr["Final inspection ok qty"]);
                decimal dc = Convert.ToDecimal(dr["dc"]);

                decimal tocl = tfD + tfgR + dc;
                dr["total fg stock"] = tocl;
            }


            // invoice
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                dr["invoice"] = match.Length > 0 ? match[0]["Qty"] : 0;
            }

            //foreach (DataRow dr in dt.Rows)
            //{
            //    string partNo = dr["Part No"].ToString();
            //    DataRow[] match = dt11.Select("[IM_Part_No] = '" + partNo + "'");
            //    dr["CustomerName"] = match.Length > 0 ? match[0]["cm_vSupplierName"] : 0;
            //}
            // Ensure CustomerName column is of type string
            if (dt.Columns.Contains("CustomerName"))
            {
                dt.Columns.Remove("CustomerName");
            }
            dt.Columns.Add("CustomerName", typeof(string));

            // Match Part No and add Customer Name if exists
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();

                var foundRow = dt11.AsEnumerable()
                                   .FirstOrDefault(r => r["IM_Part_No"].ToString() == partNo);

                dr["CustomerName"] = foundRow != null ? foundRow["cm_vSupplierName"].ToString() : null; // or string.Empty if preferred
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

          
            // fg closing rm
            foreach (DataRow dr in dt.Rows)
            {
                // decimal fgo = Convert.ToDecimal(dr["BM_Qty"]);
                //decimal fgo = dr["BM_Qty"] != DBNull.Value ? Convert.ToDecimal(dr["BM_Qty"]) : 0m;

                //decimal tfg = Convert.ToDecimal(dr["closing"]);

                //decimal tocl = (fgo * tfg)/1000;
                //dr["fg closing rm"] = tocl;


                decimal fgo = dr["BM_Qty"] != DBNull.Value ? Convert.ToDecimal(dr["BM_Qty"]) : 0m;
                decimal tfg = dr["closing"] != DBNull.Value ? Convert.ToDecimal(dr["closing"]) : 0m;

                decimal tocl = Math.Round((fgo * tfg) / 1000, 2);  // round to 2 decimals
                dr["fg closing rm"] = tocl;

            }
            // op s
            foreach (DataRow dr in dt.Rows)
            {

                //decimal PRO = Convert.ToDecimal(dr["yesterday.wip"]);
                //decimal tfg = Convert.ToDecimal(dr["yesterday.FGok"]);
                //decimal tfgR = Convert.ToDecimal(dr["yesterday.fgrej"]);
                //decimal tocl = (PRO + tfg) - tfgR;

                //dr["yes wip"] = tocl; 
                //string partNo = dr["Part No"].ToString();

                //DataRow[] invoiceMatch = dt2.Select("SD_Part_No = '" + partNo + "'");
                //decimal pin = invoiceMatch.Length > 0 ? Convert.ToDecimal(invoiceMatch[0]["Qty"]) : 0;

                //decimal yfg = Convert.ToDecimal(dr["yesterday.FGok"]);

                //decimal dc = Convert.ToDecimal(dr["YES dc"]);


                //decimal fgClosing = yfg + pin + dc;

                //dr["yes fg"] = fgClosing;    0 as [fg opening],  0 as [wip opening],

                decimal tocl = Convert.ToDecimal(dr["wip opening"]);
                decimal fgClosing = Convert.ToDecimal(dr["fg opening"]);

                

                dr["Opening Stock"] = tocl+fgClosing;
            }

           

            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            dataGridView1.Columns["yesterday.FGok"].Visible = false;
            dataGridView1.Columns["yesterday.fgrej"].Visible = false;
            dataGridView1.Columns["yesterday.wip"].Visible = false;
            dataGridView1.Columns["yes wip"].Visible = false;
            dataGridView1.Columns["yes fg"].Visible = false;
            dataGridView1.Columns["YES dc"].Visible = false;











        }

        private void DATAVALUE8()
        {
            string fromDate = dtpFrom.Value.ToString("yyyyMMdd");
            string toDate = todate.Value.ToString("yyyyMMdd");


            DataTable dt = dbFunctions.getTable(@"
            SELECT                               
            IM_ID AS ID,                              
            ROW_NUMBER() OVER(ORDER BY IM_ID) AS [S.No],                        
            IM_PartNo AS [Part No],                              
            IM_PartName AS [Part Name],

            CAST(ISNULL((SELECT SUM(fe_ok)
             FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2)) as [y.fg],

              CAST(ISNULL((SELECT SUM(fe_rej)
            FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2)) as [y.fgrej],

                  CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName AND Pq_Status='A'    
                WHERE PD_Route_Card_ID = Pq_iid AND PD_Status='A'    
                  AND CONVERT(DATE, PD_Date, 113) < '" + fromDate + @"'), 0) AS INT)   as  [y.wip],


            CAST(ISNULL((SELECT SUM(fe_ok)
             FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) as [T.fg],

              CAST(ISNULL((SELECT SUM(fe_rej)
            FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) as [T.fgrej],

                  CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName AND Pq_Status='A'    
                WHERE PD_Route_Card_ID = Pq_iid AND PD_Status='A'    
                  AND CONVERT(DATE, PD_Date, 113)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)   as  [T.wip],

                0 AS [WIP OPENIG],
                0 AS [Total Wip],
                0 AS [WIP CLOSING],
                 0 AS [Remarks]
            FROM Item_Master                               
            WHERE IM_Status = 'A'                           
            ORDER BY IM_ID");

            foreach (DataRow dr in dt.Rows)
            {
                decimal PRO = Convert.ToDecimal(dr["y.wip"]);
                decimal tfg = Convert.ToDecimal(dr["y.fg"]);
                decimal tfgR = Convert.ToDecimal(dr["y.fgrej"]);
                decimal tocl = PRO - tfg - tfgR;

                dr["WIP OPENIG"] = tocl;

            }

            foreach (DataRow dr in dt.Rows)
            {
                decimal PRO = Convert.ToDecimal(dr["WIP OPENIG"]);
                decimal tfg = Convert.ToDecimal(dr["T.wip"]);
                decimal tocl = PRO + tfg ;

                dr["Total Wip"] = tocl;

            }

            foreach (DataRow dr in dt.Rows)
            {
                decimal PRO = Convert.ToDecimal(dr["Total Wip"]);
                decimal tfg = Convert.ToDecimal(dr["T.fg"]);
                decimal tfgr = Convert.ToDecimal(dr["T.fgrej"]);
                decimal tocl = PRO - tfg - tfgr;

                dr["WIP CLOSING"] = tocl;

            }



            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);

        }

        private void DATAVALUE7()
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
                  AND CONVERT(date, SD_Created_Date, 112) >= '20250407'
                GROUP BY SD_Part_No");


            // Load Sales Invoice Details (before today)
            DataTable dt2 = dbFunctions.getTable1(@"
            SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
            FROM dbo.Sales_Invoice_Details
            WHERE SD_Status = 'A' 
              AND CONVERT(DATE, SD_Created_Date, 112) 
              < '" + fromDate + @"' AND CONVERT(date, SD_Created_Date, 112) >= '20250407'
            GROUP BY SD_Part_No");


            // main table

            DataTable dt = dbFunctions.getTable(@"
            SELECT                               
            IM_ID AS ID,                              
            ROW_NUMBER() OVER(ORDER BY IM_ID) AS [S.No],                        
            IM_PartNo AS [Part No],                              
            IM_PartName AS [Part Name],
            
             CAST(ISNULL((SELECT SUM(fe_ok)
             FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2)) as [y.fg],

            0 as [y.In],

            0 as [fg Opening],

            CAST(ISNULL((SELECT SUM(fe_ok) 
                FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                WHERE fe_partid = IM_ID  and fe_status='A'
                  AND CONVERT(DATE, fe_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) as [TO.FG],

            0 AS [Invoice],
            0 as [f.cl],

             CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName AND Pq_Status='A'    
                WHERE PD_Route_Card_ID = Pq_iid AND PD_Status='A'    
                  AND CONVERT(DATE, PD_Date, 113) < '" + fromDate + @"'), 0) AS INT)   as  [y.wip],

              CAST(ISNULL((SELECT SUM(fe_rej)
            FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'

            AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2)) as [y.fgrej],

            0 as [wip opening],
            
            CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName AND Pq_Status='A'    
                WHERE PD_Route_Card_ID = Pq_iid AND PD_Status='A'    
                  AND CONVERT(DATE, PD_Date, 113) Between '" + fromDate + @"' AND '" + toDate + @"' ), 0) AS INT)   as  [T.wip],

            0 as [tot wip],
    
             CAST(ISNULL((SELECT SUM(fe_rej) 
                FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                WHERE fe_partid = IM_ID  and fe_status='A'
                  AND CONVERT(DATE, fe_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) as [T fgrej],
    

            0 AS [WIP CLOSING],
            
             0 AS [Remarks]
            FROM Item_Master                               
            WHERE IM_Status = 'A'                           
            ORDER BY IM_ID");


            // y.in
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt2.Select("[SD_Part_No] = '" + partNo + "'");
                dr["y.In"] = match.Length > 0 ? match[0]["Qty"] : 0;
            }


            // FG OPENINF
            foreach (DataRow dr in dt.Rows)
            {

                string partNo = dr["Part No"].ToString();

                DataRow[] invoiceMatch = dt2.Select("SD_Part_No = '" + partNo + "'");
                decimal pin = invoiceMatch.Length > 0 ? Convert.ToDecimal(invoiceMatch[0]["Qty"]) : 0;

                decimal yfg = Convert.ToDecimal(dr["y.fg"]);

                decimal fgClosing = yfg - pin;


                dr["fg Opening"] = fgClosing;

            }

            // invoice
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                dr["Invoice"] = match.Length > 0 ? match[0]["Qty"] : 0;
            }

            // f.cl

            foreach (DataRow dr in dt.Rows)
            {
                decimal fgo = Convert.ToDecimal(dr["fg Opening"]);
                decimal tfg = Convert.ToDecimal(dr["TO.FG"]);
                decimal inv = Convert.ToDecimal(dr["Invoice"]);

                decimal tocl= ( fgo + tfg)-inv;

                dr["f.cl"] = tocl;




            }

            // wip opening

            foreach (DataRow dr in dt.Rows)
            {
                decimal ywp = Convert.ToDecimal(dr["y.wip"]);
                decimal tfg = Convert.ToDecimal(dr["y.fg"]);
                decimal inv = Convert.ToDecimal(dr["y.fgrej"]);

                decimal tocl = ywp - tfg - inv;

                dr["wip opening"] = tocl;
            }

            foreach(DataRow dr in dt.Rows)
            {
                decimal ywp = Convert.ToDecimal(dr["wip opening"]);
                decimal tfg = Convert.ToDecimal(dr["T.wip"]);
                decimal tocl = ywp + tfg;

                dr["tot wip"] = tocl;

            }

            foreach (DataRow dr in dt.Rows)
            {
                decimal ywp = Convert.ToDecimal(dr["tot wip"]);
                decimal tfg = Convert.ToDecimal(dr["TO.FG"]);
                decimal tFRJ = Convert.ToDecimal(dr["y.fgrej"]);
                decimal tocl = ywp - tfg - tFRJ;

                dr["WIP CLOSING"] = tocl;

            }



            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);









        }

        private void DATAVALUE6()
        {
            string fromDate = dtpFrom.Value.ToString("yyyyMMdd");
            string toDate = todate.Value.ToString("yyyyMMdd");
            string previousDate = dtpFrom.Value.AddDays(-1).ToString("yyyyMMdd");

            // Load Sales Invoice Details (today)
            DataTable dt1 = dbFunctions.getTable1(@"
                SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
                FROM dbo.Sales_Invoice_Details
                WHERE SD_Status = 'A' 
                  AND CONVERT(DATE, SD_Created_Date, 112) 
                  BETWEEN '" + fromDate + @"' AND '" + toDate + @"' 
                  AND CONVERT(date, SD_Created_Date, 112) >= '20250407'
                GROUP BY SD_Part_No");

            // Load Sales Invoice Details (before today)
            DataTable dt2 = dbFunctions.getTable1(@"
            SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
            FROM dbo.Sales_Invoice_Details
            WHERE SD_Status = 'A' 
              AND CONVERT(DATE, SD_Created_Date, 112) 
              < '" + fromDate + @"' AND CONVERT(date, SD_Created_Date, 112) >= '20250407'
            GROUP BY SD_Part_No");

            // Load yesterday's FG Qty
            DataTable dt_yesterdayFG = dbFunctions.getTable(@"
            SELECT 
                IM_ID, 
                IM_PartNo, 
                (
                    ISNULL((SELECT SUM(fe_ok) 
                            FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                            WHERE fe_partid = IM_ID and fe_status='A'
                              AND CONVERT(DATE, fe_Date, 112) = '" + previousDate + @"'), 0)
                ) AS YesterdayFG
            FROM Item_Master
            WHERE IM_Status = 'A'");

            // Load yesterday's FG Qty
            DataTable dt_yesFG = dbFunctions.getTable(@"
            SELECT 
                IM_ID, 
                IM_PartNo, 
                (
                    ISNULL((SELECT SUM(fe_rej) 
                            FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                            WHERE fe_partid = IM_ID and fe_status='A'
                              AND CONVERT(DATE, fe_Date, 112) = '" + previousDate + @"'), 0)
                ) AS Yesterday
            FROM Item_Master
            WHERE IM_Status = 'A'");

            // Load yesterday's Invoice
            DataTable dt_yesterdayInvoice = dbFunctions.getTable1(@"
            SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
            FROM dbo.Sales_Invoice_Details
            WHERE SD_Status = 'A' 
              AND CONVERT(DATE, SD_Created_Date, 112) = '" + previousDate + @"' 
              AND CONVERT(date, SD_Created_Date, 112) >= '20250407'
            GROUP BY SD_Part_No");

            // Main item list
            DataTable dt = dbFunctions.getTable(@"
            SELECT                               
            IM_ID AS ID,                              
            ROW_NUMBER() OVER(ORDER BY IM_ID) AS [S.No],                        
            IM_PartNo AS [Part No],                              
            IM_PartName AS [Part Name],
            0 AS [Opening Stock],
            0 AS [Wip Opening],

            CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName     
                WHERE PD_Route_Card_ID = Pq_iid 
                  AND CONVERT(DATE, PD_Date, 113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT) AS [prod qty1],

            0 AS [prod qty],
            0 AS [total wip stock], 
            0 AS [FG Opening],
            0 AS [fg closing],
            0 AS [fg op],
 
            CAST(ISNULL((SELECT SUM(fe_ok) 
                FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                WHERE fe_partid = IM_ID  and fe_status='A'
                  AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"' ), 0) AS DECIMAL(18,2)) AS [pervious fg],
            0 AS [pervious Inovoice],
            CAST(ISNULL((SELECT SUM(fe_ok) 
                FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                WHERE fe_partid = IM_ID  and fe_status='A'
                  AND CONVERT(DATE, fe_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2))  AS [Final inspection ok qty],
            CAST(ISNULL((SELECT SUM(fe_rej) 
                FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                WHERE fe_partid = IM_ID  and fe_status='A'
                  AND CONVERT(DATE, fe_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) AS [Final inspection rej qty],
            0 AS [total fg stock],
            0 AS [Invoice],
            0 AS [wip closing stock],
            0 AS [Closing Stock],
            0 AS [Remarks]
            FROM Item_Master                               
            WHERE IM_Status = 'A'                           
            ORDER BY IM_ID");

            // 1. Update Invoice Qty
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                dr["Invoice"] = match.Length > 0 ? match[0]["Qty"] : 0;
            }

            foreach (DataRow dr in dt.Rows)
            {

                string partNo = dr["Part No"].ToString();

                DataRow[] invoiceMatch = dt_yesterdayInvoice.Select("SD_Part_No = '" + partNo + "'");
                decimal yesterdayInvoice = invoiceMatch.Length > 0 ? Convert.ToDecimal(invoiceMatch[0]["Qty"]) : 0;

                decimal pq1 = Convert.ToDecimal(dr["pervious fg"]);

                decimal fgClosing = pq1 - yesterdayInvoice; 

                      dr["fg op"] = fgClosing;



            }


            // 2. Calculate FG Opening from Yesterday's FG Closing
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();

                // Yesterday's FG
                DataRow[] fgMatch = dt_yesterdayFG.Select("IM_PartNo = '" + partNo + "'");
                decimal yesterdayFG = fgMatch.Length > 0 ? Convert.ToDecimal(fgMatch[0]["YesterdayFG"]) : 0;

                // Yesterday's Invoice
                DataRow[] invoiceMatch = dt_yesterdayInvoice.Select("SD_Part_No = '" + partNo + "'");
                decimal yesterdayInvoice = invoiceMatch.Length > 0 ? Convert.ToDecimal(invoiceMatch[0]["Qty"]) : 0;


                // fg op

                decimal pq1 = Convert.ToDecimal(dr["pervious fg"]);

                // Yesterday FG Closing = FG - Invoice
                decimal fgClosing = (yesterdayFG + pq1 ) - yesterdayInvoice;

                // Set as today's FG Opening
                dr["FG Opening"] = fgClosing;
               // dr["pervious Opening"] = yesterdayFG;
                dr["pervious Inovoice"] = yesterdayInvoice;
            }


            // 2. Calculate FG Opening from Yesterday's FG Closing
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();

                // Yesterday's FG
                DataRow[] fgMatch = dt_yesterdayFG.Select("IM_PartNo = '" + partNo + "'");
                decimal yesterdayFG = fgMatch.Length > 0 ? Convert.ToDecimal(fgMatch[0]["YesterdayFG"]) : 0;

                // Yesterday's FG
                DataRow[] fgMat = dt_yesFG.Select("IM_PartNo = '" + partNo + "'");
                decimal yesterda = fgMat.Length > 0 ? Convert.ToDecimal(fgMat[0]["Yesterday"]) : 0;

                // Yesterday FG Closing = FG - Invoice
                decimal fgClosing = yesterdayFG - yesterda;

                // Set as today's FG Opening
                dr["Wip Opening"] = fgClosing;
            }


            // 3. Calculate total FG stock
            foreach (DataRow dr in dt.Rows)
            {
                decimal fok = Convert.ToDecimal(dr["Final inspection ok qty"]);
                decimal fop = Convert.ToDecimal(dr["FG Opening"]);
                dr["total fg stock"] = fok + fop;
            }

            // 4. Production Qty = prod qty1 - total fg stock
            foreach (DataRow dr in dt.Rows)
            {
                decimal pq1 = Convert.ToDecimal(dr["prod qty1"]);
                decimal fg = Convert.ToDecimal(dr["total fg stock"]);
                dr["prod qty"] = pq1 - fg;
            }

            // 5. Total WIP Stock
            foreach (DataRow dr in dt.Rows)
            {
                decimal pro = Convert.ToDecimal(dr["prod qty"]);
                decimal wip = Convert.ToDecimal(dr["Wip Opening"]);
                dr["total wip stock"] = pro + wip;
            }

            // 6. WIP Closing Stock
            foreach (DataRow dr in dt.Rows)
            {
                decimal twip = Convert.ToDecimal(dr["total wip stock"]);
                decimal fok = Convert.ToDecimal(dr["Final inspection ok qty"]);
                decimal rej = Convert.ToDecimal(dr["Final inspection rej qty"]);
                dr["wip closing stock"] = twip - fok - rej;
            }

            // 7. FG Closing
            foreach (DataRow dr in dt.Rows)
            {
                decimal tfg = Convert.ToDecimal(dr["total fg stock"]);
                decimal inv = Convert.ToDecimal(dr["Invoice"]);
                dr["fg closing"] = tfg - inv;
            }

            // 8. Final Closing Stock
            foreach (DataRow dr in dt.Rows)
            {
                decimal wipc = Convert.ToDecimal(dr["wip closing stock"]);
                decimal fgc = Convert.ToDecimal(dr["fg closing"]);
                dr["Closing Stock"] = wipc + fgc;
            }


            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);




        }

        private void DATAVALUE5()
        {
            string fromDate = dtpFrom.Value.ToString("yyyyMMdd");
            string toDate = todate.Value.ToString("yyyyMMdd");
            string previousDate = dtpFrom.Value.AddDays(-1).ToString("yyyyMMdd");

            // Load Sales Invoice Details (today)
            DataTable dt1 = dbFunctions.getTable1(@"
            SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
            FROM dbo.Sales_Invoice_Details
            WHERE SD_Status = 'A' 
              AND CONVERT(DATE, SD_Created_Date, 112) 
              BETWEEN '" + fromDate + @"' AND '" + toDate + @"'  AND CONVERT(date, SD_Created_Date, 112) >= '20250407'
            GROUP BY SD_Part_No");

            // Load Sales Invoice Details (before today)
            DataTable dt2 = dbFunctions.getTable1(@"
            SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
            FROM dbo.Sales_Invoice_Details
            WHERE SD_Status = 'A' 
              AND CONVERT(DATE, SD_Created_Date, 112) 
                  < '" + fromDate + @"' AND CONVERT(date, SD_Created_Date, 112) >= '20250407'
            GROUP BY SD_Part_No");

            // Load yesterday's FG Qty
            DataTable dt_yesterdayFG = dbFunctions.getTable(@"
            SELECT 
                IM_ID, 
                IM_PartNo, 
                (
                    ISNULL((SELECT SUM(fe_ok) 
                            FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                            WHERE fe_partid = IM_ID and fe_status='A'
                              AND CONVERT(DATE, fe_Date, 112) = '" + previousDate + @"'), 0)
                    +
                    ISNULL((SELECT SUM(wor_Qty) 
                            FROM Work_Order_Receive_Details  
                            WHERE wor_status = 'A' 
                              AND wor_MaterialType = 'FG' 
                              AND WOR_PID = IM_ID  
                              AND CONVERT(DATE, wor_Date, 112) = '" + previousDate + @"'), 0)
                    +
                    ISNULL((SELECT SUM(AS_Qty) 
                            FROM Assembly_Details  
                            WHERE AS_Status = 'A' 
                              AND AS_Part_ID = IM_ID  
                              AND CONVERT(DATE, AS_Date, 112) = '" + previousDate + @"'), 0)
                ) AS YesterdayFG
            FROM Item_Master
            WHERE IM_Status = 'A'");

            DataTable dt_yesterdayInvoice = dbFunctions.getTable1(@"
            SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
            FROM dbo.Sales_Invoice_Details
            WHERE SD_Status = 'A' 
              AND CONVERT(DATE, SD_Created_Date, 112) = '" + previousDate + @"'  AND CONVERT(date, SD_Created_Date, 112) >= '20250407'
            GROUP BY SD_Part_No");

            // Main item data
            DataTable dt = dbFunctions.getTable(@"
            SELECT                               
            IM_ID AS ID,                              
            ROW_NUMBER() OVER(ORDER BY IM_ID) AS [S.No],                        
            IM_PartNo AS [Part No],                              
            IM_PartName AS [Part Name],                              
            IM_Sales_Price AS [Sales Price],
            0 AS [Opening Stock1],
            0 AS [Wip Opening],
         
               CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                    FROM Production_Details 
                    LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName     
                    WHERE PD_Route_Card_ID = Pq_iid 
                      AND CONVERT(DATE, PD_Date, 113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT) AS [prod qty],
               0 AS [total wip stock], 
               0 AS [FG Opening],
                    CAST(ISNULL((SELECT SUM(fe_ok) 
                    FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                    WHERE fe_partid = IM_ID  and fe_status='A'
                      AND CONVERT(DATE, fe_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) AS [final inspection],
                0 AS [total fg stock],
            (
                (
                    CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                        FROM Production_Details 
                        LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName     
                        WHERE PD_Route_Card_ID = Pq_iid 
                          AND CONVERT(DATE, PD_Date, 113) < '" + fromDate + @"'), 0) AS INT)
                    +
                    CAST(ISNULL((SELECT SUM(CS_Qty) 
                        FROM Current_Stock 
                        WHERE CS_Item_ID = IM_ID AND CS_Type = '7' 
                          AND CONVERT(DATE, CS_CreatedDate, 113) < '" + fromDate + @"'), 0) AS INT)
                    -
                    CAST(ISNULL((SELECT SUM(fe_ok + fe_rej) 
                        FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                        WHERE fe_partid = IM_ID and fe_status='A'
                          AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2))
                )
                +
                (
                    CAST(ISNULL((SELECT SUM(fe_ok) 
                        FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                        WHERE fe_partid = IM_ID and fe_status='A'
                          AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2))
                    +
                    CAST(ISNULL((SELECT SUM(wor_Qty) 
                        FROM Work_Order_Receive_Details  
                        WHERE wor_status = 'A' 
                          AND wor_MaterialType = 'FG' 
                          AND WOR_PID = IM_ID  
                          AND CONVERT(DATE, wor_Date, 112) < '" + fromDate + @"'), 0) AS INT)
                    +
                    CAST(ISNULL((SELECT SUM(AS_Qty) 
                        FROM Assembly_Details  
                        WHERE AS_Status = 'A' 
                          AND AS_Part_ID = IM_ID  
                          AND CONVERT(DATE, AS_Date, 112) < '" + fromDate + @"'), 0) AS INT)
                )
            ) AS [Opening Stock],

                 

            (
                CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                    FROM Production_Details 
                    LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName     
                    WHERE PD_Route_Card_ID = Pq_iid 
                      AND CONVERT(DATE, PD_Date, 113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
                +
                CAST(ISNULL((SELECT SUM(CS_Qty) 
                    FROM Current_Stock 
                    WHERE CS_Item_ID = IM_ID AND CS_Type = '7' 
                      AND CONVERT(DATE, CS_CreatedDate, 113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
                -
                CAST(ISNULL((SELECT SUM(fe_ok + fe_rej) 
                    FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                    WHERE fe_partid = IM_ID  and fe_status='A'
                      AND CONVERT(DATE, fe_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2))
            ) AS [WIP Stock Qty],

            (
                CAST(ISNULL((SELECT SUM(fe_ok) 
                    FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                    WHERE fe_partid = IM_ID  and fe_status='A'
                      AND CONVERT(DATE, fe_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2))
                +
                CAST(ISNULL((SELECT SUM(wor_Qty) 
                    FROM Work_Order_Receive_Details  
                    WHERE wor_status = 'A' 
                      AND wor_MaterialType = 'FG' 
                      AND WOR_PID = IM_ID  
                      AND CONVERT(DATE, wor_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
                +
                CAST(ISNULL((SELECT SUM(AS_Qty) 
                    FROM Assembly_Details  
                    WHERE AS_Status = 'A' 
                      AND AS_Part_ID = IM_ID  
                      AND CONVERT(DATE, AS_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
            ) AS [FG QTY],

            0 AS [Invoice Qty],
            0 AS [Closing Qty],

            0 AS [wip closing stock],
            0 AS [fg closing],
            0 AS [Closing Stock],
            0 AS [Remarks]
        FROM Item_Master                               
        WHERE IM_Status = 'A'                           
        ORDER BY IM_ID");


            // Update Invoice Qty
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                dr["Invoice Qty"] = match.Length > 0 ? match[0]["Qty"] : 0;
            }

            // Update FG QTY by subtracting today's invoice from today's FG QTY
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                if (match.Length > 0)
                {
                    decimal fgQty = Convert.ToDecimal(dr["FG QTY"]);
                    decimal invQty = Convert.ToDecimal(match[0]["Qty"]);
                    dr["FG QTY"] = fgQty - invQty;
                }
            }

            // Adjust Opening Stock by subtracting past invoice qty
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt2.Select("[SD_Part_No] = '" + partNo + "'");
                if (match.Length > 0)
                {
                    decimal opening = Convert.ToDecimal(dr["Opening Stock"]);
                    decimal pastInvoice = Convert.ToDecimal(match[0]["Qty"]);
                    dr["Opening Stock"] = opening - pastInvoice;
                }

                // Calculate Closing Qty
                decimal openingStock = Convert.ToDecimal(dr["Opening Stock"]);
                decimal wipQty = Convert.ToDecimal(dr["WIP Stock Qty"]);
                decimal fgQty = Convert.ToDecimal(dr["FG QTY"]);
                decimal closingQty = openingStock + wipQty + fgQty;
                dr["Closing Qty"] = Math.Round(closingQty, 2);
            }

            // Update WIP Opening and FG Opening
            //foreach (DataRow dr in dt.Rows)
            //{
            //    decimal closing = Convert.ToDecimal(dr["Closing Qty"]);
            //    decimal fg = Convert.ToDecimal(dr["FG QTY"]);
            //    dr["Wip Opening"] = closing - fg;

            //    // FG Opening = Yesterday's FG QTY
            //    string partNo = dr["Part No"].ToString();
            //    DataRow[] fgMatch = dt_yesterdayFG.Select("IM_PartNo = '" + partNo + "'");
            //   // DataRow[] match = dt2.Select("[SD_Part_No] = '" + partNo + "'");
            //    dr["FG Opening"] = fgMatch.Length > 0 ? fgMatch[0]["YesterdayFG"] : 0;
            //}
            // Update WIP Opening and FG Opening
         
            foreach (DataRow dr in dt.Rows)
            {
                decimal WIPOP = Convert.ToDecimal(dr["Wip Opening"]);
                decimal PRO = Convert.ToDecimal(dr["prod qty"]);
                dr["total wip stock"] = WIPOP + PRO;

                decimal FG = Convert.ToDecimal(dr["FG Opening"]);
                decimal FI = Convert.ToDecimal(dr["final inspection"]);

                dr["total fg stock"] = FG + FI;


                decimal tws = Convert.ToDecimal(dr["total wip stock"]);
                decimal FIs = Convert.ToDecimal(dr["final inspection"]);

                dr["wip closing stock"] = tws - FIs;



            }
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                if (match.Length > 0)
                {
                    decimal tfgs = Convert.ToDecimal(dr["total fg stock"]);
                    decimal invQty = Convert.ToDecimal(match[0]["Qty"]);
                    dr["fg closing"] = tfgs - invQty;
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                decimal wip = Convert.ToDecimal(dr["wip closing stock"]);
                decimal fg = Convert.ToDecimal(dr["fg closing"]);

                dr["Closing Stock"] = wip + fg;

            }


            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);


         

        }

        private void DATAVALUE4()
        {
            string fromDate = dtpFrom.Value.ToString("yyyyMMdd");
            string toDate = todate.Value.ToString("yyyyMMdd");
            string previousDate = dtpFrom.Value.AddDays(-1).ToString("yyyyMMdd");

            // Load Sales Invoice Details (today)
             DataTable dt1 = dbFunctions.getTable1(@"
            SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
            FROM dbo.Sales_Invoice_Details
            WHERE SD_Status = 'A' 
              AND CONVERT(DATE, SD_Created_Date, 112) 
              BETWEEN '" + fromDate + @"' AND '" + toDate + @"'  AND CONVERT(date, SD_Created_Date, 112) >= '20250407'
            GROUP BY SD_Part_No");

            // Load Sales Invoice Details (before today)
            DataTable dt2 = dbFunctions.getTable1(@"
            SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
            FROM dbo.Sales_Invoice_Details
            WHERE SD_Status = 'A' 
              AND CONVERT(DATE, SD_Created_Date, 112) 
                  < '" + fromDate + @"' AND CONVERT(date, SD_Created_Date, 112) >= '20250407'
            GROUP BY SD_Part_No");

            // Load yesterday's FG Qty
            DataTable dt_yesterdayFG = dbFunctions.getTable(@"
            SELECT 
                IM_ID, 
                IM_PartNo, 
                (
                    ISNULL((SELECT SUM(fe_ok) 
                            FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                            WHERE fe_partid = IM_ID and fe_status='A'
                              AND CONVERT(DATE, fe_Date, 112) = '" + previousDate + @"'), 0)
                    +
                    ISNULL((SELECT SUM(wor_Qty) 
                            FROM Work_Order_Receive_Details  
                            WHERE wor_status = 'A' 
                              AND wor_MaterialType = 'FG' 
                              AND WOR_PID = IM_ID  
                              AND CONVERT(DATE, wor_Date, 112) = '" + previousDate + @"'), 0)
                    +
                    ISNULL((SELECT SUM(AS_Qty) 
                            FROM Assembly_Details  
                            WHERE AS_Status = 'A' 
                              AND AS_Part_ID = IM_ID  
                              AND CONVERT(DATE, AS_Date, 112) = '" + previousDate + @"'), 0)
                ) AS YesterdayFG
            FROM Item_Master
            WHERE IM_Status = 'A'");

            DataTable dt_yesterdayInvoice = dbFunctions.getTable1(@"
            SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
            FROM dbo.Sales_Invoice_Details
            WHERE SD_Status = 'A' 
              AND CONVERT(DATE, SD_Created_Date, 112) = '" + previousDate + @"'  AND CONVERT(date, SD_Created_Date, 112) >= '20250407'
            GROUP BY SD_Part_No");

            // Main item data
            DataTable dt = dbFunctions.getTable(@"
            SELECT                               
            IM_ID AS ID,                              
            ROW_NUMBER() OVER(ORDER BY IM_ID) AS [S.No],                        
            IM_PartNo AS [Part No],                              
            IM_PartName AS [Part Name],                              
            IM_Sales_Price AS [Sales Price],
            0 AS [Opening Stock1],
            0 AS [Wip Opening],
         
               CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                    FROM Production_Details 
                    LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName     
                    WHERE PD_Route_Card_ID = Pq_iid 
                      AND CONVERT(DATE, PD_Date, 113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT) AS [prod qty],
               0 AS [total wip stock], 
               0 AS [FG Opening],
                    CAST(ISNULL((SELECT SUM(fe_ok) 
                    FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                    WHERE fe_partid = IM_ID  and fe_status='A'
                      AND CONVERT(DATE, fe_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) AS [final inspection],
                0 AS [total fg stock],
            (
                (
                    CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                        FROM Production_Details 
                        LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName     
                        WHERE PD_Route_Card_ID = Pq_iid 
                          AND CONVERT(DATE, PD_Date, 113) < '" + fromDate + @"'), 0) AS INT)
                    +
                    CAST(ISNULL((SELECT SUM(CS_Qty) 
                        FROM Current_Stock 
                        WHERE CS_Item_ID = IM_ID AND CS_Type = '7' 
                          AND CONVERT(DATE, CS_CreatedDate, 113) < '" + fromDate + @"'), 0) AS INT)
                    -
                    CAST(ISNULL((SELECT SUM(fe_ok + fe_rej) 
                        FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                        WHERE fe_partid = IM_ID and fe_status='A'
                          AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2))
                )
                +
                (
                    CAST(ISNULL((SELECT SUM(fe_ok) 
                        FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                        WHERE fe_partid = IM_ID and fe_status='A'
                          AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2))
                    +
                    CAST(ISNULL((SELECT SUM(wor_Qty) 
                        FROM Work_Order_Receive_Details  
                        WHERE wor_status = 'A' 
                          AND wor_MaterialType = 'FG' 
                          AND WOR_PID = IM_ID  
                          AND CONVERT(DATE, wor_Date, 112) < '" + fromDate + @"'), 0) AS INT)
                    +
                    CAST(ISNULL((SELECT SUM(AS_Qty) 
                        FROM Assembly_Details  
                        WHERE AS_Status = 'A' 
                          AND AS_Part_ID = IM_ID  
                          AND CONVERT(DATE, AS_Date, 112) < '" + fromDate + @"'), 0) AS INT)
                )
            ) AS [Opening Stock],

                 

            (
                CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                    FROM Production_Details 
                    LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName     
                    WHERE PD_Route_Card_ID = Pq_iid 
                      AND CONVERT(DATE, PD_Date, 113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
                +
                CAST(ISNULL((SELECT SUM(CS_Qty) 
                    FROM Current_Stock 
                    WHERE CS_Item_ID = IM_ID AND CS_Type = '7' 
                      AND CONVERT(DATE, CS_CreatedDate, 113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
                -
                CAST(ISNULL((SELECT SUM(fe_ok + fe_rej) 
                    FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                    WHERE fe_partid = IM_ID  and fe_status='A'
                      AND CONVERT(DATE, fe_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2))
            ) AS [WIP Stock Qty],

            (
                CAST(ISNULL((SELECT SUM(fe_ok) 
                    FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                    WHERE fe_partid = IM_ID  and fe_status='A'
                      AND CONVERT(DATE, fe_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2))
                +
                CAST(ISNULL((SELECT SUM(wor_Qty) 
                    FROM Work_Order_Receive_Details  
                    WHERE wor_status = 'A' 
                      AND wor_MaterialType = 'FG' 
                      AND WOR_PID = IM_ID  
                      AND CONVERT(DATE, wor_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
                +
                CAST(ISNULL((SELECT SUM(AS_Qty) 
                    FROM Assembly_Details  
                    WHERE AS_Status = 'A' 
                      AND AS_Part_ID = IM_ID  
                      AND CONVERT(DATE, AS_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
            ) AS [FG QTY],

            0 AS [Invoice Qty],
            0 AS [Closing Qty],

            0 AS [wip closing stock],
            0 AS [fg closing],
            0 AS [Closing Stock],
            0 AS [Remarks]
        FROM Item_Master                               
        WHERE IM_Status = 'A'                           
        ORDER BY IM_ID");


            // Update Invoice Qty
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                dr["Invoice Qty"] = match.Length > 0 ? match[0]["Qty"] : 0;
            }

            // Update FG QTY by subtracting today's invoice from today's FG QTY
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                if (match.Length > 0)
                {
                    decimal fgQty = Convert.ToDecimal(dr["FG QTY"]);
                    decimal invQty = Convert.ToDecimal(match[0]["Qty"]);
                    dr["FG QTY"] = fgQty - invQty;
                }
            }

            // Adjust Opening Stock by subtracting past invoice qty
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt2.Select("[SD_Part_No] = '" + partNo + "'");
                if (match.Length > 0)
                {
                    decimal opening = Convert.ToDecimal(dr["Opening Stock"]);
                    decimal pastInvoice = Convert.ToDecimal(match[0]["Qty"]);
                    dr["Opening Stock"] = opening - pastInvoice;
                }

                // Calculate Closing Qty
                decimal openingStock = Convert.ToDecimal(dr["Opening Stock"]);
                decimal wipQty = Convert.ToDecimal(dr["WIP Stock Qty"]);
                decimal fgQty = Convert.ToDecimal(dr["FG QTY"]);
                decimal closingQty = openingStock + wipQty + fgQty;
                dr["Closing Qty"] = Math.Round(closingQty, 2);
            }

            // Update WIP Opening and FG Opening
            //foreach (DataRow dr in dt.Rows)
            //{
            //    decimal closing = Convert.ToDecimal(dr["Closing Qty"]);
            //    decimal fg = Convert.ToDecimal(dr["FG QTY"]);
            //    dr["Wip Opening"] = closing - fg;

            //    // FG Opening = Yesterday's FG QTY
            //    string partNo = dr["Part No"].ToString();
            //    DataRow[] fgMatch = dt_yesterdayFG.Select("IM_PartNo = '" + partNo + "'");
            //   // DataRow[] match = dt2.Select("[SD_Part_No] = '" + partNo + "'");
            //    dr["FG Opening"] = fgMatch.Length > 0 ? fgMatch[0]["YesterdayFG"] : 0;
            //}
            // Update WIP Opening and FG Opening
            foreach (DataRow dr in dt.Rows)
            {
                

                // FG Opening = Yesterday's FG - Yesterday's Invoice Qty
                string partNo = dr["Part No"].ToString();

                // Get Yesterday's FG
                DataRow[] fgMatch = dt_yesterdayFG.Select("IM_PartNo = '" + partNo + "'");
                decimal yesterdayFG = fgMatch.Length > 0 ? Convert.ToDecimal(fgMatch[0]["YesterdayFG"]) : 0;

                // Get Yesterday's Invoice Qty
                DataRow[] invMatch = dt_yesterdayInvoice.Select("SD_Part_No = '" + partNo + "'");
                decimal yesterdayInvoice = invMatch.Length > 0 ? Convert.ToDecimal(invMatch[0]["Qty"]) : 0;

                dr["FG Opening"] = yesterdayFG - yesterdayInvoice;


                decimal closing = Convert.ToDecimal(dr["Closing Qty"]);
                decimal fg = Convert.ToDecimal(dr["FG Opening"]);
                dr["Wip Opening"] = closing - fg;



            }
            foreach (DataRow dr in dt.Rows)
            {
                decimal WIPOP = Convert.ToDecimal(dr["Wip Opening"]);
                decimal PRO = Convert.ToDecimal(dr["prod qty"]);
                dr["total wip stock"] = WIPOP + PRO;

                decimal FG = Convert.ToDecimal(dr["FG Opening"]);
                decimal FI = Convert.ToDecimal(dr["final inspection"]);

                dr["total fg stock"] = FG + FI;


                decimal tws = Convert.ToDecimal(dr["total wip stock"]);
                decimal FIs = Convert.ToDecimal(dr["final inspection"]);

                dr["wip closing stock"] = tws - FIs;               

            }

            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                if (match.Length > 0)
                {
                    decimal tfgs = Convert.ToDecimal(dr["total fg stock"]);
                    decimal invQty = Convert.ToDecimal(match[0]["Qty"]);
                    dr["fg closing"] = tfgs - invQty;
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                decimal wip = Convert.ToDecimal(dr["wip closing stock"]);
                decimal fg = Convert.ToDecimal(dr["fg closing"]);

                dr["Closing Stock"] = wip + fg;

            }


                dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            dataGridView1.Columns["WIP Stock Qty"].Visible = false;
            dataGridView1.Columns["FG QTY"].Visible = false;
            dataGridView1.Columns["Closing Qty"].Visible = false;
            dataGridView1.Columns["Closing Qty"].Visible = false; 
            dataGridView1.Columns["Opening Stock"].Visible = false; 



        }

        private void DATAVALUE3()
        {
            string fromDate = dtpFrom.Value.ToString("yyyyMMdd");
            string toDate = todate.Value.ToString("yyyyMMdd");
            string previousDate = dtpFrom.Value.AddDays(-1).ToString("yyyyMMdd");

            // Load DT1: Sales Invoice Details
            DataTable dt1 = dbFunctions.getTable1(@"
                    SELECT 
                        SD_Part_No, 
                        SUM(SD_Qty) AS Qty 
                    FROM dbo.Sales_Invoice_Details
                    WHERE SD_Status = 'A' 
                      AND CONVERT(DATE, SD_Created_Date, 112) 
                          BETWEEN '" + fromDate + @"' AND '" + toDate + @"' 
                    GROUP BY SD_Part_No");

            DataTable dt2 = dbFunctions.getTable1(@"
                    SELECT 
                        SD_Part_No, 
                        SUM(SD_Qty) AS Qty 
                    FROM dbo.Sales_Invoice_Details
                    WHERE SD_Status = 'A' 
                      AND CONVERT(DATE, SD_Created_Date, 112) 
                          < '" + fromDate + @"' 
                    GROUP BY SD_Part_No");


            DataTable dt = dbFunctions.getTable(@"
                     SELECT                               
                    IM_ID AS ID,                              
                    ROW_NUMBER() OVER(ORDER BY IM_ID) AS [S.No],                        
                    IM_PartNo AS [Part No],                              
                    IM_PartName AS [Part Name],                              
                    IM_Sales_Price AS [Sales Price],


        -- wip Opening

                          0 as [Wip Opening],

            ---  fg opening

                        0 as as [FG Opening],

                    ((((cast(isnull((select sum(PD_OK_Qty) from  Production_Details left outer join Production_Request  on Pq_vPart_No=IM_PartName     
                        where  PD_Route_Card_ID=Pq_iid and Convert(date,PD_Date,113)  <   '" + fromDate + @"'  ),0) as int))      
                        +      
                (cast(isnull((select sum(CS_Qty) from  Current_Stock where  CS_Item_ID=im_id AND CS_Type='7' and
                Convert(date,CS_CreatedDate,113)  <   '" + fromDate + @"'  ),0) as int)))-( CAST(ISNULL((SELECT SUM(fe_ok+fe_rej) 
                                FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                                WHERE fe_partid = IM_ID  and fe_status='A'
                                  AND CONVERT(DATE, fe_Date, 112) 
                                      < '" + fromDate + @"'), 0) AS DECIMAL(18,2)) ))

                    +
                    (((CAST(ISNULL((SELECT SUM(fe_ok) 
                                FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                                WHERE fe_partid = IM_ID  and fe_status='A'
                                  AND CONVERT(DATE, fe_Date, 112) 
                                      < '" + fromDate + @"'), 0) AS DECIMAL(18,2)) )+( CAST(ISNULL((SELECT SUM(wor_Qty)  
                        FROM Work_Order_Receive_Details  
                        WHERE wor_status = 'A' 
                          AND wor_MaterialType = 'FG' 
                          AND WOR_PID = IM_ID  
                          AND CONVERT(DATE, wor_Date, 112) 
                              < '" + fromDate + @"' ), 0) AS INT))+(CAST(ISNULL((SELECT SUM(AS_Qty)  
                        FROM Assembly_Details  
                        WHERE AS_Status = 'A' 
                          AND AS_Part_ID = IM_ID  
                          AND CONVERT(DATE, AS_Date, 112) 
                              < '" + fromDate + @"' ), 0) AS INT)))))
                    as [Opening Stock],


                ---  wip stock 
                    ((cast(isnull((select sum(PD_OK_Qty) from  Production_Details left outer join Production_Request  on Pq_vPart_No=IM_PartName     
                        where  PD_Route_Card_ID=Pq_iid and Convert(date,PD_Date,113)  BETWEEN   '" + fromDate + @"' AND '" + toDate + @"' ),0) as int))      
                        +      
                (cast(isnull((select sum(CS_Qty) from  Current_Stock where  CS_Item_ID=im_id AND CS_Type='7' and
                Convert(date,CS_CreatedDate,113)  BETWEEN   '" + fromDate + @"' AND '" + toDate + @"' ),0) as int)))-( CAST(ISNULL((SELECT SUM(fe_ok+fe_rej) 
                                FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                                WHERE fe_partid = IM_ID  and fe_status='A'
                                  AND CONVERT(DATE, fe_Date, 112) 
                                      BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) ) as [WIP Stock Qty],

                    --- fg qty

                     ((CAST(ISNULL((SELECT SUM(fe_ok) 
                                FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                                WHERE fe_partid = IM_ID  and fe_status='A'
                                  AND CONVERT(DATE, fe_Date, 112) 
                                      BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) )+( CAST(ISNULL((SELECT SUM(wor_Qty)  
                        FROM Work_Order_Receive_Details  
                        WHERE wor_status = 'A' 
                          AND wor_MaterialType = 'FG' 
                          AND WOR_PID = IM_ID  
                          AND CONVERT(DATE, wor_Date, 112) 
                              BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT))+(CAST(ISNULL((SELECT SUM(AS_Qty)  
                        FROM Assembly_Details  
                        WHERE AS_Status = 'A' 
                          AND AS_Part_ID = IM_ID  
                          AND CONVERT(DATE, AS_Date, 112) 
                              BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT))) AS [FG QTY],

            
                            0 AS [Invoice Qty], -- Placeholder
                            0 AS [Closing Qty],     -- New Balance column
                            0 AS [Remarks]
                         FROM Item_Master                               
                                        WHERE IM_Status = 'A'                           
                                        ORDER BY IM_ID");

            // Update "Invoice Qty" from dt1 into dt
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                if (match.Length > 0)
                {
                    dr["Invoice Qty"] = match[0]["Qty"];
                }
                else
                {
                    dr["Invoice Qty"] = 0;
                }
            }



            foreach (DataRow dr in dt.Rows)
            {

                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                if (match.Length > 0)
                {
                    decimal existingOpening = Convert.ToDecimal(dr["FG QTY"]);
                    decimal dt2Qty = Convert.ToDecimal(match[0]["Qty"]);
                    dr["FG QTY"] = existingOpening - dt2Qty;
                }

            }

            //  

            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt2.Select("[SD_Part_No] = '" + partNo + "'");
                if (match.Length > 0)
                {
                    decimal existingOpening = Convert.ToDecimal(dr["Opening Stock"]);
                    decimal dt2Qty = Convert.ToDecimal(match[0]["Qty"]);
                    dr["Opening Stock"] = existingOpening - dt2Qty;
                }

                // Calculate Closing Qty
                decimal openingStock = Convert.ToDecimal(dr["Opening Stock"]);
                decimal wipQty = Convert.ToDecimal(dr["WIP Stock Qty"]);
                decimal fgQty = Convert.ToDecimal(dr["FG QTY"]);
                decimal invoiceQty = Convert.ToDecimal(dr["Invoice Qty"]);

                decimal closingQty = (openingStock + wipQty + fgQty);
                dr["Closing Qty"] = Math.Round(closingQty, 2); // Optional rounding
            }



            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);

        }

        private void DATAVALUE2()
        {
            string fromDate = dtpFrom.Value.ToString("yyyyMMdd");
            string toDate = todate.Value.ToString("yyyyMMdd");
            string previousDate = dtpFrom.Value.AddDays(-1).ToString("yyyyMMdd");

            // Get today's invoice
            DataTable dt1 = dbFunctions.getTable1(@"
    SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
    FROM dbo.Sales_Invoice_Details
    WHERE SD_Status = 'A' 
      AND CONVERT(DATE, SD_Created_Date, 112) 
      BETWEEN '" + fromDate + @"' AND '" + toDate + @"'  
      AND CONVERT(DATE, SD_Created_Date, 112) >= '20250407'
    GROUP BY SD_Part_No");

            // Get invoices before today
            DataTable dt2 = dbFunctions.getTable1(@"
    SELECT SD_Part_No, SUM(SD_Qty) AS Qty 
    FROM dbo.Sales_Invoice_Details
    WHERE SD_Status = 'A' 
      AND CONVERT(DATE, SD_Created_Date, 112) 
      < '" + fromDate + @"' 
      AND CONVERT(DATE, SD_Created_Date, 112) >= '20250407'
    GROUP BY SD_Part_No");

            // Get yesterday's FG
            DataTable dt_yesterdayFG = dbFunctions.getTable(@"
    SELECT 
        IM_ID, 
        IM_PartNo, 
        (
            ISNULL((SELECT SUM(fe_ok) 
                    FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                    WHERE fe_partid = IM_ID and fe_status='A'
                      AND CONVERT(DATE, fe_Date, 112) = '" + previousDate + @"'), 0)
            +
            ISNULL((SELECT SUM(wor_Qty) 
                    FROM Work_Order_Receive_Details  
                    WHERE wor_status = 'A' AND wor_MaterialType = 'FG' 
                      AND WOR_PID = IM_ID  
                      AND CONVERT(DATE, wor_Date, 112) = '" + previousDate + @"'), 0)
            +
            ISNULL((SELECT SUM(AS_Qty) 
                    FROM Assembly_Details  
                    WHERE AS_Status = 'A' 
                      AND AS_Part_ID = IM_ID  
                      AND CONVERT(DATE, AS_Date, 112) = '" + previousDate + @"'), 0)
        ) AS YesterdayFG
    FROM Item_Master
    WHERE IM_Status = 'A'");

            // Main Query
            DataTable dt = dbFunctions.getTable(@"
    SELECT                               
        IM_ID AS ID,                              
        ROW_NUMBER() OVER(ORDER BY IM_ID) AS [S.No],                        
        IM_PartNo AS [Part No],                              
        IM_PartName AS [Part Name],                              
        IM_Sales_Price AS [Sales Price],
        0 AS [Wip Opening],
     
        CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
            FROM Production_Details 
            LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName     
            WHERE PD_Route_Card_ID = Pq_iid 
              AND CONVERT(DATE, PD_Date, 113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT) AS [prod qty],

        0 AS [total wip stock], 
        0 AS [FG Opening],

        CAST(ISNULL((SELECT SUM(fe_ok) 
            FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
            WHERE fe_partid = IM_ID and fe_status='A'
              AND CONVERT(DATE, fe_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) AS [final inspection],

        0 AS [total fg stock],

        (
            (
                CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                    FROM Production_Details 
                    LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName     
                    WHERE PD_Route_Card_ID = Pq_iid 
                      AND CONVERT(DATE, PD_Date, 113) < '" + fromDate + @"'), 0) AS INT)
                +
                CAST(ISNULL((SELECT SUM(CS_Qty) 
                    FROM Current_Stock 
                    WHERE CS_Item_ID = IM_ID AND CS_Type = '7' 
                      AND CONVERT(DATE, CS_CreatedDate, 113) < '" + fromDate + @"'), 0) AS INT)
                -
                CAST(ISNULL((SELECT SUM(fe_ok + fe_rej) 
                    FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                    WHERE fe_partid = IM_ID and fe_status='A'
                      AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2))
            )
            +
            (
                CAST(ISNULL((SELECT SUM(fe_ok) 
                    FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                    WHERE fe_partid = IM_ID and fe_status='A'
                      AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2))
                +
                CAST(ISNULL((SELECT SUM(wor_Qty) 
                    FROM Work_Order_Receive_Details  
                    WHERE wor_status = 'A' 
                      AND wor_MaterialType = 'FG' 
                      AND WOR_PID = IM_ID  
                      AND CONVERT(DATE, wor_Date, 112) < '" + fromDate + @"'), 0) AS INT)
                +
                CAST(ISNULL((SELECT SUM(AS_Qty) 
                    FROM Assembly_Details  
                    WHERE AS_Status = 'A' 
                      AND AS_Part_ID = IM_ID  
                      AND CONVERT(DATE, AS_Date, 112) < '" + fromDate + @"'), 0) AS INT)
            )
        ) AS [Opening Stock],

        (
            CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName     
                WHERE PD_Route_Card_ID = Pq_iid 
                  AND CONVERT(DATE, PD_Date, 113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
            +
            CAST(ISNULL((SELECT SUM(CS_Qty) 
                FROM Current_Stock 
                WHERE CS_Item_ID = IM_ID AND CS_Type = '7' 
                  AND CONVERT(DATE, CS_CreatedDate, 113) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
            -
            CAST(ISNULL((SELECT SUM(fe_ok + fe_rej) 
                FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                WHERE fe_partid = IM_ID and fe_status='A'
                  AND CONVERT(DATE, fe_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2))
        ) AS [WIP Stock Qty],

        (
            CAST(ISNULL((SELECT SUM(fe_ok) 
                FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                WHERE fe_partid = IM_ID and fe_status='A'
                  AND CONVERT(DATE, fe_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2))
            +
            CAST(ISNULL((SELECT SUM(wor_Qty) 
                FROM Work_Order_Receive_Details  
                WHERE wor_status = 'A' 
                  AND wor_MaterialType = 'FG' 
                  AND WOR_PID = IM_ID  
                  AND CONVERT(DATE, wor_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
            +
            CAST(ISNULL((SELECT SUM(AS_Qty) 
                FROM Assembly_Details  
                WHERE AS_Status = 'A' 
                  AND AS_Part_ID = IM_ID  
                  AND CONVERT(DATE, AS_Date, 112) BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
        ) AS [FG QTY],

        0 AS [Invoice Qty],
        0 AS [Closing Qty],
        0 AS [Total Wip Stock],
        0 AS [Total FG Stock],
        0 AS [Remarks]
    FROM Item_Master                               
    WHERE IM_Status = 'A'                           
    ORDER BY IM_ID");

            // === Update Columns After Query ===

            // 1. Invoice Qty (Today)
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                dr["Invoice Qty"] = match.Length > 0 ? Convert.ToDecimal(match[0]["Qty"]) : 0;
            }

            // 2. FG QTY -= Invoice Qty (today)
            foreach (DataRow dr in dt.Rows)
            {
                decimal fgQty = Convert.ToDecimal(dr["FG QTY"]);
                decimal invQty = Convert.ToDecimal(dr["Invoice Qty"]);
                dr["FG QTY"] = fgQty - invQty;
            }

            // 3. Adjust Opening Stock -= Invoice Qty (before today)
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt2.Select("[SD_Part_No] = '" + partNo + "'");
                decimal opening = Convert.ToDecimal(dr["Opening Stock"]);
                decimal pastInv = match.Length > 0 ? Convert.ToDecimal(match[0]["Qty"]) : 0;
                dr["Opening Stock"] = opening - pastInv;

                // 4. Closing Qty = Opening + WIP + FG
                decimal wipQty = Convert.ToDecimal(dr["WIP Stock Qty"]);
                decimal fg = Convert.ToDecimal(dr["FG QTY"]);
                dr["Closing Qty"] = Math.Round(opening + wipQty + fg, 2);
            }

            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
         //   dataGridView1.Columns["Closing Qty1"].Visible = false;


        }

        private void getdata1()
        {
            dataGridView1.DataSource = null;  // Clear the existing data
            dataGridView1.Rows.Clear();       // Remove all rows
            dataGridView1.Columns.Clear();
            DataTable dt = dbFunctions.getTable("pr_get_Overall_WIP");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            //dataGridView1.Columns["FG STOCK"].DefaultCellStyle.ForeColor = Color.Green;
        }

        private void getdata()
        {
            //DataTable dt = dbFunctions.getTable("pr_get_WIP  '" + dtpFrom.Value.ToString("yyyyMM01") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            DataTable dt = dbFunctions.getTable("pr_get_WIP '" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
          //  dataGridView1.Columns["FG STOCK"].DefaultCellStyle.ForeColor = Color.Green;
        }

        private void Wip_Stock_Load(object sender, EventArgs e)
        {
            // getdata();
            DATAVALUE3();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBoxX1_TextChanged(object sender, EventArgs e)
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
        }

        private void Button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void todate_ValueChanged(object sender, EventArgs e)
        {
            final();//getdata();
                    // DATAVALUE3();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getdata1();
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            DATAVALUE3();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
               // panel1.Visible = true;
                string fromDate = dtpFrom.Value.ToString("yyyyMMdd");
                string toDate = todate.Value.ToString("yyyyMMdd");
                DataTable a = dbFunctions.getTable(@"
                     SELECT                               
                    IM_ID AS ID,                              
                    ROW_NUMBER() OVER(ORDER BY IM_ID) AS [S.No],                        
                    IM_PartNo AS [Part No],                              
                    IM_PartName AS [Part Name],
                    (cast(isnull((select sum(PD_OK_Qty) from  Production_Details left outer join Production_Request  on Pq_vPart_No=IM_PartName     
                        where  PD_Route_Card_ID=Pq_iid and Convert(date,PD_Date,113)  BETWEEN   '" + fromDate + @"' AND '" + toDate + @"' ),0) as int)) AS [Pro OK Qty],

                cast(isnull((select sum(CS_Qty) from  Current_Stock where  CS_Item_ID=im_id AND CS_Type='7' and
                Convert(date,CS_CreatedDate,113)  BETWEEN   '" + fromDate + @"' AND '" + toDate + @"' ),0) as int) as [Stock update],

CAST(ISNULL((SELECT SUM(fe_ok+fe_rej) 
                                FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                                WHERE fe_partid = IM_ID 
                                  AND CONVERT(DATE, fe_Date, 112) 
                                      BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) as [Quality ok and rej],
                  
                      ((cast(isnull((select sum(PD_OK_Qty) from  Production_Details left outer join Production_Request  on Pq_vPart_No=IM_PartName     
                        where  PD_Route_Card_ID=Pq_iid and Convert(date,PD_Date,113)  BETWEEN   '" + fromDate + @"' AND '" + toDate + @"' ),0) as int))      
                        +      
                (cast(isnull((select sum(CS_Qty) from  Current_Stock where  CS_Item_ID=im_id AND CS_Type='7' and
                Convert(date,CS_CreatedDate,113)  BETWEEN   '" + fromDate + @"' AND '" + toDate + @"' ),0) as int)))-( CAST(ISNULL((SELECT SUM(fe_ok+fe_rej) 
                                FROM [CHE_SRUTHY_PLASTIC].[final_entry]  
                                WHERE fe_partid = IM_ID 
                                  AND CONVERT(DATE, fe_Date, 112) 
                                      BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) ) as [WIP Stock Qty]

                  FROM Item_Master                               
                WHERE IM_Status = 'A'    and IM_ID='" + dataGridView1.SelectedRows[0].Cells["ID"].Value + @"'                        
                ORDER BY IM_ID");
                dataGridView2.DataSource = a;
                dbFunctions.DGVStyle(dataGridView2);
            }
            if (e.ColumnIndex == 7)
            {
              //  panel1.Visible = true;
                string fromDate = dtpFrom.Value.ToString("yyyyMMdd");
                string toDate = todate.Value.ToString("yyyyMMdd");
                DataTable a=dbFunctions.getTable(@"
             SELECT                               
                    IM_ID AS ID,                              
                    ROW_NUMBER() OVER(ORDER BY IM_ID) AS [S.No],                        
                    IM_PartNo AS [Part No],                              
                    IM_PartName AS [Part Name],
                CAST(ISNULL((SELECT SUM(fe_ok)
                                FROM[CHE_SRUTHY_PLASTIC].[final_entry]
                                WHERE fe_partid = IM_ID
                                  AND CONVERT(DATE, fe_Date, 112)
                                      BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) as [FI OK],

                CAST(ISNULL((SELECT SUM(wor_Qty)
                        FROM Work_Order_Receive_Details
                        WHERE wor_status = 'A'
                          AND wor_MaterialType = 'FG'
                          AND WOR_PID = IM_ID
                          AND CONVERT(DATE, wor_Date, 112)
                              BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT) as [DC],

            CAST(ISNULL((SELECT SUM(AS_Qty)
                        FROM Assembly_Details
                        WHERE AS_Status = 'A'
                          AND AS_Part_ID = IM_ID
                          AND CONVERT(DATE, AS_Date, 112)
                              BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT) as [Ass Qty],

                ((CAST(ISNULL((SELECT SUM(fe_ok)
                                FROM[CHE_SRUTHY_PLASTIC].[final_entry]
                                WHERE fe_partid = IM_ID
                                  AND CONVERT(DATE, fe_Date, 112)
                                      BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) )+(CAST(ISNULL((SELECT SUM(wor_Qty)
                        FROM Work_Order_Receive_Details
                        WHERE wor_status = 'A'
                          AND wor_MaterialType = 'FG'
                          AND WOR_PID = IM_ID
                          AND CONVERT(DATE, wor_Date, 112)
                              BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT))+(CAST(ISNULL((SELECT SUM(AS_Qty)
                        FROM Assembly_Details
                        WHERE AS_Status = 'A'
                          AND AS_Part_ID = IM_ID
                          AND CONVERT(DATE, AS_Date, 112)
                              BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT))) AS[FG QTY]
            FROM Item_Master                               
                WHERE IM_Status = 'A'    and IM_ID='" + dataGridView1.SelectedRows[0].Cells["ID"].Value + @"'                        
                ORDER BY IM_ID
                    ");

                dataGridView2.DataSource = a;
                dbFunctions.DGVStyle(dataGridView2);
            }
           int rclososing_qty = 0;
            if (e.ColumnIndex == 16)
            {
                DataTable a=dbFunctions.getTable(" select BM_Id,BM_BomId,BM_ItemId,BM_Qty,BM_UOM,* from bom_master where bm_bomid='" + dataGridView1.SelectedRows[0].Cells["ID"].Value + "'");
                 rclososing_qty = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);

                if (a.Rows.Count != 0)
                {
                    int remaining = Convert.ToInt32(a.Rows[0]["BM_Qty"]);

                    int totalrm = remaining * rclososing_qty;
                    panel2.Visible = true;
                }
                dataGridView2.DataSource = a;
                dbFunctions.DGVStyle(dataGridView2);

            }

            }

        private void button12_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void dtpFrom_ValueChanged_1(object sender, EventArgs e)
        {
            final();
        }
    }
}
