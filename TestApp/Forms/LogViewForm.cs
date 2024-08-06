using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class LogViewForm : Form
    {
        private TreeNode tableName;
        private TreeNode projectName;
        private string logDirectoryPath;

        private Dictionary<DateTime, string> logs = new Dictionary<DateTime, string>();
        private bool shouldClose = false;

        public LogViewForm(TreeNode selectedNode, TreeNode parentNode)
        {
            InitializeComponent();
            tableName = selectedNode;
            projectName = parentNode;

            string projectNameText = projectName.Text;
            string tableNameText = tableName.Text;
            logDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeidentificationTool", projectNameText, tableNameText, "LogFile");

            if (Directory.Exists(logDirectoryPath))
            {
                ReadLogsFromDirectory();
            }
            else
            {
                MessageBox.Show($"Log directory for project '{projectNameText}' and table '{tableNameText}' does not exist at '{logDirectoryPath}'.", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                shouldClose = true;
            }

            PopulateDateList();

            dateListBox.SelectedIndexChanged += DateListBox_SelectedIndexChanged;
            this.Resize += LogViewForm_Resize; // Handle the Resize event
        }

        private void ReadLogsFromDirectory()
        {
            try
            {
                string[] logFiles = Directory.GetFiles(logDirectoryPath, "*.log");

                foreach (string logFile in logFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(logFile);

                    if (DateTime.TryParseExact(fileName, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime logDate))
                    {
                        string logContent = File.ReadAllText(logFile);
                        logs[logDate] = logContent;
                    }
                    else
                    {
                        MessageBox.Show($"Improperly formatted log file name: {fileName}", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        shouldClose = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading log files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                shouldClose = true;
            }
        }

        private void PopulateDateList()
        {
            dateListBox.Items.Clear();

            if (logs.Count == 0)
            {
                MessageBox.Show("No logs are found for the selected project and table.", "No Logs Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                shouldClose = true;
                return;
            }

            foreach (DateTime date in logs.Keys)
            {
                dateListBox.Items.Add(date.ToString("yyyy-MM-dd"));
            }
        }

        private void DateListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dateListBox.SelectedIndex != -1)
            {
                try
                {
                    DateTime selectedDate = DateTime.ParseExact(dateListBox.SelectedItem.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    if (logs.ContainsKey(selectedDate))
                    {
                        logRichTextBox.Clear(); // Clear previous log entries
                        logRichTextBox.Text = WrapText(logs[selectedDate], 100); // Adjust maxLineLength as needed
                    }
                    else
                    {
                        logRichTextBox.Clear();
                        logRichTextBox.Text = "No logs available for selected date.";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error displaying log content: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LogViewForm_Resize(object sender, EventArgs e)
        {
            logRichTextBox.Width = this.ClientSize.Width - dateListBox.Width;
            logRichTextBox.Height = this.ClientSize.Height;
        }

        private string WrapText(string text, int maxLineLength)
        {
            var wrappedText = new StringBuilder();
            var words = text.Split(' ');
            var lineLength = 0;

            foreach (var word in words)
            {
                if (lineLength + word.Length > maxLineLength)
                {
                    wrappedText.AppendLine();
                    lineLength = 0;
                }

                wrappedText.Append(word + " ");
                lineLength += word.Length + 1;
            }

            return wrappedText.ToString();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (shouldClose)
            {
                this.Close();
            }
        }
    }
}
