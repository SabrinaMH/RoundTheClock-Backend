using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using RoundTheClock.Core;
using RoundTheClock.Core.Model;
using RoundTheClock.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace RoundTheClock.UnitTests
{
    [TestFixture]
    public class TimeEntryControllerTests
    {
        [Test]
        public void Post()
        {
            // Arrange
            var requestBody = @"[{""Customer"":""EnergiMidt"",""Project"":""Mit EnergiMidt v3"",""Task"":""Udvikling"",""Date"":""20150101"",""Hours"":2}]";
            var noRows = Regex.Matches(requestBody, "customer", RegexOptions.IgnoreCase).Count;

            var mockTimeEntryRepository = new Mock<ITimeEntryRepository>();
            mockTimeEntryRepository.Setup(mock => mock.Insert(It.IsAny<IEnumerable<TimeEntry>>())).Returns(noRows);
            Startup.Container.RegisterInstance<ITimeEntryRepository>(mockTimeEntryRepository.Object);

            var body = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // Act
            var response = GlobalInitAndTearDown.Server
                .CreateRequest("/TimeEntry")
                .And(request => request.Content = body)
                .AddHeader("Content-Type", "application/json")
                .PostAsync().Result;

            // Assert
            var task = response.Content.ReadAsAsync<int>();
            task.Wait();
            Assert.IsTrue(task.Result == noRows);
        }
    }
}

