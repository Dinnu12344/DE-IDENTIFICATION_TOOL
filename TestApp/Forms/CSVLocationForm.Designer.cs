using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
namespace DE_IDENTIFICATION_TOOL
{
    partial class CSVLocationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CSVLocationForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxForHoldingFilePath = new System.Windows.Forms.TextBox();
            this.LocationBrowseButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CanclebtnforClear = new System.Windows.Forms.Button();
            this.finishButtonInCsvlocationWindow = new System.Windows.Forms.Button();
            this.delimiterLabel = new System.Windows.Forms.Label();
            this.DelimeterComboBox = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.QuoteLabel = new System.Windows.Forms.Label();
            this.QuoteComboBox = new System.Windows.Forms.ComboBox();
            this.txtForNoofColumns = new System.Windows.Forms.TextBox();
            this.lblForNoofColumns = new System.Windows.Forms.Label();
            this.lblForTblName = new System.Windows.Forms.Label();
            this.txtForTblName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 93);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(309, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please provide the information required below";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "CSV";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 19);
            this.label3.TabIndex = 1;
            this.label3.Text = "Location";
            // 
            // textBoxForHoldingFilePath
            // 
            this.textBoxForHoldingFilePath.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.textBoxForHoldingFilePath.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxForHoldingFilePath.Cursor = System.Windows.Forms.Cursors.No;
            this.textBoxForHoldingFilePath.Location = new System.Drawing.Point(158, 126);
            this.textBoxForHoldingFilePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxForHoldingFilePath.Name = "textBoxForHoldingFilePath";
            this.textBoxForHoldingFilePath.ReadOnly = true;  // Make the text box read-only
            this.textBoxForHoldingFilePath.Size = new System.Drawing.Size(392, 23);
            this.textBoxForHoldingFilePath.TabIndex = 2;
            // 
            // LocationBrowseButton
            // 
            this.LocationBrowseButton.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationBrowseButton.Location = new System.Drawing.Point(606, 126);
            this.LocationBrowseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LocationBrowseButton.Name = "LocationBrowseButton";
            this.LocationBrowseButton.Size = new System.Drawing.Size(66, 25);
            this.LocationBrowseButton.TabIndex = 3;
            this.LocationBrowseButton.Text = "Browse";
            this.LocationBrowseButton.UseVisualStyleBackColor = true;
            this.LocationBrowseButton.Click += new System.EventHandler(this.LocationBrowsebtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CanclebtnforClear);
            this.panel2.Controls.Add(this.finishButtonInCsvlocationWindow);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 349);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 73);
            this.panel2.TabIndex = 4;
            // 
            // CanclebtnforClear
            // 
            this.CanclebtnforClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CanclebtnforClear.Location = new System.Drawing.Point(706, 28);
            this.CanclebtnforClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CanclebtnforClear.Name = "CanclebtnforClear";
            this.CanclebtnforClear.Size = new System.Drawing.Size(70, 30);
            this.CanclebtnforClear.TabIndex = 3;
            this.CanclebtnforClear.Text = "Cancel";
            this.CanclebtnforClear.UseVisualStyleBackColor = true;
            this.CanclebtnforClear.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // finishButtonInCsvlocationWindow
            // 
            this.finishButtonInCsvlocationWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.finishButtonInCsvlocationWindow.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finishButtonInCsvlocationWindow.Location = new System.Drawing.Point(602, 28);
            this.finishButtonInCsvlocationWindow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.finishButtonInCsvlocationWindow.Name = "finishButtonInCsvlocationWindow";
            this.finishButtonInCsvlocationWindow.Size = new System.Drawing.Size(70, 30);
            this.finishButtonInCsvlocationWindow.TabIndex = 2;
            this.finishButtonInCsvlocationWindow.Text = "Finish";
            this.finishButtonInCsvlocationWindow.UseVisualStyleBackColor = true;
            this.finishButtonInCsvlocationWindow.Click += new System.EventHandler(this.FinishButtonInCsvlocationWindow_Click);
            // 
            // delimiterLabel
            // 
            this.delimiterLabel.AutoSize = true;
            this.delimiterLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delimiterLabel.Location = new System.Drawing.Point(35, 178);
            this.delimiterLabel.Name = "delimiterLabel";
            this.delimiterLabel.Size = new System.Drawing.Size(69, 19);
            this.delimiterLabel.TabIndex = 5;
            this.delimiterLabel.Text = "Delimiter";
            // 
            // DelimeterComboBox
            // 
            this.DelimeterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DelimeterComboBox.FormattingEnabled = true;
            this.DelimeterComboBox.Items.AddRange(new object[] {
    ",",
    ";",
    "|",
    "\\t"});
            this.DelimeterComboBox.Location = new System.Drawing.Point(158, 178);
            this.DelimeterComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DelimeterComboBox.Name = "DelimeterComboBox";
            this.DelimeterComboBox.Size = new System.Drawing.Size(392, 23);
            this.DelimeterComboBox.TabIndex = 6;
            this.DelimeterComboBox.SelectedIndexChanged += new System.EventHandler(this.DelimeterComboBox_SelectedIndexChanged);
            // 
            // QuoteLabel
            // 
            this.QuoteLabel.AutoSize = true;
            this.QuoteLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuoteLabel.Location = new System.Drawing.Point(35, 223);
            this.QuoteLabel.Name = "QuoteLabel";
            this.QuoteLabel.Size = new System.Drawing.Size(49, 19);
            this.QuoteLabel.TabIndex = 7;
            this.QuoteLabel.Text = "Quote";
            // 
            // QuoteComboBox
            // 
            this.QuoteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QuoteComboBox.FormattingEnabled = true;
            this.QuoteComboBox.Items.AddRange(new object[] {
    "\"",
    "\'"});
            this.QuoteComboBox.Location = new System.Drawing.Point(158, 219);
            this.QuoteComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.QuoteComboBox.Name = "QuoteComboBox";
            this.QuoteComboBox.Size = new System.Drawing.Size(392, 23);
            this.QuoteComboBox.TabIndex = 8;
            this.QuoteComboBox.SelectedIndexChanged += new System.EventHandler(this.QuoteComboBox_SelectedIndexChanged);
            // 
            // txtForNoofColumns
            // 
            this.txtForNoofColumns.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForNoofColumns.Location = new System.Drawing.Point(158, 267);
            this.txtForNoofColumns.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtForNoofColumns.Name = "txtForNoofColumns";
            this.txtForNoofColumns.Size = new System.Drawing.Size(88, 23);
            this.txtForNoofColumns.TabIndex = 9;
            this.txtForNoofColumns.TextChanged += new System.EventHandler(this.TxtForNoofColumns_TextChanged);
            // 
            // lblForNoofColumns
            // 
            this.lblForNoofColumns.AutoSize = true;
            this.lblForNoofColumns.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForNoofColumns.Location = new System.Drawing.Point(35, 272);
            this.lblForNoofColumns.Name = "lblForNoofColumns";
            this.lblForNoofColumns.Size = new System.Drawing.Size(86, 19);
            this.lblForNoofColumns.TabIndex = 10;
            this.lblForNoofColumns.Text = "Rows Count";
            // 
            // lblForTblName
            // 
            this.lblForTblName.AutoSize = true;
            this.lblForTblName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForTblName.Location = new System.Drawing.Point(32, 313);
            this.lblForTblName.Name = "lblForTblName";
            this.lblForTblName.Size = new System.Drawing.Size(86, 19);
            this.lblForTblName.TabIndex = 11;
            this.lblForTblName.Text = "Table Name";
            // 
            // txtForTblName
            // 
            this.txtForTblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForTblName.Location = new System.Drawing.Point(158, 312);
            this.txtForTblName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtForTblName.Name = "txtForTblName";
            this.txtForTblName.Size = new System.Drawing.Size(392, 23);
            this.txtForTblName.TabIndex = 12;
            this.txtForTblName.TextChanged += new System.EventHandler(this.TxtForTblName_TextChanged);
            // 
            // CSVLocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 422);
            this.Controls.Add(this.txtForTblName);
            this.Controls.Add(this.lblForTblName);
            this.Controls.Add(this.lblForNoofColumns);
            this.Controls.Add(this.txtForNoofColumns);
            this.Controls.Add(this.QuoteComboBox);
            this.Controls.Add(this.QuoteLabel);
            this.Controls.Add(this.DelimeterComboBox);
            this.Controls.Add(this.delimiterLabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.LocationBrowseButton);
            this.Controls.Add(this.textBoxForHoldingFilePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CSVLocationForm";
            this.Text = "CsvLocationForm";
            this.Load += new System.EventHandler(this.CSVLocationForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private Panel panel1;
        private Label label2;
        private Label label1;
        private Label label3;
        private TextBox textBoxForHoldingFilePath;
        private Button LocationBrowseButton;
        private Panel panel2;
        private Button CanclebtnforClear;
        private Button finishButtonInCsvlocationWindow;
        private Label delimiterLabel;
        private ComboBox DelimeterComboBox;
        private ColorDialog colorDialog1;
        private Label QuoteLabel;
        private ComboBox QuoteComboBox;
        private TextBox txtForNoofColumns;
        private Label lblForNoofColumns;
        private Label lblForTblName;
        private TextBox txtForTblName;
    }

}