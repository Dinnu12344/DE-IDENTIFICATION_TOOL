using DE_IDENTIFICATION_TOOL.Models;
using DE_IDENTIFICATION_TOOL.Pythonresponse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class JsonLocationForm : Form
    {
        public readonly JsonLocationFormModel jsonLocationFormModel;
        private readonly string labelName;
        private PythonService pythonService;

        public JsonLocationForm(string labelName, TreeNode selectedNode, List<ProjectData> projectData, HomeForm homeForm)
        {
            InitializeComponent();
            pythonService = new PythonService();
            jsonLocationFormModel = new JsonLocationFormModel
            {
                homeForm = homeForm,
                projectData = projectData,
                selectedNode = selectedNode
            };
            finishButtonInCsvlocationWindow.Enabled = false;
            lblForNoofColumns.Visible = false;
            txtForNoofColumns.Visible = false;
            this.labelName = labelName;

            // Hook up the TextChanged event for validation
            txtForNoofColumns.TextChanged += TxtForNoofColumns_TextChanged;
        }

        private void TxtForNoofColumns_TextChanged(object sender, EventArgs e)
        {
            txtForNoofColumns.TextChanged -= TxtForNoofColumns_TextChanged;

            bool isValid = true;
            string errorMessage = string.Empty;

            if (!int.TryParse(txtForNoofColumns.Text, out int value))
            {
                errorMessage = "Please enter a valid number.";
                isValid = false;
            }
            else if (value < 1 || value > 10000)
            {
                errorMessage = "Please enter a number between 1 and 10000.";
                isValid = false;
            }

            if (!isValid)
            {
                MessageBox.Show(errorMessage, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtForNoofColumns.Text = "";
            }
            else
            {
                jsonLocationFormModel.EnteredText = txtForNoofColumns.Text;
                UpdateFinishButtonVisibility();
            }

            txtForNoofColumns.TextChanged += TxtForNoofColumns_TextChanged;
        }

        private void UpdateFinishButtonVisibility()
        {
            finishButtonInCsvlocationWindow.Enabled = !string.IsNullOrEmpty(jsonLocationFormModel.EnteredText) &&
                                                      !string.IsNullOrEmpty(jsonLocationFormModel.SelectedCsvFilePath);
        }

        private void FinishButtonInCsvlocationWindow_Click(object sender, EventArgs e)
        {
            string projectName = labelName;
            string username = Environment.UserName;
            string projectDirectory = $@"C:\Users\{username}\AppData\Roaming\DeidentificationTool\{projectName}";

            // Folder path associated with the table name
            //string tableDirectoryPath = Path.Combine(projectDirectory, csvLocationFormModel.TableName);

            // Full path including the LogFile subfolder
            //string logFileDirectoryPath = Path.Combine(tableDirectoryPath, "LogFile");

            // File to store the list of table names for the specific project
            string tableNamesFile = Path.Combine(projectDirectory, "TableNames.txt");

            //// Ensure the table name is trimmed of leading/trailing whitespace
            //string enteredTableName = csvLocationFormModel.TableName.Trim();

            // Check if the table name already exists in the current project (case-insensitive comparison)
            //bool tableNameExists = false;
            //if (File.Exists(tableNamesFile))
            //{
            //    var existingTableNames = File.ReadAllLines(tableNamesFile)
            //                                 .Select(name => name.Trim())
            //                                 .ToList();

            //    if (existingTableNames.Any(name => string.Equals(name, enteredTableName, StringComparison.OrdinalIgnoreCase)))
            //    {
            //        tableNameExists = true;
            //    }
            //}

            // If table name exists, prompt and return
            //if (tableNameExists)
            //{
            //    MessageBox.Show($"Table name '{enteredTableName}' already exists in project '{projectName}'. Please try another name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return; // Prevent the form from closing
            //}

            //// Create the table directory if it doesn't exist
            //if (!Directory.Exists(tableDirectoryPath))
            //{
            //    Directory.CreateDirectory(logFileDirectoryPath); // This will also create tableDirectoryPath
            //}

            if (!string.IsNullOrEmpty(jsonLocationFormModel.SelectedCsvFilePath))
            {
                //string username = Environment.UserName;
                DateTime startTime = DateTime.Now;

                string pythonScriptName = "ImportJsonConnection.py";
                string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory();
                string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);
                string pythonResponse = pythonService.JsonImport(jsonLocationFormModel.SelectedCsvFilePath, projectName, jsonLocationFormModel.EnteredText, pythonScriptPath);

                DateTime endTime = DateTime.Now;
                TimeSpan duration = endTime - startTime;

                if (pythonResponse.ToLower().Contains("success"))
                {
                    string tableNamesPart = pythonResponse.Split('\n')[1];
                    tableNamesPart = tableNamesPart.Replace("[", "").Replace("]", "").Replace("'", "").Trim();
                    string[] tableNames = tableNamesPart.Split(',')
                                                         .Select(name => name.Trim())
                                                         .ToArray();
                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                    //try
                    //{
                    //    // Append all table names line by line to the TableNames.txt file
                    File.AppendAllLines(tableNamesFile, tableNames);

                        // Inform user that the table names have been successfully added
                        //MessageBox.Show("Table names have been added to TableNames.txt successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //catch (Exception ex)
                    //{
                    //    // Handle any errors that occur during file writing
                    //    MessageBox.Show($"An error occurred while writing to TableNames.txt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}

                    foreach (string table in tableNames)
                    {
                        TreeNode tableNode = new TreeNode(table);
                        jsonLocationFormModel.selectedNode.Nodes.Add(tableNode);

                        string directoryPath = $@"C:\Users\{username}\AppData\Roaming\DeidentificationTool\{jsonLocationFormModel.selectedNode.Text}\{table}\LogFile";
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        string filePath = Path.Combine(directoryPath, $"{currentDate}.log");
                        string logContent = string.Format(
                            "Job Name:    {0}\n" +
                            "Run start:   {1}\n" +
                            "Run End:     {2}\n" +
                            "Status:      {3}\n" +
                            "Duration:    {4}\n" +
                            "Comment:     {5}\n",
                            "Import",
                            startTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),
                            endTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),
                            "success",
                            duration.TotalSeconds,
                            $"Successfully imported file : {jsonLocationFormModel.SelectedCsvFilePath} as a table : {table} inside the project : {jsonLocationFormModel.selectedNode.Text}\n"
                        );

                        File.WriteAllText(filePath, logContent);
                    }
                    jsonLocationFormModel.selectedNode.Expand();

                    var project = jsonLocationFormModel.projectData.Find(p => p.Name == jsonLocationFormModel.selectedNode.Text);
                    if (project != null)
                    {
                        foreach (string table in tableNames)
                        {
                            if (!project.Tables.Contains(table))
                            {
                                project.Tables.Add(table);
                            }
                        }
                        jsonLocationFormModel.homeForm.SaveProjectData();
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show( pythonResponse, "Error");
                }
            }
        }

        private void LocationBrowsebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                FilterIndex = 1,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Clear previous selections and inputs
                ClearFormFields();

                jsonLocationFormModel.SelectedCsvFilePath = openFileDialog.FileName;
                textBoxForHoldingFilePath.Text = jsonLocationFormModel.SelectedCsvFilePath;
                lblForNoofColumns.Visible = true;
                txtForNoofColumns.Visible = true;
                UpdateFinishButtonVisibility();
            }
        }

        private void ClearFormFields()
        {
            // Temporarily unsubscribe from the TextChanged event for row count to prevent validation
            txtForNoofColumns.TextChanged -= TxtForNoofColumns_TextChanged;
            txtForNoofColumns.Clear();
            txtForNoofColumns.TextChanged += TxtForNoofColumns_TextChanged;

            jsonLocationFormModel.EnteredText = null;
            jsonLocationFormModel.SelectedCsvFilePath = null;

            finishButtonInCsvlocationWindow.Enabled = false;
        }

        private void CanclebtnforClear_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
