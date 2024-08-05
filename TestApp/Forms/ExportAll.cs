using DE_IDENTIFICATION_TOOL.Models;
using DE_IDENTIFICATION_TOOL.Pythonresponse;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class ExportAll : Form
    {
        private readonly string _projectName;
        private DbtableFormModel _properties;
        public string _tableName;
        private PythonService pythonService;
        private Dictionary<string, string> tableSchemas = new Dictionary<string, string>();
        private Dictionary<string, List<string>> tableColumns = new Dictionary<string, List<string>>();

        public ExportAll(string projectName, string tableName)
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

            labelForDatabase.Visible = false;
            cmbDatabases.Visible = false;
            labelForDatabaseTbl.Visible = false;
            cmbTables.Visible = false;

            btnForFinish.Enabled = false;

            // Attach event handlers to text changed events
            txtForServer.TextChanged += new EventHandler(ValidateInputFields);
            txtForUsername.TextChanged += new EventHandler(ValidateInputFields);
            txtForPassword.TextChanged += new EventHandler(ValidateInputFields);
        }

        private void ValidateInputFields(object sender, EventArgs e)
        {
            // Enable the "Next" button only if all required fields are filled
            btnForNext.Enabled = !string.IsNullOrEmpty(txtForServer.Text) &&
                                 !string.IsNullOrEmpty(txtForUsername.Text) &&
                                 !string.IsNullOrEmpty(txtForPassword.Text);
        }

        private void dbTyped_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnForNext_Click(object sender, EventArgs e)
        {

        }

        private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void cmbTables_ItemCheck(object sender, ItemCheckEventArgs e)
        //{
        //    // Update the _properties.tableNames list based on the checked state
        //    string tableName = cmbTables.Items[e.Index].ToString();
        //    if (e.NewValue == CheckState.Checked)
        //    {
        //        if (!_properties.tableNames.Contains(tableName))
        //        {
        //            _properties.tableNames.Add(tableName);
        //        }
        //    }
        //    else
        //    {
        //        if (_properties.tableNames.Contains(tableName))
        //        {
        //            _properties.tableNames.Remove(tableName);
        //        }
        //    }
        //    UpdateFinishButtonVisibility();
        //}

        //private void UpdateFinishButtonVisibility()
        //{
        //    btnForFinish.Enabled = !string.IsNullOrEmpty(_properties.dbName) &&
        //                           _properties.tableNames.Count > 0;
        //}

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

        private void btnForFinish_Click(object sender, EventArgs e)
        {

        }
    }
}
