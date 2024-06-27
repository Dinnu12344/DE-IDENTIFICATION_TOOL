namespace DE_IDENTIFICATION_TOOL.Forms
{
    partial class ReNameForm
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
            this.lblForRename = new System.Windows.Forms.Label();
            this.txtBoxForRename = new System.Windows.Forms.TextBox();
            this.btnForRename = new System.Windows.Forms.Button();
            this.btnForCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblForRename
            // 
            this.lblForRename.AutoSize = true;
            this.lblForRename.Location = new System.Drawing.Point(84, 86);
            this.lblForRename.Name = "lblForRename";
            this.lblForRename.Size = new System.Drawing.Size(129, 16);
            this.lblForRename.TabIndex = 0;
            this.lblForRename.Text = "Enter the New Name";
            // 
            // txtBoxForRename
            // 
            this.txtBoxForRename.Location = new System.Drawing.Point(272, 80);
            this.txtBoxForRename.Name = "txtBoxForRename";
            this.txtBoxForRename.Size = new System.Drawing.Size(340, 22);
            this.txtBoxForRename.TabIndex = 1;
            // 
            // btnForRename
            // 
            this.btnForRename.Location = new System.Drawing.Point(349, 176);
            this.btnForRename.Name = "btnForRename";
            this.btnForRename.Size = new System.Drawing.Size(75, 23);
            this.btnForRename.TabIndex = 2;
            this.btnForRename.Text = "Rename";
            this.btnForRename.UseVisualStyleBackColor = true;
            this.btnForRename.Click += new System.EventHandler(this.btnForRename_Click);
            // 
            // btnForCancel
            // 
            this.btnForCancel.Location = new System.Drawing.Point(471, 176);
            this.btnForCancel.Name = "btnForCancel";
            this.btnForCancel.Size = new System.Drawing.Size(75, 23);
            this.btnForCancel.TabIndex = 3;
            this.btnForCancel.Text = "Cancel";
            this.btnForCancel.UseVisualStyleBackColor = true;
            // 
            // ReNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnForCancel);
            this.Controls.Add(this.btnForRename);
            this.Controls.Add(this.txtBoxForRename);
            this.Controls.Add(this.lblForRename);
            this.Name = "ReNameForm";
            this.Text = "ReNameForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblForRename;
        private System.Windows.Forms.TextBox txtBoxForRename;
        private System.Windows.Forms.Button btnForRename;
        private System.Windows.Forms.Button btnForCancel;
    }
}