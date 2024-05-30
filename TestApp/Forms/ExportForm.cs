using System;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class ExportForm : Form
    {
        private string tablename;
        private string projectName;
        private PythonService pythonService;

        public ExportForm(string tablename, string projectName)
        {
            InitializeComponent();
            pythonService = new PythonService();
            this.tablename = tablename; // Corrected field name
            this.projectName = projectName; // Corrected field name
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

                        // Add your code here to export the data to the selected folder path
                        string pythonScriptPath = @"C:\Users\Satya Pulamanthula\Desktop\PythonScriptsGit\ConnectionTestRepo_New\ConnectionTestRepo\ExportCsvConnection.py";
                        string pythonResponse = pythonService.SendDataToPython(selectedFolderPath, projectName, tablename, pythonScriptPath);
                        MessageBox.Show("The exported path is : " + pythonResponse);
                        this.Close();
                    }
                }
            }
            else if (radioBtnDatabaseExport.Checked)
            {
                SelectedImportOption = "Database";
                // Add your database export logic here
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
