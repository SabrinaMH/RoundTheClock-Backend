using NUnit.Framework;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Model;
using RoundTheClock.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace RoundTheClock.UnitTests
{
    [TestFixture]
    public class TimeEntryRepositoryTests
    {
        private EntryRepository _timeEntryRepository;
        private int _uncommittedCustomerEntries;

        [SetUp]
        public void SetUp()
        {
            var _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            var fullConnectionString = "Data Source=" + Path.Combine(Environment.CurrentDirectory, _connectionString);
            _timeEntryRepository = new EntryRepository(_dbConnection);
        }

        [Test]
        public void GetUncommittedForCustomer()
        {
            var energiMidt = new Customer("EnergiMidt");
            ClearTables(new List<Customer> { energiMidt });
            SetUpTables();

            var result = _timeEntryRepository.GetUncommittedForCustomer(energiMidt);
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

            var timeEntries = new List<Entry> {
                new Entry { Project = mitEnergiMidt, Task = nytWebsite, Hours = 3, Date = DateTime.Parse("2014-12-27"), Customer = energiMidt },
                new Entry { Project = internt, Task = testfaellesskab, Hours = 1, Date = DateTime.Parse("2014-11-09"), Customer = mjolner }
            };

            var duplicateTimeEntries = new List<Entry> {
                new Entry { Project = mitEnergiMidt, Task = nytWebsite, Hours = 3, Date = DateTime.Parse("2014-12-21"), Customer = energiMidt },
                new Entry { Project = mitEnergiMidt, Task = nytWebsite, Hours = 3, Date = DateTime.Parse("2014-12-21"), Customer = energiMidt }
            };

            var noRows = _timeEntryRepository.Insert(timeEntries);
            Assert.IsTrue(noRows == 2);

            try
            {
                _timeEntryRepository.Insert(duplicateTimeEntries);
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

            var timeEntries = new List<Entry> {
                new Entry { Project = mitEnergiMidt, Task = nytWebsite, Hours = 1, Date = DateTime.Parse("2014-12-01"), Customer = energiMidt },
                new Entry { Project = mitEnergiMidt, Task = nytWebsite, Hours = 2, Date = DateTime.Parse("2013-12-01"), Customer = energiMidt },
                new Entry { Project = mitEnergiMidt, Task = nytWebsite, Hours = 3, Date = DateTime.Parse("2013-11-02"), Customer = energiMidt },
                new Entry { Project = internt, Task = morgenmoede, Hours = 0.5F, Date = DateTime.Parse("2014-11-09"), Customer = mjolner }
            };

            var customerEntries = new List<Entry> { 
                timeEntries.Where(entry => entry.Customer.Name == energiMidt.Name).OrderBy(entry => entry.Date).First() 
            };

            _uncommittedCustomerEntries = timeEntries.Count(entry => entry.Customer.Name == energiMidt.Name) - customerEntries.Count;

            using (var conn = _dbConnection.NewConnection)
            {
                conn.Execute("INSERT INTO " + _dbConnection.TimeEntryTable +
                    "(Project, Task, Hours, Date, Customer) VALUES (@Project, @Task, @Hours, @Date, @Customer)",
                    timeEntries.Select(entry => EntryMapper.Map(entry)));

                conn.Execute("Insert into " + energiMidt.Name +
                    "(Project, Task, Date) VALUES (@Project, @Task, @Date)",
                    customerEntries.Select(entry => EntryMapper.Map(entry)));
            }
        }

        public void ClearTables(List<Customer> customers)
        {
            using (var conn = _dbConnection.NewConnection)
            {
                conn.Execute("DELETE FROM " + _dbConnection.TimeEntryTable);
                conn.Execute("DELETE FROM Mjolner");
                conn.Execute("DELETE FROM EnergiMidt");
            }
        }
    }
}


