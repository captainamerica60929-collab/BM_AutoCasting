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

    public partial class ItemMaster : Form
    {
        public string arrow = "Up";
        public int Distance = 281;
        public string ID = "";
        string ErrorMessage = "";
        public bool isMoldLoadFG = false;
        public bool isMoldLoadAssy = false;
        public ItemMaster()
        {
            InitializeComponent();
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
        }




        private void ItemMaster_Shown(object sender, EventArgs e)
        {
            //display();
            //Pending();
            Approved();
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
            
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            Radio_Active.Checked = true;
            //rbtnPending.Checked = true;
           // display();  
            Pending();
            IM_Plant.Focus();
            LoadPlant();        LoadModel();         LoadItemType();
            LoadUOM();          LoadCurrency();      LoadSupplier();
            LoadBO_UOM();       LoadBO_Currency();
            LoadRM_Supplier();    LoadRM_UOM();         LoadRM_Currency();
            LoadMold(); LoadMoldAssy();
            LoadMachineNo(); LoadMachineAssy();



            try
            {
                DataTable dt = dbFunctions.getTable("pr_GetMoldNumber1");
                IM_MouldNumer.DataSource = dt;
                IM_MouldNumer.DisplayMember = "MLD_MouldNo";
                IM_MouldNumer.ValueMember = "MLD_ID";
                IM_MouldNumer.SelectedIndex = -1;

                IM_MouldNumer_Assy.DataSource = dt;
                IM_MouldNumer_Assy.DisplayMember = "MLD_MouldNo";
                IM_MouldNumer_Assy.ValueMember = "MLD_ID";
                IM_MouldNumer_Assy.SelectedIndex = -1;

            }
            catch { }

        }

        public void LoadMachineAssy()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadMachine");
                IM_MachineNoAssy.DataSource = dt;
                IM_MachineNoAssy.DisplayMember = "MM_MachineName";
                IM_MachineNoAssy.ValueMember = "MM_ID";
                IM_MachineNoAssy.SelectedIndex = -1;
                //isMoldLoadAssy = true;
            }
            catch
            {
            }
        }

        public void LoadMachineNo()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadMachine");
                IM_MachineNo.DataSource = dt;
                IM_MachineNo.DisplayMember = "MM_MachineName";
                IM_MachineNo.ValueMember = "MM_ID";
                IM_MachineNo.SelectedIndex = -1;
                //isMoldLoadAssy = true;
            }
            catch
            {
            }
        }

          public void LoadMoldAssy()
          {
 	
              try
                {
                     DataTable dt = dbFunctions.getTable("pr_LoadMold");
                     IM_Mould_Assy.DataSource = dt;
                     IM_Mould_Assy.DisplayMember = "MLD_Mold";
                     IM_Mould_Assy.ValueMember = "MLD_Mold";
                     IM_Mould_Assy.SelectedIndex = -1;
                     isMoldLoadAssy = true;
                }
              catch
              {
              }
        }
        
        public void LoadMold()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadMold");
                IM_Mould.DataSource = dt;
                IM_Mould.DisplayMember = "MLD_Mold";
                IM_Mould.ValueMember = "MLD_Mold";
                IM_Mould.SelectedIndex = -1;
                isMoldLoadFG = true;
            }
            catch
            {
            }
        }

        public void LoadRM_Currency()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadCurrency");
                IM_Currency_RM.DataSource = dt;
                IM_Currency_RM.DisplayMember = "CM_Currency";
                IM_Currency_RM.ValueMember = "CM_iid";
                IM_Currency_RM.SelectedIndex =0;
            }
            catch
            {
            }
        }
        public void LoadRM_UOM()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadUOM");
                IM_UOM_RM.DataSource = dt;
                IM_UOM_RM.DisplayMember = "UM_UOM";
                IM_UOM_RM.ValueMember = "UM_ID";
                IM_UOM_RM.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        public void LoadRM_Supplier()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadSupplier");
                IM_Supplier_RM.DataSource = dt;
                IM_Supplier_RM.DisplayMember = "SM_Name";
                IM_Supplier_RM.ValueMember = "SM_ID";
                IM_Supplier_RM.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void LoadBO_Currency()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadCurrency");
                IM_Currency_BO.DataSource = dt;
                IM_Currency_BO.DisplayMember = "CM_Currency";
                IM_Currency_BO.ValueMember = "CM_iid";
                IM_Currency_BO.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        public void LoadBO_UOM()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadUOM_Nos");
                IM_UOM_BO.DataSource = dt;
                IM_UOM_BO.DisplayMember = "UM_UOM";
                IM_UOM_BO.ValueMember = "UM_ID";
                IM_UOM_BO.SelectedIndex = 0;
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
                IM_Supplier.DataSource = dt;
                IM_Supplier.DisplayMember = "SM_Name";
                IM_Supplier.ValueMember = "SM_ID";
                IM_Supplier.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void LoadCurrency()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadCurrency");
                IM_Currency.DataSource = dt;
                IM_Currency.DisplayMember = "CM_Currency";
                IM_Currency.ValueMember = "CM_iid";
                IM_Currency.SelectedIndex =0;
            }
            catch
            {
            }
        }
        public void LoadUOM()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadUOM_Nos");
                IM_UOM.DataSource = dt;
                IM_UOM.DisplayMember = "UM_UOM";
                IM_UOM.ValueMember = "UM_ID";
                IM_UOM.SelectedIndex = 0;
            }
            catch
            {
            }
        }       
        public void LoadItemType()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadItemType");
                IM_Type.DataSource = dt;
                IM_Type.DisplayMember = "Ty_TypeName";
                IM_Type.ValueMember = "Ty_ID";
                IM_Type.SelectedIndex =0;
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
                IM_Model.DataSource = dt;
                IM_Model.DisplayMember = "ML_Model";
                IM_Model.ValueMember = "ML_ID";
                IM_Model.SelectedIndex = -1;
            }
            catch
            {
            }
        }


        public void LoadPlant()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadPlant");
                IM_Plant.DataSource = dt;
                IM_Plant.DisplayMember = "PL_Name";
                IM_Plant.ValueMember = "PL_ID";
                IM_Plant.SelectedIndex = -1;
            }
            catch
            {
            }

        }
        public void LoadSuplier()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_Supplier");
                IM_Plant.DataSource = dt;
                IM_Plant.DisplayMember = "PL_Name";
                IM_Plant.ValueMember = "PL_ID";
                IM_Plant.SelectedIndex = -1;
            }
            catch
            {
            }

        }



        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Radio_Active.Checked = true;
            Approved();
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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
                IM_Plant.Focus();
            }

            Radio_Pending.Checked = true;
            Pending();
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


            if(IM_Type.Text=="FG")
            {
                if(tabPage1.Text=="FG")
                {
                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "pr_Insert_Item_Master_FG";

                        com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                        com.Parameters.Add("@IM_Plant", SqlDbType.Int).Value = IM_Plant.SelectedValue.ToString();
                        com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo.Text.ToString();
                        com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName.Text.ToString();
                        com.Parameters.Add("@IM_Model", SqlDbType.Int).Value = IM_Model.SelectedValue.ToString();
                        com.Parameters.Add("@IM_In_House_Or_Out_Source", SqlDbType.VarChar).Value = IM_In_House_Or_Out_Source.Text.ToString();
                        com.Parameters.Add("@IM_Usage", SqlDbType.VarChar).Value = IM_Usage.Text.ToString();
                        com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = IM_UOM.SelectedValue.ToString();
                        com.Parameters.Add("@IM_Sales_Price", SqlDbType.VarChar).Value = IM_Sales_Price.Text.ToString();
                        com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = IM_Currency.SelectedValue.ToString();
                        com.Parameters.Add("@IM_Mould", SqlDbType.VarChar).Value = IM_MouldNumer.SelectedValue.ToString();
                        com.Parameters.Add("@IM_MouldNumer", SqlDbType.VarChar).Value = IM_MouldNumer.Text.ToString();
                        com.Parameters.Add("@IM_MachineNo", SqlDbType.Int).Value = IM_MachineNo.SelectedValue.ToString();

                        com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCode.Text.ToString();
                        com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStock.Text.ToString();
                        com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStock.Text.ToString();

                        com.ExecuteNonQuery();
                        com.Connection.Close();
                        MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                       
                        IM_Plant.Focus();
                    }
                    catch (Exception Ex)
                    {
                        dbFunctions.Logs(Ex.Message, dbFunctions.username);
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            else if (IM_Type.Text == "B/O")
            {
                if (tabPage2.Text == "B/O")
                {
                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "pr_Insert_Item_Master_BO";

                        com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                        com.Parameters.Add("@IM_Supplier", SqlDbType.Int).Value = IM_Supplier.SelectedValue.ToString();
                        com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo_BO.Text.ToString();
                        com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName_BO.Text.ToString();
                        com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard_BO.Text.ToString();
                        com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = IM_RMSource.Text.ToString();
                        com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = IM_Dealer.Text.ToString();
                        com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = IM_UOM_BO.SelectedValue.ToString();
                        com.Parameters.Add("@IM_Purchase_Price", SqlDbType.VarChar).Value = IM_Purchase_Price.Text.ToString();
                        com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = IM_Currency_BO.SelectedValue.ToString();


                        com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCodeBO.Text.ToString();
                        com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStockBO.Text.ToString();
                        com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStockBO.Text.ToString();
                        
                        com.ExecuteNonQuery();
                        com.Connection.Close();
                        MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                      
                        IM_Plant.Focus();
                    }
                    catch (Exception Ex)
                    {
                        dbFunctions.Logs(Ex.Message, dbFunctions.username);
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            else if (IM_Type.Text == "RM")
            {
                if (tabPage3.Text == "RM")
                {
                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "pr_Insert_Item_Master_RM";

                        com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                        com.Parameters.Add("@IM_Supplier", SqlDbType.Int).Value = IM_Supplier_RM.SelectedValue.ToString();
                        com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo_RM.Text.ToString();
                        com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName_RM.Text.ToString();
                        com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard_RM.Text.ToString();
                        com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = IM_RMSource_RM.Text.ToString();
                        com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = IM_Dealer_RM.Text.ToString();
                        com.Parameters.Add("@IM_Grade", SqlDbType.VarChar).Value = IM_Grade_RM.Text.ToString();
                        com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = IM_UOM_RM.SelectedValue.ToString();
                        com.Parameters.Add("@IM_Purchase_Price", SqlDbType.VarChar).Value = IM_Purchase_Price_RM.Text.ToString();
                        com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = IM_Currency_RM.SelectedValue.ToString();
                        com.Parameters.Add("@IM_PackingStandard", SqlDbType.VarChar).Value = IM_PackingStandard.Text.ToString();

                        com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCodeRM.Text.ToString();
                        com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStockRM.Text.ToString();
                        com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStockRM.Text.ToString();

                       
                        com.ExecuteNonQuery();
                        com.Connection.Close();
                        MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                       
                        IM_Plant.Focus();
                    }
                    catch (Exception Ex)
                    {
                        dbFunctions.Logs(Ex.Message, dbFunctions.username);
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (IM_Type.Text == "Assembly")
            {
                if (tabPage5.Text == "Assembly")
                {
                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "pr_Insert_Item_Master_Assy";

                        com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                        com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo_Assy.Text.ToString();
                        com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName_Assy.Text.ToString();
                        com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard_Assy.Text.ToString();
                        com.Parameters.Add("@IM_Sales_Price", SqlDbType.Decimal).Value = IM_Sales_Price_Assy.Text.ToString();
                        com.Parameters.Add("@IM_Mould", SqlDbType.VarChar).Value = IM_MouldNumer_Assy.SelectedValue.ToString();
                        com.Parameters.Add("@IM_MouldNumer", SqlDbType.VarChar).Value = IM_MouldNumer_Assy.Text.ToString();
                        com.Parameters.Add("@IM_MachineNo", SqlDbType.Int).Value = IM_MachineNoAssy.SelectedValue.ToString();
                        com.Parameters.Add("@IM_PackingStandard", SqlDbType.VarChar).Value = IM_PackingStandard.Text.ToString();

                        com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCodeAssy.Text.ToString();
                        com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStockAssy.Text.ToString();
                        com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStockAssy.Text.ToString();


                        com.ExecuteNonQuery();
                        com.Connection.Close();
                        MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                      
                       
                        IM_Plant.Focus();
                    }
                    catch (Exception Ex)
                    {
                        dbFunctions.Logs(Ex.Message, dbFunctions.username);
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
              else  
            {
                if (tabPage4.Text == "Others")
                {
                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "pr_Insert_Item_Master_Others";

                        com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();

                        com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo_Oth.Text.ToString();
                        com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName_Oth.Text.ToString();
                        com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard_Oth.Text.ToString();
                        com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = IM_RMSource_Oth.Text.ToString();
                        com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = IM_Dealer_Oth.Text.ToString();

                        com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCodeOthr.Text.ToString();
                        com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStockOthr.Text.ToString();
                        com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStockOthr.Text.ToString();
                       

                        com.ExecuteNonQuery();
                        com.Connection.Close();
                        MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                       
                       
                        IM_Plant.Focus();
                    }
                    catch (Exception Ex)
                    {
                        dbFunctions.Logs(Ex.Message, dbFunctions.username);
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
           
        }
        public void Update()
        {

            //if (Radio_Pending.Checked == true)
            {
                if (IM_Type.Text == "FG")
                {
                    if (tabPage1.Text == "FG")
                    {
                        SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                        try
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand();
                            com.Connection = con;
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "pr_Update_Item_Master_FG";

                            com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = ID;
                            com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                            com.Parameters.Add("@IM_Plant", SqlDbType.Int).Value = IM_Plant.SelectedValue.ToString();
                            com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo.Text.ToString();
                            com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName.Text.ToString();
                            com.Parameters.Add("@IM_Model", SqlDbType.Int).Value = IM_Model.SelectedValue.ToString();
                            com.Parameters.Add("@IM_In_House_Or_Out_Source", SqlDbType.VarChar).Value = IM_In_House_Or_Out_Source.Text.ToString();
                            com.Parameters.Add("@IM_Usage", SqlDbType.VarChar).Value = IM_Usage.Text.ToString();
                            com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = IM_UOM.SelectedValue.ToString();
                            com.Parameters.Add("@IM_Sales_Price", SqlDbType.VarChar).Value = IM_Sales_Price.Text.ToString();
                            com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = IM_Currency.SelectedValue.ToString();
                            com.Parameters.Add("@IM_Mould", SqlDbType.VarChar).Value = IM_MouldNumer.SelectedValue.ToString();
                            com.Parameters.Add("@IM_MouldNumer", SqlDbType.VarChar).Value = IM_MouldNumer.Text.ToString();
                            com.Parameters.Add("@IM_MachineNo", SqlDbType.Int).Value = IM_MachineNo.SelectedValue.ToString();


                            com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCode.Text.ToString();
                            com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStock.Text.ToString();
                            com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStock.Text.ToString();

                            com.ExecuteNonQuery();
                            com.Connection.Close();
                            MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();
                            display();
                            IM_Plant.Focus();
                            tabControl1.SelectedIndex = 1;
                        }
                        catch (Exception Ex)
                        {
                            dbFunctions.Logs(Ex.Message, dbFunctions.username);
                            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                else if (IM_Type.Text == "B/O")
                {
                    if (tabPage2.Text == "B/O")
                    {
                        SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                        try
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand();
                            com.Connection = con;
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "pr_Update_Item_Master_BO";

                            com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = ID;
                            com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                            com.Parameters.Add("@IM_Supplier", SqlDbType.Int).Value = IM_Supplier.SelectedValue.ToString();
                            com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo_BO.Text.ToString();
                            com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName_BO.Text.ToString();
                            com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard_BO.Text.ToString();
                            com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = IM_RMSource.Text.ToString();
                            com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = IM_Dealer.Text.ToString();
                            com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = IM_UOM_BO.SelectedValue.ToString();
                            com.Parameters.Add("@IM_Purchase_Price", SqlDbType.VarChar).Value = IM_Purchase_Price.Text.ToString();
                            com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = IM_Currency_BO.SelectedValue.ToString();

                            com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCodeBO.Text.ToString();
                            com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStockBO.Text.ToString();
                            com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStockBO.Text.ToString();



                            com.ExecuteNonQuery();
                            com.Connection.Close();
                            MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();
                            display();
                            IM_Plant.Focus();
                        }
                        catch (Exception Ex)
                        {
                            dbFunctions.Logs(Ex.Message, dbFunctions.username);
                            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                else if (IM_Type.Text == "RM")
                {
                    if (tabPage3.Text == "RM")
                    {
                        SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                        try
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand();
                            com.Connection = con;
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "pr_Update_Item_Master_RM";

                            com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = ID;
                            com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                            com.Parameters.Add("@IM_Supplier", SqlDbType.Int).Value = IM_Supplier_RM.SelectedValue.ToString();
                            com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo_RM.Text.ToString();
                            com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName_RM.Text.ToString();
                            com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard_RM.Text.ToString();
                            com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = IM_RMSource_RM.Text.ToString();
                            com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = IM_Dealer_RM.Text.ToString();
                            com.Parameters.Add("@IM_Grade", SqlDbType.VarChar).Value = IM_Grade_RM.Text.ToString();
                            com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = IM_UOM_RM.SelectedValue.ToString();
                            com.Parameters.Add("@IM_Purchase_Price", SqlDbType.VarChar).Value = IM_Purchase_Price_RM.Text.ToString();
                            com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = IM_Currency_RM.SelectedValue.ToString();
                            com.Parameters.Add("@IM_PackingStandard", SqlDbType.VarChar).Value = IM_PackingStandard.Text.ToString();

                            com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCodeRM.Text.ToString();
                            com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStockRM.Text.ToString();
                            com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStockRM.Text.ToString();


                            com.ExecuteNonQuery();
                            com.Connection.Close();
                            MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();
                            display();
                            IM_Plant.Focus();
                        }
                        catch (Exception Ex)
                        {
                            dbFunctions.Logs(Ex.Message, dbFunctions.username);
                            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (IM_Type.Text == "Assembly")
                {
                    if (tabPage5.Text == "Assembly")
                    {
                        SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                        try
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand();
                            com.Connection = con;
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "pr_Update_Item_Master_Assy";

                            com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = ID;
                            com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                            com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo_Assy.Text.ToString();
                            com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName_Assy.Text.ToString();
                            com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard_Assy.Text.ToString();
                            com.Parameters.Add("@IM_Sales_Price", SqlDbType.VarChar).Value = IM_Sales_Price_Assy.Text.ToString();
                            com.Parameters.Add("@IM_Mould", SqlDbType.VarChar).Value = IM_MouldNumer_Assy.SelectedValue.ToString();
                            com.Parameters.Add("@IM_MouldNumer", SqlDbType.VarChar).Value = IM_MouldNumer_Assy.Text.ToString();
                            com.Parameters.Add("@IM_MachineNo", SqlDbType.Int).Value = IM_MachineNoAssy.SelectedValue.ToString();
                            com.Parameters.Add("@IM_PackingStandard", SqlDbType.VarChar).Value = IM_PackingStandardAssy.Text.ToString();

                            com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCodeAssy.Text.ToString();
                            com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStockAssy.Text.ToString();
                            com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStockAssy.Text.ToString();


                            com.ExecuteNonQuery();
                            com.Connection.Close();
                            MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();
                            display();
                            IM_Plant.Focus();
                        }
                        catch (Exception Ex)
                        {
                            dbFunctions.Logs(Ex.Message, dbFunctions.username);
                            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    if (tabPage4.Text == "Others")
                    {
                        SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                        try
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand();
                            com.Connection = con;
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "pr_Update_Item_Master_Others";

                            com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = ID;
                            com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                            com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo_Oth.Text.ToString();
                            com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName_Oth.Text.ToString();
                            com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard_Oth.Text.ToString();
                            com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = IM_RMSource_Oth.Text.ToString();
                            com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = IM_Dealer_Oth.Text.ToString();

                            com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCodeOthr.Text.ToString();
                            com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStockOthr.Text.ToString();
                            com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStockOthr.Text.ToString();


                            com.ExecuteNonQuery();
                            com.Connection.Close();
                            MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();
                            display();
                            IM_Plant.Focus();
                        }
                        catch (Exception Ex)
                        {
                            dbFunctions.Logs(Ex.Message, dbFunctions.username);
                            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                //}
                //else
                //{
                //    if (IM_Type.Text == "FG")
                //    {
                //        if (tabPage1.Text == "FG")
                //        {
                //            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                //            try
                //            {
                //                con.Open();
                //                SqlCommand com = new SqlCommand();
                //                com.Connection = con;
                //                com.CommandType = CommandType.StoredProcedure;
                //                com.CommandText = "pr_InsertDuplicate_Item_Master_FG";

                //                //com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = ID;
                //                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_Plant", SqlDbType.Int).Value = IM_Plant.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo.Text.ToString();
                //                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName.Text.ToString();
                //                com.Parameters.Add("@IM_Model", SqlDbType.Int).Value = IM_Model.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_In_House_Or_Out_Source", SqlDbType.VarChar).Value = IM_In_House_Or_Out_Source.Text.ToString();
                //                com.Parameters.Add("@IM_Usage", SqlDbType.VarChar).Value = IM_Usage.Text.ToString();
                //                com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = IM_UOM.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_Sales_Price", SqlDbType.VarChar).Value = IM_Sales_Price.Text.ToString();
                //                com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = IM_Currency.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_Mould", SqlDbType.VarChar).Value = IM_Mould.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_MouldNumer", SqlDbType.VarChar).Value = IM_MouldNumer.Text.ToString();
                //                com.Parameters.Add("@IM_MachineNo", SqlDbType.Int).Value = IM_MachineNo.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_UpdatedID", SqlDbType.VarChar).Value = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                //                com.Parameters.Add("@IM_OperationType", SqlDbType.VarChar).Value = "Updated";



                //                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCode.Text.ToString();
                //                com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStock.Text.ToString();
                //                com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStock.Text.ToString();

                //                com.ExecuteNonQuery();
                //                com.Connection.Close();
                //                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                Clear();
                //                //display();
                //                Pending();
                //                IM_Plant.Focus();
                //                tabControl1.SelectedIndex = 1;
                //            }
                //            catch (Exception Ex)
                //            {
                //                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                //                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            }
                //        }
                //    }

                //    else if (IM_Type.Text == "B/O")
                //    {
                //        if (tabPage2.Text == "B/O")
                //        {
                //            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                //            try
                //            {
                //                con.Open();
                //                SqlCommand com = new SqlCommand();
                //                com.Connection = con;
                //                com.CommandType = CommandType.StoredProcedure;
                //                com.CommandText = "pr_InsertDuplicate_Item_Master_BO";

                //                //com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = ID;
                //                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_Supplier", SqlDbType.Int).Value = IM_Supplier.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo_BO.Text.ToString();
                //                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName_BO.Text.ToString();
                //                com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard_BO.Text.ToString();
                //                com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = IM_RMSource.Text.ToString();
                //                com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = IM_Dealer.Text.ToString();
                //                com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = IM_UOM_BO.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_Purchase_Price", SqlDbType.VarChar).Value = IM_Purchase_Price.Text.ToString();
                //                com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = IM_Currency_BO.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_UpdatedID", SqlDbType.VarChar).Value = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                //                com.Parameters.Add("@IM_OperationType", SqlDbType.VarChar).Value = "Updated";


                //                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCodeBO.Text.ToString();
                //                com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStockBO.Text.ToString();
                //                com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStockBO.Text.ToString();

                //                com.ExecuteNonQuery();
                //                com.Connection.Close();
                //                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                Clear();
                //                //display();
                //                Pending();
                //                IM_Plant.Focus();
                //            }
                //            catch (Exception Ex)
                //            {
                //                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                //                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            }
                //        }
                //    }

                //    else if (IM_Type.Text == "RM")
                //    {
                //        if (tabPage3.Text == "RM")
                //        {
                //            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                //            try
                //            {
                //                con.Open();
                //                SqlCommand com = new SqlCommand();
                //                com.Connection = con;
                //                com.CommandType = CommandType.StoredProcedure;
                //                com.CommandText = "pr_InsertDuplicate_Item_Master_RM";

                //                //com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = ID;
                //                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_Supplier", SqlDbType.Int).Value = IM_Supplier_RM.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo_RM.Text.ToString();
                //                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName_RM.Text.ToString();
                //                com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard_RM.Text.ToString();
                //                com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = IM_RMSource_RM.Text.ToString();
                //                com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = IM_Dealer_RM.Text.ToString();
                //                com.Parameters.Add("@IM_Grade", SqlDbType.VarChar).Value = IM_Grade_RM.Text.ToString();
                //                com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = IM_UOM_RM.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_Purchase_Price", SqlDbType.VarChar).Value = IM_Purchase_Price_RM.Text.ToString();
                //                com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = IM_Currency_RM.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_UpdatedID", SqlDbType.VarChar).Value = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                //                com.Parameters.Add("@IM_OperationType", SqlDbType.VarChar).Value = "Updated";
                //                com.Parameters.Add("@IM_PackingStandard", SqlDbType.VarChar).Value = IM_PackingStandard.Text.ToString();


                //                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCodeRM.Text.ToString();
                //                com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStockRM.Text.ToString();
                //                com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStockRM.Text.ToString();

                //                com.ExecuteNonQuery();
                //                com.Connection.Close();
                //                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                Clear();
                //                //display();
                //                Pending();
                //                IM_Plant.Focus();
                //            }
                //            catch (Exception Ex)
                //            {
                //                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                //                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            }
                //        }
                //    }
                //    else if (IM_Type.Text == "Assembly")
                //    {
                //        if (tabPage5.Text == "Assembly")
                //        {
                //            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                //            try
                //            {
                //                con.Open();
                //                SqlCommand com = new SqlCommand();
                //                com.Connection = con;
                //                com.CommandType = CommandType.StoredProcedure;
                //                com.CommandText = "pr_InsertDuplicate_Item_Master_Assy";

                //                //com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = ID;
                //                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo_Assy.Text.ToString();
                //                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName_Assy.Text.ToString();
                //                com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard_Assy.Text.ToString();
                //                com.Parameters.Add("@IM_Sales_Price", SqlDbType.VarChar).Value = IM_Sales_Price_Assy.Text.ToString();
                //                com.Parameters.Add("@IM_Mould", SqlDbType.VarChar).Value = IM_Mould_Assy.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_MouldNumer", SqlDbType.VarChar).Value = IM_MouldNumer_Assy.Text.ToString();
                //                com.Parameters.Add("@IM_MachineNo", SqlDbType.Int).Value = IM_MachineNoAssy.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_UpdatedID", SqlDbType.VarChar).Value = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                //                com.Parameters.Add("@IM_OperationType", SqlDbType.VarChar).Value = "Updated";
                //                com.Parameters.Add("@IM_PackingStandard", SqlDbType.VarChar).Value = IM_PackingStandardAssy.Text.ToString();

                //                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCodeAssy.Text.ToString();
                //                com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStockAssy.Text.ToString();
                //                com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStockAssy.Text.ToString();

                //                com.ExecuteNonQuery();
                //                com.Connection.Close();
                //                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                Clear();
                //                //display();
                //                Pending();
                //                IM_Plant.Focus();
                //            }
                //            catch (Exception Ex)
                //            {
                //                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                //                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        if (tabPage4.Text == "Others")
                //        {
                //            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                //            try
                //            {
                //                con.Open();
                //                SqlCommand com = new SqlCommand();
                //                com.Connection = con;
                //                com.CommandType = CommandType.StoredProcedure;
                //                com.CommandText = "pr_InsertDuplicate_Item_Master_Others";

                //                //com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = ID;
                //                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = IM_Type.SelectedValue.ToString();
                //                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = IM_PartNo_Oth.Text.ToString();
                //                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = IM_PartName_Oth.Text.ToString();
                //                com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = IM_Mate_Standard_Oth.Text.ToString();
                //                com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = IM_RMSource_Oth.Text.ToString();
                //                com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = IM_Dealer_Oth.Text.ToString();
                //                com.Parameters.Add("@IM_UpdatedID", SqlDbType.VarChar).Value = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                //                com.Parameters.Add("@IM_OperationType", SqlDbType.VarChar).Value = "Updated";


                //                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = IM_HSNCodeOthr.Text.ToString();
                //                com.Parameters.Add("@IM_MinStock", SqlDbType.VarChar).Value = IM_MinStockOthr.Text.ToString();
                //                com.Parameters.Add("@IM_MaxStock", SqlDbType.VarChar).Value = IM_MaxStockOthr.Text.ToString();


                //                com.ExecuteNonQuery();
                //                com.Connection.Close();
                //                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                Clear();
                //                //display();
                //                Pending();
                //                IM_Plant.Focus();
                //            }
                //            catch (Exception Ex)
                //            {
                //                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                //                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            }
                //        }
                //    }
                //}
                Radio_Active.Checked = true;
            }
        }

        public void Edit()
        {
            if (IM_Type.Text == "FG")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataTable dt = dbFunctions.getTable("pr_Edit_Item_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    ID = dt.Rows[0]["IM_ID"].ToString();
                    
                    IM_Type.SelectedValue = dt.Rows[0]["IM_Type"].ToString();
                    IM_Plant.SelectedValue = dt.Rows[0]["IM_Plant"].ToString();
                    IM_PartNo.Text = dt.Rows[0]["IM_PartNo"].ToString();
                    IM_PartName.Text = dt.Rows[0]["IM_PartName"].ToString();
                    IM_Model.SelectedValue = dt.Rows[0]["IM_Model"].ToString();
                    IM_In_House_Or_Out_Source.Text=dt.Rows[0]["IM_In_House_Or_Out_Source"].ToString();
                    IM_Usage.Text = dt.Rows[0]["IM_Usage"].ToString();
                    IM_UOM.SelectedValue = dt.Rows[0]["IM_UOM"].ToString();
                    IM_Sales_Price.Text = dt.Rows[0]["IM_Sales_Price"].ToString();
                    IM_Currency.SelectedValue = dt.Rows[0]["IM_Currency"].ToString();
                   // IM_Mould.SelectedValue = dt.Rows[0]["IM_Mould"].ToString();
                    IM_MouldNumer.SelectedValue = dt.Rows[0]["IM_Mould"].ToString();
                    IM_MachineNo.SelectedValue = dt.Rows[0]["IM_MachineNo"].ToString();

                    IM_HSNCode.Text = dt.Rows[0]["IM_HSNCode"].ToString();
                    IM_MinStock.Text = dt.Rows[0]["IM_MinStock"].ToString();
                    IM_MaxStock.Text = dt.Rows[0]["IM_MaxStock"].ToString();
                    IM_MaxStock.Text = dt.Rows[0]["IM_GST_Percentage"].ToString();
                    btnsave.Text = "&Update";
                    IM_Plant.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (IM_Type.Text == "B/O")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataTable dt = dbFunctions.getTable("pr_Edit_Item_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    ID = dt.Rows[0]["IM_ID"].ToString();

                    IM_Type.SelectedValue = dt.Rows[0]["IM_Type"].ToString();
                    IM_Supplier.SelectedValue = dt.Rows[0]["IM_Supplier"].ToString();
                    IM_PartNo_BO.Text = dt.Rows[0]["IM_PartNo"].ToString();
                    IM_PartName_BO.Text = dt.Rows[0]["IM_PartName"].ToString();
                    IM_Mate_Standard_BO.Text = dt.Rows[0]["IM_Mate_Standard"].ToString();
                    IM_RMSource.Text = dt.Rows[0]["IM_RMSource"].ToString();
                    IM_Dealer.Text = dt.Rows[0]["IM_Dealer"].ToString();
                    IM_UOM_BO.SelectedValue = dt.Rows[0]["IM_UOM"].ToString();
                    IM_Purchase_Price.Text = dt.Rows[0]["IM_Purchase_Price"].ToString();
                    IM_Currency_BO.SelectedValue = dt.Rows[0]["IM_Currency"].ToString();


                    IM_HSNCodeBO.Text = dt.Rows[0]["IM_HSNCode"].ToString();
                    IM_MinStockBO.Text = dt.Rows[0]["IM_MinStock"].ToString();
                    IM_MaxStockBO.Text = dt.Rows[0]["IM_MaxStock"].ToString();


                    btnsave.Text = "&Update";
                    IM_Supplier.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (IM_Type.Text == "RM")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataTable dt = dbFunctions.getTable("pr_Edit_Item_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    ID = dt.Rows[0]["IM_ID"].ToString();

                    IM_Type.SelectedValue = dt.Rows[0]["IM_Type"].ToString();
                    IM_Supplier_RM.SelectedValue = dt.Rows[0]["IM_Supplier"].ToString();
                    IM_PartNo_RM.Text = dt.Rows[0]["IM_PartNo"].ToString();
                    IM_PartName_RM.Text = dt.Rows[0]["IM_PartName"].ToString();
                    IM_Mate_Standard_RM.Text = dt.Rows[0]["IM_Mate_Standard"].ToString();
                    IM_RMSource_RM.Text = dt.Rows[0]["IM_RMSource"].ToString();
                    IM_Dealer_RM.Text = dt.Rows[0]["IM_Dealer"].ToString();
                    IM_Grade_RM.Text = dt.Rows[0]["IM_Grade"].ToString();
                    IM_UOM_RM.SelectedValue = dt.Rows[0]["IM_UOM"].ToString();
                    IM_Purchase_Price_RM.Text = dt.Rows[0]["IM_Purchase_Price"].ToString();
                    IM_Currency_RM.SelectedValue = dt.Rows[0]["IM_Currency"].ToString();
                    IM_PackingStandard.Text = dt.Rows[0]["IM_PackingStandard"].ToString();


                    IM_HSNCodeRM.Text = dt.Rows[0]["IM_HSNCode"].ToString();
                    IM_MinStockRM.Text = dt.Rows[0]["IM_MinStock"].ToString();
                    IM_MaxStockRM.Text = dt.Rows[0]["IM_MaxStock"].ToString();

                    btnsave.Text = "&Update";
                    IM_Supplier_RM.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (IM_Type.Text == "Assembly")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataTable dt = dbFunctions.getTable("pr_Edit_Item_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    ID = dt.Rows[0]["IM_ID"].ToString();

                    IM_Type.SelectedValue = dt.Rows[0]["IM_Type"].ToString();
                    IM_PartNo_Assy.Text = dt.Rows[0]["IM_PartNo"].ToString();
                    IM_PartName_Assy.Text = dt.Rows[0]["IM_PartName"].ToString();
                    IM_Mate_Standard_Assy.Text = dt.Rows[0]["IM_Mate_Standard"].ToString();
                    IM_Sales_Price_Assy.Text = dt.Rows[0]["IM_Sales_Price"].ToString();
                    IM_Mould_Assy.SelectedValue = dt.Rows[0]["IM_Mould"].ToString();
                    IM_MouldNumer_Assy.Text = dt.Rows[0]["IM_MouldNumer"].ToString();
                    IM_MachineNoAssy.SelectedValue = dt.Rows[0]["IM_MachineNo"].ToString();
                    IM_PackingStandardAssy.Text = dt.Rows[0]["IM_PackingStandard"].ToString();


                    IM_HSNCodeAssy.Text = dt.Rows[0]["IM_HSNCode"].ToString();
                    IM_MinStockAssy.Text = dt.Rows[0]["IM_MinStock"].ToString();
                    IM_MaxStockAssy.Text = dt.Rows[0]["IM_MaxStock"].ToString();

                    btnsave.Text = "&Update";
                    IM_PartNo_Assy.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataTable dt = dbFunctions.getTable("pr_Edit_Item_Master  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    ID = dt.Rows[0]["IM_ID"].ToString();

                    IM_Type.SelectedValue = dt.Rows[0]["IM_Type"].ToString();
                    IM_PartNo_Oth.Text = dt.Rows[0]["IM_PartNo"].ToString();
                    IM_PartName_Oth.Text = dt.Rows[0]["IM_PartName"].ToString();
                    IM_Mate_Standard_Oth.Text = dt.Rows[0]["IM_Mate_Standard"].ToString();
                    IM_RMSource_Oth.Text = dt.Rows[0]["IM_RMSource"].ToString();
                    IM_Dealer_Oth.Text = dt.Rows[0]["IM_Dealer"].ToString();

                    IM_HSNCodeOthr.Text = dt.Rows[0]["IM_HSNCode"].ToString();
                    IM_MinStockOthr.Text = dt.Rows[0]["IM_MinStock"].ToString();
                    IM_MaxStockOthr.Text = dt.Rows[0]["IM_MaxStock"].ToString();

                    btnsave.Text = "&Update";
                    IM_PartNo_Oth.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }
        public void Delete()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (Radio_Pending.Checked == true)
                {
                   

                        DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            DataTable dt = dbFunctions.getTable("pr_Delete_Item_Master_FG " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                            MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // display();
                            Pending();
                            Clear();
                        }

                }
                    else
                    {
                        DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            DataTable dt = dbFunctions.getTable("pr_DeleteApproval_Item_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                            MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //display();
                            Pending();
                            Clear();
                        }

                    }
                }

            Radio_Active.Checked = true;
        }
        public void display()
        {
            if (IM_Type.Text == "FG")
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_FG '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
            else if (IM_Type.Text == "B/O")
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_BO '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
            else if (IM_Type.Text == "RM")
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_RM '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
            else if (IM_Type.Text == "Assembly")
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_Assy '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
            else //if(IM_Type.Text == "Others")//Assembly
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_Others '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
        }
        public void Clear()
        {
             //IM_Type.SelectedIndex = -1;
             IM_Plant.SelectedIndex = -1;
             IM_PartNo.Text = "";
             IM_PartName.Text = "";
             IM_Model.SelectedIndex = -1;
             IM_In_House_Or_Out_Source.SelectedIndex = -1;
             IM_Usage.Text = "";
             IM_UOM.SelectedIndex = -1;
             IM_Sales_Price.Text = "";
             IM_Currency.Text = "INR";
             IM_Mould.Text = "";
             IM_MouldNumer.Text = "";
             IM_MachineNo.SelectedIndex = -1;
             IM_PackingStandard.Text = "";

             IM_Supplier.SelectedIndex = -1;
             IM_PartNo_BO.Text = "";
             IM_PartName_BO.Text = "";
             IM_Mate_Standard_BO.Text="";
             IM_RMSource.Text = "";
             IM_Dealer.Text = "";
             IM_UOM_BO.SelectedIndex = -1;
             IM_Purchase_Price.Text = "";
             IM_Currency_BO.Text = "INR";

             IM_Supplier_RM.SelectedIndex = -1;
             IM_PartNo_RM.Text = "";
             IM_PartName_RM.Text = "";
             IM_Mate_Standard_RM.Text = "";
             IM_RMSource_RM.Text = "";
             IM_Dealer_RM.Text = "";
             IM_Grade_RM.Text = "";
             IM_UOM_RM.SelectedIndex = -1;
             IM_Purchase_Price_RM.Text = "";
             IM_Currency_RM.Text = "INR";

             IM_PartNo_Oth.Text = "";
             IM_PartName_Oth.Text = "";
             IM_Mate_Standard_Oth.Text = "";
             IM_RMSource_Oth.Text = "";
             IM_Dealer_Oth.Text = "";

             IM_PartNo_Assy.Text = "";
             IM_PartName_Assy.Text = "";
             IM_Mate_Standard_Assy.Text = "";
             IM_Sales_Price_Assy.Text = "";
             IM_Mould_Assy.SelectedValue =-1;
             IM_MouldNumer_Assy.Text = "";
             IM_MachineNoAssy.SelectedIndex = -1;
             IM_PackingStandardAssy.Text = "";

             IM_HSNCode.Text = "";
             IM_MinStock.Text="";
             IM_MaxStock.Text="";

             IM_HSNCodeBO.Text = "";
             IM_MinStockBO.Text = "";
             IM_MaxStockBO.Text = "";

             IM_HSNCodeRM.Text = "";
             IM_MinStockRM.Text = "";
             IM_MaxStockRM.Text = "";

             IM_HSNCodeOthr.Text = "";
             IM_MinStockOthr.Text = "";
             IM_MaxStockOthr.Text = "";

             IM_HSNCodeAssy.Text = "";
             IM_MinStockAssy.Text = "";
             IM_MaxStockAssy.Text = "";

            btnsave.Text = "&Save   ";
            IM_Plant.Focus();
        }
        public bool Validate()
        {
            if (IM_Type.Text == "FG")
            {

                if ((string.IsNullOrEmpty(IM_Type.Text.Trim())))
                {
                    ErrorMessage = "Type Should Not be Empty";
                    IM_Type.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_Plant.Text.Trim())))
                {
                    ErrorMessage = "Plant Should Not be Empty";
                    IM_Plant.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_Model.Text.Trim())))
                {
                    ErrorMessage = "Model Should Not be Empty";
                    IM_Model.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_In_House_Or_Out_Source.Text.Trim())))
                {
                    ErrorMessage = "IH/SC Should Not be Empty";
                    IM_In_House_Or_Out_Source.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_UOM.Text.Trim())))
                {
                    ErrorMessage = "UOM Should Not be Empty";
                    IM_UOM.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_Currency.Text.Trim())))
                {
                    ErrorMessage = "Item Type Should Not be Empty";
                    IM_Currency.Focus();
                    return true;
                }
            }
            else if (IM_Type.Text == "B/O")
            {
                if ((string.IsNullOrEmpty(IM_Type.Text.Trim())))
                {
                    ErrorMessage = "Type Should Not be Empty";
                    IM_Type.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_Supplier.Text.Trim())))
                {
                    ErrorMessage = "Supplier Should Not be Empty";
                    IM_Supplier.Focus();
                    return true;
                }
                try
                {
                    int x = int.Parse(IM_Supplier.SelectedValue.ToString());
                }
                catch
                {
                    ErrorMessage = "Select Proper Supplier ";
                    IM_Supplier.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_PartNo_BO.Text.Trim())))
                {
                    ErrorMessage = "B/O Spec Should Not be Empty";
                    IM_Model.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_PartName_BO.Text.Trim())))
                {
                    ErrorMessage = "B/O Grade Should Not be Empty";
                    IM_PartName_BO.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_UOM_BO.Text.Trim())))
                {
                    ErrorMessage = "UOM Should Not be Empty";
                    IM_UOM_BO.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_Currency_BO.Text.Trim())))
                {
                    ErrorMessage = "Currency Should Not be Empty";
                    IM_Currency_BO.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_Purchase_Price.Text.Trim())))
                {
                    ErrorMessage = "Purchase Price Should Not be Empty";
                    IM_Purchase_Price.Focus();
                    return true;
                }
            }
            else if (IM_Type.Text == "RM")
            {
                if ((string.IsNullOrEmpty(IM_Type.Text.Trim())))
                {
                    ErrorMessage = "Type Should Not be Empty";
                    IM_Type.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_Supplier_RM.Text.Trim())))
                {
                    ErrorMessage = "Supplier Should Not be Empty";
                    IM_Supplier_RM.Focus();
                    return true;
                }
                try
                {
                    int x = int.Parse(IM_Supplier_RM.SelectedValue.ToString());
                }
                catch
                {
                    ErrorMessage = "Select Proper Supplier ";
                    IM_Supplier_RM.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_PartNo_RM.Text.Trim())))
                {
                    ErrorMessage = "RM Spec Should Not be Empty";
                    IM_PartNo_RM.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_PartName_RM.Text.Trim())))
                {
                    ErrorMessage = "RM Grade Should Not be Empty";
                    IM_PartName_BO.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_UOM_RM.Text.Trim())))
                {
                    ErrorMessage = "UOM Should Not be Empty";
                    IM_UOM_RM.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_Currency_RM.Text.Trim())))
                {
                    ErrorMessage = "Currency Should Not be Empty";
                    IM_Currency_RM.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_Purchase_Price_RM.Text.Trim())))
                {
                    ErrorMessage = "Purchase Price Should Not be Empty";
                    IM_Purchase_Price_RM.Focus();
                    return true;
                }
            }
            else if (IM_Type.Text == "Assembly")
            {
                if ((string.IsNullOrEmpty(IM_Type.Text.Trim())))
                {
                    ErrorMessage = "Type Should Not be Empty";
                    IM_Type.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_PartNo_Assy.Text.Trim())))
                {
                    ErrorMessage = "Part No Should Not be Empty";
                    IM_PartNo_Assy.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_PartName_Assy.Text.Trim())))
                {
                    ErrorMessage = "Part Name Should Not be Empty";
                    IM_PartName_Assy.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_Sales_Price_Assy.Text.Trim())))
                {
                    ErrorMessage = "Sales Price Should Not be Empty";
                    IM_Sales_Price_Assy.Focus();
                    return true;
                }
            }
            else
            {
                if ((string.IsNullOrEmpty(IM_PartNo_Oth.Text.Trim())))
                {
                    ErrorMessage = "Part No Should Not be Empty";
                    IM_PartNo_Oth.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_PartName_Oth.Text.Trim())))
                {
                    ErrorMessage = "Part Name Should Not be Empty";
                    IM_PartName_Oth.Focus();
                    return true;
                }
                if ((string.IsNullOrEmpty(IM_Type.Text.Trim())))
                {
                    ErrorMessage = "Type Should Not be Empty";
                    IM_Type.Focus();
                    return true;
                }
               
            }


            return false;
           
           
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Pending();
        }

        public void Pending()
        {
            if (IM_Type.Text == "FG")
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_FG_Waiting '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible=false;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
            else if (IM_Type.Text == "B/O")
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_BO_Waiting '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
            else if (IM_Type.Text == "RM")
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_RM_Waiting '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
            else if (IM_Type.Text == "Assembly")
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_Assy_Waiting '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
            else //if(IM_Type.Text == "Others")//Assembly
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_Others_Waiting '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
        }
        private void Radio_Active_CheckedChanged(object sender, EventArgs e)
        {
            //display();
            Approved();
        }

        public void Approved()
        {
            if (IM_Type.Text == "FG")
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_FG_Approved '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
            else if (IM_Type.Text == "B/O")
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_BO_Approved '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
            else if (IM_Type.Text == "RM")
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_RM_Approved '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
            else if (IM_Type.Text == "Assembly")
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_Assy_Approved '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
            else
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_Others_Approved '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[ItemType] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
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
                //display();

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
        private void IT_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            //IM_Currency.SelectedIndex = 1;
            if (IM_Type.Text == "FG")
            {
                tabControl1.SelectedIndex = 0;
            }
            else  if (IM_Type.Text == "B/O")
            {
                tabControl1.SelectedIndex = 1;  
            }
            else if (IM_Type.Text == "RM")
            {
                tabControl1.SelectedIndex = 2; 
            }
            else if (IM_Type.Text == "Assembly")
            {
                tabControl1.SelectedIndex = 4;
            }
            else
            {
                tabControl1.SelectedIndex = 3;
            }
            Approved();
        
        }

       

        private void IM_Sales_Price_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void IM_Purchase_Price_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void IM_Purchase_Price_RM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void IM_Sales_Price_Assy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void IM_Mould_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = dbFunctions.Numeric_DecimalOnly(e.KeyChar, (TextBox)sender);
        }

        private void IM_Mould_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isMoldLoadFG)
            {
                DataTable dt = dbFunctions.getTable("pr_GetMoldNumber '" + IM_Mould.Text.ToString() + "'");
                IM_MouldNumer.DataSource = dt;
                IM_MouldNumer.DisplayMember = "MLD_MouldNo";
                IM_MouldNumer.ValueMember = "MLD_ID";
                IM_MouldNumer.SelectedIndex = -1;

            }
        }

       

        private void IM_Mould_Assy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isMoldLoadAssy)
            {

                DataTable dt = dbFunctions.getTable("pr_GetMoldNumber '" + IM_Mould_Assy.Text.ToString() + "'");
                IM_MouldNumer_Assy.DataSource = dt;
                IM_MouldNumer_Assy.DisplayMember = "MLD_MouldNo";
                IM_MouldNumer_Assy.ValueMember = "MLD_ID";
                IM_MouldNumer_Assy.SelectedIndex = -1;

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void IM_Grade_RM_TextChanged(object sender, EventArgs e)
        {

        }

        private void IM_Dealer_RM_TextChanged(object sender, EventArgs e)
        {

        }

        private void IM_Supplier_RM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbFunctions.getTable("select SM_MFRDetails from Supplier_Master where SM_ID= " + IM_Supplier_RM.SelectedValue.ToString());
                IM_RMSource_RM.Text = dt.Rows[0][0].ToString();
            }
            catch { }
        }

        private void IM_PackingStandard_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keypress = e.KeyChar;
            if (char.IsDigit(keypress) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                MessageBox.Show("Numbers Only Allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void IM_MouldNumer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label72_Click(object sender, EventArgs e)
        {

        }

      
   }
}