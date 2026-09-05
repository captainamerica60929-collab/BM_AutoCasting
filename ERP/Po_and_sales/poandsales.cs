using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using System.Linq;

namespace LarchERP.Master
{
    

    public partial class poandsales : Form
    {
        public string arrow = "Up";
        public int Distance = 220;
        public string ID = "";
        string ErrorMessage = "";
        public bool isMachSupLoad = false;
        public poandsales()
        {
            InitializeComponent();
            dbFunctions.DGVStyle(dataGridView1);
        }
        private void ItemMaster_Shown(object sender, EventArgs e)
        {
            display();
            
        }
        private void ItemMaster_Load(object sender, EventArgs e)
        {
           
            display();
           
        }
        public void display()
        {
            DataTable dt1 = dbFunctions.getTable1("poverall_report '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "' ");
            DataTable dt = dbFunctions.getTable("poverall_po_report '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "' ");
            //dataGridView1.DataSource = dt;

            // Create a new DataTable for the merged result
            DataTable resultTable = new DataTable();
            resultTable.Columns.Add("Remarks", typeof(string));       // First column for remarks
            resultTable.Columns.Add("Supplier Name", typeof(string));
            resultTable.Columns.Add("Qty (dt1)", typeof(int));
            resultTable.Columns.Add("Qty (dt2)", typeof(int));
            resultTable.Columns.Add("Qty Difference", typeof(int));   // dt1 - dt2

            // Convert DataTables to Dictionary for easy lookup
            Dictionary<string, int> dt1Data = new Dictionary<string, int>();
            Dictionary<string, int> dtData = new Dictionary<string, int>();

            // Populate dt1 dictionary
            foreach (DataRow row in dt1.Rows)
            {
                string supplier = row["Supplier Name"].ToString();
                int qty = Convert.ToInt32(row["Qty"]);
                dt1Data[supplier] = qty;
            }

            // Populate dt dictionary
            foreach (DataRow row in dt.Rows)
            {
                string supplier = row["Supplier Name"].ToString();
                int qty = Convert.ToInt32(row["Qty"]);
                dtData[supplier] = qty;
            }

            // Get all unique supplier names from both tables
            HashSet<string> allSuppliers = new HashSet<string>(dt1Data.Keys);
            allSuppliers.UnionWith(dtData.Keys);

            // Merge data into resultTable
            foreach (string supplier in allSuppliers)
            {
                int qty1 = dt1Data.ContainsKey(supplier) ? dt1Data[supplier] : 0;
                int qty2 = dtData.ContainsKey(supplier) ? dtData[supplier] : 0;
                int qtyDifference = qty1 - qty2; // Calculate the difference

                // Define remarks based on the qty difference
                string remarks = qtyDifference > 0 ? "More in dt1" : qtyDifference < 0 ? "More in dt2" : "Equal";

                resultTable.Rows.Add(remarks, supplier, qty1, qty2, qtyDifference);
            }

            // Bind the resultTable to DataGridView
            dataGridView1.DataSource = resultTable;



            dbFunctions.DGVStyle(dataGridView1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            display();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // new1();
           // new3(); // ONLY SHOW SALES DETAILS

            //  NEW4(); SHOW SHOW

            NEW5();



            //DataTable dt1 = dbFunctions.getTable1("SELECT SD_Part_No,SD_Part_Name,SD_Qty FROM Sales_Invoice_Details where  CONVERT(Date, SD_Created_Date, 113) =  '20250325'");
            //DataTable dt2 = dbFunctions.getTable("SELECT IM_ID,IM_PartNo,IM_PartName FROM Item_master");
            //// DataTable matchedTable = dt1.Clone(); // Create a new DataTable with the same structure as dt1

            //DataTable matchedTable = new DataTable();
            //matchedTable.Columns.Add("IM_ID", typeof(string));
            //matchedTable.Columns.Add("IM_PartNo", typeof(string));
            //matchedTable.Columns.Add("SD_Part_No", typeof(string));
            //matchedTable.Columns.Add("IM_PartName", typeof(string));
            //matchedTable.Columns.Add("SD_Part_Name", typeof(string));
            //matchedTable.Columns.Add("SD_Qty", typeof(int));

            //foreach (DataRow row1 in dt1.Rows)
            //{
            //    string partNo1 = row1["SD_Part_No"].ToString();

            //    foreach (DataRow row2 in dt2.Rows)
            //    {
            //        if (row2["IM_PartNo"].ToString() == partNo1)
            //        {
            //            matchedTable.Rows.Add(
            //                row2["IM_ID"],
            //                row2["IM_PartNo"],
            //                row1["SD_Part_No"],
            //                row2["IM_PartName"],
            //                row1["SD_Part_Name"],
            //                row1["SD_Qty"]
            //            );
            //            break; // Stop checking after finding the first match
            //        }
            //    }
            //}


            //dataGridView1.DataSource = matchedTable;



            //dbFunctions.DGVStyle(dataGridView1);

        }

        private void NEW5()
        {
            // Fetch Sales Invoice and Item Master data
            DataTable dt1 = dbFunctions.getTable1("SELECT SD_Part_No, SD_Part_Name, SD_Qty FROM Sales_Invoice_Details WHERE CONVERT(Date, SD_Created_Date, 113) BETWEEN '" + dtpFrom.Text + "' AND '" + todate.Text + "' ");
            DataTable dt2 = dbFunctions.getTable("SELECT IM_ID, IM_PartNo, IM_PartName FROM Item_master");

            // Create a DataTable to store matched records from dt1 and dt2
            DataTable matchedTable = new DataTable();
            matchedTable.Columns.Add("IM_ID", typeof(string));
            matchedTable.Columns.Add("IM_PartNo", typeof(string));
            matchedTable.Columns.Add("SD_Part_No", typeof(string));
            matchedTable.Columns.Add("PartName", typeof(string));
            matchedTable.Columns.Add("SD_Part_Name", typeof(string));
            matchedTable.Columns.Add("sal rm", typeof(int));

            foreach (DataRow row1 in dt1.Rows)
            {
                string partNo1 = row1["SD_Part_No"].ToString();

                foreach (DataRow row2 in dt2.Rows)
                {
                    if (row2["IM_PartNo"].ToString() == partNo1)
                    {
                        matchedTable.Rows.Add(
                            row2["IM_ID"],
                            row2["IM_PartNo"],
                            row1["SD_Part_No"],
                            row2["IM_PartName"],
                            row1["SD_Part_Name"],
                            Convert.ToInt32(row1["SD_Qty"])
                        );
                        break;
                    }
                }
            }

            // Fetch BOM data
            DataTable bomTable = dbFunctions.getTable("SELECT BM_BomId, BM_ItemId, im_id, im_partname, im_partno, BM_MoldCavity, BM_Net_Part_Wt, BM_Runner_Wt FROM bom_master LEFT OUTER JOIN item_master ON im_id = BM_ItemId");

            // Create a final table with additional calculated fields
            DataTable finalTable = new DataTable();
            finalTable.Columns.Add("BM_BomId", typeof(string));
            finalTable.Columns.Add("BM_ItemId", typeof(string));
            finalTable.Columns.Add("IM_ID", typeof(string));
            finalTable.Columns.Add("PartName", typeof(string));
            finalTable.Columns.Add("PartNo", typeof(string));
            finalTable.Columns.Add("BM_MoldCavity", typeof(string));
            finalTable.Columns.Add("BM_Net_Part_Wt", typeof(decimal));
            finalTable.Columns.Add("BM_Runner_Wt", typeof(decimal));
            finalTable.Columns.Add("sal rm", typeof(int));
            finalTable.Columns.Add("PW", typeof(decimal));
            finalTable.Columns.Add("RW", typeof(decimal));
            finalTable.Columns.Add("SAL RM", typeof(decimal));

            foreach (DataRow row in matchedTable.Rows)
            {
                string imId = row["IM_ID"].ToString();
                int sdQty = Convert.ToInt32(row["sal rm"]);

                foreach (DataRow bomRow in bomTable.Rows)
                {
                    if (bomRow["BM_BomId"].ToString() == imId)
                    {
                        decimal netPartWt = Convert.ToDecimal(bomRow["BM_Net_Part_Wt"]);
                        decimal runnerWt = Convert.ToDecimal(bomRow["BM_Runner_Wt"]);
                        decimal cavity = Convert.ToDecimal(bomRow["BM_MoldCavity"]);

                        decimal pw = (sdQty * netPartWt) / 1000;
                        decimal rw = (cavity != 0) ? (sdQty * runnerWt / cavity) / 1000 : 0;
                        decimal totalWeight = pw + rw;

                        finalTable.Rows.Add(
                            bomRow["BM_BomId"],
                            bomRow["BM_ItemId"],
                            bomRow["im_id"],
                            bomRow["im_partname"],
                            bomRow["im_partno"],
                            bomRow["BM_MoldCavity"],
                            netPartWt,
                            runnerWt,
                            sdQty,
                            pw,
                            rw,
                            totalWeight
                        );
                        break;
                    }
                }
            }

            dataGridView1.DataSource = finalTable;
            dbFunctions.DGVStyle(dataGridView1);

            // Fetch PO Data
            DataTable poTable = dbFunctions.getTable(@" SELECT POD_vItem_ID, POD_vPart_Number, POD_vSpec, IM_PartNo, IM_PartName, POD_dQty AS [PO RM]   FROM Purchase_Order_details 
                LEFT OUTER JOIN item_master ON im_id = POD_vItem_ID WHERE CONVERT(date, POD_dCreatedDate, 113) BETWEEN '" + dtpFrom.Text + "' AND '" + todate.Text+"' AND POD_vStatus = 'A'");

            // Merge finalTable and poTable
            DataTable mergedTable = finalTable.Clone();
            mergedTable.Columns.Add("POD_vItem_ID", typeof(string));
            mergedTable.Columns.Add("POD_vPart_Number", typeof(string));
            mergedTable.Columns.Add("POD_vSpec", typeof(string));
            mergedTable.Columns.Add("PO RM", typeof(int));

            foreach (DataRow finalRow in finalTable.Rows)
            {
                string itemId = finalRow["BM_ItemId"].ToString();

                foreach (DataRow poRow in poTable.Rows)
                {
                    if (poRow["POD_vItem_ID"].ToString() == itemId)
                    {
                        mergedTable.Rows.Add(
                            finalRow["BM_BomId"],
                            finalRow["BM_ItemId"],
                            finalRow["IM_ID"],
                            finalRow["PartName"],
                            finalRow["PartNo"],
                            finalRow["BM_MoldCavity"],
                            finalRow["BM_Net_Part_Wt"],
                            finalRow["BM_Runner_Wt"],
                            finalRow["sal rm"],
                            finalRow["PW"],
                            finalRow["RW"],
                            finalRow["SAL RM"],
                            poRow["POD_vItem_ID"],
                            poRow["POD_vPart_Number"],
                            poRow["POD_vSpec"],
                            Convert.ToInt32(poRow["PO RM"])
                        );
                        break;
                    }
                }
            }

            dataGridView1.DataSource = mergedTable;
            dbFunctions.DGVStyle(dataGridView1);


            //// Fetch Sales Invoice and Item Master data
            //DataTable dt1 = dbFunctions.getTable1("SELECT SD_Part_No, SD_Part_Name, SD_Qty FROM Sales_Invoice_Details WHERE CONVERT(Date, SD_Created_Date, 113) = '20250325'");
            //DataTable dt2 = dbFunctions.getTable("SELECT IM_ID, IM_PartNo, IM_PartName FROM Item_master");

            //// Create a DataTable to store matched records
            //DataTable matchedTable = new DataTable();
            //matchedTable.Columns.Add("IM_ID", typeof(string));
            //matchedTable.Columns.Add("IM_PartNo", typeof(string));
            //matchedTable.Columns.Add("SD_Part_No", typeof(string));
            //matchedTable.Columns.Add("IM_PartName", typeof(string));
            //matchedTable.Columns.Add("SD_Part_Name", typeof(string));
            //matchedTable.Columns.Add("SD_Qty", typeof(int));

            //foreach (DataRow row1 in dt1.Rows)
            //{
            //    string partNo1 = row1["SD_Part_No"].ToString();

            //    foreach (DataRow row2 in dt2.Rows)
            //    {
            //        if (row2["IM_PartNo"].ToString() == partNo1)
            //        {
            //            matchedTable.Rows.Add(
            //                row2["IM_ID"],
            //                row2["IM_PartNo"],
            //                row1["SD_Part_No"],
            //                row2["IM_PartName"],
            //                row1["SD_Part_Name"],
            //                Convert.ToInt32(row1["SD_Qty"])
            //            );
            //            break; // Stop after finding the first match
            //        }
            //    }
            //}

            //// Fetch BOM data
            //DataTable bomTable = dbFunctions.getTable(@"
            //SELECT BM_BomId, BM_ItemId, im_id, im_partname, im_partno, BM_MoldCavity, BM_Net_Part_Wt, BM_Runner_Wt 
            //FROM bom_master 
            //LEFT OUTER JOIN item_master ON im_id = BM_ItemId");

            //// Create finalTable with calculations
            //DataTable finalTable = new DataTable();
            //finalTable.Columns.Add("BM_BomId", typeof(string));
            //finalTable.Columns.Add("BM_ItemId", typeof(string));
            //finalTable.Columns.Add("IM_ID", typeof(string));
            //finalTable.Columns.Add("IM_PartName", typeof(string));
            //finalTable.Columns.Add("IM_PartNo", typeof(string));
            //finalTable.Columns.Add("BM_MoldCavity", typeof(string));
            //finalTable.Columns.Add("BM_Net_Part_Wt", typeof(decimal));
            //finalTable.Columns.Add("BM_Runner_Wt", typeof(decimal));
            //finalTable.Columns.Add("SD_Qty", typeof(int));
            //finalTable.Columns.Add("PW", typeof(decimal));  // Part Weight
            //finalTable.Columns.Add("RW", typeof(decimal));  // Runner Weight
            //finalTable.Columns.Add("Total_Weight", typeof(decimal));  // PW + RW

            //foreach (DataRow row in matchedTable.Rows)
            //{
            //    string imId = row["IM_ID"].ToString();
            //    int sdQty = Convert.ToInt32(row["SD_Qty"]);

            //    foreach (DataRow bomRow in bomTable.Rows)
            //    {
            //        if (bomRow["BM_BomId"].ToString() == imId)  // Match IM_ID with BM_ItemId
            //        {
            //            decimal netPartWt = Convert.ToDecimal(bomRow["BM_Net_Part_Wt"]);
            //            decimal runnerWt = Convert.ToDecimal(bomRow["BM_Runner_Wt"]);
            //            decimal cavity = Convert.ToDecimal(bomRow["BM_MoldCavity"]);

            //            // PW Calculation
            //            decimal pw = (sdQty * netPartWt) / 1000;

            //            // RW Calculation
            //            decimal rw = (cavity != 0) ? (sdQty * runnerWt / cavity) / 1000 : 0;

            //            // Total Weight
            //            decimal totalWeight = pw + rw;

            //            finalTable.Rows.Add(
            //                bomRow["BM_BomId"],
            //                bomRow["BM_ItemId"],
            //                bomRow["im_id"],
            //                bomRow["im_partname"],
            //                bomRow["im_partno"],
            //                bomRow["BM_MoldCavity"],
            //                netPartWt,
            //                runnerWt,
            //                sdQty,
            //                pw,
            //                rw,
            //                totalWeight
            //            );
            //            break; // Stop after finding the first match
            //        }
            //    }
            //}

            //// Fetch Purchase Order Data
            //DataTable poTable = dbFunctions.getTable(@"
            //SELECT POD_vItem_ID, POD_vPart_Number, POD_vSpec, IM_PartNo, IM_PartName, POD_dQty 
            //FROM Purchase_Order_details
            //LEFT OUTER JOIN item_master ON im_id = POD_vItem_ID
            //WHERE CONVERT(date, POD_dCreatedDate, 113) BETWEEN '20250301' AND '20250330'
            //AND POD_vStatus = 'A'");

            //// Create a new DataTable to store merged records
            //DataTable mergedTable = new DataTable();
            //mergedTable.Columns.Add("BM_BomId", typeof(string));
            //mergedTable.Columns.Add("BM_ItemId", typeof(string));
            //mergedTable.Columns.Add("IM_ID", typeof(string));
            //mergedTable.Columns.Add("IM_PartName", typeof(string));
            //mergedTable.Columns.Add("IM_PartNo", typeof(string));
            //mergedTable.Columns.Add("BM_MoldCavity", typeof(string));
            //mergedTable.Columns.Add("BM_Net_Part_Wt", typeof(decimal));
            //mergedTable.Columns.Add("BM_Runner_Wt", typeof(decimal));
            //mergedTable.Columns.Add("SD_Qty", typeof(int));
            //mergedTable.Columns.Add("PW", typeof(decimal));  // Part Weight
            //mergedTable.Columns.Add("RW", typeof(decimal));  // Runner Weight
            //mergedTable.Columns.Add("Total_Weight", typeof(decimal));
            //mergedTable.Columns.Add("POD_vItem_ID", typeof(string));
            //mergedTable.Columns.Add("POD_vPart_Number", typeof(string));
            //mergedTable.Columns.Add("POD_vSpec", typeof(string));
            //mergedTable.Columns.Add("POD_dQty", typeof(int));

            //foreach (DataRow finalRow in finalTable.Rows)
            //{
            //    string bmItemId = finalRow["BM_ItemId"].ToString();

            //    foreach (DataRow poRow in poTable.Rows)
            //    {
            //        if (poRow["POD_vItem_ID"].ToString() == bmItemId)
            //        {
            //            mergedTable.Rows.Add(
            //                finalRow["BM_BomId"],
            //                finalRow["BM_ItemId"],
            //                finalRow["IM_ID"],
            //                finalRow["IM_PartName"],
            //                finalRow["IM_PartNo"],
            //                finalRow["BM_MoldCavity"],
            //                finalRow["BM_Net_Part_Wt"],
            //                finalRow["BM_Runner_Wt"],
            //                finalRow["SD_Qty"],
            //                finalRow["PW"],
            //                finalRow["RW"],
            //                finalRow["Total_Weight"],
            //                poRow["POD_vItem_ID"],
            //                poRow["POD_vPart_Number"],
            //                poRow["POD_vSpec"],
            //                Convert.ToInt32(poRow["POD_dQty"])
            //            );
            //            break; // Stop after finding the first match
            //        }
            //    }
            //}

            //// Bind mergedTable to DataGridView
            //dataGridView1.DataSource = mergedTable;
            //dbFunctions.DGVStyle(dataGridView1);
        }

        private void NEW4()
        {
            // Fetch Sales Invoice and Item Master data
            DataTable dt1 = dbFunctions.getTable1("SELECT SD_Part_No, SD_Part_Name, SD_Qty FROM Sales_Invoice_Details WHERE CONVERT(Date, SD_Created_Date, 113) = '20250325'");
            DataTable dt2 = dbFunctions.getTable("SELECT IM_ID, IM_PartNo, IM_PartName FROM Item_master");

            // Create a DataTable to store matched records from dt1 and dt2
            DataTable matchedTable = new DataTable();
            matchedTable.Columns.Add("IM_ID", typeof(string));
            matchedTable.Columns.Add("IM_PartNo", typeof(string));
            matchedTable.Columns.Add("SD_Part_No", typeof(string));
            matchedTable.Columns.Add("IM_PartName", typeof(string));
            matchedTable.Columns.Add("SD_Part_Name", typeof(string));
            matchedTable.Columns.Add("SD_Qty", typeof(int));

            foreach (DataRow row1 in dt1.Rows)
            {
                string partNo1 = row1["SD_Part_No"].ToString();

                foreach (DataRow row2 in dt2.Rows)
                {
                    if (row2["IM_PartNo"].ToString() == partNo1)
                    {
                        matchedTable.Rows.Add(
                            row2["IM_ID"],
                            row2["IM_PartNo"],
                            row1["SD_Part_No"],
                            row2["IM_PartName"],
                            row1["SD_Part_Name"],
                            Convert.ToInt32(row1["SD_Qty"])
                        );
                        break; // Stop after finding the first match
                    }
                }
            }

            // Fetch BOM data
            DataTable bomTable = dbFunctions.getTable("SELECT BM_BomId, BM_ItemId, im_id, im_partname, im_partno, BM_MoldCavity, BM_Net_Part_Wt, BM_Runner_Wt FROM bom_master LEFT OUTER JOIN item_master ON im_id = BM_ItemId");

            // Fetch PO data
            DataTable poTable = dbFunctions.getTable(@"
                SELECT POD_vItem_ID, POD_vPart_Number, POD_vSpec, IM_PartNo, IM_PartName, POD_dQty 
                FROM Purchase_Order_details
                LEFT OUTER JOIN item_master ON im_id = POD_vItem_ID
                WHERE CONVERT(date, POD_dCreatedDate, 113) BETWEEN '20250301' AND '20250330'
                AND POD_vStatus = 'A'");

            // Create a final table with additional calculated fields
            DataTable finalTable = new DataTable();
            finalTable.Columns.Add("BM_BomId", typeof(string));
            finalTable.Columns.Add("BM_ItemId", typeof(string));
            finalTable.Columns.Add("IM_ID", typeof(string));
            finalTable.Columns.Add("IM_PartName", typeof(string));
            finalTable.Columns.Add("IM_PartNo", typeof(string));
            finalTable.Columns.Add("BM_MoldCavity", typeof(string));
            finalTable.Columns.Add("BM_Net_Part_Wt", typeof(decimal));
            finalTable.Columns.Add("BM_Runner_Wt", typeof(decimal));
            finalTable.Columns.Add("SD_Qty", typeof(int));
            finalTable.Columns.Add("PW", typeof(decimal));  // Part Weight
            finalTable.Columns.Add("RW", typeof(decimal));  // Runner Weight
            finalTable.Columns.Add("Total_Weight", typeof(decimal));  // PW + RW

            // Extra Columns from PO Data
            finalTable.Columns.Add("POD_vItem_ID", typeof(string));
            finalTable.Columns.Add("POD_vPart_Number", typeof(string));
            finalTable.Columns.Add("POD_vSpec", typeof(string));
            finalTable.Columns.Add("POD_dQty", typeof(int));

            foreach (DataRow row in matchedTable.Rows)
            {
                string imId = row["BM_ItemId"].ToString();
                int sdQty = Convert.ToInt32(row["SD_Qty"]);

                //DataRow matchingPoRow = poTable.AsEnumerable()
                //    .FirstOrDefault(poRow => poRow["POD_vItem_ID"].ToString() == imId);

                DataRow matchingPoRow = poTable.AsEnumerable()
             .FirstOrDefault(poRow => poRow["POD_vItem_ID"].ToString() == imId);



                string podItemId = matchingPoRow?["POD_vItem_ID"]?.ToString() ?? "";
                string podPartNumber = matchingPoRow?["POD_vPart_Number"]?.ToString() ?? "";
                string podSpec = matchingPoRow?["POD_vSpec"]?.ToString() ?? "";
                int podQty = matchingPoRow != null ? Convert.ToInt32(matchingPoRow["POD_dQty"]) : 0;

                foreach (DataRow bomRow in bomTable.Rows)
                {
                    if (bomRow["BM_BomId"].ToString() == imId)  // Compare IM_ID from matchedTable with BM_ItemId
                    {
                        decimal netPartWt = Convert.ToDecimal(bomRow["BM_Net_Part_Wt"]);
                        decimal runnerWt = Convert.ToDecimal(bomRow["BM_Runner_Wt"]);
                        decimal cavity = Convert.ToDecimal(bomRow["BM_MoldCavity"]);

                        // PW Calculation
                        decimal pw = (sdQty * netPartWt) / 1000;

                        // RW Calculation
                        decimal rw = (cavity != 0) ? (sdQty * runnerWt / cavity) / 1000 : 0;

                        // Total Weight
                        decimal totalWeight = pw + rw;

                        finalTable.Rows.Add(
                            bomRow["BM_BomId"],
                            bomRow["BM_ItemId"],
                            bomRow["im_id"],
                            bomRow["im_partname"],
                            bomRow["im_partno"],
                            bomRow["BM_MoldCavity"],
                            netPartWt,
                            runnerWt,
                            sdQty,
                            pw,
                            rw,
                            totalWeight,
                            podItemId,
                            podPartNumber,
                            podSpec,
                            podQty
                        );
                        break; // Stop after finding the first match
                    }
                }
            }

            dataGridView1.DataSource = finalTable;
            dbFunctions.DGVStyle(dataGridView1);

        }

        private void new3()
        {
            // Fetch Sales Invoice and Item Master data
            DataTable dt1 = dbFunctions.getTable1("SELECT SD_Part_No, SD_Part_Name, SD_Qty FROM Sales_Invoice_Details WHERE CONVERT(Date, SD_Created_Date, 113) = '20250325'");
            DataTable dt2 = dbFunctions.getTable("SELECT IM_ID, IM_PartNo, IM_PartName FROM Item_master");

            // Create a DataTable to store matched records from dt1 and dt2
            DataTable matchedTable = new DataTable();
            matchedTable.Columns.Add("IM_ID", typeof(string));
            matchedTable.Columns.Add("IM_PartNo", typeof(string));
            matchedTable.Columns.Add("SD_Part_No", typeof(string));
            matchedTable.Columns.Add("IM_PartName", typeof(string));
            matchedTable.Columns.Add("SD_Part_Name", typeof(string));
            matchedTable.Columns.Add("SD_Qty", typeof(int));

            foreach (DataRow row1 in dt1.Rows)
            {
                string partNo1 = row1["SD_Part_No"].ToString();

                foreach (DataRow row2 in dt2.Rows)
                {
                    if (row2["IM_PartNo"].ToString() == partNo1)
                    {
                        matchedTable.Rows.Add(
                            row2["IM_ID"],
                            row2["IM_PartNo"],
                            row1["SD_Part_No"],
                            row2["IM_PartName"],
                            row1["SD_Part_Name"],
                            Convert.ToInt32(row1["SD_Qty"]) // Ensure SD_Qty is an integer
                        );
                        break; // Stop after finding the first match
                    }
                }
            }

            // Fetch BOM data
            DataTable bomTable = dbFunctions.getTable("SELECT BM_BomId, BM_ItemId, im_id, im_partname, im_partno, BM_MoldCavity, BM_Net_Part_Wt, BM_Runner_Wt FROM bom_master LEFT OUTER JOIN item_master ON im_id = BM_ItemId");

            // Create a final table with additional calculated fields
            DataTable finalTable = new DataTable();
            finalTable.Columns.Add("BM_BomId", typeof(string));
            finalTable.Columns.Add("BM_ItemId", typeof(string));
            finalTable.Columns.Add("IM_ID", typeof(string));
            finalTable.Columns.Add("IM_PartName", typeof(string));
            finalTable.Columns.Add("IM_PartNo", typeof(string));
            finalTable.Columns.Add("BM_MoldCavity", typeof(string));
            finalTable.Columns.Add("BM_Net_Part_Wt", typeof(decimal));
            finalTable.Columns.Add("BM_Runner_Wt", typeof(decimal));
            finalTable.Columns.Add("SD_Qty", typeof(int));
            finalTable.Columns.Add("PW", typeof(decimal));  // Part Weight
            finalTable.Columns.Add("RW", typeof(decimal));  // Runner Weight
            finalTable.Columns.Add("Total_Weight", typeof(decimal));  // PW + RW

            foreach (DataRow row in matchedTable.Rows)
            {
                string imId = row["IM_ID"].ToString();
                int sdQty = Convert.ToInt32(row["SD_Qty"]);

                foreach (DataRow bomRow in bomTable.Rows)
                {
                    if (bomRow["BM_BomId"].ToString() == imId)  // Compare IM_ID from matchedTable with BM_ItemId
                    {
                        decimal netPartWt = Convert.ToDecimal(bomRow["BM_Net_Part_Wt"]);
                        decimal runnerWt = Convert.ToDecimal(bomRow["BM_Runner_Wt"]);
                        decimal cavity = Convert.ToDecimal(bomRow["BM_MoldCavity"]);

                        // PW Calculation
                        decimal pw = (sdQty * netPartWt) / 1000;

                        // RW Calculation
                        //decimal rw = (sdQty * runnerWt / cavity) / 1000; // Verify this logic, it seems incorrect
                        decimal rw = (cavity != 0) ? (sdQty * runnerWt / cavity) / 1000 : 0;

                        // Total Weight
                        decimal totalWeight = pw + rw;

                        finalTable.Rows.Add(
                            bomRow["BM_BomId"],
                            bomRow["BM_ItemId"],
                            bomRow["im_id"],
                            bomRow["im_partname"],
                            bomRow["im_partno"],
                            bomRow["BM_MoldCavity"],
                            netPartWt,
                            runnerWt,
                            sdQty,
                            pw,
                            rw,
                            totalWeight
                        );
                        break; // Stop after finding the first match
                    }
                }
            }
            dataGridView1.DataSource = finalTable;
            dbFunctions.DGVStyle(dataGridView1);

            // finalTable now contains calculated values for PW, RW, and Total_Weight

        }

        private void new1()
        {
            // Fetch Sales Invoice and Item Master data
            DataTable dt1 = dbFunctions.getTable1("SELECT SD_Part_No, SD_Part_Name, SD_Qty FROM Sales_Invoice_Details WHERE CONVERT(Date, SD_Created_Date, 113) = '20250325'");
            DataTable dt2 = dbFunctions.getTable("SELECT IM_ID, IM_PartNo, IM_PartName FROM Item_master");

            // Create a DataTable to store matched records from dt1 and dt2
            DataTable matchedTable = new DataTable();
            matchedTable.Columns.Add("IM_ID", typeof(string));
            matchedTable.Columns.Add("IM_PartNo", typeof(string));
            matchedTable.Columns.Add("SD_Part_No", typeof(string));
            matchedTable.Columns.Add("IM_PartName", typeof(string));
            matchedTable.Columns.Add("SD_Part_Name", typeof(string));
            matchedTable.Columns.Add("SD_Qty", typeof(int));

            // Compare dt1 and dt2 based on Part No
            foreach (DataRow row1 in dt1.Rows)
            {
                string partNo1 = row1["SD_Part_No"].ToString();

                foreach (DataRow row2 in dt2.Rows)
                {
                    if (row2["IM_PartNo"].ToString() == partNo1)
                    {
                        matchedTable.Rows.Add(
                            row2["IM_ID"],
                            row2["IM_PartNo"],
                            row1["SD_Part_No"],
                            row2["IM_PartName"],
                            row1["SD_Part_Name"],
                            row1["SD_Qty"]
                        );
                        break; // Stop after finding the first match
                    }
                }
            }

            // Fetch BOM data
            DataTable bomTable = dbFunctions.getTable("SELECT BM_BomId, BM_ItemId, im_id, im_partname, im_partno, BM_MoldCavity, BM_Net_Part_Wt, BM_Runner_Wt FROM bom_master LEFT OUTER JOIN item_master ON im_id = BM_ItemId");

            // Create a final table to store only matching IM_IDs from matchedTable and BOM
            DataTable finalTable = new DataTable();
            finalTable.Columns.Add("BM_BomId", typeof(string));
            finalTable.Columns.Add("BM_ItemId", typeof(string));
            finalTable.Columns.Add("IM_ID", typeof(string));
            finalTable.Columns.Add("IM_PartName", typeof(string));
            finalTable.Columns.Add("IM_PartNo", typeof(string));
            finalTable.Columns.Add("BM_MoldCavity", typeof(string));
            finalTable.Columns.Add("BM_Net_Part_Wt", typeof(string));
            finalTable.Columns.Add("BM_Runner_Wt", typeof(string));
            finalTable.Columns.Add("SD_Qty", typeof(int));
            finalTable.Columns.Add("SD_Part_Name", typeof(String));

            foreach (DataRow row in matchedTable.Rows)
            {
                string imId = row["IM_ID"].ToString();

                foreach (DataRow bomRow in bomTable.Rows)
                {
                    if (bomRow["BM_BomId"].ToString() == imId)  // Compare IM_ID from matchedTable with BM_ItemId
                    {
                        finalTable.Rows.Add(
                            bomRow["BM_BomId"],
                            bomRow["BM_ItemId"],
                            bomRow["im_id"],
                            bomRow["im_partname"],
                            bomRow["im_partno"],
                            bomRow["BM_MoldCavity"],
                            bomRow["BM_Net_Part_Wt"],
                            bomRow["BM_Runner_Wt"],
                            row["SD_Qty"],
                            row["SD_Part_Name"]
                        );
                        break; // Stop after finding the first match
                    }
                }
            }
            dataGridView1.DataSource = finalTable;
            dbFunctions.DGVStyle(dataGridView1);

            // Now, finalTable contains only the rows where IM_ID matches BM_ItemId

        }
    }
}