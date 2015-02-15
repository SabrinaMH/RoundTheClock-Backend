using log4net;
using RoundTheClock.Core.Model;
using RoundTheClock.Core.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace RoundTheClock.Core.Controllers
{
    public class CustomerController : ApiController
    {
        private static ILog _logger = LogManager.GetLogger(typeof(CustomerController));
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IHttpActionResult Get()
        {
            _logger.Info("In Get() in CustomerController");
            return Ok<IEnumerable<Customer>>(_customerRepository.GetCustomers());
        }
    }
}