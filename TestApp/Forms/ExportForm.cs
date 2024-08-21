﻿using DE_IDENTIFICATION_TOOL.Forms;
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
        private bool check;

        public ExportForm(string tableName, string projectName, bool check)
        {
            InitializeComponent();
            pythonService = new PythonService();
            this.tableName = tableName; 
            this.projectName = projectName;
            this.check = check;
        }
        public string SelectedImportOption { get; set; }
        private void btnForNext_Click(object sender, EventArgs e)
        {
            if (radioBtnForCsvExport.Checked)
            {
                if (check != true)
                {
                    using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                    {
                        folderBrowserDialog.Description = "Select the folder to save CSV file";
                        folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                        folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            string selectedFolderPath = folderBrowserDialog.SelectedPath;

                            string pythonScriptName = "ExportCsvConnection.py";
                            string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                            string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

                            string pythonResponse = pythonService.SendDataToPython(selectedFolderPath, projectName, tableName, pythonScriptPath);

                            if (pythonResponse.ToLower().Contains("success"))
                            {
                                MessageBox.Show("De-Identified table exported successfully");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Export is failed");
                            }

                        }
                    }
                }
                else
                {
                    using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                    {
                        folderBrowserDialog.Description = "Select the folder to save CSV file";
                        folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                        folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            string selectedFolderPath = folderBrowserDialog.SelectedPath;
                            string pythonScriptName = "ExportAllCsv.py";
                            string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                            string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);
                            string pythonResponse = pythonService.SendDataToPython(selectedFolderPath, projectName, pythonScriptPath);
                            if (pythonResponse.ToLower().Contains("success"))
                            {
                                MessageBox.Show("De-Identified tables exported successfully..");
                                this.Close();
                            }
                            else

                                MessageBox.Show(pythonResponse);
                            }

                        }
                    }
                }
            
            else if (radioBtnDatabaseExport.Checked)
            {
                SelectedImportOption = "Database";
                if (SelectedImportOption == "Database" && check != true)
                {
                    ExportDbForm dBLocationForm = new ExportDbForm(projectName, tableName);
                    dBLocationForm.ShowDialog();
                }
                if (SelectedImportOption == "Database" && check == true)
                {
                    ExportDbForm dBLocationForm = new ExportDbForm(projectName, check);
                    dBLocationForm.ShowDialog();
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnForCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
