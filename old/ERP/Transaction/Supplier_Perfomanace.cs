using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using CRM_App.Crystal;
using CrystalDecisions.CrystalReports.Engine;

namespace CRM_App.Transaction
{
    public partial class Supplier_Perfomanace : Form
    {
        public Supplier_Perfomanace()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            display_Details();
            display();
        }

        public void display_Details()
        {
            DataTable dt = dbFunctions.getTable("pr_get_Supplier_Performance '" + PO_dPO_Date.Value.ToString("dd-MMM-yyyy") + "'," + PO_vSupplier_Name.SelectedValue + ",'Quality'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);


            DataTable dt1 = dbFunctions.getTable("pr_get_Supplier_Performance '" + PO_dPO_Date.Value.ToString("dd-MMM-yyyy") + "'," + PO_vSupplier_Name.SelectedValue + ",'Perfomance'");
            dataGridView2.DataSource = dt1;
            dbFunctions.DGVStyle(dataGridView2);
        }

        public void display()
        {
            DataTable dt = dbFunctions.getTable("pr_get_Monthly_Supplier_Peromance " + PO_vSupplier_Name.SelectedValue );
            dataGridView3.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView3);


        }
           
           

        private void Supplier_Perfomanace_Load(object sender, EventArgs e)
        {
            LoadSupplier();
        }
        public void LoadSupplier()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadSupplier");
                PO_vSupplier_Name.DataSource = dt;
                PO_vSupplier_Name.DisplayMember = "SM_Name";
                PO_vSupplier_Name.ValueMember = "SM_ID";
                PO_vSupplier_Name.SelectedIndex = -1;
                
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insert();
            insert1();
        }



        public void insert1()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_Supplier_Performance_Details";
                    com.Parameters.Add("@SPD_Month", SqlDbType.VarChar).Value = PO_dPO_Date.Value.ToString("dd-MMM-yyyy");
                    com.Parameters.Add("@SPD_Supplier", SqlDbType.VarChar).Value = PO_vSupplier_Name.SelectedValue.ToString();
                    com.Parameters.Add("@SPD_SPM_ID", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["ID"].Value.ToString();
                    com.Parameters.Add("@SPD_Type", SqlDbType.VarChar).Value = "Perfomance";
                    com.Parameters.Add("@SPD_Rating_Element", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Rating Element"].Value.ToString();
                    com.Parameters.Add("@SPD_Weightage", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Weightage"].Value.ToString();
                    com.Parameters.Add("@SPD_MonthlyData", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Data"].Value.ToString();
                    com.Parameters.Add("@SPD_Score", SqlDbType.VarChar).Value = dataGridView2.Rows[i].Cells["Score"].Value.ToString();
                    com.Parameters.Add("@SPD_Create_by", SqlDbType.VarChar).Value = dbFunctions.username;
                    com.ExecuteNonQuery();
                    com.Connection.Close();
                }
                catch (Exception Ex)
                {
                    dbFunctions.Logs(Ex.Message, dbFunctions.username);
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                display();
            }

        }



        public void insert()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_Supplier_Performance_Details";
                    com.Parameters.Add("@SPD_Month", SqlDbType.VarChar).Value = PO_dPO_Date.Value.ToString("dd-MMM-yyyy");
                    com.Parameters.Add("@SPD_Supplier", SqlDbType.VarChar).Value = PO_vSupplier_Name.SelectedValue.ToString();
                    com.Parameters.Add("@SPD_SPM_ID", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["ID"].Value.ToString();
                    com.Parameters.Add("@SPD_Type", SqlDbType.VarChar).Value = "Quality";
                    com.Parameters.Add("@SPD_Rating_Element", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Rating Element"].Value.ToString();
                    com.Parameters.Add("@SPD_Weightage", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Weightage"].Value.ToString();
                    com.Parameters.Add("@SPD_MonthlyData", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Data"].Value.ToString();
                    com.Parameters.Add("@SPD_Score", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Score"].Value.ToString();
                    com.Parameters.Add("@SPD_Create_by", SqlDbType.VarChar).Value = dbFunctions.username;
                    com.ExecuteNonQuery();
                    com.Connection.Close();
                }
                catch (Exception Ex)
                {
                    dbFunctions.Logs(Ex.Message, dbFunctions.username);
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                display();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Supplier_Perfomance oRpt = new Supplier_Perfomance();

                DataTable dd = dbFunctions.getTable("pr_get_Supplier_YTD   '" + dataGridView3.SelectedRows[0].Cells[0].Value.ToString() + "01'," + PO_vSupplier_Name.SelectedValue.ToString());
                ReportDocument subreport = oRpt.Subreports[0];
                subreport.SetDataSource(dd);


                string SQlQuery = "pr_Print_Suppier_Perfomance '" + dataGridView3.SelectedRows[0].Cells[0].Value.ToString() + "'," + PO_vSupplier_Name.SelectedValue.ToString();
                dbFunctions.printpdf("Perfomance", SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch(Exception ex) { }
        }


    }
}
