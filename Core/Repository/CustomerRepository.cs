using Dapper;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace RoundTheClock.Core.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _dbConnection;

        public CustomerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Customer> GetAllCustomers()
        {
            using (var conn = _dbConnection.NewConnection)
            {
                conn.Open();
                return conn.Query<CustomerDAO>(string.Format(
                    "select * from {0}",
                    _dbConnection.CustomerTable)).Select(dao => CustomerMapper.Map(dao)).ToList();
            }
        }
    }
}