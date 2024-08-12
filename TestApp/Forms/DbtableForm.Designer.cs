using System.Windows.Forms;
namespace DE_IDENTIFICATION_TOOL.Forms
{
    partial class DbtableForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbtableForm));
            this.cmbDatabases = new System.Windows.Forms.ComboBox();
            this.cmbTables = new System.Windows.Forms.ComboBox();
            this.labelForDatabase = new System.Windows.Forms.Label();
            this.labelForDatabaseTbl = new System.Windows.Forms.Label();
            this.checkBoxforPullreleateddata = new System.Windows.Forms.CheckBox();
            this.btnForFinish = new System.Windows.Forms.Button();
            this.btnForCancel = new System.Windows.Forms.Button();
            this.btnForAddingColumns = new System.Windows.Forms.Button();
            this.btnForDeleteColumns = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.txtboxForTblName = new System.Windows.Forms.TextBox();
            this.txtBoxFoeExistingTblKey = new System.Windows.Forms.TextBox();
            this.textBoxForSourceTbl = new System.Windows.Forms.TextBox();
            this.textBoxForSourceTblKey = new System.Windows.Forms.TextBox();
            this.panelForPullreleatedData = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblForNoofColumns = new System.Windows.Forms.Label();
            this.txtForNoofColumns = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnForSavePullreleatedData = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabases.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDatabases.FormattingEnabled = true;
            this.cmbDatabases.Location = new System.Drawing.Point(219, 100);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(569, 27);
            this.cmbDatabases.TabIndex = 0;
            this.cmbDatabases.SelectedIndexChanged += new System.EventHandler(this.cmbDatabases_SelectedIndexChanged);
            // 
            // cmbTables
            // 
            this.cmbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTables.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTables.FormattingEnabled = true;
            this.cmbTables.Location = new System.Drawing.Point(219, 143);
            this.cmbTables.Name = "cmbTables";
            this.cmbTables.Size = new System.Drawing.Size(569, 27);
            this.cmbTables.TabIndex = 1;
            this.cmbTables.SelectedIndexChanged += new System.EventHandler(this.cmbTables_SelectedIndexChanged);
            // 
            // labelForDatabase
            // 
            this.labelForDatabase.AutoSize = true;
            this.labelForDatabase.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelForDatabase.Location = new System.Drawing.Point(26, 103);
            this.labelForDatabase.Name = "labelForDatabase";
            this.labelForDatabase.Size = new System.Drawing.Size(78, 19);
            this.labelForDatabase.TabIndex = 2;
            this.labelForDatabase.Text = "Databases";
            // 
            // labelForDatabaseTbl
            // 
            this.labelForDatabaseTbl.AutoSize = true;
            this.labelForDatabaseTbl.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelForDatabaseTbl.Location = new System.Drawing.Point(26, 146);
            this.labelForDatabaseTbl.Name = "labelForDatabaseTbl";
            this.labelForDatabaseTbl.Size = new System.Drawing.Size(51, 19);
            this.labelForDatabaseTbl.TabIndex = 3;
            this.labelForDatabaseTbl.Text = "Tables";
            // 
            // checkBoxforPullreleateddata
            // 
            this.checkBoxforPullreleateddata.AutoSize = true;
            this.checkBoxforPullreleateddata.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxforPullreleateddata.Location = new System.Drawing.Point(30, 242);
            this.checkBoxforPullreleateddata.Name = "checkBoxforPullreleateddata";
            this.checkBoxforPullreleateddata.Size = new System.Drawing.Size(143, 23);
            this.checkBoxforPullreleateddata.TabIndex = 4;
            this.checkBoxforPullreleateddata.Text = "Pull releated data";
            this.checkBoxforPullreleateddata.UseVisualStyleBackColor = true;
            this.checkBoxforPullreleateddata.CheckedChanged += new System.EventHandler(this.checkBoxforPullreleateddata_CheckedChanged_1);
            // 
            // btnForFinish
            // 
            this.btnForFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnForFinish.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForFinish.Location = new System.Drawing.Point(627, 558);
            this.btnForFinish.Name = "btnForFinish";
            this.btnForFinish.Size = new System.Drawing.Size(70, 30);
            this.btnForFinish.TabIndex = 6;
            this.btnForFinish.Text = "Finish";
            this.btnForFinish.UseVisualStyleBackColor = true;
            this.btnForFinish.Click += new System.EventHandler(this.btnForFinish_Click);
            // 
            // btnForCancel
            // 
            this.btnForCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnForCancel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForCancel.Location = new System.Drawing.Point(729, 558);
            this.btnForCancel.Name = "btnForCancel";
            this.btnForCancel.Size = new System.Drawing.Size(70, 30);
            this.btnForCancel.TabIndex = 7;
            this.btnForCancel.Text = "Cancel";
            this.btnForCancel.UseVisualStyleBackColor = true;
            this.btnForCancel.Click += new System.EventHandler(this.btnForCancel_Click);
            // 
            // btnForAddingColumns
            // 
            this.btnForAddingColumns.Location = new System.Drawing.Point(0, 0);
            this.btnForAddingColumns.Name = "btnForAddingColumns";
            this.btnForAddingColumns.Size = new System.Drawing.Size(75, 23);
            this.btnForAddingColumns.TabIndex = 14;
            // 
            // btnForDeleteColumns
            // 
            this.btnForDeleteColumns.Location = new System.Drawing.Point(0, 0);
            this.btnForDeleteColumns.Name = "btnForDeleteColumns";
            this.btnForDeleteColumns.Size = new System.Drawing.Size(75, 23);
            this.btnForDeleteColumns.TabIndex = 13;
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 24);
            this.checkBox1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 8;
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(0, 0);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(104, 24);
            this.checkBox2.TabIndex = 7;
            // 
            // txtboxForTblName
            // 
            this.txtboxForTblName.Location = new System.Drawing.Point(0, 0);
            this.txtboxForTblName.Name = "txtboxForTblName";
            this.txtboxForTblName.Size = new System.Drawing.Size(100, 23);
            this.txtboxForTblName.TabIndex = 6;
            // 
            // txtBoxFoeExistingTblKey
            // 
            this.txtBoxFoeExistingTblKey.Location = new System.Drawing.Point(0, 0);
            this.txtBoxFoeExistingTblKey.Name = "txtBoxFoeExistingTblKey";
            this.txtBoxFoeExistingTblKey.Size = new System.Drawing.Size(100, 23);
            this.txtBoxFoeExistingTblKey.TabIndex = 5;
            // 
            // textBoxForSourceTbl
            // 
            this.textBoxForSourceTbl.Location = new System.Drawing.Point(0, 0);
            this.textBoxForSourceTbl.Name = "textBoxForSourceTbl";
            this.textBoxForSourceTbl.Size = new System.Drawing.Size(100, 23);
            this.textBoxForSourceTbl.TabIndex = 4;
            // 
            // textBoxForSourceTblKey
            // 
            this.textBoxForSourceTblKey.Location = new System.Drawing.Point(0, 0);
            this.textBoxForSourceTblKey.Name = "textBoxForSourceTblKey";
            this.textBoxForSourceTblKey.Size = new System.Drawing.Size(100, 23);
            this.textBoxForSourceTblKey.TabIndex = 3;
            // 
            // panelForPullreleatedData
            // 
            this.panelForPullreleatedData.AutoScroll = true;
            this.panelForPullreleatedData.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelForPullreleatedData.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelForPullreleatedData.Location = new System.Drawing.Point(30, 271);
            this.panelForPullreleatedData.Name = "panelForPullreleatedData";
            this.panelForPullreleatedData.Size = new System.Drawing.Size(769, 274);
            this.panelForPullreleatedData.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 0;
            // 
            // lblForNoofColumns
            // 
            this.lblForNoofColumns.Location = new System.Drawing.Point(0, 0);
            this.lblForNoofColumns.Name = "lblForNoofColumns";
            this.lblForNoofColumns.Size = new System.Drawing.Size(100, 23);
            this.lblForNoofColumns.TabIndex = 1;
            // 
            // txtForNoofColumns
            // 
            this.txtForNoofColumns.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForNoofColumns.Location = new System.Drawing.Point(219, 190);
            this.txtForNoofColumns.Name = "txtForNoofColumns";
            this.txtForNoofColumns.Size = new System.Drawing.Size(121, 27);
            this.txtForNoofColumns.TabIndex = 0;
            this.txtForNoofColumns.TextChanged += new System.EventHandler(this.txtForNoofColumns_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(26, 193);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 19);
            this.label7.TabIndex = 15;
            this.label7.Text = "Row Count";
            // 
            // btnForSavePullreleatedData
            // 
            this.btnForSavePullreleatedData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnForSavePullreleatedData.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForSavePullreleatedData.Location = new System.Drawing.Point(529, 558);
            this.btnForSavePullreleatedData.Name = "btnForSavePullreleatedData";
            this.btnForSavePullreleatedData.Size = new System.Drawing.Size(70, 30);
            this.btnForSavePullreleatedData.TabIndex = 17;
            this.btnForSavePullreleatedData.Text = "Save";
            this.btnForSavePullreleatedData.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(953, 82);
            this.panel1.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(25, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(160, 26);
            this.label8.TabIndex = 0;
            this.label8.Text = "Import Sql Tables";
            // 
            // DbtableForm
            // 
            this.ClientSize = new System.Drawing.Size(953, 600);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnForSavePullreleatedData);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtForNoofColumns);
            this.Controls.Add(this.lblForNoofColumns);
            this.Controls.Add(this.panelForPullreleatedData);
            this.Controls.Add(this.textBoxForSourceTblKey);
            this.Controls.Add(this.textBoxForSourceTbl);
            this.Controls.Add(this.txtBoxFoeExistingTblKey);
            this.Controls.Add(this.txtboxForTblName);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnForDeleteColumns);
            this.Controls.Add(this.btnForAddingColumns);
            this.Controls.Add(this.btnForCancel);
            this.Controls.Add(this.btnForFinish);
            this.Controls.Add(this.checkBoxforPullreleateddata);
            this.Controls.Add(this.labelForDatabaseTbl);
            this.Controls.Add(this.labelForDatabase);
            this.Controls.Add(this.cmbTables);
            this.Controls.Add(this.cmbDatabases);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DbtableForm";
            this.Text = "Database and Tables";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private ComboBox cmbDatabases;
        private ComboBox cmbTables;
        private Label labelForDatabase;
        private Label labelForDatabaseTbl;
        private CheckBox checkBoxforPullreleateddata;
        private Button btnForFinish;
        private Button btnForCancel;
        private Button btnForAddingColumns;
        private Button btnForDeleteColumns;
        private CheckBox checkBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private CheckBox checkBox2;
        private TextBox txtboxForTblName;
        private TextBox txtBoxFoeExistingTblKey;
        private TextBox textBoxForSourceTbl;
        private TextBox textBoxForSourceTblKey;
        private Panel panelForPullreleatedData;
        private Label label6;
        private Label label5;
        private Label lblForNoofColumns;
        private TextBox txtForNoofColumns;
        private Label label7;
        private Button btnForSavePullreleatedData;
        private Panel panel1;
        private Label label8;
    }
}