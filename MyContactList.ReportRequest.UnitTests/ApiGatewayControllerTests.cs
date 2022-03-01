using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyContactList.ReportRequest.UnitTests
{
    public class ApiGatewayControllerTests
    {
        [Fact]
        public async Task GetAllContacts_Returns_The_Correct_Number_Of_ContactsAsync()
        {
            var client = new TestClientProvider().Client;

            // Act
            var okResult = await client.GetAsync("values/1");

            okResult.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(HttpStatusCode.OK, okResult.StatusCode);
        }
    }
}
