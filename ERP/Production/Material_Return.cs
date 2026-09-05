using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;

namespace CRM_App.Production
{
    public partial class Material_Return : Form
    {
        public Material_Return()
        {
            InitializeComponent();
        }

        private void Material_Return_Load(object sender, EventArgs e)
        {
            display();
        }
        public string IID = "";
        public string GRN_ID = "";
        private void txt_Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                DataTable dt = dbFunctions.getTable("pr_get_Barcode_Data1  '" + txt_Barcode.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    GRN_ID = dt.Rows[0]["ID"].ToString();
                    IID = dt.Rows[0]["IDS"].ToString();
                    textBox2.Text = dt.Rows[0]["GRN_vGRN_No"].ToString();
                    textBox3.Text = dt.Rows[0]["GRND_vLot_No"].ToString();
                    textBox6.Text = dt.Rows[0]["Mfg Date"].ToString();
                    textBox5.Text = dt.Rows[0]["Part_No"].ToString();
                    textBox4.Text = dt.Rows[0]["Grade"].ToString();
                    textBox7.Text = dt.Rows[0]["Qty"].ToString();
                    button1.Enabled = true;

                }
                else
                {
                    txt_Barcode.Text = "";
                    txt_Barcode.Focus();
                    MessageBox.Show("Invalid Barcode", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = false;
                }

            }
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                //DataTable dt = dbFunctions.getTable("pr_Generate_Label_Return " + GRN_ID + "," + textBox8.Text + ",'" + txt_Barcode.Text + "'");
                DataTable dt = dbFunctions.getTable("pr_Update_Stock " + IID + "," + textBox8.Text +",'"+dbFunctions.username+"'");

                MessageBox.Show("Material Retuned Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);    

                display();
                clear();

            }
            catch { }
        }

        public void display()
        {
            DataTable dd = dbFunctions.getTable("pr_get_Barcode_Detail");
            dataGridView1.DataSource = dd;
            dbFunctions.DGVStyle(dataGridView1);
        }

        void clear()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            textBox7.Text = "";
            txt_Barcode.Text = "";
            textBox8.Text = "";
                
        }

        private void Txt_Barcode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
