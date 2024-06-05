﻿using System;
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

                        // Add your code here to export the data to the selected folder path
                        string pythonScriptPath = @"Add path of export connection";
                        string pythonResponse = pythonService.SendDataToPython(selectedFolderPath, projectName, tableName, pythonScriptPath);
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
