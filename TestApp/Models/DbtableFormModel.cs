using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.Models
{
    public class DbtableFormModel
    {
        public string ConnectionString { get; set; }
        public TreeNode SelectedNode { get; set; }
        public List<ProjectData> ProjectData { get; set; }
        public HomeForm HomeForm { get; set; }
        public string ProjectName { get; set; }
        public string LabelName { get; set; }

        // Lists to keep track of dynamically added controls
        public List<ComboBox> ExistingTableCombos { get; set; } = new List<ComboBox>();
        public List<ComboBox> KeyCombos { get; set; } = new List<ComboBox>();
        public List<TextBox> SourceTableCombos { get; set; } = new List<TextBox>();
        public List<ComboBox> SourceKeyCombos { get; set; } = new List<ComboBox>();
        public List<CheckBox> SelectedCheck { get; set; } = new List<CheckBox>();
    }
}
