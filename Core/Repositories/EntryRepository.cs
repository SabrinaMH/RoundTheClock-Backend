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
        private readonly ICustomerRepository _customerRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly ITaskRepository _taskRepo;

        public EntryRepository(IRtcDbContext dbContext, ICustomerRepository customerRepo, IProjectRepository projectRepo, ITaskRepository taskRepo)
        {
            _dbContext = dbContext;
            _customerRepo = customerRepo;
            _projectRepo = projectRepo;
            _taskRepo = taskRepo;
        }

        public void Insert(Entry entry)
        {
            var entryDAO = EntryMapper.Map(entry);

            entryDAO.Customer = _customerRepo.GetCustomerDAOByName(entryDAO.Customer.Name);
            entryDAO.Project = _projectRepo.GetProjectDAOByName(entryDAO.Project.Name);
            entryDAO.Task = _taskRepo.GetTaskDAOByName(entryDAO.Task.Name);
            entryDAO.Committed = false;

            _dbContext.Entries.Add(entryDAO);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Entry> GetUncommittedEntries()
        {
            return _dbContext.Entries.Where(e => !e.Committed).Select(EntryMapper.Map);
        }
    }
}