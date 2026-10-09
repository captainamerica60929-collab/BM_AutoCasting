using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using LarchERP.Master;
using CRM_App.Production;
using CRM_App.Crystal;

namespace CRM_App.Maintanace
{
    public partial class Mould_Maintenance_List : Form
    {
        public Mould_Maintenance_List()
        {
            InitializeComponent();
        }

        private void Mould_Maintenance_List_Load(object sender, EventArgs e)
        {
            radio_Today_List.Checked = true;
            display();
            

        }

        private void radio_Today_List_CheckedChanged(object sender, EventArgs e)
        {
            display();


        }

        void display()
        {
            if (radio_Today_List.Checked == true)
            {
                


                DataTable dt = dbFunctions.getTable("pr_get_Mould_TODAY_PM_Need");
                dataGridView1.DataSource = dt;
                dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
                dataGridView1.ColumnHeadersHeight = 40;
                // CalculateAndColorRows();
                ApplyColumnColoring();
            }
            else
            {
                DataTable dt = dbFunctions.getTable("pr_get_Mould_Data");
                dataGridView1.DataSource = dt;
                dbFunctions.DGVStyleAutoSizeColumn(dataGridView1);
                dataGridView1.ColumnHeadersHeight = 40;
                ApplyColumnColoring();
            }

            dataGridView1.ReadOnly = true;
        }
        public void ApplyColumnColoring()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                if (double.TryParse(row.Cells["Remaining Shots"]?.Value?.ToString(), out double remainingShots) &&
                    double.TryParse(row.Cells["Mold Life"]?.Value?.ToString(), out double moldLife) &&
                    moldLife > 0)
                {
                    double remainingPercentage = (remainingShots / moldLife) * 100;

                    DataGridViewCell targetCell = row.Cells["Remaining Shots"];

                    if (remainingPercentage <= 10)
                    {
                        // Red - critically low
                        targetCell.Style.BackColor = Color.Red;
                        targetCell.Style.ForeColor = Color.White;
                    }
                    else if (remainingPercentage <= 25)
                    {
                        // Yellow - warning
                        targetCell.Style.BackColor = Color.Yellow;
                        targetCell.Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        // Green - sufficient remaining life
                        targetCell.Style.BackColor = Color.Green;
                        targetCell.Style.ForeColor = Color.White;
                    }
                }
            }
        }

        //public void ApplyColumnColoring()
        //{
        //    foreach (DataGridViewRow row in dataGridView1.Rows)
        //    {
        //        if (row.IsNewRow) continue;

        //        if (int.TryParse(row.Cells["After PM Prod. Shot"]?.Value?.ToString(), out int shot) &&
        //            int.TryParse(row.Cells["Frequency"]?.Value?.ToString(), out int freq) &&
        //            freq > 0)
        //        {
        //            double percentage = (double)shot / freq * 100;

        //            DataGridViewCell targetCell = row.Cells["After PM Prod. Shot"];

        //            if (percentage >= 95)
        //            {
        //                targetCell.Style.BackColor = Color.Red;
        //                targetCell.Style.ForeColor = Color.White;
        //            }
        //            else if (percentage >= 80)
        //            {
        //                targetCell.Style.BackColor = Color.Yellow;
        //                targetCell.Style.ForeColor = Color.Black;
        //            }
        //            else
        //            {
        //                targetCell.Style.BackColor = dataGridView1.DefaultCellStyle.BackColor;
        //                targetCell.Style.ForeColor = dataGridView1.DefaultCellStyle.ForeColor;
        //            }
        //        }
        //    }
        //}

        private void CalculateAndColorRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue; // skip new row

                // Parse values from column 1 and 2
                if (decimal.TryParse(row.Cells["After PM Prod. Shot"].Value?.ToString(), out decimal afterPM) &&
                    decimal.TryParse(row.Cells["Frequency"].Value?.ToString(), out decimal shotFreq))
                {
                    if (afterPM == 0) continue; // avoid division by zero

                    // Calculate percentage
                    decimal percent = (shotFreq / afterPM) * 100;

                    // Add percentage to new column (create it first if not already present)
                    row.Cells["Percentage"].Value = percent.ToString("0.00") + " %";

                    // Apply color based on percent
                    if (percent >= 95)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            }
        }


        private void radio_All_Schedule_CheckedChanged(object sender, EventArgs e)
        {
            display();
            
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               dbFunctions.Mould_ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
               dbFunctions.Mould_Name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                dbFunctions.isPM_data = true;
              

                Preventive_Matainance ObjInvoice = new Preventive_Matainance();

                panel4.Controls.Clear();
                panel4.Visible = true;

                panel4.Dock = System.Windows.Forms.DockStyle.Fill;
                if (ObjInvoice.IsDisposed)
                {
                    ObjInvoice = new Preventive_Matainance();
                }
                ObjInvoice.TopLevel = false;
                ObjInvoice.FormBorderStyle = FormBorderStyle.None;
                ObjInvoice.Dock = DockStyle.Fill;
                panel4.Controls.Add(ObjInvoice);
                ObjInvoice.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel4_ControlRemoved(object sender, ControlEventArgs e)
        {
            panel4.Visible = false;
            display();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Mould_Plan_Vs_Actual_Machine oRpt = new Mould_Plan_Vs_Actual_Machine();
                //string SQlQuery = "pr_get_MACHINE_Plan_vs_Actual'" + dateTimePicker1.Text + "'";
                string SQlQuery = "pr_get_Mould_Plan_vs_Actual'" + dateTimePicker1.Text + "'";

                
                dbFunctions.printpdf("Plan_Vs_Actual", SQlQuery, oRpt);
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ApplyColumnColoring();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Usage %"].Value != null &&
                    decimal.TryParse(row.Cells["Usage %"].Value.ToString(), out decimal usage) &&
                    usage >= 85)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;  // ✅ Highlight row
                    row.DefaultCellStyle.ForeColor = Color.Black;       // Optional for text visibility
                }
                else
                {
                    // Optional: reset color for other rows
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Part Name] LIKE '%{0}%' or [Part No] LIKE '%{0}%'", textBoxX1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // skip header row

            // Reset all rows back to default style
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.ForeColor = dataGridView1.DefaultCellStyle.ForeColor;
                }
            }

            // Highlight the clicked row in RED
            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
            selectedRow.DefaultCellStyle.BackColor = Color.Red;
            selectedRow.DefaultCellStyle.ForeColor = Color.White;
        }
    }
}
