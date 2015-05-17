using RoundTheClock.Core.DAL;
using RoundTheClock.Core.Model;

namespace RoundTheClock.Core.Mappers
{
    public class TaskMapper
    {
        public static Task Map(TaskDAO dao)
        {
            return new Task
            {
                Name = dao.Name
            };
        }

        public static TaskDAO Map(Task task)
        {
            return new TaskDAO
            {
                Name = task.Name
            };
        }
    }
}