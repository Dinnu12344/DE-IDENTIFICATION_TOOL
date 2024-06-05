using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class ViewSourceDeIdentifyForm : Form
    {
        public ViewSourceDeIdentifyForm(string jsonData)
        {
            InitializeComponent();
            InitializeDataGridView(jsonData);
        }
        private void InitializeDataGridView(string jsonData)
        {
            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoGenerateColumns = true,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.LightGray },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    WrapMode = DataGridViewTriState.True
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    WrapMode = DataGridViewTriState.True
                }
            };

            // Add DataGridView to the form's Controls collection
            Controls.Add(dataGridView);
            DataTable dataTable = ConvertJsonToDataTable(jsonData);
            if (dataTable != null)
            {
                Console.WriteLine("DataTable Columns: " + string.Join(", ", dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine("Row: " + string.Join(", ", row.ItemArray));
                }

                dataGridView.DataSource = dataTable;
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    column.MinimumWidth = 100;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                this.Controls.Add(dataGridView);
            }
            else
            {
                MessageBox.Show("Failed to convert CSV to DataTable");
            }
        }
        private DataTable ConvertJsonToDataTable(string jsonData)
        {
            DataTable dataTable = new DataTable();
            try
            {
                List<Dictionary<string, object>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);

                if (dataList.Count == 0)
                {
                    MessageBox.Show("JSON data is empty");
                    return null;
                }
                foreach (var key in dataList[0].Keys)
                {
                    dataTable.Columns.Add(key);
                }

                foreach (var dataItem in dataList)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (var key in dataItem.Keys)
                    {
                        row[key] = dataItem[key];
                    }
                    dataTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error converting JSON to DataTable: " + ex.Message);
                Console.WriteLine("Error converting JSON to DataTable: " + ex.ToString());
                Console.WriteLine("Input JSON data: " + jsonData);
                return null;
            }
            return dataTable;
        }
    }
}