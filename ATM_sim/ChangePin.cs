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
using System.Configuration;
using System.Text.RegularExpressions;

namespace ATM_sim
{
    public partial class ChangePin : Form
    {
        public ChangePin()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChangePin_Load(object sender, EventArgs e)
        {

        }
        private void btnChangePin_Click(object sender, EventArgs e)
        {
            
        }

        private void lblSignUp_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private bool isValid()
        {
            if (txtNewPassword.Text.TrimStart() == string.Empty || txtConfirmPin.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Missing information required.");
                return false;
            }
            else if(txtConfirmPin.Text != txtNewPassword.Text)
            {
                MessageBox.Show("Confirmation password does not match with the new password");
                return false;
            }
            else if (!Regex.IsMatch(txtNewPassword.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%&*]).{8,}$"))
            {
                MessageBox.Show("Password must be 8 characters long and include at leats one uppercase, one lowercase, and one special character(!, @, #, $, %, &, *).");
                return false;
            }
            else if(txtNewPassword.Text == LoginPage.passwd)
            {
                MessageBox.Show("Cannont implement the same password.");
                return false;
            }
            return true;
        }
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if(isValid()) 
            {
                try
                {
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        string query = "UPDATE Students SET Password = @pass WHERE studentNum = '" + LoginPage.stdNum + "'";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@pass", txtNewPassword.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Password Changed Succesfully");
                        txtNewPassword.Clear();
                        txtConfirmPin.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Connection failed.");
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtConfirmPin_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}


/*private string GetPassword()
        {
            string password = " ";
            try
            {
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
                            password = rdr.GetString(5).ToString();
                            break;
                        }
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
            return password;
        }*/