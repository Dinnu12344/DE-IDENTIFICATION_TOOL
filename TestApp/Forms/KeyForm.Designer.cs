using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    partial class KeyForm
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyForm));
            this.labelForKey = new System.Windows.Forms.Label();
            this.textBoxForKey = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnForSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelForKey
            // 
            this.labelForKey.AutoSize = true;
            this.labelForKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelForKey.Location = new System.Drawing.Point(81, 110);
            this.labelForKey.Name = "labelForKey";
            this.labelForKey.Size = new System.Drawing.Size(199, 24);
            this.labelForKey.TabIndex = 0;
            this.labelForKey.Text = "Please Enter Your Key";
            // 
            // textBoxForKey
            // 
            this.textBoxForKey.Location = new System.Drawing.Point(334, 114);
            this.textBoxForKey.Name = "textBoxForKey";
            this.textBoxForKey.Size = new System.Drawing.Size(238, 20);
            this.textBoxForKey.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(381, 230);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // btnForSave
            // 
            this.btnForSave.Enabled = false;
            this.btnForSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForSave.Location = new System.Drawing.Point(497, 230);
            this.btnForSave.Name = "btnForSave";
            this.btnForSave.Size = new System.Drawing.Size(75, 30);
            this.btnForSave.TabIndex = 2;
            this.btnForSave.Text = "Save";
            this.btnForSave.UseVisualStyleBackColor = true;
            this.btnForSave.Click += new System.EventHandler(this.SaveKeyData);
            // 
            // KeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnForSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.textBoxForKey);
            this.Controls.Add(this.labelForKey);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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