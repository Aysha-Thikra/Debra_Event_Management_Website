using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace Debra.GuestPage
{
    public partial class review : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFeedback();
            }
        }

        private void LoadFeedback()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Name, Email, Feedback, SubmissionDate FROM Feedback ORDER BY SubmissionDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string name = reader["Name"].ToString();
                            string feedback = reader["Feedback"].ToString();
                            DateTime submissionDate = Convert.ToDateTime(reader["SubmissionDate"]);

                            // Create feedback card
                            string feedbackCard = $@"
                            <div class='feedback-card'>
                                <div class='card-header'>
                                    <img src='../images/user.png' alt='User Profile' class='profile-img'>
                                    <h4>{name}</h4>
                                </div>
                                <div class='stars'>
                                    <i class='fas fa-star'></i>
                                    <i class='fas fa-star'></i>
                                    <i class='fas fa-star'></i>
                                    <i class='fas fa-star'></i>
                                    <i class='fas fa-star-half-alt'></i>
                                </div>
                                <p>{feedback}</p>
                                <div class='card-footer'>Posted on: {submissionDate.ToString("MMMM dd, yyyy")}</div>
                            </div>";

                            // Add feedback card to placeholder
                            FeedbackPlaceHolder.Controls.Add(new LiteralControl(feedbackCard));
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle error (optional)
                        Response.Write("<script>alert('Error loading feedback: " + ex.Message + "');</script>");
                    }
                }
            }
        }
    }
}
