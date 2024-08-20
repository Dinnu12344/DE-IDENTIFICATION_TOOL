using DE_IDENTIFICATION_TOOL.Forms;
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

        public ExportForm(string tableName, string projectName)
        {
            InitializeComponent();
            pythonService = new PythonService();
            this.tableName = tableName; 
            this.projectName = projectName; 
        }
        public ExportForm(string projectName, bool check)
        {
            InitializeComponent();
            pythonService = new PythonService();
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
                            // Save the selected folder path for further use
                            string selectedFolderPath = folderBrowserDialog.SelectedPath;

                            string pythonScriptName = "ExportCsvConnection.py";
                            string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                            string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

                            string pythonResponse = pythonService.SendDataToPython(selectedFolderPath, projectName, tableName, pythonScriptPath);

                            if (pythonResponse.ToLower().Contains("success"))
                            {
                                MessageBox.Show("Successfully exported " + pythonResponse);
                                this.Close();
                            }
                            else
                            {

                                MessageBox.Show("the Export is not done" + pythonResponse);


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
                            // Save the selected folder path for further use
                            string selectedFolderPath = folderBrowserDialog.SelectedPath;

                            string pythonScriptName = "ExportAllCsv.py";
                            string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                            string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

                            string pythonResponse = pythonService.SendDataToPython(selectedFolderPath, projectName, pythonScriptPath);

                            if (pythonResponse.ToLower().Contains("success"))
                            {
                                MessageBox.Show("Successfully Exported  : " + pythonResponse);
                                this.Close();
                            }
                            else

                                MessageBox.Show("the Export is not done" + pythonResponse);

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

        //private void btnForNext_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (radioBtnForCsvExport.Checked)
        //        {
        //            string pythonScriptName;
        //            if (SelectedImportOption == "CSV" && check != true)
        //            {
        //                pythonScriptName = "ExportCsvConnection.py";
        //            }
        //            else
        //            {
        //                pythonScriptName = "ExportAllCsv.py";
        //            }

        //            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
        //            {
        //                folderBrowserDialog.Description = "Select the folder to save CSV file";
        //                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
        //                folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        //                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        //                {
        //                    string selectedFolderPath = folderBrowserDialog.SelectedPath;
        //                    string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory();
        //                    string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

        //                    string pythonResponse = pythonService.SendDataToPython(selectedFolderPath, projectName, tableName, pythonScriptPath);

        //                    if (pythonResponse.ToLower().Contains("success"))
        //                    {
        //                        MessageBox.Show("Successfully exported: " + pythonResponse);
        //                    }
        //                    else
        //                    {
        //                        ShowErrorMessage("Export failed. Please check your Python script and folder permissions.");
        //                    }
        //                }
        //            }
        //        }
        //        else if (radioBtnDatabaseExport.Checked)
        //        {
        //            SelectedImportOption = "Database";
        //            ExportDbForm dBLocationForm;
        //            if (check != true)
        //            {
        //                dBLocationForm = new ExportDbForm(projectName, tableName);
        //            }
        //            else
        //            {
        //                dBLocationForm = new ExportDbForm(projectName, check);
        //            }
        //            dBLocationForm.ShowDialog();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowErrorMessage($"An error occurred: {ex.Message}");
        //    }
        //    finally
        //    {
        //        this.DialogResult = DialogResult.OK;
        //        this.Close();
        //    }
        //}

        //private void ShowErrorMessage(string message)
        //{
        //    // Custom error message box with more options or context
        //    using (Form errorForm = new Form())
        //    {
        //        errorForm.Text = "Error";
        //        Label errorLabel = new Label
        //        {
        //            Text = message,
        //            AutoSize = true,
        //            Padding = new Padding(10)
        //        };
        //        Button okButton = new Button
        //        {
        //            Text = "OK",
        //            DialogResult = DialogResult.OK,
        //            Anchor = AnchorStyles.Bottom | AnchorStyles.Right
        //        };
        //        okButton.Click += (sender, e) => { errorForm.Close(); };

        //        errorForm.Controls.Add(errorLabel);
        //        errorForm.Controls.Add(okButton);
        //        errorForm.AcceptButton = okButton;
        //        errorForm.StartPosition = FormStartPosition.CenterParent;
        //        errorForm.AutoSize = true;
        //        errorForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;

        //        errorForm.ShowDialog(this);
        //    }
        //}

        private void btnForCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
