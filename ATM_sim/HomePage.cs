using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace ATM_sim
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();

        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        public static string studentNum, name, surname;
        public static decimal balance;
        
        
        private void lblLogout_Click(object sender, EventArgs e)
        {
            LoginPage log = new LoginPage();    
            log.Show(); 
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            studentNum = LoginPage.stdNum;
            name = LoginPage.name;
            surname = LoginPage.surname;
            balance = LoginPage.balance;
            lblDetails.Text = name + " " + surname + " " + studentNum;
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            Deposit deposit = new Deposit();
            deposit.Show();
            this.Hide();
        }

        private void btnChangePin_Click(object sender, EventArgs e)
        {
            ChangePin changePin = new ChangePin();
            changePin.Show();
            this.Hide();

        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            Withdraw withdraw = new Withdraw();
            withdraw.Show();
            this.Hide();    
        }

        private void btnQuikCash_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Balance bala = new Balance();
            bala.Show();
            this.Hide();
        }

        private void lblDetails_Click(object sender, EventArgs e)
        {

        }

        private void lblHelpFunction_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Feedbak feedbak = new Feedbak();
            feedbak.Show();
            this.Hide();
        }

        private void btnMiniStatement_Click(object sender, EventArgs e)
        {
            MiniStatement mini = new MiniStatement();
            mini.Show();
            this.Hide();
        }

        private void lblLogout_Click_1(object sender, EventArgs e)
        {
            LoginPage log = new LoginPage();
            log.Show();
            this.Hide();
        }
    }
}
