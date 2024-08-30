namespace DE_IDENTIFICATION_TOOL.Forms
{
    partial class DBLocationForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelInDBLocationForm;
        private System.Windows.Forms.Label lblForJdbcSubPara;
        private System.Windows.Forms.Label lblForJdbc;
        private System.Windows.Forms.Label lblForTypeInJdbcFrm;
        private System.Windows.Forms.ComboBox dbTyped;
        private System.Windows.Forms.Label lblForServer;
        private System.Windows.Forms.Label lblForUserName;
        private System.Windows.Forms.Label lblForPassword;
        private System.Windows.Forms.TextBox txtForServer;
        private System.Windows.Forms.TextBox txtForUsername;
        private System.Windows.Forms.TextBox txtForPassword;
        private System.Windows.Forms.Button btnForCancelInJdbcFrm;
        private System.Windows.Forms.Button btnForFinish;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBLocationForm));
            this.panelInDBLocationForm = new System.Windows.Forms.Panel();
            this.lblForJdbcSubPara = new System.Windows.Forms.Label();
            this.lblForJdbc = new System.Windows.Forms.Label();
            this.lblForTypeInJdbcFrm = new System.Windows.Forms.Label();
            this.dbTyped = new System.Windows.Forms.ComboBox();
            this.lblForServer = new System.Windows.Forms.Label();
            this.lblForUserName = new System.Windows.Forms.Label();
            this.lblForPassword = new System.Windows.Forms.Label();
            this.txtForServer = new System.Windows.Forms.TextBox();
            this.txtForUsername = new System.Windows.Forms.TextBox();
            this.txtForPassword = new System.Windows.Forms.TextBox();
            this.btnForCancelInJdbcFrm = new System.Windows.Forms.Button();
            this.btnForFinish = new System.Windows.Forms.Button();
            this.panelInDBLocationForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelInDBLocationForm
            // 
            this.panelInDBLocationForm.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panelInDBLocationForm.Controls.Add(this.lblForJdbcSubPara);
            this.panelInDBLocationForm.Controls.Add(this.lblForJdbc);
            this.panelInDBLocationForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInDBLocationForm.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelInDBLocationForm.Location = new System.Drawing.Point(0, 0);
            this.panelInDBLocationForm.Margin = new System.Windows.Forms.Padding(2);
            this.panelInDBLocationForm.Name = "panelInDBLocationForm";
            this.panelInDBLocationForm.Size = new System.Drawing.Size(953, 100);
            this.panelInDBLocationForm.TabIndex = 0;
            // 
            // lblForJdbcSubPara
            // 
            this.lblForJdbcSubPara.AutoSize = true;
            this.lblForJdbcSubPara.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForJdbcSubPara.Location = new System.Drawing.Point(27, 65);
            this.lblForJdbcSubPara.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblForJdbcSubPara.Name = "lblForJdbcSubPara";
            this.lblForJdbcSubPara.Size = new System.Drawing.Size(324, 19);
            this.lblForJdbcSubPara.TabIndex = 1;
            this.lblForJdbcSubPara.Text = "Please provide the requested imformation below";
            // 
            // lblForJdbc
            // 
            this.lblForJdbc.AutoSize = true;
            this.lblForJdbc.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForJdbc.Location = new System.Drawing.Point(25, 28);
            this.lblForJdbc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblForJdbc.Name = "lblForJdbc";
            this.lblForJdbc.Size = new System.Drawing.Size(93, 26);
            this.lblForJdbc.TabIndex = 0;
            this.lblForJdbc.Text = "DataBase";
            // 
            // lblForTypeInJdbcFrm
            // 
            this.lblForTypeInJdbcFrm.AutoSize = true;
            this.lblForTypeInJdbcFrm.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForTypeInJdbcFrm.Location = new System.Drawing.Point(49, 128);
            this.lblForTypeInJdbcFrm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblForTypeInJdbcFrm.Name = "lblForTypeInJdbcFrm";
            this.lblForTypeInJdbcFrm.Size = new System.Drawing.Size(39, 19);
            this.lblForTypeInJdbcFrm.TabIndex = 1;
            this.lblForTypeInJdbcFrm.Text = "Type";
            // 
            // dbTyped
            // 
            this.dbTyped.Cursor = System.Windows.Forms.Cursors.Default;
            this.dbTyped.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dbTyped.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbTyped.FormattingEnabled = true;
            this.dbTyped.Items.AddRange(new object[] {
            "SQL",
            "Oracle"});
            this.dbTyped.Location = new System.Drawing.Point(212, 124);
            this.dbTyped.Margin = new System.Windows.Forms.Padding(2);
            this.dbTyped.Name = "dbTyped";
            this.dbTyped.Size = new System.Drawing.Size(382, 23);
            this.dbTyped.TabIndex = 2;
            // 
            // lblForServer
            // 
            this.lblForServer.AutoSize = true;
            this.lblForServer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForServer.Location = new System.Drawing.Point(49, 175);
            this.lblForServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblForServer.Name = "lblForServer";
            this.lblForServer.Size = new System.Drawing.Size(49, 19);
            this.lblForServer.TabIndex = 4;
            this.lblForServer.Text = "Server";
            // 
            // lblForUserName
            // 
            this.lblForUserName.AutoSize = true;
            this.lblForUserName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForUserName.Location = new System.Drawing.Point(49, 227);
            this.lblForUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblForUserName.Name = "lblForUserName";
            this.lblForUserName.Size = new System.Drawing.Size(77, 19);
            this.lblForUserName.TabIndex = 6;
            this.lblForUserName.Text = "UserName";
            // 
            // lblForPassword
            // 
            this.lblForPassword.AutoSize = true;
            this.lblForPassword.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForPassword.Location = new System.Drawing.Point(49, 274);
            this.lblForPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblForPassword.Name = "lblForPassword";
            this.lblForPassword.Size = new System.Drawing.Size(71, 19);
            this.lblForPassword.TabIndex = 7;
            this.lblForPassword.Text = "Password";
            // 
            // txtForServer
            // 
            this.txtForServer.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForServer.Location = new System.Drawing.Point(212, 171);
            this.txtForServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtForServer.Name = "txtForServer";
            this.txtForServer.Size = new System.Drawing.Size(382, 23);
            this.txtForServer.TabIndex = 9;
            // 
            // txtForUsername
            // 
            this.txtForUsername.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForUsername.Location = new System.Drawing.Point(212, 223);
            this.txtForUsername.Margin = new System.Windows.Forms.Padding(2);
            this.txtForUsername.Name = "txtForUsername";
            this.txtForUsername.Size = new System.Drawing.Size(382, 23);
            this.txtForUsername.TabIndex = 11;
            // 
            // txtForPassword
            // 
            this.txtForPassword.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForPassword.Location = new System.Drawing.Point(212, 270);
            this.txtForPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtForPassword.Name = "txtForPassword";
            this.txtForPassword.Size = new System.Drawing.Size(382, 23);
            this.txtForPassword.TabIndex = 12;
            this.txtForPassword.UseSystemPasswordChar = true;
            // 
            // btnForCancelInJdbcFrm
            // 
            this.btnForCancelInJdbcFrm.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForCancelInJdbcFrm.Location = new System.Drawing.Point(515, 324);
            this.btnForCancelInJdbcFrm.Margin = new System.Windows.Forms.Padding(2);
            this.btnForCancelInJdbcFrm.Name = "btnForCancelInJdbcFrm";
            this.btnForCancelInJdbcFrm.Size = new System.Drawing.Size(79, 30);
            this.btnForCancelInJdbcFrm.TabIndex = 13;
            this.btnForCancelInJdbcFrm.Text = "Cancel";
            this.btnForCancelInJdbcFrm.UseVisualStyleBackColor = true;
            this.btnForCancelInJdbcFrm.Click += new System.EventHandler(this.btnForCancelInJdbcFrm_Click);
            // 
            // btnForFinish
            // 
            this.btnForFinish.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForFinish.Location = new System.Drawing.Point(423, 324);
            this.btnForFinish.Margin = new System.Windows.Forms.Padding(2);
            this.btnForFinish.Name = "btnForFinish";
            this.btnForFinish.Size = new System.Drawing.Size(79, 30);
            this.btnForFinish.TabIndex = 14;
            this.btnForFinish.Text = "Finish";
            this.btnForFinish.UseVisualStyleBackColor = true;
            this.btnForFinish.Click += new System.EventHandler(this.btnForFinish_Click);
            // 
            // DBLocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(953, 483);
            this.Controls.Add(this.btnForFinish);
            this.Controls.Add(this.btnForCancelInJdbcFrm);
            this.Controls.Add(this.txtForPassword);
            this.Controls.Add(this.txtForUsername);
            this.Controls.Add(this.txtForServer);
            this.Controls.Add(this.lblForPassword);
            this.Controls.Add(this.lblForUserName);
            this.Controls.Add(this.lblForServer);
            this.Controls.Add(this.dbTyped);
            this.Controls.Add(this.lblForTypeInJdbcFrm);
            this.Controls.Add(this.panelInDBLocationForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DBLocationForm";
            this.Text = "DBLocationForm";
            this.panelInDBLocationForm.ResumeLayout(false);
            this.panelInDBLocationForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Dispose method and other event handlers
    }
}
