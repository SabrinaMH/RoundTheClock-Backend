using Dapper;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Model;
using System.Collections.Generic;

namespace RoundTheClock.Core.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IProjectRepository _projectRepository;

        public CustomerRepository(IDbConnection dbConnection, IProjectRepository projectRepository)
        {
            _dbConnection = dbConnection;
            _projectRepository = projectRepository;
        }

        public IList<Customer> GetCustomers()
        {
            var customers = new List<Customer>();
            using (var conn = _dbConnection.NewConnection)
            {
                conn.Open();
                var customerDAOs = conn.Query<CustomerDAO>(string.Format(
                    "select * from {0}",
                    _dbConnection.CustomerTable));

                foreach (var dao in customerDAOs)
                {
                    var customer = CustomerMapper.Map(dao);
                    customer.Projects = _projectRepository.GetProjectsForCustomer(CustomerMapper.Map(dao));
                    customers.Add(customer);
                }
            }
            return customers;
        }
    }
}