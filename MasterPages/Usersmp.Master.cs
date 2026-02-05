using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyWebApp.MasterPages
{
    public partial class Usersmp : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["userId"] != null)
            {
                string username = (string)Session["username"] ?? "Guest";

                profilename.InnerText = username;
            }
            else
            {
                Response.Redirect("~/Pages/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            

        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon(); 

            Response.Redirect("~/Pages/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();


        }
    }
}