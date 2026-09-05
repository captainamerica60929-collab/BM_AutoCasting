using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using LarchERP.Master;
//using LodgeManagement.Master;
using Maintanence_Printing_Tool;
using Electronika.Transaction;
using Electronika.Master;
using CRM_App.Transaction;
using CRM_App.Approvals;

namespace GenuineHR.Master
{
    public partial class Apprval_Menu : Form
    {
        
        public Apprval_Menu()
        {
            InitializeComponent();
        }
        private void Master_Menu_Load(object sender, EventArgs e)
        {
            Menu_False();
            MenuRights_fn();
            {
                DataTable dt = dbFunctions.getTable("pr_Display_RM_Waiting");
                if (dt.Rows.Count > 0)
                {
                    RMPart.ForeColor = Color.Red;
                    RMPart.Text = "    RM Part(" + dt.Rows.Count + ")";
                }
                else
                {

                    RMPart.ForeColor = Color.Black;
                    RMPart.Text = "    RM Part";
                }
            }

            {
                DataTable dt = dbFunctions.getTable("pr_Display_FG_Waiting");
                if (dt.Rows.Count > 0)
                {
                    FGPart.ForeColor = Color.Red;
                    FGPart.Text = "    FG Part(" + dt.Rows.Count + ")";
                }
                else
                {

                    FGPart.ForeColor = Color.Black;
                    FGPart.Text = "    FG Part";
                }
            }

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
        public void MenuRights_fn()
        {
            DataTable dtMenuRights = dbFunctions.getTable("pr_FetchManuRights '" + dbFunctions.rights + "'");
            if (dtMenuRights.Rows.Count > 0)
            {
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Suppliers")
                    {
                        Suppliers.Enabled = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Customers")
                    {
                        Customers.Enabled = true;
                        
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Purchase Order")
                    {
                        PurchaseOrder.Enabled = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Invoice Approval")
                    {
                        InvoiceApproval.Enabled = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "FG Part")
                    {
                        FGPart.Enabled = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "RM Part")
                    {
                        RMPart.Enabled = true;
                    }

                }
            }
        }

        public void Menu_False()
        {
            Suppliers.Enabled = false;
            Customers.Enabled = false;
            PurchaseOrder.Enabled = false;
            InvoiceApproval.Enabled = false;
            FGPart.Enabled = false;
            RMPart.Enabled = false;
           
        }
        private void BodyPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (dbFunctions.isclose == true)
            {
                //LoadMaster();
                dbFunctions.isclose = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            ApprovalSuppliers ObjSuppliers_Approval = new ApprovalSuppliers();


            BodyPanel.Controls.Clear();
            if (ObjSuppliers_Approval.IsDisposed)
            {
                ObjSuppliers_Approval = new ApprovalSuppliers();
            }
            ObjSuppliers_Approval.TopLevel = false;
            ObjSuppliers_Approval.FormBorderStyle = FormBorderStyle.None;
            ObjSuppliers_Approval.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjSuppliers_Approval);
            ObjSuppliers_Approval.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            ApprovalCustomers ObjApprovalCustomers = new ApprovalCustomers();

            BodyPanel.Controls.Clear();
            if (ObjApprovalCustomers.IsDisposed)
            {
                ObjApprovalCustomers = new ApprovalCustomers();
            }
            ObjApprovalCustomers.TopLevel = false;
            ObjApprovalCustomers.FormBorderStyle = FormBorderStyle.None;
            ObjApprovalCustomers.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjApprovalCustomers);
            ObjApprovalCustomers.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            ApprovalMaterials ObjApprovalMaterials = new ApprovalMaterials();

            BodyPanel.Controls.Clear();
            if (ObjApprovalMaterials.IsDisposed)
            {
                ObjApprovalMaterials = new ApprovalMaterials();
            }
            ObjApprovalMaterials.TopLevel = false;
            ObjApprovalMaterials.FormBorderStyle = FormBorderStyle.None;
            ObjApprovalMaterials.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjApprovalMaterials);
            ObjApprovalMaterials.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Purchase_Order_Approval ObjApproval = new Purchase_Order_Approval();

            BodyPanel.Controls.Clear();
            if (ObjApproval.IsDisposed)
            {
                ObjApproval = new Purchase_Order_Approval();
            }
            ObjApproval.TopLevel = false;
            ObjApproval.FormBorderStyle = FormBorderStyle.None;
            ObjApproval.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjApproval);
            ObjApproval.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            FG_Approval ObjApprovalMaterials = new FG_Approval();

            BodyPanel.Controls.Clear();
            if (ObjApprovalMaterials.IsDisposed)
            {
                ObjApprovalMaterials = new FG_Approval();
            }
            ObjApprovalMaterials.TopLevel = false;
            ObjApprovalMaterials.FormBorderStyle = FormBorderStyle.None;
            ObjApprovalMaterials.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjApprovalMaterials);
            ObjApprovalMaterials.Show();

        }

        private void button11_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            RM_Approval ObjApprovalMaterials = new RM_Approval();

            BodyPanel.Controls.Clear();
            if (ObjApprovalMaterials.IsDisposed)
            {
                ObjApprovalMaterials = new RM_Approval();
            }
            ObjApprovalMaterials.TopLevel = false;
            ObjApprovalMaterials.FormBorderStyle = FormBorderStyle.None;
            ObjApprovalMaterials.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjApprovalMaterials);
            ObjApprovalMaterials.Show();
        }

        private void pref_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Deliver_Schedule_Approval ObjSupplierPerformanceMonotoring = new Deliver_Schedule_Approval();

            BodyPanel.Controls.Clear();
            if (ObjSupplierPerformanceMonotoring.IsDisposed)
            {
                ObjSupplierPerformanceMonotoring = new Deliver_Schedule_Approval();
            }
            ObjSupplierPerformanceMonotoring.TopLevel = false;
            ObjSupplierPerformanceMonotoring.FormBorderStyle = FormBorderStyle.None;
            ObjSupplierPerformanceMonotoring.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjSupplierPerformanceMonotoring);
            ObjSupplierPerformanceMonotoring.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            change_Color(sender);
            PO_Details ObjPO_Details = new PO_Details();

            BodyPanel.Controls.Clear();
            if (ObjPO_Details.IsDisposed)
            {
                ObjPO_Details = new PO_Details();
            }
            ObjPO_Details.TopLevel = false;
            ObjPO_Details.FormBorderStyle = FormBorderStyle.None;
            ObjPO_Details.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjPO_Details);
            ObjPO_Details.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Production_App ObjProduction_App = new Production_App();


            BodyPanel.Controls.Clear();
            if (ObjProduction_App.IsDisposed)
            {
                ObjProduction_App = new Production_App();
            }
            ObjProduction_App.TopLevel = false;
            ObjProduction_App.FormBorderStyle = FormBorderStyle.None;
            ObjProduction_App.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjProduction_App);
            ObjProduction_App.Show();
        }
    }
}
