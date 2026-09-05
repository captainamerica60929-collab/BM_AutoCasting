using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Maintanence_Printing_Tool;

namespace Genuine_Inventory.Common
{
    public partial class Login_Settings : Form
    {
        public string ID = "";

        public Login_Settings()
        {
            InitializeComponent();
        }

        private void Login_Settings_Load(object sender, EventArgs e)
        {
            ld_vPassword.Focus();
            ld_vUserName.Text = dbFunctions.username;

            FetchLogindetails();


            pgStrength.Minimum = 0;
            pgStrength.Maximum = 10;
        }
        public void FetchLogindetails()
        {
            DataTable dt = dbFunctions.getTable("pr_Edit_Login_Details1 '" + ld_vUserName.Text.ToString() + "'");
            ID = dt.Rows[0]["um_iId"].ToString();
            ld_vFirstName.Text = dt.Rows[0]["um_vFirstName"].ToString();
            ld_vLastName.Text = dt.Rows[0]["um_vLastName"].ToString();
            ld_vUserName.Text = dt.Rows[0]["um_vLoginId"].ToString();
            ld_vPassword.Text = dt.Rows[0]["um_vPassword"].ToString();          
        }
        public void btnsave_Click(object sender, EventArgs e)
        {
            update();
        }
        private void update()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pr_Update_Login_Details";
                cmd.Parameters.Add("@um_iId", SqlDbType.Int).Value = ID;
                cmd.Parameters.Add("@um_vFirstName", SqlDbType.VarChar).Value = ld_vFirstName.Text.ToString();
                cmd.Parameters.Add("@um_vLastName", SqlDbType.VarChar).Value = ld_vLastName.Text.ToString();
                cmd.Parameters.Add("@um_vLoginId", SqlDbType.VarChar).Value = ld_vUserName.Text.ToString();
                cmd.Parameters.Add("@um_vPassword", SqlDbType.VarChar).Value = ld_vPassword.Text.ToString();
                //cmd.Parameters.Add("@ld_vUser", SqlDbType.VarChar).Value = dbFunctions.username;
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                MessageBox.Show("Details Updated Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();  
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
        public void clear()
        {
            ld_vFirstName.Text="";
            ld_vUserName.Text="";   
            ld_vLastName.Text="";
            ld_vPassword.Text = "";
        }
        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ld_vPassword_TextChanged(object sender, EventArgs e)
        {
            ld_vPassword.MaxLength = 10;
            int length = ld_vPassword.Text.Length;

            pgStrength.Value = length;
        }

        
    }
}
