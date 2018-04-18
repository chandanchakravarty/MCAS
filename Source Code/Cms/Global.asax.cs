using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Cms
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            foreach (string sCookie in Response.Cookies)
            {
                if (sCookie == "ASP.NET_SessionId")
                {
                    if (System.Environment.Version.Major < 2)
                    {
                        // Force HttpOnly to be added to the cookie header under 1.x                        
                        Response.Cookies[sCookie].Path += ";HttpOnly";
                    }
                }
                if (Request.IsSecureConnection == true)
                {
                    Response.Cookies[sCookie].Secure = true;

                }

            }


        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Session.Clear();
            Session.RemoveAll();


        }

    }
}
