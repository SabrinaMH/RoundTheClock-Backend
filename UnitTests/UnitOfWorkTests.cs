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
using System.Data;

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
        }

        [Test]
        public void FindUncommittedByCustomer()
        {
            var energiMidt = new Customer("EnergiMidt");
            ClearTables(new List<Customer> { energiMidt });
            SetUpTables();

            var result = _unitOfWork.FindUncommittedByCustomer(energiMidt);
            Assert.IsTrue(result.Count == 2);
        }

        [Test]
        public void Insert()
        {
            var energiMidt = new Customer("EnergiMidt");
            var mitEnergiMidt = new Project("MitEnergiMidt");
            var nytWebsite = new Task("Nyt website - MitEnergiMidt.dk");
            var mjolner = new Customer("Mjolner");
            var internt = new Project("Internt");
            var testfaellesskab = new Task("Testfællesskab");

            ClearTables(new List<Customer> { energiMidt, mjolner });
            SetUpTables();

            var timeEntries = new List<TimeEntry> {
                new TimeEntry { Project = mitEnergiMidt, Task = nytWebsite, Hours = 3, Date = DateTime.Parse("2014-12-27"), Customer = energiMidt },
                new TimeEntry { Project = internt, Task = testfaellesskab, Hours = 1, Date = DateTime.Parse("2014-11-09"), Customer = mjolner }
            };

            var duplicateTimeEntries = new List<TimeEntry> {
                new TimeEntry { Project = mitEnergiMidt, Task = nytWebsite, Hours = 3, Date = DateTime.Parse("2014-12-21"), Customer = energiMidt },
                new TimeEntry { Project = mitEnergiMidt, Task = nytWebsite, Hours = 3, Date = DateTime.Parse("2014-12-21"), Customer = energiMidt }
            };

            var noRows = _unitOfWork.Insert(timeEntries);
            Assert.IsTrue(noRows == 2);

            try {
                _unitOfWork.Insert(duplicateTimeEntries);
                Assert.Fail();
            }
            catch (SQLiteException sqliteEx)
            {
                if (sqliteEx.Message.IndexOf("unique constraint failed", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    Assert.Fail();
                }
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        public void SetUpTables()
        {
            var energiMidt = new Customer("EnergiMidt");
            var mitEnergiMidt = new Project("MitEnergiMidt");
            var nytWebsite = new Task("Nyt website - MitEnergiMidt.dk");
            var mjolner = new Customer("Mjolner");
            var internt = new Project("Internt");
            var morgenmoede = new Task("morgenmøde");

            var timeEntries = new List<TimeEntry> {
                new TimeEntry { Project = mitEnergiMidt, Task = nytWebsite, Hours = 1, Date = DateTime.Parse("2014-12-01"), Customer = energiMidt },
                new TimeEntry { Project = mitEnergiMidt, Task = nytWebsite, Hours = 2, Date = DateTime.Parse("2013-12-01"), Customer = energiMidt },
                new TimeEntry { Project = mitEnergiMidt, Task = nytWebsite, Hours = 3, Date = DateTime.Parse("2013-11-02"), Customer = energiMidt },
                new TimeEntry { Project = internt, Task = morgenmoede, Hours = 0.5F, Date = DateTime.Parse("2014-11-09"), Customer = mjolner }
            };

            var customerEntries = new List<TimeEntry> { 
                timeEntries.Where(entry => entry.Customer.Name == energiMidt.Name).OrderBy(entry => entry.Date).First() 
            };

            _uncommittedCustomerEntries = timeEntries.Count(entry => entry.Customer.Name == energiMidt.Name) - customerEntries.Count;

            using (var conn = new SQLiteConnection(_fullConnectionString))
            {
                conn.Execute("INSERT INTO " + DbConnection.TimeEntryTable + 
                    "(Project, Task, Hours, Date, Customer) VALUES (@Project, @Task, @Hours, @Date, @Customer)", 
                    timeEntries.Select(entry => TimeEntryDAO.Adapt(entry)));
                
                conn.Execute("Insert into " + energiMidt.Name + 
                    "(Project, Task, Date) VALUES (@Project, @Task, @Date)", 
                    customerEntries.Select(entry => TimeEntryDAO.Adapt(entry)));            
            }
        }

        public void ClearTables(List<Customer> customers)
        {
            using (var conn = new SQLiteConnection(_fullConnectionString))
            {
                conn.Execute("DELETE FROM " + DbConnection.TimeEntryTable);
                conn.Execute("DELETE FROM Mjolner");
                conn.Execute("DELETE FROM EnergiMidt");
            }
        }
    }
}


