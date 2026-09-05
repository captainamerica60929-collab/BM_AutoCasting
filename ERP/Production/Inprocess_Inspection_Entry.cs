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
    public partial class Inprocess_Inspection_Entry : Form
    {
        public string ErrorMessage = "";
        public Inprocess_Inspection_Entry()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string iTemId;
        private void Inprocess_Inspection_Entry_Load(object sender, EventArgs e)
        {
            IQD_Quality_Name.Text = dbFunctions.username;
            IQD_Quality_Name.Enabled = false;
            
            
            IQD_Shift.Text = "Shift I";
          
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_Production_Request  '" + dbFunctions.Route_Card_ID + "'");
                //ID = 
                iTemId = dt.Rows[0]["Pq_iPart_ID"].ToString();
                txtRCNo.Text = dt.Rows[0]["Pq_Route_Card_No"].ToString();
                txtPartNo.Text = dt.Rows[0]["Pq_vPart_No"].ToString();
                txtPartName.Text = dt.Rows[0]["Pq_vPart_Name"].ToString();
                txtModel.Text = dt.Rows[0]["Pq_vModel"].ToString();
                txtxplanQty.Text = dt.Rows[0]["Pq_RM_Plan_Qty"].ToString();
                textBox2.Text = dt.Rows[0]["Date"].ToString();
            }
            catch { }
            displayInProcessInspection();


            DataTable dd = dbFunctions.getTable("pr_Fetch_INPROCESS_INSPECTION_Instrument  " + iTemId);
            if (dd.Rows.Count > 0)
            {
                if (decimal.Parse(dd.Rows[0]["Remaing Days"].ToString()) <= 0)
                {
                    dataGridView1.Enabled = false;
                }
                else
                {
                    dataGridView1.Enabled = true;
                }

                label14.Text = dd.Rows[0]["MS_vInstrument_Name"].ToString();
                label16.Text = dd.Rows[0]["Plan Date"].ToString();
                label18.Text = dd.Rows[0]["Remaing Days"].ToString();

                label19.Visible = true;
                pictureBox1.Visible = true;
                label18.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
                label14.Visible = true;
                label12.Visible = true;
            }
            else
            {

                label19.Visible = false;
                label18.Visible = false;
                label16.Visible = false;
                label17.Visible = false;
                label14.Visible = false;
                label12.Visible = false;

                pictureBox1.Visible = false;
            }

        }
        public void displayInProcessInspection()
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            try
            {
                try
                {
                    dataGridView1.Columns[0].Frozen = false;
                    dataGridView1.Columns[1].Frozen = false;
                    dataGridView1.Columns[2].Frozen = false;
                    dataGridView1.Columns[3].Frozen = false;
                }
                catch { }
                DataTable dt = dbFunctions.getTable("pr_Display_Inprocess_QC_Details '" + dbFunctions.Route_Card_ID + "','" + IQD_Shift.Text.ToString() + "','" + dateTimePicker1.Value.ToString("yyyyMMdd") + "'");
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
                    dataGridView1.Columns[0].Frozen = true;
                    dataGridView1.Columns[1].Frozen = true;
                    dataGridView1.Columns[2].Frozen = true;
                    dataGridView1.Columns[3].Frozen = true;

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dataGridView1.Columns[1].Width = 200;
                    dataGridView1.Columns[2].Width = 150;
                    dataGridView1.Columns[3].Width = 150;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;

                    dataGridView1.Columns["User"].Visible = false;
                    dataGridView1.Columns["Quality Name"].Visible = false;
                    IQD_CreatedBy.Text = dataGridView1.Rows[0].Cells["User"].Value.ToString();
                    IQD_Quality_Name.Text = dataGridView1.Rows[0].Cells["Quality Name"].Value.ToString();

                }
                else
                {

                    DataTable dtb = dbFunctions.getTable("pr_Fetch_INPROCESS_INSPECTIONDetails '" + iTemId + "'");
                    dataGridView1.DataSource = dtb;
                    dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
                    dataGridView1.Columns[0].Frozen = true;
                    dataGridView1.Columns[1].Frozen = true;
                    dataGridView1.Columns[2].Frozen = true;
                    dataGridView1.Columns[3].Frozen = true;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dataGridView1.Columns[1].Width = 200;
                    dataGridView1.Columns[2].Width = 150;
                    dataGridView1.Columns[3].Width = 150;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    IQD_CreatedBy.Text = "";
                    IQD_Quality_Name.Text = dbFunctions.username;
                }
               
                  
            }
            catch(Exception ex) { }
        }

        private void Inprocess_Inspection_Entry_Shown(object sender, EventArgs e)
        {
            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            //dataGridView1.Columns.Add(btn);
            //btn.HeaderText = "Save";
            //btn.Text = "Save";
            //btn.Name = "btnsave";
            //btn.UseColumnTextForButtonValue = true;
            //check++;
            //dataGridView1.Columns[0].Visible = false;

            //DataGridViewButtonColumn btnedit = new DataGridViewButtonColumn();
            //dataGridView1.Columns.Add(btnedit);
            //btnedit.HeaderText = "Edit";
            //btnedit.Text = "Edit";
            //btnedit.Name = "btnedit";
            //btnedit.UseColumnTextForButtonValue = true;
            //check++;
            //dataGridView1.Columns[0].Visible = false;

            //DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn();
            //dataGridView1.Columns.Add(btnDel);
            //btnDel.HeaderText = "Delete";
            //btnDel.Text = "Delete";
            //btnDel.Name = "btnDel";
            //btnDel.UseColumnTextForButtonValue = true;
            //check++;
            //dataGridView1.Columns[0].Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                DataTable dt = dbFunctions.getTable("pr_Display_Inprocess_QC_Details '" + iTemId + "','" + IQD_Shift.Text.ToString() + "','" + DateTime.Now.ToString("yyyyMMdd") + "'");
                if (dt.Rows.Count > 0)
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
                            com.CommandText = "Pr_Update_Inprocess_QC_Details";

                            com.Parameters.Add("@IQD_Route_card_ID", SqlDbType.Int).Value = dbFunctions.Route_Card_ID;
                            com.Parameters.Add("@IQD_item_ID", SqlDbType.Int).Value = iTemId;// txtPartNo.Text.ToString();
                            //com.Parameters.Add("@IQD_ProcessParameter", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Process Parameter"].Value.ToString();
                            //com.Parameters.Add("@IQD_Specification", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Specification"].Value.ToString();
                            //com.Parameters.Add("@IQD_CheckMethod", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Checking Method"].Value.ToString();

                            com.Parameters.Add("@IQD_FOMin", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["FO Min"].Value.ToString();
                            com.Parameters.Add("@IQD_FOMax", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["FO Max"].Value.ToString();

                            com.Parameters.Add("@IQD_PTL_Min", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Min"].Value.ToString();
                            com.Parameters.Add("@IQD_PTL_Max", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Max"].Value.ToString();

                            com.Parameters.Add("@IQD_PTL_Min1", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Min1"].Value.ToString();
                            com.Parameters.Add("@IQD_PTL_Max1", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Max1"].Value.ToString();

                            com.Parameters.Add("@IQD_PTL_Min2", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Min2"].Value.ToString();
                            com.Parameters.Add("@IQD_PTL_Max2", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Max2"].Value.ToString();

                            com.Parameters.Add("@IQD_PTL_Min3", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Min3"].Value.ToString();
                            com.Parameters.Add("@IQD_PTL_Max3", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Max3"].Value.ToString();

                            com.Parameters.Add("@IQD_PTL_Min4", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Min4"].Value.ToString();
                            com.Parameters.Add("@IQD_PTL_Max4", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Max4"].Value.ToString();

                            com.Parameters.Add("@IQD_LO_Min", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["LO Min"].Value.ToString();
                            com.Parameters.Add("@IQD_LO_Max", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["LO Max"].Value.ToString();

                            com.Parameters.Add("@IQD_Remarks", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();



                            com.ExecuteNonQuery();
                            com.Connection.Close();


                        }
                        catch (Exception Ex)
                        {
                            dbFunctions.Logs(Ex.Message, dbFunctions.username);
                            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    
                   
                }


            }
            catch { }



            
        }
        public bool Validate()
        {
            if ((string.IsNullOrEmpty(IQD_Shift.Text.Trim())))
            {
                ErrorMessage = "Shift Should Not be Empty";
                IQD_Shift.Focus();
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
                
            }
        }

        public void insert()
        {   string s="";
        try
        {
            DataTable ddd = dbFunctions.getTable("pr_get_Report_No  " + dbFunctions.Route_Card_ID + ",'" + IQD_Shift.Text.ToString() + "','" + dateTimePicker1.Value.ToString("dd-MMM-yyyy") + "'");
            s = ddd.Rows[0][0].ToString();
        }
        catch { }

            for (int i = 0; i<dataGridView1.Rows.Count; i++)
            {
                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "Pr_Insert_Inprocess_QC_Details";
                        com.Parameters.Add("@id", SqlDbType.Int).Value = dataGridView1.Rows[i].Cells[0].Value.ToString();
                      
                        com.Parameters.Add("@IQD_Route_card_ID", SqlDbType.Int).Value = dbFunctions.Route_Card_ID;
                        com.Parameters.Add("@IQD_item_ID", SqlDbType.Int).Value = iTemId;
                        com.Parameters.Add("@IQD_ProcessParameter", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Process Parameter"].Value.ToString();
                        com.Parameters.Add("@IQD_Specification", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Specification"].Value.ToString();
                        com.Parameters.Add("@IQD_CheckMethod", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Checking Method"].Value.ToString();
                        com.Parameters.Add("@IQD_Shift", SqlDbType.VarChar).Value = IQD_Shift.Text.ToString();

                    com.Parameters.Add("@IQD_FOMin", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["AF1"].Value.ToString();
                    com.Parameters.Add("@IQD_FOMax", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["AF2"].Value.ToString();

                    com.Parameters.Add("@IQD_PTL_Min", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["AF3"].Value.ToString();
                    com.Parameters.Add("@IQD_PTL_Max", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["AM1"].Value.ToString();

                    com.Parameters.Add("@IQD_PTL_Min1", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["AM2"].Value.ToString();
                    com.Parameters.Add("@IQD_PTL_Max1", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["AM3"].Value.ToString();

                    com.Parameters.Add("@IQD_PTL_Min2", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["AC1"].Value.ToString();
                    com.Parameters.Add("@IQD_PTL_Max2", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["AC2"].Value.ToString();

                    com.Parameters.Add("@IQD_PTL_Min3", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["AC3"].Value.ToString();
                    com.Parameters.Add("@IQD_PTL_Max3", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["BF1"].Value.ToString();

                    com.Parameters.Add("@IQD_PTL_Min4", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["BF2"].Value.ToString();
                    com.Parameters.Add("@IQD_PTL_Max4", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["BF3"].Value.ToString();

                    com.Parameters.Add("@IQD_LO_Min", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["BM1"].Value.ToString();
                    com.Parameters.Add("@IQD_LO_Max", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["BM2"].Value.ToString();

                    com.Parameters.Add("@IQD_A1", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["BM3"].Value.ToString();
                    com.Parameters.Add("@IQD_A2", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["BC1"].Value.ToString();
                    com.Parameters.Add("@IQD_A3", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["BC2"].Value.ToString();
                    com.Parameters.Add("@IQD_A4", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["BC3"].Value.ToString();

                    //    com.Parameters.Add("@IQD_FOMin", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["FO Min"].Value.ToString();
                    //    com.Parameters.Add("@IQD_FOMax", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["FO Max"].Value.ToString();

                    //    com.Parameters.Add("@IQD_PTL_Min", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Min"].Value.ToString();
                    //    com.Parameters.Add("@IQD_PTL_Max", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Max"].Value.ToString();

                    //    com.Parameters.Add("@IQD_PTL_Min1", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Min1"].Value.ToString();
                    //    com.Parameters.Add("@IQD_PTL_Max1", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Max1"].Value.ToString();

                    //    com.Parameters.Add("@IQD_PTL_Min2", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Min2"].Value.ToString();
                    //    com.Parameters.Add("@IQD_PTL_Max2", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Max2"].Value.ToString();

                    //    com.Parameters.Add("@IQD_PTL_Min3", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Min3"].Value.ToString();
                    //    com.Parameters.Add("@IQD_PTL_Max3", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Max3"].Value.ToString();

                    //    com.Parameters.Add("@IQD_PTL_Min4", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Min4"].Value.ToString();
                    //    com.Parameters.Add("@IQD_PTL_Max4", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["PTL Max4"].Value.ToString();

                    //    com.Parameters.Add("@IQD_LO_Min", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["LO Min"].Value.ToString();
                    //    com.Parameters.Add("@IQD_LO_Max", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["LO Max"].Value.ToString();

                    //com.Parameters.Add("@IQD_A1", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["IQD_A1"].Value.ToString();
                    //com.Parameters.Add("@IQD_A2", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["IQD_A2"].Value.ToString();
                    //com.Parameters.Add("@IQD_A3", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["IQD_A3"].Value.ToString();
                    //com.Parameters.Add("@IQD_A4", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["IQD_A4"].Value.ToString();

                    com.Parameters.Add("@IQD_Remarks", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();

                        com.Parameters.Add("@IQD_Date", SqlDbType.VarChar).Value = dateTimePicker1.Value.ToString("dd-MMM-yyyy");
                        com.Parameters.Add("@IQD_CreatedBy", SqlDbType.VarChar).Value = IQD_CreatedBy.Text.ToString();
                        com.Parameters.Add("@IQD_Quality_Name", SqlDbType.VarChar).Value = IQD_Quality_Name.Text.ToString();
                        com.Parameters.Add("@IQD_Report_No", SqlDbType.VarChar).Value = s;



                        com.ExecuteNonQuery();
                        com.Connection.Close();
                       
                        

                    }
                    catch (Exception Ex)
                    {
                        dbFunctions.Logs(Ex.Message, dbFunctions.username);
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
            MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void IQD_Shift_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayInProcessInspection();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            displayInProcessInspection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            displayInProcessInspection();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Pq_dRevDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void Pq_vRevNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Pq_vDocNo_TextChanged(object sender, EventArgs e)
        {

        }
      
     
    }
}
