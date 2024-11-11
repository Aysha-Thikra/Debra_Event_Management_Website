using System;

namespace Debra.PartnersDashboard
{
    public partial class PartnersDashboard1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            else
            {
                string loggedInUserID = Session["userID"].ToString();

                string loggedInUsername = Session["username"]?.ToString();
            }
        }
    }
}
