using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class DeIdentifyForm : Form
    {
        private string tablename;
        private string projectName;
        private PythonService pythonService;

        public DeIdentifyForm(string tablename, string projectName)
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
                        //string response = ExportDataToCsv(selectedFolderPath, projectName, tablename);
                        string pythonScriptPath = @"C:\Users\Satya Pulamanthula\Desktop\PythonScriptsGit\ConnectionTestRepo\ExportCsvConnection.py";
                        string pythonResponse = pythonService.SendDataToPython(selectedFolderPath, projectName, tablename, pythonScriptPath);
                        MessageBox.Show("The exported path is : ", pythonResponse);
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


        //private string ExportDataToCsv(string filePath, string projectName, string tablename)
        //{ // Path to the Python executable
            

        //        string pythonScriptPath = @"C:\Users\Satya Pulamanthula\Desktop\PythonScriptsGit\ConnectionTestRepo\ExportCsvConnection.py";

        //        if (!File.Exists(pythonScriptPath))
        //        {
        //            return "Error: Python script file not found.";
        //        }

        //        var command = $"\"{pythonScriptPath}\" \"{filePath}\" \"{projectName}\" \"{tablename}\"";

        //        using (Process process = new Process())
        //        {
        //            ProcessStartInfo startInfo = new ProcessStartInfo();
        //            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //            startInfo.CreateNoWindow = true;
        //            startInfo.UseShellExecute = false;
        //            startInfo.RedirectStandardOutput = true;
        //            startInfo.FileName = GetPythonExePath();
        //            startInfo.Arguments = command;
        //            startInfo.StandardOutputEncoding = Encoding.UTF8;

        //            process.StartInfo = startInfo;
        //            process.Start();

        //            string output = process.StandardOutput.ReadToEnd();
        //            process.WaitForExit();

        //            return output;
        //        }
        //}
        //private string GetPythonExePath()
        //{
        //    string pythonExeName = "python.exe";
        //    string pythonExePath = null;

        //    string[] paths = Environment.GetEnvironmentVariable("PATH").Split(';');
        //    foreach (string path in paths)
        //    {
        //        string fullPath = Path.Combine(path, pythonExeName);
        //        if (File.Exists(fullPath))
        //        {
        //            pythonExePath = fullPath;
        //            break;
        //        }
        //    }

        //    return pythonExePath;
        //}
    }
}
