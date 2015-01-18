using System.Data.SQLite;

namespace RoundTheClock.Core.Database
{
    public class DbConnection : IDbConnection
    {
        public readonly string TimeEntryTable = "TimeEntries";
        public readonly string CustomerTable = "Customers";
        public readonly string ProjectTable = "Projects";
        public readonly string TaskTable = "Tasks";

        readonly string _connectionString;

        public DbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SQLiteConnection NewConnection
        {
            get { return _connectionString != null ? new SQLiteConnection(_connectionString) : null; }
        }
    }
}