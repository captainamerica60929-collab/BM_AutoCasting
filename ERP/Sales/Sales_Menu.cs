using System;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using ERP.Transaction;
using GenuineHR.Reports;
using System.Data;
using CRM_App.Master;

namespace CRM_App.Transaction
{
    public partial class Sales_Menu : Form
    {
        public Sales_Menu()
        {
            InitializeComponent();
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



        private void Transaction_Menu_Load(object sender, EventArgs e)
        {
            Menu_False();
            MenuRights_fn();
            

            Clear_Buttons();
            LoadMaster();
        }

        public void Menu_False()
        {
            
            Monthly_Schedule.Enabled = false;
            Invoice.Enabled = false;
            Edit_Invoice.Enabled = false;
            Invoice_Print.Enabled = false;
            FG_Stock.Enabled = false;
            Invoice_Summary.Enabled = false;
            Inv_Base_on_Part.Enabled = false;
            Invoice_Qty.Enabled = false;
            Delivery_Challan.Enabled = false;
            Delivery_Chllan_Details.Enabled = false;

                
  
        }

        public void MenuRights_fn()
        {
              DataTable dtMenuRights = dbFunctions.getTable("pr_FetchManuRights '" + dbFunctions.rights + "'");
              if (dtMenuRights.Rows.Count > 0)
              {
                  for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                  {
                      if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Monthly_Schedule")
                      {
                          Monthly_Schedule.Enabled = true;
                      }

                  }
                  for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                  {
                      if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Invoice")
                      {
                          Invoice.Enabled = true;
                      }
                  }
                   for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                      {
                          if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Edit_Invoice")
                          {
                              Edit_Invoice.Enabled = true;
                          }

                      }
                     for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                      {
                          if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Invoice_Print")
                          {
                              Invoice_Print.Enabled = true;
                          }
                      }

                   for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                          {
                              if (dtMenuRights.Rows[i]["MenuId"].ToString() == "FG_Stock")
                              {
                                  FG_Stock.Enabled = true;
                              }

                          }

                   for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                   {
                       if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Inv_Base_on_Part")
                       {
                           Inv_Base_on_Part.Enabled = true;
                       }

                   }


                   for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                   {
                       if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Invoice_Qty")
                       {
                           Invoice_Qty.Enabled = true;
                       }

                   }


                   for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                   {
                       if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Delivery_Challan")
                       {
                           Delivery_Challan.Enabled = true;
                       }

                   }


                   for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                   {
                       if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Delivery_Chllan_Details")
                       {
                           Delivery_Chllan_Details.Enabled = true;
                       }

                   }

              
              
              }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Invoice ObjInvoice = new Invoice();

            BodyPanel.Controls.Clear();
            if (ObjInvoice.IsDisposed)
            {
                ObjInvoice = new Invoice();
            }
            ObjInvoice.TopLevel = false;
            ObjInvoice.FormBorderStyle = FormBorderStyle.None;
            ObjInvoice.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjInvoice);
            ObjInvoice.Show();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Print_Invoice ObjInvoice_Print = new Print_Invoice();

            BodyPanel.Controls.Clear();
            if (ObjInvoice_Print.IsDisposed)
            {
                ObjInvoice_Print = new Print_Invoice();
            }
            ObjInvoice_Print.TopLevel = false;
            ObjInvoice_Print.FormBorderStyle = FormBorderStyle.None;
            ObjInvoice_Print.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjInvoice_Print);
            ObjInvoice_Print.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            FG_Stock ObjInvoice_Print = new FG_Stock();

            BodyPanel.Controls.Clear();
            if (ObjInvoice_Print.IsDisposed)
            {
                ObjInvoice_Print = new FG_Stock();
            }

            ObjInvoice_Print.TopLevel = false;
            ObjInvoice_Print.FormBorderStyle = FormBorderStyle.None;
            ObjInvoice_Print.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjInvoice_Print);
            ObjInvoice_Print.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            Invoice_Summary ObjInvoice_Print = new Invoice_Summary();

            BodyPanel.Controls.Clear();
            if (ObjInvoice_Print.IsDisposed)
            {
                ObjInvoice_Print = new Invoice_Summary();
            }

            ObjInvoice_Print.TopLevel = false;
            ObjInvoice_Print.FormBorderStyle = FormBorderStyle.None;
            ObjInvoice_Print.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjInvoice_Print);
            ObjInvoice_Print.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            Partwise_sales ObjInvoice_Print = new Partwise_sales();

            BodyPanel.Controls.Clear();
            if (ObjInvoice_Print.IsDisposed)
            {
                ObjInvoice_Print = new Partwise_sales();
            }

            ObjInvoice_Print.TopLevel = false;
            ObjInvoice_Print.FormBorderStyle = FormBorderStyle.None;
            ObjInvoice_Print.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjInvoice_Print);
            ObjInvoice_Print.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            change_Color(sender);
            Monthly_Schedule1 ObjInvoice_Print = new Monthly_Schedule1();

            BodyPanel.Controls.Clear();
            if (ObjInvoice_Print.IsDisposed)
            {
                ObjInvoice_Print = new Monthly_Schedule1();
            }

            ObjInvoice_Print.TopLevel = false;
            ObjInvoice_Print.FormBorderStyle = FormBorderStyle.None;
            ObjInvoice_Print.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjInvoice_Print);
            ObjInvoice_Print.Show();
        }
        Edit_Invoice ObjEdit_Invoice = new Edit_Invoice();
        private void btnEditSales_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            BodyPanel.Controls.Clear();
            if (ObjEdit_Invoice.IsDisposed)
            {
                ObjEdit_Invoice = new Edit_Invoice();
            }

            ObjEdit_Invoice.TopLevel = false;
            ObjEdit_Invoice.FormBorderStyle = FormBorderStyle.None;
            ObjEdit_Invoice.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjEdit_Invoice);
            ObjEdit_Invoice.Show();
        }
        Update_Routecard objUpdate_Routecard = new Update_Routecard();
        private void button7_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            BodyPanel.Controls.Clear();
            if (objUpdate_Routecard.IsDisposed)
            {
                objUpdate_Routecard = new Update_Routecard();
            }

            objUpdate_Routecard.TopLevel = false;
            objUpdate_Routecard.FormBorderStyle = FormBorderStyle.None;
            objUpdate_Routecard.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objUpdate_Routecard);
            objUpdate_Routecard.Show();
        }


        Delivery_Chanllan objDelivery_Chanllan = new Delivery_Chanllan();
        private void button9_Click(object sender, EventArgs e)
        {


            change_Color(sender);
            BodyPanel.Controls.Clear();
            if (objDelivery_Chanllan.IsDisposed)
            {
                objDelivery_Chanllan = new Delivery_Chanllan();
            }

            objDelivery_Chanllan.TopLevel = false;
            objDelivery_Chanllan.FormBorderStyle = FormBorderStyle.None;
            objDelivery_Chanllan.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objDelivery_Chanllan);
            objDelivery_Chanllan.Show();

        }

        Delivery_Challan_Print objDelivery_Challan_Print = new Delivery_Challan_Print();
        private void button16_Click(object sender, EventArgs e)
        {

              change_Color(sender);
            BodyPanel.Controls.Clear();
            if (objDelivery_Chanllan.IsDisposed)
            {
                objDelivery_Challan_Print = new Delivery_Challan_Print();
            }

            objDelivery_Challan_Print.TopLevel = false;
            objDelivery_Challan_Print.FormBorderStyle = FormBorderStyle.None;
            objDelivery_Challan_Print.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objDelivery_Challan_Print);
            objDelivery_Challan_Print.Show();

        }

        CRM_App.Sales.Customer_complient Customer_complient = new CRM_App.Sales.Customer_complient();
        private void button7_Click_1(object sender, EventArgs e)
        {
            change_Color(sender);
            BodyPanel.Controls.Clear();
            if (objDelivery_Chanllan.IsDisposed)
            {
                Customer_complient = new CRM_App.Sales.Customer_complient();
            }

            Customer_complient.TopLevel = false;
            Customer_complient.FormBorderStyle = FormBorderStyle.None;
            Customer_complient.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(Customer_complient);
            Customer_complient.Show();


        }
        CRM_App.Sales.SAFETY_INCIDENT SAFETY_INCIDENT_REPORT = new CRM_App.Sales.SAFETY_INCIDENT();
        private void button16_Click_1(object sender, EventArgs e)
        {
           
            change_Color(sender);
                BodyPanel.Controls.Clear();
                if (objDelivery_Chanllan.IsDisposed)
                {
                SAFETY_INCIDENT_REPORT = new CRM_App.Sales.SAFETY_INCIDENT();
                }

            SAFETY_INCIDENT_REPORT.TopLevel = false;
            SAFETY_INCIDENT_REPORT.FormBorderStyle = FormBorderStyle.None;
            SAFETY_INCIDENT_REPORT.Dock = DockStyle.Fill;
                BodyPanel.Controls.Add(SAFETY_INCIDENT_REPORT);
            SAFETY_INCIDENT_REPORT.Show();


            
        }
    }
}
