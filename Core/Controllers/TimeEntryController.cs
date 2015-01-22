using RoundTheClock.Core.Model;
using RoundTheClock.Core.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace RoundTheClock.Core.Controllers
{
    public class TimeEntryController : ApiController
    {
        private readonly ITimeEntryRepository _timeEntryRepository;

        public TimeEntryController(ITimeEntryRepository timeEntryRepository)
        {
            _timeEntryRepository = timeEntryRepository;
        }

        public IHttpActionResult Post(IEnumerable<TimeEntry> entries)
        {
            int noRows = _timeEntryRepository.Insert(entries);
            return Ok(noRows);
        }
    }
}