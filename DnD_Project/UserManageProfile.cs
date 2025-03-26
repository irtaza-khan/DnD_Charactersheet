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
    public partial class UserManageProfile : Form
    {
        public UserManageProfile()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserManageProfile sheet = new UserManageProfile();
            sheet.Show();
            this.Hide(); 
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            SignInForm signin = new SignInForm(); // Open Sign-In Form
            signin.Show();
            //P("Going to sign in");
            //MessageBox.Show("Going to sign in");
            this.Hide(); // Close Sign-Up Form
        }
    }
}
