using System.Windows.Forms;
namespace DE_IDENTIFICATION_TOOL
{
    partial class ExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
            this.radioBtnForCsvExport = new RadioButton();
            this.radioBtnDatabaseExport = new RadioButton();
            this.btnForNext = new Button();
            this.btnForCancel = new Button();
            this.SuspendLayout();
            // 
            // radioBtnForCsvExport
            // 
            this.radioBtnForCsvExport.AutoSize = true;
            this.radioBtnForCsvExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnForCsvExport.Location = new System.Drawing.Point(92, 89);
            this.radioBtnForCsvExport.Name = "radioBtnForCsvExport";
            this.radioBtnForCsvExport.Size = new System.Drawing.Size(63, 29);
            this.radioBtnForCsvExport.TabIndex = 0;
            this.radioBtnForCsvExport.TabStop = true;
            this.radioBtnForCsvExport.Text = "csv";
            this.radioBtnForCsvExport.UseVisualStyleBackColor = true;
            // 
            // radioBtnDatabaseExport
            // 
            this.radioBtnDatabaseExport.AutoSize = true;
            this.radioBtnDatabaseExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnDatabaseExport.Location = new System.Drawing.Point(92, 154);
            this.radioBtnDatabaseExport.Name = "radioBtnDatabaseExport";
            this.radioBtnDatabaseExport.Size = new System.Drawing.Size(117, 29);
            this.radioBtnDatabaseExport.TabIndex = 1;
            this.radioBtnDatabaseExport.TabStop = true;
            this.radioBtnDatabaseExport.Text = "Database";
            this.radioBtnDatabaseExport.UseVisualStyleBackColor = true;
            // 
            // btnForNext
            // 
            this.btnForNext.Location = new System.Drawing.Point(145, 283);
            this.btnForNext.Name = "btnForNext";
            this.btnForNext.Size = new System.Drawing.Size(75, 23);
            this.btnForNext.TabIndex = 2;
            this.btnForNext.Text = "Next";
            this.btnForNext.UseVisualStyleBackColor = true;
            this.btnForNext.Click += new System.EventHandler(this.btnForNext_Click);
            // 
            // btnForCancel
            // 
            this.btnForCancel.Location = new System.Drawing.Point(279, 283);
            this.btnForCancel.Name = "btnForCancel";
            this.btnForCancel.Size = new System.Drawing.Size(75, 23);
            this.btnForCancel.TabIndex = 3;
            this.btnForCancel.Text = "Cancel";
            this.btnForCancel.UseVisualStyleBackColor = true;
            // 
            // DeIdentifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 399);
            this.Controls.Add(this.btnForCancel);
            this.Controls.Add(this.btnForNext);
            this.Controls.Add(this.radioBtnDatabaseExport);
            this.Controls.Add(this.radioBtnForCsvExport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new Padding(4, 4, 4, 4);
            this.Name = "DeIdentifyForm";
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