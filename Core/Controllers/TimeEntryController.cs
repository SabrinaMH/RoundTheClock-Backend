using RoundTheClock.Core.Database;
using RoundTheClock.Core.DTO;
using RoundTheClock.Core.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using RoundTheClock.Core.Mappers;

namespace RoundTheClock.Core.Controllers
{
    public class TimeEntryController : ApiController
    {
        private readonly ITimeEntryRepository _timeEntryRepository;

        public TimeEntryController(ITimeEntryRepository timeEntryRepository)
        {
            _timeEntryRepository = timeEntryRepository;
        }

        public IHttpActionResult Post(IEnumerable<TimeEntryDTO> entries)
        {
            int noRows = _timeEntryRepository.Insert(entries.Select(dto => TimeEntryMapper.Map(dto)));
            return Ok(noRows);
        }
    }
}