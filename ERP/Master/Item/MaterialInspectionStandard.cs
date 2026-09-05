using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using System.IO;

namespace LarchERP.Master
{

    public partial class MaterialInspectionStandard : Form
    {
        public string arrow = "Up";
        public int Distance = 284;
        public string ID = "";
        string ErrorMessage = "";
        public bool isPartLoad = false;
        public MaterialInspectionStandard()
        {
            InitializeComponent();
            dbFunctions.DGVStyle(dataGridView1);
        }




        private void ItemMaster_Shown(object sender, EventArgs e)
        {
            // display();
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
            Radio_Active.Checked = true;
            //display();
          
            LoadCharecterstics();
            LoadCheckMethod();
            LoadUOM();
            get_ins();

        }


        public void get_ins()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_Instrument ");
                MS_vInstrument_Name.DataSource = dt;
                MS_vInstrument_Name.DisplayMember = "IM_Instrument_Number";
                MS_vInstrument_Name.ValueMember = "IM_Instrument_Number";
                MS_vInstrument_Name.SelectedIndex = -1;
            }
            catch { }
        }


        public void LoadUOM()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadUOM ");
                MS_UOM_ID.DataSource = dt;
                MS_UOM_ID.DisplayMember = "UM_UOM";
                MS_UOM_ID.ValueMember = "UM_ID";
                MS_UOM_ID.SelectedIndex = -1;
            }
            catch
            {
            }
        }

        public void LoadCheckMethod()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_GetCheck_Method_Master ");
                MS_CheckMethod.DataSource = dt;
                MS_CheckMethod.DisplayMember = "CM_vDescription";
                MS_CheckMethod.ValueMember = "CM_iid";
                MS_CheckMethod.SelectedIndex = -1;
            }
            catch
            {
            }
        }

        public void LoadCharecterstics()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_GetCharecterstics ");
                MS_vDescription.DataSource = dt;
                MS_vDescription.DisplayMember = "IC_vDescription";
                MS_vDescription.ValueMember = "IC_iid";
                MS_vDescription.SelectedIndex = -1;
            }
            catch
            {
            }
        }


        public void get_Item()
        {
            string s="";
            if (MS_vInspection_Type.Text == "B/O INSPECTION")
            {
                s = "B/O";
            }
            else
            if(MS_vInspection_Type.Text == "RM INSPECTION")
            {
                s = "RM";
            }
            else
            {
                s = "FG";
            }

            //DataTable dt = dbFunctions.getTable("pr_Fetch_BOM_ItemName ");
            DataTable dt = dbFunctions.getTable("pr_Fetch_Items_For_Material_Inspection1 '"+s+"'");
            MS_item_ID.DataSource = dt;
            MS_item_ID.DisplayMember = "IM_PartName";
            MS_item_ID.ValueMember = "IM_ID";
            MS_item_ID.SelectedIndex = -1;
            isPartLoad = true;
           
        }



        private void btnDisplay_Click(object sender, EventArgs e)
        {
            // display();
            Radio_Active.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                com.CommandText = "Pr_Insert_Material_Standard_Master";
                com.Parameters.Add("@MS_vDoc_No", SqlDbType.VarChar).Value = MS_vDoc_No.Text.ToString();
                com.Parameters.Add("@MS_vRev_No", SqlDbType.VarChar).Value = MS_vRev_No.Text.ToString();
                com.Parameters.Add("@MS_vRev_dated", SqlDbType.VarChar).Value = MS_vRev_dated.Text.ToString();
                com.Parameters.Add("@MS_item_ID", SqlDbType.Int).Value = MS_item_ID.SelectedValue.ToString(); // TAKE ID
                com.Parameters.Add("@MS_vDescription", SqlDbType.Int).Value = MS_vDescription.SelectedValue.ToString(); // TAKE ID
                com.Parameters.Add("@MS_ToleranceType", SqlDbType.VarChar).Value = MS_ToleranceType.Text.ToString();
                com.Parameters.Add("@MS_Min", SqlDbType.Decimal).Value = MS_Min.Text.ToString();
                com.Parameters.Add("@MS_Max", SqlDbType.Decimal).Value = MS_Max.Text.ToString();
                com.Parameters.Add("@MS_Equal", SqlDbType.Decimal).Value = MS_Equal.Text.ToString();
                com.Parameters.Add("@MS_Text", SqlDbType.VarChar).Value = MS_Text.Text.ToString();
                com.Parameters.Add("@MS_CheckMethod", SqlDbType.Int).Value = MS_CheckMethod.SelectedValue.ToString();// TAKE ID
                com.Parameters.Add("@MS_UOM_ID", SqlDbType.VarChar).Value = MS_UOM_ID.Text.ToString();
                com.Parameters.Add("@MS_Frequency", SqlDbType.VarChar).Value = MS_Frequency.Text.ToString();
                com.Parameters.Add("@MS_iOrderNo", SqlDbType.Int).Value = MS_iOrderNo.Text.ToString();
                com.Parameters.Add("@MS_vCreatedby", SqlDbType.VarChar).Value = dbFunctions.username;
                com.Parameters.Add("@MS_vInspection_Type", SqlDbType.VarChar).Value = MS_vInspection_Type.Text.ToString();
                com.Parameters.Add("@MS_vInstrument_Name", SqlDbType.VarChar).Value = MS_vInstrument_Name.Text.ToString();

              //  byte[] imageData2 = null;


                //try
                //{
                //    MemoryStream ms1 = new MemoryStream();
                //    Person.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                //    imageData2 = ms1.GetBuffer();
                //}
                //catch
                //{


                //}

                //com.Parameters.Add("@MS_image", SqlDbType.Image).Value = (object)imageData2;

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


        public void Update()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Material_Standard_Master";
                com.Parameters.Add("@MS_iID", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@MS_vDoc_No", SqlDbType.VarChar).Value = MS_vDoc_No.Text.ToString();
                com.Parameters.Add("@MS_vRev_No", SqlDbType.VarChar).Value = MS_vRev_No.Text.ToString();
                com.Parameters.Add("@MS_vRev_dated", SqlDbType.VarChar).Value = MS_vRev_dated.Text.ToString();
                com.Parameters.Add("@MS_item_ID", SqlDbType.VarChar).Value = MS_item_ID.SelectedValue.ToString();
                com.Parameters.Add("@MS_vDescription", SqlDbType.VarChar).Value = MS_vDescription.SelectedValue.ToString();
                com.Parameters.Add("@MS_ToleranceType", SqlDbType.VarChar).Value = MS_ToleranceType.Text.ToString();
                com.Parameters.Add("@MS_Min", SqlDbType.VarChar).Value = MS_Min.Text.ToString();
                com.Parameters.Add("@MS_Max", SqlDbType.VarChar).Value = MS_Max.Text.ToString();
                com.Parameters.Add("@MS_Equal", SqlDbType.VarChar).Value = MS_Equal.Text.ToString();
                com.Parameters.Add("@MS_Text", SqlDbType.VarChar).Value = MS_Text.Text.ToString();
                com.Parameters.Add("@MS_CheckMethod", SqlDbType.VarChar).Value = MS_CheckMethod.SelectedValue.ToString();
                com.Parameters.Add("@MS_UOM_ID", SqlDbType.VarChar).Value = MS_UOM_ID.Text.ToString();
                com.Parameters.Add("@MS_Frequency", SqlDbType.VarChar).Value = MS_Frequency.Text.ToString();
                com.Parameters.Add("@MS_iOrderNo", SqlDbType.VarChar).Value = MS_iOrderNo.Text.ToString();
                com.Parameters.Add("@MS_vCreatedby", SqlDbType.VarChar).Value = dbFunctions.username;
                com.Parameters.Add("@MS_vInspection_Type", SqlDbType.VarChar).Value = MS_vInspection_Type.Text.ToString();
                com.Parameters.Add("@MS_vInstrument_Name", SqlDbType.VarChar).Value = MS_vInstrument_Name.Text.ToString();
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
            MS_vDescription.Focus();
        }


        public void Edit()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataTable dt = dbFunctions.getTable("Pr_Fetch_Material_Standard_Master_ByID  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                    MS_CheckMethod.SelectedIndex = -1;
                    MS_vDescription.SelectedIndex = -1;
                    ID = dt.Rows[0]["MS_iID"].ToString();
                    MS_vDoc_No.Text = dt.Rows[0]["MS_vDoc_No"].ToString();
                    MS_vRev_No.Text = dt.Rows[0]["MS_vRev_No"].ToString();
                    MS_vRev_dated.Text = dt.Rows[0]["MS_vRev_dated"].ToString();

                    MS_vDescription.SelectedValue = dt.Rows[0]["MS_vDescription"].ToString();
                    MS_ToleranceType.Text = dt.Rows[0]["MS_ToleranceType"].ToString();
                    MS_Min.Text = dt.Rows[0]["MS_Min"].ToString();
                    MS_Max.Text = dt.Rows[0]["MS_Max"].ToString();
                    MS_Equal.Text = dt.Rows[0]["MS_Equal"].ToString();
                    MS_Text.Text = dt.Rows[0]["MS_Text"].ToString();
                    MS_CheckMethod.SelectedValue = dt.Rows[0]["MS_CheckMethod"].ToString();
                    MS_UOM_ID.Text = dt.Rows[0]["MS_UOM_ID"].ToString();
                    MS_Frequency.Text = dt.Rows[0]["MS_Frequency"].ToString();
                    MS_iOrderNo.Text = dt.Rows[0]["MS_iOrderNo"].ToString();
                    MS_vInstrument_Name.Text = dt.Rows[0]["MS_vInstrument_Name"].ToString();
                    MS_vInspection_Type.Text = dt.Rows[0]["MS_vInspection_Type"].ToString();

                    btnsave.Text = "&Update";
                }
                else
                {
                    MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                MS_vDescription.Focus();
            }
            catch { }
        }


        public void Delete()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("Pr_Delete_Material_Standard_Master " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Fetch_Material_Standard_Master_Details '" + MS_item_ID.SelectedValue.ToString() + "','" + MS_vInspection_Type.Text + "'");
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dbFunctions.DGVStyle(dataGridView1);

                txt_Rows.Text = "Rows :" + dataGridView1.Rows.Count;
            }
            catch { }
        }


        public void Clear()
        {
            //Pq_vPart_Name.Text = "";
           // Pq_vModel.Text = "";
            //PO_SupplierName.Text = "";
            //PO_vSupplier_Address.Text = "";
            //MS_item_ID.Text = "";
            MS_vDescription.Text = "";
            MS_vInstrument_Name.Text = "";
            MS_ToleranceType.SelectedIndex=-1;
            MS_Min.Text = "0";
            MS_Max.Text = "0";
            MS_Equal.Text = "0";
            MS_Text.Text = "";
            MS_CheckMethod.Text = "";
            MS_UOM_ID.Text = "";
            MS_Frequency.Text = "";
            MS_iOrderNo.Text = "0";
            btnsave.Text = "&Save    ";
            display();

            MS_vDescription.Focus();
        }


        public bool Validate()
        {
            if ((string.IsNullOrEmpty(MS_CheckMethod.Text.Trim())))
            {
                ErrorMessage = "CheckMethod Should Not be Empty";
                MS_CheckMethod.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(MS_UOM_ID.Text.Trim())))
            {
                ErrorMessage = "UOM_ID Should Not be Empty";
                MS_UOM_ID.Focus();
                return true;
            }
            return false;
        }

        private void MS_ToleranceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MS_ToleranceType.Text.Equals("Number"))
            {
              
                QPA_ToleranceType.Enabled = true;
                MS_Min.Enabled = true;
                MS_Max.Enabled = true;
                MS_Equal.Enabled = false;
                MS_Text.Enabled = false;
                MS_Equal.Text = "0";
            }
            else if (MS_ToleranceType.Text.Equals("Equal"))
            {
               
                QPA_ToleranceType.Enabled = false;
                MS_Min.Enabled = false;
                MS_Max.Enabled = false;
                MS_Equal.Enabled = true;
                MS_Text.Enabled = false;
                MS_ToleranceType.Text = "0";
                MS_Min.Text = "0";
                MS_Max.Text = "0";


            }
            else
            {
              
               QPA_ToleranceType.Enabled = false;
               MS_Min.Enabled = false;
               MS_Max.Enabled = false;
               MS_Equal.Enabled = false;
               MS_Text.Enabled = true;
               MS_ToleranceType.Text = "0";
               MS_Min.Text = "0";
               MS_Max.Text = "0";
               MS_Equal.Text = "0";

            }
        }

        private void QPA_ToleranceType_TextChanged(object sender, EventArgs e)
        {

        }

        private void MS_item_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isPartLoad)
                {
                    DataTable dt = dbFunctions.getTable("pr_Get_Part_Detailsby_ID '" + MS_item_ID.SelectedValue.ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        Pq_vPart_Name.Text = dt.Rows[0]["IM_PartNo"].ToString();
                        Pq_vModel.Text = dt.Rows[0]["IM_Mate_Standard"].ToString();
                        PO_SupplierName.Text = dt.Rows[0]["SM_Name"].ToString();
                        PO_vSupplier_Address.Text = dt.Rows[0]["SM_PlantAddr"].ToString();

                        display();
                    }
                }
            }
            catch { }
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {

                this.Close();

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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Characteristics] LIKE '%{0}%' or [Specification] LIKE '%{0}%' or [CheckMethod] LIKE '%{0}%' or [UOM] LIKE '%{0}%'or [Spec] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MS_vInspection_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_Item();
            display();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //IM_PartImage.Text = openFileDialog1.FileName.ToString();
                //Person.Image = Image.FromFile(IM_PartImage.Text.ToString());

            }
        }
    }
}