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
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
