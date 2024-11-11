using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Debra.GuestPage
{
    public partial class contact_us : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // This block is only hit on normal postbacks
                return;
            }
            else if (Request.HttpMethod == "POST")
            {
                string name = Request.Form["name"];
                string email = Request.Form["email"];
                string message = Request.Form["message"];
                string contactId = GenerateContactId();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(message))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO ContactUs (ContactID, Name, Email, Message) VALUES (@ContactID, @Name, @Email, @Message)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@ContactID", contactId);
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@Message", message);

                            try
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                                Response.Write("Success");
                            }
                            catch (SqlException sqlEx)
                            {
                                Response.Write("SQL Error: " + sqlEx.Message);
                            }
                            catch (Exception ex)
                            {
                                Response.Write("Error: " + ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private string GenerateContactId()
        {
            string contactId = string.Empty;

            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(CONVERT(INT, SUBSTRING(ContactID, 2, LEN(ContactID)-1))) FROM ContactUs";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        object result = cmd.ExecuteScalar();
                        int nextId = result != DBNull.Value ? (int)result + 1 : 1;
                        contactId = "Q" + nextId.ToString("D11");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error generating ContactID: " + ex.Message);
                    }
                }
            }

            return contactId;
        }
    }
}
