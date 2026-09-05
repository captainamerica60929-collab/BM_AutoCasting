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
    public partial class Sub_part_Issuse : Form
    {
        public string ID = "";
        public string Route_Card_ID = "";
        public Sub_part_Issuse()
        {
            InitializeComponent();
        }

        private void MaterialIssue_Issue_Load(object sender, EventArgs e)
        {
            LoadPendingProdRQ();
            load_store();
        }

        private void load_store()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dbFunctions.getTable("select * from Plant_Master where PL_Status='A'");
                store.DataSource = dt;
                store.DisplayMember = "PL_Name";
                store.ValueMember = "PL_ID";
                store.SelectedIndex = -1;

            }
            catch
            {
            }
        }
        public void LoadPendingProdRQ()
        {
            DataTable dt = dbFunctions.getTable("pr_get_sub_Stock");
            dataGridView2.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView2);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {

                this.Close();

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        string Part_ID = "";
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Edit();
        }
        string Pq_RM_ID = "";
        public void Edit()
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("pr_get_sub_part_Request  '" + dataGridView2.SelectedRows[0].Cells["AS_iid"].Value.ToString() + "'");
                //ID = dt.Rows[0]["Pq_iid"].ToString();
                Route_Card_ID = dataGridView2.SelectedRows[0].Cells["AS_iid"].Value.ToString();
                txtRCNo.Text = dt.Rows[0]["AS_Rouctcard"].ToString();
                txtPartNo.Text = dt.Rows[0]["AS_Part_No"].ToString();
                txtPartName.Text = dt.Rows[0]["AS_Part_Name"].ToString();

                //txtModel.Text = dt.Rows[0]["Pq_vModel"].ToString();
                //txtxplanQty.Text = dt.Rows[0]["Pq_RM_Plan_Qty"].ToString();
                //cmbRMSpec.Text = dt.Rows[0]["Pq_RM_Spec"].ToString();
                ////txtRMGrade.Text = dt.Rows[0]["Pq_RM_Grade"].ToString();
                ////txtReqQty.Text = dt.Rows[0]["pq_RM_Req_Qty"].ToString();
                //Pq_RM_ID = dt.Rows[0]["Pq_RM_ID"].ToString();

                txtxplanQty.Text = dt.Rows[0]["AS_Qty"].ToString();
                txtUOM.Text = "Kg";
                //txtStockQty.Text = dt.Rows[0]["Stock"].ToString();
                display_Scanned_Details();
                Part_ID = dt.Rows[0]["AS_Part_ID"].ToString();

                try
                {
                    DataTable dtx = dbFunctions.getTable("pr_getsubpartAll '" + dt.Rows[0]["AS_Rouctcard"].ToString() + "'");
                    cmbRMSpec.DataSource = dtx;
                    cmbRMSpec.DisplayMember = "IM_PartName";
                    cmbRMSpec.ValueMember = "IM_ID";
                    cmbRMSpec.SelectedIndex = 0;
                    //isRMSpec_Load = true;
                    isRM = true;
                }
                catch
                {
                }
                get_all_ScanQty();
                display_Scanned_Details();




            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void get_all_ScanQty()
        {
            DataTable dt = dbFunctions.getTable("pr_sub_DetailsAll_Qty   '" + txtRCNo.Text + "'");
            dataGridView3.DataSource = dt;
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView3);
            dataGridView3.Columns[2].Width = 50;

            decimal R = 0.0m;
            decimal S = 0.0m;
            decimal B = 0.0m;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                R+=decimal.Parse(dt.Rows[i]["Required Qty"].ToString());
                                
                S+=decimal.Parse(dt.Rows[i]["Scanned"].ToString());
            }

            
            txtRquestQty.Text = R.ToString("0.00");
            txtScanedQty.Text = S.ToString("0.00");
            txtBalanceQty.Text = (R-S).ToString("0.00");
        }

        private void txtRMqty_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SAVE();
            //DataTable dt1 = dbFunctions.getTable("Check_Barcode_Exists  '" + txt_Barcode.Text + "'");

            //if (dt1.Rows.Count > 0)
            //{
            //    DataTable dt = dbFunctions.getTable("pr_get_Barcode_details_By_ID  '" + txt_Barcode.Text + "'");
            //    save_data();
            //}
            //else
            //{
            //    txt_Barcode.Text = "";
            //    txt_Barcode.Focus();
            //    MessageBox.Show("Invalid Barcode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }


        void save_data()
        {

            DataTable dt = dbFunctions.getTable("pr_get_Barcode_details_By_ID  '" + txt_Barcode.Text + "'");
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Current_Stock";
                com.Parameters.Add("@CS_Barcode", SqlDbType.VarChar).Value = txt_Barcode.Text.ToString();
                com.Parameters.Add("@CS_RouteCardNo", SqlDbType.VarChar).Value = Route_Card_ID;
                com.Parameters.Add("@CS_Type", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Type"].ToString();
                com.Parameters.Add("@CS_Item_ID", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Item_ID"].ToString();
                com.Parameters.Add("@CS_Part_No", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Part_No"].ToString();
                com.Parameters.Add("@CS_Part_Name", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Part_Name"].ToString();
                com.Parameters.Add("@CS_Lot_No", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Lot_No"].ToString();
                com.Parameters.Add("@CS_Lot_Date", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Lot_Date"].ToString();
                com.Parameters.Add("@CS_Qty", SqlDbType.VarChar).Value = "-" + Scan_qty.Text.ToString();
                com.Parameters.Add("@CS_UOM", SqlDbType.VarChar).Value = dt.Rows[0]["CS_UOM"].ToString();
                com.Parameters.Add("@CS_Issued_By", SqlDbType.VarChar).Value = dbFunctions.username;
                com.ExecuteNonQuery();
                com.Connection.Close();
                display_Scanned_Details();
                clear();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        decimal scannedQty = 0m;

        void display_Scanned_Details()
        {

            DataTable dt = dbFunctions.getTable("pr_get_sub_Material_Issued_Details  '"+txtRCNo.Text+"' ");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);

            scannedQty = 0m;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                scannedQty += decimal.Parse(dt.Rows[i]["Qty"].ToString());
            }
            txtScanedQty.Text = scannedQty.ToString("0.00");
            get_all_ScanQty();

            if (decimal.Parse(txtRquestQty.Text) <= decimal.Parse(txtScanedQty.Text))
            {
              //  MessageBox.Show("Qty Full ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSubmitt.Enabled = true;
               // return;
            }
            else
            {
                btnSubmitt.Enabled = false;
            }
        }

        private void txtxplanQty_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBoxX1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dtFIFO = dbFunctions.getTable("pr_get_Material_Issued_Details_FIFO '" + Route_Card_ID + "','" + txt_Barcode.Text.ToString() + "'");

                if (dtFIFO.Rows.Count > 0)
                {
                    MessageBox.Show("FIFO Error. Previous Lot Available", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (decimal.Parse(txtRquestQty.Text) <= decimal.Parse(txtScanedQty.Text))
                {
                    MessageBox.Show("Qty Full ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSubmitt.Enabled = true;
                    return;
                }
                else
                {
                    btnSubmitt.Enabled = false;
                }

                DataTable dt = dbFunctions.getTable("pr_get_Barcode_Data  '" + txt_Barcode.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    string IID = cmbRMSpec.SelectedValue.ToString();

                    if (decimal.Parse(dt.Rows[0]["CS_Qty"].ToString()) > 0)
                    {
                        if (dt.Rows[0]["CS_item_iD"].ToString().Equals(IID))
                        {
                            Scan_qty.Text = dt.Rows[0]["CS_Qty"].ToString();

                            button1.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("RM Specification Missmatch", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_Barcode.Text = "";
                            txt_Barcode.Focus();
                            button1.Enabled = false;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Barcode Already Scanned", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_Barcode.Text = "";
                        txt_Barcode.Focus();
                        button1.Enabled = false;
                    }
                    //string IID = cmbRMSpec.SelectedValue.ToString();
                    //string IID = cmbRMSpec.Text;
                    //string IID1 = txtRMGrade.Text;

                    //if (decimal.Parse(dt.Rows[0]["CS_Qty"].ToString()) > 0)
                    //{
                    //    //if (dt.Rows[0]["CS_item_iD"].ToString().Equals(IID))
                    //    //if (dt.Rows[0]["CS_Part_No"].ToString().Equals(IID))
                    //    string c = dt.Rows[0]["CS_Part_No"].ToString();
                    //    if (c== IID)
                    //    {

                    //        Scan_qty.Text = dt.Rows[0]["CS_Qty"].ToString();

                    //        button1.Enabled = true;
                    //    }
                    //    else if(c == IID1)
                    //    {

                    //        Scan_qty.Text = dt.Rows[0]["CS_Qty"].ToString();

                    //        button1.Enabled = true;

                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("RM Specification Missmatch", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        txt_Barcode.Text = "";
                    //        txt_Barcode.Focus();
                    //        button1.Enabled = false;
                    //    }

                    //}
                    //else
                    //{
                    //    MessageBox.Show("Barcode Already Scanned", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txt_Barcode.Text = "";
                    //    txt_Barcode.Focus();
                    //    button1.Enabled = false;
                    //}
                }
                else
                {
                    txt_Barcode.Text = "";
                    txt_Barcode.Focus();
                    MessageBox.Show("Invalid Barcode", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = false;
                }

                Scan_qty.Focus();
            }
            //if (e.KeyCode == Keys.Enter)
            //{
            //    DataTable dtFIFO = dbFunctions.getTable("pr_get_Material_Issued_Details_FIFO '" + Route_Card_ID + "','" + txt_Barcode.Text.ToString() + "'");
            //    //if (dtFIFO.Rows.Count > 0)
            //    //{
            //    //    MessageBox.Show("FIFO Error. Previous Lot Available", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //    return;
            //    //}

            //    if (decimal.Parse(txtRquestQty.Text) <= decimal.Parse(txtScanedQty.Text))
            //    {
            //        MessageBox.Show("Qty Full ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        btnSubmitt.Enabled = true;
            //        return;
            //    }
            //    else
            //    {
            //        btnSubmitt.Enabled = false;
            //    }

            //    DataTable dt = dbFunctions.getTable("pr_get_Barcode_Data  '" + txt_Barcode.Text + "'");
            //    if (dt.Rows.Count > 0)
            //    {
            //        string IID = cmbRMSpec.SelectedValue.ToString();

            //        if (decimal.Parse(dt.Rows[0]["CS_Qty"].ToString()) > 0)
            //        {
            //            if (dt.Rows[0]["CS_item_iD"].ToString().Equals(IID))
            //            {
            //                Scan_qty.Text = dt.Rows[0]["CS_Qty"].ToString();

            //                button1.Enabled = true;
            //            }
            //            else
            //            {
            //                MessageBox.Show("RM Specification Missmatch", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                txt_Barcode.Text = "";
            //                txt_Barcode.Focus();
            //                button1.Enabled = false;
            //            }

            //        }
            //        else
            //        {
            //            MessageBox.Show("Barcode Already Scanned", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            txt_Barcode.Text = "";
            //            txt_Barcode.Focus();
            //            button1.Enabled = false;
            //        }
            //    }
            //    else
            //    {
            //        txt_Barcode.Text = "";
            //        txt_Barcode.Focus();
            //        MessageBox.Show("Invalid Barcode", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        button1.Enabled = false;
            //    }

            //    Scan_qty.Focus();
            //}
        }

        private void txtReqQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void clear()
        {
            txt_Barcode.Text = "";
        }

        private void txtRquestQty_TextChanged(object sender, EventArgs e)
        {
            calculation();
           
        }

        public void calculation()
        {
            try
            {
                if (txtRquestQty.Text == "")
                    txtRquestQty.Text = "0";

                if (txtScanedQty.Text == "")
                    txtScanedQty.Text = "0";

                txtBalanceQty.Text = (float.Parse(txtRquestQty.Text.ToString()) - float.Parse(txtScanedQty.Text.ToString())).ToString();
            }
            catch
            {
            }

        }

        private void txtScanedQty_TextChanged(object sender, EventArgs e)
        {
            calculation();
        }

        private void btnSubmitt_Click(object sender, EventArgs e)
        {

            //DataTable dtFIFO = dbFunctions.getTable("pr_get_Material_Issued_Details_FIFO '"+ Route_Card_ID + "','"+ txt_Barcode.Text.ToString() + "'");
            //if(dtFIFO.Rows.Count > 0)
            //{
            //    MessageBox.Show("FIFO Error. Previous Lot Available", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //else
            //{ 
            DataTable dt = dbFunctions.getTable("pr_update_sub_Issued_Details  '"+ txtRCNo.Text+ "'");
            MessageBox.Show("Subpart Issued Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            //}
        }

        private void Scan_qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //DataTable dtFIFO = dbFunctions.getTable("pr_get_Material_Issued_Details_FIFO '" + Route_Card_ID + "','" + txt_Barcode.Text.ToString() + "'");
                //if (dtFIFO.Rows.Count > 0)
                //{
                //    MessageBox.Show("FIFO Error. Previous Lot Available", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //save_data();
                SAVE();
            }
        }

        private void SAVE()
        {
            decimal reqQty = Convert.ToDecimal(txtReqQty.Text);
            decimal scanQty = Convert.ToDecimal(Scan_qty.Text);

            if (reqQty == scanQty)
            {

                DataTable dt = dbFunctions.getTable("pr_STOCK_SUB_details  '" + txtRCNo.Text + "','" + cmbRMSpec.Text + "'");
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_withbarcode_Current_Stock";
                    com.Parameters.Add("@CS_Barcode", SqlDbType.VarChar).Value = dt.Rows[0]["AS_Rouctcard"].ToString();
                    com.Parameters.Add("@CS_RouteCardNo", SqlDbType.VarChar).Value = '0';
                    com.Parameters.Add("@CS_Type", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Type"].ToString();
                    com.Parameters.Add("@CS_Item_ID", SqlDbType.VarChar).Value = dt.Rows[0]["IM_ID"].ToString();
                    com.Parameters.Add("@CS_Part_No", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PartNo"].ToString();
                    com.Parameters.Add("@CS_Part_Name", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PartName"].ToString();
                    //com.Parameters.Add("@CS_Lot_No", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Lot_No"].ToString();
                    //com.Parameters.Add("@CS_Lot_Date", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Lot_Date"].ToString();
                    com.Parameters.Add("@CS_Qty", SqlDbType.VarChar).Value = "-" + Scan_qty.Text.ToString();
                    com.Parameters.Add("@CS_UOM", SqlDbType.VarChar).Value = dt.Rows[0]["IM_UOM"].ToString();
                    com.Parameters.Add("@CS_storeid", SqlDbType.Int).Value = store.SelectedValue.ToString();
                    //010Parameters.Add("@wos_store", SqlDbType.VarChar).Value = store.SelectedValue.ToString();
                    com.Parameters.Add("@CS_Issued_By", SqlDbType.VarChar).Value = dbFunctions.username;
                    com.ExecuteNonQuery();
                    com.Connection.Close();
                    display_Scanned_Details();
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
                catch (Exception Ex)
                {
                    dbFunctions.Logs(Ex.Message, dbFunctions.username);
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                MessageBox.Show("ReqQty AND Scan_qty IS NOT SAME", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Scan_qty.Text = "";
                Scan_qty.Focus();


            }
        }

        public bool isRM = false;
        private void cmbRMSpec_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_sub_part_PartNameDetails '" + txtRCNo.Text + "','" + cmbRMSpec.SelectedValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    txtReqQty.Text = dt.Rows[0]["QTY"].ToString();
                    txtRMGrade.Text = dt.Rows[0]["im_partno"].ToString();

                }
            }


            catch { }


            //try
            //{
            //    DataTable dt = dbFunctions.getTable("pr_Get_RMSpec_PartNameDetails '" + Part_ID + "','" + cmbRMSpec.SelectedValue.ToString() + "'");
            //    if (dt.Rows.Count > 0)
            //    {
            //        txtRMGrade.Text = dt.Rows[0]["IM_PartName"].ToString();
            //        if (dt.Rows[0]["UM_UOM"].ToString().ToUpper().Equals("KG"))
            //        {
            //            txtReqQty.Text = ((decimal.Parse(dt.Rows[0]["BM_Qty"].ToString()) * decimal.Parse(txtxplanQty.Text)) / 1000).ToString("0.00");

            //        }
            //        else
            //        {
            //            txtReqQty.Text = (decimal.Parse(dt.Rows[0]["BM_Qty"].ToString()) * decimal.Parse(txtxplanQty.Text)).ToString("0.00");

            //        }
            //        txtUOM.Text = dt.Rows[0]["UM_UOM"].ToString();
            //        txtStockQty.Text = dt.Rows[0]["Qty"].ToString();
            //        BM_Type.Text = dt.Rows[0]["BM_Type"].ToString();


            //    }
            //}
            //catch { }

            //display_Scanned_Details();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("pr_delete_Issued_material " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    display_Scanned_Details();
                }
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Txt_Barcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtRCNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void CmbRMSpec_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable A = dbFunctions.getTable("SELECT CS_Barcode FROM CURRENT_STOCK WHERE CS_Item_ID='" + cmbRMSpec.SelectedValue.ToString() + "' and CS_Barcode NOT IN ( SELECT CS_Barcode   FROM Current_Stock    GROUP BY CS_Barcode    HAVING COUNT(*) > 1  )  ");
            foreach (DataRow row in A.Rows)
            {
                comboBox1.Items.Add(row["CS_Barcode"].ToString());
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
