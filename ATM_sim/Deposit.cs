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
using System.Xml.Linq;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ATM_sim
{
    public partial class Deposit : Form
    {
        public Deposit()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        public decimal bal;
        public decimal newBalance;
        public string studentnum = LoginPage.stdNum;

        private void getBalance()
        {
            try
            {
                conn.Open();

                if (conn.State == ConnectionState.Open) //check if connection is opened succefully 
                {
                    string query = "SELECT * FROM Students WHERE studentNum = '" + studentnum + "'";

                    SqlDataAdapter dpt = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    dpt.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            bal = rdr.GetDecimal(8);
                            break;
                        }
                        lblBalance.Text = "R " + Math.Round(bal, 2).ToString();
                    }
                    else
                    {
                        MessageBox.Show("Connection failed");
                    }
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void addTransaction(decimal DepositAmaount)
        {
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    string query = "INSERT INTO Transactions VALUES('" + "Deposit" + "','" + DepositAmaount + "','" + DateTime.Today.ToString() + "','" + LoginPage.stdNum + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                }
                else
                {
                    MessageBox.Show("Connection failed.");
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lblHelpFunction_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit(); 
        }

        private void Deposit_Load(object sender, EventArgs e)
        {
            getBalance();
        }
        private bool IsValidDate(string input)
        {
            bool isValid = true;
            if (input == string.Empty)
            {
                MessageBox.Show("Enter a valid amount");
                txtDepositAmount.Clear();
                isValid = false;
            }
            return isValid;
        }
        private void lblLogout_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidDate(txtDepositAmount.Text))
                {
                    decimal DepositAmount = Convert.ToDecimal(txtDepositAmount.Text);
                    newBalance = bal + DepositAmount;
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        string query = "UPDATE Students SET Balance = @balance WHERE studentNum = '" + studentnum + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@balance", newBalance);
                        cmd.ExecuteNonQuery();

                    }
                    else
                    {
                        MessageBox.Show("Connection failed.");
                    }
                    conn.Close();
                    addTransaction(DepositAmount);
                    getBalance();
                    MessageBox.Show("Amount Deposited Succesfully");
                    txtDepositAmount.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDepositAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If it's not a digit or control, block the keypress
                e.Handled = true;
            }
        }

        private void txtDepositAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
