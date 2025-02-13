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

namespace ATM_sim
{
    public partial class AdminSignUp : Form
    {
        public AdminSignUp()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        private void AdminSignUp_Load(object sender, EventArgs e)
        {

        }

        private bool adminExist(string adminEmail)
        {
            bool exist = false;
            string email = " ";
            try
            {
                conn.Open();

                if (conn.State == ConnectionState.Open) //check if connection is opened succefully 
                {
                    string query = "SELECT * FROM Admin WHERE Email = '" + adminEmail + "'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        email = rdr.GetString(3).ToString();
                        break;
                    }
                    if (email == adminEmail)
                    {
                        exist = true;
                    }
                }
                else
                {
                    MessageBox.Show("Connection failed");
                }
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return exist;
        }
        private bool isValid()
        {
            if (string.IsNullOrEmpty(txtadminName.Text))
            {
                MessageBox.Show("First name is required.");
                return false;
            }
            if (string.IsNullOrEmpty(txtadminSurname.Text))
            {
                MessageBox.Show("Last name is required.");
                return false;
            }
            if (!Regex.IsMatch(txtadminemail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email must be a valid email address e.g., email@gmail.com");
                return false;
            }
            if (!Regex.IsMatch(txtpassword.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%&*]).{8,}$"))
            {
                MessageBox.Show("Password must be 8 characters long and include at leats one uppercase, one lowercase, and one special character(!, @, #, $, %, &, *).");
                return false;
            }
            return true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                if (!(adminExist(txtadminemail.Text)))
                {
                    try
                    {
                        conn.Open();

                        if (conn.State == ConnectionState.Open)
                        {
                            string query = "INSERT INTO Admin VALUES('" + txtadminName.Text + "','" + txtadminSurname.Text + "','" + txtadminemail.Text + "','" + txtpassword.Text + "')";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("Connection failed");
                        }
                        conn.Close();
                        MessageBox.Show("Admin added succefully");
                        txtadminName.Clear();
                        txtadminemail.Clear();
                        txtadminSurname.Clear();
                        txtpassword.Clear();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                }
                else
                {
                    MessageBox.Show("Email already exist please try another one or request ");
                    txtadminName.Clear();
                    txtadminemail.Clear();
                    txtadminSurname.Clear();
                    txtpassword.Clear();
                }
            }
        }

        private void lblHelpFunction_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblSignUp_Click(object sender, EventArgs e)
        {
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
            this.Hide();
        }
    }
}
