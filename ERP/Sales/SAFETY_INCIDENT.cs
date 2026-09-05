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
    public partial class SAFETY_INCIDENT : Form
    {
        public SAFETY_INCIDENT()
        {
            InitializeComponent();
        }

        private void SAFETY_INCIDENT_Load(object sender, EventArgs e)
        {
            label10.Text = "REPORTED BY/DESIG-\nNATION/CONTACT NO :";
            label6.Text = "CORRECTIVE/\n PREVENTIVE ACTION TAKEN:";
            label3.Text = "PEOPLE INVOLVED NAME/\nDESIGNATION/ CONTACT NO :";
            label12.Text = "HOSPITALIZATION\n REQUIRED :";
            label7.Text = "DETAILS \n& DESCRIPTION :";
            label17.Text = "WITNESS  \nSTATEMENT :";
            label6.Text = "CORRECTIVE /\nPREVENTIVE \nACTION TAKEN :";

            LOADEMPLOYEE();
            loadno();

        }

        private void loadno()
        {
            DataTable A = dbFunctions.getTable(@"
                   WITH NumberedIncidents AS (
    SELECT
        *,
        ROW_NUMBER() OVER(PARTITION BY CAST(si_created_datetime AS DATE) ORDER BY si_id) + 1 AS RowNum
    FROM SAFETY_INCIDENT
    WHERE CAST(si_created_datetime AS DATE) = CAST(GETDATE() AS DATE)
)
SELECT
    RIGHT('000' + CAST(RowNum AS VARCHAR), 3) + '&' +
    FORMAT(CAST(si_created_datetime AS DATE), 'dd-MM-yyyy') AS si_reportno
FROM NumberedIncidents
ORDER BY si_created_datetime DESC"); // Optional: latest on top

//            WITH NumberedIncidents AS(
//    SELECT
//        *,
//        ROW_NUMBER() OVER(PARTITION BY CAST(si_created_datetime AS DATE) ORDER BY si_id) AS RowNum
//    FROM SAFETY_INCIDENT
//    WHERE CAST(si_created_datetime AS DATE) = CAST(GETDATE() AS DATE)
//)
//SELECT
//    RIGHT('000' + CAST(RowNum AS VARCHAR), 3) + '&' +
//    FORMAT(CAST(si_created_datetime AS DATE), 'dd-MM-yyyy') AS si_reportno
//FROM NumberedIncidents
//ORDER BY si_created_datetime DESC


            if (A.Rows.Count > 0)
            {
                textBox1.Text = A.Rows[0]["si_reportno"].ToString();
            }
            else
            {
                // No data yet — default to "001&today"
                textBox1.Text = "001&" + DateTime.Now.ToString("dd-MM-yyyy");
            }
        }


        private void LOADEMPLOYEE()
        {
            DataTable A = dbFunctions.getTable("select EM_iid,EM_EmployeeName from employee_master where EM_Status='A'");
            emp_name.DataSource = A;
            emp_name.DisplayMember = "EM_EmployeeName";
            emp_name.ValueMember = "EM_iid";
            emp_name.SelectedIndex = -1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        String ID = "0";
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                if (ID == "0")
                {
                    DataTable dd = dbFunctions.getTable("select isnull(max(si_id),0)+1 from SAFETY_INCIDENT");
                    if (dd.Rows.Count > 0)
                    {
                        ID = dd.Rows[0][0].ToString();
                    }
                    com.CommandText = "INSERT INTO SAFETY_INCIDENT (si_id, si_reportno, si_indiction, si_indidate, si_locindi, " +
                            "si_peopleofinvo, si_boa, si_firstgiven, si_hor, si_firthear, si_deteils, si_witsta, si_actiontaken, " +
                            "si_rootcase, si_supname, si_manual, si_hrname, si_created_name, si_created_datetime, si_status,si_empname_id,si_empname,si_attach,csb_image) " +
                            "VALUES (@si_id, @si_reportno, @si_indiction, @si_indidate, @si_locindi, @si_peopleofinvo, @si_boa, " +
                            "@si_firstgiven, @si_hor, @si_firthear, @si_deteils, @si_witsta, @si_actiontaken, @si_rootcase, " +
                            "@si_supname, @si_manual, @si_hrname, @si_created_name, GETDATE(), 'A',@si_empname_id,@si_empname,@si_attach,@csb_image)";
                    //Type = "NEW";
                }
                else
                {
                    com.CommandText = "UPDATE SAFETY_INCIDENT SET " +
            "si_reportno = @si_reportno, si_indiction = @si_indiction, si_indidate = @si_indidate, si_locindi = @si_locindi, " +
            "si_peopleofinvo = @si_peopleofinvo, si_boa = @si_boa, si_firstgiven = @si_firstgiven, si_hor = @si_hor, " +
            "si_firthear = @si_firthear, si_deteils = @si_deteils, si_witsta = @si_witsta,csb_image=@csb_image, si_actiontaken = @si_actiontaken, " +
            "si_rootcase = @si_rootcase, si_supname = @si_supname, si_manual = @si_manual, si_hrname = @si_hrname, " +
            "si_created_name = @si_created_name, si_created_datetime = GETDATE(), si_status = 'A',si_attach=@si_attach,si_empname_id=@si_empname_id,si_empname=@si_empname " +
            "WHERE si_id = @si_id";
                    //Type = "EDIT";
                    button1.Text = "&Update";

                }

                com.Parameters.Add("@si_id", SqlDbType.Int).Value = ID;
                com.Parameters.Add("@si_reportno", SqlDbType.VarChar).Value = textBox1.Text;
                com.Parameters.Add("@si_indiction", SqlDbType.VarChar).Value = comboBox2.Text;
                com.Parameters.Add("@si_empname", SqlDbType.VarChar).Value = emp_name.Text;
                com.Parameters.Add("@si_empname_id", SqlDbType.VarChar).Value = emp_name.SelectedValue.ToString();
                com.Parameters.Add("@si_indidate", SqlDbType.DateTime).Value = DateTime.Parse(dateTimePicker1.Text);
                com.Parameters.Add("@si_locindi", SqlDbType.VarChar).Value = locationofincident.Text;
                com.Parameters.Add("@si_peopleofinvo", SqlDbType.VarChar).Value = peopleinvoled.Text;
                com.Parameters.Add("@si_boa", SqlDbType.VarChar).Value = bap.Text;
                com.Parameters.Add("@si_firstgiven", SqlDbType.VarChar).Value = fag.Text;
                com.Parameters.Add("@si_hor", SqlDbType.VarChar).Value = hospital_req.Text;
                com.Parameters.Add("@si_firthear", SqlDbType.VarChar).Value = nofa.Text;
                com.Parameters.Add("@si_deteils", SqlDbType.VarChar).Value = detaisdescription.Text;
                com.Parameters.Add("@si_witsta", SqlDbType.VarChar).Value = witenessstat.Text;
                com.Parameters.Add("@si_actiontaken", SqlDbType.VarChar).Value = actiontaken.Text;
                com.Parameters.Add("@si_rootcase", SqlDbType.VarChar).Value = rootcase.Text;
                com.Parameters.Add("@si_supname", SqlDbType.VarChar).Value = supervisorname.Text;
                com.Parameters.Add("@si_manual", SqlDbType.VarChar).Value = saftyofficername.Text;
                com.Parameters.Add("@si_hrname", SqlDbType.VarChar).Value = hrname.Text;
                com.Parameters.Add("@si_attach", SqlDbType.VarChar).Value = attachement.Text;
                com.Parameters.Add("@si_created_name", SqlDbType.VarChar).Value = dbFunctions.username;
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





                com.ExecuteNonQuery();
                com.Connection.Close();
                MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ID = "0";
                clear();
                loadno();
                display();


            }
            catch (Exception Ex)
            {
                //dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Display();
            //button10.Text = "Save";
        }

        private void display()
        {
          //  DataTable a=dbFunctions.getTable()
        }

        private void clear()
        {
            hrname.Text = "";
            Person.Image = null;
            emp_name.Text = "";
            comboBox2.Text = "";
           // si_reportno.Text = "";
            locationofincident.Text = "";
            peopleinvoled.Text = "";
            bap.Text = "";
            fag.Text = "";
            hospital_req.Text = "";
            nofa.Text = "";
            detaisdescription.Text = "";
            witenessstat.Text = "";
            actiontaken.Text = "";
            rootcase.Text = "";
            supervisorname.Text = "";
            saftyofficername.Text = "";

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void hrname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            Person.Image = null;
        }
    }
}
