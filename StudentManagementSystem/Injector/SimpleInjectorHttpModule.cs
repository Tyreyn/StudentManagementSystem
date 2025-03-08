using System;
using System.Web;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace StudentManagementSystem.Injector
{
    public class SimpleInjectorHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) =>
            {
                try
                {
                    var container = (Container)HttpContext.Current.Application["Container"];
                    if (container == null)
                    {
                        throw new Exception("Kontener nie został zainicjowany.");
                    }

                    var scope = AsyncScopedLifestyle.BeginScope(container);
                    HttpContext.Current.Items["ScopedContainer"] = scope;

                    Console.WriteLine("Zakres został utworzony.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd podczas tworzenia zakresu: " + ex.Message);
                }
            };

            context.EndRequest += (sender, e) =>
            {
                try
                {
                    var scope = HttpContext.Current.Items["ScopedContainer"] as Scope;
                    if (scope != null)
                    {
                        scope.Dispose();
                        Console.WriteLine("Zakres został usunięty.");
                    }
                    else
                    {
                        Console.WriteLine("Zakres nie został ustawiony, dlatego nie można go usunąć.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd podczas usuwania zakresu: " + ex.Message);
                }
            };
        }

        public void Dispose() { }
    }
}