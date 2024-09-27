using DE_IDENTIFICATION_TOOL.Models;
using DE_IDENTIFICATION_TOOL.Pythonresponse;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DE_IDENTIFICATION_TOOL.CustomAction;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class ExportDbForm : Form
    {
        private readonly string _projectName;
        private DbtableFormModel _properties;
        public string _tableName;
        private PythonService pythonService;
        private Dictionary<string, string> tableSchemas = new Dictionary<string, string>();
        private Dictionary<string, List<string>> tableColumns = new Dictionary<string, List<string>>();
        private PictureBox picEye;


        //private bool _check;

        public ExportDbForm(string projectName, string tableName)
        {
            InitializeComponent();
            _properties = new DbtableFormModel();
            _projectName = projectName;
            _tableName = tableName;
            pythonService = new PythonService();

            lblForServer.Visible = false;
            txtForServer.Visible = false;
            lblForUserName.Visible = false;
            txtForUsername.Visible = false;
            lblForPassword.Visible = false;
            txtForPassword.Visible = false;

            btnForNext.Enabled = false;
            // Initialize and configure the eye icon
            picEye = new PictureBox
            {
                Size = new System.Drawing.Size(20, 20),
                Location = new System.Drawing.Point(txtForPassword.Width - 25, 3), // Adjust the position
                Cursor = Cursors.Hand,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.visible, // Assuming you have an image resource for the eye icon
                Visible = false // Initially hidden
            };
            txtForPassword.Controls.Add(picEye); // Add the eye icon inside the password box

            // Add event handlers for the eye icon
            picEye.MouseEnter += PicEye_MouseEnter;
            picEye.MouseLeave += PicEye_MouseLeave;
            dbTyped.SelectedIndexChanged += dbTyped_SelectedIndexChanged;

            labelForDatabase.Visible = false;
            cmbDatabases.Visible = false;
            labelForDatabaseTbl.Visible = false;
            cmbTables.Visible = false;

            btnForFinish.Enabled = false;

            // Attach event handlers to text changed events
            txtForServer.TextChanged += new EventHandler(ValidateInputFields);
            txtForUsername.TextChanged += new EventHandler(ValidateInputFields);
            txtForPassword.TextChanged += new EventHandler(ValidateInputFields);
            

            //btnForCancelInJdbcFrm.Click += new EventHandler(btnForCancel_Click); ;


        }


        private void PicEye_MouseEnter(object sender, EventArgs e)
        {
            // Show the password when the mouse enters the eye icon
            txtForPassword.UseSystemPasswordChar = false;
        }

        private void PicEye_MouseLeave(object sender, EventArgs e)
        {
            // Hide the password when the mouse leaves the eye icon
            txtForPassword.UseSystemPasswordChar = true;
        }

        private void btnForCancel_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        public ExportDbForm(string projectName, bool check)
        {
            InitializeComponent();
            _properties = new DbtableFormModel();
            _projectName = projectName;
            _properties.check = check;
            pythonService = new PythonService();

            lblForServer.Visible = false;
            txtForServer.Visible = false;
            lblForUserName.Visible = false;
            txtForUsername.Visible = false;
            lblForPassword.Visible = false;
            txtForPassword.Visible = false;

            btnForNext.Enabled = false;

            labelForDatabase.Visible = false;
            cmbDatabases.Visible = false;
            labelForDatabaseTbl.Visible = false;
            cmbTables.Visible = false;

            btnForFinish.Enabled = false;
            ComboBoxHelper.PreventScroll(this.dbTyped);

            // Attach event handlers to text changed events
            txtForServer.TextChanged += new EventHandler(ValidateInputFields);
            txtForUsername.TextChanged += new EventHandler(ValidateInputFields);
            txtForPassword.TextChanged += new EventHandler(ValidateInputFields);
        }

        private void ValidateInputFields(object sender, EventArgs e)
        {
            btnForNext.Enabled = !string.IsNullOrEmpty(txtForServer.Text) &&
                                 !string.IsNullOrEmpty(txtForUsername.Text) &&
                                 !string.IsNullOrEmpty(txtForPassword.Text);
        }

        private void dbTyped_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = dbTyped.SelectedItem.ToString();

            if (selectedItem == "SQL")
            {
                lblForServer.Visible = true;
                txtForServer.Visible = true;
                lblForUserName.Visible = true;
                txtForUsername.Visible = true;
                lblForPassword.Visible = true;
                txtForPassword.Visible = true;
                picEye.Visible = !string.IsNullOrEmpty(txtForPassword.Text);
            }
            else if (selectedItem == "Oracle")
            {
                // Handle Oracle controls visibility
            }
            else
            {
                // Handle other database types if necessary
            }
        }

        private void btnForNext_Click(object sender, EventArgs e)
        {
            string connectionString = $"server={this.txtForServer.Text};" +
                                      $"user id={this.txtForUsername.Text};" +
                                      $"password={this.txtForPassword.Text};" +
                                      $"connection timeout=30";

            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                try
                {
                    myConnection.Open();
                    string query = "SELECT name FROM sys.databases";
                    using (SqlCommand cmd = new SqlCommand(query, myConnection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbDatabases.Items.Clear();
                            while (reader.Read())
                            {
                                cmbDatabases.Items.Add(reader["name"].ToString());
                            }
                        }
                    }
                    cmbDatabases.Visible = true;
                    labelForDatabase.Visible = true;
                    btnForNext.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)


        {
            _properties.dbName = cmbDatabases.SelectedItem.ToString();

            if (_properties.check != true)
            {
                UpdateFinishButtonVisibility();
                string selectedDatabase = cmbDatabases.SelectedItem.ToString();
                LoadTables(selectedDatabase);



            }
            else
            {
                btnForFinish.Enabled = !string.IsNullOrEmpty(_properties.dbName);

            }

        }

        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            _properties.tableName = cmbTables.SelectedItem.ToString();
            UpdateFinishButtonVisibility();
        }

        private void UpdateFinishButtonVisibility()



        {
            if (_properties.check != true)
            {
                btnForFinish.Enabled = !string.IsNullOrEmpty(_properties.dbName) &&
                                       !string.IsNullOrEmpty(_properties.tableName);
            }

        }

        private void LoadTables(string database)
        {
            string connectionStringWithDatabase = $"server={this.txtForServer.Text};" +
                                                  $"user id={this.txtForUsername.Text};" +
                                                  $"password={this.txtForPassword.Text};" +
                                                  $"database={database};" +
                                                  $"connection timeout=30";

            using (SqlConnection myConnection = new SqlConnection(connectionStringWithDatabase))
            {
                try
                {
                    myConnection.Open();
                    string query = @"
                        SELECT 
                            TABLE_SCHEMA, 
                            TABLE_NAME, 
                            COLUMN_NAME 
                        FROM 
                            INFORMATION_SCHEMA.COLUMNS 
                        WHERE 
                            TABLE_NAME IN (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE')";

                    using (SqlCommand cmd = new SqlCommand(query, myConnection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbTables.Items.Clear();
                            tableSchemas.Clear();
                            tableColumns.Clear();

                            while (reader.Read())
                            {
                                string schema = reader["TABLE_SCHEMA"].ToString();
                                string tableName = reader["TABLE_NAME"].ToString();
                                string columnName = reader["COLUMN_NAME"].ToString();

                                string fullTableName = $"{schema}.{tableName}";

                                if (!cmbTables.Items.Contains(fullTableName))
                                {
                                    cmbTables.Items.Add(fullTableName);
                                    tableSchemas[tableName] = schema;
                                }

                                if (!tableColumns.ContainsKey(fullTableName))
                                {
                                    tableColumns[fullTableName] = new List<string>();
                                }

                                tableColumns[fullTableName].Add(columnName);
                            }
                            cmbTables.Visible = true;
                            labelForDatabaseTbl.Visible = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load tables: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }







        static void ValidateTables(string sqliteDbPath, string projectName)
        {
            // Connect to SQLite database
            using (var sqliteConn = new SQLiteConnection($"Data Source={sqliteDbPath};Version=3;"))
            {
                sqliteConn.Open();

                // Fetch all table names from SQLite
                var sqliteTables = new List<string>();
                using (var cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table';", sqliteConn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sqliteTables.Add(reader.GetString(0));
                    }
                }

                // Validation: Check if there are no tables in the project
                if (sqliteTables.Count == 0)
                {
                    throw new Exception($"There are no tables in the project '{projectName}'");
                }

                // Separate table names into two lists
                var nonDeidentifiedTables = new List<string>();
                var deidentifiedTables = new List<string>();

                foreach (var table in sqliteTables)
                {
                    if (table.StartsWith("de_identified_"))
                    {
                        deidentifiedTables.Add(table);
                    }
                    else
                    {
                        nonDeidentifiedTables.Add(table);
                    }
                }

                // Check if all non-deidentified table names are present in the deidentified tables list
                foreach (var table in nonDeidentifiedTables)
                {
                    if (!deidentifiedTables.Contains($"de_identified_{table}"))
                    {
                        throw new Exception($"De-identified version of table {table} is missing");
                    }
                }

                Console.WriteLine("Validation passed: All tables are present and de-identified tables are matched.");
            }
        }






        private void btnForFinish_Click(object sender, EventArgs e)
        {

            if (_properties.check == true)
            {
                string projectName = _projectName;

                string server = txtForServer.Text;
                string UserId = txtForUsername.Text;
                string password = txtForPassword.Text;
                string database = cmbDatabases.Text;


                string username = Environment.UserName;
                string toolPath = $@"C:\Users\{username}\AppData\Roaming\DeidentificationTool";
                string projectPath = System.IO.Path.Combine(toolPath, projectName);
                string tablesDataPath = System.IO.Path.Combine(projectPath, "TablesData");
                string dbFilePath = System.IO.Path.Combine(tablesDataPath, "Data.db");

                ValidateTables(dbFilePath, projectName);


                string savePythonScriptName = "EportAllConnection.py";
                string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                string savePythonScriptPath = Path.Combine(projectRootDirectory, savePythonScriptName);

                

                // Send data to Python script and capture the response
                string savePythonResponse = pythonService.SendSqlExportAllDataToPython(server, database, password, UserId, projectName, savePythonScriptPath);

                // Handle the response from the Python script
                if (savePythonResponse.ToLower().Contains("success"))
                {
                    MessageBox.Show("Data saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Schemas of de-identified tables and destination tables are not matching", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method if it fails to save data
                }



            }
            else
            {
                string projectName = _projectName;
                string mainTableName = _tableName;

                string server = txtForServer.Text;
                string UserId = txtForUsername.Text;
                string password = txtForPassword.Text;
                string database = cmbDatabases.Text;
                string selectedtable = cmbTables.Text;
                string tableName = selectedtable.Split('.')[1]; // Extract the table name
                string schemaName = tableSchemas[tableName];

                // Define the Python script path for the checkbox-checked scenario
                string savePythonScriptName = "ExportSqlConnection.py";
                string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                string savePythonScriptPath = Path.Combine(projectRootDirectory, savePythonScriptName);

                // Send data to Python script and capture the response
                string savePythonResponse = pythonService.SendSqlExportDataToPython(server, database, password, UserId, schemaName, projectName, mainTableName, tableName, savePythonScriptPath);

                // Handle the response from the Python script
                if (savePythonResponse.ToLower().Contains("success"))
                {
                    MessageBox.Show("Data saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Schema of de-identified table and destination table are not matching", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method if it fails to save data
                }

            }
            // Finish button logic
        }

        private void txtForPassword_TextChanged(object sender, EventArgs e)
        {
            _properties.password = txtForPassword.Text;
            UpdateFinishButtonVisibility();
        }
    }
}