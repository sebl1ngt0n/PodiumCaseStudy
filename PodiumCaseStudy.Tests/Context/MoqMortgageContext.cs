using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace PodiumCaseStudy.Tests.Context
{
    public class MoqMortgageContext : MoqBaseContext
    {
        public MoqMortgageContext()
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder();
            webHostBuilder.UseSetting("dbName", nameof(MoqMortgageContext));
            _testServer = new TestServer(webHostBuilder.UseStartup<Startup>());
        }
    }
}
