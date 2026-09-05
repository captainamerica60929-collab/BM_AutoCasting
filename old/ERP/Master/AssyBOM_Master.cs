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
    public partial class AssyBOM_Master : Form
    {
        public string ID = "";
        public AssyBOM_Master()
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
            DataTable dt = dbFunctions.getTable("pr_Load_FG_PartNameF ");
            ABM_FGPartName.DataSource = dt;
            ABM_FGPartName.DisplayMember = "IM_PartNo";
            ABM_FGPartName.ValueMember = "IM_ID";
            ABM_FGPartName.SelectedIndex = -1;
            isbom_Load = true;
        }


        public void get_BOM_Only()
        {
            DataTable dt = dbFunctions.getTable("pr_Load_Assy_PartName ");
            ABM_FGPartName.DataSource = dt;
            ABM_FGPartName.DisplayMember = "IM_PartNo";
            ABM_FGPartName.ValueMember = "IM_ID";
            ABM_FGPartName.SelectedIndex = -1;
            isbom_Load = true;
        }

        public void get_Item()
        {
            DataTable dt = dbFunctions.getTable("pr_Load_Assy_PartNameF ");
            ABM_AssyPartName.DataSource = dt;
            ABM_AssyPartName.DisplayMember = "IM_PartNo";
            ABM_AssyPartName.ValueMember = "IM_ID";
            ABM_AssyPartName.SelectedIndex = -1;
            isItemIDload = true;
        }


        private void PurchBM_BOM_ItemaseOrderUpload_Load(object sender, EventArgs e)
        {
            get_Item();
            get_BOM();
            LoadUOM();
            ABM_UOM.Text = "KG";
        }

        public void LoadUOM()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadUOM");
                ABM_UOM.DataSource = dt;
                ABM_UOM.DisplayMember = "UM_UOM";
                ABM_UOM.ValueMember = "UM_ID";
                ABM_UOM.SelectedIndex = -1;
            }
            catch
            {
            }
        }

        private void BM_BOMName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = dbFunctions.getTable("pr_get_Gavity   " + ABM_FGPartName.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    ABM_Qty.Text = dt.Rows[0][0].ToString();
                }
            }
            catch { }
            //
            
        }


        void display()
        {
            if (isbom_Load)
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Assy_BOM_Master   " + ABM_AssyPartName.SelectedValue.ToString());
                    dataGridView1.DataSource = dt;
                    dbFunctions.DGVStyle(dataGridView1);
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();


                    DataTable dt1 = dbFunctions.getTable("pr_Display_Assy_BOM_MasterALL  ");
                    dataGridView2.DataSource = dt1;
                    dbFunctions.DGVStyle(dataGridView2);
                }
                catch { }
            }
        }
        public string ErrorMessage = "";

        public bool Validate()
        {
            if ((string.IsNullOrEmpty(ABM_UOM.Text.Trim())))
            {
                ErrorMessage = "UOM Name Should Not be Empty";
                ABM_UOM.Focus();
                return true;
            }
            
            if ((string.IsNullOrEmpty(ABM_FGPartName.Text.Trim())))
            {
                ErrorMessage = "BOM Name Should Not be Empty";
                ABM_FGPartName.Focus();
                return true;
            }
            try
            {
                int x = int.Parse(ABM_FGPartName.SelectedValue.ToString());
            }
            catch {
            ErrorMessage = "Select Proper BOM Name ";
                ABM_FGPartName.Focus();
                return true;
            }


            if ((string.IsNullOrEmpty(ABM_AssyPartName.Text.Trim())))
            {
                ErrorMessage = "BOM Item Should Not be Empty";
                ABM_AssyPartName.Focus();
                return true;
            }


            try
            {
                int x = int.Parse(ABM_AssyPartName.SelectedValue.ToString());
            }
            catch
            {
                ErrorMessage = "Select Proper BOM Item ";
                ABM_AssyPartName.Text = "";
                ABM_AssyPartName.Focus();
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
                ABM_FGPartName.Focus();
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
              com.CommandText = "pr_insert_Assy_BOM_Master";

              com.Parameters.Add("@ABM_FGPartName", SqlDbType.Int).Value = ABM_FGPartName.SelectedValue.ToString();
              com.Parameters.Add("@ABM_AssyPartName", SqlDbType.Int).Value = ABM_AssyPartName.SelectedValue.ToString();
              com.Parameters.Add("@ABM_UOM", SqlDbType.Int).Value = ABM_UOM.SelectedValue.ToString();
              com.Parameters.Add("@ABM_Qty", SqlDbType.Decimal).Value = ABM_Qty.Text.ToString();
              

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
              com.CommandText = "pr_Update_Assy_BOM_Master";

              com.Parameters.Add("@ABM_Id", SqlDbType.Int).Value = ID;
              com.Parameters.Add("@ABM_FGPartName", SqlDbType.Int).Value = ABM_FGPartName.SelectedValue.ToString();
              com.Parameters.Add("@ABM_AssyPartName", SqlDbType.Int).Value = ABM_AssyPartName.SelectedValue.ToString();
              com.Parameters.Add("@ABM_UOM", SqlDbType.Int).Value = ABM_UOM.SelectedValue.ToString();
              com.Parameters.Add("@ABM_Qty", SqlDbType.Decimal).Value = ABM_Qty.Text.ToString();
              

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
                  DataTable dt = dbFunctions.getTable("pr_Delete_Assy_BOM_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
              DataTable dt = dbFunctions.getTable("pr_Edit_Assy_BOM_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
              ID = dt.Rows[0]["ABM_Id"].ToString();

              ABM_FGPartName.SelectedValue = dt.Rows[0]["ABM_FGPartName"].ToString();
              ABM_AssyPartName.SelectedValue = dt.Rows[0]["ABM_AssyPartName"].ToString();
              ABM_UOM.SelectedValue = dt.Rows[0]["ABM_UOM"].ToString();
              ABM_Qty.Text = dt.Rows[0]["ABM_Qty"].ToString();
              
             

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
          ABM_FGPartName.SelectedValue = -1;
          ABM_UOM.SelectedValue = -1;
          ABM_Qty.Text = "0";
        
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
                  (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[FG Part No] LIKE '%{0}%' or [FG Part Name] LIKE '%{0}%'  or [Assembly Part No] LIKE '%{0}%' ", textBoxX1.Text);
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message);
          }
      }
      private void BOM_Master_Load(object sender, EventArgs e)
      {
          get_BOM();
          get_Item();
          LoadUOM();

          
      }

      private void ABM_AssyPartName_SelectedIndexChanged(object sender, EventArgs e)
      {
          display();
      }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
