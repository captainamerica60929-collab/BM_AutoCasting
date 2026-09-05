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
    public partial class Monthly_Pro_Schedule : Form
    {
        public Monthly_Pro_Schedule()
        {
            InitializeComponent();
        }

        private void FG_Master_Load(object sender, EventArgs e)
        {

            Dispaly();
        }

        public void Dispaly()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Monthly_Pro_Schedule  '" + dateTimePicker1.Value.ToString("dd-MMM-yyyy") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);


            dataGridView1.Columns["Plan Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dataGridView1.Columns["Plan Qty"].DefaultCellStyle.Format = "##,##,##,###";


            dataGridView1.Columns["Produced Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dataGridView1.Columns["Produced Qty"].DefaultCellStyle.Format = "##,##,##,###";


            dataGridView1.Columns["Balance Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dataGridView1.Columns["Balance Qty"].DefaultCellStyle.Format = "##,##,##,###";

            dataGridView1.Columns["Pro %"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dataGridView1.Columns["Pro %"].DefaultCellStyle.Format = "##,##,##,###";

            dataGridView1.Columns["Produced Qty"].DefaultCellStyle.ForeColor = Color.Green;
            dataGridView1.Columns["Plan Qty"].DefaultCellStyle.ForeColor = Color.Blue;
            dataGridView1.Columns["Balance Qty"].DefaultCellStyle.ForeColor = Color.Red;




           

          
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
                DataTable dt = dbFunctions.getTable("Pr_Update_Monthly_Pro_Schedule1 '" + dateTimePicker1.Value.ToString("dd-MMM-yyyy") 
                    + "','" + dataGridView1.Rows[i].Cells["ID"].Value.ToString() 
                    + "','" + dataGridView1.Rows[i].Cells["Plan Qty"].Value.ToString() + "'");
                    
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Part No] LIKE '%{0}%' OR [Part Name] LIKE '%{0}%'", textBoxX1.Text);
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
