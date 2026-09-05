using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;

namespace CRM_App.Transaction
{
    public partial class Purchase_Order_Amendment : Form
    {
        public string SupplierID = "";
        public string RefID = "";
        public string ID = "";
        int rowid = 0;
        string POD_PO_No;
        string ErrorMessage = "";
        public bool isPurchse_Load = false;
        public bool isLoadItem = false;

        public Purchase_Order_Amendment()
        {
            InitializeComponent();
        }

        private void Purchase_Order_Amendment_Load(object sender, EventArgs e)
        {
            LoadPO_No();
            LoadAmend_No();
            LoadItemType();
        }

        public void LoadPO_No()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Fetch_Amend_Purchase_Order_No1");
                PO_vPO_NO.DataSource = dt;
                PO_vPO_NO.DisplayMember = "PO_vPO_NO";
                PO_vPO_NO.ValueMember = "PO_iID";
                PO_vPO_NO.SelectedIndex = -1;
                isPurchse_Load = true;
            }
            catch
            {
            }
        }



        public void LoadItemType()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadItemType");
                IM_Type.DataSource = dt;
                IM_Type.DisplayMember = "Ty_TypeName";
                IM_Type.ValueMember = "Ty_ID";
                IM_Type.SelectedIndex = -1;
            }
            catch
            {
            }
        }
       

        private void LoadAmend_No()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_GetAmend_Number");
                PO_iAmendment_No.Text = dt.Rows[0][0].ToString();
            }
            catch
            {
            }
        }

        public void LoadItem()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_GetMetireal_PartNo '" + SupplierID + "'");
                POD_vPart_No.DataSource = dt;
                POD_vPart_No.DisplayMember = "IM_PartNo";
                POD_vPart_No.ValueMember = "IM_ID";
                POD_vPart_No.SelectedIndex = -1;
                isLoadItem = true;
            }
            catch
            {
            }
        }

        private void PO_vPO_NO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (isPurchse_Load==true)
                {
                    DataTable dt = dbFunctions.getTable("pr_Get_Purchase_Order_Approval_Details '" + PO_vPO_NO.SelectedValue.ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        PO_vSupplier_Name.Text = dt.Rows[0]["PO_vSupplier_Name"].ToString();
                        SupplierID = dt.Rows[0]["PO_iSupplierID"].ToString();
                        PO_vSupplier_Address.Text = dt.Rows[0]["PO_vSupplier_Address"].ToString();
                        PO_dPO_Date.Value = DateTime.Parse(dt.Rows[0]["PO_dPO_Date"].ToString());
                        PO_vSupplier_Qtn_Ref.Text = dt.Rows[0]["PO_vSupplier_Qtn_Ref"].ToString();
                        PO_vSupplier_Qtn_Date.Value = DateTime.Parse(dt.Rows[0]["PO_vSupplier_Qtn_Date"].ToString());
                        PO_vFreight.Text = dt.Rows[0]["PO_vFreight"].ToString();
                        PO_vMode_Of_Despatch.Text = dt.Rows[0]["PO_vMode_Of_Despatch"].ToString();
                        PO_vDelivery.Text = dt.Rows[0]["PO_vDelivery"].ToString();
                        PO_vPayment_Terms.Text = dt.Rows[0]["PO_vPayment_Terms"].ToString();
                        PO_vInspection.Text = dt.Rows[0]["PO_vInspection"].ToString();
                        PO_vWarrenty_Class.Text = dt.Rows[0]["PO_vWarrenty_Class"].ToString();
                        PO_dSub_Total.Text = dt.Rows[0]["PO_dSub_Total"].ToString();
                        PO_dFreight_Charge_Percentage.Text = dt.Rows[0]["PO_dFreight_Charge_Percentage"].ToString();
                        PO_dFreight_Charge.Text = dt.Rows[0]["PO_dFreight_Charge"].ToString();
                        PO_dInsurence_Charge_Percentage.Text = dt.Rows[0]["PO_dInsurence_Charge_Percentage"].ToString();
                        PO_dInsurence_Charge.Text = dt.Rows[0]["PO_dInsurence_Charge"].ToString();
                        PO_vGST_Type1.Text = dt.Rows[0]["PO_vGST_Type1"].ToString();
                        PO_dExcise_Duty_Percent.Text = dt.Rows[0]["PO_dExcise_Duty_Percent"].ToString();
                        PO_dExcise_Duty_Amount.Text = dt.Rows[0]["PO_dExcise_Duty_Amount"].ToString();
                        PO_vGST_Type2.Text = dt.Rows[0]["PO_vGST_Type2"].ToString();
                        PO_dVAT_CST_Percentage.Text = dt.Rows[0]["PO_dVAT_CST_Percentage"].ToString();
                        PO_dVAR_CST_Amount.Text = dt.Rows[0]["PO_dVAR_CST_Amount"].ToString();
                        PO_dGrand_Total.Text = dt.Rows[0]["PO_dGrand_Total"].ToString();

                    } LoadItem();
                    Display();
                }
               
            }
            catch (Exception ex)
            {
               

            }
           
            
        }

        private void Display()
        {

            try
            {
                DataTable dtDetails = dbFunctions.getTable("Pr_Fetch_Purchase_Order_Details '" + PO_vPO_NO.SelectedValue.ToString() + "'");
                dataGridView1.DataSource = dtDetails;
                dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
                dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                calulate();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(""+ex);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("Pr_Get_Purchase_Order_Details  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            RefID = dt.Rows[0]["ID"].ToString();
            POD_vPart_No.Text = dt.Rows[0]["Grade"].ToString();
            txtGrade.Text = dt.Rows[0]["Spec"].ToString();


            POD_vSource.Text = dt.Rows[0]["Source"].ToString();
            POD_iUOM.Text = dt.Rows[0]["UOM"].ToString();
            IM_Type.Text = dt.Rows[0]["Type"].ToString();
            POD_vPacking_Std.Text = dt.Rows[0]["Packing Std"].ToString();
            POD_dUnit_Price.Text = dt.Rows[0]["Unit Price"].ToString();
            
            POD_dQty.Text = dt.Rows[0]["Qty"].ToString();
            txtTotalPrice.Text = dt.Rows[0]["Total Amount"].ToString();

            POD_Note.Text = dt.Rows[0]["Note"].ToString();
            button3.Text = "&Update";
        }

        public void UpdateStatus()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {

                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Update_Status";

                com.Parameters.Add("@POD_iid", SqlDbType.VarChar).Value = RefID;
                com.Parameters.Add("@POD_vStatus", SqlDbType.VarChar).Value = 'U';
                com.ExecuteNonQuery();
                com.Connection.Close();
                //MessageBox.Show("Details Updated Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);

            }
        }


        public void calulate()
        {
            try
            {
                decimal Gross_tot = 0m;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    Gross_tot += decimal.Parse(dataGridView1.Rows[i].Cells["Total Amount"].Value.ToString());

                }
                PO_dSub_Total.Text = Gross_tot.ToString("0.00");

                PO_dFreight_Charge.Text = (((Decimal.Parse(PO_dSub_Total.Text) * (Decimal.Parse((PO_dFreight_Charge_Percentage.Text)) / 100)))).ToString("0.00");
                PO_dInsurence_Charge.Text = (((Decimal.Parse(PO_dSub_Total.Text) * (Decimal.Parse((PO_dInsurence_Charge_Percentage.Text)) / 100)))).ToString("0.00");
                PO_dExcise_Duty_Amount.Text = (((Decimal.Parse(PO_dSub_Total.Text) * (Decimal.Parse((PO_dExcise_Duty_Percent.Text)) / 100)))).ToString("0.00");
                PO_dVAR_CST_Amount.Text = (((Decimal.Parse(PO_dSub_Total.Text) * (Decimal.Parse((PO_dVAT_CST_Percentage.Text)) / 100)))).ToString("0.00");

                PO_dGrand_Total.Text = (Decimal.Parse(PO_dSub_Total.Text) + ((Decimal.Parse(PO_dFreight_Charge.Text) + ((Decimal.Parse(PO_dInsurence_Charge.Text) + ((Decimal.Parse(PO_dExcise_Duty_Amount.Text) + Decimal.Parse(PO_dVAR_CST_Amount.Text)))))))).ToString("0");

            }
            catch (Exception ex)
            {
            }
        }

        private void PO_dFreight_Charge_Percentage_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dInsurence_Charge_Percentage_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dExcise_Duty_Percent_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dVAT_CST_Percentage_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dSub_Total_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dFreight_Charge_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dInsurence_Charge_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dExcise_Duty_Amount_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dVAR_CST_Amount_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void POD_dQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal n = decimal.Parse(POD_dQty.Text);
                if (n < 1)
                {
                    MessageBox.Show("Enter Min 1 Qty is 0", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    POD_dQty.Text = "";
                    txtTotalPrice.Text = "";
                    POD_dQty.Focus();
                }

                //if (POD_dQty.Text == "")
                //    POD_dQty.Text = "0";

                //if (POD_dUnit_Price.Text == "")
                //    POD_dUnit_Price.Text = "0";

                txtTotalPrice.Text = (float.Parse(POD_dQty.Text.ToString()) * float.Parse(POD_dUnit_Price.Text.ToString())).ToString();
            }
            catch (Exception ex)
            {
                POD_dQty.Text = "";
                POD_dQty.Focus();
            }
        }


        //decimal total = 0m;
        public void AddPurchaseOrderDetails()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {

                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Insert_Amend_Purchase_Order_Details";

                //com.Parameters.Add("@POD_iid", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@POD_PO_No", SqlDbType.Int).Value = PO_vPO_NO.SelectedValue.ToString();
                com.Parameters.Add("@POD_iType", SqlDbType.VarChar).Value = IM_Type.Text.ToString();
                com.Parameters.Add("@POD_vItem_ID", SqlDbType.Int).Value = POD_vPart_No.SelectedValue.ToString();
                com.Parameters.Add("@POD_vPart_Number", SqlDbType.VarChar).Value = POD_vPart_No.Text.ToString();
                com.Parameters.Add("@POD_vSpec", SqlDbType.VarChar).Value = txtGrade.Text.ToString();
                com.Parameters.Add("@POD_vSource", SqlDbType.VarChar).Value = POD_vSource.Text.ToString();
                com.Parameters.Add("@POD_vPacking_Std", SqlDbType.VarChar).Value = POD_vPacking_Std.Text.ToString();
                com.Parameters.Add("@POD_iUOM", SqlDbType.VarChar).Value = POD_iUOM.Text.ToString();
                com.Parameters.Add("@POD_dQty", SqlDbType.Decimal).Value = POD_dQty.Text.ToString();
                com.Parameters.Add("@POD_dUnit_Price", SqlDbType.Decimal).Value = POD_dUnit_Price.Text.ToString();
                com.Parameters.Add("@POD_dTotal_Amount", SqlDbType.Decimal).Value = txtTotalPrice.Text.ToString();
                //com.Parameters.Add("@POD_iRef_ID", SqlDbType.Int).Value = ID;
                com.Parameters.Add("@POD_iAmd_No", SqlDbType.Int).Value = PO_iAmendment_No.Text.ToString();

                com.Parameters.Add("@POD_Note", SqlDbType.VarChar).Value = POD_Note.Text;
                com.Parameters.Add("@POD_vCreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;
                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Added Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Display();
                calulate();
                POD_vPart_No.Text = "";
                POD_vSource.Text = "";
                POD_iUOM.Text = "";
                POD_dUnit_Price.Text = "";
                IM_Type.Text = "";
                POD_vPacking_Std.Text = "";
                txtGrade.Text = "";
                POD_Note.Text = "";
                POD_dQty.Text = "";
                txtTotalPrice.Text = "";
                RefID = "0";
                button3.Text = "Add";


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);

            }
        }

        public void LocalGridUpdate()
        {
            this.dataGridView1.Update();
            dataGridView1["ID", rowid].Value = RefID;
            dataGridView1["Part No", rowid].Value = POD_vPart_No.Text;
            dataGridView1["Spec", rowid].Value = txtGrade.Text;
            dataGridView1["Source", rowid].Value = POD_vSource.Text;
            dataGridView1["Part Name", rowid].Value = txtGrade.Text;
            dataGridView1["UOM", rowid].Value = POD_iUOM.Text;
            dataGridView1["Qty", rowid].Value = POD_dQty.Text;
            dataGridView1["Unit Price", rowid].Value = POD_dUnit_Price.Text;
            dataGridView1["Total Amount", rowid].Value = txtTotalPrice.Text;
            rowid += 1;
            calulate();
            POD_vPart_No.Text = "";
            POD_vSource.Text = "";
            POD_iUOM.Text = "";
            POD_dUnit_Price.Text = "";
            txtGrade.Text = "";
            POD_dQty.Text = "";
            POD_vPacking_Std.Text="";
            txtTotalPrice.Text = "";
            button3.Text = "&Save    ";
            calulate();
        }

        public void UpdatePurchaseOrderDetails()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {

                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Update_Purchase_Order_Details";

                //com.Parameters.Add("@POD_iid", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@POD_PO_No", SqlDbType.Int).Value = PO_vPO_NO.SelectedValue.ToString();
                com.Parameters.Add("@POD_iType", SqlDbType.VarChar).Value = IM_Type.Text.ToString();
                com.Parameters.Add("@POD_vItem_ID", SqlDbType.Int).Value = POD_vPart_No.SelectedValue.ToString();
                com.Parameters.Add("@POD_vPart_Number", SqlDbType.VarChar).Value = POD_vPart_No.Text.ToString();
                com.Parameters.Add("@POD_vSpec", SqlDbType.VarChar).Value = txtGrade.Text.ToString();
                com.Parameters.Add("@POD_vSource", SqlDbType.VarChar).Value = POD_vSource.Text.ToString();
                com.Parameters.Add("@POD_vPacking_Std", SqlDbType.VarChar).Value = POD_vPacking_Std.Text.ToString();
                com.Parameters.Add("@POD_iUOM", SqlDbType.VarChar).Value = POD_iUOM.Text.ToString();
                com.Parameters.Add("@POD_dQty", SqlDbType.Decimal).Value = POD_dQty.Text.ToString();
                com.Parameters.Add("@POD_dUnit_Price", SqlDbType.Decimal).Value = POD_dUnit_Price.Text.ToString();
                com.Parameters.Add("@POD_dTotal_Amount", SqlDbType.Decimal).Value = txtTotalPrice.Text.ToString();
                com.Parameters.Add("@POD_iRef_ID", SqlDbType.Int).Value = RefID;
                com.Parameters.Add("@POD_iAmd_No", SqlDbType.Int).Value = PO_iAmendment_No.Text.ToString();
                com.Parameters.Add("@POD_vCreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;
                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Updated Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateStatus();

                
                POD_vPart_No.Text = "";
                POD_vSource.Text = "";
                POD_iUOM.Text = "";
                POD_dUnit_Price.Text = "";
                txtGrade.Text = "";
                IM_Type.Text = "";
                POD_vPacking_Std.Text = "";
                POD_dQty.Text = "";
                txtTotalPrice.Text = "";
                button3.Text = "&Save    ";
                Display();
                calulate();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Validate2())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (button3.Text.ToString().Equals("&Update"))
            {
                DataTable dt = dbFunctions.getTable("delete from  Purchase_Order_Details  where POD_iid=" + RefID);
                AddPurchaseOrderDetails();
            }
            else
            {
                AddPurchaseOrderDetails();
            }
        }

        public void Clear()
        {
            POD_vPart_No.Text = "";
            POD_vSource.Text = "";
            POD_iUOM.Text = "";
            POD_dUnit_Price.Text = "";
            //POD_vSpec.Text = "";
            txtGrade.Text = "";
            POD_dQty.Text = "";
            txtTotalPrice.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Delete();
        }

        public void Delete()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("pr_Delete_Purchase_Order_Details " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Display();
                    //Clear();
                }
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool Validate1()
        {
            if ((string.IsNullOrEmpty(PO_vPO_NO.Text.Trim())))
            {
                ErrorMessage = "PO Name Should Not be Empty";
                PO_vPO_NO.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(PO_dFreight_Charge_Percentage.Text.Trim())))
            {
                ErrorMessage = "Freight Should Not be Empty";
                PO_dFreight_Charge_Percentage.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(PO_dInsurence_Charge_Percentage.Text.Trim())))
            {
                ErrorMessage = "Insurence Should Not be Empty";
                PO_dInsurence_Charge_Percentage.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(PO_vGST_Type1.Text.Trim())))
            {
                ErrorMessage = "GST Type 1 Should Not be Empty";
                PO_vGST_Type1.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(PO_vGST_Type2.Text.Trim())))
            {
                ErrorMessage = "GST Type 2 Should Not be Empty";
                PO_vGST_Type2.Focus();
                return true;
            }
            return false;
        }



        public bool Validate2()
        {
            if ((string.IsNullOrEmpty(POD_vPart_No.Text.Trim())))
            {
                ErrorMessage = "Part No Should Not be Empty";
                POD_vPart_No.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(IM_Type.Text.Trim())))
            {
                ErrorMessage = "Type Should Not be Empty";
                IM_Type.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(POD_dQty.Text.Trim())))
            {
                ErrorMessage = "Qty Should Not be Empty";
                POD_dQty.Focus();
                return true;
            }
            return false;
        }
        private void POD_vPart_No_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (isLoadItem && isPurchse_Load)
            {
                DataTable dt = dbFunctions.getTable("pr_Get_PartNo_Details '" + POD_vPart_No.SelectedValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    //POD_vPart_No.Text = dt.Rows[0]["IM_PartName"].ToString();
                    POD_vSource.Text = dt.Rows[0]["IM_RMSource"].ToString();
                    txtGrade.Text = dt.Rows[0]["IM_PartName"].ToString();
                    POD_iUOM.Text = dt.Rows[0]["UM_UOM"].ToString();
                    POD_vPacking_Std.Text = dt.Rows[0]["IM_PackingStandard"].ToString();
                    POD_dUnit_Price.Text = dt.Rows[0]["IM_Purchase_Price"].ToString();
                }
            }
            
        }

        public void save_summary()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Update_Amendment_Purchase_Order";

                com.Parameters.Add("@PO_iID", SqlDbType.VarChar).Value = PO_vPO_NO.SelectedValue.ToString();
                com.Parameters.Add("@PO_vPO_NO", SqlDbType.VarChar).Value = PO_vPO_NO.Text.ToString();
                com.Parameters.Add("@PO_dPO_Date", SqlDbType.DateTime).Value = PO_dPO_Date.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@PO_iSupplierID", SqlDbType.VarChar).Value = SupplierID;
                com.Parameters.Add("@PO_vSupplier_Name", SqlDbType.VarChar).Value = PO_vSupplier_Name.Text.ToString();
                com.Parameters.Add("@PO_vSupplier_Address", SqlDbType.VarChar).Value = PO_vSupplier_Address.Text.ToString();
                com.Parameters.Add("@PO_vSupplier_Qtn_Ref", SqlDbType.VarChar).Value = PO_vSupplier_Qtn_Ref.Text.ToString();
                com.Parameters.Add("@PO_iAmendment_No", SqlDbType.Int).Value = PO_iAmendment_No.Text.ToString();
                com.Parameters.Add("@PO_dAmendment_Date", SqlDbType.DateTime).Value = PO_dAmendment_Date.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@PO_vSupplier_Qtn_Date", SqlDbType.DateTime).Value = PO_vSupplier_Qtn_Date.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@PO_dSub_Total", SqlDbType.Decimal).Value = PO_dSub_Total.Text.ToString();
                com.Parameters.Add("@PO_dFreight_Charge_Percentage", SqlDbType.Decimal).Value = PO_dFreight_Charge_Percentage.Text.ToString();
                com.Parameters.Add("@PO_dFreight_Charge", SqlDbType.Decimal).Value = PO_dFreight_Charge.Text.ToString();
                com.Parameters.Add("@PO_dInsurence_Charge_Percentage", SqlDbType.Decimal).Value = PO_dInsurence_Charge_Percentage.Text.ToString();
                com.Parameters.Add("@PO_dInsurence_Charge", SqlDbType.Decimal).Value = PO_dInsurence_Charge.Text.ToString();
                com.Parameters.Add("@PO_vGST_Type1", SqlDbType.VarChar).Value = PO_vGST_Type1.Text.ToString();
                com.Parameters.Add("@PO_dExcise_Duty_Percent", SqlDbType.Decimal).Value = PO_dExcise_Duty_Percent.Text.ToString();
                com.Parameters.Add("@PO_dExcise_Duty_Amount", SqlDbType.Decimal).Value = PO_dExcise_Duty_Amount.Text.ToString();
                com.Parameters.Add("@PO_vGST_Type2", SqlDbType.VarChar).Value = PO_vGST_Type2.Text.ToString();
                com.Parameters.Add("@PO_dVAT_CST_Percentage", SqlDbType.Decimal).Value = PO_dVAT_CST_Percentage.Text.ToString();
                com.Parameters.Add("@PO_dVAR_CST_Amount", SqlDbType.Decimal).Value = PO_dVAR_CST_Amount.Text.ToString();
                com.Parameters.Add("@PO_dGrand_Total", SqlDbType.Decimal).Value = PO_dGrand_Total.Text.ToString();
                com.Parameters.Add("@PO_vFreight", SqlDbType.VarChar).Value = PO_vFreight.Text.ToString();
                com.Parameters.Add("@PO_vMode_Of_Despatch", SqlDbType.VarChar).Value = PO_vMode_Of_Despatch.Text.ToString();
                com.Parameters.Add("@PO_vDelivery", SqlDbType.VarChar).Value = PO_vDelivery.Text.ToString();
                com.Parameters.Add("@PO_vPayment_Terms", SqlDbType.VarChar).Value = PO_vPayment_Terms.Text.ToString();
                com.Parameters.Add("@PO_vInspection", SqlDbType.VarChar).Value = PO_vInspection.Text.ToString();
                com.Parameters.Add("@PO_vWarrenty_Class", SqlDbType.VarChar).Value = PO_vWarrenty_Class.Text.ToString();
                com.Parameters.Add("@PO_vCreatedby", SqlDbType.VarChar).Value = dbFunctions.username;

                com.ExecuteNonQuery(); 
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Clear();
                //Display();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (Validate1())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                save_summary();
            }  
        }

        private void POD_dQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
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

        private void IM_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

            POD_dUnit_Price.Enabled = false;
            POD_dUnit_Price.ReadOnly = true;

            POD_vSource.Enabled = false;
            POD_vSource.ReadOnly = true;



            txtGrade.Enabled = false;
            txtGrade.ReadOnly = true;


            if (IM_Type.Text.ToUpper().Equals("RM"))
            {
                label21.Text = "Spec :";
                label18.Text = "Grade :";

            }
            if (IM_Type.Text.ToUpper().Equals("OTHERS"))
            {

                label21.Text = "Part :";
                label18.Text = "Name :";
                POD_dUnit_Price.Enabled = true;
                POD_dUnit_Price.ReadOnly = false;
                POD_vSource.Enabled = true;
                POD_vSource.ReadOnly = false;



                txtGrade.Enabled = true;
                txtGrade.ReadOnly = false;


                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + SupplierID + "'");
                    POD_vPart_No.DataSource = dt;
                    POD_vPart_No.DisplayMember = "IM_PartNo";
                    POD_vPart_No.ValueMember = "IM_ID";
                    POD_vPart_No.SelectedIndex = -1;
                    isLoadItem = true;
                }
                catch
                {
                }



            }
            else
            {
                label21.Text = "Part :";
                label18.Text = "Name :";
            }
        }
    }
}
