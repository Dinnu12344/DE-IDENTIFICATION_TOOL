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

        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Button btnCreateProject;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateProjectForm));
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.btnCreateProject = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtProjectName
            // 
            this.txtProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjectName.Location = new System.Drawing.Point(73, 120);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(240, 38);
            this.txtProjectName.TabIndex = 0;
            // 
            // btnCreateProject
            // 
            this.btnCreateProject.Location = new System.Drawing.Point(350, 118);
            this.btnCreateProject.Name = "btnCreateProject";
            this.btnCreateProject.Size = new System.Drawing.Size(102, 36);
            this.btnCreateProject.TabIndex = 1;
            this.btnCreateProject.Text = "Create Project";
            this.btnCreateProject.UseVisualStyleBackColor = true;
            this.btnCreateProject.Click += new System.EventHandler(this.BtnCreateProject_Click);
            // 
            // CreateProjectForm
            // 
            this.ClientSize = new System.Drawing.Size(610, 354);
            this.Controls.Add(this.btnCreateProject);
            this.Controls.Add(this.txtProjectName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateProjectForm";
            this.Text = "Create Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}