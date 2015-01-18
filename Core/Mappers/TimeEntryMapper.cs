using RoundTheClock.Core.Database;
using RoundTheClock.Core.Model;
using System;
using System.Globalization;

namespace RoundTheClock.Core.Mappers
{
    public class TimeEntryMapper
    {
        public static TimeEntryDAO Map(TimeEntry timeEntry)
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

        public static TimeEntry Map(TimeEntryDAO dao)
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