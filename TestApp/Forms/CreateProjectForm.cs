using System;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class CreateProjectForm : Form
    {
        public string ProjectName { get; private set; }
        public CreateProjectForm()
        {
            InitializeComponent();
        }
        private void btnCreateProject_Click(object sender, EventArgs e)
        {
            // Create the project and close the form
            ProjectName = txtProjectName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
