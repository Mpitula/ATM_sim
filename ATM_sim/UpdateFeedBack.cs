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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ATM_sim
{
    public partial class UpdateFeedBack : Form
    {
        public UpdateFeedBack()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        public static int rating;
        private void populate()
        {
            try
            {

                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    string query = "SELECT Comments, Rating FROM Feedback WHERE StudentId = '" + LoginPage.stdNum + "'";
                    //string query = "SELECT * FROM Students";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                    //SetColumnHeaders();
                }
                else
                {
                    MessageBox.Show("Connection failed");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void UpdateFeedBack_Load(object sender, EventArgs e)
        {
            populate();
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox5.Image = Resources.white_star;

            pictureBox1.Image = Resources.yellow_star;
            pictureBox2.Image = Resources.yellow_star;
            pictureBox3.Image = Resources.yellow_star;
            pictureBox4.Image = Resources.yellow_star;
            rating = 4;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.yellow_star;
            pictureBox2.Image = Resources.yellow_star;
            pictureBox3.Image = Resources.yellow_star;
            pictureBox4.Image = Resources.yellow_star;
            pictureBox5.Image = Resources.yellow_star;
            rating = 5;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    string query = "UPDATE Feedback SET Comments = @com, Rating = @rate, Date = @date WHERE StudentId = '" + LoginPage.stdNum + "'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@com", txtcomment.Text);
                    cmd.Parameters.AddWithValue("@rate", rating);
                    cmd.Parameters.AddWithValue("@date", DateTime.Today.ToString());
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Connection failed.");
                }
                conn.Close();
                populate();
                MessageBox.Show("Feedback updated successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void lblBack_Click(object sender, EventArgs e)
        {
            Feedbak feedbak = new Feedbak();
            feedbak.Show();
            this.Hide();
        }

        private void lblHelpFunction_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
