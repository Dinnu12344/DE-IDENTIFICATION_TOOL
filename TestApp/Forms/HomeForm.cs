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
using System.Diagnostics;

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

            createdProjectsContextMenu = new ContextMenuStrip();
            ToolStripMenuItem editProjectItem = new ToolStripMenuItem("Import");
            ToolStripMenuItem keyProjectItem = new ToolStripMenuItem("Key");
            ToolStripMenuItem renameProjectItem = new ToolStripMenuItem("Rename");
            ToolStripMenuItem refreshProjectItem = new ToolStripMenuItem("Refresh");
            ToolStripMenuItem deleteProjectItem = new ToolStripMenuItem("Delete");
            ToolStripMenuItem exportAllProjectItem = new ToolStripMenuItem("ExportAll");
            createdProjectsContextMenu.Items.Add(exportAllProjectItem); 
            editProjectItem.Click += ImportProjectItem_Click;
            deleteProjectItem.Click += DeleteProjectItem_Click;
            keyProjectItem.Click += KeyProjectItem_Click;
            renameProjectItem.Click += ReNameProjectItem_Click;
            exportAllProjectItem.Click += ExportAllProjectItem_Click;
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

        private void ExportAllProjectItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPanel.SelectedNode;
            //throw new NotImplementedException();
            DbtableFormModel dbtableFormModel = new DbtableFormModel();
            //dbtableFormModel.exportAll = true;
            ExportForm exportForm = new ExportForm(selectedNode.Text,true);
            exportForm.Show();

        }

        private void ConfigMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPanel.SelectedNode;
            if (selectedNode != null)
            {
                TreeNode parentNode = selectedNode.Parent;
                if (parentNode != null) 
                {
                    string pythonScriptName = "TableColumnsConnection.py";
                    string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                    string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

                    Console.WriteLine("Python Script Path: " + pythonScriptPath);

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
            reNameForm.ShowDialog(); 
        }

        private void ReNameTableItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPanel.SelectedNode;
            TreeNode parentNode = selectedNode.Parent;
            ReNameForm reNameForm = new ReNameForm(selectedNode, parentNode);
            reNameForm.ShowDialog(); 
        }

        private void DeIdentifyMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPanel.SelectedNode;
            TreeNode parentnode = selectedNode.Parent;
            if (selectedNode != null)
            {
                string pythonScriptName = "DeidentificationConnection.py";
                string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); 
                string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);
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
            string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); 
            string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);
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
            string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); 
            string savePythonScriptPath = Path.Combine(projectRootDirectory, savePythonScriptName);
            string savePythonResponse = pythonService.checkDeidentifiedTable(selectedNode.Text, parentnode.Text, savePythonScriptPath);

            if (savePythonResponse.Contains("True"))
            {


                string pythonScriptName = "ViewDeidentifiedDataConnection.py";
                projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); 
                string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);
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
                        DBLocationForm dbLocationForm = new DBLocationForm(selectedNode.Text, selectedNode, projectData, this);
                        dbLocationForm.ShowDialog();
                        dbLocationForm.Hide();
                    }
                    else if (selectedOption == "Json")
                    {
                        JsonLocationForm jsonLocationForm = new JsonLocationForm(selectedNode.Text, selectedNode, projectData, this);

                        if (jsonLocationForm.ShowDialog() == DialogResult.OK)
                        {
                            selectedNode.Expand();

                            var project = projectData.Find(p => p.Name == selectedNode.Text);
                            if (project != null)
                            {
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
                projectData = new List<ProjectData>(); 
            }
        }

        private void RefreshItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Refreshing...");
        }

        private void DeleteSelectedNode()
        {
            TreeNode selectedNode = treeViewPanel.SelectedNode;

            if (selectedNode == null) return;

            if (selectedNode.Parent != null && selectedNode.Parent.Text == "Projects")
            {
                var project = projectData.Find(p => p.Name == selectedNode.Text);

                if (project != null)
                {
                    string projecrtname = selectedNode.Text;

                    string pythonScriptName = "DeleteProjectConnection.py";
                    string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                    string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);
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
            // Define the path for the exported data
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "projectData.json");

            // Example usage of FolderBrowserDialog to select export location
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select the folder to save exported data";
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected folder path
                    string selectedFolderPath = folderBrowserDialog.SelectedPath;

                    // Construct the full path for the JSON export
                    string exportFilePath = Path.Combine(selectedFolderPath, "projectData.json");

                    try
                    {
                        // Assuming ExportService is a class that handles data export
                        // You need to implement this according to your specific requirements
                        //ExportAllData exportService = new ExportAllData();

                        // Call the method to export all data to the specified path
                        ExportAllData(exportFilePath);

                        // Notify the user of success
                        MessageBox.Show($"All tables data has been successfully exported to {exportFilePath}");
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that occurred during the export process
                        MessageBox.Show($"An error occurred while exporting data: {ex.Message}");
                    }
                }
            }
        }
        public void ExportAllData(string exportFilePath)
        {
            // Implement logic to export all tables' data
            // For example, write to a JSON file, CSV files, etc.

            // Example:
            // - Retrieve all tables from your data source.
            // - Serialize each table's data.
            // - Write the data to the specified exportFilePath.

            // This is a placeholder for actual export logic
            File.WriteAllText(exportFilePath, "All data exported.");
        }
        private void userManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string userManualPath = Path.Combine(appDirectory, "Documentation", "UserManual.docx");

            if (!File.Exists(userManualPath))
            {
                MessageBox.Show("User manual file not found. Please make sure the file exists in the 'Documentation' folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo(userManualPath) { UseShellExecute = true });
            }
            catch (Exception)
            {
                MessageBox.Show("Could not open the user manual. Please make sure the file exists and you have the necessary permissions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

