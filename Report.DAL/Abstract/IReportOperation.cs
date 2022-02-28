using Entities.DataModel;
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
        Task<List<ReportStatus>> GetReportStatusListAsync();

        Task<Entities.DataModel.Report> AddReportAsync(ReportDtoInsert reportDtoInsert);

        Task<List<ReportDto>> GetReportsAsync();

        Task<ReportDto> GetReportByIdAsync(long id);

        Task<bool> DeleteReportByIdAsync(long id);

        Task<bool> UpdateReportAsync(ReportDtoUpdate reportDtoUpdate);

        Task<Entities.DataModel.Report> PrepareFinalReportAsync(Entities.DataModel.Report report);
    }
}
