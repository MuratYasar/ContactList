using Contact.DAL.Abstract;
using Entities.DataModel;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactOperation _contactOperationService;

        public ContactController(IContactOperation contactOperationService)
        {
            _contactOperationService = contactOperationService;
        }

        [HttpGet]
        [Route("GetAllContactsAsync")]
        //public async Task<ICollection<ContactDto>> GetAllContactsAsync()
        public async Task<IActionResult> GetAllContactsAsync()
        {
            var result = await _contactOperationService.GetAllContactsAsync();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]
        [Route("GetContactByIdAsync/{id}")]
        public async Task<ContactDto> GetContactByIdAsync(Guid id)
        {
            return await _contactOperationService.GetContactByIdAsync(id);
        }

        [HttpGet]
        [Route("GetContactDetailByContactIdAsync/{id}")]
        public async Task<ICollection<ContactDetail>> GetContactDetailByContactIdAsync(Guid id)
        {
            return await _contactOperationService.GetContactDetailByContactIdAsync(id);
        }

        [HttpGet]
        [Route("GetContactDetailByIdAsync/{id}")]
        public async Task<ContactDetail> GetContactDetailByIdAsync(long id)
        {
            return await _contactOperationService.GetContactDetailByIdAsync(id);
        }

        [HttpPost]
        [Route("AddContactAsync")]
        public async Task<IActionResult> AddContactAsync([FromBody] ContactDtoInsert contactDtoInsert)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _contactOperationService.AddContactAsync(contactDtoInsert);

            return Ok(result);
        }

        [HttpPost]
        [Route("AddContactDetailAsync")]
        public async Task<IActionResult> AddContactAsync([FromBody] ContactDetailDtoInsert contactDetailDtoInsert)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _contactOperationService.AddContactDetailAsync(contactDetailDtoInsert);

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteContacDetailtAsync/{id}")]
        public async Task<IActionResult> DeleteContacDetailtAsync(long id)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _contactOperationService.DeleteContacDetailtAsync(id);

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteContactAsync/{id}")]
        public async Task<IActionResult> DeleteContactAsync(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _contactOperationService.DeleteContactAsync(id);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateContactAsync")]
        public async Task<IActionResult> UpdateContactAsync([FromBody] ContactDtoUpdate contactDtoUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _contactOperationService.UpdateContactAsync(contactDtoUpdate);
            
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateContactDetailAsync")]
        public async Task<IActionResult> UpdateContactDetailAsync([FromBody] ContactDetailDtoUpdate contactDetailDtoUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _contactOperationService.UpdateContactDetailAsync(contactDetailDtoUpdate);

            return Ok(result);
        }

    }
}
