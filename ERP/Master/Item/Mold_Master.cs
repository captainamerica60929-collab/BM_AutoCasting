using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;

namespace LarchERP.Master
{

    public partial class Mold_Master : Form
    {
        public string arrow = "Up";
        public int Distance = 268;
        public string ID = "";
        public bool isPartNoLoad = false;
        string ErrorMessage = "";
        public Mold_Master()
        {
            InitializeComponent();
            dbFunctions.DGVStyle(dataGridView1);
        }




        private void ItemMaster_Shown(object sender, EventArgs e)
        {
            display();
            splitContainer1.SplitterDistance = Distance;
        }


        private void ArrowButton_Click(object sender, EventArgs e)
        {
            if (arrow.ToString().Equals("Up"))
            {
                splitContainer1.SplitterDistance = 25;
                arrow = "Down";
                ArrowButton.Image = CRM_App.Properties.Resources.Down;

            }
            else
            {
                splitContainer1.SplitterDistance = Distance;
                arrow = "Up";
                ArrowButton.Image = CRM_App.Properties.Resources.Up;
            }

        }

        private void ItemMaster_Load(object sender, EventArgs e)
        {

            panel3.Dock = DockStyle.Bottom;
            panel3.Height = 30;

            panel6.Dock = DockStyle.Fill;

            ArrowButton.Visible = true;
            MLD_Mold.Focus();


            MLD_PartNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            MLD_PartNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection strcollPN = new AutoCompleteStringCollection();

            DataTable dtPartNo = dbFunctions.getTable("select MLD_PartNo from Mold_Master where MLD_Status='A'");
            for (int i = 0; i < dtPartNo.Rows.Count; i++)
            {
                strcollPN.Add(dtPartNo.Rows[i]["MLD_PartNo"].ToString());
            }
            MLD_PartNo.AutoCompleteCustomSource = strcollPN;






            MLD_PartName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            MLD_PartName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection strcollPNM = new AutoCompleteStringCollection();

            DataTable dtPartName = dbFunctions.getTable("select MLD_PartName from Mold_Master where MLD_Status='A'");
            for (int i = 0; i < dtPartName.Rows.Count; i++)
            {
                strcollPNM.Add(dtPartName.Rows[i]["MLD_PartName"].ToString());
            }
            MLD_PartName.AutoCompleteCustomSource = strcollPNM;



            MLD_Mold.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            MLD_Mold.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection strcoll = new AutoCompleteStringCollection();

            DataTable dtcode = dbFunctions.getTable("select MLD_Mold from Mold_Master where MLD_Status='A'");
            for (int i = 0; i < dtcode.Rows.Count; i++)
            {
                strcoll.Add(dtcode.Rows[i]["MLD_Mold"].ToString());
            }
            MLD_Mold.AutoCompleteCustomSource = strcoll;

            
            
            
            LoadCustomer();
            LoadSupplier();
           // LoadPartNo();
            LoadModel();
            Radio_Active.Checked = true;
            display();
            Machine_Tonage();


        }

        private void Machine_Tonage()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("SELECT * FROM MACHINE_MASTER where MM_Status='A' ");
                MLD_Machine_Tonage.DataSource = dt;
                MLD_Machine_Tonage.DisplayMember = "MM_MachineName";
                MLD_Machine_Tonage.ValueMember = "MM_ID";
                MLD_Machine_Tonage.SelectedIndex = -1;
            }
            catch
            {
            }
        }

        public void LoadSupplier()
        {
            
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_LoadSupplier");
                    MLD_MoldSupplier.DataSource = dt;
                    MLD_MoldSupplier.DisplayMember = "SM_Name";
                    MLD_MoldSupplier.ValueMember = "SM_ID";
                    MLD_MoldSupplier.SelectedIndex = -1;
                }
                catch
                {
                }
        }

        public void LoadModel()
        {
           
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_LoadModel");
                    MLD_Model.DataSource = dt;
                    MLD_Model.DisplayMember = "ML_Model";
                    MLD_Model.ValueMember = "ML_ID";
                    MLD_Model.SelectedIndex = -1;
                }
                catch
                {
                }
        
        }

        //public void LoadPartNo()
        //{
        //    try
        //    {
        //        DataTable dt = dbFunctions.getTable("pr_GetPartNumber");
        //        MLD_PartNo.DataSource = dt;
        //        MLD_PartNo.DisplayMember = "IM_PartNo";
        //        MLD_PartNo.ValueMember = "IM_ID";
        //        MLD_PartNo.SelectedIndex = -1;
        //        isPartNoLoad = true;
        //    }
        //    catch
        //    {
        //    }
        //}

        public void LoadCustomer()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_GetCustomer");
                MLD_Customer.DataSource = dt;
                MLD_Customer.DisplayMember = "CM_Name";
                MLD_Customer.ValueMember = "CM_ID";
                MLD_Customer.SelectedIndex = -1;
                //isSupplierLoad = true;
            }
            catch
            {
            }
        }



        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
            Radio_Active.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //DataTable dt = dbFunctions.getTable("select ML_ID, ML_Model from Model_Master where ML_Model='" + MLD_Model.Text + "'");
            //if (dt.Rows.Count <= 0)
            //{
            //    DataTable dtx = dbFunctions.getTable("pr_Insert_Model_Master '" + MLD_Model.Text + "'");
            //    LoadModel();
            //    DataTable dd = dbFunctions.getTable("Select max(ML_ID) from Model_Master ");
            //    MLD_Model.SelectedValue = dd.Rows[0][0].ToString();
            //}
            //else
            //{
            //    MLD_Model.SelectedValue = dt.Rows[0][0].ToString();
            //}

            if(Validate())
            {
                MessageBox.Show(ErrorMessage,"Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            if (btnsave.Text.ToString().Equals("&Update"))
            {
                Update();
            }
            else
            {
                insert();
                
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;

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
                com.CommandText = "pr_Insert_Mold_Master";


                com.Parameters.Add("@MLD_Customer", SqlDbType.Int).Value = MLD_Customer.SelectedValue.ToString();
                com.Parameters.Add("@MLD_PartNo", SqlDbType.VarChar).Value = MLD_PartNo.Text.ToString();
                com.Parameters.Add("@MLD_PartName", SqlDbType.VarChar).Value = MLD_PartName.Text.ToString();
                com.Parameters.Add("@MLD_Model", SqlDbType.Int).Value = MLD_Model.SelectedValue.ToString();
                com.Parameters.Add("@MLD_Mold", SqlDbType.VarChar).Value = MLD_Mold.Text.ToString();
                com.Parameters.Add("@MLD_MouldNo", SqlDbType.VarChar).Value = MLD_MouldNo.Text.ToString();
                com.Parameters.Add("@MLD_MoldCavity", SqlDbType.VarChar).Value = MLD_MoldCavity.Text.ToString();
                com.Parameters.Add("@MLD_MoldSupplier", SqlDbType.VarChar).Value = MLD_MoldSupplier.Text.ToString();
                com.Parameters.Add("@MLD_MoludReceived", SqlDbType.DateTime).Value = MLD_MoludReceived.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MLD_ISAR_Approval", SqlDbType.DateTime).Value = MLD_ISAR_Approval.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MLD_PPAP_Approval", SqlDbType.DateTime).Value = MLD_PPAP_Approval.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MLD_SOPStart", SqlDbType.DateTime).Value = MLD_SOPStart.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MLD_LastPM", SqlDbType.DateTime).Value = MLD_LastPM.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MLD_MouldLife", SqlDbType.VarChar).Value = MLD_MouldLife.Text.ToString();
                com.Parameters.Add("@MLD_OpeningShots", SqlDbType.VarChar).Value = MLD_OpeningShots.Text.ToString();
                com.Parameters.Add("@MLD_MouldCost", SqlDbType.Decimal).Value = MLD_MouldCost.Text.ToString();
                com.Parameters.Add("@MLD_CycleTime", SqlDbType.Int).Value =  MLD_Cycle_Time.Text.ToString();
                com.Parameters.Add("@MLD_hrc_Zone", SqlDbType.VarChar).Value = MLD_nozone.Text.ToString();

                com.Parameters.Add("@MLD_Ejector_Type", SqlDbType.VarChar).Value = MLD_Ejector_Type.Text.ToString();
                com.Parameters.Add("@MLD_Mould_weight", SqlDbType.VarChar).Value = MLD_Mould_weight.Text.ToString();
                com.Parameters.Add("@MLD_Mould_Size", SqlDbType.VarChar).Value = MLD_Mould_Size.Text.ToString();
                com.Parameters.Add("@MLD_Short_Weight", SqlDbType.VarChar).Value = "";//MLD_Short_Weight.Text.ToString();
                com.Parameters.Add("@MLD_Part_Material", SqlDbType.VarChar).Value = MLD_Part_Material.Text.ToString();
                com.Parameters.Add("@MLD_Manufacturer", SqlDbType.VarChar).Value = MLD_Manufacturer.Text.ToString();
                com.Parameters.Add("@MLD_Machine_Tonage", SqlDbType.VarChar).Value = MLD_Machine_Tonage.Text.ToString();
                com.Parameters.Add("@MLD_MoldType", SqlDbType.VarChar).Value = MLD_MoldType.Text.ToString();
                com.Parameters.Add("@MLD_Core_Cavity_Material", SqlDbType.VarChar).Value = "";//MLD_Core_Cavity_Material.Text.ToString();
                com.Parameters.Add("@MLD_isCretical_Mould", SqlDbType.VarChar).Value = "";//MLD_isCretical_Mould.Text.ToString();
                com.Parameters.Add("@MLD_itc", SqlDbType.VarChar).Value = mld_itc.Text.ToString();
                com.Parameters.Add("@MLD_Approvaldatetype", SqlDbType.VarChar).Value = comboBox1.Text.ToString();
                com.Parameters.Add("@MLD_Frequency", SqlDbType.Int).Value = Convert.ToInt32(MLD_Frequency.Text);


                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                display();
               // Ty_TypeName.Focus();
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
                com.CommandText = "pr_update_Mold_Master";

                com.Parameters.Add("@MLD_ID", SqlDbType.Int).Value = ID;
                com.Parameters.Add("@MLD_Customer", SqlDbType.Int).Value = MLD_Customer.SelectedValue.ToString();
                com.Parameters.Add("@MLD_PartNo", SqlDbType.VarChar).Value = MLD_PartNo.Text.ToString();
                com.Parameters.Add("@MLD_PartName", SqlDbType.VarChar).Value = MLD_PartName.Text.ToString();
                com.Parameters.Add("@MLD_Model", SqlDbType.Int).Value = MLD_Model.SelectedValue.ToString();
                com.Parameters.Add("@MLD_Mold", SqlDbType.VarChar).Value = MLD_Mold.Text.ToString();
                com.Parameters.Add("@MLD_MouldNo", SqlDbType.VarChar).Value = MLD_MouldNo.Text.ToString();
                com.Parameters.Add("@MLD_MoldCavity", SqlDbType.VarChar).Value = MLD_MoldCavity.Text.ToString();
                com.Parameters.Add("@MLD_MoldSupplier", SqlDbType.VarChar).Value = MLD_MoldSupplier.Text.ToString();
                com.Parameters.Add("@MLD_MoludReceived", SqlDbType.DateTime).Value = MLD_MoludReceived.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MLD_ISAR_Approval", SqlDbType.DateTime).Value = MLD_ISAR_Approval.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MLD_PPAP_Approval", SqlDbType.DateTime).Value = MLD_PPAP_Approval.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MLD_SOPStart", SqlDbType.DateTime).Value = MLD_SOPStart.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MLD_LastPM", SqlDbType.DateTime).Value = MLD_LastPM.Value.ToString("dd-MMM-yyyy");
                com.Parameters.Add("@MLD_MouldLife", SqlDbType.VarChar).Value = MLD_MouldLife.Text.ToString();
                com.Parameters.Add("@MLD_OpeningShots", SqlDbType.VarChar).Value = MLD_OpeningShots.Text.ToString();
                com.Parameters.Add("@MLD_MouldCost", SqlDbType.Decimal).Value = MLD_MouldCost.Text.ToString();
                com.Parameters.Add("@MLD_CycleTime", SqlDbType.Int).Value = MLD_Cycle_Time.Text.ToString();
                com.Parameters.Add("@MLD_hrc_Zone", SqlDbType.VarChar).Value = MLD_nozone.Text.ToString();

                com.Parameters.Add("@MLD_Ejector_Type", SqlDbType.VarChar).Value = MLD_Ejector_Type.Text.ToString();
                com.Parameters.Add("@MLD_Mould_weight", SqlDbType.VarChar).Value = MLD_Mould_weight.Text.ToString();
                com.Parameters.Add("@MLD_Mould_Size", SqlDbType.VarChar).Value = MLD_Mould_Size.Text.ToString();
                com.Parameters.Add("@MLD_Short_Weight", SqlDbType.VarChar).Value = "";//MLD_Short_Weight.Text.ToString();
                com.Parameters.Add("@MLD_Part_Material", SqlDbType.VarChar).Value = MLD_Part_Material.Text.ToString();
                com.Parameters.Add("@MLD_Manufacturer", SqlDbType.VarChar).Value = MLD_Manufacturer.Text.ToString();
                com.Parameters.Add("@MLD_Machine_Tonage", SqlDbType.VarChar).Value =  MLD_Machine_Tonage.Text.ToString();
                com.Parameters.Add("@MLD_MoldType", SqlDbType.VarChar).Value = MLD_MoldType.Text.ToString();
                com.Parameters.Add("@MLD_Core_Cavity_Material", SqlDbType.VarChar).Value = "";// MLD_Core_Cavity_Material.Text.ToString();
                com.Parameters.Add("@MLD_isCretical_Mould", SqlDbType.VarChar).Value = ""; //MLD_isCretical_Mould.Text.ToString();
                com.Parameters.Add("@MLD_itc", SqlDbType.VarChar).Value = mld_itc.Text.ToString();
                com.Parameters.Add("@MLD_Frequency", SqlDbType.Int).Value = Convert.ToInt32(MLD_Frequency.Text);
                com.Parameters.Add("@MLD_Approvaldatetype", SqlDbType.VarChar).Value = comboBox1.Text.ToString();



                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Updated Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                display();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Edit()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("pr_Edit_Mold_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                ID = dt.Rows[0]["MLD_ID"].ToString();
                MLD_Customer.SelectedValue = dt.Rows[0]["MLD_Customer"].ToString();
                MLD_PartNo.Text = dt.Rows[0]["MLD_PartNo"].ToString();
                MLD_PartName.Text = dt.Rows[0]["MLD_PartName"].ToString(); try
                {
                    MLD_Model.SelectedValue = dt.Rows[0]["MLD_Model"].ToString();
                }
                catch
                {
                    MLD_Model.Text = dt.Rows[0]["MLD_Model"].ToString();
                }
                MLD_Mold.Text = dt.Rows[0]["MLD_Mold"].ToString();
                MLD_MouldNo.Text = dt.Rows[0]["MLD_MouldNo"].ToString();
                MLD_MoldCavity.Text = dt.Rows[0]["MLD_MoldCavity"].ToString();
                MLD_MoldSupplier.Text = dt.Rows[0]["MLD_MoldSupplier"].ToString();
                MLD_MoludReceived.Text = dt.Rows[0]["MLD_MoludReceived"].ToString();
                MLD_ISAR_Approval.Text = dt.Rows[0]["MLD_ISAR_Approval"].ToString();
                MLD_PPAP_Approval.Text = dt.Rows[0]["MLD_PPAP_Approval"].ToString();
                MLD_SOPStart.Text = dt.Rows[0]["MLD_SOPStart"].ToString();
                MLD_LastPM.Text = dt.Rows[0]["MLD_LastPM"].ToString();
                MLD_MouldLife.Text = dt.Rows[0]["MLD_MouldLife"].ToString();
                MLD_OpeningShots.Text = dt.Rows[0]["MLD_OpeningShots"].ToString();
                MLD_MouldCost.Text = dt.Rows[0]["MLD_MouldCost"].ToString();
                MLD_Cycle_Time.Text = dt.Rows[0]["MLD_Cycle_Time"].ToString();
                comboBox1.Text = dt.Rows[0]["MLD_Approvaldatetype"].ToString();

                MLD_Ejector_Type.Text = dt.Rows[0]["MLD_Ejector_Type"].ToString();
                MLD_Mould_weight.Text = dt.Rows[0]["MLD_Mould_weight"].ToString();
                MLD_Mould_Size.Text = dt.Rows[0]["MLD_Mould_Size"].ToString();
                //MLD_Short_Weight.Text = dt.Rows[0]["MLD_Short_Weight"].ToString();
                MLD_Part_Material.Text = dt.Rows[0]["MLD_Part_Material"].ToString();
                MLD_Manufacturer.Text = dt.Rows[0]["MLD_Manufacturer"].ToString();
                MLD_Machine_Tonage.Text = dt.Rows[0]["MLD_Machine_Tonage"].ToString();
                MLD_MoldType.Text = dt.Rows[0]["MLD_MoldType"].ToString();
                mld_itc.Text = dt.Rows[0]["MLD_itc"].ToString();
                MLD_Frequency.Text = dt.Rows[0]["MLD_Frequency"].ToString();
                MLD_nozone.Text = dt.Rows[0]["MLD_hrc_Zone"].ToString();
                //MLD_Core_Cavity_Material.Text = dt.Rows[0]["MLD_Core_Cavity_Material"].ToString();
                //MLD_isCretical_Mould.Text = dt.Rows[0]["MLD_isCretical_Mould"].ToString();

                btnsave.Text = "&Update";
                MLD_Mold.Focus();
               
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Delete()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("pr_Delete_Mold_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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


        public void display()
        {
            DataTable dt = dbFunctions.getTable("pr_Display_Mold_Master");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }


        public void Clear()
        {
            MLD_OpeningShots.Text = "0";
            MLD_Mold.Text = "";
            MLD_Customer.SelectedIndex = -1;
            MLD_PartNo.Text = "";
            MLD_PartName.Text = "";
            MLD_Model.SelectedIndex = -1;
            MLD_MouldNo.Text = "";
            MLD_MoldCavity.Text = "";
            MLD_MoldSupplier.Text = "";

            MLD_MouldLife.Text = "0";
            MLD_MouldCost.Text = "0";
            MLD_Cycle_Time.Text = "";

            MLD_Ejector_Type.Text = "";
            MLD_Mould_weight.Text = "";
            MLD_Mould_Size.Text = "";
            MLD_Short_Weight.Text = "";
            MLD_Part_Material.Text = "";
            MLD_Manufacturer.Text = "";
            MLD_Machine_Tonage.Text = "";
            MLD_MoldType.Text = "";
            MLD_Core_Cavity_Material.Text = "";
            MLD_isCretical_Mould.Text = "";
            // mld_itc.Text = "";
            comboBox1.Text = "";
            MLD_MoludReceived.Text = "";
            MLD_ISAR_Approval.Text = "";
            MLD_PPAP_Approval.Text = "";
            MLD_SOPStart.Text = "";
           // MLD_Frequency.Text = "";

            btnsave.Text = "&Save   ";
            MLD_Mold.Focus();
          
        }


        public bool Validate()
        {
            //if ((string.IsNullOrEmpty(MLD_Mold.Text.Trim())))
            //{
            //    ErrorMessage = "Mould Name Should Not be Empty";
            //    MLD_Mold.Focus();
            //    return true;
            //} 
            if ((string.IsNullOrEmpty(MLD_Customer.Text.Trim())))
            {
                ErrorMessage = "Customer Should Not be Empty";
                MLD_Customer.Focus();
                return true;
            }
            try
            {
                int x = int.Parse(MLD_Customer.SelectedValue.ToString());
            }
            catch
            {
                ErrorMessage = "Select Proper Customer";
                MLD_Customer.Focus();
                return true;
            }


            if ((string.IsNullOrEmpty(MLD_PartNo.Text.Trim())))
            {
                ErrorMessage = "PartNo Should Not be Empty";
                MLD_PartNo.Focus();
                return true;
            }
            //try
            //{
            //    int x = int.Parse(MLD_PartNo.SelectedValue.ToString());
            //}
            //catch
            //{
            //    ErrorMessage = "Select Proper Part No";
            //    MLD_PartNo.Focus();
            //    return true;
            //}


            if ((string.IsNullOrEmpty(MLD_MoldSupplier.Text.Trim())))
            {
                ErrorMessage = "Suplier Should Not be Empty";
                MLD_MoldSupplier.Focus();
                return true;
            }
            //try
            //{
            //    int x = int.Parse(MLD_MoldSupplier.SelectedValue.ToString());
            //}
            //catch
            //{
            //    ErrorMessage = "Select Proper Supplier";
            //    MLD_MoldSupplier.Focus();
            //    return true;
            //}






            if ((string.IsNullOrEmpty(MLD_Model.Text.Trim())))
            {
                ErrorMessage = "Model Should Not be Empty";
                MLD_Model.Focus();
                return true;
            }
            return false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_ItemType_Master_DeAct");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void Radio_Active_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void Ty_TypeName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                btnsave.Focus();
            }
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Part No] LIKE '%{0}%' or [Part Name] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Ty_TypeName_Enter(object sender, EventArgs e)
        {
            txtStatus.Text = "Please Enter Your Item Type";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {

                this.Close();

            }
        }

        private void Item_Type_KeyDown(object sender, KeyEventArgs e)
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
        private void MLD_PartNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isPartNoLoad)
            {
                try
                {

                    DataTable dt = dbFunctions.getTable("pr_GetPartNameByNo '" + MLD_PartNo.Text.ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        MLD_PartName.Text = dt.Rows[0]["IM_PartName"].ToString();
                    }
                }
                catch
                {
                }
            }
        }

        private void MLD_MouldCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void MLD_OpeningShots_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void MLD_MouldLife_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void MLD_MoldCavity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "PRAP")
            {
                label11.Text = "PRAP Approval Date :";
            }
            else
            {
                label11.Text = "ISIR Approval Date :";
            }
        }
    }
}