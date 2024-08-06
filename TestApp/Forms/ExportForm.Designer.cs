using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
namespace DE_IDENTIFICATION_TOOL
{
    partial class ExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
            this.radioBtnForCsvExport = new System.Windows.Forms.RadioButton();
            this.radioBtnDatabaseExport = new System.Windows.Forms.RadioButton();
            this.btnForNext = new System.Windows.Forms.Button();
            this.btnForCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioBtnForCsvExport
            // 
            this.radioBtnForCsvExport.AutoSize = true;
            this.radioBtnForCsvExport.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnForCsvExport.Location = new System.Drawing.Point(80, 83);
            this.radioBtnForCsvExport.Margin = new System.Windows.Forms.Padding(2);
            this.radioBtnForCsvExport.Name = "radioBtnForCsvExport";
            this.radioBtnForCsvExport.Size = new System.Drawing.Size(58, 27);
            this.radioBtnForCsvExport.TabIndex = 0;
            this.radioBtnForCsvExport.TabStop = true;
            this.radioBtnForCsvExport.Text = "CSV";
            this.radioBtnForCsvExport.UseVisualStyleBackColor = true;
            // 
            // radioBtnDatabaseExport
            // 
            this.radioBtnDatabaseExport.AutoSize = true;
            this.radioBtnDatabaseExport.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnDatabaseExport.Location = new System.Drawing.Point(80, 144);
            this.radioBtnDatabaseExport.Margin = new System.Windows.Forms.Padding(2);
            this.radioBtnDatabaseExport.Name = "radioBtnDatabaseExport";
            this.radioBtnDatabaseExport.Size = new System.Drawing.Size(102, 27);
            this.radioBtnDatabaseExport.TabIndex = 1;
            this.radioBtnDatabaseExport.TabStop = true;
            this.radioBtnDatabaseExport.Text = "Database";
            this.radioBtnDatabaseExport.UseVisualStyleBackColor = true;
            // 
            // btnForNext
            // 
            this.btnForNext.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForNext.Location = new System.Drawing.Point(127, 265);
            this.btnForNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnForNext.Name = "btnForNext";
            this.btnForNext.Size = new System.Drawing.Size(70, 30);
            this.btnForNext.TabIndex = 2;
            this.btnForNext.Text = "Next";
            this.btnForNext.UseVisualStyleBackColor = true;
            this.btnForNext.Click += new System.EventHandler(this.btnForNext_Click);
            // 
            // btnForCancel
            // 
            this.btnForCancel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForCancel.Location = new System.Drawing.Point(244, 265);
            this.btnForCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnForCancel.Name = "btnForCancel";
            this.btnForCancel.Size = new System.Drawing.Size(70, 30);
            this.btnForCancel.TabIndex = 3;
            this.btnForCancel.Text = "Cancel";
            this.btnForCancel.UseVisualStyleBackColor = true;
            this.btnForCancel.Click += new System.EventHandler(this.btnForCancel_Click);
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 374);
            this.Controls.Add(this.btnForCancel);
            this.Controls.Add(this.btnForNext);
            this.Controls.Add(this.radioBtnDatabaseExport);
            this.Controls.Add(this.radioBtnForCsvExport);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ExportForm";
            this.Text = "De-IdentifyForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RadioButton radioBtnForCsvExport;
        private RadioButton radioBtnDatabaseExport;
        private Button btnForNext;
        private Button btnForCancel;
    }
}