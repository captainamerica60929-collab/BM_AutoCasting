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

    public partial class Employee_Master : Form
    {
        public string arrow = "Up";
        public int Distance = 276;
        public string ID = "";
        string ErrorMessage = "";
        public Employee_Master()
        {
            InitializeComponent();
            dbFunctions.DGVStyle(dataGridView1);
        }




        private void ItemMaster_Shown(object sender, EventArgs e)
        {
            display();
            splitContainer1.SplitterDistance = Distance;
            EM_Code.Focus();
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
            Radio_Active.Checked = true;
            display();
            Person.Image = CRM_App.Properties.Resources.Nobody;
            


        }

        //public void Load_BM_BrancName()
        //{
        //    try
        //    {
        //        DataTable dt = dbFunctions.getTable("Pr_Load_BM_BrancName");
        //        EM_Branch.DataSource = dt;
        //        EM_Branch.DisplayMember = "BM_BrancName";
        //        EM_Branch.ValueMember = "BM_ID";
        //        EM_Branch.SelectedIndex = -1;
        //    }
        //    catch { }
        //}



        //public void Load_Des_Designation()
        //{
        //    try
        //    {
        //        DataTable dt = dbFunctions.getTable("Pr_Load_Des_Designation");
        //        EM_Designation.DataSource = dt;
        //        EM_Designation.DisplayMember = "Des_Designation";
        //        EM_Designation.ValueMember = "Des_ID";
        //        EM_Designation.SelectedIndex = -1;
        //    }
        //    catch { }
        //}


        //public void Load_CT_Category()
        //{
        //    try
        //    {
        //        DataTable dt = dbFunctions.getTable("Pr_Load_CT_Category");
        //        EM_Category.DataSource = dt;
        //        EM_Category.DisplayMember = "CT_Category";
        //        EM_Category.ValueMember = "CT_ID";
        //       EM_Category.SelectedIndex = -1;
        //    }
        //    catch { }
        //}


        //public void Load_Dept_Name()
        //{
        //    try
        //    {
        //        DataTable dt = dbFunctions.getTable("Pr_Load_Dept_Name");
        //        EM_Department.DataSource = dt;
        //        EM_Department.DisplayMember = "Dept_Name";
        //        EM_Department.ValueMember = "Dept_ID";
        //        EM_Department.SelectedIndex = -1;
        //    }
        //    catch { }
        //}




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
                com.CommandText = "Pr_Insert_Employee_Master";
                com.Parameters.Add("@EM_Code", SqlDbType.VarChar).Value = EM_Code.Text.ToString();
                com.Parameters.Add("@EM_EmployeeName", SqlDbType.VarChar).Value = EM_EmployeeName.Text.ToString();
                com.Parameters.Add("@EM_FaherName", SqlDbType.VarChar).Value = EM_FaherName.Text.ToString();
                com.Parameters.Add("@EM_Gender", SqlDbType.VarChar).Value = EM_Gender.Text.ToString();
                com.Parameters.Add("@EM_Marital_Status", SqlDbType.VarChar).Value = EM_Marital_Status.Text.ToString();
                com.Parameters.Add("@EM_Department", SqlDbType.VarChar).Value = EM_Department.Text.ToString();
                com.Parameters.Add("@EM_Designation", SqlDbType.VarChar).Value = EM_Designation.Text.ToString();
                com.Parameters.Add("@EM_Category", SqlDbType.VarChar).Value = EM_Category.Text.ToString();
                com.Parameters.Add("@EM_DOB", SqlDbType.VarChar).Value = EM_DOB.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@EM_DOJ", SqlDbType.VarChar).Value = EM_DOJ.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@EM_Branch", SqlDbType.VarChar).Value = EM_Branch.Text.ToString();
                com.Parameters.Add("@EM_Address1", SqlDbType.VarChar).Value = EM_Address1.Text.ToString();
                com.Parameters.Add("@EM_Address2", SqlDbType.VarChar).Value = EM_Address2.Text.ToString();
                com.Parameters.Add("@EM_City", SqlDbType.VarChar).Value = EM_City.Text.ToString();
                com.Parameters.Add("@EM_State", SqlDbType.VarChar).Value = EM_State.Text.ToString();
                com.Parameters.Add("@EM_PinCode", SqlDbType.VarChar).Value = EM_PinCode.Text.ToString();
                com.Parameters.Add("@EM_ContactNo", SqlDbType.VarChar).Value = EM_ContactNo.Text.ToString();
                com.Parameters.Add("@EM_EmailID", SqlDbType.VarChar).Value = EM_EmailID.Text.ToString();
                com.Parameters.Add("@EM_AccoutNumber", SqlDbType.VarChar).Value = EM_AccoutNumber.Text.ToString();
                com.Parameters.Add("@EM_PFNo", SqlDbType.VarChar).Value = EM_PFNo.Text.ToString();
                com.Parameters.Add("@EM_ESINo", SqlDbType.VarChar).Value = EM_ESINo.Text.ToString();

                byte[] imageData2 = null;


                try
                {
                    MemoryStream ms1 = new MemoryStream();
                    Person.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageData2 = ms1.GetBuffer();
                }
                catch
                {


                }

                com.Parameters.Add("@EM_Image", SqlDbType.Image).Value = (object)imageData2;

              
                com.Parameters.Add("@EM_User", SqlDbType.VarChar).Value = dbFunctions.username;
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
                com.CommandText = "Pr_Update_Employee_Master";
                com.Parameters.Add("@EM_iid", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@EM_Code", SqlDbType.VarChar).Value = EM_Code.Text.ToString();
                com.Parameters.Add("@EM_EmployeeName", SqlDbType.VarChar).Value = EM_EmployeeName.Text.ToString();
                com.Parameters.Add("@EM_FaherName", SqlDbType.VarChar).Value = EM_FaherName.Text.ToString();
                com.Parameters.Add("@EM_Gender", SqlDbType.VarChar).Value = EM_Gender.Text.ToString();
                com.Parameters.Add("@EM_Marital_Status", SqlDbType.VarChar).Value = EM_Marital_Status.Text.ToString();
                com.Parameters.Add("@EM_Department", SqlDbType.VarChar).Value = EM_Department.Text.ToString();
                com.Parameters.Add("@EM_Designation", SqlDbType.VarChar).Value = EM_Designation.Text.ToString();
                com.Parameters.Add("@EM_Category", SqlDbType.VarChar).Value = EM_Category.Text.ToString();
                com.Parameters.Add("@EM_DOB", SqlDbType.VarChar).Value = EM_DOB.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@EM_DOJ", SqlDbType.VarChar).Value = EM_DOJ.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@EM_Branch", SqlDbType.VarChar).Value = EM_Branch.Text.ToString();
                com.Parameters.Add("@EM_Address1", SqlDbType.VarChar).Value = EM_Address1.Text.ToString();
                com.Parameters.Add("@EM_Address2", SqlDbType.VarChar).Value = EM_Address2.Text.ToString();
                com.Parameters.Add("@EM_City", SqlDbType.VarChar).Value = EM_City.Text.ToString();
                com.Parameters.Add("@EM_State", SqlDbType.VarChar).Value = EM_State.Text.ToString();
                com.Parameters.Add("@EM_PinCode", SqlDbType.VarChar).Value = EM_PinCode.Text.ToString();
                com.Parameters.Add("@EM_ContactNo", SqlDbType.VarChar).Value = EM_ContactNo.Text.ToString();
                com.Parameters.Add("@EM_EmailID", SqlDbType.VarChar).Value = EM_EmailID.Text.ToString();
                com.Parameters.Add("@EM_AccoutNumber", SqlDbType.VarChar).Value = EM_AccoutNumber.Text.ToString();
                com.Parameters.Add("@EM_PFNo", SqlDbType.VarChar).Value = EM_PFNo.Text.ToString();
                com.Parameters.Add("@EM_ESINo", SqlDbType.VarChar).Value = EM_ESINo.Text.ToString();

                byte[] imageData2 = null;
                try
                {
                    MemoryStream ms1 = new MemoryStream();
                    Person.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageData2 = ms1.GetBuffer();
                }
                catch
                {
                    DataTable DtDetails = new DataTable();
                    DataTable dt = dbFunctions.getTable("Pr_Fetch_Employee_Master_ByID  " + ID);
                    imageData2 = (byte[])dt.Rows[0]["EM_Image"];
                }




                com.Parameters.Add("@EM_Image", SqlDbType.Image).Value = (object)imageData2;

             
                com.Parameters.Add("@EM_User", SqlDbType.VarChar).Value = dbFunctions.username;
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
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Employee_Master_ByID  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["EM_iid"].ToString();
                EM_Code.Text = dt.Rows[0]["EM_Code"].ToString();
                EM_EmployeeName.Text = dt.Rows[0]["EM_EmployeeName"].ToString();
                EM_FaherName.Text = dt.Rows[0]["EM_FaherName"].ToString();
                EM_Gender.Text = dt.Rows[0]["EM_Gender"].ToString();
                EM_Marital_Status.Text = dt.Rows[0]["EM_Marital_Status"].ToString();
                EM_Department.Text = dt.Rows[0]["EM_Department"].ToString();
                EM_Designation.Text = dt.Rows[0]["EM_Designation"].ToString();
                EM_Category.Text = dt.Rows[0]["EM_Category"].ToString();
                EM_DOB.Text = dt.Rows[0]["EM_DOB"].ToString();
                EM_DOJ.Text = dt.Rows[0]["EM_DOJ"].ToString();
                EM_Branch.Text = dt.Rows[0]["EM_Branch"].ToString();
                EM_Address1.Text = dt.Rows[0]["EM_Address1"].ToString();
                EM_Address2.Text = dt.Rows[0]["EM_Address2"].ToString();
                EM_City.Text = dt.Rows[0]["EM_City"].ToString();
                EM_State.Text = dt.Rows[0]["EM_State"].ToString();
                EM_PinCode.Text = dt.Rows[0]["EM_PinCode"].ToString();
                EM_ContactNo.Text = dt.Rows[0]["EM_ContactNo"].ToString();
                EM_EmailID.Text = dt.Rows[0]["EM_EmailID"].ToString();
                EM_AccoutNumber.Text = dt.Rows[0]["EM_AccoutNumber"].ToString();
                EM_PFNo.Text = dt.Rows[0]["EM_PFNo"].ToString();
                EM_ESINo.Text = dt.Rows[0]["EM_ESINo"].ToString();

                Person.Image = null;
                try
                {
                    byte[] imageData = (byte[])dt.Rows[0]["EM_Image"];
                    Image newImage;
                    //Read image data into a memory stream
                    using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                    {
                        ms.Write(imageData, 0, imageData.Length);

                        //Set image variable value using memory stream.
                        newImage = Image.FromStream(ms, true);
                    }

                    //set picture
                    Person.Image = newImage;
                }
                catch (Exception ex) { }
               
           
               
                btnsave.Text = "&Update";
                EM_Code.Focus();
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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_Employee_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Employee_Master_Details");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            //dataGridView1.Columns[0].Frozen = true;
            //dataGridView1.Columns[1].Frozen = true;
            //dataGridView1.Columns[2].Frozen = true;

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
            EM_Code.Text = "";
            EM_EmployeeName.Text = "";
            EM_FaherName.Text = "";
            EM_Gender.SelectedIndex = -1;
            EM_Marital_Status.SelectedIndex = -1;
            EM_Department.Text = "";
            EM_Designation.Text = "";
            EM_Category.Text = "";
            EM_DOB.Text = "";
            EM_DOJ.Text = "";
            EM_Branch.Text = "";
            EM_Address1.Text = "";
            EM_Address2.Text = "";
            EM_City.Text = "";
            EM_State.Text = "";
            EM_PinCode.Text = "";
            EM_ContactNo.Text = "";
            EM_EmailID.Text = "";
            EM_AccoutNumber.Text = "";
            EM_PFNo.Text = "";
            EM_ESINo.Text = "";
            Person.Text = "";
            Person.Image = CRM_App.Properties.Resources.Nobody;
            btnsave.Text = "&Save    ";
            EM_Code.Focus();
        }


        public bool Validate()
        {
            if ((string.IsNullOrEmpty(EM_Code.Text.Trim())))
            {
                ErrorMessage = "Code Should Not be Empty";
                EM_Code.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(EM_EmployeeName.Text.Trim())))
            {
                ErrorMessage = "EmployeeName Should Not be Empty";
                EM_EmployeeName.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(EM_Gender.Text.Trim())))
            {
                ErrorMessage = "Gender Should Not be Empty";
                EM_Gender.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(EM_Department.Text.Trim())))
            {
                ErrorMessage = "Department Should Not be Empty";
                EM_Department.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(EM_Designation.Text.Trim())))
            {
                ErrorMessage = "Designation Should Not be Empty";
                EM_Designation.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(EM_Category.Text.Trim())))
            {
                ErrorMessage = "Category Should Not be Empty";
                EM_Category.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(EM_DOB.Text.Trim())))
            {
                ErrorMessage = "DOB Should Not be Empty";
                EM_DOB.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(EM_DOJ.Text.Trim())))
            {
                ErrorMessage = "DOJ Should Not be Empty";
                EM_DOJ.Focus();
                return true;
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                IM_PartImage.Text = openFileDialog1.FileName.ToString();
                Person.Image = Image.FromFile(IM_PartImage.Text.ToString());

            }
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
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void EM_ContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);

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


   }
}