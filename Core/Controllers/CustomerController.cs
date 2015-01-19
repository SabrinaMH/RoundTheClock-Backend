using RoundTheClock.Core.Model;
using RoundTheClock.Core.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace RoundTheClock.Core.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IHttpActionResult Get()
        {
            return Ok<IEnumerable<Customer>>(_customerRepository.GetCustomers());
        }
    }
}