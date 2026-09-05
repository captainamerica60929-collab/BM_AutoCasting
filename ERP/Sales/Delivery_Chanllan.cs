using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using CRM_App.Crystal;

namespace ERP.Transaction
{
    
   
    public partial class Delivery_Chanllan : Form
    {
        public string arrow = "Up";
        public int Distance = 273;
        public string ID = "";
        string ErrorMessage = "";

        public string Check_Stock = "Yes";
        public bool issupLoad = false;

        public decimal GST = 18.0m;
        public static string Location = System.Configuration.ConfigurationSettings.AppSettings["Location"];
        public static string Filepath = System.Configuration.ConfigurationSettings.AppSettings["Filepath"];
        public Delivery_Chanllan()
        {
            InitializeComponent();
            dbFunctions.DGVStyle(dataGridView1);
            //ArrowButton.Image = ERP.Properties.Resources.Up;
        }
        private void ArrowButton_Click(object sender, EventArgs e)
        {
           

        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
           // display();
            
        }

        public bool isCusomer_Load=false;
        public bool isConsinee_Load = false;
        public bool isItemLoad = false;
        private void Invoice_Load(object sender, EventArgs e)
        {

            Load_Part_Name();
            Load_Customer_Name();
            Load_Consinee_Name();
            Get_Invoice_No();
            display();

            IN_Order_No.Text = "Non-Returnable Gate Pass";
        }

        
        private void Load_Part_Name()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("select * from Item_master where IM_Type=1");
                INV_Item_ID.DataSource = dt;
                INV_Item_ID.DisplayMember = "IM_PartName";
                INV_Item_ID.ValueMember = "IM_ID";
                INV_Item_ID.SelectedIndex = -1;
                isItemLoad = true;
            }
            catch
            {
            }
        }


        public void Get_Invoice_No()
        {
            DataTable dt = dbFunctions.getTable("Pr_DC_No");
            IN_Invoice_No.Text = dt.Rows[0][0].ToString();
        }



        public void Load_Customer_Name()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Load_CM_Name");
                IN_Customer_ID.DataSource = dt;
                IN_Customer_ID.DisplayMember = "CM_Name";
                IN_Customer_ID.ValueMember = "CM_ID";
                IN_Customer_ID.SelectedIndex = -1;
                isCusomer_Load = true;
            }
            catch { }
        }

        public void Load_Consinee_Name()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Load_CM_Name");
                IN_Consinee_ID.DataSource = dt;
                IN_Consinee_ID.DisplayMember = "CM_Name";
                IN_Consinee_ID.ValueMember = "CM_ID";
                IN_Consinee_ID.SelectedIndex = -1;
                isConsinee_Load = true;
            }
            catch { }
        }

        private void IN_Customer_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isCusomer_Load)
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("Pr_Fetch_Customer_Master_ByID  " + IN_Customer_ID.SelectedValue);
                    if (dt.Rows.Count > 0)
                    {
                        IN_Customer_Address.Text = dt.Rows[0]["CM_BillingAddress"].ToString();
                        IN_Customer_State.Text = dt.Rows[0]["CM_TIN_No"].ToString();
                        IN_Customer_State_Code.Text = dt.Rows[0]["CM_ECC_No"].ToString();
                        IN_Customer_GST_No.Text = dt.Rows[0]["CM_GSTProvisonalID"].ToString();
                        IN_Order_No.Text = dt.Rows[0]["Order_No"].ToString();
                        IN_Order_Date.Text = dt.Rows[0]["IN_Order_Date"].ToString();
                        
                        
                    }
                }
                catch { }
            }
        }

        private void IN_Consinee_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isConsinee_Load)
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("Pr_Fetch_Customer_Master_ByID  " + IN_Consinee_ID.SelectedValue);
                    if (dt.Rows.Count > 0)
                    {
                       
                        IN_Consinee_Address.Text = dt.Rows[0]["CM_BillingAddress"].ToString();
                        IN_Consinee_State.Text = dt.Rows[0]["CM_TIN_No"].ToString();
                        IN_Consinee_State_Code.Text = dt.Rows[0]["CM_ECC_No"].ToString();
                        IN_Consinee_GST_No.Text = dt.Rows[0]["CM_GSTProvisonalID"].ToString();
                    }
                }
                catch { }
            }
        }

        private void INV_Item_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isItemLoad)
            {
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Item_Master_ByID  " + INV_Item_ID.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    INV_Rate.Text = dt.Rows[0]["IM_Sales_Price"].ToString();
                    INV_Item_Description.Text = dt.Rows[0]["IM_PartNo"].ToString();
                    txt_HSN_Code.Text = dt.Rows[0]["IM_HSNCode"].ToString();
                    textBox3.Text = dt.Rows[0]["IM_GST_Percentage"].ToString();
                    GST = decimal.Parse(dt.Rows[0]["IM_GST_Percentage"].ToString());
                    INV_Tool_Cost.Text = dt.Rows[0]["IM_Tool_Cost"].ToString();
                    IM_PackingStandard.Text = dt.Rows[0]["IM_PackingStandard"].ToString();
                }
            }
        }

        private void INV_Discount_Percentage_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void INV_Qty_TextChanged(object sender, EventArgs e)
        {
            Rate_Calc(); 
        }

        void Rate_Calc()
        {
            try
            {

                INV_Discount_Amount.Text = ((decimal.Parse(INV_Rate.Text) / 100) * decimal.Parse(INV_Discount_Percentage.Text)).ToString("0.00");
                decimal Unitprice = decimal.Parse(INV_Rate.Text) - decimal.Parse(INV_Discount_Amount.Text);
                INV_Net_Amount.Text = (decimal.Parse(INV_Qty.Text) * Unitprice).ToString();


            }
            catch { }
        }

        private void INV_Discount_Percentage_TextChanged(object sender, EventArgs e)
        {
            Rate_Calc();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            Add();
        }

        public void Add()
        {


          
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_DC_Details";
                com.Parameters.Add("@INV_Invoice_No", SqlDbType.VarChar).Value = IN_Invoice_No.Text.ToString();
                com.Parameters.Add("@INV_Item_ID", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@INV_Item_Name", SqlDbType.VarChar).Value = INV_Item_Description.Text.ToString();
                com.Parameters.Add("@INV_Item_Description", SqlDbType.VarChar).Value = INV_Item_ID.Text.ToString();
                com.Parameters.Add("@INV_Unit", SqlDbType.VarChar).Value = UOM_.Text;
                com.Parameters.Add("@INV_Qty", SqlDbType.VarChar).Value = INV_Qty.Text.ToString();
                com.Parameters.Add("@INV_Rate", SqlDbType.VarChar).Value = "1";
                com.Parameters.Add("@INV_Discount_Percentage", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@INV_Discount_Amount", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@INV_Total_Amount", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@INV_Net_Amount", SqlDbType.VarChar).Value = "0";

                com.Parameters.Add("@INV_Tool_Cost", SqlDbType.VarChar).Value = "0";


                com.Parameters.Add("@INV_SGST_Percentage", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@INV_SGST_Amount", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@INV_CGST_Percentage", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@INV_CGST_Amount", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@INV_IGST_Percentage", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@INV_IGST_Amount", SqlDbType.VarChar).Value = "0";

                com.Parameters.Add("@INV_Created_By", SqlDbType.VarChar).Value = dbFunctions.username;

                com.Parameters.Add("@INV_RouteCard_No", SqlDbType.VarChar).Value = textBox1.Text;
                com.ExecuteNonQuery();
                com.Connection.Close();
                Clear_details();
                display();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Clear_details()
        {

            INV_Item_ID.Text = "";

            INV_Item_Description.Text = "";
            INV_Qty.Text = "";
            INV_Rate.Text = "";
            INV_Discount_Percentage.Text = "0";
            INV_Discount_Amount.Text = "0";
            INV_Item_ID.Focus();
            INV_Net_Amount.Text = "";
            textBox1.Text = "";
        }


        public void display()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_DC_Details_Details1   '" + IN_Invoice_No.Text + "'");
            dataGridView1.DataSource = dt;


            decimal x = 0m;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x += decimal.Parse(dt.Rows[i]["Qty"].ToString());
            }

            IN_Basic_Total.Text = x.ToString();

            dbFunctions.DGVStyle(dataGridView1);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
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
                com.CommandText = "Pr_Insert_DC";
                com.Parameters.Add("@IN_Invoice_No", SqlDbType.VarChar).Value = IN_Invoice_No.Text.ToString();
                com.Parameters.Add("@IN_Invoice_Date", SqlDbType.VarChar).Value = IN_Invoice_Date.Value.ToString("dd-MMM-yyyy hh:mm tt");
                com.Parameters.Add("@IN_Order_No", SqlDbType.VarChar).Value = IN_Order_No.Text.ToString();
                com.Parameters.Add("@IN_Order_Date", SqlDbType.VarChar).Value = IN_Order_Date.Text.ToString();
                com.Parameters.Add("@IN_Customer_ID", SqlDbType.VarChar).Value = IN_Customer_ID.SelectedValue.ToString();
                com.Parameters.Add("@IN_Customer_Name", SqlDbType.VarChar).Value = IN_Customer_ID.Text.ToString();
                com.Parameters.Add("@IN_Customer_Address", SqlDbType.VarChar).Value = IN_Customer_Address.Text.ToString();
                com.Parameters.Add("@IN_Customer_State", SqlDbType.VarChar).Value = IN_Customer_State.Text.ToString();
                com.Parameters.Add("@IN_Customer_State_Code", SqlDbType.VarChar).Value = IN_Customer_State_Code.Text.ToString();
                com.Parameters.Add("@IN_Customer_GST_No", SqlDbType.VarChar).Value = IN_Customer_GST_No.Text.ToString();
                com.Parameters.Add("@IN_Consinee_ID", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@IN_Consinee_Name", SqlDbType.VarChar).Value = IN_Consinee_ID.Text.ToString();
                com.Parameters.Add("@IN_Consinee_Address", SqlDbType.VarChar).Value = IN_Consinee_Address.Text.ToString();
                com.Parameters.Add("@IN_Consinee_State", SqlDbType.VarChar).Value = IN_Consinee_State.Text.ToString();
                com.Parameters.Add("@IN_Consinee_State_Code", SqlDbType.VarChar).Value = IN_Consinee_State_Code.Text.ToString();
                com.Parameters.Add("@IN_Consinee_GST_No", SqlDbType.VarChar).Value = IN_Consinee_GST_No.Text.ToString();
                com.Parameters.Add("@IN_Deliver_Terms", SqlDbType.VarChar).Value = IN_Deliver_Terms.Text.ToString();
                com.Parameters.Add("@IN_Remarks", SqlDbType.VarChar).Value = IN_Remarks.Text.ToString();
                com.Parameters.Add("@IN_Basic_Total", SqlDbType.VarChar).Value = IN_Basic_Total.Text.ToString();
                com.Parameters.Add("@IN_IGST_Percent", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@IN_SGST_Percent", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@IN_CGST_Percent", SqlDbType.VarChar).Value ="0";
                com.Parameters.Add("@IN_IGST_Amount", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@IN_SGST_Amount", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@IN_CGST_Amount", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@IN_GST_Amount", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@IN_Discount_Amount", SqlDbType.VarChar).Value = "0.00";
                com.Parameters.Add("@IN_Net_Amount", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@IN_Createdby", SqlDbType.VarChar).Value = dbFunctions.username;
                com.ExecuteNonQuery();
                com.Connection.Close();

                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    DC_Pdf oRpt = new DC_Pdf();

                    string SQlQuery = "pr_Print_DC '" + IN_Invoice_No.Text + "'";
                    dbFunctions.printpdf("DC", SQlQuery, oRpt);
                    Cursor.Current = Cursors.Default;
                }
                catch { }


                this.Close();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_DC_Details " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    display();
                    
                }
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            textBox2.Text = "0";
            textBox4.Text = "0";
                        textBox5.Text = "0";

        }
        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void Invoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                Check_Stock = "Yes";
                label32.Text = "Barcode :";

            }
            if (e.KeyCode == Keys.F7)
            {
                Check_Stock = "No";
                label32.Text = "Barcode ";

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

         
}
}