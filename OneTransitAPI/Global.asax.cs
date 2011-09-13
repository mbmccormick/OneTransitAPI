using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.ServiceModel.Activation;
using OneTransitAPI.Common;

namespace OneTransitAPI
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(new ServiceRoute("v1", new WebServiceHostFactory(), typeof(OneTransitAPI.TransitService)));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();

            Utilities.LogEvent("OneTransitAPI", "An unhandled exception has occurred. Message: " + ex.Message + "; " +
                                                                                   "Source: " + ex.Source + "; " +
                                                                                   "TargetSite: " + ex.TargetSite + "; " +
                                                                                   "StackTrace: " + ex.StackTrace + ";");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}