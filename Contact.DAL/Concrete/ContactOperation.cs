using Contact.DAL.Abstract;
using Contracts;
using Entities.DataModel;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contact.DAL.Concrete
{
    public class ContactOperation : IContactOperation
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;

        public ContactOperation(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> AddContactAsync(ContactDtoInsert contactDtoInsert)
        {
            var entityContact = await _unitOfWork.GetRepository<Entities.DataModel.Contact>().AddReturnEntityAsync(
                new Entities.DataModel.Contact()
                {
                    Name = contactDtoInsert.Name,
                    LastName = contactDtoInsert.LastName,
                    CompanyName = contactDtoInsert.CompanyName,
                    DateCreated = DateTime.UtcNow
                });

            bool result = await _unitOfWork.SaveChangesAsync();

            if (result == true)
            {
                _logger.LogInfo($"{entityContact.entity.Id.ToString()} - new contact added.");

                var entityContactDetail = await _unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().AddReturnEntityAsync(
                    new Entities.DataModel.ContactDetail()
                    {
                        ContactId = entityContact.entity.Id,
                        TelephoneNumber = contactDtoInsert.TelephoneNumber,
                        EMailAddress = contactDtoInsert.EMailAddress,
                        Address = contactDtoInsert.Address,
                        DateCreated = DateTime.UtcNow
                    });

                bool resultContactDetail = await _unitOfWork.SaveChangesAsync();

                if (resultContactDetail == true)
                {
                    _logger.LogInfo($"{entityContactDetail.entity.Id.ToString()} - new contact detail added for {entityContact.entity.Id.ToString()}.");

                    return true;
                }
                else
                {
                    _logger.LogError($"An error has been occurred while adding a new contact detail for the contact ({entityContact.entity.Id.ToString()}).");

                    return false;
                }
            }
            else
            {
                _logger.LogError($"An error has been occurred while adding a new contact.");

                return false;
            }
        }
    
        public async Task<bool> AddContactDetailAsync(ContactDetailDtoInsert contactDetailDtoInsert)
        {
            var entityContactDetail = await _unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().AddReturnEntityAsync(
                    new Entities.DataModel.ContactDetail()
                    {
                        ContactId = contactDetailDtoInsert.ContactId,
                        TelephoneNumber = contactDetailDtoInsert.TelephoneNumber,
                        EMailAddress = contactDetailDtoInsert.EMailAddress,
                        Address = contactDetailDtoInsert.Address,
                        DateCreated = DateTime.UtcNow
                    });

            bool resultContactDetail = await _unitOfWork.SaveChangesAsync();

            if (resultContactDetail == true)
            {
                _logger.LogInfo($"{entityContactDetail.entity.Id.ToString()} - new contact detail added for the contact ({contactDetailDtoInsert.ContactId.ToString()}).");

                return true;
            }
            else
            {
                _logger.LogError($"An error has been occurred while adding a new contact detail for the contact ({contactDetailDtoInsert.ContactId.ToString()}).");

                return false;
            }
        }    

        public async Task<bool> DeleteContacDetailtAsync(long id)
        {
            if (!_unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().Exist(x => x.Id == id)) return false;

            var contactDetailToDelete = await _unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().GetByIdAsync(id);

            await _unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().DeleteAsync(contactDetailToDelete);

            bool result = await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<bool> DeleteContactAsync(Guid id)
        {
            if (!_unitOfWork.GetRepository<Entities.DataModel.Contact>().Exist(x => x.Id == id)) return false;

            var contactToDelete = await _unitOfWork.GetRepository<Entities.DataModel.Contact>().GetByIdAsync(id);

            await _unitOfWork.GetRepository<Entities.DataModel.Contact>().DeleteAsync(contactToDelete);

            bool result = await _unitOfWork.SaveChangesAsync();

            if (result == true)
            {
                var relatedContactDetails = _unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().Query().Where(x => x.ContactId == id);

                foreach (var item in relatedContactDetails)
                {
                    await _unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().DeleteAsync(item);
                }

                await _unitOfWork.SaveChangesAsync();
            }

            return result;
        }

        public async Task<ICollection<ContactDto>> GetAllContactsAsync()
        {
            var queryContact = _unitOfWork.GetRepository<Entities.DataModel.Contact>().Query().AsNoTracking();
            var queryContactDetail = _unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().Query().AsNoTracking();

            var query = from contact in queryContact
                        select new ContactDto()
                        {
                            Id = contact.Id,
                            Name = contact.Name,
                            LastName = contact.LastName,
                            CompanyName = contact.CompanyName,
                            DateCreated = contact.DateCreated,
                            Details = queryContactDetail.Where(x => x.ContactId == contact.Id).Select(y => new ContactDetailDto()
                            {
                                Id = y.Id,
                                ContactId = y.ContactId,
                                TelephoneNumber = y.TelephoneNumber,
                                EMailAddress = y.EMailAddress,
                                Address = y.Address,
                                DateCreated = y.DateCreated
                            }).ToList()
                        };

            return await query.ToListAsync();
        }

        public async Task<ContactDto> GetContactByIdAsync(Guid id)
        {
            var queryContact = _unitOfWork.GetRepository<Entities.DataModel.Contact>().Query().AsNoTracking().Where(x => x.Id == id);

            var queryContactDetail = _unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().Query().AsNoTracking();

            var query = from contact in queryContact
                        select new ContactDto()
                        {
                            Id = contact.Id,
                            Name = contact.Name,
                            LastName = contact.LastName,
                            CompanyName = contact.CompanyName,
                            DateCreated = contact.DateCreated,
                            Details = queryContactDetail.Where(x => x.ContactId == contact.Id).Select(y => new ContactDetailDto()
                            {
                                Id = y.Id,
                                ContactId = y.ContactId,
                                TelephoneNumber = y.TelephoneNumber,
                                EMailAddress = y.EMailAddress,
                                Address = y.Address,
                                DateCreated = y.DateCreated
                            }).ToList()
                        };

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ICollection<ContactDetail>> GetContactDetailByContactIdAsync(Guid id)
        {
            var queryContactDetail = _unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().Query().AsNoTracking();

            return await queryContactDetail.ToListAsync();
        }

        public async Task<ContactDetail> GetContactDetailByIdAsync(long id)
        {
            var queryContactDetail = _unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().Query().AsNoTracking().Where(x => x.Id == id);

            return await queryContactDetail.FirstOrDefaultAsync();
        }

        public async Task<List<ContactDto>> SearchByAsync(Expression<Func<Entities.DataModel.Contact, bool>> searchBy)
        {
            var queryContact = _unitOfWork.GetRepository<Entities.DataModel.Contact>().Query().AsNoTracking().Where(searchBy);

            var queryContactDetail = _unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().Query().AsNoTracking();

            var query = from contact in queryContact
                        select new ContactDto()
                        {
                            Id = contact.Id,
                            Name = contact.Name,
                            LastName = contact.LastName,
                            CompanyName = contact.CompanyName,
                            DateCreated = contact.DateCreated,
                            Details = queryContactDetail.Where(x => x.ContactId == contact.Id).Select(y => new ContactDetailDto()
                            {
                                Id = y.Id,
                                ContactId = y.ContactId,
                                TelephoneNumber = y.TelephoneNumber,
                                EMailAddress = y.EMailAddress,
                                Address = y.Address,
                                DateCreated = y.DateCreated
                            }).ToList()
                        };

            return await query.ToListAsync();
        }

        public async Task<bool> UpdateContactAsync(ContactDtoUpdate contactDtoUpdate)
        {
            if (!_unitOfWork.GetRepository<Entities.DataModel.Contact>().Exist(x => x.Id == contactDtoUpdate.Id)) return false;

            var contactToUpdate = await _unitOfWork.GetRepository<Entities.DataModel.Contact>().GetByIdAsync(contactDtoUpdate.Id);

            contactToUpdate.Name = contactDtoUpdate.Name;
            contactToUpdate.LastName = contactDtoUpdate.LastName;
            contactToUpdate.CompanyName = contactDtoUpdate.CompanyName;

            await _unitOfWork.GetRepository<Entities.DataModel.Contact>().UpdateAsync(contactToUpdate);

            bool result = await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<bool> UpdateContactDetailAsync(ContactDetailDtoUpdate contactDetailDtoUpdate)
        {
            if (!_unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().Exist(x => x.Id == contactDetailDtoUpdate.Id)) return false;

            var contactDetailToUpdate = await _unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().GetByIdAsync(contactDetailDtoUpdate.Id);

            contactDetailToUpdate.TelephoneNumber = contactDetailDtoUpdate.TelephoneNumber;
            contactDetailToUpdate.EMailAddress = contactDetailDtoUpdate.EMailAddress;
            contactDetailToUpdate.Address = contactDetailDtoUpdate.Address;

            await _unitOfWork.GetRepository<Entities.DataModel.ContactDetail>().UpdateAsync(contactDetailToUpdate);

            bool result = await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}
