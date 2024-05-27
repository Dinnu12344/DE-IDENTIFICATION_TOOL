using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace TestApp
{
    public partial class ConfigForm : Form
    {
        private List<System.Windows.Forms.ComboBox> dynamicComboBoxes;
        private Form1 homeForm;
        private Panel scrollablePanel;
        private string pythonResponse;
        private TreeNode tabelName;
        private TreeNode projectName;

        public ConfigForm(Form1 homeForm, string response,TreeNode selectedNode, TreeNode parentNode )
        {
            this.homeForm = homeForm;
            pythonResponse = response;
            tabelName = selectedNode;
            projectName = parentNode;
            InitializeComponent();
            InitializeDynamicControls();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //private void InitializeDynamicControls()
        //{
        //    int controlWidth = 150;
        //    int controlHeight = 21;
        //    int spacing = 10;
        //    int startX = 20;
        //    int startY = 170;
        //    int headingFontSize = 12; // Adjust the font size as needed

        //    // Fetch columns from Python backend
        //    List<string> columns = FetchColumnsFromPythonBackend();

        //    // Check if columns were fetched successfully
        //    if (columns != null && columns.Count > 0)
        //    {
        //        // Add heading for checkboxes
        //        Label checkBoxHeading = new Label();
        //        checkBoxHeading.Text = "Select";
        //        checkBoxHeading.Location = new Point(startX - 15, startY - 20);
        //        checkBoxHeading.Font = new Font(checkBoxHeading.Font.FontFamily, headingFontSize, checkBoxHeading.Font.Style);
        //        this.Controls.Add(checkBoxHeading);

        //        // Add heading for column names
        //        Label columnHeading = new Label();
        //        columnHeading.Text = "Column";
        //        columnHeading.Location = new Point(startX + 80, startY - 20);
        //        columnHeading.Font = new Font(columnHeading.Font.FontFamily, headingFontSize, columnHeading.Font.Style);
        //        this.Controls.Add(columnHeading);

        //        // Add heading for data types
        //        Label dataTypeHeading = new Label();
        //        dataTypeHeading.Text = "  DataType";
        //        dataTypeHeading.Location = new Point(startX + controlWidth + 70, startY - 20);
        //        dataTypeHeading.Font = new Font(dataTypeHeading.Font.FontFamily, headingFontSize, dataTypeHeading.Font.Style);
        //        this.Controls.Add(dataTypeHeading);

        //        // Add heading for techniques
        //        Label techniqueHeading = new Label();
        //        techniqueHeading.Text = "Technique";
        //        techniqueHeading.Location = new Point(startX + 2 * (controlWidth + 60), startY - 20);
        //        techniqueHeading.Font = new Font(techniqueHeading.Font.FontFamily, headingFontSize, techniqueHeading.Font.Style);
        //        this.Controls.Add(techniqueHeading);

        //        // Add heading for keys
        //        Label keysHeading = new Label();
        //        keysHeading.Text = "Keys";
        //        keysHeading.Location = new Point(startX + 3 * (controlWidth + 50), startY - 20);
        //        keysHeading.Font = new Font(keysHeading.Font.FontFamily, headingFontSize, keysHeading.Font.Style);
        //        this.Controls.Add(keysHeading);

        //        // Adjust startY for the controls
        //        startY += 20; // Move down to leave space for the headings

        //        foreach (string column in columns)
        //        {
        //            // Create checkboxes for each column
        //            CheckBox checkBox = new CheckBox();
        //            checkBox.Location = new Point(startX, startY);
        //            checkBox.Size = new Size(15, controlHeight);
        //            this.Controls.Add(checkBox);

        //            // Create labels for column names
        //            Label columnLabel = new Label();
        //            columnLabel.Location = new Point(startX + 80, startY);
        //            columnLabel.Size = new Size(controlWidth, controlHeight);
        //            columnLabel.Text = column;
        //            this.Controls.Add(columnLabel);

        //            // Create combo boxes for datatypes, techniques, and keys
        //            ComboBox dataTypeComboBox = new ComboBox();
        //            dataTypeComboBox.Location = new Point(startX + controlWidth + 80, startY);
        //            dataTypeComboBox.Size = new Size(controlWidth, controlHeight);
        //            dataTypeComboBox.Items.AddRange(new string[] { "int", "float", "string", "DateTime", "bool" });
        //            this.Controls.Add(dataTypeComboBox);

        //            ComboBox techniqueComboBox = new ComboBox();
        //            techniqueComboBox.Location = new Point(startX + 2 * (controlWidth + 60), startY);
        //            techniqueComboBox.Size = new Size(controlWidth, controlHeight);
        //            techniqueComboBox.Items.AddRange(new string[] { "Pseudonymization", "Anonymization", "Masking", "Generalization", "dateTo20_30years", "dateTimeAddRange" });
        //            this.Controls.Add(techniqueComboBox);

        //            ComboBox keysComboBox = new ComboBox();
        //            keysComboBox.Location = new Point(startX + 3 * (controlWidth + 50), startY);
        //            keysComboBox.Size = new Size(controlWidth, controlHeight);
        //            keysComboBox.Items.AddRange(new string[] { "Yes", "No" });
        //            this.Controls.Add(keysComboBox);

        //            // Adjust startY for the next row
        //            startY += controlHeight + spacing;
        //        }
        //    }
        //}

        // Store references to dynamically created controls
        private List<Control> checkBoxes = new List<Control>();
        private Dictionary<Control, Control[]> controlMap = new Dictionary<Control, Control[]>();

        private void InitializeDynamicControls()
        {
            int controlWidth = 150;
            int controlHeight = 21;
            int spacing = 10;
            int startX = 20;
            int startY = 170;
            int headingFontSize = 12; // Adjust the font size as needed

            // Fetch columns from Python backend
            List<string> columns = FetchColumnsFromPythonBackend();

            // Check if columns were fetched successfully
            if (columns != null && columns.Count > 0)
            {
                // Add heading for checkboxes
                Label checkBoxHeading = new Label();
                checkBoxHeading.Text = "Select";
                checkBoxHeading.Location = new Point(startX - 15, startY - 20);
                checkBoxHeading.Font = new Font(checkBoxHeading.Font.FontFamily, headingFontSize, checkBoxHeading.Font.Style);
                this.Controls.Add(checkBoxHeading);

                // Add heading for column names
                Label columnHeading = new Label();
                columnHeading.Text = "Column";
                columnHeading.Location = new Point(startX + 80, startY - 20);
                columnHeading.Font = new Font(columnHeading.Font.FontFamily, headingFontSize, columnHeading.Font.Style);
                this.Controls.Add(columnHeading);

                // Add heading for data types
                Label dataTypeHeading = new Label();
                dataTypeHeading.Text = "  DataType";
                dataTypeHeading.Location = new Point(startX + controlWidth + 70, startY - 20);
                dataTypeHeading.Font = new Font(dataTypeHeading.Font.FontFamily, headingFontSize, dataTypeHeading.Font.Style);
                this.Controls.Add(dataTypeHeading);

                // Add heading for techniques
                Label techniqueHeading = new Label();
                techniqueHeading.Text = "Technique";
                techniqueHeading.Location = new Point(startX + 2 * (controlWidth + 60), startY - 20);
                techniqueHeading.Font = new Font(techniqueHeading.Font.FontFamily, headingFontSize, techniqueHeading.Font.Style);
                this.Controls.Add(techniqueHeading);

                // Add heading for keys
                Label keysHeading = new Label();
                keysHeading.Text = "Keys";
                keysHeading.Location = new Point(startX + 3 * (controlWidth + 50), startY - 20);
                keysHeading.Font = new Font(keysHeading.Font.FontFamily, headingFontSize, keysHeading.Font.Style);
                this.Controls.Add(keysHeading);

                // Adjust startY for the controls
                startY += 20; // Move down to leave space for the headings

                foreach (string column in columns)
                {
                    // Create checkboxes for each column
                    System.Windows.Forms.CheckBox checkBox = new System.Windows.Forms.CheckBox();
                    checkBox.Location = new Point(startX, startY);
                    checkBox.Size = new Size(15, controlHeight);
                    this.Controls.Add(checkBox);
                    checkBoxes.Add(checkBox);

                    // Create labels for column names
                    Label columnLabel = new Label();
                    columnLabel.Location = new Point(startX + 80, startY);
                    columnLabel.Size = new Size(controlWidth, controlHeight);
                    columnLabel.Text = column;
                    this.Controls.Add(columnLabel);

                    // Create combo boxes for datatypes, techniques, and keys
                    System.Windows.Forms.ComboBox dataTypeComboBox = new System.Windows.Forms.ComboBox();
                    dataTypeComboBox.Location = new Point(startX + controlWidth + 80, startY);
                    dataTypeComboBox.Size = new Size(controlWidth, controlHeight);
                    dataTypeComboBox.Items.AddRange(new string[] { "int", "float", "string", "DateTime", "bool" });
                    this.Controls.Add(dataTypeComboBox);

                    System.Windows.Forms.ComboBox techniqueComboBox = new System.Windows.Forms.ComboBox();
                    techniqueComboBox.Location = new Point(startX + 2 * (controlWidth + 60), startY);
                    techniqueComboBox.Size = new Size(controlWidth, controlHeight);
                    techniqueComboBox.Items.AddRange(new string[] { "Pseudonymization", "Anonymization", "Masking", "Generalization", "dateTo20_30years", "dateTimeAddRange" });
                    this.Controls.Add(techniqueComboBox);

                    System.Windows.Forms.ComboBox keysComboBox = new System.Windows.Forms.ComboBox();
                    keysComboBox.Location = new Point(startX + 3 * (controlWidth + 50), startY);
                    keysComboBox.Size = new Size(controlWidth, controlHeight);
                    keysComboBox.Items.AddRange(new string[] { "Yes", "No" });
                    this.Controls.Add(keysComboBox);

                    // Map the checkbox to the corresponding controls
                    controlMap.Add(checkBox, new Control[] { columnLabel, dataTypeComboBox, techniqueComboBox, keysComboBox });

                    // Adjust startY for the next row
                    startY += controlHeight + spacing;
                }
            }
        }

        private List<string> FetchColumnsFromPythonBackend()
        {
            // Simulated method to fetch columns from Python backend
            // Replace this with actual implementation to fetch columns dynamically
            //return new List<string>() { pythonResponse };
            string response = pythonResponse.Replace("'", "\""); // Replace single quotes with double quotes

            // Now parse the JSON-like string to get the list of column names
            List<string> columns = JsonConvert.DeserializeObject<List<string>>(response);

            return columns;
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            homeForm.Show();
            this.Hide();
        }
        private void finishButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Save Button is Hitted");
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string table = tabelName.Text;
            string project = projectName.Text;
            var selectedData = new List<object>();

            foreach (System.Windows.Forms.CheckBox checkBox in checkBoxes)
            {
                if (checkBox.Checked)
                {
                    var controls = controlMap[checkBox];
                    var columnLabel = (Label)controls[0];
                    var dataTypeComboBox = (System.Windows.Forms.ComboBox)controls[1];
                    var techniqueComboBox = (System.Windows.Forms.ComboBox)controls[2];
                    var keysComboBox = (System.Windows.Forms.ComboBox)controls[3];

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

            // Define the file path
            //string directoryPath = @"C:\Users\Satya Pulamanthula\AppData\Roaming\DeidentificationTool\Projectname\table-name\configfiles";
            string directoryPath = $@"C:\Users\Satya Pulamanthula\AppData\Roaming\DeidentificationTool\{project}\{table}\configfiles";
            string filePath = Path.Combine(directoryPath, "config.json");

            // Ensure the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Write the JSON data to the file
            File.WriteAllText(filePath, json);

            MessageBox.Show("JSON data has been saved to " + filePath);
        }

    }
}
