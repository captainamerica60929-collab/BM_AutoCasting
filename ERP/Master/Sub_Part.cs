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
    public partial class Sub_Part : Form
    {
        public Sub_Part()
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
            LoadMachineNo();
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
            DataTable dt = dbFunctions.getTable("pr_Display_FG_Details '6'");
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

        public void LoadMachineNo()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadMachine");
                IM_MachineNo    .DataSource = dt;
                IM_MachineNo.DisplayMember = "MM_MachineName";
                IM_MachineNo.ValueMember = "MM_ID";
                IM_MachineNo.SelectedIndex = -1;
                //isMoldLoadAssy = true;
            }
            catch
            {
            }
        }

        public void Load_Mould()
        {
            DataTable dt = dbFunctions.getTable("pr_GetMoldNumber12");
            IM_MouldNumer.DataSource = dt;
            IM_MouldNumer.DisplayMember = "MLD_MouldNo";
            IM_MouldNumer.ValueMember = "MLD_ID";
            IM_MouldNumer.SelectedIndex = -1;
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
                DataTable dt = dbFunctions.getTable("pr_LoadPlant");
                IM_Plant.DataSource = dt;
                IM_Plant.DisplayMember = "PL_Name";
                IM_Plant.ValueMember = "PL_ID";
                IM_Plant.SelectedIndex = 0; ;
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
               
                if ((string.IsNullOrEmpty(IM_Plant.Text.Trim())))
                {
                    ErrorMessage = "Plant Should Not be Empty";
                    IM_Plant.Focus();
                    return true;
                }

                if ((string.IsNullOrEmpty(IM_PartNo.Text.Trim())))
                {
                    ErrorMessage = "Sub Part No Should Not be Empty";
                    IM_PartNo.Focus();
                    return true;
                }


                if ((string.IsNullOrEmpty(IM_PartName.Text.Trim())))
                {
                    ErrorMessage = "Sub Part Name Should Not be Empty";
                    IM_PartName.Focus();
                    return true;
                }


                if ((string.IsNullOrEmpty(IM_Model.Text.Trim())))
                {
                    ErrorMessage = "Model Should Not be Empty";
                    IM_Model.Focus();
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
                    ErrorMessage = "prod Price Should Not be Empty";
                    IM_Sales_Price.Focus();
                    return true;
                }

            

                if ((string.IsNullOrEmpty(IM_MachineNo.Text.Trim())))
                {
                    ErrorMessage = "Machine Number Should Not be Empty";
                    IM_MachineNo.Focus();
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
                com.CommandText = "pr_Update_FG_Master";

                com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = ID;
                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value ="6";
                com.Parameters.Add("@IM_Plant", SqlDbType.Int).Value = IM_Plant.SelectedValue.ToString();
                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo.Text.ToString();
                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName.Text.ToString();
                com.Parameters.Add("@IM_Model", SqlDbType.VarChar).Value = IM_Model.Text.ToString();
                com.Parameters.Add("@IM_In_House_Or_Out_Source", SqlDbType.VarChar).Value = IM_In_House_Or_Out_Source.Text.ToString();
                com.Parameters.Add("@IM_Usage", SqlDbType.VarChar).Value = IM_Usage.Text.ToString();
                com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = IM_UOM.SelectedValue.ToString();
                com.Parameters.Add("@IM_Sales_Price", SqlDbType.VarChar).Value = IM_Sales_Price.Text.ToString();
                com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = IM_Currency.SelectedValue.ToString();
                com.Parameters.Add("@IM_Mould", SqlDbType.VarChar).Value = IM_MouldNumer.SelectedValue.ToString();
                com.Parameters.Add("@IM_MouldNumer", SqlDbType.VarChar).Value = IM_MouldNumer.Text.ToString();
                com.Parameters.Add("@IM_MachineNo", SqlDbType.Int).Value = IM_MachineNo.SelectedValue.ToString();
                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCode.Text.ToString();
                com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStock.Text.ToString();
                com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStock.Text.ToString();
                com.Parameters.Add("@IM_GST_Percentage", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@IM_PackingStandard", SqlDbType.VarChar).Value = IM_PackingStandard.Text.ToString();
                com.Parameters.Add("@IM_Tool_Cost", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@IM_aoe", SqlDbType.VarChar).Value = IM_aoe.Text.ToString();

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
                com.CommandText = "pr_Insert_Item_Master_FG";
                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = "6";
                com.Parameters.Add("@IM_Plant", SqlDbType.Int).Value = IM_Plant.SelectedValue.ToString();
                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo.Text.ToString();
                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName.Text.ToString();
                com.Parameters.Add("@IM_Model", SqlDbType.VarChar).Value = IM_Model.Text.ToString();
                com.Parameters.Add("@IM_In_House_Or_Out_Source", SqlDbType.VarChar).Value = IM_In_House_Or_Out_Source.Text.ToString();
                com.Parameters.Add("@IM_Usage", SqlDbType.VarChar).Value = IM_Usage.Text.ToString();
                com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = IM_UOM.SelectedValue.ToString();
                com.Parameters.Add("@IM_Sales_Price", SqlDbType.VarChar).Value = IM_Sales_Price.Text.ToString();
                com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = IM_Currency.SelectedValue.ToString();
                com.Parameters.Add("@IM_Mould", SqlDbType.VarChar).Value = IM_MouldNumer.SelectedValue.ToString();
                com.Parameters.Add("@IM_MouldNumer", SqlDbType.VarChar).Value = IM_MouldNumer.Text.ToString();
                com.Parameters.Add("@IM_MachineNo", SqlDbType.Int).Value = IM_MachineNo.SelectedValue.ToString();
                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCode.Text.ToString();
                com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStock.Text.ToString();
                com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStock.Text.ToString();
                com.Parameters.Add("@IM_GST_Percentage", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@IM_PackingStandard", SqlDbType.VarChar).Value = IM_PackingStandard.Text.ToString();
                com.Parameters.Add("@IM_Tool_Cost", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@IM_aoe", SqlDbType.VarChar).Value = IM_aoe.Text.ToString();

                com.Parameters.Add("@IM_Status", SqlDbType.VarChar).Value = (comboBox1.Text == "Active") ? 'A' : 'D';

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

                IM_MachineNo.Text = dataGridView1.SelectedRows[0].Cells["Machine No"].Value.ToString();
                DataTable dt = dbFunctions.getTable("pr_Edit_Item_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["IM_ID"].ToString();
                IM_Plant.SelectedValue = dt.Rows[0]["IM_Plant"].ToString();
                IM_PartNo.Text = dt.Rows[0]["IM_PartNo"].ToString();
                IM_PartName.Text = dt.Rows[0]["IM_PartName"].ToString();
                IM_Model.Text = dt.Rows[0]["IM_Model"].ToString();
                IM_In_House_Or_Out_Source.Text = dt.Rows[0]["IM_In_House_Or_Out_Source"].ToString();
                IM_Usage.Text = dt.Rows[0]["IM_Usage"].ToString();
                IM_UOM.SelectedValue = dt.Rows[0]["IM_UOM"].ToString();
                IM_PackingStandard.Text = dt.Rows[0]["IM_PackingStandard"].ToString();
                IM_Sales_Price.Text = dt.Rows[0]["IM_Sales_Price"].ToString();
                IM_Currency.SelectedValue = dt.Rows[0]["IM_Currency"].ToString();
                //IM_MouldNumer.Text = dt.Rows[0]["IM_Mould"].ToString();
                IM_MouldNumer.Text = dt.Rows[0]["IM_MouldNumer"].ToString();
                //IM_MachineNo.SelectedValue = dt.Rows[0]["IM_MachineNo"].ToString();
                IM_HSNCode.Text = dt.Rows[0]["IM_HSNCode"].ToString();
                IM_MinStock.Text = dt.Rows[0]["IM_MinStock"].ToString();
                IM_Tool_Cost.Text = dt.Rows[0]["IM_Tool_Cost"].ToString();
                IM_MaxStock.Text = dt.Rows[0]["IM_MaxStock"].ToString();
                IM_GST_Percentage.Text = dt.Rows[0]["IM_GST_Percentage"].ToString();
                string s = dt.Rows[0]["IM_Status"].ToString(); 
                comboBox1.Text = (dt.Rows[0]["IM_Status"].ToString().Trim().Equals("A")) ? "Active" : "Inactive";
                btnsave.Text = "&Update";
                IM_Plant.Focus();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            //IM_Type.SelectedIndex = -1;
            IM_Plant.SelectedIndex = 0;
            IM_PartNo.Text = "";
            IM_PartName.Text = "";
            IM_PackingStandard.Text = "";
            IM_Model.Text = "";
            IM_In_House_Or_Out_Source.SelectedIndex = 0;
            IM_Usage.Text = "";
            IM_UOM.SelectedIndex = 0;
            IM_Sales_Price.Text = "";
            IM_Currency.Text = "INR";
            IM_MouldNumer.SelectedIndex = -1;
            IM_MachineNo.SelectedIndex = -1;
            IM_HSNCode.Text = "";
            IM_MinStock.Text = "";
            IM_MaxStock.Text = "";
            IM_GST_Percentage.Text= "";
            comboBox1.Text = "Active";
            btnsave.Text = "&Save";
            IM_Plant.Focus();

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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[FG Part No] LIKE '%{0}%' OR [FG Part Name] LIKE '%{0}%' OR [Model] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
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
