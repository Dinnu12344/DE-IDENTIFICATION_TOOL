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

            if (!string.IsNullOrEmpty(jsonLocationFormModel.SelectedCsvFilePath))
            {
                string username = Environment.UserName;
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
                        string logContent = $@"Job Name: Import
                        Run start: {startTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")}
                        Run End: {endTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")}
                        Status: success
                        Duration: {duration.TotalSeconds}
                        Comment: Successfully imported file : {jsonLocationFormModel.SelectedCsvFilePath} as a table : {table} inside the project : {jsonLocationFormModel.selectedNode.Text}";

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
