using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ATM_sim
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        public static string stdNum;
        public static string name;
        public static string surname;
        public static decimal balance;  
        public static string passwd;
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private string studentStatus()
        {
            string statuss = "";
            try
            {
                con.Open();

                if (con.State == ConnectionState.Open) //check if connection is opened succefully 
                {
                   
                    string query = "SELECT * FROM Students WHERE studentNum = '" + txtStudentNum.Text + "' AND Password = '" + txtPassword.Text + "'";

                    SqlDataAdapter dpt = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    dpt.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            statuss = rdr.GetString(6).ToString();


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
            return statuss;
        }
        private bool isValid()
        {
            if (txtStudentNum.Text.TrimStart() == string.Empty || txtStudentNum.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("INVALID CREDENTIALS!");
                return false;
            }
            if (!Regex.IsMatch(txtStudentNum.Text, @"^\d{8}$"))
            {
                MessageBox.Show("INVALID CREDENTIALS!");
                return false;
            }
            if(studentStatus() == "Inactive")
            {
                MessageBox.Show("Account status is inactive please consult with the admin!");
                return false;
            }
            return true;
        }

        private void lblSignUp_Click(object sender, EventArgs e)
        {
            Account account = new Account();
            account.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();  
        }

        public static string AccNumber;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            txtStudentNum.Clear();  
            txtPassword.Clear();
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(isValid())
            {
                string studentNum = txtStudentNum.Text.ToString();
                string password = txtPassword.Text.ToString();
                try
                {
                    con.Open();

                    if(con.State == ConnectionState.Open) //check if connection is opened succefully 
                    {
                        string query = "SELECT * FROM Students WHERE studentNum = '" + txtStudentNum.Text + "' AND Password = '" + txtPassword.Text + "'";

                        SqlDataAdapter dpt = new SqlDataAdapter(query, con);
                        DataTable dt = new DataTable();
                        dpt.Fill(dt);

                        if(dt.Rows.Count == 1)
                        {
                            SqlCommand cmd = new SqlCommand(query, con);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while(rdr.Read())
                            {
                                stdNum = rdr.GetInt32(0).ToString();
                                name = rdr.GetString(1).ToString();
                                surname = rdr.GetString(2).ToString();
                                passwd = rdr.GetString(7).ToString();
                                balance = rdr.GetDecimal(8);

                                
                                break;
                            }  
                            MessageBox.Show(String.Format("Welcome " + name + " " + surname));

                            HomePage homePage = new HomePage();
                            //homePage.GetData(stdNum, name, surname, balance);
                            homePage.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("INVALID CREDENTIALS!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Connection failed");
                    }
                    con.Close();
                }
                catch(SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            AdminLogin logA = new AdminLogin();
            logA.Show();
            this.Hide();    
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            forgotpassword fg = new forgotpassword();
            fg.Show();
            this.Hide();
        }

        private void lblHelpFunction_Click(object sender, EventArgs e)
        {
            HelpFunction helpFunction = new HelpFunction();
            helpFunction.ShowDialog();
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
