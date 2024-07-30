using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows.Forms;
using DE_IDENTIFICATION_TOOL.Pythonresponse;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class ReNameForm : Form
    {
        private TreeNode selectedNode;
        private TreeNode parentNode;
        private PythonService pythonService;
        private string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "projectData.json");
        private string projectsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeidentificationTool");

        public ReNameForm(TreeNode selectedNode, TreeNode parentNode = null)
        {
            InitializeComponent();
            pythonService = new PythonService();
            this.selectedNode = selectedNode;
            this.parentNode = parentNode;
            txtBoxForRename.Text = selectedNode.Text;
        }

        private void btnForRename_Click(object sender, EventArgs e)
        {
            string newName = txtBoxForRename.Text;
            if (!string.IsNullOrEmpty(newName))
            {
                string oldName = selectedNode.Text;
                selectedNode.Text = newName;
                if (parentNode == null)
                {
                    RenameProjectFolder(oldName, newName);
                    UpdateJsonFile(oldName, newName, isProject: true);
                }
                else
                {
                    if(RenameTableInProject(oldName, newName, parentNode) == "Success")
                    {
                        UpdateJsonFile(oldName, newName, isProject: false);
                    }
                   
                }

                MessageBox.Show("Name updated successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("New name cannot be empty.");
            }
        }

        private void RenameProjectFolder(string oldName, string newName)
        {
            string oldFolderPath = Path.Combine(projectsDirectory, oldName);
            string newFolderPath = Path.Combine(projectsDirectory, newName);
            try
            {
                if (Directory.Exists(oldFolderPath))
                {
                    Directory.Move(oldFolderPath, newFolderPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error renaming project folder: {ex.Message}");
            }
        }

        private string RenameTableInProject(string oldTableName, string newTableName, TreeNode parentNode)
        {
            string parentName = parentNode.Text;
            string oldFolderPath = Path.Combine(projectsDirectory, parentName, oldTableName);
            string newFolderPath = Path.Combine(projectsDirectory, parentName, newTableName);

            try
            {
                if (oldFolderPath.Equals(newFolderPath, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("The old and new table names are the same. Please enter a different name.", "Error");
                    return "Failed";
                }

                if (Directory.Exists(oldFolderPath))
                {
                    string pythonScriptName = "RenameTableConnection.py";
                    string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                    string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);


                    string pythonResponse = pythonService.RenameTableDataToPython(parentName, oldTableName , newTableName, pythonScriptPath);

                    if (pythonResponse.ToLower().Contains("success"))
                    {
                        Directory.Move(oldFolderPath, newFolderPath);
                        MessageBox.Show("Renamed success");
                        this.Close();
                        return "Success";
                    }
                    else
                    {
                        MessageBox.Show("rRename Unsuccess"+pythonResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error renaming table folder: {ex.Message}");
            }
            return "Failed";
        }

        private void UpdateJsonFile(string oldName, string newName, bool isProject)
        {
            try
            {
                var jsonData = File.ReadAllText(jsonFilePath);
                var jsonArray = JArray.Parse(jsonData);

                foreach (var item in jsonArray)
                {
                    if (isProject)
                    {
                        if (item["Name"]?.ToString() == oldName)
                        {
                            item["Name"] = newName;
                            break;
                        }
                    }
                    else
                    {
                        var tablesArray = item["Tables"] as JArray;
                        if (tablesArray != null)
                        {
                            for (int i = 0; i < tablesArray.Count; i++)
                            {
                                if (tablesArray[i]?.ToString() == oldName)
                                {
                                    tablesArray[i] = newName;
                                    break;
                                }
                            }
                        }
                    }
                }
                File.WriteAllText(jsonFilePath, jsonArray.ToString());

                MessageBox.Show("JSON data updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating JSON file: {ex.Message}");
            }
        }
    }
}