using RoundTheClock.Core.DAL;
using RoundTheClock.Core.DTO;
using RoundTheClock.Core.Model;
using System;
using System.Globalization;

namespace RoundTheClock.Core.Mappers
{
    public class EntryMapper
    {
        public static EntryDAO Map(Entry timeEntry)
        {
            return new EntryDAO
            {
                Customers = CustomerMapper.Map(timeEntry.Customer),
                Projects = ProjectMapper.Map(timeEntry.Project),
                Tasks = TaskMapper.Map(timeEntry.Task),
                Date = timeEntry.Date.ToString("yyyyMMdd"),
                From = timeEntry.From,
                To = timeEntry.To
            };
        }

        public static Entry Map(EntryDAO dao)
        {
            return new Entry
            {
                Customer = new Customer(dao.Customers.Name),
                Project = new Project(dao.Projects.Name),
                Task = new Task(dao.Tasks.Name),
                Date = DateTime.ParseExact(RemoveTime(dao.Date), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                From = dao.From,
                To = dao.To
            };
        }

        public static Entry Map(TimeEntryDTO dto)
        {
            return new Entry
            {
                Customer = new Customer(dto.CustomerName),
                Project = new Project(dto.ProjectName),
                Task = new Task(dto.TaskName),
                Date = DateTime.ParseExact(RemoveTime(dto.Date), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                From = dto.From,
                To = dto.To
            };
        }

        private static string RemoveTime(string date)
        {
            return date.Split('T')[0];
        }
    }
}