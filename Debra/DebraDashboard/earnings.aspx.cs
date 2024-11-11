using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Debra.DebraDashboard
{
    public partial class earnings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEarnings();
            }
        }

        private void LoadEarnings()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;
            string query = @"
                SELECT EarningID, 
                       EventID, 
                       TicketID, 
                       TotalEarnings, 
                       Commission, 
                       EarningDate
                FROM Earnings";

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
                                tableData += $"<td>{row["EarningID"]}</td>";
                                tableData += $"<td>{row["EventID"]}</td>";
                                tableData += $"<td>{row["TicketID"]}</td>";
                                tableData += $"<td>RS {row["TotalEarnings"]}</td>";
                                tableData += $"<td>RS {row["Commission"]}</td>";
                                tableData += $"<td>{Convert.ToDateTime(row["EarningDate"]).ToString("yyyy-MM-dd HH:mm")}</td>";
                                tableData += "</tr>";
                            }
                            earningsTableBody.Text = tableData; 
                        }
                        else
                        {
                            earningsTableBody.Text = "<tr><td colspan='6'>No earnings found.</td></tr>";
                        }
                    }
                    catch (Exception ex)
                    {
                        earningsTableBody.Text = $"<tr><td colspan='6'>Error: {ex.Message}</td></tr>";
                    }
                }
            }
        }
    }
}
