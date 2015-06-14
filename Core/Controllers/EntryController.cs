using RoundTheClock.Core.DTO;
using RoundTheClock.Core.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Model;
using log4net;

namespace RoundTheClock.Core.Controllers
{
    public class EntryController : ApiController
    {
        private readonly IEntryRepository _entryRepository;
        private readonly ILog _logger = LogManager.GetLogger(typeof(EntryController));

        public EntryController(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public IHttpActionResult Post(TimeEntryDTO entry)
        {
            _logger.Info(string.Format("Data posted to EntryController:\n{0}", string.Join(", ", entry.CustomerName, entry.ProjectName, entry.TaskName, entry.Date, entry.From, entry.To)));
            _entryRepository.Insert(EntryMapper.Map(entry));
            return Ok();
        }

        public IHttpActionResult Options()
        {
            return Ok();
        }

        public IHttpActionResult Get()
        {
            _logger.Info("In Get() in EntryController");
            return Ok<IEnumerable<Entry>>(_entryRepository.GetUncommittedEntries());
        }
    }
}