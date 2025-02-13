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
    public partial class UC_Admin : UserControl
    {
        public UC_Admin()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void SetColumnHeaders()
        {
            // Example of setting custom header text for each column
            dgvAdmin.Columns["adminId"].HeaderText = "Admin ID";
            dgvAdmin.Columns["FirstName"].HeaderText = "Name";
            dgvAdmin.Columns["LastName"].HeaderText = "Surname";
            dgvAdmin.Columns["Email"].HeaderText = "Email";


        }
        private void loadTransaction()
        {
            try
            {
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    string query = "SELECT adminId, FirstName, LastName, Email FROM Admin";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvAdmin.DataSource = dt;
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void UC_Admin_Load(object sender, EventArgs e)
        {
            loadTransaction();
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUpdatepassword.Text))
            {
                MessageBox.Show("Password is required.");
            }
            else if (!Regex.IsMatch(txtUpdatepassword.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%&*]).{8,}$"))
            {
                MessageBox.Show("Password must be 8 characters long and include at leats one uppercase, one lowercase, and one special character(!, @, #, $, %, &, *).");
            }
            else
            {
                try
                {
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        string query = "UPDATE Admin SET Password = @pwd WHERE Email = '" + AdminLogin.AdminEmail + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@pwd", txtUpdatepassword.Text);
                        cmd.ExecuteNonQuery();



                        MessageBox.Show("Updated Succesfully");
                        txtUpdatepassword.Clear();
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
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void DeleteRecord(int id)
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM Admin WHERE adminId = @Id"; // Replace with your table name and ID column
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message);
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Succesfully deleted!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAdmin.Rows.Count == 0) // Check if there are any rows in the DataGridView
                {
                    throw new InvalidOperationException("There are no rows available to delete.");
                }

                if (dgvAdmin.SelectedRows.Count == 0) // Check if a row is selected
                {
                    throw new InvalidOperationException("No row is selected. Please select a row to delete.");
                }

                // Get the ID of the selected row
                int selectedRowIndex = dgvAdmin.SelectedRows[0].Index;
                int idToDelete = Convert.ToInt32(dgvAdmin.Rows[selectedRowIndex].Cells["adminId"].Value); // Replace "Id" with your primary key column name

                // Confirm deletion
                var confirmResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    DeleteRecord(idToDelete);
                    dgvAdmin.Rows.RemoveAt(selectedRowIndex); // Remove from DataGridView
                }
            }
            catch (InvalidOperationException ex) // Custom exception handling for no row available or selected
            {
                MessageBox.Show(ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) // General exception handling for unexpected errors
            {
                MessageBox.Show("An error occurred while attempting to delete the row: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    string query = "SELECT adminId, FirstName, LastName, Email FROM Admin WHERE Email like '" + "%" + txtSearchEmail.Text + "%" + "'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvAdmin.DataSource = dt;
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

        private void txtadminemail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
