
namespace RoundTheClock.Core.DTO
{
    public class TimeEntryDTO
    {
        public string Customer { get; set; }
        public float Hours { get; set; }
        public string Project { get; set; }
        public string Task { get; set; }
        public string Date { get; set; }

        public TimeEntryDTO() { }
    }
}