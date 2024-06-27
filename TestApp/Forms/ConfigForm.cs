using DE_IDENTIFICATION_TOOL.Enums;
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

            //Create buttonPanel and dock the bottom of the form and fixing the height
            Panel buttonPanel = new Panel();
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Height = 50;
            this.Controls.Add(buttonPanel);

            // Create and add Save button
            Button saveButton = new Button();
            saveButton.Text = "SaveFinish";
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
            headerTableLayoutPanel.ColumnCount = 6;
            headerTableLayoutPanel.RowCount = 1;

            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15)); // Select
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Column
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // DataType
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Technique
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // Data
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Keys

            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Select"), 0, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Column"), 1, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("DataType"), 2, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Technique"), 3, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Data"), 4, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Keys"), 5, 0);

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
                tableLayoutPanel.ColumnCount = 6;
                tableLayoutPanel.RowCount = columns.Count + 1;
                tableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
                tableLayoutPanel.AutoScroll = false;

                // Set column styles
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15)); // Select
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Column
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // DataType
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Technique
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // Data
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Keys
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

                int row = 1;

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
                    techniqueComboBoxForData.Items.AddRange(Enum.GetNames(typeof(AdditionalTechnique))); ;
                    techniqueComboBoxForData.Dock = DockStyle.Fill;
                    techniqueComboBoxForData.MaxDropDownItems = 5;
                    techniqueComboBoxForData.Enabled = false; // Initially hidden
                    tableLayoutPanel.Controls.Add(techniqueComboBoxForData, 4, row);

                    ComboBox keysComboBox = new ComboBox();
                    keysComboBox.Items.AddRange(Enum.GetNames(typeof(KeysOption)));
                    keysComboBox.Dock = DockStyle.Fill;
                    tableLayoutPanel.Controls.Add(keysComboBox, 5, row);

                    controlMap.Add(checkBox, new Control[] { columnLabel, dataTypeComboBox, techniqueComboBox, techniqueComboBoxForData, keysComboBox });

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

                // Find the corresponding techniqueComboBoxForData for the row
                TableLayoutPanel tableLayoutPanel = comboBox.Parent as TableLayoutPanel;
                if (tableLayoutPanel != null)
                {
                    // Determine the row of the sender ComboBox
                    int row = tableLayoutPanel.GetRow(comboBox);

                    ComboBox techniqueComboBoxForData = tableLayoutPanel.GetControlFromPosition(4, row) as ComboBox;
                    if (techniqueComboBoxForData != null)
                    {
                        if (selectedTechnique == Technique.Pseudonymization)
                        {
                            // Enable the ComboBox and populate items if needed
                            techniqueComboBoxForData.Enabled = true;
                        }
                        else
                        {
                            techniqueComboBoxForData.Refresh();
                            techniqueComboBoxForData.Enabled = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data ComboBox is null for row: " + row);
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
                    var keysComboBox = (ComboBox)controls[4];

                    // Determine Technique value
                    string techniqueValue = techniqueComboBox.SelectedItem?.ToString();

                    // Determine TechniqueData based on Technique value
                    string techniqueDataValue = null;
                    if (techniqueValue == "Pseudonymization")
                    {
                        techniqueDataValue = techniqueComboBoxForData.SelectedItem?.ToString();
                    }

                    var data = new
                    {
                        Column = columnLabel.Text,
                        DataType = dataTypeComboBox.SelectedItem?.ToString(),
                        Technique = techniqueValue,
                        HippaRelatedColumn = techniqueDataValue,
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