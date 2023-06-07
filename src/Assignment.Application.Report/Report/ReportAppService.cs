using System.Net;
using Assignment.Application.Report.Report.Dto;
using Assignment.Application.Shared;
using Assignment.Application.Shared.Dto;
using Assignment.DataAccess.Kafka.Configurations;
using Assignment.DataAccess.PostgreSQL;
using Assignment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Application.Report.Report;

public class ReportAppService : AppServiceBase, IReportAppService
{
    private readonly ReportDbContext _dbContext;

    public ReportAppService(ReportDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<AppServiceResult> CreateReportAsync()
    {
        // Tell Kafka to contact bt to begin creating a report

        return Success();
    }
    
    public async Task<AppServiceDataResult<Reports>> GetReportAsync(GetReportInput input)
    {
        var r = await _dbContext.Reports.FindAsync(input.Id);
        if (r == null) return DataError<Reports>("Report not found", HttpStatusCode.BadRequest);

        return DataSuccess(r);
    }
    
    public async Task<AppServiceDataListResult<CommonDataOutput, Reports>> GetAllReportsAsync()
    {
        var r = await _dbContext.Reports.ToListAsync();
        
        return DataListSuccess(new CommonDataOutput(r.Count), r);
    }
}
