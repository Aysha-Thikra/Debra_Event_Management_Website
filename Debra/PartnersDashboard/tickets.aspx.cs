using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Debra.PartnersDashboard
{
    public partial class tickets : Page
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

            string userId = Session["UserID"]?.ToString();

            if (string.IsNullOrEmpty(userId))
            {
                lblMessage.Text = "You must be logged in to view your tickets.";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT t.TicketID, t.EventID, e.EventName, t.Price, t.Quantity, t.Status 
                        FROM tickets t
                        INNER JOIN events e ON t.EventID = e.EventID
                        WHERE e.UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId); 
                        con.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                rptTickets.DataSource = dt; 
                                rptTickets.DataBind();
                                lblMessage.Text = ""; 
                            }
                            else
                            {
                                lblMessage.Text = "No tickets found &#128555;"; 
                                rptTickets.DataSource = null; 
                                rptTickets.DataBind(); 
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                lblMessage.Text = "An error occurred while retrieving your tickets: " + ex.Message; 
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An unexpected error occurred: " + ex.Message; 
            }
        }
    }
}
