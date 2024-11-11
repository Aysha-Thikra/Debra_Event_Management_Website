using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Debra.DebraDashboard
{
    public partial class manage_partners : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPartners();
            }
        }

        private void LoadPartners()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DebraConnection"].ConnectionString;
            string query = "SELECT userID, FirstName, LastName, ContactNumber, Email, Address, Username, companyName FROM users WHERE userLevel = 2";

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
                                tableData += "<div class='partner-card'>";
                                tableData += "<div class='card-header'>";
                                tableData += $"<h3>Partner ID: {row["userID"]}</h3>";
                                tableData += $"<h4>{row["FirstName"]} {row["LastName"]}</h4>";
                                tableData += "</div>";
                                tableData += "<div class='card-body'>";
                                tableData += $"<p><strong>Company Name:</strong> {row["companyName"]}</p>";
                                tableData += $"<p><strong>Contact Number:</strong> {row["ContactNumber"]}</p>";
                                tableData += $"<p><strong>Email:</strong> {row["Email"]}</p>";
                                tableData += $"<p><strong>Address:</strong> {row["Address"]}</p>";
                                tableData += $"<p><strong>Username:</strong> {row["Username"]}</p>";
                                tableData += "</div>";
                                tableData += "<div class='card-footer'>";
                                tableData += "<button class='delete-btn' style='background-color: red;'>Delete</button>";
                                tableData += "</div>";
                                tableData += "</div>";
                            }
                            partnersTableBody.Text = tableData; 
                        }
                        else
                        {
                            partnersTableBody.Text = "<div class='partner-card'><p>No partners found.</p></div>";
                        }
                    }
                    catch (Exception ex)
                    {
                        partnersTableBody.Text = $"<div class='partner-card'><p>Error: {ex.Message}</p></div>";
                    }
                }
            }
        }
    }
}
