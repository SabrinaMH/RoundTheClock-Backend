using Dapper;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoundTheClock.Core.Repository
{
    public class TimeEntryRepository
    {
        private readonly DbConnection _dbConnection;

        public TimeEntryRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Returns the time entries present in the time entries table, but not in the customer table
        /// </summary>
        public List<TimeEntry> GetUncommittedForCustomer(Customer customer)
        {
            using (var conn = _dbConnection.NewConnection)
            {
                conn.Open();
                return conn.Query<TimeEntryDAO>(
                    String.Format(@"Select * from {0} where `Date` > (select max(Date) from {1}) and `Customer` = '{1}'",
                                  _dbConnection.TimeEntryTable, customer.Name)).Select(dao => TimeEntryMapper.Map(dao)).ToList();
            }
        }

        public int Insert(IEnumerable<TimeEntry> entries)
        {
            int noRows = 0;
            using (var conn = _dbConnection.NewConnection)
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    // If not specifying a transaction, Execute is non-atomic according to
                    // https://code.google.com/p/dapper-dot-net/source/browse/Dapper/SqlMapper.cs?r=c9160c13fb45eb512f66524825612397aa3728ba
                    noRows = conn.Execute(
                        String.Format(@"Insert into {0} (Project, Task, Hours, Date, Customer) 
                                        values (@Project, @Task, @Hours, @Date, @Customer)", _dbConnection.TimeEntryTable),
                        entries.Select(entry => TimeEntryMapper.Map(entry)), transaction);
                    transaction.Commit();
                }
            }
            return noRows;
        }
    }
}