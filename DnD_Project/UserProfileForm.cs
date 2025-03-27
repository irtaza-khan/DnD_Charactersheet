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
    public partial class UserProfileForm : Form
    {
        private string currentUsername;
        private string connectionString = File.ReadAllText(@"db_connection.txt").Trim();
        private int userid;
        public UserProfileForm()
        {
            InitializeComponent();
        }
        public UserProfileForm(string username)
        {
            InitializeComponent();
            currentUsername = username;
            LoadUserData();
        }

        private int GetUserIDByUsername(string username)
        {
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

                    // Fetch User Data
                    string userQuery = "SELECT Name, Username, UserID FROM Users WHERE Username = @username";
                    using (SqlCommand userCmd = new SqlCommand(userQuery, conn))
                    {
                        userCmd.Parameters.AddWithValue("@username", currentUsername);

                        using (SqlDataReader reader = userCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["Name"].ToString();
                                txtEmail.Text = reader["Username"].ToString();
                            }
                        }
                    }

                    // Fetch Characters Linked to User
                    string charQuery = "SELECT COUNT(*) FROM Characters WHERE UserID = (SELECT UserID FROM Users WHERE Username = @username)";
                    int charCount = 0;

                    using (SqlCommand charCmd = new SqlCommand(charQuery, conn))
                    {
                        charCmd.Parameters.AddWithValue("@username", currentUsername);
                        charCount = (int)charCmd.ExecuteScalar();
                    }
                    cmbRole.Items.Clear();

                    string fetchCharData = "SELECT Character_ID, Character_Name FROM Characters WHERE UserID = (SELECT UserID FROM Users WHERE Username = @username)";

                    using (SqlCommand nameCmd = new SqlCommand(fetchCharData, conn))
                    {
                        nameCmd.Parameters.AddWithValue("@username", currentUsername);
                        using (SqlDataReader nameReader = nameCmd.ExecuteReader())
                        {
                            cmbRole.Items.Clear(); // Clear previous items if any

                            while (nameReader.Read())
                            {
                                int characterId = nameReader.GetInt32(0); // Fetch Character_ID
                                string characterName = nameReader.GetString(1); // Fetch Character_Name

                                cmbRole.Items.Add(new KeyValuePair<int, string>(characterId, characterName));
                            }
                        }
                    }

                    // Set display & value members
                    cmbRole.DisplayMember = "Value"; // Show Character_Name in ComboBox
                    cmbRole.ValueMember = "Key"; // Use Character_ID as the actual value

                    // If no characters found, set "None"
                    if (cmbRole.Items.Count == 0)
                    {
                        cmbRole.Items.Add(new KeyValuePair<int, string>(0, "None"));
                        txtPassword.Text = "0";
                    }
                    else
                    {
                        txtPassword.Text = cmbRole.Items.Count.ToString(); // Store character count
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CharacterSheet charactersheet = new CharacterSheet(); 
            charactersheet.Show();
            this.Hide(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CharacterSheet charactersheet = new CharacterSheet(); 
            charactersheet.Show();
            this.Hide(); 
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignInForm signin = new SignInForm();
            signin.Show();
            this.Hide(); 
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.userid = GetUserIDByUsername(this.currentUsername);
            CharacterSheet sheet = new CharacterSheet(this, userid);
            sheet.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.userid = GetUserIDByUsername(this.currentUsername);
            CharacterSheet sheet = new CharacterSheet(this, userid);
            sheet.Show();
            this.Hide(); 
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void viewCharacterButton_Click(object sender, EventArgs e)
        {
            
            CharacterListForm detailsForm = new CharacterListForm(this);
            detailsForm.Show();
            this.Hide();
        }
    }
}
