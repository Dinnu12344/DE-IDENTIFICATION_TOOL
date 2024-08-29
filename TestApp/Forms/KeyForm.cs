using System;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class KeyForm : Form
    {
        private Button btnCancel;
        private TreeNode selectNode;
        private bool isExistingKey;

        public KeyForm(TreeNode parentNode)
        {
            selectNode = parentNode;
            InitializeComponent();
            InitializeForm();
            LoadExistingKeyData();
        }

        private void InitializeForm()
        {
            textBoxForKey.TextChanged += TextBoxForKey_TextChanged;
            btnForSave.Click -= SaveKeyData;
            btnForSave.Click += SaveKeyData;
            btnCancel.Click -= CancelButton_Click;
            btnCancel.Click += CancelButton_Click;
        }

        private void TextBoxForKey_TextChanged(object sender, EventArgs e)
        {
            if (!isExistingKey)
            {
                btnForSave.Enabled = !string.IsNullOrWhiteSpace(textBoxForKey.Text);
            }
        }

        private void SaveKeyData(object sender, EventArgs e)
        {
            string project = selectNode.Text;
            string projectDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeidentificationTool", project);

            try
            {
                Directory.CreateDirectory(projectDirectory);
                string filePath = Path.Combine(projectDirectory, "keys.txt");
                string textToSave = textBoxForKey.Text;
                File.WriteAllText(filePath, textToSave);

                MessageBox.Show("Key saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadExistingKeyData()
        {
            string project = selectNode.Text;
            string projectDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeidentificationTool", project);
            string filePath = Path.Combine(projectDirectory, "keys.txt");

            if (File.Exists(filePath))
            {
                try
                {
                    string existingKeyData = File.ReadAllText(filePath);
                    textBoxForKey.Text = existingKeyData;
                    textBoxForKey.ReadOnly = true;
                    btnForSave.Enabled = false;
                    isExistingKey = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading existing key data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                textBoxForKey.ReadOnly = false;
                btnForSave.Enabled = false;
                isExistingKey = false;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}