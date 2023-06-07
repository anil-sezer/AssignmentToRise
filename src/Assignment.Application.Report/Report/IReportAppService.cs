using Assignment.Application.Report.Report.Dto;
using Assignment.Application.Shared.Dto;
using Assignment.Domain.Entities;

namespace Assignment.Application.Report.Report;

public interface IReportAppService
{
    Task<AppServiceResult> CreateReportAsync();
    Task<AppServiceDataResult<Reports>> GetReportAsync(GetReportInput input);
    Task<AppServiceDataListResult<CommonDataOutput, Reports>> GetAllReportsAsync();
}
