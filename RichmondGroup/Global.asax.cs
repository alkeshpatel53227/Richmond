using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace RichmondGroup
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        private static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);
        }
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("X-Frame-Options");
        }
        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/master").Include(
                "~/lib/moment.min.js",
                    "~/lib/fullcalendar.min.js",
                    "~/lib/gcal.min.js",
                    "~/Scripts/bootstrap.min.js",
                     "~/Scripts/angular.js",
                    "~/Scripts/angular-resource.min.js"
                    
                ));
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css/responsive").Include(
                "~/Content/bootstrap.min.css",
                    "~/Content/fullcalendar.min.css",
                    "~/Content/fullcalendar.print.min.css"
                ));

        }

    }
}