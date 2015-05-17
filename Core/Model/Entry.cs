using System;

namespace RoundTheClock.Core.Model
{
    public class Entry
    {
        public Customer Customer { get; set; }
        public Project Project { get; set; }
        public Task Task { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public Entry() { }
    }
}