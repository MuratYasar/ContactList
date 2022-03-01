using Contracts;
using Entities.DataModel;
using Moq;
using Report.API.Controllers;
using Report.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task GetReportStatusListAsync_Returns_The_List_Of_ReportStatus_Obejct()
        {
            // Arrange
            var expectedItems = CreateRandomReportStatusList();

            repositoryStub.Setup(repo => repo.GetReportStatusListAsync()).ReturnsAsync(expectedItems);

            var controller = new ReportController(repositoryStub.Object);

            // Act
            var result = await controller.GetReportStatusListAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetReportStatusListAsync_Returns_The_List_Of_ReportStatus_NotNul()
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
        public async Task GetReportStatusListAsync_Returns_The_ReportStatus_Id_1()
        {
            // Arrange
            var expectedItems = CreateRandomReportStatusList();

            repositoryStub.Setup(repo => repo.GetReportStatusListAsync()).ReturnsAsync(expectedItems);

            var controller = new ReportController(repositoryStub.Object);

            // Act
            var result = await controller.GetReportStatusListAsync();

            // Assert            
            Assert.Equal(expected: 1, actual: result[0].Id);
        }

        [Fact]
        public async Task GetReportStatusListAsync_Returns_The_ReportStatus_Id_2()
        {
            // Arrange
            var expectedItems = CreateRandomReportStatusList();

            repositoryStub.Setup(repo => repo.GetReportStatusListAsync()).ReturnsAsync(expectedItems);

            var controller = new ReportController(repositoryStub.Object);

            // Act
            var result = await controller.GetReportStatusListAsync();

            // Assert            
            Assert.Equal(expected: 2, actual: result[1].Id);
        }
    }
}
