﻿using Entities.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ContactDto
    {
        public Guid Id { get; set; }
                
        [Required(ErrorMessage = "{0} required")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters in length")]
        public string Name { get; set; }
                
        [Required(ErrorMessage = "{0} required")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters in length")]
        public string LastName { get; set; }
                
        [StringLength(maximumLength: 150, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 150 characters in length")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = System.DateTime.UtcNow;

        public List<ContactDetailDto> Details { get; set; }
    }
}
