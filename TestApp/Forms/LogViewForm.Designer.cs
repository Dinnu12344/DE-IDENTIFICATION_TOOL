using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    partial class LogViewForm
    {
        private ListBox dateListBox;
        private RichTextBox logRichTextBox;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogViewForm));
            this.dateListBox = new System.Windows.Forms.ListBox();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // dateListBox
            // 
            this.dateListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateListBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateListBox.FormattingEnabled = true;
            this.dateListBox.IntegralHeight = false;
            this.dateListBox.ItemHeight = 15;
            this.dateListBox.Location = new System.Drawing.Point(0, 0);
            this.dateListBox.Name = "dateListBox";
            this.dateListBox.Size = new System.Drawing.Size(200, 450);
            this.dateListBox.TabIndex = 0;
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logRichTextBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logRichTextBox.Location = new System.Drawing.Point(200, 0);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.Size = new System.Drawing.Size(600, 450);
            this.logRichTextBox.TabIndex = 1;
            this.logRichTextBox.Text = "";
            // 
            // LogViewForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.logRichTextBox);
            this.Controls.Add(this.dateListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogViewForm";
            this.Text = "LogView";
            this.ResumeLayout(false);

        }
    }
}
