using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DE_IDENTIFICATION_TOOL.Models;
using DE_IDENTIFICATION_TOOL.Forms;
using System.Runtime.InteropServices;
using System.Linq;
using DE_IDENTIFICATION_TOOL.Pythonresponse;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class HomeForm : Form
    {
        private const string DataFilePath = "projectData.json";
        private List<ProjectData> projectData;
        private ContextMenuStrip projectsContextMenu;
        private ContextMenuStrip createdProjectsContextMenu;
        private ContextMenuStrip tableContextMenu;
        private PythonService pythonService;
        public PythonScriptFilePath pythonScriptFilePath;
        public static readonly string pythonScriptsDirectory;

        public HomeForm()
        {
            InitializeComponent();
            pythonService = new PythonService();
            pythonScriptFilePath = new PythonScriptFilePath();
            LoadProjectData();
            PopulateTreeView();
            InitializeContextMenus();
        }

        private void PopulateTreeView()
        {
            if (treeViewPanel == null)
            {
                MessageBox.Show("TreeView is not initialized.");
                return;
            }

            treeViewPanel.Nodes.Clear();
            TreeNode rootNode = new TreeNode("Projects");
            treeViewPanel.Nodes.Add(rootNode);

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
            ToolStripMenuItem keyProjectItem = new ToolStripMenuItem("Key");
            ToolStripMenuItem renameProjectItem = new ToolStripMenuItem("Rename");
            ToolStripMenuItem refreshProjectItem = new ToolStripMenuItem("Refresh");
            ToolStripMenuItem deleteProjectItem = new ToolStripMenuItem("Delete");
            editProjectItem.Click += ImportProjectItem_Click;
            deleteProjectItem.Click += DeleteProjectItem_Click;
            keyProjectItem.Click += KeyProjectItem_Click;
            renameProjectItem.Click += ReNameProjectItem_Click;
            createdProjectsContextMenu.Items.Add(editProjectItem);
            createdProjectsContextMenu.Items.Add(keyProjectItem);
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
            ToolStripMenuItem renameMenuItem = new ToolStripMenuItem("Rename");
            configMenuItem.Click += ConfigMenuItem_Click;
            deIdentifyMenuItem.Click += DeIdentifyMenuItem_Click;
            deleteMenuItem.Click += DeleteMenuItem_Click;
            logMenuItem.Click += LogMenuItem_Click;
            viewSourceDataMenuItem.Click += ViewSourceMenuItem_Click;
            viewDeidentifiedData.Click += ViewDataMenuItem_Click;
            exportMenuItem.Click += ExportMenuItem_Click;
            renameMenuItem.Click += ReNameTableItem_Click;
            tableContextMenu.Items.Add(configMenuItem);
            tableContextMenu.Items.Add(deIdentifyMenuItem);
            tableContextMenu.Items.Add(deleteMenuItem);
            tableContextMenu.Items.Add(viewSourceDataMenuItem);
            tableContextMenu.Items.Add(viewDeidentifiedData);
            tableContextMenu.Items.Add(logMenuItem);
            tableContextMenu.Items.Add(exportMenuItem);
            tableContextMenu.Items.Add(renameMenuItem);
        }

        private void ConfigMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPanel.SelectedNode;
            if (selectedNode != null)
            {
                TreeNode parentNode = selectedNode.Parent;
                if (parentNode != null) // Ensure the selected node has a parent node
                {
                    string pythonScriptName = "TableColumnsConnection.py";
                    string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                    string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

                    Console.WriteLine("Python Script Path: " + pythonScriptPath);

                    // Assuming SendDataToPython takes table name, project name, and script path as arguments
                    string pythonResponse = pythonService.SendDataToPython(selectedNode.Text, parentNode.Text, pythonScriptPath);
                    if (IsValidPythonResponse(pythonResponse))
                    {
                        ConfigForm configForm = new ConfigForm(this, pythonResponse, selectedNode, parentNode);
                        configForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Config is not done Please Check once");
                    }

                }
                else
                {
                    MessageBox.Show("Selected node does not have a parent node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No node selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReNameProjectItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPanel.SelectedNode;
            ReNameForm reNameForm = new ReNameForm(selectedNode);
            reNameForm.ShowDialog(); // ShowDialog blocks until the form is closed
        }

        private void ReNameTableItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPanel.SelectedNode;
            TreeNode parentNode = selectedNode.Parent;
            ReNameForm reNameForm = new ReNameForm(selectedNode, parentNode);
            reNameForm.ShowDialog(); // ShowDialog blocks until the form is closed
        }

        private void DeIdentifyMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPanel.SelectedNode;
            TreeNode parentnode = selectedNode.Parent;
            if (selectedNode != null)
            {
                string pythonScriptName = "DeidentificationConnection.py";
                string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

                //string pythonScriptPath = @"E:\DE-IDENTIFICATION TOOL\DE_IDENTIFICATION_TOOL\TestApp\PythonScripts\DeIentificationConnection.py";
                string getpythonResponse = pythonService.SendDataToPython(selectedNode.Text, parentnode.Text, pythonScriptPath);
                if (getpythonResponse.ToLower().Contains("success"))
                {
                    MessageBox.Show(selectedNode.Text + " has successfully Deidentified");
                }
                else
                {
                    MessageBox.Show(getpythonResponse);
                }

            }
        }

        private void ViewSourceMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPanel.SelectedNode;
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

            string pythonScriptName = "ViewSourceDataConnection.py";
            string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
            string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

            //string pythonScriptPath = @"E:\DE-IDENTIFICATION TOOL\DE_IDENTIFICATION_TOOL\TestApp\PythonScripts\ViewScourceDataConnection.py";
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
            TreeNode selectedNode = treeViewPanel.SelectedNode;
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

            string savePythonScriptName = "checkDeidentifiedTable.py";
            string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
            string savePythonScriptPath = Path.Combine(projectRootDirectory, savePythonScriptName);

            // Send data to Python script and capture the response
            //string savePythonResponse = pythonService.SendSqlDataToPython(server, DatabaseName, password, userId, projectName, Enterno, tableName, schemaName, savePythonScriptPath, jsonData);
            string savePythonResponse = pythonService.checkDeidentifiedTable(selectedNode.Text, parentnode.Text, savePythonScriptPath);

            if (savePythonResponse.Contains("True"))
            {


                string pythonScriptName = "ViewDeidentifiedDataConnection.py";
                projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

                //string pythonScriptPath = @"E:\DE-IDENTIFICATION TOOL\DE_IDENTIFICATION_TOOL\TestApp\PythonScripts\ViewDeidentifiedDataConnection.py";
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
            else
            {
                MessageBox.Show("table is not didentify");
            }
        }

        private bool IsValidPythonResponse(string response)
        {
            return !string.IsNullOrEmpty(response);
        }
        private void KeyProjectItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPanel.SelectedNode;
            if (selectedNode != null)
            {
                KeyForm keyForm = new KeyForm(selectedNode);
                keyForm.ShowDialog();
            }
        }
        private void LogMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPanel.SelectedNode;
            TreeNode parentNode = selectedNode.Parent;

            using (LogViewForm logViewForm = new LogViewForm(selectedNode, parentNode))
            {
                logViewForm.ShowDialog();
            }
        }
        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeViewPanel.SelectedNode = e.Node;
                if (e.Node != null)
                {
                    if (e.Node.Parent == null)
                    {
                        treeViewPanel.ContextMenuStrip = projectsContextMenu;
                    }
                    else if (e.Node.Parent.Text == "Projects")
                    {
                        treeViewPanel.ContextMenuStrip = createdProjectsContextMenu;
                    }
                    else
                    {
                        treeViewPanel.ContextMenuStrip = tableContextMenu;
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
            TreeNode selectedNode = treeViewPanel.SelectedNode;

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
                        // Pass selectedNode to DBLocationForm
                        DBLocationForm dbLocationForm = new DBLocationForm(selectedNode.Text, selectedNode, projectData, this);
                        dbLocationForm.ShowDialog();
                        dbLocationForm.Hide();
                    }
                    else if (selectedOption == "Json")
                    {
                        //CSVLocationForm jsonLocationForm = new CSVLocationForm(selectedNode.Text);
                        JsonLocationForm jsonLocationForm = new JsonLocationForm(selectedNode.Text);


                        if (jsonLocationForm.ShowDialog() == DialogResult.OK)
                        {
                            string tableName = jsonLocationForm.txtForTblName.Text;
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
                }
            }
        }

        public void SaveProjectData()
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
            TreeNode selectedNode = treeViewPanel.SelectedNode;

            if (selectedNode == null) return;

            if (selectedNode.Parent != null && selectedNode.Parent.Text == "Projects")
            {
                // Delete Project
                var project = projectData.Find(p => p.Name == selectedNode.Text);

                if (project != null)
                {
                    string projecrtname = selectedNode.Text;

                    string pythonScriptName = "DeleteProjectConnection.py";
                    string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                    string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

                    //string pythonfile = @"E:\DE-IDENTIFICATION TOOL\DE_IDENTIFICATION_TOOL\TestApp\PythonScripts\DeleteProjectConnection.py";

                    string pythonResponse = pythonService.DeleteProjectData(projecrtname, pythonScriptPath);

                    if (pythonResponse.ToLower().Contains("success"))
                    {
                        MessageBox.Show("Python response is" + pythonResponse);
                        projectData.Remove(project);
                    }
                    else
                    {
                        MessageBox.Show("Python response is not deidentified :" + pythonResponse);
                    }
                }
            }
            else if (selectedNode.Parent != null && selectedNode.Parent.Parent != null && selectedNode.Parent.Parent.Text == "Projects")
            {
                // Delete Table
                var project = projectData.Find(p => p.Name == selectedNode.Parent.Text);

                if (project != null)
                {
                    string projecrtname = selectedNode.Parent.Text;

                    string tablename = selectedNode.Text;

                    string pythonScriptName = "DeleteTableConnection.py";
                    string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                    string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

                    //string pythonfile = @"E:\DE-IDENTIFICATION TOOL\DE_IDENTIFICATION_TOOL\TestApp\PythonScripts\DeleteTableConnection.py";

                    string pythonResponse = pythonService.DeleteData(projecrtname, tablename, pythonScriptPath);

                    if (pythonResponse.ToLower().Contains("success"))
                    {
                        MessageBox.Show("table is deleted Successfully : " + pythonResponse);
                        project.Tables.Remove(selectedNode.Text);
                    }
                    else
                    {
                        MessageBox.Show("Table is not deleted" + pythonResponse);
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
            TreeNode selectedNode = treeViewPanel.SelectedNode;
            TreeNode parentNode = selectedNode.Parent;
            string tablename = selectedNode.Text;
            string projectName = parentNode.Text;

            string savePythonScriptName = "checkDeidentifiedTable.py";
            string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
            string savePythonScriptPath = Path.Combine(projectRootDirectory, savePythonScriptName);

            // Send data to Python script and capture the response
            //string savePythonResponse = pythonService.SendSqlDataToPython(server, DatabaseName, password, userId, projectName, Enterno, tableName, schemaName, savePythonScriptPath, jsonData);
            string savePythonResponse = pythonService.checkDeidentifiedTable(tablename, projectName, savePythonScriptPath);

            if (savePythonResponse.Contains("True"))
            {


                ExportForm deIdentifyForm = new ExportForm(tablename, projectName);
                deIdentifyForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("table is not didentify");
            }
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "projectData.json");
        }
    }
}

