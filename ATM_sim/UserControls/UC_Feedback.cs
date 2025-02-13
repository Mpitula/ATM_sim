using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
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
    public partial class UC_Feedback : UserControl
    {
        public UC_Feedback()
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
                    string query = "SELECT * FROM Feedback";
                    //string query = "SELECT * FROM Students";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvFeedback.DataSource = dt;
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

        private void UC_Feedback_Load(object sender, EventArgs e)
        {
            populate();
        }
        private void DeleteRecord(int id)
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM Feedback WHERE FeedbackId = @Id"; // Replace with your table name and ID column
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
                if (dgvFeedback.Rows.Count == 0) // Check if there are any rows in the DataGridView
                {
                    throw new InvalidOperationException("There are no rows available to delete.");
                }

                if (dgvFeedback.SelectedRows.Count == 0) // Check if a row is selected
                {
                    throw new InvalidOperationException("No row is selected. Please select a row to delete.");
                }

                // Get the ID of the selected row
                int selectedRowIndex = dgvFeedback.SelectedRows[0].Index;
                int idToDelete = Convert.ToInt32(dgvFeedback.Rows[selectedRowIndex].Cells["FeedbackId"].Value); // Replace "Id" with your primary key column name

                // Confirm deletion
                var confirmResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    DeleteRecord(idToDelete);
                    dgvFeedback.Rows.RemoveAt(selectedRowIndex); // Remove from DataGridView
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

        private void txtSearchTransaction_TextChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    string query = "SELECT * FROM Feedback WHERE StudentId like '" + "%" + txtSearchTransaction.Text + "%" + "'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvFeedback.DataSource = dt;
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

        private void dgvFeedback_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
