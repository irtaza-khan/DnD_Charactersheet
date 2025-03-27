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
        private string connectionString = File.ReadAllText(@"D:\Study Material\Fiverr\DnD_Charactersheet\DnD_Project\db_connection.txt").Trim();

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

        private void LoadUserData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Fetch User Data
                    string userQuery = "SELECT Name, Username FROM Users WHERE Username = @username";
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

                    // Display number of characters in txtPassword (as asterisks)
                    txtPassword.Text = new string('*', charCount);

                    // Fetch Character Names
                    cmbRole.Items.Clear();
                    string fetchCharNames = "SELECT CharacterName FROM Characters WHERE UserID = (SELECT UserID FROM Users WHERE Username = @username)";

                    using (SqlCommand nameCmd = new SqlCommand(fetchCharNames, conn))
                    {
                        nameCmd.Parameters.AddWithValue("@username", currentUsername);
                        using (SqlDataReader nameReader = nameCmd.ExecuteReader())
                        {
                            while (nameReader.Read())
                            {
                                cmbRole.Items.Add(nameReader["CharacterName"].ToString());
                            }
                        }
                    }

                    // If no characters found, set "None"
                    if (cmbRole.Items.Count == 0)
                    {
                        txtPassword.Text = "0";
                        cmbRole.Items.Add("None");
                    }

                    cmbRole.SelectedIndex = 0; // Select first item
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
            CharacterSheet charactersheet = new CharacterSheet(); // Open Sign-In Form
            charactersheet.Show();
            //P("Going to sign in");
            //MessageBox.Show("Going to sign in");
            this.Hide(); // Close Sign-Up Form
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CharacterSheet charactersheet = new CharacterSheet(); // Open Sign-In Form
            charactersheet.Show();
            //P("Going to sign in");
            //MessageBox.Show("Going to sign in");
            this.Hide(); // Close Sign-Up Form
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignInForm signin = new SignInForm(); // Open Sign-In Form
            signin.Show();
            //P("Going to sign in");
            //MessageBox.Show("Going to sign in");
            this.Hide(); // Close Sign-Up Form
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CharacterSheet sheet = new CharacterSheet(this);
            sheet.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            CharacterSheet sheet = new CharacterSheet(this);
            sheet.Show();
            this.Hide(); 
        }
    }
}
