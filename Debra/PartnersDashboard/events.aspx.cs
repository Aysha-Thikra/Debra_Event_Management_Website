using System;
using System.Data.SqlClient;
using System.Configuration;
using System.IO; 

namespace Debra.PartnersDashboard
{
    public partial class events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEvents(); 
            }
        }

        private void LoadEvents()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            string loggedInUserId = Session["UserID"]?.ToString(); 

            if (string.IsNullOrEmpty(loggedInUserId))
            {
                EventCardsPlaceholder.InnerHtml = "<p>You must be logged in to view your events.</p>";
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT EventID, EventName, EventDate, Location, Description, EventPoster, EventCategory, Status FROM events WHERE UserID = @UserID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", loggedInUserId);

                    con.Open(); 
                    SqlDataReader reader = cmd.ExecuteReader(); 

                    if (!reader.HasRows)
                    {
                        EventCardsPlaceholder.InnerHtml = "<h1 style='color: red;'>No events found &#128555;</h1>"; 
                        return; 
                    }

                    while (reader.Read())
                    {
                        string eventId = reader["EventID"].ToString();
                        string eventName = reader["EventName"].ToString();
                        string eventDate = Convert.ToDateTime(reader["EventDate"]).ToString("yyyy-MM-dd");
                        string eventLocation = reader["Location"].ToString();
                        string eventDescription = reader["Description"].ToString();
                        string eventCategory = reader["EventCategory"].ToString();
                        string eventStatus = reader["Status"].ToString();

                        string eventPosterFileName = reader["EventPoster"].ToString();

                        string physicalPosterPath = Server.MapPath("~/images/events/" + eventPosterFileName);
                        string eventPosterPath;

                        if (File.Exists(physicalPosterPath))
                        {
                            eventPosterPath = ResolveUrl("~/images/events/" + eventPosterFileName);
                        }
                        else
                        {
                            eventPosterPath = ResolveUrl("~/images/events/default.png"); 
                        }

                        EventCardsPlaceholder.InnerHtml += $@"
                            <div class='event-card'>
                                <h3>Event ID: {eventId}</h3>
                                <img src='{eventPosterPath}' alt='Event Poster' class='event-poster'>
                                <p><strong>Name:</strong> {eventName}</p>
                                <p><strong>Date:</strong> {eventDate}</p>
                                <p><strong>Location:</strong> {eventLocation}</p>
                                <p><strong>Description:</strong> {eventDescription}</p>
                                <p><strong>Category:</strong> {eventCategory}</p>
                                <p><strong>Status:</strong> {eventStatus}</p>
                                <button class='edit-btn'>Edit</button>
                                <button class='delete-btn'>Delete</button>
                            </div>";
                    }
                    con.Close(); 
                }
            }
        }
    }
}
