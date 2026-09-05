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
    public partial class RM_Master : Form
    {
        public RM_Master()
        {
            InitializeComponent();
        }

        private void FG_Master_Load(object sender, EventArgs e)
        {
            Loadasoe();
            LoadPlant();
            Load_UOM();
            LoadCurrency();
            Load_Mould();
            
            comboBox1.Text = "Active";
        }
        private void Loadasoe()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("select RGV_iID,RGV_vDesciption from ReferenceGroup_Value where RGV_iRG_ID='3'  and RGV_vStatus='A'");
                IM_aoe.DataSource = dt;
                IM_aoe.DisplayMember = "RGV_vDesciption";
                IM_aoe.ValueMember = "RGV_iID";
                IM_aoe.SelectedIndex = -1;
                //isMoldLoadAssy = true;
            }
            catch
            {
            }
        }


        public void display()
        {
            DataTable dt = dbFunctions.getTable("pr_Display_RM_Details '3'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Operation Type"].Value.ToString().Equals("Waiting"))
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Cornsilk;
                }
            }
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }

       

        public void Load_Mould()
        {
            //DataTable dt = dbFunctions.getTable("pr_GetMoldNumber ''");
            //IM_Lead_Time.DataSource = dt;
            //IM_Lead_Time.DisplayMember = "MLD_MouldNo";
            //IM_Lead_Time.ValueMember = "MLD_ID";
            //IM_Lead_Time.SelectedIndex = -1;
        }

        public void LoadCurrency()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadCurrency");
                IM_Currency.DataSource = dt;
                IM_Currency.DisplayMember = "CM_Currency";
                IM_Currency.ValueMember = "CM_iid";
                IM_Currency.SelectedIndex = 0;
            }
            catch
            {
            }
        }


        public void LoadPlant()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadSupplier");
                IM_Supplier.DataSource = dt;
                IM_Supplier.DisplayMember = "SM_Name";
                IM_Supplier.ValueMember = "SM_ID";
                IM_Supplier.SelectedIndex = -1;
            }
            catch
            {
            }

        }



       



        public void Load_UOM()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadUOM_Nos");
                IM_UOM.DataSource = dt;
                IM_UOM.DisplayMember = "UM_UOM";
                IM_UOM.ValueMember = "UM_ID";
                IM_UOM.SelectedIndex = 0;
            }
            catch
            {
            }
        }

        private void IM_MaxStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        public string ErrorMessage = "";



        public bool Validate()
        {
               
                if ((string.IsNullOrEmpty(IM_Supplier.Text.Trim())))
                {
                    ErrorMessage = "Supplier Should Not be Empty";
                    IM_Supplier.Focus();
                    return true;
                }

                if ((string.IsNullOrEmpty(IM_PartNo.Text.Trim())))
                {
                    ErrorMessage = "RM Part No Should Not be Empty";
                    IM_PartNo.Focus();
                    return true;
                }


                if ((string.IsNullOrEmpty(IM_PartName.Text.Trim())))
                {
                    ErrorMessage = "RM Grade Should Not be Empty";
                    IM_PartName.Focus();
                    return true;
                }


              

                if ((string.IsNullOrEmpty(IM_HSNCode.Text.Trim())))
                {
                    ErrorMessage = "HSN Code Should Not be Empty";
                    IM_HSNCode.Focus();
                    return true;
                }


                if ((string.IsNullOrEmpty(IM_RMSource.Text.Trim())))
                {
                    ErrorMessage = "RM Source Should Not be Empty";
                    IM_RMSource.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_UOM.Text.Trim())))
                {
                    ErrorMessage = "UOM Should Not be Empty";
                    IM_UOM.Focus();
                    return true;
                }

                if ((string.IsNullOrEmpty(IM_Sales_Price.Text.Trim())))
                {
                    ErrorMessage = "Purchase Price Should Not be Empty";
                    IM_Sales_Price.Focus();
                    return true;
                }

                if ((string.IsNullOrEmpty(IM_GST_Percentage.Text.Trim())))
                {
                    ErrorMessage = "GST Percentage Should Not be Empty";
                    IM_GST_Percentage.Focus();
                    return true;
                }


                if ((string.IsNullOrEmpty(IM_GST_Percentage.Text.Trim())))
                {
                    ErrorMessage = "GST Percentage Should Not be Empty";
                    IM_GST_Percentage.Focus();
                    return true;
                }

                if ((string.IsNullOrEmpty(IM_Mate_Standard.Text.Trim())))
                {
                    ErrorMessage = "RM GRADE Should Not be Empty";
                     IM_Mate_Standard.Focus();
                    return true;
                }


            if ((string.IsNullOrEmpty(IM_Lead_Time.Text.Trim())))
                {
                    ErrorMessage = "Lead Time Should Not be Empty";
                    IM_Lead_Time.Focus();
                    return true;
                }

               

                if ((string.IsNullOrEmpty(IM_MinStock.Text.Trim())))
                {
                    ErrorMessage = "Min StockShould Not be Empty";
                    IM_MinStock.Focus();
                    return true;
                }

                if ((string.IsNullOrEmpty(IM_MaxStock.Text.Trim())))
                {
                    ErrorMessage = "Max Stock  Should Not be Empty";
                    IM_MaxStock.Focus();
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
                update();
            }
            else
            {
                insert();
                
            }
        }

        public void update()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Update_BO_Master11";

                com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = ID;
                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value ="3";
                com.Parameters.Add("@IM_Supplier", SqlDbType.Int).Value = IM_Supplier.SelectedValue.ToString();
                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo.Text.ToString();
                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName.Text.ToString();
                com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard.Text.ToString();
                com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = IM_RMSource.Text.ToString();
                com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = IM_Dealer.Text.ToString();
                com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = IM_UOM.SelectedValue.ToString();
                com.Parameters.Add("@IM_Sales_Price", SqlDbType.VarChar).Value = IM_Sales_Price.Text.ToString();
                com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = IM_Currency.SelectedValue.ToString();
                com.Parameters.Add("@IM_Lead_Time", SqlDbType.VarChar).Value = IM_Lead_Time.Text.ToString();
                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCode.Text.ToString();
                com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStock.Text.ToString();
                com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStock.Text.ToString();
                com.Parameters.Add("@IM_GST_Percentage", SqlDbType.VarChar).Value = IM_GST_Percentage.Text.ToString();
                com.Parameters.Add("@IM_PackingStandard", SqlDbType.VarChar).Value = IM_PackingStandard.Text;
                com.Parameters.Add("@IM_Color", SqlDbType.VarChar).Value = IM_Color.Text;
                com.Parameters.Add("@IM_aoe", SqlDbType.VarChar).Value = IM_aoe.Text;
                com.Parameters.Add("@IM_Status", SqlDbType.VarChar).Value = (comboBox1.Text == "Active") ? 'A' : 'D';

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

        public void insert()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Insert_Item_Master_BO11";
                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = "3";
                com.Parameters.Add("@IM_Supplier", SqlDbType.Int).Value = IM_Supplier.SelectedValue.ToString();
                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo.Text.ToString();
                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName.Text.ToString();
                com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard.Text.ToString();
                com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = IM_RMSource.Text.ToString();
                com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = IM_Dealer.Text.ToString();
                com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = IM_UOM.SelectedValue.ToString();
                com.Parameters.Add("@IM_Sales_Price", SqlDbType.VarChar).Value = IM_Sales_Price.Text.ToString();
                com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = IM_Currency.SelectedValue.ToString();
                com.Parameters.Add("@IM_Lead_Time", SqlDbType.VarChar).Value = IM_Lead_Time.Text.ToString();
                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCode.Text.ToString();
                com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStock.Text.ToString();
                com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStock.Text.ToString();
                com.Parameters.Add("@IM_GST_Percentage", SqlDbType.VarChar).Value = IM_GST_Percentage.Text.ToString();
                com.Parameters.Add("@IM_Status", SqlDbType.VarChar).Value = (comboBox1.Text == "Active") ? 'A' : 'D';
                com.Parameters.Add("@IM_Color", SqlDbType.VarChar).Value = IM_Color.Text;
                com.Parameters.Add("@IM_aoe", SqlDbType.VarChar).Value = IM_aoe.Text;
                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                display();
                Clear();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }

        public string ID = "";
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("pr_Edit_Item_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["IM_ID"].ToString();
                IM_Supplier.SelectedValue = dt.Rows[0]["IM_Supplier"].ToString();
                IM_PartNo.Text = dt.Rows[0]["IM_PartNo"].ToString();
                IM_PartName.Text = dt.Rows[0]["IM_PartName"].ToString();
                IM_aoe.Text = dt.Rows[0]["IM_aoe"].ToString(); 
                IM_Mate_Standard.Text = dt.Rows[0]["IM_Mate_Standard"].ToString();
                IM_RMSource.Text = dt.Rows[0]["IM_RMSource"].ToString();
                IM_Dealer.Text = dt.Rows[0]["IM_Dealer"].ToString();
                IM_UOM.SelectedValue = dt.Rows[0]["IM_UOM"].ToString();
                IM_Sales_Price.Text = dt.Rows[0]["IM_Purchase_Price"].ToString();
                IM_Currency.SelectedValue = dt.Rows[0]["IM_Currency"].ToString();
                IM_Lead_Time.Text = dt.Rows[0]["IM_Lead_Time"].ToString();
                IM_HSNCode.Text = dt.Rows[0]["IM_HSNCode"].ToString();
                IM_MinStock.Text = dt.Rows[0]["IM_MinStock"].ToString();

                IM_Color.Text = dt.Rows[0]["IM_Color"].ToString();


                IM_MaxStock.Text = dt.Rows[0]["IM_MaxStock"].ToString();
                IM_PackingStandard.Text = dt.Rows[0]["IM_PackingStandard"].ToString();
                IM_GST_Percentage.Text = dt.Rows[0]["IM_GST_Percentage"].ToString();
                string s = dt.Rows[0]["IM_Status"].ToString(); 
                comboBox1.Text = (dt.Rows[0]["IM_Status"].ToString().Trim().Equals("A")) ? "Active" : "Inactive";
                btnsave.Text = "&Update";
                IM_Supplier.Focus();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            //IM_Type.SelectedIndex = -1;
            IM_Supplier.SelectedIndex = -1;
            IM_PartNo.Text = "";
            IM_PartName.Text = "";
            IM_Mate_Standard.Text = "";
            IM_RMSource.Text = "";
            IM_Dealer.Text = "";
            IM_UOM.SelectedIndex = 0;
            IM_Sales_Price.Text = "";
            IM_Currency.Text = "INR";
            IM_Lead_Time.Text = "";
            IM_PackingStandard.Text = "";
            IM_HSNCode.Text = "";
            IM_MinStock.Text = "";
            IM_MaxStock.Text = "";
            IM_GST_Percentage.Text= "";
            comboBox1.Text = "Active";
            btnsave.Text = "&Save";
            IM_Supplier.Focus();

        }

        private void FG_Master_Shown(object sender, EventArgs e)
        {
            display();
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[RM Part No] LIKE '%{0}%' OR [RM Grade] LIKE '%{0}%' OR [Supplier Name] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }

        private void IM_Lead_Time_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled=dbFunctions.Numeric_DecimalOnly(e.KeyChar,(TextBox)sender);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void Label24_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void Delete()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {


                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("pr_Delete_Item_Master_FG " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     display();
                    //Pending();
                    Clear();
                }

            }
        }
    }
}
