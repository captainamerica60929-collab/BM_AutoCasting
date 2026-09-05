using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CRM_App.Crystal;

namespace GenuineHR.Reports
{
    public partial class Machine_Plan_Details : Form
    {
        public Machine_Plan_Details()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            getdata();
                 }

        void getdata()
        {
            //DataTable dt = dbFunctions.getTable("pr_Display_Machine_Master");
            //dataGridView1.DataSource = dt;
            //dbFunctions.DGVStyle(dataGridView1);
            //txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();

            //DataTable dt = dbFunctions.getTable("SELECT MM_MachineName as MachineName,Pq_RM_Spec as [RM PART NO],Pq_RM_Grade AS [RM PART NAME] FROM Production_Request LEFT OUTER JOIN Machine_Master  ON Pq_Machine_ID = MM_ID  WHERE  CONVERT(DATE, Pq_CreatedDate)  = '{todate.Value.ToString("dd-MM-yyyy")}");
            DataTable dt = dbFunctions.getTable($@"
    SELECT mm_id,PL_Name AS [Plant Name], Pq_Machine_Name as [Machine Code],              
Pq_vPart_No  AS [PART NAME] ,Pq_RM_Grade as [RM GRADE],Pq_RM_Plan_Qty AS [PLAN QTY],txt_Hourly_Pro AS [HOURLY PROD]
    FROM Production_Request 
    LEFT OUTER JOIN Machine_Master ON Pq_Machine_ID = MM_ID 
    left outer join Item_Master on IM_ID=Pq_iPart_ID
	left outer join plant_master on PL_ID=IM_Plant
    WHERE Pq_Status='A' and CONVERT(DATE, Pq_CreatedDate) = '{todate.Value.ToString("yyyy-MM-dd")}' ORDER BY [Pq_Machine_Name] ASC");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);


        }

        void calc()
        {
            int total = 0;
            int present = 0;
            int absent = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                total += int.Parse(dataGridView1.Rows[i].Cells["Total Employee"].Value.ToString());
                present += int.Parse(dataGridView1.Rows[i].Cells["Present"].Value.ToString());
                absent += int.Parse(dataGridView1.Rows[i].Cells["Absent"].Value.ToString());

            }
            label5.Text = total.ToString();
            label7.Text = present.ToString();
            label9.Text = absent.ToString();
        }

        private void shift_SelectedIndexChanged(object sender, EventArgs e)
        {
            getdata();
        }

        private void ToadayStatus_Load(object sender, EventArgs e)
        {
            getdata();
            loadpalt();




        }

        private void loadpalt()
        {
            try
            {
                DataTable a = dbFunctions.getTable("select * from plant_master where pl_status='A'");
                plant.DataSource = a;
                plant.DisplayMember = "PL_Name";
                plant.ValueMember = "PL_ID";
                plant.SelectedIndex = -1;
            }
            catch { }
        }
        private void ToadayStatus_Activated(object sender, EventArgs e)
        {
            getdata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getdata();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;

        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBoxX1_Enter(object sender, EventArgs e)
        {
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Machine_plan oRpt = new Machine_plan();

                string SQlQuery = "Machinewise_Plan '" + todate.Value.ToString("yyyyMMdd") + "'";// '" + dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString() + "'," + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + ",'" + dataGridView1.SelectedRows[0].Cells["Shift"].Value.ToString() + "'";
                dbFunctions.printpdf("QC", SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
            //try
            //{
            //    Cursor.Current = Cursors.WaitCursor;
            //    Production_Plan_ActualPrint oRpt = new Production_Plan_ActualPrint();
            //    string SQlQuery = "pr_PRINT_Machinewise_Plan  '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "','" + todate.Value.ToString("yyyyMMdd") + "'";

            //    oRpt.DataDefinition.FormulaFields["Date1"].Text = "'" + todate.Value.ToString("dd-MMM-yyyy")+ "'";
            //    oRpt.DataDefinition.FormulaFields["Date2"].Text = "'" + todate.Value.AddDays(1).ToString("dd-MMM-yyyy") + "'";
            //    oRpt.DataDefinition.FormulaFields["Date3"].Text = "'" + todate.Value.AddDays(2).ToString("dd-MMM-yyyy") + "'";
            //    oRpt.DataDefinition.FormulaFields["Date4"].Text = "'" + todate.Value.AddDays(3).ToString("dd-MMM-yyyy") + "'";
            //    oRpt.DataDefinition.FormulaFields["Date5"].Text = "'" + todate.Value.AddDays(4).ToString("dd-MMM-yyyy") + "'";
            //    oRpt.DataDefinition.FormulaFields["Date6"].Text = "'" + todate.Value.AddDays(5).ToString("dd-MMM-yyyy") + "'";
            //    oRpt.DataDefinition.FormulaFields["Date7"].Text = "'" + todate.Value.AddDays(6).ToString("dd-MMM-yyyy") + "'";
            //    oRpt.DataDefinition.FormulaFields["Machine"].Text = "'" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + "'";
            //    dbFunctions.printpdf("Plan_Sheet", SQlQuery, oRpt);
            //    Cursor.Current = Cursors.Default;
            //}
            //catch { }

        }

        private void button10_Click(object sender, EventArgs e)
        {
             //dbFunctions.isclose = true; this.Close(); 
            DialogResult result = MessageBox.Show("Are You Sure Want to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {

                this.Close();

            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            getdata();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void todate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void plant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(plant.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Plant Name] LIKE '%{0}%'", plant.Text);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
