using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
namespace DE_IDENTIFICATION_TOOL
{
    partial class ConfigForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ConfigForm));
            this.SuspendLayout();
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(787, 297);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.ResumeLayout(false);

        }

        #endregion
    }
}