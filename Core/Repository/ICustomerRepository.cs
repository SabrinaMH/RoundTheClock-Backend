using RoundTheClock.Core.Model;
using System.Collections.Generic;

namespace RoundTheClock.Core.Repository
{
    interface ICustomerRepository
    {
        List<Customer> GetAllCustomers();
    }
}
