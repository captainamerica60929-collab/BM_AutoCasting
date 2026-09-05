using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using LarchERP.Master;
//using LodgeManagement.Master;
using Maintanence_Printing_Tool;
using Electronika.Transaction;
using Electronika.Master;

namespace GenuineHR.Master
{
    public partial class Master_Menu : Form
    {
        //string Rights = "";
        public Master_Menu()
        {
            InitializeComponent();
        }




        void Clear_Buttons()
        {
            foreach (Control ctrl in this.panel2.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    // ctrl.ForeColor = System.Drawing.Color.ControlText;
                    ctrl.ForeColor = System.Drawing.SystemColors.ControlText;
                    ctrl.BackColor = System.Drawing.SystemColors.ButtonFace;//ButtonFace
                }
            }
        }

        public void change_Color(object sender)
        {
            Clear_Buttons();
            Button B = (Button)sender;
            B.ForeColor = System.Drawing.Color.Maroon;
            B.BackColor = System.Drawing.Color.Moccasin;//ButtonFace
            // B.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));


        }

        private void Master_Menu_Load(object sender, EventArgs e)
        {
            Menu_False();
            MenuRights_fn();

            LoadMaster();
        }

        public void MenuRights_fn()
        {
            DataTable dtMenuRights = dbFunctions.getTable("pr_FetchManuRights '" + dbFunctions.rights + "'");
            if (dtMenuRights.Rows.Count > 0)
            {
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Customer Master")
                    {
                        CustomerMaster.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Supplier Master")
                    {
                        SupplierMaster.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Material Master")
                    {
                        MaterialMaster.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "RM BOM")
                    {
                        RMBOM.Visible = true;
                    }

                }
               
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Currency Master")
                    {
                        CurrencyMaster.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "UOM Master")
                    {
                        UOMMaster.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Plant Master")
                    {
                        PlantMaster.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Machine Master")
                    {
                        MachineMaster.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Employee Master")
                    {
                        EmployeeMaster.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Mould Master")
                    {
                        MouldMaster.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Material Inspection Details")
                    {
                        MaterialInspection.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Check Method")
                    {
                        CheckMethod.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Charateristics Master")
                    {
                        CharateristicsMaster.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Mould Maintance Details")
                    {
                        MouldMaintanceDetailss.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Check Point")
                    {
                        CheckPoint.Visible = true;
                    }

                }
                for (int i = 0; i < dtMenuRights.Rows.Count; i++)
                {
                    if (dtMenuRights.Rows[i]["MenuId"].ToString() == "Instrument Master")
                    {
                        InstrumentMaster.Visible = true;
                    }

                }
            }
        }

        public void Menu_False()
        {
            CustomerMaster.Visible = false;
            SupplierMaster.Visible = false;
            MaterialMaster.Visible = false;
            RMBOM.Visible = false;
            
            CurrencyMaster.Visible = false;
            UOMMaster.Visible = false;
            PlantMaster.Visible = false;
            MachineMaster.Visible = false;
            EmployeeMaster.Visible = false;
            MouldMaster.Visible = false;

            MaterialInspection.Visible = false;
            CheckMethod.Visible = false;
            CharateristicsMaster.Visible = false;
            MouldMaintanceDetailss.Visible = false;
            CheckPoint.Visible = false;
            InstrumentMaster.Visible = false;
        }

        

        public void LoadMaster()
        {
            

          
        }

        private void BodyPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (dbFunctions.isclose == true)
            {
                LoadMaster();
                dbFunctions.isclose = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Contributor_Master ObjContributor_Master = new Contributor_Master();

            BodyPanel.Controls.Clear();
            if (ObjContributor_Master.IsDisposed)
            {
                ObjContributor_Master = new Contributor_Master();
            }
            ObjContributor_Master.TopLevel = false;
            ObjContributor_Master.FormBorderStyle = FormBorderStyle.None;
            ObjContributor_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjContributor_Master);
            ObjContributor_Master.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Item_Type ObjItem_Type = new Item_Type();

            BodyPanel.Controls.Clear();
            if (ObjItem_Type.IsDisposed)
            {
                ObjItem_Type = new Item_Type();
            }
            ObjItem_Type.TopLevel = false;
            ObjItem_Type.FormBorderStyle = FormBorderStyle.None;
            ObjItem_Type.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjItem_Type);
            ObjItem_Type.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            UOM_Master ObjUOM_Master = new UOM_Master();

            change_Color(sender);
            BodyPanel.Controls.Clear();
            if (ObjUOM_Master.IsDisposed)
            {
                ObjUOM_Master = new UOM_Master();
            }
            ObjUOM_Master.TopLevel = false;
            ObjUOM_Master.FormBorderStyle = FormBorderStyle.None;
            ObjUOM_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjUOM_Master);
            ObjUOM_Master.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Material_Master ObjItem_Master = new Material_Master();
            BodyPanel.Controls.Clear();
            if (ObjItem_Master.IsDisposed)
            {
                ObjItem_Master = new Material_Master();
            }
            ObjItem_Master.TopLevel = false;
            ObjItem_Master.FormBorderStyle = FormBorderStyle.None;
            ObjItem_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjItem_Master);
            ObjItem_Master.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            BOM_Master ObjBOM_Master = new BOM_Master();
            BodyPanel.Controls.Clear();
            if (ObjBOM_Master.IsDisposed)
            {
                ObjBOM_Master = new BOM_Master();
            }
            ObjBOM_Master.TopLevel = false;
            ObjBOM_Master.FormBorderStyle = FormBorderStyle.None;
            ObjBOM_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjBOM_Master);
            ObjBOM_Master.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Tax_Master ObjTax_Master = new Tax_Master();

            //BodyPanel.Controls.Clear();
            //if (ObjTax_Master.IsDisposed)
            //{
            //    ObjTax_Master = new Tax_Master();
            //}
            //ObjTax_Master.TopLevel = false;
            //ObjTax_Master.FormBorderStyle = FormBorderStyle.None;
            //ObjTax_Master.Dock = DockStyle.Fill;
            //BodyPanel.Controls.Add(ObjTax_Master);
            //ObjTax_Master.Show();
        }

     
        private void button6_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Employee_Master ObjEmployee_Master = new Employee_Master();

            BodyPanel.Controls.Clear();
            if (ObjEmployee_Master.IsDisposed)
            {
                ObjEmployee_Master = new Employee_Master();
            }
            ObjEmployee_Master.TopLevel = false;
            ObjEmployee_Master.FormBorderStyle = FormBorderStyle.None;
            ObjEmployee_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjEmployee_Master);
            ObjEmployee_Master.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            ModelMaster ObjModelMaster = new ModelMaster();

            BodyPanel.Controls.Clear();
            if (ObjModelMaster.IsDisposed)
            {
                ObjModelMaster = new ModelMaster();
            }
            ObjModelMaster.TopLevel = false;
            ObjModelMaster.FormBorderStyle = FormBorderStyle.None;
            ObjModelMaster.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjModelMaster);
            ObjModelMaster.Show();


        }

        private void button23_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Machine_Master ObjMachine_Master = new Machine_Master();


            BodyPanel.Controls.Clear();
            if (ObjMachine_Master.IsDisposed)
            {
                ObjMachine_Master = new Machine_Master();
            }
            ObjMachine_Master.TopLevel = false;
            ObjMachine_Master.FormBorderStyle = FormBorderStyle.None;
            ObjMachine_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjMachine_Master);
            ObjMachine_Master.Show();

        }

        private void button24_Click(object sender, EventArgs e)
        {
            change_Color(sender); 
            Plant_Master ObjPlant_Master = new Plant_Master();

            BodyPanel.Controls.Clear();
            if (ObjPlant_Master.IsDisposed)
            {
                ObjPlant_Master = new Plant_Master();
            }
            ObjPlant_Master.TopLevel = false;
            ObjPlant_Master.FormBorderStyle = FormBorderStyle.None;
            ObjPlant_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjPlant_Master);
            ObjPlant_Master.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Supplier_Master ObjSupplier_Master = new Supplier_Master();


            BodyPanel.Controls.Clear();
            if (ObjSupplier_Master.IsDisposed)
            {
                ObjSupplier_Master = new Supplier_Master();
            }
            ObjSupplier_Master.TopLevel = false;
            ObjSupplier_Master.FormBorderStyle = FormBorderStyle.None;
            ObjSupplier_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjSupplier_Master);
            ObjSupplier_Master.Show();

        }

        private void button25_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            CurrecyMaster ObjCurrency_Master = new CurrecyMaster();

            BodyPanel.Controls.Clear();
            if (ObjCurrency_Master.IsDisposed)
            {
                ObjCurrency_Master = new CurrecyMaster();
            }
            ObjCurrency_Master.TopLevel = false;
            ObjCurrency_Master.FormBorderStyle = FormBorderStyle.None;
            ObjCurrency_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjCurrency_Master);
            ObjCurrency_Master.Show();



        }

        private void button15_Click(object sender, EventArgs e)
        {
            change_Color(sender); 
            Mold_Master ObjMold_Master = new Mold_Master();

            BodyPanel.Controls.Clear();
            if (ObjMold_Master.IsDisposed)
            {
                ObjMold_Master = new Mold_Master();
            }
            ObjMold_Master.TopLevel = false;
            ObjMold_Master.FormBorderStyle = FormBorderStyle.None;
            ObjMold_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjMold_Master);
            ObjMold_Master.Show();

        }

        private void button22_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            MaterialInspectionStandard ObjMaterialInspectionStandard = new MaterialInspectionStandard();

            BodyPanel.Controls.Clear();
            if (ObjMaterialInspectionStandard.IsDisposed)
            {
                ObjMaterialInspectionStandard = new MaterialInspectionStandard();
            }
            ObjMaterialInspectionStandard.TopLevel = false;
            ObjMaterialInspectionStandard.FormBorderStyle = FormBorderStyle.None;
            ObjMaterialInspectionStandard.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjMaterialInspectionStandard);
            ObjMaterialInspectionStandard.Show();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            CheckMethod ObjCheckMethod = new CheckMethod();

            BodyPanel.Controls.Clear();
            if (ObjCheckMethod.IsDisposed)
            {
                ObjCheckMethod = new CheckMethod();
            }
            ObjCheckMethod.TopLevel = false;
            ObjCheckMethod.FormBorderStyle = FormBorderStyle.None;
            ObjCheckMethod.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjCheckMethod);
            ObjCheckMethod.Show();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Charateristics_Master ObjCharateristics_Master = new Charateristics_Master();

            BodyPanel.Controls.Clear();
            if (ObjCharateristics_Master.IsDisposed)
            {
                ObjCharateristics_Master = new Charateristics_Master();
            }
            ObjCharateristics_Master.TopLevel = false;
            ObjCharateristics_Master.FormBorderStyle = FormBorderStyle.None;
            ObjCharateristics_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjCharateristics_Master);
            ObjCharateristics_Master.Show();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Instument_Master objRequirement_Master = new Instument_Master();
            BodyPanel.Controls.Clear();
            if (objRequirement_Master.IsDisposed)
            {
                objRequirement_Master = new Instument_Master();
            }
            objRequirement_Master.TopLevel = false;
            objRequirement_Master.FormBorderStyle = FormBorderStyle.None;
            objRequirement_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objRequirement_Master);
            objRequirement_Master.Show();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Mould_Maintance_Details objMould_Maintance_Details = new Mould_Maintance_Details();

            BodyPanel.Controls.Clear();
            if (objMould_Maintance_Details.IsDisposed)
            {
                objMould_Maintance_Details = new Mould_Maintance_Details();
            }
            objMould_Maintance_Details.TopLevel = false;
            objMould_Maintance_Details.FormBorderStyle = FormBorderStyle.None;
            objMould_Maintance_Details.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objMould_Maintance_Details);
            objMould_Maintance_Details.Show();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            Check_PointMaster objCheck_PointMaster = new Check_PointMaster();

            BodyPanel.Controls.Clear();
            if (objCheck_PointMaster.IsDisposed)
            {
                objCheck_PointMaster = new Check_PointMaster();
            }
            objCheck_PointMaster.TopLevel = false;
            objCheck_PointMaster.FormBorderStyle = FormBorderStyle.None;
            objCheck_PointMaster.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objCheck_PointMaster);
            objCheck_PointMaster.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            change_Color(sender);
            Mess_BOM objCheck_PointMaster = new Mess_BOM();

            BodyPanel.Controls.Clear();
            if (objCheck_PointMaster.IsDisposed)
            {
                objCheck_PointMaster = new Mess_BOM();
            }
            objCheck_PointMaster.TopLevel = false;
            objCheck_PointMaster.FormBorderStyle = FormBorderStyle.None;
            objCheck_PointMaster.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(objCheck_PointMaster);
            objCheck_PointMaster.Show();
        }
        AssyBOM_Master ObjAssyBOM_Master = new AssyBOM_Master();
        private void btnAsssyBOM_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            

            BodyPanel.Controls.Clear();
            if (ObjAssyBOM_Master.IsDisposed)
            {
                ObjAssyBOM_Master = new AssyBOM_Master();
            }
            ObjAssyBOM_Master.TopLevel = false;
            ObjAssyBOM_Master.FormBorderStyle = FormBorderStyle.None;
            ObjAssyBOM_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjAssyBOM_Master);
            ObjAssyBOM_Master.Show();
        }

        private void btnAsssyBOM_Click_1(object sender, EventArgs e)
        {
            change_Color(sender);


            BodyPanel.Controls.Clear();
            if (ObjAssyBOM_Master.IsDisposed)
            {
                ObjAssyBOM_Master = new AssyBOM_Master();
            }
            ObjAssyBOM_Master.TopLevel = false;
            ObjAssyBOM_Master.FormBorderStyle = FormBorderStyle.None;
            ObjAssyBOM_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjAssyBOM_Master);
            ObjAssyBOM_Master.Show();
        }

        new Greatoo.Master.Reference_Master Reference_Master = new Greatoo.Master.Reference_Master();
        private void Button2_Click_2(object sender, EventArgs e)
        {
            change_Color(sender);


            BodyPanel.Controls.Clear();
            if (Reference_Master.IsDisposed)
            {
                Reference_Master = new Greatoo.Master.Reference_Master();
            }
            Reference_Master.TopLevel = false;
            Reference_Master.FormBorderStyle = FormBorderStyle.None;
            Reference_Master.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(Reference_Master);
            Reference_Master.Show();

        }

        Machine_Maintance_Details maintaincemaintance = new Machine_Maintance_Details();
        private void Button3_Click_1(object sender, EventArgs e)
        {
            change_Color(sender);


            BodyPanel.Controls.Clear();
            if (maintaincemaintance.IsDisposed)
            {
                maintaincemaintance = new Machine_Maintance_Details();
            }
            maintaincemaintance.TopLevel = false;
            maintaincemaintance.FormBorderStyle = FormBorderStyle.None;
            maintaincemaintance.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(maintaincemaintance);
            maintaincemaintance.Show();

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
       // Machine_Maintance_Details model = new Machine_Maintance_Details();
        private void Button10_Click(object sender, EventArgs e)
        {
            change_Color(sender);
            ModelMaster ObjModelMaster = new ModelMaster();

            BodyPanel.Controls.Clear();
            if (ObjModelMaster.IsDisposed)
            {
                ObjModelMaster = new ModelMaster();
            }
            ObjModelMaster.TopLevel = false;
            ObjModelMaster.FormBorderStyle = FormBorderStyle.None;
            ObjModelMaster.Dock = DockStyle.Fill;
            BodyPanel.Controls.Add(ObjModelMaster);
            ObjModelMaster.Show();

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }
    }
}
