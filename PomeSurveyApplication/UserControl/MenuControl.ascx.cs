using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace PomeSurveyApplication.UserControl
{
    public partial class MenuControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] != null)
            {
                var str = new StringBuilder();
                str.Append("<ul class='list-unstyled'>");
                string link = string.Empty;
                string roll = string.Empty;
                string title = string.Empty;

                roll = Session["UserRole"].ToString();

                var xml = new XmlDocument();
                var path = Server.MapPath("~/XMLFile1.xml");
                xml.Load(path);

                XmlNodeList rssnodelist = xml.SelectNodes("MainMenu/" + roll + "");
                if (rssnodelist != null)
                {
                    foreach (XmlNode childnodes in rssnodelist)
                    {

                        if (childnodes.ChildNodes.Count > 0)
                        {
                            foreach (XmlNode linknodes in childnodes)
                            {
                                link = linknodes.Attributes[0].Value;
                                title = linknodes.Attributes[1].Value;
                                str.Append("<li><a href='" + link + "'><i class='zmdi zmdi-album'></i><span class='nav-label'>" + title + "</span></a></li>");
                            }

                        }
                    }
                }
                else
                {

                }
                str.Append("</ul>");
                Literal1.Text = str.ToString();
            }
            else
            {
                Response.Redirect("../Forms/Login.aspx");
            }
        }
    }
}