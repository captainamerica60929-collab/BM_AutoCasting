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

    public partial class Plant_Master : Form
    {
        public string arrow = "Up";
        public int Distance = 106;
        public string ID = "";
        string ErrorMessage = "";
        public Plant_Master()
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
            PL_No.Focus();

        }



        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
            Radio_Active.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(Validate())
            {
                MessageBox.Show(ErrorMessage,"Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            if (btnsave.Text.ToString().Equals("&Update"))
            {
                Update();
            }
            else
            {
                insert();
                PL_No.Focus();
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
                com.CommandText = "pr_Insert_Plant_Master";

                com.Parameters.Add("@PL_No", SqlDbType.VarChar).Value = PL_No.Text.ToString();
                com.Parameters.Add("@PL_Name", SqlDbType.VarChar).Value = PL_Name.Text.ToString();
                com.Parameters.Add("@PL_Location", SqlDbType.VarChar).Value = PL_Location.Text.ToString();


                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                display();
               // Ty_TypeName.Focus();
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
                com.CommandText = "pr_Update_Plant_Master";
                com.Parameters.Add("@PL_ID", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@PL_No", SqlDbType.VarChar).Value = PL_No.Text.ToString();
                com.Parameters.Add("@PL_Name", SqlDbType.VarChar).Value = PL_Name.Text.ToString();
                com.Parameters.Add("@PL_Location", SqlDbType.VarChar).Value = PL_Location.Text.ToString(); com.ExecuteNonQuery(); com.Connection.Close();
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
                DataTable dt = dbFunctions.getTable("pr_Edit_Plant_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["PL_ID"].ToString();

                PL_No.Text = dt.Rows[0]["PL_No"].ToString();
                PL_Name.Text = dt.Rows[0]["PL_Name"].ToString();
                PL_Location.Text = dt.Rows[0]["PL_Location"].ToString();

                btnsave.Text = "&Update";
                PL_No.Focus();
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
                    DataTable dt = dbFunctions.getTable("pr_Delete_Plant_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("pr_Display_Plant_Master");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }


        public void Clear()
        {
            PL_Name.Text = "";
            PL_No.Text = "";
            PL_Location.Text = "";
            btnsave.Text = "&Save   ";
            PL_No.Focus();
        }


        public bool Validate()
        {
            if ((string.IsNullOrEmpty(PL_Name.Text.Trim())))
            {
                ErrorMessage = "Plant Should Not be Empty";
                PL_Name.Focus();
                return true;
            }
            return false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_ItemType_Master_DeAct");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void Radio_Active_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void Ty_TypeName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                btnsave.Focus();
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Plant] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }

        private void Ty_TypeName_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Your Item Type";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {

                this.Close();

            }
        }

        private void Item_Type_KeyDown(object sender, KeyEventArgs e)
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

     

       

   }
}