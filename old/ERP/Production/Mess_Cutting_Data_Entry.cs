using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;

namespace CRM_App.Production
{
    public partial class Mess_Cutting_Data_Entry : Form
    {
        public string ErrorMessage = "";
        public string ID = "";
        public Mess_Cutting_Data_Entry()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Production_Data_Entry_Load(object sender, EventArgs e)
        {

          
            PD_Date.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            PD_CreatedBy.Text = dbFunctions.username;
            Shift.Text = "Shift I";
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_MessCutting_Request  '" + dbFunctions.Route_Card_ID + "'");
                //ID = dt.Rows[0]["Pq_iid"].ToString();

                txtRCNo.Text = dt.Rows[0]["Mq_RequestNo"].ToString();
                txtPartNo.Text = dt.Rows[0]["mq_vPart_No"].ToString();
                txtPartName.Text = dt.Rows[0]["mq_vPart_Name"].ToString();
                //txtModel.Text = dt.Rows[0]["mq_vModel"].ToString();
                txtxplanQty.Text = dt.Rows[0]["mq_Plan_Qty"].ToString();
                txtStartDate.Text = dt.Rows[0]["StartDate"].ToString();
               
            }
            catch { }
        }
        public bool Validate()
        {
            if ((string.IsNullOrEmpty(PD_OK_Qty.Text.Trim())))
            {
                ErrorMessage = "OK Qty Should Not be Empty";
                PD_OK_Qty.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(PD_Reject_Qty.Text.Trim())))
            {
                ErrorMessage = "Reject Qty Should Not be Empty";
                PD_Reject_Qty.Focus();
                return true;
            }
            return false;
        }
     

        private void insert()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Insert_Mess_Production_Details";

                com.Parameters.Add("@PD_Route_Card_ID", SqlDbType.VarChar).Value = dbFunctions.Route_Card_ID;
                com.Parameters.Add("@PD_Date", SqlDbType.DateTime).Value = PD_Date.Text.ToString();
                com.Parameters.Add("@PD_CreatedBy", SqlDbType.VarChar).Value = PD_CreatedBy.Text.ToString();
                com.Parameters.Add("@PD_Shift", SqlDbType.VarChar).Value = Shift.Text.ToString();
                com.Parameters.Add("@PD_OK_Qty", SqlDbType.Int).Value = PD_OK_Qty.Text.ToString();
                com.Parameters.Add("@PD_Reject_Qty", SqlDbType.Int).Value = PD_Reject_Qty.Text.ToString();


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

        public void Clear()
        {
            PD_OK_Qty.Text="";
            PD_Reject_Qty.Text = "";
        }

        public void display()
        {
            DataTable dt = dbFunctions.getTable("pr_Display_Mess_Production_Details  " + dbFunctions.Route_Card_ID);
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns["Date"].Width = 90;

            dataGridView1.Columns["OK Qty"].Width = 90;
            dataGridView1.Columns["Rej Qty"].Width = 90;
            dataGridView1.Columns["Edit"].Width = 49;
            if (dt.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = false;
            }
            dataGridView1.Columns["OK Qty"].DefaultCellStyle.ForeColor = Color.Green;
            dataGridView1.Columns["Rej Qty"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Edit"].DefaultCellStyle.ForeColor = Color.Blue;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.ReadOnly = true;

            
        }
        private void Update()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Update_Production_Details";

                com.Parameters.Add("@PD_iid", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@PD_OK_Qty", SqlDbType.Int).Value = PD_OK_Qty.Text.ToString();
                com.Parameters.Add("@PD_Reject_Qty", SqlDbType.Int).Value = PD_Reject_Qty.Text.ToString();


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
            btnsave.Text = "&Save";
        }

        private void PD_OK_Qty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void PD_Reject_Qty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        public void Edit()
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("pr_Edit_Production_Details  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["PD_iid"].ToString();
                PD_Date.Text = dt.Rows[0]["Date"].ToString();
                PD_CreatedBy.Text = dt.Rows[0]["PD_CreatedBy"].ToString();
                Shift.Text = dt.Rows[0]["PD_Shift"].ToString();
                PD_OK_Qty.Text = dt.Rows[0]["PD_OK_Qty"].ToString();
                PD_Reject_Qty.Text = dt.Rows[0]["PD_Reject_Qty"].ToString();

                btnsave.Text = "&Update";
               
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        public void Delete()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("pr_Delete_Production_Details " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Production_Data_Entry_Shown(object sender, EventArgs e)
        {
            display();
        }

   
    }
}
