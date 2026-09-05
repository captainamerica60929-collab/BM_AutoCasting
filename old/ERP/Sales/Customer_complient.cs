using Maintanence_Printing_Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM_App.Sales
{
    
    public partial class Customer_complient : Form
    {
        public Customer_complient()
        {
            InitializeComponent();
        }

        private void Customer_complient_Load(object sender, EventArgs e)
        {
            display();
            Loadsupplier();
            LoadPART();
            textBox3.Text = dbFunctions.username;


        }

        private void LoadPART()
        {
            try
            {

                DataTable a = dbFunctions.getTable(" SELECT* FROM Item_Master where im_type = '1' and im_status = 'A' ORDER BY  IM_PartName ASC");
                part_name.DataSource = a;
                part_name.DisplayMember = "IM_PartName";
                part_name.ValueMember = "IM_ID";
                part_name.SelectedIndex = -1;
            }
            catch { }

            try
            {
                DataTable a1 = dbFunctions.getTable(" SELECT * FROM Item_Master where im_type = '1' and im_status = 'A' ORDER BY  IM_PartName ASC");
                part_no.DataSource = a1;
                part_no.DisplayMember = "IM_PartNo";
                part_no.ValueMember = "IM_ID";
                part_no.SelectedIndex = -1;
            }
            catch { }
            
        }

        private void Loadsupplier()
        {
            DataTable a= dbFunctions.getTable("SELECT * FROM Customer_Master  WHERE CM_Status='A' ORDER BY CM_Name ASC  ");
            cs_name.DataSource = a;
            cs_name.DisplayMember = "CM_Name";
            cs_name.ValueMember = "CM_ID";
            cs_name.SelectedIndex = -1;
        }

        private void part_no_SelectedIndexChanged(object sender, EventArgs e)
        {
              DataTable a= dbFunctions.getTable("SELECT * FROM Item_Master  WHERE IM_PartNo='"+ part_no.Text+ "' ");
            if (a.Rows.Count > 0)
            {
                part_name.Text=a.Rows[0]["IM_PartName"].ToString();
                model.Text=a.Rows[0]["IM_Model"].ToString();
            }

            CUMALITYQTY();

        }

        private void CUMALITYQTY()
        {
            try
            {
                DataTable A = dbFunctions.getTable("SELECT SUM(csb_qty) as [Qty] FROM customercomplaint where csb_part_no_id='" + part_no.SelectedValue.ToString() + "' and csb_status='A' AND csb_customer_id='" + cs_name.SelectedValue.ToString() + "'");
                if (A.Rows.Count > 0)
                {
                    //textBox4.Text = A.Rows[0]["Qty"].ToString();
                    textBox4.Text = string.IsNullOrEmpty(A.Rows[0]["Qty"]?.ToString()) ? "0" : A.Rows[0]["Qty"].ToString();

                    // model.Text = a.Rows[0]["IM_Model"].ToString();
                }
            }
            catch { }
        }

        private void part_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable a = dbFunctions.getTable("SELECT * FROM Item_Master  WHERE IM_PartName='" + part_name.Text + "' ");
            if (a.Rows.Count > 0)
            {
                part_no.Text = a.Rows[0]["IM_PartNo"].ToString();
                model.Text = a.Rows[0]["IM_Model"].ToString();
            }
            CUMALITYQTY();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog.Title = "Select an Image";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //  Doc_Photo.Text = openFileDialog.FileName.ToString();
                Person.Image = Image.FromFile(openFileDialog.FileName);
                Person.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Person.Image = null;
        }
        String ID1 = "0";

        private void btnsave_Click(object sender, EventArgs e)
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
                    DataTable dd = dbFunctions.getTable("select isnull(max(csb_id),0)+1 from customercomplaint");
                    if (dd.Rows.Count > 0)
                    {
                        ID1 = dd.Rows[0][0].ToString();
                    }
                    com.CommandText = @"INSERT INTO customercomplaint 
            (csb_id, csb_customer_name, csb_customer_id, csb_part_name, csb_part_name_id, 
             csb_part_no_id, csb_part_no, csb_date, csb_qty, csb_defect_name, csb_image, 
             csb_created_name, csb_created_datetime, csb_status,csb_cumqty) 
            VALUES 
            (@csb_id, @csb_customer_name, @csb_customer_id, @csb_part_name, @csb_part_name_id, 
             @csb_part_no_id, @csb_part_no, @csb_date, @csb_qty, @csb_defect_name, @csb_image, 
             @csb_created_name, GETDATE(), 'A',@csb_cumqty)";
                }
                else
                {
                    com.CommandText = @"UPDATE customercomplaint SET
                    csb_customer_name = @csb_customer_name,
                    csb_customer_id = @csb_customer_id,
                    csb_part_name = @csb_part_name,
                    csb_part_name_id = @csb_part_name_id,
                    csb_part_no_id = @csb_part_no_id,
                    csb_part_no = @csb_part_no,
                    csb_date = @csb_date,
                    csb_qty = @csb_qty,
                    csb_defect_name = @csb_defect_name,
                    csb_image = @csb_image,
                    csb_status = 'A',
                    csb_cumqty=@csb_cumqty
                    WHERE csb_id = @csb_id";
                    //Type = "EDIT";

                    btnsave.Text = "Add";

                }

                com.Parameters.Add("@csb_id", SqlDbType.VarChar).Value = ID1.ToString();
                com.Parameters.Add("@csb_customer_name", SqlDbType.VarChar).Value = cs_name.Text;
                com.Parameters.Add("@csb_customer_id", SqlDbType.Int).Value = cs_name.SelectedValue.ToString();
                com.Parameters.Add("@csb_part_name", SqlDbType.VarChar).Value = part_name.Text;
                com.Parameters.Add("@csb_part_name_id", SqlDbType.Int).Value = part_name.SelectedValue.ToString();
                com.Parameters.Add("@csb_part_no_id", SqlDbType.Int).Value = part_no.SelectedValue.ToString();
                com.Parameters.Add("@csb_part_no", SqlDbType.VarChar).Value = part_no.Text;
                com.Parameters.Add("@csb_date", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                com.Parameters.Add("@csb_qty", SqlDbType.Int).Value = Convert.ToInt32(qty.Text);
                com.Parameters.Add("@csb_defect_name", SqlDbType.VarChar).Value = defect_name.Text; 
                com.Parameters.Add("@csb_cumqty", SqlDbType.Int).Value = Convert.ToInt32(textBox4.Text); 

                byte[] imageData2 = null;
                try
                {
                    MemoryStream ms1 = new MemoryStream();
                    Person.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageData2 = ms1.GetBuffer();
                }
                catch
                {

                }
                com.Parameters.Add("@csb_image", SqlDbType.Image).Value = (object)imageData2;
               // com.Parameters.Add("@csb_image", SqlDbType.VarChar).Value = imagePath; // or SqlDbType.Image if storing binary
                com.Parameters.Add("@csb_created_name", SqlDbType.VarChar).Value = dbFunctions.username;
         




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
            display();
            clear();


            
        }

        private void display()
        {
            string fromDate = dtpFrom.Value.ToString("yyyyMMdd");
            string toDate = todate.Value.ToString("yyyyMMdd");

            DataTable a = dbFunctions.getTable("select csb_id as [ID],csb_customer_name as [Customer Name],csb_part_name as [Part Name],csb_part_no as [Part No],csb_date as [Date],csb_qty as [Qty],csb_defect_name as [Defect Name], csb_image as [Image],csb_cumqty as [CUMULATIVE QTY] from customercomplaint where  csb_status='A' and convert(date,csb_date,113) Between '"+ fromDate + "' and '" + toDate + "'  ");
            dataGridView1.DataSource = a;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void clear()
        {
            cs_name.Text = "";
            part_name.Text = "";
            part_no.Text = "";
            qty.Text = "";
            defect_name.Text = "";
            model.Text = "";
            textBox4.Text = "";
            Person.Image = null;
            dateTimePicker1.Value = DateTime.Now;  // reset to current date
            btnsave.Text = "Add";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = dbFunctions.getTable("select * from customercomplaint where csb_id='" + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + "'");
                ID1 = dt.Rows[0]["csb_id"].ToString();
                cs_name.Text = dt.Rows[0]["csb_customer_name"].ToString();
                part_name.Text = dt.Rows[0]["csb_part_name"].ToString();
                part_no.Text = dt.Rows[0]["csb_part_no"].ToString();
                dateTimePicker1.Text = dt.Rows[0]["csb_date"].ToString();
                qty.Text = dt.Rows[0]["csb_qty"].ToString();
                defect_name.Text = dt.Rows[0]["csb_defect_name"].ToString();
                byte[] imgBytes = (byte[])dt.Rows[0]["csb_image"];
                using (MemoryStream ms = new MemoryStream(imgBytes))
                {
                    Person.Image = Image.FromStream(ms);
                }
                btnsave.Text = "UPDATE";

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable a = dbFunctions.getTable("update customercomplaint set csb_status='D',csb_del_datetime=GETDATE(),csb_del_name='" + dbFunctions.username + "' WHERE csb_id='" + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + "'");
                    MessageBox.Show("Deleted  Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            display();
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Customer Name] like '%{0}%' or [Part Name] like '%{0}%' or [Part No] like '%{0}%'", textBoxX1.Text);
                }


            }
            catch (Exception ex)
            {

            }
        }

        private void todate_ValueChanged(object sender, EventArgs e)
        {
            display();
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            display();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            print_Bill();
        }

        public void print_Bill()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                CRM_App.Crystal.costomercomplient oRpt = new CRM_App.Crystal.costomercomplient();

                string SQlQuery = "Pr_Fetch_customer_complient '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'";
                dbFunctions.printpdf("QC", SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }
    }
}
