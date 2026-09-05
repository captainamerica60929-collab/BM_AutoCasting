using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using CRM_App.Crystal;

namespace LarchERP.Master
{

    public partial class Calibration_List : Form
    {
        public string arrow = "Up";
        public int Distance = 220;
        public string ID = "";
        string ErrorMessage = "";
        public bool isMachSupLoad = false;
        public Calibration_List()
        {
            InitializeComponent();
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void Calibration_List_Load(object sender, EventArgs e)
        {
           
           
            get_Type();
            get_ins();

            display();
        }
        public void get_Type()
        {
            DataTable dt = dbFunctions.getTable("pr_get_Ins_Type  ");
            INs_Type.DataSource = dt;
            INs_Type.DisplayMember = "IT_Type";
            INs_Type.ValueMember = "IT_Type";
            INs_Type.SelectedIndex = 0;
        }

        public void get_ins()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_Instrument1   '"+INs_Type.Text+"'");
                CL_iIntrument_ID.DataSource = dt;
                CL_iIntrument_ID.DisplayMember = "IM_Instrument_Number";
                CL_iIntrument_ID.ValueMember = "IM_iid";
                CL_iIntrument_ID.SelectedIndex = -1;
            }
            catch { }
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
                com.CommandText = "Pr_Insert_Calibration_List";
                com.Parameters.Add("@CL_iIntrument_ID", SqlDbType.VarChar).Value = CL_iIntrument_ID.SelectedValue.ToString();
                com.Parameters.Add("@CL_Plan", SqlDbType.VarChar).Value = CL_Plan.Text.ToString();
                com.Parameters.Add("@CL_Actual", SqlDbType.VarChar).Value = CL_Actual.Text.ToString();
                com.Parameters.Add("@CL_Next", SqlDbType.VarChar).Value = CL_Next.Text.ToString();
                com.Parameters.Add("@CL_Remarks", SqlDbType.VarChar).Value = CL_Remarks.Text.ToString();
                com.Parameters.Add("@CL_Created_by", SqlDbType.VarChar).Value = dbFunctions.username;
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
                com.CommandText = "Pr_Update_Calibration_List";
                com.Parameters.Add("@CL_iid", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@CL_iIntrument_ID", SqlDbType.VarChar).Value = CL_iIntrument_ID.SelectedValue.ToString();
                com.Parameters.Add("@CL_Plan", SqlDbType.VarChar).Value = CL_Plan.Text.ToString();
                com.Parameters.Add("@CL_Actual", SqlDbType.VarChar).Value = CL_Actual.Text.ToString();
                com.Parameters.Add("@CL_Next", SqlDbType.VarChar).Value = CL_Next.Text.ToString();
                com.Parameters.Add("@CL_Remarks", SqlDbType.VarChar).Value = CL_Remarks.Text.ToString();
                com.Parameters.Add("@CL_Created_by", SqlDbType.VarChar).Value = dbFunctions.username;
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
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Calibration_List_ByID  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["CL_iid"].ToString();
                CL_iIntrument_ID.SelectedValue = dt.Rows[0]["CL_iIntrument_ID"].ToString();
                CL_Plan.Text = dt.Rows[0]["CL_Plan"].ToString();
                CL_Actual.Text = dt.Rows[0]["CL_Actual"].ToString();
                CL_Next.Text = dt.Rows[0]["CL_Next"].ToString();
                CL_Remarks.Text = dt.Rows[0]["CL_Remarks"].ToString();
             //   CL_Created_by.Text = dt.Rows[0]["CL_Created_by"].ToString();
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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_Calibration_List " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Calibration_List_Details '"+INs_Type.Text+"'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }


        public void Clear()
        {
            CL_iIntrument_ID.Text = "";
            CL_Plan.Text = "";
            CL_Actual.Text = "";
            CL_Next.Text = "";
            CL_Remarks.Text = "";
         
            btnsave.Text = "&Save    ";
        }


        public bool Validate()
        {
            return false;
        }

        private void CL_iIntrument_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Instrument_Master_ByID  " + CL_iIntrument_ID.SelectedValue.ToString());
               // ID = dt.Rows[0]["IM_iid"].ToString();
                
                IM_Range.Text = dt.Rows[0]["IM_Range"].ToString();
                IM_Instrument_Number.Text = dt.Rows[0]["IM_Instrument_Number"].ToString();
                IM_Frequency.Text = dt.Rows[0]["IM_Frequency"].ToString();
                textBox1.Text = dt.Rows[0]["IM_Lesscounr"].ToString();
            }
            catch { }
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

        private void btnDisplay_Click(object sender, EventArgs e)
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

        private void CL_Actual_ValueChanged(object sender, EventArgs e)
        {
            CL_Next.Value = CL_Actual.Value.AddYears(1);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Calibration_List ObjSupplier_Perfomanace = new Calibration_List();
            BodyPanel.Dock = DockStyle.Fill;
            BodyPanel.Visible = true;
            BodyPanel.Controls.Clear();
            if (ObjSupplier_Perfomanace.IsDisposed)
            {
                ObjSupplier_Perfomanace = new Calibration_List();
            }
            ObjSupplier_Perfomanace.TopLevel = false;
            ObjSupplier_Perfomanace.FormBorderStyle = FormBorderStyle.None;
            ObjSupplier_Perfomanace.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjSupplier_Perfomanace);
            ObjSupplier_Perfomanace.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
                try
            {
                Cursor.Current = Cursors.WaitCursor;
                Calibration_Plan_Vs_Actual oRpt = new Calibration_Plan_Vs_Actual();
                string SQlQuery = "pr_get_CalibrationPlan '" + dateTimePicker1.Text + "','" + INs_Type.Text+"'";
                dbFunctions.printpdf("Plan_Vs_Actual", SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }

        private void INs_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            display();
            get_ins();
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Intrument Name] LIKE '%{0}%' OR [Intrument No] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}