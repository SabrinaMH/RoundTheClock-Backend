
namespace RoundTheClock.Core.DTO
{
    public class TimeEntryDTO
    {
        public string CustomerName { get; set; }
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public string Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public TimeEntryDTO() { }
    }
}