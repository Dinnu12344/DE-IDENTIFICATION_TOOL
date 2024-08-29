using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
namespace DE_IDENTIFICATION_TOOL
{
    partial class ImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
            this.radioCheckCSV = new System.Windows.Forms.RadioButton();
            this.radioCheckDatabase = new System.Windows.Forms.RadioButton();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioCheckJson = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radioCheckCSV
            // 
            this.radioCheckCSV.AutoSize = true;
            this.radioCheckCSV.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioCheckCSV.Location = new System.Drawing.Point(58, 42);
            this.radioCheckCSV.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioCheckCSV.Name = "radioCheckCSV";
            this.radioCheckCSV.Size = new System.Drawing.Size(52, 23);
            this.radioCheckCSV.TabIndex = 0;
            this.radioCheckCSV.TabStop = true;
            this.radioCheckCSV.Text = "CSV";
            this.radioCheckCSV.UseVisualStyleBackColor = false;
            // 
            // radioCheckDatabase
            // 
            this.radioCheckDatabase.AutoSize = true;
            this.radioCheckDatabase.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioCheckDatabase.Location = new System.Drawing.Point(58, 116);
            this.radioCheckDatabase.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioCheckDatabase.Name = "radioCheckDatabase";
            this.radioCheckDatabase.Size = new System.Drawing.Size(90, 23);
            this.radioCheckDatabase.TabIndex = 1;
            this.radioCheckDatabase.TabStop = true;
            this.radioCheckDatabase.Text = "Database";
            this.radioCheckDatabase.UseVisualStyleBackColor = true;
            //this.radioCheckDatabase.CheckedChanged += new System.EventHandler(this.radioCheckDatabase_CheckedChanged);
            // 
            // buttonNext
            // 
            this.buttonNext.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNext.Location = new System.Drawing.Point(58, 255);
            this.buttonNext.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(70, 30);
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(152, 255);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(70, 30);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // radioCheckJson
            // 
            this.radioCheckJson.AutoSize = true;
            this.radioCheckJson.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioCheckJson.Location = new System.Drawing.Point(58, 188);
            this.radioCheckJson.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioCheckJson.Name = "radioCheckJson";
            this.radioCheckJson.Size = new System.Drawing.Size(56, 23);
            this.radioCheckJson.TabIndex = 4;
            this.radioCheckJson.TabStop = true;
            this.radioCheckJson.Text = "Json";
            this.radioCheckJson.UseVisualStyleBackColor = true;
            //this.radioCheckJson.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.radioCheckJson);
            this.Controls.Add(this.radioCheckCSV);
            this.Controls.Add(this.radioCheckDatabase);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "ImportForm";
            this.Text = "ImportForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private RadioButton radioCheckJson;
        private RadioButton radioCheckCSV;
        private RadioButton radioCheckDatabase;
        private Button buttonNext;
        private Button buttonCancel;
    }
}