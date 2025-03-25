using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            SignInForm signInForm = new SignInForm(); // Open Sign-In Form
            signInForm.Show();
            //P("Going to sign in");
            //MessageBox.Show("Going to sign in");
            this.Hide(); // Close Sign-Up Form
        }


        private void btnSignUp_Click(object sender, EventArgs e)
        {
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

            MessageBox.Show($"Sign-Up Successful!\n\nName: {name}\nUsername: {email}\nRole: {role}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}