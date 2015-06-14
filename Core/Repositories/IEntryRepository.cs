using RoundTheClock.Core.DAL;
using RoundTheClock.Core.Model;
using System.Collections.Generic;

namespace RoundTheClock.Core.Repositories
{
    public interface IEntryRepository
    {
        void Insert(Entry entry);
        IEnumerable<Entry> GetUncommittedEntries();
    }
}
