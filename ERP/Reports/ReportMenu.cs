using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using LarchERP.Master;
using CRM_App.Transaction;

namespace GenuineHR.Reports
{
    public partial class ReportMenu : Form
    {
        public ReportMenu()
        {
            InitializeComponent();
        }

        private void ReportMenu_Load(object sender, EventArgs e)
        {
            foreach (TreeNode tn in treeView1.Nodes)
            {
                if (tn.Text.Equals("Reports"))
                {
                    tn.Expand();

                }
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string str = e.Node.Text;

            if (str.Equals("RM Stock"))
            {
                BodyPanel.Controls.Clear();
                Reports.Current_Stock obj = new Reports.Current_Stock();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();
                obj.UpdateCellColors();
            }


            if (str.Equals("Machinewise Plan"))
            {
                BodyPanel.Controls.Clear();
                Reports.Machine_Plan_Details obj = new Reports.Machine_Plan_Details();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }

            if (str.Equals("B/O Stock"))
            {
                BodyPanel.Controls.Clear();
                Reports.BO_Stock obj = new Reports.BO_Stock();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();
                obj.UpdateCellColors();

            }

            if (str.Equals("Return Report"))
            {
                BodyPanel.Controls.Clear();
                CRM_App.Reports.Return_Report obj = new CRM_App.Reports.Return_Report();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();
                //obj.UpdateCellColors();

            }
            if (str.Equals("FG Stock"))
            {
                BodyPanel.Controls.Clear();
                FG_Stock obj = new FG_Stock();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }


            if (str.Equals("Sub FG Stock"))
            {
                BodyPanel.Controls.Clear();
                Sub_FG_Stock obj = new Sub_FG_Stock();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }
            
            if (str.Equals("ASS Stock"))
            {
                BodyPanel.Controls.Clear();
                GenuineHR.Reports.Assemblyandinvoicereport obj = new GenuineHR.Reports.Assemblyandinvoicereport();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();
            }
            if (str.Equals("JO DC Report"))
            {
                BodyPanel.Controls.Clear();
                CRM_App.Reports.DC_Report obj = new CRM_App.Reports.DC_Report();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();
            }

            if (str.Equals("Assembly Details"))
            {
                BodyPanel.Controls.Clear();
                Reports.Assebly_Details obj = new Reports.Assebly_Details();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }

            if (str.Equals("RootCard Details"))
            {
                BodyPanel.Controls.Clear();
                AllList_Route_Card obj = new AllList_Route_Card();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }

            
            
            if (str.Equals("Final Inspection"))
            {
                BodyPanel.Controls.Clear();
                Print_Final_QC_Details obj = new Print_Final_QC_Details();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }

            if (str.Equals("Mess Cutting Stock"))
            {
                BodyPanel.Controls.Clear();
                MessCutting_Stock obj = new MessCutting_Stock();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }

            
            
            if (str.Equals("Inprocess Inspection Details"))
            {
                BodyPanel.Controls.Clear();
                Print_Inprocess_QC obj = new Print_Inprocess_QC();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }
            if (str.Equals("Wip_Stock"))
            {
                BodyPanel.Controls.Clear();
                CRM_App.Reports.Wip_Stock obj = new CRM_App.Reports.Wip_Stock();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }
            if (str.Equals("OEE REPORT"))
            {
                BodyPanel.Controls.Clear();
                CRM_App.Reports.oa_report obj = new CRM_App.Reports.oa_report();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }
            if (str.Equals("Production & Idl"))
            {
                BodyPanel.Controls.Clear();
                GenuineHR.Reports.Prodution_Rejection_Details obj = new GenuineHR.Reports.Prodution_Rejection_Details();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }
            if (str.Equals("Lable Print"))
            {
                BodyPanel.Controls.Clear();
                GenuineHR.Reports.Lable_Print obj = new GenuineHR.Reports.Lable_Print();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }

            if (str.Equals("FG Stock"))
            {
                BodyPanel.Controls.Clear();
                FG_Stock obj = new FG_Stock();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }

            
            if (str.Equals("posales"))
            {
                BodyPanel.Controls.Clear();
                LarchERP.Master.poandsales obj = new LarchERP.Master.poandsales();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }

            if (str.Equals("Sales Report"))
            {
                BodyPanel.Controls.Clear();
                GenuineHR.Reports.Salesqtyreport obj = new GenuineHR.Reports.Salesqtyreport();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }

            if (str.Equals("Quality Entry"))
            {
                BodyPanel.Controls.Clear();
                CRM_App.Reports.Quality_Entry obj = new CRM_App.Reports.Quality_Entry();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }

            if (str.Equals("SAFETY INCIDENT REPORT"))
            {
                BodyPanel.Controls.Clear();
                CRM_App.Transaction.SAFETY_INCIDENT_REPORT obj = new CRM_App.Transaction.SAFETY_INCIDENT_REPORT();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }
            if (str.Equals("RM Reconciliation"))
            {
                BodyPanel.Controls.Clear();
                GenuineHR.Reports.RM_MATERIAL_RECONCILATION obj = new GenuineHR.Reports.RM_MATERIAL_RECONCILATION();
                obj.TopLevel = false;
                obj.Dock = DockStyle.Fill;
                obj.FormBorderStyle = FormBorderStyle.None;
                BodyPanel.Controls.Add(obj);
                obj.Show();

            }
        }
        


        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
             
        }
    }
}
