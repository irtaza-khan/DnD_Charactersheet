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
    public partial class AdminProfileForm : Form
    {
        public AdminProfileForm()
        {
            InitializeComponent();
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
            //P("Going to sign in");
            //MessageBox.Show("Going to sign in");
            this.Hide(); // Close Sign-Up Form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CharacterSheet sheet = new CharacterSheet(this); 
            sheet.Show();
            this.Hide(); // Close Sign-Up Form
        }
    }
}
