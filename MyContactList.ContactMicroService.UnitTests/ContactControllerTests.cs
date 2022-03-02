using Contact.API.Controllers;
using Contact.DAL.Abstract;
using Entities.DataModel;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyContactList.ContactMicroService.UnitTests
{
    public class ContactControllerTests : IClassFixture<Initialization>
    {
        protected TestServer _testServer;
        private readonly Mock<IContactOperation> _repositoryStub;
        private readonly ContactController _controller;

        public ContactControllerTests(Initialization initialization)
        {
            _repositoryStub = new Mock<IContactOperation>();
            _controller = new ContactController(_repositoryStub.Object);
            _testServer = initialization.TestServer;
        }

        private ContactDto CreateRandomContactDto()
        {
            Guid contactid = Guid.NewGuid();

            return new()
            {
                Id = contactid,
                Name = "Murat",
                LastName = "Yaşar",
                CompanyName = "Home",
                DateCreated = DateTime.UtcNow,
                Details = new List<ContactDetailDto>()
                {
                    new ContactDetailDto() { Id = 1, ContactId = contactid, TelephoneNumber = "05555555555", EMailAddress = "muratyasar@boylebiradresyok.com", Address = "İstanbul", DateCreated = DateTime.UtcNow }
                }
            };
        }

        private List<ContactDto> CreateRandomContactDtoList()
        {
            Guid contacid_1 = Guid.NewGuid();
            Guid contacid_2 = Guid.NewGuid();
            Guid contacid_3 = Guid.NewGuid();
            Guid contacid_4 = Guid.NewGuid();

            List<ContactDto> items = new List<ContactDto>()
            {
                new ContactDto() {
                    Id = contacid_1,
                    Name = "Murat",
                    LastName = "Yaşar",
                    CompanyName = "Home",
                    DateCreated = DateTime.UtcNow,
                    Details = new List<ContactDetailDto>()
                    {
                        new ContactDetailDto() { Id = 1, ContactId = contacid_1, TelephoneNumber = "05555555551", EMailAddress = "muratyasar1@boylebiradresyok.com", Address = "İstanbul", DateCreated = DateTime.UtcNow },
                        new ContactDetailDto() { Id = 2, ContactId = contacid_1, TelephoneNumber = "05555555552", EMailAddress = "muratyasar2@boylebiradresyok.com", Address = "Ankara", DateCreated = DateTime.UtcNow },
                        new ContactDetailDto() { Id = 3, ContactId = contacid_1, TelephoneNumber = "05555555553", EMailAddress = "muratyasar3@boylebiradresyok.com", Address = "İzmir", DateCreated = DateTime.UtcNow }
                    }
                },
                new ContactDto() {
                    Id = Guid.NewGuid(),
                    Name = "Joe",
                    LastName = "Biden",
                    CompanyName = "White House",
                    DateCreated = DateTime.UtcNow,
                    Details = new List<ContactDetailDto>()
                    {
                        new ContactDetailDto() { Id = 4, ContactId = contacid_2, TelephoneNumber = "05555555551", EMailAddress = "joebiden1@boylebiradresyok.com", Address = "Washington", DateCreated = DateTime.UtcNow },
                        new ContactDetailDto() { Id = 5, ContactId = contacid_2, TelephoneNumber = "05555555552", EMailAddress = "joebiden1@boylebiradresyok.com", Address = "New York", DateCreated = DateTime.UtcNow },
                        new ContactDetailDto() { Id = 6, ContactId = contacid_2, TelephoneNumber = "05555555553", EMailAddress = "joebiden1@boylebiradresyok.com", Address = "Los Angeles", DateCreated = DateTime.UtcNow }
                    }
                },
                new ContactDto() {
                    Id = Guid.NewGuid(),
                    Name = "Bill",
                    LastName = "Gates",
                    CompanyName = "Microsoft",
                    DateCreated = DateTime.UtcNow,
                    Details = new List<ContactDetailDto>()
                    {
                        new ContactDetailDto() { Id = 7, ContactId = contacid_3, TelephoneNumber = "05555555551", EMailAddress = "billgates1@boylebiradresyok.com", Address = "Washington", DateCreated = DateTime.UtcNow },
                        new ContactDetailDto() { Id = 8, ContactId = contacid_3, TelephoneNumber = "05555555552", EMailAddress = "billgates1@boylebiradresyok.com", Address = "New York", DateCreated = DateTime.UtcNow },
                        new ContactDetailDto() { Id = 9, ContactId = contacid_3, TelephoneNumber = "05555555553", EMailAddress = "billgates1@boylebiradresyok.com", Address = "Los Angeles", DateCreated = DateTime.UtcNow }
                    }
                },
                new ContactDto() {
                    Id = Guid.NewGuid(),
                    Name = "Ali",
                    LastName = "Veli",
                    CompanyName = "Kendi İşi",
                    DateCreated = DateTime.UtcNow,
                    Details = new List<ContactDetailDto>()
                    {
                        new ContactDetailDto() { Id = 10, ContactId = contacid_4, TelephoneNumber = "05555555551", EMailAddress = "aliveli1@boylebiradresyok.com", Address = "İstanbul", DateCreated = DateTime.UtcNow },
                        new ContactDetailDto() { Id = 11, ContactId = contacid_4, TelephoneNumber = "05555555552", EMailAddress = "aliveli1@boylebiradresyok.com", Address = "Ankara", DateCreated = DateTime.UtcNow },
                        new ContactDetailDto() { Id = 12, ContactId = contacid_4, TelephoneNumber = "05555555553", EMailAddress = "aliveli1@boylebiradresyok.com", Address = "İzmir", DateCreated = DateTime.UtcNow }
                    }
                },
            };

            return items;
        }

        private List<ContactDetail> CreateRandomContactDetailList()
        {
            Guid contacid_1 = Guid.NewGuid();

            return new List<ContactDetail>()
            {
                new ContactDetail() { Id = 1, ContactId = contacid_1, TelephoneNumber = "05555555551", EMailAddress = "muratyasar1@boylebiradresyok.com", Address = "İstanbul", DateCreated = DateTime.UtcNow },
                new ContactDetail() { Id = 2, ContactId = contacid_1, TelephoneNumber = "05555555552", EMailAddress = "muratyasar2@boylebiradresyok.com", Address = "Ankara", DateCreated = DateTime.UtcNow },
                new ContactDetail() { Id = 3, ContactId = contacid_1, TelephoneNumber = "05555555553", EMailAddress = "muratyasar3@boylebiradresyok.com", Address = "İzmir", DateCreated = DateTime.UtcNow }
            };
        }

        private ContactDetail CreateRandomContactDetail()
        {
            Guid contacid_1 = Guid.NewGuid();

            return new ContactDetail() { Id = 1, ContactId = contacid_1, TelephoneNumber = "05555555551", EMailAddress = "muratyasar1@boylebiradresyok.com", Address = "İstanbul", DateCreated = DateTime.UtcNow };
        }

        [Fact]
        public async Task GetAllContactsAsync_ReturnsTheListOfContacts_ExpectedOkObjectResult()
        {
            // Arrange
            var expectedItems = CreateRandomContactDtoList();
            _repositoryStub.Setup(repo => repo.GetAllContactsAsync()).ReturnsAsync(expectedItems);

            // Act
            var actionResult = await _controller.GetAllContactsAsync();

            // Assert
            Assert.IsType<OkObjectResult>(actionResult as OkObjectResult);
        }

        [Fact]
        public async Task GetAllContactsAsync_ReturnsTheListOfContacts_Expected4Rows()
        {
            // Arrange
            var expectedItems = CreateRandomContactDtoList();
            _repositoryStub.Setup(repo => repo.GetAllContactsAsync()).ReturnsAsync(expectedItems);

            // Act
            var actionResult = await _controller.GetAllContactsAsync();

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult as OkObjectResult);
            var returnItems = Assert.IsType<List<ContactDto>>(result.Value);
            Assert.Equal(expectedItems, returnItems);
            Assert.Equal(4, returnItems.Count());
        }

        [Fact]
        public async Task GetAllContactsAsync_ReturnsTheListOfContacts_ExpectedNotNullResult()
        {
            // Arrange
            var expectedItems = CreateRandomContactDtoList();
            _repositoryStub.Setup(repo => repo.GetAllContactsAsync()).ReturnsAsync(expectedItems);

            // Act
            var actionResult = await _controller.GetAllContactsAsync();

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult as OkObjectResult);
            var returnItems = Assert.IsType<List<ContactDto>>(result.Value);
            Assert.NotNull(returnItems);
        }

        [Fact]
        public async Task GetContactByIdAsync_ReturnsTheSpecificContact_ExpectedOkObjectResult()
        {
            // Arrange
            var expectedItem = CreateRandomContactDto();
            _repositoryStub.Setup(repo => repo.GetContactByIdAsync(expectedItem.Id)).ReturnsAsync(expectedItem);

            // Act
            var actionResult = await _controller.GetContactByIdAsync(expectedItem.Id);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult as OkObjectResult);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200")]
        public async Task GetContactByIdAsync_ReturnsTheSpecificContact_ExpectedNotFoundResult(string guid)
        {
            // Arrange
            var expectedItem = CreateRandomContactDto();
                        
            var invalidGuid = new Guid(guid);

            _repositoryStub.Setup(repo => repo.GetContactByIdAsync(invalidGuid)).ReturnsAsync(() => null);

            // Act
            var actionResult = await _controller.GetContactByIdAsync(invalidGuid);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);            
        }

        [Fact]
        public async Task GetContactDetailByContactIdAsync_ReturnsTheContactDetailListBasedOnContactId_ExpectedOkObjectResult()
        {
            // Arrange
            var expectedItem = CreateRandomContactDetailList();

            _repositoryStub.Setup(repo => repo.GetContactDetailByContactIdAsync(expectedItem.FirstOrDefault().ContactId)).ReturnsAsync(expectedItem);

            // Act
            var actionResult = await _controller.GetContactDetailByContactIdAsync(expectedItem.FirstOrDefault().ContactId);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result as OkObjectResult);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200")]
        public async Task GetContactDetailByContactIdAsync_ReturnsTheContactDetailListBasedOnContactId_ExpectedNotFoundResult(string guid)
        {
            // Arrange
            var expectedItem = CreateRandomContactDetailList();

            var invalidGuid = new Guid(guid);

            _repositoryStub.Setup(repo => repo.GetContactDetailByContactIdAsync(invalidGuid)).ReturnsAsync(() => null);

            // Act
            var actionResult = await _controller.GetContactDetailByContactIdAsync(invalidGuid);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetContactDetailByIdAsync_ReturnsTheSpecificContactDetailBasedOnId_ExpectedOkObjectResult()
        {
            // Arrange
            var expectedItem = CreateRandomContactDetail();

            _repositoryStub.Setup(repo => repo.GetContactDetailByIdAsync(expectedItem.Id)).ReturnsAsync(expectedItem);

            // Act
            var actionResult = await _controller.GetContactDetailByIdAsync(expectedItem.Id);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult as OkObjectResult);
        }

        [Theory]
        [InlineData(100)]
        public async Task GetContactDetailByIdAsync_ReturnsTheSpecificContactDetailBasedOnId_ExpectedNotFoundResult(long id)
        {
            // Arrange
            var expectedItem = CreateRandomContactDetail();

            var invalidId = id;

            _repositoryStub.Setup(repo => repo.GetContactDetailByIdAsync(invalidId)).ReturnsAsync(() => null);

            // Act
            var actionResult = await _controller.GetContactDetailByIdAsync(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async Task AddContactAsync_ReturnsTheInvalidModelState_ExpectedInvalidModelState()
        {
            // Arrange
            var contactToInsert = new ContactDtoInsert() { 
                Name = Guid.NewGuid().ToString(), 
                LastName = Guid.NewGuid().ToString(), 
                CompanyName = Guid.NewGuid().ToString(), 
                TelephoneNumber = Guid.NewGuid().ToString(),
                EMailAddress = Guid.NewGuid().ToString()
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Contact/AddContactAsync");
            request.Content = new StringContent(JsonConvert.SerializeObject(contactToInsert), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 400, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task AddContactAsync_ReturnsTheSuccess_ExpectedOkObjectResult()
        {
            // Arrange
            var contactToInsert = new ContactDtoInsert()
            {
                Name = "Test",
                LastName = "Test",
                CompanyName = "Test",
                TelephoneNumber = "24234",
                EMailAddress = "sdf@asd.com",
                Address = "adres test"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Contact/AddContactAsync");
            request.Content = new StringContent(JsonConvert.SerializeObject(contactToInsert), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 200, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task AddContactDetailAsync_ReturnsTheInvalidModelState_ExpectedInvalidModelState()
        {
            // Arrange
            var contactDetailToInsert = new ContactDetailDtoInsert()
            {
                ContactId = Guid.NewGuid(),
                TelephoneNumber = "324234234",
                EMailAddress = "dsfsdf@asdas.com"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Contact/AddContactDetailAsync");
            request.Content = new StringContent(JsonConvert.SerializeObject(contactDetailToInsert), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 400, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task AddContactDetailAsync_ReturnsTheSuccess_ExpectedOkObjectResult()
        {
            // Arrange
            var contactDetailToInsert = new ContactDetailDtoInsert()
            {
                ContactId = Guid.NewGuid(),
                TelephoneNumber = "324234234",
                EMailAddress = "dsfsdf@asdas.com",
                Address = "test address"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Contact/AddContactDetailAsync");
            request.Content = new StringContent(JsonConvert.SerializeObject(contactDetailToInsert), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 200, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task DeleteContacDetailtAsync_ReturnsOkObjectResult_ExpectedOkObjectResult()
        {
            // Arrange
            var contactDetailToDelete = 28;

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/Contact/DeleteContacDetailtAsync/{contactDetailToDelete.ToString()}");
            // I think it is unnecessary but haven't tested yet.
            request.Content = new StringContent(JsonConvert.SerializeObject(contactDetailToDelete), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 200, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task DeleteContacDetailtAsync_ReturnsTheNotFound_ExpectedNotFoundResult()
        {
            // Arrange
            var contactDetailToDelete = 28;

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/Contact/DeleteContacDetailtAsync/{contactDetailToDelete.ToString()}");
            // I think it is unnecessary but haven't tested yet.
            request.Content = new StringContent(JsonConvert.SerializeObject(contactDetailToDelete), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 404, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task DeleteContacDetailtAsync_ReturnsTheBadRequestResult_ExpectedBadRequestResult()
        {
            // Arrange
            var contactDetailToDelete = 28;

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/Contact/DeleteContacDetailtAsync/{contactDetailToDelete.ToString()}");
            // I think it is unnecessary but haven't tested yet.
            request.Content = new StringContent(JsonConvert.SerializeObject(contactDetailToDelete), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 400, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task DeleteContactAsync_ReturnsOkObjectResult_ExpectedOkObjectResult()
        {
            // Arrange
            var contacToDelete = Guid.NewGuid();

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/Contact/DeleteContactAsync/{contacToDelete.ToString()}");
            // I think it is unnecessary but haven't tested yet.
            request.Content = new StringContent(JsonConvert.SerializeObject(contacToDelete), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 200, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task DeleteContactAsync_ReturnsTheNotFound_ExpectedNotFoundResult()
        {
            // Arrange
            var contacToDelete = Guid.NewGuid();

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/Contact/DeleteContactAsync/{contacToDelete.ToString()}");
            // I think it is unnecessary but haven't tested yet.
            request.Content = new StringContent(JsonConvert.SerializeObject(contacToDelete), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 404, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task DeleteContactAsync_ReturnsTheBadRequestResult_ExpectedBadRequestResult()
        {
            // Arrange
            var contacToDelete = Guid.NewGuid();

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/Contact/DeleteContactAsync/{contacToDelete.ToString()}");
            // I think it is unnecessary but haven't tested yet.
            request.Content = new StringContent(JsonConvert.SerializeObject(contacToDelete), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 400, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task UpdateContactAsync_ReturnsTheInvalidModelState_ExpectedInvalidModelState()
        {
            // Arrange
            var contactToUpdate = new ContactDtoUpdate() { Id = Guid.NewGuid(), Name = "Test Update", CompanyName = "Test Company" };

            var request = new HttpRequestMessage(HttpMethod.Put, "/api/Contact/UpdateContactAsync");
            request.Content = new StringContent(JsonConvert.SerializeObject(contactToUpdate), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 400, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task UpdateContactAsync_ReturnsTheBadRequestResult_ExpectedBadRequestResult()
        {
            // Arrange
            var contactToUpdate = new ContactDtoUpdate() { Id = Guid.NewGuid(), Name = "Test Update", LastName = "Test Lastname", CompanyName = "Test Company" };

            var request = new HttpRequestMessage(HttpMethod.Put, "/api/Contact/UpdateContactAsync");
            request.Content = new StringContent(JsonConvert.SerializeObject(contactToUpdate), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 400, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task UpdateContactAsync_ReturnsTheSuccess_ExpectedOkObjectResult()
        {
            // Arrange
            var contactToUpdate = new ContactDtoUpdate() { Id = Guid.NewGuid(), Name = "Test Update", LastName = "Test Lastname", CompanyName = "Test Company" };

            var request = new HttpRequestMessage(HttpMethod.Put, "/api/Contact/UpdateContactAsync");
            request.Content = new StringContent(JsonConvert.SerializeObject(contactToUpdate), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 200, actual: ((int)response.StatusCode));
        }
    }
}
