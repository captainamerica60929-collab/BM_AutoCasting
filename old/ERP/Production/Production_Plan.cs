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
    public partial class Production_Plan : Form
    {
        public Production_Plan()
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



        void generate()
        {


            S_Shift.Text = "0";
            S_Qty.Text = "0";

            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                flowLayoutPanel1.Controls.Remove(ctrl);
                ctrl.Dispose();

            }

            int Plan_Qty = 0;
            int Shift = 0;

            flowLayoutPanel1.Controls.Clear();
            DataTable dt = dbFunctions.getTable("pr_get_Monthly_PlanDetails  '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "'," + Machine_ID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DateTime Date = DateTime.Parse(dt.Rows[i]["PP_dDate"].ToString());


                Panel Plan1 = new Panel();
                Plan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                Plan1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Plan1.Location = new System.Drawing.Point(3, 3);
                Plan1.Name = "Plan1" + i;
                Plan1.Size = new System.Drawing.Size(136, 123);
                Plan1.TabIndex = 0;
                if (Date.ToString("dddd").Substring(0, 3).Equals("Sun"))
                {
                    Plan1.Enabled = false;
                    Plan1.BackColor = Color.LightPink;
                }


                flowLayoutPanel1.Controls.Add(Plan1);


                Label PD1 = new System.Windows.Forms.Label();
                PD1.AutoSize = true;
                PD1.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                PD1.ForeColor = System.Drawing.Color.Black;
                PD1.Location = new System.Drawing.Point(0, 4);
                PD1.Name = "PD" + i;
                PD1.Size = new System.Drawing.Size(88, 20);
                PD1.TabIndex = 281;

                PD1.Text = Date.ToString("dd-MMM-yy") + "(" + Date.ToString("dddd").Substring(0, 3) + ")";
                Plan1.Controls.Add(PD1);


                TextBox SIIV = new TextBox();
                SIIV.BackColor = System.Drawing.SystemColors.InactiveBorder;
                SIIV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                SIIV.Enabled = false;
                SIIV.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                SIIV.Location = new System.Drawing.Point(51, 60);
                SIIV.Name = "SIIV" + i;
                SIIV.Size = new System.Drawing.Size(50, 27);
                SIIV.TabIndex = 279;
                SIIV.Text = dt.Rows[i]["PP_Shift2_PlanQty"].ToString();
                Plan1.Controls.Add(SIIV);
                try
                {
                    Plan_Qty += int.Parse(dt.Rows[i]["PP_Shift2_PlanQty"].ToString());
                }
                catch { }
                TextBox SIV = new TextBox();
                SIV.BackColor = System.Drawing.SystemColors.InactiveBorder;
                SIV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                SIV.Enabled = false;
                SIV.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                SIV.Location = new System.Drawing.Point(52, 30);
                SIV.Name = "SIV" + i;
                SIV.Size = new System.Drawing.Size(50, 27);
                SIV.TabIndex = 278;
                SIV.Text = dt.Rows[i]["PP_Shift1_PlanQty"].ToString();
                Plan1.Controls.Add(SIV);
                try
                {
                    Plan_Qty += int.Parse(dt.Rows[i]["PP_Shift1_PlanQty"].ToString());
                }
                catch { }


                TextBox SIIIV = new TextBox();
                SIIIV.BackColor = System.Drawing.SystemColors.InactiveBorder;
                SIIIV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                SIIIV.Enabled = false;
                SIIIV.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                SIIIV.Location = new System.Drawing.Point(51, 90);
                SIIIV.Name = "SIIIV" + i;
                SIIIV.Size = new System.Drawing.Size(50, 27);
                SIIIV.TabIndex = 277;
                SIIIV.Text = dt.Rows[i]["PP_Shift3_PlanQty"].ToString();
                Plan1.Controls.Add(SIIIV);
                try
                {
                    Plan_Qty += int.Parse(dt.Rows[i]["PP_Shift3_PlanQty"].ToString());
                }
                catch { }




                TextBox ItemI = new TextBox();
                ItemI.BackColor = System.Drawing.SystemColors.InactiveBorder;
                ItemI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                ItemI.Visible = false;
                ItemI.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ItemI.Location = new System.Drawing.Point(51, 60);
                ItemI.Name = "ItemI" + i;
                ItemI.Size = new System.Drawing.Size(50, 27);
                ItemI.TabIndex = 279;
                ItemI.Text = dt.Rows[i]["PP_Shift1_Item_ID"].ToString();
                Plan1.Controls.Add(ItemI);


                TextBox ItemII = new TextBox();
                ItemII.BackColor = System.Drawing.SystemColors.InactiveBorder;
                ItemII.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                ItemII.Visible = false;
                ItemII.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ItemII.Location = new System.Drawing.Point(52, 30);
                ItemII.Name = "ItemII" + i;
                ItemII.Size = new System.Drawing.Size(50, 27);

                ItemII.TabIndex = 278;
                ItemII.Text = dt.Rows[i]["PP_Shift2_Item_ID"].ToString();
                Plan1.Controls.Add(ItemII);



                TextBox ItemIII = new TextBox();
                ItemIII.BackColor = System.Drawing.SystemColors.InactiveBorder;
                ItemIII.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                ItemIII.Enabled = false;
                ItemIII.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ItemIII.Location = new System.Drawing.Point(51, 90);
                ItemIII.Name = "ItemIII" + i;
                ItemIII.Size = new System.Drawing.Size(50, 27);
                ItemIII.TabIndex = 277;
                ItemIII.Text = dt.Rows[i]["PP_Shift3_Item_ID"].ToString();
                Plan1.Controls.Add(ItemIII);

                
                Label RH11=new System.Windows.Forms.Label();
                RH11.Location = new System.Drawing.Point(100, 36);
                RH11.AutoSize = true;
                RH11.Name = "RH1"+i;
                RH11.Size = new System.Drawing.Size(15, 14);
                RH11.TabIndex = 283;
                RH11.Text = dt.Rows[i]["PP_Shift1_Percent"].ToString();
                Plan1.Controls.Add(RH11);


                Label RH21 = new Label();
                RH21.AutoSize = true;
                RH21.Location = new System.Drawing.Point(100, 66);
                RH21.Name = "RH2"+i;
                RH21.Size = new System.Drawing.Size(15, 14);
                RH21.TabIndex = 284;
                RH21.Text = dt.Rows[i]["PP_Shift2_Percent"].ToString();
                Plan1.Controls.Add(RH21);


                Label RH31 = new Label();
                RH31.AutoSize = true;
                RH31.Location = new System.Drawing.Point(100, 96);
                RH31.Name = "RH3"+i;
                RH31.Size = new System.Drawing.Size(15, 14);
                RH31.TabIndex = 285;
                RH31.Text = dt.Rows[i]["PP_Shift3_Percent"].ToString();
                Plan1.Controls.Add(RH31);


                CheckBox SIII = new CheckBox();
                SIII.AutoSize = true;
                SIII.Location = new System.Drawing.Point(35, 96);
                SIII.Name = "SIII" + i;
                SIII.Size = new System.Drawing.Size(15, 14);
                SIII.TabIndex = 7;
                SIII.UseVisualStyleBackColor = true;
                if (dt.Rows[i]["PP_Shift3_Status"].ToString().ToLower().Equals("true"))
                {
                   
                 
                        {
                            SIII.Checked = true;
                            Shift++;

                            if (!ItemIII.Text.Equals("" + Pq_vPart_No.SelectedValue) && !ItemIII.Text.Equals("0"))
                            {
                             if((dt.Rows[i]["PP_Shift3_Percent"].ToString().ToLower().Equals("100.00")))
                                
                            {
                                SIII.Enabled = false;
                            }
                            }
                        }
                   
                }
                SIII.CheckedChanged += new System.EventHandler(this.SIII_CheckedChanged);

                Plan1.Controls.Add(SIII);

                CheckBox SII = new CheckBox();
                SII.AutoSize = true;
                SII.Location = new System.Drawing.Point(35, 66);
                SII.Name = "SII" + i;
                SII.Size = new System.Drawing.Size(15, 14);
                SII.TabIndex = 6;
                SII.UseVisualStyleBackColor = true;
                if (dt.Rows[i]["PP_Shift2_Status"].ToString().ToLower().Equals("true") && (!dt.Rows[i]["PP_Shift2_Percent"].ToString().ToLower().Equals("0.00") || dt.Rows[i]["PP_Shift2_Percent"].ToString().ToLower().Equals("100.00")))
                {
                    SII.Checked = true;
                    Shift++;

                    if (!ItemII.Text.Equals("" + Pq_vPart_No.SelectedValue) && !ItemII.Text.Equals("0"))
                    {
                        if ((dt.Rows[i]["PP_Shift2_Percent"].ToString().ToLower().Equals("100.00")))
                        {
                            SII.Enabled = false;
                        }
                    }
                }
                SII.CheckedChanged += new System.EventHandler(this.SII_CheckedChanged);

                Plan1.Controls.Add(SII);

                CheckBox SI = new CheckBox();
                SI.AutoSize = true;
                SI.Location = new System.Drawing.Point(35, 36);
                SI.Name = "SI" + i;
                SI.Size = new System.Drawing.Size(15, 14);
                SI.TabIndex = 5;
                SI.UseVisualStyleBackColor = true;
                if (dt.Rows[i]["PP_Shift1_Status"].ToString().ToLower().Equals("true") && (!dt.Rows[i]["PP_Shift1_Percent"].ToString().ToLower().Equals("0.00") || dt.Rows[i]["PP_Shift1_Percent"].ToString().ToLower().Equals("100.00")))
                {
                    SI.Checked = true;
                    Shift++;
                    if (!ItemI.Text.Equals("" + Pq_vPart_No.SelectedValue) && !ItemI.Text.Equals("0"))
                    {
                        if ((dt.Rows[i]["PP_Shift1_Percent"].ToString().ToLower().Equals("100.00")))
                        {
                            SI.Enabled = false;
                        }
                    }
                   
                }
                SI.CheckedChanged += new System.EventHandler(this.SI_CheckedChanged);
                Plan1.Controls.Add(SI);



                Label label17 = new Label();

                label17.AutoSize = true;
                label17.Location = new System.Drawing.Point(0, 96);
                label17.Name = "label17" + i;
                label17.Size = new System.Drawing.Size(35, 14);
                label17.TabIndex = 4;
                label17.Text = "S-III";
                Plan1.Controls.Add(label17);


                Label label16 = new Label();

                label16.AutoSize = true;
                label16.Location = new System.Drawing.Point(5, 66);
                label16.Name = "label16" + i;
                label16.Size = new System.Drawing.Size(30, 14);
                label16.TabIndex = 3;
                label16.Text = "S-II";
                Plan1.Controls.Add(label16);

                Label label15 = new Label();
                label15.AutoSize = true;
                label15.Location = new System.Drawing.Point(10, 36);
                label15.Name = "label15" + i;
                label15.Size = new System.Drawing.Size(25, 14);
                label15.TabIndex = 2;
                label15.Text = "S-I";
                Plan1.Controls.Add(label15);




            }


            txt_Req_Qty.Text = Plan_Qty.ToString();
            S_Qty.Text = Plan_Qty.ToString();
            S_Shift.Text = Shift.ToString();
            Calculation11();
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

      public  bool validate_Qty()
        {

            if (int.Parse(S_Qty.Text) < int.Parse(txt_Req_Qty.Text))
            {
                Error_Message = "Selected Qty  is Less than Plan Qty";
                return true;
            }


            if (int.Parse(S_Qty.Text) > int.Parse(txt_Req_Qty.Text))
            {
                Error_Message = "Selected Qty  is More than Plan Qty";
                return true;
            }

            return false;
        }

        private void SI_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox C = (CheckBox)sender;
                TextBox t = (TextBox)this.Controls.Find("SIV" + C.Name.Replace("SI", ""), true)[0];
                TextBox ItemID = (TextBox)this.Controls.Find("ItemI" + C.Name.Replace("SI", ""), true)[0];

                if (C.Checked == true)
                {
                    if (int.Parse(txt_Req_Qty.Text) - int.Parse(S_Qty.Text) <= 0)
                    {
                        MessageBox.Show("Selected Qty is More than Plan Qty", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        C.Checked = false;
                        return;
                    }
                    else
                    {
                        if (int.Parse(txt_Req_Qty.Text) - int.Parse(S_Qty.Text) > int.Parse(txt_Per_Shift_Production.Text))
                        {
                            t.Text = txt_Per_Shift_Production.Text;
                        }
                        else
                        {
                            t.Text = (int.Parse(txt_Req_Qty.Text) - int.Parse(S_Qty.Text)).ToString();
                        }
                    }
                    S_Shift.Text = (int.Parse(S_Shift.Text) + 1).ToString();
                    t.Enabled = true;
                    ItemID.Text = Pq_vPart_No.SelectedValue.ToString();
                }
                else
                {
                    S_Shift.Text = (int.Parse(S_Shift.Text) - 1).ToString();
                    S_Qty.Text = (int.Parse(S_Qty.Text) - int.Parse(txt_Per_Shift_Production.Text)).ToString();
                    t.Text = "0";
                    t.Enabled = false;
                    ItemID.Text = "0";
                }
                Calculation();
            }
            catch { }
        }


        private void SII_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox C = (CheckBox)sender;
            TextBox t = (TextBox)this.Controls.Find("SIIV" + C.Name.Replace("SII", ""), true)[0];
            TextBox ItemID = (TextBox)this.Controls.Find("ItemII" + C.Name.Replace("SII", ""), true)[0];
          
            if (C.Checked == true)
            {
                if (int.Parse(txt_Req_Qty.Text) - int.Parse(S_Qty.Text) <= 0)
                {
                    MessageBox.Show("Selected Qty is More than Plan Qty", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    C.Checked = false;
                    return;
                }
                else
                {
                    if ((int.Parse(txt_Req_Qty.Text) - int.Parse(S_Qty.Text)) > int.Parse(txt_Per_Shift_Production.Text))
                    {
                        t.Text = txt_Per_Shift_Production.Text;
                    }
                    else
                    {
                        t.Text = (int.Parse(txt_Req_Qty.Text) - int.Parse(S_Qty.Text)).ToString();
                    }
                }
                S_Shift.Text = (int.Parse(S_Shift.Text) + 1).ToString();
                t.Enabled = true;
                ItemID.Text = Pq_vPart_No.SelectedValue.ToString();
            }
            else
            {
                S_Shift.Text = (int.Parse(S_Shift.Text) - 1).ToString();
                S_Qty.Text = (int.Parse(S_Qty.Text) - int.Parse(txt_Per_Shift_Production.Text)).ToString();
                t.Text = "0";
                t.Enabled = false;
                ItemID.Text = "0";
            }
            Calculation();
        }


        private void SIII_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox C = (CheckBox)sender;
            TextBox t = (TextBox)this.Controls.Find("SIIIV" + C.Name.Replace("SIII", ""), true)[0];
            TextBox ItemID = (TextBox)this.Controls.Find("ItemIII" + C.Name.Replace("SIII", ""), true)[0];
          
            
            if (C.Checked == true)
            {
              
                //S_Qty.Text = (int.Parse(S_Qty.Text) + int.Parse(txt_Per_Shift_Production.Text)).ToString();

                if (int.Parse(txt_Req_Qty.Text) - int.Parse(S_Qty.Text) <= 0)
                {
                    MessageBox.Show("Selected Qty is More than Plan Qty", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   
                    C.Checked = false;
                    return;
                }
                else
                {
                    if (int.Parse(txt_Req_Qty.Text) - int.Parse(S_Qty.Text) > int.Parse(txt_Per_Shift_Production.Text))
                    {
                        t.Text = txt_Per_Shift_Production.Text;
                    }
                    else
                    {
                        t.Text = (int.Parse(txt_Req_Qty.Text) - int.Parse(S_Qty.Text)).ToString();
                    }
                }
                S_Shift.Text = (int.Parse(S_Shift.Text) + 1).ToString();
                t.Enabled = true;
                ItemID.Text = Pq_vPart_No.SelectedValue.ToString();
            }
            else
            {
                S_Shift.Text = (int.Parse(S_Shift.Text) - 1).ToString();
                S_Qty.Text = (int.Parse(S_Qty.Text) - int.Parse(txt_Per_Shift_Production.Text)).ToString();
                t.Text = "0";
                t.Enabled = false;
                ItemID.Text = "0";
            }
            Calculation();
           
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            Calculation();
            if (validate_Qty())
            {
                MessageBox.Show(Error_Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

            S_Shift.Text = Shift.ToString();
            S_Qty.Text = qty.ToString();
            txt_Req_Qty.Text = qty.ToString();

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

            S_Shift.Text = Shift.ToString();
            S_Qty.Text = qty.ToString();


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
                btnsave.Focus();
            }
        }



        }

       


       
    }

