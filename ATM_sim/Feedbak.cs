using ATM_sim.Properties;
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

namespace ATM_sim
{
    public partial class Feedbak : Form
    {
        public Feedbak()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        private int rating;
        private void Feedbak_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pb_str3_Click(object sender, EventArgs e)
        {

        }

        private void pb_str2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Resources.white_star;
            pictureBox3.Image = Resources.white_star;
            pictureBox4.Image = Resources.white_star;
            pictureBox5.Image = Resources.white_star;

            pictureBox1.Image = Resources.yellow_star;
            rating = 1;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = Resources.white_star;
            pictureBox4.Image = Resources.white_star;
            pictureBox5.Image = Resources.white_star;

            pictureBox1.Image = Resources.yellow_star;
            pictureBox2.Image = Resources.yellow_star;
            rating = 2;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox4.Image = Resources.white_star;
            pictureBox5.Image = Resources.white_star;

            pictureBox1.Image = Resources.yellow_star;
            pictureBox2.Image = Resources.yellow_star;
            pictureBox3.Image = Resources.yellow_star;
            rating = 3;
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            pictureBox5.Image = Resources.white_star;

            pictureBox1.Image = Resources.yellow_star;
            pictureBox2.Image = Resources.yellow_star;
            pictureBox3.Image = Resources.yellow_star;
            pictureBox4.Image = Resources.yellow_star;
            rating = 4;
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.yellow_star;
            pictureBox2.Image = Resources.yellow_star;
            pictureBox3.Image = Resources.yellow_star;
            pictureBox4.Image = Resources.yellow_star;
            pictureBox5.Image = Resources.yellow_star;
            rating = 5;
        }

        private void Feedback_Click(object sender, EventArgs e)
        {

        }

        private  bool feedbackExist()
        {
            bool exist = false;
            int id = 0;
            int studentNum = Convert.ToInt32(LoginPage.stdNum);
            try
            {
                conn.Open();

                if (conn.State == ConnectionState.Open) //check if connection is opened succefully 
                {
                    string query = "SELECT * FROM FeedBack WHERE StudentId = '" + studentNum  + "'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        id = rdr.GetInt32(1);
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
        private void lblHelpFunction_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private bool validate()
        {
            if (rating == 0)
            {
                MessageBox.Show(String.Format("Rating required."));
                return false;
            }
            if (txtcomment.Text == "")
            {
                MessageBox.Show(String.Format("Comment required to submit."));
                return false;
            }
            return true;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                try
                {
                    if (!feedbackExist())
                    {
                        conn.Open();
                        if (conn.State == ConnectionState.Open)
                        {
                            string query = "INSERT INTO Feedback VALUES('" + LoginPage.stdNum + "','" + txtcomment.Text + "','" + rating + "','" + DateTime.Today.ToString() + "')";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show(String.Format("Feedback submitted"));
                        }
                        else
                        {
                            MessageBox.Show("Connection failed.");
                        }
                        conn.Close();
                    }
                    else
                    {
                        var confirmResult = MessageBox.Show("Feedback already submitted, would you like to update your response?", "Confirm Update", MessageBoxButtons.YesNo);
                        if (confirmResult == DialogResult.Yes)
                        {
                            UpdateFeedBack updateFeedBack = new UpdateFeedBack();
                            updateFeedBack.Show();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void lblBack_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();
        }
    }
}
