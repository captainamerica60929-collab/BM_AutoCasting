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

    public partial class Supplier_Master : Form
    {
        public string arrow = "Up";
        public int Distance = 333;
        public string ID = "";
        string ErrorMessage = "";
        public string SupType = "";
        public string Department = "";
        public Supplier_Master()
        {
            InitializeComponent();


            dbFunctions.DGVStyle(dataGridView1);
        }




        private void ItemMaster_Shown(object sender, EventArgs e)
        {
            //display();
            //Pending();
            Radio_Active.Checked = true;
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
            LoadCode();
            SM_Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            SM_Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection strcoll = new AutoCompleteStringCollection();

            DataTable dtcode = dbFunctions.getTable("select SM_Name from Supplier_Master where SM_Status='A'");
            for (int i = 0; i < dtcode.Rows.Count; i++)
            {
                strcoll.Add(dtcode.Rows[i]["SM_Name"].ToString());
            }
            SM_Name.AutoCompleteCustomSource = strcoll;
            
            
            
            Radio_Active.Checked = true;
            SupType = "Manufacturer";
            Department = "Development";
            rbtMFR.Checked = true;
            pur.Checked = true;

            SM_ECCNo.Text = "Approved";
            //display();
            Pending();
            SM_Code.Focus();
            //dbFunctions.status = "F1 -Help, F2 -Save, F3 -Edit, F4 -Update,  F5 -Refresh, F6 -Delete, F7 -Clear";
        }

        public void LoadCode()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_GenerateSupplierNo");
                SM_Code.Text=dt.Rows[0][0].ToString();

            }
            catch
            {
            }
        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (Radio_Active.Checked == true)
            {

                Approved();
            }
            else
            {
                Pending();
            }
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
                com.CommandText = "pr_Insert_Supplier_Master";

                com.Parameters.Add("@SM_Code", SqlDbType.VarChar).Value = SM_Code.Text.ToString();
                com.Parameters.Add("@SM_Name", SqlDbType.VarChar).Value = SM_Name.Text.ToString();
                com.Parameters.Add("@SM_PlantAddr", SqlDbType.VarChar).Value = SM_PlantAddr.Text.ToString();
                com.Parameters.Add("@SM_MobileNo", SqlDbType.VarChar).Value = SM_MobileNo.Text.ToString();
                com.Parameters.Add("@SM_Type", SqlDbType.VarChar).Value = SupType;
                com.Parameters.Add("@SM_MFRDetails", SqlDbType.VarChar).Value = SM_MFRDetails.Text.ToString();
                com.Parameters.Add("@SM_SupType", SqlDbType.VarChar).Value = SM_SupType.Text.ToString();
                com.Parameters.Add("@SM_Certification", SqlDbType.VarChar).Value = SM_Certification.Text.ToString();
                com.Parameters.Add("@SM_Department", SqlDbType.VarChar).Value = Department;

                com.Parameters.Add("@SM_ContactPerson", SqlDbType.VarChar).Value = SM_ContactPerson.Text.ToString();
                com.Parameters.Add("@SM_Designation", SqlDbType.VarChar).Value = SM_Designation.Text.ToString();
                com.Parameters.Add("@SM_Mobile", SqlDbType.VarChar).Value = SM_Mobile.Text.ToString();
                com.Parameters.Add("@SM_Email", SqlDbType.VarChar).Value = SM_Email.Text.ToString();

                com.Parameters.Add("@SM_ContactPerson1", SqlDbType.VarChar).Value = SM_ContactPerson1.Text.ToString();
                com.Parameters.Add("@SM_Designation1", SqlDbType.VarChar).Value = SM_Designation1.Text.ToString();
                com.Parameters.Add("@SM_Mobile1", SqlDbType.VarChar).Value = SM_Mobile1.Text.ToString();
                com.Parameters.Add("@SM_Email1", SqlDbType.VarChar).Value = SM_Email1.Text.ToString();

                com.Parameters.Add("@SM_ContactPerson2", SqlDbType.VarChar).Value = SM_ContactPerson2.Text.ToString();
                com.Parameters.Add("@SM_Designation2", SqlDbType.VarChar).Value = SM_Designation2.Text.ToString();
                com.Parameters.Add("@SM_Mobile2", SqlDbType.VarChar).Value = SM_Mobile2.Text.ToString();
                com.Parameters.Add("@SM_Email2", SqlDbType.VarChar).Value = SM_Email2.Text.ToString();

                com.Parameters.Add("@SM_RangeAddr", SqlDbType.VarChar).Value = SM_RangeAddr.Text.ToString();
                com.Parameters.Add("@SM_ECCNo", SqlDbType.VarChar).Value = SM_ECCNo.Text.ToString();
                com.Parameters.Add("@SM_TINNo", SqlDbType.VarChar).Value = SM_TINNo.Text.ToString();
                com.Parameters.Add("@SM_CSTNo", SqlDbType.VarChar).Value = SM_CSTNo.Text.ToString();
                com.Parameters.Add("@SM_GSTProvisinalID", SqlDbType.VarChar).Value = SM_GSTProvisinalID.Text.ToString();
                com.Parameters.Add("@SM_ED", SqlDbType.VarChar).Value = SM_ED.Text.ToString();
                com.Parameters.Add("@SM_CST", SqlDbType.VarChar).Value = SM_CST.Text.ToString();
                com.Parameters.Add("@SM_VAT", SqlDbType.VarChar).Value = SM_VAT.Text.ToString();
                com.Parameters.Add("@SM_GST", SqlDbType.VarChar).Value = SM_GST.Text.ToString();
                com.Parameters.Add("@SM_RMDetails", SqlDbType.VarChar).Value = SM_RMDetails.Text.ToString();
                com.Parameters.Add("@SM_Remarks", SqlDbType.VarChar).Value = SM_Remarks.Text.ToString();
                com.Parameters.Add("@SM_User", SqlDbType.VarChar).Value = dbFunctions.username;

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
            //dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() != "Approved" && 
            if (rbtnPending.Checked==true)
            {
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "pr_Update_Supplier_Master";
                    com.Parameters.Add("@SM_ID", SqlDbType.VarChar).Value = ID;

                    com.Parameters.Add("@SM_Code", SqlDbType.VarChar).Value = SM_Code.Text.ToString();
                    com.Parameters.Add("@SM_Name", SqlDbType.VarChar).Value = SM_Name.Text.ToString();
                    com.Parameters.Add("@SM_PlantAddr", SqlDbType.VarChar).Value = SM_PlantAddr.Text.ToString();
                    com.Parameters.Add("@SM_MobileNo", SqlDbType.VarChar).Value = SM_MobileNo.Text.ToString();
                    com.Parameters.Add("@SM_Type", SqlDbType.VarChar).Value = SupType;
                    com.Parameters.Add("@SM_MFRDetails", SqlDbType.VarChar).Value = SM_MFRDetails.Text.ToString();
                    com.Parameters.Add("@SM_SupType", SqlDbType.VarChar).Value = SM_SupType.Text.ToString();
                    com.Parameters.Add("@SM_Certification", SqlDbType.VarChar).Value = SM_Certification.Text.ToString();
                    com.Parameters.Add("@SM_Department", SqlDbType.VarChar).Value = Department;


                    com.Parameters.Add("@SM_ContactPerson", SqlDbType.VarChar).Value = SM_ContactPerson.Text.ToString();
                    com.Parameters.Add("@SM_Designation", SqlDbType.VarChar).Value = SM_Designation.Text.ToString();
                    com.Parameters.Add("@SM_Mobile", SqlDbType.VarChar).Value = SM_Mobile.Text.ToString();
                    com.Parameters.Add("@SM_Email", SqlDbType.VarChar).Value = SM_Email.Text.ToString();

                    com.Parameters.Add("@SM_ContactPerson1", SqlDbType.VarChar).Value = SM_ContactPerson1.Text.ToString();
                    com.Parameters.Add("@SM_Designation1", SqlDbType.VarChar).Value = SM_Designation1.Text.ToString();
                    com.Parameters.Add("@SM_Mobile1", SqlDbType.VarChar).Value = SM_Mobile1.Text.ToString();
                    com.Parameters.Add("@SM_Email1", SqlDbType.VarChar).Value = SM_Email1.Text.ToString();

                    com.Parameters.Add("@SM_ContactPerson2", SqlDbType.VarChar).Value = SM_ContactPerson2.Text.ToString();
                    com.Parameters.Add("@SM_Designation2", SqlDbType.VarChar).Value = SM_Designation2.Text.ToString();
                    com.Parameters.Add("@SM_Mobile2", SqlDbType.VarChar).Value = SM_Mobile2.Text.ToString();
                    com.Parameters.Add("@SM_Email2", SqlDbType.VarChar).Value = SM_Email2.Text.ToString();

                    com.Parameters.Add("@SM_RangeAddr", SqlDbType.VarChar).Value = SM_RangeAddr.Text.ToString();
                    com.Parameters.Add("@SM_ECCNo", SqlDbType.VarChar).Value = SM_ECCNo.Text.ToString();
                    com.Parameters.Add("@SM_TINNo", SqlDbType.VarChar).Value = SM_TINNo.Text.ToString();
                    com.Parameters.Add("@SM_CSTNo", SqlDbType.VarChar).Value = SM_CSTNo.Text.ToString();
                    com.Parameters.Add("@SM_GSTProvisinalID", SqlDbType.VarChar).Value = SM_GSTProvisinalID.Text.ToString();
                    com.Parameters.Add("@SM_ED", SqlDbType.VarChar).Value = SM_ED.Text.ToString();
                    com.Parameters.Add("@SM_CST", SqlDbType.VarChar).Value = SM_CST.Text.ToString();
                    com.Parameters.Add("@SM_VAT", SqlDbType.VarChar).Value = SM_VAT.Text.ToString();
                    com.Parameters.Add("@SM_GST", SqlDbType.VarChar).Value = SM_GST.Text.ToString();
                    com.Parameters.Add("@SM_RMDetails", SqlDbType.VarChar).Value = SM_RMDetails.Text.ToString();
                    com.Parameters.Add("@SM_Remarks", SqlDbType.VarChar).Value = SM_Remarks.Text.ToString();
                    com.Parameters.Add("@SM_User", SqlDbType.VarChar).Value = dbFunctions.username;

                    com.ExecuteNonQuery();
                    com.Connection.Close();
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
            else 
            {
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "pr_Updation_Supplier_Master";

                    com.Parameters.Add("@SM_Code", SqlDbType.VarChar).Value = SM_Code.Text.ToString();
                    com.Parameters.Add("@SM_Name", SqlDbType.VarChar).Value = SM_Name.Text.ToString();
                    com.Parameters.Add("@SM_PlantAddr", SqlDbType.VarChar).Value = SM_PlantAddr.Text.ToString();
                    com.Parameters.Add("@SM_MobileNo", SqlDbType.VarChar).Value = SM_MobileNo.Text.ToString();
                    com.Parameters.Add("@SM_Type", SqlDbType.VarChar).Value = SupType;
                    com.Parameters.Add("@SM_MFRDetails", SqlDbType.VarChar).Value = SM_MFRDetails.Text.ToString();
                    com.Parameters.Add("@SM_SupType", SqlDbType.VarChar).Value = SM_SupType.Text.ToString();
                    com.Parameters.Add("@SM_Certification", SqlDbType.VarChar).Value = SM_Certification.Text.ToString();
                    com.Parameters.Add("@SM_Department", SqlDbType.VarChar).Value = Department;

                    com.Parameters.Add("@SM_ContactPerson", SqlDbType.VarChar).Value = SM_ContactPerson.Text.ToString();
                    com.Parameters.Add("@SM_Designation", SqlDbType.VarChar).Value = SM_Designation.Text.ToString();
                    com.Parameters.Add("@SM_Mobile", SqlDbType.VarChar).Value = SM_Mobile.Text.ToString();
                    com.Parameters.Add("@SM_Email", SqlDbType.VarChar).Value = SM_Email.Text.ToString();

                    com.Parameters.Add("@SM_ContactPerson1", SqlDbType.VarChar).Value = SM_ContactPerson1.Text.ToString();
                    com.Parameters.Add("@SM_Designation1", SqlDbType.VarChar).Value = SM_Designation1.Text.ToString();
                    com.Parameters.Add("@SM_Mobile1", SqlDbType.VarChar).Value = SM_Mobile1.Text.ToString();
                    com.Parameters.Add("@SM_Email1", SqlDbType.VarChar).Value = SM_Email1.Text.ToString();

                    com.Parameters.Add("@SM_ContactPerson2", SqlDbType.VarChar).Value = SM_ContactPerson2.Text.ToString();
                    com.Parameters.Add("@SM_Designation2", SqlDbType.VarChar).Value = SM_Designation2.Text.ToString();
                    com.Parameters.Add("@SM_Mobile2", SqlDbType.VarChar).Value = SM_Mobile2.Text.ToString();
                    com.Parameters.Add("@SM_Email2", SqlDbType.VarChar).Value = SM_Email2.Text.ToString();

                    com.Parameters.Add("@SM_RangeAddr", SqlDbType.VarChar).Value = SM_RangeAddr.Text.ToString();
                    com.Parameters.Add("@SM_ECCNo", SqlDbType.VarChar).Value = SM_ECCNo.Text.ToString();
                    com.Parameters.Add("@SM_TINNo", SqlDbType.VarChar).Value = SM_TINNo.Text.ToString();
                    com.Parameters.Add("@SM_CSTNo", SqlDbType.VarChar).Value = SM_CSTNo.Text.ToString();
                    com.Parameters.Add("@SM_GSTProvisinalID", SqlDbType.VarChar).Value = SM_GSTProvisinalID.Text.ToString();
                    com.Parameters.Add("@SM_ED", SqlDbType.VarChar).Value = SM_ED.Text.ToString();
                    com.Parameters.Add("@SM_CST", SqlDbType.VarChar).Value = SM_CST.Text.ToString();
                    com.Parameters.Add("@SM_VAT", SqlDbType.VarChar).Value = SM_VAT.Text.ToString();
                    com.Parameters.Add("@SM_GST", SqlDbType.VarChar).Value = SM_GST.Text.ToString();
                    com.Parameters.Add("@SM_RMDetails", SqlDbType.VarChar).Value = SM_RMDetails.Text.ToString();
                    com.Parameters.Add("@SM_Remarks", SqlDbType.VarChar).Value = SM_Remarks.Text.ToString();
                    com.Parameters.Add("@SM_UpdatedID", SqlDbType.VarChar).Value = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    com.Parameters.Add("@SM_OperationType", SqlDbType.VarChar).Value = "Updated";
                    com.Parameters.Add("@SM_User", SqlDbType.VarChar).Value = dbFunctions.username;

                    com.ExecuteNonQuery();
                    com.Connection.Close();
                    MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    //display();
                    Pending();
                    //rbtnPending.Checked = true;
                }
                catch (Exception Ex)
                {
                    dbFunctions.Logs(Ex.Message, dbFunctions.username);
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }
            Radio_Active.Checked = true;
      

            }
       
        public void Edit()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("pr_Edit_Supplier_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["SM_ID"].ToString();
                SM_Code.Text=dt.Rows[0]["SM_Code"].ToString();
                SM_Name.Text=dt.Rows[0]["SM_Name"].ToString();
                SM_PlantAddr.Text=dt.Rows[0]["SM_PlantAddr"].ToString();
                SM_MobileNo.Text = dt.Rows[0]["SM_MobileNo"].ToString();
                SupType = dt.Rows[0]["SM_Type"].ToString();
                if (SupType == "Manufacturer")
                {
                    rbtMFR.Checked = true;

                }
                else
                {
                    rbtDLR.Checked = true;

                }
              

                SM_MFRDetails.Text=dt.Rows[0]["SM_MFRDetails"].ToString();
                SM_SupType.Text=dt.Rows[0]["SM_SupType"].ToString();
                SM_Certification.Text=dt.Rows[0]["SM_Certification"].ToString();
                Department = dt.Rows[0]["SM_Department"].ToString();
                if (Department == "Development")
                {
                    dev.Checked = true;
                }
                else if (Department == "Purchase")
                {
                    pur.Checked = true;
                }
                else
                {
                    qua.Checked = true;
                }

                SM_ContactPerson.Text=dt.Rows[0]["SM_ContactPerson"].ToString();
                SM_Designation.Text=dt.Rows[0]["SM_Designation"].ToString();
                SM_Mobile.Text=dt.Rows[0]["SM_Mobile"].ToString();
                SM_Email.Text=dt.Rows[0]["SM_Email"].ToString();
                SM_ContactPerson1.Text = dt.Rows[0]["SM_ContactPerson1"].ToString();
                SM_Designation1.Text = dt.Rows[0]["SM_Designation1"].ToString();
                SM_Mobile1.Text = dt.Rows[0]["SM_Mobile1"].ToString();
                SM_Email1.Text = dt.Rows[0]["SM_Email1"].ToString();
                SM_ContactPerson2.Text = dt.Rows[0]["SM_ContactPerson2"].ToString();
                SM_Designation2.Text = dt.Rows[0]["SM_Designation2"].ToString();
                SM_Mobile2.Text = dt.Rows[0]["SM_Mobile2"].ToString();
                SM_Email2.Text = dt.Rows[0]["SM_Email2"].ToString();
                SM_RangeAddr.Text=dt.Rows[0]["SM_RangeAddr"].ToString();
                SM_ECCNo.Text=dt.Rows[0]["SM_ECCNo"].ToString();
                SM_TINNo.Text=dt.Rows[0]["SM_TINNo"].ToString();
                SM_CSTNo.Text=dt.Rows[0]["SM_CSTNo"].ToString();
                SM_GSTProvisinalID.Text=dt.Rows[0]["SM_GSTProvisinalID"].ToString();
                SM_ED.Text=dt.Rows[0]["SM_ED"].ToString();
                SM_CST.Text=dt.Rows[0]["SM_CST"].ToString();
                SM_VAT.Text=dt.Rows[0]["SM_VAT"].ToString();
                SM_GST.Text=dt.Rows[0]["SM_GST"].ToString();
                SM_RMDetails.Text=dt.Rows[0]["SM_RMDetails"].ToString();
                SM_Remarks.Text=dt.Rows[0]["SM_Remarks"].ToString();
             
                
                btnsave.Text = "&Update";
                SM_Code.Focus();
                //rbtnPending.Checked = true;
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
                            DataTable dt = dbFunctions.getTable("pr_Delete_Supplier_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
                            DataTable dt = dbFunctions.getTable("pr_DeleteApproval_Supplier_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("pr_Display_Supplier_Master");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);


            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[2].Frozen = true;
            //dataGridView1.Columns[3].Frozen = true;




            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();

        }


        public void Clear()
        {
            SM_MFRDetails.Text = "";
            SM_SupType.SelectedIndex = -1;
            SM_Certification.Text = "";
            SM_Code.Text = "";
            SM_Name.Text = "";
            SM_Remarks.Text = "";
            SM_PlantAddr.Text = "";
            SM_RangeAddr.Text = "";
            SM_MobileNo.Text = "";
            SM_CSTNo.Text = "";
            SM_GSTProvisinalID.Text = "";
            SM_ED.Text = "0";
            SM_ContactPerson.Text = "";
            SM_Designation.Text = "";
            SM_Mobile.Text = "";
            SM_Email.Text = "";

            SM_ContactPerson1.Text = "";
            SM_Designation1.Text = "";
            SM_Mobile1.Text = "";
            SM_Email1.Text = "";



            SM_ContactPerson2.Text = "";
            SM_Designation2.Text = "";
            SM_Mobile2.Text = "";
            SM_Email2.Text = "";

            SM_CST.Text = "0";
            SM_VAT.Text = "0";
            SM_ECCNo.Text = "";
            SM_TINNo.Text = "";
            SM_RMDetails.Text = "";
            SM_GST.Text = "0";
            rbtMFR.Checked = true;
            
            btnsave.Text = "&Save    ";
            SM_Code.Focus();
            rbtnPending.Checked = true;

            LoadCode();
        }


        public bool Validate()
        {
            //if ((string.IsNullOrEmpty(CB_ERPCode.Text.Trim())))
            //{
            //    ErrorMessage = "ERPCode Should Not be Empty";
            //    CB_ERPCode.Focus();
            //    return true;
            //}
            if ((string.IsNullOrEmpty(SM_Code.Text.Trim())))
            {
                ErrorMessage = "Code Should Not be Empty";
                SM_Code.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(SM_Name.Text.Trim())))
            {
                ErrorMessage = "Name Should Not be Empty";
                SM_Name.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(SM_ED.Text.Trim())))
            {
                ErrorMessage = "ED Should Not be Empty";
                SM_ED.Focus();
                return true;
            }

            if ((string.IsNullOrEmpty(SM_CST.Text.Trim())))
            {
                ErrorMessage = "CST  Should Not be Empty";
                SM_CST.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(SM_VAT.Text.Trim())))
            {
                ErrorMessage = "VAT  Should Not be Empty";
                SM_VAT.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(SM_GST.Text.Trim())))
            {
                ErrorMessage = "GST  Should Not be Empty";
                SM_GST.Focus();
                return true;
            }

            
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
            DataTable dt = dbFunctions.getTable("pr_Display_Supplier_Master_Waiting");
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
            DataTable dt = dbFunctions.getTable("pr_Display_Supplier_Master_Approved '" + SM_ECCNo.Text + "'");
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Code] LIKE '%{0}%' OR [Name] LIKE '%{0}%'", textBoxX1.Text);
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
            char keypress = e.KeyChar;
            if (char.IsDigit(keypress) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                MessageBox.Show("Numbers Only Allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void CB_PinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
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

        private void rbtMFR_CheckedChanged(object sender, EventArgs e)
        {
            SupType = "Manufacturer";
        }

        private void rbtDLR_CheckedChanged(object sender, EventArgs e)
        {
            SupType = "Dealer";
            if(rbtDLR.Checked==true)
            {
                SM_MFRDetails.ReadOnly = false;
            }
            else
            {
                SM_MFRDetails.ReadOnly = true;
            }
        }

        private void dev_CheckedChanged(object sender, EventArgs e)
        {
            Department = "Development";
            //if(dev.Checked==true)
            //{
                Developmentpanel.Visible = true;
                Developmentpanel.Location = new Point(382, 70);
                Purchasepanel.Visible = false;
                Qualitypanel.Visible = false;
            //}
            //else if (pur.Checked == true)
            //{
               
            //}
            //else
            //{

              
            //}
        }

        private void pur_CheckedChanged(object sender, EventArgs e)
        {
            Department = "Purchase";

            Purchasepanel.Visible = true;
            Purchasepanel.Location = new Point(382, 70);
            Developmentpanel.Visible = false;
            Qualitypanel.Visible = false;
        }

        private void qua_CheckedChanged(object sender, EventArgs e)
        {
            Department = "Quality";

            Qualitypanel.Visible = true;
            Qualitypanel.Location = new Point(382, 70);
            Developmentpanel.Visible = false;
            Purchasepanel.Visible = false;
        }

        private void SM_Mobile1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keypress = e.KeyChar;
            if (char.IsDigit(keypress) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                MessageBox.Show("Numbers Only Allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void SM_Mobile2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keypress = e.KeyChar;
            if (char.IsDigit(keypress) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                MessageBox.Show("Numbers Only Allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void SM_ContactPerson1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void SM_Email_TextChanged(object sender, EventArgs e)
        {

        }

        private void CM_MobileNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void SM_Designation_TextChanged(object sender, EventArgs e)
        {

        }

        private void SM_ContactPerson_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void SM_CST_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void SM_VAT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void SM_GST_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void SM_MobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keypress = e.KeyChar;
            if (char.IsDigit(keypress) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                MessageBox.Show("Numbers Only Allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void SM_TINNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void SM_ECCNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Approved();
            }
            catch { }
        }

       

   }
}