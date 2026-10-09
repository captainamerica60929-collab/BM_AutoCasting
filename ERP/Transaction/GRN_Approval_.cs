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
using Shasun_Printing_Toll.Masters;
using System.Drawing.Printing;

namespace CRM_App.Transaction
{
    public partial class Grin_Against_After_Purchase_Order : Form
    {

        public static string Location = System.Configuration.ConfigurationSettings.AppSettings["Location"];

        public string GRNDID = "";
        public string ItemID = "";
        public string Value = "";
        public string ID = "";
        string GRND_iGRN_No;
        string ErrorMessage = "";
        public bool isSupplierLoad = false;
        public bool isLoadItem = false;
        public bool isGRNload = false;


        public string GRND_QC_Appoval = "Waitting";

        public Grin_Against_After_Purchase_Order()
        {
            InitializeComponent();
        }

        private void Grin_Against_After_Purchase_Order_Load(object sender, EventArgs e)
        {
            Get_GRN_No();
            LoadType();

            dbFunctions.DGVStyle(dataGridView2);


            PrintDocument prtdoc = new PrintDocument();
            string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;

            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                cmbPrinter.Items.Add(strPrinter);
                if (strPrinter == strDefaultPrinter)
                {
                    cmbPrinter.SelectedIndex = cmbPrinter.Items.IndexOf(strPrinter);
                }
            }
            dataClass.printerName = cmbPrinter.Text;
        }

        //public void Get_GRN_No()
        //{
        //    try
        //    {
        //        DataTable dt = dbFunctions.getTable("pr_Get_GRN_No_Approval");
        //        GRN_vGRN_No.DataSource = dt;
        //        GRN_vGRN_No.DisplayMember = "GRN_vGRN_No";
        //        GRN_vGRN_No.ValueMember = "GRN_iID_1";
        //        GRN_vGRN_No.SelectedIndex = -1;
        //        isSupplierLoad = true;
        //    }
        //    catch
        //    {
        //    }
        //}

        public void Get_GRN_No()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_GRN_No_Approval");

                GRN_vGRN_No.DataSource = null;

                GRN_vGRN_No.DisplayMember = "GRN_vGRN_No";
                GRN_vGRN_No.ValueMember = "GRN_iID_1";
                GRN_vGRN_No.DataSource = dt;

                GRN_vGRN_No.SelectedIndex = -1;

                isSupplierLoad = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadType()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Type");
                GRN_iType.DataSource = dt;
                GRN_iType.DisplayMember = "Ty_TypeName";
                GRN_iType.ValueMember = "Ty_ID";
                GRN_iType.SelectedIndex = -1;
                isSupplierLoad = true;
            }
            catch
            {
            }
        }
        public DataTable GRN_Details = null;
        private void GRN_vGRN_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (isSupplierLoad)
                {
                    GRN_Details = dbFunctions.getTable("pr_Get_GRN_For_Purchase_Order_Approval1 '" + GRN_vGRN_No.SelectedValue.ToString() + "'");
                    if (GRN_Details.Rows.Count > 0)
                    {
                        GRN_dGRN_Date.Value = DateTime.Parse(GRN_Details.Rows[0]["GRN_dGRN_Date"].ToString());
                        GRN_vInvoice_No.Text = GRN_Details.Rows[0]["GRN_vInvoice_No"].ToString();
                        GRN_dInvoice_Date.Value = DateTime.Parse(GRN_Details.Rows[0]["GRN_dInvoice_Date"].ToString());
                        GRN_vStoreLocation.Text = GRN_Details.Rows[0]["GRN_vStoreLocation"].ToString();
                        GRN_iType.SelectedValue = GRN_Details.Rows[0]["GRN_iType"].ToString();
                        PO_vSupplier_Name.Text = GRN_Details.Rows[0]["PO_vSupplier_Name"].ToString();
                        PO_vSupplier_Address.Text = GRN_Details.Rows[0]["PO_vSupplier_Address"].ToString();

                    }
                    Display();
                }
                
                dataGridView2.DataSource = null;
              //  LoadDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);

            }
        }

        private void Display()
        {
            if (GRN_vGRN_No.SelectedIndex < 0)
                return;

            object selectedValue = GRN_vGRN_No.SelectedValue;

            if (selectedValue == null)
                return;
            try
            {
                string grnValue = selectedValue.ToString();
                //string Query = "Pr_Display_GRN_Details '" + GRN_vGRN_No.SelectedValue.ToString() + "'";
                string Query =
          "Pr_Display_GRN_Details '" +
          grnValue.Replace("'", "''") +
          "'";
                DataTable dtDetails = dbFunctions.getTable(Query);
                dataGridView1.DataSource = dtDetails;
                dbFunctions.DGVStyle(dataGridView1);
                if (dtDetails.Rows.Count > 0)
                {
                    GRNDID = dtDetails.Rows[0]["ID"].ToString();
                    ItemID = dtDetails.Rows[0]["Item ID"].ToString();
                }
                dataGridView1.Columns["Item ID"].Visible = false;
                dataGridView1.Columns["Part Name"].ReadOnly = true;
                dataGridView1.Columns["Part No"].ReadOnly = true;
                dataGridView1.Columns["Unit Rate"].ReadOnly = true;
                dataGridView1.Columns["No of Bag"].ReadOnly = true;
                dataGridView1.Columns["Qty Bag"].ReadOnly = true;
                dataGridView1.Columns["Qty"].ReadOnly = true;
                dataGridView1.Columns["Total Qty"].ReadOnly = true;
                dataGridView1.Columns["Document"].ReadOnly = true;
                dataGridView1.Columns["Location"].Visible = false;

             
                dataGridView1.Rows[0].Selected = true;
            }
            catch 
            {
                //MessageBox.Show(""+ex);
            }

        }


        public bool Validate()  
        {
            if ((string.IsNullOrEmpty(GRN_vGRN_No.Text.Trim())))
            {
                ErrorMessage = "GRN No Should Not be Empty";
                GRN_vGRN_No.Focus();
                return true;
            }

            if ((string.IsNullOrEmpty(GRN_iType.Text.Trim())))
            {
                ErrorMessage = "GRN Type Should Not be Empty";
                GRN_iType.Focus();
                return true;
            }
                return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

                save();
                //if (GRN_iType.Text == "RM")
                //{
                //    // Compulsory image save
                //    try
                //    {
                //        imagesave();
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show("Image save is mandatory for RM.\n" + ex.Message);
                //        return; // Stop execution if image save fails
                //    }
                //}

                //// Optional save (runs for RM and non-RM)
                //try
                //{
                //    save();
                //}
                //catch
                //{
                //    // Optional – ignore or log
                //}


                //if (GRN_vInvoice_No.Text == "RM")
                // try
                // {
                //     imagesave();
                // }
                // catch { }

                // save();
            }
        }
        


        public void save()
        {
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
            try
                {
                    decimal x = decimal.Parse(dataGridView1.Rows[i].Cells["QC OK Bag"].Value.ToString());
                }
                catch { 
                
                MessageBox.Show("Invalid OK Bag","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
                }

            try
            {
                decimal x = decimal.Parse(dataGridView1.Rows[i].Cells["Reject Bag"].Value.ToString());
            }
            catch
            {

                MessageBox.Show("Invalid Rejection Bag", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            }
            

            bool Flag = true;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                

                 SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {

                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "pr_Update_Grin_Against_Purchase_Orderd_Details";

                    com.Parameters.Add("@GRND_iid", SqlDbType.Int).Value = dataGridView1.Rows[i].Cells["ID"].Value.ToString();
                    com.Parameters.Add("@GRND_vMaterial_Name", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Part Name"].Value.ToString();
                    com.Parameters.Add("@GRND_vRM_Spec", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Part Name"].Value.ToString();
                    com.Parameters.Add("@GRND_vRM_Grade", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Part No"].Value.ToString();
                    //com.Parameters.Add("@GRND_vMaterial_Name", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Spec"].Value.ToString();
                    //com.Parameters.Add("@GRND_vRM_Spec", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Spec"].Value.ToString();
                    //com.Parameters.Add("@GRND_vRM_Grade", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Grade"].Value.ToString();
                    com.Parameters.Add("@GRND_dUnit_Rate", SqlDbType.Decimal).Value = dataGridView1.Rows[i].Cells["Unit Rate"].Value.ToString();
                    com.Parameters.Add("@GRND_dNo_Of_Bag", SqlDbType.Decimal).Value = dataGridView1.Rows[i].Cells["No of Bag"].Value.ToString();
                    com.Parameters.Add("@GRND_dQty_Per_Bag", SqlDbType.Decimal).Value = dataGridView1.Rows[i].Cells["Qty Bag"].Value.ToString();
                    com.Parameters.Add("@GRND_dTotal_Qty", SqlDbType.Decimal).Value = dataGridView1.Rows[i].Cells["Total Qty"].Value.ToString();
                    com.Parameters.Add("@GRND_QC_OK_Qty", SqlDbType.Decimal).Value = dataGridView1.Rows[i].Cells["QC OK Bag"].Value.ToString();
                    com.Parameters.Add("@GRND_Rejection_Qty", SqlDbType.Decimal).Value = dataGridView1.Rows[i].Cells["Reject Bag"].Value.ToString();
                    com.Parameters.Add("@GRND_QC_Appoval", SqlDbType.VarChar).Value = GRND_QC_Appoval;

                    com.Parameters.Add("@GRND_vCreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;
                    com.ExecuteNonQuery();

                   

               
                }
                catch (Exception ex)
                {
                    Flag = false;
                    MessageBox.Show("Error " + ex.Message);
                }
            }


            if (Flag == true)
            {
                MessageBox.Show("Submited Succesfully..", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
           
        }
        string id1 = "0";
        private void imagesave()
        {
            byte[] imgBytes = File.ReadAllBytes(Doc_Photo.Text);

            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;

                if (id1 == "0")
                {
                    DataTable dd = dbFunctions.getTable("select isnull(max(IM_No),0)+1 from Image_Upload");
                    if (dd.Rows.Count > 0)
                    {
                        id1 = dd.Rows[0][0].ToString();
                    }
                    com.CommandText = "INSERT INTO image_upload (IM_No,im_Grnno,im_image,im_created_name,im_created_date,im_status) " +
                   "VALUES (@IM_No,@im_Grnno,@im_image,@im_created_name,getdate(),@im_status)";
                    //Type = "NEW";
                }
                else
                {

                    //Type = "EDIT";
                    //   button9.Text = "&Update";

                }

                com.Parameters.Add("@IM_No", SqlDbType.Int).Value = id1.ToString();
                com.Parameters.Add("@im_Grnno", SqlDbType.VarChar).Value = GRN_vGRN_No.SelectedValue.ToString();
                com.Parameters.Add("@im_image", SqlDbType.VarBinary).Value = imgBytes;
                com.Parameters.Add("@im_created_name", SqlDbType.VarChar).Value = dbFunctions.username;
                com.Parameters.Add("@im_status", SqlDbType.VarChar).Value = 'A';
                com.ExecuteNonQuery();
                com.Connection.Close();
              //MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                id1 = "0";
                Clear1();
            }
            catch { }
        }

        private void Clear1()
        {
            Doc_Photo.Clear();
            pictureBox1.Image = null;
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

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        public void Clear()
        {
            GRN_vGRN_No.SelectedValue=-1;
            GRN_dGRN_Date.Text="";
            GRN_iType.Text="";
            GRN_vInvoice_No.Text="";
            GRN_dInvoice_Date.Text="";
            GRN_vStoreLocation.Text="";
            PO_vSupplier_Name.Text="";
            PO_vSupplier_Address.Text = "";
          
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //if (!dataGridView1.SelectedRows[0].Cells["Document"].Value.ToString().Equals("Not Uploaded"))
            //{
                DataTable dt = dbFunctions.getTable("pr_get_Inspection_Details " + dataGridView1.SelectedRows[0].Cells["Item ID"].Value.ToString() + "," + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                dataGridView2.DataSource = dt;
                dbFunctions.DGVStyle(dataGridView2);

                dataGridView2.Columns["Description"].ReadOnly = true;
                dataGridView2.Columns["Type"].Visible = false;
                dataGridView2.Columns["Specification"].ReadOnly = true;
                dataGridView2.Columns["Min"].ReadOnly = true;
                dataGridView2.Columns["Max"].ReadOnly = true;
                dataGridView2.Columns["Equal"].Visible = false;
                dataGridView2.Columns["Text"].ReadOnly = true;
                dataGridView2.Columns["Check Method"].ReadOnly = true;
                dataGridView2.Columns["Result"].ReadOnly = true;
                dataGridView2.Columns["Result"].Width = 80;
            //try
            //{
            //    String a = "";
            //    if (dataGridView2.SelectedRows.Count > 0)
            //    {
            //        int selectedRowIndex = dataGridView2.SelectedRows[0].Index;

            //        for (int i = selectedRowIndex; i < dataGridView2.Rows.Count; i++)
            //        {

            //            if (dataGridView2.Rows[i].Cells["Actual"].Value != null &&
            //                !string.IsNullOrEmpty(dataGridView2.Rows[i].Cells["Actual"].Value.ToString()))
            //            {
            //                a = "b";

            //                break;
            //            }
            //            else
            //            {
            //                break;
            //            }
            //        }
            //        if ("b" == a)
            //        {
            //            button3.Enabled = false;
            //            button1.Enabled = true;

            //        }
            //        else
            //        {
            //            button3.Enabled = true;
            //            button1.Enabled = false;


            //        }
            //    }
            //}
            //catch { }



            Load_Doc_No();
            

            //}
            //else
            //{
            //    MessageBox.Show("Pleas  Upload Document Before UpDate QC Details", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }


        public void Load_Doc_No()
        {
            DataTable dt = dbFunctions.getTable("pr_get_Inspection_Details_Doc_No " + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
            if (dt.Rows.Count > 0)
            {
                MS_vRev_dated.Text = dt.Rows[0]["Rev Date"].ToString();
                MS_vRev_No.Text = dt.Rows[0]["MS_vRev_No"].ToString();
                MS_vDoc_No.Text = dt.Rows[0]["MS_vDoc_No"].ToString();
                

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else
            {

                if (GRN_iType.Text == "RM")
                {
                    // Compulsory image save
                    try
                    {
                        imagesave();
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Image save is mandatory for RM.\n" + ex.Message);
                        //return; // Stop execution if image save fails
                    }
                }

                // Optional save (runs for RM and non-RM)
                try
                {
                    SaveDetails();
                }
                catch
                {
                    // Optional – ignore or log
                }
               
            }
            // SaveDetails();
        }


        public void SaveDetails()
        {
            String a = "";
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView2.SelectedRows[0].Index; // Get the selected row index

                // Loop through the rows starting after the selected row
                for (int i = selectedRowIndex; i < dataGridView2.Rows.Count; i++)
                {
                    // Check if the "Actual" column contains a non-null and non-empty value
                    if (dataGridView2.Rows[i].Cells["Actual"].Value == null ||
    string.IsNullOrWhiteSpace(dataGridView2.Rows[i].Cells["Actual"].Value.ToString()))
                        {
                        a = "b";
                        // Value found in the "Actual" column below the selected row
                        // MessageBox.Show("There is a value in the Actual column below the selected row.");
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                if ("b" == a)
                {
                    button1.Enabled = true;
                    button3.Enabled = false;

                }

                else
                     {

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {

                        foreach (DataRow dt in GRN_Details.Rows)
                        {


                            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                            try
                            {

                                con.Open();
                                SqlCommand com = new SqlCommand();
                                com.Connection = con;
                                com.CommandType = CommandType.StoredProcedure;
                                com.CommandText = "pr_Insert_GRN_QC_Details";

                                com.Parameters.Add("@GQ_GRND_ID", SqlDbType.Int).Value = int.Parse(dt["GRND_iid"].ToString());
                                com.Parameters.Add("@GQ_Item_ID", SqlDbType.Int).Value = int.Parse(dt["GRND_iItem"].ToString());
                                com.Parameters.Add("@GQ_Description", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Description"].Value.ToString();
                                com.Parameters.Add("@GQ_Specification", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Specification"].Value.ToString();
                                com.Parameters.Add("@GQ_Type", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Type"].Value.ToString();
                                com.Parameters.Add("@GQ_Min", SqlDbType.Decimal).Value = dataGridView2.Rows[i].Cells["Min"].Value.ToString();
                                com.Parameters.Add("@GQ_Max", SqlDbType.Decimal).Value = dataGridView2.Rows[i].Cells["Max"].Value.ToString();
                                com.Parameters.Add("@GQ_Equal", SqlDbType.Decimal).Value = dataGridView2.Rows[i].Cells["Equal"].Value.ToString();
                                com.Parameters.Add("@GQ_Text", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Text"].Value.ToString();
                                com.Parameters.Add("@GQ_CheckMethod", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Check Method"].Value.ToString();
                                com.Parameters.Add("@GQ_Remark", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Remark"].Value.ToString();
                                com.Parameters.Add("@GQ_Actual", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Actual"].Value.ToString();
                                com.Parameters.Add("@GQ_Result", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Result"].Value.ToString();
                                com.Parameters.Add("@GQ_QC_ID", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["ID"].Value.ToString();
                                com.Parameters.Add("@GQ_Doc_No", SqlDbType.VarChar).Value = MS_vDoc_No.Text.ToString();
                                com.Parameters.Add("@GQ_Rec_No", SqlDbType.VarChar).Value = MS_vRev_No.Text.ToString();
                                com.Parameters.Add("@GQ_Rev_Date", SqlDbType.VarChar).Value = MS_vRev_dated.Text.ToString();
                                com.Parameters.Add("@GQ_CreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;
                                com.ExecuteNonQuery();
                                button3.Enabled = false;
                                button1.Enabled = true;
                                GRND_QC_Appoval = "Waitting";


                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error " + ex.Message);
                            }
                        }
                    }

                    MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDisplay();
                }
            }
        }

        public void LoadDisplay()
        {
           // for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                try
                {
                    DataTable dtDetails = dbFunctions.getTable("Pr_Display_GRN_QC_Details '" + GRNDID + "'");
                    dataGridView2.DataSource = dtDetails;
                    dbFunctions.DGVStyle(dataGridView2);

                    dataGridView2.Columns["Description"].ReadOnly = true;
                    dataGridView2.Columns["Type"].Visible = false;
                    dataGridView2.Columns["Specification"].ReadOnly = true;
                    dataGridView2.Columns["Min"].ReadOnly = true;
                    dataGridView2.Columns["Max"].ReadOnly = true;
                    dataGridView2.Columns["Equal"].Visible = false;
                    dataGridView2.Columns["Text"].ReadOnly = true;
                    dataGridView2.Columns["Check Method"].ReadOnly = true;
                    dataGridView2.Columns["Result"].ReadOnly = true;
                    dataGridView2.Columns["Result"].Width = 80;
             
                }
                catch
                { }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Podc_FileName.Text = openFileDialog1.FileName.ToString();

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (Podc_Description.Text.Equals(""))
            {
                MessageBox.Show("Document Name Should Not be Empty", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please Select One Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fileName = openFileDialog1.SafeFileName;

            string dist = Location + GRN_vGRN_No.Text.Replace('/', '_').Substring(0,8) + "_" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "_" + fileName;
            string Loc = GRN_vGRN_No.Text.Replace('/', '_').Substring(0, 8) + "_" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "_" + fileName;
       
            try
            {
                File.Copy(Podc_FileName.Text, dist);
            }
            catch { }


            DataTable dd = dbFunctions.getTable("pr_Upload_GRN_Document  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + ",'" + Podc_Description.Text + "','" + Loc + "'");
            MessageBox.Show("Documnet Uploaded Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Podc_Description.Text = "";
            Podc_FileName.Text = "--";
            Display();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                try
                {
                    if (!dataGridView1.SelectedRows[0].Cells["Document"].Value.ToString().Equals("Not Uploaded"))
                    {
                        DialogResult result = MessageBox.Show("Are You Sure Want to Open File Press yes", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {

                            string Path = Location + "" + dataGridView1.SelectedRows[0].Cells["Location"].Value.ToString();
                            System.Diagnostics.Process.Start(Path);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Upload Document", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                catch { }
            }
            else
            {
                MessageBox.Show("Please Select One Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
         
            }

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                double Total_Qty = double.Parse(dataGridView1.Rows[e.RowIndex].Cells["No of Bag"].Value.ToString());
                double QC_OK = double.Parse(dataGridView1.Rows[e.RowIndex].Cells["QC OK Bag"].Value.ToString());


                dataGridView1.Rows[e.RowIndex].Cells["Reject Bag"].Value = (Total_Qty - QC_OK).ToString();

            }
            catch
            {
            }

        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Rows[e.RowIndex].Cells["Type"].Value.ToString().Trim().Equals("Text"))
                {
                    if (dataGridView2.Rows[e.RowIndex].Cells["Actual"].Value.ToString().ToUpper().Trim().Equals(dataGridView2.Rows[e.RowIndex].Cells["Text"].Value.ToString().ToUpper().Trim()))
                    {
                        
                        dataGridView2.Rows[e.RowIndex].Cells["Result"].Value = "OK";
                        dataGridView2.Rows[e.RowIndex].Cells["Result"].Style.BackColor = Color.Green;
                    }
                    else
                    {

                        dataGridView2.Rows[e.RowIndex].Cells["Result"].Value = "Not OK";
                        dataGridView2.Rows[e.RowIndex].Cells["Result"].Style.BackColor = Color.Red;
                    }

                }

                if (dataGridView2.Rows[e.RowIndex].Cells["Type"].Value.ToString().Trim().Equals("Number"))
                {
                    decimal Min = decimal.Parse(dataGridView2.Rows[e.RowIndex].Cells["Min"].Value.ToString().ToUpper().Trim());
                    
                    decimal Max=decimal.Parse(dataGridView2.Rows[e.RowIndex].Cells["Max"].Value.ToString().ToUpper().Trim());

                    decimal Current = decimal.Parse(dataGridView2.Rows[e.RowIndex].Cells["Actual"].Value.ToString().ToUpper().Trim());

                    if (Current>=Min &&  Current<=Max)
                    {

                        dataGridView2.Rows[e.RowIndex].Cells["Result"].Value = "OK";
                        dataGridView2.Rows[e.RowIndex].Cells["Result"].Style.BackColor = Color.Green;
                    }
                    else
                    {

                        dataGridView2.Rows[e.RowIndex].Cells["Result"].Value = "Not OK";
                        dataGridView2.Rows[e.RowIndex].Cells["Result"].Style.BackColor = Color.Red;
                    }

                }

        

              
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                button1.Enabled = true;
                GRND_QC_Appoval = "Approve";
            }
            else
            {
                button1.Enabled = false;
                GRND_QC_Appoval = "Waitting";
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (columnName == "QC OK Bag") // Replace with actual column name
            {
                if (int.TryParse(e.FormattedValue.ToString(), out int qcOkValue))
                {
                    int rowIndex = e.RowIndex;

                    // Get No of Bag value from the same row
                    int noOfBagValue = 0;
                    if (int.TryParse(dataGridView1.Rows[rowIndex].Cells["No of Bag"].Value?.ToString(), out noOfBagValue))
                    {
                        if (qcOkValue > noOfBagValue)
                        {
                            MessageBox.Show("QC OK Bag value cannot be more than No of Bag", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true; // Prevent the cell from being updated
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid number for QC OK Bag.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Doc_Photo.Text = openFileDialog1.FileName.ToString();
                pictureBox1.Image = Image.FromFile(Doc_Photo.Text.ToString());

              

                //Pack_No.Text = fileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Clear1();
          //  imagesave();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
