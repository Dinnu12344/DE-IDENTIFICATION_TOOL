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

            // Generate the log directory path based on the selected project and table
            string projectNameText = projectName.Text;
            string tableNameText = tableName.Text;
            logDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeidentificationTool", projectNameText, tableNameText, "LogFile");

            Console.WriteLine($"Log Directory Path: {logDirectoryPath}"); // Debug line

            // Read logs from files in the directory if it exists
            if (Directory.Exists(logDirectoryPath))
            {
                ReadLogsFromDirectory();
            }
            else
            {
                MessageBox.Show($"Log directory for project '{projectNameText}' and table '{tableNameText}' does not exist at '{logDirectoryPath}'.", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Populate date list with dates from log files
            PopulateDateList();

            // Subscribe to date selection event
            dateListBox.SelectedIndexChanged += DateListBox_SelectedIndexChanged;
            Console.WriteLine($"Subscribed to SelectedIndexChanged event. Current items: {dateListBox.Items.Count}"); // Debug line
        }

        private void ReadLogsFromDirectory()
        {
            try
            {
                // Get all log files in the directory
                string[] logFiles = Directory.GetFiles(logDirectoryPath, "*.log");
                Console.WriteLine($"Found {logFiles.Length} log files."); // Debug line

                // Process each log file
                foreach (string logFile in logFiles)
                {
                    // Extract date from the file name
                    string fileName = Path.GetFileNameWithoutExtension(logFile);
                    Console.WriteLine($"Processing file: {fileName}"); // Debug line

                    // Attempt to parse the date from the file name
                    if (DateTime.TryParseExact(fileName, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime logDate))
                    {
                        // Read the content of the log file
                        string logContent = File.ReadAllText(logFile);
                        Console.WriteLine($"Log Date: {logDate}, Content: {logContent.Substring(0, Math.Min(logContent.Length, 100))}..."); // Debug line to print a part of the log content
                        logs[logDate] = logContent;
                    }
                    else
                    {
                        // Handle improperly formatted file names
                        MessageBox.Show($"Improperly formatted log file name: {fileName}", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                // Show error message if there's an exception reading the files
                MessageBox.Show($"Error reading log files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateDateList()
        {
            // Clear the current list
            dateListBox.Items.Clear();

            Console.WriteLine($"Populating date list with {logs.Count} entries."); // Debug line

            // Add each date to the list box
            foreach (DateTime date in logs.Keys)
            {
                Console.WriteLine($"Adding date: {date.ToShortDateString()}"); // Debug line
                dateListBox.Items.Add(date.ToShortDateString());
            }
        }

        private void DateListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (dateListBox.SelectedIndex != -1)
            {
                // Parse the selected date
                DateTime selectedDate = DateTime.Parse(dateListBox.SelectedItem.ToString());
                Console.WriteLine($"Selected date: {selectedDate.ToShortDateString()}"); // Debug line

                // Display the log for the selected date
                if (logs.ContainsKey(selectedDate))
                {
                    Console.WriteLine($"Displaying log for date: {selectedDate.ToShortDateString()}"); // Debug line
                    logTextBox.Text = logs[selectedDate];
                }
                else
                {
                    Console.WriteLine($"No logs available for selected date: {selectedDate.ToShortDateString()}"); // Debug line
                    logTextBox.Text = "No logs available for selected date.";
                }
            }
        }
    }
}
