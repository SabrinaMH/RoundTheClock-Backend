using RoundTheClock.Core.Database;
using RoundTheClock.Core.Model;

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
                Name = dao.Name
            };
        }
    }
}