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

namespace LarchERP.Master
{

    public partial class SupplierPerformanceMonotoring : Form
    {
        public string arrow = "Up";
        public int Distance = 300;
        public string ID = "";
        string ErrorMessage = "";
        public SupplierPerformanceMonotoring()
        {
            InitializeComponent();
            dbFunctions.DGVStyle(dataGridView1);
        }




        private void ItemMaster_Shown(object sender, EventArgs e)
        {
            display();
            splitContainer1.SplitterDistance = Distance;
            SPM_PONo.Focus();
        }



        private void ArrowButton_Click(object sender, EventArgs e)
        {
            if (arrow.ToString().Equals("Up"))
            {
                splitContainer1.SplitterDistance = 25;
                arrow = "Down";
                ArrowButton.Image = CRM_App.Properties.Resources.Down;

            }
            else
            {
                splitContainer1.SplitterDistance = Distance;
                arrow = "Up";
                ArrowButton.Image = CRM_App.Properties.Resources.Up;
            }

        }
       // public bool isSupplierLoad = false;
        private void ItemMaster_Load(object sender, EventArgs e)
        {
            display();
            LoadSupplier();
        }
        public void LoadSupplier()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadSupplier");
                SPM_SupplierID.DataSource = dt;
                SPM_SupplierID.DisplayMember = "SM_Name";
                SPM_SupplierID.ValueMember = "SM_ID";
                SPM_SupplierID.SelectedIndex = -1;
                //isSupplierLoad = true;
            }
            catch
            {
            }
        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (btnsave.Text.ToString().Equals("&Update"))
            {
                Update();
            }
            else
            {
                insert();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;

        }

        public void insert()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Insert_Supplier_Performance_Monitoring";
                com.Parameters.Add("@SPM_PONo", SqlDbType.VarChar).Value = SPM_PONo.Text.ToString();
                com.Parameters.Add("@SPM_ItemNo", SqlDbType.VarChar).Value = SPM_ItemNo.Text.ToString();
                com.Parameters.Add("@SPM_ItemName", SqlDbType.VarChar).Value = SPM_ItemName.Text.ToString();
                com.Parameters.Add("@SPM_SupplierID", SqlDbType.VarChar).Value = SPM_SupplierID.SelectedValue.ToString();
                com.Parameters.Add("@SPM_UOM", SqlDbType.VarChar).Value = SPM_UOM.Text.ToString();
                com.Parameters.Add("@SPM_UnitCost", SqlDbType.Decimal).Value = SPM_UnitCost.Text.ToString();
                com.Parameters.Add("@SPM_PremiumFreight", SqlDbType.VarChar).Value = SPM_PremiumFreight.Text.ToString();
                com.Parameters.Add("@SPM_OrderDate", SqlDbType.DateTime).Value = SPM_OrderDate.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@SPM_Scheduledate", SqlDbType.DateTime).Value = SPM_OrderDate.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@SPM_OrderQty", SqlDbType.VarChar).Value = SPM_OrderQty.Text.ToString();
                com.Parameters.Add("@SPM_OnTimeSubmission", SqlDbType.VarChar).Value = SPM_OnTimeSubmission.Text.ToString();
                com.Parameters.Add("@SPM_RespondComplaints", SqlDbType.VarChar).Value = SPM_RespondComplaints.Text.ToString();
                com.Parameters.Add("@SPM_PPM_Rating", SqlDbType.Decimal).Value = SPM_PPM_Rating.Text.ToString();
                com.Parameters.Add("@SPM_PackkingQuality", SqlDbType.VarChar).Value = SPM_PackkingQuality.Text.ToString();
                com.Parameters.Add("@SPM_ShortageResponse", SqlDbType.VarChar).Value = SPM_ShortageResponse.Text.ToString();
                com.Parameters.Add("@SPM_DaysDelay", SqlDbType.VarChar).Value = SPM_DaysDelay.Text.ToString();
                com.Parameters.Add("@SPM_OnTimeDelivery", SqlDbType.VarChar).Value = SPM_OnTimeDelivery.Text.ToString();
                com.Parameters.Add("@SPM_QtyReceived", SqlDbType.VarChar).Value = SPM_QtyReceived.Text.ToString();
                com.Parameters.Add("@SPM_PendingExtraQty", SqlDbType.VarChar).Value = SPM_PendingExtraQty.Text.ToString();
                com.Parameters.Add("@SPM_ReceivedDate", SqlDbType.DateTime).Value = SPM_ReceivedDate.Text.ToString();

                com.Parameters.Add("@SPM_QualityatReceipt", SqlDbType.VarChar).Value = SPM_QualityatReceipt.Text.ToString();
                com.Parameters.Add("@SPM_FreightPremium", SqlDbType.VarChar).Value = SPM_FreightPremium.Text.ToString();
                com.Parameters.Add("@SPM_ResponseShortage", SqlDbType.VarChar).Value = SPM_ResponseShortage.Text.ToString();
                com.Parameters.Add("@SPM_Total", SqlDbType.Decimal).Value = SPM_Total.Text.ToString();
                com.Parameters.Add("@SPM_Grade", SqlDbType.VarChar).Value = SPM_Grade.Text.ToString();
                com.Parameters.Add("@SPM_Remars", SqlDbType.VarChar).Value = SPM_Remars.Text.ToString();

              

              
                //com.Parameters.Add("@EM_User", SqlDbType.VarChar).Value = dbFunctions.username;
                com.ExecuteNonQuery();  com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                display();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Update()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Update_Supplier_Performance_Monitoring";
                com.Parameters.Add("@SPM_Id", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@SPM_PONo", SqlDbType.VarChar).Value = SPM_PONo.Text.ToString();
                com.Parameters.Add("@SPM_ItemNo", SqlDbType.VarChar).Value = SPM_ItemNo.Text.ToString();
                com.Parameters.Add("@SPM_ItemName", SqlDbType.VarChar).Value = SPM_ItemName.Text.ToString();
                com.Parameters.Add("@SPM_SupplierID", SqlDbType.VarChar).Value = SPM_SupplierID.SelectedValue.ToString();
                com.Parameters.Add("@SPM_UOM", SqlDbType.VarChar).Value = SPM_UOM.Text.ToString();
                com.Parameters.Add("@SPM_UnitCost", SqlDbType.Decimal).Value = SPM_UnitCost.Text.ToString();
                com.Parameters.Add("@SPM_PremiumFreight", SqlDbType.VarChar).Value = SPM_PremiumFreight.Text.ToString();
                com.Parameters.Add("@SPM_OrderDate", SqlDbType.DateTime).Value = SPM_OrderDate.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@SPM_Scheduledate", SqlDbType.DateTime).Value = SPM_OrderDate.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@SPM_OrderQty", SqlDbType.VarChar).Value = SPM_OrderQty.Text.ToString();
                com.Parameters.Add("@SPM_OnTimeSubmission", SqlDbType.VarChar).Value = SPM_OnTimeSubmission.Text.ToString();
                com.Parameters.Add("@SPM_RespondComplaints", SqlDbType.VarChar).Value = SPM_RespondComplaints.Text.ToString();
                com.Parameters.Add("@SPM_PPM_Rating", SqlDbType.Decimal).Value = SPM_PPM_Rating.Text.ToString();
                com.Parameters.Add("@SPM_PackkingQuality", SqlDbType.VarChar).Value = SPM_PackkingQuality.Text.ToString();
                com.Parameters.Add("@SPM_ShortageResponse", SqlDbType.VarChar).Value = SPM_ShortageResponse.Text.ToString();
                com.Parameters.Add("@SPM_DaysDelay", SqlDbType.VarChar).Value = SPM_DaysDelay.Text.ToString();
                com.Parameters.Add("@SPM_OnTimeDelivery", SqlDbType.VarChar).Value = SPM_OnTimeDelivery.Text.ToString();
                com.Parameters.Add("@SPM_QtyReceived", SqlDbType.VarChar).Value = SPM_QtyReceived.Text.ToString();
                com.Parameters.Add("@SPM_PendingExtraQty", SqlDbType.VarChar).Value = SPM_PendingExtraQty.Text.ToString();
                com.Parameters.Add("@SPM_ReceivedDate", SqlDbType.DateTime).Value = SPM_ReceivedDate.Text.ToString();

                com.Parameters.Add("@SPM_QualityatReceipt", SqlDbType.VarChar).Value = SPM_QualityatReceipt.Text.ToString();
                com.Parameters.Add("@SPM_FreightPremium", SqlDbType.VarChar).Value = SPM_FreightPremium.Text.ToString();
                com.Parameters.Add("@SPM_ResponseShortage", SqlDbType.VarChar).Value = SPM_ResponseShortage.Text.ToString();
                com.Parameters.Add("@SPM_Total", SqlDbType.Decimal).Value = SPM_Total.Text.ToString();
                com.Parameters.Add("@SPM_Grade", SqlDbType.VarChar).Value = SPM_Grade.Text.ToString();
                com.Parameters.Add("@SPM_Remars", SqlDbType.VarChar).Value = SPM_Remars.Text.ToString();
                com.ExecuteNonQuery();  com.Connection.Close();
                MessageBox.Show("Details Updated Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                display();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Edit()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("pr_Edit_Supplier_Performance_Monitoring  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["SPM_Id"].ToString();
                SPM_PONo.Text = dt.Rows[0]["SPM_PONo"].ToString();
                SPM_ItemNo.Text = dt.Rows[0]["SPM_ItemNo"].ToString();
                SPM_ItemName.Text = dt.Rows[0]["SPM_ItemName"].ToString();
                SPM_SupplierID.SelectedValue = dt.Rows[0]["SPM_SupplierID"].ToString();
                SPM_UOM.Text = dt.Rows[0]["SPM_UOM"].ToString();
                SPM_UnitCost.Text = dt.Rows[0]["SPM_UnitCost"].ToString();
                SPM_PremiumFreight.Text = dt.Rows[0]["SPM_PremiumFreight"].ToString();
                SPM_OrderDate.Text = dt.Rows[0]["SPM_OrderDate"].ToString();
                SPM_Scheduledate.Text = dt.Rows[0]["SPM_Scheduledate"].ToString();
                SPM_OrderQty.Text = dt.Rows[0]["SPM_OrderQty"].ToString();
                SPM_OnTimeSubmission.Text = dt.Rows[0]["SPM_OnTimeSubmission"].ToString();
                SPM_RespondComplaints.Text = dt.Rows[0]["SPM_RespondComplaints"].ToString();
                SPM_PPM_Rating.Text = dt.Rows[0]["SPM_PPM_Rating"].ToString();
                SPM_PackkingQuality.Text = dt.Rows[0]["SPM_PackkingQuality"].ToString();
                SPM_ShortageResponse.Text = dt.Rows[0]["SPM_ShortageResponse"].ToString();
                SPM_DaysDelay.Text = dt.Rows[0]["SPM_DaysDelay"].ToString();
                SPM_OnTimeDelivery.Text = dt.Rows[0]["SPM_OnTimeDelivery"].ToString();
                SPM_QtyReceived.Text = dt.Rows[0]["SPM_QtyReceived"].ToString();
                SPM_PendingExtraQty.Text = dt.Rows[0]["SPM_PendingExtraQty"].ToString();
                SPM_ReceivedDate.Text = dt.Rows[0]["SPM_ReceivedDate"].ToString();


                SPM_QualityatReceipt.Text = dt.Rows[0]["SPM_QualityatReceipt"].ToString();
                SPM_FreightPremium.Text = dt.Rows[0]["SPM_FreightPremium"].ToString();
                SPM_ResponseShortage.Text = dt.Rows[0]["SPM_ResponseShortage"].ToString();
                SPM_Total.Text = dt.Rows[0]["SPM_Total"].ToString();
                SPM_Grade.Text = dt.Rows[0]["SPM_Grade"].ToString();
                SPM_Remars.Text = dt.Rows[0]["SPM_Remars"].ToString();

                btnsave.Text = "&Update";
                SPM_PONo.Focus();
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Delete()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("pr_Delete_Supplier_Performance_Monitoring " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    display();
                    Clear();
                }
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void display()
        {
            DataTable dt = dbFunctions.getTable("pr_Dispaly_Supplier_Performance_Monitoring");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
           
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }

        public void display_inactive()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Employee_Master_Details_inactive");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }


        public void Clear()
        {
            SPM_PONo.Text = "";
            SPM_ItemNo.Text = "";
            SPM_ItemName.Text = "";
            SPM_SupplierID.SelectedIndex = -1;
    
            SPM_UOM.Text = "";
            SPM_UnitCost.Text = "0";
            SPM_PremiumFreight.SelectedIndex = -1;
            SPM_OrderDate.Text = "";
            SPM_Scheduledate.Text = "";
            SPM_OrderQty.Text = "";
            SPM_OnTimeSubmission.Text = "";
            SPM_RespondComplaints.Text = "";
            SPM_PPM_Rating.Text = "0";
            SPM_PackkingQuality.Text = "";
            SPM_ShortageResponse.Text = "";
            SPM_DaysDelay.Text = "";
            SPM_OnTimeDelivery.Text = "";
            SPM_QtyReceived.Text = "";
            SPM_PendingExtraQty.Text = "";
            SPM_ReceivedDate.Text = "";


            SPM_QualityatReceipt.Text = "";
                SPM_FreightPremium.Text = "";
                SPM_ResponseShortage.Text = "";
                    SPM_Total.Text = "0";
                    SPM_Remars.Text = "";
                    SPM_Grade.Text = "";
            btnsave.Text = "&Save    ";
            SPM_PONo.Focus();
        }


        public bool Validate()
        {
            if ((string.IsNullOrEmpty(SPM_SupplierID.Text.Trim())))
            {
                ErrorMessage = "Supplier Should Not be Empty";
                SPM_SupplierID.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(SPM_PONo.Text.Trim())))
            {
                ErrorMessage = "PO Nubmer Should Not be Empty";
                SPM_PONo.Focus();
                return true;
            }
          
            return false;
        }
        private void Employee_Master_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            if (e.KeyCode == Keys.F3)
            {
                Edit();
            }
            if (e.KeyCode == Keys.F4)
            {
                if (Validate())
                {
                    MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Update();
            }
            if (e.KeyCode == Keys.F5)
            {
                display();

            }
            if (e.KeyCode == Keys.F6)
            {
                Delete();
            }
            if (e.KeyCode == Keys.F7)
            {
                Clear();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            display_inactive();
        }

        private void Radio_Active_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {

                this.Close();

            }
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(textBoxX1.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Code] LIKE '%{0}%' OR [EmployeeName] LIKE '%{0}%' OR [FaherName] LIKE '%{0}%' OR [Gender] LIKE '%{0}%' OR [Department] LIKE '%{0}%' OR [Designation] LIKE '%{0}%' OR [State] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }

        private void EM_PinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void EM_ContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);

        }

        private void EM_Code_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Code Here...";
        }

        private void EM_EmployeeName_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Name Here...";

        }

        private void EM_FaherName_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Father's Name Here...";

        }

        private void EM_Gender_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Gender Here...";

        }

        private void EM_Marital_Status_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Marital Status Here...";

        }

        private void EM_DOB_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Date Of Birth Here...";

        }

        private void EM_DOJ_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Date Of Joining Here...";

        }

        private void EM_Department_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Department Here...";

        }

        private void EM_Designation_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Designation Here...";

        }

        private void EM_Category_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Category Here...";

        }

        private void EM_Branch_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Branch Here...";

        }

        private void EM_AccoutNumber_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Account Number Here...";

        }

        private void EM_PFNo_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee PF Number Here...";

        }

        private void EM_ESINo_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee ESI Number Here...";

        }

        private void EM_Address1_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Address 1 Here...";

        }

        private void EM_Address2_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Address 2 Here...";

        }

        private void EM_City_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee City Here...";

        }

        private void EM_State_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee State Here...";

        }

        private void EM_PinCode_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee PinCode Here...";

        }

        private void EM_ContactNo_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee Contact Number Here...";

        }

        private void EM_EmailID_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Employee E-Mail ID Here...";

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void EM_Category_TextChanged(object sender, EventArgs e)
        {

        }

        private void EM_ESINo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void EM_PFNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void EM_AccoutNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Branch_Click(object sender, EventArgs e)
        {

        }

        private void EM_Branch_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }


   }
}