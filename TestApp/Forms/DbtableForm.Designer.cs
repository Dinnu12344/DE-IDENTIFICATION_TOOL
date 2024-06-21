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
            this.btnForBack = new System.Windows.Forms.Button();
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
            this.SuspendLayout();
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabases.FormattingEnabled = true;
            this.cmbDatabases.Location = new System.Drawing.Point(237, 65);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(475, 24);
            this.cmbDatabases.TabIndex = 0;
            this.cmbDatabases.SelectedIndexChanged += new System.EventHandler(this.cmbDatabases_SelectedIndexChanged);
            // 
            // cmbTables
            // 
            this.cmbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTables.FormattingEnabled = true;
            this.cmbTables.Location = new System.Drawing.Point(237, 120);
            this.cmbTables.Name = "cmbTables";
            this.cmbTables.Size = new System.Drawing.Size(475, 24);
            this.cmbTables.TabIndex = 1;
            this.cmbTables.SelectedIndexChanged += new System.EventHandler(this.cmbTables_SelectedIndexChanged);
            // 
            // labelForDatabase
            // 
            this.labelForDatabase.AutoSize = true;
            this.labelForDatabase.Location = new System.Drawing.Point(73, 73);
            this.labelForDatabase.Name = "labelForDatabase";
            this.labelForDatabase.Size = new System.Drawing.Size(74, 16);
            this.labelForDatabase.TabIndex = 2;
            this.labelForDatabase.Text = "Databases";
            // 
            // labelForDatabaseTbl
            // 
            this.labelForDatabaseTbl.AutoSize = true;
            this.labelForDatabaseTbl.Location = new System.Drawing.Point(73, 120);
            this.labelForDatabaseTbl.Name = "labelForDatabaseTbl";
            this.labelForDatabaseTbl.Size = new System.Drawing.Size(50, 16);
            this.labelForDatabaseTbl.TabIndex = 3;
            this.labelForDatabaseTbl.Text = "Tables";
            // 
            // checkBoxforPullreleateddata
            // 
            this.checkBoxforPullreleateddata.AutoSize = true;
            this.checkBoxforPullreleateddata.Location = new System.Drawing.Point(76, 199);
            this.checkBoxforPullreleateddata.Name = "checkBoxforPullreleateddata";
            this.checkBoxforPullreleateddata.Size = new System.Drawing.Size(134, 20);
            this.checkBoxforPullreleateddata.TabIndex = 4;
            this.checkBoxforPullreleateddata.Text = "Pull releated data";
            this.checkBoxforPullreleateddata.UseVisualStyleBackColor = true;
            this.checkBoxforPullreleateddata.CheckedChanged += new System.EventHandler(this.checkBoxforPullreleateddata_CheckedChanged_1);
            // 
            // btnForBack
            // 
            this.btnForBack.Location = new System.Drawing.Point(559, 536);
            this.btnForBack.Name = "btnForBack";
            this.btnForBack.Size = new System.Drawing.Size(75, 23);
            this.btnForBack.TabIndex = 5;
            this.btnForBack.Text = "Back";
            this.btnForBack.UseVisualStyleBackColor = true;
            // 
            // btnForFinish
            // 
            this.btnForFinish.Location = new System.Drawing.Point(663, 536);
            this.btnForFinish.Name = "btnForFinish";
            this.btnForFinish.Size = new System.Drawing.Size(75, 23);
            this.btnForFinish.TabIndex = 6;
            this.btnForFinish.Text = "Finish";
            this.btnForFinish.UseVisualStyleBackColor = true;
            this.btnForFinish.Click += new System.EventHandler(this.btnForFinish_Click);
            // 
            // btnForCancel
            // 
            this.btnForCancel.Location = new System.Drawing.Point(775, 536);
            this.btnForCancel.Name = "btnForCancel";
            this.btnForCancel.Size = new System.Drawing.Size(75, 23);
            this.btnForCancel.TabIndex = 7;
            this.btnForCancel.Text = "Cancel";
            this.btnForCancel.UseVisualStyleBackColor = true;
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
            this.txtboxForTblName.Size = new System.Drawing.Size(100, 22);
            this.txtboxForTblName.TabIndex = 6;
            // 
            // txtBoxFoeExistingTblKey
            // 
            this.txtBoxFoeExistingTblKey.Location = new System.Drawing.Point(0, 0);
            this.txtBoxFoeExistingTblKey.Name = "txtBoxFoeExistingTblKey";
            this.txtBoxFoeExistingTblKey.Size = new System.Drawing.Size(100, 22);
            this.txtBoxFoeExistingTblKey.TabIndex = 5;
            // 
            // textBoxForSourceTbl
            // 
            this.textBoxForSourceTbl.Location = new System.Drawing.Point(0, 0);
            this.textBoxForSourceTbl.Name = "textBoxForSourceTbl";
            this.textBoxForSourceTbl.Size = new System.Drawing.Size(100, 22);
            this.textBoxForSourceTbl.TabIndex = 4;
            // 
            // textBoxForSourceTblKey
            // 
            this.textBoxForSourceTblKey.Location = new System.Drawing.Point(0, 0);
            this.textBoxForSourceTblKey.Name = "textBoxForSourceTblKey";
            this.textBoxForSourceTblKey.Size = new System.Drawing.Size(100, 22);
            this.textBoxForSourceTblKey.TabIndex = 3;
            // 
            // panelForPullreleatedData
            // 
            this.panelForPullreleatedData.AutoScroll = true;
            this.panelForPullreleatedData.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelForPullreleatedData.Location = new System.Drawing.Point(72, 225);
            this.panelForPullreleatedData.Name = "panelForPullreleatedData";
            this.panelForPullreleatedData.Size = new System.Drawing.Size(820, 282);
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
            this.txtForNoofColumns.Location = new System.Drawing.Point(237, 165);
            this.txtForNoofColumns.Name = "txtForNoofColumns";
            this.txtForNoofColumns.Size = new System.Drawing.Size(100, 22);
            this.txtForNoofColumns.TabIndex = 0;
            this.txtForNoofColumns.TextChanged += new System.EventHandler(this.txtForNoofColumns_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(76, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Row Count";
            // 
            // btnForSavePullreleatedData
            // 
            this.btnForSavePullreleatedData.Location = new System.Drawing.Point(451, 536);
            this.btnForSavePullreleatedData.Name = "btnForSavePullreleatedData";
            this.btnForSavePullreleatedData.Size = new System.Drawing.Size(75, 23);
            this.btnForSavePullreleatedData.TabIndex = 17;
            this.btnForSavePullreleatedData.Text = "Save";
            this.btnForSavePullreleatedData.UseVisualStyleBackColor = true;
           // this.btnForSavePullreleatedData.Click += new System.EventHandler(this.btnForSavePullreleatedData_Click);
            // 
            // DbtableForm
            // 
            this.ClientSize = new System.Drawing.Size(974, 591);
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
            this.Controls.Add(this.btnForBack);
            this.Controls.Add(this.checkBoxforPullreleateddata);
            this.Controls.Add(this.labelForDatabaseTbl);
            this.Controls.Add(this.labelForDatabase);
            this.Controls.Add(this.cmbTables);
            this.Controls.Add(this.cmbDatabases);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DbtableForm";
            this.Text = "Database and Tables";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private ComboBox cmbDatabases;
        private ComboBox cmbTables;
        private Label labelForDatabase;
        private Label labelForDatabaseTbl;
        private CheckBox checkBoxforPullreleateddata;
        private Button btnForBack;
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
    }
}


//using System.Windows.Forms;
//namespace DE_IDENTIFICATION_TOOL.Forms
//{
//    partial class DbtableForm
//    {
//        private System.ComponentModel.IContainer components = null;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbtableForm));
//            this.cmbDatabases = new ComboBox();
//            this.cmbTables = new ComboBox();
//            this.labelForDatabase = new Label();
//            this.labelForDatabaseTbl = new Label();
//            this.checkBoxforPullreleateddata = new CheckBox();
//            this.btnForBack = new   Button();
//            this.btnForFinish = new Button();
//            this.btnForCancel = new Button();
//            this.btnForAddingColumns = new Button();
//            this.btnForDeleteColumns = new Button();
//            this.checkBox1 = new CheckBox();
//            this.label1 = new Label();
//            this.label2 = new Label();
//            this.label3 = new Label();
//            this.label4 = new Label();
//            this.checkBox2 = new CheckBox();
//            this.txtboxForTblName = new TextBox();
//            this.txtBoxFoeExistingTblKey = new  TextBox();
//            this.textBoxForSourceTbl = new TextBox();
//            this.textBoxForSourceTblKey = new TextBox();
//            this.panel1 = new Panel();
//            this.label6 = new Label();
//            this.label5 = new Label();
//            this.lblForNoofColumns = new Label();
//            this.txtForNoofColumns = new TextBox();
//            this.panel1.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // cmbDatabases
//            // 
//            this.cmbDatabases.DropDownStyle = ComboBoxStyle.DropDownList;
//            this.cmbDatabases.FormattingEnabled = true;
//            this.cmbDatabases.Location = new System.Drawing.Point(237, 103);
//            this.cmbDatabases.Name = "cmbDatabases";
//            this.cmbDatabases.Size = new System.Drawing.Size(475, 24);
//            this.cmbDatabases.TabIndex = 0;
//            this.cmbDatabases.SelectedIndexChanged += new System.EventHandler(this.cmbDatabases_SelectedIndexChanged);
//            // 
//            // cmbTables
//            // 
//            this.cmbTables.DropDownStyle = ComboBoxStyle.DropDownList;
//            this.cmbTables.FormattingEnabled = true;
//            this.cmbTables.Location = new System.Drawing.Point(237, 145);
//            this.cmbTables.Name = "cmbTables";
//            this.cmbTables.Size = new System.Drawing.Size(475, 24);
//            this.cmbTables.TabIndex = 1;
//            // 
//            // labelForDatabase
//            // 
//            this.labelForDatabase.AutoSize = true;
//            this.labelForDatabase.Location = new System.Drawing.Point(73, 103);
//            this.labelForDatabase.Name = "labelForDatabase";
//            this.labelForDatabase.Size = new System.Drawing.Size(74, 16);
//            this.labelForDatabase.TabIndex = 2;
//            this.labelForDatabase.Text = "Databases";
//            // 
//            // labelForDatabaseTbl
//            // 
//            this.labelForDatabaseTbl.AutoSize = true;
//            this.labelForDatabaseTbl.Location = new System.Drawing.Point(69, 153);
//            this.labelForDatabaseTbl.Name = "labelForDatabaseTbl";
//            this.labelForDatabaseTbl.Size = new System.Drawing.Size(50, 16);
//            this.labelForDatabaseTbl.TabIndex = 3;
//            this.labelForDatabaseTbl.Text = "Tables";
//            // 
//            // checkBoxforPullreleateddata
//            // 
//            this.checkBoxforPullreleateddata.AutoSize = true;
//            this.checkBoxforPullreleateddata.Location = new System.Drawing.Point(76, 199);
//            this.checkBoxforPullreleateddata.Name = "checkBoxforPullreleateddata";
//            this.checkBoxforPullreleateddata.Size = new System.Drawing.Size(134, 20);
//            this.checkBoxforPullreleateddata.TabIndex = 4;
//            this.checkBoxforPullreleateddata.Text = "Pull releated data";
//            this.checkBoxforPullreleateddata.UseVisualStyleBackColor = true;
//            // 
//            // btnForBack
//            // 
//            this.btnForBack.Location = new System.Drawing.Point(461, 536);
//            this.btnForBack.Name = "btnForBack";
//            this.btnForBack.Size = new System.Drawing.Size(75, 23);
//            this.btnForBack.TabIndex = 5;
//            this.btnForBack.Text = "Back";
//            this.btnForBack.UseVisualStyleBackColor = true;
//            // 
//            // btnForFinish
//            // 
//            this.btnForFinish.Location = new System.Drawing.Point(593, 535);
//            this.btnForFinish.Name = "btnForFinish";
//            this.btnForFinish.Size = new System.Drawing.Size(75, 23);
//            this.btnForFinish.TabIndex = 6;
//            this.btnForFinish.Text = "Finish";
//            this.btnForFinish.UseVisualStyleBackColor = true;
//            this.btnForFinish.Click += new System.EventHandler(this.btnForFinish_Click);
//            // 
//            // btnForCancel
//            // 
//            this.btnForCancel.Location = new System.Drawing.Point(712, 536);
//            this.btnForCancel.Name = "btnForCancel";
//            this.btnForCancel.Size = new System.Drawing.Size(75, 23);
//            this.btnForCancel.TabIndex = 7;
//            this.btnForCancel.Text = "Cancel";
//            this.btnForCancel.UseVisualStyleBackColor = true;
//            // 
//            // btnForAddingColumns
//            // 
//            this.btnForAddingColumns.Location = new System.Drawing.Point(72, 234);
//            this.btnForAddingColumns.Name = "btnForAddingColumns";
//            this.btnForAddingColumns.Size = new System.Drawing.Size(75, 23);
//            this.btnForAddingColumns.TabIndex = 8;
//            this.btnForAddingColumns.Text = "+  New";
//            this.btnForAddingColumns.UseVisualStyleBackColor = true;
//            this.btnForAddingColumns.Click += new System.EventHandler(this.btnForAddingColumns_Click);
//            // 
//            // btnForDeleteColumns
//            // 
//            this.btnForDeleteColumns.Location = new System.Drawing.Point(211, 234);
//            this.btnForDeleteColumns.Name = "btnForDeleteColumns";
//            this.btnForDeleteColumns.Size = new System.Drawing.Size(75, 23);
//            this.btnForDeleteColumns.TabIndex = 9;
//            this.btnForDeleteColumns.Text = "Delete";
//            this.btnForDeleteColumns.UseVisualStyleBackColor = true;
//            // 
//            // checkBox1
//            // 
//            this.checkBox1.AutoSize = true;
//            this.checkBox1.Location = new System.Drawing.Point(72, 289);
//            this.checkBox1.Name = "checkBox1";
//            this.checkBox1.Size = new System.Drawing.Size(18, 17);
//            this.checkBox1.TabIndex = 10;
//            this.checkBox1.UseVisualStyleBackColor = true;
//            // 
//            // label1
//            // 
//            this.label1.AutoSize = true;
//            this.label1.Location = new System.Drawing.Point(142, 289);
//            this.label1.Name = "label1";
//            this.label1.Size = new System.Drawing.Size(92, 16);
//            this.label1.TabIndex = 11;
//            this.label1.Text = "Existing Table";
//            // 
//            // label2
//            // 
//            this.label2.AutoSize = true;
//            this.label2.Location = new System.Drawing.Point(313, 289);
//            this.label2.Name = "label2";
//            this.label2.Size = new System.Drawing.Size(30, 16);
//            this.label2.TabIndex = 12;
//            this.label2.Text = "Key";
//            // 
//            // label3
//            // 
//            this.label3.AutoSize = true;
//            this.label3.Location = new System.Drawing.Point(478, 289);
//            this.label3.Name = "label3";
//            this.label3.Size = new System.Drawing.Size(89, 16);
//            this.label3.TabIndex = 13;
//            this.label3.Text = "Source Table";
//            // 
//            // label4
//            // 
//            this.label4.AutoSize = true;
//            this.label4.Location = new System.Drawing.Point(682, 290);
//            this.label4.Name = "label4";
//            this.label4.Size = new System.Drawing.Size(30, 16);
//            this.label4.TabIndex = 14;
//            this.label4.Text = "Key";
//            // 
//            // checkBox2
//            // 
//            this.checkBox2.AutoSize = true;
//            this.checkBox2.Location = new System.Drawing.Point(72, 338);
//            this.checkBox2.Name = "checkBox2";
//            this.checkBox2.Size = new System.Drawing.Size(18, 17);
//            this.checkBox2.TabIndex = 15;
//            this.checkBox2.UseVisualStyleBackColor = true;
//            // 
//            // txtboxForTblName
//            // 
//            this.txtboxForTblName.Location = new System.Drawing.Point(136, 332);
//            this.txtboxForTblName.Name = "txtboxForTblName";
//            this.txtboxForTblName.Size = new System.Drawing.Size(100, 22);
//            this.txtboxForTblName.TabIndex = 16;
//            this.txtboxForTblName.Text = "Table name";
//            // 
//            // txtBoxFoeExistingTblKey
//            // 
//            this.txtBoxFoeExistingTblKey.Location = new System.Drawing.Point(291, 332);
//            this.txtBoxFoeExistingTblKey.Name = "txtBoxFoeExistingTblKey";
//            this.txtBoxFoeExistingTblKey.Size = new System.Drawing.Size(100, 22);
//            this.txtBoxFoeExistingTblKey.TabIndex = 17;
//            this.txtBoxFoeExistingTblKey.Text = "Key";
//            // 
//            // textBoxForSourceTbl
//            // 
//            this.textBoxForSourceTbl.Location = new System.Drawing.Point(461, 334);
//            this.textBoxForSourceTbl.Name = "textBoxForSourceTbl";
//            this.textBoxForSourceTbl.Size = new System.Drawing.Size(100, 22);
//            this.textBoxForSourceTbl.TabIndex = 18;
//            this.textBoxForSourceTbl.Text = "Table Name ";
//            // 
//            // textBoxForSourceTblKey
//            // 
//            this.textBoxForSourceTblKey.Location = new System.Drawing.Point(651, 334);
//            this.textBoxForSourceTblKey.Name = "textBoxForSourceTblKey";
//            this.textBoxForSourceTblKey.Size = new System.Drawing.Size(100, 22);
//            this.textBoxForSourceTblKey.TabIndex = 19;
//            this.textBoxForSourceTblKey.Text = "Key";
//            // 
//            // panel1
//            // 
//            this.panel1.Controls.Add(this.label6);
//            this.panel1.Controls.Add(this.label5);
//            this.panel1.Location = new System.Drawing.Point(6, 0);
//            this.panel1.Name = "panel1";
//            this.panel1.Size = new System.Drawing.Size(967, 87);
//            this.panel1.TabIndex = 20;
//            // 
//            // label6
//            // 
//            this.label6.AutoSize = true;
//            this.label6.Location = new System.Drawing.Point(98, 52);
//            this.label6.Name = "label6";
//            this.label6.Size = new System.Drawing.Size(287, 16);
//            this.label6.TabIndex = 1;
//            this.label6.Text = "Please select the table you want  to import from ";
//            // 
//            // label5
//            // 
//            this.label5.AutoSize = true;
//            this.label5.Location = new System.Drawing.Point(38, 13);
//            this.label5.Name = "label5";
//            this.label5.Size = new System.Drawing.Size(50, 16);
//            this.label5.TabIndex = 0;
//            this.label5.Text = "Tables";
//            // 
//            // lblForNoofColumns
//            // 
//            this.lblForNoofColumns.AutoSize = true;
//            this.lblForNoofColumns.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblForNoofColumns.Location = new System.Drawing.Point(734, 145);
//            this.lblForNoofColumns.Name = "lblForNoofColumns";
//            this.lblForNoofColumns.Size = new System.Drawing.Size(100, 20);
//            this.lblForNoofColumns.TabIndex = 21;
//            this.lblForNoofColumns.Text = "Rows Count";
//            // 
//            // txtForNoofColumns
//            // 
//            this.txtForNoofColumns.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.txtForNoofColumns.Location = new System.Drawing.Point(840, 145);
//            this.txtForNoofColumns.Name = "txtForNoofColumns";
//            this.txtForNoofColumns.Size = new System.Drawing.Size(100, 27);
//            this.txtForNoofColumns.TabIndex = 22;
//            // 
//            // DbtableForm
//            // 
//            this.ClientSize = new System.Drawing.Size(974, 591);
//            this.Controls.Add(this.txtForNoofColumns);
//            this.Controls.Add(this.lblForNoofColumns);
//            this.Controls.Add(this.panel1);
//            this.Controls.Add(this.textBoxForSourceTblKey);
//            this.Controls.Add(this.textBoxForSourceTbl);
//            this.Controls.Add(this.txtBoxFoeExistingTblKey);
//            this.Controls.Add(this.txtboxForTblName);
//            this.Controls.Add(this.checkBox2);
//            this.Controls.Add(this.label4);
//            this.Controls.Add(this.label3);
//            this.Controls.Add(this.label2);
//            this.Controls.Add(this.label1);
//            this.Controls.Add(this.checkBox1);
//            this.Controls.Add(this.btnForDeleteColumns);
//            this.Controls.Add(this.btnForAddingColumns);
//            this.Controls.Add(this.btnForCancel);
//            this.Controls.Add(this.btnForFinish);
//            this.Controls.Add(this.btnForBack);
//            this.Controls.Add(this.checkBoxforPullreleateddata);
//            this.Controls.Add(this.labelForDatabaseTbl);
//            this.Controls.Add(this.labelForDatabase);
//            this.Controls.Add(this.cmbTables);
//            this.Controls.Add(this.cmbDatabases);
//            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
//            this.Name = "DbtableForm";
//            this.Text = "Database and Tables";
//            this.panel1.ResumeLayout(false);
//            this.panel1.PerformLayout();
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        private ComboBox cmbDatabases;
//        private ComboBox cmbTables;
//        private Label labelForDatabase;
//        private Label labelForDatabaseTbl;
//        private CheckBox checkBoxforPullreleateddata;
//        private Button btnForBack;
//        private Button btnForFinish;
//        private Button btnForCancel;
//        private Button btnForAddingColumns;
//        private Button btnForDeleteColumns;
//        private CheckBox checkBox1;
//        private Label label1;
//        private Label label2;
//        private Label label3;
//        private Label label4;
//        private CheckBox checkBox2;
//        private TextBox txtboxForTblName;
//        private TextBox txtBoxFoeExistingTblKey;
//        private TextBox textBoxForSourceTbl;
//        private TextBox textBoxForSourceTblKey;
//        private Panel panel1;
//        private Label label6;
//        private Label label5;
//        private Label lblForNoofColumns;
//        private TextBox txtForNoofColumns;
//    }
//}




////namespace DE_IDENTIFICATION_TOOL.Forms
////{
////    partial class DbtableForm
////    {
////        /// <summary>
////        /// Required designer variable.
////        /// </summary>
////        private System.ComponentModel.IContainer components = null;

////        /// <summary>
////        /// Clean up any resources being used.
////        /// </summary>
////        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
////        protected override void Dispose(bool disposing)
////        {
////            if (disposing && (components != null))
////            {
////                components.Dispose();
////            }
////            base.Dispose(disposing);
////        }

////        #region Windows Form Designer generated code

////        /// <summary>
////        /// Required method for Designer support - do not modify
////        /// the contents of this method with the code editor.
////        /// </summary>
////        private void InitializeComponent()
////        {
////            this.labelForDatabase = new System.Windows.Forms.Label();
////            this.SuspendLayout();
////            // 
////            // labelForDatabase
////            // 
////            this.labelForDatabase.AutoSize = true;
////            this.labelForDatabase.Location = new System.Drawing.Point(37, 45);
////            this.labelForDatabase.Name = "labelForDatabase";
////            this.labelForDatabase.Size = new System.Drawing.Size(68, 16);
////            this.labelForDatabase.TabIndex = 0;
////            this.labelForDatabase.Text = "DataBase";
////            // 
////            // DbtableForm
////            // 
////            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
////            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
////            this.ClientSize = new System.Drawing.Size(800, 450);
////            this.Controls.Add(this.labelForDatabase);
////            this.Name = "DbtableForm";
////            this.Text = "DbtableForm";
////            this.ResumeLayout(false);
////            this.PerformLayout();

////        }

////        #endregion

////        private System.Windows.Forms.Label labelForDatabase;
////    }
////}