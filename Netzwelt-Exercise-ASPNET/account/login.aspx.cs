using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Netzwelt_Exercise_ASPNET.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Web.Security;

namespace Netzwelt_Exercise_ASPNET.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/home/index.aspx");
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                //Set an object using the inputted data from the username and password fields
                object jsonObject = new
                {
                    username = Username.Text,
                    password = Password.Text
                };

                //Convert the object to a byte content
                var myContent = JsonConvert.SerializeObject(jsonObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using (var client = new HttpClient())
                {
                    var result = client.PostAsync("https://netzwelt-devtest.azurewebsites.net/Account/SignIn", byteContent).Result;
                    //Verify if the username and password is valid
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //Convert the JSON data to JSON Object
                        JObject resultContent = JObject.Parse(result.Content.ReadAsStringAsync().Result);
                        //Get the username field from the JSON Object
                        string username = (string)resultContent["username"];
                        //Get the displayName field from the JSON Object
                        string displayName = (string)resultContent["displayName"];

                        //Redirect to home page
                        FormsAuthentication.RedirectFromLoginPage(username, true);
                    }
                    else
                    {
                        //Get and display the returned message from the API
                        JObject resultContent = JObject.Parse(result.Content.ReadAsStringAsync().Result);
                        FailureText.Text = (string)resultContent["message"];
                        FailureText.Visible = true;
                    }

                }
            }
        }

        public class LoginDetails
        {
            public string username { get; set; }
            public string displayName { get; set; }
            public string roles { get; set; }
        }
    }
}