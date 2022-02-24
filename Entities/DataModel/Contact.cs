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
    [Table(name: "Application", Schema = "public")]
    [Index(nameof(Name), nameof(LastName), IsUnique = false)]
    public class Contact
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
                
        [Column("Name")]
        [Required(ErrorMessage = "{0} required")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters in length")]
        public string Name { get; set; }

        [Column("LastName")]
        [Required(ErrorMessage = "{0} required")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters in length")]
        public string LastName { get; set; }

        [Column("CompanyName")]
        [StringLength(maximumLength: 150, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 150 characters in length")]
        public string CompanyName { get; set; }

        [Column("DateCreated")]
        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = System.DateTime.UtcNow;                

        [ForeignKey("ContactId")]
        public virtual List<ContactDetail> ContactDetails { get; set; }
    }
}
