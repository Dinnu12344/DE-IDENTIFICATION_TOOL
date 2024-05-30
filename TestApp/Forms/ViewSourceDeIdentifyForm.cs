using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class ViewSourceDeIdentifyForm : Form
    {
        private Form1 homeForm;
        public ViewSourceDeIdentifyForm(Form1 homeForm, string jsonData)
        {
            InitializeComponent();
            DisplayData(jsonData);
            this.homeForm = homeForm;
        }
        private void DisplayData(string jsonData)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                var bindingList = new BindingList<Dictionary<string, object>>(data);
                var source = new BindingSource(bindingList, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying data: {ex.Message}");
            }
        }
    }
}
