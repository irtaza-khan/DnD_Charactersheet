using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.IO;


namespace DnD_Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string username = "irtazakhan";
            //string password = "MySecurePassword123";  // ✅ Password that will match
            //string salt = GenerateSalt();
            //string hashedPassword = HashPassword(password, salt);
            //string role = "User";  // Example role

            //// Insert into SQL
            //string connectionString = File.ReadAllText("D:\\Study Material\\Fiverr\\DnD_Charactersheet\\DnD_Project\\db_connection.txt").Trim();
            //string query = "INSERT INTO Users (Username, PasswordHash, Salt, Role) VALUES (@username, @passwordHash, @salt, @role)";

            //using (SqlConnection conn = new SqlConnection(connectionString))
            //using (SqlCommand cmd = new SqlCommand(query, conn))
            //{
            //    cmd.Parameters.AddWithValue("@username", username);
            //    cmd.Parameters.AddWithValue("@passwordHash", hashedPassword);
            //    cmd.Parameters.AddWithValue("@salt", salt);
            //    cmd.Parameters.AddWithValue("@role", role);
            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //}

            //Console.WriteLine("✅ User inserted successfully!");

            SetProcessDPIAware();  // Enables High DPI Scaling  
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SignUpForm());
        }
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        // Generate a random salt
        //public static string GenerateSalt()
        //{
        //    byte[] saltBytes = new byte[16];
        //    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(saltBytes);
        //    }
        //    return Convert.ToBase64String(saltBytes);
        //}

        //// Hash password using SHA-256 + Salt
        //public static string HashPassword(string password, string salt)
        //{
        //    using (SHA256 sha256 = SHA256.Create())
        //    {
        //        byte[] inputBytes = Encoding.UTF8.GetBytes(password + salt);
        //        byte[] hashBytes = sha256.ComputeHash(inputBytes);
        //        return Convert.ToBase64String(hashBytes);
        //    }
        //}
    }
}
