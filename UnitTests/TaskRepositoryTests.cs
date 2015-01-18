using Dapper;
using NUnit.Framework;
using RoundTheClock.Core.Database;
using RoundTheClock.Core.Model;
using RoundTheClock.Core.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace RoundTheClock.UnitTests
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        private string _fullConnectionString;
        private DbConnection _dbConnection;
        private TaskRepository _taskRepository;
        private List<Project> _projects;
        private List<Customer> _customers;
        private List<Task> _tasks;

        [SetUp]
        public void SetUp()
        {
            var _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            _fullConnectionString = "Data Source=" + Path.Combine(Environment.CurrentDirectory, _connectionString);
            _dbConnection = new DbConnection(_fullConnectionString);
            _taskRepository = new TaskRepository(_dbConnection);
            _tasks = new List<Task>();
        }

        [Test]
        public void GetTasksForProject()
        {
            ClearTables();
            SetUpTables();

            var result = _taskRepository.GetTasksForProject(_projects[0]);
            foreach (var r in result)
            {
                Assert.IsTrue(_tasks.Any(t => Utilities.AreTasksEqual(t, r)));
            }
        }

        public void SetUpTables()
        {
            _customers = new List<Customer> {
                new Customer { Name = "EnergiMidt" },
                new Customer { Name = "Mjolner" }
            };

            _projects = new List<Project> { 
                new Project { Name = "Nyt website", Customer = _customers[0] }, 
                new Project { Name = "Meeting", Customer = _customers[1] } 
            };

            var tasksForNewWebsite = new List<Task> {
                new Task { Name = "Development" }, 
                new Task { Name = "Transportation" } 
            };
            var tasksForMeeting = new List<Task> {
                new Task { Name = "Prepare" },
                new Task { Name = "Attend" } 
            };

            using (var conn = _dbConnection.NewConnection)
            {
                foreach (var project in _projects)
                {
                    IEnumerable<Task> tasksWithProjectId = new List<Task>();
                    var projectId = conn.Query<long>(
                            "Insert into Projects (Name, Customer) values (@Name, @Customer); select last_insert_rowid() from Projects;",
                            new { Name = project.Name, Customer = project.Customer.Name }
                        ).First();
                    if (project.Name == "Nyt website" && project.Customer == _customers[0])
                    {
                        tasksWithProjectId = tasksForNewWebsite.Select(task => new Task { Name = task.Name, ProjectId = projectId });
                        conn.Execute("Insert into Tasks (Name, ProjectId) values (@Name, @projectId)", tasksWithProjectId);
                    }
                    else if (project.Name == "Meeting" && project.Customer == _customers[1])
                    {
                        tasksWithProjectId = tasksForMeeting.Select(task => new Task { Name = task.Name, ProjectId = projectId });
                        conn.Execute("Insert into Tasks (Name, ProjectId) values (@Name, @projectId)", tasksWithProjectId);
                    }
                    _tasks.AddRange(tasksWithProjectId);
                }
            }
        }

        public void ClearTables()
        {
            using (var conn = _dbConnection.NewConnection)
            {
                conn.Execute("DELETE FROM " + _dbConnection.TaskTable);
                conn.Execute("DELETE FROM " + _dbConnection.ProjectTable);
            }
        }
    }
}
