using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace DnD_Project
{
    public partial class SignUpForm : Form
    {
        private GroupBox groupBox1;
        private Label label1, label2, label3, label4, label5;
        private TextBox txtName, txtEmail, txtPassword;

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            //Check if username exist then make label 7 visible
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void SignUpForm_Load(object sender, EventArgs e)
        {

        }

        private ComboBox cmbRole;
        private Button btnSignUp;

        public SignUpForm()
        {
            InitializeComponent();
        }



        //private void InitializeComponent()
        //{


        //}

        private void linkSignIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignInForm signInForm = new SignInForm(); 
            signInForm.Show();
            this.Hide(); 
        }
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            // Hide the label initially
            //label1.Visible = false;

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields and select a role!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = txtName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string role = cmbRole.SelectedItem.ToString();

            // Read connection string from a file
            string connectionString = File.ReadAllText(@"db_connection.txt").Trim();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if username already exists
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@username", email);
                        int userCount = (int)checkCmd.ExecuteScalar();

                        if (userCount > 0)
                        { 
                            label7.Visible = true;
                            return;
                        }
                    }

                    // Generate a unique salt
                    string salt = GenerateSalt();

                    // Hash the password with the salt
                    string hashedPassword = HashPassword(password, salt);

                    // Insert new user
                    string insertQuery = "INSERT INTO Users (Username,Name , PasswordHash, Salt, Role) VALUES (@username,@name, @passwordHash, @salt, @role)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@username", email);
                        insertCmd.Parameters.AddWithValue("@name", name);
                        insertCmd.Parameters.AddWithValue("@passwordHash", hashedPassword);
                        insertCmd.Parameters.AddWithValue("@salt", salt);
                        insertCmd.Parameters.AddWithValue("@role", role);

                        insertCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Sign-Up Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SignInForm signinForm = new SignInForm();
                signinForm.Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        // Hash password using SHA-256 + Salt
        public static string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}