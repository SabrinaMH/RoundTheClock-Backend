using RoundTheClock.Core.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace RoundTheClock.Core.Utilities
{
    public static class ConnectionUtility
    {
        public static string ConnectionString
        {
            get
            {
                return "Data Source=" + Path.Combine(Environment.CurrentDirectory,
                ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            }
        }
    }
}