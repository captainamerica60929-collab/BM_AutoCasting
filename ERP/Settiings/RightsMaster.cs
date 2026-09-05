using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace LarchERP.Transaction
{
    public partial class RightsMaster : Form
    {
        string ErrorMessage = "";

        public RightsMaster()
        {
            InitializeComponent();
            splitContainer1.SplitterDistance = 600; 

        }
        public int distance = 600;

        public string arrow = "Up";
       
     
        private void PurchaseOrderUpload_Shown(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = distance;
            fetchRightsmaster();
            FetchEntry();
         
        }

        void Update()
        {
            try
            {

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "pr_UpdateRightsMaster " + int.Parse(txthiddenid.Text.ToString()) + ",'" + txt_rightsname.Text.ToString() + "'";
                DataTable dt = dbFunctions.getTable(cmd.CommandText);
                cmd.ExecuteNonQuery();
                MessageBox.Show("updated successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fetchRightsmaster();
                clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clear();

            }
        }

        void insert()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pr_InsertRightsMaster";

            param = new SqlParameter();
            param.ParameterName = "rmm_vRightsName";
            param.SqlDbType = SqlDbType.VarChar;
            param.SqlValue = txt_rightsname.Text.ToString();
            cmd.Parameters.Add(param);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Details Inserted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            fetchRightsmaster();
            clear();
        }
        public bool Validate()
        {
            if ((string.IsNullOrEmpty(txt_rightsname.Text.Trim())))
            {
                ErrorMessage = "Right Name Should Not be Empty";
                txt_rightsname.Focus();
                return true;
            }
            return false;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (btnsave.Text.Trim().Equals("&Save"))
            {
                insert();
            }
            else
            {
                Update();
            }
        }

        private void clear()
        {
            txt_rightsname.Text = "";
            txt_rightsname.Focus();
            cmb_right.Focus();
            txt_rightsname.Focus();

        }


        private void fetchRightsmaster()
        {
            DataTable dt = new DataTable();
            string sqltext = "pr_FetchRightMaster1";
            dt = dbFunctions.getTable(sqltext);
            DataGridview.DataSource = dt;
            dbFunctions.DGVStyle(DataGridview);
            Load_Rights();
        }



        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (DataGridview.SelectedRows.Count > 0)
            {
                try
                {
                    DataTable dt = new DataTable();

                    int selectedIndex = DataGridview.SelectedRows[0].Index;
                    string rowId = DataGridview[0, selectedIndex].Value.ToString();


                    txthiddenid.Text = DataGridview[0, selectedIndex].Value.ToString();
                    txt_rightsname.Text = DataGridview[1, selectedIndex].Value.ToString();
                    btnsave.Text = "Update";
                    txt_rightsname.Focus();
                }
                catch (Exception)
                {
                    MessageBox.Show("Please select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void fetchitems()
        {

            DataTable dt = new DataTable();
            string sqltext = "pr_FetchMenuRights";
            dt = dbFunctions.getTable(sqltext);
            dgv_menurights.DataSource = dt;


        }

        //private void FetchEntry()
        //{
        //    string sqlText = "pr_FetchMenuName";
        //    DataTable dtSheet = dbFunctions.getTable(sqlText);
        //    dgv_menurights.DataSource = dtSheet;

        //  dbFunctions.DGVStyle(dgv_menurights);
        //  dgv_menurights.Columns[1].Visible = false;
        //  dgv_menurights.Columns[0].Visible = true;
        //  dgv_menurights.Columns[0].Width = 50;
        //  dgv_menurights.ReadOnly = false;
        //}

        private void InsertManu(int Menu)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                SqlParameter param = new SqlParameter();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pr_InsertMenuRights";

                param = new SqlParameter();
                param.ParameterName = "@ur_iRightsId";
                param.SqlDbType = SqlDbType.Int;
                param.Value = int.Parse(cmb_right.SelectedValue.ToString());
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@ur_iMenuId";
                param.SqlDbType = SqlDbType.Int;
                param.Value = int.Parse(Menu.ToString());
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
               


            }

            catch (Exception ex)
            {
            }
        }
        int[] count = new int[100000];

        private void RightsMaster_Load(object sender, EventArgs e)
        {
            fetchRightsmaster();
            clear();

            Load_Rights();
           

            DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
            doWork.HeaderText = "Select";
            doWork.FalseValue = "0";
            doWork.TrueValue = "1";
            dgv_menurights.Columns.Insert(0, doWork);

            fetchitems();
            //FetchEntry();
        }

        public void Load_Rights()
        {
             DataTable dtpt = new DataTable();
            string sqltext = "pr_FetchRightsName";
            dtpt = dbFunctions.getTable(sqltext);
            cmb_right.DataSource = dtpt;
            cmb_right.DisplayMember = "rmm_vRightsName";
            cmb_right.ValueMember = "rmm_iId";
            cmb_right.SelectedIndex = -1;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DataGridview.SelectedRows.Count > 0)
            {
                try
                {
                    DataTable dt = new DataTable();
                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    con.Open();
                    int selectedIndex = DataGridview.SelectedRows[0].Index;
                    txthiddenid.Text = DataGridview[0, selectedIndex].Value.ToString();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "pr_DeleteRightsMaster " + int.Parse(txthiddenid.Text.ToString());
                    dt = dbFunctions.getTable(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fetchRightsmaster();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error While Deleting", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void cmb_right_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                FetchEntry();
                DataTable dtDelete = dbFunctions.getTable("pr_FetchRightMaster '" + cmb_right.SelectedValue.ToString() + "'");
                if (dtDelete.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDelete.Rows.Count; i++)
                    {
                        string Scan = dtDelete.Rows[i]["Rights"].ToString();

                        for (int j = 0; j < dgv_menurights.Rows.Count - 1; j++)
                        {
                            string val = dgv_menurights.Rows[j].Cells[1].Value.ToString();

                            if (val == Scan)
                            {
                                dgv_menurights.Rows[j].Cells[0].Value = "1";
                            }
                        }
                    }
                }

                else
                {
                    for (int j = 0; j < dgv_menurights.Rows.Count - 1; j++)
                    {

                        dgv_menurights.Rows[j].Cells[0].Value = "0";

                    }
                }
            }
            catch
            {
            }
 
        }

        public void FetchEntry()
        {
            string sqlText = "pr_FetchMenuName";
            DataTable dtSheet = dbFunctions.getTable(sqlText);
            dgv_menurights.DataSource = dtSheet;

            dbFunctions.DGVStyle(dgv_menurights);
            dgv_menurights.Columns[1].Visible = false;
            dgv_menurights.Columns[0].Visible = true;
            dgv_menurights.Columns[0].Width = 50;
            dgv_menurights.ReadOnly = false;
        }

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    int gridCount = dgv_menurights.Rows.Count - 1;

        //    int selectCount = 0;
        //    int ArraCouint = 0;
        //    for (int i = 0; i < dgv_menurights.Rows.Count - 1; i++)
        //    {
        //        if (Convert.ToString(dgv_menurights.Rows[i].Cells[0].Value) == "1")
        //        {
        //            selectCount = selectCount + 1;
        //            count[ArraCouint] = i;//int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
        //            ArraCouint = ArraCouint + 1;
        //        }
        //    }
        //    DataTable dtDelete = dbFunctions.getTable("pr_deleteRightMaster '" + cmb_right.SelectedValue.ToString() + "'");
        //    if (selectCount > 0)
        //    {
        //        for (int i = 0; i < dgv_menurights.Rows.Count - 1; i++)
        //        {
        //            if (Convert.ToString(dgv_menurights.Rows[i].Cells[0].Value) == "1")
        //            {
        //                int Menu = int.Parse(dgv_menurights.Rows[i].Cells[1].Value.ToString());
        //                int catagory = int.Parse(cmb_right.SelectedValue.ToString());

        //                InsertManu(Menu);

        //            }
        //        }

        //        MessageBox.Show("'" + cmb_right.Text.ToString() + "' Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        //fetchitems();
        //        //clear();
        //    }
        //}

        private void txt_rightsname_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                btnsave.Focus();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int gridCount = dgv_menurights.Rows.Count - 1;

            int selectCount = 0;
            int ArraCouint = 0;
            for (int i = 0; i < dgv_menurights.Rows.Count - 1; i++)
            {
                if (Convert.ToString(dgv_menurights.Rows[i].Cells[0].Value) == "1")
                {
                    selectCount = selectCount + 1;
                    count[ArraCouint] = i;//int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    ArraCouint = ArraCouint + 1;
                }
            }
            DataTable dtDelete = dbFunctions.getTable("pr_deleteRightMaster '" + cmb_right.SelectedValue.ToString() + "'");
            if (selectCount > 0)
            {
                for (int i = 0; i < dgv_menurights.Rows.Count; i++)
                {
                    if (Convert.ToString(dgv_menurights.Rows[i].Cells[0].Value) == "1")
                    {
                        int Menu = int.Parse(dgv_menurights.Rows[i].Cells[1].Value.ToString());
                        int catagory = int.Parse(cmb_right.SelectedValue.ToString());

                        InsertManu(Menu);

                    }
                }

                MessageBox.Show("'" + cmb_right.Text.ToString() + "' Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //fetchitems(); MessageBox.Show("Inserted Successfully");
                //clear();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
             dbFunctions.isclose = true; this.Close(); 
        }

      
      
     }
}
