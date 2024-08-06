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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReNameForm));
            this.lblForRename = new System.Windows.Forms.Label();
            this.txtBoxForRename = new System.Windows.Forms.TextBox();
            this.btnForRename = new System.Windows.Forms.Button();
            this.btnForCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblForRename
            // 
            this.lblForRename.AutoSize = true;
            this.lblForRename.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForRename.Location = new System.Drawing.Point(74, 81);
            this.lblForRename.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblForRename.Name = "lblForRename";
            this.lblForRename.Size = new System.Drawing.Size(143, 19);
            this.lblForRename.TabIndex = 0;
            this.lblForRename.Text = "Enter the New Name";
            // 
            // txtBoxForRename
            // 
            this.txtBoxForRename.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxForRename.Location = new System.Drawing.Point(271, 82);
            this.txtBoxForRename.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxForRename.Name = "txtBoxForRename";
            this.txtBoxForRename.Size = new System.Drawing.Size(289, 27);
            this.txtBoxForRename.TabIndex = 1;
            // 
            // btnForRename
            // 
            this.btnForRename.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForRename.Location = new System.Drawing.Point(376, 173);
            this.btnForRename.Margin = new System.Windows.Forms.Padding(2);
            this.btnForRename.Name = "btnForRename";
            this.btnForRename.Size = new System.Drawing.Size(70, 30);
            this.btnForRename.TabIndex = 2;
            this.btnForRename.Text = "Rename";
            this.btnForRename.UseVisualStyleBackColor = true;
            this.btnForRename.Click += new System.EventHandler(this.btnForRename_Click);
            // 
            // btnForCancel
            // 
            this.btnForCancel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForCancel.Location = new System.Drawing.Point(490, 173);
            this.btnForCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnForCancel.Name = "btnForCancel";
            this.btnForCancel.Size = new System.Drawing.Size(70, 30);
            this.btnForCancel.TabIndex = 3;
            this.btnForCancel.Text = "Cancel";
            this.btnForCancel.UseVisualStyleBackColor = true;
            // 
            // ReNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 422);
            this.Controls.Add(this.btnForCancel);
            this.Controls.Add(this.btnForRename);
            this.Controls.Add(this.txtBoxForRename);
            this.Controls.Add(this.lblForRename);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
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