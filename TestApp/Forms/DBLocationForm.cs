using DE_IDENTIFICATION_TOOL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class DBLocationForm : Form
    {
        private readonly string labelName;
        private TreeNode selectedNode;
        private HomeForm homeForm;
        private List<ProjectData> projectData;
        public DbtableFormModel _dbtableFormModel;
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

            btnForFinish.Enabled = false;
        }

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

            if (selectedItem == "SQL")
            {
                lblForServer.Visible = true;
                txtForServer.Visible = true;

                lblForUserName.Visible = true;
                txtForUsername.Visible = true;

                lblForPassword.Visible = true;
                txtForPassword.Visible = true;
            }
            else if (selectedItem == "Oracle")
            {
            }
            else
            {
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
            //this.Hide();
        }        

        private void UpdateFinishButtonVisibility()
        {
            btnForFinish.Enabled = !string.IsNullOrEmpty(_dbtableFormModel.serverName) &&
                                   !string.IsNullOrEmpty(_dbtableFormModel.userName) &&
                                   !string.IsNullOrEmpty(_dbtableFormModel.password);
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
