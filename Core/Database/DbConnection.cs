using System.Data.SQLite;

namespace RoundTheClock.Core.Database
{
    public class DbConnection : IDbConnection
    {
        private readonly string _timeEntryTable = "TimeEntries";
        private readonly string _customerTable = "Customers";
        private readonly string _projectTable = "Projects";
        private readonly string _taskTable = "Tasks";

        readonly string _connectionString;

        public DbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SQLiteConnection NewConnection
        {
            get { return _connectionString != null ? new SQLiteConnection(_connectionString) : null; }
        }

        public string TimeEntryTable
        {
            get { return _timeEntryTable; }
        }

        public string CustomerTable
        {
            get { return _customerTable; }
        }

        public string ProjectTable
        {
            get { return _projectTable; }
        }

        public string TaskTable
        {
            get { return _taskTable; }
        }
    }
}