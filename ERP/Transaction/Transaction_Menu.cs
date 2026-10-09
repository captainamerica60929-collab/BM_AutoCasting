using System;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using ERP.Transaction;
using System.Data;
using LarchERP.Master;

namespace CRM_App.Transaction
{
    public partial class Transaction_Menu : Form
    {
        public Transaction_Menu()
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
            
                Purchase_Order.Visible=false;
                PO_Amendment.Visible=false;
                PO_Deliver_Schedule.Visible=false;
                PO_Print.Visible=false;
                GRN.Visible=false;
                Incoming_Inspection.Visible=false;
                GRN_Approval.Visible=false;
                GRN_Label_Print.Visible=false;
                GRN_QC_Details_Print.Visible=false;
                GRN_Details.Visible=false;
                Supplier_Problem.Visible=false;
                Deliver_Schedule_Details.Visible=false;
                Deliver_Performance.Visible = false;
            button7.Visible = false;
            MaterialIssue.Visible = false;
            button1.Visible = false;
        }

        public void MenuRights_fn()
        {
            DataTable dtMenuRights = dbFunctions.getTable("pr_FetchManuRights '" + dbFunctions.rights + "'");
            if (dtMenuRights.Rows.Count > 0)
            {
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Purchase_Order")
                    {
                        Purchase_Order.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "PO_Amendment")
                    {
                        PO_Amendment.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "PO_Deliver_Schedule")
                    {
                        //PO_Deliver_Schedule.Visible = true;
                    }

                }

                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "PO_Print")
                    {
                        PO_Print.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "GRN")
                    {
                        GRN.Visible = true;
                    }

                }

                //for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                //{
                //    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Incoming_Inspection")
                //    {
                //        Incoming_Inspection.Visible = true;
                //    }

                //}


                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "GRN_Approval")
                    {
                        GRN_Approval.Visible = true;
                    }

                }


                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "GRN_Label_Print")
                    {
                        GRN_Label_Print.Visible = true;
                    }

                }

                //for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                //{
                //    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "GRN_QC_Details_Print")
                //    {
                //        GRN_QC_Details_Print.Visible = true;
                //    }

                //}


                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "GRN_Details")
                    {
                        GRN_Details.Visible = true;
                    }

                }


                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Supplier_Problem")
                    {
                        Supplier_Problem.Visible = true;
                    }

                }



                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Deliver_Schedule_Details")
                    {
                        Deliver_Schedule_Details.Visible = true;
                    }

                }



                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Deliver_Performance")
                    {
                        Deliver_Performance.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "JO DC")
                    {
                        button7.Visible = true;
                    }

                }

                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Material Issue")
                    {
                        MaterialIssue.Visible = true;
                    }

                }

                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Sub Part  Issue")
                    {
                       // button1.Visible = true;
                    }

                }


            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Purchase_Order ObjPurchase_Order = new Purchase_Order();

            BodyPanel.Controls.Clear();
            if (ObjPurchase_Order.IsDisposed)
            {
                ObjPurchase_Order = new Purchase_Order();
            }
            ObjPurchase_Order.TopLevel = false;
            ObjPurchase_Order.FormBorderStyle = FormBorderStyle.None;
            ObjPurchase_Order.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjPurchase_Order);
            ObjPurchase_Order.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //change_Color(sender);
            //Purchase_Order_Approval ObjApproval = new Purchase_Order_Approval();

            //BodyPanel.Controls.Clear();
            //if (ObjApproval.IsDisposed)
            //{
            //    ObjApproval = new Purchase_Order_Approval();
            //}
            //ObjApproval.TopLevel = false;
            //ObjApproval.FormBorderStyle = FormBorderStyle.None;
            //ObjApproval.Dock = DockStyle.Fill;
            //BodyPanel.Controls.Add(ObjApproval);
            //ObjApproval.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Purchase_Order_Amendment ObjPurchase_Order_Amendment = new Purchase_Order_Amendment();

            BodyPanel.Controls.Clear();
            if (ObjPurchase_Order_Amendment.IsDisposed)
            {
                ObjPurchase_Order_Amendment = new Purchase_Order_Amendment();
            }
            ObjPurchase_Order_Amendment.TopLevel = false;
            ObjPurchase_Order_Amendment.FormBorderStyle = FormBorderStyle.None;
            ObjPurchase_Order_Amendment.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjPurchase_Order_Amendment);
            ObjPurchase_Order_Amendment.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Purchase_Print ObjPurchase_Approval_print = new Purchase_Print();

            BodyPanel.Controls.Clear();
            if (ObjPurchase_Approval_print.IsDisposed)
            {
                ObjPurchase_Approval_print = new Purchase_Print();
            }
            ObjPurchase_Approval_print.TopLevel = false;
            ObjPurchase_Approval_print.FormBorderStyle = FormBorderStyle.None;
            ObjPurchase_Approval_print.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjPurchase_Approval_print);
            ObjPurchase_Approval_print.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Grin_Against_Purchase_Order ObjGrin_Against_Purchase_Order = new Grin_Against_Purchase_Order();

            BodyPanel.Controls.Clear();
            if (ObjGrin_Against_Purchase_Order.IsDisposed)
            {
                ObjGrin_Against_Purchase_Order = new Grin_Against_Purchase_Order();
            }
            ObjGrin_Against_Purchase_Order.TopLevel = false;
            ObjGrin_Against_Purchase_Order.FormBorderStyle = FormBorderStyle.None;
            ObjGrin_Against_Purchase_Order.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjGrin_Against_Purchase_Order);
            ObjGrin_Against_Purchase_Order.Show();
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button19_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            GRN_Details_Print ObjGRN_Details_Print = new GRN_Details_Print();

            BodyPanel.Controls.Clear();
            if (ObjGRN_Details_Print.IsDisposed)
            {
                ObjGRN_Details_Print = new GRN_Details_Print();
            }
            ObjGRN_Details_Print.TopLevel = false;
            ObjGRN_Details_Print.FormBorderStyle = FormBorderStyle.None;
            ObjGRN_Details_Print.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjGRN_Details_Print);
            ObjGRN_Details_Print.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Supplier_Proble_Details ObjInvoice_Approval = new Supplier_Proble_Details();

            BodyPanel.Controls.Clear();
            if (ObjInvoice_Approval.IsDisposed)
            {
                ObjInvoice_Approval = new Supplier_Proble_Details();
            }
            ObjInvoice_Approval.TopLevel = false;
            ObjInvoice_Approval.FormBorderStyle = FormBorderStyle.None;
            ObjInvoice_Approval.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjInvoice_Approval);
            ObjInvoice_Approval.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Deliver_Schedule_Details ObjDeliver_Schedule = new Deliver_Schedule_Details();

            BodyPanel.Controls.Clear();
            if (ObjDeliver_Schedule.IsDisposed)
            {
                ObjDeliver_Schedule = new Deliver_Schedule_Details();
            }
            ObjDeliver_Schedule.TopLevel = false;
            ObjDeliver_Schedule.FormBorderStyle = FormBorderStyle.None;
            ObjDeliver_Schedule.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjDeliver_Schedule);
            ObjDeliver_Schedule.Show();
        }

        private void button28_Click(object sender, EventArgs e)
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

        private void BodyPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            change_Color(sender);
            //GRN_Approval ObjGrin_Against_PO_Print = new GRN_Approval();
            GRN_Approval ObjGrin_Against_PO_Print = new GRN_Approval();

            BodyPanel.Controls.Clear();
            if (ObjGrin_Against_PO_Print.IsDisposed)
            {
                ObjGrin_Against_PO_Print = new GRN_Approval();
            }
            ObjGrin_Against_PO_Print.TopLevel = false;
            ObjGrin_Against_PO_Print.FormBorderStyle = FormBorderStyle.None;
            ObjGrin_Against_PO_Print.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjGrin_Against_PO_Print);
            ObjGrin_Against_PO_Print.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            change_Color(sender);
            Print_Label ObjGrin_Against_PO_Print = new Print_Label();

            BodyPanel.Controls.Clear();
            if (ObjGrin_Against_PO_Print.IsDisposed)
            {
                ObjGrin_Against_PO_Print = new Print_Label();
            }
            ObjGrin_Against_PO_Print.TopLevel = false;
            ObjGrin_Against_PO_Print.FormBorderStyle = FormBorderStyle.None;
            ObjGrin_Against_PO_Print.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjGrin_Against_PO_Print);
            ObjGrin_Against_PO_Print.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Deliver_Schedule ObjDeliver_Schedule = new Deliver_Schedule();

            BodyPanel.Controls.Clear();
            if (ObjDeliver_Schedule.IsDisposed)
            {
                ObjDeliver_Schedule = new Deliver_Schedule();
            }
            ObjDeliver_Schedule.TopLevel = false;
            ObjDeliver_Schedule.FormBorderStyle = FormBorderStyle.None;
            ObjDeliver_Schedule.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjDeliver_Schedule);
            ObjDeliver_Schedule.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {

            change_Color(sender);
            Supplier_Perfomanace ObjSupplier_Perfomanace = new Supplier_Perfomanace();

            BodyPanel.Controls.Clear();
            if (ObjSupplier_Perfomanace.IsDisposed)
            {
                ObjSupplier_Perfomanace = new Supplier_Perfomanace();
            }
            ObjSupplier_Perfomanace.TopLevel = false;
            ObjSupplier_Perfomanace.FormBorderStyle = FormBorderStyle.None;
            ObjSupplier_Perfomanace.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjSupplier_Perfomanace);
            ObjSupplier_Perfomanace.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
           
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            change_Color(sender);
            // CRM_App.Transaction.Work_Order.Work_Order ObjSupplier_Perfomanace = new CRM_App.Transaction.Work_Order.Work_Order();
            CRM_App.Transaction.Work_Order.New_Work_Order ObjSupplier_Perfomanace = new CRM_App.Transaction.Work_Order.New_Work_Order();

            BodyPanel.Controls.Clear();
            if (ObjSupplier_Perfomanace.IsDisposed)
            {
                ObjSupplier_Perfomanace = new CRM_App.Transaction.Work_Order.New_Work_Order();
            }
            ObjSupplier_Perfomanace.TopLevel = false;
            ObjSupplier_Perfomanace.FormBorderStyle = FormBorderStyle.None;
            ObjSupplier_Perfomanace.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjSupplier_Perfomanace);
            ObjSupplier_Perfomanace.Show();
        }

        private void MaterialIssue_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            CRM_App.Production.MaterialIssue_Issue objMaterialIssue_Issue = new CRM_App.Production.MaterialIssue_Issue();

            BodyPanel.Controls.Clear();
            if (objMaterialIssue_Issue.IsDisposed)
            {
                objMaterialIssue_Issue = new CRM_App.Production.MaterialIssue_Issue();
            }
            objMaterialIssue_Issue.TopLevel = false;
            objMaterialIssue_Issue.FormBorderStyle = FormBorderStyle.None;
            objMaterialIssue_Issue.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objMaterialIssue_Issue);
            objMaterialIssue_Issue.Show();
        }

        private void button1_Click_2(object sender, EventArgs e)
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
    }
}
