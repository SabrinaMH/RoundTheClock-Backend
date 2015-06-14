using RoundTheClock.Core.DAL;

namespace RoundTheClock.Core.Repositories
{
    public interface ITaskRepository
    {
        TaskDAO GetTaskDAOByName(string name);
    }
}