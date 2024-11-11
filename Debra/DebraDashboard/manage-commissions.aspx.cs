using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Debra.DebraDashboard
{
    public partial class manage_commissions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCommissions();
            }
        }

        private void LoadCommissions()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;

            string query = @"
                SELECT c.CommissionID, 
                       CONCAT(u.FirstName, ' ', u.LastName) AS PartnerName,
                       e.EventName,
                       c.CommissionRate,
                       c.Amount AS CommissionAmount
                FROM commissions c
                JOIN tickets t ON c.TicketID = t.TicketID
                JOIN events e ON t.EventID = e.EventID
                JOIN users u ON e.UserID = u.UserID"; 

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();  

                        SqlDataAdapter da = new SqlDataAdapter(cmd);  
                        DataTable dt = new DataTable();  
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            string tableData = "";

                            foreach (DataRow row in dt.Rows)
                            {
                                tableData += "<tr>";
                                tableData += $"<td>{row["CommissionID"]}</td>";
                                tableData += $"<td>{row["PartnerName"]}</td>";
                                tableData += $"<td>{row["EventName"]}</td>";
                                tableData += $"<td>{row["CommissionRate"]}%</td>";
                                tableData += $"<td>RS.{row["CommissionAmount"]}</td>";
                                tableData += "</tr>";
                            }

                            commissionTableBody.Text = tableData; 
                        }
                        else
                        {
                            commissionTableBody.Text = "<tr><td colspan='5'>No commissions found.</td></tr>";
                        }
                    }
                    catch (Exception ex)
                    {
                        commissionTableBody.Text = $"<tr><td colspan='5'>Error: {ex.Message}</td></tr>";
                    }
                }
            }
        }
    }
}
