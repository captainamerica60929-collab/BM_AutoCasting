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

    public partial class Regular_Matainance1 : Form
    {
        public bool isMoluldLoad = false;
        public bool isRegularMatainanceLoad = false;
       
        public Regular_Matainance1()
        {
            InitializeComponent();
        }

        private void Regular_Matainance_Load(object sender, EventArgs e)
        {
            //LoadMouldName();
            textBox2.Text = dbFunctions.username;
        }
        //isMoluldLoad = false;
        public void LoadMouldName()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_LoadMachine");
                MRM_Part_No.DataSource = dt;
                MRM_Part_No.DisplayMember = "MM_MachineCode";
                MRM_Part_No.ValueMember = "MM_ID";
                MRM_Part_No.SelectedIndex = -1;

                isMoluldLoad = true;
               
            }
            catch
            {
            }
            try
            {
                DataTable dt = dbFunctions.getTable("select EM_iid,EM_EmployeeName from Employee_Master where EM_Designation='Operator'  and EM_Status='A'");
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "EM_EmployeeName";
                comboBox1.ValueMember = "EM_iid";
                comboBox1.SelectedIndex = -1;

                

            }
            catch
            {
            }
             try
            {
                DataTable dt = dbFunctions.getTable("select EM_iid,EM_EmployeeName from Employee_Master where EM_Department='Maintenance' and EM_Status='A'");
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "EM_EmployeeName";
                comboBox2.ValueMember = "EM_iid";
                comboBox2.SelectedIndex = -1;

                
               
            }
            catch
            {
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isMoluldLoad)
            {
              //  DataTable dt = dbFunctions.getTable("pr_Get_LoadMouldDetails1 '" + MRM_Part_No.SelectedValue.ToString() + "'");

                DataTable dt1 = dbFunctions.getTable(" select MM_MachineName from Machine_Master where MM_MachineCode ='"+ MRM_Part_No.Text+ "'");

                if (dt1.Rows.Count > 0)
                {
                    machine_name.Text = dt1.Rows[0]["MM_MachineName"].ToString();
                }
                //machine_name.DataSource = dt1;
                //machine_name.DisplayMember = "MM_MachineName";
                //machine_name.ValueMember = "MM_MachineName";
                //machine_name.SelectedIndex = -1;
                //                MRM_Mould_No.DataSource = dt;
                //MRM_Mould_No.DisplayMember = "MLD_MouldNo";
                //MRM_Mould_No.ValueMember = "MLD_ID";
                //MRM_Mould_No.SelectedIndex = -1;


                isRegularMatainanceLoad = true;
                //    dataGridView1.DataSource=null;
                //    MRM_LotQty_Produced.Text="";
                //    MRM_Machine.Text="";


            }
            try
            {
                if (isRegularMatainanceLoad)
                {
                    DataTable dt = dbFunctions.getTable("pr_Get_RegularMatainanceDetails1 '" + MRM_Part_No.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;


                    dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.Columns["Check Point"].Width = 100;
                    dataGridView1.Columns["OK"].Width = 100;
                    dataGridView1.Columns["Not OK"].Width = 100;
                    dataGridView1.Columns["Remarks"].Width = 250;
                    txt_Rows.Text = "Rows :" + dt.Rows.Count;


                    DataTable dtx = dbFunctions.getTable("pr_get_Mould_Shorts  " + MRM_Part_No.SelectedValue + "," + MRM_Mould_No.SelectedValue.ToString());
                    if (dtx.Rows.Count > 0)
                    {
                        // MPM_Machine_ID.Text = dtx.Rows[0]["MM_ID"].ToString();
                        MRM_Machine.Text = dtx.Rows[0]["MM_MachineName"].ToString();
                        MRM_LotQty_Produced.Text = dtx.Rows[0]["Shots"].ToString();
                        MRM_Produce_Qty.Text = (decimal.Parse(dtx.Rows[0]["Shots"].ToString()) - decimal.Parse(dtx.Rows[0]["Last_Qty"].ToString())).ToString("0");
                    }

                }

            }
            catch { }




        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {

                this.Close();

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isRegularMatainanceLoad)
                {
                    DataTable dt = dbFunctions.getTable("pr_Get_RegularMatainanceDetails1 '" + MRM_Mould_No.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;


                    dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.Columns["Check Point"].Width = 100;
                    dataGridView1.Columns["OK"].Width = 100;
                    dataGridView1.Columns["Not OK"].Width = 100;
                    dataGridView1.Columns["Remarks"].Width = 250;
                    txt_Rows.Text = "Rows :" + dt.Rows.Count;


                    DataTable dtx = dbFunctions.getTable("pr_get_Mould_Shorts  " + MRM_Part_No.SelectedValue + "," + MRM_Mould_No.SelectedValue.ToString());
                    if (dtx.Rows.Count > 0)
                    {
                        // MPM_Machine_ID.Text = dtx.Rows[0]["MM_ID"].ToString();
                        MRM_Machine.Text = dtx.Rows[0]["MM_MachineName"].ToString();
                        MRM_LotQty_Produced.Text = dtx.Rows[0]["Shots"].ToString();
                        MRM_Produce_Qty.Text = (decimal.Parse(dtx.Rows[0]["Shots"].ToString()) - decimal.Parse(dtx.Rows[0]["Last_Qty"].ToString())).ToString("0");
                    }

                }
               
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnsave_Click_1(object sender, EventArgs e)
        {

        }
        int b = 0;
        private void btnsave_Click_2(object sender, EventArgs e)
        {
            String a = "";


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((dataGridView1.Rows[i].Cells["OK"].Value != null && !string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["OK"].Value.ToString())) ||
                    (dataGridView1.Rows[i].Cells["Not OK"].Value != null && !string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["Not OK"].Value.ToString())))
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
           // insert();
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
                com.CommandText = "Pr_Insert_Mould_Regular_Maintance";
                com.Parameters.Add("@MRM_UnLoadDate", SqlDbType.VarChar).Value = MRM_UnLoadDate.Value.ToString("dd-MMM-yyyy hh:mm tt");
                com.Parameters.Add("@MRM_Part_No", SqlDbType.VarChar).Value = MRM_Part_No.Text.ToString();
                com.Parameters.Add("@MRM_Mould_No", SqlDbType.VarChar).Value = MRM_Mould_No.Text.ToString();
                com.Parameters.Add("@MRM_Shift", SqlDbType.VarChar).Value = MRM_Shift.Text.ToString();
                com.Parameters.Add("@MRM_LotQty_Produced", SqlDbType.VarChar).Value = MRM_LotQty_Produced.Text.ToString();
                com.Parameters.Add("@MRM_Machine", SqlDbType.VarChar).Value = MRM_Machine.Text.ToString();
                com.Parameters.Add("@MRM_OPERANAME", SqlDbType.VarChar).Value = comboBox1.Text.ToString();
                com.Parameters.Add("@MRM_ENG_NAME", SqlDbType.VarChar).Value = comboBox2.Text.ToString();
                com.Parameters.Add("@MRM_UpdatedBy", SqlDbType.VarChar).Value = dbFunctions.username;

                com.Parameters.Add("@MRM_Produce_Qty", SqlDbType.VarChar).Value = MRM_Produce_Qty.Text.ToString();
               
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
                    com.CommandText = "Pr_Insert_Mould_Regular_Maintance_Details";
                    com.Parameters.Add("@MRMD_MRM_ID", SqlDbType.VarChar).Value = MRMD_MRM_ID;
                    com.Parameters.Add("@MRMD_CheckPart", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Check Point"].Value.ToString();
                    com.Parameters.Add("@MRMD_Requirement", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Requirement"].Value.ToString();
                    com.Parameters.Add("@MRMD_OKStatus", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["OK"].Value.ToString();
                    com.Parameters.Add("@MRMD_Not_OK_Staus", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Not OK"].Value.ToString();
                    com.Parameters.Add("@MRMD_Remarks", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                    com.Parameters.Add("@MRMD_CreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;
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


    }
}