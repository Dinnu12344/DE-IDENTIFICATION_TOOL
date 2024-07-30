using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    partial class LogViewForm
    {
        private ListBox dateListBox;
        private ListBox logTextBox;

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(LogViewForm));
            this.dateListBox = new ListBox();
            this.logTextBox = new ListBox();
            this.SuspendLayout();
            // 
            // dateListBox
            // 
            this.dateListBox.Dock = DockStyle.Left;
            this.dateListBox.FormattingEnabled = true;
            this.dateListBox.Location = new Point(0, 0);
            this.dateListBox.Name = "dateListBox";
            this.dateListBox.Size = new Size(200, 450);
            this.dateListBox.TabIndex = 0;
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = DockStyle.Fill;
            this.logTextBox.Location = new Point(200, 0);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new Size(600, 450);
            this.logTextBox.TabIndex = 1;
            // 
            // LogViewForm
            // 
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.dateListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogViewForm";
            this.Text = "LogView";
            this.ResumeLayout(false);
        }
    }
}