using Microsoft.Owin.Testing;
using NUnit.Framework;
using RoundTheClock.Core;

namespace RoundTheClock.UnitTests
{
    [SetUpFixture]
    public class GlobalInitAndTearDown
    {
        public static TestServer Server { get; private set; }

        [SetUp]
        public void InitTestServer()
        {
            Server = TestServer.Create<Startup>();
        }

        [TearDown]
        public void DisposeTestServer()
        {
            Server.Dispose();
        }
    }
}
