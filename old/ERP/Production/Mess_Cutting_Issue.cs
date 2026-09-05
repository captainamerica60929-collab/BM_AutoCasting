using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;

namespace CRM_App.production
{
    public partial class Mess_Cutting_Issue : Form
    {
        public bool isReqNo_Load = false;
        public string MachineID = "";
        public string Mould_ID = "";

        public Mess_Cutting_Issue()
        {
            InitializeComponent();
        }


        private void Production_request_Load(object sender, EventArgs e)
        {
            RevRights();
           
            Mq_vPart_No.Focus();
            display();
            LoadRMSpec();
            Load_Part_Number();
            LoadReqNo();

        }

        public void RevRights()
        {
           
        }
       
        private void LoadRMSpec()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_getBOMDetails_Mess '" + Mq_vPart_No.SelectedValue.ToString() + "'");
                Mq_RM_Spec.DataSource = dt;
                Mq_RM_Spec.DisplayMember = "IM_PartNo";
                Mq_RM_Spec.ValueMember = "IM_ID";
                Mq_RM_Spec.SelectedIndex =0;
                //isRMSpec_Load = true;
            }
            catch(Exception ex)
            {
            }
        }

      

        public void LoadReqNo()
        {

            try
            {
                DataTable dt = dbFunctions.getTable("pr_Generate_Mess_Cutting_Req_No");
                if(dt.Rows.Count>0)
                {
                Mq_RequestNo.Text = dt.Rows[0][0].ToString();
                }

            }
            catch
            {
            }
        }

        private void Load_Part_Number()
        {
            try
            {

                DataTable dt = dbFunctions.getTable("Pr_get_Part_NameBOM");
                Mq_vPart_No.DataSource = dt;
                Mq_vPart_No.DisplayMember = "IM_PartName";
                Mq_vPart_No.ValueMember = "IM_ID";
                Mq_vPart_No.SelectedIndex = -1;
                isReqNo_Load = true;
            }
            catch
            {
            }
        }

        private void Pq_vPart_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isReqNo_Load)
                {
                    DataTable dt = dbFunctions.getTable("pr_get_PartName_Details '" + Mq_vPart_No.SelectedValue.ToString() + "'");
                    Pq_vPart_Name.Text = dt.Rows[0]["IM_PartNo"].ToString();
                    Pq_vModel.Text = dt.Rows[0]["ML_Model"].ToString();
                   
                    MachineID = dt.Rows[0]["MM_ID"].ToString();
                    Mould_ID = dt.Rows[0]["MLD_ID"].ToString();

                    LoadRMSpec();
                    SpecDetails();
                    
                     Mq_Plan_Qty.Text = dt.Rows[0]["Plan Qty"].ToString();
                    
                }
            }
            catch { }
        }

      
        public void SpecDetails()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_RMSpec_PartNameDetails '" +Mq_vPart_No.SelectedValue+"','"+ Mq_RM_Spec.SelectedValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    Mq_RM_Grade.Text = dt.Rows[0]["IM_PartName"].ToString();
                    txtRMQty.Text = dt.Rows[0]["BM_Qty"].ToString();
                    txtUOM.Text = "Metters";
                    txtStockQty.Text = dt.Rows[0]["Qty"].ToString();


                }
            }
            catch { }
        }
        private void Pq_Mould_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(isMoldLoad)
            //{
            //    DataTable dt = dbFunctions.getTable("pr_Get_ProdReq_Mould_Details '" + Pq_Mould_Name.Text.ToString() + "'");
            //    Pq_Mould_Number.Text = dt.Rows[0]["MLD_MouldNo"].ToString();
            //    txtMouldLife.Text = dt.Rows[0]["MLD_MouldLife"].ToString();
            //    txtremainingshots.Text = dt.Rows[0]["MLD_OpeningShots"].ToString();
                    
            //}
        }

      


        private void Pq_RM_Spec_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable dt = dbFunctions.getTable("pr_Get_RMSpec_PartNameDetails '" + Pq_RM_Spec.SelectedValue.ToString() + "'");
            //    if (dt.Rows.Count > 0)
            //    {
            //        Pq_RM_Grade.Text = dt.Rows[0]["IM_Grade"].ToString();
            //        txtRMQty.Text = dt.Rows[0]["BM_Qty"].ToString();
            //        txtUOM.Text = dt.Rows[0]["UM_UOM"].ToString();

            //    }
            //}
            //catch { }
            SpecDetails();
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

      
        public string ErrorMessage = "";
        public bool Validate()
        {

            if ((string.IsNullOrEmpty(Mq_Plan_Qty.Text.Trim())))
            {
                ErrorMessage = "Plan Qty Should Not be Empty";
                Mq_Plan_Qty.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(Mq_vPart_No.Text.Trim())))
            {
                ErrorMessage = "Part No Should Not be Empty";
                Mq_vPart_No.Focus();
                return true;
            }
            try
            {
                int x = int.Parse(Mq_vPart_No.SelectedValue.ToString());
            }
            catch
            {
                ErrorMessage = "Select Proper Part No";
                Mq_vPart_No.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(Mq_RM_Spec.Text.Trim())))
            {
                ErrorMessage = "Part Name Should Not be Empty";
                Mq_RM_Spec.Focus();
                return true;
            }
            //try
            //{
            //    int x = int.Parse(Pq_RM_Spec.SelectedValue.ToString());
            //}
            //catch
            //{
            //    ErrorMessage = "Select Proper Part Name";
            //    Pq_RM_Spec.Focus();
            //    return true;
            //}


          
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
               // Update();
            }
            else
            {
               
                LoadReqNo();
                insert();

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
                com.CommandText = "Pr_Insert_Mess_Cutting_Issue";
                com.Parameters.Add("@Mq_RequestNo", SqlDbType.VarChar).Value = Mq_RequestNo.Text.ToString();
                com.Parameters.Add("@Mq_Route_Card_Start_Date", SqlDbType.VarChar).Value = Mq_Route_Card_Start_Date.Value.ToString("dd-MMM-yyyy hh:mm tt");
                com.Parameters.Add("@Mq_Route_Card_End_Date", SqlDbType.VarChar).Value = Mq_Route_Card_Start_Date.Value.ToString("dd-MMM-yyyy hh:mm tt");
                com.Parameters.Add("@Mq_iPart_ID", SqlDbType.VarChar).Value = Mq_vPart_No.SelectedValue.ToString();
                com.Parameters.Add("@Mq_vPart_No", SqlDbType.VarChar).Value = Pq_vPart_Name.Text.ToString();
                com.Parameters.Add("@Mq_Plan_Qty", SqlDbType.VarChar).Value = Mq_Plan_Qty.Text.ToString();
                com.Parameters.Add("@Mq_vPart_Name", SqlDbType.VarChar).Value = Mq_vPart_No.Text.ToString();
                com.Parameters.Add("@Mq_RM_ID", SqlDbType.VarChar).Value = Mq_RM_Spec.SelectedValue.ToString();
                com.Parameters.Add("@Mq_RM_Spec", SqlDbType.VarChar).Value = Mq_RM_Spec.Text.ToString();
                com.Parameters.Add("@Mq_RM_Grade", SqlDbType.VarChar).Value = Mq_RM_Grade.Text.ToString();
                com.Parameters.Add("@Mq_RM_Req_Qty", SqlDbType.VarChar).Value = Mq_RM_Req_Qty.Text.ToString();
                com.Parameters.Add("@Mq_Barcode", SqlDbType.VarChar).Value = txt_Barcode.Text.ToString();
                com.Parameters.Add("@Mq_CreatedDate", SqlDbType.VarChar).Value = dbFunctions.username;
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
            Mq_vPart_No.Text = "";
            Pq_vPart_Name.Text = "";
            Mq_RM_Spec.SelectedValue = -1;
            Mq_RM_Spec.Text = "";
            Mq_RM_Grade.Text = "";
            Mq_Plan_Qty.Text = "";
            Pq_vModel.Text = "";
            txtRMQty.Text = "";
             txtUOM.Text.ToString();
            txtUOM.Text = "";
            txtStockQty.Text = "";
            Mq_RM_Req_Qty.Text = "";
            
        }

        public void display()
        {
            DataTable dt = dbFunctions.getTable("pr_Display_Mess_Cutting_Issue");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                    DataTable dt = dbFunctions.getTable("pr_Delete_Production_Request " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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

        private void Pq_RM_Plan_Qty_KeyDown(object sender, KeyEventArgs e)
        {
            char newchar = Convert.ToChar(e.KeyValue);
            if(char.IsControl(newchar))
            {
                return;
            }
            int value;
            e.SuppressKeyPress=int.TryParse((sender as TextBox).Text+newchar.ToString(),out value)?value==0:true;
        }

        private void Pq_RM_Plan_Qty_TextChanged(object sender, EventArgs e)
        {
            Calc();
        }

        void Calc()
        {
            try
            {

                Mq_RM_Req_Qty.Text = ((decimal.Parse(Mq_Plan_Qty.Text) * decimal.Parse(txtRMQty.Text))).ToString("0.00");


            }
            catch { }
        }

        private void txtRMQty_TextChanged(object sender, EventArgs e)
        {
            Calc();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadReqNo();
          
            save_data();
            insert();
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
                com.Parameters.Add("@CS_RouteCardNo", SqlDbType.VarChar).Value = Mq_RequestNo.Text.Replace("MC","");
                com.Parameters.Add("@CS_Type", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Type"].ToString();
                com.Parameters.Add("@CS_Item_ID", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Item_ID"].ToString();
                com.Parameters.Add("@CS_Part_No", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Part_No"].ToString();
                com.Parameters.Add("@CS_Part_Name", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Part_Name"].ToString();
                com.Parameters.Add("@CS_Lot_No", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Lot_No"].ToString();
                com.Parameters.Add("@CS_Lot_Date", SqlDbType.VarChar).Value = dt.Rows[0]["CS_Lot_Date"].ToString();
                com.Parameters.Add("@CS_Qty", SqlDbType.VarChar).Value = "-" + Mq_RM_Req_Qty.Text.ToString();
                com.Parameters.Add("@CS_UOM", SqlDbType.VarChar).Value = dt.Rows[0]["CS_UOM"].ToString();
                com.ExecuteNonQuery();
                com.Connection.Close();
                
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Mq_Barcode_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {


               

                DataTable dt = dbFunctions.getTable("pr_get_Barcode_Data  '" + txt_Barcode.Text + "'");
                if (dt.Rows.Count > 0)
                {

                    if (decimal.Parse(dt.Rows[0]["CS_Qty"].ToString()) > 0)
                    {
                        string s = Mq_RM_Spec.SelectedValue.ToString();
                        if (dt.Rows[0]["CS_item_iD"].ToString().Equals(s))
                        {
                            Scan_qty.Text = dt.Rows[0]["CS_Qty"].ToString();

                            btnsave.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("RM Specification Missmatch", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_Barcode.Text = "";
                            txt_Barcode.Focus();
                            btnsave.Enabled = false;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Barcode Already Scanned", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_Barcode.Text = "";
                        txt_Barcode.Focus();
                        btnsave.Enabled = false;
                    }
                }
                else
                {
                    txt_Barcode.Text = "";
                    txt_Barcode.Focus();
                    MessageBox.Show("Invalid Barcode", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnsave.Enabled = false;
                }

                Scan_qty.Focus();
            }
        }
    }
}
