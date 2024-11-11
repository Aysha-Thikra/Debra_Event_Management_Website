using System;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI;

namespace Debra.GuestPage
{
    public partial class _event : Page
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

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT e.EventID, e.EventName, e.EventDate, e.Location, e.EventPoster, t.Price
                    FROM events e
                    JOIN tickets t ON e.EventID = t.EventID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            // Check if the event poster file exists
                            string eventPoster = reader["EventPoster"].ToString();
                            string posterPath = Server.MapPath($"~/images/events/{eventPoster}");
                            string defaultPosterPath = Server.MapPath("~/images/events/default.png");
                            string imageUrl;

                            // Assign the image URL based on the existence of the file
                            if (File.Exists(posterPath))
                            {
                                imageUrl = $"../images/events/{eventPoster}";
                            }
                            else
                            {
                                imageUrl = "../images/events/default.png"; 
                            }

                            // Create an event card for each event
                            string eventCard = $@"
                                <div class='event-card'>
                                    <img src='{imageUrl}' alt='Event Image' class='event-image'>
                                    <div class='event-details'>
                                        <h3 class='event-name'>{reader["EventName"]}</h3>
                                        <p class='event-date-time'>
                                            <i class='fas fa-calendar-alt'></i>&nbsp {Convert.ToDateTime(reader["EventDate"]).ToString("MMMM dd, yyyy")}
                                        </p>
                                        <p class='event-location'>
                                            <i class='fas fa-map-marker-alt'></i>&nbsp; {reader["Location"]}
                                        </p>
                                        <h4 class='event-price'>Price: <span class='highlight-price'>&nbsp; RS. {reader["Price"]}</span></h4>
                                        <a href='../buy-ticket.aspx?eventId={reader["EventID"]}' class='buy-ticket-btn'>Buy Ticket</a>
                                    </div>
                                </div>";

                            // Add the event card to the events section
                            eventsPlaceholder.Controls.Add(new LiteralControl(eventCard));
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
                    }
                }
            }
        }
    }
}
