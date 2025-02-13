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
using System.Xml.Linq;

namespace ATM_sim.UserControls
{
    public partial class UC_Students : UserControl
    {
        public UC_Students()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        private void loadStudents()
        {
            try
            {

                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    string query = "SELECT s.studentNum, s.FirstName, s.LastName, s.Phone,s.Address, s.Email, s.Status, c.FirstName FROM Students s INNER JOIN Admin c ON s.AdminId = c.adminId";
                    //string query = "SELECT * FROM Students";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvStudents.DataSource = dt;
                    SetColumnHeaders();
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
        private void SetColumnHeaders()
        {
            // Example of setting custom header text for each column
            dgvStudents.Columns["studentNum"].HeaderText = "Student ID";
            dgvStudents.Columns["FirstName"].HeaderText = "Name";
            dgvStudents.Columns["LastName"].HeaderText = "Surname";
            dgvStudents.Columns["FirstName1"].HeaderText = "Admin Name";
            
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
        private void UC_Students_Load(object sender, EventArgs e)
        {
            loadStudents();
            LoadComboBoxItems();
        }
        private bool validate()
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Student number required.");
                return false;
            }
            if (!Regex.IsMatch(txtSearch.Text, @"^\d{8}$"))
            {
                MessageBox.Show("Student number is required and must be a valid 8-digits number.");
                return false;
            }
            return true;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                if (studentExist(Convert.ToInt32(txtSearch.Text)))
                {
                    try
                    {
                        conn.Open();
                        var confirmResult = MessageBox.Show("Are you sure to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo);
                        if (confirmResult == DialogResult.Yes)
                        {
                            if (conn.State == ConnectionState.Open)
                            {
                                string query = "DELETE FROM Students WHERE studentNum = '" + txtSearch.Text + "'";
                                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);

                                dgvStudents.DataSource = dt;
                            }
                            else
                            {
                                MessageBox.Show("Connection failed");
                            }
                        }
                            conn.Close();
                            loadStudents();
                            MessageBox.Show("Student successfully deleted");
                            txtSearch.Clear();
                        
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Student with this student number does not exist.");
                    txtSearch.Clear();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    string query = "SELECT s.studentNum, s.FirstName, s.LastName, s.Phone,s.Address, s.Email, s.Status, c.FirstName FROM Students s INNER JOIN Admin c ON s.AdminId = c.adminId WHERE s.studentNum like '" + "%" + txtSearch.Text + "%" + "'";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvStudents.DataSource = dt;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadComboBoxItems()
        {
            // Add some sample items to the ComboBox
            comboBox1.Items.Add("Active");
            comboBox1.Items.Add("Inactive");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                try
                {
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        string query = "UPDATE Students SET Status = @status, AdminId = @adminId WHERE studentNum = '" + txtSearch.Text + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@status", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@adminId", AdminLogin.adminId);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("Connection failed.");
                    }
                    conn.Close();
                    MessageBox.Show("Status updated successfully");
                    loadStudents();
                    txtSearch.Clear();  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

       
        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
