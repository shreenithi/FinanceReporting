using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FinReport.Models;

namespace FinReport
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static System.Timers.Timer timer = new System.Timers.Timer(7200000); // This will raise the event every two hours.
        protected void Application_Start()
        {
            timer.Enabled = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            user userObj = new user();
            userObj.populatingDb();
        }

    }
}
