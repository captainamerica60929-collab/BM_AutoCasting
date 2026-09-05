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

    public partial class Preventive_Matainance1 : Form
    {

        public bool isPreventiveMatainanceLoad = false;
        public Preventive_Matainance1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MMD_Mould_ID_SelectedIndexChanged(object sender, EventArgs e)
        {

            //  DataTable dt = dbFunctions.getTable("pr_Get_LoadMouldDetails1 '" + MRM_Part_No.SelectedValue.ToString() + "'");

            //DataTable dt1 = dbFunctions.getTable(" select MM_MachineName from Machine_Master where MM_MachineCode ='" + MPM_Part_No.Text + "'");
             DataTable dt1 = dbFunctions.getTable("SELECT MM_MachineName FROM Machine_Master WHERE MM_MachineCode = '"+ MPM_Part_No.Text.Trim() + "'");
           // DataTable dt1 = dbFunctions.getTable("SELECT MM_MachineName FROM Machine_Master WHERE MM_MachineCode = '"+ MPM_Part_No.SelectedValue.ToString() + "'");
            if (dt1.Rows.Count > 0)
                {
                    machine_name.Text = dt1.Rows[0]["MM_MachineName"].ToString();
                }
            
                //// if (ispartLoad)
                // {
                //     ismould = true;
                //     try
                //     {


                //         DataTable dt = dbFunctions.getTable("pr_Get_LoadMouldDetails1 '" + MPM_Part_No.SelectedValue.ToString() + "'");
                //         MPM_Mould_Id.DataSource = dt;
                //         MPM_Mould_Id.DisplayMember = "MLD_MouldNo";
                //         MPM_Mould_Id.ValueMember = "MLD_ID";
                //         MPM_Mould_Id.SelectedIndex = -1;
                //         ismould = true;
                //         dataGridView1.DataSource = null;
                //         MPM_CUM_Qty.Text = "";
                //         textBox1.Text = "";

                //     }
                //     catch { }
                // }
            }

        private void Preventive_Matainance_Load(object sender, EventArgs e)
        {
            LoadMachineName();
           // LoadMouldName();
            Remarks.Height = 100; // or however much you need



        }


        bool ispartLoad = false;
        bool ismould = false;
        public void LoadMouldName()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_LoadMouldName1");
                MPM_Part_No.DataSource = dt;
                MPM_Part_No.DisplayMember = "MM_MachineCode";
                MPM_Part_No.ValueMember = "MM_ID";
                MPM_Part_No.SelectedIndex = -1;
                ispartLoad = true;
               // ismould = false;
            }
            catch
            {
            }
        }
        public void LoadMachineName()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_LoadMachine");
                MPM_Part_No.DataSource = dt;
                MPM_Part_No.DisplayMember = "MM_MachineCode";
                MPM_Part_No.ValueMember = "MM_ID";
                MPM_Part_No.SelectedIndex = -1;
                ispartLoad = true;
                // ismould = false;
            }
            catch
            {
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dbFunctions.isPM_data = false;
            this.Close();

        }

        private void MPM_Mould_Id_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtx = dbFunctions.getTable("pr_get_Machine_Shorts  " + MPM_Part_No.SelectedValue + "," + MPM_Mould_Id.SelectedValue.ToString());
            if (dtx.Rows.Count > 0)
            {
                textBox2.Text = dtx.Rows[0]["Date"].ToString();
                textBox3.Text = dtx.Rows[0]["Type"].ToString();
               
            }
            
        }

        private void Preventive_Matainance_Shown(object sender, EventArgs e)
        {
            if (dbFunctions.isPM_data == true)
            {
                LoadMouldName();
                
                MPM_Part_No.Text = dbFunctions.Mould_Name;
                MPM_Mould_Id.SelectedValue = dbFunctions.Mould_ID;
                DataTable dt = dbFunctions.getTable("pr_Get_PreventiveMainatanceDetails '" + dbFunctions.Mould_ID + "'");
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["CP_Checkpoint_Name"].ReadOnly = true;
                dbFunctions.DGVStyle(dataGridView1);
            }
        }
        int b = 0;
        private void btnsave_Click(object sender, EventArgs e)
        {
            String a = "";


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Observation"].Value != null && !string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["Observation"].Value.ToString()))
                {

                    b = b + 1;

                }
                else
                {
                    MessageBox.Show("Please enter the value", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }
            if (dataGridView1.Rows.Count == b)
            {

                insert();
            }
            b = 0;
            //insert();
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
                com.CommandText = "Pr_Insert_Mould_Preventive_Maintance";
                com.Parameters.Add("@MPM_Frequency_Type", SqlDbType.VarChar).Value = MPM_Frequency_Type.Text.ToString();
                com.Parameters.Add("@MPM_PM_Date", SqlDbType.DateTime).Value = MPM_PM_Date.Value.ToString("dd-MM-yyyy hh:mm tt");
                com.Parameters.Add("@MPM_END_TIME", SqlDbType.DateTime).Value = MPM_END_TIME.Value.ToString("dd-MM-yyyy hh:mm tt");

                com.Parameters.Add("@MPM_Part_No", SqlDbType.VarChar).Value = MPM_Part_No.Text.ToString();
                com.Parameters.Add("@MPM_Mould_Id", SqlDbType.VarChar).Value = MPM_Part_No.SelectedValue.ToString();
                com.Parameters.Add("@MPM_CUM_Qty", SqlDbType.VarChar).Value = MPM_CUM_Qty.Text.ToString();
                com.Parameters.Add("@MPM_Machine_ID", SqlDbType.VarChar).Value = MPM_Machine_ID.Text.ToString();
                com.Parameters.Add("@MPM_Attend_by", SqlDbType.VarChar).Value = MPM_Attend_by.Text.ToString();
                com.Parameters.Add("@MPM_MMSTATUS", SqlDbType.VarChar).Value = "Machine";
                com.Parameters.Add("@MPM_CreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Remarks.Text.ToString();



                com.Parameters.Add("@ID", SqlDbType.Int);
                com.Parameters["@ID"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery(); com.Connection.Close();
                string POD_PO_No = com.Parameters["@ID"].Value.ToString();

                Save_details(POD_PO_No);
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public void Save_details(string MRMD_MRM_ID)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_Mould_Preventive_Maintance_Details";
                    com.Parameters.Add("@MPMD_MPM_ID", SqlDbType.VarChar).Value = MRMD_MRM_ID;
                    com.Parameters.Add("@MPMD_CheckPart", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Check Point"].Value.ToString();
                    com.Parameters.Add("@MPMD_Requirement", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Action Tacken"].Value.ToString();
                    com.Parameters.Add("@MPMD_OKStatus", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Observation"].Value.ToString();
                    com.Parameters.Add("@MPMD_Not_OK_Staus", SqlDbType.VarChar).Value = "";
                    com.Parameters.Add("@MPMD_Remarks", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                    com.Parameters.Add("@MPMD_CreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;
                    com.ExecuteNonQuery();
                    com.Connection.Close();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Error :" + Ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


            MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();


        }

        private void MPM_Frequency_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ismould)
            //{
                try
                {

                    DataTable dt;
                    if (MPM_Frequency_Type.Text.Equals("1 Month"))
                    {
                        dt = dbFunctions.getTable("pr_Get_Monthly '" + MPM_Part_No.SelectedValue.ToString() + "'");
                    }
                    else if (MPM_Frequency_Type.Text.Equals("3 Month"))
                    {
                        dt = dbFunctions.getTable("pr_Get_3_Monthly '" + MPM_Part_No.SelectedValue.ToString() + "'");
                    }
                    else if (MPM_Frequency_Type.Text.Equals("6 Month"))
                    {
                        dt = dbFunctions.getTable("pr_Get_6_Monthly '" + MPM_Part_No.SelectedValue.ToString() + "'");
                    }
                    else if (MPM_Frequency_Type.Text.Equals("Annual PM"))
                    {
                        dt = dbFunctions.getTable("pr_Get_Annual_Monthly '" + MPM_Part_No.SelectedValue.ToString() + "'");
                    }
                    else if (MPM_Frequency_Type.Text.Equals("overhauling"))
                    {
                        dt = dbFunctions.getTable("pr_Get_2_Year '" + MPM_Part_No.SelectedValue.ToString() + "'");
                    }
                else if (MPM_Frequency_Type.Text.Equals("Predictive PM"))
                {
                    dt = dbFunctions.getTable("pr_Get_Predictive '" + MPM_Part_No.SelectedValue.ToString() + "'");
                }
                else //if (MPM_Frequency_Type.Text.Equals("3 Year"))
                    {
                        dt = dbFunctions.getTable("pr_Get_PreventiveMainatanceDetails151 '" + MPM_Part_No.SelectedValue.ToString() + "'");
                    }

                    dataGridView1.DataSource = dt;
                    dbFunctions.DGVStyle(dataGridView1);
                    dataGridView1.Columns["Observation"].Width = 200;
                dataGridView1.Columns["Check Point"].ReadOnly = true;
                dataGridView1.Columns["Action Tacken"].ReadOnly = true;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;


                    DataTable dtx = dbFunctions.getTable("pr_get_Mould_Shorts  " + MPM_Part_No.SelectedValue + "," + MPM_Mould_Id.SelectedValue.ToString());
                    if (dtx.Rows.Count > 0)
                    {
                        MPM_Machine_ID.Text = dtx.Rows[0]["MM_ID"].ToString();
                        textBox1.Text = dtx.Rows[0]["MM_MachineName"].ToString();
                        MPM_CUM_Qty.Text = dtx.Rows[0]["Shots"].ToString();

                    }


                }
                catch (Exception AS) { }
            ak();
            display1();

            //}
        }

        private void ak()
        {
            //  DataTable dtx1 = dbFunctions.getTable("pr_get_Machine_Shorts  " + MPM_Part_No.SelectedValue + "," + MPM_Mould_Id.SelectedValue.ToString());

            DataTable dtx1 = dbFunctions.getTable(" select * from Mould_Preventive_Maintance where  MPM_Status = 'A' and MPM_MMSTATUS = 'Machine' and MPM_Part_No = '" + MPM_Part_No.Text + "' order by MPM_ID desc ");
            if (dtx1.Rows.Count > 0)
            {
                textBox2.Text = dtx1.Rows[0]["MPM_PM_Date"].ToString();
                textBox3.Text = dtx1.Rows[0]["MPM_Frequency_Type"].ToString();

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            DataTable A = dbFunctions.getTable("SELECT MCS_ID,(MCS_Sparename + '/'  + MCS_SpareSize ) as [MCS_Sparename]  FROM Mold_Critical_Spares WHERE (MCS_Status = 'A' OR MCS_Status = 'I')    AND MCS_Type='Machine'");
            cts_name.DataSource = A;
            cts_name.DisplayMember = "MCS_Sparename";
            cts_name.ValueMember = "MCS_ID";
            cts_name.SelectedIndex = -1;
            display1();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        String ID1 = "0";
        String ID2 = "0";
        private void button1_Click(object sender, EventArgs e)
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
                    DataTable dd1 = dbFunctions.getTable("select isnull(max(MPM_ID),0)+1 from Mould_Preventive_Maintance");
                    if (dd.Rows.Count > 0)
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
                com.Parameters.Add("@cts_usage", SqlDbType.VarChar).Value = "Machine";
                com.Parameters.Add("@cts_frequency", SqlDbType.VarChar).Value = MPM_Frequency_Type.Text; 
                com.Parameters.Add("@cts_machine_name", SqlDbType.VarChar).Value = machine_name.Text; 
                com.Parameters.Add("@cts_remarks", SqlDbType.VarChar).Value = cts_remarks.Text;
                com.Parameters.Add("@cts_machine_id", SqlDbType.VarChar).Value = MPM_Part_No.SelectedValue.ToString();
                com.Parameters.Add("@cts_cs_status", SqlDbType.VarChar).Value = "MDPM";



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

        private void display1()
        {
            DataTable a = dbFunctions.getTable("pr_load_details '"+ MPM_Frequency_Type.Text + "','" + machine_name.Text + "'" );
                  dataGridView3.DataSource = a;
            dbFunctions.DGVStyle(dataGridView3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("update cts_usage set cts_status='D',cts_del_user_name='"+dbFunctions.username+ "',cts_delete_getdate =GETDATE() where cts_id='" + dataGridView3.SelectedRows[0].Cells["Id"].Value.ToString() + "'");
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

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

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

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void Remarks_TextChanged(object sender, EventArgs e)
        {

        }
    }
}