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

            if (!result.Any())
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetContactByIdAsync/{id}")]
        public async Task<IActionResult> GetContactByIdAsync(Guid id)
        {
            var result = await _contactOperationService.GetContactByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetContactDetailByContactIdAsync/{id}")]
        public async Task<ActionResult<ICollection<ContactDetail>>> GetContactDetailByContactIdAsync(Guid id)
        {
            var result = await _contactOperationService.GetContactDetailByContactIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetContactDetailByIdAsync/{id}")]
        public async Task<IActionResult> GetContactDetailByIdAsync(long id)
        {
            var result = await _contactOperationService.GetContactDetailByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Route("AddContactAsync")]
        public async Task<IActionResult> AddContactAsync([FromBody] ContactDtoInsert contactDtoInsert)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _contactOperationService.AddContactAsync(contactDtoInsert);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Route("AddContactDetailAsync")]
        public async Task<IActionResult> AddContactAsync([FromBody] ContactDetailDtoInsert contactDetailDtoInsert)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _contactOperationService.AddContactDetailAsync(contactDetailDtoInsert);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteContacDetailtAsync/{id}")]
        public async Task<IActionResult> DeleteContacDetailtAsync(long id)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _contactOperationService.DeleteContacDetailtAsync(id);

            if (result == false)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteContactAsync/{id}")]
        public async Task<IActionResult> DeleteContactAsync(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _contactOperationService.DeleteContactAsync(id);

            if (result == false)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateContactAsync")]
        public async Task<IActionResult> UpdateContactAsync([FromBody] ContactDtoUpdate contactDtoUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _contactOperationService.UpdateContactAsync(contactDtoUpdate);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateContactDetailAsync")]
        public async Task<IActionResult> UpdateContactDetailAsync([FromBody] ContactDetailDtoUpdate contactDetailDtoUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });

            var result = await _contactOperationService.UpdateContactDetailAsync(contactDetailDtoUpdate);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

    }
}
