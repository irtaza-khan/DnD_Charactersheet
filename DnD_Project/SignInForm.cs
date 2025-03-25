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
            MainForm MainForm = new MainForm(); // Open Sign-In Form
            MainForm.Show();
            //P("Going to sign in");
            //MessageBox.Show("Going to sign in");
            this.Hide(); // Close Sign-Up Form
        }
    }
}
