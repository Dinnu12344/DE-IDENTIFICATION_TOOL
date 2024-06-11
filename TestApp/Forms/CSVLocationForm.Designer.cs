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
            this.SuspendLayout();
            // 
            // CSVLocationForm
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CSVLocationForm";
            this.Text = "CSVLocation";
            this.ResumeLayout(false);

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
            this.panel1.BackColor = SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(800, 100);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Microsoft Sans Serif", 10.2F,FontStyle.Regular,GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new Point(39, 67);
            this.label2.Name = "label2";
            this.label2.Size = new Size(349, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please provide the information required below";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new Point(34, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(61, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "CSV";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new Point(40, 136);      
            this.label3.Name = "label3";
            this.label3.Size = new Size(73, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Location";
            // 
            // textBoxForHoldingFilePath
            // 
            this.textBoxForHoldingFilePath.Location = new Point(182, 134);
            this.textBoxForHoldingFilePath.Name = "textBoxForHoldingFilePath";
            this.textBoxForHoldingFilePath.Size = new Size(448, 22);
            this.textBoxForHoldingFilePath.TabIndex = 2;
            // 
            // LocationBrowseButton
            // 
            this.LocationBrowseButton.Location = new Point(693, 133);
            this.LocationBrowseButton.Name = "LocationBrowseButton";
            this.LocationBrowseButton.Size = new Size(75, 23);
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
            this.panel2.Location = new Point(1, 373);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(800, 78);
            this.panel2.TabIndex = 4;
            // 
            // CanclebtnforClear
            // 
            this.CanclebtnforClear.Location = new Point(692, 30);
            this.CanclebtnforClear.Name = "CanclebtnforClear";
            this.CanclebtnforClear.Size = new Size(75, 23);
            this.CanclebtnforClear.TabIndex = 3;
            this.CanclebtnforClear.Text = "Cancel";
            this.CanclebtnforClear.UseVisualStyleBackColor = true;
            this.CanclebtnforClear.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // finishButtonInCsvlocationWindow
            // 
            this.finishButtonInCsvlocationWindow.Location = new Point(479, 30);
            this.finishButtonInCsvlocationWindow.Name = "finishButtonInCsvlocationWindow";
            this.finishButtonInCsvlocationWindow.Size = new Size(75, 23);
            this.finishButtonInCsvlocationWindow.TabIndex = 2;
            this.finishButtonInCsvlocationWindow.Text = "Finish";
            this.finishButtonInCsvlocationWindow.UseVisualStyleBackColor = true;
            this.finishButtonInCsvlocationWindow.Click += new System.EventHandler(this.FinishButtonInCsvlocationWindow_Click);
            // 
            // btnForBack
            // 
            this.btnForBack.Location = new Point(587, 30);
            this.btnForBack.Name = "btnForBack";
            this.btnForBack.Size = new Size(75, 23);
            this.btnForBack.TabIndex = 0;
            this.btnForBack.Text = "Back";
            this.btnForBack.UseVisualStyleBackColor = true;
            this.btnForBack.Click += new System.EventHandler(this.BtnForBack_Click);
            // 
            // delimiterLabel
            // 
            this.delimiterLabel.AutoSize = true;
            this.delimiterLabel.Font = new Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delimiterLabel.Location = new Point(40, 189);
            this.delimiterLabel.Name = "delimiterLabel";
            this.delimiterLabel.Size = new Size(82, 20);
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
            this.DelimeterComboBox.Location = new Point(182, 185);
            this.DelimeterComboBox.Name = "DelimeterComboBox";
            this.DelimeterComboBox.Size = new Size(448, 24);
            this.DelimeterComboBox.TabIndex = 6;
            this.DelimeterComboBox.SelectedIndexChanged += new System.EventHandler(this.DelimeterComboBox_SelectedIndexChanged);
            // 
            // QuoteLabel
            // 
            this.QuoteLabel.AutoSize = true;
            this.QuoteLabel.Font = new Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuoteLabel.Location = new Point(40, 238);
            this.QuoteLabel.Name = "QuoteLabel";
            this.QuoteLabel.Size = new Size(54, 20);
            this.QuoteLabel.TabIndex = 7;
            this.QuoteLabel.Text = "Quote";
            // 
            // QuoteComboBox
            // 
            this.QuoteComboBox.FormattingEnabled = true;
            this.QuoteComboBox.Items.AddRange(new object[] {
            "\"",
            "\'"});
            this.QuoteComboBox.Location = new Point(182, 234);
            this.QuoteComboBox.Name = "QuoteComboBox";
            this.QuoteComboBox.Size = new Size(448, 24);
            this.QuoteComboBox.TabIndex = 8;
            this.QuoteComboBox.SelectedIndexChanged += new System.EventHandler(this.QuoteComboBox_SelectedIndexChanged);
            // 
            // txtForNoofColumns
            // 
            this.txtForNoofColumns.Font = new Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForNoofColumns.Location = new Point(182, 284);
            this.txtForNoofColumns.Name = "txtForNoofColumns";
            this.txtForNoofColumns.Size = new Size(100, 27);
            this.txtForNoofColumns.TabIndex = 9;
            this.txtForNoofColumns.TextChanged += new System.EventHandler(this.TxtForNoofColumns_TextChanged);
            // 
            // lblForNoofColumns
            // 
            this.lblForNoofColumns.AutoSize = true;
            this.lblForNoofColumns.Font = new Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForNoofColumns.Location = new Point(40, 291);
            this.lblForNoofColumns.Name = "lblForNoofColumns";
            this.lblForNoofColumns.Size = new Size(100, 20);
            this.lblForNoofColumns.TabIndex = 10;
            this.lblForNoofColumns.Text = "Rows Count";
            // 
            // lblForTblName
            // 
            this.lblForTblName.AutoSize = true;
            this.lblForTblName.Font = new Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForTblName.Location = new Point(36, 334);
            this.lblForTblName.Name = "lblForTblName";
            this.lblForTblName.Size = new Size(99, 20);
            this.lblForTblName.TabIndex = 11;
            this.lblForTblName.Text = "Table Name";
            // 
            // txtForTblName
            // 
            this.txtForTblName.Font = new Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForTblName.Location = new Point(182, 332);
            this.txtForTblName.Name = "txtForTblName";
            this.txtForTblName.Size = new Size(282, 27);
            this.txtForTblName.TabIndex = 12;
            this.txtForTblName.TextChanged += new System.EventHandler(this.TxtForTblName_TextChanged);
            // 
            // CsvLocationForm
            // 
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
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
            this.Name = "CsvLocationForm";
            this.Text = "CsvLocationForm";
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