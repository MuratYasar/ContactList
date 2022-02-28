using Contracts;
using Entities.DataModel;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILoggerManager _logger;

        private static readonly HttpClient _httpClient = new HttpClient();

        public ReportController(ILoggerManager logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<ReportDto> reportList = new List<ReportDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5000/getreportlist"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reportList = JsonConvert.DeserializeObject<List<ReportDto>>(apiResponse);
                }
            }

            _logger.LogInfo($"List of reports has been displayed from client UI.");

            return View(reportList);
        }

        public ViewResult AddReport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReport(ReportDtoInsert reportDtoInsert)
        {
            ReportDtoInsert result = new ReportDtoInsert();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reportDtoInsert), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5000/requestanewreport", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ReportDtoInsert>(apiResponse);
                }
            }

            return View(result);
        }
    }
}
