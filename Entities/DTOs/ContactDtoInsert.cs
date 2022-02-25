using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ContactDtoInsert
    {
        [Required(ErrorMessage = "{0} required")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters in length")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters in length")]
        public string LastName { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 150 characters in length")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(maximumLength: 15, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 15 characters in length")]
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.EmailAddress)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters in length")]
        public string EMailAddress { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(maximumLength: 250, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 250 characters in length")]
        public string Address { get; set; }
    }
}
