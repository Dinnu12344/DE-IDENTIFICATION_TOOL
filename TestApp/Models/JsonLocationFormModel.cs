using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.Models
{
    public class JsonLocationFormModel
    {
        public string SelectedCsvFilePath { get; set; }
        public string EnteredText { get; set; }

        public TreeNode selectedNode { get; set; }
        public List<ProjectData> projectData { get; set; }
        public HomeForm homeForm { get; set; }


    }
}
