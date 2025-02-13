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
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ATM_sim
{
    public partial class Account : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");
        public Account()
        {
            InitializeComponent();
        }

        private bool isValid()
        {
            if (!Regex.IsMatch(txtStudentNum.Text, @"^\d{8}$"))
            {
                MessageBox.Show("Student number is required and must be a valid 8-digits number.");
                return false;
            }
            if(string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("First name is required.");
                return false;
            }
            if (string.IsNullOrEmpty(txtSurname.Text))
            {
                MessageBox.Show("Last name is required.");
                return false;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Address is required.");
                return false;
            }
            if (!Regex.IsMatch(txtPassword.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%&*]).{8,}$"))
            {
                MessageBox.Show("Password must be 8 characters long and include at leats one uppercase, one lowercase, and one special character(!, @, #, $, %, &, *).");
                return false;
            }
            if (!Regex.IsMatch(txtPhone.Text, @"^\d{10}$"))
            {
                MessageBox.Show("Phone number must be a valid 10-digit number.");
                return false;
            }
            if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email must be a valid email address e.g., email@gmail.com");
                return false;
            }
            return true;
        }

        private bool studentExist(int studentNum)
        {
            bool exist = false;
            int id = 0; 
            try
            {
                conn.Open();

                if (conn.State == ConnectionState.Open) //check if connection is opened succefully 
                {
                    string query = "SELECT * FROM Students WHERE studentNum = '" + studentNum + "'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        id = rdr.GetInt32(0);
                        break;
                    }
                    if (id == studentNum)
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
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(isValid())
            {
                int balance = 0;
                int AdminId = 1;
                int studentNum = int.Parse(txtStudentNum.Text);
                string status = "Active";

                if (!studentExist(studentNum))
                {
                    try
                    {
                        conn.Open();

                        if(conn.State == ConnectionState.Open)
                        {
                            string query = "INSERT INTO Students VALUES('" + txtStudentNum.Text + "','" + txtName.Text + "','" + txtSurname.Text + "','" + txtPhone.Text + "','" + txtAddress.Text + "','" + txtEmail.Text + "','" + status + "','" + txtPassword.Text + "','" + balance + "','" + AdminId + "')";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("Connection failed");
                        }
                        conn.Close();
                        MessageBox.Show("Student added succefully");
                        txtStudentNum.Clear();
                        txtName.Clear();
                        txtSurname.Clear();
                        txtAddress.Clear();
                        txtPassword.Clear();
                        txtPhone.Clear();
                        txtEmail.Clear();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                }
                else
                {
                    MessageBox.Show("Student with this student number already exist.");
                    txtStudentNum.Clear();
                    txtName.Clear();
                    txtSurname.Clear();
                    txtAddress.Clear();
                    txtPassword.Clear();
                    txtPhone.Clear();
                    txtEmail.Clear();
                }
            }
        }

        

        private void lblLogout_Click(object sender, EventArgs e)
        {
            LoginPage log = new LoginPage();
            log.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Account_Load(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbxEducation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_2(object sender, EventArgs e)
        {

        }

        private void txtStudentNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblHelpFunction_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If it's not a digit or control, block the keypress
                e.Handled = true;
            }
        }

        private void txtStudentNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If it's not a digit or control, block the keypress
                e.Handled = true;
            }
        }
    }
}
