using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Maintanence_Printing_Tool;
namespace CRM_App.Production
{
    public partial class Melting_Logs : Form
    {
        public Melting_Logs()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            insert();
        }
        private void display()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);

            try
            {
                con.Open();

                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Display_Melting_Logs";

                com.Parameters.Add("@routecard_id", SqlDbType.VarChar).Value =
                    dbFunctions.Route_Card_ID;

                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;

                dataGridView1.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.DisplayedCells;

                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);

                MessageBox.Show(
                    Ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Dispose();
            }
        }
        private void insert()
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);

            try
            {
                con.Open();

                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Insert_Melting_Logs";

                com.Parameters.Add("@Machine", SqlDbType.VarChar, 50).Value = Machine.Text;
                com.Parameters.Add("@Furnace_No", SqlDbType.VarChar, 50).Value = Furnace_No.Text;
                com.Parameters.Add("@Date", SqlDbType.Date).Value = Date.Value.Date;
                com.Parameters.Add("@Shift", SqlDbType.VarChar, 20).Value = Shift.Text;
                com.Parameters.Add("@Melt_No", SqlDbType.VarChar, 50).Value = Melt_No.Text;
                com.Parameters.Add("@Alloy_Grade", SqlDbType.VarChar, 50).Value = Alloy_Grade.Text;
                com.Parameters.Add("@Heat_No", SqlDbType.VarChar, 50).Value = Heat_No.Text;

                com.Parameters.Add("@MF_Set_Temp_C", SqlDbType.Decimal).Value = Convert.ToDecimal(MF_Set_Temp_C.Text);
                com.Parameters.Add("@Melt_Time_Start", SqlDbType.Time).Value =
      Melt_Time_Start.Value.TimeOfDay;

                com.Parameters.Add("@Melt_Time_End", SqlDbType.Time).Value =
                    Melt_Time_End.Value.TimeOfDay;
                com.Parameters.Add("@Metal_Mixing_Input_Ingots", SqlDbType.Decimal).Value = Convert.ToDecimal(Metal_Mixing_Input_Ingots.Text);
                com.Parameters.Add("@Metal_Mixing_Input_Runner", SqlDbType.Decimal).Value = Convert.ToDecimal(Metal_Mixing_Input_Runner.Text);
                com.Parameters.Add("@Metal_Mixing_Input_Overflows_Chill_Vents", SqlDbType.Decimal).Value = Convert.ToDecimal(Metal_Mixing_Input_Overflows_Chill_Vents.Text);
                com.Parameters.Add("@Metal_Mixing_Input_Rejection", SqlDbType.Decimal).Value = Convert.ToDecimal(Metal_Mixing_Input_Rejection.Text);

                com.Parameters.Add("@Metal_Temperature_C", SqlDbType.Decimal).Value = Convert.ToDecimal(Metal_Temperature_C.Text);
                com.Parameters.Add("@GR271_Coverall_Qty_Grams", SqlDbType.Decimal).Value = Convert.ToDecimal(GR271_Coverall_Qty_Grams.Text);
                com.Parameters.Add("@Waiting_Time_Min", SqlDbType.VarChar).Value = Waiting_Time_Min.Text;
                com.Parameters.Add("@Degassing_Gas_Pressure_Kg", SqlDbType.Decimal).Value = Convert.ToDecimal(Degassing_Gas_Pressure_Kg.Text);
                com.Parameters.Add("@Degassing_Duration_Min", SqlDbType.Decimal).Value = Convert.ToDecimal(Degassing_Duration_Min.Text);

                com.Parameters.Add("@Test_Piece_Result", SqlDbType.VarChar, 100).Value = Test_Piece_Result.Text;
                com.Parameters.Add("@Transfer_Temp_C", SqlDbType.Decimal).Value = Convert.ToDecimal(Transfer_Temp_C.Text);

                com.Parameters.Add("@Done_By", SqlDbType.VarChar, 100).Value = Done_By.Text;
                com.Parameters.Add("@Verified_By", SqlDbType.VarChar, 100).Value = Verified_By.Text;
                com.Parameters.Add("@created_by", SqlDbType.VarChar, 100).Value = dbFunctions.username;
                com.Parameters.Add("@route_Card", SqlDbType.VarChar, 199).Value = dbFunctions.Route_Card_ID;

                com.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Melting Details Saved Successfully",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Clear();
                display();
            }
            catch (Exception Ex)
            {
                dbFunctions.Logs(Ex.Message, dbFunctions.username);

                MessageBox.Show(Ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Dispose();
            }
        }

        private void Melting_Logs_Load(object sender, EventArgs e)
        {
            display();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
