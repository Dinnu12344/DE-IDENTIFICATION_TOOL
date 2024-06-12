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
            this.SuspendLayout();
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabases.FormattingEnabled = true;
            this.cmbDatabases.Location = new System.Drawing.Point(237, 103);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(475, 24);
            this.cmbDatabases.TabIndex = 0;
            this.cmbDatabases.SelectedIndexChanged += new System.EventHandler(this.cmbDatabases_SelectedIndexChanged);
            // 
            // cmbTables
            // 
            this.cmbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTables.FormattingEnabled = true;
            this.cmbTables.Location = new System.Drawing.Point(237, 145);
            this.cmbTables.Name = "cmbTables";
            this.cmbTables.Size = new System.Drawing.Size(475, 24);
            this.cmbTables.TabIndex = 1;
            // 
            // labelForDatabase
            // 
            this.labelForDatabase.AutoSize = true;
            this.labelForDatabase.Location = new System.Drawing.Point(73, 103);
            this.labelForDatabase.Name = "labelForDatabase";
            this.labelForDatabase.Size = new System.Drawing.Size(74, 16);
            this.labelForDatabase.TabIndex = 2;
            this.labelForDatabase.Text = "Databases";
            // 
            // labelForDatabaseTbl
            // 
            this.labelForDatabaseTbl.AutoSize = true;
            this.labelForDatabaseTbl.Location = new System.Drawing.Point(69, 153);
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
            this.btnForBack.Location = new System.Drawing.Point(461, 536);
            this.btnForBack.Name = "btnForBack";
            this.btnForBack.Size = new System.Drawing.Size(75, 23);
            this.btnForBack.TabIndex = 5;
            this.btnForBack.Text = "Back";
            this.btnForBack.UseVisualStyleBackColor = true;
            // 
            // btnForFinish
            // 
            this.btnForFinish.Location = new System.Drawing.Point(593, 535);
            this.btnForFinish.Name = "btnForFinish";
            this.btnForFinish.Size = new System.Drawing.Size(75, 23);
            this.btnForFinish.TabIndex = 6;
            this.btnForFinish.Text = "Finish";
            this.btnForFinish.UseVisualStyleBackColor = true;
            this.btnForFinish.Click += new System.EventHandler(this.btnForFinish_Click);
            // 
            // btnForCancel
            // 
            this.btnForCancel.Location = new System.Drawing.Point(712, 536);
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
            this.panelForPullreleatedData.AutoScroll = true;
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
            this.txtForNoofColumns.Location = new System.Drawing.Point(862, 147);
            this.txtForNoofColumns.Name = "txtForNoofColumns";
            this.txtForNoofColumns.Size = new System.Drawing.Size(100, 22);
            this.txtForNoofColumns.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(757, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Row Count";
            // 
            // DbtableForm
            // 
            this.ClientSize = new System.Drawing.Size(974, 591);
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
    }
}




//namespace DE_IDENTIFICATION_TOOL.Forms
//{
//    partial class DbtableForm
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.labelForDatabase = new System.Windows.Forms.Label();
//            this.SuspendLayout();
//            // 
//            // labelForDatabase
//            // 
//            this.labelForDatabase.AutoSize = true;
//            this.labelForDatabase.Location = new System.Drawing.Point(37, 45);
//            this.labelForDatabase.Name = "labelForDatabase";
//            this.labelForDatabase.Size = new System.Drawing.Size(68, 16);
//            this.labelForDatabase.TabIndex = 0;
//            this.labelForDatabase.Text = "DataBase";
//            // 
//            // DbtableForm
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(800, 450);
//            this.Controls.Add(this.labelForDatabase);
//            this.Name = "DbtableForm";
//            this.Text = "DbtableForm";
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion

//        private System.Windows.Forms.Label labelForDatabase;
//    }
//}