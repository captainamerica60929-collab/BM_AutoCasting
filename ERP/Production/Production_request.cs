using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;

namespace CRM_App.production
{
    public partial class Production_request : Form
    {
        public bool isReqNo_Load = false;
        public string MachineID = "";
        public string Mould_ID = "";
        
        public Production_request()
        {
            InitializeComponent();
        }

       
        private void Production_request_Load(object sender, EventArgs e)
        {
            display1();
            LoadMachineNO();
            dataGridView2.Visible = false;
            RevRights();
            Pq_vPart_No.Focus();
            display();
            LoadRMSpec();
            Load_Part_Number();
            LoadRouteCardNo();
            loadpalt();
         
 
            LoadReqNo();
            Pq_Route_Card_Start_Date.MinDate = System.DateTime.Now.AddDays(-3);
            //LoadMachineNO();
           // customerload();
        }

        private void loadpalt()
        {
            DataTable a = dbFunctions.getTable("select * from plant_master where pl_status='A'");
            plant.DataSource = a;
            plant.DisplayMember = "PL_Name";
            plant.ValueMember = "PL_ID";
            plant.SelectedIndex = -1;
        }

        private void customerload()
        {
            try
            {
                //DataTable dt = dbFunctions.getTable("select * from Supplier_Master where SM_Status='A''");

                //comboBox1.DataSource = dt;
                //comboBox1.DisplayMember = "SM_Name";
                //comboBox1.ValueMember = "SM_ID";
                //comboBox1.SelectedIndex = -1;
               
            }
            catch
            {
            }
            
        }

        public void RevRights()
        {
            if (dbFunctions.username == "admin" || dbFunctions.username == "ADMIN")
            {
                Pq_vDocNo.Enabled = true;
                Pq_vRevNo.Enabled = true;
                Pq_dRevDate.Enabled = true;
                
            }
            else
            {
                Pq_vDocNo.Enabled = false;
                Pq_vRevNo.Enabled = false;
                Pq_dRevDate.Enabled = false;
            }
        }
        
        public void LoadMachineNO()
        {
            try
            {
               // DataTable dt = dbFunctions.getTable("pr_LoadMachine '" + Pq_vPart_No.SelectedValue.ToString() + "'");
               DataTable dt = dbFunctions.getTable("pr_LoadMachine ");
                Pq_Machine_Name.DataSource = dt;
                Pq_Machine_Name.DisplayMember = "MM_MachineName";
                Pq_Machine_Name.ValueMember = "MM_ID";
                Pq_Machine_Name.SelectedIndex =-1;
                //isReqNo_Load = true;
            }
            catch
            {
            }
        }

        private void LoadRMSpec()

        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_getBOMDetails '" + Pq_vPart_No.SelectedValue.ToString() + "'");
                Pq_RM_Spec.DataSource = dt;
                Pq_RM_Spec.DisplayMember = "IM_PartNo";
                Pq_RM_Spec.ValueMember = "IM_ID";
                Pq_RM_Spec.SelectedIndex =0;
                //isRMSpec_Load = true;
            }
            catch
            {
            }
        }

        public void LoadRouteCardNo()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_GenerateRouteCardNo");
                if(dt.Rows.Count>0)
                {
                    Pq_Route_Card_No.Text = dt.Rows[0][0].ToString();
                }

            }
            catch
            {
            }
        }

        public void LoadReqNo()
        {

            try
            {
                DataTable dt = dbFunctions.getTable("pr_GenerateRequestNo");
                if(dt.Rows.Count>0)
                {
                Pq_RequestNo.Text = dt.Rows[0][0].ToString();
                }

            }
            catch
            {
            }
        }

        private void Load_Part_Number()
        {
            try
            {
                //DataTable dt = dbFunctions.getTable("pr_fetch_ItemBoM");
                DataTable dt = dbFunctions.getTable("Pr_Get_PartName_For_Production");
                Pq_vPart_No.DataSource = dt;
                Pq_vPart_No.DisplayMember = "IM_PartName";
                Pq_vPart_No.ValueMember = "IM_ID";
                Pq_vPart_No.SelectedIndex = -1;
                isReqNo_Load = true;
            }
            catch
            {
            }
        }

        private void Pq_vPart_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isReqNo_Load)
                {
                    DataTable dt = dbFunctions.getTable("pr_get_PartName_Details '" + Pq_vPart_No.SelectedValue.ToString() + "'");
                    
                        Pq_vPart_Name.Text = dt.Rows[0]["IM_PartNo"].ToString();
                    Pq_vModel.Text = dt.Rows[0]["ML_Model"].ToString();
                    Pq_Mould_Name.Text = dt.Rows[0]["MLD_Mold"].ToString();
                    //Pq_Mould_Number.Text = dt.Rows[0]["IM_MouldNumer"].ToString();
                    Pq_Mould_Number.Text = dt.Rows[0]["MLD_MouldNo"].ToString();
                    // Pq_Machine_Name.Text = dt.Rows[0]["MM_MachineName"].ToString();
                    MLD_MoldCavity.Text = dt.Rows[0]["MLD_MoldCavity"].ToString();
                    txt_No_Of_Cavity.Text = dt.Rows[0]["MLD_MoldCavity"].ToString();
                    txt_Cycle_time.Text = dt.Rows[0]["MLD_Cycle_Time"].ToString();
                    MachineID = dt.Rows[0]["MM_ID"].ToString();
                    Mould_ID = dt.Rows[0]["MLD_ID"].ToString();
                    part_price.Text = dt.Rows[0]["IM_Sales_Price"].ToString();

                    

                    Pq_RM_Spec.Text = "";
                    Pq_RM_Grade.Text = "";
                    txtRMQty.Text = "";
                    txtUOM.Text = "Kg";
                    txtStockQty.Text = "";


                    LoadRMSpec();
                    SpecDetails();
                    loadMould();
                    //LoadMachineNO();
                    pq_End_Date.Value = DateTime.Parse(dt.Rows[0]["Plan Date"].ToString());
                    Pq_RM_Plan_Qty.Text = dt.Rows[0]["Plan Qty"].ToString();
                    ShotsCalculation();



                    //DataTable dd = dbFunctions.getTable("pr_get_Mould_TODAY_PM_Need_Mould '" + Pq_Mould_Number.Text + "'");
                    //if (dd.Rows.Count > 0)
                    //{
                    //    if (decimal.Parse(dd.Rows[0]["Remaing Days"].ToString()) <= 0)
                    //    {
                    //        btnsave.Enabled = false;
                    //        label37.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        btnsave.Enabled = true;
                    //        label37.Visible = false;
                    //    }


                    //    label35.Text = dd.Rows[0]["Next PM Date"].ToString();
                    //    label33.Text = dd.Rows[0]["Remaing Days"].ToString();

                    //    label36.Visible = true;
                    //    pictureBox1.Visible = true;
                    //    label33.Visible = true;
                    //    label35.Visible = true;
                    //    label34.Visible = true;
                    //}
                    //else
                    //{

                    //    label36.Visible = false;
                    //    pictureBox1.Visible = false;
                    //    label33.Visible = false;
                    //    label35.Visible = false;
                    //    label34.Visible = false;
                    //}

                    DataTable dd = dbFunctions.getTable("pr_get_vali_demo '" + Pq_Mould_Number.Text + "'");

                    if (dd.Rows.Count > 0)
                    {
                        // Handle possible DBNull and convert safely to decimal
                        decimal fq = dd.Rows[0]["fq"] != DBNull.Value ? Convert.ToDecimal(dd.Rows[0]["fq"]) : 0;
                        decimal val = dd.Rows[0]["After PM Prod. Shot"] != DBNull.Value ? Convert.ToDecimal(dd.Rows[0]["After PM Prod. Shot"]) : 0;

                        // Compare values
                        if (fq < val)
                        {
                            btnsave.Enabled = false;
                            label37.Visible = true;
                        }
                        else
                        {
                            btnsave.Enabled = true;
                            label37.Visible = false;
                        }
                    }
                    else
                    {
                        // No rows found from the stored procedure
                        btnsave.Enabled = false;
                        label37.Visible = true;
                    }



                }



                DataTable dd1 = dbFunctions.getTable("get_Pro_qty " + Pq_vPart_No.SelectedValue.ToString() + ",'" + Pq_Route_Card_Start_Date.Value.ToString("dd-MMM-yyyy") + "'");
                if (dd1.Rows.Count > 0)
                {
                    Plan_Qty_.Text = dd1.Rows[0][0].ToString();
                    Prod_Qty.Text = dd1.Rows[0][1].ToString();
                    Balance_Qty.Text = dd1.Rows[0][2].ToString();


                }

            }
            catch { }
        }

        public void loadMould()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_ProdReq_Mould_Details '" + Pq_Mould_Number.Text.ToString() + "'");

                if (dt.Rows.Count > 0)
                {
                    Pq_Mould_Number.Text = dt.Rows[0]["MLD_MouldNo"].ToString();
                    txtMouldLife.Text = dt.Rows[0]["MLD_MouldLife"].ToString();
                    txtremainingshots.Text = dt.Rows[0]["MLD_OpeningShots"].ToString();
                }
            }
            catch
            {
            }
           
        }

        public void SpecDetails()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_RMSpec_PartNameDetails '" +Pq_vPart_No.SelectedValue+"','"+ Pq_RM_Spec.SelectedValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    Pq_RM_Grade.Text = dt.Rows[0]["IM_PartName"].ToString();
                    txtRMQty.Text = dt.Rows[0]["BM_Qty"].ToString();
                    txtUOM.Text = "Kg";
                    txtStockQty.Text = dt.Rows[0]["Qty"].ToString();


                }
            }
            catch { }
        }
        private void Pq_Mould_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(isMoldLoad)
            //{
            //    DataTable dt = dbFunctions.getTable("pr_Get_ProdReq_Mould_Details '" + Pq_Mould_Name.Text.ToString() + "'");
            //    Pq_Mould_Number.Text = dt.Rows[0]["MLD_MouldNo"].ToString();
            //    txtMouldLife.Text = dt.Rows[0]["MLD_MouldLife"].ToString();
            //    txtremainingshots.Text = dt.Rows[0]["MLD_OpeningShots"].ToString();
                    
            //}
        }

        private void txtMouldLife_TextChanged(object sender, EventArgs e)
        {
            ShotsCalculation();
        }

        public void ShotsCalculation()
        {
            try
            {

                decimal Requied =  (int.Parse(Pq_RM_Plan_Qty.Text) / int.Parse(MLD_MoldCavity.Text));
                txtrequiredshots.Text = Requied.ToString();

            }
            catch { }
        }

        private void txtremainingshots_TextChanged(object sender, EventArgs e)
        {
            ShotsCalculation();
        }

        private void Pq_RM_Spec_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable dt = dbFunctions.getTable("pr_Get_RMSpec_PartNameDetails '" + Pq_RM_Spec.SelectedValue.ToString() + "'");
            //    if (dt.Rows.Count > 0)
            //    {
            //        Pq_RM_Grade.Text = dt.Rows[0]["IM_Grade"].ToString();
            //        txtRMQty.Text = dt.Rows[0]["BM_Qty"].ToString();
            //        txtUOM.Text = dt.Rows[0]["UM_UOM"].ToString();

            //    }
            //}
            //catch { }
            SpecDetails();
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

        private void Pq_Mould_Name_TextChanged(object sender, EventArgs e)
        {

            DataTable dt = dbFunctions.getTable("pr_Get_ProdReq_Mould_Details '" + Pq_Mould_Name.Text.ToString() + "'");

            if (dt.Rows.Count > 0)
            {
                Pq_Mould_Number.Text = dt.Rows[0]["MLD_MouldNo"].ToString();
                txtMouldLife.Text = dt.Rows[0]["MLD_MouldLife"].ToString();
                txtremainingshots.Text = dt.Rows[0]["MLD_OpeningShots"].ToString();
            }
        }
        public string ErrorMessage = "";
        public bool Validate()
        {

            if ((string.IsNullOrEmpty(Pq_RM_Plan_Qty.Text.Trim())))
            {
                ErrorMessage = "Plan Qty Should Not be Empty";
                Pq_RM_Plan_Qty.Focus();
                return true;
            }

            if ((string.IsNullOrEmpty(Pq_Machine_Name.Text.Trim())))
            {
                ErrorMessage = "Machine Name Should Not be Empty";
                Pq_Machine_Name.Focus();
                return true;
            }

            if ((string.IsNullOrEmpty(Pq_vPart_No.Text.Trim())))
            {
                ErrorMessage = "Part No Should Not be Empty";
                Pq_vPart_No.Focus();
                return true;
            }
            try
            {
                int x = int.Parse(Pq_vPart_No.SelectedValue.ToString());
            }
            catch
            {
                ErrorMessage = "Select Proper Part No";
                Pq_vPart_No.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(Pq_RM_Spec.Text.Trim())))
            {
                ErrorMessage = "Part Name Should Not be Empty";
                Pq_RM_Spec.Focus();
                return true;
            }

            if ((string.IsNullOrEmpty(plant.Text.Trim())))
            {
                ErrorMessage = "plant Should Not be Empty";
                plant.Focus();
                return true;
            }

            //try
            //{
            //    int x = int.Parse(Pq_RM_Spec.SelectedValue.ToString());
            //}
            //catch
            //{
            //    ErrorMessage = "Select Proper Part Name";
            //    Pq_RM_Spec.Focus();
            //    return true;
            //}



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
               // Update();
            }
            else
            {
                LoadRouteCardNo();

                LoadReqNo();
                if (btnsave.Text == "&Save")
                {
                    insert();
                    Clear();
                }
                else
                {
                    Update1();
                    btnsave.Text = "Save";
                    Clear();
                }

            }
        }

        private void Update1()
        {
            DataTable a = dbFunctions.getTable("update Production_Request set Pq_RM_Plan_Qty='" + Pq_RM_Plan_Qty.Text + "',pq_RM_Req_Qty='" + txtReqQty.Text + "' where Pq_iid='" + upid.ToString() + "'");
            MessageBox.Show("Details Update Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnsave.Text="&Save";
            Clear();
            display();
            LoadRouteCardNo();
            LoadReqNo();
            Load_Part_Number();

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
                com.CommandText = "pr_Insert_Production_Request";


                com.Parameters.Add("@Pq_RequestNo", SqlDbType.VarChar).Value = Pq_RequestNo.Text.ToString();
                com.Parameters.Add("@Pq_Route_Card_No", SqlDbType.VarChar).Value = Pq_Route_Card_No.Text.ToString();
                com.Parameters.Add("@Pq_Route_Card_Start_Date", SqlDbType.DateTime).Value = Pq_Route_Card_Start_Date.Value.ToString("dd-MMM-yyyy hh:mm tt");

                com.Parameters.Add("@Pq_iPart_ID", SqlDbType.Int).Value = Pq_vPart_No.SelectedValue.ToString();
                com.Parameters.Add("@Pq_vPart_No", SqlDbType.VarChar).Value = Pq_vPart_No.Text.ToString();
                com.Parameters.Add("@Pq_vPart_Name", SqlDbType.VarChar).Value = Pq_vPart_Name.Text.ToString();

                com.Parameters.Add("@Pq_RM_ID", SqlDbType.Int).Value = Pq_RM_Spec.SelectedValue.ToString();
                com.Parameters.Add("@Pq_RM_Spec", SqlDbType.VarChar).Value = Pq_RM_Spec.Text.ToString();
                com.Parameters.Add("@Pq_RM_Grade", SqlDbType.VarChar).Value = Pq_RM_Grade.Text.ToString();

                com.Parameters.Add("@Pq_RM_Plan_Qty", SqlDbType.VarChar).Value = Pq_RM_Plan_Qty.Text.ToString();
                com.Parameters.Add("@Pq_vModel", SqlDbType.VarChar).Value = Pq_vModel.Text.ToString();
                //com.Parameters.Add("@Pq_Operation", SqlDbType.Int).Value = MLD_Customer.SelectedValue.ToString();
                com.Parameters.Add("@Pq_Machine_ID", SqlDbType.Int).Value = MachineID;
                com.Parameters.Add("@Pq_Machine_Name", SqlDbType.VarChar).Value = Pq_Machine_Name.Text.ToString();
                com.Parameters.Add("@Pq_vDocNo", SqlDbType.VarChar).Value = Pq_vDocNo.Text.ToString();
                com.Parameters.Add("@Pq_vRevNo", SqlDbType.VarChar).Value = Pq_vRevNo.Text.ToString();
                com.Parameters.Add("@Pq_dRevDate", SqlDbType.VarChar).Value = Pq_dRevDate.Text.ToString();
                com.Parameters.Add("@Pq_Mould_Id", SqlDbType.Int).Value = Mould_ID;
                com.Parameters.Add("@Pq_Mould_Name", SqlDbType.VarChar).Value = Pq_Mould_Name.Text.ToString();
                com.Parameters.Add("@Pq_Mould_Number", SqlDbType.VarChar).Value = Pq_Mould_Number.Text.ToString();
                com.Parameters.Add("@Pq_Createdby", SqlDbType.VarChar).Value = dbFunctions.username;
                com.Parameters.Add("@pq_RM_Req_Qty", SqlDbType.VarChar).Value = txtReqQty.Text.ToString();
                com.Parameters.Add("@pq_End_Date", SqlDbType.VarChar).Value = pq_End_Date.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@txt_Hourly_Pro", SqlDbType.VarChar).Value = txt_Hourly_Pro.Text.ToString();
                com.Parameters.Add("@pq_plid", SqlDbType.VarChar).Value = plant.SelectedValue.ToString();
                com.Parameters.Add("@pq_plname", SqlDbType.VarChar).Value = plant.Text.ToString();
                com.Parameters.Add("@part_price", SqlDbType.VarChar).Value = part_price.Text.ToString();



                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                display(); 
                LoadRouteCardNo();
                LoadReqNo();
                Load_Part_Number();
                btnsave.Text = "&Save";
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Clear()
        {
            Pq_vPart_No.Text = "";
            Pq_vPart_Name.Text = "";
            Pq_RM_Spec.SelectedValue = -1;
            Pq_RM_Spec.Text = "";
            Pq_RM_Grade.Text = "";
            Pq_RM_Plan_Qty.Text = "";
            Pq_vModel.Text = "";
            txtRMQty.Text = "";
            Pq_Machine_Name.SelectedValue = -1;
            Pq_Machine_Name.Text = "";
            txtUOM.Text.ToString();
            Pq_Mould_Name.Text = "";
            Pq_Mould_Number.Text = "";
            txtUOM.Text = "";
            txtStockQty.Text = "";
            txtReqQty.Text = "";
            txtMouldLife.Text = "0";
            txtremainingshots.Text = "0";
            txtrequiredshots.Text = "0";
            plant.SelectedValue = -1;


        }

        public void display()
        {
            DataTable dt = dbFunctions.getTable("pr_Display_Production_Request");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        public void Delete()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("pr_Delete_Production_Request " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    display();
                    //Clear();
                }
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pq_RM_Plan_Qty_KeyDown(object sender, KeyEventArgs e)
        {
            char newchar = Convert.ToChar(e.KeyValue);
            if(char.IsControl(newchar))
            {
                return;
            }
            int value;
            e.SuppressKeyPress=int.TryParse((sender as TextBox).Text+newchar.ToString(),out value)?value==0:true;
        }

        private void Pq_RM_Plan_Qty_TextChanged(object sender, EventArgs e)
        {
            Calc();
            calculate();

            try
            {
                txtrequiredshots.Text = (decimal.Parse(Pq_RM_Plan_Qty.Text.ToString()) / decimal.Parse(MLD_MoldCavity.Text.ToString())).ToString("0.00");
            }
            catch { }
        }

        void Calc()
        {
            try
            {

                txtReqQty.Text = ((decimal.Parse(Pq_RM_Plan_Qty.Text) * decimal.Parse(txtRMQty.Text)) / 1000).ToString("0.00");


            }
            catch { }
        }

        private void txtRMQty_TextChanged(object sender, EventArgs e)
        {
            Calc();
        }

        private void txt_Cycle_time_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }

        void calculate()
        {
            try
            {
                txt_Hourly_Pro.Text = (((60.0m / decimal.Parse(txt_Cycle_time.Text)) * 60.0m) * decimal.Parse(txt_No_Of_Cavity.Text)).ToString("0");

                //txt_Per_Shift_Production.Text = (decimal.Parse(txt_Hourly_Pro.Text) * 8).ToString("0");
                txt_Per_Shift_Production.Text = (decimal.Parse(txt_Hourly_Pro.Text) * 12).ToString("0");
                txt_Reqesting_Shift.Text = (Math.Round(decimal.Parse(Pq_RM_Plan_Qty.Text) / decimal.Parse(txt_Per_Shift_Production.Text))).ToString("0");
            }
            catch { }
        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void TextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (dataGridView2.Rows.Count >= 1)
                    {
                        dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.SelectedRows[0].Index + 1].Cells[1];
                    }
                   
                }
                
                if (e.KeyCode == Keys.Up)
                {
                    if (dataGridView2.Rows.Count >= 1)
                    {
                        dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.SelectedRows[0].Index - 1].Cells[1];
                    }
                   
                }
                //  textBox6.Text = "";
                if (e.KeyCode == Keys.Enter)
                {
                    if (Pq_vPart_No.Text == "")
                    {
                        // prodid = int.Parse(dataGridView2.SelectedRows[0].Cells["Prod"].Value.ToString());
                        Pq_vPart_No.Text = dataGridView2.SelectedRows[0].Cells["IM_PartName"].Value.ToString();
                        Pq_vPart_Name.Text = dataGridView2.SelectedRows[0].Cells["IM_PartNo"].Value.ToString();

                        dataGridView2.Visible =  false;
                        //pe_qty.Focus();
                    }
                    else if (Pq_vPart_Name.Text != "")
                    {
                        // prodid = int.Parse(dataGridView2.SelectedRows[0].Cells["Prod"].Value.ToString());
                        Pq_vPart_Name.Text = dataGridView2.SelectedRows[0].Cells["IM_PartNo"].Value.ToString();
                        Pq_vPart_No.Text = dataGridView2.SelectedRows[0].Cells["IM_PartName"].Value.ToString();
                        dataGridView2.Visible = false;
                        textBox6.Text = "";
                        Pq_RM_Plan_Qty.Focus();
                    }
                    else
                    {
                        // pe_medicine.Focus();
                    }
                    LoadRMSpec();
                }
            }
            catch { }
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (isReqNo_Load)
                {
                    DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];

                    Pq_vPart_No.Text = row.Cells["IM_PartName"].Value.ToString();
                     isReqNo_Load = true;
                    Pq_vPart_Name.Text = row.Cells["IM_PartNo"].Value.ToString();
                }

            }
            textBox6.Text = "";
            dataGridView2.Visible = false;
            LoadRMSpec();
            SpecDetails();
        }

        private void DataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //    DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];

                ////    Pq_vPart_No.Text = row.Cells["IM_PartNo"].Value.ToString();
                //if (dataGridView2.CurrentRow != null) // Ensure a row is selected
                //{
                //    DataGridViewRow row = dataGridView2.CurrentRow;

                //    // Retrieve the value from the "IM_PartNo" column
                //    if (row.Cells["IM_PartName"].Value != null) //(row.Cells["IM_PartNo"].Value != null)
                //    {
                //        Pq_vPart_No.Text = row.Cells["IM_PartName"].Value.ToString();
                //        Pq_vPart_Name.Text = row.Cells["IM_PartNo"].Value.ToString();
                //    }
                //}
            }
            dataGridView2.Visible = false;
            textBox6.Text = "";
            
        }
        public void display1()
        {
            DataTable dt = dbFunctions.getTable("Pr_Get_PartName_For_Production1");
            dataGridView2.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView2);

        }
        private void TextBox6_TextChanged(object sender, EventArgs e)
        {

            dataGridView2.Visible = true;
            try
            {

                if (string.IsNullOrEmpty(textBox6.Text))
                {
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = string.Format("[IM_PartNo] LIKE '%{0}%' or [IM_PartName] LIKE '%{0}%' or [IM_Model] LIKE '%{0}%' ", textBox6.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox6_Leave(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
        String upid = "0";
        private void button1_Click(object sender, EventArgs e)
        {
            btnsave.Text = "Update";
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                Pq_vPart_No.Text = row.Cells["Part Name"].Value?.ToString();
                Pq_RM_Plan_Qty.Text= row.Cells["Plan Qty"].Value?.ToString();
                upid= row.Cells["ID"].Value?.ToString();



            }
        }

        private void dataGridView2_Leave(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0) // Ensure at least one row is selected
            {
                DataGridViewRow row = dataGridView2.SelectedRows[0]; // Get the first selected row

                Pq_vPart_No.Text = row.Cells["IM_PartName"].Value?.ToString() ?? "";
                Pq_vPart_Name.Text = row.Cells["IM_PartNo"].Value?.ToString() ?? "";
            }
            else
            {
                MessageBox.Show("Please select a row.");
            }
            textBox6.Text = "";
            dataGridView2.Visible = false;
        }

        private void dataGridView2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                //if (dataGridView2.SelectedRows.Count > 0) // Ensure at least one row is selected
                //{
                //    DataGridViewRow row = dataGridView2.SelectedRows[0]; // Get the first selected row

                //    Pq_vPart_No.Text = row.Cells["IM_PartName"].Value?.ToString() ?? "";
                //    Pq_vPart_Name.Text = row.Cells["IM_PartNo"].Value?.ToString() ?? "";
                //}
                //else
                //{
                //    MessageBox.Show("Please select a row.");
                //}
                //textBox6.Text = "";
                //dataGridView2.Visible = false;
            }
        }
    }
}
