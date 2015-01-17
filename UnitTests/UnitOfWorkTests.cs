using NUnit.Framework;
using System;
using System.Data.SQLite;
using System.Configuration;
using System.Collections.Generic;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Model;
using System.IO;
using System.Globalization;
using Dapper;
using System.Linq;
using RoundTheClock.Core.Repository;

namespace RoundTheClock.UnitTests
{
    [TestFixture]
    public class UnitOfWorkTest
    {
        private UnitOfWork _unitOfWork;
        private string _fullConnectionString;
        private int _uncommittedCustomerEntries;
        
        [SetUp]
        public void SetUp()
        {
            var _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            _fullConnectionString = "Data Source=" + Path.Combine(Environment.CurrentDirectory, _connectionString);

            _unitOfWork = new UnitOfWork(new DbConnection(_fullConnectionString));

            // Does not make Insert() test redundant as this checks through _unitOfWork
            SetUpTables();
        }

        [Test]
        public void FindUncommittedByCustomer()
        {
            ClearTables();
            var result = _unitOfWork.FindUncommittedByCustomer(CustomerEnum.EnergiMidt);
            Assert.IsTrue(result.Count == 2);
        }

        [Test]
        public void Insert()
        {
            ClearTables();
            var timeEntries = new List<TimeEntry> {
                new TimeEntry { Project = "EnergiMidt", Task = "Nyt website - MitEnergiMidt.dk", Hours = 3, Date = DateTime.Parse("2014-12-27"), Customer = CustomerEnum.EnergiMidt },
                new TimeEntry { Project = "Internt", Task = "Testfællesskab", Hours = 1, Date = DateTime.Parse("2014-11-09") }
            };

            var duplicateTimeEntries = new List<TimeEntry> {
                new TimeEntry { Project = "EnergiMidt", Task = "Nyt website - MitEnergiMidt.dk", Hours = 3, Date = DateTime.Parse("2014-12-21"), Customer = CustomerEnum.EnergiMidt },
                new TimeEntry { Project = "EnergiMidt", Task = "Nyt website - MitEnergiMidt.dk", Hours = 3, Date = DateTime.Parse("2014-12-21"), Customer = CustomerEnum.EnergiMidt }
            };

            var noRows = _unitOfWork.Insert(timeEntries);
            var noDuplicateRows = _unitOfWork.Insert(duplicateTimeEntries);

            Assert.IsTrue(noRows == 2);
            Assert.IsTrue(noDuplicateRows == 1);
        }



        public void SetUpTables()
        {
            var customer = "EnergiMidt";
            var timeEntries = new List<TimeEntry> {
                new TimeEntry { Project = "EnergiMidt", Task = "Nyt website - MitEnergiMidt.dk", Hours = 1, Date = DateTime.Parse("2014-12-01"), Customer = CustomerEnum.EnergiMidt },
                new TimeEntry { Project = "EnergiMidt", Task = "Nyt website - MitEnergiMidt.dk", Hours = 2, Date = DateTime.Parse("2013-12-01"), Customer = CustomerEnum.EnergiMidt },
                new TimeEntry { Project = "EnergiMidt", Task = "Nyt website - MitEnergiMidt.dk", Hours = 3, Date = DateTime.Parse("2013-11-02"), Customer = CustomerEnum.EnergiMidt },
                new TimeEntry { Project = "Internt", Task = "Morgenmøde", Hours = 0.5F, Date = DateTime.Parse("2014-11-09") }
            };

            var customerEntries = new List<TimeEntry> { 
                timeEntries.Where(entry => entry.Customer == CustomerEnum.EnergiMidt).OrderBy(entry => entry.Date).First() 
            };

            _uncommittedCustomerEntries = timeEntries.Count(entry => entry.Customer == CustomerEnum.EnergiMidt) - customerEntries.Count;

            using (var conn = new SQLiteConnection(_fullConnectionString))
            {
                conn.Execute("INSERT INTO " + DbConnection.TimeEntryTable + 
                    "(Project, Task, Hours, Date, Customer) VALUES (@Project, @Task, @Hours, @Date, @Customer)", 
                    timeEntries.Select(entry => TimeEntryDAO.Adapt(entry)));
                
                conn.Execute("Insert into " + customer + 
                    "(Project, Task, Date) VALUES (@Project, @Task, @Date)", 
                    customerEntries.Select(entry => TimeEntryDAO.Adapt(entry)));            
            }
        }

        public void ClearTables()
        {
            using (var conn = new SQLiteConnection(_fullConnectionString))
            {
                conn.Execute("DELETE FROM " + DbConnection.TimeEntryTable);
                foreach (var customerTable in Enum.GetNames(typeof(CustomerEnum)))
                {
                    conn.Execute("DELETE FROM " + customerTable);
                }
            }
        }
    }
}


