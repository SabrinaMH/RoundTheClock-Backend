
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Model;
using RoundTheClock.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RoundTheClock.Controllers
{
    public class TimeEntriesController : ApiController
    {
        public IHttpActionResult Post(IEnumerable<TimeEntry> entries)
        {
            var unitOfWork = new UnitOfWork(new DbConnection(Helpers.ConnectionHelper.ConnectionString));
            int noRows = unitOfWork.Insert(entries);
            return Ok(noRows);
        }
    }
}