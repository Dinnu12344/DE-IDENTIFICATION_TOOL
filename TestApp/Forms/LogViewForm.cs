using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class LogViewForm : Form
    {
        private TreeNode tableName;
        private TreeNode projectName;
        private string logDirectoryPath;

        private Dictionary<DateTime, string> logs = new Dictionary<DateTime, string>();
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
            }

            PopulateDateList();

            dateListBox.SelectedIndexChanged += DateListBox_SelectedIndexChanged;
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading log files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateDateList()
        {
            dateListBox.Items.Clear();

            foreach (DateTime date in logs.Keys)
            {
                dateListBox.Items.Add(date.ToShortDateString());
            }
        }

        private void DateListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dateListBox.SelectedIndex != -1)
            {
                DateTime selectedDate = DateTime.Parse(dateListBox.SelectedItem.ToString());

                if (logs.ContainsKey(selectedDate))
                {
                    logTextBox.Text = logs[selectedDate];
                }
                else
                {
                    logTextBox.Text = "No logs available for selected date.";
                }
            }
        }
    }
}
