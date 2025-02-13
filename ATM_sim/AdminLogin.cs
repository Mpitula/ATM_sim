using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Configuration;
using System.Web;

namespace ATM_sim
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        public static int adminId;
        public static string AdminEmail;
        private bool isValid()
        {
            if (txtAdminEmail.Text.TrimStart() == string.Empty && txtAdminEmail.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("INVALID CREDENTIALS!");
                return false;
            }
            if (txtAdminEmail.Text.TrimStart() == string.Empty || txtAdminPassword.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("INVALID CREDENTIALS!");
                return false;
            }
            return true;
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
           
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(isValid())
            {
                string name = " ";
                string surname = " ";
                try
                {
                    con.Open();

                    if (con.State == ConnectionState.Open) //check if connection is opened succefully 
                    {
                        string query = "SELECT * FROM Admin WHERE Email = '" + txtAdminEmail.Text + "' AND Password = '" + txtAdminPassword.Text + "'";

                        SqlDataAdapter dpt = new SqlDataAdapter(query, con);
                        DataTable dt = new DataTable();
                        dpt.Fill(dt);

                        if (dt.Rows.Count == 1)
                        {
                            SqlCommand cmd = new SqlCommand(query, con);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                adminId = rdr.GetInt32(0);
                                name = rdr.GetString(1).ToString();
                                surname = rdr.GetString(2).ToString();
                                AdminEmail = rdr.GetString(3).ToString();

                                break;
                            }
                            MessageBox.Show(String.Format("Welcome " + name + " " + surname));
                            Admin admin = new Admin();
                            admin.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("INVALID CREDENTIALS!");
                            txtAdminEmail.Clear();
                            txtAdminPassword.Clear();
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
        }

        private void lblSignUp_Click(object sender, EventArgs e)
        {
            LoginPage log = new LoginPage();
            log.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AdminForgotPassword adminForgotPassword = new AdminForgotPassword();
            adminForgotPassword.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            AdminSignUp adminSignUp = new AdminSignUp();
            adminSignUp.Show();
            this.Hide();
        }
    }
}
