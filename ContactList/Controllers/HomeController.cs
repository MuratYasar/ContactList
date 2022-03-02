using ContactList.Models;
using Contracts;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggerManager _logger;

        public HomeController(ILoggerManager logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<ContactDto> contactList = new List<ContactDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5000/getallcontact"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        contactList = JsonConvert.DeserializeObject<List<ContactDto>>(apiResponse);

                        _logger.LogInfo($"List of contacts has been displayed from client UI.");
                    }
                }
            }

            return View(contactList);
        }

        public ViewResult AddContact() => View();

        [HttpPost]
        public async Task<IActionResult> AddContact(ContactDtoInsert contactDtoInsert)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            ContactDtoInsert result = new ContactDtoInsert();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(contactDtoInsert), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5000/addcontact", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ContactDtoInsert>(apiResponse);
                    }
                }
            }
            return View(result);
        }

        public ViewResult AddContactDetail(Guid id)
        {
            ViewBag.ContactId = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddContactDetail(ContactDetailDtoInsert contactDetailDtoInsert)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            ContactDetailDtoInsert result = new ContactDetailDtoInsert();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(contactDetailDtoInsert), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5000/addcontactdetail", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ContactDetailDtoInsert>(apiResponse);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UpdateContact(Guid id)
        {
            ContactDto result = new ContactDto();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5000/getcontactbyid/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ContactDto>(apiResponse);

                        _logger.LogInfo($"{id.ToString()} - contact has been displayed from client UI.");
                    }
                }
            }

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact(ContactDtoUpdate contactDtoUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            ContactDtoUpdate result = new ContactDtoUpdate();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(contactDtoUpdate), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:5000/updatecontact", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ContactDtoUpdate>(apiResponse);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UpdateContactDetail(long id)
        {
            ContactDetailDto result = new ContactDetailDto();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5000/getcontactdetailbyid/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ContactDetailDto>(apiResponse);
                    }
                }
            }

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContactDetail(ContactDetailDtoUpdate contactDetailDtoUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            ContactDetailDtoUpdate result = new ContactDetailDtoUpdate();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(contactDetailDtoUpdate), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:5000/updatecontactdetail", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ContactDetailDtoUpdate>(apiResponse);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            bool result = false;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5000/deletecontact/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<bool>(apiResponse);
                    }
                }
            }

            if (result == true)
            {
                return View("DeleteContactSuccess");
            }
            else
            {
                return View("DeleteContactFailed");
            }
        }

        public ViewResult DeleteContactSuccess() => View();

        public ViewResult DeleteContactFailed() => View();

        [HttpPost]
        public async Task<IActionResult> DeleteContactDetail(long id)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            bool result = false;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5000/deletecontactdetail/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<bool>(apiResponse);
                    }
                }
            }

            if (result == true)
            {
                return View("DeleteContactDetailSuccess");
            }
            else
            {
                return View("DeleteContactDetailFailed");
            }
        }

        public ViewResult DeleteContactDetailSuccess() => View();

        public ViewResult DeleteContactDetailFailed() => View();

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
