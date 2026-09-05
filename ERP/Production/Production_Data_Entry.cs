using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using Shasun_Printing_Toll.Masters;

namespace CRM_App.Production
{
    public partial class Production_Data_Entry : Form
    {
        string LabelBarCodeID = "SMFAA00000";
        public string ErrorMessage = "";
        public string ID = "";
        public Production_Data_Entry()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Production_Data_Entry_Load(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel14.Visible = false;
            loadrej();



            PD_Date.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            PD_CreatedBy.Text = dbFunctions.username;
            Shift.Text = "Shift I";
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_Production_Request  '" + dbFunctions.Route_Card_ID + "'");
                //ID = dt.Rows[0]["Pq_iid"].ToString();

                txtRCNo.Text = dt.Rows[0]["Pq_Route_Card_No"].ToString();
                txtPartNo.Text = dt.Rows[0]["Pq_vPart_No"].ToString();
                txtPartName.Text = dt.Rows[0]["Pq_vPart_Name"].ToString();
                txtModel.Text = dt.Rows[0]["Pq_vModel"].ToString();
                txtxplanQty.Text = dt.Rows[0]["Pq_RM_Plan_Qty"].ToString();
                txtStartDate.Text = dt.Rows[0]["StartDate"].ToString();

                PD_Date.MinDate = DateTime.Parse(dt.Rows[0]["StartDate"].ToString());
            }
            catch { }
            // loademployee();

            Barcodeqty();
            idle();
        }

        private void Barcodeqty()
        {
            DataTable a = dbFunctions.getTable("select SUM(CASE  WHEN ISNUMERIC(PROD_nQty) = 1 THEN CAST(PROD_nQty AS INT) ELSE 0  END) AS qty  FROM PROD_Barcode_Details WHERE PROD_Rej_Qty ='" + txtRCNo.Text + "' ");
            if (a.Rows.Count > 0)
            {
                Production_time.Text = a.Rows[0]["qty"].ToString();
            }
        }

        private void idle()
        {
            DataTable a = dbFunctions.getTable("select Rej_iid, Rej_vDescription from Rejection_Method_Master where Rej_cStatus = 'A' and Rej_Type='Idle' order by Rej_vDescription asc ");
            comboBox2.DataSource = a;
            comboBox2.DisplayMember = "Rej_vDescription";
            comboBox2.ValueMember = "Rej_iid";
            comboBox2.SelectedIndex = -1;
        }
        private void loademployee()
        {
            DataTable a = dbFunctions.getTable("select EM_iid,EM_EmployeeName from Employee_Master where EM_Status='A' AND EM_Designation = 'Operator'");
            //Employee_Name.DataSource = a;
            //Employee_Name.DisplayMember = "EM_EmployeeName";
            //Employee_Name.ValueMember = "EM_iid";
            //Employee_Name.SelectedIndex = -1;

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
            if ((string.IsNullOrEmpty(pd_lumps.Text.Trim())))
            {
                ErrorMessage = "lumps Should Not be Empty";
                pd_lumps.Focus();
                return true;
            }
          


            //if ((decimal.Parse(PD_OK_Qty.Text) + decimal.Parse(PD_Reject_Qty.Text) + decimal.Parse(label28.Text)) > decimal.Parse(txtxplanQty.Text))
            //{
            //    ErrorMessage = "Production Qty is more than Plan Qty";
            //    PD_Reject_Qty.Focus();
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
                com.CommandText = "pr_Insert_Production_Details";

                com.Parameters.Add("@PD_Route_Card_ID", SqlDbType.VarChar).Value = dbFunctions.Route_Card_ID;
                com.Parameters.Add("@PD_Date", SqlDbType.DateTime).Value = PD_Date.Text.ToString();
                com.Parameters.Add("@PD_CreatedBy", SqlDbType.VarChar).Value = PD_CreatedBy.Text.ToString();
                com.Parameters.Add("@PD_Shift", SqlDbType.VarChar).Value = Shift.Text.ToString();
                com.Parameters.Add("@PD_OK_Qty", SqlDbType.Int).Value = PD_OK_Qty.Text.ToString();
                com.Parameters.Add("@PD_Reject_Qty", SqlDbType.Int).Value = PD_Reject_Qty.Text.ToString();
                //com.Parameters.Add("@PD_EM_id", SqlDbType.Int).Value = Employee_Name.SelectedValue.ToString();
                com.Parameters.Add("@PD_EM_name", SqlDbType.VarChar).Value = Employee_Name.Text.ToString();
                //com.Parameters.Add("@PD_time", SqlDbType.VarChar).Value = Production_time.Text.ToString();
                com.Parameters.Add("@pd_time1", SqlDbType.DateTime).Value = pdtime12.Text.ToString();
                com.Parameters.Add("@pd_time2", SqlDbType.DateTime).Value = pdtime13.Text.ToString();
                com.Parameters.Add("@pd_lumps", SqlDbType.VarChar).Value = pd_lumps.Text.ToString();
                com.Parameters.Add("@pd_cavity", SqlDbType.Int).Value = textBox6.Text.ToString();


                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                idl = false;
                Clear();
                display();
                textBox4.Enabled = false;
                textBox5.Enabled = false;

            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Clear()
        {
            textBox1.Text = "";
            PD_OK_Qty.Text = "";
            PD_Reject_Qty.Text = "";
            pd_lumps.Text = "";
            Employee_Name.Text = "";
            textBox6.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            // Production_time.Text = "";
        }

        public void display()
        {
            DataTable dt = dbFunctions.getTable("pr_Display_Production_Route_Card  " + dbFunctions.Route_Card_ID);
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

            label26.Text = (OK).ToString("0");
            label27.Text = (Rejection).ToString("0");
            label28.Text = (OK + Rejection).ToString("0");

            label33.Text = (decimal.Parse(txtxplanQty.Text) - (OK + Rejection)).ToString("0");

            label31.Text = (decimal.Parse(txtxplanQty.Text)).ToString("0");
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
        Boolean idl = false;
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
                if (idl)
                {
                    insert();
                }
                else
                {
                    MessageBox.Show("Please enter the IDL reason", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                getdata();
            }
        }


        public void getdata()
        {
            panel3.Visible = true;
            try
            {
                dataGridView2.Columns.RemoveAt(6);
            }
            catch { }
            dataGridView2.DataSource = null;
            DataTable dt = dbFunctions.getTable("pr_edit_Production_Detail  " + dbFunctions.Route_Card_ID);
            dataGridView2.DataSource = dt;
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView2);
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            //dataGridView2.Columns[1].ReadOnly = true;
            //dataGridView2.Columns[2].ReadOnly = true;
            dataGridView2.Columns[1].Width = 100;
            dataGridView2.Columns[2].Width = 100;
            dataGridView2.Columns[5].Width = 100;
            dataGridView2.Columns[3].Width = 100;
            dataGridView2.Columns[4].Width = 100;
            //dataGridView2.Columns["Time1"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            //dataGridView2.Columns["Time2"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            dataGridView2.Columns["Time1"].DefaultCellStyle.Format = "dd-MM-yyyy hh:mm tt";
            dataGridView2.Columns["Time2"].DefaultCellStyle.Format = "dd-MM-yyyy hh:mm tt";


            DataGridViewButtonColumn doWork = new DataGridViewButtonColumn();
            doWork.HeaderText = " ";
            doWork.Text = "Update";
            dataGridView2.Columns.Insert(8, doWork);
            dataGridView2.Columns[8].Width = 93;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].Cells[8].Value = "Update";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            display();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                DataTable dt = dbFunctions.getTable(
                "pr_Update_Production_Details '"
                + dataGridView2.Rows[e.RowIndex].Cells["ID"].Value.ToString() + "','"
                + dataGridView2.Rows[e.RowIndex].Cells["Date"].Value.ToString() + "','"
                + dataGridView2.Rows[e.RowIndex].Cells["Shift"].Value.ToString() + "','"
                + dataGridView2.Rows[e.RowIndex].Cells["OK"].Value.ToString() + "','"
                + dataGridView2.Rows[e.RowIndex].Cells["Rejection"].Value.ToString() + "','"
                + dbFunctions.username + "','"
                + Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells["Time1"].Value).ToString("dd-MM-yyyy HH:mm:ss") + "','"
                + Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells["Time2"].Value).ToString("dd-MM-yyyy HH:mm:ss") + "'"
            );

                //DataTable dt = dbFunctions.getTable("pr_Update_Production_Details '" + dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString() + "','" + dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString() + "','" + dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString() + "','" + dbFunctions.username + "','" + dataGridView2.Rows[e.RowIndex].Cells["Time1"].Value.ToString() + "','" + dataGridView2.Rows[e.RowIndex].Cells["Time2"].Value.ToString() + "'");
                //DataTable dt = dbFunctions.getTable("pr_Update_Production_Details '" + dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString() + "','" + dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString() + "','" + dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString() + "','" + dbFunctions.username + "','" + Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells["Time1"].Value):yyyy - MM - dd HH: mm: ss + "','" + Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells["Time2"].Value):yyyy - MM - dd HH: mm: ss + "'");
                //DataTable dt = dbFunctions.getTable($"EXEC pr_Update_Production_Details {dataGridView2.Rows[e.RowIndex].Cells[0].Value}, {dataGridView2.Rows[e.RowIndex].Cells[4].Value}, {dataGridView2.Rows[e.RowIndex].Cells[5].Value}, '{Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells["Time1"].Value):yyyy-MM-dd HH:mm:ss}', '{Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells["Time2"].Value):yyyy-MM-dd HH:mm:ss}', '{dbFunctions.username}'");
                //DataTable dt = dbFunctions.getTable($"EXEC pr_Update_Production_Details {dataGridView2.Rows[e.RowIndex].Cells[0].Value}, {dataGridView2.Rows[e.RowIndex].Cells[4].Value}, {dataGridView2.Rows[e.RowIndex].Cells[5].Value}, '{dataGridView2.Rows[e.RowIndex].Cells["Time1"].Value}', '{dataGridView2.Rows[e.RowIndex].Cells["Time2"].Value}', '{dbFunctions.username}'");

                MessageBox.Show("Updated Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getdata();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                PD_OK_Qty.Text = (decimal.Parse(textBox1.Text) - decimal.Parse(PD_Reject_Qty.Text)).ToString("0");
               
            }

            catch { }
        }

        private void PD_Reject_Qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                PD_OK_Qty.Text = (decimal.Parse(textBox1.Text) - decimal.Parse(PD_Reject_Qty.Text)).ToString("0");
 
            }

            catch { }
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


        private void Employee_Name_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        string ID1 = "0";
        private void Button9_Click(object sender, EventArgs e)
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
                    com.CommandText = "insert into Production_Rejection (prej_id,prej_rid,prej_rejection,prej_name,prej_qty,pjrej_status,prej_darstatus,prej_date,prej_shift,prej_prodate) Values (@prej_id,@prej_rid,@prej_rejection,@prej_name,@prej_qty,@pjrej_status,@prej_darstatus,getdate(),@prej_shift,@prej_prodate)";
                    //Type = "NEW";
                }
                else
                {
                    com.CommandText = "update  Production_Rejection Set prej_rid=@prej_rid,prej_rejection=@prej_rejection,prej_name=@prej_name,prej_qty=@prej_qty,pjrej_status=@pjrej_status,prej_darstatus=@prej_darstatus,prej_date=getdate(),prej_shift=@prej_shift,prej_prodate=@prej_prodate where prej_id=@prej_id ";
                    //Type = "EDIT";

                }

                com.Parameters.Add("@prej_id", SqlDbType.VarChar).Value = ID1.ToString();
                com.Parameters.Add("@prej_rid", SqlDbType.VarChar).Value = dbFunctions.Route_Card_ID;
                com.Parameters.Add("@prej_rejection", SqlDbType.Int).Value = Rejection.SelectedValue.ToString();
                com.Parameters.Add("@prej_name", SqlDbType.VarChar).Value = Rejection.Text;
                com.Parameters.Add("@prej_qty", SqlDbType.VarChar).Value = textBox2.Text;
                com.Parameters.Add("@prej_shift", SqlDbType.VarChar).Value = Shift.Text; 
                com.Parameters.Add("@prej_prodate", SqlDbType.DateTime).Value = PD_Date.Text.ToString(); 
                // com.Parameters.Add("@prej_date", SqlDbType.VarChar).Value = DateTime.Now;
                com.Parameters.Add("@pjrej_status", SqlDbType.VarChar).Value = "A"; 
                com.Parameters.Add("@prej_darstatus", SqlDbType.VarChar).Value = "p";



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
            //DataTable dis = dbFunctions.getTable("Select prej_id as [ID],PD_Shift as [Shft],format(PD_Date,'dd-mm-yyyyy') as [Date], prej_name as [Rejection Name],prej_qty as [Qty] from Production_Rejection left outer join Production_Details on PD_Route_Card_ID=prej_id where prej_rid='" + dbFunctions.Route_Card_ID + "' AND  pjrej_status='A' and prej_darstatus='p'");
            DataTable dis = dbFunctions.getTable(@"
                        SELECT
                            prej_id AS[ID],
                            prej_shift AS[Shft],
                            FORMAT(prej_prodate, 'dd-MM-yyyy') AS[Date],
                            prej_name AS[Rejection Name],
                            prej_qty AS[Qty]
                        FROM
                            Production_Rejection PR
                  
                        WHERE
                            prej_rid = '" + dbFunctions.Route_Card_ID + @"'
                            AND pjrej_status = 'A'
                            AND prej_darstatus = 'p' order by prej_prodate asc
                        ");
            dataGridView3.DataSource = dis;
            dataGridView3.Columns["ID"].Visible = false;
        }

        private void clear1()
        {
            ID1 = "0";
            Rejection.Text = "";
            textBox2.Text = "";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            LarchERP.Master.RejectionMethod new1 = new LarchERP.Master.RejectionMethod();
            new1.Show();
        }
        private void loadrej()
        {
            DataTable a = dbFunctions.getTable("select Rej_iid, Rej_vDescription from Rejection_Method_Master where Rej_cStatus = 'A' and Rej_Type='Rejection' order by Rej_vDescription asc ");
            Rejection.DataSource = a;
            Rejection.DisplayMember = "Rej_vDescription";
            Rejection.ValueMember = "Rej_iid";
            Rejection.SelectedIndex = -1;
        }
        private void Rejection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable dt = dbFunctions.getTable("Pr_Rej_CheckPointRefresh");
            //    Rejection.DataSource = dt;
            //    Rejection.DisplayMember = "Rej_vDescription";
            //    Rejection.ValueMember = "Rej_iid";
            //    Rejection.SelectedIndex = 0;

            //}
            //catch
            //{
            //}
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button11_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Rej_CheckPointRefresh1");
                Rejection.DataSource = dt;
                Rejection.DisplayMember = "Rej_vDescription";    
                Rejection.ValueMember = "Rej_iid";
                Rejection.SelectedIndex = 0;

            }
            catch
            {
            }
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            save1();

        }
        public string ID2 = "0";
        private void save1()
        {

            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;

                if (ID2 == "0")
                {
                    // Generate a new idl_id
                    DataTable dd = dbFunctions.getTable("SELECT ISNULL(MAX(idl_id), 0) + 1 FROM Production_idl_resons");
                    if (dd.Rows.Count > 0)
                    {
                        ID2 = dd.Rows[0][0].ToString();
                    }

                    com.CommandText = "INSERT INTO Production_idl_resons (idl_id, idl_rcard, idl_fromtime, idl_totime, idl_createddate, idl_status, idl_createdname, idl_resons, idl_resonname, idl_shift,pro_date) " +
                                      "VALUES (@idl_id, @idl_rcard, @idl_fromtime, @idl_totime, @idl_createddate, @idl_status, @idl_createdname, @idl_resons, @idl_resonname, @idl_shift,@pro_date)";
                    // Type = "NEW";
                }
                else
                {
                    com.CommandText = "UPDATE Production_idl_resons SET idl_rcard=@idl_rcard, idl_fromtime=@idl_fromtime, idl_totime=@idl_totime, idl_createddate=@idl_createddate, " +
                                      "idl_status=@idl_status, idl_createdname=@idl_createdname, idl_resons=@idl_resons, idl_resonname=@idl_resonname, idl_shift=@idl_shift,pro_date=@pro_date " +
                                      "WHERE idl_id=@idl_id";
                    button16.Text = "&SAVE";
                    // Type = "EDIT";
                }

                // Add parameters
                com.Parameters.Add("@idl_id", SqlDbType.VarChar).Value = ID2.ToString();
                com.Parameters.Add("@idl_rcard", SqlDbType.VarChar).Value = dbFunctions.Route_Card_ID;
                com.Parameters.Add("@idl_shift", SqlDbType.VarChar).Value = Shift.Text;
                com.Parameters.Add("@idl_fromtime", SqlDbType.DateTime).Value = dateTimePicker3.Value.ToString("dd/MM/yyyy h:mm tt");
                com.Parameters.Add("@idl_totime", SqlDbType.DateTime).Value = dateTimePicker4.Value.ToString("dd/MM/yyyy h:mm tt");
                com.Parameters.Add("@idl_createddate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd/MM/yyyy h:mm tt");
                com.Parameters.Add("@idl_status", SqlDbType.VarChar).Value = "A";
                com.Parameters.Add("@idl_createdname", SqlDbType.VarChar).Value = dbFunctions.username;
                com.Parameters.Add("@idl_resons", SqlDbType.Int).Value = int.Parse(comboBox2.SelectedValue.ToString());
                com.Parameters.Add("@idl_resonname", SqlDbType.VarChar).Value = comboBox2.Text;
                com.Parameters.Add("@pro_date", SqlDbType.DateTime).Value = PD_Date.Value.ToString("dd/MM/yyyy h:mm tt");



                // Execute the query
                com.ExecuteNonQuery();
                con.Close();
                idl = true;
                MessageBox.Show("Details Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateTimePicker4.Value = DateTime.Now;
                dateTimePicker3.Value = DateTime.Now;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            display2();
            clear2();


        }

        private void display2()
        {
           // DataTable dis = dbFunctions.getTable("Select idl_id as [ID],idl_shift as [Shft],format(idl_fromtime,'dd-mm-yyyyy') as [Date], idl_resonname as [Rejection Name],idl_fromtime as [From Time],idl_totime as [To Time] from Production_idl_resons left outer join Production_Details on PD_Route_Card_ID=idl_rcard where idl_rcard='" + dbFunctions.Route_Card_ID + "' AND  idl_status='A'");
            DataTable dis = dbFunctions.getTable(@"
                        SELECT
                           idl_id as [ID], idl_shift as [Shft], format(pro_date, 'dd-MM-yyyy') as [Date], idl_resonname as [Rejection Name], idl_fromtime as [From Time],
                        idl_totime as [To Time]
                        FROM
                            Production_idl_resons PR
                        
                        WHERE
                            idl_rcard ='" + dbFunctions.Route_Card_ID + @"'
                            AND idl_status = 'A' order by pro_date asc ");
            dataGridView4.DataSource = dis;
            dataGridView4.Columns["ID"].Visible = false;
        }

        private void clear2()
        {
            ID2 = "0";
        }
        public string as1 = "";
        private void Button14_Click(object sender, EventArgs e)
        {
            DataTable a = dbFunctions.getTable("select * from Production_idl_resons where idl_id='" + dataGridView4.SelectedRows[0].Cells[0].Value.ToString() + "'");
            as1 = a.Rows[0]["idl_id"].ToString();
            comboBox2.Text = a.Rows[0]["idl_resonname"].ToString();
            dateTimePicker3.Text = a.Rows[0]["idl_fromtime"].ToString();
            dateTimePicker4.Text = a.Rows[0]["idl_totime"].ToString();

            button16.Text = "&Update";
            ID2 = dataGridView4.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("update Production_idl_resons set idl_status='D' where idl_id='" + dataGridView4.SelectedRows[0].Cells[0].Value.ToString() + "'");
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    display2();
                    //clear1();

                }
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            panel14.Visible = false;
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            LarchERP.Master.RejectionMethod new1 = new LarchERP.Master.RejectionMethod();
            new1.Show();
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Rej_CheckPointRefresh2");
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "Rej_vDescription";
                comboBox2.ValueMember = "Rej_iid";
                comboBox2.SelectedIndex = 0;

            }
            catch
            {
            }
        }

        private void Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {

            Edit1();
            button9.Text = "&Update";
            ID1 = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
        }
        private void Edit1()
        {

            DataTable a = dbFunctions.getTable("select * from Production_Rejection where prej_id='" + dataGridView3.SelectedRows[0].Cells[0].Value.ToString() + "'");
            as1 = a.Rows[0]["prej_id"].ToString();
            Rejection.Text = a.Rows[0]["prej_name"].ToString();
            textBox2.Text = a.Rows[0]["prej_qty"].ToString();


        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("update Production_Rejection set pjrej_status='D'   where prej_id='" + dataGridView3.SelectedRows[0].Cells[0].Value.ToString() + "'");
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

        private void Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button20_Click(object sender, EventArgs e)
        {
            // Checkvalidaction();
            int a = 0; // Default value if input is empty or invalid
            if (!string.IsNullOrWhiteSpace(Production_time.Text))
            {
                int.TryParse(Production_time.Text, out a);
            }
            int Count = Convert.ToInt32(txtCount.Text);
            int qty = Convert.ToInt32(pr_nQty.Text);

            int counttotal = (Count * qty);

            int total = (a + counttotal);

            // int okqty = int.Parse(dataGridView1.SelectedRows[0].Cells["OK QTY"].Value.ToString());

            int okqty = 0;
            DataTable A1 = dbFunctions.getTable("SELECT SUM(PD_OK_Qty) AS [QTY] FROM Production_Details WHERE  PD_Route_Card_ID='" + txtRCNo.Text + "'");
            if (A1.Rows.Count > 0)
            {
                
                okqty = Convert.ToInt32(A1.Rows[0]["QTY"]);
            }

            if (okqty < total)
            {
                MessageBox.Show("OK QTY IS MORE THAN YOUR PRINT QTY", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;


            }
            else 
            {





                for (int i = 0; i < int.Parse(txtCount.Text.ToString()); i++)
                {

                    string sqlText = "pr_Fetch_PROD_Top_Barcode";
                    DataTable dtLabelDetails = dbFunctions.getTable(sqlText);
                    if ((dtLabelDetails.Rows.Count > 0))
                    {
                        LabelBarCodeID = dtLabelDetails.Rows[0]["PROD_vBarcodeId"].ToString();
                    }
                    LabelBarCodeID = NextCode(LabelBarCodeID);

                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "pr_InsertPROD_PrintedDetails";
                        com.Parameters.Add("@PROD_vBarcodeId", SqlDbType.VarChar).Value = LabelBarCodeID.ToString();
                        com.Parameters.Add("@PROD_iItemId", SqlDbType.VarChar).Value = txtPartName.Text.ToString();
                        com.Parameters.Add("@PROD_vLotNo", SqlDbType.VarChar).Value = txtPartNo.Text.ToString();
                        com.Parameters.Add("@PROD_nQty", SqlDbType.VarChar).Value = pr_nQty.Text.ToString();

                        com.Parameters.Add("@PROD_vShift", SqlDbType.VarChar).Value = txtModel.Text.ToString();
                        com.Parameters.Add("@PROD_Rej_Qty", SqlDbType.VarChar).Value = txtRCNo.Text.ToString();   // ROUTE CARD NUMBER
                        com.Parameters.Add("@PROD_Sheet_Thickness", SqlDbType.VarChar).Value = txtStartDate.Text.ToString();
                        com.Parameters.Add("@PROD_Sheet_Length", SqlDbType.VarChar).Value = txtxplanQty.Text.ToString();
                        com.Parameters.Add("@PROD_Shift", SqlDbType.VarChar).Value = Shift.Text.ToString();
                        com.Parameters.Add("@PROD_date", SqlDbType.VarChar).Value = PD_Date.Text.ToString();
                        com.Parameters.Add("@prod_produqty", SqlDbType.VarChar).Value = label28.Text.ToString();

                        com.ExecuteNonQuery();
                        com.Connection.Close();


                        string text = System.IO.File.ReadAllText(@"" + dbFunctions.FMB);

                        string ReplaceText1 = text.Replace("@Partname@", txtModel.Text.ToString());
                        string ReplaceText2 = ReplaceText1.Replace("@model@", txtPartName.Text.ToString());
                        string ReplaceText3 = ReplaceText2.Replace("@PartNo@", txtPartNo.Text.ToString());
                        //string ReplaceText4 = ReplaceText3.Replace("@Invoice No@", DateTime.Now.ToString("ddMMyy") + dbFunctions.ntnShift);
                        string ReplaceText4 = ReplaceText3.Replace("@Quantity@", pr_nQty.Text.ToString());
                        //string ReplaceText6 = ReplaceText5.Replace("@ReceivedDate@", txtModel.Value.ToString("dd/MM/yyyy"));
                        //string ReplaceText7 = ReplaceText6.Replace("@Exp. Date@", cmb_dExpieryDate.Value.ToString("dd/MM/yyyy"));
                        string ReplaceText5 = ReplaceText4.Replace("@barcode@", LabelBarCodeID.ToString());
                        string ReplaceText6 = ReplaceText5.Replace("@Shift@", Shift.Text.ToString());
                        string ReplaceText7 = ReplaceText6.Replace("@PD_Date@", PD_Date.Text.ToString()); 
                        string ReplaceText8 = ReplaceText7.Replace("@R.no@", dbFunctions.Route_Card_ID); 
                        //string ReplaceText9 = ReplaceText8.Replace("@Date & Time@", DateTime.Now.ToString("dd/MM/yy hh:mm:ss tt"));
                        //string ReplaceText10 = ReplaceText9.Replace("@Manf. Date@", fmb_MfgDate.Value.ToString("dd/MM/yyyy"));
                        //string ReplaceText11 = ReplaceText10.Replace("@barcode@", LabelBarCodeID.ToString());
                        //string ReplaceText12 = ReplaceText11.Replace("@Shift@", cbShift.Text.ToString());
                        //string ReplaceText13 = ReplaceText12.Replace("@Thick@", cmb_Sheet_Thickness.Text.ToString());
                        //string ReplaceText14 = ReplaceText13.Replace("@Length@", cmb_Sheet_Length.Text.ToString());
                        //string ReplaceText15 = ReplaceText14.Replace("@SuLot@", cmb_Supplier_Lot.Text.ToString());
                        //string ReplaceText16 = ReplaceText15.Replace("@Timing@", cmb_Batch_Timing.Text.ToString());
                        //string ReplaceText17 = ReplaceText16.Replace("@PartName@", txtDescription2.Text.ToString());

                        //string ReplaceText10 = ReplaceText9.Replace("@Supplier@", dataGridView1.Rows[i].Cells["Heat Code"].Value.ToString());

                        RawPrinterHelper.SendStringToPrinter(dbFunctions.Printer_Name, ReplaceText8);
                    }
                    catch (Exception Ex)
                    {
                        dbFunctions.Logs(Ex.Message, dbFunctions.username);
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Checkvalidaction()
        {
            int a = Convert.ToInt32(Production_time.Text);
            int Count = Convert.ToInt32(txtCount.Text);
            int qty = Convert.ToInt32(pr_nQty.Text);

            int counttotal = (Count * qty);

            int total = (a + counttotal);

            // int okqty = int.Parse(dataGridView1.SelectedRows[0].Cells["OK QTY"].Value.ToString());

            int okqty = 0;
            DataTable A1 = dbFunctions.getTable("SELECT SUM(PD_OK_Qty) AS [QTY] FROM Production_Details WHERE  PD_Route_Card_ID='"+ txtRCNo.Text+"' ");
            if (A1.Rows.Count > 0)
            {
                //okqty = A1.Rows[0]["QTY"].ToString();
                okqty = Convert.ToInt32(A1.Rows[0]["QTY"]);
            }            

            if (okqty < total)
            {
                MessageBox.Show("OK QTY IS MORE THAN YOUR PRINT QTY", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;


            }

          

        }

        private string NextCode(string LabelBarCodeID)
        {
            int barN0 = int.Parse(LabelBarCodeID.Substring(6));
            string barAlfa = LabelBarCodeID.Substring(3, 3);
            string alfabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            barN0 = (barN0 + 1);
            char[] alfa = barAlfa.ToCharArray();
            if ((barN0 == 10000))
            {
                barN0 = 0;
                if ((alfa[2] == 'Z'))
                {
                    alfa[2] = 'A';
                    if ((alfa[1] == 'Z'))
                    {
                        alfa[1] = 'A';
                        if ((alfa[0] == 'Z'))
                        {
                            alfa[0] = 'A';
                        }
                        else
                        {
                            alfa[0] = Convert.ToChar(alfabets.Substring(alfabets.IndexOf(alfa[0].ToString()) + 1, 1));
                        }
                    }
                    else
                    {
                        alfa[1] = Convert.ToChar(alfabets.Substring(alfabets.IndexOf(alfa[1].ToString()) + 1, 1));
                    }
                }
                else
                {
                    alfa[2] = Convert.ToChar(alfabets.Substring(alfabets.IndexOf(alfa[2].ToString()) + 1, 1));
                }

            }

            return "SMF" + (alfa[0].ToString() + alfa[1].ToString() + alfa[2].ToString() + barN0.ToString("0000"));

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            cal();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            cal();
        }

        private void cal()
        {
            int value4 = string.IsNullOrWhiteSpace(textBox4.Text) ? 0 : Convert.ToInt32(textBox4.Text);
            int value6 = string.IsNullOrWhiteSpace(textBox6.Text) ? 0 : Convert.ToInt32(textBox6.Text);
            int value5 = string.IsNullOrWhiteSpace(textBox5.Text) ? 0 : Convert.ToInt32(textBox5.Text);

            textBox1.Text = (value4 * value6).ToString();
            PD_Reject_Qty.Text = (value5).ToString();


        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            cal();
        }
    }
}
