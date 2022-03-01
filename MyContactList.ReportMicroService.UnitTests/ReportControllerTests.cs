using Contracts;
using Entities.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Report.API;
using Report.API.Controllers;
using Report.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyContactList.ReportMicroService.UnitTests
{
    public class ReportControllerTests
    {
        private readonly Mock<IReportOperation> repositoryStub = new();

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

        [Fact]
        public async Task GetReportStatusListAsync_ReturnsTheListOfReportStatus_Expected2Rows()
        {
            // Arrange
            var expectedItems = CreateRandomReportStatusList();

            repositoryStub.Setup(repo => repo.GetReportStatusListAsync()).ReturnsAsync(expectedItems);

            var controller = new ReportController(repositoryStub.Object);

            // Act
            var actionResult = await controller.GetReportStatusListAsync();

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var returnRows = result.Value as IEnumerable<ReportStatus>;
            Assert.Equal(expectedItems, returnRows);
            Assert.Equal(expectedItems.Count, returnRows.Count());
        }

        [Fact]
        public async Task GetReportStatusListAsync_ReturnsTheListOfReportStatus_ExpectedNotNulResult()
        {
            // Arrange
            var expectedItems = CreateRandomReportStatusList();

            repositoryStub.Setup(repo => repo.GetReportStatusListAsync()).ReturnsAsync(expectedItems);

            var controller = new ReportController(repositoryStub.Object);

            // Act
            var result = await controller.GetReportStatusListAsync();

            // Assert            
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetReportStatusListAsync_ReturnsTheReportStatusId_Expected1AsIdNumber()
        {
            // Arrange
            var expectedItems = CreateRandomReportStatusList();

            repositoryStub.Setup(repo => repo.GetReportStatusListAsync()).ReturnsAsync(expectedItems);

            var controller = new ReportController(repositoryStub.Object);

            // Act
            var actionResult = await controller.GetReportStatusListAsync();

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var returnRows = result.Value as IEnumerable<ReportStatus>;
            Assert.Equal(expected: 1, actual: returnRows.ToList()[0].Id);
        }

        [Fact]
        public async Task GetReportStatusListAsync_ReturnsTheReportStatusId_Expected2AsIdNumber()
        {
            // Arrange
            var expectedItems = CreateRandomReportStatusList();

            repositoryStub.Setup(repo => repo.GetReportStatusListAsync()).ReturnsAsync(expectedItems);

            var controller = new ReportController(repositoryStub.Object);

            // Act
            var actionResult = await controller.GetReportStatusListAsync();

            // Assert            
            var result = actionResult.Result as OkObjectResult;
            var returnRows = result.Value as IEnumerable<ReportStatus>;
            Assert.Equal(expected: 1, actual: returnRows.ToList()[0].Id);
        }
    }
}
