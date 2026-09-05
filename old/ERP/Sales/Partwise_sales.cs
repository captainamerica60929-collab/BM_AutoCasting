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
    public partial class Partwise_sales : Form
    {

        public int Distance = 56;
        public Partwise_sales()
        {
            InitializeComponent();
        }

        private void Purchase_Approval_print_Load(object sender, EventArgs e)
        {
            Display();
            Load_Part_Name();
        }


        private void Load_Part_Name()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("Pr_get_FG_Part_Name_All");
                INV_Item_ID.DataSource = dt;
                INV_Item_ID.DisplayMember = "IM_PartNo";
                INV_Item_ID.ValueMember = "IM_ID";
                INV_Item_ID.SelectedIndex = -1;
                
            }
            catch
            {
            }
        }
        public void Display()
        {
            try
            {
                ReportDocument oRpt = new Invoice_Based_On_Part_();


                oRpt.DataDefinition.FormulaFields["FromDate"].Text = "'" + From_Date.Value.ToString("dd-MMM-yyyy") + "'";
                oRpt.DataDefinition.FormulaFields["ToDate"].Text = "'" + To_Date.Value.ToString("dd-MMM-yyyy") + "'";

                string SQlQuery = "pr_Print_Partwise_Sales '" + From_Date.Value.ToString("yyyyMMdd") + "','" + To_Date.Value.ToString("yyyyMMdd") + "'," + INV_Item_ID.SelectedValue;
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

        private void INV_Item_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

    }
}
