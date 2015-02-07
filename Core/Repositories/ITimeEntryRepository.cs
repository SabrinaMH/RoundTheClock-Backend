using RoundTheClock.Core.Database;
using RoundTheClock.Core.Model;
using System.Collections.Generic;

namespace RoundTheClock.Core.Repositories
{
    public interface ITimeEntryRepository
    {
        List<TimeEntry> GetUncommittedForCustomer(Customer customer);
        int Insert(IEnumerable<TimeEntryDAO> entries);
    }
}
