using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_IDENTIFICATION_TOOL.Models
{
    public class ColumnConfig
    {
        public string Column { get; set; }
        public string DataType { get; set; }
        public string Technique { get; set; }
        public string HippaRelatedColumn { get; set; }
        public string Keys { get; set; }
    }
}
