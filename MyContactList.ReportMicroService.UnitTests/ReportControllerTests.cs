using Entities.DataModel;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Moq;
using Newtonsoft.Json;
using Report.API.Controllers;
using Report.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyContactList.ReportMicroService.UnitTests
{
    public class ReportControllerTests : IClassFixture<Initialization>
    {
        protected TestServer _testServer;
        private readonly Mock<IReportOperation> _repositoryStub;
        private readonly ReportController _controller;

        public ReportControllerTests(Initialization initialization)
        {
            _repositoryStub = new Mock<IReportOperation>();
            _controller = new ReportController(_repositoryStub.Object);
            _testServer = initialization.TestServer;
        }

        private ReportStatus CreateRandomReportStatus()
        {
            return new()
            {
                Id = 1,
                Name = "Elma",
                DateCreated = DateTime.UtcNow
            };
        }

        private List<ReportStatus> CreateRandomReportStatusList()
        {
            List<ReportStatus> items = new List<ReportStatus>()
            {
                new ReportStatus() { Id = 1, Name = "Hazırlanıyor", DateCreated = DateTime.UtcNow },
                new ReportStatus() { Id = 2, Name = "Tamamlandı", DateCreated = DateTime.UtcNow }
            };

            return items;
        }

        private ReportDto CreateRandomReportDto()
        {
            return new()
            {
                Id = 1,
                ReportStatusId = 1,
                ReportStatusName = "Tamamlandı",
                ReportName = "Test1",
                Address = "İstanbul",
                ContactCount = 0,
                PhoneRecordCount = 0,
                DateRequested = DateTime.UtcNow
            };
        }

        private List<ReportDto> CreateRandomReportDtoList()
        {
            List<ReportDto> items = new List<ReportDto>()
            {
                new ReportDto() {  
                    Id = 1,
                    ReportStatusId = 2,
                    ReportStatusName = "Tamamlandı",
                    ReportName = "Test1",
                    Address = "İstanbul",
                    ContactCount = 1,
                    PhoneRecordCount = 0,
                    DateRequested = DateTime.UtcNow 
                },
                new ReportDto() {
                    Id = 2,
                    ReportStatusId = 1,
                    ReportStatusName = "Hazırlanıyor",
                    ReportName = "Test2",
                    Address = "İstanbul",
                    ContactCount = 0,
                    PhoneRecordCount = 1,
                    DateRequested = DateTime.UtcNow
                },
                new ReportDto() {
                    Id = 3,
                    ReportStatusId = 1,
                    ReportStatusName = "Hazırlanıyor",
                    ReportName = "Test3",
                    Address = "Türkiye",
                    ContactCount = 1,
                    PhoneRecordCount = 1,
                    DateRequested = DateTime.UtcNow
                },
                new ReportDto() {
                    Id = 4,
                    ReportStatusId = 2,
                    ReportStatusName = "Tamamlandı",
                    ReportName = "Test4",
                    Address = "Türkiye",
                    ContactCount = 2,
                    PhoneRecordCount = 3,
                    DateRequested = DateTime.UtcNow
                }
            };

            return items;
        }

        [Fact]
        public async Task GetReportStatusListAsync_ReturnsTheListOfReportStatus_ExpectedOkObjectResult()
        {
            // Arrange
            var expectedItems = CreateRandomReportStatusList();
            _repositoryStub.Setup(repo => repo.GetReportStatusListAsync()).ReturnsAsync(expectedItems);            

            // Act
            var actionResult = await _controller.GetReportStatusListAsync();

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result as OkObjectResult);
        }

        [Fact]
        public async Task GetReportStatusListAsync_ReturnsTheListOfReportStatus_Expected2Rows()
        {
            // Arrange
            var expectedItems = CreateRandomReportStatusList();
            _repositoryStub.Setup(repo => repo.GetReportStatusListAsync()).ReturnsAsync(expectedItems);

            // Act
            var actionResult = await _controller.GetReportStatusListAsync();

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult.Result as OkObjectResult);
            var returnItems = Assert.IsType<List<ReportStatus>>(result.Value);
            Assert.Equal(expectedItems, returnItems);
            Assert.Equal(2, returnItems.Count());
        }

        [Fact]
        public async Task GetReportStatusListAsync_ReturnsTheListOfReportStatus_ExpectedNotNulResult()
        {
            // Arrange
            var expectedItems = CreateRandomReportStatusList();
            _repositoryStub.Setup(repo => repo.GetReportStatusListAsync()).ReturnsAsync(expectedItems);

            // Act
            var actionResult = await _controller.GetReportStatusListAsync();

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult.Result as OkObjectResult);
            var returnItems = Assert.IsType<List<ReportStatus>>(result.Value);
            Assert.NotNull(returnItems);
        }

        [Fact]
        public async Task GetReportStatusListAsync_ReturnsTheReportStatusId_Expected1AsIdNumber()
        {
            // Arrange
            var expectedItems = CreateRandomReportStatusList();
            _repositoryStub.Setup(repo => repo.GetReportStatusListAsync()).ReturnsAsync(expectedItems);

            // Act
            var actionResult = await _controller.GetReportStatusListAsync();

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult.Result as OkObjectResult);
            var returnItems = Assert.IsType<List<ReportStatus>>(result.Value);
            Assert.Equal(expected: 1, actual: returnItems.ToList()[0].Id);
        }

        [Fact]
        public async Task GetReportStatusListAsync_ReturnsTheReportStatusId_Expected2AsIdNumber()
        {
            // Arrange
            var expectedItems = CreateRandomReportStatusList();
            _repositoryStub.Setup(repo => repo.GetReportStatusListAsync()).ReturnsAsync(expectedItems);

            // Act
            var actionResult = await _controller.GetReportStatusListAsync();

            // Assert            
            var result = Assert.IsType<OkObjectResult>(actionResult.Result as OkObjectResult);
            var returnItems = Assert.IsType<List<ReportStatus>>(result.Value);
            Assert.Equal(expected: 1, actual: returnItems.ToList()[0].Id);
        }

        [Fact]
        public async Task AddReportAsync_ReturnsTheInvalidModelState_ExpectedInvalidModelState()
        {
            // Arrange
            var reportToInsert = new ReportDtoInsert() { Address = "İstanbul", ReportStatusId = 1 };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Report/AddReportAsync");            
            request.Content = new StringContent(JsonConvert.SerializeObject(reportToInsert), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 400, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task AddReportAsync_ReturnsTheSuccess_ExpectedOkObjectResult()
        {
            // Arrange
            var reportToInsert = new ReportDtoInsert() { ReportName = "Test", Address = "İstanbul", ReportStatusId = 1 };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Report/AddReportAsync");
            request.Content = new StringContent(JsonConvert.SerializeObject(reportToInsert), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 200, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task GetReportsAsync_ReturnsTheListOfReports_ExpectedOkObjectResult()
        {
            // Arrange
            var expectedItems = CreateRandomReportDtoList();
            _repositoryStub.Setup(repo => repo.GetReportsAsync()).ReturnsAsync(expectedItems);

            // Act
            var actionResult = await _controller.GetReportsAsync();

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result as OkObjectResult);
        }

        [Fact]
        public async Task GetReportsAsync_ReturnsTheListOfReports_Expected4Rows()
        {
            // Arrange
            var expectedItems = CreateRandomReportDtoList();
            _repositoryStub.Setup(repo => repo.GetReportsAsync()).ReturnsAsync(expectedItems);

            // Act
            var actionResult = await _controller.GetReportsAsync();

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult.Result as OkObjectResult);
            var returnItems = Assert.IsType<List<ReportDto>>(result.Value);
            Assert.Equal(expectedItems, returnItems);
            Assert.Equal(4, returnItems.Count());
        }

        [Fact]
        public async Task GetReportsAsync_ReturnsTheListOfReports_ExpectedNotNulResult()
        {
            // Arrange
            var expectedItems = CreateRandomReportDtoList();
            _repositoryStub.Setup(repo => repo.GetReportsAsync()).ReturnsAsync(expectedItems);

            // Act
            var actionResult = await _controller.GetReportsAsync();

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult.Result as OkObjectResult);
            var returnItems = Assert.IsType<List<ReportDto>>(result.Value);
            Assert.NotNull(returnItems);
        }

        [Fact]
        public async Task DeleteReportByIdAsync_ReturnsOkObjectResult_ExpectedOkObjectResult()
        {
            // Arrange
            var reportToDelete = 28;

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/Report/DeleteReportByIdAsync/{reportToDelete.ToString()}");
            request.Content = new StringContent(JsonConvert.SerializeObject(reportToDelete), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 200, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task DeleteReportByIdAsync_ReturnsTheNotFound_ExpectedNotFoundResult()
        {
            // Arrange
            var reportToDelete = 28;

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/Report/DeleteReportByIdAsync/{reportToDelete.ToString()}");
            request.Content = new StringContent(JsonConvert.SerializeObject(reportToDelete), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 400, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task DeleteReportByIdAsync_ReturnsTheBadRequestResult_ExpectedBadRequestResult()
        {
            // Arrange
            var reportToDelete = 28;

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/Report/DeleteReportByIdAsync/{reportToDelete.ToString()}");
            request.Content = new StringContent(JsonConvert.SerializeObject(reportToDelete), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 404, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task UpdateReportAsync_ReturnsTheInvalidModelState_ExpectedInvalidModelState()
        {
            // Arrange
            var reportToUpdate = new ReportDtoUpdate() { Id = 270, ReportStatusId = 1, ReportName = "Test Ola", Address = "İstanbul", ContactCount = 0 };

            var request = new HttpRequestMessage(HttpMethod.Put, "/api/Report/UpdateReportAsync");
            request.Content = new StringContent(JsonConvert.SerializeObject(reportToUpdate), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 400, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task UpdateReportAsync_ReturnsTheBadRequestResult_ExpectedBadRequestResult()
        {
            // Arrange
            var reportToUpdate = new ReportDtoUpdate() { Id = 27, ReportStatusId = 1, ReportName = "Test Ola", Address = "İstanbul", ContactCount = 0 };

            var request = new HttpRequestMessage(HttpMethod.Put, "/api/Report/UpdateReportAsync");
            request.Content = new StringContent(JsonConvert.SerializeObject(reportToUpdate), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(expected: 404, actual: ((int)response.StatusCode));
        }

        [Fact]
        public async Task UpdateReportAsync_ReturnsTheSuccess_ExpectedOkObjectResult()
        {
            // Arrange
            var reportToUpdate = new ReportDtoUpdate() { Id = 27, ReportStatusId = 1, ReportName = "Test Ola", Address = "İstanbul", ContactCount = 10, PhoneRecordCount = 10 };

            var request = new HttpRequestMessage(HttpMethod.Put, "/api/Report/UpdateReportAsync");
            request.Content = new StringContent(JsonConvert.SerializeObject(reportToUpdate), Encoding.Default, "application/json");

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
