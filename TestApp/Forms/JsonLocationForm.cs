using DE_IDENTIFICATION_TOOL.Models;
using DE_IDENTIFICATION_TOOL.Pythonresponse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            jsonLocationFormModel = new JsonLocationFormModel();
            jsonLocationFormModel.homeForm = homeForm;
            jsonLocationFormModel.projectData = projectData;
            jsonLocationFormModel.selectedNode  = selectedNode;
            finishButtonInCsvlocationWindow.Visible = false;
            lblForNoofColumns.Visible = false;
            txtForNoofColumns.Visible = false;
            this.labelName = labelName;
        }      

        private void TxtForNoofColumns_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtForNoofColumns.Text, out int value))
            {
                MessageBox.Show("Please enter a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtForNoofColumns.Text = "";
                return;
            }

            if (value < 1 || value > 10000)
            {
                MessageBox.Show("Please enter a number between 1 and 10000.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtForNoofColumns.Text = ""; 
            }

            jsonLocationFormModel.EnteredText = txtForNoofColumns.Text;
            
            UpdateFinishButtonVisibility();
        }
        private void UpdateFinishButtonVisibility()
        {
            finishButtonInCsvlocationWindow.Visible = !string.IsNullOrEmpty(jsonLocationFormModel.EnteredText);
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
                string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);
                string pythonResponse = pythonService.JsonImport(jsonLocationFormModel.SelectedCsvFilePath, projectName, jsonLocationFormModel.EnteredText, pythonScriptPath);

                DateTime endTime = DateTime.Now;
                TimeSpan duration = endTime - startTime;

                if (pythonResponse.ToLower().Contains("success"))
                {
                    string tableNamesPart = pythonResponse.Split('\n')[1]; // Get the part after the newline
                    tableNamesPart = tableNamesPart.Replace("[", "").Replace("]", "").Replace("'", "").Trim();
                    string[] tableNames = tableNamesPart.Split(',')
                                                         .Select(name => name.Trim())
                                                         .ToArray();
                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                    try
                    {
                        // Append all table names line by line to the TableNames.txt file
                        File.AppendAllLines(tableNamesFile, tableNames);

                        // Inform user that the table names have been successfully added
                        MessageBox.Show("Table names have been added to TableNames.txt successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that occur during file writing
                        MessageBox.Show($"An error occurred while writing to TableNames.txt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

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
                    MessageBox.Show("The python response is failed. Error: " + pythonResponse, "Error");
                }
            }
        }


        private void LocationBrowsebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";

            openFileDialog.FilterIndex = 1;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                jsonLocationFormModel.SelectedCsvFilePath = openFileDialog.FileName;
                textBoxForHoldingFilePath.Text = jsonLocationFormModel.SelectedCsvFilePath;
                lblForNoofColumns.Visible = true;
                txtForNoofColumns.Visible = true;
            }
        }
    }
}
