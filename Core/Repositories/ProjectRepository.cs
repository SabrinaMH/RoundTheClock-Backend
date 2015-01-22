using Dapper;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace RoundTheClock.Core.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DbConnection _dbConnection;

        public ProjectRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Project> GetProjectsForCustomer(Customer customer)
        {
            using (var conn = _dbConnection.NewConnection)
            {
                conn.Open();
                var lookup = new Dictionary<long, Project>();
                conn.Query<ProjectDAO, TaskDAO, Project>(string.Format(@"
                    select p.*, t.*
                    from projects p
                    inner join tasks t
                    on t.ProjectId = p.Id
                    where Customer = '{0}'
                ", customer.Name), (p, t) =>
                 {
                     Project project;
                     if (!lookup.TryGetValue(p.Id, out project))
                     {
                         project = ProjectMapper.Map(p);
                         lookup.Add(p.Id, project);
                     }
                     if (project.Tasks == null)
                     {
                         project.Tasks = new List<Task>();
                     }
                     project.Tasks.Add(TaskMapper.Map(t));
                     return project;
                 }, splitOn: "Name");
                return lookup.Values.ToList();
            }
        }
    }
}