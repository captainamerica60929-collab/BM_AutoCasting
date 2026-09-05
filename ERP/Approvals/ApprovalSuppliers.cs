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

    public partial class ApprovalSuppliers : Form
    {
        public string arrow = "Up";
        public int Distance = 64;
        public string ID = "";
        string ErrorMessage = "";
        public string SupType = "";
        public string Department = "";
        public ApprovalSuppliers()
        {
            InitializeComponent();


            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
        }




        private void ItemMaster_Shown(object sender, EventArgs e)
        {
            display();
            splitContainer1.SplitterDistance = Distance;


            //DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
            //doWork.HeaderText = "Select";
            //doWork.FalseValue = "0";
            //doWork.TrueValue = "1";
            //dataGridView1.Columns.Insert(0, doWork);
            //dataGridView1.Columns[0].Width = 50;
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
            DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
            doWork.HeaderText = "Select";
            doWork.FalseValue = "0";
            doWork.TrueValue = "1";
            dataGridView1.Columns.Insert(0, doWork);
        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                

                if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) == "1")
                {

                    if (dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Updated")
                    {

                        DataTable dt = dbFunctions.getTable("pr_Edit_Supplier_Master  " + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());

                        string er = dt.Rows[0]["SM_Name"].ToString();

                        SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                        try
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand();
                            com.Connection = con;
                            com.CommandType = CommandType.StoredProcedure;
                            //com.CommandText = "pr_Updation_Supplier_Master";
                            com.CommandText = "pr_Update_Supplier_Master";

                            com.Parameters.Add("@SM_ID", SqlDbType.VarChar).Value = dt.Rows[0]["SM_UpdatedID"].ToString();
                            com.Parameters.Add("@SM_Code", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Code"].ToString();
                            com.Parameters.Add("@SM_Name", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Name"].ToString();
                            com.Parameters.Add("@SM_PlantAddr", SqlDbType.VarChar).Value = dt.Rows[0]["SM_PlantAddr"].ToString();
                            com.Parameters.Add("@SM_MobileNo", SqlDbType.VarChar).Value = dt.Rows[0]["SM_MobileNo"].ToString();
                            com.Parameters.Add("@SM_Type", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Type"].ToString();
                            com.Parameters.Add("@SM_MFRDetails", SqlDbType.VarChar).Value = dt.Rows[0]["SM_MFRDetails"].ToString();
                            com.Parameters.Add("@SM_SupType", SqlDbType.VarChar).Value = dt.Rows[0]["SM_SupType"].ToString();
                            com.Parameters.Add("@SM_Certification", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Certification"].ToString();
                            com.Parameters.Add("@SM_Department", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Department"].ToString();

                            com.Parameters.Add("@SM_ContactPerson", SqlDbType.VarChar).Value = dt.Rows[0]["SM_ContactPerson"].ToString();
                            com.Parameters.Add("@SM_Designation", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Designation"].ToString();
                            com.Parameters.Add("@SM_Mobile", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Mobile"].ToString();
                            com.Parameters.Add("@SM_Email", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Email"].ToString();

                            com.Parameters.Add("@SM_ContactPerson1", SqlDbType.VarChar).Value = dt.Rows[0]["SM_ContactPerson1"].ToString();
                            com.Parameters.Add("@SM_Designation1", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Designation1"].ToString();
                            com.Parameters.Add("@SM_Mobile1", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Mobile1"].ToString();
                            com.Parameters.Add("@SM_Email1", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Email1"].ToString();

                            com.Parameters.Add("@SM_ContactPerson2", SqlDbType.VarChar).Value = dt.Rows[0]["SM_ContactPerson2"].ToString();
                            com.Parameters.Add("@SM_Designation2", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Designation2"].ToString();
                            com.Parameters.Add("@SM_Mobile2", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Mobile2"].ToString();
                            com.Parameters.Add("@SM_Email2", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Email2"].ToString();

                            com.Parameters.Add("@SM_RangeAddr", SqlDbType.VarChar).Value = dt.Rows[0]["SM_RangeAddr"].ToString();
                            com.Parameters.Add("@SM_ECCNo", SqlDbType.VarChar).Value = dt.Rows[0]["SM_ECCNo"].ToString();
                            com.Parameters.Add("@SM_TINNo", SqlDbType.VarChar).Value = dt.Rows[0]["SM_TINNo"].ToString();
                            com.Parameters.Add("@SM_CSTNo", SqlDbType.VarChar).Value = dt.Rows[0]["SM_CSTNo"].ToString();
                            com.Parameters.Add("@SM_GSTProvisinalID", SqlDbType.VarChar).Value = dt.Rows[0]["SM_GSTProvisinalID"].ToString();
                            com.Parameters.Add("@SM_ED", SqlDbType.VarChar).Value = dt.Rows[0]["SM_ED"].ToString();
                            com.Parameters.Add("@SM_CST", SqlDbType.VarChar).Value = dt.Rows[0]["SM_CST"].ToString();
                            com.Parameters.Add("@SM_VAT", SqlDbType.VarChar).Value = dt.Rows[0]["SM_VAT"].ToString();
                            com.Parameters.Add("@SM_GST", SqlDbType.VarChar).Value = dt.Rows[0]["SM_GST"].ToString();
                            com.Parameters.Add("@SM_RMDetails", SqlDbType.VarChar).Value = dt.Rows[0]["SM_RMDetails"].ToString();
                            com.Parameters.Add("@SM_Remarks", SqlDbType.VarChar).Value = dt.Rows[0]["SM_Remarks"].ToString();
                            com.Parameters.Add("@SM_User", SqlDbType.VarChar).Value = dbFunctions.username;
                            //com.Parameters.Add("@SM_Status", SqlDbType.VarChar).Value = "D";

                            com.ExecuteNonQuery();
                            com.Connection.Close();
                            
                            UpdateSelected();
                            MessageBox.Show("Updated Sucessfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(er  +"Already Exists");
                        }
                        
                    }
                    else if (dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Waiting For Delete")
                    {
                        DataTable dt = dbFunctions.getTable("pr_Delete_Supplier_Master " + dataGridView1.Rows[i].Cells["ID"].Value.ToString());
                        MessageBox.Show("Deleted Sucessfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //insert();
                        DataTable dt = dbFunctions.getTable("pr_Approve_Supplier_Master '" + dataGridView1.Rows[i].Cells["ID"].Value.ToString() + "','" + dbFunctions.username + "'");
                        MessageBox.Show("Approved Sucessfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                
            }
            display();
        }

        private void UpdateSelected()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Delete_Updated_Supplier " + dataGridView1.SelectedRows[0].Cells["SM_UpdatedID"].Value.ToString());
            }
            catch
            {
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //Edit();
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
            //if (dataGridView1.SelectedRows.Count > 0)
            //{
                //DialogResult result = MessageBox.Show("Are You Sure Want to Approve ? Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //if (result == DialogResult.Yes)
                //{
//                    DataTable dt = dbFunctions.getTable("pr_Approve_Supplier_Master '" + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + "','" + dbFunctions.username + "'");
                    //MessageBox.Show("Approved Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //display();

                //}
            //}
            //else
            //{
            //    MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        public void Update()
        {  
        }
        public void Edit()
        {

        }
        public void Delete()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) == "1")
                {
                    if (dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Updated")
                    {
                        DataTable dt = dbFunctions.getTable("pr_Reject_Supplier_Master " + dataGridView1.Rows[i].Cells["SM_UpdatedID"].Value.ToString());

                    }
                    else if (dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Newly Added" || dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Waiting For Delete")
                    {
                        DataTable dt = dbFunctions.getTable("pr_Reject_Supplier_MasterNew " + dataGridView1.Rows[i].Cells["ID"].Value.ToString());
                    }
                }
            }
            display();
        }
        public void display()
        {

            //DataTable dt = dbFunctions.getTable("pr_DisplayWaiting_Supplier_Master");
            DataTable dt = dbFunctions.getTable("pr_DisplayWaitingList_Supplier_Master");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
            //dataGridView1.ReadOnly = false;
            //dbFunctions.DGVStyle(dataGridView1);


            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Frozen = true;
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[0].Width = 50;




            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }
        public void Clear()
        {
        }


        //public bool Validate()
        //{
        //    //if ((string.IsNullOrEmpty(CB_ERPCode.Text.Trim())))
        //    //{
        //    //    ErrorMessage = "ERPCode Should Not be Empty";
        //    //    CB_ERPCode.Focus();
        //    //    return true;
        //    //}
        //    if ((string.IsNullOrEmpty(SM_Code.Text.Trim())))
        //    {
        //        ErrorMessage = "Code Should Not be Empty";
        //        SM_Code.Focus();
        //        return true;
        //    }
        //    if ((string.IsNullOrEmpty(SM_Name.Text.Trim())))
        //    {
        //        ErrorMessage = "Name Should Not be Empty";
        //        SM_Name.Focus();
        //        return true;
        //    }
        //    if ((string.IsNullOrEmpty(SM_ED.Text.Trim())))
        //    {
        //        ErrorMessage = "ED Should Not be Empty";
        //        SM_ED.Focus();
        //        return true;
        //    }

        //    if ((string.IsNullOrEmpty(SM_CST.Text.Trim())))
        //    {
        //        ErrorMessage = "CST  Should Not be Empty";
        //        SM_CST.Focus();
        //        return true;
        //    }
        //    if ((string.IsNullOrEmpty(SM_VAT.Text.Trim())))
        //    {
        //        ErrorMessage = "VAT  Should Not be Empty";
        //        SM_VAT.Focus();
        //        return true;
        //    }
        //    if ((string.IsNullOrEmpty(SM_GST.Text.Trim())))
        //    {
        //        ErrorMessage = "GST  Should Not be Empty";
        //        SM_GST.Focus();
        //        return true;
        //    }

            
        //    return false;
        //}
        private void Radio_Active_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("pr_Display_Customer_Master");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
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
        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {

                this.Close();

            }
        }
   }
}