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

namespace CRM_App.Transaction.Work_Order
{
    
    public partial class Work_Order : Form
    {
        public bool isReqNo_Load = false;
        public Work_Order()
        {
            InitializeComponent();
        }

        private void Work_Order_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;

            panel1.Visible = false;
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView2);
            dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            LoadPo_No();
            LoadSupplier();
            Rmload();
            LoadItemType();
            wos_MaterialName.Text = "RM";
            display1();
            //wor_MaterialName.Text= "RM";
            display();
            loaddisplay();
            dataGridView3.Visible = false;
        }

        private void loaddisplay()
        {
            DataTable dt = dbFunctions.getTable("Pr_Get_PartName_DC_Production");
            dataGridView5.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView5);
        }

        public void LoadSupplier()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadSupplier");
                wos_SupplierName.DataSource = dt;
                wos_SupplierName.DisplayMember = "SM_Name";
                wos_SupplierName.ValueMember = "SM_ID";
                wos_SupplierName.SelectedIndex = -1;
              

            }
            catch
            {
            }
        }



        private void LoadPo_No()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_WO_Number");
                PO_vPO_NO.Text = dt.Rows[0][0].ToString();


            }
            catch
            {
            }
        }

        public void LoadItemType()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadItemType");
                wos_MaterialName.DataSource = dt;
                wos_MaterialName.DisplayMember = "Ty_TypeName";
                wos_MaterialName.ValueMember = "Ty_ID";
                wos_MaterialName.SelectedIndex = -1;

                DataTable dt1 = dbFunctions.getTable("pr_LoadItemType");
                wor_MaterialName.DataSource = dt1;
                wor_MaterialName.DisplayMember = "Ty_TypeName";
                wor_MaterialName.ValueMember = "Ty_ID";
                wor_MaterialName.SelectedIndex = -1;
            }
            catch
            {
            }
        }

        private void PO_vSupplier_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
           try
            { 
                DataTable dt = dbFunctions.getTable("pr_GetSupplier_Details '" + wos_SupplierName.SelectedValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    wos_SupplierId.Text = dt.Rows[0]["SM_PlantAddr"].ToString();
                    PO_dExcise_Duty_Percent.Text = dt.Rows[0]["ED"].ToString();
                    PO_dVAT_CST_Percentage.Text = dt.Rows[0]["TT"].ToString();

                    wos_MaterialType.Text = "";
                    POD_vSource.Text = "";
                    POD_iUOM.Text = "";
                  //  wos_Price.Text = "";
                    POD_vSpec.Text = "";
                    POD_vPacking_Std.Text = "";
                    textBox1.Text = "";
                    txtGrade.Text = "";
                    wos_Qty.Text = "";
                    txtTotalPrice.Text = "";
                   
                    LoadItem();
                }
            }
            catch
            {

            }
            
        }

        public void LoadItem()
        {
            //try
            //{
            //    //DataTable dt = dbFunctions.getTable("pr_GetMetireal_PartNo '" + wos_SupplierName.SelectedValue.ToString() + "'");
            //    DataTable dt = dbFunctions.getTable("pr_GetMetireal_PartNo '" + wos_MaterialName.SelectedValue.ToString() + "'");
            //    wos_MaterialType.DataSource = dt;
            //    wos_MaterialType.DisplayMember = "IM_PartNo";
            //    wos_MaterialType.ValueMember = "IM_ID";
            //    wos_MaterialType.SelectedIndex = -1;




            //}
            //catch
            //{
            //}

            try
            {
                //DataTable dt = dbFunctions.getTable("pr_fetch_ItemBoM");
                DataTable dt = dbFunctions.getTable("Pr_Get_PartName_For_Production");
                wos_MaterialType.DataSource = dt;
                wos_MaterialType.DisplayMember = "IM_PartName";
                wos_MaterialType.ValueMember = "IM_ID";
                wos_MaterialType.SelectedIndex = -1;
                isReqNo_Load = true;
            }
            catch
            {
            }
        }

        private void IM_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
               wos_Price.Enabled = false;
            //    wos_Price.ReadOnly = true;

            POD_vSource.Enabled = false;
            POD_vSource.ReadOnly = true;



            txtGrade.Enabled = false;
            txtGrade.ReadOnly = true;


            if (wos_MaterialName.Text.ToUpper().Equals("RM"))
            {
                label21.Text = "Spec :";
                label19.Text = "Grade :";

                try
                {
                    //DataTable dt = dbFunctions.getTable("pr_GetMetireal_PartNo_Store_Issue");
                    //wos_MaterialType.DataSource = dt;
                    //wos_MaterialType.DisplayMember = "IM_PartNo";
                    //wos_MaterialType.ValueMember = "IM_ID";
                    //wos_MaterialType.SelectedIndex = -1;

                }
                catch
                {
                }

            }
            if (wos_MaterialName.Text.ToUpper().Equals("OTHERS"))
            {

                label21.Text = "Part :";
                label19.Text = "Name :";
                wos_Price.Enabled = true;
               // wos_Price.ReadOnly = false;
                POD_vSource.Enabled = true;
                POD_vSource.ReadOnly = false;



                txtGrade.Enabled = true;
                txtGrade.ReadOnly = false;


                try
                {
                //    DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + wos_SupplierName.SelectedValue.ToString() + "'");
                //    wos_MaterialType.DataSource = dt;
                //    wos_MaterialType.DisplayMember = "IM_PartNo";
                //    wos_MaterialType.ValueMember = "IM_ID";
                //    wos_MaterialType.SelectedIndex = -1;
                   
                }
                catch
                {
                }



            }
            else
            {
                label21.Text = "Part :";
                label19.Text = "Name :";
            }
        }
        string Part_ID = "";
        private void POD_vPart_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRMSpec();
            SpecDetails();
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_RMSpec_PartNameDetails '" + Part_ID + "','" + wos_MaterialType.SelectedValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    //textBox4.Text = dt.Rows[0]["IM_PartName"].ToString();

                    // txtRMGrade.Text = dt.Rows[0]["IM_PartName"].ToString();
                    //if (dt.Rows[0]["UM_UOM"].ToString().ToUpper().Equals("KG"))
                    //{
                    //    txtReqQty.Text = ((decimal.Parse(dt.Rows[0]["BM_Qty"].ToString()) * decimal.Parse(txtxplanQty.Text)) / 1000).ToString("0.00");

                    //}
                    //else
                    //{
                    //    txtReqQty.Text = (decimal.Parse(dt.Rows[0]["BM_Qty"].ToString()) * decimal.Parse(txtxplanQty.Text)).ToString("0.00");

                    //}
                    //txtUOM.Text = dt.Rows[0]["UM_UOM"].ToString();
                    //txtStockQty.Text = dt.Rows[0]["Qty"].ToString();
                    //BM_Type.Text = dt.Rows[0]["BM_Type"].ToString();


                }
            }
            catch { }
            try
            {
                DataTable dt = dbFunctions.getTable("pr_getCurrent_Stock1 '" + wos_MaterialType.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    ////POD_vSpec.Text = dt.Rows[0]["IM_PartName"].ToString();
                    //POD_vSource.Text = dt.Rows[0]["IM_RMSource"].ToString();
                    //POD_iUOM.Text = dt.Rows[0]["UM_UOM"].ToString();
                    //textBox1.Text = dt.Rows[0]["IM_HSNCode"].ToString();
                    //POD_vPacking_Std.Text = dt.Rows[0]["IM_PackingStandard"].ToString();
                    wos_Price.Text = dt.Rows[0]["IM_Purchase_Price"].ToString();
                    txtStockQty.Text = dt.Rows[0]["Stock"].ToString();
                }
            }
            catch
            { }
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_PartNo_Details '" + wos_MaterialType.SelectedValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    //POD_vSpec.Text = dt.Rows[0]["IM_PartName"].ToString();
                    textBox4.Text = dt.Rows[0]["IM_PartName"].ToString();
                    POD_vSource.Text = dt.Rows[0]["IM_RMSource"].ToString();
                    txtGrade.Text = dt.Rows[0]["IM_PartName"].ToString();
                    POD_iUOM.Text = dt.Rows[0]["UM_UOM"].ToString();
                    textBox1.Text = dt.Rows[0]["IM_HSNCode"].ToString();
                    POD_vPacking_Std.Text = dt.Rows[0]["IM_PackingStandard"].ToString();
                   wos_Price.Text = dt.Rows[0]["IM_Purchase_Price"].ToString();
                }
            }
            catch
            { }
        }

        private void SpecDetails()
        {
            try
            {
                //DataTable dt = dbFunctions.getTable("pr_Get_RMSpec_PartNameDetails '" + wos_MaterialType.SelectedValue + "','" + Pq_RM_Spec.SelectedValue.ToString() + "'");
                DataTable dt = dbFunctions.getTable("pr_Get_RMSpec_PartNameDetails12 '" + wos_MaterialType.SelectedValue + "','" + comboBox2.SelectedValue.ToString() + "'");
                
                if (dt.Rows.Count > 0)
                {
                    //Pq_RM_Grade.Text = dt.Rows[0]["IM_PartName"].ToString();
                    //  txtRMQty.Text = dt.Rows[0]["BM_Qty"].ToString();
                    //POD_iUOM.Text = "Kg";
                    //  txtStockQty.Text = dt.Rows[0]["Qty"].ToString();
                    textBox5.Text = dt.Rows[0]["BM_Net_Part_Wt"].ToString();
                    textBox2.Text = dt.Rows[0]["BM_Runner_Wt"].ToString();
                    CAVITY1.Text = dt.Rows[0]["BM_MoldCavity"].ToString();
                    //PART_WEIGHT.Text = dt.Rows[0]["BM_Net_Part_Wt"].ToString();
                    //  RUNNER_WEIGHT.Text = dt.Rows[0]["BM_Runner_Wt"].ToString();
                    //CAVITY.Text = dt.Rows[0]["BM_MoldCavity"].ToString();
                    //OYT1.Text = dt.Rows[0]["BM_Qty"].ToString();
                    wos_Price.Text = dt.Rows[0]["IM_Purchase_Price"].ToString();



                }
            }
            catch { }
        }

        private void LoadRMSpec()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_getBOMDetails '" + wos_MaterialType.SelectedValue.ToString() + "'");
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "IM_PartNo";
                comboBox2.ValueMember = "IM_ID";
                comboBox2.SelectedIndex = 0;
                //isRMSpec_Load = true;
            }
            catch
            {
            }
        }

        private void IM_TYPE_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //wor_Price.Enabled = false;
            //wor_Price.ReadOnly = true;

            if (wor_MaterialName.Text.ToUpper().Equals("RM"))
            {
                label21.Text = "Spec :";
                label19.Text = "Grade :";

                try
                {
                    DataTable dt = dbFunctions.getTable("pr_GetMetireal_PartNo_Store_Issue");
                    wor_MaterialType.DataSource = dt;
                    wor_MaterialType.DisplayMember = "IM_PartNo";
                    wor_MaterialType.ValueMember = "IM_ID";
                    wor_MaterialType.SelectedIndex = -1;

                }
                catch
                {
                }
            }

            if (wor_MaterialName.Text.ToUpper().Equals("FG"))
            {
                label21.Text = "Spec :";
                label19.Text = "Grade :";

                try
                {
                    DataTable dt = dbFunctions.getTable("pr_GetMetireal_PartNo_WO_Issue_FG");
                    wor_MaterialType.DataSource = dt;
                    wor_MaterialType.DisplayMember = "IM_PartNo";
                    wor_MaterialType.ValueMember = "IM_ID";
                    wor_MaterialType.SelectedIndex = -1;

                }
                catch
                {
                }
            }
            if (wor_MaterialName.Text.ToUpper().Equals("OTHERS"))
            {

                label21.Text = "Part :";
                label19.Text = "Name :";
                //wor_Price.Enabled = true;
                //wor_Price.ReadOnly = false;
                POD_vSource.Enabled = true;
                POD_vSource.ReadOnly = false;



                txtGrade.Enabled = true;
                txtGrade.ReadOnly = false;


                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + wos_SupplierName.SelectedValue.ToString() + "'");
                    wor_MaterialType.DataSource = dt;
                    wor_MaterialType.DisplayMember = "IM_PartNo";
                    wor_MaterialType.ValueMember = "IM_ID";
                    wor_MaterialType.SelectedIndex = -1;

                }
                catch
                {
                }



            }
            else
            {
                //label21.Text = "Part :";
                //label19.Text = "Name :";
            }
        }

        private void POD_vPart_No_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_PartNo_Details '" + wor_MaterialType.SelectedValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    //POD_vSpec.Text = dt.Rows[0]["IM_PartName"].ToString();
                    POD_vSource.Text = dt.Rows[0]["IM_RMSource"].ToString();
                    txtGrade.Text = dt.Rows[0]["IM_PartName"].ToString();
                    POD_iUOM_1.Text = dt.Rows[0]["UM_UOM"].ToString();
                    textBox1.Text = dt.Rows[0]["IM_HSNCode"].ToString();
                    POD_vPacking_Std.Text = dt.Rows[0]["IM_PackingStandard"].ToString();
                   // wor_Price.Text = dt.Rows[0]["IM_Purchase_Price"].ToString();
                }
            }
            catch
            { }
            partload();

        }

        private void partload()
        {
            DataTable a = dbFunctions.getTable("select wos_PARTWEIGHT,wos_RUNNERWEIGHT,wos_txtRMQty,CAVITY from Work_Order_Send_Details where wos_WorkOrderNo='" + PO_vPO_NO.Text+"' ");
            if (a.Rows.Count > 0)
            {
                textBox5.Text = a.Rows[0]["wos_PARTWEIGHT"].ToString();
                textBox2.Text = a.Rows[0]["wos_RUNNERWEIGHT"].ToString();
                textBox6.Text= a.Rows[0]["wos_txtRMQty"].ToString();
                CAVITY1.Text = a.Rows[0]["CAVITY"].ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            insert();
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
                com.CommandText = "PR_INSERT_Work_Order_Send_Details";
                com.Parameters.Add("@wos_WorkOrderNo", SqlDbType.VarChar).Value = PO_vPO_NO.Text.ToString();
                com.Parameters.Add("@wos_SupplierId", SqlDbType.Int).Value = wos_SupplierName.SelectedValue.ToString();
                com.Parameters.Add("@wos_SupplierName", SqlDbType.VarChar).Value = wos_SupplierName.Text.ToString();
                com.Parameters.Add("@wos_Date", SqlDbType.DateTime).Value = PO_dPO_Date.Value.ToString("dd/MMM/yyyy"); 
                //com.Parameters.Add("@PO_dPO_Date", SqlDbType.DateTime).Value = PO_dPO_Date.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@wos_MaterialType", SqlDbType.VarChar).Value = wos_MaterialName.Text.ToString();
                //com.Parameters.Add("@wos_MaterialId", SqlDbType.Int).Value = wos_MaterialType.SelectedValue.ToString();
                //com.Parameters.Add("@wos_MaterialName", SqlDbType.VarChar).Value = wos_MaterialType.Text.ToString();
                com.Parameters.Add("@wos_MaterialId", SqlDbType.Int).Value = Pq_RM_Spec.SelectedValue.ToString();
                com.Parameters.Add("@wos_MaterialName", SqlDbType.VarChar).Value = Pq_RM_Spec.Text.ToString();
                com.Parameters.Add("@wos_Qty", SqlDbType.VarChar).Value = wos_Qty.Text.ToString();
                com.Parameters.Add("@wos_Price", SqlDbType.VarChar).Value = wos_Price.Text.ToString();
                com.Parameters.Add("@wos_Note", SqlDbType.VarChar).Value = wos_Note.Text.ToString();
                com.Parameters.Add("@wos_Remark", SqlDbType.VarChar).Value = wos_Remark.Text.ToString();
                com.Parameters.Add("@wos_User", SqlDbType.VarChar).Value = dbFunctions.username;
                //com.Parameters.Add("@V_no", SqlDbType.VarChar).Value = V_no.Text.ToString();
                //com.Parameters.Add("@wos_PARTWEIGHT", SqlDbType.VarChar).Value = PART_WEIGHT.Text.ToString();
                //com.Parameters.Add("@wos_RUNNERWEIGHT", SqlDbType.VarChar).Value = RUNNER_WEIGHT.Text.ToString();
                //com.Parameters.Add("@CAVITY", SqlDbType.VarChar).Value = CAVITY.Text.ToString();
                //com.Parameters.Add("@wos_PLAN", SqlDbType.VarChar).Value = PLAN.Text.ToString();
                //com.Parameters.Add("@wos_txtRMQty", SqlDbType.VarChar).Value = txtRMQty.Text.ToString();
                //com.Parameters.Add("@wos_PLAN", SqlDbType.VarChar).Value = int.Parse(PLAN.Text);

                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
               Clear();
                dataGridView3.Visible = true;
                display();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        string ErrorMessage = "";


        public void display()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Work_Order_Send_Details '"+ PO_vPO_NO.Text.ToString() + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);

            DataTable dt1 = dbFunctions.getTable("Pr_Fetch_Work_Order_Send_Details_WO '" + PO_vPO_NO.Text.ToString() + "'");
           if(dt1.Rows.Count > 0)
            {
                wos_SupplierName.Text = dt1.Rows[0]["wos_SupplierName"].ToString();
                PO_dPO_Date.Text = dt1.Rows[0]["wos_Date"].ToString();
                wos_SupplierId.Text = dt1.Rows[0]["SM_PlantAddr"].ToString();
            }
        }

        public void display_Receive()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_WO_Receive_Details '" + PO_vPO_NO.Text.ToString() + "'");
            dataGridView2.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView2);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                       // DataTable dt1 = dbFunctions.getTable("pr_delete_Issued_material " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        DataTable dt = dbFunctions.getTable("Pr_Delete_Work_Order_Send_Details " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
        }

        public void Clear()
        {
            //   wos_SupplierName.Text = "";
            wos_Qty.Text = "";
            //wos_Note.Text = "";
            //wor_Qty.Text = "";
            //wor_Note.Text = "";
            Pq_RM_Spec.Text = "";
        }

        public bool Validate2()
        {
            if ((string.IsNullOrEmpty(wos_MaterialType.Text.Trim())))
            {
                ErrorMessage = "Name Should Not be Empty";
                wos_MaterialType.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(wos_MaterialName.Text.Trim())))
            {

                ErrorMessage = "Type Should Not be Empty";
                wos_MaterialName.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(wos_Qty.Text.Trim())))
            {

                ErrorMessage = "Qty Should Not be Empty";
                wos_Qty.Focus();
                return true;
            }
            return false;
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

        public bool Validate()
        {

            if ((string.IsNullOrEmpty(wos_SupplierName.Text.Trim())))
            {
                ErrorMessage = "Supplier Name Should Not be Empty";
                wos_SupplierName.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(PO_vPO_NO.Text.Trim())))
            {
                ErrorMessage = "DAC NO Not be Empty";
                PO_vPO_NO.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(wos_Qty.Text.Trim())))
            {
                ErrorMessage = "WOS QTY Should Not be Empty";
                wos_Qty.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(wor_Qty.Text.Trim())))
            {
                //ErrorMessage = "WOR QTY Should Not be Empty";
                //wor_Qty.Focus();
                //return true;
            }


            return false;
        }


        private void button1_Click(object sender, EventArgs e)
        {

            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                LoadPo_No();
              
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                //Cursor.Current = Cursors.WaitCursor;

                //DC_Print11 oRpt = new DC_Print11();
                //string SQlQuery = "PR_FETCH_DC_PRINT '" + PO_vPO_NO.Text.ToString() + "'";
                //dbFunctions.printpdf(PO_vPO_NO.Text.ToString(), SQlQuery, oRpt);
                //Cursor.Current = Cursors.Default;
            }
               catch (Exception Ex)

            {
                throw (Ex);
            }
        }

        //private void button4_Click(object sender, EventArgs e)
        //{
            
        //}

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "PR_INSERT_Work_Order_Receive_Details";
                com.Parameters.Add("@wor_WorkOrderNo", SqlDbType.VarChar).Value = PO_vPO_NO.Text.ToString();
                com.Parameters.Add("@wor_SupplierId", SqlDbType.VarChar).Value = wos_SupplierName.SelectedValue.ToString();
                com.Parameters.Add("@WOR_PID", SqlDbType.VarChar).Value = wos_MaterialType.SelectedValue.ToString();
                com.Parameters.Add("@wor_SupplierName", SqlDbType.VarChar).Value = wos_SupplierName.Text.ToString();
                com.Parameters.Add("@wor_Date", SqlDbType.DateTime).Value = PO_dPO_Date.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@wor_MaterialType", SqlDbType.VarChar).Value = wor_MaterialName.Text.ToString();
                com.Parameters.Add("@wor_MaterialId", SqlDbType.VarChar).Value = comboBox2.SelectedValue.ToString();
                com.Parameters.Add("@wor_MaterialName", SqlDbType.VarChar).Value = comboBox2.Text.ToString();
                com.Parameters.Add("@wor_Qty", SqlDbType.VarChar).Value = wor_Qty.Text.ToString();
                com.Parameters.Add("@wor_Price", SqlDbType.VarChar).Value = wor_Price.Text.ToString();
                com.Parameters.Add("@wor_Note", SqlDbType.VarChar).Value = wor_Note.Text.ToString();
                com.Parameters.Add("@wor_User", SqlDbType.VarChar).Value = dbFunctions.username;
                com.Parameters.Add("@wor_supplierqty", SqlDbType.VarChar).Value = textBox7.Text.ToString();
                com.Parameters.Add("@PARTWEIGHT", SqlDbType.VarChar).Value = textBox8.Text.ToString();
                com.Parameters.Add("@RUNNERWEIGHT", SqlDbType.VarChar).Value = textBox9.Text.ToString();
                com.ExecuteNonQuery(); com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                display_Receive();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        DataTable dt = dbFunctions.getTable("UPDATE  Work_Order_Receive_Details set wor_status='D' WHERE wor_Id= '" + dataGridView2.SelectedRows[0].Cells[0].Value.ToString() + "' ");
                       // DataTable dt = dbFunctions.getTable("Pr_Delete_Work_Order_Receive_Details " + dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                        MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        display_Receive();
                        Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //display();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataTable dt = dbFunctions.getTable("Pr_Fetch_Work_Order_Receive_Details  ");
            //dataGridView2.DataSource = dt;
            //dbFunctions.DGVStyle(dataGridView2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wos_SupplierName.Text = "";
            wor_Qty.Text = "";
            wor_Note.Text = "";
        }

        private void POD_iUOM_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void wos_Qty_TextChanged(object sender, EventArgs e)
        {
           // PLAN.Text = (((decimal.Parse(wos_Qty.Text)) * 1000) / decimal.Parse(txtRMQty.Text)).ToString();
          //  PLAN.Text = ((decimal)((decimal.Parse(wos_Qty.Text) * 1000) / decimal.Parse(txtRMQty.Text))).ToString();


        }

        private void wos_Price_TextChanged(object sender, EventArgs e)
        {

        }

        private void wos_Note_TextChanged(object sender, EventArgs e)
        {

        }

        private void PO_vPO_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                display();
                display_Receive();
                display_Scanned_Details();
            }
        }

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Txt_Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comboBox1.Items.Contains(txt_Barcode.Text))
                {

                    DataTable dt = dbFunctions.getTable("pr_get_Barcode_Data  '" + txt_Barcode.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                      //  string IID = wos_MaterialType.SelectedValue.ToString();

                        if (decimal.Parse(dt.Rows[0]["CS_Qty"].ToString()) > 0)
                        {
                            //if (dt.Rows[0]["CS_item_iD"].ToString().Equals(IID))
                            //{
                            Scan_qty.Text = dt.Rows[0]["CS_Qty"].ToString();

                            //    button1.Enabled = true;
                            //}
                            //else
                            //{
                            //    MessageBox.Show("RM Specification Missmatch", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    txt_Barcode.Text = "";
                            //    txt_Barcode.Focus();
                            //    button1.Enabled = false;
                            //}

                        }
                        else
                        {
                            MessageBox.Show("Barcode Already Scanned", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_Barcode.Text = "";
                            txt_Barcode.Focus();
                            button1.Enabled = false;
                        }
                    }
                }
                else
                {
                    txt_Barcode.Text = "";
                    txt_Barcode.Focus();
                    MessageBox.Show("Invalid Barcode", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = false;
                }

                Scan_qty.Focus();
            }
        }

        private void Txt_Barcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void Scan_qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dtFIFO = dbFunctions.getTable("pr_get_Material_Issued_Details_FIFO '" + Route_Card_ID + "','" + txt_Barcode.Text.ToString() + "'");
                //if (dtFIFO.Rows.Count > 0)
                //{
                //    MessageBox.Show("FIFO Error. Previous Lot Available", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                save_data();
            }
        }
        public string Route_Card_ID = "";
        private void save_data()
        {
            DataTable dt = dbFunctions.getTable("pr_get_Barcode_details_By_ID  '" + txt_Barcode.Text + "'");
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Current_Stock1";
                com.Parameters.Add("@CS_Barcode", SqlDbType.VarChar).Value = txt_Barcode.Text.ToString();
                // com.Parameters.Add("@CS_RouteCardNo", SqlDbType.VarChar).Value = Route_Card_ID;PO_vPO_NO
                com.Parameters.Add("@CS_RouteCardNo", SqlDbType.VarChar).Value = PO_vPO_NO.Text.ToString();
                com.Parameters.Add("@CS_Type", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Type"].ToString();
                com.Parameters.Add("@CS_Item_ID", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Item_ID"].ToString();
                com.Parameters.Add("@CS_Part_No", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Part_No"].ToString();
                com.Parameters.Add("@CS_Part_Name", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Part_Name"].ToString();
                com.Parameters.Add("@CS_Lot_No", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Lot_No"].ToString();
                com.Parameters.Add("@CS_Lot_Date", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Lot_Date"].ToString();
                com.Parameters.Add("@CS_Qty", SqlDbType.VarChar).Value = "-" + Scan_qty.Text.ToString();
                com.Parameters.Add("@CS_UOM", SqlDbType.VarChar).Value = dt.Rows[0]["CS_UOM"].ToString();
                com.Parameters.Add("@CS_Issued_By", SqlDbType.VarChar).Value = dbFunctions.username;
                com.ExecuteNonQuery();
                com.Connection.Close();
                dataGridView3.Visible = true;
                display_Scanned_Details();
               // calc();
                Scan_qty.Text = "";
                //wos_Qty.Text = "";
                txt_Barcode.Text = "";
                txt_Barcode.Focus();
                dataGridView3.Visible = true;
                //clear();

            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
              //  MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void calc()
        {
            decimal tot_gst = 0m;
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                if (dataGridView3.Rows[i].Cells["Qty"].Value != null &&
                    int.TryParse(dataGridView3.Rows[i].Cells["Qty"].Value.ToString(), out int qty))
                {
                    tot_gst += qty;
                }
            }

            wos_Qty.Text = tot_gst.ToString();
            //for (int i = 0; i < dataGridView3.Rows.Count; i++)
            //{
            //    tot_gst += decimal.Parse(dataGridView3.Rows[i].Cells["Qty"].Value.ToString());
            //}
            //wos_Qty.Text = tot_gst.ToString("0.00");
        }

        private void display_Scanned_Details()
        {
            dataGridView3.Visible = true;

            //DataTable a = dbFunctions.getTable("select CS_Part_No as [Part no], ABS(CS_Qty) as Qty from current_stock where CS_RouteCardNo='" + PO_vPO_NO.Text+ "'");
            //dataGridView3.DataSource = a;
            DataTable a = dbFunctions.getTable("select CS_iID as [ID],CS_Part_No as [Part no], ABS(CS_Qty) as Qty from current_stock where CS_RouteCardNo='" + PO_vPO_NO.Text + "' and cs_dc='dc' AND CS_Item_ID='"+ Pq_RM_Spec.SelectedValue.ToString()+ "' ");
            dataGridView3.DataSource = a;
            dataGridView3.Columns["ID"].Visible = true;
            decimal tot_gst = 0m;
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                if (dataGridView3.Rows[i].Cells["Qty"].Value != null &&
                    decimal.TryParse(dataGridView3.Rows[i].Cells["Qty"].Value.ToString(), out decimal qty)) // Use decimal.TryParse
                {
                    tot_gst += qty;
                }
            }

            wos_Qty.Text = tot_gst.ToString();



        }

        private void Wos_MaterialType_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    DataTable A = dbFunctions.getTable("SELECT CS_Barcode FROM CURRENT_STOCK WHERE CS_PART_NO='" + wos_MaterialType.Text + "' and CS_RouteCardNo is null");
            //    foreach (DataRow row in A.Rows)
            //    {
            //        comboBox1.Items.Add(row["CS_Barcode"].ToString());
            //    }

            //}
        }

        private void Wos_MaterialType_Leave(object sender, EventArgs e)
        {
            //DataTable A = dbFunctions.getTable("SELECT CS_Barcode FROM CURRENT_STOCK WHERE CS_PART_NO='" + wos_MaterialType.Text + "' and CS_RouteCardNo is null");
            //foreach (DataRow row in A.Rows)
            //{
            //    comboBox1.Items.Add(row["CS_Barcode"].ToString());
            //}
        }

        private void Pq_RM_Spec_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //   // DataTable A = dbFunctions.getTable("SELECT CS_Barcode FROM CURRENT_STOCK WHERE CS_PART_NO='" + Pq_RM_Spec.Text + "' and  CS_Barcode NOT IN ( SELECT CS_Barcode   FROM Current_Stock    GROUP BY CS_Barcode    HAVING COUNT(*) > 1  ) ");
            //   // DataTable A = dbFunctions.getTable("SELECT CS_Barcode FROM CURRENT_STOCK WHERE CS_Item_ID='" + Pq_RM_Spec.SelectedValue.ToString() + "' and CS_RouteCardNo is null  AND CS_Barcode NOT IN ( SELECT CS_Barcode FROM Current_Stock  GROUP BY CS_Barcode  HAVING COUNT(*) > 1) ");
            //    DataTable A = dbFunctions.getTable("SELECT CS_Barcode FROM CURRENT_STOCK WHERE CS_Item_ID = '" + Pq_RM_Spec.SelectedValue.ToString() + "' and CS_RouteCardNo is null  AND CS_Barcode NOT IN(SELECT CS_Barcode FROM Current_Stock  GROUP BY CS_Barcode HAVING COUNT(*) > 1) ");
            //    comboBox1.Text = "";
            //    foreach (DataRow row in A.Rows)
            //    {
            //        comboBox1.Items.Add(row["CS_Barcode"].ToString());
            //    }

            //}

        }

        private void Pq_RM_Spec_Leave(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Items.Clear();
                DataTable A = dbFunctions.getTable("SELECT CS_Barcode FROM CURRENT_STOCK WHERE CS_Item_ID = '" + Pq_RM_Spec.SelectedValue.ToString() + "' and CS_RouteCardNo is null  AND CS_Barcode NOT IN(SELECT CS_Barcode FROM Current_Stock  GROUP BY CS_Barcode HAVING COUNT(*) > 1) ");
               // comboBox1.Text = "";
                foreach (DataRow row in A.Rows)
                {
                    comboBox1.Items.Add(row["CS_Barcode"].ToString());
                }
            }
            catch { }
            


        }

        private void PLAN_TextChanged(object sender, EventArgs e)
        {

        }

        private void Wor_Qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //// textBox7.Text = ((decimal.Parse(wor_Qty.Text) * decimal.Parse(textBox6.Text)) / 1000).ToString("0.00");
                textBox8.Text = ((decimal.Parse(wor_Qty.Text) * decimal.Parse(textBox5.Text)) / 1000).ToString("0.00");
                //// textBox9.Text= ((decimal.Parse(wor_Qty.Text) * decimal.Parse(textBox5.Text))/ decimal.Parse(CAVITY1.Text) / 1000).ToString("0.00");

                textBox9.Text = ((decimal.Parse(wor_Qty.Text) * decimal.Parse(textBox2.Text)) / decimal.Parse(CAVITY1.Text) / 1000).ToString("0.00");
                textBox7.Text = ((decimal.Parse(textBox8.Text) + decimal.Parse(textBox9.Text))).ToString("0.00");

            }
            catch { }
        }

        private void Pq_RM_Spec_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Rmload();
            comboBox1.Items.Clear();
            try
            {
                DataTable a = dbFunctions.getTable("select *,dbo.fget_Current_Stock('" + Pq_RM_Spec.SelectedValue.ToString() + "') as Qty   from item_master where IM_ID='" + Pq_RM_Spec.SelectedValue.ToString() + "'");

                if (a.Rows.Count > 0)
                {

                    wos_Price.Text = a.Rows[0]["IM_Purchase_Price"].ToString();
                    txtStockQty.Text= a.Rows[0]["Qty"].ToString();
                }
            }
            catch { }
        }

        private void Rmload()
        {
            DataTable A = dbFunctions.getTable("SELECT IM_ID,IM_PartName FROM ITEM_MASTER WHERE IM_TYPE='3' AND IM_Status='A'");
            Pq_RM_Spec.DataSource = A;
            Pq_RM_Spec.DisplayMember = "IM_PartName";
            Pq_RM_Spec.ValueMember = "IM_ID";
            Pq_RM_Spec.SelectedIndex = -1;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable A = dbFunctions.getTable("SELECT * FROM BOM_Master WHERE  ");
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string a = comboBox3.Text;
            if (a == "Out-Ward")
            {
                panel3.Visible = true;

                panel1.Visible = false;
                
                button7.Visible = true;
                LoadPo_No();
            }
            else
            {
                panel3.Visible = false;

                panel1.Visible = true;
                PO_vPO_NO.Text = "";
                button7.Visible = false;

            }
        }

        private void PO_vPO_NO_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DialogResult result = MessageBox.Show("Do You Want to Delete, Press YES", "Alcove", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                   // dbFunctions.getTable("update sales_order set so_status='D',so_del_name='" + dbFunctions.username + "',so_del_date=getdate() where so_id='" + dataGridView2.Rows[e.RowIndex].Cells["ID"].Value.ToString() + "'");
                    dbFunctions.getTable("DELETE FROM Current_Stock WHERE CS_iID = '" + dataGridView3.Rows[e.RowIndex].Cells["ID"].Value.ToString() + "'");
                    //fetch_rec();
                    display_Scanned_Details();
                }
            }

        }

        private void TextBox10_TextChanged(object sender, EventArgs e)
        {
            dataGridView4.Visible = true;
            //display1();
            try
            {

                if (string.IsNullOrEmpty(textBox10.Text))
                {
                    (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = string.Format("[IM_PartNo] LIKE '%{0}%' or [IM_PartName] LIKE '%{0}%'", textBox10.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void display1()
        {
            DataTable dtDetails = dbFunctions.getTable(" select IM_PartNo,IM_PartName from item_master where im_type=1 OR im_type=6 and im_status='A' ");
            dataGridView4.DataSource = dtDetails;
        }

        private void TextBox10_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (dataGridView4.Rows.Count >= 1)
                    {
                        dataGridView4.CurrentCell = dataGridView4.Rows[dataGridView4.SelectedRows[0].Index + 1].Cells[1];
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (dataGridView4.Rows.Count >= 1)
                    {
                        dataGridView4.CurrentCell = dataGridView4.Rows[dataGridView4.SelectedRows[0].Index - 1].Cells[1];
                    }
                }
                //  textBox6.Text = "";
                if (e.KeyCode == Keys.Enter)
                {
                    if (wos_MaterialType.Text == "")
                    {
                        // prodid = int.Parse(dataGridView2.SelectedRows[0].Cells["Prod"].Value.ToString());
                        wos_MaterialType.Text = dataGridView4.SelectedRows[0].Cells["IM_PartNo"].Value.ToString();

                        dataGridView4.Visible = false;
                        //pe_qty.Focus();
                    }
                    else if (wos_MaterialType.Text != "")
                    {
                        // prodid = int.Parse(dataGridView2.SelectedRows[0].Cells["Prod"].Value.ToString());
                        wos_MaterialType.Text = dataGridView4.SelectedRows[0].Cells["IM_PartNo"].Value.ToString();

                        dataGridView4.Visible = false;
                        textBox6.Text = "";
                        textBox1.Focus();
                    }
                    else
                    {
                        // pe_medicine.Focus();
                    }
                }
            }
            catch { }
        }

        private void DataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = this.dataGridView4.Rows[e.RowIndex];

                wos_MaterialType.Text = row.Cells["IM_PartName"].Value.ToString();

            }
            textBox6.Text = "";
            dataGridView4.Visible = false;
        }

        private void DataGridView4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //    DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];

                //    Pq_vPart_No.Text = row.Cells["IM_PartNo"].Value.ToString();
                if (dataGridView4.CurrentRow != null) // Ensure a row is selected
                {
                    DataGridViewRow row = dataGridView4.CurrentRow;

                    // Retrieve the value from the "IM_PartNo" column
                    if (row.Cells["IM_PartNo"].Value != null) //(row.Cells["IM_PartNo"].Value != null)
                    {
                        wos_MaterialType.Text = row.Cells["IM_PartName"].Value.ToString();
                    }
                }
            }
            textBox10.Text = "";
            dataGridView4.Visible = false;

        }

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {
            dataGridView5.Visible = true;
            try
            {

                if (string.IsNullOrEmpty(textBox12.Text))
                {
                    (dataGridView5.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView5.DataSource as DataTable).DefaultView.RowFilter = string.Format("[IM_PartNo] LIKE '%{0}%' or [IM_PartName] LIKE '%{0}%'", textBox12.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox12_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (dataGridView5.Rows.Count >= 1)
                    {
                        dataGridView5.CurrentCell = dataGridView5.Rows[dataGridView5.SelectedRows[0].Index + 1].Cells[1];
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (dataGridView5.Rows.Count >= 1)
                    {
                        dataGridView5.CurrentCell = dataGridView5.Rows[dataGridView5.SelectedRows[0].Index - 1].Cells[1];
                    }
                }
                //  textBox6.Text = "";
                if (e.KeyCode == Keys.Enter)
                {
                    if (Pq_RM_Spec.Text == "")
                    {
                        // prodid = int.Parse(dataGridView2.SelectedRows[0].Cells["Prod"].Value.ToString());
                        Pq_RM_Spec.Text = dataGridView5.SelectedRows[0].Cells["IM_PartNo"].Value.ToString();

                        dataGridView5.Visible = false;
                        //pe_qty.Focus();
                    }
                    else if (Pq_RM_Spec.Text != "")
                    {
                        // prodid = int.Parse(dataGridView2.SelectedRows[0].Cells["Prod"].Value.ToString());
                        Pq_RM_Spec.Text = dataGridView5.SelectedRows[0].Cells["IM_PartNo"].Value.ToString();

                        dataGridView5.Visible = false;
                        textBox12.Text = "";
                        textBox1.Focus();
                    }
                    else
                    {
                        // pe_medicine.Focus();
                    }
                }
            }
            catch { }
        }

        private void DataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = this.dataGridView5.Rows[e.RowIndex];

                Pq_RM_Spec.Text = row.Cells["IM_PartName"].Value.ToString();

            }
            textBox6.Text = "";
            dataGridView5.Visible = false;
        }

        private void DataGridView5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //    DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];

                //    Pq_vPart_No.Text = row.Cells["IM_PartNo"].Value.ToString();
                if (dataGridView5.CurrentRow != null) // Ensure a row is selected
                {
                    DataGridViewRow row = dataGridView5.CurrentRow;

                    // Retrieve the value from the "IM_PartNo" column
                    if (row.Cells["IM_PartNo"].Value != null) //(row.Cells["IM_PartNo"].Value != null)
                    {
                        Pq_RM_Spec.Text = row.Cells["IM_PartName"].Value.ToString();
                    }
                }
            }
            textBox12.Text = "";
            dataGridView5.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Clear1();
        }

        private void Clear1()
        {

            wos_SupplierName.Text = "";
            wos_Qty.Text = "";
            LoadPo_No();

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Clear1();
            MessageBox.Show("Details Submit Successfully ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            display();


        }

        private void button11_Click(object sender, EventArgs e)
        {

            Clear();
            wos_SupplierName.Text = "";
            wos_Qty.Text = "";
            wos_Note.Text = "";
            wor_Qty.Text = "";
            wor_Note.Text = "";
            PO_vPO_NO.Text = "";
            display_Receive();
        }
    }
}
