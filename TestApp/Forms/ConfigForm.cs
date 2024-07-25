using DE_IDENTIFICATION_TOOL.CustomAction;
using DE_IDENTIFICATION_TOOL.Enums;
using DE_IDENTIFICATION_TOOL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class ConfigForm : Form
    {
        private List<ComboBox> dynamicComboBoxes;
        private HomeForm homeForm;
        private Panel scrollablePanel;
        private string pythonResponse;
        private TreeNode tabelName;
        private TreeNode projectName;

        public ConfigForm(HomeForm homeForm, string response, TreeNode selectedNode, TreeNode parentNode)
        {
            this.homeForm = homeForm;
            pythonResponse = response;
            tabelName = selectedNode;
            projectName = parentNode;
            InitializeComponent();
            InitializeCustomControls();
            InitializeDynamicControls();
        }

        // Store references to dynamically created controls
        private List<Control> checkBoxes = new List<Control>();
        private Dictionary<Control, Control[]> controlMap = new Dictionary<Control, Control[]>();

        private void InitializeCustomControls()
        {
            this.Text = "Config Form";
            this.StartPosition = FormStartPosition.CenterScreen;
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 30;
            this.Controls.Add(headerPanel);

            scrollablePanel = new Panel();
            scrollablePanel.Dock = DockStyle.Fill;
            scrollablePanel.AutoScroll = true;
            this.Controls.Add(scrollablePanel);

            // Create buttonPanel and dock it at the bottom of the form and fix the height
            Panel buttonPanel = new Panel();
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Height = 50;
            this.Controls.Add(buttonPanel);

            // Create and add Save button
            Button saveButton = new Button();
            saveButton.Text = "Save";
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right; // Anchoring to the bottom and left
            saveButton.Click += new EventHandler(SaveBtn_Click);
            saveButton.Location = new Point(600, 10); // Adjust location within the button panel
            buttonPanel.Controls.Add(saveButton);

            // Create and add Cancel button
            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Click += new EventHandler(CancelButton_Click);
            cancelButton.Location = new Point(700, 10);
            buttonPanel.Controls.Add(cancelButton);

            // Initialize the header panel with column names
            InitializeHeaderPanel(headerPanel);
        }

        private void InitializeHeaderPanel(Panel headerPanel)
        {
            TableLayoutPanel headerTableLayoutPanel = new TableLayoutPanel();
            headerTableLayoutPanel.Dock = DockStyle.Fill;
            headerTableLayoutPanel.ColumnCount = 8; // Adjust the column count
            headerTableLayoutPanel.RowCount = 1;

            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11)); // Select
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11)); // Column
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11)); // DataType
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11)); // Technique
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Data
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11)); // Start Date
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11)); // End Date
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11)); // Keys

            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Select"), 0, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Column"), 1, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("DataType"), 2, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Technique"), 3, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Data"), 4, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Start Date"), 5, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("End Date"), 6, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Keys"), 7, 0);

            headerPanel.Controls.Add(headerTableLayoutPanel);
        }

        private void InitializeDynamicControls()
        {
            // Fetch columns from Python backend
            List<string> columns = FetchColumnsFromPythonBackend();

            if (columns != null && columns.Count > 0)
            {
                TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                tableLayoutPanel.Dock = DockStyle.Top;
                tableLayoutPanel.AutoSize = true;
                tableLayoutPanel.ColumnCount = 8; // Adjust the column count
                tableLayoutPanel.RowCount = columns.Count + 1;
                tableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
                tableLayoutPanel.AutoScroll = false;

                // Set column styles
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Select
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Column
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // DataType
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Technique
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Data
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Start Date
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // End Date
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Keys
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

                int row = 1;

                // Load existing configuration if it exists
                List<ColumnConfig> existingConfig = LoadExistingConfig();

                foreach (string column in columns)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.AutoSize = true;
                    checkBox.Checked = true;
                    checkBox.Anchor = AnchorStyles.Left;
                    checkBox.Margin = new Padding(3, 3, 3, 3); // Adding some margin to ensure it does not overlap
                    tableLayoutPanel.Controls.Add(checkBox, 0, row);
                    checkBoxes.Add(checkBox);

                    Label columnLabel = new Label();
                    columnLabel.Text = column;
                    columnLabel.AutoSize = true;
                    columnLabel.Anchor = AnchorStyles.Left;
                    tableLayoutPanel.Controls.Add(columnLabel, 1, row);

                    ComboBox dataTypeComboBox = new ComboBox();
                    dataTypeComboBox.Items.AddRange(Enum.GetNames(typeof(DataType)));
                    dataTypeComboBox.Dock = DockStyle.Fill;
                    tableLayoutPanel.Controls.Add(dataTypeComboBox, 2, row);

                    ComboBox techniqueComboBox = new ComboBox();
                    techniqueComboBox.Items.AddRange(Enum.GetNames(typeof(Technique)));
                    techniqueComboBox.Dock = DockStyle.Fill;
                    techniqueComboBox.SelectedIndexChanged += TechniqueComboBox_SelectedIndexChanged; // Handle selection change
                    tableLayoutPanel.Controls.Add(techniqueComboBox, 3, row);

                    ComboBox techniqueComboBoxForData = new ComboBox();
                    techniqueComboBoxForData.Items.AddRange(Enum.GetNames(typeof(AdditionalTechnique)));
                    techniqueComboBoxForData.Dock = DockStyle.Fill;
                    techniqueComboBoxForData.MaxDropDownItems = 5;
                    techniqueComboBoxForData.Enabled = false; // Initially hidden
                    tableLayoutPanel.Controls.Add(techniqueComboBoxForData, 4, row);

                    CustomDatePicker startDatePicker = new CustomDatePicker();
                    startDatePicker.Enabled = false; // Initially disabled
                    tableLayoutPanel.Controls.Add(startDatePicker, 5, row);

                    CustomDatePicker endDatePicker = new CustomDatePicker();
                    endDatePicker.Enabled = false; // Initially disabled
                    tableLayoutPanel.Controls.Add(endDatePicker, 6, row);

                    ComboBox keysComboBox = new ComboBox();
                    keysComboBox.Items.AddRange(Enum.GetNames(typeof(KeysOption)));
                    keysComboBox.Dock = DockStyle.Fill;
                    tableLayoutPanel.Controls.Add(keysComboBox, 7, row);

                    // Load existing data if available
                    ColumnConfig config = existingConfig?.Find(c => c.Column == column);
                    if (config != null)
                    {
                        dataTypeComboBox.SelectedItem = config.DataType;
                        techniqueComboBox.SelectedItem = config.Technique;
                        if (config.Technique == "Pseudonymization")
                        {
                            techniqueComboBoxForData.Enabled = true;
                            techniqueComboBoxForData.SelectedItem = config.HippaRelatedColumn;
                        }
                        else if (config.Technique == "DateRange")
                        {
                            startDatePicker.Enabled = true;
                            endDatePicker.Enabled = true;
                            startDatePicker.Value = config.StartDate ?? DateTime.Now;
                            endDatePicker.Value = config.EndDate ?? DateTime.Now;
                        }
                        keysComboBox.SelectedItem = config.Keys;
                    }

                    controlMap[checkBox] = new Control[] { columnLabel, dataTypeComboBox, techniqueComboBox, techniqueComboBoxForData, startDatePicker, endDatePicker, keysComboBox };

                    row++;
                }

                scrollablePanel.Controls.Add(tableLayoutPanel);
            }
        }



        private void TechniqueComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                string selectedValue = comboBox.SelectedItem.ToString();
                Technique selectedTechnique = (Technique)Enum.Parse(typeof(Technique), selectedValue);

                // Find the corresponding controls for the row
                TableLayoutPanel tableLayoutPanel = comboBox.Parent as TableLayoutPanel;
                if (tableLayoutPanel != null)
                {
                    // Determine the row of the sender ComboBox
                    int row = tableLayoutPanel.GetRow(comboBox);

                    ComboBox techniqueComboBoxForData = tableLayoutPanel.GetControlFromPosition(4, row) as ComboBox;
                    DateTimePicker startDatePicker = tableLayoutPanel.GetControlFromPosition(5, row) as DateTimePicker;
                    DateTimePicker endDatePicker = tableLayoutPanel.GetControlFromPosition(6, row) as DateTimePicker;

                    if (selectedTechnique == Technique.Pseudonymization)
                    {
                        // Enable the additional data ComboBox
                        techniqueComboBoxForData.Enabled = true;

                        // Clear and disable the date pickers
                        startDatePicker.CustomFormat = " ";
                        startDatePicker.Enabled = false;
                        endDatePicker.CustomFormat = " ";
                        endDatePicker.Enabled = false;
                    }
                    else if (selectedTechnique == Technique.DateTimeAddRange)
                    {
                        // Disable and clear the additional data ComboBox
                        techniqueComboBoxForData.Enabled = false;
                        techniqueComboBoxForData.SelectedIndex = -1; // Clear the selection

                        // Enable the start and end date pickers
                        startDatePicker.CustomFormat = "dd/MM/yy";
                        startDatePicker.Enabled = true;
                        endDatePicker.CustomFormat = "dd/MM/yy";
                        endDatePicker.Enabled = true;
                    }
                    else
                    {
                        techniqueComboBoxForData.Enabled = false;
                        techniqueComboBoxForData.SelectedIndex = -1; // Clear the selection

                        startDatePicker.CustomFormat = " ";
                        startDatePicker.Enabled = false;
                        endDatePicker.CustomFormat = " ";
                        endDatePicker.Enabled = false;
                    }
                }
            }
        }




        private Label CreateHeaderLabel(string text)
        {
            return new Label
            {
                Text = text,
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                AutoSize = true,
                Anchor = AnchorStyles.Right | AnchorStyles.Left,
            };
        }

        private List<string> FetchColumnsFromPythonBackend()
        {
            string response = pythonResponse.Replace("'", "\"");

            List<string> columns = JsonConvert.DeserializeObject<List<string>>(response);

            return columns;
        }

        private List<ColumnConfig> LoadExistingConfig()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeidentificationTool", projectName.Text, tabelName.Text, "ConfigFile");
            string filePath = Path.Combine(directoryPath, $"{tabelName.Text}.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<ColumnConfig>>(json);
            }

            return null;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            homeForm.Show();
            this.Hide();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string table = tabelName.Text;
            string project = projectName.Text;
            var selectedData = new List<object>();

            foreach (CheckBox checkBox in checkBoxes)
            {
                if (checkBox.Checked)
                {
                    var controls = controlMap[checkBox];
                    var columnLabel = (Label)controls[0];
                    var dataTypeComboBox = (ComboBox)controls[1];
                    var techniqueComboBox = (ComboBox)controls[2];
                    var techniqueComboBoxForData = (ComboBox)controls[3];
                    var startDatePicker = (DateTimePicker)controls[4];
                    var endDatePicker = (DateTimePicker)controls[5];
                    var keysComboBox = (ComboBox)controls[6];

                    // Determine Technique value
                    string techniqueValue = techniqueComboBox.SelectedItem?.ToString();

                    // Determine TechniqueData based on Technique value
                    string techniqueDataValue = null;
                    DateTime? startDateValue = null;
                    DateTime? endDateValue = null;
                    if (techniqueValue == "Pseudonymization")
                    {
                        techniqueDataValue = techniqueComboBoxForData.SelectedItem?.ToString();
                    }
                    else if (techniqueValue == "DateTimeAddRange")
                    {
                        startDateValue = startDatePicker.Value;
                        endDateValue = endDatePicker.Value;
                    }

                    var data = new
                    {
                        Column = columnLabel.Text,
                        DataType = dataTypeComboBox.SelectedItem?.ToString(),
                        Technique = techniqueValue,
                        HippaRelatedColumn = techniqueDataValue,
                        StartDate = startDateValue,
                        EndDate = endDateValue,
                        Keys = keysComboBox.SelectedItem?.ToString()
                    };

                    selectedData.Add(data);
                }
            }

            string json = JsonConvert.SerializeObject(selectedData, Formatting.Indented);
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeidentificationTool", project, table, "ConfigFile");
            string filePath = Path.Combine(directoryPath, $"{table}.json");

            // Ensure the directory exists
            Directory.CreateDirectory(directoryPath);

            File.WriteAllText(filePath, json);

            MessageBox.Show("JSON data has been saved to " + filePath);
            this.Close();
        }
    }
}
