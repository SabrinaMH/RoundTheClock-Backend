using RoundTheClock.Core.DAL;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace RoundTheClock.Core.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IRtcDbContext _dbContext;

        public ProjectRepository(IRtcDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Project> GetProjectsForCustomer(Customer customer)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.Name == customer.Name).Projects.Select(p => ProjectMapper.Map(p)).ToList();
        }

        public ProjectDAO GetProjectDAOByName(string name)
        {
            return _dbContext.Projects.FirstOrDefault(p => p.Name == name);
        }
    }
}