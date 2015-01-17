using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RoundTheClock.Core.Model;
using System.Globalization;

namespace RoundTheClock.Core.Database
{
    public class TimeEntryDAO
    {
        public string Customer { get; set; }
        public float Hours { get; set; }
        public string Project { get; set; }
        public string Task { get; set; }
        public string Date { get; set; }

        public static TimeEntryDAO Adapt(TimeEntry timeEntry)
        {
            return new TimeEntryDAO
            {
                Customer = timeEntry.Customer != null ? Enum.GetName(typeof(CustomerEnum), timeEntry.Customer) : null,
                Hours = timeEntry.Hours,
                Project = timeEntry.Project,
                Task = timeEntry.Task,
                Date = timeEntry.Date.ToString("yyyyMMdd")
            };
        }

        public static TimeEntry Adapt(TimeEntryDAO dao)
        {
            return new TimeEntry
            {
                Customer = (CustomerEnum)Enum.Parse(typeof(CustomerEnum), dao.Customer, ignoreCase: true),
                Hours = dao.Hours,
                Project = dao.Project,
                Task = dao.Task,
                Date = DateTime.ParseExact(dao.Date, "yyyyMMdd", CultureInfo.InvariantCulture)
            }; 
        }
    }
}