using RoundTheClock.Core.Database;
using RoundTheClock.Core.DTO;
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
                Date = DateTime.ParseExact(RemoveTime(dao.Date), "yyyy-MM-dd", CultureInfo.InvariantCulture)
            };
        }

        public static TimeEntry Map(TimeEntryDTO dto)
        {
            return new TimeEntry
            {
                Customer = new Customer(dto.Customer),
                Hours = dto.Hours,
                Project = new Project(dto.Project),
                Task = new Task(dto.Task),
                Date = DateTime.ParseExact(RemoveTime(dto.Date), "yyyy-MM-dd", CultureInfo.InvariantCulture)
            };
        }

        private static string RemoveTime(string date)
        {
            return date.Split('T')[0];
        }
    }
}