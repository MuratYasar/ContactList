using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ReportDtoInsert
    {
        [Required(ErrorMessage = "{0} required")]
        public byte ReportStatusId { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters in length")]
        public string ReportName { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(maximumLength: 250, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 250 characters in length")]
        public string Address { get; set; }        
    }
}
