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
    public partial class Deliver_Schedule : Form
    {
        public Deliver_Schedule()
        {
            InitializeComponent();
        }

        private void Deliver_Schedule_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Fetch_PO_No");
                DS_PO_No.DataSource = dt;
                DS_PO_No.DisplayMember = "PO_vPO_NO";
                DS_PO_No.ValueMember = "PO_iID";
                DS_PO_No.SelectedIndex = -1;
            }
            catch
            {
            }

        }

        private void PO_No_SelectedIndexChanged(object sender, EventArgs e)
        {
               DataTable dt = dbFunctions.getTable("pr_Get_Purchase_Order '" + DS_PO_No.SelectedValue.ToString() + "'");
               if (dt.Rows.Count > 0)
               {
                   PO_SupplierName.Text = dt.Rows[0]["PO_vSupplier_Name"].ToString();
                   PO_vSupplier_Address.Text = dt.Rows[0]["PO_vSupplier_Address"].ToString();
               }

               try
               {
                   DataTable dt1 = dbFunctions.getTable("pr_PO_Metireals '" + DS_PO_No.SelectedValue.ToString() + "'");
                   DS_item_ID.DataSource = dt1;
                   DS_item_ID.DisplayMember = "IM_PartNo";
                   DS_item_ID.ValueMember = "IM_ID";
                   DS_item_ID.SelectedIndex = -1;
                   
               }
               catch
               {
               }

               display();
        }
        public void display()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Deliver_Schedule_Details  " + DS_PO_No.SelectedValue);
                dataGridView1.DataSource = dt;
                dbFunctions.DGVStyle(dataGridView1);


                for (int i=0; i < dt.Rows.Count; i++)
                {
                }
            }
            catch { }
        }


        private void PO_vSupplier_Address_TextChanged(object sender, EventArgs e)
        {

        }

        private void POD_vPart_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("pr_Get_PO_Item_Detail '"+DS_PO_No.SelectedValue.ToString()+"','" + DS_item_ID.SelectedValue.ToString() + "'");
            if (dt.Rows.Count > 0)
            {

                DS_Grade.Text = dt.Rows[0]["IM_Grade"].ToString();
                DS_Unit.Text = dt.Rows[0]["UM_UOM"].ToString();
                DS_PO_Qty.Text = dt.Rows[0]["POD_dQty"].ToString();
                POD_vPacking_Std.Text = dt.Rows[0]["IM_PackingStandard"].ToString();
                DS_Price.Text = dt.Rows[0]["IM_Purchase_Price"].ToString();
            }
            get_Number();
            

        }
        public void get_Number()
        {
            try
            {
                DataTable dd = dbFunctions.getTable("pr_Get_Shc_No '" + DS_PO_No.SelectedValue.ToString() + "','" + DS_item_ID.SelectedValue.ToString() + "'");
                DS_Schedule_No.Text = dd.Rows[0][0].ToString();
                DS_Schedule_No.Enabled = false;
            }
            catch { }
        }

        public bool Validate()
        {
            if ((string.IsNullOrEmpty(DS_PO_No.Text.Trim())))
            {
                ErrorMessage = "PO_No Should Not be Empty";
                DS_PO_No.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(DS_Schedule_No.Text.Trim())))
            {
                ErrorMessage = "Schedule_No Should Not be Empty";
                DS_Schedule_No.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(DS_item_ID.Text.Trim())))
            {
                ErrorMessage = "item_ID Should Not be Empty";
                DS_item_ID.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(DS_Qty.Text.Trim())))
            {
                ErrorMessage = "Qty Should Not be Empty";
                DS_Qty.Focus();
                return true;
            }
            return false;
        }

        public string ErrorMessage = "";


        private void button2_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            insert();
            get_Number();
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
                com.CommandText = "Pr_Insert_Deliver_Schedule";
                com.Parameters.Add("@DS_PO_No", SqlDbType.VarChar).Value = DS_PO_No.SelectedValue.ToString();
                com.Parameters.Add("@DS_Schedule_No", SqlDbType.VarChar).Value = DS_Schedule_No.Text.ToString();
                com.Parameters.Add("@DS_item_ID", SqlDbType.VarChar).Value = DS_item_ID.SelectedValue.ToString();
                com.Parameters.Add("@DS_Spec", SqlDbType.VarChar).Value = DS_item_ID.Text.ToString();
                com.Parameters.Add("@DS_Grade", SqlDbType.VarChar).Value = DS_Grade.Text.ToString();
                com.Parameters.Add("@DS_PO_Qty", SqlDbType.VarChar).Value = DS_PO_Qty.Text.ToString();
                com.Parameters.Add("@DS_Qty", SqlDbType.VarChar).Value = DS_Qty.Text.ToString();
                com.Parameters.Add("@DS_Unit", SqlDbType.VarChar).Value = DS_Unit.Text.ToString();
                com.Parameters.Add("@DS_Price", SqlDbType.VarChar).Value = DS_Price.Text.ToString();
                com.Parameters.Add("@DS_Schedule_Date", SqlDbType.VarChar).Value = DS_Schedule_Date.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@DS_Created_By", SqlDbType.VarChar).Value = dbFunctions.username;
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

        public void Clear()
        {
            DS_item_ID.Text = "";
            DS_Grade.Text = "";
            DS_PO_Qty.Text = "";
            DS_Qty.Text = "";
            DS_Unit.Text = "";
            DS_Price.Text = "";
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
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
                    DataTable dt = dbFunctions.getTable("Pr_Delete_Deliver_Schedule " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
}
