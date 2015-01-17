using System;

namespace RoundTheClock.Core.Model
{
    public class TimeEntry
    {
		public Customer Customer { get; set; }
        public float Hours { get; set; }
        public Project Project { get; set; }
        public Task Task { get; set; }
        public DateTime Date { get; set; }

        public TimeEntry() { }
    }
}