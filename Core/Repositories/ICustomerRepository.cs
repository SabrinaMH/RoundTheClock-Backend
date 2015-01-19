using RoundTheClock.Core.Model;
using System.Collections.Generic;

namespace RoundTheClock.Core.Repositories
{
    public interface ICustomerRepository
    {
        IList<Customer> GetCustomers();
    }
}
