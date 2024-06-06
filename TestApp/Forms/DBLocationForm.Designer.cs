namespace DE_IDENTIFICATION_TOOL.Forms
{
    partial class DBLocationForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelInDBLocationForm = new System.Windows.Forms.Panel();
            this.lblForJdbcSubPara = new System.Windows.Forms.Label();
            this.lblForJdbc = new System.Windows.Forms.Label();
            this.lblForTypeInJdbcFrm = new System.Windows.Forms.Label();
            this.dbTypedd = new System.Windows.Forms.ComboBox();
            this.panelHoldingButtonsInJdbcWindow = new System.Windows.Forms.Panel();
            this.btnForFinish = new System.Windows.Forms.Button();
            this.btnForCancelInJdbcFrm = new System.Windows.Forms.Button();
            this.btnFroNextinJdbcFrm = new System.Windows.Forms.Button();
            this.btnForBackInJdbcFrm = new System.Windows.Forms.Button();
            this.lblForServer = new System.Windows.Forms.Label();
            this.lblForUserName = new System.Windows.Forms.Label();
            this.lblForPassword = new System.Windows.Forms.Label();
            this.txtForServer = new System.Windows.Forms.TextBox();
            this.txtForUsername = new System.Windows.Forms.TextBox();
            this.txtForPassword = new System.Windows.Forms.TextBox();
            this.panelInDBLocationForm.SuspendLayout();
            this.panelHoldingButtonsInJdbcWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelInDBLocationForm
            // 
            this.panelInDBLocationForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelInDBLocationForm.Controls.Add(this.lblForJdbcSubPara);
            this.panelInDBLocationForm.Controls.Add(this.lblForJdbc);
            this.panelInDBLocationForm.Location = new System.Drawing.Point(1, 1);
            this.panelInDBLocationForm.Name = "panelInDBLocationForm";
            this.panelInDBLocationForm.Size = new System.Drawing.Size(800, 123);
            this.panelInDBLocationForm.TabIndex = 0;
            // 
            // lblForJdbcSubPara
            // 
            this.lblForJdbcSubPara.AutoSize = true;
            this.lblForJdbcSubPara.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForJdbcSubPara.Location = new System.Drawing.Point(36, 80);
            this.lblForJdbcSubPara.Name = "lblForJdbcSubPara";
            this.lblForJdbcSubPara.Size = new System.Drawing.Size(367, 20);
            this.lblForJdbcSubPara.TabIndex = 1;
            this.lblForJdbcSubPara.Text = "Please provide the requested imformation below";
            // 
            // lblForJdbc
            // 
            this.lblForJdbc.AutoSize = true;
            this.lblForJdbc.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForJdbc.Location = new System.Drawing.Point(33, 35);
            this.lblForJdbc.Name = "lblForJdbc";
            this.lblForJdbc.Size = new System.Drawing.Size(75, 29);
            this.lblForJdbc.TabIndex = 0;
            this.lblForJdbc.Text = "JDBC";
            // 
            // lblForTypeInJdbcFrm
            // 
            this.lblForTypeInJdbcFrm.AutoSize = true;
            this.lblForTypeInJdbcFrm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForTypeInJdbcFrm.Location = new System.Drawing.Point(38, 158);
            this.lblForTypeInJdbcFrm.Name = "lblForTypeInJdbcFrm";
            this.lblForTypeInJdbcFrm.Size = new System.Drawing.Size(45, 20);
            this.lblForTypeInJdbcFrm.TabIndex = 1;
            this.lblForTypeInJdbcFrm.Text = "Type";
            // 
            // dbTypedd
            // 
            this.dbTypedd.FormattingEnabled = true;
            this.dbTypedd.Items.AddRange(new object[] {
            "SQL",
            "Oracel"});
            this.dbTypedd.Location = new System.Drawing.Point(185, 154);
            this.dbTypedd.Name = "dbTypedd";
            this.dbTypedd.Size = new System.Drawing.Size(344, 24);
            this.dbTypedd.TabIndex = 2;
            this.dbTypedd.SelectedIndexChanged += new System.EventHandler(this.dbTypedd_SelectedIndexChanged);
            // 
            // panelHoldingButtonsInJdbcWindow
            // 
            this.panelHoldingButtonsInJdbcWindow.Controls.Add(this.btnForFinish);
            this.panelHoldingButtonsInJdbcWindow.Controls.Add(this.btnForCancelInJdbcFrm);
            this.panelHoldingButtonsInJdbcWindow.Controls.Add(this.btnFroNextinJdbcFrm);
            this.panelHoldingButtonsInJdbcWindow.Controls.Add(this.btnForBackInJdbcFrm);
            this.panelHoldingButtonsInJdbcWindow.Location = new System.Drawing.Point(1, 469);
            this.panelHoldingButtonsInJdbcWindow.Name = "panelHoldingButtonsInJdbcWindow";
            this.panelHoldingButtonsInJdbcWindow.Size = new System.Drawing.Size(800, 73);
            this.panelHoldingButtonsInJdbcWindow.TabIndex = 3;
            // 
            // btnForFinish
            // 
            this.btnForFinish.Location = new System.Drawing.Point(617, 26);
            this.btnForFinish.Name = "btnForFinish";
            this.btnForFinish.Size = new System.Drawing.Size(75, 23);
            this.btnForFinish.TabIndex = 3;
            this.btnForFinish.Text = "Finish";
            this.btnForFinish.UseVisualStyleBackColor = true;
            this.btnForFinish.Click += new System.EventHandler(this.btnForFinish_Click);
            // 
            // btnForCancelInJdbcFrm
            // 
            this.btnForCancelInJdbcFrm.Location = new System.Drawing.Point(712, 26);
            this.btnForCancelInJdbcFrm.Name = "btnForCancelInJdbcFrm";
            this.btnForCancelInJdbcFrm.Size = new System.Drawing.Size(75, 23);
            this.btnForCancelInJdbcFrm.TabIndex = 2;
            this.btnForCancelInJdbcFrm.Text = "Cancel";
            this.btnForCancelInJdbcFrm.UseVisualStyleBackColor = true;
            this.btnForCancelInJdbcFrm.Click += new System.EventHandler(this.btnForCancelInJdbcFrm_Click);
            // 
            // btnFroNextinJdbcFrm
            // 
            this.btnFroNextinJdbcFrm.Location = new System.Drawing.Point(516, 26);
            this.btnFroNextinJdbcFrm.Name = "btnFroNextinJdbcFrm";
            this.btnFroNextinJdbcFrm.Size = new System.Drawing.Size(75, 23);
            this.btnFroNextinJdbcFrm.TabIndex = 1;
            this.btnFroNextinJdbcFrm.Text = "Next";
            this.btnFroNextinJdbcFrm.UseVisualStyleBackColor = true;
            this.btnFroNextinJdbcFrm.Click += new System.EventHandler(this.btnForNextinJdbcFrm_Click);
            // 
            // btnForBackInJdbcFrm
            // 
            this.btnForBackInJdbcFrm.Location = new System.Drawing.Point(412, 26);
            this.btnForBackInJdbcFrm.Name = "btnForBackInJdbcFrm";
            this.btnForBackInJdbcFrm.Size = new System.Drawing.Size(75, 23);
            this.btnForBackInJdbcFrm.TabIndex = 0;
            this.btnForBackInJdbcFrm.Text = "Back";
            this.btnForBackInJdbcFrm.UseVisualStyleBackColor = true;
            this.btnForBackInJdbcFrm.Click += new System.EventHandler(this.btnForBackInJdbcFrm_Click);
            // 
            // lblForServer
            // 
            this.lblForServer.AutoSize = true;
            this.lblForServer.Location = new System.Drawing.Point(38, 215);
            this.lblForServer.Name = "lblForServer";
            this.lblForServer.Size = new System.Drawing.Size(47, 16);
            this.lblForServer.TabIndex = 4;
            this.lblForServer.Text = "Server";
            // 
            // lblForUserName
            // 
            this.lblForUserName.AutoSize = true;
            this.lblForUserName.Location = new System.Drawing.Point(36, 279);
            this.lblForUserName.Name = "lblForUserName";
            this.lblForUserName.Size = new System.Drawing.Size(73, 16);
            this.lblForUserName.TabIndex = 6;
            this.lblForUserName.Text = "UserName";
            // 
            // lblForPassword
            // 
            this.lblForPassword.AutoSize = true;
            this.lblForPassword.Location = new System.Drawing.Point(36, 337);
            this.lblForPassword.Name = "lblForPassword";
            this.lblForPassword.Size = new System.Drawing.Size(67, 16);
            this.lblForPassword.TabIndex = 7;
            this.lblForPassword.Text = "Password";
            // 
            // txtForServer
            // 
            this.txtForServer.Location = new System.Drawing.Point(185, 209);
            this.txtForServer.Name = "txtForServer";
            this.txtForServer.Size = new System.Drawing.Size(344, 22);
            this.txtForServer.TabIndex = 9;
            // 
            // txtForUsername
            // 
            this.txtForUsername.Location = new System.Drawing.Point(185, 273);
            this.txtForUsername.Name = "txtForUsername";
            this.txtForUsername.Size = new System.Drawing.Size(344, 22);
            this.txtForUsername.TabIndex = 11;
            // 
            // txtForPassword
            // 
            this.txtForPassword.Location = new System.Drawing.Point(185, 331);
            this.txtForPassword.Name = "txtForPassword";
            this.txtForPassword.Size = new System.Drawing.Size(344, 22);
            this.txtForPassword.TabIndex = 12;
            // 
            // DBLocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 543);
            this.Controls.Add(this.txtForPassword);
            this.Controls.Add(this.txtForUsername);
            this.Controls.Add(this.txtForServer);
            this.Controls.Add(this.lblForPassword);
            this.Controls.Add(this.lblForUserName);
            this.Controls.Add(this.lblForServer);
            this.Controls.Add(this.panelHoldingButtonsInJdbcWindow);
            this.Controls.Add(this.dbTypedd);
            this.Controls.Add(this.lblForTypeInJdbcFrm);
            this.Controls.Add(this.panelInDBLocationForm);
            this.Name = "DBLocationForm";
            this.Text = "DBLocationForm";
            this.panelInDBLocationForm.ResumeLayout(false);
            this.panelInDBLocationForm.PerformLayout();
            this.panelHoldingButtonsInJdbcWindow.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelInDBLocationForm;
        private System.Windows.Forms.Label lblForJdbcSubPara;
        private System.Windows.Forms.Label lblForJdbc;
        private System.Windows.Forms.Label lblForTypeInJdbcFrm;
        private System.Windows.Forms.ComboBox dbTypedd;
        private System.Windows.Forms.Panel panelHoldingButtonsInJdbcWindow;
        private System.Windows.Forms.Button btnForCancelInJdbcFrm;
        private System.Windows.Forms.Button btnFroNextinJdbcFrm;
        private System.Windows.Forms.Button btnForBackInJdbcFrm;
        private System.Windows.Forms.Label lblForServer;
        private System.Windows.Forms.Label lblForUserName;
        private System.Windows.Forms.Label lblForPassword;
        private System.Windows.Forms.TextBox txtForServer;
        private System.Windows.Forms.TextBox txtForUsername;
        private System.Windows.Forms.TextBox txtForPassword;
        private System.Windows.Forms.Button btnForFinish;
    }
}