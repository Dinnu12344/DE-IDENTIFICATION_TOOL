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
            btnForSave.Enabled = false; 
            textBoxForKey.TextChanged += TextBox1_TextChanged; 
            btnForSave.Click += SaveKeyData; 
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            btnForSave.Enabled = !string.IsNullOrWhiteSpace(textBoxForKey.Text);
        }

        //private void SaveKeyData(object sender, EventArgs e)
        //{
        //    string project = selectNode.Text;
        //    string projectDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeidentificationTool", project);

        //    try
        //    {
        //        Directory.CreateDirectory(projectDirectory);
        //        string filePath = Path.Combine(projectDirectory, "keys.txt");
        //        string textToSave = textBoxForKey.Text;
        //        File.WriteAllText(filePath, textToSave);
        //        MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //HomeForm form1 = new HomeForm();
        //        //form1.ShowDialog();
        //        this.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void SaveKeyData(object sender, EventArgs e)
        {
            string project = selectNode.Text;
            string projectDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeidentificationTool", project);

            try
            {
                // Ensure the directory exists
                Directory.CreateDirectory(projectDirectory);

                // Construct the file path
                string filePath = Path.Combine(projectDirectory, "keys.txt");

                // Get the text from the TextBox
                string textToSave = textBoxForKey.Text;

                // Write the text to the file
                File.WriteAllText(filePath, textToSave);

                // Inform the user of success
                MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close the current form
                this.Close();
            }
            catch (Exception ex)
            {
                // Inform the user of an error
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
