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

namespace ATM_sim.UserControls
{
    public partial class UC_Transactions : UserControl
    {
        public UC_Transactions()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aussc\OneDrive\Documents\CashlessSystem.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            try
            {
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    string query = "SELECT * FROM Transactions";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvtransaction.DataSource = dt;
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
        private bool validate()
        {
            if (string.IsNullOrEmpty(txtSearchTransaction.Text))
            {
                MessageBox.Show("Student number required.");
                return false;
            }
            if (!Regex.IsMatch(txtSearchTransaction.Text, @"^\d{8}$"))
            {
                MessageBox.Show("Student number is required and must be a valid 8-digits number.");
                return false;
            }
            return true;
        }
        private void UC_Transactions_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            
        }
        
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    string query = "SELECT * FROM Transactions WHERE studentId like '" + "%" + txtSearchTransaction.Text + "%" + "'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvtransaction.DataSource = dt;
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        
        private void dgvtransaction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void SetColumnHeaders()
        {
            // Example of setting custom header text for each column
            dgvtransaction.Columns["TransactionId"].HeaderText = "Transaction ID";
            dgvtransaction.Columns["studentId"].HeaderText = "Student ID";

        }
        private void DeleteRecord(int id)
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM Transactions WHERE TransactionId = @Id"; // Replace with your table name and ID column
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
                if (dgvtransaction.Rows.Count == 0) // Check if there are any rows in the DataGridView
                {
                    throw new InvalidOperationException("There are no rows available to delete.");
                }

                if (dgvtransaction.SelectedRows.Count == 0) // Check if a row is selected
                {
                    throw new InvalidOperationException("No row is selected. Please select a row to delete.");
                }

                // Get the ID of the selected row
                int selectedRowIndex = dgvtransaction.SelectedRows[0].Index;
                int idToDelete = Convert.ToInt32(dgvtransaction.Rows[selectedRowIndex].Cells["TransactionId"].Value); // Replace "Id" with your primary key column name

                // Confirm deletion
                var confirmResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    DeleteRecord(idToDelete);
                    dgvtransaction.Rows.RemoveAt(selectedRowIndex); // Remove from DataGridView
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
    }
}
