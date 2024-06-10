using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_IDENTIFICATION_TOOL.Models
{
    public class CSVLocationFormModel
    {
        public string SelectedCsvFilePath { get; set; }
        public string SelectedDelimiter { get; set; }
        public string SelectedQuote { get; set; }
        public string EnteredText { get; set; }
        public string TableName { get; set; }
    }
}
