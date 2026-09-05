using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Electronika.Transaction
{
    public partial class Mess_BOM : Form
    {
        public string ID = "";
        public Mess_BOM()
        {
            InitializeComponent();
         
        }

        private void BOM_Master_Shown(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 196; 
        }
        public bool isItemIDload = false;
        public bool isbom_Load = false;
        public void get_BOM()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_BOM_Name_Mess ");
            BM_BOMName.DataSource = dt;
            BM_BOMName.DisplayMember = "IM_PartNo";
            BM_BOMName.ValueMember = "IM_ID";
            BM_BOMName.SelectedIndex = -1;
            isbom_Load = true;
        }


        public void get_BOM_Only()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_ItemBoM ");
            BM_BOMName.DataSource = dt;
            BM_BOMName.DisplayMember = "BM_ItemId";
            BM_BOMName.ValueMember = "BM_Id";
            BM_BOMName.SelectedIndex = -1;
            isbom_Load = true;
        }

        public void get_Item()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_BOM_ItemName ");
            BM_BOM_Item.DataSource = dt;
            BM_BOM_Item.DisplayMember = "IM_PartName";
            BM_BOM_Item.ValueMember = "IM_ID";
            BM_BOM_Item.SelectedIndex = -1;
            isItemIDload = true;
        }


        private void PurchBM_BOM_ItemaseOrderUpload_Load(object sender, EventArgs e)
        {
            get_Item();
            get_BOM();
            LoadUOM();
            BM_UOM.Text = "KG";
        }

        public void LoadUOM()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadUOM");
                BM_UOM.DataSource = dt;
                BM_UOM.DisplayMember = "UM_UOM";
                BM_UOM.ValueMember = "UM_ID";
                BM_UOM.SelectedIndex = -1;
            }
            catch
            {
            }
        }

        private void BM_BOMName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = dbFunctions.getTable("pr_get_Gavity   " + BM_BOMName.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    BM_MoldCavity.Text = dt.Rows[0][0].ToString();
                }
            }
            catch { }
            display();
            BM_MoldCavity.Text = "0";
        }

        
        void display()
        {
            if (isbom_Load)
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_getBOMName_Mess  " + BM_BOMName.SelectedValue.ToString());
                    dataGridView1.DataSource = dt;
                    dbFunctions.DGVStyle(dataGridView1);
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();



                    DataTable dt1 = dbFunctions.getTable("pr_getBOMName_MessAll ");
                    dataGridView2.DataSource = dt1;
                    dbFunctions.DGVStyle(dataGridView2);
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView2.Rows.Count).ToString();
                }
                catch { }
            }
        }
        public string ErrorMessage = "";

        public bool Validate()
        {
            if ((string.IsNullOrEmpty(BM_UOM.Text.Trim())))
            {
                ErrorMessage = "UOM Name Should Not be Empty";
                BM_UOM.Focus();
                return true;
            }
            
            if ((string.IsNullOrEmpty(BM_BOMName.Text.Trim())))
            {
                ErrorMessage = "BOM Name Should Not be Empty";
                BM_BOMName.Focus();
                return true;
            }
            try
            {
                int x = int.Parse(BM_BOMName.SelectedValue.ToString());
            }
            catch {
            ErrorMessage = "Select Proper BOM Name ";
                BM_BOMName.Focus();
                return true;
            }


            if ((string.IsNullOrEmpty(BM_BOM_Item.Text.Trim())))
            {
                ErrorMessage = "BOM Item Should Not be Empty";
                BM_BOM_Item.Focus();
                return true;
            }


            try
            {
                int x = int.Parse(BM_BOM_Item.SelectedValue.ToString());
            }
            catch
            {
                ErrorMessage = "Select Proper BOM Item ";
                BM_BOM_Item.Text = "";
                BM_BOM_Item.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(BM_Qty.Text.Trim())))
            {
                ErrorMessage = "Quantity Should Not be Empty";
                BM_Qty.Focus();
                return true;
            }
           
            return false;
        }

      private void button3_Click(object sender, EventArgs e)
        {

            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (btnsave.Text.ToString().Equals("&Update"))
            {
                Update();
            }
            else
            {
                insert();
                BM_BOMName.Focus();
            }
            //DataTable dt = dbFunctions.getTable("pr_insert_BOM_Master  " + BM_BOMName.SelectedValue.ToString() + "," + BM_BOM_Item.SelectedValue.ToString() + "," + BM_Qty.Text);     
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
              com.CommandText = "pr_insert_BOM_Master";

              com.Parameters.Add("@BM_BomId", SqlDbType.Int).Value = BM_BOMName.SelectedValue.ToString();
              com.Parameters.Add("@BM_ItemId", SqlDbType.Int).Value = BM_BOM_Item.SelectedValue.ToString();
              com.Parameters.Add("@BM_Qty", SqlDbType.Decimal).Value = BM_Qty.Text.ToString();
              com.Parameters.Add("@BM_UOM", SqlDbType.Int).Value = BM_UOM.SelectedValue.ToString();
              com.Parameters.Add("@BM_MoldCavity", SqlDbType.Decimal).Value = BM_MoldCavity.Text.ToString();
              com.Parameters.Add("@BM_Net_Part_Wt", SqlDbType.Decimal).Value = BM_Net_Part_Wt.Text.ToString();
              com.Parameters.Add("@BM_Runner_Wt", SqlDbType.Decimal).Value = BM_Runner_Wt.Text.ToString();
              com.Parameters.Add("@BM_Purging_Loss", SqlDbType.Decimal).Value = BM_Purging_Loss.Text.ToString();
              com.Parameters.Add("@BM_Shot_Wt", SqlDbType.Decimal).Value = BM_Shot_Wt.Text.ToString();
              com.Parameters.Add("@BM_Gross_Part_Wt", SqlDbType.Decimal).Value = BM_Gross_Part_Wt.Text.ToString();
              com.Parameters.Add("@BM_Scrap_Generation", SqlDbType.Decimal).Value = BM_Scrap_Generation.Text.ToString();

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
      public void Update()
      {
          SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
          try
          {
              con.Open();
              SqlCommand com = new SqlCommand();
              com.Connection = con;
              com.CommandType = CommandType.StoredProcedure;
              com.CommandText = "pr_Update_BOM_Master";

              com.Parameters.Add("@BM_Id", SqlDbType.Int).Value = ID;
              com.Parameters.Add("@BM_BomId", SqlDbType.Int).Value = BM_BOMName.SelectedValue.ToString();
              com.Parameters.Add("@BM_ItemId", SqlDbType.Int).Value = BM_BOM_Item.SelectedValue.ToString();
              com.Parameters.Add("@BM_Qty", SqlDbType.Decimal).Value = BM_Qty.Text.ToString();
              com.Parameters.Add("@BM_UOM", SqlDbType.Int).Value = BM_UOM.SelectedValue.ToString();
              com.Parameters.Add("@BM_MoldCavity", SqlDbType.Decimal).Value = BM_MoldCavity.Text.ToString();
              com.Parameters.Add("@BM_Net_Part_Wt", SqlDbType.Decimal).Value = BM_Net_Part_Wt.Text.ToString();
              com.Parameters.Add("@BM_Runner_Wt", SqlDbType.Decimal).Value = BM_Runner_Wt.Text.ToString();
              com.Parameters.Add("@BM_Purging_Loss", SqlDbType.Decimal).Value = BM_Purging_Loss.Text.ToString();
              com.Parameters.Add("@BM_Shot_Wt", SqlDbType.Decimal).Value = BM_Shot_Wt.Text.ToString();
              com.Parameters.Add("@BM_Gross_Part_Wt", SqlDbType.Decimal).Value = BM_Gross_Part_Wt.Text.ToString();
              com.Parameters.Add("@BM_Scrap_Generation", SqlDbType.Decimal).Value = BM_Scrap_Generation.Text.ToString();

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

      private void button5_Click(object sender, EventArgs e)
      {
          if (dataGridView1.SelectedRows.Count > 0)
          {
              DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
              if (result == DialogResult.Yes)
              {
                  DataTable dt = dbFunctions.getTable("Pr_Delete_BOM_Master_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                  MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  display();
                 
              }
          }
          else
          {
              MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
      }

      private void checkBox1_CheckedChanged(object sender, EventArgs e)
      {
          if (checkBox1.Checked == true)
          {
              get_BOM_Only();
          }
          else
          {
              get_BOM();
          }
      }
      private void BOM_Master_KeyDown(object sender, KeyEventArgs e)
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

      public void Edit()
      {
          if (dataGridView1.SelectedRows.Count > 0)
          {
              DataTable dt = dbFunctions.getTable("pr_Edit_BOM_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
              ID = dt.Rows[0]["BM_Id"].ToString();

              BM_BOMName.SelectedValue = dt.Rows[0]["BM_BomId"].ToString();
              BM_BOM_Item.SelectedValue = dt.Rows[0]["BM_ItemId"].ToString();
              BM_Qty.Text = dt.Rows[0]["BM_Qty"].ToString();
              BM_UOM.SelectedValue = dt.Rows[0]["BM_UOM"].ToString();
              BM_MoldCavity.Text = dt.Rows[0]["BM_MoldCavity"].ToString();
              BM_Net_Part_Wt.Text = dt.Rows[0]["BM_Net_Part_Wt"].ToString();
              BM_Runner_Wt.Text = dt.Rows[0]["BM_Runner_Wt"].ToString();
              BM_Purging_Loss.Text = dt.Rows[0]["BM_Purging_Loss"].ToString();
              BM_Shot_Wt.Text = dt.Rows[0]["BM_Shot_Wt"].ToString();
              BM_Gross_Part_Wt.Text = dt.Rows[0]["BM_Gross_Part_Wt"].ToString();
              BM_Scrap_Generation.Text = dt.Rows[0]["BM_Scrap_Generation"].ToString();
             

              btnsave.Text = "&Update";
             
          }
          else
          {
              MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
      }

      public void Clear()
      {
         // BM_BOMName.SelectedValue = -1;
          BM_BOM_Item.SelectedValue = -1;
          BM_Qty.Text = "";
          BM_UOM.SelectedValue = -1;
          BM_MoldCavity.Text = "0";
          BM_Net_Part_Wt.Text = "0";
          BM_Runner_Wt.Text = "0";
          BM_Purging_Loss.Text = "0";
          BM_Shot_Wt.Text = "1500";
          BM_Gross_Part_Wt.Text = "1000";
          BM_Scrap_Generation.Text = "0";
          btnsave.Text = "Add";
      }

      public void Delete()
      {
          
      }
      private void BM_Qty_KeyPress(object sender, KeyPressEventArgs e)
      {
          e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
      }

      private void BM_MoldCavity_KeyPress(object sender, KeyPressEventArgs e)
      {
          e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
      }

      private void BM_Net_Part_Wt_KeyPress(object sender, KeyPressEventArgs e)
      {
          e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
      }

      private void BM_Runner_Wt_KeyPress(object sender, KeyPressEventArgs e)
      {
          e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
      }

      private void BM_Purging_Loss_KeyPress(object sender, KeyPressEventArgs e)
      {
          e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
      }

      private void BM_Shot_Wt_KeyPress(object sender, KeyPressEventArgs e)
      {
          e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
      }

      private void BM_Gross_Part_Wt_KeyPress(object sender, KeyPressEventArgs e)
      {
          e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
      }

      private void BM_Scrap_Generation_KeyPress(object sender, KeyPressEventArgs e)
      {
          e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
      }

      private void button1_Click(object sender, EventArgs e)
      {
          DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
          if (result == DialogResult.Yes)
          {

              this.Close();

          }
      }

      private void button4_Click(object sender, EventArgs e)
      {
          Edit();
      }

      private void button8_Click(object sender, EventArgs e)
      {
          Cursor.Current = Cursors.WaitCursor;
          dbFunctions.ExportExcel(dataGridView2);
          Cursor.Current = Cursors.Default;
      }

      private void button6_Click(object sender, EventArgs e)
      {
          Clear();
      }

      private void button10_Click(object sender, EventArgs e)
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
                  (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[BOM Name] LIKE '%{0}%' or [Material Name] LIKE '%{0}%'", textBoxX1.Text);
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message);
          }
      }

      private void BM_Purging_Loss_TextChanged(object sender, EventArgs e)
      {
          Calculate();
      }

      void Calculate()
      {
          try
          {

             // BM_Purging_Loss.Text = 
              decimal dd = ((Math.Floor(decimal.Parse(BM_Shot_Wt.Text) / decimal.Parse(BM_Net_Part_Wt.Text)) * Math.Floor(decimal.Parse(BM_Gross_Part_Wt.Text) / decimal.Parse(BM_Runner_Wt.Text))) * 0.95m);//((+decimal.Parse(BM_Runner_Wt.Text)) / 100.0);
              BM_Purging_Loss.Text = dd.ToString("0");
              //BM_Shot_Wt.Text = (((decimal.Parse(BM_MoldCavity.Text) * decimal.Parse(BM_Net_Part_Wt.Text)) + decimal.Parse(BM_Runner_Wt.Text)) + decimal.Parse(BM_Purging_Loss.Text)).ToString("0.00");
              //BM_Scrap_Generation.Text=
              //BM_Gross_Part_Wt.Text = (decimal.Parse(BM_Shot_Wt.Text) / decimal.Parse(BM_MoldCavity.Text)).ToString("0.00");

              BM_Qty.Text = (1/ dd).ToString();
              //BM_Qty.Text = BM_Gross_Part_Wt.Text;

              //BM_Scrap_Generation.Text = (decimal.Parse(BM_Gross_Part_Wt.Text) - dd - decimal.Parse(BM_Net_Part_Wt.Text)).ToString("0.00");


          }
          catch { }
      }

      private void BM_MoldCavity_TextChanged(object sender, EventArgs e)
      {
          Calculate();
      }

      private void BM_Net_Part_Wt_TextChanged(object sender, EventArgs e)
      {
          Calculate();
      }

      private void BM_Runner_Wt_TextChanged(object sender, EventArgs e)
      {
          Calculate();
      }

      private void BOM_Master_Load(object sender, EventArgs e)
      {
          get_BOM();
          get_Item();
          LoadUOM();
      }

      private void BM_BOM_Item_SelectedIndexChanged(object sender, EventArgs e)
      {

      }

    }
}
