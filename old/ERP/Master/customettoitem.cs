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

    public partial class customettoitem : Form
    {
        public string arrow = "Up";
        public int Distance = 114;
        public string ID = "";
        string ErrorMessage = "";
        public customettoitem()
        {
            InitializeComponent();
            dbFunctions.DGVStyle(dataGridView1);
        }




        private void ItemMaster_Shown(object sender, EventArgs e)
        {
            display();
            splitContainer1.SplitterDistance = Distance;
        }


        private void ArrowButton_Click(object sender, EventArgs e)
        {

        }

        private void ItemMaster_Load(object sender, EventArgs e)
        {
            Radio_Active.Checked = true;
            display();
            SM_NAME.Focus();
            SM_LOAD();
            ITEM_LOAD();


        }

        private void ITEM_LOAD()
        {
            DataTable dt = dbFunctions.getTable("SELECT * FROM ITEM_MASTER WHERE IM_STATUS='A' AND IM_TYPE='3'");
            SM_NAME.DataSource = dt;
            CM_SUPPLIER.DisplayMember = "SM_Name";
            CM_SUPPLIER.ValueMember = "SM_ID";
            CM_SUPPLIER.SelectedIndex = -1;
        }

        private void SM_LOAD()
        {
            DataTable dt = dbFunctions.getTable("SELECT * FROM SUPPLIER_MASTER WHERE SM_STATUS='A'");
            SM_NAME.DataSource = dt;
            SM_NAME.DisplayMember = "SM_Name";
            SM_NAME.ValueMember = "SM_ID";
            SM_NAME.SelectedIndex = -1;
        }

       



        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SM_NAME.Text = "";
            CM_SUPPLIER.Text = "";
            //if (Validate())
            //{
            //    MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
            //if (btnsave.Text.ToString().Equals("&Update"))
            //{
            //    Update();
            //}
            //else
            //{
            //    insert();
            //}
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
            ST_StoreName.Focus();
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
                com.CommandText = "Pr_Insert_Store_Master";
                com.Parameters.Add("@ST_StoreName", SqlDbType.VarChar).Value = ST_StoreName.Text.ToString();
                com.Parameters.Add("@ST_Location", SqlDbType.VarChar).Value = ST_Location.Text.ToString();
                com.Parameters.Add("@ST_User", SqlDbType.VarChar).Value = dbFunctions.username;
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
                com.CommandText = "Pr_Update_Store_Master";
                com.Parameters.Add("@ST_ID", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@ST_StoreName", SqlDbType.VarChar).Value = ST_StoreName.Text.ToString();
                com.Parameters.Add("@ST_Location", SqlDbType.VarChar).Value = ST_Location.Text.ToString();
                com.Parameters.Add("@ST_User", SqlDbType.VarChar).Value = dbFunctions.username;
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
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Store_Master_ByID  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["ST_ID"].ToString();
                ST_StoreName.Text = dt.Rows[0]["ST_StoreName"].ToString();
                ST_Location.Text = dt.Rows[0]["ST_Location"].ToString();
                
                btnsave.Text = "&Update";
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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_Store_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Store_Master_Details");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            txt_Rows.Text = dbFunctions.getRows(dataGridView1);
        }


        public void Clear()
        {
            //ST_StoreName.Text = "";
            //ST_Location.Text = "";
            //            btnsave.Text = "&Save    ";
            //            ST_StoreName.Focus();
        }


        public bool Validate()
        {
            //if ((string.IsNullOrEmpty(ST_StoreName.Text.Trim())))
            //{
            //    ErrorMessage = "StoreName Should Not be Empty";
            //    ST_StoreName.Focus();
            //    return true;
            //}
            //if ((string.IsNullOrEmpty(ST_Location.Text.Trim())))
            //{
            //    ErrorMessage = "Location Should Not be Empty";
            //    ST_Location.Focus();
            //    return true;
            //}
            //return false;
        }

        private void ST_StoreName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                ST_Location.Focus();
                
            }
        }

        private void ST_Location_KeyDown(object sender, KeyEventArgs e)
        {
             if(e.KeyCode==Keys.Enter)
            {
                 btnsave.Focus();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Store_Master_DetailsDeAct");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void Radio_Active_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void ST_StoreName_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Store Name Here....";
        }

        private void ST_Location_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Location Here....";
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[StoreName] LIKE '%{0}%'", textBoxX1.Text);

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Store_Master_KeyDown(object sender, KeyEventArgs e)
        {
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
        String ID2 = "0";
        private void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;

                if (ID2 == "0")
                {
                    // Generate a new idl_id
                    DataTable dd = dbFunctions.getTable("SELECT ISNULL(MAX(cfs_id), 0) + 1 FROM cfs");
                    if (dd.Rows.Count > 0)
                    {
                        ID2 = dd.Rows[0][0].ToString();
                    }

                    com.CommandText = "INSERT INTO Production_idl_resons (cfs_id,cfs_cus_id,cfs_cus_name,cfs_item_id,cfs_item_name,cfs_date,cfs_created_name,cfs_status) " +
                                      "VALUES (@cfs_id,@cfs_cus_id,@cfs_cus_name,@cfs_item_id,@cfs_item_name,@cfs_date,@cfs_created_name,@cfs_status)";
                    // Type = "NEW";
                }
                else
                {
                    com.CommandText = "UPDATE cfs SET cfs_id=@cfs_id,cfs_cus_id=@cfs_cus_id,cfs_cus_name=@cfs_cus_name,cfs_item_id=@cfs_item_id,cfs_item_name=@cfs_item_name,cfs_date=@cfs_date,cfs_created_name=@cfs_created_name,cfs_status=@cfs_status where cfs_id=@cfs_id";


                    //  button16.Text = "&SAVE";
                    // Type = "EDIT";
                }

                // Add parameters
                com.Parameters.Add("@cfs_id", SqlDbType.VarChar).Value = ID2.ToString();
                com.Parameters.Add("@cfs_cus_id", SqlDbType.VarChar).Value = SM_NAME.SelectedValue.ToString();
                com.Parameters.Add("@cfs_cus_name", SqlDbType.VarChar).Value = SM_NAME.Text;
                com.Parameters.Add("@cfs_item_id", SqlDbType.DateTime).Value = CM_SUPPLIER.SelectedValue.ToString();
                com.Parameters.Add("@cfs_item_name", SqlDbType.DateTime).Value = CM_SUPPLIER.Text;
                com.Parameters.Add("@cfs_date", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd/MM/yyyy h:mm tt");
                com.Parameters.Add("@cfs_status", SqlDbType.VarChar).Value = "A";
                com.Parameters.Add("@cfs_created_name", SqlDbType.VarChar).Value = dbFunctions.username;



                // Execute the query
                com.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Details Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            clear();

        }

        private void clear()
        {
            CM_SUPPLIER.Text = "";
            SM_NAME.Focus();
        }
    }
}