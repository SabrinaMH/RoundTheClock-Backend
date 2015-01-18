using RoundTheClock.Core.Database;
using RoundTheClock.Core.Model;

namespace RoundTheClock.Core.Mappers
{
    public class TaskMapper
    {
        public static Task Map(TaskDAO dao)
        {
            return new Task
            {
                Name = dao.Name,
                ProjectId = dao.ProjectId
            };
        }
    }
}