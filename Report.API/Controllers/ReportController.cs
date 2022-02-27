using Entities.DTOs;
using Entities.ReportConsumeModel;
using Microsoft.AspNetCore.Mvc;
using Report.DAL.Abstract;
using ReportBusConfigurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportOperation _reportOperationService;

        public ReportController(IReportOperation reportOperationService)
        {
            _reportOperationService = reportOperationService;
        }

        [HttpPost]
        [Route("AddReportAsync")]
        public async Task<IActionResult> AddReportAsync(ReportDtoInsert reportDtoInsert)
        {
            var reportInserted = _reportOperationService.AddReportAsync(reportDtoInsert);

            var bus = BusConfigurator.ConfigureBus();
            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}/{RabbitMqConstants.ConsumerQueue}");
            var endPoint = await bus.GetSendEndpoint(sendToUri);
            await endPoint.Send<IReportConsumer>(reportDtoInsert);
            return Ok("Your report request has been received ");
        }

        [HttpGet]
        [Route("GetReportsAsync")]
        public async Task<List<ReportDto>> GetReportsAsync()
        {
            return await _reportOperationService.GetReportsAsync();
        }

        [HttpDelete]
        [Route("DeleteReportByIdAsync/{id}")]
        public async Task<IActionResult> DeleteReportByIdAsync(long id)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _reportOperationService.DeleteReportByIdAsync(id);

            return Ok(result);
        }

    }
}
