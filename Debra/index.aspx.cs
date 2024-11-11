using Debra.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

namespace Debra
{
    public partial class index : Page
    {
        protected List<EventModel> events = new List<EventModel>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEvents();
            }
        }

        private void LoadEvents()
        {
            string connectionString = "Data Source=HP;Initial Catalog=DebraDB;Integrated Security=True";
            string query = @"
                SELECT e.EventID, e.EventName, e.EventPoster, e.EventDate, e.Location, t.Price 
                FROM events e
                LEFT JOIN tickets t ON e.EventID = t.EventID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EventModel ev = new EventModel
                    {
                        EventID = reader["EventID"].ToString(),
                        EventName = reader["EventName"].ToString(),
                        EventPoster = GetEventPosterPath(reader["EventPoster"].ToString()), 
                        EventDate = DateTime.Parse(reader["EventDate"].ToString()),
                        Location = reader["Location"].ToString(),
                        Price = reader["Price"] != DBNull.Value ? decimal.Parse(reader["Price"].ToString()) : 0m 
                    };
                    events.Add(ev);
                }
            }
        }

        private string GetEventPosterPath(string posterFileName)
        {
            string posterPath = Path.Combine(Server.MapPath("~/images/events/"), posterFileName);
            return File.Exists(posterPath) ? posterFileName : "default.png"; 
        }
    }
}
