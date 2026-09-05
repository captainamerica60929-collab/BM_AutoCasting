using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using CRM_App.Crystal;
using WebApplication;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.OleDb;
using System.Data.SqlClient;


namespace CRM_App.Transaction
{
    public partial class Update_Routecard : Form
    {

        public int Distance = 56;
        public Update_Routecard()
        {
            InitializeComponent();
        }

        private void Purchase_Approval_print_Load(object sender, EventArgs e)
        {
            Display();
            Load_Part_Name();
        }


        private void Load_Part_Name()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_get_FG_Part_Name_All");
                INV_Item_ID.DataSource = dt;
                INV_Item_ID.DisplayMember = "IM_PartNo";
                INV_Item_ID.ValueMember = "IM_ID";
                INV_Item_ID.SelectedIndex = -1;
                
            }
            catch
            {
            }
        }
        public void Display()
        {
          
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void INV_Item_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("pr_get_Qty  '" + INV_Item_ID.SelectedValue+ "'");
            dataGridView1.DataSource = dt;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            openFileDialog1.Filter = "xls File|*.xls";
            openFileDialog1.FilterIndex = 2;
            DialogResult dlgResult = dlg.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                txtFile.Text = dlg.FileName;
            }

            string xslPath = txtFile.Text.Trim();
            string query = "SELECT * from [" + "Exported by Genuine" + "$A01:Z9000]";
            DataTable dt = getFromExcel(xslPath, query);

            // dataGridView1.Columns.Remove("Select");
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
      
        }

        private DataTable getFromExcel(string ExcelPath, string query)
        {
            DataTable dt = new DataTable();

            DataColumn col = dt.Columns.Add("RowNumber", typeof(int));
            col.AutoIncrementSeed = 1;
            col.AutoIncrement = true;

            string cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelPath + ";Extended Properties=Excel 12.0;";
            //string cs = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + ExcelPath + @"; Extended Properties=""Excel 8.0;IMEX=0; HDR=YES""";

            OleDbConnection con = new OleDbConnection(cs);

            OleDbCommand cmd = new OleDbCommand(query, con);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            insert();
        }


        public void insert()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {

                if (decimal.Parse(dataGridView1.Rows[i].Cells["ID"].Value.ToString()) > 0)
                {
                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "pr_Update_Inv_Qty";
                        com.Parameters.Add("@INV_iid", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["ID"].Value.ToString();
                        com.Parameters.Add("@INV_Qty", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Qty"].Value.ToString();
                        com.Parameters.Add("@INV_RouteCard_No", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["RouteCard"].Value.ToString();
                        com.ExecuteNonQuery();
                        com.Connection.Close();
                    }
                    catch (Exception Ex)
                    {
                        // dbFunctions.Logs(Ex.Message, dbFunctions.username);
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {

                 
                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "pr_insert_Inv_Qty";
                        com.Parameters.Add("@INV_Qty", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Qty"].Value.ToString();
                        com.Parameters.Add("@INV_Item", SqlDbType.VarChar).Value = INV_Item_ID.SelectedValue.ToString();
                        com.Parameters.Add("@INV_Rate", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Rate"].Value.ToString();
                        com.Parameters.Add("@INV_RouteCard_No", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["RouteCard"].Value.ToString();
                        com.Parameters.Add("@Inv_No", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["INV No"].Value.ToString();
                        com.ExecuteNonQuery();
                        com.Connection.Close();
                    }
                    catch (Exception Ex)
                    {
                        // dbFunctions.Logs(Ex.Message, dbFunctions.username);
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            MessageBox.Show("Recode Saved Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }


    }
}
