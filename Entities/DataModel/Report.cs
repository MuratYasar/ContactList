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
    [Table(name: "Report", Schema = "public")]
    [Index(nameof(ReportStatusId))]
    public class Report
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("ReportStatusId")]
        [Required(ErrorMessage = "{0} required")]
        public byte ReportStatusId { get; set; }

        [Column("ReportName")]
        [Required(ErrorMessage = "{0} required")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters in length")]
        public string ReportName { get; set; }

        [Column("Address")]
        [Required(ErrorMessage = "{0} required")]
        [StringLength(maximumLength: 250, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 250 characters in length")]
        public string Address { get; set; }

        [Column("ContactCount")]
        [Required(ErrorMessage = "{0} required")]
        public int ContactCount { get; set; }

        [Column("PhoneRecordCount")]
        [Required(ErrorMessage = "{0} required")]
        public int PhoneRecordCount { get; set; }

        [Column("DateCreated")]
        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = System.DateTime.UtcNow;
    }
}
