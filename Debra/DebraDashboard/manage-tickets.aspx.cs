using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Debra.DebraDashboard
{
    public partial class manage_tickets : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTickets();
            }
        }

        private void LoadTickets()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT t.TicketID, t.Price, t.Quantity, t.EventID, 
                               e.EventName, t.Status AS TicketStatus
                        FROM tickets t
                        INNER JOIN events e ON t.EventID = e.EventID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        ticketCardGrid.Controls.Clear();

                        while (reader.Read())
                        {
                            var ticketCard = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            ticketCard.Attributes.Add("class", "ticket-card");

                            var cardHeader = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            cardHeader.Attributes.Add("class", "card-header");
                            cardHeader.InnerHtml = $"<h3>Ticket ID: {reader["TicketID"]}</h3><h4>Price: RS.{reader["Price"]}</h4>";

                            var cardBody = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            cardBody.Attributes.Add("class", "card-body");
                            cardBody.InnerHtml = $@"
                                <p><strong>Quantity:</strong> {reader["Quantity"]}</p>
                                <p><strong>Event ID:</strong> {reader["EventID"]}</p>
                                <p><strong>Event Name:</strong> {reader["EventName"]}</p>
                                <p><strong>Ticket Status:</strong> {reader["TicketStatus"]}</p>";

                            var cardFooter = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            cardFooter.Attributes.Add("class", "card-footer");

                            var acceptButton = new System.Web.UI.WebControls.Button
                            {
                                Text = "Accept",
                                CommandArgument = reader["TicketID"].ToString(),
                                CssClass = "accept-btn"
                            };
                            acceptButton.Click += AcceptButton_Click;

                            var rejectButton = new System.Web.UI.WebControls.Button
                            {
                                Text = "Reject",
                                CommandArgument = reader["TicketID"].ToString(),
                                CssClass = "reject-btn"
                            };
                            rejectButton.Click += RejectButton_Click;

                            cardFooter.Controls.Add(acceptButton);
                            cardFooter.Controls.Add(rejectButton);

                            ticketCard.Controls.Add(cardHeader);
                            ticketCard.Controls.Add(cardBody);
                            ticketCard.Controls.Add(cardFooter);
                            ticketCardGrid.Controls.Add(ticketCard);
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error loading tickets: {ex.Message}");
            }
        }

        protected void AcceptButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string ticketId = button.CommandArgument;
            UpdateTicketStatus(ticketId, "Accepted");
        }

        protected void RejectButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string ticketId = button.CommandArgument;
            UpdateTicketStatus(ticketId, "Rejected");
        }

        private void UpdateTicketStatus(string ticketId, string newStatus)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE tickets SET Status = @Status WHERE TicketID = @TicketID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@TicketID", ticketId);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            LoadTickets(); 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error updating ticket status: {ex.Message}");
            }
        }
    }
}
