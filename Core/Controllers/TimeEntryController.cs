using RoundTheClock.Core.Database;
using RoundTheClock.Core.Model;
using RoundTheClock.Core.Repositories;
using RoundTheClock.Core.Utilities;
using System.Collections.Generic;
using System.Web.Http;

namespace RoundTheClock.Core.Controllers
{
    public class TimeEntryController : ApiController
    {
        public IHttpActionResult Post(IEnumerable<TimeEntry> entries)
        {
            var timeEntryRepository = new TimeEntryRepository(new DbConnection(ConnectionUtility.ConnectionString));
            int noRows = timeEntryRepository.Insert(entries);
            return Ok(noRows);
        }
    }
}