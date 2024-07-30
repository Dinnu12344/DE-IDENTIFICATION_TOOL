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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(CSVLocationForm));
            this.panel1 = new Panel();
            this.label2 = new Label();
            this.label1 = new Label();
            this.label3 = new Label();
            this.textBoxForHoldingFilePath = new TextBox();
            this.LocationBrowseButton = new Button();
            this.panel2 = new Panel();
            this.CanclebtnforClear = new Button();
            this.finishButtonInCsvlocationWindow = new Button();
            this.btnForBack = new Button();
            this.delimiterLabel = new Label();
            this.DelimeterComboBox = new ComboBox();
            this.colorDialog1 = new ColorDialog();
            this.QuoteLabel = new Label();
            this.QuoteComboBox = new ComboBox();
            this.txtForNoofColumns = new TextBox();
            this.lblForNoofColumns = new Label();
            this.lblForTblName = new Label();
            this.txtForTblName = new TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new Point(1, 0);
            this.panel1.Margin = new Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Dock = DockStyle.Top;
            //this.panel1.Size = new System.Drawing.Size(700, 93);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new Point(34, 62);
            this.label2.Name = "label2";
            this.label2.Size = new Size(309, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please provide the information required below";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new Point(31, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(45, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "CSV";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new Point(35, 127);
            this.label3.Name = "label3";
            this.label3.Size = new Size(64, 19);
            this.label3.TabIndex = 1;
            this.label3.Text = "Location";
            // 
            // textBoxForHoldingFilePath
            // 
            this.textBoxForHoldingFilePath.Location = new Point(158, 126);
            this.textBoxForHoldingFilePath.Margin = new Padding(3, 2, 3, 2);
            this.textBoxForHoldingFilePath.Name = "textBoxForHoldingFilePath";
            this.textBoxForHoldingFilePath.Size = new Size(392, 23);
            this.textBoxForHoldingFilePath.TabIndex = 2;
            // 
            // LocationBrowseButton
            // 
            this.LocationBrowseButton.Location = new Point(606, 125);
            this.LocationBrowseButton.Margin = new Padding(3, 2, 3, 2);
            this.LocationBrowseButton.Name = "LocationBrowseButton";
            this.LocationBrowseButton.Size = new Size(66, 22);
            this.LocationBrowseButton.TabIndex = 3;
            this.LocationBrowseButton.Text = "Browse";
            this.LocationBrowseButton.UseVisualStyleBackColor = true;
            this.LocationBrowseButton.Click += new System.EventHandler(this.LocationBrowsebtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CanclebtnforClear);
            this.panel2.Controls.Add(this.finishButtonInCsvlocationWindow);
            this.panel2.Controls.Add(this.btnForBack);
            this.panel2.Location = new Point(1, 350);
            this.panel2.Margin = new Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(700, 73);
            this.panel2.TabIndex = 4;
            // 
            // CanclebtnforClear
            // 
            this.CanclebtnforClear.Location = new Point(606, 28);
            this.CanclebtnforClear.Margin = new Padding(3, 2, 3, 2);
            this.CanclebtnforClear.Name = "CanclebtnforClear";
            this.CanclebtnforClear.Size = new Size(66, 22);
            this.CanclebtnforClear.TabIndex = 3;
            this.CanclebtnforClear.Text = "Cancel";
            this.CanclebtnforClear.UseVisualStyleBackColor = true;
            this.CanclebtnforClear.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // finishButtonInCsvlocationWindow
            // 
            this.finishButtonInCsvlocationWindow.Font = new Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finishButtonInCsvlocationWindow.Location = new Point(419, 28);
            this.finishButtonInCsvlocationWindow.Margin = new Padding(3, 2, 3, 2);
            this.finishButtonInCsvlocationWindow.Name = "finishButtonInCsvlocationWindow";
            this.finishButtonInCsvlocationWindow.Size = new Size(66, 22);
            this.finishButtonInCsvlocationWindow.TabIndex = 2;
            this.finishButtonInCsvlocationWindow.Text = "Finish";
            this.finishButtonInCsvlocationWindow.UseVisualStyleBackColor = true;
            this.finishButtonInCsvlocationWindow.Click += new System.EventHandler(this.FinishButtonInCsvlocationWindow_Click);
            // 
            // btnForBack
            // 
            this.btnForBack.Location = new Point(514, 28);
            this.btnForBack.Margin = new Padding(3, 2, 3, 2);
            this.btnForBack.Name = "btnForBack";
            this.btnForBack.Size = new Size(66, 22);
            this.btnForBack.TabIndex = 0;
            this.btnForBack.Text = "Back";
            this.btnForBack.UseVisualStyleBackColor = true;
            this.btnForBack.Click += new System.EventHandler(this.BtnForBack_Click);
            // 
            // delimiterLabel
            // 
            this.delimiterLabel.AutoSize = true;
            this.delimiterLabel.Font = new Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delimiterLabel.Location = new Point(35, 178);
            this.delimiterLabel.Name = "delimiterLabel";
            this.delimiterLabel.Size = new Size(73, 19);
            this.delimiterLabel.TabIndex = 5;
            this.delimiterLabel.Text = "Delimeter";
            // 
            // DelimeterComboBox
            // 
            this.DelimeterComboBox.FormattingEnabled = true;
            this.DelimeterComboBox.Items.AddRange(new object[] {
            ";",
            "|",
            "tab",
            ","});
            this.DelimeterComboBox.Location = new Point(158, 173);
            this.DelimeterComboBox.Margin = new Padding(3, 2, 3, 2);
            this.DelimeterComboBox.Name = "DelimeterComboBox";
            this.DelimeterComboBox.Size = new Size(392, 23);
            this.DelimeterComboBox.TabIndex = 6;
            this.DelimeterComboBox.SelectedIndexChanged += new System.EventHandler(this.DelimeterComboBox_SelectedIndexChanged);
            // 
            // QuoteLabel
            // 
            this.QuoteLabel.AutoSize = true;
            this.QuoteLabel.Font = new Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuoteLabel.Location = new Point(35, 223);
            this.QuoteLabel.Name = "QuoteLabel";
            this.QuoteLabel.Size = new Size(49, 19);
            this.QuoteLabel.TabIndex = 7;
            this.QuoteLabel.Text = "Quote";
            // 
            // QuoteComboBox
            // 
            this.QuoteComboBox.FormattingEnabled = true;
            this.QuoteComboBox.Items.AddRange(new object[] {
            "\"",
            "\'"});
            this.QuoteComboBox.Location = new Point(158, 219);
            this.QuoteComboBox.Margin = new Padding(3, 2, 3, 2);
            this.QuoteComboBox.Name = "QuoteComboBox";
            this.QuoteComboBox.Size = new Size(392, 23);
            this.QuoteComboBox.TabIndex = 8;
            this.QuoteComboBox.SelectedIndexChanged += new System.EventHandler(this.QuoteComboBox_SelectedIndexChanged);
            // 
            // txtForNoofColumns
            // 
            this.txtForNoofColumns.Font = new Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForNoofColumns.Location = new Point(158, 267);
            this.txtForNoofColumns.Margin = new Padding(3, 2, 3, 2);
            this.txtForNoofColumns.Name = "txtForNoofColumns";
            this.txtForNoofColumns.Size = new Size(88, 23);
            this.txtForNoofColumns.TabIndex = 9;
            this.txtForNoofColumns.TextChanged += new System.EventHandler(this.TxtForNoofColumns_TextChanged);
            // 
            // lblForNoofColumns
            // 
            this.lblForNoofColumns.AutoSize = true;
            this.lblForNoofColumns.Font = new Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForNoofColumns.Location = new Point(35, 272);
            this.lblForNoofColumns.Name = "lblForNoofColumns";
            this.lblForNoofColumns.Size = new Size(86, 19);
            this.lblForNoofColumns.TabIndex = 10;
            this.lblForNoofColumns.Text = "Rows Count";
            // 
            // lblForTblName
            // 
            this.lblForTblName.AutoSize = true;
            this.lblForTblName.Font = new Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForTblName.Location = new Point(32, 313);
            this.lblForTblName.Name = "lblForTblName";
            this.lblForTblName.Size = new Size(86, 19);
            this.lblForTblName.TabIndex = 11;
            this.lblForTblName.Text = "Table Name";
            // 
            // txtForTblName
            // 
            this.txtForTblName.Font = new Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForTblName.Location = new Point(158, 312);
            this.txtForTblName.Margin = new Padding(3, 2, 3, 2);
            this.txtForTblName.Name = "txtForTblName";
            this.txtForTblName.Size = new Size(246, 23);
            this.txtForTblName.TabIndex = 12;
            this.txtForTblName.TextChanged += new System.EventHandler(this.TxtForTblName_TextChanged);
            // 
            // CSVLocationForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(700, 422);
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
            this.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new Padding(3, 2, 3, 2);
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
        private Button btnForBack;
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