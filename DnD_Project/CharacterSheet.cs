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
    public partial class CharacterSheet : Form
    {
        private Form previousForm; // Store the reference to the previous form
        public CharacterSheet()
        {
            InitializeComponent();
        }

        // Constructor accepts previous form as parameter
        public CharacterSheet(Form callerForm)
        {
            InitializeComponent();
            this.previousForm = callerForm; // Store reference
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }
        private void SavingThrows_CheckedChanged(object sender, EventArgs e)
        {
            // Get all checkboxes inside the GroupBox
            var checkboxes = groupBox11.Controls.OfType<CheckBox>();

            // Count how many checkboxes are checked
            int checkedCount = checkboxes.Count(cb => cb.Checked);

            // Disable unselected checkboxes if 2 are already checked
            if (checkedCount >= 2)
            {
                foreach (var checkbox in checkboxes)
                {
                    if (!checkbox.Checked)
                        checkbox.Enabled = false;
                }
            }
            else
            {
                // Re-enable all checkboxes if less than 2 are checked
                foreach (var checkbox in checkboxes)
                {
                    checkbox.Enabled = true;
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Blocks non-numeric input
            }
        }

        private void button1_Click(object sender, EventArgs e) //Go back Button
        {
            this.Hide(); 
            previousForm.Show();
        }

        private void CharacterSheet_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
