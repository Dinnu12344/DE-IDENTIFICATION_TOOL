namespace DE_IDENTIFICATION_TOOL.Forms
{
    partial class JsonLocationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JsonLocationForm));
            this.txtForTblName = new System.Windows.Forms.TextBox();
            this.lblForTblName = new System.Windows.Forms.Label();
            this.lblForNoofColumns = new System.Windows.Forms.Label();
            this.txtForNoofColumns = new System.Windows.Forms.TextBox();
            this.LocationBrowseButton = new System.Windows.Forms.Button();
            this.textBoxForHoldingFilePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CanclebtnforClear = new System.Windows.Forms.Button();
            this.finishButtonInCsvlocationWindow = new System.Windows.Forms.Button();
            this.btnForBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtForTblName
            // 
            this.txtForTblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForTblName.Location = new System.Drawing.Point(180, 236);
            this.txtForTblName.Name = "txtForTblName";
            this.txtForTblName.Size = new System.Drawing.Size(282, 27);
            this.txtForTblName.TabIndex = 23;
            this.txtForTblName.TextChanged += new System.EventHandler(this.TxtForTblName_TextChanged);
            // 
            // lblForTblName
            // 
            this.lblForTblName.AutoSize = true;
            this.lblForTblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForTblName.Location = new System.Drawing.Point(38, 243);
            this.lblForTblName.Name = "lblForTblName";
            this.lblForTblName.Size = new System.Drawing.Size(99, 20);
            this.lblForTblName.TabIndex = 22;
            this.lblForTblName.Text = "Table Name";
            this.lblForTblName.Click += new System.EventHandler(this.lblForTblName_Click);
            // 
            // lblForNoofColumns
            // 
            this.lblForNoofColumns.AutoSize = true;
            this.lblForNoofColumns.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForNoofColumns.Location = new System.Drawing.Point(38, 178);
            this.lblForNoofColumns.Name = "lblForNoofColumns";
            this.lblForNoofColumns.Size = new System.Drawing.Size(100, 20);
            this.lblForNoofColumns.TabIndex = 21;
            this.lblForNoofColumns.Text = "Rows Count";

            // 
            // txtForNoofColumns
            // 
            this.txtForNoofColumns.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForNoofColumns.Location = new System.Drawing.Point(180, 175);
            this.txtForNoofColumns.Name = "txtForNoofColumns";
            this.txtForNoofColumns.Size = new System.Drawing.Size(100, 27);
            this.txtForNoofColumns.TabIndex = 20;
            this.txtForNoofColumns.TextChanged += new System.EventHandler(this.TxtForNoofColumns_TextChanged);

            // 
            // LocationBrowseButton
            // 
            this.LocationBrowseButton.Location = new System.Drawing.Point(691, 112);
            this.LocationBrowseButton.Name = "LocationBrowseButton";
            this.LocationBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.LocationBrowseButton.TabIndex = 15;
            this.LocationBrowseButton.Text = "Browse";
            this.LocationBrowseButton.UseVisualStyleBackColor = true;
            this.LocationBrowseButton.Click += new System.EventHandler(this.LocationBrowsebtn_Click);
            // 
            // textBoxForHoldingFilePath
            // 
            this.textBoxForHoldingFilePath.Location = new System.Drawing.Point(180, 113);
            this.textBoxForHoldingFilePath.Name = "textBoxForHoldingFilePath";
            this.textBoxForHoldingFilePath.Size = new System.Drawing.Size(448, 22);
            this.textBoxForHoldingFilePath.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(38, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Location";
            // 
            // CanclebtnforClear
            // 
            this.CanclebtnforClear.Location = new System.Drawing.Point(663, 390);
            this.CanclebtnforClear.Name = "CanclebtnforClear";
            this.CanclebtnforClear.Size = new System.Drawing.Size(75, 23);
            this.CanclebtnforClear.TabIndex = 26;
            this.CanclebtnforClear.Text = "Cancel";
            this.CanclebtnforClear.UseVisualStyleBackColor = true;
            // 
            // finishButtonInCsvlocationWindow
            // 
            this.finishButtonInCsvlocationWindow.Location = new System.Drawing.Point(450, 390);
            this.finishButtonInCsvlocationWindow.Name = "finishButtonInCsvlocationWindow";
            this.finishButtonInCsvlocationWindow.Size = new System.Drawing.Size(75, 23);
            this.finishButtonInCsvlocationWindow.TabIndex = 25;
            this.finishButtonInCsvlocationWindow.Text = "Finish";
            this.finishButtonInCsvlocationWindow.UseVisualStyleBackColor = true;
            this.finishButtonInCsvlocationWindow.Click += new System.EventHandler(this.FinishButtonInCsvlocationWindow_Click);

            // 
            // btnForBack
            // 
            this.btnForBack.Location = new System.Drawing.Point(558, 390);
            this.btnForBack.Name = "btnForBack";
            this.btnForBack.Size = new System.Drawing.Size(75, 23);
            this.btnForBack.TabIndex = 24;
            this.btnForBack.Text = "Back";
            this.btnForBack.UseVisualStyleBackColor = true;
            // 
            // JsonLocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CanclebtnforClear);
            this.Controls.Add(this.finishButtonInCsvlocationWindow);
            this.Controls.Add(this.btnForBack);
            this.Controls.Add(this.txtForTblName);
            this.Controls.Add(this.lblForTblName);
            this.Controls.Add(this.lblForNoofColumns);
            this.Controls.Add(this.txtForNoofColumns);
            this.Controls.Add(this.LocationBrowseButton);
            this.Controls.Add(this.textBoxForHoldingFilePath);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JsonLocationForm";
            this.Text = "JsonLocationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtForTblName;
        private System.Windows.Forms.Label lblForTblName;
        private System.Windows.Forms.Label lblForNoofColumns;
        private System.Windows.Forms.TextBox txtForNoofColumns;
        private System.Windows.Forms.Button LocationBrowseButton;
        private System.Windows.Forms.TextBox textBoxForHoldingFilePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CanclebtnforClear;
        private System.Windows.Forms.Button finishButtonInCsvlocationWindow;
        private System.Windows.Forms.Button btnForBack;
    }
}