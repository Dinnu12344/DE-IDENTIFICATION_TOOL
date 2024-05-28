 using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TestApp.Models;

namespace TestApp
{
    public partial class Form1 : Form
    {
        private const string DataFilePath = "projectData.json";
        private List<ProjectData> projectData;
        private ContextMenuStrip projectsContextMenu;
        private ContextMenuStrip createdProjectsContextMenu;
        private ContextMenuStrip tableContextMenu;

        public Form1()
        {
            InitializeComponent();
            LoadProjectData();
            PopulateTreeView();
            InitializeContextMenus();            
        }

        private void PopulateTreeView()
        {
            if (treeView == null)
            {
                MessageBox.Show("TreeView is not initialized.");
                return;
            }

            treeView.Nodes.Clear();
            TreeNode rootNode = new TreeNode("Projects");
            treeView.Nodes.Add(rootNode);

            if (projectData == null)
            {
                MessageBox.Show("Project data is null.");
                return;
            }

            foreach (var project in projectData)
            {
                TreeNode projectNode = new TreeNode(project.Name);
                foreach (var table in project.Tables)
                {
                    projectNode.Nodes.Add(new TreeNode(table));
                }
                rootNode.Nodes.Add(projectNode);
            }

            rootNode.Expand();
        }

        private void InitializeContextMenus()
        {
            // Context menu for "Projects" node
            projectsContextMenu = new ContextMenuStrip();
            ToolStripMenuItem createProjectItem = new ToolStripMenuItem("Create Project");
            ToolStripMenuItem refreshItem = new ToolStripMenuItem("Refresh");
            createProjectItem.Click += CreateProjectItem_Click;
            refreshItem.Click += RefreshItem_Click;
            projectsContextMenu.Items.Add(createProjectItem);
            projectsContextMenu.Items.Add(refreshItem);

            // Context menu for created projects
            createdProjectsContextMenu = new ContextMenuStrip();
            ToolStripMenuItem editProjectItem = new ToolStripMenuItem("Import");
            ToolStripMenuItem renameProjectItem = new ToolStripMenuItem("Rename");
            ToolStripMenuItem refreshProjectItem = new ToolStripMenuItem("Refresh");
            ToolStripMenuItem deleteProjectItem = new ToolStripMenuItem("Delete");
            editProjectItem.Click += ImportProjectItem_Click;
            deleteProjectItem.Click += DeleteProjectItem_Click;
            //renameProjectItem.Click += RenameProjectItem_Click;
            //refreshProjectItem.Click += RefreshProjectItem_Click;
            createdProjectsContextMenu.Items.Add(editProjectItem);
            createdProjectsContextMenu.Items.Add(deleteProjectItem);
            createdProjectsContextMenu.Items.Add(renameProjectItem);
            createdProjectsContextMenu.Items.Add(refreshProjectItem);

            tableContextMenu = new ContextMenuStrip();
            ToolStripMenuItem configMenuItem = new ToolStripMenuItem("Config");
            ToolStripMenuItem deIdentifyMenuItem = new ToolStripMenuItem("De-Identify");
            ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Delete");
            ToolStripMenuItem viewSourceDataMenuItem = new ToolStripMenuItem("View Source Data");
            ToolStripMenuItem viewDeidentifiedData = new ToolStripMenuItem("View De-identified Data");
            ToolStripMenuItem logMenuItem = new ToolStripMenuItem("Log");
            ToolStripMenuItem exportMenuItem = new ToolStripMenuItem("Export");
            ToolStripMenuItem refreshMenuItem = new ToolStripMenuItem("Refresh");
            configMenuItem.Click += ConfigMenuItem_Click;
            deIdentifyMenuItem.Click += DeIdentifyMenuItem_Click;
            //deleteMenuItem.Click += DeleteMenuItem_Click;
            //viewSourceDataMenuItem.Click += DeleteMenuItem_Click;
            //viewDeidentifiedData.Click += DeleteMenuItem_Click;
            //logMenuItem.Click += DeleteMenuItem_Click;
            exportMenuItem.Click += ExportMenuItem_Click;
            //refreshMenuItem.Click += DeleteMenuItem_Click;
            tableContextMenu.Items.Add(configMenuItem);
            tableContextMenu.Items.Add(deIdentifyMenuItem);
            tableContextMenu.Items.Add(deleteMenuItem);
            tableContextMenu.Items.Add(viewSourceDataMenuItem);
            tableContextMenu.Items.Add(viewDeidentifiedData);
            tableContextMenu.Items.Add(logMenuItem);
            tableContextMenu.Items.Add(exportMenuItem);
            tableContextMenu.Items.Add(refreshMenuItem);
        }

        private void ConfigMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView.SelectedNode;
            if (selectedNode != null)
            {
                TreeNode parentNode = selectedNode.Parent;
                var pythonResponse = SendDataToPython(selectedNode, parentNode);

                // Handle the "Config" click event
                //ConfigForm configForm = new ConfigForm(Form1 homeForm,pythonResponse);
                ConfigForm configForm = new ConfigForm(this, pythonResponse, selectedNode, parentNode);
                configForm.Show();
                // Add your config logic pythonResponse
            }
        }

        private string SendDataToPython(TreeNode selectedNode, TreeNode projectName )
        {
            string table = selectedNode.Text;
            string project = projectName.Text;

            string pythonScriptPath = @"C:\Users\Satya Pulamanthula\Desktop\PythonScriptsGit\ConnectionTestRepo\tableColumnsConnection.py";

            if (!File.Exists(pythonScriptPath))
            {
                return "Error: Python script file not found.";
            }

            var command = $"\"{pythonScriptPath}\" \"{table}\" \"{project}\"";

            using (Process process = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.FileName = GetPythonExePath();
                startInfo.Arguments = command;
                startInfo.StandardOutputEncoding = Encoding.UTF8;

                process.StartInfo = startInfo;
                process.Start();

                var output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return output;
            }
        }

        private string GetPythonExePath()
        {
            string pythonExeName = "python.exe";
            string pythonExePath = null;

            string[] paths = Environment.GetEnvironmentVariable("PATH").Split(';');
            foreach (string path in paths)
            {
                string fullPath = Path.Combine(path, pythonExeName);
                if (File.Exists(fullPath))
                {
                    pythonExePath = fullPath;
                    break;
                }
            }

            return pythonExePath;
        }

        private void DeIdentifyMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView.SelectedNode;
            TreeNode parentnode = selectedNode.Parent;
            if (selectedNode != null)
            {
                string TblName= selectedNode.Text;
                string projectName = parentnode.Text;

                string pythonResponse = SendDataToPythonandGetResponse(TblName, projectName);
                if (pythonResponse.ToLower().Contains("success"))
                {
                    MessageBox.Show("Python response is" , pythonResponse);

                }
                else
                {
                    MessageBox.Show("Python response is not deidentified");
                }
                //// Handle the "De-Identify" click event
                //DeIdentifyForm deIdentifyForm = new DeIdentifyForm();
                //deIdentifyForm.Show();
                
            }
        }


        private string SendDataToPythonandGetResponse(string tableName,string projectName)
        {
            string pythonScriptPath = @"C:\Users\Satya Pulamanthula\Desktop\PythonScriptsGit\ConnectionTestRepo\DeIentificationConnection.py";

            if (!File.Exists(pythonScriptPath))
            {
                return "Error: Python script file not found.";
            }

            var command = $"\"{pythonScriptPath}\" \"{projectName}\" \"{tableName}\"";

            using (Process process = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.FileName = GetPythonExePath();
                startInfo.Arguments = command;
                startInfo.StandardOutputEncoding = Encoding.UTF8;

                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return output;
            }
        }


        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView.SelectedNode = e.Node;
                if (e.Node != null)
                {
                    if (e.Node.Parent == null)
                    {
                        // Root level (Projects)
                        treeView.ContextMenuStrip = projectsContextMenu;
                    }
                    else if (e.Node.Parent.Text == "Projects")
                    {
                        // Project level
                        treeView.ContextMenuStrip = createdProjectsContextMenu;
                    }
                    else
                    {
                        // Table level
                        treeView.ContextMenuStrip = tableContextMenu;
                    }
                }
            }
        }

        private void CreateProjectItem_Click(object sender, EventArgs e)
        {
            // Open form to create a project
            CreateProjectForm createProjectForm = new CreateProjectForm();
            if (createProjectForm.ShowDialog() == DialogResult.OK)
            {
                var newProject = new ProjectData { Name = createProjectForm.ProjectName, Tables = new List<string>() };
                projectData.Add(newProject);
                SaveProjectData();
                PopulateTreeView();
            }
        }

        private void ImportProjectItem_Click(object sender, EventArgs e)
        {
            // Get the selected project node
            TreeNode selectedNode = treeView.SelectedNode;

            if (selectedNode != null)
            {
                ImportForm importForm = new ImportForm(selectedNode.Text);
                if (importForm.ShowDialog() == DialogResult.OK)
                {
                    string selectedOption = importForm.SelectedImportOption;
                    if (selectedOption == "CSV")
                    {
                        CSVLocationForm csvLocationForm = new CSVLocationForm(selectedNode.Text);
                        if (csvLocationForm.ShowDialog() == DialogResult.OK)
                        {
                            string tableName = csvLocationForm.TableName;
                            TreeNode tableNode = new TreeNode(tableName);
                            selectedNode.Nodes.Add(tableNode);
                            selectedNode.Expand();

                            var project = projectData.Find(p => p.Name == selectedNode.Text);
                            if (project != null)
                            {
                                project.Tables.Add(tableName);
                                SaveProjectData();
                            }
                        }
                    }
                    else if (selectedOption == "Database")
                    {
                        // Handle database import
                    }
                }
            }
        }

        private void SaveProjectData()
        {
            try
            {
                var json = JsonConvert.SerializeObject(projectData, Formatting.Indented);
                File.WriteAllText(DataFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }

        private void LoadProjectData()
        {
            try
            {
                if (File.Exists(DataFilePath))
                {
                    var json = File.ReadAllText(DataFilePath);
                    projectData = JsonConvert.DeserializeObject<List<ProjectData>>(json) ?? new List<ProjectData>();
                }
                else
                {
                    projectData = new List<ProjectData>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
                projectData = new List<ProjectData>(); // Ensure projectData is initialized even if an error occurs
            }
        }

        private void RefreshItem_Click(object sender, EventArgs e)
        {
            // Implement refresh functionality
            MessageBox.Show("Refreshing...");
        }
        private void DeleteProjectItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView.SelectedNode;
            if (selectedNode != null && selectedNode.Parent != null && selectedNode.Parent.Text == "Projects")
            {
                var project = projectData.Find(p => p.Name == selectedNode.Text);
                if (project != null)
                {
                    projectData.Remove(project);
                    SaveProjectData();
                    PopulateTreeView();
                }
            }
        }

        private void ExportMenuItem_Click(object sender, EventArgs e)
        {
             TreeNode selectedNode = treeView.SelectedNode;
            TreeNode parentNode = selectedNode.Parent;
            string tablename = selectedNode.Text;
            string projectName = parentNode.Text;
            DeIdentifyForm deIdentifyForm = new DeIdentifyForm(tablename, projectName);
            deIdentifyForm.ShowDialog();

        }
    }
}
    
