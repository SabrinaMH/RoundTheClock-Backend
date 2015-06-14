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
                Customer = CustomerMapper.Map(timeEntry.Customer),
                Project = ProjectMapper.Map(timeEntry.Project),
                Task = TaskMapper.Map(timeEntry.Task),
                Date = timeEntry.Date.ToString("yyyy-MM-dd"),
                From = timeEntry.From,
                To = timeEntry.To
            };
        }

        public static Entry Map(EntryDAO dao)
        {
            return new Entry
            {
                Customer = new Customer(dao.Customer.Name),
                Project = new Project(dao.Project.Name),
                Task = new Task(dao.Task.Name),
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
                Date = DateTime.ParseExact(dto.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture),
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