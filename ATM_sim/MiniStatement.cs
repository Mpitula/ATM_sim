using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ATM_sim
{
    public partial class MiniStatement : Form
    {
        public MiniStatement()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            try
            {
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    string query = "SELECT * FROM Transactions WHERE studentId = '" + LoginPage.stdNum + "'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvMniStmnt.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Connection failed");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void MiniStatement_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void lblSignUp_Click(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblHelpFunction_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblReturn_Click(object sender, EventArgs e)
        {
            HomePage home = new HomePage();
            home.Show();
            this.Hide();
        }
    }
}
