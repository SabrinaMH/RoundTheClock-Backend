using RoundTheClock.Core.Database;
using RoundTheClock.Core.Repository;
using RoundTheClock.Core.Utilities;
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

        // Get all customers
        public IHttpActionResult Get()
        {
            return null;
        }
    }
}