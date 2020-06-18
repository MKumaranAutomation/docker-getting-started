using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace Generator.IntegrationTests
{
    public class TestBase
    {
        public HttpClient Client { get; private set; }

        public TestBase()
        {
            var factory = new WebApplicationFactory<Startup>();
            Client = factory.CreateClient();
        }
    }
}