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
                Customer = timeEntry.Customer.Name,
                Hours = timeEntry.Hours,
                Project = timeEntry.Project.Name,
                Task = timeEntry.Task.Name,
                Date = timeEntry.Date.ToString("yyyyMMdd")
            };
        }

        public static TimeEntry Adapt(TimeEntryDAO dao)
        {
            return new TimeEntry
            {
                Customer = new Customer(dao.Customer),
                Hours = dao.Hours,
                Project = new Project(dao.Project),
                Task = new Task(dao.Task),
                Date = DateTime.ParseExact(dao.Date, "yyyyMMdd", CultureInfo.InvariantCulture)
            }; 
        }
    }
}