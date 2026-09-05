using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;

namespace CRM_App.Transaction
{
    public partial class Purchase_Order : Form
    {
        int rowid = 0;
        string POD_PO_No;
        public bool isSupplierLoad = false;
        public bool isLoadItem = false;
        string ErrorMessage = "";

        public Purchase_Order()
        {
            InitializeComponent();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
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
                save_summary();
            } 
        }



        public void save_summary()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Insert_Purchase_Order";

                com.Parameters.Add("@PO_vPO_NO", SqlDbType.VarChar).Value = PO_vPO_NO.Text.ToString();
                com.Parameters.Add("@PO_dPO_Date", SqlDbType.DateTime).Value = PO_dPO_Date.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@PO_iSupplierID", SqlDbType.VarChar).Value = PO_vSupplier_Name.SelectedValue.ToString();
                com.Parameters.Add("@PO_vSupplier_Name", SqlDbType.VarChar).Value = PO_vSupplier_Name.Text.ToString();
                com.Parameters.Add("@PO_vSupplier_Address", SqlDbType.VarChar).Value = PO_vSupplier_Address.Text.ToString();
                com.Parameters.Add("@PO_vSupplier_Qtn_Ref", SqlDbType.VarChar).Value = PO_vSupplier_Qtn_Ref.Text.ToString();
                com.Parameters.Add("@PO_vSupplier_Qtn_Date", SqlDbType.DateTime).Value = PO_vSupplier_Qtn_Date.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@PO_dSub_Total", SqlDbType.Decimal).Value = PO_dSub_Total.Text.ToString();
                com.Parameters.Add("@PO_dFreight_Charge_Percentage", SqlDbType.Decimal).Value = PO_dFreight_Charge_Percentage.Text.ToString();
                com.Parameters.Add("@PO_dFreight_Charge", SqlDbType.Decimal).Value = PO_dFreight_Charge.Text.ToString();
                com.Parameters.Add("@PO_dInsurence_Charge_Percentage", SqlDbType.Decimal).Value = PO_dInsurence_Charge_Percentage.Text.ToString();
                com.Parameters.Add("@PO_dInsurence_Charge", SqlDbType.Decimal).Value = PO_dInsurence_Charge.Text.ToString();
                com.Parameters.Add("@PO_vGST_Type1", SqlDbType.VarChar).Value = PO_vGST_Type1.Text.ToString();
                com.Parameters.Add("@PO_dExcise_Duty_Percent", SqlDbType.Decimal).Value = PO_dExcise_Duty_Percent.Text.ToString();
                com.Parameters.Add("@PO_dExcise_Duty_Amount", SqlDbType.Decimal).Value = PO_dExcise_Duty_Amount.Text.ToString();
                com.Parameters.Add("@PO_vGST_Type2", SqlDbType.VarChar).Value = PO_vGST_Type2.Text.ToString();
                com.Parameters.Add("@PO_dVAT_CST_Percentage", SqlDbType.Decimal).Value = PO_dVAT_CST_Percentage.Text.ToString();
                com.Parameters.Add("@PO_dVAR_CST_Amount", SqlDbType.Decimal).Value = PO_dVAR_CST_Amount.Text.ToString();
                com.Parameters.Add("@PO_dGrand_Total", SqlDbType.Decimal).Value = PO_dGrand_Total.Text.ToString();
                com.Parameters.Add("@PO_vFreight", SqlDbType.VarChar).Value = PO_vFreight.Text.ToString();
                com.Parameters.Add("@PO_vMode_Of_Despatch", SqlDbType.VarChar).Value = PO_vMode_Of_Despatch.Text.ToString();
                com.Parameters.Add("@PO_vDelivery", SqlDbType.VarChar).Value = PO_vDelivery.Text.ToString();
                com.Parameters.Add("@PO_vPayment_Terms", SqlDbType.VarChar).Value = PO_vPayment_Terms.Text.ToString();
                com.Parameters.Add("@PO_vInspection", SqlDbType.VarChar).Value = PO_vInspection.Text.ToString();
                com.Parameters.Add("@PO_vWarrenty_Class", SqlDbType.VarChar).Value = PO_vWarrenty_Class.Text.ToString();
               

                com.Parameters.Add("@PO_vCreatedby", SqlDbType.VarChar).Value = dbFunctions.username;
                
                com.Parameters.Add("@ID", SqlDbType.Int);
                com.Parameters["@ID"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery(); com.Connection.Close();
                POD_PO_No = com.Parameters["@ID"].Value.ToString();
                //MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                save();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        decimal total = 0m;
        public void LocalGrid()
        {
            dataGridView1.Rows.Add();
            dataGridView1["POD_vItem_ID", rowid].Value = POD_vPart_No.SelectedValue;
            dataGridView1["POD_vPart_Number", rowid].Value = POD_vPart_No.Text;
            dataGridView1["POD_vPartName", rowid].Value = txtGrade.Text;
            dataGridView1["POD_iType", rowid].Value = IM_Type.Text;
            dataGridView1["POD_vSource1", rowid].Value = POD_vSource.Text;
            //dataGridView1["POD_vPartName", rowid].Value = txtGrade.Text;
            dataGridView1["POD_iUOM1", rowid].Value = POD_iUOM.Text;
            dataGridView1["POD_dQty1", rowid].Value = POD_dQty.Text;
            dataGridView1["POD_vPacking_Std1", rowid].Value = POD_vPacking_Std.Text;
            dataGridView1["POD_dUnit_Price1", rowid].Value = POD_dUnit_Price.Text;
            dataGridView1["txtTotalPrice1", rowid].Value = txtTotalPrice.Text;
            dataGridView1["POD_vTotal_Price", rowid].Value = POD_vTotal_Price.Text;
            dataGridView1["Note", rowid].Value = richTextBox1.Text;
            rowid += 1;

            calulate();
            POD_vPart_No.Text = "";
            POD_vSource.Text = "";
            POD_iUOM.Text = "";
            POD_dUnit_Price.Text = "";
            POD_vSpec.Text = "";
            POD_vPacking_Std.Text = "";
            textBox1.Text = "";
            txtGrade.Text = "";
            POD_dQty.Text = "";
            txtTotalPrice.Text = "";
            
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            if (Validate2())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                LocalGrid();
            } 
            
            calulate();
            button1.Enabled = true;
        }

        public bool Validate2()
        {
            if ((string.IsNullOrEmpty(POD_vPart_No.Text.Trim())))
            {
                ErrorMessage = "Name Should Not be Empty";
                POD_vPart_No.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(IM_Type.Text.Trim())))
            {

                ErrorMessage = "Type Should Not be Empty";
                IM_Type.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(POD_dQty.Text.Trim())))
            {
               
                    ErrorMessage = "Qty Should Not be Empty";
                    POD_dQty.Focus();
                    return true;
            }
            return false;
        }


        void calulate()
        {
            try
            {
                decimal Gross_tot = 0m;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    Gross_tot += decimal.Parse(dataGridView1.Rows[i].Cells["txtTotalPrice1"].Value.ToString());

                }
                PO_dSub_Total.Text = Gross_tot.ToString("0.00");

                PO_dFreight_Charge.Text = (((Decimal.Parse(PO_dSub_Total.Text) * (Decimal.Parse((PO_dFreight_Charge_Percentage.Text)) / 100)))).ToString("0.00");
                PO_dInsurence_Charge.Text = (((Decimal.Parse(PO_dSub_Total.Text) * (Decimal.Parse((PO_dInsurence_Charge_Percentage.Text)) / 100)))).ToString("0.00");
                PO_dExcise_Duty_Amount.Text = (((Decimal.Parse(PO_dSub_Total.Text) * (Decimal.Parse((PO_dExcise_Duty_Percent.Text)) / 100)))).ToString("0.00");
                PO_dVAR_CST_Amount.Text = (((Decimal.Parse(PO_dSub_Total.Text) * (Decimal.Parse((PO_dVAT_CST_Percentage.Text)) / 100)))).ToString("0.00");
                POD_vTotal_Price.Text = (Decimal.Parse(POD_vPacking_Std.Text) * Decimal.Parse(POD_dUnit_Price.Text)).ToString("0.00");



                PO_dGrand_Total.Text = (Decimal.Parse(PO_dSub_Total.Text) + ((Decimal.Parse(PO_dFreight_Charge.Text) + ((Decimal.Parse(PO_dInsurence_Charge.Text) + ((Decimal.Parse(PO_dExcise_Duty_Amount.Text) + Decimal.Parse(PO_dVAR_CST_Amount.Text)))))))).ToString("0.00");

            }
            catch (Exception ex)
            {
            }
        }


        void save()
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
                    com.CommandText = "pr_insert_Purchase_Order_Details";

                    com.Parameters.Add("@POD_PO_No", SqlDbType.VarChar).Value = POD_PO_No;
                    com.Parameters.Add("@POD_iType", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["POD_iType"].Value.ToString();
                    com.Parameters.Add("@POD_vItem_ID", SqlDbType.Int).Value = dataGridView1.Rows[i].Cells["POD_vItem_ID"].Value.ToString();
                    com.Parameters.Add("@POD_vPart_Number", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["POD_vPartName"].Value.ToString();
                    com.Parameters.Add("@POD_vSpec", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["POD_vPart_Number"].Value.ToString();
                    com.Parameters.Add("@POD_vSource", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["POD_vSource1"].Value.ToString();
                    com.Parameters.Add("@POD_vPacking_Std", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["POD_vPacking_Std1"].Value.ToString();
                    com.Parameters.Add("@POD_iUOM", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["POD_iUOM1"].Value.ToString();
                    com.Parameters.Add("@POD_dQty", SqlDbType.Decimal).Value = dataGridView1.Rows[i].Cells["POD_dQty1"].Value.ToString();
                    com.Parameters.Add("@POD_dUnit_Price", SqlDbType.Decimal).Value = dataGridView1.Rows[i].Cells["POD_dUnit_Price1"].Value.ToString();
                    com.Parameters.Add("@POD_dTotal_Amount", SqlDbType.Decimal).Value = dataGridView1.Rows[i].Cells["txtTotalPrice1"].Value.ToString();

                    com.Parameters.Add("@POD_Note", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Note"].Value.ToString();
                   

                    com.Parameters.Add("@POD_vCreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;

                    com.ExecuteNonQuery();
                    MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                } 
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Purchase_Order_Load(object sender, EventArgs e)
        {
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      
        
            LoadPo_No();
            LoadSupplier();
            LoadItemType();
            IM_Type.Text = "RM";

            PO_vDelivery.Text = "At our Factory located at Ayyanambakkam on or Before " + PO_dPO_Date.Value.AddDays(15).ToString("dd-MMM-yyyy");
        }


        public void LoadItemType()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadItemType");
                IM_Type.DataSource = dt;
                IM_Type.DisplayMember = "Ty_TypeName";
                IM_Type.ValueMember = "Ty_ID";
                IM_Type.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        private void LoadPo_No()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_GetPo_Number1 '" + PO_NO.Text+ "'");
                PO_vPO_NO.Text=dt.Rows[0][0].ToString();
               
                          
            }
            catch
            {
            }
        }

        public void LoadSupplier()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadSupplier");
                PO_vSupplier_Name.DataSource = dt;
                PO_vSupplier_Name.DisplayMember = "SM_Name";
                PO_vSupplier_Name.ValueMember = "SM_ID";
                PO_vSupplier_Name.SelectedIndex = -1;
                isSupplierLoad = true;
            }
            catch
            {
            }
        }

        public void LoadItem()
        {
            try
            {
                //DataTable dt = dbFunctions.getTable("pr_GetMetireal_PartNo '" + PO_vSupplier_Name.SelectedValue.ToString() + "'");
                DataTable dt = dbFunctions.getTable("pr_GetMetireal_PartNo '" + PO_vSupplier_Name.SelectedValue.ToString() + "','"+ IM_Type.SelectedValue.ToString() + "'");
                POD_vPart_No.DataSource = dt;
                POD_vPart_No.DisplayMember = "IM_PartNo";
                POD_vPart_No.ValueMember = "IM_ID";
                POD_vPart_No.SelectedIndex = -1;
                isLoadItem = true;
            }
            catch
            {
            }
        }

        public bool Validate()
        {

            if ((string.IsNullOrEmpty(PO_vSupplier_Name.Text.Trim())))
            {
                ErrorMessage = "Supplier Name Should Not be Empty";
                PO_vSupplier_Name.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(PO_dFreight_Charge_Percentage.Text.Trim())))
            {
                ErrorMessage = "Freight  Should Not be Empty";
                PO_dFreight_Charge_Percentage.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(PO_dInsurence_Charge_Percentage.Text.Trim())))
            {
                ErrorMessage = "Insurence Charge Should Not be Empty";
                PO_dInsurence_Charge_Percentage.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(PO_vGST_Type1.Text.Trim())))
            {
                ErrorMessage = "GST Type 1 Should Not be Empty";
                PO_vGST_Type1.Focus();
                return true;
            }
            

            return false;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void POD_dQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal n = decimal.Parse(POD_dQty.Text);
                if (n < 1)
                {
                    MessageBox.Show("Enter Min 1 Qty is 0", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    POD_dQty.Text = "";
                    txtTotalPrice.Text = "";
                    POD_dQty.Focus();
                }

                //if (POD_dQty.Text == "")
                //    POD_dQty.Text = "0";

                //if (POD_dUnit_Price.Text == "")
                //    POD_dUnit_Price.Text = "0";

                txtTotalPrice.Text = (float.Parse(POD_dQty.Text.ToString()) * float.Parse(POD_dUnit_Price.Text.ToString())).ToString("0.00");
            }
            catch (Exception ex)
            {
                POD_dQty.Text = "";
                POD_dQty.Focus();
            }
        }

       
        public void Clear()
        {
            PO_vSupplier_Name.SelectedIndex = -1;
            PO_vSupplier_Address.Text = "";
            PO_vPO_NO.Text = "";
            PO_vSupplier_Qtn_Ref.Text = "";
            PO_dPO_Date.Text = "";
            PO_vSupplier_Qtn_Date.Text = "";
            PO_vFreight.Text = "";
            PO_vMode_Of_Despatch.Text = "";
            PO_vDelivery.Text = "";
            PO_vPayment_Terms.Text = "";
            PO_vInspection.Text = "";
            PO_vWarrenty_Class.Text = "";
            PO_dSub_Total.Text = "";
            PO_dFreight_Charge_Percentage.Text = "";
            PO_dFreight_Charge.Text = "";
            PO_dInsurence_Charge_Percentage.Text = "";
            PO_dInsurence_Charge.Text = "";
            PO_dExcise_Duty_Percent.Text = "";
            PO_dExcise_Duty_Amount.Text = "";
            PO_dVAT_CST_Percentage.Text = "";
            PO_dVAR_CST_Amount.Text = "";
            PO_dGrand_Total.Text = "";
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                rowid--;
                calulate();
            } 
     
        }

        private void PO_dFreight_Charge_Percentage_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dInsurence_Charge_Percentage_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dGrand_Total_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void PO_dSub_Total_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dFreight_Charge_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dInsurence_Charge_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dExcise_Duty_Amount_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dVAR_CST_Amount_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_vSupplier_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isSupplierLoad)
            {
                DataTable dt = dbFunctions.getTable("pr_GetSupplier_Details '" + PO_vSupplier_Name.SelectedValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    PO_vSupplier_Address.Text = dt.Rows[0]["SM_PlantAddr"].ToString();
                    PO_dExcise_Duty_Percent.Text = dt.Rows[0]["ED"].ToString();
                    PO_dVAT_CST_Percentage.Text = dt.Rows[0]["TT"].ToString();

                    POD_vPart_No.Text = "";
                    POD_vSource.Text = "";
                    POD_iUOM.Text = "";
                    POD_dUnit_Price.Text = "";
                    POD_vSpec.Text = "";
                    POD_vPacking_Std.Text = "";
                    textBox1.Text = "";
                    txtGrade.Text = "";
                    POD_dQty.Text = "";
                    txtTotalPrice.Text = "";
                    isLoadItem = false;
                    LoadItem();
                }
            }
            
        }

        private void POD_vPart_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadItem)
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Get_PartNo_Details '" + POD_vPart_No.SelectedValue.ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        //POD_vSpec.Text = dt.Rows[0]["IM_PartName"].ToString();
                        POD_vSource.Text = dt.Rows[0]["IM_RMSource"].ToString();
                        txtGrade.Text = dt.Rows[0]["IM_PartName"].ToString();
                        POD_iUOM.Text = dt.Rows[0]["UM_UOM"].ToString();
                        textBox1.Text = dt.Rows[0]["IM_HSNCode"].ToString();
                        POD_vPacking_Std.Text = dt.Rows[0]["IM_PackingStandard"].ToString();
                        POD_dUnit_Price.Text = dt.Rows[0]["IM_Purchase_Price"].ToString();
                        PO_dExcise_Duty_Percent.Text = dt.Rows[0]["IM_GST_Percentage"].ToString();
                        PO_dVAT_CST_Percentage.Text = dt.Rows[0]["IM_GST_Percentage"].ToString();
                    }
                }
                catch { }
                
                
              
            }
        }

        private void POD_dQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void PO_dExcise_Duty_Percent_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void PO_dVAT_CST_Percentage_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void IM_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            POD_dUnit_Price.Enabled = false;
            POD_dUnit_Price.ReadOnly = true;

            POD_vSource.Enabled = false;
            POD_vSource.ReadOnly = true;



            txtGrade.Enabled = false;
            txtGrade.ReadOnly = true;


            if (IM_Type.Text.ToUpper().Equals("RM"))
            {
                //label21.Text = "Spec :";
                //label19.Text = "Grade :";
                label21.Text = "Part No :";
                label19.Text = "Part Name :";

            }

            if (IM_Type.Text.ToUpper().Equals("OTHERS"))
            {

                label21.Text = "Part :";
                label19.Text = "Name :";
                POD_dUnit_Price.Enabled = true;
                POD_dUnit_Price.ReadOnly = false;
                POD_vSource.Enabled = true;
                POD_vSource.ReadOnly = false;



                txtGrade.Enabled = true;
                txtGrade.ReadOnly = false;


                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + PO_vSupplier_Name.SelectedValue.ToString() + "'");
                    POD_vPart_No.DataSource = dt;
                    POD_vPart_No.DisplayMember = "IM_PartNo";
                    POD_vPart_No.ValueMember = "IM_ID";
                    POD_vPart_No.SelectedIndex = -1;
                    isLoadItem = true;
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
            LoadItem();
        }

            //POD_dUnit_Price.Enabled = false;
            //POD_dUnit_Price.ReadOnly = true;

            //POD_vSource.Enabled = false;
            //POD_vSource.ReadOnly = true;



            //txtGrade.Enabled = false;
            //txtGrade.ReadOnly = true;



            //if (IM_Type.Text.ToUpper().Equals("RM"))
            //{
            //    label21.Text = "Spec :";
            //    label19.Text = "Grade :";
            //    POD_dUnit_Price.Enabled = true;
            //    POD_dUnit_Price.ReadOnly = false;
            //    POD_vSource.Enabled = true;
            //    POD_vSource.ReadOnly = false;



            //    txtGrade.Enabled = true;
            //    txtGrade.ReadOnly = false;


            //    try
            //    {
            //        DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + PO_vSupplier_Name.SelectedValue.ToString() + "','"+ IM_Type.SelectedValue.ToString() + "'");
            //        POD_vPart_No.DataSource = dt;
            //        POD_vPart_No.DisplayMember = "IM_PartNo";
            //        POD_vPart_No.ValueMember = "IM_ID";
            //        POD_vPart_No.SelectedIndex = -1;
            //        isLoadItem = true;
            //    }
            //    catch
            //    {
            //    }

            //}
            //if (IM_Type.Text.ToUpper().Equals("B/O"))
            //{
            //    label21.Text = "Part :";
            //    label19.Text = "Name :";
            //    POD_dUnit_Price.Enabled = true;
            //    POD_dUnit_Price.ReadOnly = false;
            //    POD_vSource.Enabled = true;
            //    POD_vSource.ReadOnly = false;



            //    txtGrade.Enabled = true;
            //    txtGrade.ReadOnly = false;


            //    try
            //    {
            //        DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + PO_vSupplier_Name.SelectedValue.ToString() + "','" + IM_Type.SelectedValue.ToString() + "'");
            //        //DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + PO_vSupplier_Name.SelectedValue.ToString() + "'");
            //        POD_vPart_No.DataSource = dt;
            //        POD_vPart_No.DisplayMember = "IM_PartNo";
            //        POD_vPart_No.ValueMember = "IM_ID";
            //        POD_vPart_No.SelectedIndex = -1;
            //        isLoadItem = true;
            //    }
            //    catch
            //    {
            //    }

            //}
            //if (IM_Type.Text.ToUpper().Equals("FG "))
            //{
            //    label21.Text = "Part :";
            //    label19.Text = "Name :";
            //    POD_dUnit_Price.Enabled = true;
            //    POD_dUnit_Price.ReadOnly = false;
            //    POD_vSource.Enabled = true;
            //    POD_vSource.ReadOnly = false;



            //    txtGrade.Enabled = true;
            //    txtGrade.ReadOnly = false;


            //    try
            //    {
            //        DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + PO_vSupplier_Name.SelectedValue.ToString() + "','" + IM_Type.SelectedValue.ToString() + "'");
            //        //DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + PO_vSupplier_Name.SelectedValue.ToString() + "'");
            //        POD_vPart_No.DataSource = dt;
            //        POD_vPart_No.DisplayMember = "IM_PartNo";
            //        POD_vPart_No.ValueMember = "IM_ID";
            //        POD_vPart_No.SelectedIndex = -1;
            //        isLoadItem = true;
            //    }
            //    catch
            //    {
            //    }

            //}
            //if (IM_Type.Text.ToUpper().Equals("SubPart"))
            //{
            //    label21.Text = "Spec :";
            //    label19.Text = "Grade :";
            //    POD_dUnit_Price.Enabled = true;
            //    POD_dUnit_Price.ReadOnly = false;
            //    POD_vSource.Enabled = true;
            //    POD_vSource.ReadOnly = false;



            //    txtGrade.Enabled = true;
            //    txtGrade.ReadOnly = false;


            //    try
            //    {

            //        DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + PO_vSupplier_Name.SelectedValue.ToString() + "','" + IM_Type.SelectedValue.ToString() + "'");
            //       // DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + PO_vSupplier_Name.SelectedValue.ToString() + "'");
            //        POD_vPart_No.DataSource = dt;
            //        POD_vPart_No.DisplayMember = "IM_PartNo";
            //        POD_vPart_No.ValueMember = "IM_ID";
            //        POD_vPart_No.SelectedIndex = -1;
            //        isLoadItem = true;
            //    }
            //    catch
            //    {
            //    }

            //}

            //if (IM_Type.Text.ToUpper().Equals("OTHERS"))
            //{

            //    label21.Text = "Part :";
            //    label19.Text = "Name :";
            //    POD_dUnit_Price.Enabled = true;
            //    POD_dUnit_Price.ReadOnly = false;
            //    POD_vSource.Enabled = true;
            //    POD_vSource.ReadOnly = false;



            //    txtGrade.Enabled = true;
            //    txtGrade.ReadOnly = false;


            //    try
            //    {
            //          DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + PO_vSupplier_Name.SelectedValue.ToString() + "','"+ IM_Type.SelectedValue.ToString() + "'");
            //        //DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + PO_vSupplier_Name.SelectedValue.ToString() + "'");
            //        POD_vPart_No.DataSource = dt;
            //        POD_vPart_No.DisplayMember = "IM_PartNo";
            //        POD_vPart_No.ValueMember = "IM_ID";
            //        POD_vPart_No.SelectedIndex = -1;
            //        isLoadItem = true;
            //    }
            //    catch
            //    {
            //    }



            //}
            //else 
            //{
            // label21.Text = "Part :";
            // label19.Text = "Name :";
            //}

            //try
            //{

            //    DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + PO_vSupplier_Name.SelectedValue.ToString() + "','" + IM_Type.SelectedValue.ToString() + "'");
            //    // DataTable dt = dbFunctions.getTable("pr_Get_Ohter_Metireals '" + PO_vSupplier_Name.SelectedValue.ToString() + "'");
            //    POD_vPart_No.DataSource = dt;
            //    POD_vPart_No.DisplayMember = "IM_PartNo";
            //    POD_vPart_No.ValueMember = "IM_ID";
            //    POD_vPart_No.SelectedIndex = -1;
            //    isLoadItem = true;
            //}
            //catch
            //{
            //}
        

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPo_No();
        }

        private void PO_vPayment_Terms_TextChanged(object sender, EventArgs e)
        {

        }

        private void PO_dPO_Date_ValueChanged(object sender, EventArgs e)
        {
            PO_vDelivery.Text = "At our Factory located at Ayyanambakkam on or Before " + PO_dPO_Date.Value.AddDays(15).ToString("dd-MMM-yyyy");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_GetMetireal_PartNoall '" + PO_vSupplier_Name.SelectedValue.ToString() + "'");
                    POD_vPart_No.DataSource = dt;
                    POD_vPart_No.DisplayMember = "IM_PartNo";
                    POD_vPart_No.ValueMember = "IM_ID";
                    POD_vPart_No.SelectedIndex = -1;
                    isLoadItem = true;
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_GetMetireal_PartNo '" + PO_vSupplier_Name.SelectedValue.ToString() + "'");
                    POD_vPart_No.DataSource = dt;
                    POD_vPart_No.DisplayMember = "IM_PartNo";
                    POD_vPart_No.ValueMember = "IM_ID";
                    POD_vPart_No.SelectedIndex = -1;
                    isLoadItem = true;
                }
                catch
                {
                }
            }
        }

        private void PO_vPO_NO_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label24_Click(object sender, EventArgs e)
        {

        }

        private void Label21_Click(object sender, EventArgs e)
        {

        }

        private void POD_vPacking_Std_TextChanged(object sender, EventArgs e)
        {
            calulate();
        }
    }
}
