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
    public partial class BreakDowns : Form
    {
        public BreakDowns()
        {
            InitializeComponent();
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
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
                com.CommandText = "Pr_Insert_Breakdown";
                com.Parameters.Add("@BD_Breakdown_Type", SqlDbType.VarChar).Value = BD_Breakdown_Type.Text.ToString();
                com.Parameters.Add("@BD_Mould_ID", SqlDbType.VarChar).Value = BD_Mould_ID.SelectedValue.ToString();
                com.Parameters.Add("@BD_Mould_Name", SqlDbType.VarChar).Value = BD_Mould_ID.Text.ToString();
                com.Parameters.Add("@BD_Problem_Description", SqlDbType.VarChar).Value = BD_Problem_Description.Text.ToString();
                com.Parameters.Add("@BD_Problem_Datetime", SqlDbType.VarChar).Value = BD_Problem_Datetime.Value.ToString("dd-MMM-yyyy hh:mm tt");
                com.Parameters.Add("@BD_Reported_By", SqlDbType.VarChar).Value = BD_Reported_By.Text.ToString();
                com.Parameters.Add("@BD_Createdby", SqlDbType.VarChar).Value = dbFunctions.username;

                com.Parameters.Add("@BD_MoldName", SqlDbType.VarChar).Value = BD_MoldName.Text.ToString();
                com.Parameters.Add("@BD_M_Department", SqlDbType.VarChar).Value = BD_M_Department.Text.ToString();
                com.Parameters.Add("@BD_ActionTaken", SqlDbType.VarChar).Value = BD_ActionTaken.Text.ToString();
                com.Parameters.Add("@BD_SpareUsed", SqlDbType.VarChar).Value = BD_SpareUsed.Text.ToString();
                com.Parameters.Add("@BD_FromTime", SqlDbType.DateTime).Value = BD_FromTime.Text.ToString();
                com.Parameters.Add("@BD_ToTime", SqlDbType.DateTime).Value = BD_ToTime.Text.ToString();
                com.Parameters.Add("@BD_Machine_id", SqlDbType.VarChar).Value = Machinename.SelectedValue.ToString();
                com.Parameters.Add("@BD_Machine_name", SqlDbType.VarChar).Value = Machinename.Text.ToString();
                com.Parameters.Add("@BD_SpareCast ", SqlDbType.VarChar).Value = BD_SpareCast.Text.ToString();
                com.Parameters.Add("@root_cause ", SqlDbType.VarChar).Value = root_cause.Text.ToString(); 
                com.Parameters.Add("@BD_STATUSDE ", SqlDbType.VarChar).Value = comboBox1.Text.ToString();
                com.Parameters.Add("@bd_remarks ", SqlDbType.VarChar).Value = remarks.Text.ToString();


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

        string ID = "";

        public void Update()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Breakdown";
                com.Parameters.Add("@BD_iid", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@BD_Breakdown_Type", SqlDbType.VarChar).Value = BD_Breakdown_Type.Text.ToString();
                com.Parameters.Add("@BD_Mould_ID", SqlDbType.VarChar).Value = BD_Mould_ID.SelectedValue.ToString();
                com.Parameters.Add("@BD_Mould_Name", SqlDbType.VarChar).Value = BD_Mould_ID.Text.ToString();
                com.Parameters.Add("@BD_Problem_Description", SqlDbType.VarChar).Value = BD_Problem_Description.Text.ToString();
                com.Parameters.Add("@BD_Problem_Datetime", SqlDbType.VarChar).Value = BD_Problem_Datetime.Value.ToString("dd-MMM-yyyy hh:mm tt");
                com.Parameters.Add("@BD_Reported_By", SqlDbType.VarChar).Value = BD_Reported_By.Text.ToString();
                com.Parameters.Add("@BD_Createdby", SqlDbType.VarChar).Value = dbFunctions.username;

                com.Parameters.Add("@BD_SpareCast", SqlDbType.VarChar).Value = BD_SpareCast.Text.ToString();
                

                com.Parameters.Add("@BD_MoldName", SqlDbType.VarChar).Value = BD_MoldName.Text.ToString();
                com.Parameters.Add("@BD_M_Department", SqlDbType.VarChar).Value = BD_M_Department.Text.ToString();
                com.Parameters.Add("@BD_ActionTaken", SqlDbType.VarChar).Value = BD_ActionTaken.Text.ToString();
                com.Parameters.Add("@BD_SpareUsed", SqlDbType.VarChar).Value = BD_SpareUsed.Text.ToString();
                com.Parameters.Add("@BD_FromTime", SqlDbType.DateTime).Value = BD_FromTime.Text.ToString();
                com.Parameters.Add("@BD_ToTime", SqlDbType.DateTime).Value = BD_ToTime.Text.ToString();
                com.Parameters.Add("@root_cause", SqlDbType.VarChar).Value = root_cause.Text.ToString();
                com.Parameters.Add("@bd_remarks", SqlDbType.VarChar).Value = remarks.Text.ToString();

                

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
              //  BD_Mould_ID.Text = dataGridView1.SelectedRows[0]["Mould_No"].ToString();
               
              //  BD_MoldName.Text = dataGridView1.SelectedRows[0].Cells["Mould Name"].Value.ToString();
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Breakdown_ByID  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["BD_iid"].ToString();
                BD_Breakdown_Type.Text = dt.Rows[0]["BD_Breakdown_Type"].ToString();
                BD_Mould_ID.SelectedValue = dt.Rows[0]["BD_Mould_ID"].ToString();
                //BD_Mould_Name.Text = dt.Rows[0]["BD_Mould_Name"].ToString();
                BD_Problem_Description.Text = dt.Rows[0]["BD_Problem_Description"].ToString();
                BD_Problem_Datetime.Text = dt.Rows[0]["BD_Problem_Datetime"].ToString();
                BD_Reported_By.Text = dt.Rows[0]["BD_Reported_By"].ToString();
                BD_SpareCast.Text = dt.Rows[0]["BD_SpareCast"].ToString();
                BD_MoldName.Text = dt.Rows[0]["BD_MoldName"].ToString();
                BD_M_Department.Text = dt.Rows[0]["BD_M_Department"].ToString();
                BD_ActionTaken.Text = dt.Rows[0]["BD_ActionTaken"].ToString();
                BD_SpareUsed.Text = dt.Rows[0]["BD_SpareUsed"].ToString();
                BD_FromTime.Text = dt.Rows[0]["BD_FromTime"].ToString();
                BD_ToTime.Text = dt.Rows[0]["BD_ToTime"].ToString();
                root_cause.Text = dt.Rows[0]["root_cause"].ToString();
                Machinename.Text = dt.Rows[0]["BD_Machine_name"].ToString();
                comboBox1.Text = dt.Rows[0]["BD_STATUSDE"].ToString();
                remarks.Text = dt.Rows[0]["bd_remarks"].ToString();
                BD_Mould_ID.Text = dataGridView1.SelectedRows[0].Cells["Mould_No"].Value.ToString();
                

                //    BD_Createdby.Text = dt.Rows[0]["BD_Createdby"].ToString();
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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_Breakdown " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Breakdown_Details");
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Mould_ID"].Visible = false;
            dbFunctions.DGVStyle(dataGridView1);
        }


        public void Clear()
        {
            BD_Breakdown_Type.Text = "";
            BD_Mould_ID.Text = "";
            //BD_Mould_Name.Text = "";
            BD_Problem_Description.Text = "";
            root_cause.Text = "";
            BD_SpareCast.Text = "";
            BD_Problem_Datetime.Text = "";
            remarks.Text = "";
            BD_Reported_By.Text = "";
           // BD_Createdby.Text = "";
            BD_MoldName.Text = "";
            BD_ActionTaken.Text = "";
            BD_SpareUsed.Text = "";
            BD_M_Department.Text = "";
             
            btnsave.Text = "&Save    ";
        }


        public bool Validate()
        {
            return false;
        }

        private void BreakDowns_Load(object sender, EventArgs e)
        {
            display();
            Load_Mould();
            //LoadMoulsNAme();
        }

        public void LoadMoulsNAme()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_GetMoldNumber '" + BD_MoldName.SelectedValue.ToString() + "'");
                BD_Mould_ID.DataSource = dt;
                BD_Mould_ID.DisplayMember = "MLD_MouldNo";
                BD_Mould_ID.ValueMember = "MLD_ID";
                BD_Mould_ID.SelectedIndex = -1;
            }
            catch
            {
            }
        }

        public void Load_Mould()
        {
            if (BD_Breakdown_Type.Text.ToString().Equals("Mould"))
            {
                DataTable dt = dbFunctions.getTable("pr_GetMoldName");
                BD_MoldName.DataSource = dt;
                BD_MoldName.DisplayMember = "MLD_PartName";
                BD_MoldName.ValueMember = "MLD_ID";
                BD_MoldName.SelectedIndex = -1;
                isBR = true;
            }
            else
            {
                DataTable dt = dbFunctions.getTable("pr_GetMachineName");
                Machinename.DataSource = dt;
                Machinename.DisplayMember = "MM_MachineCode";
                Machinename.ValueMember = "MM_ID";
                Machinename.SelectedIndex = -1;
                isBR = true;

            }
           
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Delete();
        }
        public bool isBR = false;
        private void BD_MoldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(isBR)
            {
            LoadMoulsNAme();
            }
        }

        private void BD_Mould_ID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BD_Breakdown_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Mould();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Mould Name] LIKE '%{0}%' OR [SpareUsed] LIKE '%{0}%' OR [Mould_No] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            DataTable A = dbFunctions.getTable("SELECT MCS_ID,(MCS_Sparename + '/'  + MCS_SpareSize ) as [MCS_Sparename]  FROM Mold_Critical_Spares WHERE (MCS_Status = 'A' OR MCS_Status = 'I')   AND MCS_Type='Mould'");
            cts_name.DataSource = A;
            cts_name.DisplayMember = "MCS_Sparename";
            cts_name.ValueMember = "MCS_ID";
            cts_name.SelectedIndex = -1;
            display1();
        }

        private void display1()
        {
            DataTable a = dbFunctions.getTable("pr_load_Mould_details1 '" + BD_Breakdown_Type.Text + "','" + Machinename.Text + "'");
            dataGridView3.DataSource = a;
            dbFunctions.DGVStyle(dataGridView3);
        }
        String ID1 = "0";
        String ID2 = "0";
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                if (ID1 == "0")
                {
                    DataTable dd = dbFunctions.getTable("select isnull(max(cts_id),0)+1 from cts_usage");
                    if (dd.Rows.Count > 0)
                    {
                        ID1 = dd.Rows[0][0].ToString();
                    }
                    DataTable dd1 = dbFunctions.getTable("select isnull(max(BD_iid),0)+1 from Breakdown");
                    if (dd1.Rows.Count > 0)
                    {
                        ID2 = dd1.Rows[0][0].ToString();
                    }
                    com.CommandText = "INSERT INTO cts_usage (cts_id, cts_name_id, cts_qty, cts_create_user_name, cts_create_getdate, cts_status, cts_usage,cts_frequency,cts_machine_name,cts_machine_id,cts_remarks,cts_cs_status,BD_MID) " +
                       "VALUES (@cts_id, @cts_name_id, @cts_qty, @cts_create_user_name, GETDATE(), @cts_status, @cts_usage,@cts_frequency,@cts_machine_name,@cts_machine_id,@cts_remarks,@cts_cs_status,@BD_MID)";
                }
                else
                {
                    com.CommandText = "UPDATE cts_usage " +
                      "SET cts_name_id = @cts_name_id, " +
                      "cts_qty = @cts_qty, " +
                      "cts_status = @cts_status, " +
                      "cts_usage = @cts_usage, " +
                      "cts_del_user_name = @cts_del_user_name, " +
                      "cts_delete_getdate = GETDATE() " +
                      "cts_remarks = @cts_remarks " +
                      "WHERE cts_id = @cts_id";
                    //Type = "EDIT";

                }

                com.Parameters.Add("@cts_id", SqlDbType.VarChar).Value = ID1.ToString();
                com.Parameters.Add("@BD_MID", SqlDbType.VarChar).Value = ID2.ToString();
                com.Parameters.Add("@cts_name_id", SqlDbType.VarChar).Value = cts_name.SelectedValue.ToString();
                com.Parameters.Add("@cts_qty", SqlDbType.Int).Value = qty.Text.ToString();
                com.Parameters.Add("@cts_create_user_name", SqlDbType.VarChar).Value = dbFunctions.username;
                //com.Parameters.Add("@cts_create_getdate", SqlDbType.VarChar).Value = textBox2.Text;
                com.Parameters.Add("@cts_status", SqlDbType.VarChar).Value = "A";
                com.Parameters.Add("@cts_usage", SqlDbType.VarChar).Value = "Mould";
                com.Parameters.Add("@cts_frequency", SqlDbType.VarChar).Value = BD_Breakdown_Type.Text;
                com.Parameters.Add("@cts_machine_name", SqlDbType.VarChar).Value = Machinename.Text;
                com.Parameters.Add("@cts_remarks", SqlDbType.VarChar).Value = cts_remarks.Text;
                com.Parameters.Add("@cts_machine_id", SqlDbType.VarChar).Value = Machinename.SelectedValue.ToString();
                com.Parameters.Add("@cts_cs_status", SqlDbType.VarChar).Value = "MDBD";



                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ID1 = "0";


            }
            catch (Exception Ex)
            {
                //dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            display1();
            clear1();
        }
        private void clear1()
        {
            qty.Text = "";
            cts_name.Text = "";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("update cts_usage set cts_status='D',cts_del_user_name='" + dbFunctions.username + "',cts_delete_getdate =GETDATE() where cts_id='" + dataGridView3.SelectedRows[0].Cells["Id"].Value.ToString() + "'");
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    display1();
                    //clear1();

                }
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        int remaining = 0;
        private void qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int qty1 = Convert.ToInt32(qty.Text);  // Fix casing (qty.Text instead of qty.text)

                DataTable a = dbFunctions.getTable(@"
                    SELECT 
                        SUM(MCS_Actual) - ISNULL((
                            SELECT SUM(cts_qty) 
                            FROM [CHE_SRUTHY_PLASTIC].[cts_usage] 
                            WHERE cts_name_id = " + cts_name.SelectedValue.ToString() + @" AND cts_status = 'A'
                        ), 0) AS Remaining
                    FROM Mold_Critical_Spares 
                    WHERE MCS_Status = 'A' AND MCS_ID = " + cts_name.SelectedValue.ToString()
                );

                // Check if there's a result and column exists
                if (a.Rows.Count > 0 && a.Columns.Contains("Remaining"))
                {
                    remaining = Convert.ToInt32(a.Rows[0]["Remaining"]);

                    if (qty1 > remaining)
                    {
                        MessageBox.Show("Entered stock quantity is more than available quantity (" + remaining + ").", "Stock Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        qty.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Entered stock quantity is more than available quantity (" + remaining + ").", "Stock Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }
    }
}
