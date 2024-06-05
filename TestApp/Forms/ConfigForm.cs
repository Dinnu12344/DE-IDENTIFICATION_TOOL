using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    public partial class ConfigForm : Form
    {
        private List<System.Windows.Forms.ComboBox> dynamicComboBoxes;
        private Form1 homeForm;
        private Panel scrollablePanel;
        private string pythonResponse;
        private TreeNode tabelName;
        private TreeNode projectName;

        public ConfigForm(Form1 homeForm, string response,TreeNode selectedNode, TreeNode parentNode)
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
            Panel headerPanel = new Panel() ;
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
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left; // Anchoring to the bottom and left
            saveButton.Click += new EventHandler(saveBtn_Click);
            saveButton.Location = new Point(10, 10); // Adjust location within the button panel
            buttonPanel.Controls.Add(saveButton);

            // Create and add Cancel button
            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cancelButton.Click += new EventHandler(cancelButton_Click);
            cancelButton.Location = new Point(110, 10);
            buttonPanel.Controls.Add(cancelButton);

            // Initialize the header panel with column names
            InitializeHeaderPanel(headerPanel);
        }

        private void InitializeHeaderPanel(Panel headerPanel)
        {
            TableLayoutPanel headerTableLayoutPanel = new TableLayoutPanel();
            headerTableLayoutPanel.Dock = DockStyle.Fill;
            headerTableLayoutPanel.ColumnCount = 5;
            headerTableLayoutPanel.RowCount = 1;

            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15)); // Select
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Column
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // DataType
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // Technique
            headerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // Keys

            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Select"), 0, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Column"), 1, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("DataType"), 2, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Technique"), 3, 0);
            headerTableLayoutPanel.Controls.Add(CreateHeaderLabel("Keys"), 4, 0);

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
                tableLayoutPanel.ColumnCount = 5; 
                tableLayoutPanel.RowCount = columns.Count + 1; 
                tableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
                tableLayoutPanel.AutoScroll = false;

                // Set column styles
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15)); // Select
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Column
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // DataType
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // Technique
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // Keys
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

                int row = 1;

                foreach (string column in columns)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.AutoSize = true;
                    tableLayoutPanel.Controls.Add(checkBox, 0, row);
                    checkBoxes.Add(checkBox);

                    Label columnLabel = new Label();
                    columnLabel.Text = column;
                    columnLabel.AutoSize = true;
                    tableLayoutPanel.Controls.Add(columnLabel, 1, row);

                    ComboBox dataTypeComboBox = new ComboBox();
                    dataTypeComboBox.Items.AddRange(new string[] { "int", "float", "string", "DateTime", "bool" });
                    dataTypeComboBox.Dock = DockStyle.Fill;
                    tableLayoutPanel.Controls.Add(dataTypeComboBox, 2, row);

                    ComboBox techniqueComboBox = new ComboBox();
                    techniqueComboBox.Items.AddRange(new string[] { "Pseudonymization", "Anonymization", "Masking", "Generalization", "dateTo20_30years", "dateTimeAddRange" });
                    techniqueComboBox.Dock = DockStyle.Fill;
                    tableLayoutPanel.Controls.Add(techniqueComboBox, 3, row);

                    ComboBox keysComboBox = new ComboBox();
                    keysComboBox.Items.AddRange(new string[] { "Yes", "No" });
                    keysComboBox.Dock = DockStyle.Fill;
                    tableLayoutPanel.Controls.Add(keysComboBox, 4, row);

                    controlMap.Add(checkBox, new Control[] { columnLabel, dataTypeComboBox, techniqueComboBox, keysComboBox });

                    row++; 
                }

                scrollablePanel.Controls.Add(tableLayoutPanel);
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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            homeForm.Show();
            this.Hide();
        }

        private void saveBtn_Click(object sender, EventArgs e)
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
                    var keysComboBox = (ComboBox)controls[3];

                    var data = new
                    {
                        Column = columnLabel.Text,
                        DataType = dataTypeComboBox.SelectedItem?.ToString(),
                        Technique = techniqueComboBox.SelectedItem?.ToString(),
                        Keys = keysComboBox.SelectedItem?.ToString()
                    };

                    selectedData.Add(data);
                }
            }

            // Convert the selected data to JSON format
            string json = JsonConvert.SerializeObject(selectedData, Formatting.Indented);
            string userName = Environment.UserName;
            string directoryPath = $@"C:\Users\{userName}\AppData\Roaming\DeidentificationTool\{project}\{table}\ConfigFile";
            string filePath = Path.Combine(directoryPath, (table+".json"));

            // Ensure the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllText(filePath, json);

            MessageBox.Show("JSON data has been saved to " + filePath);
            this.Close();
        }
    }
}
