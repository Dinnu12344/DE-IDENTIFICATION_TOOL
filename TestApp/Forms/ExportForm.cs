using DE_IDENTIFICATION_TOOL.Forms;
using DE_IDENTIFICATION_TOOL.Models;
using DE_IDENTIFICATION_TOOL.Pythonresponse;
using System;
using System.IO;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class ExportForm : Form
    {
        private string tableName;
        private string projectName;
        private PythonService pythonService;

        public ExportForm(string tableName, string projectName)
        {
            InitializeComponent();
            pythonService = new PythonService();
            this.tableName = tableName; 
            this.projectName = projectName; 
        }
        public string SelectedImportOption { get; set; }
        private void btnForNext_Click(object sender, EventArgs e)
        {
            if (radioBtnForCsvExport.Checked)
            {
                SelectedImportOption = "CSV";

                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.Description = "Select the folder to save CSV file";
                    folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                    folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Save the selected folder path for further use
                        string selectedFolderPath = folderBrowserDialog.SelectedPath;

                        string pythonScriptName = "ExportCsvConnection.py";
                        string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                        string pythonScriptPath = Path.Combine(projectRootDirectory, "PythonScripts", pythonScriptName);

                        string pythonResponse = pythonService.SendDataToPython(selectedFolderPath, projectName, tableName, pythonScriptPath);

                        if (pythonResponse.ToLower().Contains("success"))
                        {
                            MessageBox.Show("The exporte is success and the export path is : " + pythonResponse);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("the Export is not done");
                        }
                        
                    }
                }
            }
            else if (radioBtnDatabaseExport.Checked)
            {
                SelectedImportOption = "Database";
                if (SelectedImportOption == "Database")
                {
                    ExportDbForm dBLocationForm = new ExportDbForm(projectName, tableName);
                    dBLocationForm.ShowDialog();
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
