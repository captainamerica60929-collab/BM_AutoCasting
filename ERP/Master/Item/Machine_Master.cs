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

    public partial class Machine_Master : Form
    {
        public string arrow = "Up";
        public int Distance = 220;
        public string ID = "";
        string ErrorMessage = "";
        public bool isMachSupLoad = false;
        public Machine_Master()
        {
            InitializeComponent();
            dbFunctions.DGVStyle(dataGridView1);
        }




        private void ItemMaster_Shown(object sender, EventArgs e)
        {
            display();
            splitContainer1.SplitterDistance = Distance;
        }


        private void ArrowButton_Click(object sender, EventArgs e)
        {
            if (arrow.ToString().Equals("Up"))
            {
                splitContainer1.SplitterDistance = 25;
                arrow = "Down";
                ArrowButton.Image = CRM_App.Properties.Resources.Down;

            }
            else
            {
                splitContainer1.SplitterDistance = Distance;
                arrow = "Up";
                ArrowButton.Image = CRM_App.Properties.Resources.Up;
            }

        }

        private void ItemMaster_Load(object sender, EventArgs e)
        {
            Radio_Active.Checked = true;
            display();
            MM_MachineCode.Focus();
            LoadSupplier();

        }

        public void LoadSupplier()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadSupplier");
                MM_Mach_SupplierName.DataSource = dt;
                MM_Mach_SupplierName.DisplayMember = "SM_Name";
                MM_Mach_SupplierName.ValueMember = "SM_ID";
                MM_Mach_SupplierName.SelectedIndex = -1;
                isMachSupLoad = true;
            }
            catch
            {
            }
        }



        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
            Radio_Active.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(Validate())
            {
                MessageBox.Show(ErrorMessage,"Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            if (btnsave.Text.ToString().Equals("&Update"))
            {
                Update();
            }
            else
            {
                insert();
                MM_MachineCode.Focus();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;

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
                com.CommandText = "pr_Insert_Machine_Master";

                com.Parameters.Add("@MM_MachineCode", SqlDbType.VarChar).Value = MM_MachineCode.Text.ToString();
                com.Parameters.Add("@MM_MachineName", SqlDbType.VarChar).Value = MM_MachineName.Text.ToString();
                com.Parameters.Add("@MM_ModelNo", SqlDbType.VarChar).Value = MM_ModelNo.Text.ToString();
                com.Parameters.Add("@MM_PO_No", SqlDbType.VarChar).Value = MM_PO_No.Text.ToString();
                com.Parameters.Add("@MM_PO_Date", SqlDbType.DateTime).Value = MM_PO_Date.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MM_Mach_SupplierName", SqlDbType.VarChar).Value = MM_Mach_SupplierName.SelectedValue.ToString();
                com.Parameters.Add("@MM_Mach_Address", SqlDbType.VarChar).Value = MM_Mach_Address.Text.ToString();
                com.Parameters.Add("@MM_Mach_Installation", SqlDbType.DateTime).Value = MM_Mach_Installation.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MM_Mach_BasicCost", SqlDbType.VarChar).Value = MM_Mach_BasicCost.Text.ToString();
                com.Parameters.Add("@MM_Mach_ED", SqlDbType.VarChar).Value = MM_Mach_ED.Text.ToString();
                com.Parameters.Add("@MM_Mach_VAT", SqlDbType.VarChar).Value = MM_Mach_VAT.Text.ToString();
                com.Parameters.Add("@MM_Mach_TotalCost", SqlDbType.VarChar).Value = MM_Mach_TotalCost.Text.ToString();


                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                display();
               // Ty_TypeName.Focus();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Update()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Update_Machine_Master";

                com.Parameters.Add("@MM_ID", SqlDbType.Int).Value = ID;
                com.Parameters.Add("@MM_MachineCode", SqlDbType.VarChar).Value = MM_MachineCode.Text.ToString();
                com.Parameters.Add("@MM_MachineName", SqlDbType.VarChar).Value = MM_MachineName.Text.ToString();
                com.Parameters.Add("@MM_ModelNo", SqlDbType.VarChar).Value = MM_ModelNo.Text.ToString();
                com.Parameters.Add("@MM_PO_No", SqlDbType.VarChar).Value = MM_PO_No.Text.ToString();
                com.Parameters.Add("@MM_PO_Date", SqlDbType.DateTime).Value = MM_PO_Date.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MM_Mach_SupplierName", SqlDbType.VarChar).Value = MM_Mach_SupplierName.SelectedValue.ToString();
                com.Parameters.Add("@MM_Mach_Address", SqlDbType.VarChar).Value = MM_Mach_Address.Text.ToString();
                com.Parameters.Add("@MM_Mach_Installation", SqlDbType.DateTime).Value = MM_Mach_Installation.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MM_Mach_BasicCost", SqlDbType.VarChar).Value = MM_Mach_BasicCost.Text.ToString();
                com.Parameters.Add("@MM_Mach_ED", SqlDbType.VarChar).Value = MM_Mach_ED.Text.ToString();
                com.Parameters.Add("@MM_Mach_VAT", SqlDbType.VarChar).Value = MM_Mach_VAT.Text.ToString();
                com.Parameters.Add("@MM_Mach_TotalCost", SqlDbType.VarChar).Value = MM_Mach_TotalCost.Text.ToString();


                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Updated Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                display();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Edit()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("pr_Edit_Machine_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["MM_ID"].ToString();
                MM_MachineCode.Text = dt.Rows[0]["MM_MachineCode"].ToString();
                MM_MachineName.Text = dt.Rows[0]["MM_MachineName"].ToString();
                MM_ModelNo.Text = dt.Rows[0]["MM_ModelNo"].ToString();
                MM_PO_No.Text = dt.Rows[0]["MM_PO_No"].ToString();
                MM_PO_Date.Text = dt.Rows[0]["MM_PO_Date"].ToString();
                MM_Mach_SupplierName.SelectedValue = dt.Rows[0]["MM_Mach_SupplierName"].ToString();
                MM_Mach_Address.Text = dt.Rows[0]["MM_Mach_Address"].ToString();
                MM_Mach_Installation.Text = dt.Rows[0]["MM_Mach_Installation"].ToString();
                MM_Mach_BasicCost.Text = dt.Rows[0]["MM_Mach_BasicCost"].ToString();
                MM_Mach_ED.Text = dt.Rows[0]["MM_Mach_ED"].ToString();
                MM_Mach_VAT.Text = dt.Rows[0]["MM_Mach_VAT"].ToString();
                MM_Mach_TotalCost.Text = dt.Rows[0]["MM_Mach_TotalCost"].ToString();
                btnsave.Text = "&Update";
                MM_MachineCode.Focus();
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Delete()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("pr_Delete_Machine_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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


        public void display()
        {
            DataTable dt = dbFunctions.getTable("pr_Display_Machine_Master");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }


        public void Clear()
        {
            MM_MachineCode.Text = "";
            MM_MachineName.Text = "";
            MM_ModelNo.Text = "";
            MM_PO_No.Text = "";
            MM_Mach_SupplierName.Text = "";
            MM_Mach_Address.Text = "";
            MM_Mach_BasicCost.Text = "";
            MM_Mach_VAT.Text = "";
            MM_Mach_ED.Text = "";
            MM_Mach_TotalCost.Text = "";
            
            btnsave.Text = "&Save   ";
            MM_MachineCode.Focus();
        }


        public bool Validate()
        {

            if ((string.IsNullOrEmpty(MM_Mach_SupplierName.Text.Trim())))
            {
                ErrorMessage = "Supplier Should Not be Empty";
                MM_Mach_SupplierName.Focus();
                return true;
            }
            try
            {
                int x = int.Parse(MM_Mach_SupplierName.SelectedValue.ToString());
            }
            catch
            {
                ErrorMessage = "Select Proper Supplier ";
                MM_Mach_SupplierName.Focus();
                return true;
            }

            if ((string.IsNullOrEmpty(MM_MachineCode.Text.Trim())))
            {
                ErrorMessage = "MachineCode Should Not be Empty";
                MM_MachineCode.Focus();
                return true;
            }
            return false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_ItemType_Master_DeAct");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void Radio_Active_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void Ty_TypeName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                btnsave.Focus();
            }
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Code] LIKE '%{0}%' or [Name] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Ty_TypeName_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Your Item Type";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {

                this.Close();

            }
        }

        private void Item_Type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                Edit();
            }
            if (e.KeyCode == Keys.F4)
            {
                if (Validate())
                {
                    MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Update();
            }
            if (e.KeyCode == Keys.F5)
            {
                display();

            }
            if (e.KeyCode == Keys.F6)
            {
                Delete();
            }
            if (e.KeyCode == Keys.F7)
            {
                Clear();
            }
        }
        private void MM_Mach_BasicCost_TextChanged(object sender, EventArgs e)
        {
            calculation();
        }

        public void calculation()
        {
            try
            {
                if (MM_Mach_BasicCost.Text == "")
                    MM_Mach_BasicCost.Text = "0";

                if (MM_Mach_ED.Text == "")
                    MM_Mach_ED.Text = "0";

                if (MM_Mach_VAT.Text == "")
                    MM_Mach_VAT.Text = "0";


                decimal Basic = decimal.Parse(MM_Mach_BasicCost.Text.ToString());
                decimal VAT = decimal.Parse(MM_Mach_VAT.Text.ToString());
                decimal ED = decimal.Parse(MM_Mach_ED.Text.ToString());


                MM_Mach_TotalCost.Text = (Basic+VAT+ED).ToString("0.00");
            }
            catch { }
        }

        private void MM_Mach_ED_TextChanged(object sender, EventArgs e)
        {
            calculation();
        }

        private void MM_Mach_VAT_TextChanged(object sender, EventArgs e)
        {
            calculation();
        }

        private void MM_Mach_SupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(isMachSupLoad)
            {
                DataTable dt = dbFunctions.getTable("pr_GET_Supler_Address '" + MM_Mach_SupplierName.Text.ToString()+ "'");
                MM_Mach_Address.Text = dt.Rows[0]["SM_PlantAddr"].ToString();
            }


        }

     

       

   }
}