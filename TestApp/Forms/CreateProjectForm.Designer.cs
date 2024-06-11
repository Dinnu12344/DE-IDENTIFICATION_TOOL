using System.Windows.Forms;
using System.Drawing;
namespace DE_IDENTIFICATION_TOOL
{
    partial class CreateProjectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        private TextBox txtProjectName;
        private Button btnCreateProject;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateProjectForm));
            this.txtProjectName = new TextBox();
            this.btnCreateProject = new Button();
            this.SuspendLayout();
            // 
            // txtProjectName
            // 
            this.txtProjectName.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.txtProjectName.Location = new Point(73, 120);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new Size(240, 38);
            this.txtProjectName.TabIndex = 0;
            // 
            // btnCreateProject
            // 
            this.btnCreateProject.Location = new Point(350, 118);
            this.btnCreateProject.Name = "btnCreateProject";
            this.btnCreateProject.Size = new Size(102, 36);
            this.btnCreateProject.TabIndex = 1;
            this.btnCreateProject.Text = "Create Project";
            this.btnCreateProject.UseVisualStyleBackColor = true;
            this.btnCreateProject.Click += new System.EventHandler(this.BtnCreateProject_Click);
            // 
            // CreateProjectForm
            // 
            this.ClientSize = new Size(610, 354);
            this.Controls.Add(this.btnCreateProject);
            this.Controls.Add(this.txtProjectName);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateProjectForm";
            this.Text = "Create Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}