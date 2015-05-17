using RoundTheClock.Core.DAL;
using RoundTheClock.Core.Model;
using System.Linq;

namespace RoundTheClock.Core.Mappers
{
    public class ProjectMapper
    {
        public static Project Map(ProjectDAO dao)
        {
            return new Project
            {
                Name = dao.Name,
                Tasks = dao.Tasks.Select(t => TaskMapper.Map(t)).ToList()
            };
        }

        public static ProjectDAO Map(Project project)
        {
            return new ProjectDAO
            {
                Name = project.Name
            };
        }
    }
}