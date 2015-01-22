
using System.Collections.Generic;
namespace RoundTheClock.Core.Model
{
    public class Project
    {
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }

        public Project() { }

        public Project(string name)
        {
            Name = name;
        }
    }
}