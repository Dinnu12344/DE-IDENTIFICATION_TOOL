using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DE_IDENTIFICATION_TOOL.Models;
using DE_IDENTIFICATION_TOOL.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class Form1 : Form
    {
        private const string DataFilePath = "projectData.json";
        private List<ProjectData> projectData;
        private ContextMenuStrip projectsContextMenu;
        private ContextMenuStrip createdProjectsContextMenu;
        private ContextMenuStrip tableContextMenu;
        private PythonService pythonService;

        public Form1()
        {
            InitializeComponent();
            pythonService = new PythonService();
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
            deleteMenuItem.Click += DeleteMenuItem_Click;
            viewSourceDataMenuItem.Click += ViewSourceMenuItem_Click;
            viewDeidentifiedData.Click += ViewDataMenuItem_Click;
            //logMenuItem.Click += LogMenuItem_Click;
            exportMenuItem.Click += ExportMenuItem_Click;
            //refreshMenuItem.Click += RefreshMenuItem_Click;
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
                string pythonScriptPath = @"Add Path of tables columns connection";
                string pythonResponse = pythonService.SendDataToPython(selectedNode.Text, parentNode.Text, pythonScriptPath);
                ConfigForm configForm = new ConfigForm(this, pythonResponse, selectedNode, parentNode);
                configForm.Show();
            }
        }

        private void DeIdentifyMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView.SelectedNode;
            TreeNode parentnode = selectedNode.Parent;
            if (selectedNode != null)
            {
                string pythonScriptPath = @"Add Path of De-identification connection ";
                string getpythonResponse = pythonService.SendDataToPython(selectedNode.Text, parentnode.Text, pythonScriptPath);
                if (getpythonResponse.ToLower().Contains("success"))
                {
                    MessageBox.Show(selectedNode.Text + " has successfully Deidentified");
                }
                else
                {
                    MessageBox.Show("Python response is not deidentified : " +  getpythonResponse);
                }

            }
        }

        private void ViewSourceMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("No node is selected.");
                return;
            }

            TreeNode parentnode = selectedNode.Parent;
            if (parentnode == null)
            {
                MessageBox.Show("The selected node has no parent.");
                return;
            }

            string pythonScriptPath = @"Add path of viewsource data connection";
            string getpythonResponse = pythonService.SendDataToPython(selectedNode.Text, parentnode.Text, pythonScriptPath);
            Console.WriteLine("Python Response: " + getpythonResponse);
            if (IsValidPythonResponse(getpythonResponse))
            {
                ViewSourceDeIdentifyForm viewData = new ViewSourceDeIdentifyForm(getpythonResponse);
                viewData.Show();
            }
            else
            {
                MessageBox.Show("Python response is not valid: " + getpythonResponse);
            }
        }
        private void ViewDataMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("No node is selected.");
                return;
            }

            TreeNode parentnode = selectedNode.Parent;
            if (parentnode == null)
            {
                MessageBox.Show("The selected node has no parent.");
                return;
            }

            string pythonScriptPath = @"Add Path of view deidentify connection";
            string getpythonResponse = pythonService.SendDataToPython(selectedNode.Text, parentnode.Text, pythonScriptPath);
            Console.WriteLine("Python Response: " + getpythonResponse);

            if (IsValidPythonResponse(getpythonResponse))
            {
                ViewSourceDeIdentifyForm viewData = new ViewSourceDeIdentifyForm(getpythonResponse);
                viewData.Show();
            }
            else
            {
                MessageBox.Show("Python response is not valid: " + getpythonResponse);
            }
        }
        private bool IsValidPythonResponse(string response)
        {
            return !string.IsNullOrEmpty(response);
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
                        treeView.ContextMenuStrip = projectsContextMenu;
                    }
                    else if (e.Node.Parent.Text == "Projects")
                    {
                        treeView.ContextMenuStrip = createdProjectsContextMenu;
                    }
                    else
                    {
                        treeView.ContextMenuStrip = tableContextMenu;
                    }
                }
            }
        }

        private void CreateProjectItem_Click(object sender, EventArgs e)
        {
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
                            string tableName = csvLocationForm.csvLocationFormModel.TableName;
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
                        DBLocationForm dbLocationForm = new DBLocationForm();
                        if(dbLocationForm.ShowDialog() == DialogResult.OK)
                        {
                            dbLocationForm.Show();
                        }
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
        private void DeleteSelectedNode()
        {
            TreeNode selectedNode = treeView.SelectedNode;

            string projectName = selectedNode.Parent.Text;
            string tablename = selectedNode.Text;

            if (selectedNode == null) return;

            if (selectedNode.Parent != null && selectedNode.Parent.Text == "Projects")
            {
                // Delete Project
                var project = projectData.Find(p => p.Name == selectedNode.Text);
                if (project != null)
                {
                    string projecrtname = selectedNode.Text;                    
                    string pythonfile = @"E:\DE-IDENTIFICATION TOOL\DE_IDENTIFICATION_TOOL\TestApp\bin\Debug\PythonScriptsGit\ConnectionTestRepo_New\ConnectionTestRepo\DeleteTableConnection.py";
                    string pythonResponse = pythonService.DeleteProjectData(projecrtname, pythonfile);
                    if (pythonResponse.ToLower().Contains("success"))
                    {
                        MessageBox.Show("Python response is" + pythonResponse);
                        //projectData.Remove(project);

                    }
                    else
                    {
                        MessageBox.Show("Python response is not deleted :" + pythonResponse);
                    }
                }
            }
            else if (selectedNode.Parent != null && selectedNode.Parent.Parent != null && selectedNode.Parent.Parent.Text == "Projects")
            {
                // Delete Table
                var project = projectData.Find(p => p.Name == selectedNode.Parent.Text);
                if (project != null)
                {
                    string pythonfile = @"Add path of delete table connection";
                    string projecrtname = selectedNode.Parent.Text;
                    string pythonResponse = pythonService.DeleteData(projectName, tablename, pythonfile);
                    if (pythonResponse.ToLower().Contains("success"))
                    {
                        MessageBox.Show("Python response is"+ pythonResponse);
                        project.Tables.Remove(selectedNode.Text);

                    }
                    else
                    {
                        MessageBox.Show("Python response is not deleted the project :"+ pythonResponse);
                    }     
                }
            }
            SaveProjectData();
            PopulateTreeView();
        }
        private void DeleteProjectItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedNode();
        }
        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedNode();
        }
        private void ExportMenuItem_Click(object sender, EventArgs e)
        {
             TreeNode selectedNode = treeView.SelectedNode;
            TreeNode parentNode = selectedNode.Parent;
            string tablename = selectedNode.Text;
            string projectName = parentNode.Text;
            ExportForm deIdentifyForm = new ExportForm(tablename, projectName);
            deIdentifyForm.ShowDialog();
        }
    }
}
    
