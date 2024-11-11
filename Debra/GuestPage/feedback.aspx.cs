using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Debra.GuestPage
{
    public partial class feedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the form is submitted
            if (IsPostBack)
            {
                string name = Request.Form["name"];
                string email = Request.Form["email"];
                string feedbackMessage = Request.Form["feedback"];
                string feedbackId = GenerateFeedbackId();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(feedbackMessage))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Feedback (FeedbackID, Name, Email, Feedback) VALUES (@FeedbackID, @Name, @Email, @Feedback)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@FeedbackID", feedbackId);
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@Feedback", feedbackMessage);

                            try
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();

                                // Redirect to review page after successful submission
                                Response.Redirect("~/GuestPage/review.aspx");
                            }
                            catch (Exception ex)
                            {
                                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                            }
                        }
                    }
                }
            }
        }

        private string GenerateFeedbackId()
        {
            string feedbackId = string.Empty;

            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(FeedbackID, 2, LEN(FeedbackID) - 1) AS INT)), 0) FROM Feedback";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        object result = cmd.ExecuteScalar();
                        int nextId = (int)result + 1;
                        feedbackId = "F" + nextId.ToString("D11"); // Generate ID in format F00000000001
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error generating FeedbackID: " + ex.Message + "');</script>");
                    }
                }
            }

            return feedbackId;
        }
    }
}
