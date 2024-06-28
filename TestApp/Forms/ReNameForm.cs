using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class ReNameForm : Form
    {
        private TreeNode selectedNode;
        private TreeNode parentNode;
        private string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "projectData.json");
        private string projectsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeidentificationTool");

        public ReNameForm(TreeNode selectedNode, TreeNode parentNode = null)
        {
            InitializeComponent();
            this.selectedNode = selectedNode;
            this.parentNode = parentNode;
            txtBoxForRename.Text = selectedNode.Text;
        }

        private void btnForRename_Click(object sender, EventArgs e)
        {
            string newName = txtBoxForRename.Text;
            if (!string.IsNullOrEmpty(newName))
            {
                // Rename the TreeNode
                string oldName = selectedNode.Text;
                selectedNode.Text = newName;

                if (parentNode == null)
                {
                    // Renaming a project
                    RenameProjectFolder(oldName, newName);
                    UpdateJsonFile(oldName, newName, isProject: true);
                }
                else
                {
                    // Renaming a table
                    RenameTableInProject(oldName, newName, parentNode);
                    UpdateJsonFile(oldName, newName, isProject: false);
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

        private void RenameTableInProject(string oldTableName, string newTableName, TreeNode parentNode)
        {
            string parentName = parentNode.Text;
            string oldFolderPath = Path.Combine(projectsDirectory, parentName, oldTableName);
            string newFolderPath = Path.Combine(projectsDirectory, parentName, newTableName);

            try
            {
                // Check if the old and new paths are different
                if (oldFolderPath.Equals(newFolderPath, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("The old and new table names are the same. Please enter a different name.", "Error");
                    return;
                }

                if (Directory.Exists(oldFolderPath))
                {
                    Directory.Move(oldFolderPath, newFolderPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error renaming table folder: {ex.Message}");
            }
        }

        private void UpdateJsonFile(string oldName, string newName, bool isProject)
        {
            try
            {
                // Read the JSON file
                var jsonData = File.ReadAllText(jsonFilePath);
                var jsonArray = JArray.Parse(jsonData);

                // Find the project or table with the old name and update it
                foreach (var item in jsonArray)
                {
                    if (isProject)
                    {
                        // Update project name
                        if (item["Name"]?.ToString() == oldName)
                        {
                            item["Name"] = newName;
                            break;
                        }
                    }
                    else
                    {
                        // Update table name within a project
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

                // Write the updated JSON back to the file
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