using System;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace Debra.PartnersDashboard
{
    public partial class add_events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void SubmitEvent_Click(object sender, EventArgs e)
        {
            string eventNameInput = eventName.Text;
            string eventDateInput = eventDate.Text;
            string eventLocationInput = eventLocation.SelectedValue;
            string eventDescriptionInput = eventDescription.Text;
            string eventCategoryInput = eventCategory.SelectedValue;
            string eventPosterPath = string.Empty;

            if (IsDateTaken(eventDateInput))
            {
                errorMessageLabel.Text = "An event is already scheduled for this date. Please choose a different date.";
                errorMessageLabel.Visible = true; 
                return; 
            }

            if (eventPoster.HasFile)
            {
                string fileName = Path.GetFileName(eventPoster.FileName);
                eventPosterPath = "~/images/events/" + fileName;

                string physicalPath = @"C:\Users\h\Desktop\SOC\Assignment\Debra\Debra\images\events\" + fileName;

                eventPoster.SaveAs(physicalPath);
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO events (EventID, UserID, EventName, EventDate, Location, Description, EventPoster, EventCategory, Status) " +
                               "VALUES (@EventID, @UserID, @EventName, @EventDate, @Location, @Description, @EventPoster, @EventCategory, @Status)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EventID", GenerateEventID()); 
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                    cmd.Parameters.AddWithValue("@EventName", eventNameInput);
                    cmd.Parameters.AddWithValue("@EventDate", eventDateInput);
                    cmd.Parameters.AddWithValue("@Location", eventLocationInput);
                    cmd.Parameters.AddWithValue("@Description", eventDescriptionInput);
                    cmd.Parameters.AddWithValue("@EventPoster", eventPosterPath); 
                    cmd.Parameters.AddWithValue("@EventCategory", eventCategoryInput);
                    cmd.Parameters.AddWithValue("@Status", "Pending"); 

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            Response.Redirect("events.aspx");
        }

        private bool IsDateTaken(string eventDate)
        {
            bool isTaken = false;
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM events WHERE EventDate = @EventDate";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EventDate", eventDate);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    isTaken = count > 0; 
                }
            }

            return isTaken;
        }

        private string GenerateEventID()
        {
            string newEventID = string.Empty;
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(CAST(SUBSTRING(EventID, 2, LEN(EventID)) AS INT)) FROM events";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    int lastEventID = result != DBNull.Value ? (int)result : 0;
                    newEventID = "E" + (lastEventID + 1).ToString("D11"); 
                }
            }

            return newEventID;
        }
    }
}
