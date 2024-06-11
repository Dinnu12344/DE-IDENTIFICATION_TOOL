namespace DE_IDENTIFICATION_TOOL.Forms
{
    partial class LogViewForm
    {
        private System.Windows.Forms.ListBox dateListBox;
        private System.Windows.Forms.TextBox logTextBox;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogViewForm));
            this.dateListBox = new System.Windows.Forms.ListBox();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // dateListBox
            // 
            this.dateListBox.FormattingEnabled = true;
            this.dateListBox.Location = new System.Drawing.Point(12, 12);
            this.dateListBox.Name = "dateListBox";
            this.dateListBox.Size = new System.Drawing.Size(200, 290);
            this.dateListBox.TabIndex = 0;
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(230, 12);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(300, 290);
            this.logTextBox.TabIndex = 1;
            // 
            // LogViewForm
            // 
            this.ClientSize = new System.Drawing.Size(550, 320);
            this.Controls.Add(this.dateListBox);
            this.Controls.Add(this.logTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogViewForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
