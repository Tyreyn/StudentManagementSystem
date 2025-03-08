using StudentManagementSystem.Models;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using StudentManagementSystem.Services;

namespace StudentManagementSystem
{
    public class Global : HttpApplication
    {
        public static Container Container { get; private set; }


        void Application_Start(object sender, EventArgs e)
        {
            // Kod uruchamiany podczas uruchamiania aplikacji
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //// Initialize the product database.
            Database.SetInitializer(new CreateDatabaseIfNotExists<StudentContext>());
            using (var context = new StudentContext())
            {
                context.Database.Initialize(force: false);
            }

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<StudentContext>(Lifestyle.Singleton);

            container.Register<StudentService>(Lifestyle.Singleton);

            container.Verify();

            Application["Container"] = container;

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.FilePath == "/")
            {
                HttpContext.Current.RewritePath("~/StudentList.aspx");
            }
        }

    }
}