using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contact.DAL.Abstract
{
    public interface IContactOperation
    {        
        Task<ICollection<Entities.DTOs.ContactDto>> GetAllContactsAsync();
        
        Task<Entities.DTOs.ContactDto> GetContactByIdAsync(Guid id);

        Task<ICollection<Entities.DataModel.ContactDetail>> GetContactDetailByContactIdAsync(Guid id);

        Task<Entities.DataModel.ContactDetail> GetContactDetailByIdAsync(long id);
        
        Task<List<Entities.DTOs.ContactDto>> SearchByAsync(Expression<Func<Entities.DataModel.Contact, bool>> searchBy);
        
        Task<bool> AddContactAsync(Entities.DTOs.ContactDtoInsert contactDtoInsert);

        Task<bool> AddContactDetailAsync(Entities.DTOs.ContactDetailDtoInsert contactDetailDtoInsert);

        Task<bool> UpdateContactAsync(Entities.DTOs.ContactDtoUpdate contactDtoUpdate);

        Task<bool> UpdateContactDetailAsync(Entities.DTOs.ContactDetailDtoUpdate contactDetailDtoUpdate);

        Task<bool> DeleteContactAsync(Guid id);

        Task<bool> DeleteContacDetailtAsync(long id);
    }
}
