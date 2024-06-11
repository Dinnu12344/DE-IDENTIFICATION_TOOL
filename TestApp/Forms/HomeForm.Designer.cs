using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
namespace DE_IDENTIFICATION_TOOL
{
    partial class HomeForm
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

        private TreeView treeView;

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(HomeForm));
            this.treeView = new TreeView();
            this.menuStrip1 = new MenuStrip();
            this.fileToolStripMenuItem = new ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new ToolStripMenuItem();
            this.exportDataToolStripMenuItem = new ToolStripMenuItem();
            this.reToolStripMenuItem = new ToolStripMenuItem();
            this.editToolStripMenuItem = new ToolStripMenuItem();
            this.viewToolStripMenuItem = new ToolStripMenuItem();
            this.monitoeToolStripMenuItem = new ToolStripMenuItem();
            this.helpToolStripMenuItem = new ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Location = new Point(1, 32);
            this.treeView.Name = "treeView";
            this.treeView.Size = new Size(211, 532);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = SystemColors.ActiveCaption;
            this.menuStrip1.ImageScalingSize = new Size(20, 20);
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.monitoeToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(953, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.exportDataToolStripMenuItem,
            this.reToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new Size(178, 26);
            this.newProjectToolStripMenuItem.Text = "New Project";
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new Size(178, 26);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            // 
            // exportDataToolStripMenuItem
            // 
            this.exportDataToolStripMenuItem.Name = "exportDataToolStripMenuItem";
            this.exportDataToolStripMenuItem.Size = new Size(178, 26);
            this.exportDataToolStripMenuItem.Text = "Export Data";
            // 
            // reToolStripMenuItem
            // 
            this.reToolStripMenuItem.Name = "reToolStripMenuItem";
            this.reToolStripMenuItem.Size = new Size(178, 26);
            this.reToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new Size(49, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new Size(55, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // monitoeToolStripMenuItem
            // 
            this.monitoeToolStripMenuItem.Name = "monitoeToolStripMenuItem";
            this.monitoeToolStripMenuItem.Size = new Size(76, 24);
            this.monitoeToolStripMenuItem.Text = "Monitor";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // HomeForm
            // 
            this.ClientSize = new Size(953, 564);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "HomeForm";
            this.Text = "Cognine De-Identification Tool";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newProjectToolStripMenuItem;
        private ToolStripMenuItem openProjectToolStripMenuItem;
        private ToolStripMenuItem exportDataToolStripMenuItem;
        private ToolStripMenuItem reToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem monitoeToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
    }
}

