using System;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class ImportForm : Form
    {
        public string SelectedImportOption { get; private set; }

        public ImportForm()
        {
            InitializeComponent();

            // Link event handlers to radio buttons
            radioCheckCSV.CheckedChanged += CheckBox_CheckedChanged;
            radioCheckDatabase.CheckedChanged += CheckBox_CheckedChanged;
            radioCheckJson.CheckedChanged += CheckBox_CheckedChanged;

            // Link the Next button's Click event
            buttonNext.Click += NextButton_Click;

            // Link the Cancel button's Click event
            buttonCancel.Click += CancelButton_Click;

            // Initialize the form's state
            buttonNext.Enabled = false;  // Disable Next button initially
        }

        // Method to reset the form's state
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Enable the Next button if any radio button is checked
            buttonNext.Enabled = radioCheckCSV.Checked || radioCheckDatabase.Checked || radioCheckJson.Checked;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (radioCheckCSV.Checked)
            {
                SelectedImportOption = "CSV";
            }
            else if (radioCheckDatabase.Checked)
            {
                SelectedImportOption = "Database";
            }
            else if (radioCheckJson.Checked)
            {
                SelectedImportOption = "Json";
            }

            this.DialogResult = DialogResult.OK;  // Set result to OK to indicate successful selection
            this.Close();  // Close the form (does not dispose it unless explicitly done elsewhere)
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;  // Set result to Cancel to indicate user cancellation
            this.Close();  // Close the form
        }
    }
}
