using System;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace Debra
{
    public partial class buy_ticket1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string eventId = Request.QueryString["eventId"];
                if (!string.IsNullOrEmpty(eventId))
                {
                    LoadEventDetails(eventId);
                }
                else
                {
                    Response.Write("<script>alert('Invalid Event');</script>");
                }
            }
        }

        private void LoadEventDetails(string eventId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT e.EventName, e.EventDate, e.Location, e.Description, e.EventPoster, e.EventCategory, t.Price
                                 FROM events e
                                 JOIN tickets t ON e.EventID = t.EventID
                                 WHERE e.EventID = @EventID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EventID", eventId);

                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string posterFileName = reader["EventPoster"].ToString();
                            string posterFilePath = Server.MapPath("~/images/events/" + posterFileName);

                            // Check if the poster file exists
                            if (File.Exists(posterFilePath))
                            {
                                eventPoster.Src = "~/images/events/" + posterFileName;
                            }
                            else
                            {
                                eventPoster.Src = "~/images/events/default.png"; // Fallback image
                            }

                            lblEventID.Text = eventId;
                            lblEventName.Text = reader["EventName"].ToString();
                            lblEventDate.Text = Convert.ToDateTime(reader["EventDate"]).ToString("MMMM dd, yyyy");
                            lblLocation.Text = reader["Location"].ToString();
                            lblCategory.Text = reader["EventCategory"].ToString();
                            lblPrice.Text = Convert.ToDecimal(reader["Price"]).ToString("F2");
                        }
                        else
                        {
                            Response.Write("<script>alert('Event not found');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
                    }
                }
            }
        }

        // Confirm Purchase Button Click Event
        protected void ConfirmPurchase_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(customerNameTextBox.Value) &&
                !string.IsNullOrEmpty(customerEmailTextBox.Value) &&
                !string.IsNullOrEmpty(customerContactTextBox.Value) &&
                !string.IsNullOrEmpty(cardNumberTextBox.Value) &&
                !string.IsNullOrEmpty(ticketQuantityTextBox.Value))
            {
                // Get data from controls
                string eventId = lblEventID.Text; // ASP Label, so use .Text
                string eventName = lblEventName.Text; // ASP Label
                string customerName = customerNameTextBox.Value; // Input field, use .Value
                string customerEmail = customerEmailTextBox.Value;
                string customerContact = customerContactTextBox.Value;
                string cardNumber = cardNumberTextBox.Value;
                decimal pricePerTicket = Convert.ToDecimal(lblPrice.Text); // ASP Label
                int ticketQuantity = Convert.ToInt32(ticketQuantityTextBox.Value); // Input field, use .Value
                decimal totalPrice = pricePerTicket * ticketQuantity;

                // Save purchase details to the database
                SavePurchaseDetails(eventId, eventName, customerName, customerEmail, customerContact, cardNumber, pricePerTicket, ticketQuantity, totalPrice);
            }
            else
            {
                Response.Write("<script>alert('Some fields are missing or not initialized properly.');</script>");
            }
        }

        private void SavePurchaseDetails(string eventId, string eventName, string customerName, string customerEmail, string customerContact, string cardNumber, decimal pricePerTicket, int ticketQuantity, decimal totalPrice)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string customerID = GenerateCustomerID(); // Call a method to generate a unique CustomerID
                string saleID = GenerateSaleID(); // Generate unique SaleID
                string ticketID = GenerateTicketID(); // Generate unique TicketID

                string query = @"
                    INSERT INTO customers 
                    (CustomerID, EventID, EventName, CustomerName, CustomerEmail, CustomerContact, CardNumber, PerTicketPrice, TicketQuantity, TotalPrice, PurchaseDate) 
                    VALUES 
                    (@CustomerID, @EventID, @EventName, @CustomerName, @CustomerEmail, @CustomerContact, @CardNumber, @PerTicketPrice, @TicketQuantity, @TotalPrice, GETDATE());

                    INSERT INTO Sales (SaleID, TicketID, EventID, CustomerID, Quantity, TotalPrice, SaleDate) 
                    VALUES 
                    (@SaleID, @TicketID, @EventID, @CustomerID, @TicketQuantity, @TotalPrice, GETDATE());

                    INSERT INTO Earnings (EarningID, EventID, TicketID, TotalEarnings, Commission, EarningDate) 
                    VALUES 
                    (@EarningID, @EventID, @TicketID, @TotalPrice, @Commission, GETDATE());";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);
                    cmd.Parameters.AddWithValue("@EventID", eventId);
                    cmd.Parameters.AddWithValue("@EventName", eventName);
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@CustomerEmail", customerEmail);
                    cmd.Parameters.AddWithValue("@CustomerContact", customerContact);
                    cmd.Parameters.AddWithValue("@CardNumber", cardNumber);
                    cmd.Parameters.AddWithValue("@PerTicketPrice", pricePerTicket);
                    cmd.Parameters.AddWithValue("@TicketQuantity", ticketQuantity);
                    cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                    cmd.Parameters.AddWithValue("@SaleID", saleID); // Unique SaleID
                    cmd.Parameters.AddWithValue("@TicketID", ticketID); // Unique TicketID
                    cmd.Parameters.AddWithValue("@EarningID", GenerateEarningID()); // Unique EarningID
                    cmd.Parameters.AddWithValue("@Commission", totalPrice * 0.1m); // Assuming 10% commission

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('Purchase successful');</script>");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error occurred: " + ex.Message + "');</script>");
                    }
                }
            }
        }

        private string GenerateCustomerID()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;
            string newCustomerID = string.Empty;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT MAX(CustomerID) FROM customers WHERE CustomerID LIKE 'C%'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            string maxCustomerID = result.ToString();
                            long numericPart = Convert.ToInt64(maxCustomerID.Substring(1)); // Remove 'C'
                            numericPart++; // Increment the numeric part
                            newCustomerID = "C" + numericPart.ToString("D11"); // Format to 11 digits
                        }
                        else
                        {
                            newCustomerID = "C00000000001"; // Starting CustomerID
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error generating CustomerID: " + ex.Message + "');</script>");
                    }
                }
            }

            return newCustomerID;
        }

        private string GenerateSaleID()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;
            string newSaleID = string.Empty;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT MAX(SaleID) FROM Sales WHERE SaleID LIKE 'S%'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            string maxSaleID = result.ToString();
                            long numericPart = Convert.ToInt64(maxSaleID.Substring(1)); // Remove 'S'
                            numericPart++; // Increment the numeric part
                            newSaleID = "S" + numericPart.ToString("D11"); // Format to 11 digits
                        }
                        else
                        {
                            newSaleID = "S00000000001"; // Starting SaleID
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error generating SaleID: " + ex.Message + "');</script>");
                    }
                }
            }

            return newSaleID;
        }

        private string GenerateTicketID()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;
            string newTicketID = string.Empty;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT MAX(TicketID) FROM tickets WHERE TicketID LIKE 'T%'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            string maxTicketID = result.ToString();
                            long numericPart = Convert.ToInt64(maxTicketID.Substring(1)); // Remove 'T'
                            numericPart++; // Increment the numeric part
                            newTicketID = "T" + numericPart.ToString("D11"); // Format to 11 digits
                        }
                        else
                        {
                            newTicketID = "T00000000001"; // Starting TicketID
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error generating TicketID: " + ex.Message + "');</script>");
                    }
                }
            }

            return newTicketID;
        }

        private string GenerateEarningID()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;
            string newEarningID = string.Empty;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT MAX(EarningID) FROM Earnings WHERE EarningID LIKE 'E%'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            string maxEarningID = result.ToString();
                            long numericPart = Convert.ToInt64(maxEarningID.Substring(1)); // Remove 'E'
                            numericPart++; // Increment the numeric part
                            newEarningID = "E" + numericPart.ToString("D11"); // Format to 11 digits
                        }
                        else
                        {
                            newEarningID = "E00000000001"; // Starting EarningID
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error generating EarningID: " + ex.Message + "');</script>");
                    }
                }
            }

            return newEarningID;
        }
    }
}
