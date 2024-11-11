using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Debra.DebraDashboard
{
    public partial class manage_sales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSales();
            }
        }

        private void LoadSales()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;
            string query = @"
                SELECT s.SaleID, 
                       s.TicketID,
                       s.EventID,
                       c.CustomerID,
                       c.CustomerName,
                       s.Quantity,
                       s.TotalPrice,
                       s.SaleDate
                FROM Sales s
                JOIN customers c ON s.CustomerID = c.CustomerID"; 

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
                                tableData += $"<td>{row["SaleID"]}</td>";
                                tableData += $"<td>{row["TicketID"]}</td>";
                                tableData += $"<td>{row["EventID"]}</td>";
                                tableData += $"<td>{row["CustomerID"]}</td>";
                                tableData += $"<td>{row["CustomerName"]}</td>";
                                tableData += $"<td>{row["Quantity"]}</td>";
                                tableData += $"<td>RS {row["TotalPrice"]}</td>"; 
                                tableData += $"<td>{Convert.ToDateTime(row["SaleDate"]).ToString("yyyy-MM-dd HH:mm")}</td>";
                                tableData += "</tr>";
                            }
                            salesTableBody.InnerHtml = tableData; 
                        }
                        else
                        {
                            salesTableBody.InnerHtml = "<tr><td colspan='8'>No sales found.</td></tr>";
                        }
                    }
                    catch (Exception ex)
                    {
                        salesTableBody.InnerHtml = $"<tr><td colspan='8'>Error: {ex.Message}</td></tr>";
                    }
                }
            }
        }
    }
}
