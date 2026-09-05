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
    public partial class MaterialIssue_Issue : Form
    {
        public string ID = "";
        public string Route_Card_ID = "";
        public MaterialIssue_Issue()
        {
            InitializeComponent(); 
        }

        private void MaterialIssue_Issue_Load(object sender, EventArgs e)
        {
            LoadPendingProdRQ();
            loadpalt();
        }

        private void loadpalt()
        {
            DataTable a = dbFunctions.getTable("select * from plant_master where pl_status='A'");
            plant.DataSource = a;
            plant.DisplayMember = "PL_Name";
            plant.ValueMember = "PL_ID";
            plant.SelectedIndex = -1;
        }
        public void LoadPendingProdRQ()
        {
            DataTable dt = dbFunctions.getTable("pr_Display_Production_Request_Details");
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
                DataTable dt = dbFunctions.getTable("pr_get_Production_Request  '" + dataGridView2.SelectedRows[0].Cells["ID"].Value.ToString() + "'");
                //ID = dt.Rows[0]["Pq_iid"].ToString();
                Route_Card_ID = dataGridView2.SelectedRows[0].Cells["ID"].Value.ToString();
                txtRCNo.Text = dt.Rows[0]["Pq_Route_Card_No"].ToString();
                txtPartNo.Text = dt.Rows[0]["Pq_vPart_No"].ToString();
                txtPartName.Text = dt.Rows[0]["Pq_vPart_Name"].ToString();
                txtModel.Text = dt.Rows[0]["Pq_vModel"].ToString();
                txtxplanQty.Text = dt.Rows[0]["Pq_RM_Plan_Qty"].ToString();
                cmbRMSpec.Text = dt.Rows[0]["Pq_RM_Spec"].ToString();
                //txtRMGrade.Text = dt.Rows[0]["Pq_RM_Grade"].ToString();
                //txtReqQty.Text = dt.Rows[0]["pq_RM_Req_Qty"].ToString();
                Pq_RM_ID = dt.Rows[0]["Pq_RM_ID"].ToString();
                txtRquestQty.Text = dt.Rows[0]["pq_RM_Req_Qty"].ToString();
                txtUOM.Text = "Kg";
               // Part_ID = dt.Rows[0]["Pq_iPart_ID"].ToString();
                //txtStockQty.Text = dt.Rows[0]["Stock"].ToString();
                display_Scanned_Details();
                Part_ID = dt.Rows[0]["Pq_iPart_ID"].ToString();

                try
                {
                    DataTable dtx = dbFunctions.getTable("pr_getBOMDetailsAll '" + dt.Rows[0]["Pq_iPart_ID"].ToString() + "'");
                    cmbRMSpec.DataSource = dtx;
                    cmbRMSpec.DisplayMember = "IM_PartNo";
                    cmbRMSpec.ValueMember = "IM_ID";
                    cmbRMSpec.SelectedIndex = 0;
                    //isRMSpec_Load = true;
                    isRM = true;
                }
                catch
                {
                }
                get_all_ScanQty();
                
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void get_all_ScanQty()
        {
            DataTable dt = dbFunctions.getTable("pr_getBOMDetailsAll_Qty   '" + Part_ID + "','" + txtxplanQty.Text+"','"+Route_Card_ID+"'");
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

            DataTable dt1 = dbFunctions.getTable("Check_Barcode_Exists  '" + txt_Barcode.Text + "'");

            if (dt1.Rows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("pr_get_Barcode_details_By_ID  '" + txt_Barcode.Text + "'");
                save_data();
            }
            else
            {
                txt_Barcode.Text = "";
                txt_Barcode.Focus();
                MessageBox.Show("Invalid Barcode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void save_data()
        {

            DataTable dt = dbFunctions.getTable("pr_get_Barcode_details_By_ID  '" + txt_Barcode.Text + "' ");
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
                Scan_qty.Text = "";
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

            DataTable dt = dbFunctions.getTable("pr_get_Material_Issued_Details  "+Route_Card_ID);
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
            //else if (decimal.Parse(txtRquestQty.Text) >= decimal.Parse(txtScanedQty.Text)) {
            //    btnSubmitt.Enabled = true;
            //}

            else
            {
                btnSubmitt.Enabled = false;
            }
        }

        private void txtxplanQty_TextChanged(object sender, EventArgs e)
        {
           
        }
        DataTable dtFIFO = new DataTable();
        private void textBoxX1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if (checkBox1.Checked)
                //{
                //     dtFIFO = dbFunctions.getTable("pr_get_Material_Issued_Details_FIFO11 '" + Route_Card_ID + "','" + txt_Barcode.Text.ToString() + "'");
                //}
                //else {
                //     dtFIFO = dbFunctions.getTable("pr_get_Material_Issued_Details_FIFO '" + Route_Card_ID + "','" + txt_Barcode.Text.ToString() + "'");
                //}
                //DataTable a = dbFunctions.getTable(@"SELECT *
                //    FROM current_stock
                //    WHERE CS_Barcode ='" + txt_Barcode.Text.ToString() + @"'
                //      AND(
                //          SELECT COUNT(*)
                //          FROM current_stock
                //          WHERE CS_Barcode = '" + txt_Barcode.Text.ToString() + @"'
                //      ) > 1");
                //if (a.Rows.Count != 0)
                //{
                //    MessageBox.Show("Barcode Already Scanned", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    txt_Barcode.Text = "";
                //    return;

                //}
                //DataTable a = dbFunctions.getTable(@"SELECT SUM(CS_Qty) AS QTY
                //                     FROM current_stock
                //                     WHERE CS_Barcode = '" + txt_Barcode.Text + "'");

                //// Always check if the result has rows before accessing them
                //if (a.Rows.Count > 0)
                //{
                //    // Try parsing the value safely
                //    decimal qty = 0;
                //    decimal.TryParse(a.Rows[0]["QTY"].ToString(), out qty);

                //    if (qty == 0)
                //    {
                //        MessageBox.Show("Barcode Already Scanned", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        txt_Barcode.Text = "";
                //        return;
                //    }
                //}

                DataTable a = dbFunctions.getTable(@"
                                    SELECT 
                                        SUM(CS_Qty) AS QTY,
                                        MAX(CS_Lot_Date) AS LastDate
                                    FROM current_stock
                                    WHERE CS_Barcode = '" + txt_Barcode.Text + "'");

                if (a.Rows.Count > 0)
                {
                    // Safely parse QTY
                    decimal qty = 0;
                    decimal.TryParse(a.Rows[0]["QTY"].ToString(), out qty);

                    // Safely parse date
                    DateTime lastDate;
                    DateTime.TryParse(a.Rows[0]["LastDate"].ToString(), out lastDate);

                    // Check quantity first
                    if (qty == 0)
                    {
                        MessageBox.Show("Barcode Already Scanned", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_Barcode.Text = "";
                        return;
                    }

                    // Then check if older than 1 year
                    if (!checkBox1.Checked &&  lastDate < DateTime.Now.AddYears(-1))
                    {
                        MessageBox.Show("Expired material", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_Barcode.Text = "";
                        return;
                    }
                }


                dtFIFO = dbFunctions.getTable("pr_get_Material_Issued_Details_FIFO '" + txt_Barcode.Text.ToString() + "'");
                if(checkBox1.Checked == false)
                {
                    if (dtFIFO.Rows.Count > 0)   
                    {
                        MessageBox.Show("FIFO ERROR", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_Barcode.Text="";
                        return;

                    }
                    //else if (GRND_vMFG_Date to get date 1 year below)
                    //{
                    //    MessageBox.Show("Self life Experid", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;

                    //}
                   else if(dtFIFO.Rows.Count < 0)
                    {
                        MessageBox.Show("ERROR", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                         
                   }
                }
                if (decimal.Parse(txtRquestQty.Text) <= decimal.Parse(txtScanedQty.Text))
                {
                    //MessageBox.Show("Qty Full ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //btnSubmitt.Enabled = true;
                    //return;
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
                            Scan_qty.Focus();

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
                }
                else
                {
                    txt_Barcode.Text = "";
                    txt_Barcode.Focus();
                    MessageBox.Show("Invalid Barcode", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = false;
                }

               // Scan_qty.Focus();
            }

            //if (e.KeyCode == Keys.Enter)
            //{
            //    DataTable dtFIFO = dbFunctions.getTable("pr_get_Material_Issued_Details_FIFO '" + Route_Card_ID + "','" + txt_Barcode.Text.ToString() + "'");

            //    //DataTable dtFIFO = dbFunctions.getTable("pr_get_Material_Issued_Details_FIFO '" + Route_Card_ID + "','" + txt_Barcode.Text.ToString() + "'");
            //    if (dtFIFO.Rows.Count > 0)
            //    //     if (dtFIFO.Rows.Count == 0)
            //    {
            //        MessageBox.Show("FIFO Error. Previous Lot Available", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    //if (dtFIFO.Rows.Count == 0)
            //    //{
            //    //    MessageBox.Show("Previous GRN Barcode Pending", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //            //if (dt.Rows[0]["CS_item_iD"].ToString().Equals(IID))
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

            DataTable dtFIFO = dbFunctions.getTable("pr_get_Material_Issued_Details_FIFO '"+ Route_Card_ID + "','"+ txt_Barcode.Text.ToString() + "'");
            if(dtFIFO.Rows.Count > 0)
           //     if (dtFIFO.Rows.Count < 0)
                {
                MessageBox.Show("FIFO Error. Previous Lot Available", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                DataTable a = dbFunctions.getTable("update Production_Request set Pa_txtScanedQty='"+ txtScanedQty.Text+ "' where Pq_IID='" + Route_Card_ID + "' ");
            DataTable dt = dbFunctions.getTable("pr_update_Material_Issued_Details  " + Route_Card_ID);
            MessageBox.Show("Material Issued Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            }
        }

        private void Scan_qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dtFIFO = dbFunctions.getTable("pr_get_Material_Issued_Details_FIFO '" + Route_Card_ID + "','" + txt_Barcode.Text.ToString() + "'");
                if (checkBox1.Checked == false)
                {
                    if (dtFIFO.Rows.Count > 0)
                    // if (dtFIFO.Rows.Count < 0)
                    {
                        MessageBox.Show("FIFO Error. Previous Lot Available", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }


                save_data();
                txt_Barcode.Focus();
            }
        }
        public bool isRM = false;
        private void cmbRMSpec_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_RMSpec_PartNameDetails '" + Part_ID + "','" + cmbRMSpec.SelectedValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    txtRMGrade.Text = dt.Rows[0]["IM_PartName"].ToString();
                    if (dt.Rows[0]["UM_UOM"].ToString().ToUpper().Equals("KG"))
                    {
                        txtReqQty.Text = ((decimal.Parse(dt.Rows[0]["BM_Qty"].ToString()) * decimal.Parse(txtxplanQty.Text)) / 1000).ToString("0.00");

                    }
                    else
                    {
                        txtReqQty.Text = (decimal.Parse(dt.Rows[0]["BM_Qty"].ToString()) * decimal.Parse(txtxplanQty.Text)).ToString("0.00");

                    }
                    txtUOM.Text = dt.Rows[0]["UM_UOM"].ToString();
                    txtStockQty.Text = dt.Rows[0]["Qty"].ToString();
                    BM_Type.Text = dt.Rows[0]["BM_Type"].ToString();


                }
            }
            catch { }

            display_Scanned_Details();
            
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

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Txt_Barcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void Scan_qty_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtRCNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void plant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(plant.Text))
                {
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Plant Name] LIKE '%{0}%'", plant.Text);
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }


        public class PasswordForm : Form
        {
            private TextBox txtPassword;
            private Button btnOK;
            private Button btnCancel;
            private Label lblPrompt;

            public string Password => txtPassword.Text;

            public PasswordForm()
            {
                this.Text = "Password Required";
                this.Size = new System.Drawing.Size(300, 150);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterParent;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                lblPrompt = new Label() { Text = "Enter Password:", Left = 10, Top = 15, Width = 260 };

                txtPassword = new TextBox()
                {
                    Left = 10,
                    Top = 35,
                    Width = 260,
                    PasswordChar = '*'   // ← This masks input as ***
                };

                btnOK = new Button()
                {
                    Text = "OK",
                    Left = 110,
                    Top = 70,
                    Width = 75,
                    DialogResult = DialogResult.OK
                };

                btnCancel = new Button()
                {
                    Text = "Cancel",
                    Left = 195,
                    Top = 70,
                    Width = 75,
                    DialogResult = DialogResult.Cancel
                };

                this.Controls.AddRange(new Control[] { lblPrompt, txtPassword, btnOK, btnCancel });
                this.AcceptButton = btnOK;
                this.CancelButton = btnCancel;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            using (PasswordForm pf = new PasswordForm())
            {
                if (pf.ShowDialog() == DialogResult.OK)
                {
                    if (pf.Password != "KYH@123#")
                    {
                        MessageBox.Show("Invalid Password", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        checkBox1.Checked = false;
                    }
                }
                else
                {
                    // User clicked Cancel
                    checkBox1.Checked = false;
                }
            }
        }
    }
}
