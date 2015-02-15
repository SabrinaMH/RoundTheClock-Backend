using Microsoft.Owin.Hosting;
using RoundTheClock.Core;
using System;
using System.ServiceProcess;
using log4net;

namespace RoundTheClock.HostingConsoleApp
{
    class Program : ServiceBase
    {
        private static ILog _logger = LogManager.GetLogger(typeof(Program));

        private const string _baseAddress = "http://localhost:9000/";

        static void Main(string[] args)
        {
            ServiceBase.Run(new Program());
        }

        public Program()
        {
            try
            {
                _logger.Info("New Program instance");
                this.ServiceName = "RoundTheClock";
                using (WebApp.Start<Startup>(url: _baseAddress))
                {
                    _logger.Info("Console is running");
                    Console.ReadKey();
                    _logger.Info("Console shut down");
                }
            }
            catch (Exception ex)
            {
                _logger.Fatal("Unhandled exception catched in Program()", ex);
            }
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}
