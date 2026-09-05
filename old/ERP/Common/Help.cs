using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maintanence_Printing_Tool
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //linkLabel2.LinkVisited = true;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "mailto:info@Genuine.in?subject=Milton Roy India-Reg&body=Enter Your Quaries";
            proc.Start();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           // linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("IExplore", "http://www.Genuine.in");
        }

        private void button4_Click(object sender, EventArgs e)
        {
             dbFunctions.isclose = true; this.Close(); 
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore", "https://twitter.com/Genuine");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore", "http://in.linkedin.com/pub/Genuine");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore", "http://www.facebook.com/Genuine/");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore", "http://www.facebook.com/Genuine/");
        }

        private void Help_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Help_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                 dbFunctions.isclose = true; this.Close(); 
            }
        }
    }
}
