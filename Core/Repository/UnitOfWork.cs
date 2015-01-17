using RoundTheClock.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using Dapper;
using RoundTheClock.Core.Database;

namespace RoundTheClock.Core.Repository
{
    public class UnitOfWork
    {
        private readonly DbConnection _dbConnection;

        public UnitOfWork(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Returns the time entries present in the time entries table, but not in the customer table
        /// </summary>
        public List<TimeEntry> FindUncommittedByCustomer(Customer customer)
        {
            using (var conn = _dbConnection.Connection)
            {
                conn.Open();
                return conn.Query<TimeEntryDAO>(
                    String.Concat("Select * from ", DbConnection.TimeEntryTable,
                    " where `Date` > (select max(Date) from ", customer.Name, ")",
                    " and `Customer` = '", customer.Name, "'")).Select(dao => TimeEntryDAO.Adapt(dao)).ToList();
            }
        }

        public int Insert(IEnumerable<TimeEntry> entries)
        {
            int noRows = 0;
            using (var conn = _dbConnection.Connection)
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    // If not specifying a transaction, Execute is non-atomic according to
                    // https://code.google.com/p/dapper-dot-net/source/browse/Dapper/SqlMapper.cs?r=c9160c13fb45eb512f66524825612397aa3728ba
                    noRows = conn.Execute(
                        String.Concat("Insert into ", DbConnection.TimeEntryTable,
                        "(Project, Task, Hours, Date, Customer) values (@Project, @Task, @Hours, @Date, @Customer)"),
                        entries.Select(entry => TimeEntryDAO.Adapt(entry)), transaction);
                    transaction.Commit();
                }
            }
            return noRows;
        }
    }
}