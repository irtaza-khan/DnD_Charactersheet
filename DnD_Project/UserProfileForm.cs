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
    public partial class UserProfileForm : Form
    {
        public UserProfileForm()
        {
            InitializeComponent();
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
