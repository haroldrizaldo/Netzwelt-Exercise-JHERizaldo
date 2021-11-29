using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace Netzwelt_Exercise_ASPNET
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                //Displays the Home Page & Log out link if the user is authenticated
                lnkHome.Visible = false;
                lnkLogOut.Visible = false;
            }
            else
            {
                //Hide the Home Page & Log out link if the user is not authenticated
                lnkHome.Visible = true;
                lnkLogOut.Visible = true;
            }
        }

        protected void lnkLogOut_ServerClick(object sender, System.EventArgs e)
        {
            //Sign Out the Form Authentication and clear the Cookies
            FormsAuthentication.SignOut();
            Session.Abandon();
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            //Redirect to the Login Page
            FormsAuthentication.RedirectToLoginPage();
        }
    }

}