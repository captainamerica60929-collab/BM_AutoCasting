using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using CRM_App.Master;

namespace LarchERP.Master
{

    public partial class Material_Master : Form
    {
        public string arrow = "Up";
        public int Distance = 109;
        public string ID = "";
        public string ErrorMessage = "";
        public Material_Master()
        {
            InitializeComponent();
        }




        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IM_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (IM_Type.SelectedValue.ToString() == "1")
                {
                    FG_Master ObjInvoice = new FG_Master();

                    panel1.Controls.Clear();
                    panel1.Visible = true;

                    //panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                    if (ObjInvoice.IsDisposed)
                    {
                        ObjInvoice = new FG_Master();
                    }
                    ObjInvoice.TopLevel = false;
                    ObjInvoice.FormBorderStyle = FormBorderStyle.None;
                    ObjInvoice.Dock = DockStyle.Fill;
                    panel1.Controls.Add(ObjInvoice);
                    ObjInvoice.Show();
                }
                else if (IM_Type.SelectedValue.ToString() == "2")
                {
                    BO_Master ObjInvoice = new BO_Master();

                    panel1.Controls.Clear();
                    panel1.Visible = true;

                    //panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                    if (ObjInvoice.IsDisposed)
                    {
                        ObjInvoice = new BO_Master();
                    }
                    ObjInvoice.TopLevel = false;
                    ObjInvoice.FormBorderStyle = FormBorderStyle.None;
                    ObjInvoice.Dock = DockStyle.Fill;
                    panel1.Controls.Add(ObjInvoice);
                    ObjInvoice.Show();
                }
                else if (IM_Type.SelectedValue.ToString() == "3")
                {
                    RM_Master ObjInvoice = new RM_Master();

                    panel1.Controls.Clear();
                    panel1.Visible = true;

                    //panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                    if (ObjInvoice.IsDisposed)
                    {
                        ObjInvoice = new RM_Master();
                    }
                    ObjInvoice.TopLevel = false;
                    ObjInvoice.FormBorderStyle = FormBorderStyle.None;
                    ObjInvoice.Dock = DockStyle.Fill;
                    panel1.Controls.Add(ObjInvoice);
                    ObjInvoice.Show();
                }
            


                else if (IM_Type.SelectedValue.ToString() == "4")
                {
                    Assmbly_Master ObjInvoice = new Assmbly_Master();

                    panel1.Controls.Clear();
                    panel1.Visible = true;

                    //panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                    if (ObjInvoice.IsDisposed)
                    {
                        ObjInvoice = new Assmbly_Master();
                    }
                    ObjInvoice.TopLevel = false;
                    ObjInvoice.FormBorderStyle = FormBorderStyle.None;
                    ObjInvoice.Dock = DockStyle.Fill;
                    panel1.Controls.Add(ObjInvoice);
                    ObjInvoice.Show();
                }
                else if (IM_Type.SelectedValue.ToString() == "5" )
                {
                    Others_Master ObjOthers_Master = new Others_Master();

                    panel1.Controls.Clear();
                    panel1.Visible = true;

                    //panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                    if (ObjOthers_Master.IsDisposed)
                    {
                        ObjOthers_Master = new Others_Master();
                    }
                    ObjOthers_Master.TopLevel = false;
                    ObjOthers_Master.FormBorderStyle = FormBorderStyle.None;
                    ObjOthers_Master.Dock = DockStyle.Fill;
                    panel1.Controls.Add(ObjOthers_Master);
                    ObjOthers_Master.Show();
                }
                else if (IM_Type.SelectedValue.ToString() == "6")
                {
                    Sub_Part ObjOthers_Master = new Sub_Part();

                    panel1.Controls.Clear();
                    panel1.Visible = true;

                    //panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                    if (ObjOthers_Master.IsDisposed)
                    {
                        ObjOthers_Master = new Sub_Part();
                    }
                    ObjOthers_Master.TopLevel = false;
                    ObjOthers_Master.FormBorderStyle = FormBorderStyle.None;
                    ObjOthers_Master.Dock = DockStyle.Fill;
                    panel1.Controls.Add(ObjOthers_Master);
                    ObjOthers_Master.Show();
                }
                else
                {
                    panel1.Controls.Clear();
                    panel1.Visible = true;

                }
            }
            catch { }
        }

        private void Material_Master_Load(object sender, EventArgs e)
        {
            LoadItemType();

            FG_Master ObjInvoice = new FG_Master();

            panel1.Controls.Clear();
            panel1.Visible = true;

            //panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            if (ObjInvoice.IsDisposed)
            {
                ObjInvoice = new FG_Master();
            }
            ObjInvoice.TopLevel = false;
            ObjInvoice.FormBorderStyle = FormBorderStyle.None;
            ObjInvoice.Dock = DockStyle.Fill;
            panel1.Controls.Add(ObjInvoice);
            ObjInvoice.Show();
        }

        public void LoadItemType()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadItemType");
                IM_Type.DataSource = dt;
                IM_Type.DisplayMember = "Ty_TypeName";
                IM_Type.ValueMember = "Ty_ID";
                IM_Type.SelectedIndex = 0;
            }
            catch
            {
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}