using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace StudentManagementSystem
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Kod uruchamiany podczas uruchamiania aplikacji
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //// Initialize the product database.
            Database.SetInitializer(new CreateDatabaseIfNotExists<StudentContext>());
            using (var context = new StudentContext())
            {
                context.Database.Initialize(force: false);  // to wymusi sprawdzenie i stworzenie bazy
            }
        }
    }
}