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
    public partial class Inspected_Production_Data_Entry : Form
    {
        public string ErrorMessage = "";
        public string ID = "";
        public Inspected_Production_Data_Entry()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string Part_ID = "";
        private void Production_Data_Entry_Load(object sender, EventArgs e)
        {
            Load1();



        }

        private void Load1()
        {
            panel5.Visible = false;

            PD_Date.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            DataTable ddd = dbFunctions.getTable("pr_get_Inspected_List  '" + dbFunctions.Route_Card_ID + "'");
            dataGridView3.DataSource = ddd;
            dbFunctions.DGVStyle(dataGridView3);
            PD_CreatedBy.Text = dbFunctions.username;
            Shift.Text = "Shift I";
            try
            {
                if (comboBox1.Text == "Manual")
                {
                    aci_vCardNo1.Visible = false;
                    DataTable dt = dbFunctions.getTable("pr_get_Production_Request  '" + dbFunctions.Route_Card_ID + "'");
                    //ID = dt.Rows[0]["Pq_iid"].ToString();
                    Part_ID = dt.Rows[0]["Pq_iPart_ID"].ToString();
                    txtRCNo.Text = dt.Rows[0]["Pq_Route_Card_No"].ToString();
                    txtPartNo.Text = dt.Rows[0]["Pq_vPart_No"].ToString();
                    txtPartName.Text = dt.Rows[0]["Pq_vPart_Name"].ToString();
                    txtPartName.Text = dt.Rows[0]["Pq_vPart_No"].ToString();
                    txtPartNo.Text = dt.Rows[0]["Pq_vPart_Name"].ToString();
                    txtModel.Text = dt.Rows[0]["Pq_vModel"].ToString();
                    txtxplanQty.Text = dt.Rows[0]["Pq_RM_Plan_Qty"].ToString();
                    txtStartDate.Text = dt.Rows[0]["StartDate"].ToString();

                    lbl_Prod_Qty.Text = dt.Rows[0]["Prod_Qty"].ToString();
                }
                else
                {
                    aci_vCardNo1.Visible = true;
                    txtPartNo.Text = "";
                    txtPartName.Text = "";
                    txtPartNo.Text = "";
                    txtModel.Text = "";
                    txtxplanQty.Text = "";
                    txtStartDate.Text = "";
                }

            }
            catch { }
            CAL1();
            loademployee();
            loadrej();
        }

        private void loadrej()
        {
            DataTable a = dbFunctions.getTable("select Rej_iid, Rej_vDescription from Rejection_Method_Master where Rej_cStatus = 'A' and Rej_Type='Rejection' ");
            Rejection.DataSource = a;
            Rejection.DisplayMember = "Rej_vDescription";
            Rejection.ValueMember = "Rej_iid";
            Rejection.SelectedIndex = -1;
        }
        private void CAL1()
        {
            decimal value1, value2;


            bool isValue1Valid = decimal.TryParse(lbl_Prod_Qty.Text, out value1);
            bool isValue2Valid = decimal.TryParse(label28.Text, out value2);

            if (isValue1Valid && isValue2Valid)
            {

                decimal sum = value1 - value2;


                label31.Text = sum.ToString();
            }

            else
            {

                label28.Text = string.Empty;
            }
        }

        public bool Validate()
        {
            if ((string.IsNullOrEmpty(PD_OK_Qty.Text.Trim())))
            {
                ErrorMessage = "OK Qty Should Not be Empty";
                PD_OK_Qty.Focus();
                return true;
            }
            if ((string.IsNullOrEmpty(PD_Reject_Qty.Text.Trim())))
            {
                ErrorMessage = "Reject Qty Should Not be Empty";
                PD_Reject_Qty.Focus();
                return true;
            }

            //if (decimal.Parse(txtxplanQty.Text) < (decimal.Parse(PD_OK_Qty.Text.Trim()) + decimal.Parse(PD_Reject_Qty.Text.Trim()) + decimal.Parse(label28.Text.Trim())))
            //{
            //    ErrorMessage = "More than Production Qty.";
            //    PD_Reject_Qty.Focus();
            //    PD_Reject_Qty.Text = "";
            //    return true;
            //}

            return false;
        }


        private void insert()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Insert_Production_Details_Inspection";

                com.Parameters.Add("@PD_Route_Card_ID", SqlDbType.VarChar).Value = dbFunctions.Route_Card_ID;
                com.Parameters.Add("@PD_Date", SqlDbType.DateTime).Value = PD_Date.Text.ToString();
                com.Parameters.Add("@PD_CreatedBy", SqlDbType.VarChar).Value = PD_CreatedBy.Text.ToString();
                com.Parameters.Add("@PD_Shift", SqlDbType.VarChar).Value = Shift.Text.ToString();
                com.Parameters.Add("@PD_OK_Qty", SqlDbType.Int).Value = PD_OK_Qty.Text.ToString();
                com.Parameters.Add("@PD_Reject_Qty", SqlDbType.Int).Value = PD_Reject_Qty.Text.ToString();
                com.Parameters.Add("@FID_INSPECTOR_NAME", SqlDbType.VarChar).Value = Employee_Name.Text.ToString();
                com.Parameters.Add("@FID_em_id", SqlDbType.VarChar).Value = Employee_Name.SelectedValue.ToString();

                com.Parameters.Add("@ID", SqlDbType.Int);
                com.Parameters["@ID"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery(); com.Connection.Close();
                E_ID = com.Parameters["@ID"].Value.ToString();

                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //   DataTable a = dbFunctions.getTable(
                //    "UPDATE PROD_Barcode_Details " +
                //    "SET prod_status = 'SCANNED', " +
                //    "prod_scanned_date = '" + dbFunctions.getdate() + "', " +
                //    "prod_scanned_name = '" + dbFunctions.username + "' " +
                //    "WHERE PROD_vBarcodeId = '" + aci_vCardNo1.Text + "'"
                //);
                DataTable a = dbFunctions.getTable("update PROD_Barcode_Details set prod_qty_status='A' Where PROD_Rej_Qty='" + txtRCNo.Text + "' and prod_status='SCANNED'");
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
            PD_OK_Qty.Text = "";
            PD_Reject_Qty.Text = "";
            textBox1.Text = "";
        }

        public void display()
        {
            DataTable dt = dbFunctions.getTable("pr_Display_Production_Final_Inspection  " + dbFunctions.Route_Card_ID);
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns["Date"].Width = 90;

            dataGridView1.Columns["OK Qty"].Width = 90;
            dataGridView1.Columns["Rej Qty"].Width = 90;
            dataGridView1.Columns["Edit"].Width = 49;
            if (dt.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = false;
            }
            dataGridView1.Columns["OK Qty"].DefaultCellStyle.ForeColor = Color.Green;
            dataGridView1.Columns["Rej Qty"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Edit"].DefaultCellStyle.ForeColor = Color.Blue;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.ReadOnly = true;

            decimal OK = 0.0m;
            decimal Rejection = 0.0m;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                OK += decimal.Parse(dataGridView1.Rows[i].Cells["OK Qty"].Value.ToString());
                Rejection += decimal.Parse(dataGridView1.Rows[i].Cells["Rej Qty"].Value.ToString());

            }

            label26.Text = (OK).ToString("0.00");
            label27.Text = (Rejection).ToString("0.00");
            label28.Text = (OK + Rejection).ToString("0.00");
            CAL1();

        }
        private void Update()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Update_Production_Details";

                com.Parameters.Add("@PD_iid", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@PD_OK_Qty", SqlDbType.Int).Value = PD_OK_Qty.Text.ToString();
                com.Parameters.Add("@PD_Reject_Qty", SqlDbType.Int).Value = PD_Reject_Qty.Text.ToString();


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
            btnsave.Text = "&Save";
        }

        private void PD_OK_Qty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void PD_Reject_Qty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnsave_Click(object sender, EventArgs e)
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

        public void Edit()
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("pr_Edit_Production_Details  " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ID = dt.Rows[0]["PD_iid"].ToString();
                PD_Date.Text = dt.Rows[0]["Date"].ToString();
                PD_CreatedBy.Text = dt.Rows[0]["PD_CreatedBy"].ToString();
                Shift.Text = dt.Rows[0]["PD_Shift"].ToString();
                PD_OK_Qty.Text = dt.Rows[0]["PD_OK_Qty"].ToString();
                PD_Reject_Qty.Text = dt.Rows[0]["PD_Reject_Qty"].ToString();

                btnsave.Text = "&Update";

            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
                    DataTable dt = dbFunctions.getTable("pr_Delete_Production_Details " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Production_Data_Entry_Shown(object sender, EventArgs e)
        {
            display();
        }
        private void loademployee()
        {
            DataTable a = dbFunctions.getTable("select EM_iid,EM_EmployeeName from Employee_Master where EM_Status='A' AND EM_Category != 'Staff'");
            Employee_Name.DataSource = a;
            Employee_Name.DisplayMember = "EM_EmployeeName";
            Employee_Name.ValueMember = "EM_iid";
            Employee_Name.SelectedIndex = -1;

        }

        private void button1_Click(object sender, EventArgs e)
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

            if (decimal.Parse(lbl_Prod_Qty.Text) <= decimal.Parse(label28.Text.Trim()))
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Close Routecard", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dd = dbFunctions.getTable("update Production_Request set    Pq_Route_Card_End_Date=(select Max(FID_Date) from Production_Details_Inspection where FID_Route_Card_ID=" + dbFunctions.Route_Card_ID + "), RC_Status='Closed' where pq_iid=" + dbFunctions.Route_Card_ID);

                }

            }


            groupBox2.Visible = true;
            groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            displayInProcessInspection();
        }

        public string E_ID = "";

        public void displayInProcessInspection()
        {

            DataTable ddd = dbFunctions.getTable("pr_get_Inspected_List  '" + dbFunctions.Route_Card_ID + "'");
            dataGridView3.DataSource = ddd;
            dbFunctions.DGVStyle(dataGridView3);
            try
            {
                try
                {
                    dataGridView2.Columns[0].Frozen = false;
                    dataGridView2.Columns[1].Frozen = false;
                    dataGridView2.Columns[2].Frozen = false;
                }
                catch { }
                DataTable dt = dbFunctions.getTable("pr_Display_Final_QC_Details '" + dbFunctions.Route_Card_ID + "','" + E_ID + "'");
                if (dt.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dt;
                    dbFunctions.DGVStyle(dataGridView2);
                    dataGridView2.Columns[0].Frozen = true;
                    dataGridView2.Columns[1].Frozen = true;
                    dataGridView2.Columns[2].Frozen = true;

                    dataGridView2.Columns[1].Width = 250;
                    dataGridView2.Columns[2].Width = 250;
                    dataGridView2.SelectionMode = DataGridViewSelectionMode.CellSelect;


                }
                else
                {

                    DataTable dtb = dbFunctions.getTable("pr_Fetch_Final_INSPECTIONDetail '" + Part_ID + "'");
                    dataGridView2.DataSource = dtb;
                    dbFunctions.DGVStyle(dataGridView2);
                    dataGridView2.Columns[0].Frozen = true;
                    dataGridView2.Columns[1].Frozen = true;
                    dataGridView2.Columns[2].Frozen = true;

                    dataGridView2.Columns[1].Width = 250;
                    dataGridView2.Columns[2].Width = 250;
                    dataGridView2.SelectionMode = DataGridViewSelectionMode.CellSelect;

                }


            }
            catch (Exception ex) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool Flag = true;



            DataTable dtd = dbFunctions.getTable("delete from Final_Inspection_QC_Details where FI_Production_ID=" + E_ID);

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_Final_Inspection_QC_Details";
                    com.Parameters.Add("@FI_RouteCard_ID", SqlDbType.VarChar).Value = dbFunctions.Route_Card_ID;
                    com.Parameters.Add("@FI_Production_ID", SqlDbType.VarChar).Value = E_ID;
                    com.Parameters.Add("@FI_Description", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Description"].Value.ToString();
                    com.Parameters.Add("@FI_Parameters", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Parameter"].Value.ToString();
                    com.Parameters.Add("@FI_X1", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["X1"].Value.ToString();
                    com.Parameters.Add("@FI_X2", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["X2"].Value.ToString();
                    com.Parameters.Add("@FI_X3", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["X3"].Value.ToString();
                    com.Parameters.Add("@FI_X4", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["X4"].Value.ToString();
                    com.Parameters.Add("@FI_X5", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["X5"].Value.ToString();
                    com.Parameters.Add("@FI_Inpection_Status", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Status"].Value.ToString();
                    com.Parameters.Add("@FI_UserDetails", SqlDbType.VarChar).Value = dbFunctions.username;
                    com.ExecuteNonQuery();
                    com.Connection.Close();
                    Clear();
                    display();
                }
                catch (Exception Ex)
                {
                    Flag = false;
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (Flag)
            {
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                groupBox2.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
        }

        private void dataGridView3_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = dbFunctions.getTable("pr_Display_Final_QC_Details '" + dbFunctions.Route_Card_ID + "','" + E_ID + "'");
            if (dt.Rows.Count > 0)
            {
                dataGridView2.DataSource = dt;
                dbFunctions.DGVStyle(dataGridView2);
                dataGridView2.Columns[0].Frozen = true;
                dataGridView2.Columns[1].Frozen = true;
                dataGridView2.Columns[2].Frozen = true;

                dataGridView2.Columns[1].Width = 250;
                dataGridView2.Columns[2].Width = 250;
                dataGridView2.SelectionMode = DataGridViewSelectionMode.CellSelect;


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {


            try
            {
                groupBox2.Visible = true;
                groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;

                DataTable dt = dbFunctions.getTable("pr_Display_Final_QC_Details '" + dbFunctions.Route_Card_ID + "','" + dataGridView3.SelectedRows[0].Cells[0].Value.ToString() + "'");

                E_ID = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
                if (dt.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dt;
                    dbFunctions.DGVStyle(dataGridView2);
                    dataGridView2.Columns[0].Frozen = true;
                    dataGridView2.Columns[1].Frozen = true;
                    dataGridView2.Columns[2].Frozen = true;

                    dataGridView2.Columns[1].Width = 250;
                    dataGridView2.Columns[2].Width = 250;
                    dataGridView2.SelectionMode = DataGridViewSelectionMode.CellSelect;


                }
            }
            catch { }
        }

        private void PD_OK_Qty_TextChanged(object sender, EventArgs e)
        {
            //  CAL();

        }

        private void Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                getdata();
            }
        }

        private void getdata()
        {
            panel14.Visible = true;
            try
            {
                dataGridView5.Columns.RemoveAt(6);
            }
            catch { }
            dataGridView5.DataSource = null;
            DataTable dt = dbFunctions.getTable("pr_edit_Final_Inspection_Detail  " + dbFunctions.Route_Card_ID);
            dataGridView5.DataSource = dt;
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView5);
            dataGridView5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridView5.Columns[1].ReadOnly = true;
            dataGridView5.Columns[2].ReadOnly = true;
            dataGridView5.Columns[1].Width = 100;
            dataGridView5.Columns[2].Width = 100;
            dataGridView5.Columns[5].Width = 100;
            dataGridView5.Columns[3].Width = 100;
            dataGridView5.Columns[4].Width = 100;
            DataGridViewButtonColumn doWork = new DataGridViewButtonColumn();
            doWork.HeaderText = " ";
            doWork.Text = "Update";
            dataGridView5.Columns.Insert(6, doWork);
            dataGridView5.Columns[6].Width = 93;
            for (int i = 0; i < dataGridView5.Rows.Count; i++)
            {
                dataGridView5.Rows[i].Cells[6].Value = "Update";
            }
        }

        private void PD_Reject_Qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int A = Int32.Parse(PD_Reject_Qty.Text);
                if (A >= 1)
                {
                    panel5.Visible = true;
                    display1();
                }
                else if (A == 0)
                {
                    panel5.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            save();

        }
        public string ID1 = "0";
        private void save()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                if (ID1 == "0")
                {
                    DataTable dd = dbFunctions.getTable("select isnull(max(prej_id),0)+1 from Production_Rejection");
                    if (dd.Rows.Count > 0)
                    {
                        ID1 = dd.Rows[0][0].ToString();
                    }
                    com.CommandText = "insert into Production_Rejection (prej_id,prej_rid,prej_rejection,prej_name,prej_qty,pjrej_status,prej_darstatus,prej_date) Values (@prej_id,@prej_rid,@prej_rejection,@prej_name,@prej_qty,@pjrej_status,@prej_darstatus,getdate())";
                    //Type = "NEW";
                }
                else
                {
                    com.CommandText = "update  Production_Rejection Set prej_rid=@prej_rid,prej_rejection=@prej_rejection,prej_name=@prej_name,prej_qty=@prej_qty,pjrej_status=@pjrej_status,prej_darstatus=@prej_darstatus,prej_date=getdate() where prej_id=@prej_id ";
                    //Type = "EDIT";
                    button9.Text = "&Update";

                }

                com.Parameters.Add("@prej_id", SqlDbType.VarChar).Value = ID1.ToString();
                com.Parameters.Add("@prej_rid", SqlDbType.VarChar).Value = dbFunctions.Route_Card_ID;
                com.Parameters.Add("@prej_rejection", SqlDbType.Int).Value = Rejection.SelectedValue.ToString();
                com.Parameters.Add("@prej_name", SqlDbType.VarChar).Value = Rejection.Text;
                com.Parameters.Add("@prej_qty", SqlDbType.VarChar).Value = textBox2.Text;
                // com.Parameters.Add("@prej_date", SqlDbType.VarChar).Value = DateTime.Now;
                com.Parameters.Add("@pjrej_status", SqlDbType.VarChar).Value = "A";
                com.Parameters.Add("@prej_darstatus", SqlDbType.VarChar).Value = "F";



                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception Ex)
            {
                //dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            display1();
            clear1();
        }

        private void display1()
        {
            DataTable dis = dbFunctions.getTable("Select prej_id as [ID], prej_name as [Rejection Name],prej_qty as [Qty] from Production_Rejection where prej_rid='" + dbFunctions.Route_Card_ID + "' AND  pjrej_status='A' and prej_darstatus='F'");
            dataGridView4.DataSource = dis;
            dataGridView4.Columns["ID"].Visible = false;
        }

        private void clear1()
        {
            ID1 = "0";
            Rejection.Text = "";
            textBox2.Text = "";

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Edit1();
            button9.Text = "&Update";
            ID1 = dataGridView4.SelectedRows[0].Cells[0].Value.ToString();
        }
        public string as1 = "";
        private void Edit1()
        {
            DataTable a = dbFunctions.getTable("select * from Production_Rejection where prej_id='" + dataGridView4.SelectedRows[0].Cells[0].Value.ToString() + "'");
            as1 = a.Rows[0]["prej_id"].ToString();
            Rejection.Text = a.Rows[0]["prej_name"].ToString();
            textBox2.Text = a.Rows[0]["prej_qty"].ToString();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("update Production_Rejection set pjrej_status='D'   where prej_id='" + dataGridView4.SelectedRows[0].Cells[0].Value.ToString() + "'");
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    display1();
                    clear1();

                }
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            LarchERP.Master.RejectionMethod new1 = new LarchERP.Master.RejectionMethod();
            new1.Show();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Rej_CheckPointRefresh");
                Rejection.DataSource = dt;
                Rejection.DisplayMember = "Rej_vDescription";
                Rejection.ValueMember = "Rej_iid";
                Rejection.SelectedIndex = 0;

            }
            catch
            {
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            save();
        }

        private void Aci_vCardNo1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    DataTable dt = dbFunctions.getTable("select * from PROD_Barcode_Details where PROD_vBarcodeId=  '" + aci_vCardNo1.Text + "' and PROD_Rej_Qty= '" + dbFunctions.Route_Card_ID + "'");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["prod_status"].ToString() == "SCANNED")
                        {

                            MessageBox.Show("ALREADY SCANNED", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            aci_vCardNo1.Text = "";
                            return;

                        }
                    }
                    DataTable dt1 = dbFunctions.getTable("select * from PROD_Barcode_Details where PROD_Rej_Qty = '" + dbFunctions.Route_Card_ID + "' and prod_status is null ORDER BY 1 ASC");
                    if (dt1.Rows.Count > 0)
                    {

                        string A = dt1.Rows[0]["PROD_vBarcodeId"].ToString();

                        if (A.Equals(aci_vCardNo1.Text, StringComparison.OrdinalIgnoreCase))
                        {
                            txtRCNo.Text = dt1.Rows[0]["PROD_Rej_Qty"].ToString();
                            txtPartNo.Text = dt1.Rows[0]["PROD_iItemId"].ToString();
                            txtPartName.Text = dt1.Rows[0]["PROD_vLotNo"].ToString();


                            txtModel.Text = dt1.Rows[0]["PROD_vShift"].ToString();
                            txtxplanQty.Text = dt1.Rows[0]["PROD_Sheet_Length"].ToString();
                            txtStartDate.Text = dt1.Rows[0]["PROD_Sheet_Thickness"].ToString();
                            textBox6.Text = dt1.Rows[0]["PROD_nQty"].ToString();
                            DataTable a = dbFunctions.getTable(" SELECT SUM(CAST(prod_ok AS INT)) AS Total_Prod_OK,SUM(CAST(prod_rej AS INT)) AS Total_Prod_Rej  FROM PROD_Barcode_Details  WHERE PROD_Rej_Qty = '" + txtRCNo.Text + "'  AND prod_qty_status is null");
                            if (a.Rows.Count > 0)
                            {
                                textBox4.Text = a.Rows[0]["Total_Prod_OK"].ToString();
                                textBox5.Text = a.Rows[0]["Total_Prod_Rej"].ToString();
                            }

                        }

                        else
                        {
                            MessageBox.Show("Barcode Mis-Match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }
                    }
                    else
                    {

                        MessageBox.Show("FIFO ERROR", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }



                    //if (dt1.Rows.Count > 0)
                    //{
                    //    if (dt1.Rows[0]["prod_status"].ToString() == "SCANNED")
                    //    {

                    //        MessageBox.Show("ALREADY SCANNED", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //        aci_vCardNo1.Text = "";

                    //    }
                    //    else
                    //    {
                    //        txtRCNo.Text = dt1.Rows[0]["PROD_Rej_Qty"].ToString();
                    //        txtPartNo.Text = dt1.Rows[0]["PROD_iItemId"].ToString();
                    //        txtPartName.Text = dt1.Rows[0]["PROD_vLotNo"].ToString();


                    //        txtModel.Text = dt1.Rows[0]["PROD_vShift"].ToString();
                    //        txtxplanQty.Text = dt1.Rows[0]["PROD_Sheet_Length"].ToString();
                    //        txtStartDate.Text = dt1.Rows[0]["PROD_Sheet_Thickness"].ToString();
                    //        textBox6.Text = dt1.Rows[0]["PROD_nQty"].ToString();
                    //        DataTable a = dbFunctions.getTable(" SELECT SUM(CAST(prod_ok AS INT)) AS Total_Prod_OK,SUM(CAST(prod_rej AS INT)) AS Total_Prod_Rej  FROM PROD_Barcode_Details  WHERE PROD_Rej_Qty = '" + txtRCNo.Text + "'  AND prod_qty_status is null");
                    //        //    "SELECT SUM(CAST(prod_ok AS INT)) AS Total_Prod_OK, " +
                    //        //    "SUM(CAST(prod_rej AS INT)) AS Total_Prod_Rej " +
                    //        //    "FROM PROD_Barcode_Details " +
                    //        //    "WHERE PROD_Rej_Qty = '" + txtRCNo.Text + "' AND prod_qty_status != 'A'"
                    //        //);

                    //        //  DataTable a = dbFunctions.getTable("select   SUM(CAST(prod_ok AS INT)) AS Total_Prod_OK,    SUM(CAST(prod_rej AS INT)) AS Total_Prod_Rej from PROD_Barcode_Details where PROD_Rej_Qty='" + txtRCNo.Text + '" and prod_qty_status !='A' ");
                    //        // DataTable a = dbFunctions.getTable(" SUM(CAST(prod_ok AS INT)) AS Total_Prod_OK,    SUM(CAST(prod_rej AS INT)) AS Total_Prod_Rej  FROM PROD_Barcode_Details WHERE PROD_Rej_Qty='" + txtRCNo.Text+ "' and prod_qty_status='A' ");
                    //        if (a.Rows.Count > 0)
                    //        {
                    //            textBox4.Text = a.Rows[0]["Total_Prod_OK"].ToString();
                    //            textBox5.Text = a.Rows[0]["Total_Prod_Rej"].ToString();
                    //        }

                    //        // lbl_Prod_Qty.Text = dt.Rows[0]["PROD_Sheet_Length"].ToString();
                    //    }

                    //}
                    ////ID = dt.Rows[0]["Pq_iid"].ToString();
                    //Part_ID = dt.Rows[0]["Pq_iPart_ID"].ToString();
                    //if (dt.Rows[0]["prod_status"].ToString() == "SCANNED")
                    //{

                    //    MessageBox.Show("ALREADY SCANNED", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    aci_vCardNo1.Text = "";

                    //}
                    //else
                    //{
                    //    txtRCNo.Text = dt.Rows[0]["PROD_Rej_Qty"].ToString();
                    //    txtPartNo.Text = dt.Rows[0]["PROD_iItemId"].ToString();
                    //    txtPartName.Text = dt.Rows[0]["PROD_vLotNo"].ToString(); 


                    //    txtModel.Text = dt.Rows[0]["PROD_vShift"].ToString();
                    //    txtxplanQty.Text = dt.Rows[0]["PROD_Sheet_Length"].ToString();
                    //    txtStartDate.Text = dt.Rows[0]["PROD_Sheet_Thickness"].ToString();
                    //    textBox6.Text= dt.Rows[0]["PROD_nQty"].ToString();
                    //    DataTable a = dbFunctions.getTable(" SELECT SUM(CAST(prod_ok AS INT)) AS Total_Prod_OK,SUM(CAST(prod_rej AS INT)) AS Total_Prod_Rej  FROM PROD_Barcode_Details  WHERE PROD_Rej_Qty = '" + txtRCNo.Text + "'  AND prod_qty_status is null");
                    //    //    "SELECT SUM(CAST(prod_ok AS INT)) AS Total_Prod_OK, " +
                    //    //    "SUM(CAST(prod_rej AS INT)) AS Total_Prod_Rej " +
                    //    //    "FROM PROD_Barcode_Details " +
                    //    //    "WHERE PROD_Rej_Qty = '" + txtRCNo.Text + "' AND prod_qty_status != 'A'"
                    //    //);

                    //    //  DataTable a = dbFunctions.getTable("select   SUM(CAST(prod_ok AS INT)) AS Total_Prod_OK,    SUM(CAST(prod_rej AS INT)) AS Total_Prod_Rej from PROD_Barcode_Details where PROD_Rej_Qty='" + txtRCNo.Text + '" and prod_qty_status !='A' ");
                    //   // DataTable a = dbFunctions.getTable(" SUM(CAST(prod_ok AS INT)) AS Total_Prod_OK,    SUM(CAST(prod_rej AS INT)) AS Total_Prod_Rej  FROM PROD_Barcode_Details WHERE PROD_Rej_Qty='" + txtRCNo.Text+ "' and prod_qty_status='A' ");
                    //    if (a.Rows.Count > 0)
                    //    {
                    //        textBox4.Text = a.Rows[0]["Total_Prod_OK"].ToString();
                    //        textBox5.Text = a.Rows[0]["Total_Prod_Rej"].ToString();
                    //    }

                    //    // lbl_Prod_Qty.Text = dt.Rows[0]["PROD_Sheet_Length"].ToString();
                    //}
                    //   CAL1();

                }
                catch
                {
                    MessageBox.Show("Plece check the Barcode Once Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    aci_vCardNo1.Text = "";


                }
                panel3.Visible = true;
            }
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable a = dbFunctions.getTable("update PROD_Barcode_Details set prod_ok ='" + prod_ok.Text + "', prod_rej='" + prod_rej.Text + "',prod_status = 'SCANNED', prod_scanned_date = '" + dbFunctions.getdate() + "',prod_scanned_name = '" + dbFunctions.username + "' where PROD_vBarcodeId='" + aci_vCardNo1.Text + "'");


                MessageBox.Show("Stock Updated ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                panel3.Visible = false;
            }
            catch { }
        }

        private void Prod_ok_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    int a = Convert.ToInt32(textBox4.Text);

            //    int b = Convert.ToInt32(prod_ok.Text);

            //    int c= Convert.ToInt32(textBox6.Text);

            //    textBox6.Text= (c-b).ToString();
            //    textBox4.Text = (a + b).ToString();
            //}
            //catch { }
            try
            {

                int a = Convert.ToInt32(textBox4.Text) - Convert.ToInt32(prod_ok.Tag);
                int b = Convert.ToInt32(prod_ok.Text);

                textBox4.Text = (a + b).ToString();
                prod_ok.Tag = b;  // Store the last entered value of b
            }
            catch { }
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            PD_Reject_Qty.Text = textBox5.Text;
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            PD_OK_Qty.Text = textBox4.Text;
        }

        private void Prod_rej_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //int a = Convert.ToInt32(textBox5.Text);

                //int b = Convert.ToInt32(prod_rej.Text);
                //textBox5.Text = (a + b).ToString();


                int a = Convert.ToInt32(textBox5.Text) - Convert.ToInt32(prod_rej.Tag);
                int b = Convert.ToInt32(prod_rej.Text);

                textBox5.Text = (a + b).ToString();
                prod_rej.Tag = b;  // Store the last entered value of b

            }
            catch { }
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                DataTable dt = dbFunctions.getTable("pr_Update_Final_Inspection_Details '" + dataGridView5.Rows[e.RowIndex].Cells[0].Value.ToString() + "','" + dataGridView5.Rows[e.RowIndex].Cells["OK"].Value.ToString() + "','" + dataGridView5.Rows[e.RowIndex].Cells["Rejection"].Value.ToString() + "','" + dbFunctions.username + "'");
                //DataTable dt = dbFunctions.getTable($"EXEC pr_Update_Production_Details {dataGridView2.Rows[e.RowIndex].Cells[0].Value}, {dataGridView2.Rows[e.RowIndex].Cells[4].Value}, {dataGridView2.Rows[e.RowIndex].Cells[5].Value}, '{Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells["Time1"].Value):yyyy-MM-dd HH:mm:ss}', '{Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells["Time2"].Value):yyyy-MM-dd HH:mm:ss}', '{dbFunctions.username}'");
                //DataTable dt = dbFunctions.getTable($"EXEC pr_Update_Production_Details {dataGridView2.Rows[e.RowIndex].Cells[0].Value}, {dataGridView2.Rows[e.RowIndex].Cells[4].Value}, {dataGridView2.Rows[e.RowIndex].Cells[5].Value}, '{dataGridView2.Rows[e.RowIndex].Cells["Time1"].Value}', '{dataGridView2.Rows[e.RowIndex].Cells["Time2"].Value}', '{dbFunctions.username}'");

                MessageBox.Show("Qty Updated Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getdata();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            panel14.Visible = false;
            display();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load1();
        }
    }
}
