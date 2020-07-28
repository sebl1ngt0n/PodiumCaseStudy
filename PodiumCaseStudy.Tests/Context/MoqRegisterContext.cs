using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace PodiumCaseStudy.Tests.Context
{
    public class MoqRegisterContext : MoqBaseContext
    {
        public MoqRegisterContext()
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder();
            webHostBuilder.UseSetting("dbName", nameof(MoqRegisterContext));
            _testServer = new TestServer(webHostBuilder.UseStartup<Startup>());
        }
    }
}
