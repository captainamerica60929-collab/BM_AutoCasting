using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using CRM_App.Crystal.Maintanance;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace LarchERP.Master
{

    public partial class Mould_History_Card : Form
    {

        public bool isPreventiveMatainanceLoad = false;
        public Mould_History_Card()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MMD_Mould_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                DataTable dt = dbFunctions.getTable("pr_Get_LoadMouldDetails1 '" + MPM_Part_No.SelectedValue.ToString() + "'");
                MPM_Mould_Id.DataSource = dt;
                MPM_Mould_Id.DisplayMember = "MLD_MouldNo";
                MPM_Mould_Id.ValueMember = "MLD_ID";
                MPM_Mould_Id.SelectedIndex = -1;
                isPreventiveMatainanceLoad = true;
            }
            catch { }
        }

        private void Preventive_Matainance_Load(object sender, EventArgs e)
        {
            LoadMouldName();

        }



        public void LoadMouldName()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_LoadMouldName");
                MPM_Part_No.DataSource = dt;
                MPM_Part_No.DisplayMember = "MLD_Mold";
                MPM_Part_No.ValueMember = "MLD_ID";
                MPM_Part_No.SelectedIndex = -1;

            }
            catch
            {
            }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            dbFunctions.isPM_data = false;
            this.Close();

        }

        private void MPM_Mould_Id_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isPreventiveMatainanceLoad)
                {
                    DataTable dt = dbFunctions.getTable("pr_get_Mould_History_Card '" + MPM_Mould_Id.SelectedValue.ToString() + "'");
                    dataGridView1.DataSource = dt;
                    dbFunctions.DGVStyle(dataGridView1);


                    DataTable dtt = dbFunctions.getTable("pr_Get_BreakdownDetails '" + MPM_Mould_Id.SelectedValue.ToString() + "'");
                    dataGridView2.DataSource = dtt;
                    dbFunctions.DGVStyle(dataGridView2);
                    
                }
            }
            catch { }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button9_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count > 0)
            //{
            //    print_Bill(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
            //}
            //else
            //{
            //    MessageBox.Show("Please Select one Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

          // pdf1();
           pdf2();
        }

        private void pdf2(){
            try  
            {
                DataTable dt = dbFunctions.getTable("pr_Get_print_BreakdownDetails '" + MPM_Mould_Id.SelectedValue.ToString() + "'");
                DataTable dt1 = dbFunctions.getTable("pr_get_new_Mould_History_Card_print '" + MPM_Mould_Id.SelectedValue.ToString() + "'");
                DataTable ReportData = new DataTable();
                ReportDocument oRpt = new ReportDocument();
                ReportData = dbFunctions.getTable("pr_get_new_Mould_master_print '" + MPM_Mould_Id.SelectedValue.ToString() + "'");
              
                // ReportData = dbFunctions.getTable("pr_get_new_Mould_History_Card_print '" + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + "'");
                // DataTable dt = dbFunctions.getTable("pr_Get_print_BreakdownDetails '" + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + "'");
                oRpt = new CRM_App.Crystal.New_Mould_Break_Down1();
                oRpt.Subreports[0].SetDataSource(dt1);
                oRpt.Subreports[1].SetDataSource(dt);
                try
                {
                    ExportOptions exportOpts = new ExportOptions();
                    DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();

                    oRpt.Database.Tables[0].SetDataSource(ReportData);
                    ExportOptions CrExportOptions = default(ExportOptions);
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    CrDiskFileDestinationOptions.DiskFileName = System.IO.Path.Combine(System.Configuration.ConfigurationSettings.AppSettings["Filepath"].ToString(), ".pdf");
                    //CrDiskFileDestinationOptions.DiskFileName = System.IO.Path.Combine(System.Configuration.ConfigurationSettings.AppSettings["Filepath"].ToString(), ds_uniqueno.Text + ".pdf");
                    //CrDiskFileDestinationOptions.DiskFileName = System.IO.Path.Combine(dbFunctions.path, PdfFileName + ".pdf");

                    CrExportOptions = oRpt.ExportOptions;
                    var _with1 = CrExportOptions;
                    _with1.ExportDestinationType = ExportDestinationType.DiskFile;
                    _with1.ExportFormatType = ExportFormatType.PortableDocFormat;
                    _with1.DestinationOptions = CrDiskFileDestinationOptions;
                    _with1.FormatOptions = CrFormatTypeOptions;
                    oRpt.Export();
                    DialogResult result = MessageBox.Show("If You Want view to Print Press YES", "Alcove", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        //PrintPDFs(CrDiskFileDestinationOptions.DiskFileName);
                        System.Diagnostics.Process.Start(CrDiskFileDestinationOptions.DiskFileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    // MessageBox.Show("Error in Bill Creation, Contact Admin");
                }

            }
            catch { }

        }


        private void pdf1()
        {
            try
            {
                DataTable ReportData = new DataTable();
                ReportDocument oRpt = new ReportDocument();
                ReportData = dbFunctions.getTable("pr_get_new_Mould_History_Card_print '" + MPM_Part_No.SelectedValue.ToString() + "'");
                DataTable dt = dbFunctions.getTable("pr_Get_print_BreakdownDetails '" + MPM_Part_No.SelectedValue.ToString() + "'");
               //DataTable dt1 = dbFunctions.getTable("pr_get_new_Mould_master_print '" + MPM_Part_No.SelectedValue.ToString() + "'");
                // ReportData = dbFunctions.getTable("pr_get_new_Mould_History_Card_print '" + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + "'");
                // DataTable dt = dbFunctions.getTable("pr_Get_print_BreakdownDetails '" + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString() + "'");
                oRpt = new CRM_App.Crystal.New_Mould_Break_Down();
                oRpt.Subreports[0].SetDataSource(dt);
               // oRpt.Subreports[1].SetDataSource(dt1);
                try
                {
                    ExportOptions exportOpts = new ExportOptions();
                    DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();

                    oRpt.Database.Tables[0].SetDataSource(ReportData);
                    ExportOptions CrExportOptions = default(ExportOptions);
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    CrDiskFileDestinationOptions.DiskFileName = System.IO.Path.Combine(System.Configuration.ConfigurationSettings.AppSettings["Filepath"].ToString(), ".pdf");
                    //CrDiskFileDestinationOptions.DiskFileName = System.IO.Path.Combine(System.Configuration.ConfigurationSettings.AppSettings["Filepath"].ToString(), ds_uniqueno.Text + ".pdf");
                    //CrDiskFileDestinationOptions.DiskFileName = System.IO.Path.Combine(dbFunctions.path, PdfFileName + ".pdf");

                    CrExportOptions = oRpt.ExportOptions;
                    var _with1 = CrExportOptions;
                    _with1.ExportDestinationType = ExportDestinationType.DiskFile;
                    _with1.ExportFormatType = ExportFormatType.PortableDocFormat;
                    _with1.DestinationOptions = CrDiskFileDestinationOptions;
                    _with1.FormatOptions = CrFormatTypeOptions;
                    oRpt.Export();
                    DialogResult result = MessageBox.Show("If You Want view to Print Press YES", "Alcove", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        //PrintPDFs(CrDiskFileDestinationOptions.DiskFileName);
                        System.Diagnostics.Process.Start(CrDiskFileDestinationOptions.DiskFileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    // MessageBox.Show("Error in Bill Creation, Contact Admin");
                }

            }
            catch { }
        }

        public void print_Bill(string s)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //CRM_App.Crystal.Mould_Break_Down oRpt = new CRM_App.Crystal.Mould_Break_Down();
                CRM_App.Crystal.New_Mould_Break_Down oRpt = new CRM_App.Crystal.New_Mould_Break_Down();
                //string SQlQuery = "pr_get_Mould_History_Card_print '" + s + "'";
                string SQlQuery = "pr_get_new_Mould_History_Card_print '" + s + "'";
                
                dbFunctions.printpdf(s, SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }

        private void Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button8_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.WaitCursor;
            //dbFunctions.ExportExcel(dataGridView1) AND(dataGridView2);
            //Cursor.Current = Cursors.Default;

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                dbFunctions.ExportExcel(dataGridView1);
                dbFunctions.ExportExcel(dataGridView2);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }
    }
}