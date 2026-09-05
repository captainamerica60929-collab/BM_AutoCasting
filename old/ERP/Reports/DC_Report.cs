using Maintanence_Printing_Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CRM_App.Reports
{
    public partial class DC_Report : Form
    {
        public DC_Report()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxX1.Visible = true;
            textBoxX2.Visible = false;
            textBoxX3.Visible = false;
            //String fdate = dtpFrom.Text;
            //String tdate = todate.Text;
            DataTable dt = dbFunctions.getTable("pr_Fetch_DC_Receive_Details   '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
           // DataTable dt = dbFunctions.getTable("pr_Fetch_DC_Receive_Details  '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            //dataGridView1.Columns[0].Visible = true;
            //dataGridView1.Columns[0].Width = 150;
            //txt_Rows.Text = dbFunctions.getRows(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxX1.Visible = true;
            textBoxX2.Visible = false;
            textBoxX3.Visible = false;
            //String fdate = dtpFrom.Text;
            //String tdate = todate.Text;


            // DataTable dt = dbFunctions.getTable("pr_Fetch_DC_Send_Details  '" + fdate + "','" + tdate + "'");
            DataTable dt = dbFunctions.getTable("pr_Fetch_DC_Send_Details   '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            //dataGridView1.Columns[0].Visible = true;
            //dataGridView1.Columns[0].Width = 150;
            //txt_Rows.Text = dbFunctions.getRows(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }

        private void DC_Report_Load(object sender, EventArgs e)
        {
            textBoxX2.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxX1.Visible = false;
            textBoxX2.Visible = true;
            DataTable dtDetails = dbFunctions.getTable("pr_Fetch_GRN_Details_DC_report '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dtDetails;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxX1.Visible = true;
            textBoxX2.Visible = false;
            DataTable dtDetails = dbFunctions.getTable("pr_Get_Pending_DC '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dtDetails;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(textBoxX1.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[DC NO] LIKE '%{0}%' OR [Supplier Name] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }

        private void TextBoxX2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxX2.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[DC NO] LIKE '%{0}%' OR [Grn No] LIKE '%{0}%'", textBoxX2.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Dc_Overall_Report ob = new Dc_Overall_Report();
            DialogResult s = ob.ShowDialog();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            textBoxX1.Visible = false;
            textBoxX2.Visible = false;
            textBoxX3.Visible = true;

            //String fdate = dtpFrom.Text;
            //String tdate = todate.Text;
            DataTable a = dbFunctions.getTable("dc_overall_report_new   '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            //DataTable a = dbFunctions.getTable("dc_overall_report  '" + fdate + "','" + tdate + "'");
            // DataTable a = dbFunctions.getTable("dc_overall_report'" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = a;
            dbFunctions.DGVStyle(dataGridView1);
          
        }

        private void TextBoxX3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxX3.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[supplier name] LIKE '%{0}%' ", textBoxX3.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBoxX4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxX4.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[supplier] LIKE '%{0}%' OR [send MaterialName] LIKE '%{0}%' or [type] LIKE '%{0}%' OR [Inward DC No] LIKE '%{0}%'", textBoxX4.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBoxX5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxX5.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Supplier Name] LIKE '%{0}%' OR [Material Name] LIKE '%{0}%'", textBoxX5.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 2)
            {
                //String fdate.Value.ToString("yyyyMMdd") = dtpFrom.Text;
                    string fdate = dtpFrom.Value.ToString("yyyyMMdd");
                string tdate = todate.Value.ToString("yyyyMMdd");
                //String tdate.Value.ToString("yyyyMMdd") = todate.Text;
                panel1.Visible = true;
                panel3.Visible = true;
                DataTable dt = dbFunctions.getTable("dc_overall_report_received_new  '" + fdate + "', '" + tdate + "', '" + dataGridView1.SelectedRows[0].Cells["supplier name"].Value + "','" + dataGridView1.SelectedRows[0].Cells["RM_ID"].Value + "'");
                dataGridView2.DataSource = dt;
                //dataGridView2.Columns["Stock"].ReadOnly = true;
                //dataGridView2.Columns[2].ReadOnly = true;
                dbFunctions.DGVStyle(dataGridView2);
                label12.Text= dataGridView1.SelectedRows[0].Cells["Supplied Qty"].Value.ToString();


                cal();
            }
        }

        private void cal()
        {
            decimal totrm = 0.00m;
            decimal Partwei = 0.00m;
          //  decimal runwei = 0.00m;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                totrm += decimal.Parse(dataGridView2.Rows[i].Cells["Qty"].Value.ToString());
                Partwei += decimal.Parse(dataGridView2.Rows[i].Cells["STOCK"].Value.ToString());
               // runwei += decimal.Parse(dataGridView2.Rows[i].Cells["RUNNERWEIGHT"].Value.ToString());

            }
            label5.Text = totrm.ToString();
            //label3.Text = Partwei.ToString();
            //label4.Text = runwei.ToString();
            label11.Text = (Partwei ).ToString();
            label18.Text = (Partwei).ToString();
            value1();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = false;
        }

        private void TextBoxX6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxX6.Text))
                {
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Part Name] LIKE '%{0}%' or [Part No] LIKE '%{0}%' ", textBoxX6.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cal();
        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {
            value1();
            
        }

        private void value1()
        {
            try
            {
                // decimal a = decimal(18, 2)(label12.Text);
                decimal a = Math.Round(Convert.ToDecimal(label12.Text), 2);
                decimal b = Math.Round(Convert.ToDecimal(label18.Text), 2);
               // decimal c = Math.Round(Convert.ToDecimal(label21.Text), 2);
                label19.Text = (a - b).ToString();
            }
            catch { }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            label21.Text = textBox1.Text;
            value1();
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            //String tdate = todate.Text;
            DataTable dt = dbFunctions.getTable("pr_Fetch_DC_Receive_Details_Summary'" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            // DataTable dt = dbFunctions.getTable("pr_Fetch_DC_Receive_Details  '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            //dataGridView1.Columns[0].Visible = true;
            //dataGridView1.Columns[0].Width = 150;
            //txt_Rows.Text = dbFunctions.getRows(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_DC_send_Details_Summary'" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            // DataTable dt = dbFunctions.getTable("pr_Fetch_DC_Receive_Details  '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            //dataGridView1.Columns[0].Visible = true;
            //dataGridView1.Columns[0].Width = 150;
            //txt_Rows.Text = dbFunctions.getRows(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();

        }
    }
}
