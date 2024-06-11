using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
namespace DE_IDENTIFICATION_TOOL.Forms
{
    partial class LogViewForm
    {
        private ListBox dateListBox;
        private TextBox logTextBox;

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(LogViewForm));
            this.dateListBox = new ListBox();
            this.logTextBox = new TextBox();
            this.SuspendLayout();
            // 
            // dateListBox
            // 
            this.dateListBox.FormattingEnabled = true;
            this.dateListBox.Location = new Point(12, 12);
            this.dateListBox.Name = "dateListBox";
            this.dateListBox.Size = new Size(200, 290);
            this.dateListBox.TabIndex = 0;
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new Point(230, 12);
            this.logTextBox.Multiline = true;
            this.logTextBox.ScrollBars = ScrollBars.Vertical;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new Size(300, 290);
            this.logTextBox.TabIndex = 1;
            // 
            // LogViewForm
            // 
            this.ClientSize = new Size(550, 320);
            this.Controls.Add(this.dateListBox);
            this.Controls.Add(this.logTextBox);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogViewForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
