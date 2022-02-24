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

        [Column("DateCreated")]
        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = System.DateTime.UtcNow;
    }
}
