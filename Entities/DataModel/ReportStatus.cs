using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataModel
{
    [Table(name: "ReportStatus", Schema = "public")]
    public class ReportStatus
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [Column("Name")]
        [Required(ErrorMessage = "{0} required")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters in length")]
        public string Name { get; set; }

        [Column("DateCreated")]
        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = System.DateTime.UtcNow;
    }
}
