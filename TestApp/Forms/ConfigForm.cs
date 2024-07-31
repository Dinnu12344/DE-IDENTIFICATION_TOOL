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

            Panel buttonPanel = new Panel();
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Height = 50;
            this.Controls.Add(buttonPanel);

            Button saveButton = new Button();
            saveButton.Text = "Save";
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveButton.Click += new EventHandler(SaveBtn_Click);
            saveButton.Location = new Point(600, 10);
            buttonPanel.Controls.Add(saveButton);

            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Click += new EventHandler(CancelButton_Click);
            cancelButton.Location = new Point(700, 10);
            buttonPanel.Controls.Add(cancelButton);

            InitializeHeaderPanel(headerPanel);
        }

        private void InitializeHeaderPanel(Panel headerPanel)
        {
            TableLayoutPanel headerTableLayoutPanel = new TableLayoutPanel();
            headerTableLayoutPanel.Dock = DockStyle.Fill;
            headerTableLayoutPanel.ColumnCount = 8;
            headerTableLayoutPanel.RowCount = 1;

            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11));
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11));
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11));
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11));
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11));
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11));
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11));

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
            List<string> columns = FetchColumnsFromPythonBackend();

            if (columns != null && columns.Count > 0)
            {
                TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                tableLayoutPanel.Dock = DockStyle.Top;
                tableLayoutPanel.AutoSize = true;
                tableLayoutPanel.ColumnCount = 8;
                tableLayoutPanel.RowCount = columns.Count + 1;
                tableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
                tableLayoutPanel.AutoScroll = false;

                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

                int row = 1;

                List<ColumnConfig> existingConfig = LoadExistingConfig();

                foreach (string column in columns)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.AutoSize = true;
                    checkBox.Checked = true;
                    checkBox.Anchor = AnchorStyles.Left;
                    checkBox.Margin = new Padding(3, 3, 3, 3);
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
                    techniqueComboBox.SelectedIndexChanged += TechniqueComboBox_SelectedIndexChanged;
                    tableLayoutPanel.Controls.Add(techniqueComboBox, 3, row);

                    ComboBox techniqueComboBoxForData = new ComboBox();
                    techniqueComboBoxForData.Items.AddRange(Enum.GetNames(typeof(AdditionalTechnique)));
                    techniqueComboBoxForData.Dock = DockStyle.Fill;
                    techniqueComboBoxForData.MaxDropDownItems = 5;
                    techniqueComboBoxForData.Enabled = false;
                    tableLayoutPanel.Controls.Add(techniqueComboBoxForData, 4, row);

                    CustomDatePicker startDatePicker = new CustomDatePicker();
                    startDatePicker.Enabled = false;
                    tableLayoutPanel.Controls.Add(startDatePicker, 5, row);

                    CustomDatePicker endDatePicker = new CustomDatePicker();
                    endDatePicker.Enabled = false;
                    tableLayoutPanel.Controls.Add(endDatePicker, 6, row);

                    ComboBox keysComboBox = new ComboBox();
                    keysComboBox.Items.AddRange(Enum.GetNames(typeof(KeysOption)));
                    keysComboBox.Dock = DockStyle.Fill;
                    tableLayoutPanel.Controls.Add(keysComboBox, 7, row);

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
                    controlMap[checkBox] = new Control[]
                    {
                        columnLabel, dataTypeComboBox, techniqueComboBox, techniqueComboBoxForData,
                        startDatePicker, endDatePicker, keysComboBox
                    };
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

                TableLayoutPanel tableLayoutPanel = comboBox.Parent as TableLayoutPanel;
                if (tableLayoutPanel != null)
                {
                    int row = tableLayoutPanel.GetRow(comboBox);

                    ComboBox techniqueComboBoxForData = tableLayoutPanel.GetControlFromPosition(4, row) as ComboBox;
                    DateTimePicker startDatePicker = tableLayoutPanel.GetControlFromPosition(5, row) as DateTimePicker;
                    DateTimePicker endDatePicker = tableLayoutPanel.GetControlFromPosition(6, row) as DateTimePicker;

                    if (selectedTechnique == Technique.Pseudonymization)
                    {
                        techniqueComboBoxForData.Enabled = true;

                        startDatePicker.CustomFormat = " ";
                        startDatePicker.Enabled = false;
                        endDatePicker.CustomFormat = " ";
                        endDatePicker.Enabled = false;
                    }
                    else if (selectedTechnique == Technique.DateTimeAddRange)
                    {
                        techniqueComboBoxForData.Enabled = false;
                        techniqueComboBoxForData.SelectedIndex = -1;

                        startDatePicker.CustomFormat = "dd/MM/yy";
                        startDatePicker.Enabled = true;
                        endDatePicker.CustomFormat = "dd/MM/yy";
                        endDatePicker.Enabled = true;
                    }
                    else
                    {
                        techniqueComboBoxForData.Enabled = false;
                        techniqueComboBoxForData.SelectedIndex = -1;

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

                    string techniqueValue = techniqueComboBox.SelectedItem?.ToString();
                    string techniqueDataValue = null;
                    string startDateValue = null;
                    string endDateValue = null;

                    if (techniqueValue == "Pseudonymization")
                    {
                        techniqueDataValue = techniqueComboBoxForData.SelectedItem?.ToString();
                    }
                    else if (techniqueValue == "DateTimeAddRange")
                    {
                        startDateValue = startDatePicker.Value.ToString("yyyy-MM-dd");
                        endDateValue = endDatePicker.Value.ToString("yyyy-MM-dd");
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
            Directory.CreateDirectory(directoryPath);
            File.WriteAllText(filePath, json);
            MessageBox.Show("JSON data has been saved to " + filePath);
            this.Close();
        }
    }
}
