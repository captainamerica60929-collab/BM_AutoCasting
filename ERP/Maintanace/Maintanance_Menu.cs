using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using CRM_App.production;
using Maintanence_Printing_Tool;
using LarchERP.Master;
using CRM_App.Maintanace;
using CRM_App.Transaction;
using CRM_App.Master;

namespace CRM_App.Production
{
    public partial class Maintanance_Menu : Form
    {
        public Maintanance_Menu()
        {
            InitializeComponent();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            change_Color(sender);

            Machine_Master ObjMachine_Master = new Machine_Master();


            BodyPanel.Controls.Clear();
            if (ObjMachine_Master.IsDisposed)
            {
                ObjMachine_Master = new Machine_Master();
            }
            ObjMachine_Master.TopLevel = false;
            ObjMachine_Master.FormBorderStyle = FormBorderStyle.None;
            ObjMachine_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjMachine_Master);
            ObjMachine_Master.Show();

        }

        private void Production_Menu_Load(object sender, EventArgs e)
        {
            Menu_False();
            MenuRights_fn();
            
            
            //LoadMaster();
            Clear_Buttons();
            //Mould_Maintenance_List ObjMould_Maintenance_List = new Mould_Maintenance_List();
            
            //BodyPanel.Controls.Clear();
            //if (ObjMould_Maintenance_List.IsDisposed)
            //{
            //    ObjMould_Maintenance_List = new Mould_Maintenance_List();
            //}
            //ObjMould_Maintenance_List.TopLevel = false;
            //ObjMould_Maintenance_List.FormBorderStyle = FormBorderStyle.None;
            //ObjMould_Maintenance_List.Dock = DockStyle.Fill;
            //BodyPanel.Controls.Add(ObjMould_Maintenance_List);
            //ObjMould_Maintenance_List.Show();
        }

        public void MenuRights_fn()
        {
             DataTable dtMenuRights = dbFunctions.getTable("pr_FetchManuRights '" + dbFunctions.rights + "'");
             if (dtMenuRights.Rows.Count > 0)
             {
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Mould List")
                     {
                         MouldList.Enabled = true;
                     }

                 }
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Preventive Maintance")
                     {
                         PreventiveMaintance.Enabled = true;
                     }

                 }
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Regular Maintanace Enty")
                     {
                         RegularMaintanaceEnty.Enabled = true;
                     }

                 }
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Print PM Details")
                     {
                         PrintPMDetails.Enabled = true;
                     }

                 }
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Mould History Card")
                     {
                         MouldHistoryCard.Enabled = true;
                     }

                 }
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Break Down Entry")
                     {
                         BreakDownEntry.Enabled = true;
                     }

                 }
                 for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                 {
                     if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Print Regular Maintanace")
                     {
                         PrintRegularMaintanace.Enabled = true;
                     }

                 }
             }
        }

        public void Menu_False()
        {   
            MouldList.Enabled=false;
            PreventiveMaintance.Enabled=false;
            RegularMaintanaceEnty.Enabled=false;
            PrintRegularMaintanace.Enabled=false;
            PrintPMDetails.Enabled=false;
            MouldHistoryCard.Enabled=false;
            BreakDownEntry.Enabled = false;
            
        }


        void Clear_Buttons()
        {
            foreach (Control ctrl in this.panel2.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                   // ctrl.ForeColor = System.Drawing.Color.ControlText;
                       ctrl.ForeColor = System.Drawing.SystemColors.ControlText;
                       ctrl.BackColor= System.Drawing.SystemColors.ButtonFace;//ButtonFace
                }
            }
        }

        public void change_Color(object sender)
        {
            Clear_Buttons();
            Button B = (Button)sender;
            B.ForeColor = System.Drawing.Color.Maroon;
            B.BackColor = System.Drawing.Color.Moccasin;//ButtonFace
            // B.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));


        }
        public void LoadMaster()
        {



        }

        private void BodyPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (dbFunctions.isclose == true)
            {
                LoadMaster();
                dbFunctions.isclose = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Mold_Master ObjMold_Master = new Mold_Master();

            BodyPanel.Controls.Clear();
            if (ObjMold_Master.IsDisposed)
            {
                ObjMold_Master = new Mold_Master();
            }
            ObjMold_Master.TopLevel = false;
            ObjMold_Master.FormBorderStyle = FormBorderStyle.None;
            ObjMold_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjMold_Master);
            ObjMold_Master.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Check_PointMaster objCheck_PointMaster = new Check_PointMaster();

            BodyPanel.Controls.Clear();
            if (objCheck_PointMaster.IsDisposed)
            {
                objCheck_PointMaster = new Check_PointMaster();
            }
            objCheck_PointMaster.TopLevel = false;
            objCheck_PointMaster.FormBorderStyle = FormBorderStyle.None;
            objCheck_PointMaster.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objCheck_PointMaster);
            objCheck_PointMaster.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            Requirement_Master objRequirement_Master = new Requirement_Master();
            BodyPanel.Controls.Clear();
            if (objRequirement_Master.IsDisposed)
            {
                objRequirement_Master = new Requirement_Master();
            }
            objRequirement_Master.TopLevel = false;
            objRequirement_Master.FormBorderStyle = FormBorderStyle.None;
            objRequirement_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objRequirement_Master);
            objRequirement_Master.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Mould_Maintance_Details objMould_Maintance_Details = new Mould_Maintance_Details();

            BodyPanel.Controls.Clear();
            if (objMould_Maintance_Details.IsDisposed)
            {
                objMould_Maintance_Details = new Mould_Maintance_Details();
            }
            objMould_Maintance_Details.TopLevel = false;
            objMould_Maintance_Details.FormBorderStyle = FormBorderStyle.None;
            objMould_Maintance_Details.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objMould_Maintance_Details);
            objMould_Maintance_Details.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Regular_Matainance ObjRegular_Matainance = new Regular_Matainance();

            BodyPanel.Controls.Clear();
            if (ObjRegular_Matainance.IsDisposed)
            {
                ObjRegular_Matainance = new Regular_Matainance();
            }
            ObjRegular_Matainance.TopLevel = false;
            ObjRegular_Matainance.FormBorderStyle = FormBorderStyle.None;
            ObjRegular_Matainance.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjRegular_Matainance);
            ObjRegular_Matainance.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Preventive_Matainance ObjPreventive_Matainance = new Preventive_Matainance();
            BodyPanel.Controls.Clear();
            if (ObjPreventive_Matainance.IsDisposed)
            {
                ObjPreventive_Matainance = new Preventive_Matainance();
            }
            ObjPreventive_Matainance.TopLevel = false;
            ObjPreventive_Matainance.FormBorderStyle = FormBorderStyle.None;
            ObjPreventive_Matainance.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjPreventive_Matainance);
            ObjPreventive_Matainance.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear_Buttons();
            Mould_Maintenance_List ObjMould_Maintenance_List = new Mould_Maintenance_List();

            BodyPanel.Controls.Clear();
            if (ObjMould_Maintenance_List.IsDisposed)
            {
                ObjMould_Maintenance_List = new Mould_Maintenance_List();
            }
            ObjMould_Maintenance_List.TopLevel = false;
            ObjMould_Maintenance_List.FormBorderStyle = FormBorderStyle.None;
            ObjMould_Maintenance_List.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjMould_Maintenance_List);
            ObjMould_Maintenance_List.Show();
            ObjMould_Maintenance_List.ApplyColumnColoring();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            change_Color(sender);
            Regular_Maintanace_Print ObjRegular_Matainance = new Regular_Maintanace_Print();

            BodyPanel.Controls.Clear();
            if (ObjRegular_Matainance.IsDisposed)
            {
                ObjRegular_Matainance = new Regular_Maintanace_Print();
            }
            ObjRegular_Matainance.TopLevel = false;
            ObjRegular_Matainance.FormBorderStyle = FormBorderStyle.None;
            ObjRegular_Matainance.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjRegular_Matainance);
            ObjRegular_Matainance.Show();

        }

        private void button8_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            PM_Maintanace_Print ObjRegular_Matainance = new PM_Maintanace_Print();

            BodyPanel.Controls.Clear();
            if (ObjRegular_Matainance.IsDisposed)
            {
                ObjRegular_Matainance = new PM_Maintanace_Print();
            }
            ObjRegular_Matainance.TopLevel = false;
            ObjRegular_Matainance.FormBorderStyle = FormBorderStyle.None;
            ObjRegular_Matainance.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjRegular_Matainance);
            ObjRegular_Matainance.Show();
        }

        private void button12_Click_1(object sender, EventArgs e)
        {

            change_Color(sender);
            Mould_History_Card ObjRegular_Matainance = new Mould_History_Card();

            BodyPanel.Controls.Clear();
            if (ObjRegular_Matainance.IsDisposed)
            {
                ObjRegular_Matainance = new Mould_History_Card();
            }
            ObjRegular_Matainance.TopLevel = false;
            ObjRegular_Matainance.FormBorderStyle = FormBorderStyle.None;
            ObjRegular_Matainance.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjRegular_Matainance);
            ObjRegular_Matainance.Show();

        }

        private void button14_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            BreakDowns ObjRegular_Matainance = new BreakDowns();
            BodyPanel.Controls.Clear();
            if (ObjRegular_Matainance.IsDisposed)
            {
                ObjRegular_Matainance = new BreakDowns();
            }
            ObjRegular_Matainance.TopLevel = false;
            ObjRegular_Matainance.FormBorderStyle = FormBorderStyle.None;
            ObjRegular_Matainance.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjRegular_Matainance);
            ObjRegular_Matainance.Show();
        }

        private void MoldCriticals_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Mold_Critical_Spares ObjMold_Critical_Spares = new Mold_Critical_Spares();
            BodyPanel.Controls.Clear();
            if (ObjMold_Critical_Spares.IsDisposed)
            {
                ObjMold_Critical_Spares = new Mold_Critical_Spares();
            }
            ObjMold_Critical_Spares.TopLevel = false;
            ObjMold_Critical_Spares.FormBorderStyle = FormBorderStyle.None;
            ObjMold_Critical_Spares.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjMold_Critical_Spares);
            ObjMold_Critical_Spares.Show();
        }
        //Machine_Critical_Spares
        private void MachineCriticals_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Machine_Critical_Spares ObjMachine_Critical_Spares = new Machine_Critical_Spares();
            BodyPanel.Controls.Clear();
            if (ObjMachine_Critical_Spares.IsDisposed)
            {
                ObjMachine_Critical_Spares = new Machine_Critical_Spares();
            }
            ObjMachine_Critical_Spares.TopLevel = false;
            ObjMachine_Critical_Spares.FormBorderStyle = FormBorderStyle.None;
            ObjMachine_Critical_Spares.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjMachine_Critical_Spares);
            ObjMachine_Critical_Spares.Show();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            //change_Color(sender);
            //Calibration_List ObjSupplier_Perfomanace = new Calibration_List();

            //BodyPanel.Controls.Clear();
            //if (ObjSupplier_Perfomanace.IsDisposed)
            //{
            //    ObjSupplier_Perfomanace = new Calibration_List();
            //}
            //ObjSupplier_Perfomanace.TopLevel = false;
            //ObjSupplier_Perfomanace.FormBorderStyle = FormBorderStyle.None;
            //ObjSupplier_Perfomanace.Dock = DockStyle.Fill;
            //BodyPanel.Controls.Add(ObjSupplier_Perfomanace);
            //ObjSupplier_Perfomanace.Show();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }

        private void Button12_Click_2(object sender, EventArgs e)
        {

        }
    }
}
