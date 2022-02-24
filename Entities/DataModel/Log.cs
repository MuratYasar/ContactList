using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.DataModel
{
    [Table(name: "Log", Schema = "public")]
    public class Log
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("MachineName")]
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 200 characters in length")]
        public string MachineName { get; set; }

        [Column("SiteName")]
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 200 characters in length")]
        public string SiteName { get; set; }

        [Column("Level")]
        [StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 20 characters in length")]
        public string Level { get; set; }

        [Column("Message")]
        [Required(ErrorMessage = "{0} required")]
        public string Message { get; set; }

        [Column("Logger")]
        [StringLength(maximumLength: 500, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 500 characters in length")]
        public string Logger { get; set; }

        [Column("CallSite")]
        public string CallSite { get; set; }

        [Column("Exception")]
        public string Exception { get; set; }              

        [Column("StackTrace")]
        public string StackTrace { get; set; }

        [Column("ErrorMessage")]
        public string ErrorMessage { get; set; }

        [Column("URL")]
        public string URL { get; set; }

        [Column("RequestMethod")]
        [StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 20 characters in length")]
        public string RequestMethod { get; set; }

        [Column("RequestUserAgent")]
        [StringLength(maximumLength: 300, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 300 characters in length")]
        public string RequestUserAgent { get; set; }

        [Column("TraceIdentifier")]
        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 100 characters in length")]
        public string TraceIdentifier { get; set; }

        [Column("CorrelationId")]
        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 100 characters in length")]
        public string CorrelationId { get; set; }

        [Column("ServerAddress")]
        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 100 characters in length")]
        public string ServerAddress { get; set; }

        [Column("RemoteAddress")]
        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 100 characters in length")]
        public string RemoteAddress { get; set; }        

        [Column("DateCreated")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = System.DateTime.UtcNow;
    }
}
