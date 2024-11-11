using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace Debra
{
    public partial class login1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LoginBtn(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Username and Password are required.');", true);
                return;
            }

            // Define your connection string (update the name if necessary)
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            // Update query to select Password, UserLevel, and UserID from the users table
            string query = "SELECT UserID, Password, UserLevel FROM users WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string storedPassword = reader["Password"].ToString();
                        // Hash the input password to compare with the stored hash
                        string hashedInputPassword = HashPassword(password);

                        if (storedPassword.Equals(hashedInputPassword, StringComparison.OrdinalIgnoreCase))
                        {
                            // Successful login
                            Session["username"] = username;

                            // Get UserID and store it in the session
                            Session["userID"] = reader["UserID"].ToString();

                            // Get user level
                            int userLevel = Convert.ToInt32(reader["UserLevel"]);

                            if (userLevel == 1)
                            {
                                Response.Redirect("DebraDashboard/DebraDashboard.aspx");
                            }
                            else if (userLevel == 2)
                            {
                                Response.Redirect("PartnersDashboard/PartnersDashboard.aspx");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Incorrect Password.');", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Username not found.');", true);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
                }
            }
        }

        // Hashing the password using SHA-256 (same method as in signup)
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
