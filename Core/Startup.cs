using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Dependencies;
using RoundTheClock.Core.Logging;
using RoundTheClock.Core.Repositories;
using RoundTheClock.Core.Utilities;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

[assembly: OwinStartup(typeof(RoundTheClock.Core.Startup))]

namespace RoundTheClock.Core
{
    public class Startup
    {
        public static UnityContainer Container { get; private set; }

        public void Configuration(IAppBuilder builder)
        {
            var config = new HttpConfiguration();        

            // IoC
            Container = new UnityContainer();
            Container.RegisterType<IDbConnection, DbConnection>(new InjectionConstructor(ConnectionUtility.ConnectionString));
            Container.RegisterType<ICustomerRepository, CustomerRepository>();
            Container.RegisterType<IProjectRepository, ProjectRepository>();
            Container.RegisterType<ITimeEntryRepository, TimeEntryRepository>();
            config.DependencyResolver = new UnityResolver(Container);

            // Routing
            config.Routes.MapHttpRoute("Default",
                "{controller}/{id}",
                new { id = RouteParameter.Optional });

            // Return type
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Logging
            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            builder.UseWebApi(config);
        }
    }
}
