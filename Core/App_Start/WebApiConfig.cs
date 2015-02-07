using Microsoft.Practices.Unity;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Dependencies;
using RoundTheClock.Core.Logging;
using RoundTheClock.Core.Repositories;
using RoundTheClock.Core.Utilities;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace RoundTheClock
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // IoC
            var container = new UnityContainer();
            container.RegisterType<IDbConnection, DbConnection>(new InjectionConstructor(ConnectionUtility.ConnectionString));
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<IProjectRepository, ProjectRepository>();
            container.RegisterType<ITimeEntryRepository, TimeEntryRepository>();
            config.DependencyResolver = new UnityResolver(container);

            // Routing
            config.Routes.MapHttpRoute("Default",
                "{controller}/{id}",
                new { id = RouteParameter.Optional });

            // Return type
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Logging
            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }
}