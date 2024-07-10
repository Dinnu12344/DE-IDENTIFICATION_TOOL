using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
namespace DE_IDENTIFICATION_TOOL.Forms
{
    partial class KeyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyForm));
            this.labelForKey = new System.Windows.Forms.Label();
            this.textBoxForKey = new System.Windows.Forms.TextBox();
            this.btnForSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelForKey
            // 
            this.labelForKey.AutoSize = true;
            this.labelForKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelForKey.Location = new System.Drawing.Point(108, 135);
            this.labelForKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelForKey.Name = "labelForKey";
            this.labelForKey.Size = new System.Drawing.Size(255, 29);
            this.labelForKey.TabIndex = 0;
            this.labelForKey.Text = "Please Enter Your Key";
            // 
            // textBoxForKey
            // 
            this.textBoxForKey.Location = new System.Drawing.Point(445, 140);
            this.textBoxForKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxForKey.Name = "textBoxForKey";
            this.textBoxForKey.Size = new System.Drawing.Size(316, 22);
            this.textBoxForKey.TabIndex = 1;
            // 
            // btnForSave
            // 
            this.btnForSave.Enabled = false;
            this.btnForSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForSave.Location = new System.Drawing.Point(663, 283);
            this.btnForSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnForSave.Name = "btnForSave";
            this.btnForSave.Size = new System.Drawing.Size(100, 37);
            this.btnForSave.TabIndex = 2;
            this.btnForSave.Text = "Save";
            this.btnForSave.UseVisualStyleBackColor = true;
            this.btnForSave.Click += new System.EventHandler(this.SaveKeyData);
            // 
            // KeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.btnForSave);
            this.Controls.Add(this.textBoxForKey);
            this.Controls.Add(this.labelForKey);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "KeyForm";
            this.Text = "KeyForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelForKey;
        private TextBox textBoxForKey;
        private Button btnForSave;
    }
}