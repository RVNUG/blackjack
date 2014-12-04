using System;
using System.Web.UI;

namespace BlackJack_RVNUG
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("Index.html");
        }
    }
}