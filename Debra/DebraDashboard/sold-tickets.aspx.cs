using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Debra.DebraDashboard
{
    public partial class current_tickets : Page
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

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT EventID, EventName, CustomerName, CustomerEmail, CustomerContact, CardNumber, PerTicketPrice, TicketQuantity, TotalPrice, PurchaseDate FROM customers";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        rptCurrentTickets.DataSource = dt;
                        rptCurrentTickets.DataBind();
                    }
                }
            }
        }
    }
}
