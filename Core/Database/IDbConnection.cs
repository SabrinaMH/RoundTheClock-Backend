using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundTheClock.Core.Database
{
    public interface IDbConnection
    {
        SQLiteConnection NewConnection { get; }

        string TimeEntryTable { get; }
        string CustomerTable { get; }
        string ProjectTable { get; }
        string TaskTable { get; }
    }
}
