using RoundTheClock.Core.DAL;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoundTheClock.Core.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly IRtcDbContext _dbContext;

        public EntryRepository(IRtcDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Insert(Entry entry)
        {
            _dbContext.Entries.Add(EntryMapper.Map(entry));
            _dbContext.SaveChanges();
        }
    }
}