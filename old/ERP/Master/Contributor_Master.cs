using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;

namespace LarchERP.Master
{

    public partial class Contributor_Master : Form
    {
        public string arrow = "Up";
        public int Distance = 301;
        public string ID = "";
        string ErrorMessage = "";
        public Contributor_Master()
        {
            InitializeComponent();


            dbFunctions.DGVStyle(dataGridView1);
        }




        private void ItemMaster_Shown(object sender, EventArgs e)
        {
            Radio_Active.Checked = true;
            //display();
            //Pending();
            Approved();
            splitContainer1.SplitterDistance = Distance;
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

        private void ItemMaster_Load(object sender, EventArgs e)
        {
            LoadCustomerCode();
            CM_Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CM_Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection strcoll = new AutoCompleteStringCollection();

            DataTable dtcode = dbFunctions.getTable("select CM_Name from Customer_Master where CM_Status='A'");
            for (int i = 0; i < dtcode.Rows.Count; i++)
            {
                strcoll.Add(dtcode.Rows[i]["CM_Name"].ToString());
            }
            CM_Name.AutoCompleteCustomSource = strcoll;
            
            
            
            
            Radio_Active.Checked = true;
            //display();
            Pending();
            CM_Code.Focus();
            //dbFunctions.status = "F1 -Help, F2 -Save, F3 -Edit, F4 -Update,  F5 -Refresh, F6 -Delete, F7 -Clear";
         


        }

        public void LoadCustomerCode()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_GenerateCustomerNo");
                CM_Code.Text = dt.Rows[0][0].ToString();

            }
            catch
            {
            }
        }



        private void btnDisplay_Click(object sender, EventArgs e)
        {
            //display();
            Approved();
            Radio_Active.Checked = true;
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
                com.CommandText = "pr_Insert_Customer_Master";
               
                com.Parameters.Add("@CM_Code", SqlDbType.VarChar).Value = CM_Code.Text.ToString();
                com.Parameters.Add("@CM_Name", SqlDbType.VarChar).Value = CM_Name.Text.ToString();
                com.Parameters.Add("@CM_BillingAddress", SqlDbType.VarChar).Value = CM_BillingAddress.Text.ToString();
                com.Parameters.Add("@CM_ContactPerson", SqlDbType.VarChar).Value = CM_ContactPerson.Text.ToString();
                com.Parameters.Add("@CM_PersonDesign", SqlDbType.VarChar).Value = CM_PersonDesign.Text.ToString();
                com.Parameters.Add("@CM_MobileNo", SqlDbType.VarChar).Value = CM_MobileNo.Text.ToString();
                com.Parameters.Add("@CM_Email", SqlDbType.VarChar).Value = CM_Email.Text.ToString();
                com.Parameters.Add("@CM_AddressOfTheRange", SqlDbType.VarChar).Value = CM_AddressOfTheRange.Text.ToString();
                com.Parameters.Add("@CM_ECC_No", SqlDbType.VarChar).Value = CM_ECC_No.Text.ToString();
                com.Parameters.Add("@CM_TIN_No", SqlDbType.VarChar).Value = CM_TIN_No.Text.ToString();
                com.Parameters.Add("@CM_CSTNo_Date", SqlDbType.VarChar).Value = CM_CSTNo_Date.Text.ToString();
                com.Parameters.Add("@CM_GSTProvisonalID", SqlDbType.VarChar).Value = CM_GSTProvisonalID.Text.ToString();
                com.Parameters.Add("@CM_ED", SqlDbType.VarChar).Value = CM_ED.Text.ToString();
                com.Parameters.Add("@CM_CST", SqlDbType.VarChar).Value = CM_CST.Text.ToString();
                com.Parameters.Add("@CM_VAT", SqlDbType.VarChar).Value = CM_VAT.Text.ToString();
                com.Parameters.Add("@CM_GST", SqlDbType.VarChar).Value = CM_GST.Text.ToString();
                com.Parameters.Add("@CM_MoldAmortisationCOst", SqlDbType.Decimal).Value = "0";
                com.Parameters.Add("@CM_Remarks", SqlDbType.VarChar).Value = CM_Remarks.Text.ToString();
                com.Parameters.Add("@CM_User", SqlDbType.VarChar).Value = dbFunctions.username;

                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                //display();
                Pending();
                Radio_Active.Checked = true;
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        public void Update()
        {
            if (rbtnPending.Checked == true)
            {

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "pr_Update_Customer_Master";
                    com.Parameters.Add("@CM_ID", SqlDbType.VarChar).Value = ID;
                    com.Parameters.Add("@CM_Code", SqlDbType.VarChar).Value = CM_Code.Text.ToString();
                    com.Parameters.Add("@CM_Name", SqlDbType.VarChar).Value = CM_Name.Text.ToString();
                    com.Parameters.Add("@CM_BillingAddress", SqlDbType.VarChar).Value = CM_BillingAddress.Text.ToString();
                    com.Parameters.Add("@CM_ContactPerson", SqlDbType.VarChar).Value = CM_ContactPerson.Text.ToString();
                    com.Parameters.Add("@CM_PersonDesign", SqlDbType.VarChar).Value = CM_PersonDesign.Text.ToString();
                    com.Parameters.Add("@CM_MobileNo", SqlDbType.VarChar).Value = CM_MobileNo.Text.ToString();
                    com.Parameters.Add("@CM_Email", SqlDbType.VarChar).Value = CM_Email.Text.ToString();
                    com.Parameters.Add("@CM_AddressOfTheRange", SqlDbType.VarChar).Value = CM_AddressOfTheRange.Text.ToString();
                    com.Parameters.Add("@CM_ECC_No", SqlDbType.VarChar).Value = CM_ECC_No.Text.ToString();
                    com.Parameters.Add("@CM_TIN_No", SqlDbType.VarChar).Value = CM_TIN_No.Text.ToString();
                    com.Parameters.Add("@CM_CSTNo_Date", SqlDbType.VarChar).Value = CM_CSTNo_Date.Text.ToString();
                    com.Parameters.Add("@CM_GSTProvisonalID", SqlDbType.VarChar).Value = CM_GSTProvisonalID.Text.ToString();
                    com.Parameters.Add("@CM_ED", SqlDbType.VarChar).Value = CM_ED.Text.ToString();
                    com.Parameters.Add("@CM_CST", SqlDbType.VarChar).Value = CM_CST.Text.ToString();
                    com.Parameters.Add("@CM_VAT", SqlDbType.VarChar).Value = CM_VAT.Text.ToString();
                    com.Parameters.Add("@CM_GST", SqlDbType.VarChar).Value = CM_GST.Text.ToString();
                    com.Parameters.Add("@CM_MoldAmortisationCOst", SqlDbType.Decimal).Value = MA_Cost.Text.ToString();
                    com.Parameters.Add("@CM_Remarks", SqlDbType.VarChar).Value = CM_Remarks.Text.ToString();
                    com.Parameters.Add("@CM_User", SqlDbType.VarChar).Value = dbFunctions.username;

                    com.ExecuteNonQuery();
                    com.Connection.Close();
                    MessageBox.Show("Details Updated Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    //display();
                    Pending();
                }
                catch (Exception Ex)
                {
                    dbFunctions.Logs(Ex.Message, dbFunctions.username);
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_UpdateDuplicate_Customer_Master";

                com.Parameters.Add("@CM_Code", SqlDbType.VarChar).Value = CM_Code.Text.ToString();
                com.Parameters.Add("@CM_Name", SqlDbType.VarChar).Value = CM_Name.Text.ToString();
                com.Parameters.Add("@CM_BillingAddress", SqlDbType.VarChar).Value = CM_BillingAddress.Text.ToString();
                com.Parameters.Add("@CM_ContactPerson", SqlDbType.VarChar).Value = CM_ContactPerson.Text.ToString();
                com.Parameters.Add("@CM_PersonDesign", SqlDbType.VarChar).Value = CM_PersonDesign.Text.ToString();
                com.Parameters.Add("@CM_MobileNo", SqlDbType.VarChar).Value = CM_MobileNo.Text.ToString();
                com.Parameters.Add("@CM_Email", SqlDbType.VarChar).Value = CM_Email.Text.ToString();
                com.Parameters.Add("@CM_AddressOfTheRange", SqlDbType.VarChar).Value = CM_AddressOfTheRange.Text.ToString();
                com.Parameters.Add("@CM_ECC_No", SqlDbType.VarChar).Value = CM_ECC_No.Text.ToString();
                com.Parameters.Add("@CM_TIN_No", SqlDbType.VarChar).Value = CM_TIN_No.Text.ToString();
                com.Parameters.Add("@CM_CSTNo_Date", SqlDbType.VarChar).Value = CM_CSTNo_Date.Text.ToString();
                com.Parameters.Add("@CM_GSTProvisonalID", SqlDbType.VarChar).Value = CM_GSTProvisonalID.Text.ToString();
                com.Parameters.Add("@CM_ED", SqlDbType.VarChar).Value = CM_ED.Text.ToString();
                com.Parameters.Add("@CM_CST", SqlDbType.VarChar).Value = CM_CST.Text.ToString();
                com.Parameters.Add("@CM_VAT", SqlDbType.VarChar).Value = CM_VAT.Text.ToString();
                com.Parameters.Add("@CM_GST", SqlDbType.VarChar).Value = CM_GST.Text.ToString();
                com.Parameters.Add("@CM_MoldAmortisationCOst", SqlDbType.Decimal).Value = MA_Cost.Text.ToString();
                com.Parameters.Add("@CM_Remarks", SqlDbType.VarChar).Value = CM_Remarks.Text.ToString();
                com.Parameters.Add("@CM_User", SqlDbType.VarChar).Value = dbFunctions.username;
                com.Parameters.Add("@CM_UpdatedID", SqlDbType.VarChar).Value = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                com.Parameters.Add("@CM_OperationType", SqlDbType.VarChar).Value = "Updated";

                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                //display();
                Pending();
                
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Radio_Active.Checked = true;

            }
       
         
        }


        public void Edit()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("pr_Edit_Customer_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["CM_ID"].ToString();
                CM_Code.Text = dt.Rows[0]["CM_Code"].ToString();
                CM_Name.Text = dt.Rows[0]["CM_Name"].ToString();
                CM_BillingAddress.Text = dt.Rows[0]["CM_BillingAddress"].ToString();
                CM_ContactPerson.Text = dt.Rows[0]["CM_ContactPerson"].ToString();
                CM_PersonDesign.Text = dt.Rows[0]["CM_PersonDesign"].ToString();
                CM_MobileNo.Text = dt.Rows[0]["CM_MobileNo"].ToString();
                CM_Email.Text = dt.Rows[0]["CM_Email"].ToString();
                CM_AddressOfTheRange.Text = dt.Rows[0]["CM_AddressOfTheRange"].ToString();
                CM_ECC_No.Text = dt.Rows[0]["CM_ECC_No"].ToString();
                CM_TIN_No.Text = dt.Rows[0]["CM_TIN_No"].ToString();
                CM_CSTNo_Date.Text = dt.Rows[0]["CM_CSTNo_Date"].ToString();
                CM_GSTProvisonalID.Text = dt.Rows[0]["CM_GSTProvisonalID"].ToString();
                CM_ED.Text = dt.Rows[0]["CM_ED"].ToString();
                CM_CST.Text = dt.Rows[0]["CM_CST"].ToString();
                CM_VAT.Text = dt.Rows[0]["CM_VAT"].ToString();
                CM_GST.Text = dt.Rows[0]["CM_GST"].ToString();
                MA_Cost.Text = dt.Rows[0]["CM_MoldAmortisationCOst"].ToString();
                CM_Remarks.Text = dt.Rows[0]["CM_Remarks"].ToString();
             
                
                btnsave.Text = "&Update";
                CM_Code.Focus();
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
                if (rbtnPending.Checked == true)
                {
                   
                        DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            DataTable dt = dbFunctions.getTable("pr_Delete_Customer_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                            MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // display();
                            Pending();
                            Clear();
                        }

                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            DataTable dt = dbFunctions.getTable("pr_DeleteApproval_Customer_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                            MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //display();
                            Pending();
                            Clear();
                        }

                    }
                }

            Radio_Active.Checked = true;
        
        }


        public void display()
        {
            DataTable dt = dbFunctions.getTable("pr_Display_Customer_Master");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);

            
            //dataGridView1.Columns[0].Frozen = true;
            //dataGridView1.Columns[1].Frozen = true;
            //dataGridView1.Columns[2].Frozen = true;
            //dataGridView1.Columns[3].Frozen = true;




            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();

        }


        public void Clear()
        {
            
            CM_Code.Text = "";
            CM_Name.Text = "";
            CM_Remarks.Text = "";
            CM_BillingAddress.Text = "";
            CM_AddressOfTheRange.Text = "";
            CM_CSTNo_Date.Text = "";
            CM_GSTProvisonalID.Text = "";
            CM_ED.Text = "";
            CM_ContactPerson.Text = "";
            CM_PersonDesign.Text = "";
            CM_MobileNo.Text = "";
            CM_Email.Text = "";
            CM_CST.Text = "";
            CM_VAT.Text = "0";
            CM_ECC_No.Text = "";
            CM_TIN_No.Text = "";
            MA_Cost.Text = "0";
            CM_GST.Text = "0";
           
            
            btnsave.Text = "&Save    ";
            CM_Code.Focus();
            rbtnPending.Checked = true;
            LoadCustomerCode();
        }


        public bool Validate()
        {
            //if ((string.IsNullOrEmpty(CB_ERPCode.Text.Trim())))
            //{
            //    ErrorMessage = "ERPCode Should Not be Empty";
            //    CB_ERPCode.Focus();
            //    return true;
            //}
            if ((string.IsNullOrEmpty(CM_Code.Text.Trim())))
            {
                ErrorMessage = "Code Should Not be Empty";
                CM_Code.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(CM_Name.Text.Trim())))
            {
                ErrorMessage = "Name Should Not be Empty";
                CM_Name.Focus();
                return true;
            }
            //if ((string.IsNullOrEmpty(CM_Remarks.Text.Trim())))
            //{
            //    ErrorMessage = "BillingName Should Not be Empty";
            //    CM_Remarks.Focus();
            //    return true;
            //}

            //if ((string.IsNullOrEmpty(CB_Type.Text.Trim())))
            //{
            //    ErrorMessage = "Type  Should Not be Empty";
            //    CB_Type.Focus();
            //    return true;
            //}

            
            return false;
        }


      
      

        private void CB_ERPCode_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter ERP Code Here.....";
        }

        private void CB_Code_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Code Here.....";
        }

        private void CB_Name_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Name Here.....";
        }

        private void CB_BillingName_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Billing Name Here.....";
        }

        private void CB_ContactPerson_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Contact Person Here.....";
        }

        private void CB_ContactNo1_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Contact No1 Here.....";
        }

        private void CB_ContactNo2_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Contact No1 Here.....";
        }

        private void CB_EmailID_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter E-Mail ID Here.....";
        }

        private void CB_Address1_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Address1 Here.....";
        }

        private void CB_Address2_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Address2 Here.....";
        }

        private void CB_City_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter City Here.....";
        }

        private void CB_Country_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Country Here.....";
        }

        private void CB_PinCode_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter PinCode Here.....";
        }

        private void CB_WebSite_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Website Here.....";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Pending();
        }

        public void Pending()
        {
            DataTable dt = dbFunctions.getTable("pr_DisplayWaiting_Customer_Master");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }

        private void Radio_Active_CheckedChanged(object sender, EventArgs e)
        {
            Approved();
        }

        public void Approved()
        {
            DataTable dt = dbFunctions.getTable("pr_Display_Customer_Master_Approved");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Name] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }

        private void Contributor_Master_KeyDown(object sender, KeyEventArgs e)
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

        private void CB_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CB_ContactNo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar,(TextBox)sender);
        }

        private void CB_ContactNo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void CB_PinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar,(TextBox)sender);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {

                this.Close();

            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CM_VAT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void CM_GST_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void CM_MoldAmortisationCOst_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void CM_ECC_No_TextChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = dbFunctions.getTable("select * from State_Details where SD_State_Code='" + CM_ECC_No.Text+ "'");
                if (dt.Rows.Count > 0)
                {
                    CM_TIN_No.Text = dt.Rows[0]["SD_State_Name"].ToString();

                }

            }
            catch { }
        }

       

   }
}