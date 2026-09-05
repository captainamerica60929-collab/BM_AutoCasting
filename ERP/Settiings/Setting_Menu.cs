using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using LarchERP.Transaction;
using LarchERP.Master;
using Genuine_Inventory.Common;


namespace GenuineHR.Settiings
{
    public partial class Setting_Menu : Form
    {
        public Setting_Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RightsMaster ObjRightsMaster = new RightsMaster();

            BodyPanel.Controls.Clear();
            // this.IsMdiContainer = true;
            if (ObjRightsMaster.IsDisposed)
            {
                ObjRightsMaster = new RightsMaster();
            }
            ObjRightsMaster.TopLevel = false;
            ObjRightsMaster.FormBorderStyle = FormBorderStyle.None;
            ObjRightsMaster.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjRightsMaster);
            ObjRightsMaster.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //RightsMaster ObjRightsMaster = new RightsMaster();

            //BodyPanel.Controls.Clear();
            //// this.IsMdiContainer = true;
            //if (ObjRightsMaster.IsDisposed)
            //{
            //    ObjRightsMaster = new ObjRightsMaster();
            //}
            //ObjRightsMaster.TopLevel = false;
            //ObjRightsMaster.FormBorderStyle = FormBorderStyle.None;
            //ObjRightsMaster.Dock = DockStyle.Fill;
            //BodyPanel.Controls.Add(ObjRightsMaster);
            //ObjRightsMaster.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            UserMaster ObjUserMaster = new UserMaster();

            BodyPanel.Controls.Clear();
            // this.IsMdiContainer = true;
            if (ObjUserMaster.IsDisposed)
            {
                ObjUserMaster = new UserMaster();
            }
            ObjUserMaster.TopLevel = false;
            ObjUserMaster.FormBorderStyle = FormBorderStyle.None;
            ObjUserMaster.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjUserMaster);
            ObjUserMaster.Show();
        }
        private void button12_Click_1(object sender, EventArgs e)
        {
            //Menu_Master ObjMenu_Master = new Menu_Master();

            //BodyPanel.Controls.Clear();
            //// this.IsMdiContainer = true;
            //if (ObjMenu_Master.IsDisposed)
            //{
            //    ObjMenu_Master = new Menu_Master();
            //}
            //ObjMenu_Master.TopLevel = false;
            //ObjMenu_Master.FormBorderStyle = FormBorderStyle.None;
            //ObjMenu_Master.Dock = DockStyle.Fill;
            //BodyPanel.Controls.Add(ObjMenu_Master);
            //ObjMenu_Master.Show();
        }

      
        Login_Settings ObjLogin_Settings = new Login_Settings();
        private void button3_Click(object sender, EventArgs e)
        {
            BodyPanel.Controls.Clear();
            // this.IsMdiContainer = true;
            if (ObjLogin_Settings.IsDisposed)
            {
                ObjLogin_Settings = new Login_Settings();
            }
            ObjLogin_Settings.TopLevel = false;
            ObjLogin_Settings.FormBorderStyle = FormBorderStyle.None;
            ObjLogin_Settings.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjLogin_Settings);
            ObjLogin_Settings.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {

        }

        private void Setting_Menu_Load(object sender, EventArgs e)
        {

        }

      
    }
}
