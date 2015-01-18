
namespace RoundTheClock.Core.Database
{
    public class TimeEntryDAO
    {
        public string Customer { get; set; }
        public float Hours { get; set; }
        public string Project { get; set; }
        public string Task { get; set; }
        public string Date { get; set; }

        public TimeEntryDAO() { }
    }
}