using Maintanence_Printing_Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM_App.Reports
{
    public partial class Dc_Overall_Report : Form
    {
        public Dc_Overall_Report()
        {
            InitializeComponent();
        }

        private void Dc_Overall_Report_Load(object sender, EventArgs e)
        {
            stock();
        }

        private void stock()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataTable a = dbFunctions.getTable("dc_overall_report");
            dataGridView1.DataSource = a;
            dbFunctions.DGVStyle(dataGridView1);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dbFunctions.ExportExcel(dataGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[SupplierName] LIKE '%{0}%' OR [Part No] LIKE '%{0}%' OR [DC No] LIKE '%{0}%'", textBoxX2.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
