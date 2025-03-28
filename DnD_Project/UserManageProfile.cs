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
        private string connectionString = File.ReadAllText(@"db_connection.txt").Trim();

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

                        // Fetch Characters Linked to User
                        string charQuery1 = "SELECT COUNT(*) FROM Characters WHERE UserID = (SELECT UserID FROM Users WHERE Username = @username)";
                        int charCount = 0;

                        using (SqlCommand charCmd = new SqlCommand(charQuery, conn))
                        {
                            charCmd.Parameters.AddWithValue("@username", selectedUsername);
                            charCount = (int)charCmd.ExecuteScalar();
                        }
                        cmbChar.Items.Clear();

                        // Fetch character data from database
                        string fetchCharData = "SELECT Character_ID, Character_Name FROM Characters WHERE UserID = (SELECT UserID FROM Users WHERE Username = @username)";

                        List<KeyValuePair<int, string>> characterList = new List<KeyValuePair<int, string>>();

                        using (SqlCommand nameCmd = new SqlCommand(fetchCharData, conn))
                        {
                            nameCmd.Parameters.AddWithValue("@username", selectedUsername);
                            using (SqlDataReader nameReader = nameCmd.ExecuteReader())
                            {
                                while (nameReader.Read())
                                {
                                    int characterId = nameReader.GetInt32(0);
                                    string characterName = nameReader.GetString(1);

                                    characterList.Add(new KeyValuePair<int, string>(characterId, characterName));
                                }
                            }
                        }

                        // If no characters found, add "None" option
                        if (characterList.Count == 0)
                        {
                            characterList.Add(new KeyValuePair<int, string>(0, "None"));
                            txtChar.Text = "0";
                        }
                        else
                        {
                            txtChar.Text = characterList.Count.ToString(); // Store character count
                        }

                        // Bind the list to the ComboBox
                        cmbChar.DataSource = characterList;
                        cmbChar.DisplayMember = "Value"; // Show Character_Name
                        cmbChar.ValueMember = "Key"; // Store Character_ID

                    }
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
            if (cmbChar.SelectedValue != null && int.TryParse(cmbChar.SelectedValue.ToString(), out int selectedCharacterID))
            {
                CharacterSheet characterSheetForm = new CharacterSheet(this, GetUserIDByUsername(this.selectedUsername), selectedCharacterID, true);
                characterSheetForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid character selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
