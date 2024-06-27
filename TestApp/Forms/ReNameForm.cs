using DE_IDENTIFICATION_TOOL.Pythonresponse;
using Newtonsoft.Json.Linq;
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
    public partial class ReNameForm : Form
    {
        //private TextBox txtBoxForRename;
        //private Button btnForRename;
        private TreeNode selectedNode;
        private string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "projectData.json");
        private string projectsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeidentificationTool");


        public ReNameForm(TreeNode selectedNode)
        {
            InitializeComponent();
            this.selectedNode = selectedNode;
            //txtBoxForRename.Text = selectedNode.Text;
        }

        private void btnForRename_Click(object sender, EventArgs e)
        {
            string newName = txtBoxForRename.Text;
            if (!string.IsNullOrEmpty(newName))
            {
                // Rename the TreeNode
                string oldName = selectedNode.Text;
                selectedNode.Text = newName;

                // Update the JSON file
                UpdateJsonFile(oldName, newName);

                // Rename the project folder
                RenameProjectFolder(oldName, newName);

                MessageBox.Show("Project name updated successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("New name cannot be empty.");
                this.Close();
            }
        }

        private void UpdateJsonFile(string oldName, string newName)
        {
            try
            {
                // Read the JSON file
                var jsonData = File.ReadAllText(jsonFilePath);
                var jsonArray = JArray.Parse(jsonData);

                // Find the project with the old name and update it
                foreach (var project in jsonArray)
                {
                    if (project["name"]?.ToString() == oldName)
                    {
                        project["name"] = newName;
                        break;
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

        //private void RenameProjectFolder(string oldName, string newName)
        //{
        //    try
        //    {
        //        string oldFolderPath = Path.Combine(projectsDirectory, oldName);
        //        string newFolderPath = Path.Combine(projectsDirectory, newName);

        //        if (Directory.Exists(oldFolderPath))
        //        {
        //            Directory.Move(oldFolderPath, newFolderPath);
        //            MessageBox.Show("Project folder renamed successfully.");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Old project folder does not exist.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error renaming project folder: {ex.Message}");
        //    }
        //}

        private void RenameProjectFolder(string oldName, string newName)
        {
            try
            {
                string oldFolderPath = Path.Combine(projectsDirectory, oldName);
                string newFolderPath = Path.Combine(projectsDirectory, newName);

                if (Directory.Exists(oldFolderPath))
                {
                    bool renameSuccessful = false;
                    int maxRetries = 3;
                    int delay = 1000; // 1 second delay between retries

                    for (int i = 0; i < maxRetries; i++)
                    {
                        try
                        {
                            Directory.Move(oldFolderPath, newFolderPath);
                            renameSuccessful = true;
                            break;
                        }
                        catch (IOException ex)
                        {
                            if (i < maxRetries - 1)
                            {
                                System.Threading.Thread.Sleep(delay); // Wait before retrying
                            }
                            else
                            {
                                MessageBox.Show($"Error renaming project folder after {maxRetries} attempts: {ex.Message}");
                                throw;
                            }
                        }
                    }

                    if (renameSuccessful)
                    {
                        MessageBox.Show("Project folder renamed successfully.");
                    }
                }
                else
                {
                    MessageBox.Show("Old project folder does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error renaming project folder: {ex.Message}");
            }
        }




        //if (projectName != null)
        //{
        //    string pythonScriptName = "RenameFolderConnection.py";
        //    string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
        //    string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

        //    //string pythonScriptPath = @"E:\DE-IDENTIFICATION TOOL\DE_IDENTIFICATION_TOOL\TestApp\PythonScripts\DeIentificationConnection.py";
        //    string getpythonResponse = pythonService.RenameDataToPython(selectedNode.Text, pythonScriptPath);
        //    if (getpythonResponse.ToLower().Contains("success"))
        //    {
        //        MessageBox.Show(selectedNode.Text + "has renamed");
        //        this.Close();
        //    }
        //    else
        //    {
        //        MessageBox.Show(getpythonResponse);
        //    }

        //}


        //}
    }
}
