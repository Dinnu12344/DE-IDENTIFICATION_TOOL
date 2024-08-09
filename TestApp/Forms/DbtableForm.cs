using DE_IDENTIFICATION_TOOL.Models;
using DE_IDENTIFICATION_TOOL.Pythonresponse;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class DbtableForm : Form
    {
        private DBLocationForm _previousForm;
        private readonly DbtableFormModel _properties;
        private PythonService pythonService;
        private Button btnNew;
        private Button btnDelete;
        public DbtableForm(DbtableFormModel properties, DBLocationForm previousForm)
        {
            InitializeComponent();
            btnForSavePullreleatedData.Enabled = false;
            pythonService = new PythonService();
            _properties = properties;
            _previousForm = previousForm;


            LoadDatabases();

            btnNew = new Button { Text = "+ New", Visible = false };

            btnNew.Click += BtnNew_Click;

            Controls.Add(btnNew);

            btnDelete = new Button { Text = "Delete", Visible = false };

            btnDelete.Click += BtnDelete_Click;

            Controls.Add(btnDelete);

            btnForFinish.Enabled = true;
            _previousForm = previousForm;
        }

        private void LoadDatabases()
        {
            string connectionString = _properties.ConnectionString;
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
            _properties.dbName = cmbDatabases.SelectedItem.ToString();
            UpdateFinishButtonVisibility();
            string selectedDatabase = cmbDatabases.SelectedItem.ToString();
            LoadTables(selectedDatabase);
        }

        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)

        {
            _properties.tableName = cmbTables.SelectedItem.ToString();
            UpdateFinishButtonVisibility();
        }

        private void txtForNoofColumns_TextChanged(object sender, EventArgs e)
        {
            _properties.rowCount = txtForNoofColumns.Text;
            UpdateFinishButtonVisibility();
        }

        private void UpdateFinishButtonVisibility()
        {
            btnForFinish.Enabled = !string.IsNullOrEmpty(_properties.dbName) &&

                                   !string.IsNullOrEmpty(_properties.tableName) &&

                                   !string.IsNullOrEmpty(_properties.rowCount);
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
                    string query = @"
                        SELECT TABLE_SCHEMA,TABLE_NAME,COLUMN_NAME 
                        FROM INFORMATION_SCHEMA.COLUMNS 
                        WHERE TABLE_NAME IN 
                        (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE')";

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
                            cmbTables.Show();
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

            try
            {
                if (checkBoxforPullreleateddata.Checked)
                {
                    var selectedData = new List<SelectedTableData>();

                    int count = _properties.SelectedCheck.Count;
                    if (_properties.ExistingTableCombos.Count != count ||
                        _properties.KeyCombos.Count != count ||
                        _properties.SourceTableCombos.Count + _properties.SourceTableTextBoxs.Count != count ||
                        _properties.SourceKeyCombos.Count != count)
                    {
                        throw new InvalidOperationException("Collection counts are not synchronized.");
                    }

                    for (int i = 0; i < count; i++)
                    {
                        if (_properties.SelectedCheck[i].Checked)
                        {
                            var data = new SelectedTableData
                            {
                                ExistingTable = _properties.ExistingTableCombos[i].Text,
                                ExistingColumn = _properties.KeyCombos[i].Text,
                                SourceTable = _properties.SourceTableCombos.Count > i
                                              ? _properties.SourceTableCombos[i].Text
                                              : _properties.SourceTableTextBoxs[i - _properties.SourceTableCombos.Count].Text,
                                SourceColumn = _properties.SourceKeyCombos[i].Text
                            };
                            selectedData.Add(data);
                        }
                    }

                    string jsonData = JsonConvert.SerializeObject(selectedData);
                    MessageBox.Show(jsonData);

                    // Define the Python script path for the checkbox-checked scenario
                    string savePythonScriptName = "ImportRelatedSqlDataConnection.py";
                    string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                    string savePythonScriptPath = Path.Combine(projectRootDirectory, savePythonScriptName);
                    string savePythonResponse = pythonService.SendSqlDataToPython(projectName, server, DatabaseName, userId, password, TableName, jsonData, Enterno, savePythonScriptPath);

                    if (savePythonResponse.ToLower().Contains("success"))
                    {
                        string username = Environment.UserName;
                        string directoryPath = $@"C:\Users\{username}\AppData\Roaming\DeidentificationTool\{projectName}\{tableName}\LogFile";
                        // Ensure the directory exists
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        MessageBox.Show(" Related Data saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                        MessageBox.Show("Related Failed to save data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    string importPythonScriptName = "ImportSqlConnection.py";
                    string importProjectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                    string importPythonScriptPath = Path.Combine(importProjectRootDirectory, importPythonScriptName);

                    string importPythonResponse = pythonService.SendSqlImportDataToPython(server, DatabaseName, password, userId, projectName, Enterno, tableName, schemaName, importPythonScriptPath);

                    if (importPythonResponse.ToLower().Contains("success"))
                    {

                        string username = Environment.UserName;
                        string directoryPath = $@"C:\Users\{username}\AppData\Roaming\DeidentificationTool\{projectName}\{tableName}\LogFile";
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }


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
                        MessageBox.Show("The CSV file is not valid. Error: " + importPythonResponse, "Error");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void checkBoxforPullreleateddata_CheckedChanged_1(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Checked)
            {
                ShowNewAndDeleteButtons();
                ShowHeadersAndDropDowns();
            }
            else
            {
                HideNewAndDeleteButtons();
                ClearDynamicControls();
            }
        }

        private void ClearDynamicControls()

        {


            var controlsToRemove = panelForPullreleatedData.Controls.OfType<Control>()

                                        .Where(c => c is ComboBox || c is TextBox || c is Label || c is CheckBox)

                                        .ToList();

            foreach (var control in controlsToRemove)

            {

                panelForPullreleatedData.Controls.Remove(control);

            }
            _properties.SelectedCheck.Clear();

            _properties.ExistingTableCombos.Clear();

            _properties.KeyCombos.Clear();

            _properties.SourceTableCombos.Clear();

            _properties.SourceKeyCombos.Clear();

            _properties.SourceTableTextBoxs.Clear();
        }

        private void HideNewAndDeleteButtons()

        {
            btnNew.Visible = false;
            btnDelete.Visible = false;
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

        private void ShowHeadersAndDropDowns(/*string response*/)

        {

            string projectName = _properties.ProjectName;

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string jsonFilePath = Path.Combine(baseDirectory, "projectData.json");

            List<string> existingTables = GetAllTablesFromJson(jsonFilePath, projectName);

            string selectedTable = existingTables.FirstOrDefault();

            if (selectedTable != null)

            {

                UpdateKeysForSelectedTable(selectedTable);

            }

            string sourceColumn = GetSourceColumns();

            List<string> sourceTblKey = tableColumns.ContainsKey(cmbTables.Text) ? tableColumns[cmbTables.Text] : new List<string>();

            AddHeaderLabels();

            // Use sourceColumns here

            AddDropDowns(existingTables, new List<string>(), sourceColumn, sourceTblKey);

        }

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

        private void CbExistingTable_SelectedIndexChanged(object sender, EventArgs e)

        {

            ComboBox cbExistingTable = sender as ComboBox;

            if (cbExistingTable != null)

            {

                string selectedTable = cbExistingTable.SelectedItem.ToString();

                UpdateKeysForSelectedTable(selectedTable);

            }

        }

        private void UpdateKeysForSelectedTable(string tableName)

        {

            string projectName = _properties.ProjectName;

            string username = Environment.UserName;

            string pythonScriptName = "TableColumnsConnection.py";

            string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method

            string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

            string pythonResponse = pythonService.SendDataToPython(tableName, projectName, pythonScriptPath);

            //List<string> result = pythonResponse.Split(',').ToList();

            // Clean up the response string

            pythonResponse = pythonResponse = pythonResponse.Replace("[", "").Replace("]", "").Replace("'", "").Trim();

            // Split the cleaned string into a list of values

            List<string> result = pythonResponse.Split(new char[] { ',' }).Select(s => s.Trim()).ToList();

            List<string> keys = result;

            // Update the key ComboBoxes with the new keys

            foreach (var keyCombo in _properties.KeyCombos)

            {

                keyCombo.Items.Clear();

                keyCombo.Items.AddRange(keys.ToArray());

            }

        }

        private void AddDropDowns(List<string> existingTables, List<string> keys, string sourceColumns, List<string> sourceTblKey)

        {

            int startPositionY = 123;

            CheckBox selectCheckbox = new CheckBox

            {

                Location = new Point(47, startPositionY),

                Size = new Size(18, 17),

                Text = "" // or any relevant text for your checkbox

            };

            ComboBox cbExistingTable = new ComboBox

            {

                Location = new Point(142, startPositionY),

                Size = new Size(100, 22)

            };

            cbExistingTable.Items.AddRange(existingTables.ToArray());

            cbExistingTable.SelectedIndexChanged += CbExistingTable_SelectedIndexChanged;

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

            panelForPullreleatedData.Controls.Add(txtSourceTable);

            panelForPullreleatedData.Controls.Add(cbSourceKey);

            _properties.SelectedCheck.Add(selectCheckbox);

            _properties.ExistingTableCombos.Add(cbExistingTable);

            _properties.KeyCombos.Add(cbKey);

            _properties.SourceTableTextBoxs.Add(txtSourceTable);

            _properties.SourceKeyCombos.Add(cbSourceKey);

        }

        private void BtnNew_Click(object sender, EventArgs e)

        {

            int spacingY = 30; // Adjust this value based on your layout preferences

            int lastIndex = _properties.ExistingTableCombos.Count - 1;

            int startY = _properties.ExistingTableCombos.Count > 0 ? _properties.ExistingTableCombos[lastIndex].Location.Y + spacingY : 240;

            int newIndex = _properties.ExistingTableCombos.Count; // New index for the new controls

            // Create new checkbox

            CheckBox selectCheckbox = new CheckBox

            {

                Location = new Point(47, startY),

                Size = new Size(18, 20),

                Tag = newIndex

            };

            ComboBox cbExistingTable = new ComboBox

            {

                Location = new Point(142, startY),

                Size = new Size(100, 22)

            };

            cbExistingTable.Items.AddRange(_properties.ExistingTableCombos[0].Items.Cast<string>().ToArray());

            cbExistingTable.SelectedIndexChanged += CbExistingTable_SelectedIndexChanged;

            ComboBox cbKey = new ComboBox

            {

                Location = new Point(311, startY),

                Size = new Size(70, 22)

            };

            cbKey.Items.AddRange(_properties.KeyCombos[0].Items.Cast<string>().ToArray());

            ComboBox cbSourceTable = new ComboBox

            {

                Location = new Point(478, startY),

                Size = new Size(100, 22),

                Tag = newIndex

            };

            cbSourceTable.Items.AddRange(_properties.ExistingTableCombos[0].Items.Cast<string>().ToArray());

            ComboBox cbSourceKey = new ComboBox

            {

                Location = new Point(682, startY),

                Size = new Size(70, 22),

                Tag = newIndex

            };

            cbSourceKey.Items.AddRange(_properties.KeyCombos[0].Items.Cast<string>().ToArray());

            // Add them to the panel and the respective lists

            panelForPullreleatedData.Controls.Add(selectCheckbox);

            panelForPullreleatedData.Controls.Add(cbExistingTable);

            panelForPullreleatedData.Controls.Add(cbKey);

            panelForPullreleatedData.Controls.Add(cbSourceTable);

            panelForPullreleatedData.Controls.Add(cbSourceKey);

            _properties.SelectedCheck.Add(selectCheckbox);

            _properties.ExistingTableCombos.Add(cbExistingTable);

            _properties.KeyCombos.Add(cbKey);

            _properties.SourceTableCombos.Add(cbSourceTable);

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
        private void btnForCancel_Click(object sender, EventArgs e)
        {
            _previousForm.Show();
            this.Close();
        }
    }
}