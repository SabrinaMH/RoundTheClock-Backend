using System;

namespace RoundTheClock.Core.Model
{
    public class TimeEntry
    {
		public CustomerEnum? Customer { get; set; }
        public float Hours { get; set; }
        public string Project { get; set; }
        public string Task { get; set; }
        public DateTime Date { get; set; }

        public TimeEntry() { }
    }
}