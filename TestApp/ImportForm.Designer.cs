namespace TestApp
{
    partial class ImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
            this.radioCheckCSV = new System.Windows.Forms.RadioButton();
            this.radioCheckDatabase = new System.Windows.Forms.RadioButton();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImportForm";
            this.Text = "ImportForm";
            this.ResumeLayout(false);

            // 
            // radioCheckCSV
            // 
            this.radioCheckCSV.AutoSize = true;
            this.radioCheckCSV.Location = new System.Drawing.Point(50, 30);
            this.radioCheckCSV.Name = "radioCheckCSV";
            this.radioCheckCSV.Size = new System.Drawing.Size(47, 17);
            this.radioCheckCSV.TabIndex = 0;
            this.radioCheckCSV.TabStop = true;
            this.radioCheckCSV.Text = "CSV";
            this.radioCheckCSV.UseVisualStyleBackColor = true;
            // 
            // radioCheckDatabase
            // 
            this.radioCheckDatabase.AutoSize = true;
            this.radioCheckDatabase.Location = new System.Drawing.Point(50, 60);
            this.radioCheckDatabase.Name = "radioCheckDatabase";
            this.radioCheckDatabase.Size = new System.Drawing.Size(74, 17);
            this.radioCheckDatabase.TabIndex = 1;
            this.radioCheckDatabase.TabStop = true;
            this.radioCheckDatabase.Text = "Database";
            this.radioCheckDatabase.UseVisualStyleBackColor = true;
            // 
            // nextButton
            // 
            this.buttonNext.Location = new System.Drawing.Point(50, 100);
            this.buttonNext.Name = "nextButton";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // cancelButton
            // 
            this.buttonCancel.Location = new System.Drawing.Point(150, 100);
            this.buttonCancel.Name = "cancelButton";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // ImportForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.radioCheckCSV);
            this.Controls.Add(this.radioCheckDatabase);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonCancel);
            this.Name = "ImportForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}