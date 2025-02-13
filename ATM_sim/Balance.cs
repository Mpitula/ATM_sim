using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ATM_sim
{
    public partial class Balance : Form
    {
        public Balance()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");
        private void loadbalance(string stdnum)
        {
            try
            {
                decimal balance = 0;
                con.Open();

                if (con.State == ConnectionState.Open) //check if connection is opened succefully 
                {
                    string query = "SELECT * FROM Students WHERE studentNum = '" + stdnum + "'";

                    SqlDataAdapter dpt = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    dpt.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            balance = rdr.GetDecimal(8);
                            break;
                        }
                        lblstudentNum.Text = stdnum;
                        lblBalance.Text = "R " + Math.Round(balance, 2).ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Connection failed");
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        private void Balance_Load(object sender, EventArgs e)
        {
            loadbalance(LoginPage.stdNum);
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();
        }

        private void lblHelpFunction_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
