using RoundTheClock.Core.DAL;
using RoundTheClock.Core.Model;
using System.Linq;

namespace RoundTheClock.Core.Mappers
{
    public class CustomerMapper
    {
        public static CustomerDAO Map(Customer customer)
        {
            return new CustomerDAO
            {
                Name = customer.Name
            };
        }

        public static Customer Map(CustomerDAO dao)
        {
            return new Customer
            {
                Name = dao.Name,
                Projects = dao.Projects.Select(p => ProjectMapper.Map(p)).ToList()
            };
        }
    }
}