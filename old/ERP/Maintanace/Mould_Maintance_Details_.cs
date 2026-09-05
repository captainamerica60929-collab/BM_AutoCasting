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

    public partial class Mould_Maintance_Details : Form
    {
        public string arrow = "Up";
        public int Distance = 183;
        public string ID = "";
        string ErrorMessage = "";
        public Mould_Maintance_Details()
        {
            InitializeComponent();
            dbFunctions.DGVStyle(dataGridView1);
        }




        private void ItemMaster_Shown(object sender, EventArgs e)
        {
            // display();
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
            Radio_Active.Checked = true;
           // display();

            LoadMould();
            LoadCheckPoint();
            LoadRequirement();
           

        }

        private void LoadMould()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Load_MouldName_Matainance");
                MMD_Mould_ID.DataSource = dt;
                MMD_Mould_ID.DisplayMember = "MLD_PartName";
                MMD_Mould_ID.ValueMember = "MLD_ID";
                MMD_Mould_ID.SelectedIndex = -1;

            }
            catch
            {
            }
        }

        public void LoadRequirement()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Load_Requirement");
                MMD_Requirement.DataSource = dt;
                MMD_Requirement.DisplayMember = "RM_Requirement";
                MMD_Requirement.ValueMember = "RM_ID";
                MMD_Requirement.SelectedIndex = -1;

            }
            catch
            {
            }
        }

        public void LoadCheckPoint()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Load_CheckPoint");
                MMD_Check_Point.DataSource = dt;
                MMD_Check_Point.DisplayMember = "CP_Checkpoint_Name";
                MMD_Check_Point.ValueMember = "CP_iid";
                MMD_Check_Point.SelectedIndex = -1;

            }
            catch
            {
            }
           
        }



        private void btnDisplay_Click(object sender, EventArgs e)
        {
            // display();
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
                com.CommandText = "Pr_Insert_Mould_Maintance_Details";
                com.Parameters.Add("@MMD_Type", SqlDbType.VarChar).Value = MMD_Type.Text.ToString();
                com.Parameters.Add("@MMD_Mould_ID", SqlDbType.Int).Value = MMD_Mould_ID.SelectedValue.ToString();
                com.Parameters.Add("@MMD_Check_Point", SqlDbType.Int).Value = MMD_Check_Point.SelectedValue.ToString();
                com.Parameters.Add("@MMD_Requirement", SqlDbType.Int).Value = MMD_Requirement.SelectedValue.ToString();
                com.Parameters.Add("@MMD_Order_id", SqlDbType.Int).Value = MMD_Order_id.Text.ToString();
                com.Parameters.Add("@MMD_Method", SqlDbType.VarChar).Value = MMD_Method.Text.ToString();
                com.Parameters.Add("@MMD_Created_by", SqlDbType.VarChar).Value = dbFunctions.username;
                com.Parameters.Add("@MMD_MSTATUS", SqlDbType.VarChar).Value = textBox2.Text.ToString();
                com.ExecuteNonQuery();
                com.Connection.Close();
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
                com.CommandText = "Pr_Update_Mould_Maintance_Details";
                com.Parameters.Add("@MMD_iid", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@MMD_Type", SqlDbType.VarChar).Value = MMD_Type.Text.ToString();
                com.Parameters.Add("@MMD_Mould_ID", SqlDbType.Int).Value = MMD_Mould_ID.SelectedValue.ToString();
                com.Parameters.Add("@MMD_Check_Point", SqlDbType.Int).Value = MMD_Check_Point.SelectedValue.ToString();
                com.Parameters.Add("@MMD_Requirement", SqlDbType.Int).Value = MMD_Requirement.SelectedValue.ToString();
                com.Parameters.Add("@MMD_Method", SqlDbType.VarChar).Value = MMD_Method.Text.ToString();
                com.Parameters.Add("@MMD_Order_id", SqlDbType.Int).Value = MMD_Order_id.Text.ToString();
                com.Parameters.Add("@MMD_Created_by", SqlDbType.VarChar).Value = dbFunctions.username;
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


        public void Edit()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Mould_Maintance_Details_ByID  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["MMD_iid"].ToString();
                MMD_Type.Text = dt.Rows[0]["MMD_Type"].ToString();
                MMD_Mould_ID.SelectedValue = dt.Rows[0]["MMD_Mould_ID"].ToString();
                MMD_Check_Point.SelectedValue = dt.Rows[0]["MMD_Check_Point"].ToString();
                MMD_Method.SelectedValue = dt.Rows[0]["MMD_Method"].ToString();
                MMD_Requirement.SelectedValue = dt.Rows[0]["MMD_Requirement"].ToString();
                MMD_Order_id.Text = dt.Rows[0]["MMD_Order_id"].ToString();
                
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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_Mould_Maintance_Details " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Mould_Maintance_Details_Details '" + MMD_Mould_ID.SelectedValue.ToString() + "','" + MMD_Type.Text.ToString() + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);

            txt_Rows.Text = "Rows :"+dataGridView1.Rows.Count.ToString();
        }


        public void Clear()
        {
            MMD_Type.Text = "";
            //MMD_Mould_ID.Text = "";
            MMD_Check_Point.Text = "";
            MMD_Requirement.Text = "";
            MMD_Order_id.Text = "0";
            MMD_Method.Text = "";



            btnsave.Text = "&Save    ";
        }


        public bool Validate()
        {
            if ((string.IsNullOrEmpty(MMD_Type.Text.Trim())))
            {
                ErrorMessage = "Name Should Not be Empty";
                MMD_Type.Focus();
                return true;
            }
            return false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {

                this.Close();

            }
        }

        private void MMD_Mould_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable a = dbFunctions.getTable("select * from Mold_Master where MLD_PartName = '" + MMD_Mould_ID.Text+ "' and  MLD_Status='A'");
            if (a.Rows.Count > 0)
            {
                textBox1.Text = a.Rows[0]["MLD_MouldNo"].ToString();
            }
            display();
        }

        private void MMD_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            display();
        }

        private void MMD_Order_id_KeyPress(object sender, KeyPressEventArgs e)
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Requirement] LIKE '%{0}%' or [Check Point] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Check_PointMaster ObjCheck_PointMaster = new Check_PointMaster();
            ObjCheck_PointMaster.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Requirement_Master ObjRequirement_Master = new Requirement_Master();
            ObjRequirement_Master.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CheckPointRefersh();
        }

        public void CheckPointRefersh()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Load_CheckPointRefresh");
                MMD_Check_Point.DataSource = dt;
                MMD_Check_Point.DisplayMember = "CP_Checkpoint_Name";
                MMD_Check_Point.ValueMember = "CP_iid";
                MMD_Check_Point.SelectedIndex =0;

            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshRequirement();
        }

        public void RefreshRequirement()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Load_RequirementRefresh");
                MMD_Requirement.DataSource = dt;
                MMD_Requirement.DisplayMember = "RM_Requirement";
                MMD_Requirement.ValueMember = "RM_ID";
                MMD_Requirement.SelectedIndex =0;

            }
            catch
            {
            }
        }

    }
}