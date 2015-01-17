using RoundTheClock.Core.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;

namespace RoundTheClock.Core.Database
{
    public class DbConnection
    {
        const string _timeEntryTable = "TimeEntries";
        readonly string _connectionString;
        
        public DbConnection(string connectionString){
            _connectionString = connectionString;
        }

        public SQLiteConnection Connection
        {
            get { return _connectionString != null ? new SQLiteConnection(_connectionString) : null; }
        }

        public static string TimeEntryTable
        {
            get { return _timeEntryTable; }
        }
    }
}