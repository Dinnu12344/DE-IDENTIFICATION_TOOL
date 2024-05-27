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

namespace TestApp
{
    public partial class CSVLocationForm : Form
    {
        public string SelectedCsvFilePath { get; private set; }
        public string SelectedDelimiter { get; private set; }
        public string SelectedQuote { get; private set; }
        public string EnteredText { get; private set; }
        public string TableName { get; private set; }

        private readonly string labelName;

        public CSVLocationForm(string labelName)
        {
            InitializeComponent();
            // Initialize controls visibility and other settings
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
        }

        private void LocationBrowsebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedCsvFilePath = openFileDialog.FileName;
                textBoxForHoldingFilePath.Text = SelectedCsvFilePath;
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
            SelectedDelimiter = DelimeterComboBox.SelectedItem?.ToString();
            UpdateFinishButtonVisibility();
        }

        private void QuoteComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedQuote = QuoteComboBox.SelectedItem?.ToString();
            UpdateFinishButtonVisibility();
        }

        private void txtForNoofColumns_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtForNoofColumns.Text, out int value))
            {
                MessageBox.Show("Please enter a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtForNoofColumns.Text = ""; // Clear the text box
                return;
            }

            if (value < 1 || value > 10000)
            {
                MessageBox.Show("Please enter a number between 1 and 10000.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtForNoofColumns.Text = ""; // Clear the text box
            }

            EnteredText = txtForNoofColumns.Text;
            UpdateFinishButtonVisibility();
        }

        private void txtForTblName_TextChanged(object sender, EventArgs e)
        {
            TableName = txtForTblName.Text;
            UpdateFinishButtonVisibility();
        }

        private void UpdateFinishButtonVisibility()
        {
            finishButtonInCsvlocationWindow.Visible = !string.IsNullOrEmpty(SelectedDelimiter) &&
                                                      !string.IsNullOrEmpty(SelectedQuote) &&
                                                      !string.IsNullOrEmpty(EnteredText) &&
                                                      !string.IsNullOrEmpty(TableName);
        }

        private void finishButtonInCsvlocationWindow_Click(object sender, EventArgs e)
        {
            string projectName = labelName;

            if (!string.IsNullOrEmpty(SelectedCsvFilePath))
            {
                // Simulate sending data to Python script
                string pythonResponse = SendDataToPython(SelectedCsvFilePath, projectName, "deIdLogfile", SelectedQuote, SelectedDelimiter, EnteredText, TableName);
                
                // Check the response from the Python script
                if (pythonResponse.ToLower().Contains("success"))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
                else
                {
                    // Display error message
                    MessageBox.Show("The CSV file is not valid. Error: " + pythonResponse, "Error");
                }
            }
        }

        private string SendDataToPython(string filePath, string projectName, string logFile, string quote, string delimiter, string noOfRows, string tableName)
        {
            string pythonScriptPath = @"C:\Users\Satya Pulamanthula\Downloads\ConnectionTest\De-identification\ImportConnection.py";

            if (!File.Exists(pythonScriptPath))
            {
                return "Error: Python script file not found.";
            }

            var command = $"\"{pythonScriptPath}\" \"{filePath}\" \"{projectName}\" \"{noOfRows}\" \"{tableName}\" \"{delimiter}\" \"{quote}\"";

            using (Process process = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.FileName = GetPythonExePath();
                startInfo.Arguments = command;
                startInfo.StandardOutputEncoding = Encoding.UTF8;

                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return output;
            }
        }

        private string GetPythonExePath()
        {
            string pythonExeName = "python.exe";
            string pythonExePath = null;

            string[] paths = Environment.GetEnvironmentVariable("PATH").Split(';');
            foreach (string path in paths)
            {
                string fullPath = Path.Combine(path, pythonExeName);
                if (File.Exists(fullPath))
                {
                    pythonExePath = fullPath;
                    break;
                }
            }

            return pythonExePath;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnForBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
