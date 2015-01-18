using RoundTheClock.Core.Database;
using RoundTheClock.Core.Model;

namespace RoundTheClock.Core.Mappers
{
    public class ProjectMapper
    {
        public static Project Map(ProjectDAO dao)
        {
            return new Project
            {
                Name = dao.Name,
                Id = dao.Id,
                Customer = new Customer { Name = dao.Customer }
            };
        }
    }
}