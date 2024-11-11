using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Debra
{
    public partial class signup1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void SubmitForm(object sender, EventArgs e)
        {
            try
            {
                // Capture form data
                string firstName = FirstName.Text;
                string lastName = LastName.Text;
                string companyName = CompanyName.Text;  
                string email = Email.Text;
                string contactNumber = ContactNumber.Text;
                string address = Address.Text;
                string username = Username.Text;

                // Validate if the username is already taken
                if (IsUsernameTaken(username))
                {
                    ErrorMessage.Text = "<div class='error'>The username is already taken. Please choose another one.</div>";
                    return; 
                }

                // Hash the password before storing it
                string password = HashPassword(Password.Text);

                string userId = GenerateUserID();

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

                // SQL query to insert data into the users table
                string query = "INSERT INTO users (userID, FirstName, LastName, CompanyName, ContactNumber, Email, Address, Username, Password, UserLevel) " +
                               "VALUES (@userID, @FirstName, @LastName, @CompanyName, @ContactNumber, @Email, @Address, @Username, @Password, @UserLevel)";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@userID", userId);
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@CompanyName", companyName);
                        cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@UserLevel", 2); 

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                Response.Redirect("login.aspx");
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = $"<div class='error'>An error occurred: {ex.Message}</div>";
            }
        }

        // Check if the username already exists in the database
        private bool IsUsernameTaken(string username)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM users WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; 
                }
            }
        }

        private string GenerateUserID()
        {
            int userIdNumber;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(userID, 2, LEN(userID) - 1) AS INT)), 0) FROM users";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    userIdNumber = (int)cmd.ExecuteScalar() + 1; 
                }
                con.Close();
            }

            return $"U{userIdNumber:D11}"; 
        }

        // Hashing the password using SHA-256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));  
                }
                return builder.ToString();
            }
        }
    }
}
