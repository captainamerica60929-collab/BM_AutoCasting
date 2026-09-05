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

    public partial class ApprovalCustomers : Form
    {
        public string arrow = "Up";
        public int Distance = 64;
        public string ID = "";
        string ErrorMessage = "";
        public string SupType = "";
        public string Department = "";
        public ApprovalCustomers()
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
                    if (dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Updated" && dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() != "Approved"&& dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() != "Newly Added")
                    {

                        DataTable dt = dbFunctions.getTable("pr_Edit_Customer_Master  " + dataGridView1.Rows[i].Cells["ID"].Value.ToString());
                        string er = dt.Rows[0]["CM_Name"].ToString();
                        SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                        try
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand();
                            com.Connection = con;
                            com.CommandType = CommandType.StoredProcedure;
                            //com.CommandText = "pr_Updation_Supplier_Master";dt.Rows[0]["SM_UpdatedID"].ToString();
                            com.CommandText = "pr_Update_Customer_Master";

                            com.Parameters.Add("@CM_ID", SqlDbType.VarChar).Value = dt.Rows[0]["CM_UpdatedID"].ToString();
                            com.Parameters.Add("@CM_Code", SqlDbType.VarChar).Value = dt.Rows[0]["CM_Code"].ToString();
                            com.Parameters.Add("@CM_Name", SqlDbType.VarChar).Value = dt.Rows[0]["CM_Name"].ToString();
                            com.Parameters.Add("@CM_BillingAddress", SqlDbType.VarChar).Value = dt.Rows[0]["CM_BillingAddress"].ToString();
                            com.Parameters.Add("@CM_ContactPerson", SqlDbType.VarChar).Value = dt.Rows[0]["CM_ContactPerson"].ToString();
                            com.Parameters.Add("@CM_PersonDesign", SqlDbType.VarChar).Value = dt.Rows[0]["CM_PersonDesign"].ToString();
                            com.Parameters.Add("@CM_MobileNo", SqlDbType.VarChar).Value = dt.Rows[0]["CM_MobileNo"].ToString();
                            com.Parameters.Add("@CM_Email", SqlDbType.VarChar).Value = dt.Rows[0]["CM_Email"].ToString();
                            com.Parameters.Add("@CM_AddressOfTheRange", SqlDbType.VarChar).Value = dt.Rows[0]["CM_AddressOfTheRange"].ToString();
                            com.Parameters.Add("@CM_ECC_No", SqlDbType.VarChar).Value = dt.Rows[0]["CM_ECC_No"].ToString();
                            com.Parameters.Add("@CM_TIN_No", SqlDbType.VarChar).Value = dt.Rows[0]["CM_TIN_No"].ToString();
                            com.Parameters.Add("@CM_CSTNo_Date", SqlDbType.VarChar).Value = dt.Rows[0]["CM_CSTNo_Date"].ToString();
                            com.Parameters.Add("@CM_GSTProvisonalID", SqlDbType.VarChar).Value = dt.Rows[0]["CM_GSTProvisonalID"].ToString();
                            com.Parameters.Add("@CM_ED", SqlDbType.VarChar).Value = dt.Rows[0]["CM_ED"].ToString();
                            com.Parameters.Add("@CM_CST", SqlDbType.VarChar).Value = dt.Rows[0]["CM_CST"].ToString();
                            com.Parameters.Add("@CM_VAT", SqlDbType.VarChar).Value = dt.Rows[0]["CM_VAT"].ToString();
                            com.Parameters.Add("@CM_GST", SqlDbType.VarChar).Value = dt.Rows[0]["CM_GST"].ToString();
                            com.Parameters.Add("@CM_MoldAmortisationCOst", SqlDbType.Decimal).Value = dt.Rows[0]["CM_MoldAmortisationCOst"].ToString();
                            com.Parameters.Add("@CM_Remarks", SqlDbType.VarChar).Value = dt.Rows[0]["CM_Remarks"].ToString();
                            com.Parameters.Add("@CM_User", SqlDbType.VarChar).Value = dbFunctions.username;

                            com.ExecuteNonQuery();
                            com.Connection.Close();
                           MessageBox.Show("Details Updated Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            UpdateSelected();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(er + "Already Exists");
                        }
                       
                    }
                    else if (dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Waiting For Delete" && dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() != "Approved" && dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() != "Newly Added")
                    {
                        DataTable dt = dbFunctions.getTable("pr_Delete_Customer_Master " + dataGridView1.Rows[i].Cells["ID"].Value.ToString());
                        MessageBox.Show("Deleted Sucessfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() != "Approved")
                    {
                        DataTable dt = dbFunctions.getTable("pr_Approve_Customer_Master '" + dataGridView1.Rows[i].Cells["ID"].Value.ToString() + "','" + dbFunctions.username + "'");
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
                DataTable dt = dbFunctions.getTable("pr_Delete_Updated_Customer " + dataGridView1.SelectedRows[0].Cells["CM_UpdatedID"].Value.ToString());
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
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) == "1")
                {
                    DialogResult result = MessageBox.Show("Are You Sure Want to Approve ? Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        DataTable dt = dbFunctions.getTable("pr_Approve_Customer_Master '" + dataGridView1.Rows[i].Cells["ID"].Value.ToString() + "','" + dbFunctions.username + "'");
                        MessageBox.Show("Approved Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        display();

                    }
                }
            }
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
                        DataTable dt = dbFunctions.getTable("pr_Reject_Customer_Master " + dataGridView1.Rows[i].Cells["CM_UpdatedID"].Value.ToString());
                     
                    }
                    else if (dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Newly Added" || dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Waiting For Delete")
                    {
                        DataTable dt = dbFunctions.getTable("pr_Reject_Customer_MasterNew " + dataGridView1.Rows[i].Cells["ID"].Value.ToString());
                    }
                
                }
            }
            display();


        }
        public void display()
        {

            //DataTable dt = dbFunctions.getTable("pr_DisplayWaiting_Customer_Master");
            DataTable dt = dbFunctions.getTable("pr_DisplayWaitingList_Customer_Master");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
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
                //DataTable dt = dbFunctions.getTable("pr_Delete_AllPurchase_Entrydetails  " + txtPO.Text);
                this.Close();

            }
        }
   }
}