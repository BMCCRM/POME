using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PomeSurveyApplication.Handler
{
    /// <summary>
    /// Summary description for SessionHandler1
    /// </summary>
    public class SessionHandler1 : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            try
            {
                string sessionidentity = context.Request.QueryString["SessionIdentity"];
                if (sessionidentity == "ABC")
                {
                    var userid = context.Session["UserRole"];
                    var username = context.Session["UserName"];
                    var userrole = context.Session["UserRole"];
                    context.Response.Write(userid + "," + username +","+ userrole);
                }

            }
            catch (Exception ex)
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}