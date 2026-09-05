using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace LarchERP.Master
{

    public partial class UserMaster : Form
    {
        public string arrow = "Up";
        public int Distance = 173;
        public string ID = "";
        public string ErrorMessage = "";
        public UserMaster()
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
               // ArrowButton.Image = GenuineHR.Properties.Resources.Down;

            }
            else
            {
                splitContainer1.SplitterDistance = Distance;
                arrow = "Up";
               // ArrowButton.Image = GenuineHR.Properties.Resources.Up;
            }

        }

        public void get_user()
        {
            string sqlText = "select * from employee_Master  where EM_Status='A'";
            DataTable dtpt = dbFunctions.getTable(sqlText);
            um_vFirstName.DataSource = dtpt;
            um_vFirstName.DisplayMember = "EM_EmployeeName";
            um_vFirstName.ValueMember = "EM_EmployeeName";
            um_vFirstName.SelectedIndex = -1;
        }

        private void ItemMaster_Load(object sender, EventArgs e)
        {
            get_user();
            display();
            um_vFirstName.Focus();
            um_vLoginId.Text = dbFunctions.username;

            DataTable dtpt = new DataTable();
            string sqlText = "pr_FetchRightsName";
            dtpt = dbFunctions.getTable(sqlText);
            um_iRightsId.DataSource = dtpt;
            um_iRightsId.DisplayMember = "rmm_vRightsName";
            um_iRightsId.ValueMember = "rmm_iId";
            um_iRightsId.SelectedIndex = -1;
        }



        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }

        public static string DecryptThis(string ToDecrypt, string Keys)
        {
            byte[] IV = new byte[8] { 210, 1, 25, 101, 0, 76, 173, 59 };
            byte[] buffer = Convert.FromBase64String(ToDecrypt);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Keys));
            des.IV = IV;
            return Encoding.ASCII.GetString(
            des.CreateDecryptor().TransformFinalBlock(
            buffer,
            0,
            buffer.Length
            )
            );

        }


        public static string EncryptThis(string ToEncrypt, string Keys)
        {
            byte[] IV = new byte[8] { 210, 1, 25, 101, 0, 76, 173, 59 };
            byte[] buffer = Encoding.ASCII.GetBytes(ToEncrypt);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Keys));
            des.IV = IV;
            return Convert.ToBase64String(
            des.CreateEncryptor().TransformFinalBlock(
            buffer,
            0,
            buffer.Length
            )
            );
        }
        public string  Password="";
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Password = um_vPassword.Text;
            if (btnsave.Text.ToString().Equals("Update"))
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
                com.CommandText = "Pr_Insert_User_Mst";
                com.Parameters.Add("@um_vFirstName", SqlDbType.VarChar).Value = um_vFirstName.Text.ToString();
                com.Parameters.Add("@um_vLastName", SqlDbType.VarChar).Value = um_vLastName.Text.ToString();
                com.Parameters.Add("@um_vLoginId", SqlDbType.VarChar).Value = um_vLoginId.Text.ToString();
                com.Parameters.Add("@um_vPassword", SqlDbType.VarChar).Value = Password;// um_vPassword.Text.ToString();
                com.Parameters.Add("@um_vAuthority", SqlDbType.VarChar).Value = um_vAuthority.Text.ToString();
                com.Parameters.Add("@um_iRightsId", SqlDbType.Int).Value = um_iRightsId.SelectedValue.ToString();
                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                display();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show("Details Save Failed, Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                com.CommandText = "Pr_Update_User_Mst";
                com.Parameters.Add("@um_iId", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@um_vFirstName", SqlDbType.VarChar).Value = um_vFirstName.Text.ToString();
                com.Parameters.Add("@um_vLastName", SqlDbType.VarChar).Value = um_vLastName.Text.ToString();
                com.Parameters.Add("@um_vLoginId", SqlDbType.VarChar).Value = um_vLoginId.Text.ToString();
                com.Parameters.Add("@um_vPassword", SqlDbType.VarChar).Value = Password;// um_vPassword.Text.ToString();
                com.Parameters.Add("@um_vAuthority", SqlDbType.VarChar).Value = um_vAuthority.Text.ToString();
                com.Parameters.Add("@um_iRightsId", SqlDbType.VarChar).Value = um_iRightsId.SelectedValue.ToString();
                com.ExecuteNonQuery();  com.Connection.Close();
                MessageBox.Show("Details Updated Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                display();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show("Details Update Failed, Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Edit()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("Pr_Fetch_User_Mst_ByID  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["um_iId"].ToString();
                um_vFirstName.Text = dt.Rows[0]["um_vFirstName"].ToString();
                um_vLastName.Text = dt.Rows[0]["um_vLastName"].ToString();
                um_vLoginId.Text = dt.Rows[0]["um_vLoginId"].ToString();
                um_vPassword.Text = dt.Rows[0]["um_vPassword"].ToString();
                um_vAuthority.Text = dt.Rows[0]["um_vAuthority"].ToString();
                um_iRightsId.SelectedValue = dt.Rows[0]["um_iRightsId"].ToString();
                btnsave.Text = "Update";
                um_vFirstName.Focus();
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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_User_Mst " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("Pr_Fetch_User_Mst_Details");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }


        public void Clear()
        {
            um_vFirstName.Text = "";
            um_vLastName.Text = "";
            um_vLoginId.Text = "";
            um_vPassword.Text = "";
            um_iRightsId.Text = "";
            um_iRightsId.Text = "";
            btnsave.Text = "&Save    ";
            um_vFirstName.Focus();
        }


        public bool Validate()
        {
            if ((string.IsNullOrEmpty(um_vFirstName.Text.Trim())))
            {
                ErrorMessage = "FirstName Should Not be Empty";
                um_vFirstName.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(um_vLoginId.Text.Trim())))
            {
                ErrorMessage = "LoginId Should Not be Empty";
                um_vLoginId.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(um_vPassword.Text.Trim())))
            {
                ErrorMessage = "Password Should Not be Empty";
                um_vPassword.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(um_iRightsId.Text.Trim())))
            {
                ErrorMessage = "Rights Should Not be Empty";
                um_iRightsId.Focus();
                return true;
            }
            return false;
        }


private void btnClear_Click(object sender, EventArgs e)
{
    Clear();
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
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[FirstName] LIKE '%{0}%'", textBoxX1.Text);
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[LastName] LIKE '%{0}%'", textBoxX1.Text);

            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[LoginId] LIKE '%{0}%'", textBoxX1.Text);


        }
    }
    catch (Exception ex)
    {
    }

    txtStatus.Text = "Total Rows Cout :" + (dataGridView1.Rows.Count - 1).ToString();
}

private void button3_Click(object sender, EventArgs e)
{
     dbFunctions.isclose = true; this.Close(); 
}

private void um_vFirstName_Enter(object sender, EventArgs e)
{
    txtStatus.Text = "Please Enter First Name Here.....";
}

private void um_vLastName_Enter(object sender, EventArgs e)
{
    txtStatus.Text = "Please Enter Last Name Here.....";

}

private void um_vAuthority_Enter(object sender, EventArgs e)
{
    txtStatus.Text = "Please Enter Authority Here.....";

}

private void um_vLoginId_Enter(object sender, EventArgs e)
{
    txtStatus.Text = "Please Enter Login Id Here.....";

}

private void um_vPassword_Enter(object sender, EventArgs e)
{
    txtStatus.Text = "Please Enter Password Here.....";

}

private void um_iRightsId_Enter(object sender, EventArgs e)
{
    txtStatus.Text = "Please Choose type Of Right Here.....";

}

private void UserMaster_KeyDown(object sender, KeyEventArgs e)
{
    if(e.KeyCode==Keys.Enter)
    {
        SendKeys.Send("{TAB}");
    }
}

private void panel6_Paint(object sender, PaintEventArgs e)
{

}

    }
}