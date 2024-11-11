using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class add_tickets : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEventDropdown();
            }
        }

        private void LoadEventDropdown()
        {
            string userID = Session["UserID"]?.ToString(); 

            if (string.IsNullOrEmpty(userID))
            {
                lblMessage.Text = "User not logged in.";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT EventID, EventName FROM events WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlEvent.DataSource = reader;
                    ddlEvent.DataTextField = "EventName";
                    ddlEvent.DataValueField = "EventID";
                    ddlEvent.DataBind();
                }
            }
            ddlEvent.Items.Insert(0, new ListItem("-- Select Event --", ""));
        }

        protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEventName.Text = ddlEvent.SelectedItem.Text;
        }

        protected void btnAddTicket_Click(object sender, EventArgs e)
        {
            string eventID = ddlEvent.SelectedValue;

            if (TicketExists(eventID))
            {
                lblMessage.Text = "A ticket has already been created for this event.";
                lblMessage.ForeColor = System.Drawing.Color.Red; 
                return;
            }

            string ticketID = GenerateTicketID(); 
            string eventName = txtEventName.Text;
            decimal price;
            int quantity;

            if (!decimal.TryParse(txtPrice.Text, out price) || !int.TryParse(txtQuantity.Text, out quantity))
            {
                lblMessage.Text = "Please enter valid price and quantity.";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        string query = "INSERT INTO tickets (TicketID, EventID, EventName, Price, Quantity, Status) " +
                                       "VALUES (@TicketID, @EventID, @EventName, @Price, @Quantity, @Status)";

                        using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@TicketID", ticketID);
                            cmd.Parameters.AddWithValue("@EventID", eventID);
                            cmd.Parameters.AddWithValue("@EventName", eventName);
                            cmd.Parameters.AddWithValue("@Price", price);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@Status", "Pending"); 

                            cmd.ExecuteNonQuery();
                        }

                        // Calculate commission
                        decimal commissionRate = 0.10m; 
                        decimal commissionAmount = price * commissionRate;

                        // Generate a new CommissionID
                        string commissionID = GenerateCommissionID();

                        string commissionQuery = "INSERT INTO commissions (CommissionID, TicketID, Amount, CommissionRate) " +
                                                  "VALUES (@CommissionID, @TicketID, @Amount, @CommissionRate)";

                        using (SqlCommand cmd = new SqlCommand(commissionQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@CommissionID", commissionID);
                            cmd.Parameters.AddWithValue("@TicketID", ticketID);
                            cmd.Parameters.AddWithValue("@Amount", commissionAmount);
                            cmd.Parameters.AddWithValue("@CommissionRate", commissionRate);

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            lblMessage.Text = "Ticket added successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green; 

            Response.Redirect("tickets.aspx"); 
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PartnersDashboard.aspx"); 
        }

        private bool TicketExists(string eventID)
        {
            bool exists = false;
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM tickets WHERE EventID = @EventID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EventID", eventID);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    exists = count > 0; 
                }
            }

            return exists;
        }

        private string GenerateTicketID()
        {
            string newTicketID = string.Empty;
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(CAST(SUBSTRING(TicketID, 2, LEN(TicketID)) AS INT)) FROM tickets";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    int lastTicketID = result != DBNull.Value ? (int)result : 0; 
                    newTicketID = "T" + (lastTicketID + 1).ToString("D11");
                }
            }

            return newTicketID;
        }

        private string GenerateCommissionID()
        {
            string newCommissionID = string.Empty;
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(CAST(SUBSTRING(CommissionID, 2, LEN(CommissionID)) AS INT)) FROM commissions";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    int lastCommissionID = result != DBNull.Value ? (int)result : 0; 
                    newCommissionID = "R" + (lastCommissionID + 1).ToString("D11"); 
                }
            }

            return newCommissionID;
        }
    }
}
