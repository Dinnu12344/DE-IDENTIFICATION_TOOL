using DE_IDENTIFICATION_TOOL.Models;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using DE_IDENTIFICATION_TOOL.Pythonresponse;
using System.IO;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class DbtableForm : Form
    {
        private string _connectionString;
        private readonly string labelName;
        private PythonService pythonService;

        private TreeNode _selectedNode;
        private List<ProjectData> _projectData;
        private HomeForm _homeForm;
        public string EnteredText { get; set; }

        public DbtableForm(string connectionString, TreeNode selectednode, List<ProjectData> projectData, HomeForm homeForm)
        {
            InitializeComponent();
            pythonService = new PythonService();
            _connectionString = connectionString;
            _selectedNode = selectednode;
            _homeForm = homeForm;
            _projectData = projectData;
            LoadDatabases();
            this.labelName = labelName;
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

        private Dictionary<string, string> tableSchemas = new Dictionary<string, string>();

        private void LoadTables(string database)
        {
            string connectionStringWithDatabase = $"{_connectionString};database={database}";

            using (SqlConnection myConnection = new SqlConnection(connectionStringWithDatabase))
            {
                try
                {
                    myConnection.Open();

                    // Query to get all table names and schemas
                    string query = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

                    using (SqlCommand cmd = new SqlCommand(query, myConnection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbTables.Items.Clear();
                            tableSchemas.Clear();
                            while (reader.Read())
                            {
                                string schema = reader["TABLE_SCHEMA"].ToString();
                                string tableName = reader["TABLE_NAME"].ToString();
                                cmbTables.Items.Add($"{schema}.{tableName}");
                                tableSchemas[tableName] = schema;
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
        //private void LoadTables(string database)
        //{
        //    string connectionStringWithDatabase = $"{_connectionString};database={database}";

        //    using (SqlConnection myConnection = new SqlConnection(connectionStringWithDatabase))
        //    {
        //        try
        //        {
        //            myConnection.Open();

        //            // Query to get all table names
        //            string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

        //            using (SqlCommand cmd = new SqlCommand(query, myConnection))
        //            {
        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    cmbTables.Items.Clear();
        //                    while (reader.Read())
        //                    {
        //                        cmbTables.Items.Add(reader["TABLE_NAME"].ToString());
        //                    }
        //                    cmbTables.Show(); // Show the tables dropdown once it's populated
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Failed to load tables: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        private void btnForAddingColumns_Click(object sender, EventArgs e)
        {
            AddColumns();
        }

        private void AddColumns()
        {

        }

        private void btnForFinish_Click(object sender, EventArgs e)
        {
            string projectName = _selectedNode.Text;
            string TableName = cmbTables.Text;
            string tableName = TableName.Split('.')[1]; // Extract the table name
            string schemaName = tableSchemas[tableName]; // Get the schema name from the dictionary
            string DatabaseName = cmbDatabases.Text;
            string connectionString = _connectionString;
            string Enterno = txtForNoofColumns.Text;

            string serverPattern = @"server\s*=\s*([^;]+)";
            string userIdPattern = @"user\s*id\s*=\s*([^;]+)";
            string passwordPattern = @"password\s*=\s*([^;]+)";
            string timeoutPattern = @"connection\s*timeout\s*=\s*([^;]+)";

            string server = Regex.Match(connectionString, serverPattern, RegexOptions.IgnoreCase).Groups[1].Value;
            string userId = Regex.Match(connectionString, userIdPattern, RegexOptions.IgnoreCase).Groups[1].Value;
            string password = Regex.Match(connectionString, passwordPattern, RegexOptions.IgnoreCase).Groups[1].Value;
            string connectionTimeout = Regex.Match(connectionString, timeoutPattern, RegexOptions.IgnoreCase).Groups[1].Value;

            //MessageBox.Show("Server: " + server);
            //MessageBox.Show("User ID: " + userId);
            //MessageBox.Show("Password: " + password);
            //MessageBox.Show("Connection Timeout: " + connectionTimeout);
            //MessageBox.Show("Project name is: " + projectName + " and table name is: " + tableName);

            string pythonScriptName = "ImportSqlConnection.py";
            string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
            string pythonScriptPath = Path.Combine(projectRootDirectory, "PythonScripts", pythonScriptName);

            //string pythonScriptPath = @"E:\DE-IDENTIFICATION TOOL\DE_IDENTIFICATION_TOOL\TestApp\PythonScripts\ImportSqlConnection.py";
            string pythonResponse = pythonService.SendSqlDataToPython(server, DatabaseName, password, userId, projectName, Enterno, tableName, schemaName, pythonScriptPath);

            if (pythonResponse.ToLower().Contains("success"))
            {
                if (_selectedNode == null)
                {
                    MessageBox.Show("Selected node is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_projectData == null)
                {
                    MessageBox.Show("Project data is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_homeForm == null)
                {
                    MessageBox.Show("Home form reference is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Add the table to the selected node in the TreeView
                TreeNode tableNode = new TreeNode(tableName);
                _selectedNode.Nodes.Add(tableNode);
                _selectedNode.Expand();

                var project = _projectData.Find(p => p.Name == _selectedNode.Text);
                if (project != null)
                {
                    project.Tables.Add(tableName);
                    _homeForm.SaveProjectData();
                }

                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
            else
            {
                MessageBox.Show("The CSV file is not valid. Error: " + pythonResponse, "Error");
            }
        }
    }
}
