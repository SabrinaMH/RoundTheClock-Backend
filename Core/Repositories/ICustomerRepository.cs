using RoundTheClock.Core.DAL;
using RoundTheClock.Core.Model;
using System.Collections.Generic;

namespace RoundTheClock.Core.Repositories
{
    public interface ICustomerRepository
    {
        IList<Customer> GetCustomers();
        CustomerDAO GetCustomerDAOByName(string name);
    }
}
