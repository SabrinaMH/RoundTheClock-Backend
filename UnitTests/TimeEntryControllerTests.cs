using NUnit.Framework;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace RoundTheClock.UnitTests
{
    [TestFixture]
    public class TimeEntryControllerTests
    {
        [Test]
        public void Post()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["serverUrl"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            var requestBody = @"{""customer"":""EnergiMidt"",""project"":""Mit EnergiMidt v3"",""task"":""Udvikling"",""date"":null,""hours"":2}";
            HttpContent body = new ObjectContent<string>(requestBody, jsonFormatter);
            var test = client.PostAsync("/TimeEntry", body).Result;

        }
    }
}

