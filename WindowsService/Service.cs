using Microsoft.Owin.Hosting;
using RoundTheClock.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RoundTheClockWindowsService
{
    public partial class Service : ServiceBase
    {
        private const string _baseAddress = "http://localhost:9000/";
        private IDisposable _server = null;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _server = WebApp.Start<Startup>(url: _baseAddress);
        }

        protected override void OnStop()
        {
            if (_server != null)
            {
                _server.Dispose();
            }
            base.OnStop();
        }
    }
}
