using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ReportConsumeModel
{
    public class ReportConsumer : IReportConsumer
    {
        public long Id { get; set; }

        public string ReportName { get; set; }

        public string Address { get; set; }

        public int ContactCount { get; set; }

        public int PhoneRecordCount { get; set; }

        public DateTime RequestedDate { get; set; }

        public DateTime DateCreated { get; set; }

        public string ReportStatusName { get; set; }
    }
}
