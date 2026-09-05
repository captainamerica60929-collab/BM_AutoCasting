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

namespace GenuineHR.Reports
{
    public partial class Assembly_Stock : Form
    {
        public Assembly_Stock()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = dbFunctions.getTable("pr_gettodayStatus_all '" + dtpFrom.Value.ToString("yyyyMMdd") + "',1");//+ shift.SelectedValue);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns["Code"].Width = 800;
            dataGridView1.Columns["Name"].Width = 180;
            dataGridView1.Columns["Designation"].Width = 180;
            dataGridView1.Columns["Category"].Width = 120;

            int ab = 0, late = 0, present = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Status"].Equals("Absent"))
                {
                    ab++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                }

                if (dt.Rows[i]["Status"].Equals("Late"))
                {
                    late++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                }

                if (dt.Rows[i]["Status"].Equals("Present"))
                {
                    present++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightSeaGreen;
                }

            }
            //txt_Absent.Text = ab.ToString();
            //txtPresent.Text = (present + late).ToString();
            //txtLate.Text = late.ToString();
            //txtTotal.Text = dt.Rows.Count.ToString();
            //txt_early.Text = present.ToString();

                 }

        void getdata()
        {
            DataTable dt = dbFunctions.getTable("pr_getCurrent_Assy_Stock");//  '" + dtpFrom.Value.ToString("yyyyMMdd") + "','" + todate.Value.ToString("yyyyMMdd") + "'");
            dataGridView1.DataSource = dt;
            dbFunctions.DGVStyle(dataGridView1);
            //dataGridView1.Columns[0].Visible = true;
            //dataGridView1.Columns[0].Width = 150;
            //txt_Rows.Text = dbFunctions.getRows(dataGridView1);
            txt_Rows.Text = "Total Rows Count :" + (dataGridView1.Rows.Count).ToString();

            //calc();
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
            try
            {

                if (string.IsNullOrEmpty(textBoxX1.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Part No] like '%{0}%' or [Part Name] like '%{0}%'", textBoxX1.Text);
                }

               
            }
            catch (Exception ex)
            {
               
            }
            calc();
            txt_Rows.Text = dbFunctions.getRows(dataGridView1);
        }

        private void textBoxX1_Enter(object sender, EventArgs e)
        {
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
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
    }
}
