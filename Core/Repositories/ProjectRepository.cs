using Dapper;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace RoundTheClock.Core.Repositories
{
    public class ProjectRepository
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
                return conn.Query<ProjectDAO>(string.Format(
                    "select * from {0} where Customer = '{1}'", _dbConnection.ProjectTable, customer.Name)).Select(dao => ProjectMapper.Map(dao)).ToList();
            }
        }
    }
}