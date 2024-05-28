using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    //public partial class ImportForm : Form
    //{
    //    private string labelName;
    //    public bool IsCSVChecked { get; private set; }
    //    public bool IsDatabaseChecked { get; private set; }
    //    public string SelectedImportOption { get; internal set; }

    //    public ImportForm(string text)
    //    {
    //        InitializeComponent();
    //    }

    //    private void CheckBox_CheckedChanged(object sender, EventArgs e)
    //    {
    //        buttonNext.Enabled = checkBoxCSV.Checked || checkBoxDatabase.Checked;
    //    }

    //    private void ButtonNext_Click(object sender, EventArgs e)
    //    {
    //        IsCSVChecked = checkBoxCSV.Checked;
    //        IsDatabaseChecked = checkBoxDatabase.Checked;

    //        if (IsCSVChecked)
    //        {
    //            // Navigate to CSV Import form
    //            CSVLocationForm csvForm = new CSVLocationForm(labelName);
    //            csvForm.ShowDialog();
    //        }
    //        else if (IsDatabaseChecked)
    //        {
    //            // Navigate to Database Import form
    //            //DatabaseImportForm dbForm = new DatabaseImportForm();
    //            //dbForm.ShowDialog();
    //        }
    //    }
    //    private void ButtonBack_Click(object sender, EventArgs e)
    //    {
    //        this.Close();
    //    }

    //    private void ButtonCancel_Click(object sender, EventArgs e)
    //    {
    //        this.Close();
    //    }

    //    private CheckBox checkBoxCSV;
    //    private CheckBox checkBoxDatabase;
    //    private Button buttonNext;
    //    private Button buttonBack;
    //    private Button buttonCancel;
    //}

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
        private void nextButton_Click(object sender, EventArgs e)
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

        private void cancelButton_Click(object sender, EventArgs e)
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
        //private Button buttonBack;
        private Button buttonCancel;
    }
}
