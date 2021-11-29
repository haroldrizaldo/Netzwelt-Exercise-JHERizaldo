using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Netzwelt_Exercise_ASPNET.home
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!this.Page.User.Identity.IsAuthenticated)
                {
                    //If the user is not authenticated, redirects the site to the Login page
                    FormsAuthentication.RedirectToLoginPage();
                }
                else
                {
                    //Displays the Treeview of Territories data from the API
                    LoadTerritories();
                }
            }
        }

        protected void LoadTerritories()
        {
            using (var client = new HttpClient())
            {
                //GET the JSON data from the URI
                var result = client.GetAsync("https://netzwelt-devtest.azurewebsites.net/Territories/All").Result;
                //Check if the result is OK
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Convert the JSON data to JSON Object
                    JObject resultContent = JObject.Parse(result.Content.ReadAsStringAsync().Result);
                    //Convert the JSON Object to a defined List
                    var territories = resultContent["data"].ToObject<List<Territories>>();

                    //Get the list of Root Nodes from the complete list
                    var rootTerritories = territories.Where(x => x.parent == null).ToList();
                    for (int i = 0; i < rootTerritories.Count; i++)
                    {
                        //Set the Treeview of Root Nodes
                        TreeNode headnode = new TreeNode();
                        headnode.Text = rootTerritories[i].name.ToString();
                        TreeViewTerritories.Nodes.Add(headnode);
                        headnode.Collapse();

                        //Get the list of First Child Nodes from the Root Nodes
                        var firstChildTerritories = territories.Where(x => x.parent == rootTerritories[i].id).ToList();
                        for (int ii = 0; ii < firstChildTerritories.Count; ii++)
                        {
                            //Set the Treeview of First Child Nodes
                            TreeNode firstnode = new TreeNode();
                            firstnode.Text = firstChildTerritories[ii].name.ToString();
                            firstnode.Collapse();
                            headnode.ChildNodes.Add(firstnode);

                            //Get the list of Second Child Nodes from the First Child Nodes
                            var secondChildTerritories = territories.Where(x => x.parent == firstChildTerritories[ii].id).ToList();
                            for (int iii = 0; iii < secondChildTerritories.Count; iii++)
                            {
                                //Set the Treeview of Second Child Nodes
                                TreeNode secondnode = new TreeNode();
                                secondnode.Text = secondChildTerritories[iii].name.ToString();
                                firstnode.ChildNodes.Add(secondnode);

                                //Get the list of Third Child Nodes from the Second Child Nodes - not applicable for this case
                                //var thirdChildTerritories = territories.Where(x => x.parent == secondChildTerritories[iii].id).ToList();
                                //for (int iiii = 0; iiii < secondChildTerritories.Count; iiii++)
                                //{
                                //    //Set the Treeview of Third Child Nodes
                                //    TreeNode thirdnode = new TreeNode();
                                //    thirdnode.Text = secondChildTerritories[iiii].name.ToString();
                                //    secondnode.ChildNodes.Add(thirdnode);
                                //}
                            }
                        }
                    }
                }
            }
        }

        public class Territories
        {
            public string id { get; set; }
            public string name { get; set; }
            public string parent { get; set; }
        }

    }
}