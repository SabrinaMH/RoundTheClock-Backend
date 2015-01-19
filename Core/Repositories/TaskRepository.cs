using Dapper;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace RoundTheClock.Core.Repositories
{
    public class TaskRepository
    {
        private readonly DbConnection _dbConnection;

        public TaskRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Task> GetTasksForProject(Project project)
        {
            using (var conn = _dbConnection.NewConnection)
            {
                conn.Open();
                return conn.Query<TaskDAO>(string.Format(
                    "select * from {0} where ProjectId = {1}", _dbConnection.TaskTable, project.Id)
                    ).Select(dao => TaskMapper.Map(dao)).ToList();
            }
        }
    }
}