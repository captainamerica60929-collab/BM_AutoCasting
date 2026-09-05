using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;

namespace CRM_App.Master
{
    public partial class Monthly_Schedule : Form
    {
        public Monthly_Schedule()
        {
            InitializeComponent();
        }

        private void FG_Master_Load(object sender, EventArgs e)
        {
            Load_Customer_Name();
            Load_Part_Name();
            display();
        }



        private void Load_Part_Name()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_get_Part_Name");
                MS_Part_Name.DataSource = dt;
                MS_Part_Name.DisplayMember = "IM_PartName";
                MS_Part_Name.ValueMember = "IM_ID";
                MS_Part_Name.SelectedIndex = -1;

            }
            catch
            {
            }
        }
        


        public void Load_Customer_Name()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Load_CM_Name");
                MS_Customer_Name.DataSource = dt;
                MS_Customer_Name.DisplayMember = "CM_Name";
                MS_Customer_Name.ValueMember = "CM_ID";
                MS_Customer_Name.SelectedIndex = -1;
                
            }
            catch { }
        }



        private void IM_MaxStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        public string ErrorMessage = "";


        private void btnsave_Click(object sender, EventArgs e)
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
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
                com.CommandText = "Pr_Insert_Monthly_Schedule";
                com.Parameters.Add("@MS_Month", SqlDbType.VarChar).Value = MS_Month.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MS_Customer_Name", SqlDbType.VarChar).Value = MS_Customer_Name.Text.ToString();
                com.Parameters.Add("@MS_Part_ID", SqlDbType.VarChar).Value = MS_Part_Name.SelectedValue.ToString();
                com.Parameters.Add("@MS_Part_Number", SqlDbType.VarChar).Value = MS_Part_Name.Text.ToString();
                com.Parameters.Add("@MS_Part_Name", SqlDbType.VarChar).Value = MS_Part_Name.Text.ToString();
                com.Parameters.Add("@MS_Model", SqlDbType.VarChar).Value = MS_Model.Text.ToString();
                com.Parameters.Add("@MS_Cost", SqlDbType.VarChar).Value = MS_Cost.Text.ToString();
                com.Parameters.Add("@MS_Opening_Stock", SqlDbType.VarChar).Value = MS_Opening_Stock.Text.ToString();
                com.Parameters.Add("@MS_Monthly_Scheduler", SqlDbType.VarChar).Value = MS_Monthly_Scheduler.Text.ToString();
                com.Parameters.Add("@MS_Safty_Stock", SqlDbType.VarChar).Value = MS_Safty_Stock.Text.ToString();
                com.Parameters.Add("@MS_Required_Qty", SqlDbType.VarChar).Value = MS_Required_Qty.Text.ToString();
                com.Parameters.Add("@MS_Created_By", SqlDbType.VarChar).Value = dbFunctions.username;
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
                com.CommandText = "Pr_Update_Monthly_Schedule";
                com.Parameters.Add("@MS_iid", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@MS_Month", SqlDbType.VarChar).Value = MS_Month.Text.ToString();
                com.Parameters.Add("@MS_Customer_Name", SqlDbType.VarChar).Value = MS_Customer_Name.Text.ToString();
                com.Parameters.Add("@MS_Part_ID", SqlDbType.VarChar).Value = MS_Part_Name.SelectedValue.ToString();
                com.Parameters.Add("@MS_Part_Number", SqlDbType.VarChar).Value = MS_Part_Name.Text.ToString();
                com.Parameters.Add("@MS_Part_Name", SqlDbType.VarChar).Value = MS_Part_Name.Text.ToString();
                com.Parameters.Add("@MS_Model", SqlDbType.VarChar).Value = MS_Model.Text.ToString();
                com.Parameters.Add("@MS_Cost", SqlDbType.VarChar).Value = MS_Cost.Text.ToString();
                com.Parameters.Add("@MS_Opening_Stock", SqlDbType.VarChar).Value = MS_Opening_Stock.Text.ToString();
                com.Parameters.Add("@MS_Monthly_Scheduler", SqlDbType.VarChar).Value = MS_Monthly_Scheduler.Text.ToString();
                com.Parameters.Add("@MS_Safty_Stock", SqlDbType.VarChar).Value = MS_Safty_Stock.Text.ToString();
                com.Parameters.Add("@MS_Required_Qty", SqlDbType.VarChar).Value = MS_Required_Qty.Text.ToString();
                com.Parameters.Add("@MS_Created_By", SqlDbType.VarChar).Value = dbFunctions.username;
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

        string ID = "";

        public void Edit()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Monthly_Schedule_ByID  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["MS_iid"].ToString();
                MS_Month.Text = dt.Rows[0]["MS_Month"].ToString();
                MS_Customer_Name.Text = dt.Rows[0]["MS_Customer_Name"].ToString();
                MS_Part_Name.SelectedValue = dt.Rows[0]["MS_Part_ID"].ToString();
               // MS_Part_Number.Text = dt.Rows[0]["MS_Part_Number"].ToString();
                MS_Part_Name.Text = dt.Rows[0]["MS_Part_Name"].ToString();
                MS_Model.Text = dt.Rows[0]["MS_Model"].ToString();
                MS_Cost.Text = dt.Rows[0]["MS_Cost"].ToString();
                MS_Opening_Stock.Text = dt.Rows[0]["MS_Opening_Stock"].ToString();
                MS_Monthly_Scheduler.Text = dt.Rows[0]["MS_Monthly_Scheduler"].ToString();
                MS_Safty_Stock.Text = dt.Rows[0]["MS_Safty_Stock"].ToString();
                MS_Required_Qty.Text = dt.Rows[0]["MS_Required_Qty"].ToString();
              //  MS_Created_By.Text = dt.Rows[0]["MS_Created_By"].ToString();
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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_Monthly_Schedule " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Monthly_Schedule_Details");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }


        public void Clear()
        {
            MS_Month.Text = "";
            MS_Customer_Name.Text = "";
            MS_Part_Name.SelectedIndex = 0;
            MS_Part_Name.Text = "";
            MS_Model.Text = "";
            MS_Cost.Text = "";
            MS_Opening_Stock.Text = "";
            MS_Monthly_Scheduler.Text = "";
            MS_Safty_Stock.Text = "";
            MS_Required_Qty.Text = "";
            btnsave.Text = "&Save    ";
        }


        public bool Validate()
        {
            return false;
        }

        private void MS_Part_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_Part_Detail '" + MS_Part_Name.SelectedValue.ToString() + "'");
                MS_Part_Number.Text = dt.Rows[0]["IM_PartNo"].ToString();
                MS_Safty_Stock.Text = dt.Rows[0]["IM_MinStock"].ToString();
                MS_Model.Text = dt.Rows[0]["ML_Model"].ToString();
                MS_Cost.Text = dt.Rows[0]["IM_Sales_Price"].ToString();
            }
            catch { }
        }

        private void MS_Cost_TextChanged(object sender, EventArgs e)
        {

        }

        public void calculate()
        {
            try
            {
                decimal Req = 0.0m;
                MS_Required_Qty.Text = (decimal.Parse(MS_Monthly_Scheduler.Text) - (decimal.Parse(MS_Opening_Stock.Text)) + (decimal.Parse(MS_Safty_Stock.Text.ToString()))).ToString();
            }
            catch { }

        }

        private void MS_Safty_Stock_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void MS_Monthly_Scheduler_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void MS_Opening_Stock_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
