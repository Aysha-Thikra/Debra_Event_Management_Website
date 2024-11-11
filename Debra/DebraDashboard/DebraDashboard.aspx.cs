using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Debra.DebraDashboard
{
    public partial class DebraDashboard1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboardData();
            }
        }

        private void LoadDashboardData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            int totalEvents = GetTotalCount("SELECT COUNT(*) FROM events");

            int ticketsSold = GetTotalCount("SELECT SUM(TicketQuantity) FROM customers");

            decimal totalEarnings = GetTotalEarnings();

            UpdateDashboardCards(totalEvents, ticketsSold, totalEarnings);
        }

        private int GetTotalCount(string query)
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    count = result != DBNull.Value ? Convert.ToInt32(result) : 0; 
                }
            }
            return count;
        }

        private decimal GetTotalEarnings()
        {
            decimal totalEarnings = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString))
            {
                string query = "SELECT SUM(PerTicketPrice * TicketQuantity) FROM customers"; 
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value) 
                    {
                        totalEarnings = Convert.ToDecimal(result); 
                    }
                }
            }
            return totalEarnings;
        }

        private void UpdateDashboardCards(int totalEvents, int ticketsSold, decimal totalEarnings)
        {
            if (TotalEventsLabel != null)
            {
                TotalEventsLabel.Text = totalEvents.ToString();
            }
            else
            {
                Console.WriteLine("TotalEventsLabel is null");
            }

            if (TicketsSoldLabel != null)
            {
                TicketsSoldLabel.Text = ticketsSold.ToString();
            }
            else
            {
                Console.WriteLine("TicketsSoldLabel is null");
            }

            if (TotalEarningsLabel != null)
            {
                TotalEarningsLabel.Text = totalEarnings.ToString("C"); 
            }
            else
            {
                Console.WriteLine("TotalEarningsLabel is null");
            }
        }
    }
}
