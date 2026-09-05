using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using CRM_App.Crystal;
using WebApplication;
using CrystalDecisions.CrystalReports.Engine;


namespace CRM_App.Transaction
{
    public partial class Invoice_Summary : Form
    {

        public int Distance = 56;
        public Invoice_Summary()
        {
            InitializeComponent();
        }

        private void Purchase_Approval_print_Load(object sender, EventArgs e)
        {
           
            Load_Customer_Name();
            Display();
        }


        public void Load_Customer_Name()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_Load_CM_Name");
                IN_Customer_ID.DataSource = dt;
                IN_Customer_ID.DisplayMember = "CM_Name";
                IN_Customer_ID.ValueMember = "CM_ID";
                IN_Customer_ID.SelectedIndex = -1;
                
            }
            catch { }
        }

       
        public void Display()
        {
            try
            {
                ReportDocument oRpt = new Invoice_Summary_();


                oRpt.DataDefinition.FormulaFields["FromDate"].Text = "'" + From_Date.Value.ToString("dd-MMM-yyyy") + "'";
                oRpt.DataDefinition.FormulaFields["ToDate"].Text = "'" + To_Date.Value.ToString("dd-MMM-yyyy") + "'";

                string SQlQuery = "pr_Print_invoice_Summary '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "'," + IN_Customer_ID.SelectedValue;
                DataTable ReportData = dbFunctions.getTable(SQlQuery);
                oRpt.Database.Tables[0].SetDataSource(ReportData);
                crystalReportViewer1.ReportSource = oRpt;

                crystalReportViewer1.DisplayToolbar = true;

                crystalReportViewer1.DisplayGroupTree = false;

                //crystalReportViewer1.HasToggleGroupTreeButton = false;

            }
            catch (Exception ex) { }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
