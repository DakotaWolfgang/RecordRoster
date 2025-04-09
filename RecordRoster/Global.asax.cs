using RecordRoster.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RecordRoster
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Initialize database
            using (var context = new RecordRosterDb())
            {
                try
                {
                    if (!context.Database.Exists())
                    {
                        context.Database.Create();
                    }
                    var albumCount = context.Albums.Count();
                    System.Diagnostics.Debug.WriteLine($"Database initialized with {albumCount} albums");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database initialization error: {ex.Message}");
                }
            }
        }
    }
}
