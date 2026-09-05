using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using CRM_App.Crystal;

namespace CRM_App.Transaction
{
    public partial class Purchase_Order_Approval : Form
    {
        //int rowid = 0;
        //string POD_PO_No;
        public string ID = "";
        public bool isSupplierLoad = false;
        public bool isLoadItem = false;
        string ErrorMessage = "";

        public Purchase_Order_Approval()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Approval_Load(object sender, EventArgs e)
        {
            LoadSupplier();
        }

        public void LoadSupplier()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Fetch_Purchase_Order_No");
                PO_No.DataSource = dt;
                PO_No.DisplayMember = "PO_vPO_NO";
                PO_No.ValueMember = "PO_iID";
                PO_No.SelectedIndex = -1;
                isSupplierLoad = true;
            }
            catch
            {
            }
        }

        private void PO_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void Display()
        {
            try
            {
                DataTable dtDetails = dbFunctions.getTable("Pr_Fetch_Purchase_Order_Details11 '" + PO_No.SelectedValue.ToString() + "'");
                dataGridView1.DataSource = dtDetails;
             
                dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
                dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

                PO_vWarrenty_Class.Text = dtDetails.Rows[0]["Note"].ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(""+ex);
            }

        }

        public void Clear()
        {
            //PO_No.SelectedIndex = -1;
            PO_vSupplier_Address.Text = "";
            PO_SupplierName.Text = "";
            PO_vSupplier_Qtn_Ref.Text = "";
            PO_dPO_Date.Text = "";
            PO_vSupplier_Qtn_Date.Text = "";
            //PO_dAmendment_Date.Text = "";
            PO_vFreight.Text = "";
            PO_vMode_Of_Despatch.Text = "";
            PO_vDelivery.Text = "";
            PO_vPayment_Terms.Text = "";
            PO_vInspection.Text = "";
            PO_vWarrenty_Class.Text = "";
            PO_dSub_Total.Text = "";
            PO_dFreight_Charge_Percentage.Text = "";
            PO_dFreight_Charge.Text = "";
            PO_dInsurence_Charge_Percentage.Text = "";
            PO_dInsurence_Charge.Text = "";
            PO_dExcise_Duty_Percent.Text = "";
            PO_dExcise_Duty_Amount.Text = "";
            PO_dVAT_CST_Percentage.Text = "";
            PO_dVAR_CST_Amount.Text = "";
            PO_dGrand_Total.Text = "";
            //dataGridView1.Rows.Clear();
        }


        public bool Validate()
        {
            if ((string.IsNullOrEmpty(PO_No.Text.Trim())))
            {
                ErrorMessage = "Name Should Not be Empty";
                PO_No.Focus();
                return true;
            }
            //if ((string.IsNullOrEmpty(POD_dQty.Text.Trim())))
            //{
            //    ErrorMessage = "Qty Should Not be Empty";
            //    POD_dQty.Focus();
            //    return true;
            //}
            return false;
        }

        private void button5_Click(object sender, EventArgs e)
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
                    com.CommandText = "Pr_Update_Purchase_Order_Approval ";
                    com.Parameters.Add("@PO_iID", SqlDbType.Int).Value = PO_No.SelectedValue.ToString();
                    com.Parameters.Add("@PO_vApproval_Status", SqlDbType.VarChar).Value = "Approval";
                    com.Parameters.Add("@PO_dApproved_Date", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    com.Parameters.Add("@PO_vApproved_by", SqlDbType.VarChar).Value = dbFunctions.username;
                    //com.Parameters.Add("@PO_vCreatedby", SqlDbType.VarChar).Value = dbFunctions.username;
                    com.ExecuteNonQuery(); com.Connection.Close();
                    MessageBox.Show("PO No :" + PO_No.Text+ " Approved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

                }
                catch (Exception Ex)
                {
                    dbFunctions.Logs(Ex.Message, dbFunctions.username);
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        

        private void button6_Click_1(object sender, EventArgs e)
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
                    com.CommandText = "Pr_Update_Purchase_Order_UnApproval";
                    com.Parameters.Add("@PO_iID", SqlDbType.Int).Value = PO_No.SelectedValue.ToString();
                    com.Parameters.Add("@PO_vApproval_Status", SqlDbType.VarChar).Value = "UnApproval";
                    com.Parameters.Add("@PO_dApproved_Date", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd/MM/yyyy");
                    com.Parameters.Add("@PO_vApproved_by", SqlDbType.VarChar).Value = dbFunctions.username;
                    //com.Parameters.Add("@PO_vCreatedby", SqlDbType.VarChar).Value = dbFunctions.username;
                    com.ExecuteNonQuery(); com.Connection.Close();
                    MessageBox.Show("PO Rejected  Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    //display();
                }

                catch (Exception Ex)
                {
                    dbFunctions.Logs(Ex.Message, dbFunctions.username);
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
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
                    DialogResult result = MessageBox.Show("Are You Sure Want to Cancel?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                        if (InputBox("Reson ", "Enter  the Reson for Cancel:", ref value) == DialogResult.OK)
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand();
                            com.Connection = con;
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "Pr_Update_Purchase_Order_Approval_Cancel ";
                            com.Parameters.Add("@PO_iID", SqlDbType.Int).Value = PO_No.SelectedValue;
                            com.Parameters.Add("@PO_vApproval_Status", SqlDbType.VarChar).Value = "Cancel";
                            com.Parameters.Add("@PO_dApproved_Date", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd/MM/yyyy");
                            com.Parameters.Add("@PO_vApproved_by", SqlDbType.VarChar).Value = dbFunctions.username;
                            com.Parameters.Add("@PO_vNotes", SqlDbType.VarChar).Value = value;
                            //com.Parameters.Add("@PO_vCreatedby", SqlDbType.VarChar).Value = dbFunctions.username;

                            com.ExecuteNonQuery(); com.Connection.Close();
                            MessageBox.Show("Details Approval Cancel Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();

                        }
                    }

                }
                catch (Exception Ex)
                {
                    dbFunctions.Logs(Ex.Message, dbFunctions.username);
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

       

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 70);
            // textBox.Multiline = true;
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        //if (InputBox("Reson ", "Enter  the Reson for UnApprove:", ref value) == DialogResult.OK)
        //       {
        //          // MessageBox.Show(value);
        //       }
        string value = "";

        private void PO_No_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {

                if (isSupplierLoad)
                {
                    DataTable dt = dbFunctions.getTable("pr_Get_Purchase_Order '" + PO_No.SelectedValue.ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        PO_SupplierName.Text = dt.Rows[0]["PO_vSupplier_Name"].ToString();
                        PO_vSupplier_Address.Text = dt.Rows[0]["PO_vSupplier_Address"].ToString();
                        PO_iAmendment_No.Text = dt.Rows[0]["PO_iAmendment_No"].ToString();
                        PO_dAmendment_Date.Text = dt.Rows[0]["PO_dAmendment_Date"].ToString();
                        PO_dPO_Date.Text = dt.Rows[0]["PO_dPO_Date"].ToString();
                        PO_vSupplier_Qtn_Ref.Text = dt.Rows[0]["PO_vSupplier_Qtn_Ref"].ToString();
                        PO_vSupplier_Qtn_Date.Text = dt.Rows[0]["PO_vSupplier_Qtn_Date"].ToString();
                        PO_vFreight.Text = dt.Rows[0]["PO_vFreight"].ToString();
                        PO_vMode_Of_Despatch.Text = dt.Rows[0]["PO_vMode_Of_Despatch"].ToString();
                        PO_vDelivery.Text = dt.Rows[0]["PO_vDelivery"].ToString();
                        PO_vPayment_Terms.Text = dt.Rows[0]["PO_vPayment_Terms"].ToString();
                        PO_vInspection.Text = dt.Rows[0]["PO_vInspection"].ToString();
                        PO_vWarrenty_Class.Text = dt.Rows[0]["PO_vWarrenty_Class"].ToString();
                        PO_dSub_Total.Text = dt.Rows[0]["PO_dSub_Total"].ToString();
                        PO_dFreight_Charge_Percentage.Text = dt.Rows[0]["PO_dFreight_Charge_Percentage"].ToString();
                        PO_dFreight_Charge.Text = dt.Rows[0]["PO_dFreight_Charge"].ToString();
                        PO_dInsurence_Charge_Percentage.Text = dt.Rows[0]["PO_dInsurence_Charge_Percentage"].ToString();
                        PO_dInsurence_Charge.Text = dt.Rows[0]["PO_dInsurence_Charge"].ToString();
                        PO_dExcise_Duty_Percent.Text = dt.Rows[0]["PO_dExcise_Duty_Percent"].ToString();
                        PO_dExcise_Duty_Amount.Text = dt.Rows[0]["PO_dExcise_Duty_Amount"].ToString();
                        PO_dVAT_CST_Percentage.Text = dt.Rows[0]["PO_dVAT_CST_Percentage"].ToString();
                        PO_dVAR_CST_Amount.Text = dt.Rows[0]["PO_dVAR_CST_Amount"].ToString();
                        PO_dGrand_Total.Text = dt.Rows[0]["PO_dGrand_Total"].ToString();

                    }
                }
                Display();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Other_Purchase oRpt = new Other_Purchase();
                string SQlQuery = "Pr_Print_Purchase_Order_Approval_Report '" + PO_No.SelectedValue + "'";
                dbFunctions.printpdf(PO_No.SelectedValue.ToString(), SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                CrystalReport1 oRpt = new CrystalReport1();
                string SQlQuery = "Pr_Print_Purchase_Order_Approval_Report '" + PO_No.SelectedValue + "'";
                dbFunctions.printpdf(PO_No.SelectedValue.ToString(), SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }

        private void Label24_Click(object sender, EventArgs e)
        {

        }
    }
}
