using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using GenuineHR.Master;
using GenuineHR.Reports;
//using GenuineHR.Transaction;
using GenuineHR.Settiings;
using Maintanence_Printing_Tool;
using System.IO;
using System.Reflection;
using CRM_App.Transaction;
using CRM_App.Production;
using LarchERP.Master;
using CRM_App;

namespace GenuineHR
{
    public partial class Main : Form
    {
        private int childFormNumber = 0;
        string Rights = "";

        public Main()
        {
            InitializeComponent();
        }

        
        Master_Menu objMasterMenu = new Master_Menu();

        
        private void Main_Load(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Instrument_alert  '"+System.DateTime.Now.ToString("yyyy")+"'");
            if (dt.Rows.Count > 0)
            {
                panel2.Visible = true;
                label1.Text = dt.Rows.Count.ToString("0");
                
            }

            DataTable dt__ = dbFunctions.getTable("pr_Get_GRN_No_Approval");
            if (dt__.Rows.Count > 0)
            {
                panel4.Visible = true;
                label8.Text = dt__.Rows.Count.ToString("0");
            }


            DataTable dt1 = dbFunctions.getTable("pr_get_Mould_TODAY_PM_Need ");
            if (dt1.Rows.Count > 0)
            {
                panel3.Visible = true;
                label6.Text = dt1.Rows.Count.ToString("0");

            }

            DataTable dt2 = dbFunctions.getTable("pr_get_Machine_dash_bord ");
            if (dt2.Rows.Count > 0)
            {
                panel3.Visible = true;
                label14.Text = dt2.Rows.Count.ToString("0");

            }

            DataTable dt3 = dbFunctions.getTable("pr_get_Mould_dash_bord ");
            if (dt3.Rows.Count > 0)
            {
                panel3.Visible = true;
                label16.Text = dt3.Rows.Count.ToString("0");

            }
            DataTable dt4 = dbFunctions.getTable("pr_RM_DASHBORD_Stock ");
            if (dt4.Rows.Count > 0)
            {
                panel3.Visible = true;
                label18.Text = dt4.Rows.Count.ToString("0");
                

            }
            DataTable dt5= dbFunctions.getTable("pr_BO_DASHBORD_Stock ");
            if (dt5.Rows.Count > 0)
            {
                panel3.Visible = true;
                //label20.Text = dt5.Rows.Count.ToString("0");
                label20.Text = dt5.Rows.Count.ToString("0");

            }


            display();






            Menu_False();
            MenuRights_fn();
           
            Clear_Buttons();
            User.Text = dbFunctions.username;
            LoginTime.Text = DateTime.Now.ToString("hh:mm tt");

            display();



        }

        private void display()
        {
            DataTable dt = dbFunctions.getTable("pro_rej_status_dashbord ");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);

            DataTable dt1 = dbFunctions.getTable("Machine_status_dashbord ");
            dataGridView2.DataSource = dt1;
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);

            DataTable dt2 = dbFunctions.getTable("Mould_status_dashbord ");
            dataGridView4.DataSource = dt2;
            dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
        }

        void Clear_Buttons()
        {
            foreach (Control ctrl in this.panel1.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    ctrl.ForeColor = System.Drawing.Color.DimGray;


                }
            }
        }

        private void MenuRights_fn()
        {
            DataTable dtMenuRights = dbFunctions.getTable("pr_FetchManuRights '" + dbFunctions.rights + "'");
             if (dtMenuRights.Rows.Count > 0)
             {

                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Master")
                     {
                         btnMaster.Enabled = true;
                     }

                 }
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Approval")
                     {
                         btnApproval.Enabled = true;
                     }

                 }
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Purchase")
                     {
                         btnPurchase.Enabled = true;
                     }

                 }
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Production")
                     {
                         btnProduction.Enabled = true;
                     }

                 }
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Mould Maint")
                     {
                         btnmouldmatanance.Enabled = true;
                     }

                 }
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Sales")
                     {
                         btnSales.Enabled = true;
                     }

                 }
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Reports")
                     {
                         btnReports.Enabled = true;
                     }

                 }
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Settings")
                     {
                         btnSettings.Enabled = true;
                     }

                 }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Machine Maint")
                    {
                        button2.Enabled = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Quality")
                    {
                        button8.Enabled = true;
                    }

                }
            }
        }

        private void Menu_False()
        {
           
            btnMaster.Enabled=false;
            btnApproval.Enabled = false;
            btnPurchase.Enabled=false;
            btnProduction.Enabled = false;
            btnmouldmatanance.Enabled = false;
            btnSales.Enabled = false;
            btnReports.Enabled=false;
            btnSettings.Enabled=false;
            button2.Enabled=false;
            button8.Enabled=false;




        }

        private void button3_Click(object sender, EventArgs e)
        {
            change_Color(sender); 
            dbFunctions.status = "Architech and Maintaining By Genuine Solutions.";

            Maintanence_Printing_Tool.Help objHelp = new Maintanence_Printing_Tool.Help();
            objHelp.Show();
           
           
            
        }

        private void Master_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            dbFunctions.status = "Architech and Maintaining By Genuine";
           
            MainPanel.Controls.Clear();
            this.IsMdiContainer = true;
            if (objMasterMenu.IsDisposed)
            {
                objMasterMenu = new Master_Menu();
            }
            objMasterMenu.TopLevel = false;
            objMasterMenu.FormBorderStyle = FormBorderStyle.None;
            objMasterMenu.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(objMasterMenu);
            objMasterMenu.Show();
        }

        private void Report_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            dbFunctions.status = "Architech and Maintaining By Genuine.";
            ReportMenu objReportMenu = new ReportMenu();
            MainPanel.Controls.Clear();
            this.IsMdiContainer = true;
            if (objReportMenu.IsDisposed)
            {
                objReportMenu = new ReportMenu();
            }
            objReportMenu.TopLevel = false;
            objReportMenu.FormBorderStyle = FormBorderStyle.None;
            objReportMenu.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(objReportMenu);
            objReportMenu.Show();
        }

        public void change_Color(object sender)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            Clear_Buttons();
            Button B = (Button)sender;
            B.ForeColor = System.Drawing.Color.Maroon;

          // B.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));

           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            dbFunctions.status = "Architech and Maintaining By Genuine.";
            Transaction_Menu objTransaction_Menu = new Transaction_Menu();
            MainPanel.Controls.Clear();
            this.IsMdiContainer = true;
            if (objTransaction_Menu.IsDisposed)
            {
                objTransaction_Menu = new Transaction_Menu();
            }
            objTransaction_Menu.TopLevel = false;
            objTransaction_Menu.FormBorderStyle = FormBorderStyle.None;
            objTransaction_Menu.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(objTransaction_Menu);
            objTransaction_Menu.Show();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            change_Color(sender); 
            dbFunctions.status = "Architech and Maintaining By Genuine.";
            Setting_Menu objSetting_Menu = new Setting_Menu();
            MainPanel.Controls.Clear();
            this.IsMdiContainer = true;
            if (objSetting_Menu.IsDisposed)
            {
                objSetting_Menu = new Setting_Menu();
            }
            objSetting_Menu.TopLevel = false;
            objSetting_Menu.FormBorderStyle = FormBorderStyle.None;
            objSetting_Menu.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(objSetting_Menu);
            objSetting_Menu.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            change_Color(sender);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();

                Log1 lg = new Log1();
                lg.Show();
            }
        }

       

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Status_Main.Text = dbFunctions.status;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            MainPanel.Controls.Clear();
            panel2.Visible = false;
            MainPanel.Visible = true;

            DataTable dt = dbFunctions.getTable("Pr_Fetch_Instrument_alert  '" + System.DateTime.Now.ToString("yyyy") + "'");
            if (dt.Rows.Count > 0)
            {
                panel2.Visible = true;
                label1.Text = dt.Rows.Count.ToString("0");

            }



            DataTable dt1 = dbFunctions.getTable("pr_get_Mould_TODAY_PM_Need ");
            if (dt1.Rows.Count > 0)
            {
                panel3.Visible = true;
                label6.Text = dt1.Rows.Count.ToString("0");

            }
            display();
            this.Hide();
            Main objMain = new Main();
            objMain.Show();

        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnApproval_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            dbFunctions.status = "Architech and Maintaining By Genuine";
            Apprval_Menu objApprval_Menu = new Apprval_Menu();
            MainPanel.Controls.Clear();
            this.IsMdiContainer = true;
            if (objApprval_Menu.IsDisposed)
            {
                objApprval_Menu = new Apprval_Menu();
            }
            objApprval_Menu.TopLevel = false;
            objApprval_Menu.FormBorderStyle = FormBorderStyle.None;
            objApprval_Menu.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(objApprval_Menu);
            objApprval_Menu.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            change_Color(sender); 
            dbFunctions.status = "Architech and Maintaining By Genuine";
            Production_Menu objProduction_Menu = new Production_Menu();
            MainPanel.Controls.Clear();
            this.IsMdiContainer = true;
            if (objProduction_Menu.IsDisposed)
            {
                objProduction_Menu = new Production_Menu();
            }
            objProduction_Menu.TopLevel = false;
            objProduction_Menu.FormBorderStyle = FormBorderStyle.None;
            objProduction_Menu.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(objProduction_Menu);
            objProduction_Menu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            dbFunctions.status = "Architech and Maintaining By Genuine";
            Maintanance_Menu objMaintanance_Menu = new Maintanance_Menu();
            MainPanel.Controls.Clear();
            this.IsMdiContainer = true;
            if (objMaintanance_Menu.IsDisposed)
            {
                objMaintanance_Menu = new Maintanance_Menu();
            }
            objMaintanance_Menu.TopLevel = false;
            objMaintanance_Menu.FormBorderStyle = FormBorderStyle.None;
            objMaintanance_Menu.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(objMaintanance_Menu);
            objMaintanance_Menu.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            dbFunctions.status = "Architech and Maintaining By Genuine";
            Sales_Menu ObjSales_Menu = new Sales_Menu();

            MainPanel.Controls.Clear();
            this.IsMdiContainer = true;
            if (ObjSales_Menu.IsDisposed)
            {
                ObjSales_Menu = new Sales_Menu();
            }
            ObjSales_Menu.TopLevel = false;
            ObjSales_Menu.FormBorderStyle = FormBorderStyle.None;
            ObjSales_Menu.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(ObjSales_Menu);
            ObjSales_Menu.Show();
        }
        CurrentDatePlansDetails Objlink = new CurrentDatePlansDetails();
        private void btnlink_Click(object sender, EventArgs e)
        {
            // change_Color(sender);PM_Alert_Details.cs
            

            //MainPanel.Controls.Clear();
            this.IsMdiContainer = true;
            if (Objlink.IsDisposed)
            {
                Objlink = new CurrentDatePlansDetails();
            }
            ////Objlink.TopLevel = false;
           // Objlink.FormBorderStyle = FormBorderStyle.None;
            ////Objlink.Dock = DockStyle.Fill;
            //MainPanel.Controls.Add(Objlink);
            Objlink.Show();
        }
        PM_Alert_Details objPM_Alert_Details = new PM_Alert_Details();
        private void button1_Click_2(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            if (objPM_Alert_Details.IsDisposed)
            {
                objPM_Alert_Details = new PM_Alert_Details();
            }
            objPM_Alert_Details.Show();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            //change_Color(sender);
            //dbFunctions.status = "Architech and Maintaining By Genuine";
            //Machine_Maintanance_Menu objMaintanance_Menu = new Machine_Maintanance_Menu();
            //MainPanel.Controls.Clear();
            //this.IsMdiContainer = true;
            //if (objMaintanance_Menu.IsDisposed)
            //{
            //    objMaintanance_Menu = new Machine_Maintanance_Menu();
            //}
            //objMaintanance_Menu.TopLevel = false;
            //objMaintanance_Menu.FormBorderStyle = FormBorderStyle.None;
            //objMaintanance_Menu.Dock = DockStyle.Fill;
            //MainPanel.Controls.Add(objMaintanance_Menu);
            //objMaintanance_Menu.Show();

            change_Color(sender);
            dbFunctions.status = "Architech and Maintaining By Genuine";
            Machine_Maintanance_Menu objProduction_Menu = new Machine_Maintanance_Menu();
            MainPanel.Controls.Clear();
            this.IsMdiContainer = true;
            if (objProduction_Menu.IsDisposed)
            {
                objProduction_Menu = new Machine_Maintanance_Menu();
            }
            objProduction_Menu.TopLevel = false;
            objProduction_Menu.FormBorderStyle = FormBorderStyle.None;
            objProduction_Menu.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(objProduction_Menu);
            objProduction_Menu.Show();
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        Machine_Alreat machine_Alert_Details = new Machine_Alreat();
        private void button4_Click_2(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            if (machine_Alert_Details.IsDisposed)
            {
                machine_Alert_Details = new Machine_Alreat();
            }
            machine_Alert_Details.Show();
        }
        Mould_Alreat mould_Alert_Details = new Mould_Alreat();
        private void button5_Click(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            if (mould_Alert_Details.IsDisposed)
            {
                mould_Alert_Details = new Mould_Alreat();
            }
            mould_Alert_Details.Show();

        }
        RM_Alreat RM_Alert_Details = new RM_Alreat();
        private void button6_Click(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            if (RM_Alert_Details.IsDisposed)
            {
                RM_Alert_Details = new RM_Alreat();
            }
            RM_Alert_Details.Show();

        }

        BO_Alreat BO_Alert_Details = new BO_Alreat();
        private void button7_Click(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            if (BO_Alert_Details.IsDisposed)
            {
                BO_Alert_Details = new BO_Alreat();
            }
            BO_Alert_Details.Show();

        }

        private void button8_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            dbFunctions.status = "Architech and Maintaining By Genuine";
            Quality_Menu objProduction_Menu = new Quality_Menu();
            MainPanel.Controls.Clear();
            this.IsMdiContainer = true;
            if (objProduction_Menu.IsDisposed)
            {
                objProduction_Menu = new Quality_Menu();
            }
            objProduction_Menu.TopLevel = false;
            objProduction_Menu.FormBorderStyle = FormBorderStyle.None;
            objProduction_Menu.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(objProduction_Menu);
            objProduction_Menu.Show();
        }
    }
}
