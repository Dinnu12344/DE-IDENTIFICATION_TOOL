using DE_IDENTIFICATION_TOOL.Models;
using DE_IDENTIFICATION_TOOL.Pythonresponse;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class CSVLocationForm : Form
    {
        private readonly string labelName;
        private PythonService pythonService;
        public readonly CSVLocationFormModel csvLocationFormModel;
        private readonly string pythonScriptsDirectory;

        public CSVLocationForm(string labelName)
        {
            InitializeComponent();
            pythonService = new PythonService();
            //pythonService = new PythonService();
            csvLocationFormModel = new CSVLocationFormModel();
            delimiterLabel.Visible = false;
            DelimeterComboBox.Visible = false;
            QuoteLabel.Visible = false;
            QuoteComboBox.Visible = false;
            finishButtonInCsvlocationWindow.Visible = false;
            lblForNoofColumns.Visible = false;
            txtForNoofColumns.Visible = false;
            lblForTblName.Visible = false;
            txtForTblName.Visible = false;

            this.labelName = labelName;
            pythonScriptsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PythonScripts");

        }

        private void LocationBrowsebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Update the model with the selected file path
                csvLocationFormModel.SelectedCsvFilePath = openFileDialog.FileName;
                textBoxForHoldingFilePath.Text = csvLocationFormModel.SelectedCsvFilePath;

                // Show other controls
                delimiterLabel.Visible = true;
                DelimeterComboBox.Visible = true;
                QuoteLabel.Visible = true;
                QuoteComboBox.Visible = true;
                lblForNoofColumns.Visible = true;
                txtForNoofColumns.Visible = true;
                lblForTblName.Visible = true;
                txtForTblName.Visible = true;
            }
        }
        private void DelimeterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            csvLocationFormModel.SelectedDelimiter = DelimeterComboBox.SelectedItem?.ToString();
            UpdateFinishButtonVisibility();
        }
        private void QuoteComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            csvLocationFormModel.SelectedQuote = QuoteComboBox.SelectedItem?.ToString();
            UpdateFinishButtonVisibility();
        }
        private void TxtForNoofColumns_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtForNoofColumns.Text, out int value))
            {
                MessageBox.Show("Please enter a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtForNoofColumns.Text = ""; 
                return;
            }

            if (value < 1 || value > 10000)
            {
                MessageBox.Show("Please enter a number between 1 and 10000.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtForNoofColumns.Text = ""; // Clear the text box
            }

            csvLocationFormModel.EnteredText = txtForNoofColumns.Text;
            UpdateFinishButtonVisibility();
        }
        private void TxtForTblName_TextChanged(object sender, EventArgs e)
        {
            csvLocationFormModel.TableName = txtForTblName.Text;
            UpdateFinishButtonVisibility();
        }
        private void UpdateFinishButtonVisibility()
        {
            finishButtonInCsvlocationWindow.Visible = !string.IsNullOrEmpty(csvLocationFormModel.SelectedDelimiter) &&
                                                      !string.IsNullOrEmpty(csvLocationFormModel.SelectedQuote) &&
                                                      !string.IsNullOrEmpty(csvLocationFormModel.EnteredText) &&
                                                      !string.IsNullOrEmpty(csvLocationFormModel.TableName);
        }
        private void FinishButtonInCsvlocationWindow_Click(object sender, EventArgs e)
        {
            string projectName = labelName;

            if (!string.IsNullOrEmpty(csvLocationFormModel.SelectedCsvFilePath))
            {
                string username = Environment.UserName;
                string directoryPath = $@"C:\Users\{username}\AppData\Roaming\DeidentificationTool\{projectName}\{csvLocationFormModel.TableName}\LogFile";
                // Ensure the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string pythonScriptName = "ImportCsvConnection.py";
                string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);
                string pythonResponse = pythonService.SendDataToPython(csvLocationFormModel.SelectedCsvFilePath, projectName, csvLocationFormModel.TableName,csvLocationFormModel.SelectedDelimiter, csvLocationFormModel.SelectedQuote, csvLocationFormModel.EnteredText, pythonScriptPath);
                
                if (pythonResponse.ToLower().Contains("success"))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("The CSV file is not valid. Error: " + pythonResponse, "Error");
                }
            }
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void BtnForBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
