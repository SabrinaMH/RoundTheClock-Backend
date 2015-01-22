using RoundTheClock.Core.Database;

namespace RoundTheClock.Core.Repositories
{
    // smh: probably not needed!
    public class TaskRepository
    {
        private readonly DbConnection _dbConnection;
        private readonly ProjectRepository _projectRepository;

        public TaskRepository(DbConnection dbConnection, ProjectRepository projectRepository = null) // smh temp fix
        {
            _dbConnection = dbConnection;
            _projectRepository = projectRepository;
        }

        //public List<Task> GetTasksForProject(Project project)
        //{
        //    using (var conn = _dbConnection.NewConnection)
        //    {
        //        conn.Open();
        //        return null;
        //        // smh: need method GetProjectId(Project) on ProjectRepository
        //        //return conn.Query<TaskDAO>(string.Format(
        //        //    "select * from {0} where ProjectId = {1}", _dbConnection.TaskTable, project.Id)
        //        //    ).Select(dao => TaskMapper.Map(dao)).ToList();
        //    }
        //}
    }
}