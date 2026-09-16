using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;

namespace CRM_App.Production
{
    public partial class Sandblasting_Entry : Form
    {
        public Sandblasting_Entry()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load1();
        }
        string Part_ID = "";
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
                    DataTable dt = dbFunctions.getTable("pr_get_Production_Flittling_Request  '" + dbFunctions.Route_Card_ID + "'");
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
                com.CommandText = "pr_Insert_Production_Sandblasting_Details";

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



                // DataTable a = dbFunctions.getTable("update PROD_Barcode_Details set prod_qty_status='A' Where PROD_Rej_Qty='" + txtRCNo.Text + "' and prod_status='SCANNED'");
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
            DataTable dt = dbFunctions.getTable("pr_Display_Production_SandBlasting_Inspection  " + dbFunctions.Route_Card_ID);
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
        private void getdata()
        {
            panel14.Visible = true;
            try
            {
                dataGridView5.Columns.RemoveAt(6);
            }
            catch { }
            dataGridView5.DataSource = null;
            DataTable dt = dbFunctions.getTable("pr_edit_Sandblasting_Detail  " + dbFunctions.Route_Card_ID);
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
        public string as1 = "";
        private void Edit1()
        {
            DataTable a = dbFunctions.getTable("select * from Production_Rejection where prej_id='" + dataGridView4.SelectedRows[0].Cells[0].Value.ToString() + "'");
            as1 = a.Rows[0]["prej_id"].ToString();
            Rejection.Text = a.Rows[0]["prej_name"].ToString();
            textBox2.Text = a.Rows[0]["prej_qty"].ToString();
        }

        private void clear1()
        {
            ID1 = "0";
            Rejection.Text = "";
            textBox2.Text = "";

        }
        private void loademployee()
        {
            DataTable a = dbFunctions.getTable("select EM_iid,EM_EmployeeName from Employee_Master where EM_Status='A' AND EM_Category != 'Staff'");
            Employee_Name.DataSource = a;
            Employee_Name.DisplayMember = "EM_EmployeeName";
            Employee_Name.ValueMember = "EM_iid";
            Employee_Name.SelectedIndex = -1;

        }
        public string ErrorMessage = "";
        public string ID = "";
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

            //if (decimal.Parse(lbl_Prod_Qty.Text) <= decimal.Parse(label28.Text.Trim()))
            //{
            //    DialogResult result = MessageBox.Show("Are You Sure Want to Close Routecard", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //    if (result == DialogResult.Yes)
            //    {
            //        DataTable dd = dbFunctions.getTable("update Production_Request set    Pq_Route_Card_End_Date=(select Max(FID_Date) from Production_Details_Inspection where FID_Route_Card_ID=" + dbFunctions.Route_Card_ID + "), RC_Status='Closed' where pq_iid=" + dbFunctions.Route_Card_ID);

            //    }

            //}


        }

        private void button12_Click(object sender, EventArgs e)
        {
            save();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            save();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Edit1();
            button9.Text = "&Update";
            ID1 = dataGridView4.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
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

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            panel14.Visible = false;
            display();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 10)
            {
                getdata();
            }
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                DataTable dt = dbFunctions.getTable("pr_Update_Sandblasting_Details '" + dataGridView5.Rows[e.RowIndex].Cells[0].Value.ToString() + "','" + dataGridView5.Rows[e.RowIndex].Cells["OK"].Value.ToString() + "','" + dataGridView5.Rows[e.RowIndex].Cells["Rejection"].Value.ToString() + "','" + dbFunctions.username + "'");
                //DataTable dt = dbFunctions.getTable($"EXEC pr_Update_Production_Details {dataGridView2.Rows[e.RowIndex].Cells[0].Value}, {dataGridView2.Rows[e.RowIndex].Cells[4].Value}, {dataGridView2.Rows[e.RowIndex].Cells[5].Value}, '{Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells["Time1"].Value):yyyy-MM-dd HH:mm:ss}', '{Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells["Time2"].Value):yyyy-MM-dd HH:mm:ss}', '{dbFunctions.username}'");
                //DataTable dt = dbFunctions.getTable($"EXEC pr_Update_Production_Details {dataGridView2.Rows[e.RowIndex].Cells[0].Value}, {dataGridView2.Rows[e.RowIndex].Cells[4].Value}, {dataGridView2.Rows[e.RowIndex].Cells[5].Value}, '{dataGridView2.Rows[e.RowIndex].Cells["Time1"].Value}', '{dataGridView2.Rows[e.RowIndex].Cells["Time2"].Value}', '{dbFunctions.username}'");

                MessageBox.Show("Qty Updated Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getdata();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
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

        private void Sandblasting_Entry_Load(object sender, EventArgs e)
        {
            Load1();
        }

        private void Sandblasting_Entry_Shown(object sender, EventArgs e)
        {
            display();
        }
    }
}
