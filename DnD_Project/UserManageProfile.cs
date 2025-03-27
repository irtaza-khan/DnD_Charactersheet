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
    public partial class UserManageProfile : Form
    {
        private string adminUsername;
        private string selectedUsername;
        private string connectionString = File.ReadAllText(@"D:\Study Material\Fiverr\DnD_Charactersheet\DnD_Project\db_connection.txt").Trim();

        public UserManageProfile()
        {
            InitializeComponent();
        }

        public UserManageProfile(string username, string adminUsername)
        {
            InitializeComponent();
            selectedUsername = username;
            LoadUserData();
            this.adminUsername = adminUsername;
        }

        private int GetUserIDByUsername(string username)
        {
            string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db_connection.txt");
            string connectionString = File.ReadAllText(dbFilePath).Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT UserID FROM Users WHERE Username = @Username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    object result = cmd.ExecuteScalar();
                    return (result != null) ? Convert.ToInt32(result) : -1; // Return UserID or -1 if not found
                }
            }
        }

        private void LoadUserData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Fetch User's Name and Email
                    string userQuery = "SELECT Name, Username FROM Users WHERE Username = @username";
                    using (SqlCommand userCmd = new SqlCommand(userQuery, conn))
                    {
                        userCmd.Parameters.AddWithValue("@username", selectedUsername);
                        using (SqlDataReader reader = userCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["Name"].ToString();
                                txtEmail.Text = reader["Username"].ToString();
                            }
                        }
                    }

                    // Fetch Number of Characters and Character List
                    cmbChar.Items.Clear();
                    string charQuery = "SELECT COUNT(*) FROM Characters WHERE UserID = (SELECT UserID FROM Users WHERE Username = @username)";

                    int characterCount = 0;
                    using (SqlCommand charCmd = new SqlCommand(charQuery, conn))
                    {
                        charCmd.Parameters.AddWithValue("@username", selectedUsername);
                        characterCount = (int)charCmd.ExecuteScalar();
                    }

                    txtChar.Text = characterCount.ToString(); // Set character count

                    if (characterCount > 0)
                    {
                        //string getCharactersQuery = "SELECT COUNT(*) FROM Characters WHERE UserID = (SELECT UserID FROM Users WHERE Username = @username)";

                        // Fetch Character Names
                        string getCharactersQuery = "SELECT Character_Name FROM Characters WHERE UserID = (SELECT UserID FROM Users WHERE Username = @username)";
                        using (SqlCommand getCharCmd = new SqlCommand(getCharactersQuery, conn))
                        {
                            getCharCmd.Parameters.AddWithValue("@username", selectedUsername);
                            using (SqlDataReader charReader = getCharCmd.ExecuteReader())
                            {
                                while (charReader.Read())
                                {
                                    cmbChar.Items.Add(charReader["Character_Name"].ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        cmbChar.Items.Add("None");
                    }

                    cmbChar.SelectedIndex = 0; // Select first item
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CharacterSheet sheet = new CharacterSheet(this, GetUserIDByUsername(this.selectedUsername));
            sheet.Show();
            this.Hide(); 
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            SignInForm signin = new SignInForm(); 
            signin.Show();
            this.Hide(); 
        }

        private void UserManageProfile_Load(object sender, EventArgs e)
        {

        }

        private void txtChar_TextChanged(object sender, EventArgs e)
        {

        }

        private void goBackButton_Click(object sender, EventArgs e)
        {
            AdminProfileForm adminprofile = new AdminProfileForm(adminUsername);
            adminprofile.Show();
            this.Hide();
        }

        private void cmbChar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
