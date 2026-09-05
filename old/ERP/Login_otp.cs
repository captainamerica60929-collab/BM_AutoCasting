using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using LarchERP;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using System.Security.Cryptography;
using GenuineHR;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace MyData
{
    public partial class Login_otp : Form
    {
        public Login_otp()
        {
            InitializeComponent();
       
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //this.TransparencyKey = Color.White;
            //this.BackColor = Color.White;
            //this.textBox1.BackColor = Color.WhiteSmoke;
            //this.textBox2.BackColor = Color.WhiteSmoke;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
           get_Login();
           //Backup();
        }
        //public void Backup()
        //{
        //    int days = 0;

        //    DataTable dtb = dbFunctions.getTable("pr_getDAte");
        //    days = Convert.ToInt32(dtb.Rows[0]["HourDiff"].ToString());
            
        //    if (days > 120)
        //    {
        //        DialogResult result = MessageBox.Show("Need a Backup? Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        //        if (result == DialogResult.Yes)
        //        {
        //            savedate();
        //            DataTable dt = dbFunctions.getTable("pr_GetBackup");
        //            MessageBox.Show("Backup Saved Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
                  
        //        }

        //    }
        //}
        //public void savedate()
        //{
        //    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = con;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "pr_InsertDate";
        //        cmd.Parameters.Add("@Bk_Datetime", SqlDbType.DateTime).Value = DateTime.Now.ToString();

        //        cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();
        //    }
        //    catch
        //    {
        //    }

        //}
        void get_Login()
        {

            SqlConnection objCon = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                if (txtusername.Text.Trim() != "" && txtpassword.Text.Trim() != "")
                {
                    // string enc = DecryptThis("BurWAhQFGrvQz5PcbAWHfw==", "Enc");

                     string Password =txtpassword.Text;// EncryptThis(txtpassword.Text.Trim(), "Enc");

                    DataTable dt = dbFunctions.getTable("pr_getLogin  '" + txtusername.Text + "','" + Password + "'");
                    if (dt.Rows.Count > 0)
                    {

                       dbFunctions.username = txtusername.Text;
                       dbFunctions.rights = dt.Rows[0]["um_iRightsId"].ToString();





                        this.Hide();
                        Main objMain = new Main();
                        objMain.Show();


                    }
                    else
                    {
                        MessageBox.Show("Invalid User Name And Password","Message",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        txtpassword.Text = "";
                        txtusername.Text = "";
                        
                        txtusername.Focus();


                    }
                }
                else
                {
                    MessageBox.Show("Login Details Incorrect", "Error");
                    txtpassword.Text = "";
                    txtusername.Text = "";

                    return;
                }



            }




            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                txtusername.Focus();
                objCon.Close();
            }


        }

        public static string DecryptThis(string ToDecrypt, string Keys)
        {
            byte[] IV = new byte[8] { 210, 1, 25, 101, 0, 76, 173, 59 };
            byte[] buffer = Convert.FromBase64String(ToDecrypt);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Keys));
            des.IV = IV;
            return Encoding.ASCII.GetString(
            des.CreateDecryptor().TransformFinalBlock(
            buffer,
            0,
            buffer.Length
            )
            );

        }


        public static string EncryptThis(string ToEncrypt, string Keys)
        {
            byte[] IV = new byte[8] { 210, 1, 25, 101, 0, 76, 173, 59 };
            byte[] buffer = Encoding.ASCII.GetBytes(ToEncrypt);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Keys));
            des.IV = IV;
            return Convert.ToBase64String(
            des.CreateEncryptor().TransformFinalBlock(
            buffer,
            0,
            buffer.Length
            )
            );
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
             dbFunctions.isclose = true; this.Close(); 
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                txtpassword.Focus();



            }

        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.Focus();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("osk");

        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }
        string generatedOtp = "";
        private void button3_Click(object sender, EventArgs e)
        {
            string user = txtusername.Text.Trim();

            if (string.IsNullOrEmpty(user))
            {
                MessageBox.Show("Enter Username");
                return;
            }

            // Generate 6-digit OTP
            Random rand = new Random();
            string otp = rand.Next(100000, 999999).ToString(); // store generated OTP

            DateTime expiry = DateTime.Now.AddMinutes(2); // OTP valid 2 min

            // Send OTP to email
            SendOtpByEmail("nithishjoin@gmail.com", otp);

            using (SqlConnection con = new SqlConnection(dbFunctions.connectionstring))
            {
                con.Open();
                string query = "INSERT INTO OTP_Login (UserName, OTP, ExpiryTime) VALUES (@user, @otp, @expiry)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@otp", otp);
                    cmd.Parameters.AddWithValue("@expiry", expiry);
                    cmd.ExecuteNonQuery();
                }
                // Show OTP (instead of sending SMS/Email)
                // txtpassword.Text = "Your OTP is: " + otp;
            }
        }

        private void SendOtpByEmail(string toEmail, string otp)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("nithishjoin@gmail.com");
                mail.To.Add(toEmail);
                mail.Subject = "Your OTP Code";
                mail.Body = "Your OTP is: " + otp;

                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("joinnithish@gmail.com", "Sadha@#321");
                smtp.EnableSsl = true;

                smtp.Send(mail);

                MessageBox.Show("OTP sent successfully to " + toEmail);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Email send failed: " + ex.Message);
            }
        }
    }
}
