using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;

namespace Electronika.Master
{
    public partial class CurrecyMaster : Form
    {
        public CurrecyMaster()
        {
            InitializeComponent();
        }

        public string arrow = "Up";
        public int Distance = 202;
        public string ID = "0";
        string ErrorMessage = "";
        private void btnsave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public void insert()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Currency_Master";
                com.Parameters.Add("@CM_Currency", SqlDbType.VarChar).Value = CM_Currency.Text.ToString();
                com.Parameters.Add("@CM_EX_Rate", SqlDbType.VarChar).Value = CM_EX_Rate.Text.ToString();
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
                com.CommandText = "Pr_Update_Currency_Master";
                com.Parameters.Add("@CM_iid", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@CM_Currency", SqlDbType.VarChar).Value = CM_Currency.Text.ToString();
                com.Parameters.Add("@CM_EX_Rate", SqlDbType.VarChar).Value = CM_EX_Rate.Text.ToString();
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
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Currency_Master_ByID  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["CM_iid"].ToString();
                CM_Currency.Text = dt.Rows[0]["CM_Currency"].ToString();
                CM_EX_Rate.Text = dt.Rows[0]["CM_EX_Rate"].ToString();
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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_Currency_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Currency_Master_Details");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }


        public void Clear()
        {
            CM_Currency.Text = "";
            CM_EX_Rate.Text = "";
            btnsave.Text = "&Save    ";
        }


        public bool Validate()
        {
            if ((string.IsNullOrEmpty(CM_Currency.Text.Trim())))
            {
                ErrorMessage = "Currency Should Not be Empty";
                CM_Currency.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(CM_EX_Rate.Text.Trim())))
            {
                ErrorMessage = "Rate Should Not be Empty";
                CM_EX_Rate.Focus();
                return true;
            }
            return false;
        }

        private void CurrecyMaster_Load(object sender, EventArgs e)
        {
            display();
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }

        private void CM_EX_Rate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)CM_EX_Rate);
        }

        private void button2_Click(object sender, EventArgs e)
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

                if (string.IsNullOrEmpty(textBoxX1.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Currency] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
