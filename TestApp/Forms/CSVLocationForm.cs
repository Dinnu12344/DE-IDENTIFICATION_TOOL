using DE_IDENTIFICATION_TOOL.Forms;
using DE_IDENTIFICATION_TOOL.Models;
using DE_IDENTIFICATION_TOOL.Pythonresponse;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class CSVLocationForm : Form
    {
        private const int MaxTextBoxWidth = 500;
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
            this.Resize += new EventHandler(CSVLocationForm_Resize);
        }
        private void CSVLocationForm_Resize(object sender, EventArgs e)
        {
            AdjustTextBoxWidth();
        }
        private void AdjustTextBoxWidth()
        {
            // Adjust the width of textBoxForHoldingFilePath
            int availableWidth = this.ClientSize.Width - (LocationBrowseButton.Width + 30); // Add a buffer for padding

            // Set the width of the textbox, making sure it doesn't exceed the maximum allowed width
            textBoxForHoldingFilePath.Width = Math.Min(MaxTextBoxWidth, availableWidth);

            // Adjust the width of other controls similarly if necessary
            DelimeterComboBox.Width = textBoxForHoldingFilePath.Width;
            QuoteComboBox.Width = textBoxForHoldingFilePath.Width;
            txtForTblName.Width = textBoxForHoldingFilePath.Width;

            // Optionally, you can adjust the position of the Browse button if necessary
            LocationBrowseButton.Left = textBoxForHoldingFilePath.Right + 10; // Add padding between the textbox and button
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
            txtForNoofColumns.TextChanged -= TxtForNoofColumns_TextChanged;

            bool isValid = true;
            string errorMessage = string.Empty;

            if (!int.TryParse(txtForNoofColumns.Text, out int value))
            {
                errorMessage = "Please enter a valid number.";
                isValid = false;
            }
            else if (value < 1 || value > 10000)
            {
                errorMessage = "Please enter a number between 1 and 10000.";
                isValid = false;
            }

            if (!isValid)
            {
                MessageBox.Show(errorMessage, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtForNoofColumns.Text = ""; 
            }
            else
            {
                csvLocationFormModel.EnteredText = txtForNoofColumns.Text;
                UpdateFinishButtonVisibility();
            }

            txtForNoofColumns.TextChanged += TxtForNoofColumns_TextChanged;
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
                string projectDirectory = $@"C:\Users\{username}\AppData\Roaming\DeidentificationTool\{projectName}";
                string directoryPath = Path.Combine(projectDirectory, csvLocationFormModel.TableName, "LogFile");

                // File to store the list of table names for the specific project
                string tableNamesFile = Path.Combine(projectDirectory, "TableNames.txt");

                // Ensure the table name is trimmed of leading/trailing whitespace
                string enteredTableName = csvLocationFormModel.TableName.Trim();

                // Check if the table name already exists in the current project (case-insensitive comparison)
                if (File.Exists(tableNamesFile))
                {
                    var existingTableNames = File.ReadAllLines(tableNamesFile)
                                                 .Select(name => name.Trim())
                                                 .ToList();

                    if (existingTableNames.Any(name => string.Equals(name, enteredTableName, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show($"Table name '{enteredTableName}' already exists in project '{projectName}'. Please try another name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Prevent the form from closing
                    }
                }

                // Create directory if it doesn't exist
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Save the new table name to the project's TableNames.txt
                File.AppendAllLines(tableNamesFile, new[] { enteredTableName });

                // Path to the Python script
                string pythonScriptName = "ImportCsvConnection.py";
                string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);

                // Call Python service
                string pythonResponse = pythonService.SendDataToPython(
                    csvLocationFormModel.SelectedCsvFilePath,
                    projectName,
                    enteredTableName,
                    csvLocationFormModel.SelectedDelimiter,
                    csvLocationFormModel.SelectedQuote,
                    csvLocationFormModel.EnteredText,
                    pythonScriptPath
                );

                if (pythonResponse.ToLower().Contains("success"))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("The Python response failed. Error: " + pythonResponse, "Error");
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

        private void CSVLocationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
