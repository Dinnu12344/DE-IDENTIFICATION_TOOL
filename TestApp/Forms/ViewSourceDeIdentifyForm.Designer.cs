using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
namespace DE_IDENTIFICATION_TOOL.Forms
{
    partial class ViewSourceDeIdentifyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ViewSourceDeIdentifyForm));
            this.SuspendLayout();
            // 
            // ViewSourceDeIdentifyForm
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewSourceDeIdentifyForm";
            this.Text = "View Data";
            this.ResumeLayout(false);

        }

        #endregion
    }
}