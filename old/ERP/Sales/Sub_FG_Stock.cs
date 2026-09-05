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
    public partial class Sub_FG_Stock : Form
    {
        public Sub_FG_Stock()
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
            DataTable dt = dbFunctions.getTable("pr_get_Sub_FG_Stock  '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
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
           // FINAL();




        }

        private void ToadayStatus_Activated(object sender, EventArgs e)
        {
            getdata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getdata();
            //FINAL();
        }

        private void FINAL()
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


            DataTable dt = dbFunctions.getTable(@"
            SELECT                               
            IM_ID AS ID,                              
            ROW_NUMBER() OVER(ORDER BY IM_ID) AS [S.No],                        
            IM_PartNo AS [Part No],                              
            IM_PartName AS [Part Name],
            0 as [OPENING STOCK],           
            0 as [WIP OPENING STOCK],
              CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName AND Pq_Status='A'    
                WHERE PD_Route_Card_ID = Pq_iid AND PD_Status='A'    
                  AND CONVERT(DATE, PD_Date, 113)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT) 
            +
			  CAST(ISNULL((SELECT SUM(CS_Qty)
             FROM CURRENT_STOCK
            WHERE CS_Item_ID = IM_ID  and CS_Status = 'A' AND CS_Type=6
            AND CONVERT(DATE, CS_CreatedDate, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT) as [PRD OK QTY],

            0 as [TOTAL WIP STOCK],
            0 as [FG OPENING],

            CAST(ISNULL((SELECT SUM(fe_ok)
             FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2)) 
            +
               CAST(ISNULL((SELECT SUM(wor_Qty)
             FROM Work_Order_Receive_Details
            WHERE WOR_PID = IM_ID  and wor_status = 'A' and wor_MaterialType='SubPart'
            AND CONVERT(DATE, wor_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2))
                
            as [QC.OK],

                CAST(ISNULL((SELECT SUM(fe_rej)
            FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS DECIMAL(18,2))  as [QC.REJ],

            0 as [TOTAL FG STOCK],

            CAST(ISNULL((SELECT SUM(ASD_Qty) 
                FROM Assembly_data_Details  
                LEFT OUTER JOIN Assembly_details ON AS_iid = as_id
                WHERE  ASD_ITEMID =IM_ID and  CONVERT(DATE, AS_Date, 113)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
                +
            CAST(ISNULL((SELECT SUM(CS_Qty) 
                FROM CURRENT_STOCK     
                WHERE  CS_Item_ID =IM_ID and  CS_Type=21 AND   CONVERT(DATE, CS_CreatedDate, 113)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT)
             as [ASSY. OK QTY],  

            CAST(ISNULL((SELECT SUM(ASD_Rejqty) 
                FROM Assembly_data_Details
                LEFT OUTER JOIN Assembly_details ON AS_iid = as_id
                WHERE  ASD_ITEMID =IM_ID and  CONVERT(DATE, AS_Date, 113)BETWEEN '" + fromDate + @"' AND '" + toDate + @"'), 0) AS INT) as [ASSY. REJ],

            0 as [INVOICE],
            0 as [WIP CLOSING],
            0 as [FG CLOSING],
            0 as [CLOSING STOCK],

            CAST(ISNULL((SELECT SUM(PD_OK_Qty) 
                FROM Production_Details 
                LEFT JOIN Production_Request ON Pq_vPart_No = IM_PartName AND Pq_Status='A'    
                WHERE PD_Route_Card_ID = Pq_iid AND PD_Status='A'    
                  AND CONVERT(DATE, PD_Date, 113)< '" + fromDate + @"'), 0) AS INT) 
            +
			  CAST(ISNULL((SELECT SUM(CS_Qty)
             FROM CURRENT_STOCK
            WHERE CS_Item_ID = IM_ID  and CS_Status = 'A' AND CS_Type=6
            AND CONVERT(DATE, CS_CreatedDate, 112)< '" + fromDate + @"'), 0) AS INT) as [yes PRD OK QTY],


             CAST(ISNULL((SELECT SUM(fe_ok)
             FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112) < '" + fromDate + @"'), 0) AS DECIMAL(18,2)) +
               +
               CAST(ISNULL((SELECT SUM(wor_Qty)
             FROM Work_Order_Receive_Details
            WHERE WOR_PID = IM_ID  and wor_status = 'A' and wor_MaterialType='SubPart'
            AND CONVERT(DATE, wor_Date, 112)< '" + fromDate + @"' ), 0) AS DECIMAL(18,2))
            as [Yes QC.OK],

                  CAST(ISNULL((SELECT SUM(fe_rej)
            FROM[CHE_SRUTHY_PLASTIC].[final_entry]
            WHERE fe_partid = IM_ID  and fe_status = 'A'
            AND CONVERT(DATE, fe_Date, 112)< '" + fromDate + @"' ), 0) AS DECIMAL(18,2))  as [Yes QC.REJ],

                 CAST(ISNULL((SELECT SUM(ASD_Qty) 
                FROM Assembly_data_Details  
                LEFT OUTER JOIN Assembly_details ON AS_iid = as_id     
                WHERE  ASD_ITEMID =IM_ID and  CONVERT(DATE, AS_Date, 113)< '" + fromDate + @"' ), 0) AS INT)
                +
            CAST(ISNULL((SELECT SUM(CS_Qty) 
                FROM CURRENT_STOCK     
                WHERE  CS_Item_ID =IM_ID and  CS_Type=21 AND   CONVERT(DATE, CS_CreatedDate, 113) < '" + fromDate + @"' ), 0) AS INT)
             as [Yes ASSY. OK QTY],  
            CAST(ISNULL((SELECT SUM(ASD_Rejqty) 
                FROM Assembly_data_Details  
                LEFT OUTER JOIN Assembly_details ON AS_iid = as_id     
                WHERE  ASD_ITEMID =IM_ID and  CONVERT(DATE, AS_Date, 113)< '" + fromDate + @"' ), 0) AS INT) as [Yes ASSY. REJ],


            
                0 AS [Remarks]
                    FROM Item_Master                               
                    WHERE IM_Status = 'A' AND IM_Type=6 AND IM_ID NOT IN (
            SELECT ABM_AssyPartName
            FROM Assy_BOM_Master
            WHERE ABM_Status = 'A')                          
            ORDER BY IM_ID");
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["Part No"].ToString();
                DataRow[] match = dt1.Select("[SD_Part_No] = '" + partNo + "'");
                dr["invoice"] = match.Length > 0 ? match[0]["Qty"] : 0;
            }
            foreach (DataRow dr in dt.Rows)
            {

                decimal ypq = Convert.ToDecimal(dr["yes PRD OK QTY"]);
                decimal yqo = Convert.ToDecimal(dr["Yes QC.OK"]);
                decimal yqr = Convert.ToDecimal(dr["Yes QC.REJ"]);
                decimal tocl = ypq - yqo - yqr;

                dr["WIP OPENING STOCK"] = tocl;
            }
            foreach (DataRow dr in dt.Rows)
            {

                decimal wos = Convert.ToDecimal(dr["WIP OPENING STOCK"]);
                decimal poq = Convert.ToDecimal(dr["PRD OK QTY"]);
               
                decimal tocl = wos + poq;

                dr["TOTAL WIP STOCK"] = tocl;
            }

            foreach (DataRow dr in dt.Rows)
            {

                decimal tws = Convert.ToDecimal(dr["TOTAL WIP STOCK"]);
                decimal qo = Convert.ToDecimal(dr["QC.OK"]);
                decimal qr = Convert.ToDecimal(dr["QC.REJ"]);
                decimal tocl = tws - qo - qr;

                dr["WIP CLOSING"] = tocl;
            }
            foreach (DataRow dr in dt.Rows)
            {

                decimal tws = Convert.ToDecimal(dr["Yes QC.OK"]);
                decimal qo = Convert.ToDecimal(dr["Yes ASSY. OK QTY"]);
                decimal qr = Convert.ToDecimal(dr["Yes ASSY. REJ"]);

                string partNo = dr["Part No"].ToString();

                DataRow[] invoiceMatch = dt2.Select("SD_Part_No = '" + partNo + "'");
                decimal pin = invoiceMatch.Length > 0 ? Convert.ToDecimal(invoiceMatch[0]["Qty"]) : 0;

                decimal tocl = tws - qo - qr - pin;

                dr["FG OPENING"] = tocl;
            }
            foreach (DataRow dr in dt.Rows)
            {

                decimal tws = Convert.ToDecimal(dr["QC.OK"]);
                decimal qo = Convert.ToDecimal(dr["FG OPENING"]);
               
                decimal tocl = tws + qo;

                dr["TOTAL fg STOCK"] = tocl;
            }

            foreach (DataRow dr in dt.Rows)
            {

                decimal tws = Convert.ToDecimal(dr["TOTAL FG STOCK"]);
                decimal qo = Convert.ToDecimal(dr["ASSY. OK QTY"]);
                decimal qr = Convert.ToDecimal(dr["ASSY. REJ"]);
                decimal IN = Convert.ToDecimal(dr["INVOICE"]);
                decimal tocl = tws - qo - qr-IN;

                dr["FG CLOSING"] = tocl;
            }



            foreach (DataRow dr in dt.Rows)
            {

                //decimal tws = Convert.ToDecimal(dr["TOTAL WIP STOCK"]);
                //decimal qo = Convert.ToDecimal(dr["QC.OK"]);
                //decimal qr = Convert.ToDecimal(dr["QC.REJ"]);
                //decimal tocl = tws - qo - qr;




                //decimal tws1 = Convert.ToDecimal(dr["TOTAL FG STOCK"]);
                //decimal qo1 = Convert.ToDecimal(dr["ASSY. OK QTY"]);
                //decimal qr1 = Convert.ToDecimal(dr["ASSY. REJ"]);
                //decimal IN1 = Convert.ToDecimal(dr["INVOICE"]);
                //decimal tocl1 = tws1 - qo1 - qr1 - IN1;

                decimal tws = Convert.ToDecimal(dr["WIP CLOSING"]);
                decimal qo = Convert.ToDecimal(dr["FG CLOSING"]);
                dr["CLOSING STOCK"] = tws + qo;


                decimal tws1 = Convert.ToDecimal(dr["WIP OPENING STOCK"]);
                decimal qo1 = Convert.ToDecimal(dr["FG OPENING"]);
                dr["OPENING STOCK"] = tws1 + qo1;





            }

            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);

            dataGridView1.Columns["yes PRD OK QTY"].Visible = false;
            dataGridView1.Columns["Yes QC.OK"].Visible = false;
            dataGridView1.Columns["Yes QC.REJ"].Visible = false;
            dataGridView1.Columns["Yes ASSY. OK QTY"].Visible = false;
            dataGridView1.Columns["Yes ASSY. REJ"].Visible = false;


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

        private void Button2_Click(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("pr_get_sub_current_stock");//  '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }
    }
}
