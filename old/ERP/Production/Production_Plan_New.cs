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
    public partial class Production_Plan_New : Form
    {
        public Production_Plan_New()
        {
            InitializeComponent();
        }
        public string Error_Message = "";
        public static int Machine_ID = 0;
        private void Production_Plan_Load(object sender, EventArgs e)
        {
            Load_Part_Name();
          //  From_Date.MinDate = DateTime.Now.Date;

            //From_Date.MinDate = System.DateTime.Now;

        }


        private void Load_Part_Name()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_get_Part_Name");
                Pq_vPart_No.DataSource = dt;
                Pq_vPart_No.DisplayMember = "IM_PartName";
                Pq_vPart_No.ValueMember = "IM_ID";
                Pq_vPart_No.SelectedIndex = -1;

            }
            catch
            {
            }
        }
        

        private void Pq_vPart_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_Part_Detail '" + Pq_vPart_No.SelectedValue.ToString() + "'");
                Pq_vPart_Name.Text = dt.Rows[0]["IM_PartNo"].ToString();
                Pq_vModel.Text = dt.Rows[0]["ML_Model"].ToString();
                Pq_Mould_Name.Text = dt.Rows[0]["MLD_Mold"].ToString();
                MM_MachineCode.Text = dt.Rows[0]["MM_MachineCode"].ToString();
                Machine_ID =int.Parse( dt.Rows[0]["MM_ID"].ToString());
                txt_No_Of_Cavity.Text = dt.Rows[0]["MLD_MoldCavity"].ToString();
                txt_Cycle_time.Text = dt.Rows[0]["MLD_Cycle_Time"].ToString();
                textBox4.Text = dt.Rows[0]["IM_MouldNumer"].ToString();
                textBox3.Text = dt.Rows[0]["MM_MachineName"].ToString();
                To_Date.Value = From_Date.Value.AddDays(15);
                generate();
            }
            catch { }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void button7_Click(object sender, EventArgs e)
        {
            generate(); 
        }

        public void generate()
        {
            DataTable dt = dbFunctions.getTable("pr_getProduction_Plan  '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "','" + Pq_vPart_No.SelectedValue + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            cal();
        }

        public void cal()
        {
            decimal ShI = 0.0m;
            decimal ShII = 0.0m;
            decimal ShIII = 0.0m;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ShI += decimal.Parse(dataGridView1.Rows[i].Cells["Shift1"].Value.ToString());
                ShII += decimal.Parse(dataGridView1.Rows[i].Cells["Shift2"].Value.ToString());
                ShIII += decimal.Parse(dataGridView1.Rows[i].Cells["Shift3"].Value.ToString());
            }
            ShiftI.Text = ShI.ToString("0.00");
            ShiftII.Text = ShII.ToString("0.00");
            ShiftIII.Text = ShIII.ToString("0.00");
        }
        void calculate()
        {
            try
            {
                txt_Hourly_Pro.Text = (((60.0m / decimal.Parse(txt_Cycle_time.Text)) * 60.0m) * decimal.Parse(txt_No_Of_Cavity.Text)).ToString("0");

                txt_Per_Shift_Production.Text = (decimal.Parse(txt_Hourly_Pro.Text) * 8).ToString("0");
                txt_Reqesting_Shift.Text = (Math.Round(decimal.Parse(txt_Req_Qty.Text) / decimal.Parse(txt_Per_Shift_Production.Text))).ToString("0");
            }
            catch { }
        }
        private void txt_Req_Qty_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void txt_Cycle_time_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }


        private void btnsave_Click(object sender, EventArgs e)
        {
            Calculation();
           

             DataTable dt = dbFunctions.getTable("pr_get_Monthly_PlanDetails  '"+From_Date.Value.ToString("yyyyMMdd")+"','"+To_Date.Value.ToString("yyyyMMdd")+"',"+Machine_ID);
             bool flag = true;
             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                 try
                 {
                     con.Open();
                     SqlCommand com = new SqlCommand();
                     com.Connection = con;
                     com.CommandType = CommandType.StoredProcedure;
                     com.CommandText = "Pr_Update_Production_Plan";
                     com.Parameters.Add("@PP_iid", SqlDbType.VarChar).Value = dt.Rows[i]["PP_iid"].ToString();
                     com.Parameters.Add("@PP_iMachineID", SqlDbType.VarChar).Value = Machine_ID;


                     CheckBox Ch1 = (CheckBox)this.Controls.Find("SI" + i, true)[0];
                     com.Parameters.Add("@PP_Shift1_Status", SqlDbType.VarChar).Value = (Ch1.Checked == true) ? "true" : "false";

                     TextBox Item1 = (TextBox)this.Controls.Find("ItemI" + i, true)[0];
                     com.Parameters.Add("@PP_Shift1_Item_ID", SqlDbType.VarChar).Value = Item1.Text.ToString();

                     TextBox SV1 = (TextBox)this.Controls.Find("SIV" + i, true)[0];
                     com.Parameters.Add("@PP_Shift1_PlanQty", SqlDbType.VarChar).Value = SV1.Text.ToString();
                     com.Parameters.Add("@PP_Shift1_Shift", SqlDbType.VarChar).Value = "0";

                     CheckBox Ch2 = (CheckBox)this.Controls.Find("SII" + i, true)[0];
                     com.Parameters.Add("@PP_Shift2_Status", SqlDbType.VarChar).Value = (Ch2.Checked == true) ? "true" : "false";

                     TextBox Item2 = (TextBox)this.Controls.Find("ItemII" + i, true)[0];
                     com.Parameters.Add("@PP_Shift2_Item_ID", SqlDbType.VarChar).Value = Item2.Text.ToString();

                     TextBox SV2 = (TextBox)this.Controls.Find("SIIV" + i, true)[0];
                     com.Parameters.Add("@PP_Shift2_PlanQty", SqlDbType.VarChar).Value = SV2.Text.ToString();
                     com.Parameters.Add("@PP_Shift2_Shift", SqlDbType.VarChar).Value = "0";

                     CheckBox Ch3 = (CheckBox)this.Controls.Find("SIII" + i, true)[0];
                     com.Parameters.Add("@PP_Shift3_Status", SqlDbType.VarChar).Value = (Ch3.Checked == true) ? "true" : "false";

                     TextBox Item3 = (TextBox)this.Controls.Find("ItemIII" + i, true)[0];
                     com.Parameters.Add("@PP_Shift3_Item_ID", SqlDbType.VarChar).Value = Item3.Text.ToString();

                     TextBox SV3 = (TextBox)this.Controls.Find("SIIIV" + i, true)[0]; 
                     com.Parameters.Add("@PP_Shift3_PlanQty", SqlDbType.VarChar).Value = SV3.Text.ToString();
                     com.Parameters.Add("@PP_Shift3_Shift", SqlDbType.VarChar).Value = "0";
                     com.Parameters.Add("@PP_Plan_Updated_By", SqlDbType.VarChar).Value = dbFunctions.username;

                     com.Parameters.Add("@pp_Per_Shift_Qty", SqlDbType.VarChar).Value = txt_Per_Shift_Production.Text;
                     com.ExecuteNonQuery();
                     com.Connection.Close();
                     
                 }
                 catch (Exception Ex)
                 {
                     flag = false;
                 }

             }


             if (flag == true)
             {
                 MessageBox.Show("Plan Saved Successfully","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                 this.Close();

             }

        }

        void Calculation11()
        {
            DataTable dt = dbFunctions.getTable("pr_get_Monthly_PlanDetails  '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "'," + Machine_ID);

            int qty = 0;
            int Shift = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                TextBox Item1 = (TextBox)this.Controls.Find("ItemI" + i, true)[0];
                if (Item1.Text.Equals("" + Pq_vPart_No.SelectedValue))
                {

                    CheckBox Ch1 = (CheckBox)this.Controls.Find("SI" + i, true)[0];
                    if (Ch1.Checked == true)
                    {
                        Shift += 1;
                        TextBox SV1 = (TextBox)this.Controls.Find("SIV" + i, true)[0];
                        qty += int.Parse(SV1.Text.ToString());

                    }
                }



                TextBox Item11 = (TextBox)this.Controls.Find("ItemII" + i, true)[0];
                if (Item11.Text.Equals("" + Pq_vPart_No.SelectedValue))
                {

                    CheckBox Ch1 = (CheckBox)this.Controls.Find("SII" + i, true)[0];
                    if (Ch1.Checked == true)
                    {
                        Shift += 1;
                        TextBox SV1 = (TextBox)this.Controls.Find("SIIV" + i, true)[0];
                        qty += int.Parse(SV1.Text.ToString());

                    }
                }

                TextBox Item1I1 = (TextBox)this.Controls.Find("ItemIII" + i, true)[0];
                if (Item11.Text.Equals("" + Pq_vPart_No.SelectedValue))
                {

                    CheckBox Ch1 = (CheckBox)this.Controls.Find("SIII" + i, true)[0];
                    if (Ch1.Checked == true)
                    {
                        Shift += 1;
                        TextBox SV1 = (TextBox)this.Controls.Find("SIIIV" + i, true)[0];
                        qty += int.Parse(SV1.Text.ToString());

                    }
                }


            }

           
           // txt_Req_Qty.Text = qty.ToString();

        }


        void Calculation()
        {
            DataTable dt = dbFunctions.getTable("pr_get_Monthly_PlanDetails  '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "'," + Machine_ID);

            int qty = 0;
            int Shift = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
             
                    TextBox Item1 = (TextBox)this.Controls.Find("ItemI" + i, true)[0];
                    if (Item1.Text.Equals(""+Pq_vPart_No.SelectedValue))
                    {

                        CheckBox Ch1 = (CheckBox)this.Controls.Find("SI" + i, true)[0];
                        if (Ch1.Checked == true)
                        {
                            Shift += 1;
                            TextBox SV1 = (TextBox)this.Controls.Find("SIV" + i, true)[0];
                            qty += int.Parse(SV1.Text.ToString());

                        }
                    }



                    TextBox Item11 = (TextBox)this.Controls.Find("ItemII" + i, true)[0];
                    if (Item11.Text.Equals("" + Pq_vPart_No.SelectedValue))
                    {

                        CheckBox Ch1 = (CheckBox)this.Controls.Find("SII" + i, true)[0];
                        if (Ch1.Checked == true)
                        {
                            Shift += 1;
                            TextBox SV1 = (TextBox)this.Controls.Find("SIIV" + i, true)[0];
                            qty += int.Parse(SV1.Text.ToString());

                        }
                    }

                    TextBox Item1I1 = (TextBox)this.Controls.Find("ItemIII" + i, true)[0];
                    if (Item1I1.Text.Equals("" + Pq_vPart_No.SelectedValue))
                    {

                        CheckBox Ch1 = (CheckBox)this.Controls.Find("SIII" + i, true)[0];
                        if (Ch1.Checked == true)
                        {
                            Shift += 1;
                            TextBox SV1 = (TextBox)this.Controls.Find("SIIIV" + i, true)[0];
                            qty += int.Parse(SV1.Text.ToString());

                        }
                    }


            }

          //  S_Shift.Text = Shift.ToString();
           // S_Qty.Text = qty.ToString();


        }
        private void txt_Req_Qty_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode==Keys.Enter)
           {
               To_Date.Focus();
           }
        }

        private void To_Date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // btnsave.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panel1.Visible = true;
            DataTable dt = dbFunctions.getTable("pr_get_ProductionDetails " + Machine_ID + ",'" + dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString() + "'");
            dataGridView2.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView2);

            Date.Text = dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString();

            DataGridViewCheckBoxColumn doWork1 = new DataGridViewCheckBoxColumn();
            doWork1.HeaderText = "S1";
            doWork1.FalseValue = "0";
            doWork1.TrueValue = "1";
            dataGridView2.Columns.Insert(2, doWork1);
            dataGridView2.Columns[2].Width = 50;

            DataGridViewCheckBoxColumn doWork2 = new DataGridViewCheckBoxColumn();
            doWork2.HeaderText = "S2";
            doWork2.FalseValue = "0";
            doWork2.TrueValue = "1";
            dataGridView2.Columns.Insert(4, doWork2);
            dataGridView2.Columns[4].Width = 50;

            DataGridViewCheckBoxColumn doWork3 = new DataGridViewCheckBoxColumn();
            doWork3.HeaderText = "S3";
            doWork3.FalseValue = "0";
            doWork3.TrueValue = "1";
            dataGridView2.Columns.Insert(6, doWork3);
            dataGridView2.Columns[6].Width = 50;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                SqlConnection con1 = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con1.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con1;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_Production_Plan_New";

                    com.Parameters.Add("@PP_iMachineID", SqlDbType.Int).Value = Machine_ID;
                    com.Parameters.Add("@PP_dDate", SqlDbType.DateTime).Value = dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString();
                    com.Parameters.Add("@PP_Shift", SqlDbType.VarChar).Value = "S1";
                    com.Parameters.Add("@PP_Hour", SqlDbType.Int).Value = i + 1;
                    com.Parameters.Add("@PP_Item", SqlDbType.Int).Value = Pq_vPart_No.SelectedValue.ToString();
                    com.Parameters.Add("@PP_Qty", SqlDbType.Int).Value = dataGridView2.Rows[i].Cells["S1-Qty"].Value.ToString();
                    com.Parameters.Add("@PP_CreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;


                    com.ExecuteNonQuery();
                    com.Connection.Close();
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "Already Exists");
                }
                //
                SqlConnection con2 = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con2.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con2;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_Production_Plan_New";

                    com.Parameters.Add("@PP_iMachineID", SqlDbType.Int).Value = Machine_ID;
                    com.Parameters.Add("@PP_dDate", SqlDbType.DateTime).Value = dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString();
                    com.Parameters.Add("@PP_Shift", SqlDbType.VarChar).Value = "S2";
                    com.Parameters.Add("@PP_Hour", SqlDbType.Int).Value = i + 1;
                    com.Parameters.Add("@PP_Item", SqlDbType.Int).Value = Pq_vPart_No.SelectedValue.ToString();
                    com.Parameters.Add("@PP_Qty", SqlDbType.Int).Value = dataGridView2.Rows[i].Cells["S2-Qty"].Value.ToString();
                    com.Parameters.Add("@PP_CreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;


                    com.ExecuteNonQuery();
                    com.Connection.Close();
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "Already Exists");
                }
                //
                SqlConnection con3 = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con3.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con3;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_Production_Plan_New";

                    com.Parameters.Add("@PP_iMachineID", SqlDbType.Int).Value = Machine_ID;
                    com.Parameters.Add("@PP_dDate", SqlDbType.DateTime).Value = dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString();
                    com.Parameters.Add("@PP_Shift", SqlDbType.VarChar).Value = "S3";
                    com.Parameters.Add("@PP_Hour", SqlDbType.Int).Value = i + 1;
                    com.Parameters.Add("@PP_Item", SqlDbType.Int).Value = Pq_vPart_No.SelectedValue.ToString();
                    com.Parameters.Add("@PP_Qty", SqlDbType.Int).Value = dataGridView2.Rows[i].Cells["S3-Qty"].Value.ToString();
                    com.Parameters.Add("@PP_CreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;


                    com.ExecuteNonQuery();
                    com.Connection.Close();
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "Already Exists");
                }
                
            }
            MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            cals();
            CheckedValue();
        }

        public void CheckedValue()
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.CurrentCell.ColumnIndex == 2)
                {
                    dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[3].Value = txt_Hourly_Pro.Text;
                    textBox1.Focus();
                }
                else if (dataGridView2.CurrentCell.ColumnIndex == 4)
                {
                    dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[5].Value = txt_Hourly_Pro.Text;
                    textBox1.Focus();
                }
                else if (dataGridView2.CurrentCell.ColumnIndex == 6)
                {
                    dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[7].Value = txt_Hourly_Pro.Text;
                    textBox1.Focus();
                }
            }
        }

        public void cals()
        {
            try
            {
                decimal S1 = 0.0m;
                decimal S2 = 0.0m;
                decimal S3 = 0.0m;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    S1 += decimal.Parse(dataGridView2.Rows[i].Cells["S1-Qty"].Value.ToString());
                    S2 += decimal.Parse(dataGridView2.Rows[i].Cells["S2-Qty"].Value.ToString());
                    S3 += decimal.Parse(dataGridView2.Rows[i].Cells["S3-Qty"].Value.ToString());
                }
                label17.Text = S1.ToString("0.00");
                label23.Text = S2.ToString("0.00");
                label25.Text = S3.ToString("0.00");
            }
            catch
            {
            }
        }

    


    }

       


       
    }

