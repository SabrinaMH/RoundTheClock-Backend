using NUnit.Framework;
using RoundTheClock.Core.DAL;
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
    public class ProjectRepositoryTests
    {
        private ProjectRepository _projectRepository;
        private string _fullConnectionString;
        private List<Project> _projects;
        private List<Customer> _customers;
        private List<TaskDAO> _tasks;

        [SetUp]
        public void SetUp()
        {
            var _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            _fullConnectionString = "Data Source=" + Path.Combine(Environment.CurrentDirectory, _connectionString);
            _projectRepository = new ProjectRepository(_dbConnection);
            _tasks = new List<TaskDAO>();
            _projects = new List<Project>();
        }

        [Test]
        public void GetProjectsForCustomer()
        {
            ClearTables();
            SetUpTables();

            var customer = _customers[0];
            var result = _projectRepository.GetProjectsForCustomer(customer);
            foreach (var r in result)
            {
                Assert.IsTrue(customer.Projects.Any(p => Utilities.AreProjectsEqual(p, r)));
            }
            Assert.IsTrue(customer.Projects.Count() == result.Count());
        }

        public void SetUpTables()
        {
            var tasksForNewWebsite = new List<Task> {
                new Task { Name = "Development" }, 
                new Task { Name = "Transportation" } 
            };
            var tasksForNewDatabase = new List<Task>{
                new Task { Name = "Development" },
                new Task {Name = "Design" }
            };
            var tasksForMeeting = new List<Task> {
                new Task { Name = "Prepare" },
                new Task { Name = "Attend" } 
            };

            var projectsForEnergiMidt = new List<Project> { 
                new Project { Name = "Nyt website", Tasks = tasksForNewWebsite }, 
                new Project { Name = "Ny database", Tasks = tasksForNewDatabase }
            };
            var projectsForMjolner = new List<Project> {
                new Project { Name = "Meeting", Tasks = tasksForMeeting }
            };
            _projects.AddRange(projectsForEnergiMidt);
            _projects.AddRange(projectsForMjolner);

            _customers = new List<Customer> {
                new Customer { Name = "EnergiMidt", Projects = projectsForEnergiMidt },
                new Customer { Name = "Mjolner", Projects = projectsForMjolner }
            };

            using (var conn = _dbConnection.NewConnection)
            {
                foreach (var project in _projects)
                {
                    // Insert into Projects table
                    var projectId = conn.Query<long>(
                            "Insert into Projects (Name, Customer) values (@Name, @Customer); select last_insert_rowid() from Projects;",
                            new { Name = project.Name, Customer = _customers.First(c => c.Projects.Any(p => Utilities.AreProjectsEqual(p, project))).Name }
                        ).First();

                    // Insert into Tasks table
                    var tasksWithProjectId = project.Tasks.Select(task => new TaskDAO { Name = task.Name, ProjectId = projectId });
                    conn.Execute("Insert into Tasks (Name, ProjectId) values (@Name, @projectId)", tasksWithProjectId);
                    _tasks.AddRange(tasksWithProjectId);
                }
            }
        }

        public void ClearTables()
        {
            using (var conn = _dbConnection.NewConnection)
            {
                conn.Execute("DELETE FROM " + _dbConnection.ProjectTable);
                conn.Execute("DELETE FROM " + _dbConnection.TaskTable);
            }
        }
    }
}
