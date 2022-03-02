using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactList.ContactMicroService.UnitTests
{
    public class Initialization
    {

        public TestServer TestServer { get; }

        public Initialization()
        {
            var webBuilder = new WebHostBuilder();
            webBuilder.UseStartup<Startup>();

            TestServer = new TestServer(webBuilder);
            TestServer.BaseAddress = new System.Uri("http://localhost:6000");
        }
    }
}
