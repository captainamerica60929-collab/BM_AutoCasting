using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Text;
using System.Windows.Forms;
using Maintanence_Printing_Tool;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;

namespace LarchERP.Master
{

    public partial class Preventive_Matainance : Form
    {
        string ErrorMessage = "";

        public bool isPreventiveMatainanceLoad = false;
        public Preventive_Matainance()
        {
            InitializeComponent();
            InitializeDataGridView();
        }
        private void InitializeDataGridView()
        {
            // Create and configure the DataGridView
            DataGridView dgv = new DataGridView
            {
                Name = "dataGridView1",
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 60 }
            };

            // Add a Text Column
            dgv.Columns.Add("CheckPoint", "Check Point");

            // Add Button Column
            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn
            {
                Name = "IMAGE",
                HeaderText = "Upload",
                Text = "Browse",
                UseColumnTextForButtonValue = true
            };
            dgv.Columns.Add(btnCol);

            // Add Image Column
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn
            {
                Name = "Person",
                HeaderText = "Photo",
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            dgv.Columns.Add(imgCol);

            // Add a few rows to demonstrate
            for (int i = 0; i < 3; i++)
            {
                dgv.Rows.Add("Check " + (i + 1));
            }

            // Add the DataGridView to the form
            this.Controls.Add(dgv);

            // Add event handler
           // dgv.CellContentClick += DataGridView1_CellContentClick;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MMD_Mould_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ispartLoad)
            {
                ismould = true;
                try
                {


                    DataTable dt = dbFunctions.getTable("pr_Get_LoadMouldDetails1 '" + MPM_Part_No.SelectedValue.ToString() + "'");
                   
                    MPM_Mould_Id.DataSource = dt;
                    MPM_Mould_Id.DisplayMember = "MLD_MouldNo";
                    MPM_Mould_Id.ValueMember = "MLD_ID";
                    MPM_Mould_Id.SelectedIndex = -1;

                    if (dt.Rows.Count > 0)
                    {
                        MPM_Mould_Id.Text=dt.Rows[0]["MLD_MouldNo"].ToString();

                    }
                    ismould = true;
                    dataGridView1.DataSource = null;
                    MPM_CUM_Qty.Text = "";
                    textBox1.Text = "";

                }
                catch (Exception ex) { }
            }
          
        }

        private void Preventive_Matainance_Load(object sender, EventArgs e)
        {
            LoadMouldName();
            DataGridViewButtonColumn extraButtonColumn = new DataGridViewButtonColumn();
            extraButtonColumn.Name = "ExtraButton";
            extraButtonColumn.HeaderText = "Extra Button";
            extraButtonColumn.Text = "Click Me";
            extraButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(extraButtonColumn);

            // Add the Browse Button column (second column)
            DataGridViewButtonColumn browseButtonColumn = new DataGridViewButtonColumn();
            browseButtonColumn.Name = "BrowseButton";
            browseButtonColumn.HeaderText = "Browse";
            browseButtonColumn.Text = "Browse";
            browseButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(browseButtonColumn);

            // Add the Image column (third column)
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "ImageColumn";
            imageColumn.HeaderText = "Image";
            dataGridView1.Columns.Add(imageColumn);

            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            //btn.HeaderText = "Image";
            //btn.Name = "Image";
            //btn.Text = "Upload";
            //btn.UseColumnTextForButtonValue = true;
            //dataGridView1.Columns.Add(btn);


        }


        bool ispartLoad = false;
        bool ismould = false;
        public void LoadMouldName()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_Get_LoadMouldName");
                MPM_Part_No.DataSource = dt;
                MPM_Part_No.DisplayMember = "MLD_Mold";
                MPM_Part_No.ValueMember = "MLD_ID";
                MPM_Part_No.SelectedIndex = -1;
                ispartLoad = true;
                ismould = false;
            }
            catch
            {
            }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            dbFunctions.isPM_data = false;
            this.Close();

        }

        private void MPM_Mould_Id_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtx = dbFunctions.getTable("pr_get_Mould_Shorts  " + MPM_Part_No.SelectedValue + "," + MPM_Mould_Id.SelectedValue.ToString());
            if (dtx.Rows.Count > 0)
            {
                textBox2.Text = dtx.Rows[0]["Date"].ToString();
                textBox3.Text = dtx.Rows[0]["Type"].ToString();
                textBox1.Text = dtx.Rows[0]["MLD_Machine_Tonage"].ToString();
                MPM_CUM_Qty.Text = dtx.Rows[0]["Shots"].ToString(); 


            }
            try
            {
                DataTable a = dbFunctions.getTable("select * from Item_Master left outer join mold_master on IM_Mould=MLD_ID where IM_ID ='" + MPM_Part_No.SelectedValue.ToString()+ "'");

                if (a.Rows.Count > 0)
                {
                    MPM_Frequency_Type.Text = a.Rows[0]["MLD_Frequency"].ToString();

                }

            }
            catch { }

        }

        private void Preventive_Matainance_Shown(object sender, EventArgs e)
        {
            if (dbFunctions.isPM_data == true)
            {
                LoadMouldName();
                
                MPM_Part_No.Text = dbFunctions.Mould_Name;
                MPM_Mould_Id.SelectedValue = dbFunctions.Mould_ID;
                DataTable dt = dbFunctions.getTable("pr_Get_PreventiveMainatanceDetails '" + dbFunctions.Mould_ID + "'");
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["CP_Checkpoint_Name"].ReadOnly = true;
                dataGridView1.Columns["RM_Requirement"].ReadOnly = true;
                dbFunctions.DGVStyle1(dataGridView1);
            }
        }
        int b = 0;
        private void btnsave_Click(object sender, EventArgs e)
        {
            String a = "";


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Status"].Value != null && !string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["Status"].Value.ToString())) 
                {

                    b = b + 1;

                }
                else
                {
                    MessageBox.Show("Please enter the value", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }
            if (dataGridView1.Rows.Count == b)
            {
                if (Validate())
                {
                    MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                insert();
            }
            b = 0;
            //insert();
        }
        public bool Validate()
        {
            if ((string.IsNullOrEmpty(textBox4.Text.Trim())))
            {
                ErrorMessage = "Attend by Should Not be Empty";
                textBox4.Focus();
                return true;
            }
            return false;
        }

        public void insert()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Mould_Preventive_Maintance";
                com.Parameters.Add("@MPM_Frequency_Type", SqlDbType.VarChar).Value = MPM_Frequency_Type.Text.ToString();
                com.Parameters.Add("@MPM_PM_Date", SqlDbType.VarChar).Value = MPM_PM_Date.Value.ToString("dd-MMM-yyyy hh:mm tt");
                com.Parameters.Add("@MPM_END_TIME", SqlDbType.VarChar).Value = MPM_END_TIME.Value.ToString("dd-MMM-yyyy hh:mm tt");
                
                com.Parameters.Add("@MPM_Part_No", SqlDbType.VarChar).Value = MPM_Part_No.Text.ToString();
                com.Parameters.Add("@MPM_Mould_Id", SqlDbType.VarChar).Value = MPM_Mould_Id.SelectedValue.ToString();
                com.Parameters.Add("@MPM_CUM_Qty", SqlDbType.VarChar).Value = MPM_CUM_Qty.Text.ToString();
                com.Parameters.Add("@MPM_Machine_ID", SqlDbType.VarChar).Value = MPM_Machine_ID.Text.ToString();
                com.Parameters.Add("@MPM_Attend_by ", SqlDbType.VarChar).Value = textBox4.Text.ToString();
                com.Parameters.Add("@MPM_MMSTATUS", SqlDbType.VarChar).Value ="Mould";
                com.Parameters.Add("@MPM_CreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Remarks.Text.ToString();


                com.Parameters.Add("@ID", SqlDbType.Int);
                com.Parameters["@ID"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery(); com.Connection.Close();
                string POD_PO_No = com.Parameters["@ID"].Value.ToString();

                Save_details(POD_PO_No);
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public void Save_details(string MRMD_MRM_ID)
        {
            using (SqlConnection con = new SqlConnection(dbFunctions.connectionstring))
            {
                con.Open();

                // Begin a transaction to batch all insertions
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    SqlCommand com = new SqlCommand
                    {
                        Connection = con,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "Pr_Insert_Mould_Preventive_Maintance_Details",
                        Transaction = transaction
                    };

                    try
                    {
                        // Loop through all rows and add them to the command
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            // Clear any existing parameters to prevent re-adding
                            com.Parameters.Clear();

                            com.Parameters.Add("@MPMD_MPM_ID", SqlDbType.VarChar).Value = MRMD_MRM_ID;
                            com.Parameters.Add("@MPMD_CheckPart", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Check Point"].Value.ToString();
                            com.Parameters.Add("@MPMD_Requirement", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Observation"].Value.ToString();
                            com.Parameters.Add("@MPMD_OKStatus", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Status"].Value.ToString();
                            com.Parameters.Add("@MPMD_Not_OK_Staus", SqlDbType.VarChar).Value = "";
                            com.Parameters.Add("@MPMD_Remarks", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                            com.Parameters.Add("@MPMD_CreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;

                            // Handle image data
                            byte[] imageData2 = null;
                            try
                            {
                                var cellValue = dataGridView1.Rows[i].Cells["ImageColumn"].Value;
                                if (cellValue != null && cellValue is Image img)
                                {
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                        imageData2 = ms.ToArray();
                                    }
                                }
                            }
                            catch
                            {
                                // Optional: log or ignore image handling failure
                            }

                            com.Parameters.Add("@ImageData", SqlDbType.Image).Value = (object)imageData2 ?? DBNull.Value;

                            // Execute the command for the current row
                            com.ExecuteNonQuery();
                        }

                        // Commit the transaction after all rows are processed
                        transaction.Commit();

                        MessageBox.Show("Details Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception Ex)
                    {
                        // Rollback the transaction in case of an error
                        transaction.Rollback();
                        MessageBox.Show("Error: " + Ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            //    try
            //    {
            //        con.Open();
            //        SqlCommand com = new SqlCommand();
            //        com.Connection = con;
            //        com.CommandType = CommandType.StoredProcedure;
            //        com.CommandText = "Pr_Insert_Mould_Preventive_Maintance_Details";
            //        com.Parameters.Add("@MPMD_MPM_ID", SqlDbType.VarChar).Value = MRMD_MRM_ID;
            //        com.Parameters.Add("@MPMD_CheckPart", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Check Point"].Value.ToString();
            //        com.Parameters.Add("@MPMD_Requirement", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Observation"].Value.ToString();
            //        com.Parameters.Add("@MPMD_OKStatus", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Status"].Value.ToString(); 
            //        com.Parameters.Add("@MPMD_Not_OK_Staus", SqlDbType.VarChar).Value = "";
            //        com.Parameters.Add("@MPMD_Remarks", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
            //        com.Parameters.Add("@MPMD_CreatedBy", SqlDbType.VarChar).Value = dbFunctions.username;
            //        //byte[] imageData2 = null;
            //        //try
            //        //{
            //        //    MemoryStream ms1 = new MemoryStream();
            //        //    Image img .Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
            //        //    imageData2 = ms1.GetBuffer();
            //        //}
            //        //catch
            //        //{

            //        //}
            //        byte[] imageData2 = null;
            //        try
            //        {
            //            var cellValue = dataGridView1.Rows[i].Cells["ImageColumn"].Value;
            //            if (cellValue != null && cellValue is Image img)
            //            {
            //                using (MemoryStream ms = new MemoryStream())
            //                {
            //                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //                    imageData2 = ms.ToArray();
            //                }
            //            }
            //        }
            //        catch
            //        {
            //            // Optional: log or ignore image handling failure
            //        }

            //        com.Parameters.Add("@ImageData", SqlDbType.Image).Value = (object)imageData2 ?? DBNull.Value;
            //      //  com.Parameters.Add("@ImageData", SqlDbType.Image).Value = (object)imageData2;
            //        com.ExecuteNonQuery();
            //        com.Connection.Close();

            //    }
            //    catch (Exception Ex)
            //    {
            //        MessageBox.Show("Error :" + Ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }

            //}


            //MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //this.Close();


        }

        private void MPM_Frequency_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ismould)
            {
                try
                {

                    DataTable dt;
                    if (MPM_Frequency_Type.Text.Equals("48000 Shots") || MPM_Frequency_Type.Text.Equals("6 Month "))
                    {
                        dt = dbFunctions.getTable("pr_Get_PreventiveMainatanceDetails'" + MPM_Mould_Id.SelectedValue.ToString() + "'");
                    }
                    else
                    {
                        dt = dbFunctions.getTable("pr_Get_PreventiveMainatanceDetails15 '" + MPM_Mould_Id.SelectedValue.ToString() + "'");
                    }

                    dataGridView1.DataSource = dt;
                    dbFunctions.DGVStyle(dataGridView1);
                    //dataGridView1.Columns["Action Tacken"].Width = 200;
                    dataGridView1.Columns["Check Point"].ReadOnly = true;
                    dataGridView1.Columns["Observation"].ReadOnly = true;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;


                    DataTable dtx = dbFunctions.getTable("pr_get_Mould_Shorts  " + MPM_Part_No.SelectedValue + "," + MPM_Mould_Id.SelectedValue.ToString());
                    if (dtx.Rows.Count > 0)
                    {
                        MPM_Machine_ID.Text = dtx.Rows[0]["MM_ID"].ToString();
                        textBox1.Text = dtx.Rows[0]["MM_MachineName"].ToString();
                        MPM_CUM_Qty.Text = dtx.Rows[0]["Shots"].ToString();

                    }


                }
                catch { }
            }
        }

        private void LOADSUPPLIER()
        {
            try
            {
                DataTable dt = dbFunctions.getTable("pr_LoadSupplier");
                //comboBox1.DataSource = dt;
                //comboBox1.DisplayMember = "SM_Name";
                //comboBox1.ValueMember = "SM_ID";
                //comboBox1.SelectedIndex = -1;
                
            }
            catch
            {
            }
        }

        private void Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label14_Click(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
           
            panel5.Visible = true;
            DataTable A = dbFunctions.getTable("SELECT MCS_ID,(MCS_Sparename + '/'  + MCS_SpareSize ) as [MCS_Sparename]  FROM Mold_Critical_Spares WHERE MCS_Status='A'     AND MCS_Type='Mould'");
            cts_name.DataSource = A;
            cts_name.DisplayMember = "MCS_Sparename";
            cts_name.ValueMember = "MCS_ID";
            cts_name.SelectedIndex = -1;
            display1();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        String ID1 = "0";
        String ID2 = "0";
        private void button1_Click(object sender, EventArgs e)
        {
            
              SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.Text;
                    if (ID1 == "0")
                    {
                        DataTable dd = dbFunctions.getTable("select isnull(max(cts_id),0)+1 from cts_usage");
                        if (dd.Rows.Count > 0)
                        {
                            ID1 = dd.Rows[0][0].ToString();
                        }
                        DataTable dd1 = dbFunctions.getTable("select isnull(max(MPM_ID),0)+1 from Mould_Preventive_Maintance");
                        if (dd.Rows.Count > 0)
                        {
                            ID2 = dd1.Rows[0][0].ToString();
                        }

                            com.CommandText = "INSERT INTO cts_usage (cts_id, cts_name_id, cts_qty, cts_create_user_name, cts_create_getdate, cts_status, cts_usage,cts_frequency,cts_machine_name,cts_machine_id,BD_MID,cts_remarks) " +
                           "VALUES (@cts_id, @cts_name_id, @cts_qty, @cts_create_user_name, GETDATE(), @cts_status, @cts_usage,@cts_frequency,@cts_machine_name,@cts_machine_id,@BD_MID,@cts_remarks)";
                    }
                    else
                    {
                        com.CommandText = "UPDATE cts_usage " +
                          "SET cts_name_id = @cts_name_id, " +
                          "cts_qty = @cts_qty, " +
                          "cts_status = @cts_status, " +
                          "cts_usage = @cts_usage, " +
                          "cts_del_user_name = @cts_del_user_name, " +
                          "cts_delete_getdate = GETDATE(), " +
                          "cts_remarks= @cts_remarks  " +
                          "WHERE cts_id = @cts_id";
                        //Type = "EDIT";

                    }

                    com.Parameters.Add("@cts_id", SqlDbType.VarChar).Value = ID1.ToString();
                com.Parameters.Add("@BD_MID", SqlDbType.VarChar).Value = ID2.ToString();

                com.Parameters.Add("@cts_name_id", SqlDbType.VarChar).Value = cts_name.SelectedValue.ToString();
                    com.Parameters.Add("@cts_qty", SqlDbType.Int).Value = qty.Text.ToString();
                    com.Parameters.Add("@cts_create_user_name", SqlDbType.VarChar).Value = dbFunctions.username;
                    //com.Parameters.Add("@cts_create_getdate", SqlDbType.VarChar).Value = textBox2.Text;
                    com.Parameters.Add("@cts_status", SqlDbType.VarChar).Value = "A";
                    com.Parameters.Add("@cts_usage", SqlDbType.VarChar).Value = "Mould";
                    com.Parameters.Add("@cts_frequency", SqlDbType.VarChar).Value = MPM_Frequency_Type.Text;
                    com.Parameters.Add("@cts_machine_name", SqlDbType.VarChar).Value = MPM_Part_No.Text;
                    com.Parameters.Add("@cts_machine_id", SqlDbType.VarChar).Value = MPM_Part_No.SelectedValue.ToString();
                com.Parameters.Add("@cts_remarks", SqlDbType.VarChar).Value = cts_remarks.Text;



                com.ExecuteNonQuery();
                    com.Connection.Close();
                    MessageBox.Show("Details Saved Successfully ", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ID1 = "0";
                   


            }
                catch (Exception Ex)
                {
                    //dbFunctions.Logs(Ex.Message, dbFunctions.username);
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                display1();
                clear1();
        }

            private void clear1()
            {
                qty.Text = "";
                cts_name.Text = "";
            cts_remarks.Text = "";
        }

            private void display1()
            {
                DataTable a = dbFunctions.getTable("pr_load_Mould_details '" + MPM_Frequency_Type.Text + "','" + MPM_Part_No.Text + "'");
                dataGridView3.DataSource = a;
                dbFunctions.DGVStyle(dataGridView3);
            }

        private void button3_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure Want to Delete Press YES", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable dt = dbFunctions.getTable("update cts_usage set cts_status='D' WHERE cts_id='" + dataGridView3.SelectedRows[0].Cells[0].Value.ToString() + "'");
                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    display1();
                    //clear1();

                }
            }
            else
            {
                MessageBox.Show("Please Select Row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { // Handle the Extra Button click
            if (e.ColumnIndex == dataGridView1.Columns["ExtraButton"].Index)
            {
                // Custom action for the extra button (e.g., show a message)
                MessageBox.Show("You clicked the extra button in row " + e.RowIndex);
            }

            // Handle the Browse Button click
            if (e.ColumnIndex == dataGridView1.Columns["BrowseButton"].Index)
            {
                var currentValue = dataGridView1.Rows[e.RowIndex].Cells["ImageColumn"].Value;

                if (currentValue != null && currentValue is Image)
                {
                    // If there's already an image, set it to null (remove it)
                    dataGridView1.Rows[e.RowIndex].Cells["ImageColumn"].Value = null;
                }
                else
                {
                    // Open the file dialog to select an image
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                    openFileDialog.Title = "Select an Image";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string imagePath = openFileDialog.FileName;

                        // Set the selected image in the corresponding row's image column
                        //dataGridView1.Rows[e.RowIndex].Cells["ImageColumn"].Value = Image.FromFile(imagePath);
                        FileInfo fileInfo = new FileInfo(imagePath);

                        // Check if file size is less than or equal to 5MB (5 * 1024 * 1024 bytes)
                        if (fileInfo.Length <= 5 * 1024 * 1024)
                        {
                            // Set the selected image in the corresponding row's image column
                            dataGridView1.Rows[e.RowIndex].Cells["ImageColumn"].Value = Image.FromFile(imagePath);
                        }
                        else
                        {
                            MessageBox.Show("Please select an image smaller than or equal to 5MB.", "File Size Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }
        int remaining = 0;
        private void qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int qty1 = Convert.ToInt32(qty.Text);  // Fix casing (qty.Text instead of qty.text)

                DataTable a = dbFunctions.getTable(@"
                    SELECT 
                        SUM(MCS_Actual) - ISNULL((
                            SELECT SUM(cts_qty) 
                            FROM [CHE_SRUTHY_PLASTIC].[cts_usage] 
                            WHERE cts_name_id = " + cts_name.SelectedValue.ToString() + @" AND cts_status = 'A'
                        ), 0) AS Remaining
                    FROM Mold_Critical_Spares 
                    WHERE MCS_Status = 'A' AND MCS_ID = " + cts_name.SelectedValue.ToString()
                );

                // Check if there's a result and column exists
                if (a.Rows.Count > 0 && a.Columns.Contains("Remaining"))
                {
                    remaining = Convert.ToInt32(a.Rows[0]["Remaining"]);

                    if (qty1 > remaining)
                    {
                        MessageBox.Show("Entered stock quantity is more than available quantity (" + remaining + ").", "Stock Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        qty.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Entered stock quantity is more than available quantity (" + remaining + ").", "Stock Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
        //private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    DataGridView dgv = sender as DataGridView;

        //    if (e.RowIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "IMAGE")
        //    {
        //        using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
        //        {
        //            openFileDialog1.Title = "Select an Image";
        //            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

        //            if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //            {
        //                string selectedImagePath = openFileDialog1.FileName;

        //                try
        //                {
        //                    Image img = Image.FromFile(selectedImagePath);
        //                    dgv.Rows[e.RowIndex].Cells["Person"].Value = img;
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show("Failed to load image: " + ex.Message);
        //                }
        //            }
        //        }
        //    }
    }

            //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
            //    {
            //        //if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "IMAGE")
            //        //{
            //        //    using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            //        //    {
            //        //        openFileDialog1.Title = "Select an Image";
            //        //        openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            //        //        if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //        //        {
            //        //            string selectedImagePath = openFileDialog1.FileName;

            //        //            // Load the image
            //        //           // Image img = Image.FromFile(selectedImagePath);
            //        //            Image img = Image.FromFile("your_file_path.jpg");


            //        //            // Set the image in the appropriate column (make sure the column name is correct)
            //        //            dataGridView1.Rows[e.RowIndex].Cells["Person"].Value = img;
            //        //        }
            //        //    }
            //        //}
            //        DataGridView dgv = sender as DataGridView;

            //        // Make sure we're clicking the "IMAGE" button column
            //        if (e.RowIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "IMAGE")
            //        {
            //            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            //            {
            //                openFileDialog1.Title = "Select an Image";
            //                openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            //                if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //                {
            //                    string selectedImagePath = openFileDialog1.FileName;

            //                    try
            //                    {
            //                        Image img = Image.FromFile(selectedImagePath);
            //                        dgv.Rows[e.RowIndex].Cells["Person"].Value = img;
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        MessageBox.Show("Failed to load image: " + ex.Message);
            //                    }
            //                }
            //            }
            //        }
            //    }

    }

 