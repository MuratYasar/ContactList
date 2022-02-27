using Contracts;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Report.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.DAL.Concrete
{
    public class ReportOperation : IReportOperation
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;

        public ReportOperation(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Entities.DataModel.Report> AddReportAsync(ReportDtoInsert reportDtoInsert)
        {
            var entityReport = await _unitOfWork.GetRepository<Entities.DataModel.Report>().AddReturnEntityAsync(
                new Entities.DataModel.Report()
                {
                    ReportStatusId = 1,
                    ReportName = reportDtoInsert.ReportName,
                    Address = reportDtoInsert.Address,
                    DateRequested = DateTime.UtcNow
                });

            bool result = await _unitOfWork.SaveChangesAsync();

            if (result == true)
            {
                _logger.LogInfo($"{entityReport.entity.Id.ToString()} - new report added.");

                return entityReport.entity;
            }
            else
            {
                _logger.LogError($"An error has been occurred while adding a new report.");

                return null;
            }
        }

        public async Task<bool> DeleteReportByIdAsync(long id)
        {
            if (!_unitOfWork.GetRepository<Entities.DataModel.Report>().Exist(x => x.Id == id)) return false;

            var reportToDelete = await _unitOfWork.GetRepository<Entities.DataModel.Report>().GetByIdAsync(id);

            await _unitOfWork.GetRepository<Entities.DataModel.Report>().DeleteAsync(reportToDelete);

            bool result = await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<ReportDto> GetReportByIdAsync(long id)
        {
            var queryReport = _unitOfWork.GetRepository<Entities.DataModel.Report>().Query().AsNoTracking().Where(x => x.Id == id);

            var queryReportStatus = _unitOfWork.GetRepository<Entities.DataModel.ReportStatus>().Query().AsNoTracking();

            var query = from report in queryReport
                        join repstatus in queryReportStatus on report.ReportStatusId equals repstatus.Id
                        select new ReportDto()
                        {
                            Id = report.Id,
                            ReportStatusId = report.ReportStatusId,
                            ReportStatusName = repstatus.Name,
                            ReportName = report.ReportName,
                            Address = report.Address,
                            ContactCount = report.ContactCount,
                            PhoneRecordCount = report.PhoneRecordCount,
                            DateRequested = report.DateRequested,
                            DateCreated = report.DateCreated
                        };

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<ReportDto>> GetReportsAsync()
        {
            var queryReport = _unitOfWork.GetRepository<Entities.DataModel.Report>().Query().AsNoTracking();

            var queryReportStatus = _unitOfWork.GetRepository<Entities.DataModel.ReportStatus>().Query().AsNoTracking();

            var query = from report in queryReport
                        join repstatus in queryReportStatus on report.ReportStatusId equals repstatus.Id
                        select new ReportDto()
                        {
                            Id = report.Id,
                            ReportStatusId = report.ReportStatusId,
                            ReportStatusName = repstatus.Name,
                            ReportName = report.ReportName,
                            Address = report.Address,
                            ContactCount = report.ContactCount,
                            PhoneRecordCount = report.PhoneRecordCount,
                            DateRequested = report.DateRequested,
                            DateCreated = report.DateCreated
                        };

            return await query.ToListAsync();
        }

        public async Task<bool> UpdateReportAsync(ReportDtoUpdate reportDtoUpdate)
        {
            if (!_unitOfWork.GetRepository<Entities.DataModel.Report>().Exist(x => x.Id == reportDtoUpdate.Id)) return false;

            var reportEntityToUpdate = await _unitOfWork.GetRepository<Entities.DataModel.Report>().GetByIdAsync(reportDtoUpdate.Id);

            reportEntityToUpdate.ReportStatusId = reportDtoUpdate.ReportStatusId;
            reportEntityToUpdate.ReportName = reportDtoUpdate.ReportName;
            reportEntityToUpdate.Address = reportDtoUpdate.Address;
            reportEntityToUpdate.ContactCount = reportDtoUpdate.ContactCount;
            reportEntityToUpdate.PhoneRecordCount = reportDtoUpdate.PhoneRecordCount;
            reportEntityToUpdate.DateCreated = reportDtoUpdate.DateCreated;

            await _unitOfWork.GetRepository<Entities.DataModel.Report>().UpdateAsync(reportEntityToUpdate);

            bool result = await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}
