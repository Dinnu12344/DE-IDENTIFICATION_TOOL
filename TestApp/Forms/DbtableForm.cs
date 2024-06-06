using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class DbtableForm : Form
    {
        private string _connectionString;

        public DbtableForm(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            LoadDatabases();
        }

        private void LoadDatabases()
        {
            using (SqlConnection myConnection = new SqlConnection(_connectionString))
            {
                try
                {
                    myConnection.Open();

                    // Query to get all database names
                    string query = "SELECT name FROM sys.databases";

                    using (SqlCommand cmd = new SqlCommand(query, myConnection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbDatabases.Items.Add(reader["name"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load databases: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDatabase = cmbDatabases.SelectedItem.ToString();
            LoadTables(selectedDatabase);
        }

        private void LoadTables(string database)
        {
            string connectionStringWithDatabase = $"{_connectionString};database={database}";

            using (SqlConnection myConnection = new SqlConnection(connectionStringWithDatabase))
            {
                try
                {
                    myConnection.Open();

                    // Query to get all table names
                    string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

                    using (SqlCommand cmd = new SqlCommand(query, myConnection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbTables.Items.Clear();
                            while (reader.Read())
                            {
                                cmbTables.Items.Add(reader["TABLE_NAME"].ToString());
                            }
                            cmbTables.Show(); // Show the tables dropdown once it's populated
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load tables: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnForAddingColumns_Click(object sender, EventArgs e)
        {
            AddColumns();
        }

        private void AddColumns()
        {

        }
    }
}
