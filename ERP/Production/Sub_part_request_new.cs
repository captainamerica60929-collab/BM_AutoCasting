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

    public partial class Sub_part_request_new : Form
    {
        public bool isAssyItemload = false;
        public Sub_part_request_new()
        {
            InitializeComponent();
        }

        private void Material_Return_Load(object sender, EventArgs e)
        {
            LoadRouteCardNo();
            LoadAssyPartNo();
            display();
        }

        public void LoadRouteCardNo()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_GeneratesubRouteCardNo");
                if (dt.Rows.Count > 0)
                {

                    Pq_Route_Card_No.Text = dt.Rows[0][0].ToString();
                }

            }
            catch
            {
            }
        }



        public void LoadAssyPartNo()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Load_ProdPlan_All_details ");
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
            if (button1.Text == "Save")
            {


                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "PR_INSERT_assemble_production_request";

                    com.Parameters.Add("@AS_Rouctcard", SqlDbType.VarChar).Value = Pq_Route_Card_No.Text.ToString();
                    com.Parameters.Add("@AS_Part_No", SqlDbType.VarChar).Value = AS_Part_Name.Text.ToString();
                    com.Parameters.Add("@AS_Part_ID", SqlDbType.Int).Value = AS_Part_No.SelectedValue.ToString();
                    com.Parameters.Add("@AS_Part_Name", SqlDbType.VarChar).Value = AS_Part_No.Text.ToString();
                    com.Parameters.Add("@AS_Qty", SqlDbType.Int).Value = AS_Qty.Text.ToString();
                    com.Parameters.Add("@AS_Created_by", SqlDbType.VarChar).Value = dbFunctions.username;




                    com.ExecuteNonQuery();
                    com.Connection.Close();
                    // DI();
                    MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    display();
                    /* SaveDetails()*/

                    Clear();
                    LoadRouteCardNo();

                }
                catch (Exception Ex)
                {
                    dbFunctions.Logs(Ex.Message, dbFunctions.username);
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                update1();
                button1.Text = "Save";
            }



        }

        private void update1()
        {
            DataTable a = dbFunctions.getTable("update assemble_production_request set AS_Qty='" + AS_Qty.Text + "' where AS_iid='" + upid.ToString() + "'");
            MessageBox.Show("Details Update Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            display();
            /* SaveDetails()*/

            Clear();
            LoadRouteCardNo();
            


        }

        private void DI()
        {
            DataTable A = dbFunctions.getTable("SELECT AS_Barcode AS [Barcode], AS_Qty AS [Using Qty] FROM Assembly_Production WHERE AS_Barcode='" + Barcode + "'");
            dataGridView1.DataSource = A;

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
                        com.CommandText = "pr_Insert_Assembly_Production_Details";

                        com.Parameters.Add("@ASD_AS_iid", SqlDbType.Int).Value = AS_Part_No.SelectedValue.ToString();
                        com.Parameters.Add("@ASD_Barcode", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Barcode"].Value.ToString();
                        com.Parameters.Add("@ASD_Qty", SqlDbType.Int).Value = this.dataGridView1.Rows[i].Cells["Using Qty"].Value.ToString();
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
            AS_Part_No.Text = "";
            AS_Part_Name.Text = "";
            AS_Model.Text = "";
            AS_Qty.Text = "";

            AS_Part_No.Focus();
        }

        public void display()
        {
            DataTable dd = dbFunctions.getTable("pr_get_sub_Stock");

            dataGridView1.DataSource = dd;

            //dataGridView1.Columns["Using Qty"].ReadOnly = false;
            dbFunctions.DGVStyle(dataGridView1);
            //dataGridView1.Columns["Using Qty"].Width = 50;

        }

        void clear()
        {
            AS_Qty.Text = "";

        }

        private void Pq_vPart_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isAssyItemload)
                {
                    DataTable dt = dbFunctions.getTable("pr_Get_ProdPlan_AssyPartName  '" + AS_Part_No.SelectedValue.ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        AS_Part_Name.Text = dt.Rows[0]["IM_PartNo"].ToString();
                        AS_Model.Text = dt.Rows[0]["IM_Model"].ToString();
                        textBox1.Text = dt.Rows[0]["IM_PackingStandard"].ToString();
                        txtStockQty.Text= dt.Rows[0]["Qty"].ToString();
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
            //    display();
            //    if (dataGridView1.Columns.Contains("Using Qty"))
            //    {
            //        string inputText = AS_Qty.Text;

            //        for (int i = 0; i < dataGridView1.RowCount; i++)
            //        {
            //            var row = dataGridView1.Rows[i];
            //            if (row.Cells["Using Qty"] != null)
            //            {
            //                row.Cells["Using Qty"].Value = inputText;
            //            }
            //        }

            //    }
            //dk1();

        }
        private void dk1()
        {
            if (dataGridView1.Columns.Contains("Using Qty") && dataGridView1.Columns.Contains("Qty") && dataGridView1.Columns.Contains("Using Qty1"))
            {

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    var row = dataGridView1.Rows[i];

                    // Ensure the cells are not null
                    //object usingQtyValue = row.Cells["Using Qty"].Value;
                    //object qtyValue = row.Cells["Qty"].Value;
                    int usingQty = Convert.ToInt32(row.Cells["Using Qty"].Value ?? 0);
                    int qty = Convert.ToInt32(row.Cells["Qty"].Value ?? 0);

                    row.Cells["Using Qty1"].Value = usingQty * qty;


                }

            }
            else
            {
                MessageBox.Show("Required columns are missing in DataGridView.");
            }


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

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("update assemble_production_request set  As_Status='D' WHERE AS_iid= '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    display();
                    //Clear();
                }
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        String upid = "0";
        private void button2_Click_1(object sender, EventArgs e)
        {
            button1.Text = "Update";
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                upid = row.Cells["AS_iid"].Value?.ToString(); 
                AS_Qty.Text = row.Cells["AS_Qty"].Value?.ToString();
                Pq_Route_Card_No.Text= row.Cells["AS_Rouctcard"].Value?.ToString();
                AS_Part_No.Text = row.Cells["AS_Part_Name"].Value?.ToString();
                
                //AS_Qty.Text = row.Cells["Qty"].Value?.ToString();
                //upid = row.Cells["AS_iid"].Value?.ToString();



            }
        }
    }
}
