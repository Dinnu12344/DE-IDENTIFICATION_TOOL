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

namespace DE_IDENTIFICATION_TOOL.Forms
{
    public partial class DBLocationForm : Form
    {
        public DBLocationForm()
        {
            InitializeComponent();
            lblForServer.Visible = false;
            txtForServer.Visible = false;

            lblForPort.Visible = false;
            txtForPort.Visible = false;

            lblForUserName.Visible = false;
            txtForUsername.Visible = false;

            lblForPassword.Visible = false;
            txtForPassword.Visible = false;

            lblForDatabase.Visible = false;
            txtForDatabase.Visible = false;

            btnFroNextinJdbcFrm.Enabled = false;
            btnForFinish.Enabled = true;
            //btnForCancelInJdbcFrm.Enabled = false;


        }


        private void btnForBackInJdbcFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnForNextinJdbcFrm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button is clicked");
        }

        private void btnForCancelInJdbcFrm_Click(object sender, EventArgs e)
        {
            dbTypedd.Text = string.Empty;
            txtForServer.Text = string.Empty;
            txtForPort.Text = string.Empty;
            txtForUsername.Text = string.Empty;
            lblForPassword.Text = string.Empty;
            txtForPassword.Text = string.Empty;
            txtForDatabase.Text = string.Empty;
        }

        private void dbTypedd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = dbTypedd.SelectedItem.ToString();

            // If "My Sql" is selected
            if (selectedItem == "My SQL")
            {
                // Hide MySQL related controls
                lblForServer.Visible = true;
                txtForServer.Visible = true;

                lblForPort.Visible = true;
                txtForPort.Visible = true;

                lblForUserName.Visible = true;
                txtForUsername.Visible = true;

                lblForPassword.Visible = true;
                txtForPassword.Visible = true;

                lblForDatabase.Visible = true;
                txtForDatabase.Visible = true;
            }
            // If "Oracle" is selected
            else if (selectedItem == "Oracle")
            {
                // Hide Oracle related control
                //txt.Visible = false;

                // You may need to show/hide other controls based on your requirements
            }
            // Add other cases for different database types if needed
            else
            {
                // Handle other database types if necessary
            }
        }


        private void btnForFinish_Click(object sender, EventArgs e)
        {
            string connectionString = $"server={this.txtForServer.Text};" +
                                      //$"database={this.txtForDatabase.Text}; " +
                                      $"user id={this.txtForUsername.Text};" +
                                      $"password={this.txtForPassword.Text};" +
                                      $"connection timeout=30";

            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                try
                {
                    myConnection.Open();
                    MessageBox.Show("Connection opened");

                    // Query to get all database names
                    string query = "SELECT name FROM sys.databases";

                    using (SqlCommand cmd = new SqlCommand(query, myConnection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Use a StringBuilder to collect all database names
                            StringBuilder databaseNames = new StringBuilder();

                            while (reader.Read())
                            {
                                databaseNames.AppendLine(reader["name"].ToString());
                            }

                            MessageBox.Show($"Databases on the server:\n{databaseNames.ToString()}", "Databases", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        //private void btnForFinish_Click(object sender, EventArgs e)
        //{
        //    string connectionString = $"server={this.txtForServer.Text};" +
        //                              //$"database={this.txtForDatabase.Text}; " +
        //                              $"user id={this.txtForUsername.Text};" +
        //                              $"password={this.txtForPassword.Text};" +
        //                              $"connection timeout=30";

        //    using (SqlConnection myConnection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            myConnection.Open();
        //            MessageBox.Show("Connection opened");

        //            // Query to get all table names
        //            string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

        //            using (SqlCommand cmd = new SqlCommand(query, myConnection))
        //            {
        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    // Use a StringBuilder to collect all table names
        //                    System.Text.StringBuilder tableNames = new System.Text.StringBuilder();

        //                    while (reader.Read())
        //                    {
        //                        tableNames.AppendLine(reader["TABLE_NAME"].ToString());
        //                    }

        //                    MessageBox.Show($"Tables in the database:\n{tableNames.ToString()}", "Tables", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Connection failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}
    }
}
