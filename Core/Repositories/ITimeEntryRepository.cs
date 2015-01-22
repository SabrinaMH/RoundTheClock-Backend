using RoundTheClock.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundTheClock.Core.Repositories
{
    public interface ITimeEntryRepository
    {
        List<TimeEntry> GetUncommittedForCustomer(Customer customer);
        int Insert(IEnumerable<TimeEntry> entries);
    }
}
