using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using CRM_App.production;
using Maintanence_Printing_Tool;
using CRM_App.Transaction;
using GenuineHR.Reports;
using CRM_App.Master;

namespace CRM_App.Production
{
    public partial class Quality_Menu : Form
    {
        public Quality_Menu()
        {
            InitializeComponent();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Monthly_Pro_Schedule ObjProduction_Plan = new Monthly_Pro_Schedule();

            BodyPanel.Controls.Clear();
            if (ObjProduction_Plan.IsDisposed)
            {
                ObjProduction_Plan = new Monthly_Pro_Schedule();
            }
            ObjProduction_Plan.TopLevel = false;
            ObjProduction_Plan.FormBorderStyle = FormBorderStyle.None;
            ObjProduction_Plan.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjProduction_Plan);
            ObjProduction_Plan.Show();
        }

        private void Production_Menu_Load(object sender, EventArgs e)
        {
            Menu_False();
            MenuRights_fn();
            LoadMaster();
        }

        public void MenuRights_fn()
        {
            DataTable dtMenuRights = dbFunctions.getTable("pr_FetchManuRights '" + dbFunctions.rights + "'");
            if (dtMenuRights.Rows.Count > 0)
            {
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Production Plan")
                    {
                        ProductionPlan.Enabled = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Production Request")
                    {
                        ProductionRequest.Enabled = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Material Issue")
                    {
                        MaterialIssue.Enabled = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Material Return")
                    {
                        MaterialReturn.Enabled = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Route Card List")
                    {
                        RouteCardList.Enabled = true;
                    }

                }
              
            }
        }

        public void Menu_False()
        {

            ProductionPlan.Enabled = false;
            ProductionRequest.Enabled = false;
            MaterialIssue.Enabled = false;
            MaterialReturn.Enabled = false;
            RouteCardList.Enabled = false;
           
        }

        void Clear_Buttons()
        {
            foreach (Control ctrl in this.panel2.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    // ctrl.ForeColor = System.Drawing.Color.ControlText;
                    ctrl.ForeColor = System.Drawing.SystemColors.ControlText;
                    ctrl.BackColor = System.Drawing.SystemColors.ButtonFace;//ButtonFace
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
            Production_request ObjProduction_request = new Production_request();

            BodyPanel.Controls.Clear();
            if (ObjProduction_request.IsDisposed)
            {
                ObjProduction_request = new Production_request();
            }
            ObjProduction_request.TopLevel = false;
            ObjProduction_request.FormBorderStyle = FormBorderStyle.None;
            ObjProduction_request.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjProduction_request);
            ObjProduction_request.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            MaterialIssue_Issue objMaterialIssue_Issue = new MaterialIssue_Issue();

            BodyPanel.Controls.Clear();
            if (objMaterialIssue_Issue.IsDisposed)
            {
                objMaterialIssue_Issue = new MaterialIssue_Issue();
            }
            objMaterialIssue_Issue.TopLevel = false;
            objMaterialIssue_Issue.FormBorderStyle = FormBorderStyle.None;
            objMaterialIssue_Issue.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objMaterialIssue_Issue);
            objMaterialIssue_Issue.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            List_Route_Card objList_Route_Card = new List_Route_Card();
            panel2.Visible = false;
            splitContainer1.Panel1Collapsed = true;
            splitContainer1.SplitterDistance = 0;
            splitContainer1.Panel1.Hide();
            BodyPanel.Controls.Clear();
            if (objList_Route_Card.IsDisposed)
            {
                objList_Route_Card = new List_Route_Card();
            }
            objList_Route_Card.TopLevel = false;
            objList_Route_Card.FormBorderStyle = FormBorderStyle.None;
            objList_Route_Card.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objList_Route_Card);
            objList_Route_Card.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Material_Return objMaterial_Return = new Material_Return();

            BodyPanel.Controls.Clear();
            if (objMaterial_Return.IsDisposed)
            {
                objMaterial_Return = new Material_Return();
            }
            objMaterial_Return.TopLevel = false;
            objMaterial_Return.FormBorderStyle = FormBorderStyle.None;
            objMaterial_Return.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objMaterial_Return);
            objMaterial_Return.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Mess_Cutting_Issue objList_Route_Card = new Mess_Cutting_Issue();
            
            BodyPanel.Controls.Clear();
            if (objList_Route_Card.IsDisposed)
            {
                objList_Route_Card = new Mess_Cutting_Issue();
            }
            objList_Route_Card.TopLevel = false;
            objList_Route_Card.FormBorderStyle = FormBorderStyle.None;
            objList_Route_Card.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objList_Route_Card);
            objList_Route_Card.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            Pending_Mess_Cutting_List objList_Route_Card = new Pending_Mess_Cutting_List();
            panel2.Visible = false;
            splitContainer1.Panel1Collapsed = true;
            splitContainer1.SplitterDistance = 0;
            splitContainer1.Panel1.Hide();
            BodyPanel.Controls.Clear();
            if (objList_Route_Card.IsDisposed)
            {
                objList_Route_Card = new Pending_Mess_Cutting_List();
            }
            objList_Route_Card.TopLevel = false;
            objList_Route_Card.FormBorderStyle = FormBorderStyle.None;
            objList_Route_Card.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objList_Route_Card);
            objList_Route_Card.Show();

        }

        private void button13_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            change_Color(sender);
            Assembly_Production objList_Route_Card = new Assembly_Production();
            panel2.Visible = false;
            splitContainer1.Panel1Collapsed = true;
            splitContainer1.SplitterDistance = 0;
            splitContainer1.Panel1.Hide();
            BodyPanel.Controls.Clear();
            if (objList_Route_Card.IsDisposed)
            {
                objList_Route_Card = new Assembly_Production();
            }
            objList_Route_Card.TopLevel = false;
            objList_Route_Card.FormBorderStyle = FormBorderStyle.None;
            objList_Route_Card.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objList_Route_Card);
            objList_Route_Card.Show();

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            change_Color(sender);
            Assebly_Details objList_Route_Card = new Assebly_Details();
            panel2.Visible = false;
            splitContainer1.Panel1Collapsed = true;
            splitContainer1.SplitterDistance = 0;
            splitContainer1.Panel1.Hide();
            BodyPanel.Controls.Clear();
            if (objList_Route_Card.IsDisposed)
            {
                objList_Route_Card = new Assebly_Details();
            }
            objList_Route_Card.TopLevel = false;
            objList_Route_Card.FormBorderStyle = FormBorderStyle.None;
            objList_Route_Card.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objList_Route_Card);
            objList_Route_Card.Show();
        }

        private void Button9_Click_1(object sender, EventArgs e)
        {
            change_Color(sender);
            LarchERP.Master.Calibration_List ObjSupplier_Perfomanace = new LarchERP.Master.Calibration_List();

            BodyPanel.Controls.Clear();
            if (ObjSupplier_Perfomanace.IsDisposed)
            {
                ObjSupplier_Perfomanace = new LarchERP.Master.Calibration_List();
            }
            ObjSupplier_Perfomanace.TopLevel = false;
            ObjSupplier_Perfomanace.FormBorderStyle = FormBorderStyle.None;
            ObjSupplier_Perfomanace.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjSupplier_Perfomanace);
            ObjSupplier_Perfomanace.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            CRM_App.Production.Sub_part_request_new ObjSupplier_Perfomanace = new CRM_App.Production.Sub_part_request_new();

            BodyPanel.Controls.Clear();
            if (ObjSupplier_Perfomanace.IsDisposed)
            {
                ObjSupplier_Perfomanace = new CRM_App.Production.Sub_part_request_new();
            }
            ObjSupplier_Perfomanace.TopLevel = false;
            ObjSupplier_Perfomanace.FormBorderStyle = FormBorderStyle.None;
            ObjSupplier_Perfomanace.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjSupplier_Perfomanace);
            ObjSupplier_Perfomanace.Show();

        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            change_Color(sender);
            CRM_App.Production.Sub_part_Issuse_new ObjSupplier_Perfomanace = new CRM_App.Production.Sub_part_Issuse_new();

            BodyPanel.Controls.Clear();
            if (ObjSupplier_Perfomanace.IsDisposed)
            {
                ObjSupplier_Perfomanace = new CRM_App.Production.Sub_part_Issuse_new();
            }
            ObjSupplier_Perfomanace.TopLevel = false;
            ObjSupplier_Perfomanace.FormBorderStyle = FormBorderStyle.None;
            ObjSupplier_Perfomanace.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjSupplier_Perfomanace);
            ObjSupplier_Perfomanace.Show();

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            CRM_App.Quality.Final_Inspection_Entry ObjSupplier_Perfomanace = new CRM_App.Quality.Final_Inspection_Entry();

            BodyPanel.Controls.Clear();
            if (ObjSupplier_Perfomanace.IsDisposed)
            {
                ObjSupplier_Perfomanace = new CRM_App.Quality.Final_Inspection_Entry();
            }
            ObjSupplier_Perfomanace.TopLevel = false;
            ObjSupplier_Perfomanace.FormBorderStyle = FormBorderStyle.None;
            ObjSupplier_Perfomanace.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjSupplier_Perfomanace);
            ObjSupplier_Perfomanace.Show();


        }

        private void button18_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            Assembly_Production objList_Route_Card = new Assembly_Production();
            panel2.Visible = false;
            splitContainer1.Panel1Collapsed = true;
            splitContainer1.SplitterDistance = 0;
            splitContainer1.Panel1.Hide();
            BodyPanel.Controls.Clear();
            if (objList_Route_Card.IsDisposed)
            {
                objList_Route_Card = new Assembly_Production();
            }
            objList_Route_Card.TopLevel = false;
            objList_Route_Card.FormBorderStyle = FormBorderStyle.None;
            objList_Route_Card.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objList_Route_Card);
            objList_Route_Card.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Assebly_Details objList_Route_Card = new Assebly_Details();
            panel2.Visible = false;
            splitContainer1.Panel1Collapsed = true;
            splitContainer1.SplitterDistance = 0;
            splitContainer1.Panel1.Hide();
            BodyPanel.Controls.Clear();
            if (objList_Route_Card.IsDisposed)
            {
                objList_Route_Card = new Assebly_Details();
            }
            objList_Route_Card.TopLevel = false;
            objList_Route_Card.FormBorderStyle = FormBorderStyle.None;
            objList_Route_Card.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objList_Route_Card);
            objList_Route_Card.Show();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            change_Color(sender);
            LarchERP.Master.Calibration_List ObjSupplier_Perfomanace = new LarchERP.Master.Calibration_List();

            BodyPanel.Controls.Clear();
            if (ObjSupplier_Perfomanace.IsDisposed)
            {
                ObjSupplier_Perfomanace = new LarchERP.Master.Calibration_List();
            }
            ObjSupplier_Perfomanace.TopLevel = false;
            ObjSupplier_Perfomanace.FormBorderStyle = FormBorderStyle.None;
            ObjSupplier_Perfomanace.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjSupplier_Perfomanace);
            ObjSupplier_Perfomanace.Show();
        }

        private void Incoming_Inspection_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Grin_Against_After_Purchase_Order ObjGrin_Against_After_Purchase_Order = new Grin_Against_After_Purchase_Order();

            BodyPanel.Controls.Clear();
            if (ObjGrin_Against_After_Purchase_Order.IsDisposed)
            {
                ObjGrin_Against_After_Purchase_Order = new Grin_Against_After_Purchase_Order();
            }
            ObjGrin_Against_After_Purchase_Order.TopLevel = false;
            ObjGrin_Against_After_Purchase_Order.FormBorderStyle = FormBorderStyle.None;
            ObjGrin_Against_After_Purchase_Order.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjGrin_Against_After_Purchase_Order);
            ObjGrin_Against_After_Purchase_Order.Show();
        }

        private void GRN_QC_Details_Print_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Grin_Against_PO_Print ObjGrin_Against_PO_Print = new Grin_Against_PO_Print();

            BodyPanel.Controls.Clear();
            if (ObjGrin_Against_PO_Print.IsDisposed)
            {
                ObjGrin_Against_PO_Print = new Grin_Against_PO_Print();
            }
            ObjGrin_Against_PO_Print.TopLevel = false;
            ObjGrin_Against_PO_Print.FormBorderStyle = FormBorderStyle.None;
            ObjGrin_Against_PO_Print.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjGrin_Against_PO_Print);
            ObjGrin_Against_PO_Print.Show();
        }

        private void button25_Click_1(object sender, EventArgs e)
        {
            change_Color(sender);
            CRM_App.Sales.Customer_complient ObjGrin_Against_PO_Print = new CRM_App.Sales.Customer_complient();

            BodyPanel.Controls.Clear();
            if (ObjGrin_Against_PO_Print.IsDisposed)
            {
                ObjGrin_Against_PO_Print = new CRM_App.Sales.Customer_complient();
            }
            ObjGrin_Against_PO_Print.TopLevel = false;
            ObjGrin_Against_PO_Print.FormBorderStyle = FormBorderStyle.None;
            ObjGrin_Against_PO_Print.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjGrin_Against_PO_Print);
            ObjGrin_Against_PO_Print.Show();

            
        }
    }
}
