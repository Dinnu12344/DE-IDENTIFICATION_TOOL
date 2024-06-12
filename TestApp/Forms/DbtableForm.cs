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
using System.Linq;
using static System.Windows.Forms.AxHost;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class DbtableForm : Form
    {
        //private string _connectionString;
        //private string _projectName;
        //private readonly string labelName;
        //private PythonService pythonService;

        //private TreeNode _selectedNode;
        //private List<ProjectData> _projectData;
        //private HomeForm _homeForm;
        //public string EnteredText { get; set; }

        //// List to keep track of dynamically added controls
        //private List<ComboBox> existingTableCombos = new List<ComboBox>();
        //private List<ComboBox> keyCombos = new List<ComboBox>();
        //private List<TextBox> sourceTableCombos = new List<TextBox>();
        //private List<ComboBox> sourceKeyCombos = new List<ComboBox>();
        //List<CheckBox> selectedCheck = new List<CheckBox>();

        //private Button btnNew;
        //private Button btnDelete;

        //public DbtableForm(string connectionString, TreeNode selectednode, List<ProjectData> projectData, HomeForm homeForm,string projectName)
        //{
        //    InitializeComponent();
        //    pythonService = new PythonService();
        //    _connectionString = connectionString;
        //    _projectName = projectName;
        //    _selectedNode = selectednode;
        //    _homeForm = homeForm;
        //    _projectData = projectData;
        //    LoadDatabases();
        //    this.labelName = labelName;

        //    // Initialize New and Delete buttons
        //    btnNew = new Button { Text = "+ New", Visible = false };
        //    btnNew.Click += BtnNew_Click;
        //    Controls.Add(btnNew);

        //    btnDelete = new Button { Text = "Delete", Visible = false };
        //    btnDelete.Click += BtnDelete_Click;
        //    Controls.Add(btnDelete);
        //}

        private readonly DbtableFormModel _properties;
        private PythonService pythonService;
        private Button btnNew;
        private Button btnDelete;

        public DbtableForm(DbtableFormModel properties)
        {
            InitializeComponent();
            pythonService = new PythonService();
            _properties = properties;
            LoadDatabases();

            // Initialize New and Delete buttons
            btnNew = new Button { Text = "+ New", Visible = false };
            btnNew.Click += BtnNew_Click;
            Controls.Add(btnNew);

            btnDelete = new Button { Text = "Delete", Visible = false };
            btnDelete.Click += BtnDelete_Click;
            Controls.Add(btnDelete);
        }

        private void LoadDatabases()
        {
            string connectionString = _properties.ConnectionString;
            using (SqlConnection myConnection = new SqlConnection(connectionString))
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
        private Dictionary<string, List<string>> tableColumns = new Dictionary<string, List<string>>();
        private void LoadTables(string database)
        {
            string connectionStringWithDatabase = $"{_properties.ConnectionString};database={database}";

            using (SqlConnection myConnection = new SqlConnection(connectionStringWithDatabase))
            {
                try
                {
                    myConnection.Open();

                    // Query to get all table names, schemas, and columns
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
                            tableColumns.Clear(); // Initialize tableColumns dictionary

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


        private void btnForFinish_Click(object sender, EventArgs e)
        {
            string projectName = _properties.SelectedNode.Text;
            string TableName = cmbTables.Text;
            string tableName = TableName.Split('.')[1]; // Extract the table name
            string schemaName = tableSchemas[tableName]; // Get the schema name from the dictionary
            string DatabaseName = cmbDatabases.Text;
            string connectionString = _properties.ConnectionString;
            string Enterno = txtForNoofColumns.Text;

            string serverPattern = @"server\s*=\s*([^;]+)";
            string userIdPattern = @"user\s*id\s*=\s*([^;]+)";
            string passwordPattern = @"password\s*=\s*([^;]+)";
            string timeoutPattern = @"connection\s*timeout\s*=\s*([^;]+)";

            string server = Regex.Match(connectionString, serverPattern, RegexOptions.IgnoreCase).Groups[1].Value;
            string userId = Regex.Match(connectionString, userIdPattern, RegexOptions.IgnoreCase).Groups[1].Value;
            string password = Regex.Match(connectionString, passwordPattern, RegexOptions.IgnoreCase).Groups[1].Value;
            string connectionTimeout = Regex.Match(connectionString, timeoutPattern, RegexOptions.IgnoreCase).Groups[1].Value;

            string pythonScriptName = "ImportSqlConnection.py";
            string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
            string pythonScriptPath = Path.Combine(projectRootDirectory, "PythonScripts", pythonScriptName);

            string pythonResponse = pythonService.SendSqlDataToPython(server, DatabaseName, password, userId, projectName, Enterno, tableName, schemaName, pythonScriptPath);

            if (pythonResponse.ToLower().Contains("success"))
            {
                if (_properties.SelectedNode == null)
                {
                    MessageBox.Show("Selected node is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_properties.ProjectData == null)
                {
                    MessageBox.Show("Project data is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_properties.HomeForm == null)
                {
                    MessageBox.Show("Home form reference is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Add the table to the selected node in the TreeView
                TreeNode tableNode = new TreeNode(tableName);
                _properties.SelectedNode.Nodes.Add(tableNode);
                _properties.SelectedNode.Expand();

                var project = _properties.ProjectData.Find(p => p.Name == _properties.SelectedNode.Text);
                if (project != null)
                {
                    project.Tables.Add(tableName);
                    _properties.HomeForm.SaveProjectData();
                }

                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
            else
            {
                MessageBox.Show("The CSV file is not valid. Error: " + pythonResponse, "Error");
            }
        }

        private void checkBoxforPullreleateddata_CheckedChanged_1(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Checked)
            {
                string pythonScriptName = ".py";
                string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                string pythonScriptPath = Path.Combine(projectRootDirectory, "PythonScripts", pythonScriptName);

                string response = ""; // Call your method to get the response here

                // Show buttons and headers
                ShowNewAndDeleteButtons();
                ShowHeadersAndDropDowns(response);
            }
            else
            {
                HideNewAndDeleteButtons();
                ClearDynamicControls();
            }
        }

        private void ClearDynamicControls()
        {
            // Remove all dynamically added controls
            foreach (var control in panelForPullreleatedData.Controls.OfType<ComboBox>().ToList())
            {
                panelForPullreleatedData.Controls.Remove(control);
            }

            // Clear the lists holding references to the dynamic controls
            _properties.ExistingTableCombos.Clear();
            _properties.KeyCombos.Clear();
            _properties.SourceTableCombos.Clear();
            _properties.SourceKeyCombos.Clear();
        }

        private void ShowNewAndDeleteButtons()
        {
            btnNew.Location = new Point(22, 14);
            btnNew.Visible = true;
            panelForPullreleatedData.Controls.Add(btnNew);

            btnDelete.Location = new Point(154, 14);
            btnDelete.Visible = true;
            panelForPullreleatedData.Controls.Add(btnDelete);
        }

        private void HideNewAndDeleteButtons()
        {
            btnNew.Visible = false;
            btnDelete.Visible = false;
        }

        private void ShowHeadersAndDropDowns(string response)
        {
            string projectName = _properties.ProjectName;

            // Get the base directory of the currently executing application domain
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Construct the dynamic path to the JSON file
            string jsonFilePath = Path.Combine(baseDirectory, "projectData.json");

            // Get the list of tables from JSON file
            List<string> existingTables = GetAllTablesFromJson(jsonFilePath, projectName);

            List<string> keys = new List<string> { "Key1", "Key2" };
            string sourceColumn = GetSourceColumns();
            List<string> sourceTblKey = tableColumns.ContainsKey(cmbTables.Text) ? tableColumns[cmbTables.Text] : new List<string>();

            AddHeaderLabels();

            // Use sourceColumns here
            AddDropDowns(existingTables, keys, sourceColumn, sourceTblKey);
        }


        //private List<string> GetAllTablesFromJson(string filePath,string _projectName)
        //{
        //    try
        //    {
        //        var jsonData = File.ReadAllText(filePath);
        //        var projectDataList = JsonConvert.DeserializeObject<List<ProjectData>>(jsonData);

        //        // Extract all tables from all projects into a single list
        //        var allTables = projectDataList.SelectMany(p => p.Tables).ToList();
        //        return allTables;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Failed to load tables from JSON: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return new List<string>();
        //    }
        //}
        private List<string> GetAllTablesFromJson(string filePath, string projectName)
        {
            try
            {
                var jsonData = File.ReadAllText(filePath);
                var projectDataList = JsonConvert.DeserializeObject<List<ProjectData>>(jsonData);

                // Find the project with the specified name and return its tables
                var project = projectDataList.FirstOrDefault(p => p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase));
                if (project != null)
                {
                    return project.Tables;
                }
                else
                {
                    MessageBox.Show($"Project '{projectName}' not found in JSON.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load tables from JSON: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<string>();
            }
        }

        private string GetSourceColumns()
        {
            string TableName = cmbTables.Text;
            string tableName = "";

            // Check if the TableName contains a dot and split accordingly
            if (TableName.Contains('.'))
            {
                tableName = TableName.Split('.')[1];
            }
            else
            {
                tableName = TableName; // Fallback in case there is no dot
            }

            // Return the list containing the table name
            return tableName;
        }

        private void AddHeaderLabels()
        {
            Label lblSelect = new Label
            {
                Text = "Select",
                Location = new Point(35, 83)
            };
            panelForPullreleatedData.Controls.Add(lblSelect);

            Label lblExistingTable = new Label
            {
                Text = "Existing Table",
                Location = new Point(142, 83)
            };
            panelForPullreleatedData.Controls.Add(lblExistingTable);

            Label lblKey = new Label
            {
                Text = "Key",
                Location = new Point(311, 83)
            };
            panelForPullreleatedData.Controls.Add(lblKey);

            Label lblSourceTable = new Label
            {
                Text = "Source Table",
                Location = new Point(478, 83)
            };
            panelForPullreleatedData.Controls.Add(lblSourceTable);

            Label lblSourceKey = new Label
            {
                Text = "Key",
                Location = new Point(682, 83)
            };
            panelForPullreleatedData.Controls.Add(lblSourceKey);
        }


        private void AddDropDowns(List<string> existingTables, List<string> keys, string sourceColumns, List<string> sourceTblKey)
        {
            int yOffset = 30;
            int startPositionY = 123;

            CheckBox selectCheckbox = new CheckBox
            {
                Location = new Point(47, startPositionY),
                Size = new Size(18, 17),
                Text = "" // or any relevant text for your checkbox
            };

            //ComboBox cbExistingTable = new ComboBox
            //{
            //    Location = new Point(142, startPositionY),
            //    Size = new Size(100, 22)
            //};
            //cbExistingTable.Items.AddRange(existingTables.ToArray());
            ComboBox cbExistingTable = new ComboBox
            {
                Location = new Point(142, startPositionY),
                Size = new Size(100, 22),
                MaxDropDownItems = 10 // This will show a scrollbar if there are more than 10 items
            };
            cbExistingTable.Items.AddRange(existingTables.ToArray());


            ComboBox cbKey = new ComboBox
            {
                Location = new Point(311, startPositionY),
                Size = new Size(70, 22)
            };
            cbKey.Items.AddRange(keys.ToArray());

            TextBox txtSourceTable = new TextBox
            {
                Location = new Point(478, startPositionY),
                Size = new Size(100, 22),
                Text = sourceColumns
            };

            ComboBox cbSourceKey = new ComboBox
            {
                Location = new Point(682, startPositionY),
                Size = new Size(70, 22)
            };
            cbSourceKey.Items.AddRange(sourceTblKey.ToArray());

            panelForPullreleatedData.Controls.Add(selectCheckbox);
            panelForPullreleatedData.Controls.Add(cbExistingTable);
            panelForPullreleatedData.Controls.Add(cbKey);
            panelForPullreleatedData.Controls.Add(txtSourceTable); // Add the TextBox instead of ComboBox
            panelForPullreleatedData.Controls.Add(cbSourceKey);

            _properties.SelectedCheck.Add(selectCheckbox);
            _properties.ExistingTableCombos.Add(cbExistingTable);
            _properties.KeyCombos.Add(cbKey);
            _properties.SourceTableCombos.Add(txtSourceTable); // Change the list to hold TextBox
            _properties.SourceKeyCombos.Add(cbSourceKey);
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            int spacingY = 30; // Adjust this value based on your layout preferences
            int lastIndex = _properties.ExistingTableCombos.Count - 1;
            int startY = _properties.ExistingTableCombos.Count > 0 ? _properties.ExistingTableCombos[lastIndex].Location.Y + spacingY : 240;
            int newIndex = _properties.ExistingTableCombos.Count; // New index for the new controls
            string TableName = cmbTables.Text;
            string tableName = TableName.Split('.')[1];

            // Create new checkbox
            CheckBox selectCheckbox = new CheckBox
            {
                Location = new Point(47, startY),
                Size = new Size(18, 20),
                Tag = newIndex
            };

            // Create new drop-downs
            ComboBox cbExistingTable = new ComboBox
            {
                Location = new Point(142, startY),
                Size = new Size(100, 22),
                Tag = newIndex
            };
            cbExistingTable.Items.AddRange(_properties.ExistingTableCombos[0].Items.Cast<string>().ToArray());

            ComboBox cbKey = new ComboBox
            {
                Location = new Point(311, startY),
                Size = new Size(70, 22),
                Tag = newIndex
            };
            cbKey.Items.AddRange(_properties.KeyCombos[0].Items.Cast<string>().ToArray());

            TextBox txtSourceTable = new TextBox
            {
                Location = new Point(478, startY),
                Size = new Size(100, 22),
                Tag = newIndex,
                Text = tableName // Set the TextBox value from sourceColumns
            };

            ComboBox cbSourceKey = new ComboBox
            {
                Location = new Point(682, startY),
                Size = new Size(70, 22),
                Tag = newIndex
            };
            cbSourceKey.Items.AddRange(_properties.SourceKeyCombos[0].Items.Cast<string>().ToArray());

            // Add them to the panel and the respective lists
            panelForPullreleatedData.Controls.Add(selectCheckbox);
            panelForPullreleatedData.Controls.Add(cbExistingTable);
            panelForPullreleatedData.Controls.Add(cbKey);
            panelForPullreleatedData.Controls.Add(txtSourceTable); // Ensure adding TextBox here
            panelForPullreleatedData.Controls.Add(cbSourceKey);

            _properties.SelectedCheck.Add(selectCheckbox);
            _properties.ExistingTableCombos.Add(cbExistingTable);
            _properties.KeyCombos.Add(cbKey);
            _properties.SourceTableCombos.Add(txtSourceTable); // Ensure adding TextBox to the list
            _properties.SourceKeyCombos.Add(cbSourceKey);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            for (int i = _properties.SelectedCheck.Count - 1; i >= 0; i--)
            {
                if (_properties.SelectedCheck[i].Checked)
                {
                    int index = (int)_properties.SelectedCheck[i].Tag;
                    panelForPullreleatedData.Controls.Remove(_properties.SelectedCheck[i]);
                    panelForPullreleatedData.Controls.Remove(_properties.ExistingTableCombos[index]);
                    panelForPullreleatedData.Controls.Remove(_properties.KeyCombos[index]);
                    panelForPullreleatedData.Controls.Remove(_properties.SourceTableCombos[index]);
                    panelForPullreleatedData.Controls.Remove(_properties.SourceKeyCombos[index]);
                    _properties.SelectedCheck.RemoveAt(i);
                    _properties.ExistingTableCombos.RemoveAt(index);
                    _properties.KeyCombos.RemoveAt(index);
                    _properties.SourceTableCombos.RemoveAt(index);
                    _properties.SourceKeyCombos.RemoveAt(index);

                    // Adjust tags for remaining controls
                    for (int j = i; j < _properties.SelectedCheck.Count; j++)
                    {
                        _properties.SelectedCheck[j].Tag = j;
                        _properties.ExistingTableCombos[j].Tag = j;
                        _properties.KeyCombos[j].Tag = j;
                        _properties.SourceTableCombos[j].Tag = j;
                        _properties.SourceKeyCombos[j].Tag = j;
                    }
                }
            }
        }
    }
}