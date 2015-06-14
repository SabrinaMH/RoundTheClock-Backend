using RoundTheClock.Core.DAL;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace RoundTheClock.Core.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IRtcDbContext _dbContext;

        public CustomerRepository(IRtcDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Customer> GetCustomers()
        {
            var customers = new List<Customer>();

            foreach (var dao in _dbContext.Customers)
            {
                var customer = CustomerMapper.Map(dao);
                customer.Projects = dao.Projects.Select(projDao => ProjectMapper.Map(projDao)).ToList();
                customers.Add(customer);
            }

            return customers;
        }

        public CustomerDAO GetCustomerDAOByName(string name)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.Name == name);
        }
    }
}