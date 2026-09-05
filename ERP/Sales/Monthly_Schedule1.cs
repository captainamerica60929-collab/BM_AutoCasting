using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;

namespace CRM_App.Master
{
    public partial class Monthly_Schedule1 : Form
    {
        public Monthly_Schedule1()
        {
            InitializeComponent();
        }

        private void FG_Master_Load(object sender, EventArgs e)
        {

            Dispaly();
        }

        public void Dispaly()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Monthly_Schedule  '" + dateTimePicker1.Value.ToString("dd-MMM-yyyy")+"'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[2].Frozen = true;
            dataGridView1.Columns[3].Frozen = true;
            dataGridView1.ReadOnly = false;

            decimal Total = 0.0m;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Total += decimal.Parse(dataGridView1.Rows[i].Cells["Total Cost"].Value.ToString());
            }
            txt_Rows.Text = "Total Cost :" + Total.ToString("0.00");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataTable dt = dbFunctions.getTable("Pr_Update_Monthly_Schedule1 '" + dataGridView1.Rows[i].Cells[0].Value.ToString() 
                    + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() 
                    + "','" + dataGridView1.Rows[i].Cells[2].Value.ToString()
                    + "','" + dataGridView1.Rows[i].Cells[3].Value.ToString()
                    + "','" + dataGridView1.Rows[i].Cells[4].Value.ToString() 
                    + "','" + dataGridView1.Rows[i].Cells[5].Value.ToString()
                    + "','" + dataGridView1.Rows[i].Cells[6].Value.ToString() 
                    + "','" + dataGridView1.Rows[i].Cells[7].Value.ToString()
                    
                    + "','" + dbFunctions.username
                     + "','" + dateTimePicker1.Value.ToString("dd-MMM-yyyy")+ "'");
                    
            }
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Part No] LIKE '%{0}%' OR [Part Name] LIKE '%{0}%' OR [Model] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dispaly();
        }

    }
}
