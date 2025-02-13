using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ATM_sim
{
    public partial class AdminForgotPassword : Form
    {
        public AdminForgotPassword()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        private bool isvalid()
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Please enter an email address.");
                return false;
            }
            if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email must be a valid email address e.g., email@gmail.com");
                return false;
            }
            return true;
        }
        private void SendPasswordEmail(string toEmail, string password)
        {
            string fromEmail = "37460366@mynwu.ac.za";
            string emailPassword = "Alone#12";
            string smtpHost = "smtp.office365.com";  // Replace with your SMTP server
            int smtpPort = 587;  // Replace with your SMTP port

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromEmail, "ATM SIMULATION SYSTEM");
                    mail.To.Add(toEmail);
                    mail.Subject = "Your Password";
                    mail.Body = $"Your  Password is: {password}";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(smtpHost, smtpPort))
                    {
                        smtp.Credentials = new NetworkCredential(fromEmail, emailPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        MessageBox.Show("Password has been sent to your email.");
                    }
                }
                LoginPage loginPage = new LoginPage();
                loginPage.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending email: {ex.Message}");
            }
        }
        private void AdminForgotPassword_Load(object sender, EventArgs e)
        {

        }

        private void btnemail_Click(object sender, EventArgs e)
        {
            if (isvalid())
            {
                try
                {
                    string password = "";
                    con.Open();

                    if (con.State == ConnectionState.Open) //check if connection is opened succefully 
                    {
                        string query = "SELECT * FROM Admin WHERE Email = '" + txtEmail.Text + "'";

                        SqlDataAdapter dpt = new SqlDataAdapter(query, con);
                        DataTable dt = new DataTable();
                        dpt.Fill(dt);

                        if (dt.Rows.Count == 1)
                        {
                            SqlCommand cmd = new SqlCommand(query, con);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                password = rdr.GetString(4).ToString();
                                break;
                            }
                            SendPasswordEmail(txtEmail.Text, password);
                        }
                        else
                        {
                            MessageBox.Show("Email does not exist!");
                            txtEmail.Clear();
                        }
                        con.Close();
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
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
            this.Hide();
        }
    }
}
