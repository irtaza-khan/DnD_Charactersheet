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
    public partial class CharacterListForm : Form
    {
        private string connectionString = File.ReadAllText(@"db_connection.txt").Trim();
        private Form previousForm; // Store the reference to the previous form

        public CharacterListForm()
        {
            InitializeComponent();
            LoadCharacterList();
        }
        public CharacterListForm(Form prev)
        {
            InitializeComponent();
            LoadCharacterList();
            this.previousForm = prev;
        }

        private void LoadCharacterList()
        {
            string query = @"
            SELECT 
                c.Character_ID,
                c.Character_Name, 
                c.Class, 
                c.Race, 
                c.Level, 
                u.Username AS Creator 
            FROM Characters c
            JOIN Users u ON c.UserID = u.UserID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt; // Bind data to DataGridView
                }
            }

            // Adjust DataGridView properties for better display
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btngoback_Click(object sender, EventArgs e)
        {
            this.Hide();
            previousForm.Show();
        }
    }
}
