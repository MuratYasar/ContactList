using Entities.DataModel;
using Entities.DTOs;
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

        [HttpGet]
        [Route("GetReportStatusListAsync")]
        public async Task<ActionResult<List<ReportStatus>>> GetReportStatusListAsync()
        {
            var reportStatusValues = await _reportOperationService.GetReportStatusListAsync();

            if (!reportStatusValues.Any())
                return NotFound();

            return Ok(reportStatusValues);
        }

        [HttpPost]
        [Route("AddReportAsync")]
        public async Task<IActionResult> AddReportAsync(ReportDtoInsert reportDtoInsert)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var reportInserted = await _reportOperationService.AddReportAsync(reportDtoInsert);

            var bus = BusConfigurator.ConfigureBus();
            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}/{RabbitMqConstants.ReportConsumerQueue}");
            var endPoint = await bus.GetSendEndpoint(sendToUri);
            await endPoint.Send<Entities.DataModel.Report>(reportInserted);
            return Ok(reportDtoInsert);
        }

        [HttpPost]
        [Route("PrepareFinalReport")]
        public async Task<IActionResult> PrepareFinalReport(Entities.DataModel.Report report)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _reportOperationService.PrepareFinalReportAsync(report);
            
            return Ok(result);
        }

        [HttpGet]
        [Route("GetReportsAsync")]
        public async Task<ActionResult<List<ReportDto>>> GetReportsAsync()
        {
            var reportList = await _reportOperationService.GetReportsAsync();

            if (!reportList.Any())
                return NotFound();

            return Ok(reportList);
        }

        [HttpDelete]
        [Route("DeleteReportByIdAsync/{id}")]
        public async Task<IActionResult> DeleteReportByIdAsync(long id)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _reportOperationService.DeleteReportByIdAsync(id);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateReportAsync")]
        public async Task<IActionResult> UpdateReportAsync([FromBody] ReportDtoUpdate reportDtoUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _reportOperationService.UpdateReportAsync(reportDtoUpdate);
            if (result == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
