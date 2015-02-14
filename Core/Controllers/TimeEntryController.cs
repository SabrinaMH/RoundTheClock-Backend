using RoundTheClock.Core.DTO;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
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

        public IHttpActionResult Post(IEnumerable<TimeEntryDTO> entries)
        {
            int noRows = _timeEntryRepository.Insert(entries.Select(dto => TimeEntryMapper.Map(dto)));
            return Ok(noRows);
        }
    }
}