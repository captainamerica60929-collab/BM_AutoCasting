using Maintanence_Printing_Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM_App.Quality
{
    
    public partial class Final_Inspection_Entry : Form
    {
        public Final_Inspection_Entry()
        {
            InitializeComponent();
        }

        private void Final_Inspection_Entry_Load(object sender, EventArgs e)
        {
            LoadRouteCardNo();
            Load_Part_Number();
            loademployee();
            loadrej();
            Display();
            display1();


        }
        private void loadrej()
        {
            DataTable a = dbFunctions.getTable("select Rej_iid, Rej_vDescription from Rejection_Method_Master where Rej_cStatus = 'A' and Rej_Type='Rejection' order by Rej_vDescription  ASC ");
            Rejection.DataSource = a;
            Rejection.DisplayMember = "Rej_vDescription";
            Rejection.ValueMember = "Rej_iid";
            Rejection.SelectedIndex = -1;
        }
        private void loademployee()
        {
            //DataTable a = dbFunctions.getTable("select EM_iid,EM_EmployeeName from Employee_Master where EM_Status='A' AND EM_Category != 'Staff'");
            //Employee_Name.DataSource = a;
            //Employee_Name.DisplayMember = "EM_EmployeeName";
            //Employee_Name.ValueMember = "EM_iid";
            //Employee_Name.SelectedIndex = -1;

        }
        public void LoadRouteCardNo()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_GenerateFENo");
                if (dt.Rows.Count > 0)
                {
                    rc_no.Text = dt.Rows[0][0].ToString();
                }

            }
            catch
            {
            }
        }
        private void Load_Part_Number()
        {
            try
            {
                //DataTable dt = dbFunctions.getTable("pr_fetch_ItemBoM");
                DataTable dt = dbFunctions.getTable("Pr_Get_PartName_For_FE");
                Pq_vPart_No.DataSource = dt;
                Pq_vPart_No.DisplayMember = "IM_PartName";
                Pq_vPart_No.ValueMember = "IM_ID";
                Pq_vPart_No.SelectedIndex = -1;
                
            }
            catch
            {
            }

            try
            {
                //DataTable dt = dbFunctions.getTable("pr_fetch_ItemBoM");
                DataTable dt = dbFunctions.getTable("Pr_Get_PartName_For_FE");
                Pq_vPart_Name.DataSource = dt;
                Pq_vPart_Name.DisplayMember = "IM_PartNo";
                Pq_vPart_Name.ValueMember = "IM_ID";
                Pq_vPart_Name.SelectedIndex = -1;

            }
            catch
            {
            }
        }

        private void PD_OK_Qty_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

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

        private void Pq_vPart_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_PartName_Details '" + Pq_vPart_No.SelectedValue.ToString() + "'");
                Pq_vPart_Name.Text = dt.Rows[0]["IM_PartNo"].ToString();
                txtModel.Text = dt.Rows[0]["ML_Model"].ToString();
                Stock.Text = dt.Rows[0]["Stock"].ToString();

            }
            catch
            {

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SUBMIT();
            
            LoadRouteCardNo();
            display1();

        }

        private void clear()
        {
            fe_price.Text = "";
            Pq_vPart_No.Text = "";
            Pq_vPart_Name.Text = "";
            txtModel.Text = "";
            Employee_Name.Text = "";
            Stock.Text = "";
            PD_Reject_Qty.Text = "0";
            PD_OK_Qty.Text = "";
            Shift.Text = "";

        }
        public string ErrorMessage = "";

        public bool Validate()
        {
            if ((string.IsNullOrEmpty(Shift.Text.Trim())))
            {
                ErrorMessage = "Shift Should Not be Empty";
                Shift.Focus();
                return true;
            }

            if ((string.IsNullOrEmpty(PD_Reject_Qty.Text.Trim())))
            {
                ErrorMessage = "Reject Qty Should Not be Empty";
                PD_Reject_Qty.Focus();
                return true;
            }

            if ((string.IsNullOrEmpty(PD_OK_Qty.Text.Trim())))
            {
                ErrorMessage = "Ok Qty Should Not be Empty";
                PD_OK_Qty.Focus();
                return true;
            }



            return false;
        }
        public string ID = "0";
        private void SUBMIT()
        {
            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                if (ID == "0")
                {
                    DataTable dd = dbFunctions.getTable("select isnull(max(fe_id),0)+1 from final_entry");
                    if (dd.Rows.Count > 0)
                    {
                        ID = dd.Rows[0][0].ToString();
                    }
                    com.CommandText = "INSERT INTO final_entry (fe_id, fe_rcno, fe_partname, fe_partid, fe_inspecter_name, fe_inspecter_id, fe_Date, fe_Shift, fe_ok, fe_rej, fe_status, fe_create_name, fe_create_date,fe_price) " +
                   "VALUES (@fe_id, @fe_rcno, @fe_partname, @fe_partid, @fe_inspecter_name, @fe_inspecter_id, @fe_Date, @fe_Shift, @fe_ok, @fe_rej, 'A', @fe_create_name, GETDATE(),@fe_price)";
                    //Type = "NEW";
                }
                else
                {
                    com.CommandText = "UPDATE final_entry " +
                   "SET fe_rcno = @fe_rcno, " +
                   "    fe_partname = @fe_partname, " +
                   "    fe_partid = @fe_partid, " +
                   "    fe_inspecter_name = @fe_inspecter_name, " +
                   "    fe_inspecter_id = @fe_inspecter_id, " +
                   "    fe_Date = @fe_Date, " +
                   "    fe_Shift = @fe_Shift, " +
                   "    fe_ok = @fe_ok, " +
                   "    fe_rej = @fe_rej, " +
                   "    fe_status = 'A', " +
                   "    fe_create_name = @fe_create_name, " +
                   "    fe_create_date = GETDATE(),fe_price=@fe_price WHERE fe_id = @fe_id";
                    //Type = "EDIT";
                    button9.Text = "&Update";

                }

                com.Parameters.Add("@fe_id", SqlDbType.Int).Value = ID.ToString();
                com.Parameters.Add("@fe_rcno", SqlDbType.VarChar).Value = rc_no.Text;
                com.Parameters.Add("@fe_partname", SqlDbType.VarChar).Value = Pq_vPart_No.Text;
                com.Parameters.Add("@fe_partid", SqlDbType.Int).Value = Pq_vPart_No.SelectedValue.ToString();
                com.Parameters.Add("@fe_inspecter_name", SqlDbType.VarChar).Value = Employee_Name.Text;
                com.Parameters.Add("@fe_inspecter_id", SqlDbType.Int).Value = 0; 
                com.Parameters.Add("@fe_Date", SqlDbType.DateTime).Value = DateTime.Parse(Date.Text);
                com.Parameters.Add("@fe_Shift", SqlDbType.VarChar).Value = Shift.Text;
                com.Parameters.Add("@fe_price", SqlDbType.VarChar).Value = fe_price.Text;

                com.Parameters.Add("@fe_ok", SqlDbType.Int).Value = PD_OK_Qty.Text;// int.Parse(PD_OK_Qty.Text);
                com.Parameters.Add("@fe_rej", SqlDbType.Int).Value = PD_Reject_Qty.Text;// int.Parse(PD_Reject_Qty.Text);
                //com.Parameters.Add("@fe_status", SqlDbType.VarChar).Value = status.Text;
                com.Parameters.Add("@fe_create_name", SqlDbType.VarChar).Value = dbFunctions.username;








                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ID = "0";


            }
            catch (Exception Ex)
            {
                //dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Display();
            clear();
            button10.Text = "Save";
        }

        private void Display()
        {
            DataTable dis = dbFunctions.getTable("pr_fedisplay_fe '"+ Date .Text+ "'");
            dataGridView1.DataSource = dis;
            dbFunctions.DGVStyle(dataGridView1);
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
                com.Parameters.Add("@prej_rid", SqlDbType.VarChar).Value = rc_no.Text;
                com.Parameters.Add("@prej_rejection", SqlDbType.Int).Value = Rejection.SelectedValue.ToString();
                com.Parameters.Add("@prej_name", SqlDbType.VarChar).Value = Rejection.Text;
                com.Parameters.Add("@prej_qty", SqlDbType.VarChar).Value = textBox2.Text;
                // com.Parameters.Add("@prej_date", SqlDbType.VarChar).Value = DateTime.Now;
                com.Parameters.Add("@pjrej_status", SqlDbType.VarChar).Value = "A";
                com.Parameters.Add("@prej_darstatus", SqlDbType.VarChar).Value = "M";



                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ID1 = "0";


            }
            catch (Exception Ex)
            {
                //dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            display1();
            button9.Text = "Save";
            Rejection.Text = "";
            textBox2.Text = "";
            //clear1();
        }

        private void display1()
        {
            DataTable dis = dbFunctions.getTable("pr_RJdisplay_fe '"+ rc_no.Text+ "'");
            dataGridView4.DataSource = dis;
            dbFunctions.DGVStyle(dataGridView4);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            save();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataTable a = dbFunctions.getTable("select * from final_entry where fe_id='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");
            ID = a.Rows[0]["fe_id"].ToString();
            rc_no.Text = a.Rows[0]["fe_rcno"].ToString();
            Pq_vPart_No.Text = a.Rows[0]["fe_partname"].ToString();
            Employee_Name.Text = a.Rows[0]["fe_inspecter_name"].ToString();
            Date.Text = a.Rows[0]["fe_Date"].ToString();
            Shift.Text = a.Rows[0]["fe_Shift"].ToString();
            PD_OK_Qty.Text = a.Rows[0]["fe_ok"].ToString();
            PD_Reject_Qty.Text = a.Rows[0]["fe_rej"].ToString();
            fe_price.Text = a.Rows[0]["fe_price"].ToString();
            

            button10.Text = "Update";


        }

        private void button6_Click(object sender, EventArgs e)
        {
            Edit1();
           
        }
        private void Edit1()
        {
            DataTable a = dbFunctions.getTable("select * from Production_Rejection where prej_id='" + dataGridView4.SelectedRows[0].Cells[0].Value.ToString() + "'");
            ID1 = a.Rows[0]["prej_id"].ToString();
            Rejection.Text = a.Rows[0]["prej_name"].ToString();
            textBox2.Text = a.Rows[0]["prej_qty"].ToString();

            button9.Text = "Update";

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {


                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("update final_entry set fe_status='D', fe_del_name='"+dbFunctions.username+ "',fe_del_date=GETDATE() WHERE fe_id='" + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString()+"'");
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Display();
                   
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (dataGridView4.SelectedRows.Count > 0)
            {


                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("update Production_Rejection set pjrej_status='D' WHERE prej_id='" + dataGridView4.SelectedRows[0].Cells["ID"].Value.ToString() + "'");
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    display1(); 

                }

            }

        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxX2.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Part Name] LIKE '%{0}%'", textBoxX2.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Pq_vPart_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_get_PartName_Details '" + Pq_vPart_Name.SelectedValue.ToString() + "'");
                Pq_vPart_No.Text = dt.Rows[0]["IM_PartName"].ToString();
                txtModel.Text = dt.Rows[0]["ML_Model"].ToString();
                Stock.Text = dt.Rows[0]["Stock"].ToString();

            }
            catch
            {

            }
        }

        private void Date_ValueChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }
    }
}
