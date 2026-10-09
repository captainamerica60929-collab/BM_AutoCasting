using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using System.IO;

namespace CRM_App.Transaction
{
    public partial class Grin_Against_Purchase_Order : Form
    {

        public string SupplierID = "";
        public string ID = "";
        string GRND_iGRN_No;
        string ErrorMessage = "";
        public bool isSupplierLoad = false;
        public bool isLoadItem = false;

        public Grin_Against_Purchase_Order()
        {
            InitializeComponent();
        }

        public void LoadSupplierNo()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Fetch_Amend_Purchase_Order_No");
                PO_vPO_NO.DataSource = dt;
                PO_vPO_NO.DisplayMember = "PO_vPO_NO";
                PO_vPO_NO.ValueMember = "PO_iID";
                PO_vPO_NO.SelectedIndex = -1;
                isSupplierLoad = true;
            }
            catch
            {
            }
        }

        //private void LoadAmend_No()
        //{
        //    try
        //    {
        //        DataTable dt = dbFunctions.getTable("pr_GetAmend_Number");
        //        PO_iAmendment_No.Text = dt.Rows[0][0].ToString();
        //    }
        //    catch
        //    {
        //    }
        //}

      

        private void Display()
        {

            try
            {
                DataTable dtDetails = dbFunctions.getTable("Pr_Fetch_POD_And_GRN_Details '" + PO_vPO_NO.SelectedValue.ToString() + "'");
                dataGridView1.DataSource = dtDetails;
                dbFunctions.DGVStyle(dataGridView1);
                dataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                //dataGridView1.Columns["Spec"].ReadOnly = true;
                dataGridView1.Columns["Part Name"].ReadOnly = true;
                dataGridView1.Columns["Item ID"].Visible = false;
                dataGridView1.Columns["GRN QTY"].Visible = false;
                dataGridView1.Columns["Part No"].ReadOnly = true;
                dataGridView1.Columns["Source"].Width = 150;
                dataGridView1.Columns["Source"].ReadOnly = true;
                dataGridView1.Columns["Unit Price"].ReadOnly = true;
                dataGridView1.Columns["Qty"].ReadOnly = true;
                dataGridView1.Columns["Total Amount"].ReadOnly = true;
                dataGridView1.Columns["Total Qty"].ReadOnly = true;
                dataGridView1.Columns["Type"].Visible = true;
                //if()
               // dataGridView1.Columns["MFG Date"].Visible = false;
                
                calulate();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(""+ex);
            }

        }

        public void calulate()
        {
            try
            {
                decimal Gross_tot = 0m;
                try
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells["Total Amount"].Value.ToString()!="")
                        {
                            Gross_tot += decimal.Parse(dataGridView1.Rows[i].Cells["Total Amount"].Value.ToString());
                        }

                    }
                }
                catch { }
                GRN_dSubTotal.Text = Gross_tot.ToString("0.00");

                GRN_dGST_Amount1.Text = (((Decimal.Parse(GRN_dSubTotal.Text) * (Decimal.Parse((GRN_dGST_Percentage1.Text)) / 100)))).ToString("0.00");
                GRN_dGST_Amount2.Text = (((Decimal.Parse(GRN_dSubTotal.Text) * (Decimal.Parse((GRN_dGST_Percentage2.Text)) / 100)))).ToString("0.00");

                GRN_dGross_Total.Text = (Decimal.Parse(GRN_dSubTotal.Text) + ((Decimal.Parse(GRN_dFreight.Text) + ((Decimal.Parse(GRN_dOthers.Text) + ((Decimal.Parse(GRN_dGST_Amount1.Text) + Decimal.Parse(GRN_dGST_Amount2.Text)))))))).ToString("0");

            }
            catch (Exception ex)
            {
            }
        }

        private void PO_vPO_NO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (isSupplierLoad)
                {
                    DataTable dt = dbFunctions.getTable("pr_Get_Purchase_Order_Approval_Details '" + PO_vPO_NO.SelectedValue.ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        PO_vSupplier_Name.Text = dt.Rows[0]["PO_vSupplier_Name"].ToString();
                        PO_dPO_Date.Text = dt.Rows[0]["PO_dPO_Date"].ToString();
                        SupplierID = dt.Rows[0]["PO_iSupplierID"].ToString();
                        PO_vSupplier_Address.Text = dt.Rows[0]["PO_vSupplier_Address"].ToString();
                        GRN_dGST_Percentage1.Text = dt.Rows[0]["PO_dExcise_Duty_Percent"].ToString();
                        GRN_dGST_Percentage2.Text = dt.Rows[0]["PO_dVAT_CST_Percentage"].ToString();
                    }
                }
                Display();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);

            }
        }

        private void Grin_Against_Purchase_Order_Load(object sender, EventArgs e)
        {
            LoadSupplierNo();
            GRN_No();
            LoadType();
            GRN_dGRN_Date.MinDate = System.DateTime.Now;

            GRN_iType.Text = "RM";

        }

        private void GRN_No()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_GRN_Number");
                GRN_vGRN_No.Text = dt.Rows[0][0].ToString();
            }
            catch
            {
            }
        }

        public void LoadType()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Type");
                GRN_iType.DataSource = dt;
                GRN_iType.DisplayMember = "Ty_TypeName";
                GRN_iType.ValueMember = "Ty_ID";
                GRN_iType.SelectedIndex = -1;
                isSupplierLoad = true;
            }
            catch
            {
            }
        }


        public bool Validate()
        {
            if ((string.IsNullOrEmpty(PO_vPO_NO.Text.Trim())))
            {
                ErrorMessage = "PO Name Should Not be Empty";
                PO_vPO_NO.Focus();
                return true;
            }
         
            if ((string.IsNullOrEmpty(GRN_iType.Text.Trim())))
            {
                ErrorMessage = "Type Should Not be Empty";
                GRN_iType.Focus();
                return true;
            }

            if ((string.IsNullOrEmpty(GRN_vInvoice_No.Text.Trim())))
            {
                ErrorMessage = "Invoice_No Should Not be Empty";
                GRN_vInvoice_No.Focus();
                return true;
            }

            if ((string.IsNullOrEmpty(GRN_dFreight.Text.Trim())))
            {
                ErrorMessage = "Freight Should Not be Empty";
                GRN_dFreight.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(GRN_dOthers.Text.Trim())))
            {
                ErrorMessage = "Others Should Not be Empty";
                GRN_dOthers.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(GRN_dGST_Percentage1.Text.Trim())))
            {
                ErrorMessage = "GST Percentage1 Should Not be Empty";
                GRN_dGST_Percentage1.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(GRN_dGST_Percentage2.Text.Trim())))
            {
                ErrorMessage = "GST Percentage2 Should Not be Empty";
                GRN_dGST_Percentage2.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(GRN_vGST_Type1.Text.Trim())))
            {
                ErrorMessage = "GST Type 1 Should Not be Empty";
                GRN_vGST_Type1.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(GRN_vGST_Type2.Text.Trim())))
            {
                ErrorMessage = "GST Type 2 Should Not be Empty";
                GRN_vGST_Type2.Focus();
                return true;
            }
          


            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            //    try
            //    {
            //        DateTime dt = DateTime.Parse(dataGridView1.Rows[i].Cells["MFG Date"].Value.ToString());

            //    }
            //    catch
            //    {

            //        ErrorMessage = "Invalid MFG Date. Ex : dd-MMM-yyyy";

            //        return true;
            //    }
            //}

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    // dataGridView1.Rows[i].Cells["Lot No"].Value.ToString();
                    //var lotValue = dataGridView1.Rows[i].Cells["Lot No"].Value;

                    //if (lotValue == null || string.IsNullOrWhiteSpace(lotValue.ToString()))
                    //{
                    //    ErrorMessage = "Please enter the Lot No in row " + (i + 1);
                    //    return true;
                    //}
                    var lotValue = dataGridView1.Rows[i].Cells["Lot No"].Value;

                    if (string.IsNullOrWhiteSpace(Convert.ToString(lotValue)))
                    {
                        ErrorMessage = "Please enter the Lot No in row ";
                        return true;   // Validation failed
                    }
                    var lomfg = dataGridView1.Rows[i].Cells["MFG Date"].Value;

                    if (lomfg == null || string.IsNullOrWhiteSpace(lomfg.ToString()))
                    {
                        ErrorMessage = "Please enter the MFG DATE in row " + (i + 1);
                        return true;
                    }

                }
                catch
                {

                    ErrorMessage = "Please enter the Lot No";

                    return true;
                }
            }
            return false;
        }

        public void button1_Click(object sender, EventArgs e)
        {

            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //if (Podc_Description.Text.Equals(""))
                //{
                //    MessageBox.Show("Document Name Should Not be Empty", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                DialogResult result = MessageBox.Show("Everything is correct?", "Confirmation",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    save_summary();
                }
                else
                {
                    return;
                }
            }
        }

        public void Clear()
        {
            GRN_vGRN_No.Text= "";
            GRN_dGRN_Date.Text= "";
            PO_vPO_NO.Text = "";
            GRN_vInvoice_No.Text = "";
            GRN_dInvoice_Date.Text = "";
           // GRN_vStoreLocation.Text = "";
            GRN_iType.Text = "";
            GRN_dSubTotal.Text = "";
            GRN_vGST_Type1.Text = "";
            GRN_dGST_Percentage1.Text = "";
            GRN_dGST_Amount1.Text = "";
            GRN_vGST_Type2.Text = "";
            GRN_dGST_Percentage2.Text = "";
            GRN_dGST_Amount2.Text = "";
            GRN_dFreight.Text= "";
            GRN_dOthers.Text= "";
            GRN_dGross_Total.Text= "";
            dataGridView1.Rows.Clear();
        }


        private void save_summary()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Insert_Grin_Against_Purchase_Order";

                //com.Parameters.Add("@GRN_iID", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@GRN_vGRN_No", SqlDbType.VarChar).Value = GRN_vGRN_No.Text.ToString();
                com.Parameters.Add("@GRN_dGRN_Date", SqlDbType.DateTime).Value = GRN_dGRN_Date.Value.ToString("dd-MMM-yyyy hh:mm tt");
                com.Parameters.Add("@GRN_iPO_NO", SqlDbType.Int).Value = PO_vPO_NO.SelectedValue.ToString();
                com.Parameters.Add("@GRN_vInvoice_No", SqlDbType.VarChar).Value = GRN_vInvoice_No.Text.ToString();
                //com.Parameters.Add("@GRN_dInvoice_Date", SqlDbType.DateTime).Value = GRN_dInvoice_Date.Value.ToString("dd-MMM-yyyy");
               // com.Parameters.Add("@GRN_dInvoice_Date", SqlDbType.DateTime).Value = GRN_dInvoice_Date.Value.ToString("yyyyMMMdd");
                com.Parameters.Add("@GRN_dInvoice_Date", SqlDbType.Date).Value = GRN_dInvoice_Date.Value.Date;

                com.Parameters.Add("@GRN_vStoreLocation", SqlDbType.VarChar).Value = "";// GRN_vStoreLocation.Text.ToString();
                com.Parameters.Add("@GRN_iType", SqlDbType.Int).Value = GRN_iType.SelectedValue.ToString();
                com.Parameters.Add("@GRN_dSubTotal", SqlDbType.Decimal).Value = GRN_dSubTotal.Text.ToString();
                com.Parameters.Add("@GRN_vGST_Type1", SqlDbType.VarChar).Value = GRN_vGST_Type1.Text.ToString();
                com.Parameters.Add("@GRN_dGST_Percentage1", SqlDbType.Decimal).Value = GRN_dGST_Percentage1.Text.ToString();
                com.Parameters.Add("@GRN_dGST_Amount1", SqlDbType.Decimal).Value = GRN_dGST_Amount1.Text.ToString();
                com.Parameters.Add("@GRN_vGST_Type2", SqlDbType.VarChar).Value = GRN_vGST_Type2.Text.ToString();
                com.Parameters.Add("@GRN_dGST_Percentage2", SqlDbType.Decimal).Value = GRN_dGST_Percentage2.Text.ToString();
                com.Parameters.Add("@GRN_dGST_Amount2", SqlDbType.Decimal).Value = GRN_dGST_Amount2.Text.ToString();
                com.Parameters.Add("@GRN_dFreight", SqlDbType.Decimal).Value = GRN_dFreight.Text.ToString();
                com.Parameters.Add("@GRN_dOthers", SqlDbType.Decimal).Value = GRN_dOthers.Text.ToString();
                com.Parameters.Add("@GRN_dGross_Total", SqlDbType.Decimal).Value = GRN_dGross_Total.Text.ToString();
                //com.Parameters.Add("@GRN_dCreatedDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd/MM/yyyy");
                com.Parameters.Add("@GRN_dCreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;

                com.Parameters.Add("@ID", SqlDbType.Int);
                com.Parameters["@ID"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery(); 
                com.Connection.Close();
                GRND_iGRN_No = com.Parameters["@ID"].Value.ToString();
                //MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                save();

               

            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void save()
        {
            var flags = true;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                decimal qty = 0.0m;
                try
                {
                    qty = qty + decimal.Parse(dataGridView1.Rows[i].Cells["Total Qty"].Value.ToString());
                }catch{

                    qty = 0.0m;
                }

                if (qty > 0)
                {

                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {

                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "pr_Insert_Grin_Against_Purchase_Orderd_Details";

                        com.Parameters.Add("@GRND_iGRN_No", SqlDbType.Int).Value = GRND_iGRN_No;
                        com.Parameters.Add("@GRND_iItem", SqlDbType.Int).Value = dataGridView1.Rows[i].Cells["Item ID"].Value.ToString();
                        //com.Parameters.Add("@GRND_vMaterial_Name", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Spec"].Value.ToString();
                        //com.Parameters.Add("@GRND_vRM_Spec", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Spec"].Value.ToString();
                        com.Parameters.Add("@GRND_vMaterial_Name", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Part Name"].Value.ToString();
                        com.Parameters.Add("@GRND_vRM_Spec", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Part Name"].Value.ToString();
                        //com.Parameters.Add("@GRND_vRM_Grade", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Grade"].Value.ToString();
                        com.Parameters.Add("@GRND_vRM_Grade", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Part No"].Value.ToString();
                        com.Parameters.Add("@GRND_dUnit_Rate", SqlDbType.Decimal).Value = dataGridView1.Rows[i].Cells["Unit Price"].Value.ToString();
                        com.Parameters.Add("@GRND_dNo_Of_Bag", SqlDbType.Decimal).Value = dataGridView1.Rows[i].Cells["No of Bag"].Value.ToString();
                        com.Parameters.Add("@GRND_dQty_Per_Bag", SqlDbType.Decimal).Value = dataGridView1.Rows[i].Cells["Qty Bag"].Value.ToString();
                        com.Parameters.Add("@GRND_dTotal_Qty", SqlDbType.Decimal).Value = dataGridView1.Rows[i].Cells["Total Qty"].Value.ToString();
                        com.Parameters.Add("@GRND_vLot_No", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Lot No"].Value.ToString();
                        com.Parameters.Add("@GRND_vMFG_Date", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["MFG Date"].Value.ToString();
                        com.Parameters.Add("@GRND_vCreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;
                        com.Parameters.Add("@GRND_HeatNo", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Heat No"].Value.ToString();
                        com.Parameters.Add("@GRND_Type", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Type"].Value.ToString();
                        com.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error " + ex.Message);
                        flags = false;
                    }
                }

            }
            if (flags == true)
            {
                MessageBox.Show("GRN  Submited Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Upload_Data();
                this.Close();
            }
            //dataGridView1.Rows.Clear();
            
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if (dataGridView1.Columns[e.ColumnIndex].Name != "No of Pack")
                    return;

                DataGridViewRow currentRow = dataGridView1.Rows[e.RowIndex];

                string value = Convert.ToString(
                    currentRow.Cells["No of Pack"].Value
                );

                if (string.IsNullOrWhiteSpace(value))
                    return;

                if (!int.TryParse(value, out int noOfPack))
                    return;

                if (noOfPack <= 1)
                    return;

                DataTable dt = (DataTable)dataGridView1.DataSource;
                
                DataRow sourceRow =
                    ((DataRowView)currentRow.DataBoundItem).Row;
                sourceRow["No of Bag"] = "1";
                // Add duplicate rows
                for (int i = 1; i < noOfPack; i++)
                {
                    DataRow newRow = dt.NewRow();

                    foreach (DataColumn column in dt.Columns)
                    {
                        newRow[column.ColumnName] = sourceRow[column.ColumnName];
                    }
                    newRow["No of Bag"] = "1";
                    
                    dt.Rows.Add(newRow);
                }

                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            calulate();
        }

        private void GRN_dFreight_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void GRN_dOthers_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void GRN_dGST_Percentage1_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void GRN_dGST_Percentage2_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void GRN_dGST_Amount2_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void GRN_dGST_Amount1_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                double NoofBags = double.Parse(dataGridView1.Rows[e.RowIndex].Cells["No of Bag"].Value.ToString());
                double QtyBags = double.Parse(dataGridView1.Rows[e.RowIndex].Cells["Qty Bag"].Value.ToString());
                dataGridView1.Rows[e.RowIndex].Cells["Total Qty"].Value = (NoofBags * QtyBags).ToString();

                double UnitPrice = double.Parse(dataGridView1.Rows[e.RowIndex].Cells["Unit Price"].Value.ToString());

                dataGridView1.Rows[e.RowIndex].Cells["Total Amount"].Value = ((NoofBags * QtyBags) * UnitPrice).ToString();
                
            }
            catch
            {
            }
        }

        private void PO_vPO_NO_MouseClick(object sender, MouseEventArgs e)
        {
          
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {

            }
        }

        private void GRN_iType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        int row = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex ==  12)
                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "MFG Date")
                {
                row = e.RowIndex;
                monthCalendar1.Visible = true;
            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            dataGridView1.Rows[row].Cells["MFG Date"].Value = monthCalendar1.SelectionRange.Start.ToString("dd-MMM-yyyy");
            monthCalendar1.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Podc_FileName.Text = openFileDialog1.FileName.ToString();

            }
        }

        public static string Location = System.Configuration.ConfigurationSettings.AppSettings["Location"];

        public void Upload_Data()
        {

          


            string fileName = openFileDialog1.SafeFileName;

            DataTable dt = dbFunctions.getTable("pr_get_GRN_Detail_ID  '" + GRND_iGRN_No + "'");
            string dist = Location + GRN_vGRN_No.Text.Replace('/', '_') + "_" + dt.Rows[0][0].ToString() + "_" + fileName;

            string Loc = GRN_vGRN_No.Text.Replace('/', '_') + "_" + dt.Rows[0][0].ToString() + "_" + fileName;
            
            try
            {
                File.Copy(Podc_FileName.Text, dist);
            }
            catch(Exception ex) { }


            DataTable dd = dbFunctions.getTable("pr_Upload_GRN_Document  " + dt.Rows[0][0].ToString() + ",'" + Podc_Description.Text + "','" + Loc + "'");
            MessageBox.Show("Documnet Uploaded Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Podc_Description.Text = "";
            Podc_FileName.Text = "--";

        }
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
