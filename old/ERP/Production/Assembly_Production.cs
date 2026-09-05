using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;

namespace CRM_App.Production
{
    public partial class Assembly_Production : Form
    {
        public bool isAssyItemload = false;
        public Assembly_Production()
        {
            InitializeComponent();
        }

        private void Material_Return_Load(object sender, EventArgs e)
        {

            LoadAssyPartNo();
            //display();
        }

        public void LoadAssyPartNo()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Load_ProdPlan_AssyPart ");
                AS_Part_No.DataSource = dt;
                AS_Part_No.DisplayMember = "IM_PartNo";
                AS_Part_No.ValueMember = "IM_ID";
                AS_Part_No.SelectedIndex = -1;
                isAssyItemload = true;
            }
            catch
            {
            }
            
        }
        public string GRN_ID = "";
        private void txt_Barcode_KeyDown(object sender, KeyEventArgs e)
        {
        
        }
        public string Barcode = "";
        private void button1_Click(object sender, EventArgs e)
        {
           
            //DataTable dt = dbFunctions.getTable("pr_get_Ass_barcode");
            //if(dt.Rows.Count>0)
            //{
            //    Barcode = dt.Rows[0]["Bardcode"].ToString();
            //}


            //decimal d = 0;
            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    d+=decimal.Parse[dataGridView1.Rows[i].Cells["Using Qty"].Value.ToString()
            //}


            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Insert_Assembly_details";

                com.Parameters.Add("@AS_Date", SqlDbType.VarChar).Value = AS_Date.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@AS_Shift", SqlDbType.VarChar).Value = AS_Shift.Text.ToString();
                com.Parameters.Add("@AS_Part_No", SqlDbType.VarChar).Value = AS_Part_No.Text.ToString();
                com.Parameters.Add("@AS_Part_ID", SqlDbType.Int).Value = AS_Part_No.SelectedValue.ToString();
                com.Parameters.Add("@AS_Part_Name", SqlDbType.VarChar).Value = AS_Part_Name.Text.ToString();
                com.Parameters.Add("@AS_Model", SqlDbType.VarChar).Value = AS_Model.Text.ToString();
                com.Parameters.Add("@AS_Qty", SqlDbType.Int).Value = AS_Qty.Text.ToString();
                com.Parameters.Add("@AS_REJQTY", SqlDbType.Int).Value = rejqty.Text.ToString();
                com.Parameters.Add("@AS_Created_by", SqlDbType.VarChar).Value = dbFunctions.username;
               


                com.ExecuteNonQuery();
                com.Connection.Close();
                //MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SaveDetails();
                Clear();
                display();

            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void SaveDetails()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (this.dataGridView1.Rows[i].Cells["Using Qty"].Value.ToString() != "")
                {
                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "pr_Insert_Assembly_Prod_Details";

                        com.Parameters.Add("@ASD_AS_iid", SqlDbType.Int).Value = AS_Part_No.SelectedValue.ToString();
                        com.Parameters.Add("@ASD_ITEMID", SqlDbType.Int).Value = this.dataGridView1.Rows[i].Cells["id"].Value.ToString();
                        com.Parameters.Add("@ASD_Qty", SqlDbType.Int).Value = this.dataGridView1.Rows[i].Cells["Using Qty"].Value.ToString();
                        var rejQtyValue = this.dataGridView1.Rows[i].Cells["Rej qty"].Value;
                        int rejQty = string.IsNullOrWhiteSpace(rejQtyValue?.ToString()) ? 0 : Convert.ToInt32(rejQtyValue);

                        com.Parameters.Add("@ASD_Rejqty", SqlDbType.Int).Value = rejQty;
                        //com.Parameters.Add("@ASD_Rejqty", SqlDbType.Int).Value = this.dataGridView1.Rows[i].Cells["Rej qty"].Value.ToString();
                        com.Parameters.Add("@ASD_Createdby", SqlDbType.VarChar).Value = Barcode;
                        

                        com.ExecuteNonQuery();
                        com.Connection.Close();



                    }
                    catch (Exception Ex)
                    {
                        dbFunctions.Logs(Ex.Message, dbFunctions.username);
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   
                }
                else
                {
                    MessageBox.Show("Using Qty Should't Emplty ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                }
            MessageBox.Show("Submitted Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
         
            
        }

        public void Clear()
        {
           // AS_Part_No.SelectedIndex = -1;
            AS_Part_Name.Text = "";
            AS_Model.Text = "";
            AS_Qty.Text = "";
            AS_Shift.Text = "";
            rejqty.Text = "";
            AS_Part_No.Focus();
        }

        public void display()
        {
            DataTable dd = dbFunctions.getTable("pr_get_Assembly_Stock '" + AS_Part_No.SelectedValue.ToString() + "','" + AS_Qty.Text.ToString() + "'");
            dataGridView1.DataSource = dd;
            //dataGridView1.Columns["Using Qty"].ReadOnly = false;
            dbFunctions.DGVStyle(dataGridView1);
        }

        void clear()
        {
            AS_Qty.Text = "";
                
        }

        private void Pq_vPart_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            if(isAssyItemload)
            {
                DataTable dt = dbFunctions.getTable("pr_Get_ProdPlan_AssyPartName  '" + AS_Part_No.SelectedValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    AS_Part_Name.Text = dt.Rows[0]["IM_PartNo"].ToString();
                    AS_Model.Text = dt.Rows[0]["IM_Model"].ToString();
                    textBox1.Text = dt.Rows[0]["IM_PackingStandard"].ToString();
                }
                display();
            }
            
            }
            catch
            {
            }
        }

        private void AS_Qty_TextChanged(object sender, EventArgs e)
        {
            display();
            if (dataGridView1.Columns.Contains("Using Qty") && dataGridView1.Columns.Contains("Qty"))
            {
                string inputText = AS_Qty.Text;

                if (decimal.TryParse(inputText, out decimal inputValue))
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        var row = dataGridView1.Rows[i];

                        if (row.Cells["Qty"].Value != null && decimal.TryParse(row.Cells["Qty"].Value.ToString(), out decimal qty))
                        {
                            decimal result = qty * inputValue;
                            row.Cells["Using Qty"].Value = result.ToString("0.##"); // You can use "0.00" if you want 2 decimals always
                        }
                        else
                        {
                            row.Cells["Using Qty"].Value = 0;
                        }
                    }
                }
                else
                {
                  //  MessageBox.Show("Please enter a valid number in AS_Qty.");
                }
            }
            //if (dataGridView1.Columns.Contains("Using Qty"))
            //{
            //    string inputText = AS_Qty.Text;

            //    for (int i = 0; i < dataGridView1.RowCount; i++)
            //    {
            //        var row = dataGridView1.Rows[i];
            //        if (row.Cells["Using Qty"] != null)
            //        {
            //            row.Cells["Using Qty"].Value = inputText;
            //        }
            //    }

            //}

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void rejqty_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Contains("Rej Qty") && dataGridView1.Columns.Contains("Qty"))
            {
                string inputText = rejqty.Text;

                if (decimal.TryParse(inputText, out decimal inputValue))
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        var row = dataGridView1.Rows[i];

                        if (row.Cells["Qty"].Value != null && decimal.TryParse(row.Cells["Qty"].Value.ToString(), out decimal qty))
                        {
                            decimal result = qty * inputValue;
                            row.Cells["rej Qty"].Value = result.ToString("0.##"); // You can use "0.00" if you want 2 decimals always
                        }
                        else
                        {
                            row.Cells["rej Qty"].Value = 0;
                        }
                    }
                }
                else
                {
                    //  MessageBox.Show("Please enter a valid number in AS_Qty.");
                }
            }
        }
    }
}
