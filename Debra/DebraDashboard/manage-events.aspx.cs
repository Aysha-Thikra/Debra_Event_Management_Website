using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Debra.DebraDashboard
{
    public partial class manage_events : Page
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

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT EventID, EventName, EventDate, Location, Description, EventPoster, EventCategory, Status FROM events WHERE Status IN ('Pending', 'Approved', 'Rejected')";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();


                        eventCardGrid.Controls.Clear();

                        while (reader.Read())
                        {

                            var eventCard = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            eventCard.Attributes.Add("class", "event-card");


                            var cardHeader = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            cardHeader.Attributes.Add("class", "card-header");
                            cardHeader.InnerHtml = $"<h3>Event ID: {reader["EventID"]}</h3><h4>{reader["EventName"]}</h4>";

                            var cardBody = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            cardBody.Attributes.Add("class", "card-body");

                            string posterFileName = reader["EventPoster"].ToString();
                            string posterPath = $"../images/events/{posterFileName}";

                            if (!System.IO.File.Exists(Server.MapPath(posterPath)))
                            {
                                posterPath = "../images/events/default.png"; 
                            }

                            cardBody.InnerHtml = $@"
                                <div class='poster-container'>
                                    <img src='{posterPath}' alt='Event Poster' class='event-poster'>
                                </div>
                                <p><strong>Date:</strong> {Convert.ToDateTime(reader["EventDate"]).ToString("MMMM dd, yyyy")}</p>
                                <p><strong>Location:</strong> {reader["Location"]}</p>
                                <p><strong>Description:</strong> {reader["Description"]}</p>
                                <p><strong>Category:</strong> {reader["EventCategory"]}</p>
                                <p><strong>Status:</strong> {reader["Status"]}</p>";

                            var cardFooter = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            cardFooter.Attributes.Add("class", "card-footer");

                            // Accept button
                            var acceptButton = new System.Web.UI.WebControls.Button
                            {
                                Text = "Accept",
                                CommandArgument = reader["EventID"].ToString(),
                                CssClass = "accept-btn"
                            };
                            acceptButton.Click += AcceptButton_Click;

                            // Reject button
                            var rejectButton = new System.Web.UI.WebControls.Button
                            {
                                Text = "Reject",
                                CommandArgument = reader["EventID"].ToString(),
                                CssClass = "reject-btn"
                            };
                            rejectButton.Click += RejectButton_Click;

                            cardFooter.Controls.Add(acceptButton);
                            cardFooter.Controls.Add(rejectButton);

                            eventCard.Controls.Add(cardHeader);
                            eventCard.Controls.Add(cardBody);
                            eventCard.Controls.Add(cardFooter);

                            eventCardGrid.Controls.Add(eventCard);
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error loading events: {ex.Message}");
            }
        }

        protected void AcceptButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string eventId = button.CommandArgument;
            UpdateEventStatus(eventId, "Approved");
        }

        protected void RejectButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string eventId = button.CommandArgument;
            UpdateEventStatus(eventId, "Rejected");
        }

        private void UpdateEventStatus(string eventId, string status)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE events SET Status = @Status WHERE EventID = @EventID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@EventID", eventId);
                        con.Open();

                        Response.Write($"Executing SQL: {query} with EventID: {eventId} and Status: {status}<br/>");

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Response.Write("No rows were updated. Please check the Event ID.<br/>");
                        }
                        else
                        {
                            Response.Write($"{rowsAffected} row(s) updated successfully.<br/>");
                        }
                    }
                }

                LoadEvents();
            }
            catch (Exception ex)
            {
                Response.Write($"Error updating status: {ex.Message}<br/>");
            }
        }
    }
}
