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

    public partial class Instument_Master : Form
    {
        public string arrow = "Up";
        public int Distance = 220;
        public string ID = "";
        string ErrorMessage = "";
        public bool isMachSupLoad = false;
        public Instument_Master()
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

            IM_Intrument_type.Text = "Caliper";
                 display();
           
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


        public void insert()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Instrument_Master";
                com.Parameters.Add("@IM_Intrument_Name", SqlDbType.VarChar).Value = IM_Intrument_Name.Text.ToString();
                com.Parameters.Add("@IM_Range", SqlDbType.VarChar).Value = IM_Range.Text.ToString();
                com.Parameters.Add("@IM_Instrument_Number", SqlDbType.VarChar).Value = IM_Instrument_Number.Text.ToString();
                com.Parameters.Add("@IM_Frequency", SqlDbType.VarChar).Value = IM_Frequency.Text.ToString();
                com.Parameters.Add("@IM_Make", SqlDbType.VarChar).Value = IM_Make.Text.ToString();
                com.Parameters.Add("@IM_Last_Plan", SqlDbType.VarChar).Value = IM_Last_Plan.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@IM_Last_Actual", SqlDbType.VarChar).Value = IM_Last_Actual.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@IM_Last_Status", SqlDbType.VarChar).Value = IM_Last_Status.Text.ToString();
                com.Parameters.Add("@IM_Next_Plan", SqlDbType.VarChar).Value = IM_Next_Plan.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@IM_Next_Actual", SqlDbType.VarChar).Value = IM_Next_Actual.Text.ToString();
                com.Parameters.Add("@IM_Remarks", SqlDbType.VarChar).Value = IM_Remarks.Text.ToString();
                com.Parameters.Add("@IM_Intrument_type", SqlDbType.VarChar).Value = IM_Intrument_type.Text.ToString();
                com.Parameters.Add("@IM_Lesscounr", SqlDbType.VarChar).Value = IM_Lesscounr.Text.ToString();
                com.Parameters.Add("@IM_User", SqlDbType.VarChar).Value = dbFunctions.username;
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
                com.CommandText = "Pr_Update_Instrument_Master";
                com.Parameters.Add("@IM_iid", SqlDbType.VarChar).Value = ID;

                com.Parameters.Add("@IM_Intrument_Name", SqlDbType.VarChar).Value = IM_Intrument_Name.Text.ToString();
                com.Parameters.Add("@IM_Range", SqlDbType.VarChar).Value = IM_Range.Text.ToString();
                com.Parameters.Add("@IM_Instrument_Number", SqlDbType.VarChar).Value = IM_Instrument_Number.Text.ToString();
                com.Parameters.Add("@IM_Frequency", SqlDbType.VarChar).Value = IM_Frequency.Text.ToString();
                com.Parameters.Add("@IM_Make", SqlDbType.VarChar).Value = IM_Make.Text.ToString();
                com.Parameters.Add("@IM_Last_Plan", SqlDbType.VarChar).Value = IM_Last_Plan.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@IM_Last_Actual", SqlDbType.VarChar).Value = IM_Last_Actual.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@IM_Last_Status", SqlDbType.VarChar).Value = IM_Last_Status.Text.ToString();
                com.Parameters.Add("@IM_Next_Plan", SqlDbType.VarChar).Value = IM_Next_Plan.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@IM_Next_Actual", SqlDbType.VarChar).Value = IM_Next_Actual.Text.ToString();
                com.Parameters.Add("@IM_Remarks", SqlDbType.VarChar).Value = IM_Remarks.Text.ToString();
                com.Parameters.Add("@IM_User", SqlDbType.VarChar).Value = dbFunctions.username;
                com.Parameters.Add("@IM_Intrument_type", SqlDbType.VarChar).Value = IM_Intrument_type.Text.ToString();
                com.Parameters.Add("@IM_Lesscounr", SqlDbType.VarChar).Value = IM_Lesscounr.Text.ToString();
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
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Instrument_Master_ByID  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["IM_iid"].ToString();
                IM_Intrument_Name.Text = dt.Rows[0]["IM_Intrument_Name"].ToString();
                IM_Range.Text = dt.Rows[0]["IM_Range"].ToString();
                IM_Instrument_Number.Text = dt.Rows[0]["IM_Instrument_Number"].ToString();
                IM_Frequency.Text = dt.Rows[0]["IM_Frequency"].ToString();
                IM_Make.Text = dt.Rows[0]["IM_Make"].ToString();
                IM_Last_Plan.Text = dt.Rows[0]["IM_Last_Plan"].ToString();
                IM_Last_Actual.Text = dt.Rows[0]["IM_Last_Actual"].ToString();
                IM_Last_Status.Text = dt.Rows[0]["IM_Last_Status"].ToString();
                IM_Next_Plan.Text = dt.Rows[0]["IM_Next_Plan"].ToString();
                IM_Next_Actual.Text = dt.Rows[0]["IM_Next_Actual"].ToString();
                IM_Remarks.Text = dt.Rows[0]["IM_Remarks"].ToString();
                IM_Lesscounr.Text = dt.Rows[0]["IM_Lesscounr"].ToString();
                
                IM_Intrument_type.Text = dt.Rows[0]["IM_Intrument_type"].ToString();
               // IM_User.Text = dt.Rows[0]["IM_User"].ToString();
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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_Instrument_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Instrument_Master_Details '"+IM_Intrument_type.Text+"'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }


        public void Clear()
        {
            IM_Intrument_Name.Text = "";
            IM_Range.Text = "";
            IM_Instrument_Number.Text = "";
            IM_Frequency.Text = "1 Year";
            IM_Make.Text = "";
            IM_Last_Plan.Text = "";
            IM_Last_Actual.Text = "";
            IM_Last_Status.Text = "";
            IM_Next_Plan.Text = "";
            IM_Next_Actual.Text = "";
            IM_Remarks.Text = "";
            IM_Lesscounr.Text = "";
           // IM_User.Text = "";
            btnsave.Text = "&Save    ";
        }


        public bool Validate()
        {
            if ((string.IsNullOrEmpty(IM_Intrument_Name.Text.Trim())))
            {
                ErrorMessage = "Intrument_Name Should Not be Empty";
                IM_Intrument_Name.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(IM_Range.Text.Trim())))
            {
                ErrorMessage = "Range Should Not be Empty";
                IM_Range.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(IM_Instrument_Number.Text.Trim())))
            {
                ErrorMessage = "Instrument_Number Should Not be Empty";
                IM_Instrument_Number.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(IM_Frequency.Text.Trim())))
            {
                ErrorMessage = "Frequency Should Not be Empty";
                IM_Frequency.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(IM_Make.Text.Trim())))
            {
                ErrorMessage = "Make Should Not be Empty";
                IM_Make.Focus();
                return true;
            }
            return false;
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

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IM_Intrument_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            display();
        }

        private void IM_Make_TextChanged(object sender, EventArgs e)
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Intrument_Name] LIKE '%{0}%' OR [Instrument_Number] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}