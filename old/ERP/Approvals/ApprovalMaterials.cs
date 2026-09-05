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

    public partial class ApprovalMaterials : Form
    {
        public string arrow = "Up";
        public int Distance = 66;
        public string ID = "";
        string ErrorMessage = "";
        public string SupType = "";
        public string Department = "";
        public ApprovalMaterials()
        {
            InitializeComponent();
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
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
            LoadItemType();
            
            DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
            doWork.HeaderText = "Select";
            doWork.FalseValue = "0";
            doWork.TrueValue = "1";
            dataGridView1.Columns.Insert(0, doWork);
        }

        public void LoadItemType()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadItemType");
                IM_Type.DataSource = dt;
                IM_Type.DisplayMember = "Ty_TypeName";
                IM_Type.ValueMember = "Ty_ID";
                IM_Type.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) == "1")
                {

                    if (dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Updated" && dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() != "Approved" && dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() != "Newly Added")
                    {

                        DataTable dt = dbFunctions.getTable("pr_Edit_Item_Master  " + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
                        if (IM_Type.Text == "FG")
                        {

                            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                            try
                            {
                                con.Open();
                                SqlCommand com = new SqlCommand();
                                com.Connection = con;
                                com.CommandType = CommandType.StoredProcedure;
                                com.CommandText = "pr_Update_Item_Master_FG";

                                com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = dt.Rows[0]["IM_UpdatedID"].ToString();
                                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = dt.Rows[0]["IM_Type"].ToString();
                                com.Parameters.Add("@IM_Plant", SqlDbType.Int).Value = dt.Rows[0]["IM_Plant"].ToString();
                                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PartNo"].ToString();
                                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PartName"].ToString();
                                com.Parameters.Add("@IM_Model", SqlDbType.Int).Value = dt.Rows[0]["IM_Model"].ToString();
                                com.Parameters.Add("@IM_In_House_Or_Out_Source", SqlDbType.VarChar).Value = dt.Rows[0]["IM_In_House_Or_Out_Source"].ToString();
                                com.Parameters.Add("@IM_Usage", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Usage"].ToString();
                                com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = dt.Rows[0]["IM_UOM"].ToString();
                                com.Parameters.Add("@IM_Sales_Price", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Sales_Price"].ToString();
                                com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = dt.Rows[0]["IM_Currency"].ToString();
                                com.Parameters.Add("@IM_Mould", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Mould"].ToString();
                                com.Parameters.Add("@IM_MouldNumer", SqlDbType.VarChar).Value = dt.Rows[0]["IM_MouldNumer"].ToString();
                                com.Parameters.Add("@IM_MachineNo", SqlDbType.Int).Value = dt.Rows[0]["IM_MachineNo"].ToString();

                                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = dt.Rows[0]["IM_HSNCode"].ToString();
                                com.Parameters.Add("@IM_MinStock", SqlDbType.Decimal).Value = dt.Rows[0]["IM_MinStock"].ToString();
                                com.Parameters.Add("@IM_MaxStock", SqlDbType.Decimal).Value = dt.Rows[0]["IM_MaxStock"].ToString();

                                com.ExecuteNonQuery();
                                com.Connection.Close();
                                UpdateSelected();

                            }
                            catch (Exception Ex)
                            {
                                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }

                        else if (IM_Type.Text == "B/O")
                        {

                            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                            try
                            {
                                con.Open();
                                SqlCommand com = new SqlCommand();
                                com.Connection = con;
                                com.CommandType = CommandType.StoredProcedure;
                                com.CommandText = "pr_Update_Item_Master_BO";

                                com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = dt.Rows[0]["IM_UpdatedID"].ToString();
                                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = dt.Rows[0]["IM_Type"].ToString();
                                com.Parameters.Add("@IM_Supplier", SqlDbType.Int).Value = dt.Rows[0]["IM_Supplier"].ToString();
                                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PartNo"].ToString();
                                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PartName"].ToString();
                                com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Mate_Standard"].ToString();
                                com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = dt.Rows[0]["IM_RMSource"].ToString();
                                com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Dealer"].ToString();
                                com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = dt.Rows[0]["IM_UOM"].ToString();
                                com.Parameters.Add("@IM_Purchase_Price", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Purchase_Price"].ToString();
                                com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = dt.Rows[0]["IM_Currency"].ToString();

                                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = dt.Rows[0]["IM_HSNCode"].ToString();
                                com.Parameters.Add("@IM_MinStock", SqlDbType.Decimal).Value = dt.Rows[0]["IM_MinStock"].ToString();
                                com.Parameters.Add("@IM_MaxStock", SqlDbType.Decimal).Value = dt.Rows[0]["IM_MaxStock"].ToString();

                                com.ExecuteNonQuery();
                                com.Connection.Close();
                                UpdateSelected();

                            }
                            catch (Exception Ex)
                            {
                                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }

                        else if (IM_Type.Text == "RM")
                        {

                            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                            try
                            {
                                con.Open();
                                SqlCommand com = new SqlCommand();
                                com.Connection = con;
                                com.CommandType = CommandType.StoredProcedure;
                                com.CommandText = "pr_Update_Item_Master_RM";

                                com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = dt.Rows[0]["IM_UpdatedID"].ToString();
                                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = dt.Rows[0]["IM_Type"].ToString();
                                com.Parameters.Add("@IM_Supplier", SqlDbType.Int).Value = dt.Rows[0]["IM_Supplier"].ToString();
                                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PartNo"].ToString();
                                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PartName"].ToString();
                                com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Mate_Standard"].ToString();
                                com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = dt.Rows[0]["IM_RMSource"].ToString();
                                com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Dealer"].ToString();
                                com.Parameters.Add("@IM_Grade", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Grade"].ToString();
                                com.Parameters.Add("@IM_UOM", SqlDbType.Int).Value = dt.Rows[0]["IM_UOM"].ToString();
                                com.Parameters.Add("@IM_Purchase_Price", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Purchase_Price"].ToString();
                                com.Parameters.Add("@IM_Currency", SqlDbType.Int).Value = dt.Rows[0]["IM_Currency"].ToString();
                                com.Parameters.Add("@IM_PackingStandard", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PackingStandard"].ToString();



                                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = dt.Rows[0]["IM_HSNCode"].ToString();
                                com.Parameters.Add("@IM_MinStock", SqlDbType.Decimal).Value = dt.Rows[0]["IM_MinStock"].ToString();
                                com.Parameters.Add("@IM_MaxStock", SqlDbType.Decimal).Value = dt.Rows[0]["IM_MaxStock"].ToString();




                                com.ExecuteNonQuery();
                                com.Connection.Close();
                                UpdateSelected();

                            }
                            catch (Exception Ex)
                            {
                                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else if (IM_Type.Text == "Assembly")
                        {

                            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                            try
                            {
                                con.Open();
                                SqlCommand com = new SqlCommand();
                                com.Connection = con;
                                com.CommandType = CommandType.StoredProcedure;
                                com.CommandText = "pr_Update_Item_Master_Assy";

                                com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = dt.Rows[0]["IM_UpdatedID"].ToString();
                                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = dt.Rows[0]["IM_Type"].ToString();
                                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PartNo"].ToString();
                                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PartName"].ToString();
                                com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Mate_Standard"].ToString();
                                com.Parameters.Add("@IM_Sales_Price", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Sales_Price"].ToString();
                                com.Parameters.Add("@IM_Mould", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Mould"].ToString();
                                com.Parameters.Add("@IM_MouldNumer", SqlDbType.VarChar).Value = dt.Rows[0]["IM_MouldNumer"].ToString();
                                com.Parameters.Add("@IM_MachineNo", SqlDbType.Int).Value = dt.Rows[0]["IM_MachineNo"].ToString();
                                com.Parameters.Add("@IM_PackingStandard", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PackingStandard"].ToString();


                                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = dt.Rows[0]["IM_HSNCode"].ToString();
                                com.Parameters.Add("@IM_MinStock", SqlDbType.Decimal).Value = dt.Rows[0]["IM_MinStock"].ToString();
                                com.Parameters.Add("@IM_MaxStock", SqlDbType.Decimal).Value = dt.Rows[0]["IM_MaxStock"].ToString();


                                com.ExecuteNonQuery();
                                com.Connection.Close();
                                UpdateSelected();

                            }
                            catch (Exception Ex)
                            {
                                dbFunctions.Logs(Ex.Message, dbFunctions.username);
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
                                com.CommandText = "pr_Update_Item_Master_Others";

                                com.Parameters.Add("@IM_ID", SqlDbType.Int).Value = dt.Rows[0]["IM_UpdatedID"].ToString();
                                com.Parameters.Add("@IM_Type", SqlDbType.Int).Value = dt.Rows[0]["IM_Type"].ToString();
                                com.Parameters.Add("@IM_PartNo", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PartNo"].ToString();
                                com.Parameters.Add("@IM_PartName", SqlDbType.VarChar).Value = dt.Rows[0]["IM_PartName"].ToString();
                                com.Parameters.Add("@IM_Mate_Standard", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Mate_Standard"].ToString();
                                com.Parameters.Add("@IM_RMSource", SqlDbType.VarChar).Value = dt.Rows[0]["IM_RMSource"].ToString();
                                com.Parameters.Add("@IM_Dealer", SqlDbType.VarChar).Value = dt.Rows[0]["IM_Dealer"].ToString();

                                com.Parameters.Add("@IM_HSNCode", SqlDbType.VarChar).Value = dt.Rows[0]["IM_HSNCode"].ToString();
                                com.Parameters.Add("@IM_MinStock", SqlDbType.Decimal).Value = dt.Rows[0]["IM_MinStock"].ToString();
                                com.Parameters.Add("@IM_MaxStock", SqlDbType.Decimal).Value = dt.Rows[0]["IM_MaxStock"].ToString();



                                com.ExecuteNonQuery();
                                com.Connection.Close();
                                UpdateSelected();
                            }
                            catch (Exception Ex)
                            {
                                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }

                    }
                    else if (dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Waiting For Delete" && dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() != "Approved" && dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() != "Newly Added")
                    {
                        DataTable dt = dbFunctions.getTable("Pr_Delete_Item_Master " + dataGridView1.Rows[i].Cells["ID"].Value.ToString());
                    }
                    else if (dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() != "Approved")
                    {
                        DataTable dt = dbFunctions.getTable("pr_Approve_Item_Master '" + dataGridView1.Rows[i].Cells["ID"].Value.ToString() + "','" + dbFunctions.username + "'");
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Any One Option ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            display();
        }

        private void UpdateSelected()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Delete_Updated_Item_Master " + dataGridView1.SelectedRows[0].Cells["IM_UpdatedID"].Value.ToString());
            }
            catch
            {
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //Edit();
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
        public void Delete()
        {

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) == "1")
                {
                    if (dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Updated")
                    {
                        DataTable dt = dbFunctions.getTable("pr_Reject_Item_Master " + dataGridView1.Rows[i].Cells["IM_UpdatedID"].Value.ToString());
                    }
                    else if (dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Newly Added" || dataGridView1.SelectedRows[0].Cells["Operation Type"].Value.ToString() == "Waiting For Delete")
                    {
                        DataTable dt = dbFunctions.getTable("pr_Reject_Item_MasterNew " + dataGridView1.Rows[i].Cells["ID"].Value.ToString());
                    }
                }
                display();
            }
        }
        public void display()
        {
            if (IM_Type.Text == "FG")
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_FG_Waiting '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
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
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
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
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
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
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
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
                    DataTable dt = dbFunctions.getTable("pr_Display_Item_Master_Others_Waiting '" + IM_Type.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
                    txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
                }
                catch (Exception Ex)
                {

                }
            }






























            //if (IM_Type.Text == "FG")
            //{
            //    try
            //    {
            //        DataTable dt = dbFunctions.getTable("pr_DisplayWaitingList_Item_Master_FG '" + IM_Type.SelectedValue.ToString() + "'");
            //        dataGridView1.DataSource = dt;
            //        dataGridView1.Columns[1].Visible = false;
            //        dataGridView1.Columns[2].Visible = false;
            //       // dataGridView1.Columns[3].Visible = false;
            //        txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
            //    }
            //    catch (Exception Ex)
            //    {

            //    }
            //}
            //else if (IM_Type.Text == "B/O")
            //{
            //    try
            //    {
            //        DataTable dt = dbFunctions.getTable("pr_DisplayWaitingList_Item_Master_BO '" + IM_Type.SelectedValue.ToString() + "'");
            //        dataGridView1.DataSource = dt;
            //        dataGridView1.Columns[1].Visible = false;
            //        dataGridView1.Columns[2].Visible = false;
            //        //dataGridView1.Columns[3].Visible = false;
            //        txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
            //    }
            //    catch (Exception Ex)
            //    {

            //    }
            //}
            //else if (IM_Type.Text == "RM")
            //{
            //    try
            //    {
            //        DataTable dt = dbFunctions.getTable("pr_DisplayWaitingList_Item_Master_RM '" + IM_Type.SelectedValue.ToString() + "'");
            //        dataGridView1.DataSource = dt;
            //        dataGridView1.Columns[1].Visible = false;
            //        dataGridView1.Columns[2].Visible = false;
            //        //dataGridView1.Columns[3].Visible = false;
            //        txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
            //    }
            //    catch (Exception Ex)
            //    {

            //    }
            //}
            //else if (IM_Type.Text == "Assembly")
            //{
            //    try
            //    {
            //        DataTable dt = dbFunctions.getTable("pr_DisplayWaitingList_Item_Master_Assy '" + IM_Type.SelectedValue.ToString() + "'");
            //        dataGridView1.DataSource = dt;
            //        dataGridView1.Columns[1].Visible = false;
            //        dataGridView1.Columns[2].Visible = false;
            //        //dataGridView1.Columns[3].Visible = false;
            //        txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
            //    }
            //    catch (Exception Ex)
            //    {

            //    }
            //}
            //else //if(IM_Type.Text == "Others")//Assembly
            //{
            //    try
            //    {
            //        DataTable dt = dbFunctions.getTable("pr_DisplayWaitingList_Item_Master_Others '" + IM_Type.SelectedValue.ToString() + "'");
            //        dataGridView1.DataSource = dt;
            //        dataGridView1.Columns[1].Visible = false;
            //        dataGridView1.Columns[2].Visible = false;
            //        //dataGridView1.Columns[3].Visible = false;
            //        txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
            //    }
            //    catch (Exception Ex)
            //    {

            //    }
            //}

            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }
        public void Clear()
        {
        }


        //public bool Validate()
        //{
        //    //if ((string.IsNullOrEmpty(CB_ERPCode.Text.Trim())))
        //    //{
        //    //    ErrorMessage = "ERPCode Should Not be Empty";
        //    //    CB_ERPCode.Focus();
        //    //    return true;
        //    //}
        //    if ((string.IsNullOrEmpty(SM_Code.Text.Trim())))
        //    {
        //        ErrorMessage = "Code Should Not be Empty";
        //        SM_Code.Focus();
        //        return true;
        //    }
        //    if ((string.IsNullOrEmpty(SM_Name.Text.Trim())))
        //    {
        //        ErrorMessage = "Name Should Not be Empty";
        //        SM_Name.Focus();
        //        return true;
        //    }
        //    if ((string.IsNullOrEmpty(SM_ED.Text.Trim())))
        //    {
        //        ErrorMessage = "ED Should Not be Empty";
        //        SM_ED.Focus();
        //        return true;
        //    }

        //    if ((string.IsNullOrEmpty(SM_CST.Text.Trim())))
        //    {
        //        ErrorMessage = "CST  Should Not be Empty";
        //        SM_CST.Focus();
        //        return true;
        //    }
        //    if ((string.IsNullOrEmpty(SM_VAT.Text.Trim())))
        //    {
        //        ErrorMessage = "VAT  Should Not be Empty";
        //        SM_VAT.Focus();
        //        return true;
        //    }
        //    if ((string.IsNullOrEmpty(SM_GST.Text.Trim())))
        //    {
        //        ErrorMessage = "GST  Should Not be Empty";
        //        SM_GST.Focus();
        //        return true;
        //    }

            
        //    return false;
        //}
        private void Radio_Active_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("pr_Display_Customer_Master");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Code] LIKE '%{0}%' OR [Name] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                
                this.Close();

            }
        }

        private void IM_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            display();
        }
   }
}