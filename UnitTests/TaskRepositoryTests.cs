using NUnit.Framework;
using RoundTheClock.Core.DAL;
using RoundTheClock.Core.Mappers;
using RoundTheClock.Core.Model;
using RoundTheClock.Core.Repositories;
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
        private List<Project> _projects;
        private List<Customer> _customers;
        private List<TaskDAO> _tasks;

        [SetUp]
        public void SetUp()
        {
            var _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            _fullConnectionString = "Data Source=" + Path.Combine(Environment.CurrentDirectory, _connectionString);
            _tasks = new List<TaskDAO>();
        }

        //[Test]
        //public void GetTasksForProject()
        //{
        //    ClearTables();
        //    SetUpTables();

        //    var result = _taskRepository.GetTasksForProject(_projects[0]);
        //    var tasks = _tasks.Select(dao => TaskMapper.Map(dao));

        //    foreach (var r in result)
        //    {
        //        Assert.IsTrue(tasks.Any(t => Utilities.AreTasksEqual(t, r)));
        //    }
        //}

        public void SetUpTables()
        {
            _customers = new List<Customer> {
                new Customer { Name = "EnergiMidt" },
                new Customer { Name = "Mjolner" }
            };

            _projects = new List<Project> { 
                new Project { Name = "Nyt website" }, 
                new Project { Name = "Meeting" } 
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
                    IEnumerable<TaskDAO> tasksWithProjectId = new List<TaskDAO>();
                    var projectId = conn.Query<long>(
                            "Insert into Projects (Name, Customer) values (@Name, @Customer); select last_insert_rowid() from Projects;",
                            new { Name = project.Name, Customer = _customers.First(c => c.Projects.Any(p => Utilities.AreProjectsEqual(p, project))).Name }
                        ).First();
                    if (project.Name == "Nyt website")
                    {
                        tasksWithProjectId = tasksForNewWebsite.Select(task => new TaskDAO { Name = task.Name, ProjectId = projectId });
                        conn.Execute("Insert into Tasks (Name, ProjectId) values (@Name, @projectId)", tasksWithProjectId);
                    }
                    else if (project.Name == "Meeting")
                    {
                        tasksWithProjectId = tasksForMeeting.Select(task => new TaskDAO { Name = task.Name, ProjectId = projectId });
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
