using CRM_App.Crystal;
using Maintanence_Printing_Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM_App.Transaction.Work_Order
{
    
    public partial class New_Work_Order : Form
    {
        public New_Work_Order()
        {
            InitializeComponent();
        }

        private void Pq_RM_Spec_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Pq_RM_Spec_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Pq_RM_Spec_Leave(object sender, EventArgs e)
        {

        }

        private void Label15_Click(object sender, EventArgs e)
        {

        }

        private void wos_Price_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label50_Click(object sender, EventArgs e)
        {

        }

        private void TxtStockQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void wos_Qty_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string a = comboBox3.Text;
            if (a == "Inward")
            {
                tabControl1.SelectedTab = tabPage2;
                outwardload();
                comboBox5.Text = "";
                label5.Text = "JO DC Inward";
                Loadmaterial();
                textBox6.Text = "";

            }
            else if (a == "Out-Ward") {
                tabControl1.SelectedTab = tabPage1;
                Loaddetails();
                label5.Text = "JO DC Out-ward";

            }
            else
            { }
            wos_SupplierName.Focus();


        }

        private void outwardload()
        {
            pendingponumber();
        }

        private void Loaddetails()
        {
            LoadPo_No();
          
            Rmload();
            BOM();
            
            display_Scanned_Details();
            LoadItemType();
            display();
        }

        public void LoadItemType()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadItemType");
                wos_MaterialName.DataSource = dt;
                wos_MaterialName.DisplayMember = "Ty_TypeName";
                wos_MaterialName.ValueMember = "Ty_ID";
                wos_MaterialName.SelectedIndex = -1;
            }
            catch
            {
            }
        }

        // LOAD STORE
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

        // load doc no

        private void LoadPo_No()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_WO_Number");
                PO_vPO_NO.Text = dt.Rows[0][0].ToString();


            }
            catch
            {
            }
        }


        // supplier
        public void LoadSupplier()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadSupplier");
                wos_SupplierName.DataSource = dt;
                wos_SupplierName.DisplayMember = "SM_Name";
                wos_SupplierName.ValueMember = "SM_ID";
                wos_SupplierName.SelectedIndex = -1;


            }
            catch
            {
            }
        }

        // load rm
        private void Rmload()
        {
            DataTable A = dbFunctions.getTable("SELECT IM_ID,IM_PartName FROM ITEM_MASTER WHERE IM_TYPE='3' or IM_TYPE ='2' AND IM_Status='A'");
            Pq_RM_Spec.DataSource = A;
            Pq_RM_Spec.DisplayMember = "IM_PartName";
            Pq_RM_Spec.ValueMember = "IM_ID";
            Pq_RM_Spec.SelectedIndex = -1;
        }
        // load bom
        private void BOM()
        {
            try
            {
                DataTable A = dbFunctions.getTable("SELECT  (IM_PARTNO + ' / ' + IM_PARTNAME) AS [NAME],* FROM BOM_MASTER Left outer join item_master on IM_ID=BM_BomId WHERE BM_ItemId ='" + Pq_RM_Spec.SelectedValue.ToString() + "' AND BM_Status ='A'");
                if (A.Rows.Count > 0)
                {
                    comboBox4.DataSource = A;
                    comboBox4.DisplayMember = "NAME";
                    comboBox4.ValueMember = "IM_ID";
                    comboBox4.SelectedIndex = -1;               
                }

            }
            catch { }
        }

        DataTable dt = new DataTable();
        private void wos_SupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                dt = dbFunctions.getTable("pr_GetSupplier_Details '" + wos_SupplierName.SelectedValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    wos_SupplierId.Text = dt.Rows[0]["SM_PlantAddr"].ToString();
                    //PO_dExcise_Duty_Percent.Text = dt.Rows[0]["ED"].ToString();
                    //PO_dVAT_CST_Percentage.Text = dt.Rows[0]["TT"].ToString();

                    //wos_MaterialType.Text = "";
                    //POD_vSource.Text = "";
                    //POD_iUOM.Text = "";
                    //// wos_Price.Text = "";
                    //POD_vSpec.Text = "";
                    //POD_vPacking_Std.Text = "";
                    //textBox1.Text = "";
                    //txtGrade.Text = "";
                    //wos_Qty.Text = "";
                    //txtTotalPrice.Text = "";

                    //LoadItem();
                }
               // pendingponumber();
            }
            catch
            {

            }
            Loadmaterial();
        }

        private void New_Work_Order_Load(object sender, EventArgs e)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            load_store();
            LoadItemType();
            LoadSupplier();
            Loadmaterial();
        }

        private void Loadmaterial()
        {
            try
            {
                DataTable a = dbFunctions.getTable("select * from item_master where im_type='1'");
                wos_MaterialType.DataSource = a;
                wos_MaterialType.DisplayMember = "IM_PartName";
                wos_MaterialType.ValueMember = "im_id";
                wos_MaterialType.SelectedIndex = -1;



                textBox6.DataSource = a;
                textBox6.DisplayMember = "IM_PartNo";
                textBox6.ValueMember = "im_id";
                textBox6.SelectedIndex = -1;
            }
            catch (Exception EX){ }
        }

        private void pendingponumber()
        {
            //DataTable a = dbFunctions.getTable("pr_Fetch_Amend_Work_order_pending_details '" + wos_SupplierName.SelectedValue.ToString() + "'");
            DataTable a = dbFunctions.getTable($"pr_Fetch_Amend_Work_order_pending_details '{wos_SupplierName.SelectedValue}'");

            comboBox5.DataSource = a;
            comboBox5.DisplayMember = "Work Order No";
            comboBox5.ValueMember = "Id";
            comboBox5.SelectedIndex = -1;

        }



        private void Pq_RM_Spec_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            BOM();
            try
            {
                DataTable a1 = dbFunctions.getTable("select * from item_master where im_status='A' AND IM_ID='" + Pq_RM_Spec.SelectedValue.ToString() + "'");
                if (a1.Rows.Count > 0)
                {
                    wos_Price.Text = a1.Rows[0]["IM_Purchase_Price"].ToString();
                }
                DataTable a2 = dbFunctions.getTable("select sum(CS_Qty) as qty from current_stock where CS_Status='A' AND CS_Item_ID='" + Pq_RM_Spec.SelectedValue.ToString() + "'");
                if (a2.Rows.Count > 0)
                {
                    txtStockQty.Text = a2.Rows[0]["qty"].ToString();
                }
            }
            catch { }
          
        }
        String CAVITY1 = "0";
        String textBox17 = "0";
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("pr_Get_RMSpec_PartNameDetails '" + comboBox4.SelectedValue + "','" + Pq_RM_Spec.SelectedValue.ToString() + "'");

            if (dt.Rows.Count > 0)
            {
                //Pq_RM_Grade.Text = dt.Rows[0]["IM_PartName"].ToString();
                //  txtRMQty.Text = dt.Rows[0]["BM_Qty"].ToString();
                //POD_iUOM.Text = "Kg";
                //  txtStockQty.Text = dt.Rows[0]["Qty"].ToString();
                textBox15.Text = dt.Rows[0]["BM_Net_Part_Wt"].ToString();
                textBox16.Text = dt.Rows[0]["BM_Runner_Wt"].ToString();
                //CAVITY1.Text = dt.Rows[0]["BM_MoldCavity"].ToString();
                ////PART_WEIGHT.Text = dt.Rows[0]["BM_Net_Part_Wt"].ToString();
                ////  RUNNER_WEIGHT.Text = dt.Rows[0]["BM_Runner_Wt"].ToString();
                //textBox17.Text = dt.Rows[0]["BM_MoldCavity"].ToString();
                //OYT1.Text = dt.Rows[0]["BM_Qty"].ToString();
                //wos_Price.Text = dt.Rows[0]["IM_Purchase_Price"].ToString();



            }
        }
        public string ErrorMessage = "";
        public bool Validate()
        {

            if ((string.IsNullOrEmpty(wos_Qty.Text.Trim())))
            {
                ErrorMessage = "Qty Should Not be Empty";
                wos_Qty.Focus();
                return true;

            }

            if ((string.IsNullOrEmpty(wos_MaterialName.Text.Trim())))
            {
                ErrorMessage = "Material Type Should Not be Empty";
                wos_MaterialName.Focus();
                return true;
            }

            return false;
        }

       
        String ID = "0";
        private void button7_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.Text;
                    if (ID == "0")
                    {
                        DataTable dd = dbFunctions.getTable("select isnull(max(dc_id),0)+1 from doc_Details");
                        if (dd.Rows.Count > 0)
                        {
                            ID = dd.Rows[0][0].ToString();
                        }
                        com.CommandText = "insert into doc_Details (dc_id,dc_docno,dc_data,dc_rmid,dc_rmname,dc_fgid,dc_fgname,dc_fgqty,dc_rmkg,dc_status,dc_cratedate,dc_createduser,dc_pw) " +
                            "Values (@dc_id,@dc_docno,@dc_data,@dc_rmid,@dc_rmname,@dc_fgid,@dc_fgname,@dc_fgqty,@dc_rmkg,@dc_status,@dc_cratedate,@dc_createduser,@dc_pw)";
                        //Type = "NEW";
                    }
                    else
                    {
                        com.CommandText = "update  doc_Details Set un_name=@un_name,un_value=@un_value,un_status=@un_status,un_craeteddate=@un_craeteddate,un_username=@un_username,dc_pw=@dc_pw where un_id=@un_id ";
                        //Type = "EDIT";

                    }

                    com.Parameters.Add("@dc_id", SqlDbType.Int).Value = ID.ToString();
                    com.Parameters.Add("@dc_docno", SqlDbType.Int).Value = PO_vPO_NO.Text;
                    com.Parameters.Add("@dc_data", SqlDbType.DateTime).Value = PO_dPO_Date.Value.ToString("dd/MMM/yyyy");
                    com.Parameters.Add("@dc_rmid", SqlDbType.Int).Value = Pq_RM_Spec.SelectedValue.ToString();
                    com.Parameters.Add("@dc_rmname", SqlDbType.VarChar).Value = Pq_RM_Spec.Text;
                    com.Parameters.Add("@dc_fgid", SqlDbType.Int).Value = comboBox4.SelectedValue.ToString();
                    com.Parameters.Add("@dc_fgname", SqlDbType.VarChar).Value = comboBox4.Text;
                    com.Parameters.Add("@dc_fgqty", SqlDbType.Int).Value = textBox14.Text;
                    com.Parameters.Add("@dc_rmkg", SqlDbType.VarChar).Value = wos_Qty.Text;
                   // com.Parameters.Add("@dc_stockid", SqlDbType.VarChar).Value = store.SelectedValue.ToString();
                    com.Parameters.Add("@dc_status", SqlDbType.VarChar).Value = 'A';
                    com.Parameters.Add("@dc_cratedate", SqlDbType.DateTime).Value = dbFunctions.getdate();
                    com.Parameters.Add("@dc_createduser", SqlDbType.VarChar).Value = dbFunctions.username;
                    com.Parameters.Add("@dc_pw", SqlDbType.VarChar).Value = textBox15.Text;

                    com.ExecuteNonQuery();
                    com.Connection.Close();
                    MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ID = "0";


                }
                catch (Exception Ex)
                {
                    //dbFunctions.Logs(Ex.Message, dbFunctions.username);
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                panel2.Visible = true;
                display_Scanned_Details();
           
                //display();
                clear();
                //display_Scanned_Details();
                //panel2.Visible = true;
            }
        }
        private void display_Scanned_Details1()
        {
            dataGridView3.Visible = true;

            //DataTable a = dbFunctions.getTable("select CS_Part_No as [Part no], ABS(CS_Qty) as Qty from current_stock where CS_RouteCardNo='" + PO_vPO_NO.Text+ "'");
            //dataGridView3.DataSource = a;
            DataTable a = dbFunctions.getTable("select CS_iID as [ID],CS_Part_No as [Part no], ABS(CS_Qty) as Qty from current_stock where CS_RouteCardNo='" + PO_vPO_NO.Text + "' and cs_dc='dc' AND CS_Item_ID='" + Pq_RM_Spec.SelectedValue.ToString() + "' ");
            dataGridView4.DataSource = a;
            dataGridView4.Columns["ID"].Visible = true;
            decimal tot_gst = 0m;
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                if (dataGridView4.Rows[i].Cells["Qty"].Value != null &&
                    decimal.TryParse(dataGridView4.Rows[i].Cells["Qty"].Value.ToString(), out decimal qty)) // Use decimal.TryParse
                {
                    tot_gst += qty;
                }
            }

            wos_Qty.Text = tot_gst.ToString();



        }

        private void clear()
        {
            comboBox4.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
        }

        private void display_Scanned_Details()
        {
            DataTable a = dbFunctions.getTable("select dc_id as [ID],dc_fgname as [Part name], dc_fgqty as Qty from doc_Details where dc_docno='" + PO_vPO_NO.Text + "' and dc_status='A'");
            dataGridView3.DataSource = a;
          //  dataGridView3.Columns["ID"].Visible = false;
            dbFunctions.DGVStyle(dataGridView3);
        }

        private void PO_vPO_NO_TextChanged(object sender, EventArgs e)
        {
            display();
            display_Scanned_Details();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DialogResult result = MessageBox.Show("Do You Want to Delete, Press YES", "Alcove", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                   dbFunctions.getTable("UPDATE doc_Details SET dc_status='D' WHERE dc_id = '" + dataGridView3.Rows[e.RowIndex].Cells["ID"].Value.ToString() + "'");
                    //fetch_rec();
                    display_Scanned_Details();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
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
                    com.CommandText = "PR_INSERT_Work_Order_Send_Details";
                    com.Parameters.Add("@wos_WorkOrderNo", SqlDbType.VarChar).Value = PO_vPO_NO.Text.ToString();
                    com.Parameters.Add("@wos_SupplierId", SqlDbType.Int).Value = wos_SupplierName.SelectedValue.ToString();
                    com.Parameters.Add("@wos_SupplierName", SqlDbType.VarChar).Value = wos_SupplierName.Text.ToString();
                    com.Parameters.Add("@wos_Date", SqlDbType.DateTime).Value = PO_dPO_Date.Value.ToString("dd/MMM/yyyy");
                    //com.Parameters.Add("@PO_dPO_Date", SqlDbType.DateTime).Value = PO_dPO_Date.Value.ToString("dd-MMM-yyyy");
                    com.Parameters.Add("@wos_MaterialType", SqlDbType.VarChar).Value = wos_MaterialName.Text.ToString();
                    //com.Parameters.Add("@wos_MaterialId", SqlDbType.Int).Value = wos_MaterialType.SelectedValue.ToString();
                    //com.Parameters.Add("@wos_MaterialName", SqlDbType.VarChar).Value = wos_MaterialType.Text.ToString();
                    com.Parameters.Add("@wos_MaterialId", SqlDbType.Int).Value = Pq_RM_Spec.SelectedValue.ToString();
                    com.Parameters.Add("@wos_MaterialName", SqlDbType.VarChar).Value = Pq_RM_Spec.Text.ToString();
                    com.Parameters.Add("@wos_Qty", SqlDbType.VarChar).Value = wos_Qty.Text.ToString();
                    com.Parameters.Add("@wos_Price", SqlDbType.VarChar).Value = wos_Price.Text.ToString();
                    com.Parameters.Add("@wos_Note", SqlDbType.VarChar).Value = wos_Note.Text.ToString();
                   // com.Parameters.Add("@wos_store", SqlDbType.VarChar).Value = store.SelectedValue.ToString();
                    com.Parameters.Add("@wos_Remark", SqlDbType.VarChar).Value = wos_Remark.Text.ToString();
                    com.Parameters.Add("@wos_User", SqlDbType.VarChar).Value = dbFunctions.username;
                   // com.Parameters.Add("@V_no", SqlDbType.VarChar).Value = V_no.Text.ToString();
                    //com.Parameters.Add("@wos_PARTWEIGHT", SqlDbType.VarChar).Value = textBox15.Text.ToString();
                    //com.Parameters.Add("@wos_RUNNERWEIGHT", SqlDbType.VarChar).Value = textBox16.Text.ToString();
                    ////com.Parameters.Add("@CAVITY", SqlDbType.VarChar).Value = CAVITY.Text.ToString();
                    //com.Parameters.Add("@wos_PLAN", SqlDbType.VarChar).Value = textBox14.Text.ToString();
                    //com.Parameters.Add("@wos_partid", SqlDbType.Int).Value = comboBox4.SelectedValue.ToString();
                    ////com.Parameters.Add("@wos_partid", SqlDbType.VarChar).Value = comboBox4.SelectedValue.ToString();
                    //com.Parameters.Add("@wos_partname", SqlDbType.VarChar).Value = comboBox4.Text.ToString();
                    //com.Parameters.Add("@wos_txtRMQty", SqlDbType.VarChar).Value = txtRMQty.Text.ToString();
                    //com.Parameters.Add("@wos_PLAN", SqlDbType.VarChar).Value = int.Parse(PLAN.Text);

                    com.ExecuteNonQuery();
                    com.Connection.Close();
                    MessageBox.Show("Details Saved Successfully ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear1();
                    ////dataGridView3.Visible = false;
                    display();
                }
                catch (Exception Ex)
                {
                    dbFunctions.Logs(Ex.Message, dbFunctions.username);
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Clear1()
        {
            Pq_RM_Spec.Text = "";
            txtStockQty.Text = "";
            wos_Price.Text = "";
            wos_Qty.Text = "";
        }

        private void display()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Work_Order_Send_Details '" + PO_vPO_NO.Text.ToString() + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPo_No();
            display();
            display_Scanned_Details();
            display_Scanned_Details1();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dictionary<string, decimal> amount = new Dictionary<string, decimal>();
                DataTable dt = dbFunctions.getTable($"PR_FETCH_DC_PRINT  {PO_vPO_NO.Text.ToString()}");
                decimal totalAmount = 0m;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if ((!amount.ContainsKey(dt.Rows[i]["wos_MaterialName"].ToString())))
                        {
                            decimal qty = 0m;
                            decimal tot = 0m;
                            decimal pw = 0m;
                            qty = decimal.Parse(dt.Rows[i]["wos_Qty"].ToString());
                            tot = decimal.Parse(dt.Rows[i]["wos_Price"].ToString());
                            //pw = decimal.Parse(dt.Rows[i]["dc_pw"].ToString());
                            totalAmount += qty * tot;
                            amount.Add(dt.Rows[i]["wos_MaterialName"].ToString(), tot);
                        }
                    }
                }
                //decimal totalprice = Convert.ToDecimal(totalAmount)(18, 2);
                decimal totalprice = Math.Round(Convert.ToDecimal(totalAmount), 2);

                //DC_Print11 oRpt = new DC_Print11();
                //oRpt.DataDefinition.FormulaFields["totalprice"].Text = "'" + totalprice.ToString() + "'";
                //string SQlQuery = "PR_FETCH_DC_PRINT '" + PO_vPO_NO.Text.ToString() + "'";
                //dbFunctions.printpdf(PO_vPO_NO.Text.ToString(), SQlQuery, oRpt);
                //Cursor.Current = Cursors.Default;
            }
            catch (Exception Ex)

            {
                throw (Ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void PO_vSupplier_Name_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void IM_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable a = dbFunctions.getTable("Pr_Get_Partload_details '" + comboBox5.Text + "'");
                wos_MaterialType.DataSource = a;
                wos_MaterialType.DisplayMember = "IM_PartName";
                wos_MaterialType.ValueMember = "im_id";
                wos_MaterialType.SelectedIndex = -1;
            }
            catch { }

            display_Receive();


        }
        String Rmid = "0";
        String subid = "0";
        private void wos_MaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable a = dbFunctions.getTable("select * from item_master where im_id='" + wos_MaterialType.SelectedValue.ToString() + "'");
                if (a.Rows.Count > 0)
                {
                    textBox6.Text = a.Rows[0]["IM_PartNo"].ToString();
                }
                else
                {
                    textBox6.Text = "";
                }
            }
            catch
            {
                textBox6.Text = "";

            }
            //DataTable a = dbFunctions.getTable("Pr_Get_Partload_details_qty '" + comboBox5.Text + "','"+ wos_MaterialType.Text+ "'");
            //if (a.Rows.Count > 0)
            //{
            //    textBox1.Text=a.Rows[0]["dc_fgqty"].ToString();
            //    comboBox2.Text=a.Rows[0]["dc_rmname"].ToString();
            //    Rmid=a.Rows[0]["dc_rmid"].ToString();
            //    wor_Price.Text = a.Rows[0]["IM_Sales_Price"].ToString();

            //    subid = a.Rows[0]["im_id"].ToString(); //sub part



            //}
            //SpecDetails();


            //try
            //{
            //    DataTable A = dbFunctions.getTable("SELECT  (IM_PARTNO + ' / ' + IM_PARTNAME) AS [NAME],* FROM BOM_MASTER Left outer join item_master on IM_ID=BM_BomId WHERE BM_ItemId ='" + wos_MaterialType.SelectedValue.ToString() + "' AND BM_Status ='A'");
            //    if (A.Rows.Count > 0)
            //    {
            //        comboBox2.DataSource = A;
            //        comboBox2.DisplayMember = "NAME";
            //        comboBox2.ValueMember = "IM_ID";
            //        comboBox2.SelectedIndex = -1;


            //    }

            //}
            //catch { }

            //  DataTable a = dbFunctions.getTable("Pr_Get_Partload_details_qty '" + comboBox5.Text + "','" + wos_MaterialType.Text + "'");

        }
        private void SpecDetails()
        {
            try
            {
                //DataTable dt = dbFunctions.getTable("pr_Get_RMSpec_PartNameDetails '" + wos_MaterialType.SelectedValue + "','" + Pq_RM_Spec.SelectedValue.ToString() + "'");
                DataTable dt = dbFunctions.getTable("pr_Get_RMSpec_PartNameDetails12 '" + wos_MaterialType.SelectedValue + "','" + Rmid.ToString() + "'");

                if (dt.Rows.Count > 0)
                {
                    //Pq_RM_Grade.Text = dt.Rows[0]["IM_PartName"].ToString();
                    //  txtRMQty.Text = dt.Rows[0]["BM_Qty"].ToString();
                    //POD_iUOM.Text = "Kg";
                    //  txtStockQty.Text = dt.Rows[0]["Qty"].ToString();
                    textBox5.Text = dt.Rows[0]["BM_Net_Part_Wt"].ToString();
                    textBox2.Text = dt.Rows[0]["BM_Runner_Wt"].ToString();
                    CAVITY.Text = dt.Rows[0]["BM_MoldCavity"].ToString();
                    //PART_WEIGHT.Text = dt.Rows[0]["BM_Net_Part_Wt"].ToString();
                    //  RUNNER_WEIGHT.Text = dt.Rows[0]["BM_Runner_Wt"].ToString();
                    //CAVITY.Text = dt.Rows[0]["BM_MoldCavity"].ToString();
                    //OYT1.Text = dt.Rows[0]["BM_Qty"].ToString();
                    wos_Price.Text = dt.Rows[0]["IM_Purchase_Price"].ToString();



                }
            }
            catch { }
        }


        private void button6_Click(object sender, EventArgs e)
        {
         if (string.IsNullOrWhiteSpace(wos_MaterialName.Text))
                {

                ErrorMessage = "Material Type Should Not be Empty";
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                wos_MaterialName.Focus();
                return;
                              
            }
         if (string.IsNullOrWhiteSpace(wor_Price.Text))
                {

                ErrorMessage = "Price Should Not be Empty";
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                wor_Price.Focus();
                return;

            }
             if (string.IsNullOrWhiteSpace(wor_Qty.Text))
                {

                ErrorMessage = "Qty Should Not be Empty";
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                wor_Qty.Focus();
                return;

               }
            if (string.IsNullOrWhiteSpace(wor_supllierdcno.Text))
            {

                ErrorMessage = "Doc No Should Not be Empty";
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                wor_supllierdcno.Focus();
                return;

            }

            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {

                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "PR_INSERT_Work_Order_Receive_Details";
                com.Parameters.Add("@wor_WorkOrderNo", SqlDbType.VarChar).Value = wor_supllierdcno.Text.ToString();
                com.Parameters.Add("@wor_SupplierId", SqlDbType.VarChar).Value = wos_SupplierName.SelectedValue.ToString();
                com.Parameters.Add("@WOR_PID", SqlDbType.VarChar).Value = wos_MaterialType.SelectedValue.ToString();
                com.Parameters.Add("@wor_SupplierName", SqlDbType.VarChar).Value = wos_SupplierName.Text.ToString();
                com.Parameters.Add("@wor_Date", SqlDbType.DateTime).Value = PO_dPO_Date.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@wor_MaterialType", SqlDbType.VarChar).Value = wos_MaterialName.Text.ToString();
                //com.Parameters.Add("@wor_MaterialId", SqlDbType.VarChar).Value = comboBox2.SelectedValue.ToString();
                //com.Parameters.Add("@wor_MaterialId", SqlDbType.VarChar).Value = Rmid.ToString();
                //com.Parameters.Add("@wor_MaterialName", SqlDbType.VarChar).Value = comboBox2.Text.ToString();
                com.Parameters.Add("@wor_Qty", SqlDbType.VarChar).Value = wor_Qty.Text.ToString();
                com.Parameters.Add("@wor_Price", SqlDbType.VarChar).Value = wor_Price.Text.ToString();
                com.Parameters.Add("@wor_Note", SqlDbType.VarChar).Value = wor_Note.Text.ToString();
                com.Parameters.Add("@wor_User", SqlDbType.VarChar).Value = dbFunctions.username;
                com.Parameters.Add("@wor_supplierqty", SqlDbType.VarChar).Value = textBox7.Text.ToString();
                com.Parameters.Add("@PARTWEIGHT", SqlDbType.VarChar).Value = textBox8.Text.ToString();
                com.Parameters.Add("@RUNNERWEIGHT", SqlDbType.VarChar).Value = textBox9.Text.ToString();
                // com.Parameters.Add("@wor_plantid", SqlDbType.VarChar).Value = store.SelectedValue.ToString();
                //  com.Parameters.Add("@wor_MaterialType_ID", SqlDbType.VarChar).Value = wos_MaterialName.SelectedValue.ToString();
                //com.Parameters.Add("@WOR_PRTID", SqlDbType.VarChar).Value = subid.ToString();
                //com.Parameters.Add("@WOR_PRTIDNAME", SqlDbType.VarChar).Value = wos_MaterialType.Text.ToString(); 
                //com.Parameters.Add("@wor_supllierdcno", SqlDbType.VarChar).Value = wor_supllierdcno.Text.ToString(); 
                com.ExecuteNonQuery(); 
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                display_Receive();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Clear()
        {
            wos_MaterialType.Text = "";
            textBox1.Text = "";
            comboBox2.Text = "";
            wor_Price.Text = "";
            wor_Note.Text = "";
            textBox3.Text = "";
        }

        private void display_Receive()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_WO_Receive_Details '" + wor_supllierdcno.Text.ToString() + "'");
            dataGridView2.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        DataTable dt = dbFunctions.getTable("UPDATE  Work_Order_Receive_Details set wor_status='D' WHERE wor_Id= '" + dataGridView2.SelectedRows[0].Cells[0].Value.ToString() + "' ");
                        // DataTable dt = dbFunctions.getTable("Pr_Delete_Work_Order_Receive_Details " + dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                        MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        display_Receive();
                        Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    //DataTable dt1 = dbFunctions.getTable("pr_delete_Issued_material " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dt = dbFunctions.getTable("Pr_Delete_Work_Order_Send_Details " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    display(); 
                    Clear1();
                }
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable a = dbFunctions.getTable("select * from  Work_Order_Send_Details where wos_Id ='"+ dataGridView1.SelectedRows[0].Cells[0].Value.ToString()+"' ");
                if (a.Rows.Count > 1)
                {
                 //   Pq_RM_Spec.Text=a.Rows.cells[

                }

            }
        }

        private void txt_Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable a = dbFunctions.getTable(@"SELECT SUM(CS_Qty) AS QTY
                                     FROM current_stock
                                     WHERE CS_Barcode = '" + txt_Barcode.Text + "'");

                // Always check if the result has rows before accessing them
                if (a.Rows.Count > 0)
                {
                    // Try parsing the value safely
                    decimal qty = 0;
                    decimal.TryParse(a.Rows[0]["QTY"].ToString(), out qty);

                    if (qty == 0)
                    {
                        MessageBox.Show("Barcode Already Scanned", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_Barcode.Text = "";
                        return;
                    }
                }
                if (comboBox1.Items.Contains(txt_Barcode.Text))
                {

                    DataTable dt = dbFunctions.getTable("pr_get_Barcode_Data  '" + txt_Barcode.Text + "'");
                    if (dt.Rows.Count > 0)
                    {

                        if (decimal.Parse(dt.Rows[0]["CS_Qty"].ToString()) > 0)
                        {
                            Scan_qty.Text = dt.Rows[0]["CS_Qty"].ToString();


                        }
                        else
                        {
                            MessageBox.Show("Barcode Already Scanned", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_Barcode.Text = "";
                            txt_Barcode.Focus();
                            button1.Enabled = false;
                        }
                    }
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
        }

        private void Pq_RM_Spec_Leave_1(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Items.Clear();
                DataTable A = dbFunctions.getTable("SELECT CS_Barcode FROM CURRENT_STOCK WHERE CS_Item_ID = '" + Pq_RM_Spec.SelectedValue.ToString() + "' and CS_RouteCardNo is null  AND CS_Barcode NOT IN(SELECT CS_Barcode FROM Current_Stock  GROUP BY CS_Barcode HAVING COUNT(*) > 1) ");
                // comboBox1.Text = "";
                foreach (DataRow row in A.Rows)
                {
                    comboBox1.Items.Add(row["CS_Barcode"].ToString());
                }
            }
            catch { }

        }

        public string Route_Card_ID = "";
        private void Scan_qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dtFIFO = dbFunctions.getTable("pr_get_Material_Issued_Details_FIFO '" + Route_Card_ID + "','" + txt_Barcode.Text.ToString() + "'");
                //if (dtFIFO.Rows.Count > 0)
                //{
                //    MessageBox.Show("FIFO Error. Previous Lot Available", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                save_data();
            }
        }
        private void save_data()
        {
            DataTable dt = dbFunctions.getTable("pr_get_Barcode_details_By_ID  '" + txt_Barcode.Text + "'");
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Current_Stock1";
                com.Parameters.Add("@CS_Barcode", SqlDbType.VarChar).Value = txt_Barcode.Text.ToString();
                // com.Parameters.Add("@CS_RouteCardNo", SqlDbType.VarChar).Value = Route_Card_ID;PO_vPO_NO
                com.Parameters.Add("@CS_RouteCardNo", SqlDbType.VarChar).Value = PO_vPO_NO.Text.ToString();
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
                dataGridView3.Visible = true;
                display_Scanned_Details();
                display_Scanned_Details1();
                // calc();
                Scan_qty.Text = "";
                //wos_Qty.Text = "";
                txt_Barcode.Text = "";
                txt_Barcode.Focus();
                dataGridView3.Visible = true;
                //clear();

            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                //  MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DialogResult result = MessageBox.Show("Do You Want to Delete, Press YES", "Alcove", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    // dbFunctions.getTable("update sales_order set so_status='D',so_del_name='" + dbFunctions.username + "',so_del_date=getdate() where so_id='" + dataGridView2.Rows[e.RowIndex].Cells["ID"].Value.ToString() + "'");
                    dbFunctions.getTable("DELETE FROM Current_Stock WHERE CS_iID = '" + dataGridView4.Rows[e.RowIndex].Cells["ID"].Value.ToString() + "'");
                    //fetch_rec();
                    display_Scanned_Details1();
                }
            }
        }

        private void wor_Qty_TextChanged(object sender, EventArgs e)
        {

        }

        private void wor_Qty_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                //// textBox7.Text = ((decimal.Parse(wor_Qty.Text) * decimal.Parse(textBox6.Text)) / 1000).ToString("0.00");
                textBox8.Text = ((decimal.Parse(wor_Qty.Text) * decimal.Parse(textBox5.Text)) / 1000).ToString("0.00");
                //// textBox9.Text= ((decimal.Parse(wor_Qty.Text) * decimal.Parse(textBox5.Text))/ decimal.Parse(CAVITY1.Text) / 1000).ToString("0.00");

                textBox9.Text = ((decimal.Parse(wor_Qty.Text) * decimal.Parse(textBox2.Text)) / decimal.Parse(CAVITY.Text) / 1000).ToString("0.00");
                textBox7.Text = ((decimal.Parse(textBox8.Text) + decimal.Parse(textBox9.Text))).ToString("0.00");

            }
            catch { }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("pr_Get_RMSpec_PartNameDetails '" + comboBox2.SelectedValue + "','" + wos_MaterialType.SelectedValue.ToString() + "'");

            if (dt.Rows.Count > 0)
            {
                textBox5.Text = dt.Rows[0]["BM_Net_Part_Wt"].ToString();
                textBox2.Text = dt.Rows[0]["BM_Runner_Wt"].ToString();
                CAVITY.Text = dt.Rows[0]["BM_MoldCavity"].ToString();
            }
        }

        private void PO_vPO_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                display_Scanned_Details1();

            }
        }

        private void textBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable a = dbFunctions.getTable("select * from item_master where im_id='" + textBox6.SelectedValue.ToString() + "'");
                if (a.Rows.Count > 0)
                {
                    wos_MaterialType.Text = a.Rows[0]["IM_PartName"].ToString();
                }
                else
                {
                    wos_MaterialType.Text = "";
                }
            }
            catch
            {
                wos_MaterialType.Text = "";

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            wos_SupplierName.SelectedValue = -1;
            V_no.Text = "";
            comboBox5.SelectedValue=-1;
            wor_supllierdcno.Text = "";
            wos_MaterialType.SelectedValue = -1;
            textBox6.SelectedValue = -1;
            wor_Note.Text = "";
            wor_Price.Text = "";
            wor_Qty.Text = "";
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }
    }
}
