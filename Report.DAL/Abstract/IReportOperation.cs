using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.DAL.Abstract
{
    public interface IReportOperation
    {
        Task<Entities.DataModel.Report> AddReportAsync(ReportDtoInsert reportDtoInsert);

        Task<List<ReportDto>> GetReportsAsync();

        Task<ReportDto> GetReportByIdAsync(long id);

        Task<bool> DeleteReportByIdAsync(long id);

        Task<bool> UpdateReportAsync(ReportDtoUpdate reportDtoUpdate);
    }
}
