using NUnit.Framework;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using Dapper;
using RoundTheClock.Core.Model;

namespace RoundTheClock.UnitTests
{
    [TestFixture]
    public class AdministrationTests
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

        public void SetUpTables()
        {
            var energiMidt = new Customer("EnergiMidt");
            var mjolner = new Customer("Mjolner");
            var customers = new List<Customer> { energiMidt, mjolner };
            var tasksForNewWebsite = new List<Task> { new Task { Name = "Development" }, new Task { Name = "Transportation" } };
            var tasksForMeeting = new List<Task> { new Task { Name = "Prepare" }, new Task { Name = "Attend" } };
            var tasks = new List<Task>();
            tasks.AddRange(tasksForMeeting);
            tasks.AddRange(tasksForNewWebsite);

            var projects = new List<Project> { 
                new Project { Name = "Nyt website", Customer = energiMidt }, 
                new Project { Name = "Meeting", Customer = mjolner } 
            };

            using (var conn = new SQLiteConnection(_fullConnectionString))
            {
                conn.Execute("Insert into Customers (Name) values (@Name)", customers);
                foreach (var project in projects)
                {
                    var id = conn.Execute("Insert into Projects (Name, Customer) values (@Name, @Customer); select cast(scope_identity() as int)", new { Name = project.Name, Customer = project.Customer.Name });
                    if (project.Name == "Nyt website" && project.Customer == energiMidt)
                    {
                        conn.Execute("Insert into Tasks (Name, ProjectId) values (@Name, @ProjectId)", tasksForNewWebsite.Select(task => new { Name = task.Name, ProjectId = id }));
                    }
                    else if (project.Name == "Meeting" && project.Customer == mjolner)
                    {
                        conn.Execute("Insert into Tasks (Name, ProjectId) values (@Name, @ProjectId)", tasksForMeeting.Select(task => new { Name = task.Name, ProjectId = id }));
                    }
                }
            }
        }

        [Test]
        public void TestInsertion()
        {
            ClearTables();
            SetUpTables();
            Assert.True(true);
        }

        public void ClearTables()
        {
            using (var conn = new SQLiteConnection(_fullConnectionString))
            {
                conn.Execute("DELETE FROM Customers");
                conn.Execute("DELETE FROM Projects");
                conn.Execute("DELETE FROM Tasks");
            }
        }
    }
}
