using System;
using System.IO;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class KeyForm : Form
    {
        private TreeNode selectNode;

        public KeyForm(TreeNode parentNode)
        {
            selectNode = parentNode;
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            button1.Enabled = false; 
            textBox1.TextChanged += TextBox1_TextChanged; 
            button1.Click += SaveKeyData; 
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text);
        }

        private void SaveKeyData(object sender, EventArgs e)
        {
            string project = selectNode.Text;
            string projectDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeidentificationTool", project);

            try
            {
                Directory.CreateDirectory(projectDirectory);
                string filePath = Path.Combine(projectDirectory, "keys.txt");
                string textToSave = textBox1.Text;
                File.WriteAllText(filePath, textToSave);
                MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //HomeForm form1 = new HomeForm();
                //form1.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
