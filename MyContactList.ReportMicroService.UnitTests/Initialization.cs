using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.TestHost;

namespace MyContactList.ReportMicroService.UnitTests
{
    public class Initialization
    {
        public TestServer TestServer { get; }

        public Initialization()
        {
            var webBuilder = new WebHostBuilder();
            webBuilder.UseStartup<Startup>();

            TestServer = new TestServer(webBuilder);
            TestServer.BaseAddress = new System.Uri("http://localhost:7000");
        }
    }
}
