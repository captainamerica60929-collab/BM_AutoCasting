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

    public partial class Requirement : Form
    {
        public string arrow = "Up";
        public int Distance = 106;
        public string ID = "";
        string ErrorMessage = "";
        public Requirement()
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
            PT_PaymentTerms.Focus();
            

        }



        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {           
            
             if(Validate())
            {
                MessageBox.Show(ErrorMessage,"Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
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
                com.CommandText = "Pr_Insert_PaymentTerms_Master";
                com.Parameters.Add("@PT_PaymentTerms", SqlDbType.VarChar).Value = PT_PaymentTerms.Text.ToString();
                com.Parameters.Add("@PT_User", SqlDbType.VarChar).Value = dbFunctions.username;
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
                com.CommandText = "Pr_Update_PaymentTerms_Master";
                com.Parameters.Add("@PT_ID", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@PT_PaymentTerms", SqlDbType.VarChar).Value = PT_PaymentTerms.Text.ToString();
                com.Parameters.Add("@PT_User", SqlDbType.VarChar).Value = dbFunctions.username;
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
                DataTable dt = dbFunctions.getTable("Pr_Fetch_PaymentTerms_Master_ByID  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["PT_ID"].ToString();
                PT_PaymentTerms.Text = dt.Rows[0]["PT_PaymentTerms"].ToString();
               
                btnsave.Text = "&Update";
                PT_PaymentTerms.Focus();
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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_PaymentTerms_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("Pr_Fetch_PaymentTerms_Master_Details");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            txt_Rows.Text = dbFunctions.getRows(dataGridView1);
        }


        public void Clear()
        {
            PT_PaymentTerms.Text = "";
            btnsave.Text = "&Save    ";
            PT_PaymentTerms.Focus();

        }


        public bool Validate()
        {
            if ((string.IsNullOrEmpty(PT_PaymentTerms.Text.Trim())))
            {
                ErrorMessage = "PaymentTerms Should Not be Empty";
                PT_PaymentTerms.Focus();
                return true;
            }
          
            return false;
        }

        private void Radio_Active_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[PaymentTerms] LIKE '%{0}%'", textBoxX1.Text);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void statusStrip1_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void PaymentTerms_KeyDown(object sender, KeyEventArgs e)
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