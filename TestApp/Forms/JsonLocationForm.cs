using DE_IDENTIFICATION_TOOL.Models;
using DE_IDENTIFICATION_TOOL.Pythonresponse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class JsonLocationForm : Form
    {
        public readonly JsonLocationFormModel jsonLocationFormModel;
        private readonly string labelName;
        private PythonService pythonService;
        public JsonLocationForm(string labelName)
        {
            InitializeComponent();
            pythonService = new PythonService();
            ////pythonService = new PythonService();
            //csvLocationFormModel = new CSVLocationFormModel();
            //delimiterLabel.Visible = false;
            //DelimeterComboBox.Visible = false;
            //QuoteLabel.Visible = false;
            //QuoteComboBox.Visible = false;
            jsonLocationFormModel = new JsonLocationFormModel();
            //finishButtonInCsvlocationWindow.Visible = false;
            lblForNoofColumns.Visible = false;
            txtForNoofColumns.Visible = false;
            lblForTblName.Visible = false;
            txtForTblName.Visible = false;

            this.labelName = labelName;
            //pythonScriptsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PythonScripts");
        }


        //public CSVLocationForm(string labelName)
        //{
        //    InitializeComponent();
        //    pythonService = new PythonService();
        //    //pythonService = new PythonService();
            
        //    lblForNoofColumns.Visible = false;
        //    txtForNoofColumns.Visible = false;
        //    lblForTblName.Visible = false;
        //    txtForTblName.Visible = false;

        //    this.labelName = labelName;
        //    pythonScriptsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PythonScripts");

        //}

        private void txtForNoofColumns_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblForTblName_Click(object sender, EventArgs e)
        {

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

            jsonLocationFormModel.EnteredText = txtForNoofColumns.Text;
            UpdateFinishButtonVisibility();
        }
        private void TxtForTblName_TextChanged(object sender, EventArgs e)
        {
            jsonLocationFormModel.TableName = txtForTblName.Text;
            UpdateFinishButtonVisibility();
        }
        private void UpdateFinishButtonVisibility()
        {
            finishButtonInCsvlocationWindow.Visible = !string.IsNullOrEmpty(jsonLocationFormModel.EnteredText)&&
                                                      !string.IsNullOrEmpty(jsonLocationFormModel.TableName);
        }

        private void FinishButtonInCsvlocationWindow_Click(object sender, EventArgs e)
        {
            string projectName = labelName;

            if (!string.IsNullOrEmpty(jsonLocationFormModel.SelectedCsvFilePath))
            {
                string username = Environment.UserName;
                string directoryPath = $@"C:\Users\{username}\AppData\Roaming\DeidentificationTool\{projectName}\{jsonLocationFormModel.TableName}\LogFile";
                // Ensure the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string pythonScriptName = "ImportJsonConnection.py";
                string projectRootDirectory = PythonScriptFilePath.FindProjectRootDirectory(); // Use the class name to call the static method
                string pythonScriptPath = Path.Combine(projectRootDirectory, pythonScriptName);
                string pythonResponse = pythonService.JsonImport(jsonLocationFormModel.SelectedCsvFilePath, projectName, jsonLocationFormModel.TableName, jsonLocationFormModel.EnteredText, pythonScriptPath);

                if (pythonResponse.ToLower().Contains("success"))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("The python response is failed. Error: " + pythonResponse, "Error");
                }
            }

        }
        

        private void LocationBrowsebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";

            openFileDialog.FilterIndex = 1;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Update the model with the selected file path
                jsonLocationFormModel.SelectedCsvFilePath = openFileDialog.FileName;
                textBoxForHoldingFilePath.Text = jsonLocationFormModel.SelectedCsvFilePath;
                

                //// Show other controls
                //delimiterLabel.Visible = true;
                //DelimeterComboBox.Visible = true;
                //QuoteLabel.Visible = true;
                //QuoteComboBox.Visible = true;
                lblForNoofColumns.Visible = true;
                txtForNoofColumns.Visible = true;
                lblForTblName.Visible = true;
                txtForTblName.Visible = true;
            }
        }
    }
}
