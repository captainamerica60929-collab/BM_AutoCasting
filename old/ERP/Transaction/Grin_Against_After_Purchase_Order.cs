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
    public partial class GRN_Approval : Form
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

        public GRN_Approval()
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

        public void Get_GRN_No()
        {
            try
            {

                DataTable dt = dbFunctions.getTable("pr_Get_GRN_No_Approval__");
                GRN_vGRN_No.DataSource = dt;
                GRN_vGRN_No.DisplayMember = "GRN_vGRN_No";
                GRN_vGRN_No.ValueMember = "GRN_iID";
                GRN_vGRN_No.SelectedIndex = -1;
                isSupplierLoad = true;
            }
            catch
            {
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

        private void GRN_vGRN_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (isSupplierLoad)
                {
                    DataTable dt = dbFunctions.getTable("pr_Get_GRN_For_Purchase_Order_Approval1 '" + GRN_vGRN_No.SelectedValue.ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        
                        GRN_dGRN_Date.Value = DateTime.Parse(dt.Rows[0]["GRN_dGRN_Date"].ToString());
                        GRN_vInvoice_No.Text = dt.Rows[0]["GRN_vInvoice_No"].ToString();
                        GRN_dInvoice_Date.Value = DateTime.Parse(dt.Rows[0]["GRN_dInvoice_Date"].ToString());
                        GRN_vStoreLocation.Text = dt.Rows[0]["GRN_vStoreLocation"].ToString();
                        GRN_iType.SelectedValue = dt.Rows[0]["GRN_iType"].ToString();
                        PO_vSupplier_Name.Text = dt.Rows[0]["PO_vSupplier_Name"].ToString();
                        PO_vSupplier_Address.Text = dt.Rows[0]["PO_vSupplier_Address"].ToString();

                       


                        
                    }
                }
                Display();
                LoadDisplay();

                display_QC();
                button1.Enabled = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.Message);

            }
        }

        void display_QC()
        {

            if (!dataGridView1.SelectedRows[0].Cells["Document"].Value.ToString().Equals("Not Uploaded"))
            {
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
                Load_Doc_No();

            }
            else
            {
               // MessageBox.Show("Pleas  Upload Document Before UpDate QC Details", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Display()
        {

            try
            {
                DataTable dtDetails = dbFunctions.getTable("Pr_Display_GRN_Details1 '" + GRN_vGRN_No.SelectedValue.ToString() + "'");
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
                dataGridView1.Columns["Qty/Bag"].ReadOnly = true;
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
                    com.CommandText = "pr_Update_Approve_GRN";

                    com.Parameters.Add("@GRND_iid", SqlDbType.Int).Value = dataGridView1.Rows[i].Cells["ID"].Value.ToString();
                    com.Parameters.Add("@Apporved_by", SqlDbType.VarChar).Value = dbFunctions.username;

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
                MessageBox.Show("GRN Approved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }


        }


        public void print_QC_Lable()
        {
            DataTable dt = dbFunctions.getTable("pr_get_Lable_For_Print  " + GRN_vGRN_No.SelectedValue.ToString());
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string PrinterName = dbFunctions.Printer_Name;
                string text = System.IO.File.ReadAllText(@"D:\Lable.prn");

                string ReplaceText1 = text.Replace("@Format@", dt.Rows[i]["CS_Lable_Format"].ToString());
                string ReplaceText2 = ReplaceText1.Replace("@PartNo@", dt.Rows[i]["Part_No"].ToString());
                string ReplaceText3 = ReplaceText2.Replace("@PartGrade@", dt.Rows[i]["Grade"].ToString());
                string ReplaceText4 = ReplaceText3.Replace("@PartName@", dt.Rows[i]["CS_Part_Name"].ToString());
                string ReplaceText5 = ReplaceText4.Replace("@GRNNo@", dt.Rows[i]["GRN_vGRN_No"].ToString());
                string ReplaceText6 = ReplaceText5.Replace("@GRNDate@", DateTime.Parse(dt.Rows[i]["GRN Date"].ToString()).ToString("dd-MMM-yyyy"));
                string ReplaceText7 = ReplaceText6.Replace("@Lot@", dt.Rows[i]["GRND_vLot_No"].ToString());
                string ReplaceText8 = ReplaceText7.Replace("@PackQty@", dt.Rows[i]["Qty"].ToString());
                string ReplaceText9 = ReplaceText8.Replace("@Rec_Date@", DateTime.Parse(dt.Rows[i]["Received Date"].ToString()).ToString("dd-MMM-yyyy"));
                string ReplaceText10 = ReplaceText9.Replace("@Mfg_Date@", DateTime.Parse(dt.Rows[i]["Mfg Date"].ToString()).ToString("dd-MMM-yyyy"));
                string ReplaceText11 = ReplaceText10.Replace("@Exp_Date@", DateTime.Parse(dt.Rows[i]["ExpDate"].ToString()).ToString("dd-MMM-yyyy"));
                string ReplaceText12 = ReplaceText11.Replace("@Approve_By@", dt.Rows[i]["Useer"].ToString());
                string ReplaceText13 = ReplaceText12.Replace("@Approve_Date@", DateTime.Parse(dt.Rows[i]["GRN Date"].ToString()).ToString("dd-MMM-yyyy"));
                string ReplaceText14 = ReplaceText13.Replace("@Year@", System.DateTime.Now.ToString("MMM yyyy"));
                string ReplaceText15 = ReplaceText14.Replace("@Barcode@", dt.Rows[i]["Barcode"].ToString());
                RawPrinterHelper.SendStringToPrinter(PrinterName, ReplaceText15);

            }
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
            //if (Validate2())
            //{
            //    MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else
            //{
            //    SaveDetails();
            //}
            SaveDetails();
        }


        public void SaveDetails()
        {

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {

                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "pr_Insert_GRN_QC_Details";

                    com.Parameters.Add("@GQ_GRND_ID", SqlDbType.Int).Value = GRNDID;
                    com.Parameters.Add("@GQ_Item_ID", SqlDbType.Int).Value = ItemID;
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
                    button1.Enabled = true;

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }  
            }

            MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDisplay();
        }

        public void LoadDisplay()
        {
          //  for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                try
                {
                    DataTable dtDetails = dbFunctions.getTable("Pr_Display_GRN_QC_Details '" + GRNDID + "'");
                    dataGridView2.DataSource = dtDetails;
                    dbFunctions.DGVStyle(dataGridView2);

                    dataGridView2.Columns["Description"].ReadOnly = true;
                    dataGridView2.Columns["Specification"].ReadOnly = true;
                    dataGridView2.Columns["Type"].ReadOnly = true;
                    dataGridView2.Columns["Min"].ReadOnly = true;
                    dataGridView2.Columns["Max"].ReadOnly = true;
                    dataGridView2.Columns["Equal"].ReadOnly = true;
                    dataGridView2.Columns["Text"].ReadOnly = true;
                    dataGridView2.Columns["Check Method"].ReadOnly = true;
                    dataGridView2.Columns["Actual"].ReadOnly = true;
                    dataGridView2.Columns["Remark"].ReadOnly = true;
                    dataGridView2.Columns["Result"].ReadOnly = true;
                    dataGridView2.Columns["Doc No"].ReadOnly = true;
                    dataGridView2.Columns["Rev No"].ReadOnly = true;
                    dataGridView2.Columns["Rev Date"].ReadOnly = true;
                    dataGridView2.Columns["Doc No"].Visible = false;
                    dataGridView2.Columns["Rev No"].Visible = false;
                    dataGridView2.Columns["Rev Date"].Visible = false;
                    
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
           

            string fileName = openFileDialog1.SafeFileName;

            string dist = Location + GRN_vGRN_No.Text.Replace('/','_') + "_" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "_" + fileName;
            string Loc = GRN_vGRN_No.Text.Replace('/', '_') + "_" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "_" + fileName;
       
            try
            {
                File.Copy(Podc_FileName.Text, dist);
            }
            catch { }


            DataTable dd = dbFunctions.getTable("pr_Upload_GRN_Document  " + dataGridView1.Rows[0].Cells[0].Value.ToString() + ",'" + Podc_Description.Text + "','" + Loc + "'");
            MessageBox.Show("Documnet Uploaded Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Podc_Description.Text = "";
            Podc_FileName.Text = "--";
            Display();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
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

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
