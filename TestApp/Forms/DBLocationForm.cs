using DE_IDENTIFICATION_TOOL.CustomAction;
using DE_IDENTIFICATION_TOOL.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class DBLocationForm : Form
    {
        private readonly string labelName;
        private TreeNode selectedNode;
        private HomeForm homeForm;
        private List<ProjectData> projectData;
        public DbtableFormModel _dbtableFormModel;
        private PictureBox picEye;

        public DBLocationForm(string labelName, TreeNode selectedNode, List<ProjectData> projectData, HomeForm homeForm)
        {
            InitializeComponent();
            this.labelName = labelName;
            this.selectedNode = selectedNode;
            this.homeForm = homeForm;
            this.projectData = projectData;
            _dbtableFormModel = new DbtableFormModel();

            lblForServer.Visible = false;
            txtForServer.Visible = false;

            lblForUserName.Visible = false;
            txtForUsername.Visible = false;

            lblForPassword.Visible = false;
            txtForPassword.Visible = false;

            // Initialize and configure the eye icon
            picEye = new PictureBox
            {
                Size = new System.Drawing.Size(20, 20),
                Location = new System.Drawing.Point(txtForPassword.Width - 25, 3), // Adjust the position
                Cursor = Cursors.Hand,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.visible, // Assuming you have an image resource for the eye icon
                Visible = false // Initially hidden
            };
            txtForPassword.Controls.Add(picEye); // Add the eye icon inside the password box

            btnForFinish.Enabled = false;
            ComboBoxHelper.PreventScroll(this.dbTyped);

            // Add event handlers for the eye icon
            picEye.MouseEnter += PicEye_MouseEnter;
            picEye.MouseLeave += PicEye_MouseLeave;

            // Add event handler for ComboBox
            dbTyped.SelectedIndexChanged += dbTyped_SelectedIndexChanged;

            // Add event handlers for text boxes
            txtForServer.TextChanged += txtForServer_TextChanged;
            txtForUsername.TextChanged += txtForUsername_TextChanged;
            txtForPassword.TextChanged += txtForPassword_TextChanged;
        }

        private void PicEye_MouseEnter(object sender, EventArgs e)
        {
            // Show the password when the mouse enters the eye icon
            txtForPassword.UseSystemPasswordChar = false;
        }

        private void PicEye_MouseLeave(object sender, EventArgs e)
        {
            // Hide the password when the mouse leaves the eye icon
            txtForPassword.UseSystemPasswordChar = true;
        }

        // Other event handlers and methods remain unchanged
        private void btnForBackInJdbcFrm_Click(object sender, EventArgs e)
        {
            this.Hide();
            //homeForm.Show();
        }

        private void btnForNextinJdbcFrm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button is clicked");
        }

        private void btnForCancelInJdbcFrm_Click(object sender, EventArgs e)
        {
            dbTyped.Text = string.Empty;
            txtForServer.Text = string.Empty;
            txtForUsername.Text = string.Empty;
            txtForPassword.Text = string.Empty;
            this.Close();
        }

        private void dbTyped_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = dbTyped.SelectedItem.ToString();

            Debug.WriteLine($"Selected item: {selectedItem}");

            if (selectedItem == "SQL")
            {
                lblForServer.Visible = true;
                txtForServer.Visible = true;

                lblForUserName.Visible = true;
                txtForUsername.Visible = true;

                lblForPassword.Visible = true;
                txtForPassword.Visible = true;

                picEye.Visible = !string.IsNullOrEmpty(txtForPassword.Text);
            }
            else
            {
                lblForServer.Visible = false;
                txtForServer.Visible = false;

                lblForUserName.Visible = false;
                txtForUsername.Visible = false;

                lblForPassword.Visible = false;
                txtForPassword.Visible = false;

                picEye.Visible = false; // Hide the eye icon
            }
        }

        private void btnForFinish_Click(object sender, EventArgs e)
        {
            string projectName = labelName;
            string connectionString = $"server={this.txtForServer.Text};" +
                                      $"user id={this.txtForUsername.Text};" +
                                      $"password={this.txtForPassword.Text};" +
                                      $"connection timeout=30";

            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                try
                {
                    myConnection.Open();

                    string query = "SELECT name FROM sys.databases";

                    using (SqlCommand cmd = new SqlCommand(query, myConnection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            StringBuilder databaseNames = new StringBuilder();

                            while (reader.Read())
                            {
                                databaseNames.AppendLine(reader["name"].ToString());
                            }
                        }
                    }

                    DbtableFormModel model = new DbtableFormModel
                    {
                        ConnectionString = connectionString,
                        SelectedNode = selectedNode,
                        ProjectData = projectData,
                        HomeForm = homeForm,
                        ProjectName = projectName
                    };

                    DbtableForm dbTableForm = new DbtableForm(model, this);
                    dbTableForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateFinishButtonVisibility()
        {
            bool isServerNameNotEmpty = !string.IsNullOrEmpty(txtForServer.Text);
            bool isUserNameNotEmpty = !string.IsNullOrEmpty(txtForUsername.Text);
            bool isPasswordNotEmpty = !string.IsNullOrEmpty(txtForPassword.Text);

            btnForFinish.Enabled = isServerNameNotEmpty && isUserNameNotEmpty && isPasswordNotEmpty;

            picEye.Visible = isPasswordNotEmpty;

            Debug.WriteLine($"Server: {isServerNameNotEmpty}, User: {isUserNameNotEmpty}, Password: {isPasswordNotEmpty}");
        }

        private void txtForServer_TextChanged(object sender, EventArgs e)
        {
            _dbtableFormModel.serverName = txtForServer.Text;
            UpdateFinishButtonVisibility();
        }

        private void txtForUsername_TextChanged(object sender, EventArgs e)
        {
            _dbtableFormModel.userName = txtForUsername.Text;
            UpdateFinishButtonVisibility();
        }

        private void txtForPassword_TextChanged(object sender, EventArgs e)
        {
            _dbtableFormModel.password = txtForPassword.Text;
            UpdateFinishButtonVisibility();
        }
    }
}

