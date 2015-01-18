
namespace RoundTheClock.Core.Model
{
    public class Task
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }

        public Task() { }

        public Task(string name)
        {
            Name = name;
        }
    }
}