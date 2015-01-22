using System.Configuration;

namespace RoundTheClock.Core.Utilities
{
    public static class ConnectionUtility
    {
        public static string ConnectionString
        {
            get
            {
                return "Data Source = " + ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            }
        }
    }
}