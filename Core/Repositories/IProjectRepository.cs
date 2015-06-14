using RoundTheClock.Core.DAL;
using RoundTheClock.Core.Model;
using System.Collections.Generic;

namespace RoundTheClock.Core.Repositories
{
    public interface IProjectRepository
    {
        List<Project> GetProjectsForCustomer(Customer customer);
        ProjectDAO GetProjectDAOByName(string name);
    }
}