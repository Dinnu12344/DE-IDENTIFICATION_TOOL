using System;
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

            // Event handler for when text changes in the project name text box
            txtProjectName.TextChanged += TxtProjName_TextChanged;

            // Initially disable the Create Project button until valid input is provided
            btnCreateProject.Enabled = false;

            // Set the AcceptButton property to handle Enter key press
            this.AcceptButton = btnCreateProject;
        }

        private void TxtProjName_TextChanged(object sender, EventArgs e)
        {
            // Enable the Create Project button if the text box is not empty or whitespace
            btnCreateProject.Enabled = !string.IsNullOrWhiteSpace(txtProjectName.Text);
        }

        private void BtnCreateProject_Click(object sender, EventArgs e)
        {
            // Get the project name from the text box
            ProjectName = txtProjectName.Text.Trim();
            string username = Environment.UserName;

            // Define the directory path using the project name
            string directoryPath = $@"C:\Users\{username}\AppData\Roaming\DeidentificationTool\{ProjectName}";

            if (Directory.Exists(directoryPath))
            {
                // Show an error message if the directory already exists
                MessageBox.Show("A project with this name already exists. Please choose a different name.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                // Clear the text box and set focus to it
                txtProjectName.Clear();
                txtProjectName.Focus();
            }
            else
            {
                try
                {
                    // Create the directory if it does not exist
                    Directory.CreateDirectory(directoryPath);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that might occur during directory creation
                    MessageBox.Show($"An error occurred while creating the project: {ex.Message}",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }
    }
}
