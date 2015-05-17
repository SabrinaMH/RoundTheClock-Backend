using RoundTheClock.Core.DAL;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace RoundTheClock.Core.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IProjectRepository _projectRepository;

        public CustomerRepository(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IList<Customer> GetCustomers()
        {
            var customers = new List<Customer>();

            using (var context = new RtcDbContext())
            {
                foreach (var dao in context.Customers)
                {
                    var customer = CustomerMapper.Map(dao);
                    customer.Projects = dao.Projects.Select(projDao => ProjectMapper.Map(projDao)).ToList();
                    customers.Add(customer);
                }
            }

            return customers;
        }
    }
}