﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class CSVLocationForm : Form
    {
        public string SelectedCsvFilePath { get; set; }
        public string SelectedDelimiter { get; set; }
        public string SelectedQuote { get; set; }
        public string EnteredText { get; set; }
        public string TableName { get; set; }

        private readonly string labelName;
        private PythonService pythonService;

        public CSVLocationForm(string labelName)
        {
            InitializeComponent();
            pythonService = new PythonService();
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

            EnteredText = txtForNoofColumns.Text;
            UpdateFinishButtonVisibility();
        }
        private void TxtForTblName_TextChanged(object sender, EventArgs e)
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
        private void FinishButtonInCsvlocationWindow_Click(object sender, EventArgs e)
        {
            string projectName = labelName;

            if (!string.IsNullOrEmpty(SelectedCsvFilePath))
            {
                string pythonScriptPath = @"C:\Users\Dinesh Puvvala\source\repos\DE-IDENTIFICATION_TOOL_new\TestApp\PythonScripts\ImportCsvConnection.py"; 
                string pythonResponse = pythonService.SendDataToPython(SelectedCsvFilePath, projectName, TableName,SelectedDelimiter, SelectedQuote, EnteredText, pythonScriptPath);
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
