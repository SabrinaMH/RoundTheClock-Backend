using Microsoft.Practices.Unity;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Dependencies;
using RoundTheClock.Core.Logging;
using RoundTheClock.Core.Repositories;
using RoundTheClock.Core.Utilities;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace RoundTheClock
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IDbConnection, DbConnection>(new InjectionConstructor(ConnectionUtility.ConnectionString));
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            config.DependencyResolver = new UnityResolver(container);

            config.Routes.MapHttpRoute("Default",
                "{controller}/{id}",
                new { id = RouteParameter.Optional });

            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }
}