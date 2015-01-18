using Dapper;
using NUnit.Framework;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Model;
using RoundTheClock.Core.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace RoundTheClock.UnitTests
{
    [TestFixture]
    public class ProjectRepositoryTests
    {
        private ProjectRepository _projectRepository;
        private string _fullConnectionString;
        private List<Project> _projects;
        private List<Customer> _customers;
        private DbConnection _dbConnection;

        [SetUp]
        public void SetUp()
        {
            var _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            _fullConnectionString = "Data Source=" + Path.Combine(Environment.CurrentDirectory, _connectionString);
            _dbConnection = new DbConnection(_fullConnectionString);
            _projectRepository = new ProjectRepository(_dbConnection);
        }

        [Test]
        public void GetProjectsForCustomer()
        {
            ClearTables();
            SetUpTables();

            var customer = _customers[0];
            var result = _projectRepository.GetProjectsForCustomer(customer);
            foreach (var r in result)
            {
                Assert.IsTrue(_projects.Any(p => Utilities.AreProjectsEqual(p, r)));
            }
            Assert.IsTrue(_projects.Where(proj => proj.Customer == customer).Count() == result.Count());
        }

        public void SetUpTables()
        {
            _customers = new List<Customer> {
                new Customer { Name = "EnergiMidt" },
                new Customer { Name = "Mjolner" }
            };

            _projects = new List<Project> { 
                new Project { Name = "Nyt website", Customer = _customers[0] }, 
                new Project { Name = "Ny database", Customer = _customers[0] },
                new Project { Name = "Meeting", Customer = _customers[1] } 
            };

            using (var conn = _dbConnection.NewConnection)
            {
                conn.Execute("Insert into Projects (Name, Customer) values (@Name, @Customer)",
                    _projects.Select(proj => new { proj.Name, Customer = proj.Customer.Name }));
            }
        }

        public void ClearTables()
        {
            using (var conn = _dbConnection.NewConnection)
            {
                conn.Execute("DELETE FROM " + _dbConnection.ProjectTable);
            }
        }
    }
}
