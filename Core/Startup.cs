using log4net;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;
using Owin;
using RoundTheClock.Core.DAL;
using RoundTheClock.Core.Unity;
using RoundTheClock.Core.Logging;
using RoundTheClock.Core.Repositories;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Data.Entity.Infrastructure.Interception;

[assembly: OwinStartup(typeof(RoundTheClock.Core.Startup))]

namespace RoundTheClock.Core
{
    public class Startup
    {
        private static ILog _logger = LogManager.GetLogger(typeof(Startup));
        public static UnityContainer Container { get; private set; }

        public void Configuration(IAppBuilder builder)
        {
            _logger.Info("In Configuration method in Startup class");
            var config = new HttpConfiguration();

            // IoC
            Container = new UnityContainer();
            Container.RegisterType<IRtcDbContext, RtcDbContext>(new PerRequestLifetimeManager());
            Container.RegisterType<ICustomerRepository, CustomerRepository>();
            Container.RegisterType<IProjectRepository, ProjectRepository>();
            Container.RegisterType<ITaskRepository, TaskRepository>();
            Container.RegisterType<IEntryRepository, EntryRepository>();
            config.DependencyResolver = new UnityResolver(Container);

            // Routing
            config.Routes.MapHttpRoute("Default",
                "{controller}/{id}",
                new { id = RouteParameter.Optional });

            // JSON settings
            //config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Logging
            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            DbInterception.Add(new SqlLogger());

            builder.UseWebApi(config);
        }
    }
}
