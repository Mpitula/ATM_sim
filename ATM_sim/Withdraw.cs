using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ATM_sim
{
    public partial class Withdraw : Form
    {
        public Withdraw()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        public decimal bal;
        public decimal newBalance;
        public decimal Towithdraw;
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
        private void addTransaction(decimal withDrawAmaount)
        {
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    string query = "INSERT INTO Transactions VALUES('" + "Withdraw" + "','" + withDrawAmaount + "','" + DateTime.Today.ToString() + "','" + LoginPage.stdNum + "')";
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

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void lblBack_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();
        }

        private void btnChangePin_Click(object sender, EventArgs e)
        {
        }

        private void Withdraw_Load(object sender, EventArgs e)
        {
            getBalance();
        }
        private void withdraw()
        {
            if (isvalid())
            {
                try
                {
                    Towithdraw = Convert.ToDecimal(txtWithdrawAmount.Text);
                    newBalance = bal - Towithdraw;
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        string query = "UPDATE Students SET Balance = @balance WHERE studentNum = '" + studentnum + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@balance", newBalance);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Amount Withdrawn Succesfully");
                        txtWithdrawAmount.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Connection failed.");
                    }
                    conn.Close();
                    addTransaction(Towithdraw);
                    getBalance();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
      
        private void lblHelpFunction_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private bool isvalid()
        {
            if (string.IsNullOrEmpty(txtWithdrawAmount.Text))
            {
                MessageBox.Show("Amount is required for your withdrawal.");
                return false;
            }
            if (Convert.ToDecimal(txtWithdrawAmount.Text) > bal)
            {
                MessageBox.Show("Not Enough funds to Withdraw Entered Amount!");
                txtWithdrawAmount.Clear();
                return false;
            }
            if (Convert.ToDecimal(txtWithdrawAmount.Text) < 50)
            {
                MessageBox.Show("Acceptable withdrawal is R50 and above");
                txtWithdrawAmount.Clear();
                return false;
            }
            return true;
        }
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            withdraw();
        }

        private void lblstudentNum_Click(object sender, EventArgs e)
        {

        }

        private void txtWithdrawAmount_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Towithdraw = 100;
            if (Towithdraw > bal)
            {
                MessageBox.Show("Not enough funds to withdraw the selected amount!");
            }
            else
            {
                try
                {
                    newBalance = bal - Towithdraw;
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        string query = "UPDATE Students SET Balance = @balance WHERE studentNum = '" + studentnum + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@balance", newBalance);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Amount Withdrawn Succesfully");
                    }
                    else
                    {
                        MessageBox.Show("Connection failed.");
                    }
                    conn.Close();
                    addTransaction(Towithdraw);
                    getBalance();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            Towithdraw = 50;
            if(Towithdraw > bal)
            {
                MessageBox.Show("Not enough funds to withdraw the selected amount!");
            }
            else
            {
                try
                {
                    newBalance = bal - Towithdraw;
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        string query = "UPDATE Students SET Balance = @balance WHERE studentNum = '" + studentnum + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@balance", newBalance);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Amount Withdrawn Succesfully");
                    }
                    else
                    {
                        MessageBox.Show("Connection failed.");
                    }
                    conn.Close();
                    addTransaction(Towithdraw); 
                    getBalance();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Towithdraw = 500;
            if (Towithdraw > bal)
            {
                MessageBox.Show("Not enough funds to withdraw the selected amount!");
            }
            else
            {
                try
                {
                    newBalance = bal - Towithdraw;
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        string query = "UPDATE Students SET Balance = @balance WHERE studentNum = '" + studentnum + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@balance", newBalance);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Amount Withdrawn Succesfully");
                    }
                    else
                    {
                        MessageBox.Show("Connection failed.");
                    }
                    conn.Close();
                    addTransaction(Towithdraw);
                    getBalance();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Towithdraw = 200;
            if (Towithdraw > bal)
            {
                MessageBox.Show("Not enough funds to withdraw the selected amount!");
            }
            else
            {
                try
                {
                    newBalance = bal - Towithdraw;
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        string query = "UPDATE Students SET Balance = @balance WHERE studentNum = '" + studentnum + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@balance", newBalance);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Amount Withdrawn Succesfully");
                    }
                    else
                    {
                        MessageBox.Show("Connection failed.");
                    }
                    conn.Close();
                    addTransaction(Towithdraw);
                    getBalance();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Towithdraw = 1000;
            if (Towithdraw > bal)
            {
                MessageBox.Show("Not enough funds to withdraw the selected amount!");
            }
            else
            {
                try
                {
                    newBalance = bal - Towithdraw;
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        string query = "UPDATE Students SET Balance = @balance WHERE studentNum = '" + studentnum + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@balance", newBalance);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Amount Withdrawn Succesfully");
                    }
                    else
                    {
                        MessageBox.Show("Connection failed.");
                    }
                    conn.Close();
                    addTransaction(Towithdraw);
                    getBalance();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Towithdraw = 1500;
            if (Towithdraw > bal)
            {
                MessageBox.Show("Not enough funds to withdraw the selected amount!");
            }
            else
            {
                try
                {
                    newBalance = bal - Towithdraw;
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        string query = "UPDATE Students SET Balance = @balance WHERE studentNum = '" + studentnum + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@balance", newBalance);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Amount Withdrawn Succesfully");
                    }
                    else
                    {
                        MessageBox.Show("Connection failed.");
                    }
                    conn.Close();
                    addTransaction(Towithdraw);
                    getBalance();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtWithdrawAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If it's not a digit or control, block the keypress
                e.Handled = true;
            }
        }
    }
}
