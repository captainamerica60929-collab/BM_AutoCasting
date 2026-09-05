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

    public partial class Shift_Master : Form
    {
        public string arrow = "Up";
        public int Distance = 106;
        public string ID = "";
        string ErrorMessage = "";
        public Shift_Master()
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
            SH_ShiftCode.Focus();

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
                SH_ShiftCode.Focus();
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
                com.CommandText = "Pr_Insert_Shift_Master";
                com.Parameters.Add("@SH_ShiftCode", SqlDbType.VarChar).Value = SH_ShiftCode.Text.ToString();
                com.Parameters.Add("@SH_Shift", SqlDbType.VarChar).Value = SH_Shift.Text.ToString();
                com.Parameters.Add("@SH_Intime", SqlDbType.VarChar).Value = SH_Intime.Text.ToString();
                com.Parameters.Add("@SH_OutTime", SqlDbType.VarChar).Value = SH_OutTime.Text.ToString();
                com.Parameters.Add("@SH_Grace_time", SqlDbType.Int).Value = SH_Grace_time.Text.ToString();
                com.Parameters.Add("@SH_User", SqlDbType.VarChar).Value = dbFunctions.username;
                com.ExecuteNonQuery();  com.Connection.Close();
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
                com.CommandText = "Pr_Update_Shift_Master";
                com.Parameters.Add("@SH_ID", SqlDbType.Int).Value = ID;
                com.Parameters.Add("@SH_ShiftCode", SqlDbType.VarChar).Value = SH_ShiftCode.Text.ToString();
                com.Parameters.Add("@SH_Shift", SqlDbType.VarChar).Value = SH_Shift.Text.ToString();
                com.Parameters.Add("@SH_Intime", SqlDbType.VarChar).Value = SH_Intime.Text.ToString();
                com.Parameters.Add("@SH_OutTime", SqlDbType.VarChar).Value = SH_OutTime.Text.ToString();
                com.Parameters.Add("@SH_Grace_time", SqlDbType.VarChar).Value = SH_Grace_time.Text.ToString();
                com.Parameters.Add("@SH_User", SqlDbType.VarChar).Value = dbFunctions.username;
                com.ExecuteNonQuery();  com.Connection.Close();
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
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Shift_Master_ByID  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["SH_ID"].ToString();
                SH_ShiftCode.Text = dt.Rows[0]["SH_ShiftCode"].ToString();
                SH_Shift.Text = dt.Rows[0]["SH_Shift"].ToString();
                SH_Intime.Text = dt.Rows[0]["SH_Intime"].ToString();
                SH_OutTime.Text = dt.Rows[0]["SH_OutTime"].ToString();
                SH_Grace_time.Text = dt.Rows[0]["SH_Grace_time"].ToString();
               
                btnsave.Text = "&Update";
                SH_ShiftCode.Focus();
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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_Shift_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Shift_Master_Details");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }


        public void Clear()
        {
            SH_ShiftCode.Text = "";
            SH_Shift.Text = "";
            SH_Intime.Text = "";
            SH_OutTime.Text = "";
            SH_Grace_time.Text = "";
        
            btnsave.Text = "&Save    ";
            SH_ShiftCode.Focus();
        }


        public bool Validate()
        {
            if ((string.IsNullOrEmpty(SH_ShiftCode.Text.Trim())))
            {
                ErrorMessage = "ShiftCode Should Not be Empty";
                SH_ShiftCode.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(SH_Shift.Text.Trim())))
            {
                ErrorMessage = "Shift Should Not be Empty";
                SH_Shift.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(SH_Intime.Text.Trim())))
            {
                ErrorMessage = "Intime Should Not be Empty";
                SH_Intime.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(SH_OutTime.Text.Trim())))
            {
                ErrorMessage = "OutTime Should Not be Empty";
                SH_OutTime.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(SH_Grace_time.Text.Trim())))
            {
                ErrorMessage = "Grace_time Should Not be Empty";
                SH_Grace_time.Focus();
                return true;
            }
            return false;
        }


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Shift_Master_InAct");
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
                SH_Shift.Focus();
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Shift] LIKE '%{0}%' or [ShiftCode] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Ty_TypeName_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Shift Code";
        }

        private void SH_Shift_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SH_Intime.Focus();
            }
        }

        private void SH_Intime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SH_OutTime.Focus();
            }
        }

        private void SH_OutTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SH_Grace_time.Focus();
            }
        }

        private void SH_Grace_time_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                btnsave.Focus();
            }
        }

        private void SH_Shift_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Shift Time Here....";
        }

        private void SH_Intime_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please  Choose In Time Here....";
        }

        private void SH_OutTime_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Choose Out Time Here....";
        }

        private void SH_Grace_time_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Grace Time Here....";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Shift_Master_KeyDown(object sender, KeyEventArgs e)
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