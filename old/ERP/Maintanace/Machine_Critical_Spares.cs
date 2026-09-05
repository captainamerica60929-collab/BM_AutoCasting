using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using LarchERP.Master;
using CRM_App.Production;
using System.Data.SqlClient;

namespace CRM_App.Maintanace
{
    public partial class Machine_Critical_Spares : Form
    {
        public Machine_Critical_Spares()
        {
            InitializeComponent();
        }

        string ErrorMessage = "";
        string ID = "";

        private void Mould_Maintenance_List_Load(object sender, EventArgs e)
        {
            MCS_Type.Text = "Mould";
            display();
        }

        private void radio_Today_List_CheckedChanged(object sender, EventArgs e)
        {
            display();


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

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
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
                com.CommandText = "Pr_Insert_Mold_Critical_Spares";
                com.Parameters.Add("@MCS_Sparename", SqlDbType.VarChar).Value = MCS_Sparename.Text.ToString();
                com.Parameters.Add("@MCS_Type", SqlDbType.VarChar).Value = MCS_Type.Text.ToString();

                com.Parameters.Add("@MCS_SpareSize", SqlDbType.VarChar).Value = MCS_SpareSize.Text.ToString();
                com.Parameters.Add("@MCS_Min", SqlDbType.VarChar).Value = MCS_Min.Text.ToString();
                com.Parameters.Add("@MCS_Max", SqlDbType.VarChar).Value = MCS_Max.Text.ToString();
                com.Parameters.Add("@MCS_Actual", SqlDbType.VarChar).Value = MCS_Actual.Text.ToString();
                com.Parameters.Add("@MCS_Remarks", SqlDbType.VarChar).Value = MCS_Remarks.Text.ToString();
                com.Parameters.Add("@MCS_Cost", SqlDbType.Decimal).Value = MCS_Cost.Text.ToString();
                com.Parameters.Add("@MCS_CreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;
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
                com.CommandText = "Pr_Update_Mold_Critical_Spares";
                com.Parameters.Add("@MCS_ID", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@MCS_Sparename", SqlDbType.VarChar).Value = MCS_Sparename.Text.ToString();
                com.Parameters.Add("@MCS_SpareSize", SqlDbType.VarChar).Value = MCS_SpareSize.Text.ToString();
                com.Parameters.Add("@MCS_Min", SqlDbType.VarChar).Value = MCS_Min.Text.ToString();
                com.Parameters.Add("@MCS_Max", SqlDbType.VarChar).Value = MCS_Max.Text.ToString();
                com.Parameters.Add("@MCS_Actual", SqlDbType.VarChar).Value = MCS_Actual.Text.ToString();
                com.Parameters.Add("@MCS_Cost", SqlDbType.Decimal).Value = MCS_Cost.Text.ToString();
                com.Parameters.Add("@MCS_Remarks", SqlDbType.VarChar).Value = MCS_Remarks.Text.ToString();
                com.Parameters.Add("@MCS_CreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;
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
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Mold_Critical_Spares_ByID  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["MCS_ID"].ToString();
                MCS_Sparename.Text = dt.Rows[0]["MCS_Sparename"].ToString();
                MCS_SpareSize.Text = dt.Rows[0]["MCS_SpareSize"].ToString();
                MCS_Min.Text = dt.Rows[0]["MCS_Min"].ToString();
                MCS_Cost.Text = dt.Rows[0]["MCS_Cost"].ToString();
                MCS_Max.Text = dt.Rows[0]["MCS_Max"].ToString();
                MCS_Actual.Text = dt.Rows[0]["MCS_Actual"].ToString();
                MCS_Remarks.Text = dt.Rows[0]["MCS_Remarks"].ToString();
                //MCS_CreatedBy.Text = dt.Rows[0]["MCS_CreatedBy"].ToString();
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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_Mold_Critical_Spares " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Mold_Critical_Spares_Details  '"+MCS_Type.Text+"'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }


        public void Clear()
        {
            MCS_Sparename.Text = "";
            MCS_SpareSize.Text = "";
            MCS_Min.Text = "";
            MCS_Max.Text = "";
            MCS_Actual.Text = "";
            MCS_Remarks.Text = "";
            MCS_Cost.Text = "";
            //MCS_CreatedBy.Text = "";
            btnsave.Text = "&Save    ";
        }


        public bool Validate()
        {
            if ((string.IsNullOrEmpty(MCS_Sparename.Text.Trim())))
            {
                ErrorMessage = "Spare Name Should Not be Empty";
                MCS_Sparename.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(MCS_SpareSize.Text.Trim())))
            {
                ErrorMessage = "Spare Size Should Not be Empty";
                MCS_SpareSize.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(MCS_Min.Text.Trim())))
            {
                ErrorMessage = "Min Should Not be Empty";
                MCS_Min.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(MCS_Max.Text.Trim())))
            {
                ErrorMessage = "Max Should Not be Empty";
                MCS_Max.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(MCS_Actual.Text.Trim())))
            {
                ErrorMessage = "Actual Should Not be Empty";
                MCS_Actual.Focus();
                return true;
            }
            return false;
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
                MCS_Sparename.Focus();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Sparename] LIKE '%{0}%' OR [SpareSize] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MCS_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            display();
        }

    }
}
