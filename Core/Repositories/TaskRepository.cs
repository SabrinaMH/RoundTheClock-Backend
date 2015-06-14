using RoundTheClock.Core.DAL;
using RoundTheClock.Core.Mappers;
using System.Collections.Generic;
using System.Linq;

namespace RoundTheClock.Core.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IRtcDbContext _dbContext;

        public TaskRepository(IRtcDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TaskDAO GetTaskDAOByName(string name)
        {
            return _dbContext.Tasks.FirstOrDefault(t => t.Name == name);
        }
    }
}