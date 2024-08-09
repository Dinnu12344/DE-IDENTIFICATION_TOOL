﻿using System;
using System.IO;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class CreateProjectForm : Form
    {
        public string ProjectName { get; private set; }
        public CreateProjectForm()
        {
            InitializeComponent();
            txtProjectName.TextChanged += TxtProjName_TextChanged;
            btnCreateProject.Enabled = false;
        }
        private void TxtProjName_TextChanged(object sender, EventArgs e)
        {
            btnCreateProject.Enabled = !string.IsNullOrWhiteSpace(txtProjectName.Text);
        }
        private void BtnCreateProject_Click(object sender, EventArgs e)
        {
            ProjectName = txtProjectName.Text;
            string username = Environment.UserName;
            string directoryPath = $@"C:\Users\{username}\AppData\Roaming\DeidentificationTool\{ProjectName}";

            if (Directory.Exists(directoryPath))
            {
                // Show an error message if the directory already exists
                MessageBox.Show("A project with this name already exists. Please choose a different name.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                // Clear the text box
                txtProjectName.Clear();
            }
            else
            {
                // Create the directory if it does not exist
                Directory.CreateDirectory(directoryPath);
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
