using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DnD_Project
{
    public partial class SignInForm : Form
    {
        public SignInForm()
        {
            InitializeComponent();
        }

        private void SignInForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkSignIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUpForm signUpForm = new SignUpForm(); // Open Sign-In Form
            signUpForm.Show();
            //P("Going to sign in");
            //MessageBox.Show("Going to sign in");
            this.Hide(); // Close Sign-Up Form
        }
        private void SignInForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0) // If no forms are open, exit
            {
                Application.Exit();
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string role = cmbRole.SelectedItem?.ToString(); // Get selected role

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please select a role before signing in.", "Role Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate credentials (Replace with actual database verification)
            //bool isValidUser = AuthenticateUser(username, password, role);
            bool isValidUser = true;
            if (isValidUser)
            {
                this.Hide(); // Hide the SignInForm

                if (role == "Admin")
                {
                    AdminProfileForm adminForm = new AdminProfileForm();
                    adminForm.Show();
                }
                else if (role == "User")
                {
                    UserProfileForm userForm = new UserProfileForm();
                    userForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
