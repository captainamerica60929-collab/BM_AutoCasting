using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using Maintanence_Printing_Tool;

namespace Asset_Tracking_System.common
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            PrintDocument prtdoc = new PrintDocument();
            string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;

            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                cmbPrinter.Items.Add(strPrinter);
                if (strPrinter == strDefaultPrinter)
                {
                    cmbPrinter.SelectedIndex = cmbPrinter.Items.IndexOf(strPrinter);
                }
            }
            dataClass.printerName = cmbPrinter.Text;
        }

        private void btnPrinter_Click(object sender, EventArgs e)
        {
            dataClass.printerName = cmbPrinter.Text;
            //this.MdiParent.MainMenuStrip.Items[0].Enabled = true;
             dbFunctions.isclose = true; this.Close(); 
            //Conference_Management_System.Transaction.Create_Label objPrintLabel = new Conference_Management_System.Transaction.Create_Label();

            //if (objPrintLabel.IsDisposed)
            //{
            //    objPrintLabel = new Conference_Management_System.Transaction.Create_Label();
            //}
            //objPrintLabel.Show();
            //objPrintLabel.BringToFront();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
             dbFunctions.isclose = true; this.Close(); 
        }

        private void Settings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult Dr = new DialogResult();
                Dr = MessageBox.Show("Are you sure want Exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (Dr == DialogResult.Yes)
                {
                     dbFunctions.isclose = true; this.Close(); 
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult Dr = new DialogResult();
            Dr = MessageBox.Show("Are you sure want Exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (Dr == DialogResult.Yes)
            {
                 dbFunctions.isclose = true; this.Close(); 
            }
        }
    }
}
