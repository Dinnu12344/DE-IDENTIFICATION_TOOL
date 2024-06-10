using System;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class ImportForm : Form
    {
        public string SelectedImportOption { get; set; }

        public ImportForm(string projectName)
        {
            InitializeComponent();
            this.Text = $"Import for {projectName}";
        }
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            buttonNext.Enabled = radioCheckCSV.Checked || radioCheckDatabase.Checked;
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

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void ButtonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private RadioButton radioCheckCSV;
        private RadioButton radioCheckDatabase;
        private Button buttonNext;
        private Button buttonCancel;
    }
}
