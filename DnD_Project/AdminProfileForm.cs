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
using System.IO;

namespace DnD_Project
{
    public partial class AdminProfileForm : Form
    {
        public AdminProfileForm()
        {
            InitializeComponent();
        }

        private string adminUsername;
        private string connectionString = File.ReadAllText(@"db_connection.txt").Trim();

        public AdminProfileForm(string username)
        {
            InitializeComponent();
            adminUsername = username;
            LoadAdminData();
        }

        private void LoadAdminData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Fetch Admin Data
                    string adminQuery = "SELECT Name, Username FROM Users WHERE Username = @username";
                    using (SqlCommand adminCmd = new SqlCommand(adminQuery, conn))
                    {
                        adminCmd.Parameters.AddWithValue("@username", adminUsername);
                        using (SqlDataReader reader = adminCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["Name"].ToString();
                                txtEmail.Text = reader["Username"].ToString();
                            }
                        }
                    }

                    // Fetch All Users (Except Admin)
                    cmbRole.Items.Clear();
                    string userQuery = "SELECT Username FROM Users WHERE Role = 'User'"; // Fetch only users, not admins

                    using (SqlCommand userCmd = new SqlCommand(userQuery, conn))
                    {
                        using (SqlDataReader userReader = userCmd.ExecuteReader())
                        {
                            while (userReader.Read())
                            {
                                cmbRole.Items.Add(userReader["Username"].ToString());
                            }
                        }
                    }

                    // If no users found, set "No Users Available"
                    if (cmbRole.Items.Count == 0)
                    {
                        cmbRole.Items.Add("No Users Available");
                    }

                    cmbRole.SelectedIndex = 0; // Select first item

                    // Limit dropdown to show only 5 rows
                    cmbRole.DropDownHeight = cmbRole.ItemHeight * 5;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading admin data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignInForm signin = new SignInForm(); // Open Sign-In Form
            signin.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbRole.SelectedItem == null || cmbRole.SelectedItem.ToString() == "No Users Available")
            {
                MessageBox.Show("Please select a valid user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedUser = cmbRole.SelectedItem.ToString();
            UserManageProfile userManageForm = new UserManageProfile(selectedUser, adminUsername);
            userManageForm.Show();
            this.Hide();
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CharacterListForm detailsForm = new CharacterListForm(this);
            detailsForm.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
