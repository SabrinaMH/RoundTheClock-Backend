using Dapper;
using NUnit.Framework;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Model;
using RoundTheClock.Core.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace RoundTheClock.UnitTests
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        private CustomerRepository _customerRepository;
        private List<Customer> _customers;
        private DbConnection _dbConnection;

        [SetUp]
        public void SetUp()
        {
            var _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            var fullConnectionString = "Data Source=" + Path.Combine(Environment.CurrentDirectory, _connectionString);
            _dbConnection = new DbConnection(fullConnectionString);
            _customerRepository = new CustomerRepository(_dbConnection);
        }

        [Test]
        public void GetAllCustomers()
        {
            ClearTables();
            SetUpTables();

            var result = _customerRepository.GetAllCustomers();

            Assert.IsTrue(result.Count == _customers.Count);
        }

        public void SetUpTables()
        {
            _customers = new List<Customer> { new Customer("EnergiMidt"), new Customer("Mjolner") };

            using (var conn = _dbConnection.NewConnection)
            {
                conn.Execute("Insert into Customers (Name) values (@Name)", _customers);
            }
        }

        public void ClearTables()
        {
            using (var conn = _dbConnection.NewConnection)
            {
                conn.Execute("DELETE FROM " + _dbConnection.CustomerTable);
            }
        }
    }
}
