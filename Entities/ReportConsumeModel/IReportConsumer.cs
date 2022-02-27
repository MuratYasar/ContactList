using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ReportConsumeModel
{
    public interface IReportConsumer
    {
        long Id { get; set; }

        string ReportName { get; set; }
        
        string Address { get; set; }
        
        int ContactCount { get; set; }

        int PhoneRecordCount { get; set; }
        
        DateTime RequestedDate { get; set; }

        DateTime DateCreated { get; set; }

        string ReportStatusName { get; set; }
    }
}
