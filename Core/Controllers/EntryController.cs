using RoundTheClock.Core.DTO;
using RoundTheClock.Core.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using RoundTheClock.Core.Mappers;

namespace RoundTheClock.Core.Controllers
{
    public class EntryController : ApiController
    {
        const string CLIENT_URL = "clientUrl";

        private readonly IEntryRepository _timeEntryRepository;

        public EntryController(IEntryRepository timeEntryRepository)
        {
            _timeEntryRepository = timeEntryRepository;
        }

        public IHttpActionResult Post(TimeEntryDTO entry)
        {
            _timeEntryRepository.Insert(EntryMapper.Map(entry));
            return Ok();
        }

        public IHttpActionResult Options()
        {
            return Ok();
        }
    }
}