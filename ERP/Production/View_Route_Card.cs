using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CRM_App.Crystal;

namespace CRM_App.Production
{
    public partial class View_Route_Card : Form
    {
        public string ErrorMessage = "";
        public string ID = "";
        public View_Route_Card()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void View_Route_Card_Load(object sender, EventArgs e)
        {

            try
            {
                ReportDocument oRpt = new RouteCard_Print();



                

               
                DataTable dd1 = dbFunctions.getTable("pr_Print_Production_Route_Card  " + dbFunctions.Route_Card_ID + "");
                oRpt.Subreports["Production_Details.rpt"].SetDataSource(dd1);



                DataTable dd4 = dbFunctions.getTable("pr_Print_Production_FLITTLING_Inspection  " + dbFunctions.Route_Card_ID + "");
                oRpt.Subreports["Flittling_Inspection_Details.rpt"].SetDataSource(dd4);

                DataTable dd3 = dbFunctions.getTable("pr_Print_Production_Sandblasting_Inspection  " + dbFunctions.Route_Card_ID + "");
                oRpt.Subreports[".Sandblasting_Inspection_Details.rpt"].SetDataSource(dd3);
                //ReportDocument subreport1 = oRpt.Subreports[0];
                //subreport1.SetDataSource(dd1);

                DataTable dd = dbFunctions.getTable("pr_Print_Production_Final_Inspection  " + dbFunctions.Route_Card_ID + "");
                oRpt.Subreports["Final_Inspection_Details.rpt"].SetDataSource(dd);
                //ReportDocument subreport = oRpt.Subreports[1];
                //subreport.SetDataSource(dd);


                DataTable dd2 = dbFunctions.getTable("get_Stock_data  " + dbFunctions.Route_Card_ID + "");

                for (int i =1; i < dd2.Rows.Count; i++)
                {
                    dd2.Rows[i]["Openning"] = dd2.Rows[i - 1]["Closing"];
                    dd2.Rows[i]["Closing"] = decimal.Parse(dd2.Rows[i]["Openning"].ToString()) + decimal.Parse(dd2.Rows[i]["Inward"].ToString()) - decimal.Parse(dd2.Rows[i]["Outward"].ToString());
                }

                oRpt.Subreports["Invoice_Issue_Details.rpt"].SetDataSource(dd2);
             //   ReportDocument subreport2 = oRpt.Subreports[2];
               // subreport2.SetDataSource(dd2);

                string SQlQuery = "pr_print_RouteCard '" + dbFunctions.Route_Card_ID + "'";
                DataTable ReportData = dbFunctions.getTable(SQlQuery);
                oRpt.Database.Tables[0].SetDataSource(ReportData);
                crystalReportViewer1.ReportSource = oRpt;

                
                crystalReportViewer1.DisplayToolbar = true;

                crystalReportViewer1.DisplayGroupTree = false;

                //crystalReportViewer1.HasToggleGroupTreeButton = false;

            }
            catch(Exception ex) { }

        }

        private void button8_Click(object sender, EventArgs e)
        {


            ReportDocument oRpt = new RouteCard_Print();



            DataTable dd = dbFunctions.getTable("pr_Print_Production_Final_Inspection  " + dbFunctions.Route_Card_ID + "");
            ReportDocument subreport = oRpt.Subreports["Subreport2"];
            subreport.SetDataSource(dd);


            DataTable dd1 = dbFunctions.getTable("pr_Print_Production_Route_Card  " + dbFunctions.Route_Card_ID + "");
            ReportDocument subreport1 = oRpt.Subreports["Subreport1"];
            subreport1.SetDataSource(dd1);

            DataTable dd2 = dbFunctions.getTable("pr_Print_Despatch_Invoice_details  " + dbFunctions.Route_Card_ID + "");
            ReportDocument subreport2 = oRpt.Subreports["Subreport3"];
            subreport2.SetDataSource(dd2);


            string SQlQuery = "pr_print_RouteCard '" + dbFunctions.Route_Card_ID + "'";


            dbFunctions.printWord("RouteCard", SQlQuery, oRpt);


        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
