using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataModel
{
    [Table(name: "ContactDetail", Schema = "public")]
    [Index(nameof(ContactId))]
    public class ContactDetail
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("ContactId")]
        [Required(ErrorMessage = "{0} required")]
        public Guid ContactId { get; set; }

        [Column("TelephoneNumber")]
        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(maximumLength: 15, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 15 characters in length")]
        public string TelephoneNumber { get; set; }

        [Column("EMailAddress")]
        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.EmailAddress)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters in length")]
        public string EMailAddress { get; set; }

        [Column("Address")]
        [Required(ErrorMessage = "{0} required")]
        [StringLength(maximumLength: 250, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 250 characters in length")]
        public string Address { get; set; }

        [Column("DateCreated")]
        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = System.DateTime.UtcNow;
        
        public Contact Contact { get; set; }
    }
}
